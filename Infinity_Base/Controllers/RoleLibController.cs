using Infinity_Base.Hilfsklassen;
using Microsoft.AspNetCore.Hosting;
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

namespace Infinity_Base.Controllers
{
    public class RoleLibController : Controller
    {
        private readonly IHostingEnvironment _appEnvironment;
        public string Dateipfad;

        public RoleLibController(IHostingEnvironment appEnvironment)
        {
            
            _appEnvironment = appEnvironment;
            string path_Root = _appEnvironment.ContentRootPath;
            Dateipfad = path_Root + "\\Upload\\turbo.aml";
            globals._myglobalDoc = CAEXDocument.LoadFromFile(Dateipfad);
        }


        public IActionResult Index()
        {
            var myDoc = globals._myglobalDoc;
            ViewBag.myDoc = myDoc;
            //ViewData["NavMenuPage"] = "RoleLib";
            return View("RoleLib");
        }
    }
}