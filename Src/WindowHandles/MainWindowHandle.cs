using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Controls;

using CP_Dev_Tools.Frontend;
using CP_Dev_Tools.Src.Models;
using CP_Dev_Tools.Src.Managers;
using CP_Dev_Tools.Src.Exceptions;

using static CP_Dev_Tools.Src.Services.FileHandling;

namespace CP_Dev_Tools.Src.WindowHandles
{
    public class MainWindowHandle
    {

        public MainWindow Owner { get; private set; }

        private string PathToCurrentCase;
        private CanvasManager Manager;
        private Thread SavingThread;
        private PlacementItem Item = new PlacementItem();

        private bool[] SaveableElements = new bool[3] { false, false, false };


        /// <summary>
        /// Spawns a new MainWindowHandle instance, sets the Owner field to the given owner parameter.
        /// </summary>
        /// <param name="owner"></param>
        public MainWindowHandle(MainWindow owner)
        {
            Owner = owner;
        }


        /// <summary>
        /// Inits the main window with a CanvasManager instance
        /// </summary>
        public void InitMainWindow()
        {
            Manager = new CanvasManager(Owner.MapCanvas);
        }


        /// <summary>
        /// Handles the Open_Click event, makes the program read in a existing project folder.
        /// </summary>
        public void OpenClickEvent()
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "JSON file (*.json)| *.json | All files (*.*) | *.*";
                fileDialog.FilterIndex = 1;
                fileDialog.CheckFileExists = true;
                fileDialog.InitialDirectory = @"C:\";

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    PathToCurrentCase = fileDialog.FileName;
                }
            }
        }


        /// <summary>
        /// Detects left button click on the canvas element,
        /// replaces the element under the mose with what is definded by the PlacmentItem Item field
        /// </summary>
        /// <param name="element"> The element under the mouse when the user clicks left mouse button </param>
        public void MapCanvasLeftButtonDownEvent(FrameworkElement element)
        {

            if (Item.Current == CurrentPlacementItem.None)
                return;

            string elementNameProperty = element.Name;

            if ( Item.Current == CurrentPlacementItem.Surface )
            {

            } else if ( Item.Current == CurrentPlacementItem.Decal )
            {

            }

            else throw new UnrechableCodeException();


        }


        /// <summary>
        /// Detects the right button down event for the canvas element,
        /// then spawns a dialog box for the given element under the mouse with the specific options for that element
        /// </summary>
        /// <param name="element"> The element under the mouse when the right button is clicked </param>
        public void MapCanvasRightButtonDownEvent(FrameworkElement element)
        {
            MapItemDetailsWindow detailsWindow = new MapItemDetailsWindow(element);
            detailsWindow.ShowDialog();
        }


        /// <summary>
        /// Routes the data of the current canvas to the FileHandle service.
        /// This is done on a thread so the user can work in the background.
        /// 
        /// NOTE: This will save to the current project folder if defined, else it will run the SaveAsClickEvent method.
        /// 
        /// </summary>
        public void SaveClickEvent()
        {
            if (PathToCurrentCase is null)
            {
                SaveAsClickEvent();
                return;
            }

            SavingThread = SpawnNewSaveThread(PathToCurrentCase, /* Need to get the content at this point, TBD */ "");
            SavingThread.Start();
        }


        /// <summary>
        /// Routes the data of the current canvas to the FileHandle service.
        /// This is done on a thread so the user can work in the background.
        /// 
        /// NOTE: The save destination is decided by the user via the FolderBrowserDialog class.
        /// 
        /// </summary>
        public void SaveAsClickEvent()
        {

            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.ShowNewFolderButton = true;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    if (SavingThread.IsAlive)
                        SavingThread.Abort();

                    string path = folderDialog.SelectedPath;
                    SavingThread = SpawnNewSaveThread(path, /* Need to get the content at this point, TBD */ "");
                    SavingThread.Start();

                }

            }

        }


        /// <summary>
        /// Detects the click of the new map button, spawns a new SizeWindow element.
        /// </summary>
        public void NewMapClickEvent()
        {
            SizeWindow sizeWindow = new SizeWindow();
            sizeWindow.Caller = this;
            sizeWindow.New = true;
            sizeWindow.Show();
        }


        /// <summary>
        /// Changes the size of the map on canvas, this can both be safe ( does not remove any user data )
        /// and unsafe ( remove user data on downsize )
        /// </summary>
        public void ChangeSizeClickEvent()
        {
            SizeWindow sizeWindow = new SizeWindow();
            sizeWindow.Caller = this;
            sizeWindow.New = false;
            sizeWindow.Show();
        }


        /// <summary>
        /// Removes all tiles from the canvas elements, making the screen blank.
        /// WARNING: THIS WILL REMOVE ALL PROGRESS IF PROCED
        /// </summary>
        public void ClearMapClickEvent()
        {
            Manager.MapCanvas.Children.Clear();
        }


        /// <summary>
        /// Detects the clicked element und the Edit menu item, then routes the specific choice
        /// to the desired method
        /// </summary>
        /// <param name="element"> The framework element effcted by the mouse click </param>
        public void EditChildElementClickEvent(FrameworkElement element)
        {

            bool init = true;
            int[] dims;

            switch (element.Name)
            {
                case "small":
                    dims = new int[] { 25, 25 };
                    break;
                case "medium":
                    dims = new int[] { 50, 50 };
                    break;
                case "large":
                    dims = new int[] { 100, 100 };
                    break;
                case "ultra_large":
                    dims = new int[] { 255, 255 };
                    break;
                case "custom_size":
                    if (Manager.MapCanvas.Children.Count > 0)
                        ChangeSizeClickEvent();
                    else
                        NewMapClickEvent();
                    return;
                case "clear_map":
                    Manager.MapCanvas.Children.Clear();
                    return;
                default:
                    throw new UnrechableCodeException();
            }

            SizeWindowCall(dims, init);

        }


        /// <summary>
        /// Detects the click of a TreeViewItem and then sets the field PlacementItem Item to the correct state
        /// </summary>
        /// <param name="element"> The framework element effected by the mouse click </param>
        public void TreeViewMouseDownEvent(FrameworkElement element)
        {
            if (element.Parent.GetType().ToString() != "System.Windows.Controls.TreeViewItem")
                return;

            TreeViewItem treeViewItem = (TreeViewItem)element;
            TreeViewItem parent = (TreeViewItem)element.Parent;

            int r;

            switch (parent.Tag)
            {
                case "tile":
                    bool success = int.TryParse(element.Tag.ToString(), out r);
                    if (!success)
                        throw new FormatException();
                    HandlePlacemanetItemChangeTile(r);
                    break;
                case "decal":
                    bool sucess = int.TryParse(element.Tag.ToString(), out r);
                    if (!sucess)
                        throw new FormatException();
                    HandlePlacementItemChangeDecal(r, treeViewItem.Header.ToString());
                    break;
                default:
                    throw new UnrechableCodeException();
            }
        }


        /// <summary>
        /// Changes the field PlacementItem Item to the selected tile.
        /// </summary>
        /// <param name="i"> The TileSurface that the user selected </param>
        private void HandlePlacemanetItemChangeTile(int i)
        {
            TileSurface newTileSurface = (TileSurface)i;
            Item.SetSurface(newTileSurface);

        }


        /// <summary>
        /// Changes the field PlacementItem Item to the selected decal.
        /// </summary>
        /// <param name="i"> The Decal that the user selected </param>
        /// <param name="ident"> An identifier for if the program to determin the potential need, for some program pre-setup </param>
        private void HandlePlacementItemChangeDecal(int i, string ident)
        {
            throw new TBD();
        }


        /// <summary>
        /// Handles the SizeWindows result in a way that makes it hopefuly safe for the end user,
        /// but no promisis.
        /// </summary>
        /// <param name="dims"> Dimensions of the new canvas </param>
        /// <param name="init"> Tells if this is a resize or the initialization of the canvas </param>
        public void SizeWindowCall(int[] dims, bool init = false)
        {
            if (init)
                Manager.DrawInitial(dims);
            else
                Manager.ResizeCanvas(dims);
        }


        /// <summary>
        /// Interfaces with the WriteFile function, saveing the content to the designated path.
        /// Makes sure the program does not overwrite someting unwanted
        /// </summary>
        /// <param name="path"> Path to the desired destination for the saved files </param>
        /// <param name="content"> The content to be saved, NOTE: should be change from a string with the content in it to something better, like making tmp files to house data and copying them </param>
        private void SaveContent(string path, string content)
        {
            if (!WriteFile(path, content))
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("You are about to overwrite a file, are you sure?", "Warning", MessageBoxButton.YesNo);
                if (result != MessageBoxResult.Yes)
                    return;
                WriteFile(path, content, overwrite: true);
            }
        }


        /// <summary>
        /// Spawns a new saving thread, that can save the current content in the background, while you still work
        /// </summary>
        /// <param name="path"> Path to the desired destination for the saved files </param>
        /// <param name="content"> The content to be saved, NOTE: should be change from a string with the content in it to something better, like making tmp files to house data and copying them </param>
        /// <returns> Returns a thread, setup to start saving the current elements </returns>
        private Thread SpawnNewSaveThread(string path, string content)
        {
            Thread t = new Thread(() => SaveContent(path, content));
            return t;
        }


        /// <summary>
        /// Changes a tile from the MapCanvas child elements
        /// </summary>
        /// <param name="tile"> The tile perameters that will be changed by the method </param>
        private void ChangeTile(FrameworkElement tile)
        {
            throw new TBD();
        }


        /// <summary>
        /// Changes a decal on a tile
        /// </summary>
        /// <param name="decal"> The dacal parameters that will be changed by the method </param>
        private void ChangeDecal(FrameworkElement decal)
        {
            throw new TBD();
        }
    }


    internal struct PlacementItem
    {
        public TileSurface Surface { get; private set; }
        public TileDecal Decal { get; private set; }
        public CurrentPlacementItem Current { get; private set; }

        public PlacementItem(TileSurface surface = TileSurface.None, TileDecal decal = TileDecal.None)
        {
            Surface = surface;
            Decal = decal;
            Current = CurrentPlacementItem.None;
        }

        public void SetSurface(TileSurface surface)
        {
            Surface = surface;
            Decal = TileDecal.None;
            Current = CurrentPlacementItem.Surface;
        }

        public void SetDecal(TileDecal decal)
        {
            Surface = TileSurface.None;
            Decal = decal;
            Current = CurrentPlacementItem.Decal;
        }
    }


    internal enum CurrentPlacementItem
    {
        None,
        Surface,
        Decal,
    }

}
