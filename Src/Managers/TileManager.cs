﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using CP_Dev_Tools.Src.Models;
using CP_Dev_Tools.Src.Services;

namespace CP_Dev_Tools.Src.Managers
{
    class TileManager
    {
        public static TileSurface DefualtSurface { get; private set; }
        private static int[] Dims { get; set; }
        private TempFileService FileService;

        private static int CurrentActiveThreads;

        /// <summary>
        /// Creates an instance of the TileManager class.
        /// </summary>
        /// <param name="dims"> The dimenisions of the Tile map </param>
        /// <param name="tileSurface"> The default surface for all tiles to start with </param>
        public TileManager( int[] dims, TileSurface tileSurface )
        {
            Dims = dims;
            DefualtSurface = tileSurface;
            FileService = new TempFileService();
            InitArray();
        }


        /// <summary>
        /// Creates the Tile temp file, using the TempFileService.
        /// </summary>
        /// <param name="tileSurface"> Surface used for the tiles on creation </param>
        private void InitArray()
        {
            int amt = Dims[0] * Dims[1];
            // The amount of threas that is needed to write the tmp file;
            // 25 * 25 = 625,
            // Minimum of 1 thread will be used
            int threadCount = (int)Math.Max(1, Math.Floor((double)amt / 625));
            List<TempFileService> tmpFiles = Enumerable.Range(0, threadCount).Select(i => new TempFileService()).ToList();

            float f_tilesPerThread = amt / threadCount;
            bool isNotWhole = false;

            if (f_tilesPerThread % 1 != 0)
            {
                isNotWhole = true;
                f_tilesPerThread -=  f_tilesPerThread % 1;
            }
            int i_tilesPerThread = (int)f_tilesPerThread;

            for ( int i = 0, currentTileNr = 0; i < threadCount; i++, currentTileNr += i_tilesPerThread )
            {
                if ( isNotWhole && i == threadCount-1 )
                    i_tilesPerThread++;

                Thread t = new Thread(WriteTMPFiles);
                ThreadData data = new ThreadData(i, currentTileNr, currentTileNr + i_tilesPerThread, tmpFiles[i]);
                t.Start(data);
                CurrentActiveThreads++;
            }

            while ( CurrentActiveThreads > 0 ) { }

            StitchFilesToggether( tmpFiles );

        }


        /// <summary>
        /// Task for threads to compleate
        /// </summary>
        /// <param name="parameters"></param>
        private static void WriteTMPFiles(object parameters)
        {
            ThreadData data = (ThreadData)parameters;

            for ( int i = data.Start; i < data.Stop; i++ )
            {
                data.TmpService.CreateTile(i, Dims, DefualtSurface);
            }

            CurrentActiveThreads--;
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


        public IEnumerable<Tile> TileSet()
        {
            foreach ( Tile tile in FileService.GetTiles() )
            {
                yield return tile;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        private void StitchFilesToggether(List<TempFileService> list)
        {
            foreach ( TempFileService item in list )
            {
                string chunks = item.GetFullFile();
                FileService.WriteData(chunks);
            }
        }


        /// <summary>
        /// Writes tiles to a specified file service
        /// </summary>
        /// <param name="nr"> The nr of tile this is in comparison to when it was created </param>
        /// <param name="fileService"></param>
        public void CreateTile(int nr, TempFileService fileService)
        {
            int y = (int)Math.Floor((double)nr / Dims[1]);
            int x = nr - (y * Dims[1]);
            Tile tmp = new Tile(x, y, DefualtSurface);
            fileService.WriteData(tmp);

            return;
        }
    }

    internal struct ThreadData
    {
        public int I;
        public int Start;
        public int Stop;
        public TempFileService TmpService;

        public ThreadData( int i, int a, int b, TempFileService c )
        {
            I = i;
            Start = a;
            Stop = b;
            TmpService = c;
        }

        public override string ToString()
        {
            return $"ThreadData(I={I}, Start={Start}, Stop={Stop}, TmpService={TmpService.TMPFilePath})";
        }

    }

}
