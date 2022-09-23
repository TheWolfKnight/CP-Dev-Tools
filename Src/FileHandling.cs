using System;
using System.IO;
using System.Reflection;


namespace CP_Dev_Tools.Src
{
    public class FileHandling {

        private static string assemblyLoaction = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        /// <summary>
        /// Reads a file into memory and returns it to the program
        /// </summary>
        /// <param name="filePath"> Contains the path to the disired file </param>
        /// <returns> The contents of the specified file </returns>
        public static string ReadFile( string filePath )
        {

            string path = $@"{assemblyLoaction}\{filePath}";

            if ( !File.Exists( path ) )
                throw new FileNotFoundException($"could not find the file path: {path}");
            string r = File.ReadAllText( path );

            return r;
        }


        /// <summary>
        /// Emptys the file at the designated path
        /// </summary>
        /// <param name="filePath"> contains the path to the desired file </param>
        /// <param name="content"> the content to be writen in the file </param>
        /// <param name="overwrite"> tells the function wether or not it can overwrite the file at the destination </param>
        /// <returns> returns true if it was allowed to write to file, false if the file existed. </returns>
        public static bool WriteFile( string filePath, string content, bool overwrite=false )
        {

            string path = $@"{assemblyLoaction}\{filePath}";
            if (File.Exists(path) && !overwrite)
            {
                return false;
            }
            
            using (StreamWriter wstream = new StreamWriter(new FileStream(path, FileMode.Open, FileAccess.Write)))
            {
                wstream.Write(content);
                wstream.Flush();
            }

            return true;

        }


        /// <summary>
        /// Appends an element to a file
        /// </summary>
        /// <param name="filePath"> contains the path to the desired file </param>
        /// <param name="content"> the content to be appended to the file </param>
        public static void AppendFile( string filePath, string content)
        {
            string path = $@"{assemblyLoaction}\{filePath}";
            using ( StreamWriter wstream = new StreamWriter(new FileStream(path, FileMode.Append, FileAccess.Write)) )
            {
                wstream.WriteLine(content);
                wstream.Flush();
            }

        }


        /// <summary>
        /// Emptys a file of its contents by deletion, then it rewrites a file to the samme path
        /// </summary>
        /// <param name="filePath"> contains the path to the desired file </param>
        /// <param name="overwrite"> tells wether or not the file may be overwriten </param>
        /// <returns> Returns true if it was allowed to empty the file, else returns false </returns>
        public static bool EmptyFile( string filePath, bool overwrite=false )
        {
            if (File.Exists(filePath) && !overwrite)
            {
                return false;
            }

            string path = $@"{assemblyLoaction}\{filePath}";
            FileStream f = File.Open(path, FileMode.Open);
            // Deletes the file by setting its length to 0
            f.SetLength(0);
            f.Close();
            // Creates a new file in place of the old file
            f = File.Create(path);
            f.Close();

            // This shit must be done to delete the content of a file becouse C# is some shit

            return true;
        }

    }

    class CannotReadFileException : Exception
    {
        public CannotReadFileException() : base() { }
        public CannotReadFileException( string msg ) : base( msg ) { }
        public CannotReadFileException( string msg, Exception inner ) : base( msg, inner ) { }
    }

}
