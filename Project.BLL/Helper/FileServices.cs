using Microsoft.AspNetCore.Http;

namespace Project.BLL.Helper
{
    public static class FileServices
    {
        public static string UploadFile(this IFormFile formFile, string foldername, string root = null)
        {
            string filepath;
            if (root == null)
            {
                filepath = Path.Combine(Directory.GetCurrentDirectory(), "Files", foldername);
            }
            else
            {
                filepath = Path.Combine(Directory.GetCurrentDirectory(), root, "Files", foldername);
            }

            var filename = Guid.NewGuid() + Path.GetFileName(formFile.FileName);

            var fullpath = Path.Combine(filepath, filename);

            using (var streem = new FileStream(fullpath, FileMode.Create))
            {
                formFile.CopyTo(streem);
            }

            return filename;
        }

        public static string RemoveFile(this IFormFile formFile, string foldername, string root = null)
        {
            try
            {
                string filepath;
                if (root == null)
                {
                    filepath = Path.Combine(Directory.GetCurrentDirectory(), "Files", foldername, formFile.FileName);
                }
                else
                {
                    filepath = Path.Combine(Directory.GetCurrentDirectory(), root, "Files", foldername, formFile.FileName);
                }

                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
                return "File Not Found";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }





    }
}
