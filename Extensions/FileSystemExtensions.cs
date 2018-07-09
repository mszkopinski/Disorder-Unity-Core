using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UnityCore
{
    public static class FileSystemExtensions
    {

        public static IEnumerable<string> GetFileNamesWithExtension(this string directory, string extension)
        {
            var files = Directory.EnumerateFiles(directory).Select(Path.GetFileName);

            return files.Where(file => Path.GetExtension(file) == extension).Select(Path.GetFileNameWithoutExtension);
        }
    }
}