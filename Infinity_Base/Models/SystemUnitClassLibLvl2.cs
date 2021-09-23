using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinity_Base.Models
{
    public class SystemUnitClassLibLvl2
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int lvl1Id { get; set; }
        public virtual SystemUnitClassLibLvl1 SystemUnitClassLibLvl1 { get; set; }
    }
}
