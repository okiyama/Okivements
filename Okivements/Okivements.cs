using OWML.ModHelper;
using OWML.Common;
using UnityEngine;

namespace Okivements
{
    public class Okivements : ModBehaviour
    {
        private static new IModHelper ModHelper = null;
        private static AchievementManager AchievementManager = null;

        private void Awake()
        {
        }

        private void Start()
        {
            ModHelper = base.ModHelper;
            ModHelper.Manifest.ModFolderPath = ModHelper.Manifest.ModFolderPath.Replace("\\", "/");
            base.ModHelper.Events.Subscribe<Flashlight>(Events.AfterStart);
            base.ModHelper.Events.Event += OnEvent;

            base.ModHelper.HarmonyHelper.AddPrefix<Achievements>("Earn", typeof(Okivements), nameof(PatchedMethod));
        }

        public static void PatchedMethod(Achievements.Type type)
        {
            AchievementManager.EarnAchivement(type);
        }

        private void OnEvent(MonoBehaviour behaviour, Events ev)
        {
            base.ModHelper.Console.WriteLine("Behaviour name: " + behaviour.name);
            if (behaviour.GetType() == typeof(Flashlight) && ev == Events.AfterStart)
            {
                Achievements.ResetAll();
                base.ModHelper.Console.WriteLine("Flashlight has started!");
                AchievementManager = AchievementManager ?? new AchievementManager(ModHelper);
            }
        }
    }
}
