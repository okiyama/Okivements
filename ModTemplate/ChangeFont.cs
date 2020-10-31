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
        private void Awake()
        {
            // You won't be able to access OWML's mod helper in Awake.
            // So you probably don't want to do anything here.
            // Use Start() instead.
        }

        private void Start()
        {
            // Starting here, you'll have access to OWML's mod helper.
            ModHelper.Console.WriteLine($"My mod {nameof(ChangeFont)} is loaded!", MessageType.Success);

            var font = Font.CreateDynamicFontFromOSFont("AR BLANCA", 10);

            var texts = FindObjectsOfType<Text>();
            foreach (var text in texts)
            {
                text.font = font;
            }
        }
    }
}
