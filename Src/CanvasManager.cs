using System;
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

        private readonly string TilePrefix = $@"{Environment.CurrentDirectory}\Gfx\Tiles\";
        private readonly int[] TileDims = new int[2] { 22, 26 };
        private readonly int TileHalfWidth = 11;

        private TileManager TileManagerHolder;

        public CanvasManager( Canvas mapCanvas )
        {
            MapCanvas = mapCanvas;
        }


        /// <summary>
        /// Draws the initial canvas element when the map is created.
        /// </summary>
        /// <param name="mapDims"> The dimentions of the map that will be created on the inital load </param>
        /// <param name="defualtSurface"> The default surface tile to be drawn. Defualt value: TileSurface.Void </param>
        public void DrawInitial( int[] mapDims, TileSurface defualtSurface = TileSurface.Void )
        {
            MapCanvas.Children.Clear();

            MapCanvas.Width = mapDims[0] * TileDims[0] + 40;
            MapCanvas.Height = mapDims[1] * 19 + 40;

            TileManagerHolder = new TileManager(mapDims, defualtSurface);

            TileManagerHolder.TileSet.ForEach(yDim => yDim.ForEach(tile => Draw(tile)));

        }


        /// <summary>
        /// Takes a Tile instance and draws an image to the screen with the given elements
        /// of the Tile. Need to rework this, it does not work for more than 10 or so tiles in either direction.
        /// </summary>
        /// <param name="toDraw"> The Tile instance to draw to the screen </param>
        private void Draw( Tile toDraw )
        {
            Image img = GenerateImage(toDraw.Surface);

            double x = toDraw.Coordinates.X * TileDims[0];

            // Need to put the y down by ca. 3/4 of the total length of the img size, 26/4 = 19.5.
            // Just don't realy want the .5 in there couse dealing with floats can be bad in big sample sizes.
            double y = toDraw.Coordinates.Y * 19;

            if (toDraw.Coordinates.Y % 2 != 0)
                x += TileHalfWidth;

            img.Margin = new Thickness(x, y, 0, 0);
            Panel.SetZIndex(img, 0);
            MapCanvas.Children.Add(img);

        }


        /// <summary>
        /// Takes a TileChanger and replaces a tile on the screen with a new Tile.
        /// </summary>
        /// <param name="changer">
        /// TileChanger structure, containing both the Tile that will be hanged
        /// And the til that will be changed
        /// </param>
        public void Draw( TileChange changer )
        {
            Tile toDraw = changer.replacer;
            Image img = GenerateImage(toDraw.Surface);

            img.Margin = changer.ToChange.Margin;

            MapCanvas.Children.Add(img);
            MapCanvas.Children.Remove(changer.ToChange);

        }


        /// <summary>
        /// Generates an image instance used when appending to the canvas children.
        /// Rotates the given image by 90 degress to achive the correct result. Do not let people make their own tile types.
        /// </summary>
        /// <param name="surface"></param>
        /// <returns> The Image instance that is created for the draw call </returns>
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
        /// Resizes the canvas to a specified grid size. WARNING: this can delete progress if the size is lowerd.
        /// </summary>
        /// <param name="resizeValues"> The XY values for the new tile set </param>
        /// <param name="defualtSurface"> The default tile to be placed when the reszie results in a bigger surface. Defualt surface: TileSurface.Void </param>
        public void ResizeCanvas( int[] resizeValues, TileSurface defualtSurface = TileSurface.Void )
        {
            TileManagerHolder.Resize(resizeValues, defualtSurface);

            MapCanvas.Width = resizeValues[0] * TileDims[0] + TileDims[0];
            MapCanvas.Height = resizeValues[1] * TileDims[1] + TileDims[1];

        }


        /// <summary>
        /// Takes a tile element and replaces the already existing element in its current position
        /// </summary>
        /// <param name="change"> The element that the tile, with the same TileID, will be changed into </param>
        public void ChangeTileElement( Tile change )
        {
            Image result = (Image)MapCanvas.FindName(change.TileID);

            if (result is null)
            {
                MessageBox.Show("Could not find a tile with the given id", "Error", MessageBoxButton.OK);
            }

            TileChange tileChange = new TileChange();
            tileChange.replacer = change;
            tileChange.ToChange = result;

            Draw(tileChange);

        }

    }

    internal struct TileChange
    {
        public Image ToChange;
        public Tile replacer;
    }

}
