﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Diagnostics;
using System.Data;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using Microsoft.Win32;
using BasCommon_BO;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.IO.Compression;

namespace BasCommon_DAL
{




    public static partial class DAC
    {

        public static string PathRest
        {
            get
            {

                return System.Configuration.ConfigurationManager.AppSettings["PathRest" + prefix];

            }

        }
        public static string token
        {
            get
            {

                return System.Configuration.ConfigurationManager.AppSettings["token" + prefix];

            }
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
            int idCabinet = 1;
            if (DateTime.TryParse(objValidityDate, out ValidityDate) )
            {

                  idCabinet = Convert.ToInt32(objValidityUser);

                prefix = Cabinets.Find(c => c.Id == idCabinet).prefix;
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
        private static  List<Cabinet> _lstCabinet;
        public static  List<Cabinet> Cabinets
        {
            get
            {
                if (_lstCabinet == null)
                    _lstCabinet = getAllCabinets();
                return _lstCabinet;
            }
            set
            {
                _lstCabinet = value;
            }
        }
        public static List<Cabinet> getAllCabinets()
        {
            List<Cabinet> lst = new List<BasCommon_BO.Cabinet>();
            string FILE_PATH = System.Configuration.ConfigurationManager.AppSettings["cabinets"];
            var xmlString = File.ReadAllText(FILE_PATH);
            var stringReader = new StringReader(xmlString);
            var dsSet = new DataSet();
            dsSet.ReadXml(stringReader);
            DataTable dt = dsSet.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Cabinet cab = new Cabinet();
                cab.Id = dr["Id"] is DBNull ? -1 : Convert.ToInt32(dr["Id"]);
                cab.nomCabinet = dr["nomcabinet"] is DBNull ? "" : Convert.ToString(dr["nomcabinet"]).Trim();
                cab.prefix = dr["prefix"] is DBNull ? "" : Convert.ToString(dr["prefix"]).Trim();
                lst.Add(cab);
            }

            return lst;
        }
        public static JObject getMethodeJsonObjet(string methode)
        {
            System.Net.WebClient client = new System.Net.WebClient();
            System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);
            client.Encoding = Encoding.UTF8;
            client.Headers.Add("content-type", "application/json");//set your header here, you can add multiple headers
            client.Headers.Add("Authorization", "bearer " + token);
            var response = client.DownloadString(PathRest + methode);
            if (response.ToString().Equals("")) return null;
            return JObject.Parse(response);
        }

        public static string getMethodeJsonString(string methode)
        {
            System.Net.WebClient client = new System.Net.WebClient();
            client.Encoding = Encoding.UTF8;
            client.Headers.Add("content-type", "application/json");//set your header here, you can add multiple headers
            client.Headers.Add("Authorization", "bearer " + token);
            return client.DownloadString(PathRest + methode);

        }



        public static JArray getMethodeJsonArray(string methode)
        {
            string result;
            var client = (HttpWebRequest)WebRequest.Create(PathRest + methode);
            System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);
            client.Headers.Add("charset", "UTF-8 ");
            //client.Headers.Add("content-type", "application/json");//set your header here, you can add multiple headers
            client.Headers.Add("Authorization", "bearer " + token);
            client.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
             var httpResponse = (HttpWebResponse)client.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            if (result.ToString().Equals("")) return null;
            if (result.ToString().Equals("{}")) return null;
            return JArray.Parse(result);
        }

        public static JArray PostingToPKFAndDecompress(string sData, string sUrl)
        {
            var jOBj = new JArray();
            try
            {

                try
                {
                    string urlStr = PathRest+sUrl;
                    string result = "";


                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlStr);
                    System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);
                    request.Headers.Add("Authorization", "bearer " + token);
                    request.Headers.Add("charset", "UTF-8 ");
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream resStream = response.GetResponseStream();

                    var t = ReadFully(resStream);
                    var y = Decompress(t);
                    using (var ms = new MemoryStream(y))
                   
                    using (var streamReader = new StreamReader(ms))
                    using (var jsonReader = new JsonTextReader(streamReader))
                    {
                        result = streamReader.ReadToEnd();
                        jOBj = JArray.Parse(result);
                    }


                }
                catch (System.Net.Sockets.SocketException)
                {
                    // The remote site is currently down. Try again next time. 
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return jOBj;
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        static byte[] Decompress(byte[] gzip)
        {
            // Create a GZIP stream with decompression mode.
            // ... Then create a buffer and write into while reading from the GZIP stream.
            using (GZipStream stream = new GZipStream(new MemoryStream(gzip),
                CompressionMode.Decompress))
            {
                const int size = 4096;
                byte[] buffer = new byte[size];
                using (MemoryStream memory = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        count = stream.Read(buffer, 0, size);
                        if (count > 0)
                        {
                            memory.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);
                    return memory.ToArray();
                }
            }
        }

        public static void insertRest(string methode, Object obj)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(PathRest + methode);
            System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("Authorization", "bearer " + token);
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
                streamWriter.Write(json);
            }
            int x;
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }

        }
        public static JArray postRestWithResponse(string methode, Object obj)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(PathRest + methode);
            System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);
            string result;
            string json;
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("Authorization", "bearer " + token);
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                json = JsonConvert.SerializeObject(obj, Formatting.Indented);
                streamWriter.Write(json);
            }
            int x;
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            if (result.ToString().Equals("")) return null;
            return JArray.Parse(result);
        }
        private static bool _ArchiveMode = false;
        public static bool ArchiveMode
        {
            get
            {
                return _ArchiveMode;
            }
            set
            {
                _ArchiveMode = value;
            }
        }

        static object lockobj = new object();

        public static string connectionString = "";
        public static string connectionStringBL = "";
        public static string connectionStringBV = "";
        public static string connectionStringBP = "";
        //private static FbConnection connection = null;
        //private static FbConnection connectionBL = null;
        //private static FbConnection connectionBaseView = null;
        //private static FbConnection connectionBaseProduct = null;
        private static MySqlConnection connection = null;
        private static MySqlConnection connectionBL = null;

        private static MySqlConnection connectionBaseView = null;
        private static MySqlConnection connectionBaseProduct = null;

        #region connections



        public static void getConnection()
        {
            //    If the connection string is null, use a default.


            try
            {
                if (connectionString == "")
                {
                    MySqlConnectionStringBuilder cs = new MySqlConnectionStringBuilder();


                    if (ArchiveMode)
                    {
                        //   cs.DataSource = ConfigurationManager.AppSettings["ArchiveDataSource"];
                        cs.Database = ConfigurationManager.AppSettings["ArchiveDatabase" + prefix];
                        cs.UserID = ConfigurationManager.AppSettings["ArchiveUserID" + prefix];
                        cs.Password = ConfigurationManager.AppSettings["ArchivePassword" + prefix];
                        cs.Port = Convert.ToUInt32(ConfigurationManager.AppSettings["PORT"]);
                        //   cs.Dialect = Convert.ToInt32(ConfigurationManager.AppSettings["ArchiveDialect"]);
                        //   cs.ServerType = (FbServerType)Enum.Parse(typeof(FbServerType), ConfigurationManager.AppSettings["ServerType"]);

                    }
                    else
                    {

                        cs.Server = ConfigurationManager.AppSettings["DataSource" + prefix];
                        cs.Database = ConfigurationManager.AppSettings["Database" + prefix];
                        cs.UserID = ConfigurationManager.AppSettings["UserID" + prefix];
                        cs.Password = ConfigurationManager.AppSettings["Password" + prefix];
                        cs.Port = Convert.ToUInt32(ConfigurationManager.AppSettings["PORT"]);
                     //   cs.ConnectionTimeout = 5000;
                    }
                    connectionString = cs.ToString();



                }
#if TRACE
                Console.WriteLine("connectionString : " + connectionString);
#endif

                connection = new MySqlConnection(connectionString);
                //connection.Open();

            }
            catch (System.Exception ex)
            {

#if TRACE
                Console.WriteLine("connectionString Error : " + ex.ToString());

#endif

                throw ex;
            }
        }

        private static void getBaseLaboConnection()
        {
            //    If the connection string is null, use a default.

            if (connectionStringBL == "")
            {

                MySqlConnectionStringBuilder cs = new MySqlConnectionStringBuilder();

                cs.Server = ConfigurationManager.AppSettings["DataSource" + prefix];
                cs.Database = ConfigurationManager.AppSettings["Database" + prefix];
                cs.UserID = ConfigurationManager.AppSettings["UserID" + prefix];
                cs.Password = ConfigurationManager.AppSettings["Password" + prefix];
                cs.Port = Convert.ToUInt32(ConfigurationManager.AppSettings["PORT"]);

                connectionStringBL = cs.ToString();
            }

            connectionBL = new MySqlConnection(connectionStringBL);
            connectionBL.Open();
        }

        private static void getBaseViewConnection()
        {
            //    If the connection string is null, use a default.

            if (connectionStringBV == "")
            {
                MySqlConnectionStringBuilder cs = new MySqlConnectionStringBuilder();

                cs.Server = ConfigurationManager.AppSettings["DataSource" + prefix];
                cs.Database = ConfigurationManager.AppSettings["Database" + prefix];
                cs.UserID = ConfigurationManager.AppSettings["UserID" + prefix];
                cs.Password = ConfigurationManager.AppSettings["Password" + prefix];
                cs.Port = Convert.ToUInt32(ConfigurationManager.AppSettings["PORT"]);


                connectionStringBV = cs.ToString();
            }

            connectionBaseView = new MySqlConnection(connectionStringBV);
            connectionBaseView.Open();
        }

        private static void getBaseProductConnection()
        {
            //    If the connection string is null, use a default.

            try
            {
                if (connectionStringBP == "")
                {
                    MySqlConnectionStringBuilder cs = new MySqlConnectionStringBuilder();

                    cs.Server = ConfigurationManager.AppSettings["DataSource" + prefix];
                    cs.Database = ConfigurationManager.AppSettings["Database" + prefix];
                    cs.UserID = ConfigurationManager.AppSettings["UserID" + prefix];
                    cs.Password = ConfigurationManager.AppSettings["Password" + prefix];
                    cs.Port = Convert.ToUInt32(ConfigurationManager.AppSettings["PORT"]);

                    connectionStringBP = cs.ToString();
                }

                connectionBaseProduct = new MySqlConnection(connectionStringBP);
                connectionBaseProduct.Open();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        #endregion

     
    }
 
}
