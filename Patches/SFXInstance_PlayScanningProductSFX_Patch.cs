using HarmonyLib;
using UnityEngine;

namespace AudioSettings.Patches
{
    public static partial class SFXInstance_Patches
    {
        [HarmonyPatch(typeof(SFXInstance), "PlayScanningProductSFX")]
        public static class SFXInstance_PlayScanningProductSFX_Patch
        {
            public static void Prefix(SFXInstance __instance)
            {
                Plugin.ScanningProductVolume.Add(__instance.transform.GetChild(0).GetComponent<AudioSource>(), 0.3f);
            }
        }
    }
}
