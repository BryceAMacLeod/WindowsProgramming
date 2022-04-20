using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryAppp_GRPB.Models
{
    public class GameCharacter
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }

        public GameCharacter(string name, string desc, int lvl)
        {
            Name = name;
            Description = desc; 
            Level = lvl;    
        }
    }
}
