using GroupDocs.Viewer;
using GroupDocs.Viewer.Options;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;    //*IFormFile
using Microsoft.AspNetCore.Hosting;


namespace Infinity_Base.Controllers
{
    public class FileController : Controller
    {
        private readonly IHostingEnvironment _appEnvironment;
        public FileController(IHostingEnvironment appEnvironment)

        {

            //----< Init: Controller >----

            _appEnvironment = appEnvironment;

            //----</ Init: Controller >----

        }



        [HttpGet] //1.Load//Postback
        public IActionResult FileUpload()
        {
            return View();
        }

        [HttpPost] //Postback
        public async Task<IActionResult> FileUpload(IFormFile file)

        {

            //--------< Upload_File() >--------

            //< check >

            if (file == null || file.Length == 0) return Content("file not selected");

            //</ check >



            //< get Path >

            string path_Root = _appEnvironment.ContentRootPath;

            string path_to_Output = path_Root + "\\Upload\\" + file.FileName;

            //</ get Path >



            //< Copy File to Target >

            using (var stream = new FileStream(path_to_Output, FileMode.Create))

            {

                await file.CopyToAsync(stream);

            }

            //</ Copy File to Target >



            //< output >

            ViewData["FilePath"] = path_to_Output;

            return View();

            //</ output >

            //--------</ Upload_File() >--------

        }
    }
}
