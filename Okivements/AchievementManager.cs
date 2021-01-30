using OWML.Common;
using OWML.ModHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Okivements
{
    public class AchievementManager
    {

        private List<Achievement> achievements;
        private IModHelper modHelper;

        public AchievementManager(IModHelper modHelper)
        {
            this.modHelper = modHelper;
            this.achievements = initAchievements();
        }

        //TODO load icons into game objects or textures or whatever

        private List<Achievement> initAchievements()
        {
            var field = typeof(Achievements).GetField("s_isEarned", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            var isEarned = (bool[])field.GetValue(null);

            modHelper.Console.WriteLine("about to load achievements");
            //TODO this load fails
            List<Achievement> achievements = modHelper.Storage.Load<List<Achievement>>("achievements.json");
            modHelper.Console.WriteLine("loaded " + achievements.Count + " achievements");
            foreach(var achievement in achievements)
            {
                modHelper.Console.WriteLine("Setting earned for " + achievement.Name);
                achievement.Earned = isEarned[(int) achievement.Type];
                modHelper.Console.WriteLine("Achievement: " + achievement.Name + " earned: " + achievement.Earned);
            }

            return achievements;
        }

        public bool isEarned(Achievements.Type type)
        {
            return getByType(type).Earned;
        }

        public Achievement getByType(Achievements.Type type)
        {
            return achievements.Find(a => a.Type == type);
        }

        public void earnAchivement(Achievements.Type type)
        {
            getByType(type).Earned = true;
        }
    }
}
