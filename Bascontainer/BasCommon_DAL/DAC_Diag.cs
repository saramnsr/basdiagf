using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using System.Configuration;
using BasCommon_BO;
using MySql.Data.MySqlClient;

namespace BasCommon_DAL
{
    public static partial class DAC
    {
        public static DataRow getDiag(int id)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = "SELECT a.ID, a.LIBELLE, a.CATEGORIE, a.QUESTION, a.PHOTOS";
                selectQuery += "      FROM bas_commondiag a where a.ID = @id ";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@id", id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count == 0) return null;

                return dt.Rows[0];



            }
            catch (System.SystemException ex)
            {
                throw ex;
            }
            finally
            {
               connection.Close();
               

            }
        }

        public static DataTable getObjectifs(int id)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = "SELECT  a.ID_OBJ ,b.LIBELLE,b.DESCRIPTION,b.CATEGORIE FROM bas_commondiag_commonobj a";
                selectQuery += "      inner join BAS_COMMONOBJECTIF b on a.ID_OBJ = b.ID where a.ID_DIAG = @id ";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@id", id);
                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];

                return dt;

            }
            catch (System.SystemException ex)
            {
                throw ex;
            }
            finally
            {
               connection.Close();

            }

        }

        public static DataRow getDiagObj(int id_diag, int id_obj)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {

                string selectQuery = "SELECT a.ID_DIAG, a.ID_OBJ, a.DESCRIPTION, a.INVISALIGNTXT, a.NUM_DEVIS, a.SPECIALINSTRUCTIONS";
                selectQuery += "      FROM bas_commondiag_commonobj a  where a.ID_DIAG=@id_diag AND a.ID_OBJ=@id_obj";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@id_diag", id_diag);
                command.Parameters.AddWithValue("@id_obj", id_obj);
                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count == 0) return null;

                return dt.Rows[0];
            }
            catch (System.SystemException ex)
            {
                throw ex;
            }
            finally
            {
               connection.Close();

            }

        }

        public static void InsertObjectif(Objectif objectif)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "select MAX(ID)+1 as NEWID from BAS_COMMONOBJECTIF";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                object obj = command.ExecuteScalar();

                if (obj == DBNull.Value)
                    objectif.id_objectif = 1;
                else
                    objectif.id_objectif = Convert.ToInt32(obj);
                selectQuery = "insert into BAS_COMMONOBJECTIF (ID, ";
                selectQuery += "                             LIBELLE, ";
                selectQuery += "                             DESCRIPTION, ";
                selectQuery += "                             CATEGORIE)";
                selectQuery += " values (@ID, ";
                selectQuery += "        @LIBELLE, ";
                selectQuery += "        @DESCRIPTION, ";
                selectQuery += "        @CATEGORIE )";
                command.CommandText = selectQuery;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@ID", objectif.id_objectif);
                command.Parameters.AddWithValue("@LIBELLE", objectif.libelle);
                command.Parameters.AddWithValue("@DESCRIPTION", objectif.description);
                command.Parameters.AddWithValue("@CATEGORIE", objectif.categorie);
                command.ExecuteNonQuery();

                transaction.Commit();

            }
            catch (System.Exception e)
            {
                transaction.Rollback();
                throw e;
            }
            finally
            {
               connection.Close();

            }



        }
    }
}
