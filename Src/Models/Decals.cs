using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CP_Dev_Tools.Src.Models
{
    public class TileDecal
    {
        public Tile Owner { get; set; }
        public Decals Decal { get; private set; }
        public string DecalID { get; private set; }

        public TileDecal( int[] dims, Decals decal )
        {
            Decal = decal;
            DecalID = $"D{dims[0]}x{dims[1]}";
        }

        public void SetDecal( Decals decal )
        {
            Decal = decal;
        }
    }

    public enum Decals
    {
        None,
        Town,
        Road,
        River,
        Mountin,
        Hils,
        Forest,
    }

}
