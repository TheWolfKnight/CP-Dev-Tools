﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Controls;

using CP_Dev_Tools.Src.Managers;
using CP_Dev_Tools.Frontend;
using CP_Dev_Tools.Src.Models;

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
        /// 
        /// </summary>
        /// <param name="owner"></param>
        public MainWindowHandle( MainWindow owner )
        {
            Owner = owner;
        }


        /// <summary>
        /// 
        /// </summary>
        public void InitMainWindow()
        {
            Manager = new CanvasManager(Owner.MapCanvas);
        }


        /// <summary>
        /// 
        /// </summary>
        public void OpenClickEvent()
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "JSON file (*.json)| *.json | All files (*.*) | *.*";
                fileDialog.FilterIndex = 1;
                fileDialog.CheckFileExists = true;
                fileDialog.InitialDirectory = @"C:\";

                if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    PathToCurrentCase = fileDialog.FileName;
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        public void MapCanvasLeftButtonDownEvent( FrameworkElement element )
        {
            string elementName = element.Name;


            bool[] h = new bool[2] { Item.Decal == Decals.None, Item.Surface == TileSurface.None };


            if (h[0] && !h[1])
            {
                // Triggers if the decal is active, and the tile surface is not
            }
            else if (!h[0] && h[1])
            {
                // Triggers if the tile surface is active, and the decal is not
            }
            else throw new Exception("Unrechable code");

            
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        public void MapCanvasRightButtonDownEvent( FrameworkElement element )
        {
            throw new Exception("TBD");
        }


        /// <summary>
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
        /// 
        /// </summary>
        public void NewMapClickEvent()
        {
            SizeWindow sizeWindow = new SizeWindow();
            sizeWindow.Caller = this;
            sizeWindow.New = true;
            sizeWindow.Show();
        }


        /// <summary>
        /// 
        /// </summary>
        public void ChangeSizeClickEvent()
        {
            SizeWindow sizeWindow = new SizeWindow();
            sizeWindow.Caller = this;
            sizeWindow.New = false;
            sizeWindow.Show();
        }


        /// <summary>
        /// 
        /// </summary>
        public void ClearMapClickEvent()
        {
            Manager.MapCanvas.Children.Clear();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"> The framework element effcted by the mouse click </param>
        public void EditChildElementClickEvent( FrameworkElement element )
        {

            bool init = true;
            int[] dims;

            switch ( element.Name )
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
                    throw new Exception("Unrechable code");
            }

            SizeWindowCall(dims, init);

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"> The framework element effected by the mouse click </param>
        public void TreeViewMouseDownEvent( FrameworkElement element )
        {
            throw new Exception("TBD");
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
        /// <returns></returns>
        private Thread SpawnNewSaveThread(string path, string content)
        {
            Thread t = new Thread(() => SaveContent(path, content));
            return t;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tile"></param>
        private void ChangeTile( FrameworkElement tile )
        {

        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="decal"></param>
        private void ChangeDecal( FrameworkElement decal )
        {

        }



    }


    internal struct PlacementItem
    {
        public TileSurface Surface { get; private set; }
        public Decals Decal { get; private set; }

        public PlacementItem(TileSurface surface=TileSurface.None, Decals decal=Decals.None)
        {
            Surface = surface;
            Decal = decal;
        }

        public void SetSurface( TileSurface surface )
        {
            Surface = surface;
            Decal = Decals.None;
        }

        public void SetDecal( Decals decal )
        {
            Surface = TileSurface.None;
            Decal = decal;
        }

    }

}
