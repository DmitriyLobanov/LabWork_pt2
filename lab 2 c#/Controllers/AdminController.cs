using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabWork_pt2.Entity;
using LabWork_pt2.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using System;
using LabWork_pt2.Service;

namespace LabWork_pt2.Controllers
{
    [Authorize(Roles="ROLE_ADMIN")]
    public class AdminController : Controller
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly ILogger _logger;
        private readonly FileService _fileService;
        public AdminController(DataBaseContext dataBaseContext, IWebHostEnvironment hostEnvironment, ILogger<AdminController> logger, FileService fileService)
        {
            _dataBaseContext = dataBaseContext;
            _appEnvironment = hostEnvironment;
            _logger = logger;
            _fileService = fileService;
        }
        public async Task<ActionResult> Index()
        {
            return View(await _dataBaseContext.Files.ToListAsync());
        }


        [HttpPost]
        public async Task<ActionResult> AddFile([FromForm] IFormFile uploadedFile, string fileDescription)
        {
            if(uploadedFile !=null)
            {
                string path = "\\Files\\" + uploadedFile.FileName;

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                FileModel fileModel = new FileModel(uploadedFile.FileName, path, fileDescription);
                _dataBaseContext.Files.Add(fileModel);
                _dataBaseContext.SaveChanges();
                _logger.LogInformation("File {filename} loaded on server and saved in directory {path}", uploadedFile.FileName, path);
            }

            return Redirect("Index");
        }
        [HttpPost]
        public async Task<ActionResult> DeleteFile(string filename)
        {
           // string absolutePath = "C:\\Users\\guard\\Desktop\\СПП\\LabWork_pt2\\LabWork_pt2\\wwwroot";
            string relativePath = "wwwroot";
            FileModel fileModel = await _dataBaseContext.Files.FirstOrDefaultAsync(x => x.Name == filename);
            FileInfo file = new FileInfo(relativePath + fileModel.Path);
             file.Delete();
            _dataBaseContext.Remove(fileModel);
            _dataBaseContext.SaveChanges();
            _logger.LogInformation("File {filename} deleted from server", file.Name);
            return Redirect("Index");
        }

      
        public IActionResult GetFile(int fileId)
        {
            return RedirectToAction("GetFile", "Service", new {fileId = fileId});
        }
        [HttpPost]
        public async Task<ActionResult> ChangeDiscription([FromForm] FileChangeDescriptionDto fileChangeDescriptionDto)
        {
            int id = int.Parse(fileChangeDescriptionDto.id);
            FileModel fileModel = await _dataBaseContext.Files.FirstOrDefaultAsync(x => x.Id == id);
            string oldDescription = fileModel.FileDescription;
            fileModel.FileDescription = fileChangeDescriptionDto.newFileDescription;
            _dataBaseContext.Update(fileModel);
            _dataBaseContext.SaveChanges();
            _logger.LogInformation("File {filename} changed description:\n old: {oldDescription} \nnew: {newFileDescription}", fileModel.Name, oldDescription, fileChangeDescriptionDto.newFileDescription);
            return Redirect("Index");
        }
     

        public IActionResult Search(string searchString)
        {
            var files = _fileService.Search(searchString);
            if (files !=null)
            {
                return View("Index", files);
            }

            return View("Index");
        }
       
    }
}
