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
        //**********************************************************************************************
        //Der AML Controller befasst sich mit der Anzeige und der Bearbeitung der AML Datei.
        //Jede Ebene (Anlage,Station,Funktionsgruppe usw) hat ihr eigenes Model für das Anlegen und Anzeigen von weiteren Informationen
        //**********************************************************************************************


        //Dieser Abschnitt befasst sich mit der Dateiverwaltung
        //Den Pfad ermitteln wo die Daten liegen. Hier wird dann die AML in die globale Variable geladen
        //und der Dateipfad zur verfügung gestellt zum Speichern

        private readonly IHostingEnvironment _appEnvironment;
        public string Dateipfad;

        IHostingEnvironment _appEnviroment2;
        public AMLController(IHostingEnvironment appEnvironment)

        {
            _appEnvironment = appEnvironment;
            string path_Root = _appEnvironment.ContentRootPath;
            Dateipfad = path_Root + "\\Upload\\turbo.aml";
            globals._myglobalDoc = CAEXDocument.LoadFromFile(Dateipfad);     
        }


        //Das erste Laden der AML Seite bekommt die AML Datei als ViewBag übergeben

        public IActionResult Index()
        {
            var myDoc = globals._myglobalDoc;
            ViewBag.myDoc = myDoc;
            ViewData["NavMenuPage"] = "AML";
            return View();
        }
        
        //Die Methode wird aufgerufen aus der Index Seite mit dem "+ Add Station" (Create) Button ->Station.Id == null
        //oder wenn man eine Station anklickt (Edit) ->Station.Id != null
        //wird mit der foreach Schleife in die Ebene navigiert und alle Informationen die dieser ID
        //zugehören in die View übergeben

        public IActionResult CreateStation(AML_Station AML_Station)
        {
            var myDoc = globals._myglobalDoc;


            if (AML_Station.Id == null)
            {
                return View("CreateEditStation");
            }
            else
            { 
                foreach (InstanceHierarchyType ih1 in myDoc.CAEXFile.InstanceHierarchy)
                {
                    foreach (InternalElementType ih2 in ih1.InternalElement)
                    {
                        foreach (InternalElementType ih3 in ih2.InternalElement)
                        {
                            if (ih3.ID == AML_Station.Id)
                            {
                                AML_Station.Stationsnummer = ih3.Name;
                                AML_Station.Zusatzinformationen = ih3.Description;
                            }
                        }
                    }
                }
            }
            return View("CreateEditStation", AML_Station);
        }

        [HttpPost]
        public IActionResult CreateEditStation(AML_Station AML_Station)
        {
            var myDoc = globals._myglobalDoc;
            if (AML_Station.Stationsnummer == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach (InstanceHierarchyType ih1 in myDoc.CAEXFile.InstanceHierarchy)
                {
                    foreach (InternalElementType ih2 in ih1.InternalElement)
                    {
                        var caexObj_1 = myDoc.FindByID(AML_Station.Id);
                        if (caexObj_1 == null)
                        {
                            var new_Element = ih2.InternalElement.Append(AML_Station.Stationsnummer);
                            new_Element.Description = AML_Station.Zusatzinformationen;
                            myDoc.SaveToFile(Dateipfad, true);
                            return RedirectToAction("Index");
                        }
                        if (caexObj_1 != null)
                        {

                            myDoc.SaveToFile(Dateipfad, true);
                            return RedirectToAction("Index");
                        }
                    }
                }
            }

            myDoc.SaveToFile(Dateipfad, true);
            return RedirectToAction("Index");
        }



        public IActionResult CreateEditFunctiongroup(AML_Functiongroup AML_Functiongroup)
        {


            if (AML_Functiongroup.Id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var myDoc = globals._myglobalDoc;
                foreach (InstanceHierarchyType ih1 in myDoc.CAEXFile.InstanceHierarchy)
                {
                    foreach (InternalElementType ih2 in ih1.InternalElement)
                    {
                        foreach (InternalElementType ih3 in ih2.InternalElement)
                        {
                            var caexObj_1 = myDoc.FindByID(AML_Functiongroup.Id);
                            if (caexObj_1 == null)
                            {
                                var new_Element = ih2.InternalElement.Append(AML_Functiongroup.Name);
                                new_Element.Description = AML_Functiongroup.Zusatzinformationen;

                                myDoc.SaveToFile(Dateipfad, true);
                                return RedirectToAction("Index");
                            }
                            if (caexObj_1 != null)
                            {
                                myDoc.SaveToFile(Dateipfad, true);
                                return RedirectToAction("Index");
                            }
                        }
                    }
                }
            }

            return View("CreateEditFunktiongroup", AML_Functiongroup);
        }




        [HttpPost]
        public IActionResult CreateFunctiongroup(AML_Functiongroup AML_Functiongroup)
        {
            var myDoc = globals._myglobalDoc;
            
            if (AML_Functiongroup == null)
            {
                return View("CreateEditFunctiongroup");
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
                                }
                            }
                        }
                    }
                }
            }
            return View("CreateEditFunctiongroup", AML_Functiongroup);
        }


    }
}
