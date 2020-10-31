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

		public override void Configure(IModConfig config)
		{
            var fontName = config.GetSettingsValue<string>("Font Name");
            _font = Font.CreateDynamicFontFromOSFont(fontName, 300);
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
        }

        private void ReplaceFonts()
        {
            var texts = FindObjectsOfType<Text>();
            foreach (var text in texts)
            {
                text.font = _font;
            }
        }
    }
}
