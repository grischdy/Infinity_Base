using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinity_Base.Models
{
    public class AML_Roles
    {
        public AML_Roles()
        {
            RoleList = new List<Hilfsklassen.RoleListDB>() 
            {};
        }
        public List<Hilfsklassen.RoleListDB> RoleList { get; set; }
    }
}
