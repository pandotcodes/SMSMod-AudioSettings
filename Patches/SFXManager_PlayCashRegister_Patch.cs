using HarmonyLib;

namespace AudioSettings.Patches
{
    public static partial class SFXManager_Patches
    {
        [HarmonyPatch(typeof(SFXManager), "PlayCashRegister")]
        public static class SFXManager_PlayCashRegister_Patch
        {
            public static void Prefix(SFXManager __instance)
            {
                Plugin.CashRegisterVolume.Add(__instance.m_CashRegisterOpen, 0.7f);
                Plugin.CashRegisterVolume.Add(__instance.m_CashRegisterClose, 0.7f);
            }
        }
    }
}
