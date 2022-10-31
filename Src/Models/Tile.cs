using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP_Dev_Tools.Src.Models
{
    public class Tile
    {
        public string TileID { get; private set; }
        public TileSurface Surface { get; set; }
        public TileDecal Decal { get; set; }
        public Vector2D Coordinates { get; set; }


        public Tile() { }


        /// <summary>
        /// Creates a new tile instance with an x, y, surface and decal.
        /// </summary>
        /// <param name="x"> The X value for the Tile instance </param>
        /// <param name="y"> The  </param>
        /// <param name="surface"></param>
        public Tile(int x, int y, TileSurface surface=TileSurface.Void, TileDecal decal=TileDecal.None)
        {
            Coordinates = new Vector2D(x, y);
            WriteTileID();
            Surface = surface;
            Decal = decal;
        }


        /// <summary>
        /// Sets the decal for a Tile.
        /// </summary>
        /// <param name="decal"> The new Tile decal </param>
        public void SetDecal( TileDecal decal )
        {
            Decal = decal;
        }


        /// <summary>
        /// Sets the surface for a Tile
        /// </summary>
        /// <param name="surface"> The new Surface </param>
        public void SetSurface( TileSurface surface )
        {

            Surface = surface;
        }


        /// <summary>
        /// Sets the coordinates for a Tile
        /// </summary>
        /// <param name="x"> The new X value </param>
        /// <param name="y"> The new Y value </param>
        public void SetCoordinates( int x, int y )
        {
            if (Coordinates == null)
                Coordinates = new Vector2D();

            Coordinates.X = x;
            Coordinates.Y = y;
            WriteTileID();
        }


        /// <summary>
        /// Set the coordinates for a Tile
        /// </summary>
        /// <param name="vector2"> The new Vector position for the Tile </param>
        public void SetCoordinates(Vector2D vector2)
        {
            Coordinates = vector2;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private void WriteTileID()
        {
            TileID = $"T{Coordinates.X}x{Coordinates.Y}";
        }


        public override string ToString()
        {
            return $"Tile(TileID={TileID}, Coordinates={Coordinates}, Surface={Surface}, Decal={Decal})";
        }

    }


    /// <summary>
    /// The types of surfaces that a tile can have
    /// </summary>
    public enum TileSurface
    {
        None    = 0,     // Not used in the actual file system
        Void    = 1,     // 000001
        Ocean   = 2,     // 000010
        Grass   = 4,     // 000100
        Sand    = 8,     // 001000
        Snow    = 16,    // 010000
        Stone   = 32,    // 100000
    }


    /// <summary>
    /// The fetures a tile can posses
    /// </summary>
    public enum TileDecal
    {
        None    = 1,     // 0000001
        Town    = 2,     // 0000010
        Road    = 4,     // 0000100
        River   = 8,     // 0001000
        Mountin = 16,    // 0010000
        Hils    = 32,    // 0100000
        Forest  = 64,    // 1000000
    }
}
