using ActivitySystem.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitySystem.Services
{
    public static class ActivityImageUploadService
    {
        public static IFormFile UploadedFile(ActivityFormViewModel model, IWebHostEnvironment webHostEnvironment)
        {
            if (model.ActivityImage != null)
            {
                string UploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/Activity");
                string FileName = Guid.NewGuid().ToString() + "_" + model.ActivityImage.FileName;
                string FilePath = Path.Combine(UploadFolder, FileName);
                using (var fileStream = new FileStream(FilePath, FileMode.Create))
                {
                    model.ActivityImage.CopyTo(fileStream);
                }
            }
            
            return model.ActivityImage;
        }
    }
}
