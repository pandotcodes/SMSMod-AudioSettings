using HarmonyLib;

namespace AudioSettings.Patches
{
    public static partial class SFXManager_Patches
    {
        [HarmonyPatch(typeof(SFXManager), "PlaySwitchSFX")]
        public static class SFXManager_PlaySwitchSFX_Patch
        {
            public static void Prefix(SFXManager __instance)
            {
                Plugin.LightSwitchVolume.Add(__instance.m_SwitchOn, 1);
                Plugin.LightSwitchVolume.Add(__instance.m_SwitchOff, 1);
            }
        }
    }
}
