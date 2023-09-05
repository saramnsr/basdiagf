using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;
using System.Xml;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using BasCommon_BO;



namespace BasCommon_BL
{

    public static class CustomXMLSerializer
    {

        



        public static Color ReadColorFromXMLContent(XmlNode MainNode)
        {
            return Color.FromArgb(Convert.ToInt32(MainNode.InnerText));


        }

        public static void WriteColorToXMLContent(XmlTextWriter writer, Color color)
        {
            if (color == null) return;
            writer.WriteString(color.ToArgb().ToString());
        }


        public static Font ReadFontFromXMLContent(XmlNode MainNode)
        {
            float size = 0; ;
            string FamilyName = "";
            FontStyle fs = FontStyle.Regular;
            GraphicsUnit unit = GraphicsUnit.Pixel;
            byte GdiCharset = 0;
            bool GdiVerticalFont = false;

            foreach (XmlNode node in MainNode.ChildNodes)
            {
                switch (node.Name)
                {
                    case "Size": size = Convert.ToSingle(node.InnerText); break;
                    case "Name": FamilyName = node.InnerText; break;
                    case "Style": fs = (FontStyle)Enum.Parse(typeof(FontStyle), node.InnerText); break;
                    case "Unit": unit = (GraphicsUnit)Enum.Parse(typeof(GraphicsUnit), node.InnerText); break;
                    case "GdiCharSet": GdiCharset = Convert.ToByte(node.InnerText); break;
                    case "GdiVerticalFont": GdiVerticalFont = Convert.ToBoolean(node.InnerText); break;

                }
            }

            Font fnt = new Font(FamilyName, size, fs, unit, GdiCharset, GdiVerticalFont);
            return fnt;

        }

        public static void WriteFontToXMLContent(XmlTextWriter writer, Font fnt)
        {
            if (fnt == null) return;
            //writer.WriteStartElement("Font");
            writer.WriteElementString("Size", fnt.Size.ToString());
            writer.WriteElementString("Name", fnt.Name);
            writer.WriteElementString("Style", fnt.Style.ToString());
            writer.WriteElementString("Unit", fnt.Unit.ToString());
            writer.WriteElementString("GdiCharSet", fnt.GdiCharSet.ToString());
            writer.WriteElementString("GdiVerticalFont", fnt.GdiVerticalFont.ToString());

            writer.WriteElementString("Bold", fnt.Bold.ToString());
            writer.WriteElementString("Italic", fnt.Italic.ToString());
            writer.WriteElementString("Underline", fnt.Underline.ToString());
            // writer.WriteEndElement();
        }


        public static Guid ReadGuidFromXMLContent(XmlNode MainNode)
        {
            string guidstr = "";

            foreach (XmlNode node in MainNode.ChildNodes)
            {
                switch (node.Name)
                {
                    case "Value": guidstr = (node.InnerText); break;

                }
            }

            return new Guid(guidstr);

        }

        public static void WriteGuidToXMLContent(XmlTextWriter writer, Guid gd)
        {
            if (gd == Guid.Empty) return;
            writer.WriteElementString("Value", gd.ToString());
        }



        public static PointF ReadPointFFromXMLContent(XmlNode MainNode)
        {
            PointF pt = new PointF(0, 0);

            foreach (XmlNode node in MainNode.ChildNodes)
            {
                switch (node.Name)
                {
                    case "X": pt.X = Convert.ToSingle(node.InnerText); break;
                    case "Y": pt.Y = Convert.ToSingle(node.InnerText); break;

                }
            }

            return pt;

        }

        public static DateTime ReadDateTimeFromXMLContent(XmlNode MainNode)
        {
            DateTime dt = DateTime.MinValue;
            foreach (XmlNode node in MainNode.ChildNodes)
            {
                switch (node.Name)
                {
                    case "Value": dt = DateTime.FromBinary(Convert.ToInt64(node.InnerText)); break;

                }
            }

            return dt;

        }



        public static void WritePointFToXMLContent(XmlTextWriter writer, PointF point)
        {

            writer.WriteElementString("X", point.X.Equals(float.NaN)?"0":point.X.ToString());
            writer.WriteElementString("Y", point.Y.Equals(float.NaN) ? "0" : point.Y.ToString());
        }


        public static RectangleF ReadRectangleFFromXMLContent(XmlNode MainNode)
        {
            RectangleF pt = new RectangleF(0, 0, 5, 5);

            foreach (XmlNode node in MainNode.ChildNodes)
            {
                switch (node.Name)
                {
                    case "X": pt.X = Convert.ToSingle(node.InnerText); break;
                    case "Y": pt.Y = Convert.ToSingle(node.InnerText); break;
                    case "Width": pt.Width = Convert.ToSingle(node.InnerText); break;
                    case "Height": pt.Height = Convert.ToSingle(node.InnerText); break;

                }
            }

            return pt;

        }

        public static void WriteRectangleFToXMLContent(XmlTextWriter writer, RectangleF rect)
        {
            writer.WriteElementString("X", rect.X.ToString());
            writer.WriteElementString("Y", rect.Y.ToString());
            writer.WriteElementString("Width", rect.Width.ToString());
            writer.WriteElementString("Height", rect.Height.ToString());
        }

        public static void WriteDateTimeToXMLContent(XmlTextWriter writer, DateTime dt)
        {
            writer.WriteElementString("Value", dt.ToBinary().ToString());
        }


        public static void ReadXMLContent(XmlNode node, object obj, List<string> hierarchy, int level, List<XMLSerObj> objlst)
        {

            try
            {
                if (node.Attributes["UID"] != null)
                {
                    XMLSerObj xmlobj = new XMLSerObj(Convert.ToInt32(node.Attributes["UID"].Value), obj);
                    objlst.Add(xmlobj);
                }

                System.Reflection.PropertyInfo lastnfo = null;

                System.Reflection.PropertyInfo[] nfos = obj.GetType().GetProperties(
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic |
                                            System.Reflection.BindingFlags.Public);
                string indent = "";
                for (int i = 0; i < level; i++) indent += "   ";
                hierarchy.Add(indent + obj.GetType().ToString());

                bool refFound = false;

                foreach (XmlNode nd in node.ChildNodes)
                {



                    foreach (System.Reflection.PropertyInfo nfo in nfos)
                    {
                        lastnfo = nfo;
                        if (nfo.Name == nd.Name)
                        {

                            if (nd.Attributes["Reference"] != null)
                            {
                                foreach (XMLSerObj objs in objlst)
                                {
                                    if (objs.UID.ToString() == nd.Attributes["Reference"].Value)
                                    {
                                        if (nfo.GetSetMethod()!=null) 
                                            nfo.SetValue(obj, objs.ToSerialize, null);
                                        refFound = true;
                                        continue;
                                    }
                                }
                                if (refFound) continue;
                            }

                            try
                            {

                                if ((nd.Attributes["IsNull"] != null) && (nd.Attributes["IsNull"].Value == "True"))
                                {
                                    if (nfo.GetSetMethod() != null)
                                        nfo.SetValue(obj, null, null);
                                }
                                else
                                    if (nfo.PropertyType == typeof(byte[]))
                                    {
                                        byte[] array = Convert.FromBase64String(nd.InnerText);
                                        try
                                        {
                                            if (nfo.GetSetMethod() != null)
                                                nfo.SetValue(obj, array, null);
                                        }
                                        catch (System.Exception ex)
                                        {
                                            throw new System.Exception("Erreur à l'affectation de " + obj.GetType().FullName + "." + nfo.Name + " : " + ex.Message, ex);
                                        }
                                    }
                                    else
                                        if (nfo.PropertyType.IsPrimitive)
                                        {
                                            try
                                            {
                                                if (nfo.GetSetMethod() != null)
                                                    nfo.SetValue(obj, Convert.ChangeType(nd.InnerText, nfo.PropertyType), null);

                                            }
                                            catch (System.ArgumentException ex)
                                            {
                                                throw new System.Exception("Erreur à l'affectation de " + obj.GetType().FullName + "." + nfo.Name + " : " + ex.Message, ex);
                                            }
                                        }
                                        else
                                        {

                                            if (nfo.PropertyType == typeof(string))
                                            {
                                                if (nfo.GetSetMethod() != null)
                                                    nfo.SetValue(obj, nd.InnerText, null);
                                            }
                                            else
                                            {
                                                if (nfo.PropertyType.GetInterface("System.Collections.IEnumerable")!=null)
                                                {

                                                    IList newlst = (IList)Activator.CreateInstance(nfo.PropertyType);
                                                    foreach (XmlNode childnd in nd.ChildNodes)
                                                    {
                                                        if (childnd.Attributes["Reference"] != null)
                                                        {
                                                            foreach (XMLSerObj objs in objlst)
                                                            {
                                                                if (objs.UID.ToString() == childnd.Attributes["Reference"].Value)
                                                                {
                                                                    newlst.Add(objs.ToSerialize);
                                                                    continue;
                                                                }
                                                            }
                                                            if (refFound) continue;
                                                        }
                                                        else
                                                        {
                                                            Type type = Type.GetType(childnd.Attributes["Type"].Value);
                                                            object newobj = null;
                                                            if ((type.IsPrimitive) || (type == typeof(string)))
                                                            {
                                                                newobj = Convert.ChangeType(childnd.InnerText, type);
                                                            }
                                                            else
                                                            {
                                                                newobj = Activator.CreateInstance(type);
                                                                ReadXMLContent(childnd, newobj, hierarchy, ++level, objlst);
                                                            }
                                                            newlst.Add(newobj);
                                                        }
                                                    }
                                                    if (nfo.GetSetMethod() != null)
                                                        nfo.SetValue(obj, newlst, null);
                                                }
                                                else
                                                {
                                                    if (nfo.PropertyType.IsEnum)
                                                    {
                                                        if (nfo.GetSetMethod() != null)
                                                            nfo.SetValue(obj, Enum.Parse(nfo.PropertyType, nd.InnerXml), null);
                                                    }
                                                    else
                                                    {

                                                        if (nfo.PropertyType == typeof(Font))
                                                        {
                                                            Font fnt = ReadFontFromXMLContent(nd);
                                                            if (nfo.GetSetMethod() != null)
                                                                nfo.SetValue(obj, fnt, null);
                                                        }
                                                        else
                                                            if (nfo.PropertyType == typeof(Color))
                                                            {
                                                                if (nfo.GetSetMethod() != null)
                                                                    nfo.SetValue(obj, ReadColorFromXMLContent(nd), null);
                                                            }
                                                            else
                                                            {
                                                                if (nfo.PropertyType == typeof(PointF))
                                                                {
                                                                    if (nfo.GetSetMethod() != null)
                                                                        nfo.SetValue(obj, ReadPointFFromXMLContent(nd), null);
                                                                }
                                                                else
                                                                {


                                                                    if (nfo.PropertyType == typeof(Guid))
                                                                    {
                                                                        if (nfo.GetSetMethod() != null)
                                                                            nfo.SetValue(obj, ReadGuidFromXMLContent(nd), null);
                                                                    }
                                                                    else
                                                                    {


                                                                        if (nfo.PropertyType == typeof(RectangleF))
                                                                        {
                                                                            if (nfo.GetSetMethod() != null)
                                                                                nfo.SetValue(obj, ReadRectangleFFromXMLContent(nd), null);
                                                                        }
                                                                        else
                                                                        {

                                                                            if (nfo.PropertyType == typeof(DateTime))
                                                                            {
                                                                                if (nfo.GetSetMethod() != null)
                                                                                    nfo.SetValue(obj, ReadDateTimeFromXMLContent(nd), null);
                                                                            }
                                                                            else
                                                                            {

                                                                                Type type = Type.GetType(nd.Attributes["Type"].Value);
                                                                                object newobj = Activator.CreateInstance(type);
                                                                                ReadXMLContent(nd, newobj, hierarchy, ++level, objlst);
                                                                                if (nfo.GetSetMethod() != null)
                                                                                    nfo.SetValue(obj, newobj, null);

                                                                            }

                                                                        }

                                                                    }

                                                                }


                                                            }

                                                    }
                                                }
                                            }


                                        }

                            }
                            catch (System.Exception ex)
                            {
                                string s = "";
                                foreach (string ss in hierarchy)
                                    s += ss + "\n";
                                s += indent + "   " + lastnfo.PropertyType.ToString() + "\n" + ex.Message;
                                throw new System.Exception(s,ex);
                            }
                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public static void ReadXMLContent(XmlNode node, object obj)
        {
            CustomXMLSerializer.ReadXMLContent(node, obj, new List<string>(), 0, new List<XMLSerObj>());
        }




        private static bool WriteXMLContent(object obj, XmlTextWriter writer, string name, List<XMLSerObj> objlst)
        {

            if (obj != null)
            {

                CanBeReallySerialized att = (CanBeReallySerialized)System.ComponentModel.TypeDescriptor.GetAttributes(obj)[typeof(CanBeReallySerialized)];

                if ((att != null) && (!att.value))
                    return true;
            }


            foreach (XMLSerObj serobj in objlst)
                if (serobj.ToSerialize == obj)
                {
                    writer.WriteAttributeString("Reference", serobj.UID.ToString());
                    return true;
                }



            if (obj != null)
            {
                XMLSerObj sero = new XMLSerObj(objlst.Count, obj);
                objlst.Add(sero);
                writer.WriteAttributeString("UID", sero.UID.ToString());
                writer.WriteAttributeString("Type", obj.GetType().FullName + "," + obj.GetType().Assembly.FullName);
            }
            else
            {
                writer.WriteAttributeString("IsNull", "True");
            }

            System.Reflection.PropertyInfo lastnfo = null;
            bool Serialized = false;
            try
            {
                if (obj != null)
                {
                    System.Reflection.PropertyInfo[] nfos = obj.GetType().GetProperties(
                                                            System.Reflection.BindingFlags.Instance |
                                                            System.Reflection.BindingFlags.NonPublic |
                                                            System.Reflection.BindingFlags.Public);
                    foreach (System.Reflection.PropertyInfo nfo in nfos)
                    {
                        lastnfo = nfo;



                        /*
                        if (nfo.CanRead &&
                            (((nfo.GetCustomAttributes(typeof(SerializeReferenceOnly), false).Length > 0) && !onlyReference)||
                            (nfo.GetCustomAttributes(typeof(PropertyCanBeSerialized), false).Length > 0)  ||
                            ((nfo.GetCustomAttributes(typeof(ReferenceForSerialization), false).Length > 0) && onlyReference)))
                        */

                        if (nfo.CanRead &&
                            ((nfo.GetCustomAttributes(typeof(PropertyCanBeSerialized), false).Length > 0)))
                        {



                            if (nfo.PropertyType == typeof(byte[]))
                            {
                                byte[] array = (byte[])nfo.GetValue(obj, null);


                                writer.WriteStartElement(nfo.Name);
                                if (array != null) writer.WriteBase64(array, 0, array.Length);

                                writer.WriteEndElement();

                            }
                            else
                                if (nfo.GetValue(obj, null) is IList)
                                {

                                    System.Collections.IEnumerator enumerator = ((System.Collections.IEnumerable)nfo.GetValue(obj, null)).GetEnumerator();
                                    enumerator.Reset();
                                    writer.WriteStartElement(nfo.Name);
                                    while (enumerator.MoveNext())
                                    {
                                        object objct = enumerator.Current;
                                        Type tp = objct.GetType();

                                        CanBeReallySerialized att = (CanBeReallySerialized)System.ComponentModel.TypeDescriptor.GetAttributes(objct)[typeof(CanBeReallySerialized)];

                                        if ((att == null) || (att.value))
                                        {

                                            writer.WriteStartElement(tp.FullName);

                                            if (tp.IsPrimitive || tp.IsEnum || (tp == typeof(string)))
                                            {
                                                if (objct != null)
                                                {
                                                    writer.WriteAttributeString("Type", objct.GetType().FullName + "," + objct.GetType().Assembly.FullName);
                                                    writer.WriteString(objct.ToString());
                                                }
                                            }
                                            else
                                                WriteXMLContent(objct, writer, nfo.Name, objlst);
                                            writer.WriteEndElement();
                                        }
                                    }
                                    writer.WriteEndElement();

                                }
                                else
                                {


                                    {
                                        writer.WriteStartElement(nfo.Name);

                                        if (nfo.PropertyType.IsPrimitive || nfo.PropertyType.IsEnum || (nfo.PropertyType == typeof(string)))
                                        {
                                            object val = nfo.GetValue(obj, null);
                                            if (val != null) writer.WriteString(val.ToString());
                                            Serialized = true;
                                        }
                                        else
                                        {
                                            if (nfo.PropertyType == typeof(Color))
                                            {
                                                WriteColorToXMLContent(writer, (Color)nfo.GetValue(obj, null));
                                            }
                                            else
                                                if (nfo.PropertyType == typeof(Font))
                                                {
                                                    WriteFontToXMLContent(writer, (Font)nfo.GetValue(obj, null));
                                                }
                                                else
                                                    if (nfo.PropertyType == typeof(PointF))
                                                    {
                                                        WritePointFToXMLContent(writer, (PointF)nfo.GetValue(obj, null));
                                                    }
                                                    else
                                                        if (nfo.PropertyType == typeof(Guid))
                                                        {
                                                            WriteGuidToXMLContent(writer, (Guid)nfo.GetValue(obj, null));
                                                        }
                                                        else
                                                            if (nfo.PropertyType == typeof(RectangleF))
                                                            {
                                                                WriteRectangleFToXMLContent(writer, (RectangleF)nfo.GetValue(obj, null));
                                                            }
                                                            else
                                                            {
                                                                if (nfo.PropertyType == typeof(DateTime))
                                                                {
                                                                    WriteDateTimeToXMLContent(writer, (DateTime)nfo.GetValue(obj, null));
                                                                }
                                                                else
                                                                {

                                                                    CanBeReallySerialized att = (CanBeReallySerialized)System.ComponentModel.TypeDescriptor.GetAttributes(obj)[typeof(CanBeReallySerialized)];

                                                                    if ((att == null) || (att.value))
                                                                    {
                                                                        object val = nfo.GetValue(obj, null);
                                                                        WriteXMLContent(val, writer, nfo.Name, objlst);
                                                                    }
                                                                }
                                                            }
                                        }

                                        writer.WriteEndElement();
                                    }

                                }
                        }

                    }
                }
                /*
                if (obj != null)
                    writer.WriteElementString(name, obj.ToString());
                else
                    writer.WriteElementString(name, "null");
                 * */
            }
            catch (System.Exception ex)
            {
                writer.Flush();
                throw new System.Exception("Error writting " + lastnfo.Name, ex);
            }


            return Serialized;

        }

        public static void WriteXML(object obj, XmlTextWriter writer, string name)
        {
            writer.WriteStartElement(name);
            WriteXMLContent(obj, writer, "", new List<XMLSerObj>());
            writer.WriteEndElement();
        }


        public static void ReadXML(object objToRead, XmlTextReader reader)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            XmlNode noeud = doc.DocumentElement;
            CustomXMLSerializer.ReadXMLContent(noeud, objToRead, new List<string>(), 0, new List<XMLSerObj>());
        }

        public static object Cloner(object obj)
        {

            object clone = Activator.CreateInstance(obj.GetType());





            StringWriter stringWriter = new StringWriter();
            XmlTextWriter writer = new XmlTextWriter(stringWriter);
            CustomXMLSerializer.WriteXML(obj, writer, obj.ToString());
            /*
            writer = new XmlTextWriter("c:\\test.xml", null);
            writer.Formatting = Formatting.Indented;
            AggXMLSerialization.WriteXML(obj, writer, obj.ToString());
            writer.Close();
            */

            StringReader stringReader = new StringReader(stringWriter.ToString());
            XmlTextReader reader = new XmlTextReader(stringReader);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            XmlNode noeud = doc.DocumentElement;
            CustomXMLSerializer.ReadXMLContent(noeud, clone, new List<string>(), 0, new List<XMLSerObj>());

            return clone;

        }
    }





}
