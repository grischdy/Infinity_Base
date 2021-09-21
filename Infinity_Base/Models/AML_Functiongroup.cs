using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aml.Engine.AmlObjects;
using Aml.Engine.CAEX;
using Aml.Engine.CAEX.Extensions;
using Aml.Engine.Services;
using Aml.Engine.Xml.Extensions;
using Infinity_Base.Hilfsklassen;

namespace Infinity_Base.Models
{
    public class AML_Functiongroup
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Zusatzinformationen { get; set; }

        public AML_Functiongroup()
        {
            var myDoc = globals._myglobalDoc;

            RoleList = new List<Hilfsklassen.RoleListDB>()
            {new Hilfsklassen.RoleListDB { Text = "Empty"} };

            foreach (var rc1 in myDoc.CAEXFile.RoleClassLib[1])
            {
                foreach (var rc2 in rc1)
                {
                    var tc = new Hilfsklassen.RoleListDB {Text = rc2.Name};
                    RoleList.Add(tc);
                    foreach (var rc3 in rc2)
                    {
                        var tc2 = new Hilfsklassen.RoleListDB {Text = rc3.Name};
                        RoleList.Add(tc2);
                    }
                }
                    
            }
        }
        public List<Hilfsklassen.RoleListDB> RoleList { get; set; }
        public string Select { get; set; }
    }
}
