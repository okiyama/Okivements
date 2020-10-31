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
        const int _fontResizeAmount = 1;
        const float _lineHeightAmount = 0.2f;
        private readonly Dictionary<string, IModInputCombination> _inputs = new Dictionary<string, IModInputCombination>();

        public override void Configure(IModConfig config)
		{
            var fontName = config.GetSettingsValue<string>("Font Name");
            _font = Font.CreateDynamicFontFromOSFont(fontName, 100);

            foreach (var input in _inputs)
            {
                ModHelper.Input.UnregisterCombination(input.Value);
            }
            foreach (var key in config.Settings.Keys)
            {
                var value = config.GetSettingsValue<string>(key);
                if (!string.IsNullOrEmpty(value) && key != "Font Name")
                {
                    _inputs[key] = ModHelper.Input.RegisterCombination(this, key, value);
                }
            }
        }

        private void Start()
        {
            ModHelper.Console.WriteLine($"{nameof(ChangeFont)} is loaded!", MessageType.Success);
        }

        private void Update()
        {
            if (ModHelper.Input.IsNewlyPressed(_inputs["Replace fonts"]))
            {
                ReplaceFonts();
            }
            if (ModHelper.Input.IsNewlyPressed(_inputs["Increase font size"]))
            {
                ResizeFonts(_fontResizeAmount);
            }
            if (ModHelper.Input.IsNewlyPressed(_inputs["Decrease font size"]))
            {
                ResizeFonts(-_fontResizeAmount);
            }
            if (ModHelper.Input.IsNewlyPressed(_inputs["Increase line height"]))
            {
                LineHeight(_lineHeightAmount);
            }
            if (ModHelper.Input.IsNewlyPressed(_inputs["Decrease line height"]))
            {
                LineHeight(-_lineHeightAmount);
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

        private void LineHeight(float amount)
        {
            var texts = FindObjectsOfType<Text>();
            foreach (var text in texts)
            {
                text.lineSpacing += amount;
            }
        }
    }
}
