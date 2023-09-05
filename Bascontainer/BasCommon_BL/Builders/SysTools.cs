using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.IO;

namespace BasCommon_BL.Builders
{
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
}
