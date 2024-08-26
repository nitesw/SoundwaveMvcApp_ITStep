using Core.Interfaces;

namespace SoundwaveMvcApp_ITStep.Services
{
    public class FilesService : IFilesService
    {
        const string imageFolder = "images";
        private readonly IWebHostEnvironment environment;

        public FilesService(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        public Task DeleteTrackImage(string path)
        {
            string root = environment.WebRootPath;
            string fullPath = root + path;

            if (File.Exists(fullPath))
                return Task.Run(() => File.Delete(fullPath));

            return Task.CompletedTask;
        }

        public async Task<string> EditTrackImage(string oldPath, IFormFile newFile)
        {
            await DeleteTrackImage(oldPath);
            return await SaveTrackImage(newFile);
        }

        public async Task<string> SaveTrackImage(IFormFile file)
        {
            string root = environment.WebRootPath;  
            string name = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(file.FileName);
            string fullName = name + extension;

            string imagePath = Path.Combine(imageFolder, fullName);
            string imageFullPath = Path.Combine(root, imagePath);

            using (FileStream fs = new FileStream(imageFullPath, FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }

            return Path.DirectorySeparatorChar + imagePath;
        }
    }
}
