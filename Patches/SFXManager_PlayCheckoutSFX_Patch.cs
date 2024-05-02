using HarmonyLib;

namespace AudioSettings.Patches
{
    public static partial class SFXManager_Patches
    {
        [HarmonyPatch(typeof(SFXManager), "PlayCheckoutSFX")]
        public static class SFXManager_PlayCheckoutSFX_Patch
        {
            public static void Prefix(SFXManager __instance)
            {
                Plugin.CheckoutCompleteVolume.Add(__instance.m_Checkout, 0.7f);
            }
        }
    }
}
