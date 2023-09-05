using BasCommon_BL;
using BasCommon_BO;
using BasCommon_BO.ElementsEnBouche.BO;
using BASEDiag_BO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace BASEDiag_BL
{


    sealed class BaseDiagDeserializationBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            Type t = null;
            if (typeName == "BASEDiag_BO.PointToTake")
                t = typeof(BASEDiag_BO.PointToTake);
            if (t==null)
                t = Type.GetType(typeName);
            return t;
        }
    }

    public static class SysTools
    {
        #region Fields and properties
        public static Type TpOf_String = typeof(string);
        public static Type TpOf_Int = typeof(int);
        public static Type TpOf_UInt = typeof(uint);
        public static Type TpOf_Byte = typeof(byte);
        public static Type TpOf_SByte = typeof(sbyte);
        public static Type TpOf_Short = typeof(short);
        public static Type TpOf_UShort = typeof(ushort);
        public static Type TpOf_Long = typeof(long);
        public static Type TpOf_ULong = typeof(ulong);
        public static Type TpOf_Char = typeof(char);
        public static Type TpOf_Decimal = typeof(decimal);
        public static Type TpOf_Double = typeof(double);
        public static Type TpOf_Float = typeof(float);
        public static Type TpOf_DateTime = typeof(DateTime);
        public static Type TpOf_TimeSpan = typeof(TimeSpan);
        public static Type TpOf_Bool = typeof(bool);
        #endregion

        #region Tools

        public static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            if (imageIn == null) return null;
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;

        }

        public static double ObjectToDouble(object Value)
        {
            return ObjectToDouble(Value, 0);
        }

        public static double ObjectToDouble(object Value, double DefaultValue)
        {
            if (Value == null || Value == DBNull.Value) return DefaultValue;
            try
            {
                return double.Parse(Value.ToString());
            }
            catch { }

            return DefaultValue;
        }

        public static decimal ObjectToDecimal(object Value)
        {
            return ObjectToDecimal(Value, 0);
        }

        public static decimal ObjectToDecimal(object Value, decimal DefaultValue)
        {
            if (Value == null || Value == DBNull.Value) return DefaultValue;

            // Première tentative en Cast
            try
            {
                return (decimal)Value;
            }
            catch { }

            // Deuxième tentative en Parse
            try
            {
                return decimal.Parse(Value.ToString());
            }
            catch { }

            // Tant pis !
            return DefaultValue;
        }

        public static int ObjectToInt(object Value)
        {
            return ObjectToInt(Value, 0);
        }

        public static int ObjectToInt(object Value, int DefaultValue)
        {
            if (Value == null || Value == DBNull.Value) return DefaultValue;


            // Première tentative en Cast
            Type Tpe = Value.GetType();

            if (Tpe == SysTools.TpOf_Decimal) try { return (int)((decimal)Value); }
                catch { }
            if (Tpe == SysTools.TpOf_Byte) try { return (int)((byte)Value); }
                catch { }
            if (Tpe == SysTools.TpOf_Double) try { return (int)((double)Value); }
                catch { }
            if (Tpe == SysTools.TpOf_Float) try { return (int)((float)Value); }
                catch { }
            if (Tpe == SysTools.TpOf_Long) try { return (int)((long)Value); }
                catch { }
            if (Tpe == SysTools.TpOf_Short) try { return (int)((short)Value); }
                catch { }
            if (Tpe == SysTools.TpOf_UInt) try { return (int)((uint)Value); }
                catch { }
            if (Tpe == SysTools.TpOf_ULong) try { return (int)((ulong)Value); }
                catch { }
            if (Tpe == SysTools.TpOf_UShort) try { return (int)((ushort)Value); }
                catch { }

            try
            {
                return (int)Value;
            }
            catch { }

            // Deuxième tentative en Parse
            try
            {
                return int.Parse(Value.ToString());
            }
            catch { }

            // Tant pis !
            return DefaultValue;
        }

        public static long ObjectToLong(object Value)
        {
            return ObjectToLong(Value, 0);
        }

        public static long ObjectToLong(object Value, long DefaultValue)
        {
            if (Value == null || Value == DBNull.Value) return DefaultValue;


            // Première tentative en Cast
            Type Tpe = Value.GetType();

            if (Tpe == SysTools.TpOf_Decimal) try { return (long)((decimal)Value); }
                catch { }
            if (Tpe == SysTools.TpOf_Byte) try { return (long)((byte)Value); }
                catch { }
            if (Tpe == SysTools.TpOf_Double) try { return (long)((double)Value); }
                catch { }
            if (Tpe == SysTools.TpOf_Float) try { return (long)((float)Value); }
                catch { }
            if (Tpe == SysTools.TpOf_Long) try { return (long)((long)Value); }
                catch { }
            if (Tpe == SysTools.TpOf_Short) try { return (long)((short)Value); }
                catch { }
            if (Tpe == SysTools.TpOf_UInt) try { return (long)((uint)Value); }
                catch { }
            if (Tpe == SysTools.TpOf_ULong) try { return (long)((ulong)Value); }
                catch { }
            if (Tpe == SysTools.TpOf_UShort) try { return (long)((ushort)Value); }
                catch { }
            if (Tpe == SysTools.TpOf_Int) try { return (long)((int)Value); }
                catch { }

            try
            {
                return (long)Value;
            }
            catch { }

            // Deuxième tentative en Parse
            try
            {
                return long.Parse(Value.ToString());
            }
            catch { }

            // Tant pis !
            return DefaultValue;
        }

        public static string ObjectToString(object Value)
        {
            return ObjectToString(Value, "");
        }

        public static string ObjectToString(object Value, string DefaultValue)
        {
            if (Value == null || Value == DBNull.Value) return DefaultValue;

            return Value.ToString().Trim();
        }

        public static bool ObjectToBool(object Value)
        {
            return ObjectToBool(Value, false);
        }

        public static bool ObjectToBool(object Value, bool DefaultValue)
        {
            if (Value == null || Value == DBNull.Value) return DefaultValue;

            Type Tp = Value.GetType();


            // Première tentative en Cast
            if (Tp == TpOf_Bool) return (bool)Value;
            if (Tp == TpOf_Decimal) return ((decimal)Value == 0 ? false : true);
            if (Tp == TpOf_Byte) return ((byte)Value == 0 ? false : true);
            if (Tp == TpOf_Int) return ((int)Value == 0 ? false : true);
            if (Tp == TpOf_Long) return ((long)Value == 0 ? false : true);
            if (Tp == TpOf_SByte) return ((sbyte)Value == 0 ? false : true);
            if (Tp == TpOf_Short) return ((short)Value == 0 ? false : true);
            if (Tp == TpOf_UInt) return ((uint)Value == 0 ? false : true);
            if (Tp == TpOf_ULong) return ((ulong)Value == 0 ? false : true);
            if (Tp == TpOf_UShort) return ((ushort)Value == 0 ? false : true);

            //			try
            //			{
            //				return (bool)Value;
            //			}
            //			catch{}

            // Deuxième tentative en Parse
            try
            {
                return bool.Parse(Value.ToString());
            }
            catch { }

            // Tant pis !
            return DefaultValue;
        }

        public static DateTime ObjectToDateTime(object Value)
        {
            return ObjectToDateTime(Value, DateTime.MinValue);
        }

        public static DateTime ObjectToDateTime(object Value, DateTime DefaultValue)
        {
            if (Value == null || Value == DBNull.Value) return DefaultValue;

            // Première tentative en Cast
            try
            {
                return (DateTime)Value;
            }
            catch { }

            // Deuxième tentative en Parse
            try
            {
                return DateTime.Parse(Value.ToString());
            }
            catch { }

            // Tant pis !
            return DefaultValue;
        }
        #endregion

        #region DataRow GET
        public static DateTime DatatRow_ValueDateTime(DataRow Rw, string FieldName)
        {
            if (Rw == null) return DateTime.MinValue;
            return ObjectToDateTime(DataRow_Value(Rw, FieldName, DateTime.MinValue), DateTime.MinValue);
        }

        public static DateTime DatatRow_ValueDateTime(DataRow Rw, string FieldName, DateTime DefaultValue)
        {
            if (Rw == null) return DefaultValue;
            return ObjectToDateTime(DataRow_Value(Rw, FieldName, DefaultValue), DefaultValue);
        }

        public static bool DataRow_ValueBool(DataRow Rw, string FieldName)
        {
            if (Rw == null) return false;
            return ObjectToBool(DataRow_Value(Rw, FieldName, false), false);
        }

        public static bool DataRow_ValueBool(DataRow Rw, string FieldName, bool DefaultValue)
        {
            if (Rw == null) return DefaultValue;
            return ObjectToBool(DataRow_Value(Rw, FieldName, DefaultValue), DefaultValue);
        }

        public static double DataRow_ValueDouble(DataRow Rw, string FieldName)
        {
            if (Rw == null) return 0;
            return ObjectToDouble(DataRow_Value(Rw, FieldName, 0), 0);
        }

        public static double DataRow_ValueDouble(DataRow Rw, string FieldName, double DefaultValue)
        {
            if (Rw == null) return DefaultValue;
            return ObjectToDouble(DataRow_Value(Rw, FieldName, DefaultValue), DefaultValue);
        }

        public static DateTime DataRow_ValueDateTime(DataRow Rw, string FieldName)
        {
            if (Rw == null) return DateTime.MinValue;
            return ObjectToDateTime(DataRow_Value(Rw, FieldName, DateTime.MinValue), DateTime.MinValue);
        }

        public static DateTime DataRow_ValueDateTime(DataRow Rw, string FieldName, DateTime DefaultValue)
        {
            if (Rw == null) return DefaultValue;
            return ObjectToDateTime(DataRow_Value(Rw, FieldName, DefaultValue), DefaultValue);
        }

        public static string DataRow_ValueString(DataRow Rw, string FieldName)
        {
            if (Rw == null) return "";
            return ObjectToString(DataRow_Value(Rw, FieldName, ""), "");
        }

        public static Image DataRow_ValueImage(DataRow Rw, string FieldName)
        {
            if (Rw == null) return null;
            byte[] array = (byte[])DataRow_Value(Rw, FieldName, null);
            if (array == null) return null;
            Image img = byteArrayToImage(array);
            return img;

        }

        public static string DataRow_ValueString(DataRow Rw, string FieldName, string DefaultValue)
        {
            if (Rw == null) return DefaultValue;
            return ObjectToString(DataRow_Value(Rw, FieldName, DefaultValue), DefaultValue);
        }

        public static int DataRow_ValueInt(DataRow Rw, string FieldName)
        {
            if (Rw == null) return -1;
            return ObjectToInt(DataRow_Value(Rw, FieldName, 0), 0);
        }

        public static int DataRow_ValueInt(DataRow Rw, string FieldName, int DefaultValue)
        {
            if (Rw == null) return DefaultValue;
            return ObjectToInt(DataRow_Value(Rw, FieldName, DefaultValue), DefaultValue);
        }

        public static object DataRow_Value(DataRow Rw, string FieldName)
        {
            return DataRow_Value(Rw, FieldName, null);
        }

        public static object DataRow_Value(DataRow Rw, string FieldName, object DefaultValue)
        {
            if (Rw == null || Rw.Table == null || Rw.RowState == DataRowState.Deleted) return DefaultValue;

            int Ndx = Rw.Table.Columns.IndexOf(FieldName);
            if (Ndx < 0) return DefaultValue;

            if (Rw[Ndx] is DBNull) return DefaultValue;
            return Rw[Ndx];
        }
        #endregion

    }

    public static class Builders
    {


        



        public static PlanTraitementObject BuildPlanTraitementObject(DataRow r)
        {
            PlanTraitementObject su = new PlanTraitementObject();
            su.IdResumclinique = SysTools.DataRow_ValueInt(r, "ID_DIAG");
            su.CtrlKey = SysTools.DataRow_ValueString(r, "CTRLKEY");
            su.ResourceName = SysTools.DataRow_ValueString(r, "RESOURCENAME");
            double x1 = SysTools.DataRow_ValueDouble(r, "X1");
            double y1 = SysTools.DataRow_ValueDouble(r, "Y1");
            double x2 = SysTools.DataRow_ValueDouble(r, "X2");
            double y2 = SysTools.DataRow_ValueDouble(r, "Y2");

            su.Point1 = new PointF((float)x1, (float)y1);
            su.Point2 = new PointF((float)x2, (float)y2);

            return su;

        }

        public static PlanTraitementObject BuildPlanTraitementObjectJson(JObject r)
        {
            PlanTraitementObject su = new PlanTraitementObject();
            su.IdResumclinique = r["id_diag"].ToString() == "" ? -1 : Convert.ToInt32(r["id_diag"]);
            su.CtrlKey = Convert.ToString(r["ctrlkey"]);
            su.ResourceName = Convert.ToString(r["resourcename"]);
            double x1 = r["x1"].ToString() == "" ? -1 : Convert.ToDouble(r["x1"]);
            double y1 = r["y1"].ToString() == "" ? -1 : Convert.ToDouble(r["y1"]);
            double x2 = r["x2"].ToString() == "" ? -1 : Convert.ToDouble(r["x2"]);
            double y2 = r["Y2"].ToString() == "" ? -1 : Convert.ToDouble(r["Y2"]);

            su.Point1 = new PointF((float)x1, (float)y1);
            su.Point2 = new PointF((float)x2, (float)y2);

            return su;

        }


        public static ScenarEnBouche BuildScenarEnBouche(DataRow r)
        {
            ScenarEnBouche su = new ScenarEnBouche();
            su.IdCommDebut = SysTools.DataRow_ValueInt(r, "ID_COMM_DEBUT");
            su.IdCommFin = SysTools.DataRow_ValueInt(r, "ID_COMM_FIN");
            su.Id = SysTools.DataRow_ValueInt(r, "ID");
            su.type = (ElementDent.Materials)SysTools.DataRow_ValueInt(r, "TYPEMATERIAL");
            su.Dents = SysTools.DataRow_ValueString(r, "DENTS");
            su.IdAppareil = SysTools.DataRow_ValueInt(r, "ID_APPAREIL");
            su.Haut = SysTools.DataRow_ValueBool(r, "HAUT");
            su.Bas = SysTools.DataRow_ValueBool(r, "BAS");

            return su;

        }


        public static ScenarCommPhoto BuildScenarCommPhotos(DataRow r)
        {
            ScenarCommPhoto com = new ScenarCommPhoto();

            com.typephoto = (CommPhoto.TypePhoto)Convert.ToInt32(r["TYPEPHOTO"]);
            return com;
        }

        public static ScenarCommRadio BuildScenarCommRadio(DataRow r)
        {
            ScenarCommRadio com = new ScenarCommRadio();

            com.typeradio = (CommRadio.TypeRadio)Convert.ToInt32(r["TYPERADIO"]);
            return com;
        }



        public static CommCliniqueDetailsScenario BuildCommCliniqueAfaire(DataRow r)
        {
            CommCliniqueDetailsScenario com = new CommCliniqueDetailsScenario();

            com.Id = Convert.ToInt32(r["ID"]);
            com.IdActe = r["ID_ACTE"] is DBNull ? -1 : Convert.ToInt32(r["ID_ACTE"]);
            com.Commentaire = Convert.ToString(r["COMMENTAIRES"]);
            com.CommentaireAFaire = Convert.ToString(r["COMMENTAIRESAFAIRE"]);
            com.IdScenario = r["ID_SCENARIO"] is DBNull ? -1 : Convert.ToInt32(r["ID_SCENARIO"]);
            com.NbJours = r["NBJOURS"] is DBNull ? -1 : Convert.ToInt32(r["NBJOURS"]);
            com.NbMois = r["NBMOIS"] is DBNull ? -1 : Convert.ToInt32(r["NBMOIS"]);
            com.numSemestre = r["num_semestre"] is DBNull ? "" : Convert.ToString(r["num_semestre"]);
            com.IdParent = r["id_parentcomment"] is DBNull ? -1 : Convert.ToInt32(r["id_parentcomment"]);
            com.Ordre = r["ordre"] is DBNull ? -1 : Convert.ToInt32(r["ordre"]);
            com.IsReferenceDate = r["refdate"] is DBNull ? false : Convert.ToString(r["refdate"]) == "Y" || Convert.ToString(r["refdate"]) == "T" || Convert.ToString(r["refdate"]) == "0";

            return com;
        }
        public static CommonObjectif BuildCommonObjectifJson(JObject r)
        {
            CommonObjectif obj = new CommonObjectif();

            obj.Id = r["id"].ToString() == "" ? -1 : Convert.ToInt32(r["id"]);
            obj.Libelle = Convert.ToString(r["libelle"]);
            obj.Description = Convert.ToString(r["description"]);

            return obj;

        }

        private static bool exist(string path)
        {
            try
                    {
                        var req = System.Net.WebRequest.Create(path);
                        using (Stream stream = req.GetResponse().GetResponseStream())
                        {
                          
                        }
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
            return true;

        }
        public static ResumeClinique BuildResumeCliniqueJson(JObject r)
        {
            ResumeClinique rc = new ResumeClinique();

            rc.Id = r["id"].ToString() == "" ? -1 : Convert.ToInt32(r["id"]);
            rc.dateResume = Convert.ToDateTime(r["dateresume"]);
            rc.IdPatient = r["id_patient"].ToString() == "" ? -1 : Convert.ToInt32(r["id_patient"]);

            rc.DeviationLevreInf = r["deviationlevreinf"].ToString() == "" ? -1 : Convert.ToInt32(r["deviationlevreinf"]);
            rc.DeviationMenton = r["deviationmenton"].ToString() == "" ? -1 : Convert.ToInt32(r["deviationmenton"]);
            rc.EtageInf = r["etageinf"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_EtageInf.undefined : (BasCommon_BO.EntentePrealable.en_EtageInf)Convert.ToInt32(r["etageinf"]);


            rc.sourireDentaire = r["souriredentaire"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_SourireDentaire.undefined : (BasCommon_BO.EntentePrealable.en_SourireDentaire)Convert.ToInt32(r["souriredentaire"]);
            rc.DiagAlveolaire = r["diagalveolaire"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_DiagAlveolaire.undefined : (BasCommon_BO.EntentePrealable.en_DiagAlveolaire)Convert.ToInt32(r["diagalveolaire"]);
           // rc.TNLDroit = r["TNLDroit"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_TriangleNoirLateral.undefined : (BasCommon_BO.EntentePrealable.en_TriangleNoirLateral)Convert.ToInt32(r["TNLDroit"]);
            rc.TNLDroit = r["tnldroit"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_TriangleNoirLateralType.undefined : (BasCommon_BO.EntentePrealable.en_TriangleNoirLateralType)Convert.ToInt32(r["tnldroit"]);
            rc.TNLGauche = r["tnlgauche"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_TriangleNoirLateralType.undefined : (BasCommon_BO.EntentePrealable.en_TriangleNoirLateralType)Convert.ToInt32(r["tnlgauche"]);
            rc.DecalageInterIncisiveHaut = r["decalageinterincisivedg"].ToString() == "" ? -1 : Convert.ToInt32(r["decalageinterincisivedg"]);
            rc.DecalageInterIncisiveBas = r["decalageinterincisivehb"].ToString() == "" ? -1 : Convert.ToInt32(r["decalageinterincisivehb"]);


            rc.ClasseCanD = r["classecand"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_Class.undefined : (BasCommon_BO.EntentePrealable.en_Class)Convert.ToInt32(r["classecand"]);
            rc.ClasseCanG = r["classecang"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_Class.undefined : (BasCommon_BO.EntentePrealable.en_Class)Convert.ToInt32(r["classecang"]);
            rc.ClasseMolD = r["classemold"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_Class.undefined : (BasCommon_BO.EntentePrealable.en_Class)Convert.ToInt32(r["classemold"]);
            rc.ClasseMolG = r["classemolg"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_Class.undefined : (BasCommon_BO.EntentePrealable.en_Class)Convert.ToInt32(r["classemolg"]);
            rc.SensTransvMand = r["senstransvmand"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_DiagOsseux.undefined : (BasCommon_BO.EntentePrealable.en_DiagOsseux)Convert.ToInt32(r["senstransvmand"]);
            rc.SensTransvMax = r["senstransvmax"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_DiagOsseux.undefined : (BasCommon_BO.EntentePrealable.en_DiagOsseux)Convert.ToInt32(r["senstransvmax"]);
            rc.DiagMand = r["diagmand"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_DiagAlveolaire.undefined : (BasCommon_BO.EntentePrealable.en_DiagAlveolaire)Convert.ToInt32(r["diagmand"]);
            rc.DiagMax = r["diagmax"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_DiagAlveolaire.undefined : (BasCommon_BO.EntentePrealable.en_DiagAlveolaire)Convert.ToInt32(r["diagmax"]);
            rc.OcclusionInverse = r["occlusioninverse"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_OccInverse.undefined : (BasCommon_BO.EntentePrealable.en_OccInverse)Convert.ToInt32(r["occlusioninverse"]);
            rc.SautArticule = r["sautarticule"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_OuiNon.undefined : (BasCommon_BO.EntentePrealable.en_OuiNon)Convert.ToInt32(r["sautarticule"]);
           //rc.ArticuleInvAnt = r["articuleinvant"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_OuiNon.undefined : (BasCommon_BO.EntentePrealable.en_OuiNon)Convert.ToInt32(r["articuleinvant"]);

            rc.NoTaquets = r["notaquets"].ToString() == "" ? "" : Convert.ToString(r["notaquets"]);
            rc.NoMvts = r["nomvts"].ToString() == "" ? "" : Convert.ToString(r["nomvts"]);

           






            rc.OcclusionValue = r["occlusionvalue"].ToString() == "" ? -1 : Convert.ToInt32(r["occlusionvalue"]);
            rc.OcclusionFace = r["occlusionface"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_OccFace.undefined : (BasCommon_BO.EntentePrealable.en_OccFace)Convert.ToInt32(r["occlusionface"]);
            rc.Laterodeviation = r["laterodeviation"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_Laterodeviation.undefined : (BasCommon_BO.EntentePrealable.en_Laterodeviation)Convert.ToInt32(r["laterodeviation"]);

            rc.InterpositonLingual = r["interpositonlingual"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_InterpositionLingual.undefined : (BasCommon_BO.EntentePrealable.en_InterpositionLingual)Convert.ToInt32(r["interpositonlingual"]);
            rc.FormeArcade = r["formearcade"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_FormeArcade.undefined : (BasCommon_BO.EntentePrealable.en_FormeArcade)Convert.ToInt32(r["formearcade"]);
            rc.SurplombValue = r["surplombvalue"].ToString() == "" ? -1 : Convert.ToInt32(r["surplombvalue"]);
            rc.FreinLabial = r["freinlabial"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_OuiNon.undefined : (BasCommon_BO.EntentePrealable.en_OuiNon)Convert.ToInt32(r["freinlabial"]);
            rc.FreinLingual = r["freinlingual"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_OuiNon.undefined : (BasCommon_BO.EntentePrealable.en_OuiNon)Convert.ToInt32(r["freinlingual"]);


            rc.LangueBasse = r["languebasse"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_OuiNon.undefined : (BasCommon_BO.EntentePrealable.en_OuiNon)Convert.ToInt32(r["languebasse"]);
            rc.DDD = r["ddd"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_OuiNon.undefined : (BasCommon_BO.EntentePrealable.en_OuiNon)Convert.ToInt32(r["ddd"]);
            rc.DDM = r["ddm"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_OuiNon.undefined : (BasCommon_BO.EntentePrealable.en_OuiNon)Convert.ToInt32(r["ddm"]);


            rc.SourireGingivalSup = r["souriregingivalsup"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_OuiNon.undefined : (BasCommon_BO.EntentePrealable.en_OuiNon)Convert.ToInt32(r["souriregingivalsup"]);
            rc.LabialValue = r["labialvalue"].ToString() == "" ? -1 : Convert.ToInt32(r["labialvalue"]);
            rc.GingivalInfValue = r["gingivalinfvalue"].ToString() == "" ? -1 : Convert.ToInt32(r["gingivalinfvalue"]);
            rc.GingivalSupValue = r["gingivalsupvalue"].ToString() == "" ? -1 : Convert.ToInt32(r["gingivalsupvalue"]);
            rc.SourireGingivalInf = r["souriregingivalinf"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_OuiNon.undefined : (BasCommon_BO.EntentePrealable.en_OuiNon)Convert.ToInt32(r["souriregingivalinf"]);
            rc.SourireLabial = r["sourirelabial"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_OuiNon.undefined : (BasCommon_BO.EntentePrealable.en_OuiNon)Convert.ToInt32(r["sourirelabial"]);


            rc.InclinaisonIncisiveSupValue = r["inclinaisonincisivesupvalue"].ToString() == "" ? -1 : Convert.ToInt32(r["inclinaisonincisivesupvalue"]);
            rc.IncisiveSuperieur = r["incisivesuperieur"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_ProRetro.undefined : (BasCommon_BO.EntentePrealable.en_ProRetro)Convert.ToInt32(r["incisivesuperieur"]);
            rc.Menton = r["menton"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_ProRetro.undefined : (BasCommon_BO.EntentePrealable.en_ProRetro)Convert.ToInt32(r["menton"]);
            rc.LevreInferieur = r["levreinferieur"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_ProRetro.undefined : (BasCommon_BO.EntentePrealable.en_ProRetro)Convert.ToInt32(r["levreinferieur"]);
            rc.LevreSuperieur = r["levresuperieur"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_ProRetro.undefined : (BasCommon_BO.EntentePrealable.en_ProRetro)Convert.ToInt32(r["levresuperieur"]);

            rc.IdModelEntente = r["id_modele_entente"].ToString() == "" ? -1 : Convert.ToInt32(r["id_modele_entente"]);
            rc.FormeRespiration = r["formerespiration"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_Respiration.undefined : (BasCommon_BO.EntentePrealable.en_Respiration)Convert.ToInt32(r["formerespiration"]);



            rc.SensSagittalMandBasal = r["senssagittalmandbasal"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_ProRetro.undefined : (BasCommon_BO.EntentePrealable.en_ProRetro)Convert.ToInt32(r["senssagittalmandbasal"]);
            rc.SensSagittalMaxBasal = r["senssagittalmaxbasal"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_ProRetro.undefined : (BasCommon_BO.EntentePrealable.en_ProRetro)Convert.ToInt32(r["senssagittalmaxbasal"]);
            rc.IncisiveInferieur = r["incisiveinferieur"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_ProRetro.undefined : (BasCommon_BO.EntentePrealable.en_ProRetro)Convert.ToInt32(r["incisiveinferieur"]);
            rc.SensSagittal = r["senssagittal"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_Class.undefined : (BasCommon_BO.EntentePrealable.en_Class)Convert.ToInt32(r["senssagittal"]);
            rc.SensVertical = r["sensvertical"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_Divergence.undefined : (BasCommon_BO.EntentePrealable.en_Divergence)Convert.ToInt32(r["sensvertical"]);
            rc.SPP_SPA = r["spp"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_SPP.normale : (BasCommon_BO.EntentePrealable.en_SPP)Convert.ToInt32(r["spp"]);


            rc.EvolGermesDesDentsSur = r["evolgermesdesdentssur"].ToString() == "" ? "" : Convert.ToString(r["evolgermesdesdentssur"]);
            rc.EvolGermesDesDents = r["evolgermesdesdents"].ToString() == "" ? BasCommon_BO.EntentePrealable.en_OuiNon.undefined : (BasCommon_BO.EntentePrealable.en_OuiNon)Convert.ToInt32(r["evolgermesdesdents"]);
            rc.DentsDeSagesse = r["dentsdesagesse"].ToString() == "" ? "" : Convert.ToString(r["dentsdesagesse"]);
            rc.DentsSurnumeraires = r["dentssurnumeraires"].ToString() == "" ? "" : Convert.ToString(r["dentssurnumeraires"]);
            rc.DentsIncluses = r["dentsincluses"].ToString() == "" ? "" : Convert.ToString(r["dentsincluses"]);
            rc.Agenesie = r["agenesie"].ToString() == "" ? "" : Convert.ToString(r["agenesie"]);
            string tmpPath = "";



            rc.Img_Rad_Face = r["img_rad_face"].ToString() == "" ? "" : Convert.ToString(r["img_rad_face"]);
            tmpPath = basePatient.RepertoireImage + rc.IdPatient + "/" + rc.Img_Rad_Face.Substring(rc.Img_Rad_Face.LastIndexOf("\\") + 1); 
            if ((rc.Img_Rad_Face != "") && exist(tmpPath))
                rc.Img_Rad_Face = tmpPath;
            tmpPath = "";

            rc.Img_Rad_Pano = r["img_rad_pano"].ToString() == "" ? "" : Convert.ToString(r["img_rad_pano"]);

            tmpPath = basePatient.RepertoireImage + rc.IdPatient + "/" + rc.Img_Rad_Pano.Substring(rc.Img_Rad_Pano.LastIndexOf("\\") + 1); 

            if ((rc.Img_Rad_Pano != "") && exist(tmpPath))
                rc.Img_Rad_Pano = tmpPath;

            tmpPath = "";
            rc.Img_Rad_Profile = Convert.ToString(r["img_rad_profile"]);
            tmpPath = basePatient.RepertoireImage + rc.IdPatient + "/" + rc.Img_Rad_Profile.Substring(rc.Img_Rad_Profile.LastIndexOf("\\") + 1);
            if ((rc.Img_Rad_Profile != "") && exist(tmpPath))
                rc.Img_Rad_Profile = tmpPath;
            tmpPath = "";
            rc.Img_Ext_Face = Convert.ToString(r["img_ext_face"]);
            tmpPath = basePatient.RepertoireImage + rc.IdPatient + "/" + rc.Img_Ext_Face.Substring(rc.Img_Ext_Face.LastIndexOf("\\") + 1);
            if ((rc.Img_Ext_Face != "") && exist(tmpPath))
                rc.Img_Ext_Face = tmpPath;
            tmpPath = "";
            rc.Img_Ext_Profile = Convert.ToString(r["img_ext_profile"]);
            tmpPath = basePatient.RepertoireImage + rc.IdPatient + "/" + rc.Img_Ext_Profile.Substring(rc.Img_Ext_Profile.LastIndexOf("\\") + 1);
            if ((rc.Img_Ext_Profile != "") && exist(tmpPath))
                rc.Img_Ext_Profile = tmpPath;
            tmpPath = "";
            rc.Img_Ext_Profile_Sourire = r["img_ext_profile_sourire"].ToString() == "" ? "" : Convert.ToString(r["img_ext_profile_sourire"]);
            tmpPath = basePatient.RepertoireImage + rc.IdPatient + "/" + rc.Img_Ext_Profile_Sourire.Substring(rc.Img_Ext_Profile_Sourire.LastIndexOf("\\") + 1);
            if ((rc.Img_Ext_Profile_Sourire != "") && exist(tmpPath))
                rc.Img_Ext_Profile_Sourire = tmpPath;
            tmpPath = "";
            rc.Img_Ext_Face_Sourire = r["img_ext_face_sourire"].ToString() == "" ? "" : Convert.ToString(r["img_ext_face_sourire"]);
            tmpPath = basePatient.RepertoireImage + rc.IdPatient + "/" + rc.Img_Ext_Face_Sourire.Substring(rc.Img_Ext_Face_Sourire.LastIndexOf("\\") + 1);
            if ((rc.Img_Ext_Face_Sourire != "") && exist(tmpPath))
                rc.Img_Ext_Face_Sourire = tmpPath;
            tmpPath = "";
            rc.Img_Ext_Sourire = r["img_ext_sourire"].ToString() == "" ? "" : Convert.ToString(r["img_ext_sourire"]);
            tmpPath = basePatient.RepertoireImage + rc.IdPatient + "/" + rc.Img_Ext_Sourire.Substring(rc.Img_Ext_Sourire.LastIndexOf("\\") + 1);
            if ((rc.Img_Ext_Sourire != "") && exist(tmpPath))
                rc.Img_Ext_Sourire = tmpPath;
            tmpPath = "";
            rc.Img_Int_Droit = r["img_int_droit"].ToString() == "" ? "" : Convert.ToString(r["img_int_droit"]);
            tmpPath = basePatient.RepertoireImage + rc.IdPatient + "/" + rc.Img_Int_Droit.Substring(rc.Img_Int_Droit.LastIndexOf("\\") + 1);
            if ((rc.Img_Int_Droit != "") && exist(tmpPath))
                rc.Img_Int_Droit = tmpPath;
            tmpPath = "";
            rc.Img_Int_SurPlomb = r["img_int_surplomb"].ToString() == "" ? "" : Convert.ToString(r["img_int_surplomb"]);
            tmpPath = basePatient.RepertoireImage + rc.IdPatient + "/" + rc.Img_Int_SurPlomb.Substring(rc.Img_Int_SurPlomb.LastIndexOf("\\") + 1);
            if ((rc.Img_Int_SurPlomb != "") && exist(tmpPath))
                rc.Img_Int_SurPlomb = tmpPath;
            tmpPath = "";
            rc.Img_Int_Face = r["img_int_face"].ToString() == "" ? "" : Convert.ToString(r["img_int_face"]);
            tmpPath = basePatient.RepertoireImage + rc.IdPatient + "/" + rc.Img_Int_Face.Substring(rc.Img_Int_Face.LastIndexOf("\\") + 1);

            if ((rc.Img_Int_Face != "") && exist(tmpPath))
                rc.Img_Int_Face = tmpPath;
            tmpPath = "";
            rc.Img_Int_Gauche = r["img_int_gauche"].ToString() == "" ? "" : Convert.ToString(r["img_int_gauche"]);
            tmpPath = basePatient.RepertoireImage + rc.IdPatient + "/" + rc.Img_Int_Gauche.Substring(rc.Img_Int_Gauche.LastIndexOf("\\") + 1);

            if ((rc.Img_Int_Gauche != "") && exist(tmpPath))
                rc.Img_Int_Gauche = tmpPath;
            tmpPath = "";
            rc.Img_Int_Max = r["img_int_max"].ToString() == "" ? "" : Convert.ToString(r["img_int_max"]);
            tmpPath = basePatient.RepertoireImage + rc.IdPatient + "/" + rc.Img_Int_Max.Substring(rc.Img_Int_Max.LastIndexOf("\\") + 1);

            if ((rc.Img_Int_Max != "") && exist(tmpPath))
                rc.Img_Int_Max = tmpPath;
            tmpPath = "";
            rc.Img_Int_Man = r["img_int_man"].ToString() == "" ? "" : Convert.ToString(r["img_int_man"]);
            tmpPath = basePatient.RepertoireImage + rc.IdPatient + "/" + rc.Img_Int_Man.Substring(rc.Img_Int_Man.LastIndexOf("\\") + 1);

            if ((rc.Img_Int_Man != "") && exist(tmpPath))
                rc.Img_Int_Man = tmpPath;
            tmpPath = "";

            rc.Img_Moul_Droit = r["img_moul_droit"].ToString() == "" ? "" : Convert.ToString(r["img_moul_droit"]);
            tmpPath = basePatient.RepertoireImage + rc.IdPatient + "/" + rc.Img_Moul_Droit.Substring(rc.Img_Moul_Droit.LastIndexOf("\\") + 1);

            if ((rc.Img_Moul_Droit != "") && exist(tmpPath))
                rc.Img_Moul_Droit = tmpPath;

            tmpPath = "";
            rc.Img_Moul_Face = r["img_moul_face"].ToString() == "" ? "" : Convert.ToString(r["img_moul_face"]);
            tmpPath = basePatient.RepertoireImage + rc.IdPatient + "/" + rc.Img_Moul_Face.Substring(rc.Img_Moul_Face.LastIndexOf("\\") + 1);

            if ((rc.Img_Moul_Face != "") && exist(tmpPath))
                rc.Img_Moul_Face = tmpPath;
            tmpPath = "";

            rc.Img_Moul_Gauche = r["img_moul_gauche"].ToString() == "" ? "" : Convert.ToString(r["img_moul_gauche"]);
            tmpPath = basePatient.RepertoireImage + rc.IdPatient + "/" + rc.Img_Moul_Gauche.Substring(rc.Img_Moul_Gauche.LastIndexOf("\\") + 1);

            if ((rc.Img_Moul_Gauche != "") && exist(tmpPath))
                rc.Img_Moul_Gauche = tmpPath;
            tmpPath = "";
            rc.Img_Moul_Max = r["img_moul_max"].ToString() == "" ? "" : Convert.ToString(r["img_moul_max"]);
            tmpPath = basePatient.RepertoireImage + rc.IdPatient + "/" + rc.Img_Moul_Max.Substring(rc.Img_Moul_Max.LastIndexOf("\\") + 1);

            if ((rc.Img_Moul_Max != "") && exist(tmpPath))
                rc.Img_Moul_Max = tmpPath;
            tmpPath = "";
            rc.Img_Moul_Man = r["img_moul_man"].ToString() == "" ? "" : Convert.ToString(r["img_moul_man"]);
            tmpPath = basePatient.RepertoireImage + rc.IdPatient + "/" + rc.Img_Moul_Man.Substring(rc.Img_Moul_Man.LastIndexOf("\\") + 1);

            if ((rc.Img_Moul_Man != "") && exist(tmpPath))
                rc.Img_Moul_Man = tmpPath;
            tmpPath = "";



            if (!(r["listofpointsan1"].ToString() == ""))
            {

                byte[] array = (byte[])r["listofpointsan1"];
                System.IO.MemoryStream stream = new System.IO.MemoryStream(array);
                BinaryFormatter serializer = new BinaryFormatter();

                serializer.Binder = new BaseDiagDeserializationBinder();

                rc.LstPtAnalyse1 = (List<PointToTake>)serializer.Deserialize(stream);

            }

            if (!(r["listofpointsan2"].ToString() == ""))
            {
                byte[] array = (byte[])r["listofpointsan2"];
                System.IO.MemoryStream stream = new System.IO.MemoryStream(array);
                BinaryFormatter serializer = new BinaryFormatter();

                serializer.Binder = new BaseDiagDeserializationBinder();

                rc.LstPtAnalyse2 = (List<PointToTake>)serializer.Deserialize(stream);

            }

            if (!(r["listofpointsan6"].ToString() == ""))
            {
                byte[] array = (byte[])r["listofpointsan6"];
                System.IO.MemoryStream stream = new System.IO.MemoryStream(array);
                BinaryFormatter serializer = new BinaryFormatter();

                serializer.Binder = new BaseDiagDeserializationBinder();

                rc.LstPtAnalyse62 = (List<PointToTake>)serializer.Deserialize(stream);

            }

            if (!(r["listofpointsan7"].ToString() == ""))
            {
                byte[] array = (byte[])r["listofpointsan7"];
                System.IO.MemoryStream stream = new System.IO.MemoryStream(array);
                BinaryFormatter serializer = new BinaryFormatter();

                serializer.Binder = new BaseDiagDeserializationBinder();

                rc.LstPtAnalyse7 = (List<PointToTake>)serializer.Deserialize(stream);

            }

            if ((r["syncro_x"].ToString() == "") || (r["syncro_y"].ToString() == "") || (r["syncro_zoom"].ToString() == "") || (r["syncro_rotation"].ToString() == ""))
                rc.IsSynchronized = false;
            else
                rc.IsSynchronized = true;

            float x = r["syncro_x"].ToString() == "" ? 0 : Convert.ToSingle(r["syncro_x"]);
            float y = r["syncro_y"].ToString() == "" ? 0 : Convert.ToSingle(r["syncro_y"]);
            rc.synchrooffset = new System.Drawing.PointF(x, y);
            rc.synchrozoom = r["syncro_zoom"].ToString() == "" ? 0 : Convert.ToSingle(r["syncro_zoom"]);
            rc.synchroangle = r["syncro_rotation"].ToString() == "" ? 0 : Convert.ToSingle(r["syncro_rotation"]);


            return rc;

        }



        public static ScenarCommMateriel BuildScenarCommMateriel(DataRow r)
        {
            ScenarCommMateriel com = new ScenarCommMateriel();

            com.IdBaseProduit = r["ID_BASEPRODUIT"] is DBNull ? -1 : Convert.ToInt32(r["ID_BASEPRODUIT"]);
            com.Libelle = Convert.ToString(r["LIBELLE"]).Trim();
            com.Qte = Convert.ToInt32(r["QTE"]);
            com.ShortLib = Convert.ToString(r["SHORTLIB"]).Trim();

            return com;
        }


        public static ScenarioCommClinique BuildCommcliniquescenario(DataRow dr)
        {
            ScenarioCommClinique cs = new ScenarioCommClinique();
            cs.Id = Convert.ToInt32(dr["ID"]);
            cs.Libelle = Convert.ToString(dr["LIBELLE"]);
            cs.NbSemestres = Convert.ToInt32(dr["NBSemestres"]);
            cs.TypeTtmnt = Convert.ToString(dr["TypeTtmnt"]);

            return cs;
        }

        public static CommClinique BuildCommClinique(DataRow r)
        {
            CommClinique com = new CommClinique();

            com.Id = Convert.ToInt32(r["ID"]);
            com.IdPraticien = r["ID_PRATICIEN"] is DBNull ? -1 : Convert.ToInt32(r["ID_PRATICIEN"]);
            com.IdAssistante = r["ID_ASSISTANTE"] is DBNull ? -1 : Convert.ToInt32(r["ID_ASSISTANTE"]);
            com.IdSecretaire = r["ID_SECRETAIRE"] is DBNull ? -1 : Convert.ToInt32(r["ID_SECRETAIRE"]);
            com.IdActe = r["ID_ACTE"] is DBNull ? -1 : Convert.ToInt32(r["ID_ACTE"]);
            com.Hygiene = r["Hygiene"] is DBNull ? -1 : Convert.ToInt32(r["Hygiene"]);
            com.IdRDV = r["ID_RDV"] is DBNull ? -1 : Convert.ToInt32(r["ID_RDV"]);
            com.IdPatient = r["ID_PATIENT"] is DBNull ? -1 : Convert.ToInt32(r["ID_PATIENT"]);
            com.date = r["DATE_COMM"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(r["DATE_COMM"]);
            com.Commentaire = Convert.ToString(r["COMMENTAIRES"]);
            com.CommentaireAFaire = r["COMMENTAIRESAFAIRE"] is DBNull ? "" : Convert.ToString(r["COMMENTAIRESAFAIRE"]);
            com.Hygiene = r["Hygiene"] is DBNull ? -1 : Convert.ToInt32(r["Hygiene"]);
            com.NbJours = r["NBJOURS"] is DBNull ? 0 : Convert.ToInt32(r["NBJOURS"]);
            com.NbMois = r["NBMOIS"] is DBNull ? 0 : Convert.ToInt32(r["NBMOIS"]);
            com.IdScenario = r["Id_Scenario"] is DBNull ? -1 : Convert.ToInt32(r["Id_Scenario"]);
            com.IdSemestre = r["Id_Semestre"] is DBNull ? -1 : Convert.ToInt32(r["Id_Semestre"]);
            com.IdParentComment = r["Id_ParentComment"] is DBNull ? -1 : Convert.ToInt32(r["Id_ParentComment"]);
            com.IsDateDeRef = r["IsDateDeRef"] is DBNull ? false : Convert.ToString(r["IsDateDeRef"]) == "T" || Convert.ToString(r["IsDateDeRef"]) == "Y";

            return com;
        }


        public static EntiteJuridique BuildEntiteJuridique(DataRow r)
        {

            EntiteJuridique apg = new EntiteJuridique();
            apg.Id = r["ID"] is DBNull ? -1 : Convert.ToInt32(r["ID"]);
            apg.Nom = r["NOM"] is DBNull ? "" : Convert.ToString(r["NOM"]);
            apg.Adresse1 = r["ADRESSE1"] is DBNull ? "" : Convert.ToString(r["ADRESSE1"]);
            apg.Adresse2 = r["ADRESSE2"] is DBNull ? "" : Convert.ToString(r["ADRESSE2"]);
            apg.CodePostal = r["CODEPOSTAL"] is DBNull ? "" : Convert.ToString(r["CODEPOSTAL"]);
            apg.Ville = r["VILLE"] is DBNull ? "" : Convert.ToString(r["VILLE"]);

            return apg;
        }


        
        public static CommentHisto BuildCommentHisto(DataRow r)
        {
            CommentHisto pa = new CommentHisto();
            pa.Id = r["ID"] is DBNull ? -1 : Convert.ToInt32(r["ID"]);
            pa.Id_Ecrivain = r["ID_WRITER"] is DBNull ? -1 : Convert.ToInt32(r["ID_WRITER"]);
            pa.IdPatient = r["ID_PATIENT"] is DBNull ? -1 : Convert.ToInt32(r["ID_PATIENT"]);
            pa.typecomment = r["TYPE_COMMENT"] is DBNull ? CommentHisto.CommentHistoType.undefined : (CommentHisto.CommentHistoType)Convert.ToInt32(r["TYPE_COMMENT"]);
            pa.DateCommentaire = r["DATE_COMMENT"] is DBNull ? DateTime.Now : Convert.ToDateTime(r["DATE_COMMENT"]);
            pa.comment = r["COMMENT"] is DBNull ? "" : Convert.ToString(r["COMMENT"]);
            return pa;
        }

        public static ModeleDePropositions BuildModeleDeProposition(DataRow r)
        {
            ModeleDePropositions mdl = new ModeleDePropositions();

            mdl.Id = r["ID"] is DBNull ? -1 : Convert.ToInt32(r["ID"]);
            mdl.Nom = r["LIBELLE"] is DBNull ? "" : Convert.ToString(r["LIBELLE"]); 

            return mdl;
        }
        /*
        public static PoseAppareil BuildPoseAppareil(DataRow r)
        {
            PoseAppareil pa = new PoseAppareil();
            pa.Id = r["ID"] is DBNull ? -1 : Convert.ToInt32(r["ID"]);
            pa.appareil = AppareilMgmt.getAppareil(Convert.ToInt32(r["ID_APPAREIL"]));
            return pa;
        }
        */
        public static Surveillance BuildSurveillance(DataRow r)
        {
            Surveillance su = new Surveillance();
            su.DateDebut = SysTools.DataRow_ValueDateTime(r, "DATEDEBUT");
            su.DateFin = SysTools.DataRow_ValueDateTime(r, "DATEFIN");
            su.Id = SysTools.DataRow_ValueInt(r, "ID");
            su.Montant_Honoraire = SysTools.DataRow_ValueDouble(r, "MONTANT");
            su.traitementSecu = r["ID_TRAITMNTSECU"] is DBNull ? null : TemplateApctePGMgmt.getTemplatesActeGestion(Convert.ToInt32(r["ID_TRAITMNTSECU"]));
            su.IdSemestre = SysTools.DataRow_ValueInt(r, "ID_SEMESTRE");

            return su;

        }

        
        
        public static CommonAppareilFromObj BuildCommonAppareilFromObj(DataRow r)
        {
            CommonAppareilFromObj cod = new CommonAppareilFromObj();
            cod.appareil = AppareilMgmt.getAppareil(Convert.ToInt32(r["ID_APPAREIL"]));
            cod.objectif = CommonObjectifsMgmt.getCommonObjectif(Convert.ToInt32(r["ID_OBJECTIF"]));
            cod.Description = r["DESCRIPTION"] is DBNull ? "" : Convert.ToString(r["DESCRIPTION"]);
            return cod;

        }

        public static CommonObjectifFromDiag BuildCommonObjectifFromDiag(DataRow r)
        {
            CommonObjectifFromDiag cod = new CommonObjectifFromDiag();
            cod.diagnostic = CommonDiagnosticsMgmt.getCommonDiagnostics(Convert.ToInt32(r["ID_DIAG"]));
            cod.objectif = CommonObjectifsMgmt.getCommonObjectif(Convert.ToInt32(r["ID_OBJ"]));
            cod.Descritpion = r["DESCRIPTION"] is DBNull ? "" : Convert.ToString(r["DESCRIPTION"]);
            cod.TxtInvisalign = r["INVISALIGNTXT"] is DBNull ? "" : Convert.ToString(r["INVISALIGNTXT"]);
            cod.NumOption = r["NUM_OPTION"] is DBNull ? -1 : Convert.ToInt32(r["NUM_OPTION"]);
            cod.NumDevis = r["NUM_DEVIS"] is DBNull ? -1 : Convert.ToInt32(r["NUM_DEVIS"]);
            cod.DisplayOrder = r["DISPLAYORDER"] is DBNull ? -1 : Convert.ToInt32(r["DISPLAYORDER"]);
            cod.NumDiag = r["NumDiag"] is DBNull ? "" : Convert.ToString(r["NumDiag"]);
            cod.SpecialInstruction = r["SPECIALINSTRUCTIONS"] is DBNull ? "" : Convert.ToString(r["SPECIALINSTRUCTIONS"]);
            cod.DiagCanceled = r["DiagCanceled"] is DBNull ? false : Convert.ToString(r["DiagCanceled"])=="T";

            return cod;

        }

        public static CommonObjectifFromDiag BuildCommonObjectifFromDiagJson(JObject r)
        {
            CommonObjectifFromDiag cod = new CommonObjectifFromDiag();
            cod.diagnostic = CommonDiagnosticsMgmt.getCommonDiagnostics(Convert.ToInt32(r["id_diag"]));
            cod.objectif = CommonObjectifsMgmt.getCommonObjectif(Convert.ToInt32(r["id_obj"]));
            cod.Descritpion =Convert.ToString(r["description"]);
            cod.TxtInvisalign = Convert.ToString(r["invisaligntxt"]);
            cod.NumOption = r["num_option"].ToString() == "" ? -1 : Convert.ToInt32(r["num_option"]);
            cod.NumDevis = r["num_devis"].ToString() == "" ? -1 : Convert.ToInt32(r["num_devis"]);
            cod.DisplayOrder = r["displayorder"].ToString() == "" ? -1 : Convert.ToInt32(r["displayorder"]);
            cod.NumDiag = Convert.ToString(r["NumDiag"]);
            cod.SpecialInstruction = Convert.ToString(r["specialinstructions"]);
            cod.DiagCanceled = r["diagcanceled"].ToString() == "" ? false : Convert.ToString(r["diagcanceled"]) == "T";

            return cod;

        }

        public static CommonObjectif BuildCommonObjectif(DataRow r)
        {
            CommonObjectif obj = new CommonObjectif();

            obj.Id = r["ID"] == DBNull.Value ? -1 : Convert.ToInt32(r["ID"]);
            obj.Libelle = r["LIBELLE"] == DBNull.Value ? "" : Convert.ToString(r["LIBELLE"]);
            obj.Description = r["description"] == DBNull.Value ? "" : Convert.ToString(r["description"]);
            obj.categorie = r["Categorie"] == DBNull.Value ? CommonObjectif.CategorieObjectifs.Aucune : (CommonObjectif.CategorieObjectifs)Convert.ToInt32(r["Categorie"]);

            return obj;

        }

        public static CommonDiagnostic BuildCommonDiagnostic(DataRow r)
        {
            CommonDiagnostic obj = new CommonDiagnostic();

            obj.Id = r["ID"] == DBNull.Value ? -1 : Convert.ToInt32(r["ID"]);
            obj.Libelle = r["LIBELLE"] == DBNull.Value ? "" : Convert.ToString(r["LIBELLE"]);
            obj.question = r["QUESTION"] == DBNull.Value ? "" : Convert.ToString(r["QUESTION"]);
            obj.SetPhotos( r["Photos"] == DBNull.Value ? "" : Convert.ToString(r["Photos"]));
            obj.DisplayOrder = r["DisplayOrder"] == DBNull.Value ? 0 : Convert.ToInt32(r["DisplayOrder"]);
            return obj;

        }
        public static CommonDiagnostic BuildCommonDiagnosticJson(JObject r)
        {
            CommonDiagnostic obj = new CommonDiagnostic();

            obj.Id = r["id"].ToString() == "" ? -1 : Convert.ToInt32(r["id"]);
            obj.Libelle = Convert.ToString(r["libelle"]);
            obj.question = Convert.ToString(r["question"]);
            obj.SetPhotos(Convert.ToString(r["photos"]));
            obj.DisplayOrder = r["displayorder"].ToString() == "" ? 0 : Convert.ToInt32(r["displayorder"]);

            return obj;

        }
       
        public static TypePers BuildTypePers(DataRow r)
        {
            TypePers obj = new TypePers();

            obj.IdType = r["ID_TYPE"] == DBNull.Value ? -1 : Convert.ToInt32(r["ID_TYPE"]);
            obj.Nom = r["Nom"] == DBNull.Value ? "" : Convert.ToString(r["Nom"]);


            return obj;

        }



     

        public static BasePrinterSetting BuildBasePrinterSetting(DataRow r)
        {
            BasePrinterSetting cs = new BasePrinterSetting();
            cs.Descriptif = Convert.ToString(r["DESCRIPTIF"]).Trim();
            cs.Libelle = Convert.ToString(r["LIBELLE"]).Trim();

            MemoryStream stream = new MemoryStream((byte[])r["settings"]);
            BinaryFormatter bformatter = new BinaryFormatter();

            cs.settings = (PrinterSettings)bformatter.Deserialize(stream);
            stream.Close();

            return cs;
        }

        public static CodePrestation BuildCodePresta(DataRow r)
        {
            CodePrestation cs = new CodePrestation();
            cs.Code = Convert.ToString(r["ID_PRESTATION"]).Trim();
            cs.Libelle = Convert.ToString(r["LIBELLE"]).Trim();
            cs.Valeur = Convert.ToDouble(r["VALEUR_CLE_EURO"]);
            return cs;
        }

        public static TemplateActePG BuildTemplateActePG(DataRow r)
        {
            TemplateActePG cs = new TemplateActePG();
            cs.Id = Convert.ToInt32(r["ID"]);
            cs.Nom = Convert.ToString(r["CODE"]).Trim();
            cs.Libelle = Convert.ToString(r["LIBELLE"]).Trim();
            cs.Code = TemplateApctePGMgmt.getCodePrestation(Convert.ToString(r["CODE_PRESTATION"]).Trim());
            cs.Coeff = Convert.ToInt32(r["ACTE_COEFF"]);
            cs.CoeffDecompose = Convert.ToString(r["DECOMP"]);
            cs.IsDecomposed = Convert.ToBoolean(r["DECOMPISVISIBLE"]);
            cs.NeedDEP = r["NEED_DEP"] is DBNull ? false : Convert.ToInt16(r["NEED_DEP"]) == 1;
            cs.NeedFS = r["NEED_FSE"] is DBNull ? false : Convert.ToInt16(r["NEED_FSE"]) == 1;
            cs.NBJours = r["NB_JOURS"] is DBNull ? null : (int?)Convert.ToInt32(r["NB_JOURS"]);
            cs.NBMois = r["NB_MOIS"] is DBNull ? null : (int?)Convert.ToInt32(r["NB_MOIS"]);
            cs.Valeur = Convert.ToDouble(r["VALEUR"]);
            cs.phase = r["PHASE"] is DBNull ? BasCommon_BO.Traitement.EnumPhase.Aucune : (BasCommon_BO.Traitement.EnumPhase)Convert.ToInt16(r["PHASE"]);
            cs.CorrespondanceOrthalis = r["ID_ACTE_ORTHALIS"] is DBNull ? -1:Convert.ToInt32(r["ID_ACTE_ORTHALIS"]);

            return cs;
        }


        
        public static ObjSuivi BuildObjSuivi(DataRow r)
        {



            ObjSuivi suivi = new ObjSuivi();
            suivi.Id = Convert.ToInt32(r["id"]);
            suivi.NatureTravaux = Convert.ToString(r["nature"]);
            suivi.Details = Convert.ToString(r["Detail"]);
            suivi.PoseApp = Convert.ToDateTime(r["pose_app"]);
            suivi.Empreinte = Convert.ToDateTime(r["DATEEMPREINTE"]);

            return suivi;
        }
        
        public static ObjectifSuggests BuildObjectifSuggests(DataRow r)
        {

            ObjectifSuggests dvs = new ObjectifSuggests();

            dvs.Id = Convert.ToInt32(r["id_key"]);
            dvs.Libelle = Convert.ToString(r["libelle"]);
            
            return dvs;
        }

        public static ActePGPropose BuildActesHorstraitement(DataRow dr)
        {


            ActePGPropose co = new ActePGPropose();
            co.IdDevis = Convert.ToInt32(dr["ID_DEVIS"]);
            co.DateExecution = dr["DATE_EXECUTION"] is DBNull ? null : (DateTime?)Convert.ToDateTime(dr["DATE_EXECUTION"]);
            co.IdTemplateActePG = Convert.ToInt32(dr["ID_TEMPLATE_ACTE_GESTION"]);
            co.Montant = Convert.ToDouble(dr["MONTANT"]);
            co.Optionnel = Convert.ToBoolean(dr["OPTIONAL"]);
            co.Libelle = Convert.ToString(dr["Libelle"]);
            co.Qte = Convert.ToInt32(dr["QTE"]);
            return co;

        }
        

        public static Devis BuildDevis(DataRow r)
        {

            Devis devis = new Devis();
            devis.Id = Convert.ToInt32(r["ID"]);
            devis.DateProposition = Convert.ToDateTime(r["DATEPROPOSITION"]);
            devis.DateAcceptation = r["DATEACCEPTATION"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["DATEACCEPTATION"]);
            devis.DateEcheance = r["DATEECHEANCE"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["DATEECHEANCE"]);


            devis.IdPatient = Convert.ToInt32(r["ID_PATIENT"]);
            devis.IdObjetBaseView = r["ID_OBJET_BASEVIEW"] is DBNull ? -1 : Convert.ToInt32(r["ID_OBJET_BASEVIEW"]);
            
            return devis;
        }

        /*
        public static Devis BuildDevis(DataRow r)
        {

            Devis dvs = new Devis();
            dvs.Id = Convert.ToInt32(r["ID"]);
            dvs.IdPatient = Convert.ToInt32(r["ID_PATIENT"]);
            dvs.typedevis = TypeDevisMgmt.getTypeDevis(Convert.ToInt32(r["TYPE_DEVIS"]));
            dvs.DateProposition = r["DATEPROPOSITION"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["DATEPROPOSITION"]);
            dvs.DateSignature = r["DATESIGNATURE"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["DATESIGNATURE"]);
            dvs.Montant = Convert.ToInt32(r["MONTANT_DEVIS"]);
            dvs.Duree = Convert.ToInt32(r["DUREE_DEVIS"]);
            dvs.FacetteEsthetique = Convert.ToBoolean(r["OPT_FACETTE"]);
            dvs.KitEclaircissement = Convert.ToBoolean(r["OPT_KIT_ECLAIR"]);
            dvs.KitTractionSurMiniVis = Convert.ToBoolean(r["OPT_KIT_TRACTION"]);
            dvs.ContentionBAS1Arcade = Convert.ToBoolean(r["OPT_CONT_BAS_1ARC"]);
            dvs.ContentionBAS2Arcades = Convert.ToBoolean(r["OPT_CONT_BAS_2ARC"]);
            dvs.ContentionBASJeu = Convert.ToBoolean(r["OPT_CONT_BAS_JEU"]);
            dvs.ContentionVIVERA1Arcade = Convert.ToBoolean(r["OPT_CONT_VIVERA_1ARC"]);
            dvs.ContentionVIVERA2Arcades = Convert.ToBoolean(r["OPT_CONT_VIVERA_2ARC"]);
            dvs.ContentionVIVERAJeu = Convert.ToBoolean(r["OPT_CONT_VIVERA_JEU"]);
            dvs.ContentionFilMetal1Arcade = Convert.ToBoolean(r["OPT_CONT_FILMETAL_1ARC"]);
            dvs.ContentionFilMetal2Arcade = Convert.ToBoolean(r["OPT_CONT_FILMETAL_2ARC"]);
            dvs.ContentionFilOr1Arcade = Convert.ToBoolean(r["OPT_CONT_FILOR_1ARC"]);
            dvs.ContentionFilOr2Arcades = Convert.ToBoolean(r["OPT_CONT_FILOR_2ARC"]);
            dvs.ContentionFilFibre1Arcade = Convert.ToBoolean(r["OPT_CONT_FILFIBRE_1ARC"]);
            dvs.ContentionFilFibre2Arcades = Convert.ToBoolean(r["OPT_CONT_FILFIBRE_2ARC"]);
            dvs.NbMiniVis = Convert.ToInt32(r["OPT_NBMINIVIS"]);

            return dvs;
        }

        public static TypeDevis BuildTypeDevis(DataRow r)
        {

            TypeDevis nfocmpl = new TypeDevis();
            nfocmpl.Id = Convert.ToInt32(r["ID"]);
            nfocmpl.libelle = Convert.ToString(r["LIBELLE"]);
            nfocmpl.Categorie = (TypeDevis.CategorieDevis)Convert.ToInt32(r["CATEGORIE"]);
            return nfocmpl;
        }
        */
        
        public static Proposition BuildProposition(DataRow r)
        {

            Proposition proposition = new Proposition();
            proposition.Id = Convert.ToInt32(r["ID"]);
            proposition.Etat = (Proposition.EtatProposition)Convert.ToInt32(r["ETAT"]);
            proposition.DateEvenement = r["DATEEVENT"] is DBNull?null: (DateTime?)Convert.ToDateTime(r["DATEEVENT"]);
            proposition.libelle = Convert.ToString(r["LIBELLE"]);
            proposition.DateAcceptation = r["DATE_ACCEPTATION"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["DATE_ACCEPTATION"]);
            proposition.DateProposition = r["DATE_PROPOSITION"]==DBNull.Value?DateTime.MinValue:Convert.ToDateTime(r["DATE_PROPOSITION"]);
            proposition.IdDevis = r["IDDEVIS"] is DBNull?-1:Convert.ToInt32(r["IDDEVIS"]);

            proposition.IdScenario = r["IDSCENARIO"] is DBNull ? -1 : Convert.ToInt32(r["IDSCENARIO"]);

            proposition.IdModel = r["ID_MODELE"] is DBNull ? -1 : Convert.ToInt32(r["ID_MODELE"]);
           
            return proposition;
        }


        public static Traitement BuildTraitement(DataRow r)
        {

            Traitement tt = new Traitement();
            tt.Id = Convert.ToInt32(r["ID"]);
            tt.Libelle = Convert.ToString(r["LIBELLE"]);
            tt.Phase = ((BasCommon_BO.Traitement.EnumPhase)Convert.ToInt16(r["PHASE"]));
            tt.CodeTraitement = r["CodeTraitement"] is DBNull ? "" : Convert.ToString(r["CodeTraitement"]);


            return tt;
        }


        public static InfoPatientComplementaire BuildInfoPatientComplementaire(DataRow r)
        {

            InfoPatientComplementaire nfocmpl = new InfoPatientComplementaire();
            nfocmpl.IdPatient = Convert.ToInt32(r["IDPATIENT"]);
            nfocmpl.AssistanteResponsable = r["ASSISTANTE_RESP"] is DBNull?null:UtilisateursMgt.getUtilisateur(Convert.ToInt32(r["ASSISTANTE_RESP"]));

             
            nfocmpl.PraticienResponsable = r["PRATICIEN_RESP"] is DBNull ? null : UtilisateursMgt.getUtilisateur(Convert.ToInt32(r["PRATICIEN_RESP"]));


            
            nfocmpl.NbSemestresEntame = r["SEMESTRESENTAMES"] is DBNull ? 0 : Convert.ToInt32(r["SEMESTRESENTAMES"]);
            nfocmpl.Ameliorations = r["AMELIORATIONS"] is DBNull ? "" : Convert.ToString(r["AMELIORATIONS"]);
            nfocmpl.DateDebutTraitement = r["DEBUTTRAITEMENTENVISAGE"] is DBNull?null:(DateTime?)Convert.ToDateTime(r["DEBUTTRAITEMENTENVISAGE"]);

            
            return nfocmpl;
        }
        
        public static Utilisateur BuildUtilisateur(DataRow r)
        {

            Utilisateur Ut = new Utilisateur();
            Ut.Id = Convert.ToInt32(r["ID_PERSONNE"]);
            Ut.Nom = Convert.ToString(r["PER_NOM"]).Trim();

            Ut.DateFinContrat = r["DATEFINCONTRAT"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["DATEFINCONTRAT"]);
            Ut.Actif = ((Convert.ToString(r["UTIL_ACTIF"]) == "Y") && ((Ut.DateFinContrat > DateTime.Now) || (Ut.DateFinContrat == null)));


            Ut.Prenom = Convert.ToString(r["PER_PRENOM"]).Trim();

            Ut.Mail = Convert.ToString(r["PER_EMAIL"]).Trim();
            Ut.Profession = Convert.ToString(r["PROFESSION"]).Trim();
            Ut.Fonction = Convert.ToString(r["NOMTYPE"]).Trim();
            Ut.Tel = Convert.ToString(r["PER_TELPRINC"]).Trim();

            Ut.Adresse.Adress1 = Convert.ToString(r["PER_ADR1"]).Trim();
            Ut.Adresse.Adress2 = Convert.ToString(r["PER_ADR2"]).Trim();
            Ut.Adresse.CP = Convert.ToString(r["PER_CPOSTAL"]).Trim();
            Ut.Adresse.Ville = Convert.ToString(r["PER_VILLE"]).Trim();

            Ut.Civilite = Convert.ToString(r["PERS_TITRE"]).Trim();


            return Ut;
        }
        
        public static ResumeClinique BuildResumeClinique(DataRow r)
        {
            ResumeClinique rc = new ResumeClinique();
            try
            {

                rc.Id = r["ID"] is DBNull ? -1 : Convert.ToInt32(r["ID"]);
                rc.dateResume = Convert.ToDateTime(r["DateResume"]);
                rc.IdPatient = r["ID_PATIENT"] is DBNull ? -1 : Convert.ToInt32(r["ID_PATIENT"]);

                rc.DeviationLevreInf = r["DeviationLevreInf"] is DBNull ? -1 : Convert.ToInt32(r["DeviationLevreInf"]);
                rc.DeviationMenton = r["DeviationMenton"] is DBNull ? -1 : Convert.ToInt32(r["DeviationMenton"]);
                rc.EtageInf = r["EtageInf"] is DBNull ? BasCommon_BO.EntentePrealable.en_EtageInf.undefined : (BasCommon_BO.EntentePrealable.en_EtageInf)Convert.ToInt32(r["EtageInf"]);


                rc.sourireDentaire = r["sourireDentaire"] is DBNull ? BasCommon_BO.EntentePrealable.en_SourireDentaire.undefined : (BasCommon_BO.EntentePrealable.en_SourireDentaire)Convert.ToInt32(r["sourireDentaire"]);
                rc.DiagAlveolaire = r["DiagAlveolaire"] is DBNull ? BasCommon_BO.EntentePrealable.en_DiagAlveolaire.undefined : (BasCommon_BO.EntentePrealable.en_DiagAlveolaire)Convert.ToInt32(r["DiagAlveolaire"]);
                rc.TNLDroit = r["TNLDroit"] is DBNull ? BasCommon_BO.EntentePrealable.en_TriangleNoirLateralType.undefined : (BasCommon_BO.EntentePrealable.en_TriangleNoirLateralType)Convert.ToInt32(r["TNLDroit"]);
                rc.TNLGauche = r["TNLGauche"] is DBNull ? BasCommon_BO.EntentePrealable.en_TriangleNoirLateralType.undefined : (BasCommon_BO.EntentePrealable.en_TriangleNoirLateralType)Convert.ToInt32(r["TNLGauche"]);
                rc.DecalageInterIncisiveHaut = r["DecalageInterIncisiveDG"] is DBNull ? -1 : Convert.ToInt32(r["DecalageInterIncisiveDG"]);
                rc.DecalageInterIncisiveBas = r["DecalageInterIncisiveHB"] is DBNull ? -1 : Convert.ToInt32(r["DecalageInterIncisiveHB"]);
                rc.SPP_SPA = r["spp"] is DBNull ? BasCommon_BO.EntentePrealable.en_SPP.normale : (BasCommon_BO.EntentePrealable.en_SPP)Convert.ToInt32(r["spp"]);


                rc.ClasseCanD = r["ClasseCanD"] is DBNull ? BasCommon_BO.EntentePrealable.en_Class.undefined : (BasCommon_BO.EntentePrealable.en_Class)Convert.ToInt32(r["ClasseCanD"]);
                rc.ClasseCanG = r["ClasseCanG"] is DBNull ? BasCommon_BO.EntentePrealable.en_Class.undefined : (BasCommon_BO.EntentePrealable.en_Class)Convert.ToInt32(r["ClasseCanG"]);
                rc.ClasseMolD = r["ClasseMolD"] is DBNull ? BasCommon_BO.EntentePrealable.en_Class.undefined : (BasCommon_BO.EntentePrealable.en_Class)Convert.ToInt32(r["ClasseMolD"]);
                rc.ClasseMolG = r["ClasseMolG"] is DBNull ? BasCommon_BO.EntentePrealable.en_Class.undefined : (BasCommon_BO.EntentePrealable.en_Class)Convert.ToInt32(r["ClasseMolG"]);
                rc.SensTransvMand = r["SensTransvMand"] is DBNull ? BasCommon_BO.EntentePrealable.en_DiagOsseux.undefined : (BasCommon_BO.EntentePrealable.en_DiagOsseux)Convert.ToInt32(r["SensTransvMand"]);
                rc.SensTransvMax = r["SensTransvMax"] is DBNull ? BasCommon_BO.EntentePrealable.en_DiagOsseux.undefined : (BasCommon_BO.EntentePrealable.en_DiagOsseux)Convert.ToInt32(r["SensTransvMax"]);
                rc.DiagMand = r["DiagMand"] is DBNull ? BasCommon_BO.EntentePrealable.en_DiagAlveolaire.undefined : (BasCommon_BO.EntentePrealable.en_DiagAlveolaire)Convert.ToInt32(r["DiagMand"]);
                rc.DiagMax = r["DiagMax"] is DBNull ? BasCommon_BO.EntentePrealable.en_DiagAlveolaire.undefined : (BasCommon_BO.EntentePrealable.en_DiagAlveolaire)Convert.ToInt32(r["DiagMax"]);
                rc.OcclusionInverse = r["OcclusionInverse"] is DBNull ? BasCommon_BO.EntentePrealable.en_OccInverse.undefined : (BasCommon_BO.EntentePrealable.en_OccInverse)Convert.ToInt32(r["OcclusionInverse"]);
                rc.SautArticule = r["SautArticule"] is DBNull ? BasCommon_BO.EntentePrealable.en_OuiNon.undefined : (BasCommon_BO.EntentePrealable.en_OuiNon)Convert.ToInt32(r["SautArticule"]);

                rc.OcclusionValue = r["OcclusionValue"] is DBNull ? -1 : Convert.ToInt32(r["OcclusionValue"]);
                rc.OcclusionFace = r["OcclusionFace"] is DBNull ? BasCommon_BO.EntentePrealable.en_OccFace.undefined : (BasCommon_BO.EntentePrealable.en_OccFace)Convert.ToInt32(r["OcclusionFace"]);
                rc.Laterodeviation = r["Laterodeviation"] is DBNull ? BasCommon_BO.EntentePrealable.en_Laterodeviation.undefined : (BasCommon_BO.EntentePrealable.en_Laterodeviation)Convert.ToInt32(r["Laterodeviation"]);

                rc.InterpositonLingual = r["InterpositonLingual"] is DBNull ? BasCommon_BO.EntentePrealable.en_InterpositionLingual.undefined : (BasCommon_BO.EntentePrealable.en_InterpositionLingual)Convert.ToInt32(r["InterpositonLingual"]);
               
                rc.FormeRespiration = r["formerespiration"] is DBNull ? BasCommon_BO.EntentePrealable.en_Respiration.undefined : (BasCommon_BO.EntentePrealable.en_Respiration)Convert.ToInt32(r["formerespiration"]);

                rc.FormeArcade = r["FormeArcade"] is DBNull ? BasCommon_BO.EntentePrealable.en_FormeArcade.undefined : (BasCommon_BO.EntentePrealable.en_FormeArcade)Convert.ToInt32(r["FormeArcade"]);
                rc.SurplombValue = r["SurplombValue"] is DBNull ? -1 : Convert.ToInt32(r["SurplombValue"]);
                rc.FreinLabial = r["FreinLabial"] is DBNull ? BasCommon_BO.EntentePrealable.en_OuiNon.undefined : (BasCommon_BO.EntentePrealable.en_OuiNon)Convert.ToInt32(r["FreinLabial"]);
                rc.FreinLingual = r["FreinLingual"] is DBNull ? BasCommon_BO.EntentePrealable.en_OuiNon.undefined : (BasCommon_BO.EntentePrealable.en_OuiNon)Convert.ToInt32(r["FreinLingual"]);
                rc.Diasteme = r["Diasteme"] is DBNull ? BasCommon_BO.EntentePrealable.en_OuiNon.Oui : (BasCommon_BO.EntentePrealable.en_OuiNon)Convert.ToInt32(r["Diasteme"]);


                rc.LangueBasse = r["LangueBasse"] is DBNull ? BasCommon_BO.EntentePrealable.en_OuiNon.undefined : (BasCommon_BO.EntentePrealable.en_OuiNon)Convert.ToInt32(r["LangueBasse"]);
                rc.DDD = r["DDD"] is DBNull ? BasCommon_BO.EntentePrealable.en_OuiNon.undefined : (BasCommon_BO.EntentePrealable.en_OuiNon)Convert.ToInt32(r["DDD"]);
                rc.DDM = r["DDM"] is DBNull ? BasCommon_BO.EntentePrealable.en_OuiNon.undefined : (BasCommon_BO.EntentePrealable.en_OuiNon)Convert.ToInt32(r["DDM"]);


                rc.SourireGingivalSup = r["SourireGingivalSup"] is DBNull ? BasCommon_BO.EntentePrealable.en_OuiNon.undefined : (BasCommon_BO.EntentePrealable.en_OuiNon)Convert.ToInt32(r["SourireGingivalSup"]);
                rc.LabialValue = r["LabialValue"] is DBNull ? -1 : Convert.ToInt32(r["LabialValue"]);
                rc.GingivalInfValue = r["GingivalInfValue"] is DBNull ? -1 : Convert.ToInt32(r["GingivalInfValue"]);
                rc.GingivalSupValue = r["GingivalSupValue"] is DBNull ? -1 : Convert.ToInt32(r["GingivalSupValue"]);
                rc.SourireGingivalInf = r["SourireGingivalInf"] is DBNull ? BasCommon_BO.EntentePrealable.en_OuiNon.undefined : (BasCommon_BO.EntentePrealable.en_OuiNon)Convert.ToInt32(r["SourireGingivalInf"]);
                rc.SourireLabial = r["SourireLabial"] is DBNull ? BasCommon_BO.EntentePrealable.en_OuiNon.undefined : (BasCommon_BO.EntentePrealable.en_OuiNon)Convert.ToInt32(r["SourireLabial"]);


                rc.InclinaisonIncisiveSupValue = r["InclinaisonIncisiveSupValue"] is DBNull ? -1 : Convert.ToInt32(r["InclinaisonIncisiveSupValue"]);
                rc.IncisiveSuperieur = r["IncisiveSuperieur"] is DBNull ? BasCommon_BO.EntentePrealable.en_ProRetro.undefined : (BasCommon_BO.EntentePrealable.en_ProRetro)Convert.ToInt32(r["IncisiveSuperieur"]);
                rc.Menton = r["Menton"] is DBNull ? BasCommon_BO.EntentePrealable.en_ProRetro.undefined : (BasCommon_BO.EntentePrealable.en_ProRetro)Convert.ToInt32(r["Menton"]);
                rc.LevreInferieur = r["LevreInferieur"] is DBNull ? BasCommon_BO.EntentePrealable.en_ProRetro.undefined : (BasCommon_BO.EntentePrealable.en_ProRetro)Convert.ToInt32(r["LevreInferieur"]);
                rc.LevreSuperieur = r["LevreSuperieur"] is DBNull ? BasCommon_BO.EntentePrealable.en_ProRetro.undefined : (BasCommon_BO.EntentePrealable.en_ProRetro)Convert.ToInt32(r["LevreSuperieur"]);

                rc.IdModelEntente = r["ID_MODELE_ENTENTE"] is DBNull ? -1 : Convert.ToInt32(r["ID_MODELE_ENTENTE"]);



                rc.SensSagittalMandBasal = r["SensSagittalMandBasal"] is DBNull ? BasCommon_BO.EntentePrealable.en_ProRetro.undefined : (BasCommon_BO.EntentePrealable.en_ProRetro)Convert.ToInt32(r["SensSagittalMandBasal"]);
                rc.SensSagittalMaxBasal = r["SensSagittalMaxBasal"] is DBNull ? BasCommon_BO.EntentePrealable.en_ProRetro.undefined : (BasCommon_BO.EntentePrealable.en_ProRetro)Convert.ToInt32(r["SensSagittalMaxBasal"]);
                rc.IncisiveInferieur = r["IncisiveInferieur"] is DBNull ? BasCommon_BO.EntentePrealable.en_ProRetro.undefined : (BasCommon_BO.EntentePrealable.en_ProRetro)Convert.ToInt32(r["IncisiveInferieur"]);
                rc.SensSagittal = r["SensSagittal"] is DBNull ? BasCommon_BO.EntentePrealable.en_Class.undefined : (BasCommon_BO.EntentePrealable.en_Class)Convert.ToInt32(r["SensSagittal"]);
                rc.SensVertical = r["SensVertical"] is DBNull ? BasCommon_BO.EntentePrealable.en_Divergence.undefined : (BasCommon_BO.EntentePrealable.en_Divergence)Convert.ToInt32(r["SensVertical"]);


                rc.EvolGermesDesDentsSur = r["EvolGermesDesDentsSur"] is DBNull ? "" : Convert.ToString(r["EvolGermesDesDentsSur"]);
                rc.EvolGermesDesDents = r["EvolGermesDesDents"] is DBNull ? BasCommon_BO.EntentePrealable.en_OuiNon.undefined : (BasCommon_BO.EntentePrealable.en_OuiNon)Convert.ToInt32(r["EvolGermesDesDents"]);
                rc.DentsDeSagesse = r["DentsDeSagesse"] is DBNull ? "" : Convert.ToString(r["DentsDeSagesse"]);

                rc.NoTaquets = r["NoTaquets"] is DBNull ? "" : Convert.ToString(r["NoTaquets"]);
                rc.NoMvts = r["NoMvts"] is DBNull ? "" : Convert.ToString(r["NoMvts"]);


                rc.DentsSurnumeraires = r["DentsSurnumeraires"] is DBNull ? "" : Convert.ToString(r["DentsSurnumeraires"]);
                rc.DentsIncluses = r["DentsIncluses"] is DBNull ? "" : Convert.ToString(r["DentsIncluses"]);
                rc.Controle = r["CONTROLE"] is DBNull ? "" : Convert.ToString(r["CONTROLE"]);
                rc.Agenesie = r["Agenesie"] is DBNull ? "" : Convert.ToString(r["Agenesie"]);

                rc.Img_Rad_Face = r["Img_Rad_Face"] is DBNull ? "" : Convert.ToString(r["Img_Rad_Face"]);
                if ((rc.Img_Rad_Face != "") && (!System.IO.File.Exists(rc.Img_Rad_Face)))
                    rc.Img_Rad_Face = basePatient.RepertoireImage + "\\" + rc.Img_Rad_Face;

                rc.Img_Rad_Pano = r["Img_Rad_Pano"] is DBNull ? "" : Convert.ToString(r["Img_Rad_Pano"]);
                if ((rc.Img_Rad_Pano != "") && (!System.IO.File.Exists(rc.Img_Rad_Pano)))
                    rc.Img_Rad_Pano = basePatient.RepertoireImage + "\\" + rc.Img_Rad_Pano;

                rc.Img_Rad_Profile = r["Img_Rad_Profile"] is DBNull ? "" : Convert.ToString(r["Img_Rad_Profile"]);
                if ((rc.Img_Rad_Profile != "") && (!System.IO.File.Exists(rc.Img_Rad_Profile)))
                    rc.Img_Rad_Profile = basePatient.RepertoireImage + "\\" + rc.Img_Rad_Profile;

                rc.Img_Ext_Face = r["Img_Ext_Face"] is DBNull ? "" : Convert.ToString(r["Img_Ext_Face"]);
                if ((rc.Img_Ext_Face != "") && (!System.IO.File.Exists(rc.Img_Ext_Face)))
                    rc.Img_Ext_Face = basePatient.RepertoireImage + "\\" + rc.Img_Ext_Face;

                rc.Img_Ext_Profile = r["Img_Ext_Profile"] is DBNull ? "" : Convert.ToString(r["Img_Ext_Profile"]);
                if ((rc.Img_Ext_Profile != "") && (!System.IO.File.Exists(rc.Img_Ext_Profile)))
                    rc.Img_Ext_Profile = basePatient.RepertoireImage + "\\" + rc.Img_Ext_Profile;

                rc.Img_Ext_Profile_Sourire = r["Img_Ext_Profile_Sourire"] is DBNull ? "" : Convert.ToString(r["Img_Ext_Profile_Sourire"]);
                if ((rc.Img_Ext_Profile_Sourire != "") && (!System.IO.File.Exists(rc.Img_Ext_Profile_Sourire)))
                    rc.Img_Ext_Profile_Sourire = basePatient.RepertoireImage + "\\" + rc.Img_Ext_Profile_Sourire;

                rc.Img_Ext_Face_Sourire = r["Img_Ext_Face_Sourire"] is DBNull ? "" : Convert.ToString(r["Img_Ext_Face_Sourire"]);
                if ((rc.Img_Ext_Face_Sourire != "") && (!System.IO.File.Exists(rc.Img_Ext_Face_Sourire)))
                    rc.Img_Ext_Face_Sourire = basePatient.RepertoireImage + "\\" + rc.Img_Ext_Face_Sourire;

                rc.Img_Ext_Sourire = r["Img_Ext_Sourire"] is DBNull ? "" : Convert.ToString(r["Img_Ext_Sourire"]);
                if ((rc.Img_Ext_Sourire != "") && (!System.IO.File.Exists(rc.Img_Ext_Sourire)))
                    rc.Img_Ext_Sourire = basePatient.RepertoireImage + "\\" + rc.Img_Ext_Sourire;

                rc.Img_Int_Droit = r["Img_Int_Droit"] is DBNull ? "" : Convert.ToString(r["Img_Int_Droit"]);
                if ((rc.Img_Int_Droit != "") && (!System.IO.File.Exists(rc.Img_Int_Droit)))
                    rc.Img_Int_Droit = basePatient.RepertoireImage + "\\" + rc.Img_Int_Droit;

                rc.Img_Int_SurPlomb = r["Img_Int_SurPlomb"] is DBNull ? "" : Convert.ToString(r["Img_Int_SurPlomb"]);
                if ((rc.Img_Int_SurPlomb != "") && (!System.IO.File.Exists(rc.Img_Int_SurPlomb)))
                    rc.Img_Int_SurPlomb = basePatient.RepertoireImage + "\\" + rc.Img_Int_SurPlomb;

                rc.Img_Int_Face = r["Img_Int_Face"] is DBNull ? "" : Convert.ToString(r["Img_Int_Face"]);
                if ((rc.Img_Int_Face != "") && (!System.IO.File.Exists(rc.Img_Int_Face)))
                    rc.Img_Int_Face = basePatient.RepertoireImage + "\\" + rc.Img_Int_Face;

                rc.Img_Int_Gauche = r["Img_Int_Gauche"] is DBNull ? "" : Convert.ToString(r["Img_Int_Gauche"]);
                if ((rc.Img_Int_Gauche != "") && (!System.IO.File.Exists(rc.Img_Int_Gauche)))
                    rc.Img_Int_Gauche = basePatient.RepertoireImage + "\\" + rc.Img_Int_Gauche;

                rc.Img_Int_Max = r["Img_Int_Max"] is DBNull ? "" : Convert.ToString(r["Img_Int_Max"]);
                if ((rc.Img_Int_Max != "") && (!System.IO.File.Exists(rc.Img_Int_Max)))
                    rc.Img_Int_Max = basePatient.RepertoireImage + "\\" + rc.Img_Int_Max;

                rc.Img_Int_Man = r["Img_Int_Man"] is DBNull ? "" : Convert.ToString(r["Img_Int_Man"]);
                if ((rc.Img_Int_Man != "") && (!System.IO.File.Exists(rc.Img_Int_Man)))
                    rc.Img_Int_Man = basePatient.RepertoireImage + "\\" + rc.Img_Int_Man;

                rc.Img_Moul_Droit = r["Img_Moul_Droit"] is DBNull ? "" : Convert.ToString(r["Img_Moul_Droit"]);
                if ((rc.Img_Moul_Droit != "") && (!System.IO.File.Exists(rc.Img_Moul_Droit)))
                    rc.Img_Moul_Droit = basePatient.RepertoireImage + "\\" + rc.Img_Moul_Droit;


                rc.Img_Moul_Face = r["Img_Moul_Face"] is DBNull ? "" : Convert.ToString(r["Img_Moul_Face"]);
                if ((rc.Img_Moul_Face != "") && (!System.IO.File.Exists(rc.Img_Moul_Face)))
                    rc.Img_Moul_Face = basePatient.RepertoireImage + "\\" + rc.Img_Moul_Face;

                rc.Img_Moul_Gauche = r["Img_Moul_Gauche"] is DBNull ? "" : Convert.ToString(r["Img_Moul_Gauche"]);
                if ((rc.Img_Moul_Gauche != "") && (!System.IO.File.Exists(rc.Img_Moul_Gauche)))
                    rc.Img_Moul_Gauche = basePatient.RepertoireImage + "\\" + rc.Img_Moul_Gauche;

                rc.Img_Moul_Max = r["Img_Moul_Max"] is DBNull ? "" : Convert.ToString(r["Img_Moul_Max"]);
                if ((rc.Img_Moul_Max != "") && (!System.IO.File.Exists(rc.Img_Moul_Max)))
                    rc.Img_Moul_Max = basePatient.RepertoireImage + "\\" + rc.Img_Moul_Max;

                rc.Img_Moul_Man = r["Img_Moul_Man"] is DBNull ? "" : Convert.ToString(r["Img_Moul_Man"]);
                if ((rc.Img_Moul_Man != "") && (!System.IO.File.Exists(rc.Img_Moul_Man)))
                    rc.Img_Moul_Man = basePatient.RepertoireImage + "\\" + rc.Img_Moul_Man;



                if (!(r["LISTOFPOINTSAN1"] is DBNull))
                {
                    byte[] array = (byte[])r["LISTOFPOINTSAN1"];
                    MemoryStream stream = new MemoryStream(array);
                    BinaryFormatter serializer = new BinaryFormatter();

                    serializer.Binder = new BaseDiagDeserializationBinder();

                    rc.LstPtAnalyse1 = (List<PointToTake>)serializer.Deserialize(stream);

                }

                if (!(r["LISTOFPOINTSAN2"] is DBNull))
                {
                    byte[] array = (byte[])r["LISTOFPOINTSAN2"];
                    MemoryStream stream = new MemoryStream(array);
                    BinaryFormatter serializer = new BinaryFormatter();

                    serializer.Binder = new BaseDiagDeserializationBinder();

                    rc.LstPtAnalyse2 = (List<PointToTake>)serializer.Deserialize(stream);

                }

                if (!(r["LISTOFPOINTSAN6"] is DBNull))
                {
                    byte[] array = (byte[])r["LISTOFPOINTSAN6"];
                    MemoryStream stream = new MemoryStream(array);
                    BinaryFormatter serializer = new BinaryFormatter();

                    serializer.Binder = new BaseDiagDeserializationBinder();

                    rc.LstPtAnalyse62 = (List<PointToTake>)serializer.Deserialize(stream);

                }

                if (!(r["LISTOFPOINTSAN7"] is DBNull))
                {
                    byte[] array = (byte[])r["LISTOFPOINTSAN7"];
                    MemoryStream stream = new MemoryStream(array);
                    BinaryFormatter serializer = new BinaryFormatter();

                    serializer.Binder = new BaseDiagDeserializationBinder();

                    rc.LstPtAnalyse7 = (List<PointToTake>)serializer.Deserialize(stream);

                }

                if (!(r["LISTOFPOINTSANOCCD"] is DBNull))
                {
                    byte[] array = (byte[])r["LISTOFPOINTSANOCCD"];
                    MemoryStream stream = new MemoryStream(array);
                    BinaryFormatter serializer = new BinaryFormatter();

                    serializer.Binder = new BaseDiagDeserializationBinder();

                    rc.LstPtAnalyseOccD = (List<PointToTake>)serializer.Deserialize(stream);

                }

                if (!(r["LISTOFPOINTSANOCCF"] is DBNull))
                {
                    byte[] array = (byte[])r["LISTOFPOINTSANOCCF"];
                    MemoryStream stream = new MemoryStream(array);
                    BinaryFormatter serializer = new BinaryFormatter();

                    serializer.Binder = new BaseDiagDeserializationBinder();

                    rc.LstPtAnalyseOccF = (List<PointToTake>)serializer.Deserialize(stream);

                }

                if (!(r["LISTOFPOINTSANOCCG"] is DBNull))
                {
                    byte[] array = (byte[])r["LISTOFPOINTSANOCCG"];
                    MemoryStream stream = new MemoryStream(array);
                    BinaryFormatter serializer = new BinaryFormatter();

                    serializer.Binder = new BaseDiagDeserializationBinder();

                    rc.LstPtAnalyseOccG = (List<PointToTake>)serializer.Deserialize(stream);

                }

                if (!(r["LISTOFPOINTSSOURIRE"] is DBNull))
                {
                    byte[] array = (byte[])r["LISTOFPOINTSSOURIRE"];
                    MemoryStream stream = new MemoryStream(array);
                    BinaryFormatter serializer = new BinaryFormatter();

                    serializer.Binder = new BaseDiagDeserializationBinder();

                    rc.LstPtAnalyseSourire = (List<PointToTake>)serializer.Deserialize(stream);

                }

                if ((r["SYNCRO_X"] is DBNull) || (r["SYNCRO_Y"] is DBNull) || (r["SYNCRO_ZOOM"] is DBNull) || (r["SYNCRO_ROTATION"] is DBNull))
                    rc.IsSynchronized = false;
                else
                    rc.IsSynchronized = true;

                float x = r["SYNCRO_X"] is DBNull ? 0 : Convert.ToSingle(r["SYNCRO_X"]);
                float y = r["SYNCRO_Y"] is DBNull ? 0 : Convert.ToSingle(r["SYNCRO_Y"]);
                rc.synchrooffset = new System.Drawing.PointF(x, y);
                rc.synchrozoom = r["SYNCRO_ZOOM"] is DBNull ? 0 : Convert.ToSingle(r["SYNCRO_ZOOM"]);
                rc.synchroangle = r["SYNCRO_ROTATION"] is DBNull ? 0 : Convert.ToSingle(r["SYNCRO_ROTATION"]);

            }
            catch (Exception ex)
            {
                
            }

          

           
            return rc;

        }


        public static void BuildDiagEntente(DataRow r,ref EntentePrealable ep)
        {
            if (Convert.ToBoolean(r["me_alvemaxpro"])) ep.SensSagittalAlveolaireMax = EntentePrealable.en_ProRetro.Pro;
            if (Convert.ToBoolean(r["me_alvemaxpro"])) ep.SensSagittalAlveolaireMax = EntentePrealable.en_ProRetro.Pro;
            if (Convert.ToBoolean(r["me_alvemaxendo"])) ep.SensTransversalAlveolaireMax = EntentePrealable.en_DiagAlveolaire.Endoalveolie;
            if (Convert.ToBoolean(r["me_alvemaxsupra"])) ep.SensVerticalAlveolaire = EntentePrealable.en_OccFace.Supraclusion;
            if (Convert.ToBoolean(r["me_alvemaxretro"])) ep.SensSagittalAlveolaireMax = EntentePrealable.en_ProRetro.Retro;
            if (Convert.ToBoolean(r["me_alvemaxexo"])) ep.SensTransversalAlveolaireMax = EntentePrealable.en_DiagAlveolaire.Exoalveolie;
            if (Convert.ToBoolean(r["me_alvemandpro"])) ep.SensSagittalAlveolaireMand = EntentePrealable.en_ProRetro.Pro;
            if (Convert.ToBoolean(r["me_alvemandendo"])) ep.SensTransversalAlveolaireMand = EntentePrealable.en_DiagAlveolaire.Endoalveolie;
            if (Convert.ToBoolean(r["me_alvemandinfra"])) ep.SensVerticalAlveolaire = EntentePrealable.en_OccFace.Infraclusion;
            if (Convert.ToBoolean(r["me_alvemandretro"])) ep.SensSagittalAlveolaireMand = EntentePrealable.en_ProRetro.Retro;
            if (Convert.ToBoolean(r["me_alvemandexo"])) ep.SensTransversalAlveolaireMand = EntentePrealable.en_DiagAlveolaire.Exoalveolie;
            if (Convert.ToBoolean(r["me_basmaxpro"])) ep.SensSagittalBasalMax = EntentePrealable.en_ProRetro.Pro;
            if (Convert.ToBoolean(r["me_basmaxendo"])) ep.SensTransversalBasalMax = EntentePrealable.en_DiagOsseux.Endognatie;
            if (Convert.ToBoolean(r["me_basmaxhypo"])) ep.SensVerticalBasal = EntentePrealable.en_Divergence.Hypodivergent;
            if (Convert.ToBoolean(r["me_basmaxretro"])) ep.SensSagittalBasalMax = EntentePrealable.en_ProRetro.Retro;
            if (Convert.ToBoolean(r["me_basmaxexo"])) ep.SensTransversalBasalMax = EntentePrealable.en_DiagOsseux.Exognatie;
            if (Convert.ToBoolean(r["me_basmandpro"])) ep.SensSagittalBasalMand = EntentePrealable.en_ProRetro.Pro;
            if (Convert.ToBoolean(r["me_basmandendo"])) ep.SensTransversalBasalMand = EntentePrealable.en_DiagOsseux.Endognatie;
            if (Convert.ToBoolean(r["me_basmandhyper"])) ep.SensVerticalBasal = EntentePrealable.en_Divergence.Hyperdivergent;
            if (Convert.ToBoolean(r["me_basmandretro"])) ep.SensSagittalBasalMand = EntentePrealable.en_ProRetro.Retro;
            if (Convert.ToBoolean(r["me_basmandexo"])) ep.SensTransversalBasalMand = EntentePrealable.en_DiagOsseux.Exognatie;
            if (Convert.ToBoolean(r["me_mol1"])) ep.ClasseDentaireMolaire = EntentePrealable.en_Class.Class_I;
            if (Convert.ToBoolean(r["me_mol2"])) ep.ClasseDentaireMolaire = EntentePrealable.en_Class.Class_II;
            if (Convert.ToBoolean(r["me_mol3"])) ep.ClasseDentaireMolaire = EntentePrealable.en_Class.Class_III;
            ep.ClasseDentaireMolaireTxt = Convert.ToString(r["me_moltext"]);
            if (Convert.ToBoolean(r["me_can1"])) ep.ClasseDentaireCanine = EntentePrealable.en_Class.Class_I;
            if (Convert.ToBoolean(r["me_can2"])) ep.ClasseDentaireCanine = EntentePrealable.en_Class.Class_II;
            if (Convert.ToBoolean(r["me_can3"])) ep.ClasseDentaireCanine = EntentePrealable.en_Class.Class_III;
            ep.ClasseDentaireCanineTxt = Convert.ToString(r["me_cantext"]);
            if (Convert.ToBoolean(r["me_occludroit"]) && Convert.ToBoolean(r["me_occlugauche"])) ep.occInverse = EntentePrealable.en_OccInverse.Droite_Et_Gauche;
            else
                if (Convert.ToBoolean(r["me_occlugauche"])) ep.occInverse = EntentePrealable.en_OccInverse.Gauche;
                else
                    if (Convert.ToBoolean(r["me_occludroit"])) ep.occInverse = EntentePrealable.en_OccInverse.Droite;
            if (Convert.ToBoolean(r["me_occluanter"])) ep.occInverse = EntentePrealable.en_OccInverse.Anterieur;
            ep.Agenesie = Convert.ToString(r["me_agnesie"]);
            ep.DentsIncluseSurnum = Convert.ToString(r["me_dentincl"]);
            ep.Malposition = Convert.ToString(r["me_malpos"]);
            ep.DDM = Convert.ToBoolean(r["me_dysharmo"]);
            ep.DDD = Convert.ToBoolean(r["me_dysharmodd"]);
            ep.FacteurFonctionnel = Convert.ToString(r["me_facteurfonc"]);
            ep.PlanDeTraitement = Convert.ToString(r["pat_objectif_trait2"]);
            ep.Commentaires = Convert.ToString(r["pat_objectif_comm2"]);
            ep.IdDiag = (r["id"] is DBNull) ? 0 : Convert.ToInt32(r["id"]);

        }

        public static void BuildEntenteWithoutDiag(DataRow r,ref EntentePrealable ep)
        {

            if (Convert.ToBoolean(r["ee_debuttraitement"])) ep.typetraitement = EntentePrealable.TypeDeTraitement.Debut;
            if (Convert.ToBoolean(r["ee_surveillance"])) ep.typetraitement = EntentePrealable.TypeDeTraitement.Surveillance;
            if (Convert.ToBoolean(r["ee_suite"])) ep.typetraitement = EntentePrealable.TypeDeTraitement.Semestre;
            if (Convert.ToBoolean(r["ee_contention"])) ep.typetraitement = EntentePrealable.TypeDeTraitement.Contention;
            if (Convert.ToBoolean(r["ee_autre"])) ep.typetraitement = EntentePrealable.TypeDeTraitement.Autre;
            
            int var = 0;
            
            int.TryParse(Convert.ToString(r["ee_annee"]),out var);
            ep.Contention = var;

            int.TryParse(Convert.ToString(r["ee_numsemestre"]), out var);
            ep.Semestre = var;

            ep.Autre = Convert.ToString(r["ee_autre"]);
            ep.ImmatAssure = Convert.ToString(r["ee_immat"]);
            ep.dateProposition = Convert.ToDateTime(r["ee_dateprop"]);
            ep.DateImpression = r["ee_DateImpression"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["ee_DateImpression"]);
            ep.DateAccord = r["EE_DATEACCORD"] is DBNull?null: (DateTime?)Convert.ToDateTime(r["EE_DATEACCORD"]);

            ep.Praticien = r["ID_PRATICIEN"] is DBNull ? null : UtilisateursMgt.getUtilisateur(Convert.ToInt32(r["ID_PRATICIEN"]));

            ep.cotationDesActes = Convert.ToString(r["ee_cotation"]);
            ep.IsDevisSigned = Convert.ToBoolean(r["ee_devis"]);
            ep.Commentaires = Convert.ToString(r["ee_commentaire1"]) + "\n" + Convert.ToString(r["ee_commentaire2"]) + "\n" + Convert.ToString(r["ee_commentaire3"]);
            ep.PlanDeTraitement = Convert.ToString(r["ee_traitement1"]) + "\n" + Convert.ToString(r["ee_traitement2"]) + "\n" + Convert.ToString(r["ee_traitement3"]);

            if (Convert.ToInt32(r["ee_rmo"]) == 0)
                ep.ReferenceNationalOpposable = EntentePrealable.RNO.R;
            if (Convert.ToInt32(r["ee_rmo"])==1)
                ep.ReferenceNationalOpposable = EntentePrealable.RNO.HR;
            if (Convert.ToInt32(r["ee_rmo"])==-1)
                ep.ReferenceNationalOpposable = EntentePrealable.RNO.None;


            ep.IdDiag = r["ID_MODELE_ENVOI"] is DBNull?-1: Convert.ToInt32(r["ID_MODELE_ENVOI"]);

            ep.IdModele = r["id"] is DBNull ? 0 : Convert.ToInt32(r["id"]);
        }


       

       
           


        
        public static Attribut BuildAttribut(DataRow r)
        {

            Attribut obj = null;

            obj = new Attribut();
            obj.Id = Convert.ToInt32(r["PK_ATTRIBUT"]);
            obj.Nom = Convert.ToString(r["NOM"]);


            obj.Valeur = Convert.ToString(r["VALEUR"]);
            

            return obj;
        }
        
        public static ObjImage BuildObjImage(DataRow r)
        {

            ObjImage obj = null;

            obj = new ObjImage();
            obj.Id = Convert.ToInt32(r["pk_objet"]);
            obj.fichier = Convert.ToString(r["fichier"]);
            obj.nom = Convert.ToString(r["nom"]);
            
            
            return obj;
        }
    }
}
