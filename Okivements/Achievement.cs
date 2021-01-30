using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OWML.ModHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Okivements
{
    public class Achievement
    {
        [JsonConverter(typeof(StringEnumConverter))]
        //TODO this won't work for custom achievements, maybe have a translation layer?
        public Achievements.Type Type { get; set; }
        public bool Earned { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public ModHelper ModHelper { get; set; }
        //Custom achievements or not, false == Steam achievement
        public bool Custom { get; set; }
    }

}
