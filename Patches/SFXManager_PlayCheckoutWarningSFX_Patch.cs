using HarmonyLib;

namespace AudioSettings.Patches
{
    public static partial class SFXManager_Patches
    {
        [HarmonyPatch(typeof(SFXManager), "PlayCheckoutWarningSFX")]
        public static class SFXManager_PlayCheckoutWarningSFX_Patch
        {
            public static void Prefix(SFXManager __instance)
            {
                Plugin.CheckoutWarningVolume.Add(__instance.m_CheckoutWarning, 0.5f);
            }
        }
    }
}
