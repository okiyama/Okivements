using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using OWML.Utils;

namespace Okivements.test
{
    [TestFixture]
    class JsonTests
    {
        [Test]
        public void loadAchievements()
        {
            var path = "achievements.json";
            Assert.True(File.Exists(path));

            var achievements = JsonHelper.LoadJsonObject<List<Achievement>>(path);
            Assert.NotNull(achievements);
            Assert.IsNotEmpty(achievements);
        }
    }
}
