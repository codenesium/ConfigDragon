namespace Codenesium.ConfigDragonLib
{
    /// <summary>
    /// A file system mock interface
    /// </summary>
    public interface IFileSystem
    {
        /// <summary>
        /// Writes the provided contents to the path
        /// </summary>
        /// <param name="path">The file to write to</param>
        /// <param name="contents">The string to write to disk</param>
        void WriteAllText(string path, string contents);

        /// <summary>
        /// Reads the contents of the provided file
        /// </summary>
        /// <param name="path">The path to read from</param>
        /// <returns>The contents of the provided file</returns>
        string ReadAllText(string path);
    }
}
