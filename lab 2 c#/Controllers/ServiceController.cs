using LabWork_pt2.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

namespace LabWork_pt2.Controllers
{
    [Authorize]
    public class ServiceController : Controller
    {
        private DataBaseContext _dataBaseContext;
        private Microsoft.Extensions.Logging.ILogger _logger;
        private FileService _fileService;

        public ServiceController(DataBaseContext dataBaseContext, ILogger<ServiceController> logger, FileService fileService)
        {
            _logger = logger;
            _dataBaseContext = dataBaseContext;
            _fileService = fileService;
        }


        public async Task<ActionResult> Index()
        {
            return View(await _dataBaseContext.Files.ToListAsync());
        }
        public IActionResult Search(string searchString)
        {
            var result = _fileService.Search(searchString);
            if (result != null)
            {
                return View("Index", result);
            }
            return View("Index");
        }
        public IActionResult GetFile(int fileId)
        {
            var file = _dataBaseContext.Files.
                Where(x => x.Id == fileId)
                .FirstOrDefault();
            if (file != null)
            {
                file.downloadedCounter = ++file.downloadedCounter;
                
                _dataBaseContext.SaveChanges();
                _logger.LogInformation("File {filename} downloaded from server", file.Name);
                return File(file.Path, MIMEAssistant.GetMIMEType(file.Name), file.Name);

            }
            return Redirect("View");
            //return View("View");
        }

        
        public IActionResult Logout()
        {
            return RedirectToAction("Logout", "Account");
        }
    }

}
