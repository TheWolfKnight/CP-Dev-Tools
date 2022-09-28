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

        public Tile(int x, int y, TileSurface surface )
        {
            TileID = $"{x}:{y}";
            Coordinates = new Vector2D(x, y);
            Surface = surface;
        }

        public void SetDecal( TileDecal decal )
        {
            TileDecal = decal;
        }

        public void SetSurface( TileSurface surface )
        {

            Surface = surface;
        }

        public void SetCoordinates( int x, int y )
        {
            Coordinates.X = x;
            Coordinates.Y = y;
        }

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
