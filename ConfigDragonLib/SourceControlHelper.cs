namespace Codenesium.ConfigDragonLib
{
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    /// Helper functions for determining if the current executing process is in
    /// a mercurial or git repository
    /// </summary>
    public class SourceControlHelper
    {
        /// <summary>
        /// Attempts to return the mercurial repository root
        /// </summary>
        /// <param name="executablePath">Location of the HG executable</param>
        /// <returns>Repository root directory or empty</returns>
        public static string GetHGRepositoryRootPath(string executablePath)
        {
            string response = string.Empty;

            if (File.Exists(executablePath))
            {
                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = executablePath,
                        Arguments = "root",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };

                proc.Start();

                proc.WaitForExit();

                while (!proc.StandardOutput.EndOfStream)
                {
                    string line = proc.StandardOutput.ReadLine();
                    if (!line.ToUpper().Contains("ABORT"))
                    {
                        response = line;
                        break;
                    }
                }
            }

            return response;
        }

        /// <summary>
        /// Attempts to determine the root directory of a GIT repository
        /// </summary>
        /// <param name="executablePath">Location of the GIET executable</param>
        /// <returns>Repository root directory or empty</returns>
        public static string GetGitRepositoryRootPath(string executablePath)
        {
            string response = string.Empty;

            if (File.Exists(executablePath))
            {
                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = executablePath,
                        Arguments = "rev-parse --show-toplevel",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };

                proc.Start();
                proc.WaitForExit();

                while (!proc.StandardOutput.EndOfStream)
                {
                    string line = proc.StandardOutput.ReadLine();
                    if (!line.ToUpper().Contains("FATAL"))
                    {
                        response = line;
                        break;
                    }
                }
            }

            return response;
        }
    }
}
