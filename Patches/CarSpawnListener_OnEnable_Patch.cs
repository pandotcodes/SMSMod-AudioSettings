using HarmonyLib;
using System;
using UnityEngine;

namespace AudioSettings.Patches
{
    [HarmonyPatch(typeof(CarSpawnListener), "OnEnable")]
    public static class CarSpawnListener_OnEnable_Patch
    {
        public static void Postfix(CarSpawnListener __instance)
        {
            Plugin.CarVolume.Add(__instance.GetComponent<AudioSource>(), 0.1f);
        }
    }
}
