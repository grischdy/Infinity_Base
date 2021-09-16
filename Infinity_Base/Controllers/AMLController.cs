using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
    public class AMLController : Controller
    {
        private readonly IHostingEnvironment _appEnvironment;
        private readonly CAEXDocument _myDoc;
        public string Dateipfad;


        IHostingEnvironment _appEnviroment2;
        public AMLController(IHostingEnvironment appEnvironment)

        {

            _appEnvironment = appEnvironment;
            string path_Root = _appEnvironment.WebRootPath;
            Dateipfad = path_Root + "\\Upload\\turbo.aml";
            globals._myglobalDoc = CAEXDocument.LoadFromFile(Dateipfad);
            
        }




        public IActionResult Index()
        {

            var myDoc = globals._myglobalDoc;

            ViewBag.myDoc = myDoc;

            return View();
        }


        public IActionResult CreateStation()
        {
            return View("CreateEditStation");
        }

        [HttpPost]
        public IActionResult CreateEditStation(AML_Station AML_Station)
        {
            var myDoc = globals._myglobalDoc;
            foreach (InstanceHierarchyType ih1 in myDoc.CAEXFile.InstanceHierarchy)
            {
                foreach (InternalElementType ih2 in ih1.InternalElement)
                {

                    var ih3 = ih2.InternalElement.Append(AML_Station.Stationsnummer);

                }
            }
            myDoc.SaveToFile(Dateipfad, true);

            return RedirectToAction("Index");
        }
    }
}
