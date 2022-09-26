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
            
        }
    }

    public enum Decals
    {
        Town,
        Road,
        River,
        Mountin,
        Forest,
    }

}
