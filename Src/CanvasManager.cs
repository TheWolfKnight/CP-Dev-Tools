﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;


using CP_Dev_Tools.Src;
using CP_Dev_Tools.Src.Models;


namespace CP_Dev_Tools.Src
{
    class CanvasManager
    {
        public Canvas MapCanvas { get; set; }

        private const string TilePrefix = @"..\Gfx\Tiles\";
        private readonly int[] TileDims = new int[2] { 26, 23 };

        private TileManager TileManagerHolder;

        public CanvasManager( Canvas mapCanvas )
        {
            MapCanvas = mapCanvas;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapDims"> The dimentions of the map that will be created on the inital load </param>
        /// <param name="defualtSurface"> The default surface tile to be drawn. Defualt value: TileSurface.Void </param>
        public void DrawInitial( int[] mapDims, TileSurface defualtSurface = TileSurface.Void )
        {
            MapCanvas.Width = TileDims[0] * mapDims[0];
            MapCanvas.Height = TileDims[1] * mapDims[1];

            TileManagerHolder = new TileManager(mapDims, defualtSurface);

            foreach ( Tile tile in TileManagerHolder.TileSet )
            {
                Draw(tile);
            }

        }

        /// <summary>
        /// Takes a Tile instance and draws an image to the screen with the given elements
        /// of the Tile
        /// </summary>
        /// <param name="toDraw"> The Tile instance to draw to the screen </param>
        private void Draw( Tile toDraw )
        {
            Image img = GenerateImage(toDraw.Surface);

            double x = toDraw.Coordinates.X * TileDims[0];

            if (toDraw.Coordinates.X % 2 != 0)
                x += TileDims[0] * .5d;

            img.Margin = new Thickness(x, toDraw.Coordinates.Y, 0, 0);
        }

        /// <summary>
        /// Takes a TileChanger and replaces a tile on the screen with a new Tile.
        /// </summary>
        /// <param name="changer">
        /// TileChanger structure, containing both the Tile that will be hanged
        /// And the til that will be changed
        /// </param>
        private void Draw( TileChange changer )
        {
            Image img = GenerateImage(changer.replacer.Surface);
            double x = changer.replacer.Coordinates.X * TileDims[0];

            if (changer.replacer.Coordinates.X % 2 != 0)
                x += TileDims[0] * .5d;

            img.Margin = new Thickness(x, changer.replacer.Coordinates.Y, 0,0);
            // TBD

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="surface"></param>
        /// <returns></returns>
        private Image GenerateImage( TileSurface surface )
        {
            Image img = new Image();
            BitmapImage bitmap = new BitmapImage(new Uri($"{TilePrefix}{surface.ToString()}Tile.png"));
            img.Source = bitmap;
            img.Width = bitmap.Width;
            img.Height = bitmap.Height;
            img.HorizontalAlignment = HorizontalAlignment.Left;
            img.VerticalAlignment = VerticalAlignment.Top;

            return img;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="resizeValues"> The XY values for the new tile set </param>
        /// <param name="defualtSurface"> The default tile to be placed when the reszie results in a bigger surface. Defualt surface: TileSurface.Void </param>
        public void ResizeCanvas( int[] resizeValues, TileSurface defualtSurface = TileSurface.Void )
        {

        }


        /// <summary>
        /// Takes a tile element and replaces the already existing element in its current position
        /// </summary>
        /// <param name="change"> The element that the tile, with the same TileID, will be changed into </param>
        public void ChangeTileElement( Tile change )
        {
            UIElement result = (UIElement)MapCanvas.FindName(change.TileID);

            if (result is null)
            {
                MessageBox.Show("Could not find a tile with the given id", "Error", MessageBoxButton.OK);
            }

            TileChange tileChange = new TileChange();
            tileChange.replacer = change;
            tileChange.toChange = result;

            Draw(tileChange);

        }

    }

    internal struct TileChange
    {
        public UIElement toChange;
        public Tile replacer;
    }

}