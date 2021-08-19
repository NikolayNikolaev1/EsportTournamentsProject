namespace EsportsTournaments.Core.Extensions
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using static Common.GlobalConstants.ImageFile;

    public static class ImageFileExtensions
    {
        // Gets file and root path from controller, uploads file to root folder "images" and returns guid file name.
        public static async Task<string> UploadFileAsync(IFormFile file, string webRootPath)
        {
            string uniqueFileName = null;

            if (file != null)
            {
                var uploadsFolder = Path.Combine(webRootPath, "images");
                uniqueFileName = string.Concat(Guid.NewGuid().ToString(), GetFileExtension(file.FileName));
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }

            return uniqueFileName;
        }

        // Returns info if image file extension is valid.
        public static bool FileExtensionValidation(string uploadedFileName)
        {
            string ext = GetFileExtension(uploadedFileName);

            return !string.IsNullOrEmpty(ext) &&
                PermittedExtensions.Contains(ext);
        }

        // Returns info if image file signature is valid.
        public static bool FileSignatureValidation(string uploadedFileName, Stream uploadedFileData)
        {
            string ext = GetFileExtension(uploadedFileName);

            using (BinaryReader reader = new BinaryReader(uploadedFileData))
            {
                var signatures = ValidSignatures[ext];
                byte[] headerBytes = reader
                    .ReadBytes(signatures.Max(m => m.Length));

                return signatures.Any(
                    signature => headerBytes
                    .Take(signature.Length)
                    .SequenceEqual(signature));
            }
        }

        // Returns file extension.
        private static string GetFileExtension(string uploadedFileName)
           => Path
           .GetExtension(uploadedFileName)
           .ToLowerInvariant();
    }
}
