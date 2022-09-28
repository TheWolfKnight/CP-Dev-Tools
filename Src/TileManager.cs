using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CP_Dev_Tools.Src.Models;

namespace CP_Dev_Tools.Src
{
    class TileManager
    {
        public Tile[,] TileSet { get; private set; }
        public TileSurface DefualtSurface { get; private set; }
        private int[] Dims { get; set; }

        public TileManager( int[] dims, TileSurface tileSurface )
        {

            TileSet = new Tile[dims[1], dims[0]];
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
            for (int i = 0; i < Dims[1]; i++)
            {
                for (int j = 0; j < Dims[0]; j++)
                {
                    TileSet[i, j] = new Tile(j, i, DefualtSurface);
                }
            }
            return;
        }

        /// <summary>
        /// Resizes the array for the tile set, this action can, and will, delete data outside the 
        /// </summary>
        /// <param name="dims"> Dimensions for the new tile set </param>
        public void Resize(int[] dims)
        {
            Tile[,] tiles = new Tile[dims[0], dims[1]];

            for ( int i = 0; i < dims[0]; i++ )
            {
                for ( int j = 0; j < dims[1]; j++ )
                {
                    if (i < Dims[0] && j < Dims[1])
                        tiles[i, j] = TileSet[i, j];
                    else
                        tiles[i, j] = new Tile(j, i, DefualtSurface);
                }
            }

            TileSet = (Tile[,])tiles.Clone();
            return;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coords"></param>
        /// <param name="tile"></param>
        public void ChangeTile( int[] coords, Tile tile)
        {
            Tile activeTile = TileSet[coords[0], coords[1]];
            activeTile.SetSurface(tile.Surface);
            activeTile.SetDecal(tile.TileDecal);

            TileSet[coords[0], coords[1]] = activeTile;

            return;

        }

    }
}
