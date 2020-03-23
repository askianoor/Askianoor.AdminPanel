using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Askianoor.AdminPanel.Data;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Askianoor.AdminPanel.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment environment;


        public UploadController(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> MultipleAsync(IFormFile[] files, string CurrentDirectory)
        {

            //TODO : Add A Security Check 
            if (string.IsNullOrEmpty(CurrentDirectory))
                return BadRequest();

            try
            {
                if (HttpContext.Request.Form.Files.Any())
                {
                    foreach (var file in HttpContext.Request.Form.Files)
                    {
                        // reconstruct the path to ensure everything 
                        // goes to uploads directory
                        string RequestedPath = CurrentDirectory.ToLower(CultureInfo.CurrentCulture).Replace(environment.WebRootPath.ToLower(CultureInfo.CurrentCulture), "", StringComparison.OrdinalIgnoreCase);

                        if (RequestedPath.Contains("\\uploads\\", StringComparison.OrdinalIgnoreCase))
                        {
                            RequestedPath = RequestedPath.Replace("\\uploads\\", "", StringComparison.OrdinalIgnoreCase);
                        }
                        else
                        {
                            RequestedPath = "";
                        }
                        string path = Path.Combine(environment.WebRootPath, "uploads", RequestedPath, file.FileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream).ConfigureAwait(true);
                        }
                    }
                }
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}