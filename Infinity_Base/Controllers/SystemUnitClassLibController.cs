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
using System.Text.Json;
using Infinity_Base.Data;
using Newtonsoft.Json;

namespace Infinity_Base.Controllers
{
    public class SystemUnitClassLibController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _appEnvironment;
        public string Dateipfad;

        public SystemUnitClassLibController(ApplicationDbContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            string path_Root = _appEnvironment.ContentRootPath;
            Dateipfad = path_Root + "\\Upload\\turbo.aml";
            globals._myglobalDoc = CAEXDocument.LoadFromFile(Dateipfad);
        }

        [HttpGet]
        //Das erste Laden der SystemUnitClassLib Seite bekommt die AML Datei als ViewBag übergeben
        public IActionResult Index(SystemUnitClassLibLvl1 systemUnitClassLibLvl1, SystemUnitClassLibLvl2 systemUnitClassLibLvl2)
        {
            List<SystemUnitClassTreeViewNode> nodes = new List<SystemUnitClassTreeViewNode>();

            int parentid = 0;
            int subid = 0;
            var myDoc = globals._myglobalDoc;
            var DB = _context.SystemUnitClassLibLvl1.Find(1);
            if (DB == null)
            {
                foreach (var suc1 in myDoc.CAEXFile.SystemUnitClassLib)
                {
                    systemUnitClassLibLvl1.Title = suc1.Name;
                    systemUnitClassLibLvl1.Id = parentid + 1;
                    
                    var type = systemUnitClassLibLvl1;
                    nodes.Add(new SystemUnitClassTreeViewNode { id = type.Id.ToString(), parent = "#", text = type.Title });

                    foreach (var suc2 in suc1)
                    {
                        systemUnitClassLibLvl2.Name = suc2.Name;
                        systemUnitClassLibLvl2.Id = subid + 1;
                        var subtype = systemUnitClassLibLvl2;
                        subid += 1;
                        nodes.Add(new SystemUnitClassTreeViewNode { id = type.Id.ToString() + "-" + subtype.Id.ToString(), parent = type.Id.ToString(), text = subtype.Name });
                    }
                    parentid = parentid + 1;
                }
            }

            ViewBag.Json = JsonConvert.SerializeObject(nodes);
            ViewData["NavMenuPage"] = "SystemUnitClassLib";
            return View("SystemUnitClassLib");

        }
        public IActionResult LoadTree(SystemUnitClassLibLvl1 systemUnitClassLibLvl1, SystemUnitClassLibLvl2 systemUnitClassLibLvl2)
        {
            int parentid = 0;
            var myDoc = globals._myglobalDoc;
            var DB = _context.SystemUnitClassLibLvl1.Find(1);
            if (DB == null)
            {
                foreach (var suc1 in myDoc.CAEXFile.SystemUnitClassLib)
                {
                    systemUnitClassLibLvl1.Title = suc1.Name;
                    systemUnitClassLibLvl1.parentId = parentid + 1;
                    parentid = parentid + 1;
                    //Id muss null sein weil das die SQL id ist, somit wird automatisch eine vergeben
                    systemUnitClassLibLvl1.Id = 0;
                    _context.SystemUnitClassLibLvl1.Add(systemUnitClassLibLvl1);
                    _context.SaveChanges();
                    foreach (SystemUnitClassType suc2 in suc1)
                    {
                        systemUnitClassLibLvl2.Name = suc2.Name;
                        systemUnitClassLibLvl2.lvl1Id = systemUnitClassLibLvl1.parentId;
                        systemUnitClassLibLvl2.Id = 0;
                        _context.SystemUnitClassLibLvl2.Add(systemUnitClassLibLvl2);
                        _context.SaveChanges();
                    }
                }
            }
            
            return RedirectToAction("Index");
        }
        public IActionResult DeleteTree(SystemUnitClassLibLvl1 systemUnitClassLibLvl1, SystemUnitClassLibLvl2 systemUnitClassLibLvl2)
        {
            


            foreach (SystemUnitClassLibLvl2 suc3 in _context.SystemUnitClassLibLvl2)
            {
                _context.SystemUnitClassLibLvl2.Remove(suc3);

            }
            foreach (SystemUnitClassLibLvl1 suc2 in _context.SystemUnitClassLibLvl1)
            {
                _context.SystemUnitClassLibLvl1.Remove(suc2);
                
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
