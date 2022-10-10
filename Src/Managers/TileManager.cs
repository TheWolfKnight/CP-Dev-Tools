using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CP_Dev_Tools.Src.Models;

namespace CP_Dev_Tools.Src.Managers
{
    class TileManager
    {
        public List<Tile> TileSet { get; private set; }
        public TileSurface DefualtSurface { get; private set; }
        private int[] Dims { get; set; }

        public TileManager( int[] dims, TileSurface tileSurface )
        {
            Dims = dims;
            DefualtSurface = tileSurface;
            InitArray();
        }

        /// <summary>
        /// Creates the initial Tile array that is used to keep track of the tiles of the program
        /// </summary>
        /// <param name="tileSurface"> Surface used for the tiles on creation </param>
        private void InitArray()
        {
            TileSet = Enumerable.Range(0, Dims[0] * Dims[1]).Select(i => CreateTile(i)).ToList();
        }


        private Tile CreateTile( int position )
        {
            int y = (int)Math.Floor( (double)position / Dims[1] );
            int x = position - (y * Dims[1]);
            return new Tile(x, y, DefualtSurface);
        }


        /// <summary>
        /// Resizes the array for the tile set, this action can, and will, delete data outside the 
        /// </summary>
        /// <param name="dims"> Dimensions for the new tile set </param>
        public void Resize(int[] dims, TileSurface defaultSurface=TileSurface.Void)
        {

            throw new Exception("TBD");

            return;

        }

        /// <summary>
        /// Replaces the data of a specific tile, at the given coordinates
        /// </summary>
        /// <param name="tile"> The new data for a tile </param>
        public void ChangeTile(Tile tile)
        {

            throw new Exception("TBD");

            return;
        }

    }
}
