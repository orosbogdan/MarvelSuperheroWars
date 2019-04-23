using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperheroWars
{
    public class Planet
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Modifier Modifiers { get; set; }


        public override string ToString()
        {
            return String.Format("{0} {1} {2} " + Environment.NewLine + "Modifiers {3}" + Environment.NewLine, 
                                Id,Name,Description,JsonConvert.SerializeObject(Modifiers));
        }
    }
}
