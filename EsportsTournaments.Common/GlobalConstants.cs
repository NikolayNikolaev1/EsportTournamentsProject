namespace EsportsTournaments.Common
{
    using System.Collections.Generic;

    public static class GlobalConstants
    {
        public const string SolutionName = "EsportsTournaments";

        public static class ImageFile
        {
            public static readonly string[] PermittedExtensions = { ".jpeg", ".jpg", ".png" };

            public static readonly IDictionary<string, List<byte[]>> ValidSignatures =
                new Dictionary<string, List<byte[]>>
                {
                    {
                        ".jpeg",
                        new List<byte[]>
                        {
                            new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                            new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                            new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                        }
                    },
                    {
                        ".jpg",
                        new List<byte[]>
                        {
                            new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                            new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                            new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                        }
                    },
                    {
                        ".png",
                        new List<byte[]>
                        {
                            new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A },
                        }
                    },
                };

            public const int MaxImageSize = 2097152;
        }
    }
}
