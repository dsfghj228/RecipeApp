using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace api.Controllers
{
    [Route("api/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly PhotoStorageService _storageService;

        public PhotosController(PhotoStorageService storageService)
        {
            _storageService = storageService;
        }

        [HttpPost]
        public ActionResult<Photo> UploadPhoto(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Нет файла для загрузки.");
            }

            var uploadedPhoto = _storageService.UploadPhoto(file);
            return CreatedAtAction(nameof(GetPhoto), new { fileName = uploadedPhoto.FileName }, uploadedPhoto);
        }

        [HttpGet("{fileName}")]
        public ActionResult GetPhoto(string fileName)
        {
            var photo = _storageService.GetPhoto(fileName);

            if (photo == null)
            {
                return NotFound();
            }

            var fileBytes = System.IO.File.ReadAllBytes(photo.FilePath);
            return File(fileBytes, "image/jpeg");
        }

        [HttpDelete("{fileName}")]
        public ActionResult DeletePhoto(string fileName)
        {
            var success = _storageService.DeletePhoto(fileName);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}