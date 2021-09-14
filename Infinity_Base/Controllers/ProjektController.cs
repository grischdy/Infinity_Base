using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Infinity_Base.Data;
using Infinity_Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aml.Engine.Adapter;
using Aml.Engine.AmlObjects;
using Aml.Engine.CAEX;
using Aml.Engine.CAEX.Extensions;
using Aml.Engine.Services;
using Aml.Engine.Xml.Extensions;


namespace Infinity_Base.Controllers
{
    [Authorize]
    public class ProjektController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CAEXDocument _myDoc;
        public ProjektController(ApplicationDbContext context, CAEXDocument mydoc)
        {
            _context = context;
            _myDoc = mydoc;
        }


        public IActionResult Index()
        {
            var projekte = _context.Projekte.ToList();
            ViewBag.Projekte = projekte;

            return View();
        }

        public IActionResult Projektleiter()
        {
            var projekte = _context.Projekte.ToList();

            ViewBag.Projekte = projekte;

            return View();
        }
        public IActionResult Hardwarekonstruktion()
        {
            var projekte = _context.Projekte.ToList();

            ViewBag.Projekte = projekte;

            return View();
        }

        public IActionResult Softwarekonstruktion()
        {
            var projekte = _context.Projekte.ToList();

            ViewBag.Projekte = projekte;

            return View();
        }

        public IActionResult Administrator()
        {
            var projekte = _context.Projekte.ToList();
            ViewBag.Projekte = projekte;

            return View();
        }

        public IActionResult CreateEdit(int id)
        {
            if(id == 0)
            {
                return View("CreateEditProjekt");
            }
            var projektInDB = _context.Projekte.Find(id);
            if(projektInDB == null)
            {
                return NotFound();
            }
            return View("CreateEditProjekt", projektInDB);
        }

        [HttpPost]
        public IActionResult CreateEditProjekt(Projekt projekt)
        {
            if(projekt.Id == 0)
            {
                _context.Projekte.Add(projekt);
            }
            else
            {
                _context.Projekte.Update(projekt);
            }

            _context.SaveChanges();
            return RedirectToAction("Projektleiter");
        }


        public IActionResult DeleteProjekt(int id)
        {
            var projektInDB = _context.Projekte.Find(id);

            if (projektInDB == null)
            {
                return NotFound();
            }

            _context.Projekte.Remove(projektInDB);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
