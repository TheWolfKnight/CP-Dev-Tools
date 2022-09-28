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
        private int[] Dims { get; set; }

        public TileManager( int[] dims, TileSurface tileSurface )
        {
            TileSet = new Tile[dims[0], dims[1]];
            Dims = dims;
            InitArray( tileSurface );
        }

        private void InitArray( TileSurface tileSurface )
        {
            for (int i = 0; i < Dims[0]; i++)
            {
                for (int j = 0; j < Dims[1]; j++)
                {
                    TileSet[i, j] = new Tile(j, i, tileSurface);
                }
            }
            return;
        }

    }
}
