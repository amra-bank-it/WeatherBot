using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBot.Model
{
    public class WeatherAttr
    {
        public int id { get; set; }
        public string Cities { get; set; }
        public Nullable<int> Temperature_in_degrees_Celsius { get; set; }
        public string Weather { get; set; }
        public Nullable<System.DateTime> Stamp { get; set; }
    }
}
