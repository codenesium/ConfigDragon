namespace Codenesium.ConfigDragonLib
{
    using System.IO;

    /// <summary>
    /// File system interface to allow for mocking of File.ReadAllText etc.
    /// </summary>
    public class FileSystem : IFileSystem
    {
        /// <summary>
        /// Reads the contents of the provided file
        /// </summary>
        /// <param name="path">The path to read from</param>
        /// <returns>The contents of the provided file</returns>
        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        /// <summary>
        /// Writes the provided contents to the path
        /// </summary>
        /// <param name="path">The file to write to</param>
        /// <param name="contents">The string to write to disk</param>
        public void WriteAllText(string path, string contents)
        {
            File.WriteAllText(path, contents);
        }
    }
}
