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
            string UploadFolder = Path.Combine(webHostEnvironment.WebRootPath, @"images\Upload\Activity");
            if (model.ActivityImage != null)
            {
                if (!Directory.Exists(UploadFolder))      // 檢查 wwwroot 是否有上傳資料夾
                {
                    Directory.CreateDirectory(UploadFolder);
                }
                string FileName = Guid.NewGuid().ToString() + "_" + model.ActivityImage.FileName;
                model.ActivityImageFileName = FileName;

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
