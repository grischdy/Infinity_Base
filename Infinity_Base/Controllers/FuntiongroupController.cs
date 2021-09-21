using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aml.Engine.AmlObjects;
using Aml.Engine.CAEX;
using Aml.Engine.CAEX.Extensions;
using Aml.Engine.Services;
using Aml.Engine.Xml.Extensions;
using Microsoft.AspNetCore.Hosting;
using Infinity_Base.Hilfsklassen;
using Infinity_Base.Models;

namespace Infinity_Base.Controllers
{
    public class FuntiongroupController : Controller
    {
        private readonly IHostingEnvironment _appEnvironment;
        public string Dateipfad;

        IHostingEnvironment _appEnviroment2;
        public FuntiongroupController(IHostingEnvironment appEnvironment)

        {
            _appEnvironment = appEnvironment;
            string path_Root = _appEnvironment.ContentRootPath;
            Dateipfad = path_Root + "\\Upload\\turbo.aml";
            globals._myglobalDoc = CAEXDocument.LoadFromFile(Dateipfad);
        }
        public IActionResult Index()
        {
            return View("../AML/Index");
        }


        public IActionResult CreateEditFunctiongroup(AML_Functiongroup AML_Functiongroup)
        {
            if (AML_Functiongroup.Id == null)
            {
                return RedirectToAction("Index", "AML");
            }
            else
            {
                var myDoc = globals._myglobalDoc;
                foreach (InstanceHierarchyType ih1 in myDoc.CAEXFile.InstanceHierarchy)
                {
                    foreach (InternalElementType ih2 in ih1.Descendants<InternalElementType>())
                    {
                        if (ih2.ID == AML_Functiongroup.Id)
                        {
                            //ih2.RoleReferences.Append(AML_Functiongroup.Select);
                            ih2.New_SupportedRoleClass(AML_Functiongroup.Select);
                            ih2.Description = AML_Functiongroup.Zusatzinformationen;
                            myDoc.SaveToFile(Dateipfad, true);
                            return RedirectToAction("Index", "AML");
                        }
                    }
                }
            }
            return RedirectToAction("Index", "AML");
        }




        [HttpPost]
        public IActionResult CreateFunctiongroup(AML_Functiongroup AML_Functiongroup)
        {
            var myDoc = globals._myglobalDoc;

            if (AML_Functiongroup == null)
            {
                return View("../AML/CreateEditFunctiongroup");
            }
            else
            {
                foreach (InstanceHierarchyType ih1 in myDoc.CAEXFile.InstanceHierarchy)
                {
                    foreach (InternalElementType ih2 in ih1.InternalElement)
                    {
                        foreach (InternalElementType ih3 in ih2.InternalElement)
                        {
                            foreach (InternalElementType ih4 in ih3.InternalElement)
                            {
                                var caexObj_1 = myDoc.FindByID(AML_Functiongroup.Id);
                                if (caexObj_1 != null)
                                {
                                    AML_Functiongroup.Name = caexObj_1.Name;
                                    AML_Functiongroup.Zusatzinformationen = caexObj_1.Description;
                                    AML_Functiongroup.Zusatzinformationen = caexObj_1.Description;
                                }
                            }
                        }
                    }
                }
            }
            return View("../AML/CreateEditFunctiongroup", AML_Functiongroup);
        }
    }
}
