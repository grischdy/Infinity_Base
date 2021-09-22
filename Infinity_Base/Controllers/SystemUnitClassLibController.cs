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
    public class SystemUnitClassLibController : Controller
    {
        //Das erste Laden der SystemUnitClassLib Seite bekommt die AML Datei als ViewBag übergeben
        public IActionResult Index()
        {
            var myDoc = globals._myglobalDoc;
            ViewBag.myDoc = myDoc;
            // data to be passed to the component; 
            // will also be used to determine if the navbar should be displayed
            ViewData["NavMenuPage"] = "SystemUnitClassLib";
            return View("SystemUnitClassLib");
        }
    }
}
