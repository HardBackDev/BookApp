using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace BookAppServer.Controllers
{
    [Route("api/file")]
    public class FileController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment; 

        public FileController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("upload")]
        public IActionResult UploadPdf(IFormFile File)
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", File.FileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                File.CopyTo(fileStream);
            }
            return Ok(new {path = Path.Combine("wwwroot", "uploads", File.FileName)});
        }
        [HttpGet, DisableRequestSizeLimit]
        [Route("download")]
        public async Task<IActionResult> Download([FromQuery] string fileUrl)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileUrl);
            if (!System.IO.File.Exists(filePath))
                return NotFound();
            var memory = new MemoryStream();
            await using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filePath), filePath);
        }

        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;

            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }
    }
}
