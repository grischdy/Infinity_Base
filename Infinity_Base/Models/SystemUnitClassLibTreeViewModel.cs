using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Infinity_Base.Models
{
    public class SystemUnitClassLibTreeViewModel
    {
        public class Category
        {
            //Cat Id  
            public int ID { get; set; }

            //Cat Name  
            public string Name { get; set; }

            //Cat Description  
            public string Description { get; set; }

            //represnts Parent ID and it's nullable  
            public int? Pid { get; set; }
            [ForeignKey("Pid")]
            public virtual Category Parent { get; set; }
            public virtual ICollection<Category> Childs { get; set; }
        }
    }
}
