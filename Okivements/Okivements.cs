using OWML.ModHelper;
using OWML.Common;
using System;
using UnityEngine;
using System.Reflection;

namespace Okivements
{
    public class Okivements : ModBehaviour
    {
        private static IModHelper modHelper = null;

        private void Awake()
        {
        }

        private void Start()
        {
            modHelper = ModHelper;
            ModHelper.Console.WriteLine($"My mod {nameof(Okivements)} is loaded!", MessageType.Success);
            ModHelper.Events.Subscribe<Flashlight>(Events.AfterStart);
            ModHelper.Events.Event += OnEvent;

            ModHelper.HarmonyHelper.AddPrefix<Achievements>("Earn", typeof(Okivements), nameof(PatchedMethod));
        }

        public static void PatchedMethod(Achievements.Type type)
        {
            modHelper.Console.WriteLine("Earned the " + type + " achievement! ");
        }

        private void OnEvent(MonoBehaviour behaviour, Events ev)
        {
            ModHelper.Console.WriteLine("Behaviour name: " + behaviour.name);
            if (behaviour.GetType() == typeof(Flashlight) && ev == Events.AfterStart)
            {
                ModHelper.Console.WriteLine("Flashlight has started!");
                /*Achievements.Earn(Achievements.Type.HARMONIC_CONVERGENCE);

                for(var i = 0; i < 17; i++)
                {
                    //ModHelper.Console.WriteLine("i: " + i);
                    var field = typeof(Achievements).GetField("s_isEarned", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                    //ModHelper.Console.WriteLine("Field: " + field + " field null: " + field == null);
                    var isEarned = (bool[])field.GetValue(null);
                    //ModHelper.Console.WriteLine("is earned: " + isEarned);
                    ModHelper.Console.WriteLine("Achievement " + Enum.GetName(typeof(Achievements.Type), i) + " is achieved: " + isEarned[i]);
                }
                */
            }
        }
    }
}
