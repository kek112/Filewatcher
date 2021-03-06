﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kley.Base.Infrastructure;
using System.Windows.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.IO;
using System.Windows.Media.Imaging;
using System.Reflection;
using System.Windows.Media;
using System.Xml;
using System.Windows;

namespace FileWatcher.Model
{
    class MainModel : BaseNotifyPropertyChanged
    {
        #region Fields

        private ImageSource activeImage;
        private string sourceFilePath;
        private string targetFilePath;
        private string informationContent;
        private string statusBar;
        private string extraExtension;
        private int selectedListBoxItemIndex;
        private bool elementActive;
        private List<string> bottomList;
        private List<string> CheckedExtensions = new List<string>();

        private CheckableObservableCollection<string> items;
        private RelayCommand scanFolderCommand;
        private RelayCommand setSourceFilePathCommand;
        private RelayCommand setTargetFilePathCommand;
        private RelayCommand stopWatcherCommand;
        private RelayCommand startWatcherCommand;
        private RelayCommand addExtraExtensionCommand;
        private RelayCommand removeExtensionCommand;

        private FileSystemWatcher _FileWatcher;
        private FileSystemWatcher _DireWatcher;
        private bool running;
        
        #endregion Fields

        #region Constructor

        public MainModel()
        {
            Items = new CheckableObservableCollection<string>();
            SourceFilePath = "Sourcepath -->";
            TargetFilePath = "Targetpath -->";
            ExtraExtension = "add own extension";
            StatusBarMode(false);
            showImage("Red");
            running = false;
            ElementActive = true;

            LoadXML();
        }

   
        #endregion Constructor

        #region Properties

        public string StatusBar
        {
            get { return statusBar; }
            set
            {
                statusBar = value;
                NotifyPropertyChanged("StatusBar");
              
            }
        }

        public string SourceFilePath
        {
            get 
            {
                return sourceFilePath; 
            }
            set 
            {
                if (sourceFilePath != value)
                {
                    sourceFilePath = value;
                    NotifyPropertyChanged("SourceFilePath");
                }
                
            }
        }

        public string TargetFilePath
        {
            get { return targetFilePath; }
            set
            {
                if (targetFilePath != value)
                {
                    targetFilePath = value;
                    NotifyPropertyChanged("TargetFilePath");
                }
            }
        }

        public string InformationContent
        {
            get { return informationContent; }
            set
            {
                if (informationContent != value)
                {
                    informationContent = value;
                    NotifyPropertyChanged("InformationContent");
                }
            }
        }

        public CheckableObservableCollection<string> Items
        {
            get { return items; }
            set
            {
                items = value;
                NotifyPropertyChanged("Items");
            }
        }

        public ImageSource ActiveImage 
        {
            get { return activeImage; }
            set 
            {
                activeImage = value;
                NotifyPropertyChanged("ActiveImage");
             }
         }

        public string ExtraExtension
        {
            get { return extraExtension; }
            set 
            {
                extraExtension = value;
                NotifyPropertyChanged("ExtraExtension");               
             }
         }

        public int SelectedListBoxItemIndex
        {
            get { return selectedListBoxItemIndex; }
            set 
            {
                selectedListBoxItemIndex = value;
                NotifyPropertyChanged("SelectedListBoxItemIndex");
             }
         }

        public bool ElementActive 
        {
            get { return elementActive; }
            set
            {
                if (elementActive != value)
                {
                    elementActive = value;
                    NotifyPropertyChanged("ElementActive");
                }
            }
        }

        public List<string> BottomList
        {
            get
            {
                return bottomList;
            }
            set
            {
                if (bottomList != value)
                {
                    bottomList = value;
                    NotifyPropertyChanged("BottomList");
                }

            }
        }

        #endregion Properties

        #region Commands

        public RelayCommand AddExtraExtensionCommand
        {
            get
            {
                if (addExtraExtensionCommand == null)
                {
                    addExtraExtensionCommand = new RelayCommand(
                        (obj) =>
                        {
                            return !running;
                        },
                        (obj) =>
                        {
                            if (!string.IsNullOrEmpty(ExtraExtension) )
                            {
                                Items.Add(ExtraExtension);
                            }
                        }
                        );
                }
                return addExtraExtensionCommand;
            }
        }

        public RelayCommand RemoveExtensionCommand
        {
            get
            {
                if (removeExtensionCommand == null)
                {
                    removeExtensionCommand = new RelayCommand(
                        (obj) =>
                        {
                            return !running;
                        },
                        (obj) =>
                        {
                            if (SelectedListBoxItemIndex >= 0)
                            {
                                Items.RemoveAt(SelectedListBoxItemIndex);
                            }
                            //CreateListBox();
                        }
                        );
                }
                return removeExtensionCommand;
            }
        }

        public RelayCommand SetSourceFilePathCommand
        {
            get
            {
                if (setSourceFilePathCommand == null)
                {
                    setSourceFilePathCommand = new RelayCommand(
                        (obj) =>
                        {
                            return !running;
                        },
                        (obj) =>
                        {
                            OpenFileDialog ofd = new OpenFileDialog();
                            if (Directory.Exists(SourceFilePath))
                                ofd.InitialDirectory = SourceFilePath;

                            ofd.CheckFileExists = false;
                            ofd.FileName = "Select folder";

                            if (ofd.ShowDialog().Value)
                            {
                                SourceFilePath = Path.GetDirectoryName(ofd.FileName);
                                ScanFolderCommand.InformCanExecuteChanged();
                                StartWatcherCommand.InformCanExecuteChanged();
                                StopWatcherCommand.InformCanExecuteChanged();
                            }
                        }
                        );
                }
                return setSourceFilePathCommand;
            }
        }

        public RelayCommand SetTargetFilePathCommand
        {
            get
            {
                if (setTargetFilePathCommand == null)
                {
                    setTargetFilePathCommand = new RelayCommand(
                        (obj) =>
                        {
                            return !running;
                        },
                        (obj) =>
                        {
                            OpenFileDialog ofd = new OpenFileDialog();
                            if (Directory.Exists(TargetFilePath))
                                ofd.InitialDirectory = TargetFilePath;

                            ofd.CheckFileExists = false;
                            ofd.FileName = "Select folder";

                            if (ofd.ShowDialog().Value)
                            {
                                TargetFilePath = Path.GetDirectoryName(ofd.FileName);
                                ScanFolderCommand.InformCanExecuteChanged();
                                StartWatcherCommand.InformCanExecuteChanged();
                                StopWatcherCommand.InformCanExecuteChanged();
                            }
                        }
                        );
                }
                return setTargetFilePathCommand;
            }
        }

        public RelayCommand ScanFolderCommand 
        {
            get
            {
                if (scanFolderCommand == null)
                {
                    scanFolderCommand = new RelayCommand(
                        (obj) =>
                        {
                            return Directory.Exists(SourceFilePath)&&
                                Directory.Exists(TargetFilePath)&&!running;
                        },
                        (obj) =>
                        {
                            RunFolder();
                            SetFolderInformation();
                        }
                        );
                }
                return scanFolderCommand;
            }
        }

        public RelayCommand StartWatcherCommand
        {
            get
            {
                if (startWatcherCommand == null)
                {
                    startWatcherCommand = new RelayCommand(
                        (obj) =>
                        {
                            return Directory.Exists(SourceFilePath) &&
                                Directory.Exists(TargetFilePath) && !running;
                        },
                        (obj) =>
                        {
                            running = true;
                            ScanFolderCommand.InformCanExecuteChanged();
                            AddExtraExtensionCommand.InformCanExecuteChanged();
                            RemoveExtensionCommand.InformCanExecuteChanged();
                            SetSourceFilePathCommand.InformCanExecuteChanged();
                            SetTargetFilePathCommand.InformCanExecuteChanged();
                            StopWatcherCommand.InformCanExecuteChanged();
                            StartWatcherCommand.InformCanExecuteChanged();
                            Watch();
                        }
                        );
                }
                return startWatcherCommand;
            }
        }

        public RelayCommand StopWatcherCommand
        {
            get
            {
                if (stopWatcherCommand == null)
                {
                    stopWatcherCommand = new RelayCommand(
                        (obj) =>
                        {
                            return Directory.Exists(SourceFilePath) &&
                                Directory.Exists(TargetFilePath) && running;
                        },
                        (obj) =>
                        {
                            running = false;
                            StartWatcherCommand.InformCanExecuteChanged();
                            ScanFolderCommand.InformCanExecuteChanged();
                            AddExtraExtensionCommand.InformCanExecuteChanged();
                            RemoveExtensionCommand.InformCanExecuteChanged();
                            SetSourceFilePathCommand.InformCanExecuteChanged();
                            SetTargetFilePathCommand.InformCanExecuteChanged();
                            StopWatcherCommand.InformCanExecuteChanged();
                            
                            Unwatch();
                        }
                        );
                }
                return stopWatcherCommand;
            }
        }

        #endregion Commands

        #region Handler

        /// <summary>
        /// set Image to an UI Element
        /// element has to be in the Images folder and has to be jpg
        /// function just needs the name not the extension
        /// </summary>
        /// <param name="ImgName"></param>
        void showImage(string ImgName)
        {
               if(!string.IsNullOrEmpty(ImgName))
            {
                var yourImage = new BitmapImage(new Uri(String.Format("Images/{0}.jpg", ImgName), UriKind.Relative));
                //yourImage.Freeze(); // -> to prevent error: "Must create DependencySource on same Thread as the DependencyObject"
                ActiveImage = yourImage;
            }
            else
            {
                ActiveImage = null;   
            }
        }

        /// <summary>
        /// scans the folder for all the extensions 
        /// calls the remove double entries
        /// add the extension to the Listbox
        /// saves entered URL
        /// 
        /// </summary>
        private void RunFolder() 
        {
            DirectoryInfo di = new DirectoryInfo(@SourceFilePath);
            List<string> extensionList = new List<string>(CheckExtensionsInFolder(di));
            extensionList = removeDoubleEntries(extensionList);
            Items.Clear();

            
            foreach(string str in extensionList)
            {
                Items.Add(str);
            }
            SaveUrlToXml();
        }

        /// <summary>
        /// start the filewatcher and activate the eventhandler
        /// reacts to new/changed files
        /// set one watcher for files and directories 
        /// so i can distinguish between a change in a folder or file
        /// is needed because ignore subdirectories isnt handling this kind of change
        /// set green image
        /// </summary>
        public void Watch()
        {
            if (Directory.Exists(SourceFilePath) && Directory.Exists(TargetFilePath))
            {
                InitialSort();

                _FileWatcher = new FileSystemWatcher();

                _FileWatcher.Path = SourceFilePath;

                _FileWatcher.NotifyFilter = NotifyFilters.FileName;
                _FileWatcher.IncludeSubdirectories = false;

                _FileWatcher.Changed += OnWeightFilesDirectoryChanged;
                _FileWatcher.Created += OnWeightFilesDirectoryChanged;

                _FileWatcher.EnableRaisingEvents = true;

                _DireWatcher = new FileSystemWatcher();

                _DireWatcher.Path = SourceFilePath;

                _DireWatcher.NotifyFilter = NotifyFilters.DirectoryName;
                _DireWatcher.IncludeSubdirectories = false;

                _DireWatcher.Changed += OnWeightFilesDirectoryChanged;
                _DireWatcher.Created += OnWeightFilesDirectoryChanged;

                _DireWatcher.EnableRaisingEvents = true;

                StatusBarMode(true);
                showImage("Green");

                ElementActive = false;

                
                //BottomList.Add( "Watcher started");
                
            }            
        }

        /// <summary>
        /// creates directories from the checked extensions 
        /// sorts all the files to the folders
        /// files with same names will be overridden
        /// files be copied and moved 
        /// copied to the target subfolder 
        /// moved to source processed 
        /// </summary>
        private void InitialSort()
        {

            foreach(var Item in Items)
            {
                if (Item.IsChecked) 
                {
                    CheckedExtensions.Add(Item.Value);
                }
            }

            DirectoryInfo di = new DirectoryInfo(@SourceFilePath);

            foreach(var extension in CheckedExtensions)
            {
                Directory.CreateDirectory(@TargetFilePath + "/" + extension.ToString() );     
            }
            Directory.CreateDirectory(@SourceFilePath + "/Processed");

            foreach (var file in di.GetFiles()) 
            {
                foreach (var extensions in CheckedExtensions) 
                {
                    if (file.Extension == "."+extensions) 
                    {
                        // set preferences for overwrite
                        string target = @TargetFilePath + "\\" + extensions +"\\"+ file.Name;
                       
                        try
                        {
                            File.Copy(file.FullName.ToString(), target,true);
                            
                        }
                        catch (Exception e) 
                        {
                            MessageBox.Show(e.Message);
                        }

                        try
                        {
                            //File.MoveTo(@TargetFilePath + "/Processed/" + file.Name);
                            File.Move(file.FullName.ToString(),@SourceFilePath + "\\Processed\\" + file.Name);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                    
                    }
                }
            }
        }

        /// <summary>
        /// deactivate the watcher settings
        /// set red image 
        /// </summary>
        private void Unwatch()
        {
            if (_FileWatcher != null)
            {
                _FileWatcher.Changed -= OnWeightFilesDirectoryChanged;
                _FileWatcher.Created -= OnWeightFilesDirectoryChanged;

                _FileWatcher.EnableRaisingEvents = false;

                _DireWatcher.Changed -= OnWeightFilesDirectoryChanged;
                _DireWatcher.Created -= OnWeightFilesDirectoryChanged;

                _DireWatcher.EnableRaisingEvents = false;

                StatusBarMode(false);
                ElementActive = true;
                showImage("Red");
                //BottomList.Add( "Watcher ended");
            }
           
        }

        /// <summary>
        /// get several information from source/target folder to display them
        /// name and size atm
        /// </summary>
        void SetFolderInformation()
        {
            InformationContent = "";
            DirectoryInfo di = new DirectoryInfo(@SourceFilePath);
            InformationContent += "Name: " + di.FullName.ToString()+"\n";

            try
            {
                double Size = di.EnumerateFiles("*.*", SearchOption.AllDirectories).Sum(fi => fi.Length);
                Size /= 1024*1024 ;
                Size=Math.Round(Size, 3);
                InformationContent += "Size: " + Size.ToString()+" MB";
                NotifyPropertyChanged("InformationContent");
            }
            catch (Exception e) 
            {
              
            }

            if (SourceFilePath != TargetFilePath) 
            {
                DirectoryInfo di_t = new DirectoryInfo(@TargetFilePath);
                InformationContent += "\n" + "\n" + "Name: " + di_t.FullName.ToString() + "\n";

                try
                {
                    double Size = di_t.EnumerateFiles("*.*", SearchOption.AllDirectories).Sum(fi => fi.Length);
                    Size /= 1024 * 1024;
                    Size = Math.Round(Size, 3);
                    InformationContent += "Size: " + Size.ToString() +" MB";
                    NotifyPropertyChanged("InformationContent");
                }
                catch (Exception e)
                {

                }
            }
            // read folder and set InfromationContent
        }

        /// <summary>
        /// eventhandler for changed directory
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnWeightFilesDirectoryChanged(object source, FileSystemEventArgs e)
        {
            Char splitchar = '.';
            string Fileextension = e.Name.Split(splitchar).Last();
            if (source == _FileWatcher)
            {
                foreach (var extension in CheckedExtensions)
                {
                    if (Fileextension == extension)
                    {
                        string target = @TargetFilePath + "\\" + extension + "\\" + e.Name;

                        try
                        {
                            File.Copy(e.FullPath, target, true);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                        try
                        {
                            File.Move(e.FullPath, @SourceFilePath + "\\Processed\\" + e.Name);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            else 
            {
                //FOLDER was changed
            }
            

        }

        /// <summary>
        /// check all the existing files in folder an get the extensions from them to put them in a list
        /// </summary>
        /// <param name="di"></param>
        /// <returns></returns>
        private List<string> CheckExtensionsInFolder(DirectoryInfo di) 
        { 
            List<string> extensionList = new List<string>();   
            
            foreach (var filename in di.GetFiles())
            {
                extensionList.Add(filename.Extension.ToString().Substring(1));
            }
            return extensionList;
           
        }

        /// <summary>
        /// changes the Statusbarmode if Filewatcher active/inactive
        /// </summary>
        /// <param name="p"></param>
        private void StatusBarMode(bool p)
        {
            if (p)
            {
                StatusBar = "Filewatcher aktiv";
            }
            else
            {
                StatusBar = "Filewatcher nicht aktiv";
            }
        }

        /// <summary>
        /// as name says removes double entries from list
        /// </summary>
        /// <param name="stringList"></param>
        /// <returns></returns>
        private static List<string> removeDoubleEntries(List<string> stringList)
        {
            return (new HashSet<string>(stringList)).ToList();
        }

        /// <summary>
        /// Create a Settings xml file to save the entered URL to load them with the start of the program
        /// </summary>
        private void SaveUrlToXml()
        {
            XmlDocument doc = new XmlDocument();
            XmlNode myRoot, myNode;

            myRoot = doc.CreateElement("URL");
            doc.AppendChild(myRoot);

            myNode = doc.CreateElement("SourcePath");
            myNode.InnerText = SourceFilePath.ToString();
            myRoot.AppendChild(myNode);

            myNode = doc.CreateElement("TargetPath");
            myNode.InnerText = TargetFilePath.ToString();
            myRoot.AppendChild(myNode);

            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString();
                path += "\\FileWatcherSettings";
                Directory.CreateDirectory(path);
                doc.Save(path + "\\Settings.xml");
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }


        }

        /// <summary>
        /// Loads the SettingsXML to get the URL for the Watcher
        /// </summary>
        private void LoadXML()
        {
            try 
            {
                List<string> url = new List<string>();

                XmlDocument doc = new XmlDocument();
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString();
                path += "\\FileWatcherSettings\\Settings.xml";
                doc.Load(path);
                XmlElement root = doc.DocumentElement;
                
                foreach (XmlNode data in root) 
                {
                   url.Add(data.InnerText);
                }
                SourceFilePath = url[0];
                TargetFilePath = url[1];
            }
            catch(Exception e)
            {
            }
        }

        #endregion Handler

    }

}
