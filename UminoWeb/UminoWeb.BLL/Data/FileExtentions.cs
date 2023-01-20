using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UminoWeb.BLL.Data
{
    public static class FileExtensions
    {
        public async static Task<string> GenerateFile(this IFormFile file, string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var unicalName = $"{Guid.NewGuid()}-{file.FileName}";

            using FileStream fs = new(Path.Combine(path, unicalName), FileMode.Create);
            await file.CopyToAsync(fs);

            return unicalName;
        }
    }
}
