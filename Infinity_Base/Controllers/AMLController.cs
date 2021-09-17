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
            return View();
        }
        
        //Die Methode wird aufgerufen aus der Index Seite mit dem "+ Add Station" (Create) Button ->Station.Id == null
        //oder wenn man eine Station anklickt (Edit) ->Station.Id != null
        //wird mit der foreach Schleife in die Ebene navigiert und alle Informationen die dieser ID
        //zugehören in die View übergeben

        public IActionResult CreateStation(AML_Station AML_Station)
        {


            if (AML_Station.Id == null)
            {
                return View("CreateEditStation");
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


        public IActionResult CreateEditFunktiongroup(AML_Functiongroup AML_Functiongroup)
        {


            if (AML_Functiongroup.Id == null)
            {
                return View("CreateEditStation");
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
                            if (ih3.ID == AML_Functiongroup.Id)
                            {
                                AML_Functiongroup.Name = ih3.Name;
                                AML_Functiongroup.Zusatzinformationen = ih3.Description;
                            }
                        }
                    }
                }
            }

            return View("CreateEditStation", AML_Functiongroup);
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
                        if(caexObj_1==null)
                        {
                            var new_Element =ih2.InternalElement.Append(AML_Station.Stationsnummer);
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
    }
}
