using Microsoft.AspNetCore.Http;

namespace Application.Helpers
{
    public static class ImageHelper
    {
        private static readonly string BasePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

        public static async Task<List<string>> SaveImagesAsync(List<IFormFile> files, string folderName)
        {
            List<string> imageNames = new List<string>();

            if (files == null || files.Count == 0)
            {
                return imageNames;
            }

            var uploads = Path.Combine(BasePath, "Images", folderName);


            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }

            foreach (var file in files)
            {
                if (file.Length > 0)
                {

                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);


                    var filePath = Path.Combine(uploads, uniqueFileName);


                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    imageNames.Add(uniqueFileName);
                }
            }

            return imageNames;
        }
    }
}