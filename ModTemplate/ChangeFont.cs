using OWML.ModHelper;
using OWML.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using OWML.ModHelper.Events;

namespace ModTemplate
{
    public class ChangeFont : ModBehaviour
    {
        Font _font;
        int _fontSizeAmount = 1;

		public override void Configure(IModConfig config)
		{
            var fontName = config.GetSettingsValue<string>("Font Name");
            _font = Font.CreateDynamicFontFromOSFont(fontName, 100);
        }

        private void Start()
        {
            ModHelper.Console.WriteLine($"{nameof(ChangeFont)} is loaded!", MessageType.Success);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                ReplaceFonts();
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                ResizeFonts(_fontSizeAmount);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                ResizeFonts(-_fontSizeAmount);
            }
        }

        private void ReplaceFonts()
        {
            var texts = FindObjectsOfType<Text>();
            foreach (var text in texts)
            {
                text.font = _font;
            }
        }
        private void ResizeFonts(int amount)
        {
            var texts = FindObjectsOfType<Text>();
            foreach (var text in texts)
            {
                text.fontSize += amount;
            }
        }
    }
}
