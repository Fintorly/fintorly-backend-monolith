using System;
using Fintorly.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Fintorly.Application.Interfaces.Utils
{
    public class FileUpload
    {
        private static string _currentDirectory = Environment.CurrentDirectory + @"\wwwroot\Uploads\";

        public static IResult Upload(IFormFile file, string folderName)
        {
            var fileExists = CheckFileExists(file);
            if (!fileExists.Succeeded )
                return Result.Fail(fileExists.Message);
            var type = Path.GetExtension(file.FileName);
            var typeValid = CheckFileTypeValid(type);
            if (!typeValid.Succeeded)
                return Result.Fail(typeValid.Message);
            var randomName = Guid.NewGuid().ToString();
            CheckDirectoryExists(_currentDirectory);
            CreateImageFile(_currentDirectory + randomName + type, file);
            return Result.Success((_currentDirectory + $"\\{folderName}\\" + randomName + type).Replace("\\", "/"), randomName + type + file);
        }

        public static IResult UploadAlternative(IFormFile file, string folderName, string? oldFilePath = "")
        {
            if (!string.IsNullOrEmpty(oldFilePath) && oldFilePath != null && File.Exists(oldFilePath))
                File.Delete(oldFilePath);
            if (!Directory.Exists(_currentDirectory + folderName))
                Directory.CreateDirectory(_currentDirectory + folderName);

            string fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var path = _currentDirectory + folderName + @"\" + fileName;
            var virtualPath = "Uploads/" + folderName + "/" + fileName;
            var type = Path.GetExtension(file.FileName);
            var typeValid = CheckFileTypeValid(type);
            if (!typeValid.Succeeded)
                return Result.Fail(typeValid.Message);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return Result.Success(virtualPath, path);
        }
        public static IResult Update(IFormFile file, string folderName, string oldImageName)
        {
            var fileExists = CheckFileExists(file);
            if (fileExists.Message != null)
                return Result.Fail(fileExists.Message);
            var type = Path.GetExtension(file.FileName);
            var typeValid = CheckFileTypeValid(type);
            var randomName = Guid.NewGuid().ToString();

            if (typeValid.Message != null)
                return Result.Fail(typeValid.Message);

            DeleteOldImageFile((_currentDirectory + $"\\{folderName}" + oldImageName).Replace("/", "\\"));
            CheckDirectoryExists(_currentDirectory + $"\\{folderName}");
            CreateImageFile(_currentDirectory + $"\\{folderName}" + randomName + type, file);
            return Result.Success((_currentDirectory + randomName + type).Replace("\\", "/"));
        }

        public static IResult Delete(string path)
        {
            DeleteOldImageFile((_currentDirectory + path).Replace("/", "\\"));
            return Result.Success("Başarıyla silindi.");
        }

        private static IResult CheckFileExists(IFormFile file)
        {
            if (file != null && file.Length > 0)
                return Result.Fail("Mevcut.");
            return Result.Success("Böyle bir dosya mevcut değil");
        }

        private static IResult CheckFileTypeValid(string type)
        {
            type = type.ToLower();
            if (type == ".jpeg" || type == ".png" || type == ".jpg")
                return Result.Success("Geçerli dosya");
            return Result.Fail("Dosya tipi yanlış formatta.");
        }
        private static void CheckDirectoryExists(string directory)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }
        private static void CreateImageFile(string directory, IFormFile file)
        {
            using (FileStream fs = File.Create(directory))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
        }

        private static void DeleteOldImageFile(string directory)
        {
            if (File.Exists(directory.Replace("/", "\\")))
                File.Delete(directory.Replace("/", "\\"));
        }
    }
}

