using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Infinity_Base.Data;
using Infinity_Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Infinity_Base.Controllers
{
    [Authorize]
    public class ProjektController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProjektController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
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
            return RedirectToAction("Index");
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
