using System;
using System.Collections.Generic;
using System.Linq;

using System.IO;

using CP_Dev_Tools.Src.Models;


namespace CP_Dev_Tools.Src.Services
{
    public class TempFileService
    {
        public string TMPFilePath;

        /// <summary>
        /// Creates a new TempFileService instance, this will place a file in you local TEMP folder.
        /// The Class implements a destructor to clean up after it self, but can fail if the program
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
                throw new Exception( $"Failed to create a TEMP file, or set the attributes of it, please check the permisions of your acount", ex );
            }
        }


        /// <summary>
        /// Appends a new tile to the tmp file, this creates a new tile to be used on the screen.
        /// </summary>
        /// <param name="tile"> The tile data to be writen to the end of the file </param>
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
        /// <param name="chunkStart"> The offset at which the write process should be done </param>
        /// <param name="chunkData"> The data to be writen at the designated offset </param>
        public void WriteData( int chunkStart, Tile chunkData )
        {
            string[] data = File.ReadAllText(TMPFilePath).Split('\n');
            string chunk = ConvertTileToChunk(chunkData);
            data[chunkStart] = chunk;
            File.WriteAllText(TMPFilePath, string.Join("\n", data));
        }


        /// <summary>
        /// Writes a chunk of data to the end of the file at TMPFilePath.
        /// </summary>
        /// <param name="chunks"> The chunk of data to be writen </param>
        public void WriteData( string chunks )
        {
            using ( StreamWriter writer = File.AppendText(TMPFilePath) )
            {
                writer.Write(chunks);
                writer.Flush();
            }
        }


        /// <summary>
        /// Writes the entire file to the stdout
        /// </summary>
        public void DumpFile()
        {
            int i = 1;
            foreach (string line in File.ReadLines(TMPFilePath))
            {
                Console.Write($"{i}: ");
                Console.WriteLine(line);
                i++;
            }
        }


        /// <summary>
        /// Gets an IEnumerable of all the Tiles in the TEMP file.
        /// </summary>
        /// <returns> An IEnumrable of all tile </returns>
        public IEnumerable<Tile> GetTiles()
        {
            foreach ( string chunk in File.ReadLines(TMPFilePath) ) {
                Tile t = ConvertChunkToTile(chunk);
                yield return t;
            }
        }


        /// <summary>
        /// Gets the entire file as a string
        /// </summary>
        /// <returns> A string representation of the tmp file </returns>
        public string GetFullFile()
        {
            string data = File.ReadAllText(TMPFilePath);
            return data;
        }


        /// <summary>
        /// Gets a tile by a specific offset, in the TMP file.
        /// </summary>
        /// <param name="offset"> The offset from which the TEMPFileService instance will read at </param>
        /// <returns> The tile representation of the chunk data </returns>
        public Tile GetTileAtOffset( int offset )
        {
            string chunk = File.ReadLines(TMPFilePath).Skip(offset).FirstOrDefault();
            Tile tile = ConvertChunkToTile(chunk);
            return tile;
        }


        /// <summary>
        /// Converts a tile instance to a chunk for a file.
        /// </summary>
        /// <param name="tile"></param>
        /// <returns></returns>
        private string ConvertTileToChunk( Tile tile )
        {
            string r = "";
            r += $"{tile.Coordinates.X:x5} {tile.Coordinates.Y:x5} ";
            r += $"{(int)tile.Surface:x2} {(int)tile.Decal:x2}";
            return r;
        }


        /// <summary>
        /// Converts a pice of chunk data into a Tile instance
        /// </summary>
        /// <param name="chunk"> The data from the chunk </param>
        /// <returns> The Tile reprecentation of the Tile data </returns>
        private Tile ConvertChunkToTile(string chunk)
        {
            chunk = chunk.Trim();
            string[] split = chunk.Split(' ');
            Tile tmp = new Tile();
            try
            {
                int x = int.Parse(split[0], System.Globalization.NumberStyles.HexNumber);
                int y = int.Parse(split[1], System.Globalization.NumberStyles.HexNumber);
                tmp.SetCoordinates(x, y);
            } catch ( FormatException fe )
            {
                throw new InvalidDataException("The tile could not be parsed for it's coordinates", fe);
            }
            TileSurface surface = (TileSurface)int.Parse(split[2], System.Globalization.NumberStyles.HexNumber);
            TileDecal decal = (TileDecal)int.Parse(split[3], System.Globalization.NumberStyles.HexNumber);
            tmp.SetSurface(surface);
            tmp.SetDecal(decal);
            return tmp;
        }


        /// <summary>
        /// Deletes the tmp file when the instance goes out of scope
        /// </summary>
        ~TempFileService()
        {
            if (File.Exists(TMPFilePath))
                File.Delete(TMPFilePath);
            return;
        }

    }
}
