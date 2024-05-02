using HarmonyLib;
using UnityEngine;

namespace AudioSettings.Patches
{
    public static partial class SFXInstance_Patches
    {
        [HarmonyPatch(typeof(SFXInstance), "PlayCheckoutSFX")]
        public static class SFXInstance_PlayCheckoutSFX_Patch
        {
            public static void Prefix(SFXInstance __instance)
            {
                Plugin.CheckoutCompleteVolume.Add(__instance.transform.GetChild(1).GetComponent<AudioSource>(), 0.3f);
            }
        }
    }
}
