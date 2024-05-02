using HarmonyLib;

namespace AudioSettings.Patches
{
    public static partial class SFXManager_Patches
    {
        [HarmonyPatch(typeof(SFXManager), "PlayMouseClickSFX")]
        public static class SFXManager_PlayMouseClickSFX_Patch
        {
            public static void Prefix(SFXManager __instance)
            {
                Plugin.MouseClickVolume.Add(__instance.m_MouseClick, 0.5f);
            }
        }
    }
}
