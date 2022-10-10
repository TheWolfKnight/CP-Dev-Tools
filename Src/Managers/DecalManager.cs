using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CP_Dev_Tools.Src.Models;

namespace CP_Dev_Tools.Src.Managers
{
    public class DecalManager
    {
        public List<TileDecal> ActiveDecals { get; private set; }
        public Vector2D Dims { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dims"></param>
        /// <param name="defaultDecal"></param>
        public DecalManager( int[] dims, Decals defaultDecal=Decals.None )
        {
            Dims = new Vector2D(dims[0], dims[1]);
            ActiveDecals = Enumerable.Range(0, dims[0] * dims[1]).Select(i => CreateDecal(i)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dims"></param>
        /// <param name="defaultDecal"></param>
        public DecalManager(Vector2D dims, Decals defaultDecal=Decals.None )
        {
            Dims = dims;

            ActiveDecals = Enumerable.Range(0, dims.X * dims.Y).Select(i => CreateDecal(i)).ToList();
        }

        public void ChangeDecal( int[] dims, Decals change )
        {
            throw new Exception("TBD");
        }

        private TileDecal CreateDecal( int position )
        {

            int y = (int)Math.Floor((double)position / Dims.Y);
            int x = position - (y * Dims.X);

            return new TileDecal(new int[2] { x, y }, Decals.None);
        }


    }
}
