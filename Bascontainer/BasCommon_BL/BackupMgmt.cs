using BasCommon_DAL;
using Ionic.Zip;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;


namespace BasCommon_BL
{
    public static class BackupMgmt
    {


        public static bool backupCredo { get; set; }
        public static bool BackupClasseur { get; set; }
        public static bool BackupLabo { get; set; }
        public static bool BackupPhoto { get; set; }

        public static string ZipFilename { get; set; }


        public static StreamWriter writer { get; set; }


        static BackupMgmt()
        {
            backupCredo = true;
            BackupClasseur = true;
            BackupLabo = true;
        }

        private static void WriteLog(string message)
        {
            writer.WriteLine(DateTime.Now.ToString()+" : "+ message);
        }


        private static string BackUpToTempDir( Action<int,string> progress)
        {
            string tempfolder = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempfolder);



            BackUpToDir(tempfolder, progress);

            return tempfolder;

        }

        private static void BackUpToDir(string folder, Action<int,string> progress)
        {
            WriteLog("Folder : " + folder);

            if (progress!=null) progress(0,"Start DB");
            BackUpDBToDir(folder, progress);

            if (BackupPhoto)
            {
                if (progress != null) progress(0, "Start Folder");
                BackUpPhotosToFolder(folder, progress);
            }

            if (BackupHisto)
            {
                if (progress != null) progress(0, "Start Historique");
                BackupHistoryToFolder(folder, progress);
            }
            if (progress != null) progress(100, "End");


        }



        private static string _RegistryKey = "Software\\BASE\\BASEPractice";

        private static string _RegistryKeyPref = _RegistryKey + "\\Preferences";
        private static string _CurrentCabRegistryKey = _RegistryKeyPref + "\\CurrentCab";

        public static void GetCurrentCabOnRegistry()
        {



            RegistryKey key = Registry.CurrentUser.OpenSubKey(_CurrentCabRegistryKey);

            // If the return value is null, the key doesn't exist
            if (key == null) return;

            string objValidityDate = (string)key.GetValue("ValidityDate");
            string objValidityUser = (string)key.GetValue("ValidityCab");

            key.Close();

            DateTime ValidityDate;

            if (DateTime.TryParse(objValidityDate, out ValidityDate))
            {
                int idCabinet = Convert.ToInt32(objValidityUser);
                prefix = CabinetMgmt.Cabinet.Find(c => c.Id == idCabinet).prefix;
            }
        }
        private static string _prefix = "";
        public static string prefix
        {
            get
            {
                if (_prefix == null || _prefix == "")
                    GetCurrentCabOnRegistry();
                return _prefix;
            }
            set
            {
                _prefix = "_" + value;
            }


        }
        private static void BackupHistoryToFolder(string folder, Action<int, string> progress)
        {
            NbElementsACopier = 0;
            string tempHistoFolder = Path.Combine(folder, "Historique");
            if (!Directory.Exists(tempHistoFolder))
                Directory.CreateDirectory(tempHistoFolder);

            WriteLog("Starting BackUp Historique to " + tempHistoFolder);

            string TempFolder = ConfigurationManager.AppSettings["HISTORY_FOLDER" + prefix];



            DeepCount(TempFolder, ref NbElementsACopier);
            int currentelement = 1;

            DeepCopy(TempFolder, tempHistoFolder, ref currentelement, progress);
            
        }

        private static void DeepCount(string sourcePath,ref int NbElements)
        {
            if (Directory.Exists(sourcePath))
            {
                string[] files = Directory.GetFiles(sourcePath);
                string[] directories = Directory.GetDirectories(sourcePath);

                // Copy the files and overwrite destination files if they already exist.
                foreach (string s in files)
                    NbElements++;

                foreach (string s in directories)
                    DeepCount(s, ref NbElements);
            }
                
        }

        private static void ZipDirectoryFolder(string folder,string zipfile)
        {
            using (ZipFile zip = new ZipFile())
            {

                zip.AddDirectory(folder);
                zip.Save(zipfile);
            }
        }

        private static void DeepCopy(string sourcePath, string targetPath,ref int currentElement,Action<int,string> progress)
        {
            if (Directory.Exists(sourcePath))
            {
                WriteLog("BackUp " + sourcePath);


                string[] files = Directory.GetFiles(sourcePath);

                // Copy the files and overwrite destination files if they already exist.
                foreach (string s in files)
                {
                    WriteLog(currentElement.ToString()+"/"+NbElementsACopier.ToString()+" : "+ s);
                    // Use static Path methods to extract only the file name from the path.
                    string fileName = Path.GetFileName(s);
                    string destFile = Path.Combine(targetPath, fileName);
                    File.Copy(s, destFile, true);
                    currentElement++;
                    if (progress != null)
                        progress((int)(((float)currentElement / NbElementsACopier) * 100.0f), "");
                }

                string[] directories = Directory.GetDirectories(sourcePath);
                foreach (string s in directories)
                {
                    DirectoryInfo srcdirinfo = new DirectoryInfo(s);
                    DirectoryInfo destdirinfo = new DirectoryInfo(Path.Combine(targetPath,srcdirinfo.Name));
                    destdirinfo.Create();
                    DeepCopy(srcdirinfo.FullName, destdirinfo.FullName,ref currentElement,progress);
                    
                }
            }
            else
            {
                WriteLog("Source path does not exist : " + sourcePath);
            }

        }

        static int NbElementsACopier;

        private static void BackUpPhotosToFolder(string vDirectory,Action<int,string> progress)
        {
            NbElementsACopier = 0;
            string tempPhotoFolder = Path.Combine(vDirectory, "Photos");
            if (!Directory.Exists(tempPhotoFolder))
                Directory.CreateDirectory(tempPhotoFolder);

            WriteLog("Starting BackUp Photo to " + tempPhotoFolder);

            string TempFolder = ConfigurationManager.AppSettings["PHOTO_FOLDER_PATH" + prefix];


            DeepCount(TempFolder, ref NbElementsACopier);
            int currentelement = 1;

            DeepCopy(TempFolder, tempPhotoFolder,ref currentelement,progress);
            
        }

        private static void BackUpDBToDir(string vDirectory, Action<int, string> progress)
        {
            string tempDataBaseFolder = Path.Combine(vDirectory, "DataBases");
            if (!Directory.Exists(tempDataBaseFolder))
                Directory.CreateDirectory(tempDataBaseFolder);

            if (backupCredo)
            {
                string DataBaseFile = ConfigurationManager.AppSettings["Database"];
                string DataBaseFolder = ConfigurationManager.AppSettings["DBFolderToBckUp"];

                FileInfo nfoCredo = new FileInfo(DataBaseFile);
                DataBaseFile = Path.Combine(DataBaseFolder, nfoCredo.Name);
                FileInfo nfo = new FileInfo(DataBaseFile);
                
                if (nfo.Exists)
                {
                    

                    File.Copy(DataBaseFile, Path.Combine(tempDataBaseFolder, nfo.Name), true);
                    WriteLog("Database copied successfully !");
                }
                else
                    WriteLog("DB Not Found at : " + DataBaseFile);
            }
            if (progress != null) progress(33, "");

            if (BackupClasseur)
            {

                string DataBaseFile = ConfigurationManager.AppSettings["BASPHOTO_Database"];
                string DataBaseFolder = ConfigurationManager.AppSettings["DBFolderToBckUp"];

                FileInfo nfoCredo = new FileInfo(DataBaseFile);
                DataBaseFile = Path.Combine(DataBaseFolder, nfoCredo.Name);
                FileInfo nfo = new FileInfo(DataBaseFile);

                if (nfo.Exists)
                {
                    File.Copy(DataBaseFile, Path.Combine(tempDataBaseFolder, nfo.Name), true);
                    WriteLog("Database Photo copied successfully !");
                }
                else
                    WriteLog("DB Not Found at : " + DataBaseFile);
            }
            if (progress != null) progress(66, "");

            if (BackupLabo)
            {

                string DataBaseFile = ConfigurationManager.AppSettings["BaseProduct_Database"];
                string DataBaseFolder = ConfigurationManager.AppSettings["DBFolderToBckUp"];

                FileInfo nfoCredo = new FileInfo(DataBaseFile);
                DataBaseFile = Path.Combine(DataBaseFolder, nfoCredo.Name);
                FileInfo nfo = new FileInfo(DataBaseFile);

                if (nfo.Exists)
                {
                    File.Copy(DataBaseFile, Path.Combine(tempDataBaseFolder, nfo.Name), true);
                    WriteLog("Database Labo copied successfully !");
                }
                else
                    WriteLog("DB Not Found at : " + DataBaseFile);
            }
            if (progress != null) progress(100, "");
        }

        

        public static void StartBackUp(StreamWriter sw,Action<int,string> progress)
        {


            if (!Directory.Exists(BackupDestFolder))
            {
                throw new DirectoryNotFoundException(BackupDestFolder + " n'existe pas!");
            }
            else
            {

                writer = sw;
                WriteLog("Back-Up process started !");


                if (!string.IsNullOrEmpty(ZipFilename))
                {

                    string tempfolder = BackUpToTempDir(progress);
                    ZipDirectoryFolder(tempfolder, ZipFilename);
                }else
                    BackUpToDir(BackupDestFolder,progress);
             


                WriteLog("Back-Up process ended !");
            }

        }

        public static string BackupDestFolder { get; set; }

        public static bool BackupHisto { get; set; }
    }
}
