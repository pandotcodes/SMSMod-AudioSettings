using HarmonyLib;

namespace AudioSettings.Patches
{
    public static partial class SFXManager_Patches
    {
        [HarmonyPatch(typeof(SFXManager), "PlayPlacingProductSFX")]
        public static class SFXManager_PlayPlacingProductSFX_Patch
        {
            public static void Prefix(SFXManager __instance)
            {
                Plugin.MovingProductVolume.Add(__instance.m_PlacingProduct, 0.7f);
            }
        }
    }
}
