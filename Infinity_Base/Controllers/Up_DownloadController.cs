using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Infinity_Base.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Up_DownloadController : ControllerBase
    {
        private readonly string filePath;
        public Up_DownloadController(string filePath)
        {
            this.filePath = filePath;
        }
        // GET: api/<Up_DownloadController>
        [HttpGet]
        public FileContentResult Get()
        {
             return File(System.IO.File.ReadAllBytes(filePath), "application/octet-stream", "Turbo.aml");
            //https://localhost:5001/api/up_download
        }
    }
}
