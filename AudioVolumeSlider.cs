using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AudioSettings
{
    public class AudioVolumeSlider
    {
        public static GUIStyleState font { get; set; } = new GUIStyleState { textColor = Color.white };
        public static int SliderCount = 0;
        public List<(AudioSource source, float multiplier)> Sources = new List<(AudioSource source, float multiplier)>();
        private float ValueLastTick { get; set; }
        private string Name { get; set; }
        private string Description { get; set; }
        public static void Reset()
        {
            SliderCount = 0;
        }
        public ConfigEntry<float> Volume { get; set; }
        public void Add(AudioSource source, float multiplier)
        {
            if (Sources.Any(x => x.source == source))
            {
                return;
            }
            Console.WriteLine("AudioSource on \"" + source.gameObject.name + "\" sorted into \"" + Name + "\".");
            Sources.Add((source, multiplier));
            Apply();
        }
        public void Apply()
        {
            var vol = Volume.Value;
            Console.WriteLine("Applying volume setting \"" + Name + "\": " + vol + ", " + Sources.Count + " sources, " + Plugin.Sliders.Count + " sliders.");
            foreach (var item in Sources)
            {
                item.source.volume = item.multiplier * vol;
            }
        }
        public AudioVolumeSlider(ConfigFile Config, string name, string description)
        {
            Name = name;
            Description = description;
            Console.WriteLine("Volume slider \"" + name + "\" initiated.");
            Volume = Config.Bind(name, "Volume", 1f, description + "\nValue goes from 0 to 1.");
            Volume.SettingChanged += Volume_SettingChanged;
            ValueLastTick = Volume.Value;
        }

        public void Volume_SettingChanged(object sender, EventArgs e)
        {
            Apply();
        }

        public void InitUI(int width)
        {
            //var go = GameObject.Find("Ingame Canvas");
            //if (go == null) return null;
            //var slider = DefaultControls.CreateSlider(DefaultControls.Resources.);
            ////slider.SetActive(false);
            //slider.transform.position = new Vector3(0, 0, SliderCount * 25);
            //slider.transform.SetParent(go.transform);
            //return slider;

            var rectNameLabel = new Rect(10, SliderCount * 50 + 10, width * (2f / 7f), 25);
            var rectDescLabel = new Rect(10, SliderCount * 50 + 30, width, 25);
            var rectPercLabel = new Rect(width * (2f / 7f), SliderCount * 50 + 10, width * (0.5f / 7f), 25);

            GUI.Label(rectNameLabel, Name, new GUIStyle { alignment = TextAnchor.MiddleLeft, normal = font });
            GUI.Label(rectPercLabel, Volume.Value.ToString("##0%"), new GUIStyle { alignment = TextAnchor.MiddleRight, normal = font });
            GUI.Label(rectDescLabel, Description, new GUIStyle { alignment = TextAnchor.MiddleLeft, normal = font });

            var rect = new Rect(width * (2.5f / 7f) + 10, SliderCount * 50 + 16, width * (4.5f / 7f), 25);
            var style = new GUIStyle(GUI.skin.horizontalSlider);

            style.normal.background = MakeTex(600, 1, Volume.Value, Color.white, Color.gray);
            style.alignment = TextAnchor.MiddleLeft;

            var value = GUI.HorizontalSlider(rect, Volume.Value, 0, 1, style, GUI.skin.horizontalScrollbarThumb);
            if (value != ValueLastTick)
            {
                Volume.Value = value;
                ValueLastTick = value;
            }
            SliderCount++;
        }
        Dictionary<string, Texture2D> TextureCache { get; set; } = new();
        private Texture2D MakeTex(int width, int height, float midPointPercentage, Color col1, Color col2)
        {
            string texKey = width + "x" + height + ":" + midPointPercentage;
            if(TextureCache.ContainsKey(texKey)) return TextureCache[texKey];
            Color[] pix = new Color[width * height];
            int midPoint = (int)(width * midPointPercentage);

            // Left side is white
            for (int x = 0; x < midPoint; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    pix[x + y * width] = col1;
                }
            }

            // Right side is dark gray
            for (int x = midPoint; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    pix[x + y * width] = col2;
                }
            }

            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            TextureCache.Add(texKey, result);
            return result;
        }
    }
}
