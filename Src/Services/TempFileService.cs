using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;


using CP_Dev_Tools.Src.Models;


namespace CP_Dev_Tools.Src.Services
{
    public class TempFileService
    {

        public string TMPFilePath;

        /// <summary>
        /// Creates a new TempFileService instance, this will place a file in you local TEMP folder.
        /// The Class implements a destructor to clean up after it self, but can fail it the program
        /// is not closed correctly.
        /// </summary>
        public TempFileService()
        {
            try
            {
                TMPFilePath = Path.GetTempFileName();
                FileInfo info = new FileInfo(TMPFilePath);
                info.Attributes = FileAttributes.Temporary;
            } catch ( Exception ex )
            {
                Console.WriteLine( $"Failed to create a TEMP file, or set the attributes of it, please check the permisions of your acount", ex.Message );
            }
        }


        /// <summary>
        /// Appends a new tile to the tmp file, this creates a new tile to be used on the screen.
        /// </summary>
        /// <param name="chunk"></param>
        public void WriteData( Tile tile )
        {
            using ( StreamWriter writer = File.AppendText(TMPFilePath) )
            {
                string chunk = ConvertTileToChunk(tile);
                writer.WriteLine(chunk);
                writer.Flush();
            }
        }


        /// <summary>
        /// Writes a chunk at the given offset. WARNING: this is ment to overwrite data at the given offset,
        /// and cannot append to the file, if you want to write a new tile only give the function the new tile.
        /// </summary>
        /// <param name="chunkStart"></param>
        /// <param name="chunkData"></param>
        public void WriteData( int chunkStart, Tile chunkData )
        {
            string[] data = File.ReadAllText(TMPFilePath).Split('\n');
            string chunk = ConvertTileToChunk(chunkData);
            data[chunkStart] = chunk;
            File.WriteAllText(TMPFilePath, string.Join("\n", data));
        }


        /// <summary>
        /// Writes the entire file to the stdout
        /// </summary>
        public void DumpFile()
        {
            string data = File.ReadAllText(TMPFilePath);
            Console.WriteLine(data);

        }


        /// <summary>
        /// Converts a tile instance to a chunk for a file.
        /// </summary>
        /// <param name="tile"></param>
        /// <returns></returns>
        private string ConvertTileToChunk( Tile tile )
        {
            string r = "";
            r += $"{tile.Coordinates.X:x10} {tile.Coordinates.Y:x10} ";
            r += $"{(int)tile.Surface:x10} {(int)tile.TileDecal:x10}";
            return r;
        }



        /// <summary>
        /// Deletes the tmp file when the program is done with it
        /// </summary>
        ~TempFileService()
        {
            if (File.Exists(TMPFilePath))
                File.Delete(TMPFilePath);
            return;
        }


    }
}
