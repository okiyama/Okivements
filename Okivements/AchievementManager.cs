using OWML.Common;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using System.Collections;

namespace Okivements
{
    public class AchievementManager
    {

        private List<Achievement> Achievements;
        private IModHelper ModHelper;
        private AudioSource StandardSoundEffect;
        private List<AudioSource> EnhancedSoundEffects;
        private System.Random Random = new System.Random();
        private ArrayList EnhancedSoundEffectsPlayed = new ArrayList();

        public AchievementManager(IModHelper modHelper)
        {
            this.ModHelper = modHelper;
            this.Achievements = InitAchievements();
            this.StandardSoundEffect = InitStandardSoundEffect();
            this.EnhancedSoundEffects = InitEnhancedSoundEffects();
        }

        private List<AudioSource> InitEnhancedSoundEffects()
        {
            var enchancedSoundEffectsFolderPath = "resources/enhanced_sound_effects/";
            return Directory.GetFiles(ModHelper.Manifest.ModFolderPath + "/" + enchancedSoundEffectsFolderPath)
                .Select(path => createAudioSourceFromPath(enchancedSoundEffectsFolderPath + Path.GetFileName(path)))
                .ToList();
        }

        private AudioSource InitStandardSoundEffect()
        {
            return createAudioSourceFromPath("resources/standard_sound_effect.wav");
        }

        private AudioSource createAudioSourceFromPath(string path)
        {
            var gameObject = new GameObject().AddComponent<AudioSource>();
            gameObject.clip = ModHelper.Assets.GetAudio(path);
            return gameObject;
        }

        private List<Achievement> InitAchievements()
        {
            var field = typeof(Achievements).GetField("s_isEarned", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            var isEarned = (bool[])field.GetValue(null);

            List<Achievement> achievements = ModHelper.Storage.Load<List<Achievement>>("resources/achievements.json");
            foreach(var achievement in achievements)
            {
                achievement.Earned = isEarned[(int) achievement.Type];
                ModHelper.Console.WriteLine("Achievement: " + achievement.Name + " earned: " + achievement.Earned);

                achievement.Icon = ModHelper.Assets.GetTexture("resources/icons/" + achievement.IconPath);
            }

            return achievements;
        }

        public void EarnAchivement(Achievements.Type type)
        {
            var achievement = GetByType(type);
            if(!achievement.Earned)
            {
                ModHelper.Console.WriteLine("Earned the " + type + " achievement for the first time! ");
                if(ModHelper.Config.GetSettingsValue<bool>("Enable Enhanced Sound Effects")) {
                    int index;
                    do
                    {
                        index = Random.Next(EnhancedSoundEffects.Count);
                    } while (!EnhancedSoundEffectsPlayed.Contains(index) || EnhancedSoundEffectsPlayed.Count >= EnhancedSoundEffects.Count);

                    EnhancedSoundEffects[index].Play();
                    EnhancedSoundEffectsPlayed.Add(index);

                } else
                {
                    StandardSoundEffect.Play();
                }
            } else
            {
                ModHelper.Console.WriteLine("Earned the " + type + " achievement! But not for the first time.");
            }

            achievement.Earned = true;
        }

        public bool IsEarned(Achievements.Type type)
        {
            return GetByType(type).Earned;
        }

        public Achievement GetByType(Achievements.Type type)
        {
            return Achievements.Find(a => a.Type == type);
        }
    }
}
