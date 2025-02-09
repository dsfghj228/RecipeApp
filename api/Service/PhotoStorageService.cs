using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Service
{
    public class PhotoStorageService
    {
        private readonly string _storagePath;
        public PhotoStorageService(string storagePath)
        {
            _storagePath = storagePath;

            if(Directory.Exists(_storagePath))
            {
                Directory.CreateDirectory(_storagePath);
            }
        }

        public Photo UploadPhoto(IFormFile file)
        {
            var fileName = Path.GetFileNameWithoutExtension(file.FileName) + "_" + Guid.NewGuid() + Path.GetExtension(file.FileName);
            var FilePath = Path.Combine(_storagePath, fileName);

            using (var fileStream = new FileStream(FilePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return new Photo 
            {
                FileName = fileName,
                FilePath = FilePath,
                UploadDate = DateTime.Now
            };
        }

        public Photo GetPhoto(string fileName)
        {
            var filePath = Path.Combine(_storagePath, fileName);
            if (File.Exists(filePath))
            {
                return new Photo
                {
                    FileName = fileName,
                    FilePath = filePath,
                    UploadDate = File.GetCreationTime(filePath)
                };
            }

            return null;
        }

        public bool DeletePhoto(string fileName)
        {
            var filePath = Path.Combine(_storagePath, fileName);
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }

                return false;
            }
        }
    }
}