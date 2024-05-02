using HarmonyLib;

namespace AudioSettings.Patches
{
    public static partial class SFXManager_Patches
    {
        [HarmonyPatch(typeof(SFXManager), "PlayCoinSFX")]
        [HarmonyPatch(typeof(SFXManager), "PlayMoneyPaperSFX")]
        public static class SFXManager_PlayCoinSFX_Patch
        {
            public static void Prefix(SFXManager __instance)
            {
                Plugin.TakingChangeVolume.Add(__instance.m_CoinSFX, 1);
                Plugin.TakingChangeVolume.Add(__instance.m_MoneyPaperSFX, 1);
            }
        }
    }
}
