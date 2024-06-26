namespace Qate3Dashboard.Helpers
{
    public static class DocumentSettings
    {
        public static string UploadFile(IFormFile File, string FolderName)
        {
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Images\\AppImages",FolderName);

            if(Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);
            

            string FileName = $"{Guid.NewGuid()}{Path.GetExtension(File.FileName)}";

            string filePath = Path.Combine(FolderPath,FileName);

            using var FileStream = new FileStream(filePath, FileMode.Create);

            File.CopyTo(FileStream);

            return FileName;
        }

        public static void DeleteFile(string FileName, string FolderName)
        {
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\AppImages", FolderName, FileName);

            if(File.Exists(FilePath))
                File.Delete(FilePath);

        }
    }
}
