using OWML.ModHelper;
using OWML.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace ModTemplate
{
    public class ChangeFont : ModBehaviour
    {
        Font _font;

        private void Start()
        {
            ModHelper.Console.WriteLine($"My mod {nameof(ChangeFont)} is loaded!", MessageType.Success);

            _font = Font.CreateDynamicFontFromOSFont("AR BLANCA", 10);
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
