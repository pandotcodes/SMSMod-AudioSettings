using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using MyBox;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace AudioSettings
{
    [BepInPlugin(TRANSLATOR_GUID, TRANSLATOR_NAME, TRANSLATOR_VERSION)]
    public class Translator : BaseUnityPlugin
    {
        public const string TRANSLATOR_GUID = PluginInfo.PLUGIN_GUID + ".Translations";
        public const string TRANSLATOR_NAME = PluginInfo.PLUGIN_NAME + " Translations";
        public const string TRANSLATOR_VERSION = PluginInfo.PLUGIN_VERSION;
        public static ConfigFile Config { get; private set; }
        public void Awake()
        {
            //Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            Config = base.Config;
        }
    }
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static AudioVolumeSlider CarVolume { get; set; }
        public static AudioVolumeSlider AmbienceVolume { get; set; }
        public static AudioVolumeSlider CashRegisterVolume { get; set; }
        public static AudioVolumeSlider CheckoutCompleteVolume { get; set; }
        public static AudioVolumeSlider ScanningProductVolume { get; set; }
        public static AudioVolumeSlider CheckoutWarningVolume { get; set; }
        public static AudioVolumeSlider TakingChangeVolume { get; set; }
        public static AudioVolumeSlider BoxSoundsVolume { get; set; }
        public static AudioVolumeSlider MouseClickVolume { get; set; }
        public static AudioVolumeSlider MovingProductVolume { get; set; }
        public static AudioVolumeSlider LightSwitchVolume { get; set; }
        public static AudioVolumeSlider MainMenuVolume { get; set; }
        public static List<AudioVolumeSlider> Sliders { get; set; } = new();
        private void Awake()
        {
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            harmony.PatchAll();
            SceneManager.sceneLoaded += (a, b) =>
            {
                Sliders.ForEach(x =>
                {
                    x.Volume.SettingChanged -= x.Volume_SettingChanged;
                });
                CarVolume = new AudioVolumeSlider(Config, "Cars", "The volume of the cars on the roads.");
                AmbienceVolume = new AudioVolumeSlider(Config, "Ambience", "The volume of the background noises.");
                CashRegisterVolume = new AudioVolumeSlider(Config, "Cash Register", "The volume of the opening and closing of cash registers.");
                CheckoutCompleteVolume = new AudioVolumeSlider(Config, "Checkout Complete", "The volume of the sound effect for a completed checkout.");
                ScanningProductVolume = new AudioVolumeSlider(Config, "Scanning Product", "The volume of the beeping sound when scanning a product.");
                CheckoutWarningVolume = new AudioVolumeSlider(Config, "Checkout Ding", "The volume of the ding sound that's played when a customer is waiting for checkout.");
                TakingChangeVolume = new AudioVolumeSlider(Config, "Taking Change", "The volume of taking coins or bills out of the cash register.");
                BoxSoundsVolume = new AudioVolumeSlider(Config, "Box Sounds", "The volume of the sounds of dropping and picking up boxes.");
                MouseClickVolume = new AudioVolumeSlider(Config, "Mouse Clicks", "The volume of the mouse click sounds played when using the computer.");
                MovingProductVolume = new AudioVolumeSlider(Config, "Moving Product", "The volume of moving products from and to shelves.");
                LightSwitchVolume = new AudioVolumeSlider(Config, "Light Switch", "The volume of pressing the light switch.");
                MainMenuVolume = new AudioVolumeSlider(Config, "Main Menu Music", "The volume of the music in the main menu.");

                Sliders = new() {
                    CarVolume,
                    AmbienceVolume,
                    CashRegisterVolume,
                    CheckoutCompleteVolume,
                    ScanningProductVolume,
                    CheckoutWarningVolume,
                    TakingChangeVolume,
                    BoxSoundsVolume,
                    MouseClickVolume,
                    MovingProductVolume,
                    LightSwitchVolume,
                    MainMenuVolume
                };
                Sliders.ForEach(x => x.Apply());
            };
        }
        public void OnGUI()
        {
            int width = 730; // In-Game Width
            if (Singleton<EscapeMenuManager>.Instance != null)
            {
                if (!Singleton<EscapeMenuManager>.Instance.m_Paused) return;
            } else
            {
                width = 625; // Main Menu Width
            }
            AudioVolumeSlider.Reset();
            Sliders.ForEach(x => x.InitUI(width));
        }
        static AudioSource backgroundSfx;
        static AudioSource mainMenuSfx;
        public void Update()
        {
            if (Singleton<EscapeMenuManager>.Instance != null)
            {
                if (backgroundSfx == null)
                {
                    Console.WriteLine("Looking for ambience object");
                    var go = GameObject.Find("Background SFX");
                    if (go != null)
                    {
                        backgroundSfx = go.GetComponent<AudioSource>();
                        if (backgroundSfx != null)
                        {
                            AmbienceVolume.Add(backgroundSfx, 0.8f);
                        }
                    }
                }
            }
            else
            {
                if (mainMenuSfx == null)
                {
                    Console.WriteLine("Looking for music object");
                    var go = GameObject.Find("Main Menu Music");
                    if (go != null)
                    {
                        mainMenuSfx = go.GetComponent<AudioSource>();
                        if (mainMenuSfx != null)
                        {
                            MainMenuVolume.Add(mainMenuSfx, 0.2f);
                        }
                    }
                }
            }
        }
    }
}
