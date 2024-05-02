using HarmonyLib;

namespace AudioSettings.Patches
{
    public static partial class SFXManager_Patches
    {
        [HarmonyPatch(typeof(SFXManager), "PlayScanningProductSFX")]
        public static class SFXManager_PlayScanningProductSFX_Patch
        {
            public static void Prefix(SFXManager __instance)
            {
                Plugin.ScanningProductVolume.Add(__instance.m_ScanningProduct, 0.2f);
            }
        }
    }
}
