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
        public Vector2D Coordinates { get; private set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="surface"></param>
        public Tile(int x, int y, TileSurface surface )
        {
            TileID = $"{x}:{y}";
            Coordinates = new Vector2D(x, y);
            Surface = surface;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="decal"></param>
        public void SetDecal( TileDecal decal )
        {
            decal.Owner = this;
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

    public enum TileSurface
    {
        Void,
        Ocean,
        Grass,
        Sand,
        Snow,
        Stone,
    }

}
