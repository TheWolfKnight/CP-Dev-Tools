using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CP_Dev_Tools.Src.Models
{
    public class TileDecal
    {
        public float MovementMod { get; private set; }
        public Decals Decal { get; private set; }

        public TileDecal( float movementMod, Decals decal )
        {
            MovementMod = movementMod;
            Decal = decal;
        }

        public void SetDecal( Decals decal )
        {
            Decal = decal;
            
            switch ( decal )
            {
                case Decals.Town:
                    break;
                case Decals.Road:
                    break;
                case Decals.River:
                    break;
                case Decals.Mountin:
                    break;
                case Decals.Forest:
                    break;
                case Decals.Hils:
                    break;
                default:
                    throw new Exception("Unrechable code");
            }

            return;

        }
    }

    public enum Decals
    {
        Town,
        Road,
        River,
        Mountin,
        Hils,
        Forest,
    }

}
