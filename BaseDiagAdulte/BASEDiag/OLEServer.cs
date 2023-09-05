using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Reflection;
using System.ComponentModel;
using System.Windows.Forms;
using BasCommon_BO;

using System.IO;
namespace BASEDiagAdulte
{


    [ComVisible(true)]
    public class OLEPatient : basePatient
    {
        
        public OLEPatient()
        {
        }

        public string HelloWorld()
        {
            return "Hello World from patient : " + this.ToString();
        }
               
    }

    [ComVisible(true)]
    [Guid("6F656B86-350B-4907-ACB3-75523130A515")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IBDOLEServer
    {
        [DispId(1)]
        string HelloWorld(bool param);

        [DispId(2)]
        string Version();

        [DispId(3)]
        string FullPath();

        [DispId(4)]
        void SetPatient(int Id);                
              

        [DispId(5)]
        void Activate();
       
        [DispId(6)]
        string GetExePath();

        [DispId(7)]
        void OpenPatientFromFile(string filename);

    }

    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComDefaultInterface(typeof(IBDOLEServer))]
    [Guid("A123714B-F555-4AAE-84BA-AF7B14946668")]
    public class OLEServer : IBDOLEServer
    {

        [ComRegisterFunction]
        public static void RegisterFunction(Type t)
        {

            AttributeCollection Interfaceattributes = TypeDescriptor.GetAttributes(typeof(IBDOLEServer));

            AttributeCollection attributes = TypeDescriptor.GetAttributes(t);
            ProgIdAttribute ProgIdAttr = attributes[typeof(ProgIdAttribute)] as ProgIdAttribute;

            string ProgId = ProgIdAttr != null ? ProgIdAttr.Value : t.FullName;

            GuidAttribute GUIDAttr = attributes[typeof(GuidAttribute)] as GuidAttribute;
            string GUID = "{" + GUIDAttr.Value + "}";

            GuidAttribute GUIDInterfaceattributes = Interfaceattributes[typeof(GuidAttribute)] as GuidAttribute;
            string GUIDInterfaceatt = "{" + GUIDInterfaceattributes.Value + "}";

            RegistryKey localServer32 = Registry.ClassesRoot.CreateSubKey(String.Format("CLSID\\{0}\\LocalServer32", GUID));
            localServer32.SetValue(null, t.Module.FullyQualifiedName);

            RegistryKey CLSIDProgID = Registry.ClassesRoot.CreateSubKey(String.Format("CLSID\\{0}\\ProgId", GUID));
            CLSIDProgID.SetValue(null, ProgId);

            RegistryKey TypeLib = Registry.ClassesRoot.CreateSubKey(String.Format("CLSID\\{0}\\TypeLib", GUID));
            TypeLib.SetValue(null, GUIDInterfaceatt);

            RegistryKey Version = Registry.ClassesRoot.CreateSubKey(String.Format("CLSID\\{0}\\Version", GUID));
            Version.SetValue(null, "1.0");

            RegistryKey ProgIDCLSID = Registry.ClassesRoot.CreateSubKey(String.Format("CLSID\\{0}", ProgId));
            ProgIDCLSID.SetValue(null, GUID);

            RegistryKey TypeL = Registry.ClassesRoot.CreateSubKey(String.Format("TypeLib\\{0}\\1.0", GUIDInterfaceatt));
            TypeL.SetValue(null, Application.ProductName + "(bibliotheque)");

            RegistryKey TypeLibWin32 = Registry.ClassesRoot.CreateSubKey(String.Format("TypeLib\\{0}\\1.0\\0\\Win32", GUIDInterfaceatt));
            TypeLibWin32.SetValue(null, t.Module.FullyQualifiedName);

            RegistryKey FlagsKey = Registry.ClassesRoot.CreateSubKey(String.Format("TypeLib\\{0}\\1.0\\FLAGS", GUIDInterfaceatt));
            FlagsKey.SetValue(null, "0");

            FileInfo nfo = new FileInfo(t.Module.FullyQualifiedName);


            RegistryKey Helpdir = Registry.ClassesRoot.CreateSubKey(String.Format("TypeLib\\{0}\\1.0\\HELPDIR", GUIDInterfaceatt));
            Helpdir.SetValue(null, nfo.Directory.FullName);



            //Registry.ClassesRoot.CreateSubKey(String.Format("CLSID\\{0}\\Implemented Categories\\{{63D5F432-CFE4-11D1-B2C8-0060083BA1FB}}", GUID));
            //Registry.ClassesRoot.CreateSubKey(String.Format("CLSID\\{0}\\Implemented Categories\\{{63D5F430-CFE4-11d1-B2C8-0060083BA1FB}}", GUID));
            //Registry.ClassesRoot.CreateSubKey(String.Format("CLSID\\{0}\\Implemented Categories\\{{62C8FE65-4EBB-45e7-B440-6E39B2CDBF29}}", GUID));
        }

        [ComUnregisterFunction]
        public static void UnregisterFunction(Type t)
        {

            AttributeCollection Interfaceattributes = TypeDescriptor.GetAttributes(typeof(IBDOLEServer));
            GuidAttribute GUIDInterfaceattributes = Interfaceattributes[typeof(GuidAttribute)] as GuidAttribute;
            string GUIDInterfaceatt = "{" + GUIDInterfaceattributes.Value + "}";

            AttributeCollection attributes = TypeDescriptor.GetAttributes(t);
            ProgIdAttribute ProgIdAttr = attributes[typeof(ProgIdAttribute)] as ProgIdAttribute;

            string ProgId = ProgIdAttr != null ? ProgIdAttr.Value : t.FullName;

            Registry.ClassesRoot.DeleteSubKeyTree("CLSID\\{" + t.GUID + "}");
            Registry.ClassesRoot.DeleteSubKeyTree("CLSID\\" + ProgId);

            Registry.ClassesRoot.DeleteSubKeyTree("TypeLib\\" + GUIDInterfaceatt);
        }

        public string HelloWorld(bool param)
        {
            return "Hello World ! ";
        }

        public string FullPath()
        {
            return Assembly.GetExecutingAssembly().GetName().FullName;
        }

        public void SetPatient(int Id)
        {
            int test = Id;

            Program.MainForm.Invoke(Program.MainForm.m_DelegateSetPatient, new object[] { Id });
        }


        public void OpenPatientFromFile(string filename)
        {
            Program.MainForm.Invoke(Program.MainForm.m_DelegateOpenPatientFromFile, new object[] { filename });
        }

        public string Version()
        {
            string Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            return Version;


        }

        
        

        public void Activate()
        {

        }

        

        public string GetExePath()
        {
            System.Reflection.Assembly a = System.Reflection.Assembly.GetEntryAssembly();
            string baseDir = Path.GetDirectoryName(a.Location);
            return baseDir;
        }                

        public string GetPhotoIdentByNomPrenom(string nom, string Prenom)
        {
            return "";
        }

        public string GetPhotoIdentByNomPrenom(string nom, string Prenom, string DateNaiss)
        {
            return "";
        }
              

       
    }
}
