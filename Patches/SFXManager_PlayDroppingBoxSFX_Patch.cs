using HarmonyLib;

namespace AudioSettings.Patches
{
    public static partial class SFXManager_Patches
    {
        [HarmonyPatch(typeof(SFXManager), "PlayDroppingBoxSFX")]
        [HarmonyPatch(typeof(SFXManager), "PlayPickingUpBoxSFX")]
        public static class SFXManager_PlayDroppingBoxSFX_Patch
        {
            public static void Prefix(SFXManager __instance)
            {
                Plugin.BoxSoundsVolume.Add(__instance.m_DroppingBoxSFX, 0.8f);
                Plugin.BoxSoundsVolume.Add(__instance.m_PickingUpBox, 0.2f);
            }
        }
    }
}