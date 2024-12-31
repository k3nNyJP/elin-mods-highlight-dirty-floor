using UnityEngine;
using BepInEx;
using HarmonyLib;

internal static class ModInfo
{
    internal const string Guid = "k3nny_51rcy.elinplugins.highlightdirtyfloor";
    internal const string Name = "Highlight Dirty Floor";
    internal const string Version = "0.0.1";
}

[BepInPlugin(ModInfo.Guid, ModInfo.Name, ModInfo.Version)]
public class MyFirstMod : BaseUnityPlugin
{
    private void Start()
    {
        var harmony = new Harmony(ModInfo.Guid);
        harmony.PatchAll();
    }
}

[HarmonyPatch(typeof(Player), nameof(Player.MarkMapHighlights))]
class PlayerMarkMapHighlightsPatch
{
    static void Postfix(Player __instance)
    {
        if (__instance.currentHotItem?.Thing?.trait is TraitBroom)
        {
            SetHighlightDirtyCells();
        }
    }

    static void SetHighlightDirtyCells()
    {
        EClass._map.ForeachSphere(EClass.pc.pos.x, EClass.pc.pos.z, EClass.player.lightRadius, delegate (Point p)
        {
            if (TaskClean.CanClean(p))
            {
                p.SetHighlight(8);
            }
        });
    }
}
