using Fintorly.Application.Dtos.FileDtos;
using Microsoft.AspNetCore.Http;

namespace Fintorly.Application.Utilities;

public static class FileUpload
{
    private static string _currentDirectory = Environment.CurrentDirectory + @"\wwwroot\Uploads\";

    public static async Task<IResult> Upload(IFormFile file, string folderName)
    {
        var fileExists =await CheckFileExists(file);
        if (!fileExists.Succeeded)
            return await Result.FailAsync(fileExists.Message);
        var type = Path.GetExtension(file.FileName);
        var typeValid =await CheckFileTypeValid(type);
        if (!typeValid.Succeeded)
            return await Result.FailAsync(typeValid.Message);
        var randomName = Guid.NewGuid().ToString();
        CheckDirectoryExists(_currentDirectory);
        CreateImageFile(_currentDirectory + randomName + type, file);
        return await Result.SuccessAsync((_currentDirectory + $"\\{folderName}\\" + randomName + type).Replace("\\", "/"),
            randomName + type + file);
    }

    public static async Task<IResult<UploadResult>> UploadAlternative(IFormFile file, string folderName, string? oldFilePath = "")
    {
        if (!string.IsNullOrEmpty(oldFilePath) && oldFilePath != null && File.Exists(oldFilePath))
            File.Delete(oldFilePath);
        if (!Directory.Exists(_currentDirectory + folderName))
            Directory.CreateDirectory(_currentDirectory + folderName);

        string fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
        var path = _currentDirectory + folderName + @"\" + fileName;
        var virtualPath = "Uploads/" + folderName + "/" + fileName;
        var type = Path.GetExtension(file.FileName);
        var typeValid =await CheckFileTypeValid(type);
        if (!typeValid.Succeeded)
            return await Result<UploadResult>.FailAsync(typeValid.Message);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            file.CopyTo(stream);
        }
        var uploadResult = new UploadResult()
        {
            FilePath = path,
            FileVirtualPath = virtualPath
        };
        return await Result<UploadResult>.SuccessAsync(uploadResult);
    }

    public static async Task<IResult> Update(IFormFile file, string folderName, string oldImageName)
    {
        var fileExists =await CheckFileExists(file);
        if (fileExists.Message != null)
            return await Result.FailAsync(fileExists.Message);
        var type = Path.GetExtension(file.FileName);
        var typeValid =await CheckFileTypeValid(type);
        var randomName = Guid.NewGuid().ToString();

        if (typeValid.Message != null)
            return await Result.FailAsync(typeValid.Message);

        DeleteOldImageFile((_currentDirectory + $"\\{folderName}" + oldImageName).Replace("/", "\\"));
        CheckDirectoryExists(_currentDirectory + $"\\{folderName}");
        CreateImageFile(_currentDirectory + $"\\{folderName}" + randomName + type, file);
        return await Result.SuccessAsync((_currentDirectory + randomName + type).Replace("\\", "/"));
    }

    public static async Task<IResult> Delete(string path)
    {
        DeleteOldImageFile((_currentDirectory + path).Replace("/", "\\"));
        return await Result.SuccessAsync("Başarıyla silindi.");
    }

    private static async Task<IResult> CheckFileExists(IFormFile file)
    {
        if (file != null && file.Length > 0)
            return await Result.FailAsync("Mevcut.");
        return await Result.SuccessAsync("Böyle bir dosya mevcut değil");
    }

    private static async Task<IResult> CheckFileTypeValid(string type)
    {
        type = type.ToLower();
        if (type == ".jpeg" || type == ".png" || type == ".jpg")
            return await Result.SuccessAsync("Geçerli dosya");
        return await Result.FailAsync("Dosya tipi yanlış formatta.");
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