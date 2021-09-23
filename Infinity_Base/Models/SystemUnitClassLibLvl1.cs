using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinity_Base.Models
{
    public class SystemUnitClassLibLvl1
    {


            public int Id { get; set; }
            public int parentId { get; set; }
            public string Title { get; set; }
            public virtual List<SystemUnitClassLibLvl2> SystemUnitClassLibLvl2 { get; set; }
        
    }
}
