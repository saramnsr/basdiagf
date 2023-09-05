using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using System.Xml;
using System.Windows.Forms;

namespace BASEDiagAdulte.Remoting
{
    [Serializable]
    public class NetWorkMsg
    {
        private string _From;
        public string From
        {
            get
            {
                return _From;
            }
            set
            {
                _From = value;
            }
        }

        private string _To;
        public string To
        {
            get
            {
                return _To;
            }
            set
            {
                _To = value;
            }
        }

        private string _ActionCODE;
        public string ActionCODE
        {
            get
            {
                return _ActionCODE;
            }
            set
            {
                _ActionCODE = value;
            }
        }

        private string _Message;
        public string Message
        {
            get
            {
                return _Message;
            }
            set
            {
                _Message = value;
            }
        }
    }

    public class MgmtBaseControls
    {
        public MgmtBaseControls()
        {

        }

        static int Port = 9237;

        public static void SendMessage(Object objToSerialize)
        {
            MemoryStream fs = new MemoryStream();
            XmlSerializer xs = new XmlSerializer(typeof(NetWorkMsg));
            XmlTextWriter xmlTextWriter = new XmlTextWriter(fs, Encoding.UTF8);
            xs.Serialize(fs, objToSerialize);

            UdpClient server = new UdpClient("127.0.0.1", Port);
            server.EnableBroadcast = true;

            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);

            byte[] data = fs.ToArray();
            server.Send(data, data.Length);

        }



        public static void SendIdPatientCourantToBaseControl(int id)
        {
            NetWorkMsg netmsg = new NetWorkMsg();

            netmsg.From = System.Windows.Forms.Application.ProductName;
            netmsg.To = "BASE_CONTROLS";
            netmsg.ActionCODE = "changementIdPatient";
            if (id != 0)
                netmsg.Message = "IdPatient," + id;

            MgmtBaseControls.SendMessage(netmsg);
        }

        public static void SendNomPatientCourantToBaseControl(string nom)
        {
            NetWorkMsg netmsg = new NetWorkMsg();

            netmsg.From = System.Windows.Forms.Application.ProductName;
            netmsg.To = "BASE_CONTROLS";
            netmsg.ActionCODE = "changementNomPatient";
            if (nom != "")
                netmsg.Message = "NomPatient," + nom;

            MgmtBaseControls.SendMessage(netmsg);
        }

        public static void SendPrenomPatientCourantToBaseControl(string prenom)
        {
            NetWorkMsg netmsg = new NetWorkMsg();

            netmsg.From = System.Windows.Forms.Application.ProductName;
            netmsg.To = "BASE_CONTROLS";
            netmsg.ActionCODE = "changementPrenomPatient";
            if (prenom != "")
                netmsg.Message = "PrenomPatient," + prenom;

            MgmtBaseControls.SendMessage(netmsg);
        }
    }
}
