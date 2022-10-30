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
        public TileDecal TileDecal { get; set; }
        public Vector2D Coordinates { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="surface"></param>
        public Tile(int x, int y, TileSurface surface)
        {
            TileID = $"T{x}x{y}";
            Coordinates = new Vector2D(x, y);
            Surface = surface;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="decal"></param>
        public void SetDecal( TileDecal decal )
        {
            TileDecal = decal;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="surface"></param>
        public void SetSurface( TileSurface surface )
        {

            Surface = surface;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetCoordinates( int x, int y )
        {
            Coordinates.X = x;
            Coordinates.Y = y;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vector2"></param>
        public void SetCoordinates(Vector2D vector2)
        {
            Coordinates = vector2;
        }

    }


    /// <summary>
    /// The types of surfaces that a tile can have
    /// </summary>
    public enum TileSurface
    {
        None    = 1,     // 0000001
        Void    = 2,     // 0000010
        Ocean   = 4,     // 0000100
        Grass   = 8,     // 0001000
        Sand    = 16,    // 0010000
        Snow    = 32,    // 0100000
        Stone   = 64,    // 1000000
    }


    /// <summary>
    /// The fetures a tile can posses
    /// </summary>
    public enum TileDecal
    {
        None    = 1,     // 000000
        Town    = 2,     // 000001
        Road    = 4,     // 000010
        River   = 8,     // 000100
        Mountin = 16,    // 001000
        Hils    = 32,    // 010000
        Forest  = 64,    // 100000
    }
}
