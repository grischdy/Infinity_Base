using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Infinity_Base.Models;

namespace Infinity_Base.Controllers
{
    public class NewFileController : Controller
    {
        public ActionResult Index()
        {
            //////Fetch all files in the Folder (Directory).
            //////string[] filePaths = Directory.GetFiles(Server.MapPath("~/Upload/"));

            //////Copy File names to Model collection.
            ////List<FileModel> files = new List<FileModel>();
            ////foreach (string filePath in filePaths)
            ////{
            ////    files.Add(new FileModel { FileName = Path.GetFileName(filePath) });
            ////}

            return View();
        }
    }
}
