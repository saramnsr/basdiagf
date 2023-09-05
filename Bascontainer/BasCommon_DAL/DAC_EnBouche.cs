using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using System.Configuration;
using BasCommon_BO;
using BasCommon_BO.ElementsEnBouche.BO;
using MySql.Data.MySqlClient;

namespace BasCommon_DAL
{
    public static partial class DAC
    {


        #region En Bouche




        public static DataTable getAccessoiresEnbouche(int Id_Patient)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += " typematerial, ";
                selectQuery += " datedebut, ";
                selectQuery += " datefin, ";
                selectQuery += " dents,";
                selectQuery += " ID_APPAREIL,";
                selectQuery += " ID_COMM_DEBUT,";
                selectQuery += " ID_COMM_FIN,";
                selectQuery += " ID_PATIENT";
                selectQuery += " from bas_enbouche";
                selectQuery += " where typematerial is not null and ID_PATIENT=@Id";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@Id", Id_Patient);

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

        public static DataTable getAppareilsEnbouche(int Id_Patient)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += " typematerial, ";
                selectQuery += " datedebut, ";
                selectQuery += " datefin, ";
                selectQuery += " dents,";
                selectQuery += " ID_APPAREIL,";
                selectQuery += " ID_COMM_DEBUT,";
                selectQuery += " ID_COMM_FIN,";
                selectQuery += " ID_PATIENT,";
                selectQuery += " HAUT,";
                selectQuery += " BAS,";

                selectQuery += " ID_PATIENT";
                selectQuery += " from bas_enbouche";
                selectQuery += " where ID_APPAREIL is not null and ID_PATIENT=@Id";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@Id", Id_Patient);

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

        public static void InsertEnbouche(IElementDent elem)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "select MAX(ID)+1 as NEWID from bas_enbouche";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                object obj = command.ExecuteScalar();

                if (obj == DBNull.Value)
                    elem.Id = 1;
                else
                    elem.Id = Convert.ToInt32(obj);


                selectQuery = "insert into bas_enbouche (id, ";
                selectQuery += "                             TYPEMATERIAL, ";
                selectQuery += "                             DATEDEBUT, ";
                selectQuery += "                             DATEFIN, ";
                selectQuery += "                             DENTS, ";
                selectQuery += "                             HAUT,";
                selectQuery += "                             BAS,";
                selectQuery += "                             ID_COMM_DEBUT, ";
                selectQuery += "                             ID_COMM_FIN, ";
                selectQuery += "                             ID_PATIENT)";
                selectQuery += " values (@id, ";
                selectQuery += "                             @TYPEMATERIAL, ";
                selectQuery += "                             @DATEDEBUT, ";
                selectQuery += "                             @DATEFIN, ";
                selectQuery += "                             @DENTS, ";
                selectQuery += "                             @HAUT,";
                selectQuery += "                             @BAS,";
                selectQuery += "                             @ID_COMM_DEBUT, ";
                selectQuery += "                             @ID_COMM_FIN, ";
                selectQuery += "                             @ID_PATIENT)";




                command.CommandText = selectQuery;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", elem.Id);
                command.Parameters.AddWithValue("@ID_PATIENT", elem.IdPatient);
                command.Parameters.AddWithValue("@TYPEMATERIAL", elem.typeMaterial);

                command.Parameters.AddWithValue("@DATEDEBUT", elem.DateInstallation);
                command.Parameters.AddWithValue("@DATEFIN", elem.Datesuppression);
                command.Parameters.AddWithValue("@ID_COMM_DEBUT", elem.IdCommDebut);
                command.Parameters.AddWithValue("@ID_COMM_FIN", elem.IdCommFin);
                command.Parameters.AddWithValue("@DENTS", elem.Dents);
                command.Parameters.AddWithValue("@HAUT", elem.Haut);
                command.Parameters.AddWithValue("@BAS", elem.Bas);

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

        public static void RetirerEnbouche(IElementDent elem)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "update BAS_ENBOUCHE set DATEFIN=@DATEFIN  ";
                selectQuery += " ,ID_COMM_FIN=@ID_COMM_FIN ";
                selectQuery += " where ID=@ID ";



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                command.CommandText = selectQuery;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@ID", elem.Id);
                command.Parameters.AddWithValue("@DATEFIN", elem.Datesuppression);
                command.Parameters.AddWithValue("@ID_COMM_FIN", elem.IdCommFin);

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

        public static void InsertEnbouche(IElementAppareil elem)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "select MAX(ID)+1 as NEWID from bas_enbouche";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                object obj = command.ExecuteScalar();

                if (obj == DBNull.Value)
                    elem.Id = 1;
                else
                    elem.Id = Convert.ToInt32(obj);


                selectQuery = "insert into bas_enbouche (id, ";
                selectQuery += "                             DATEDEBUT, ";
                selectQuery += "                             DATEFIN, ";
                selectQuery += "                             ID_COMM_DEBUT, ";
                selectQuery += "                             ID_COMM_FIN, ";
                selectQuery += "                             ID_APPAREIL,";
                selectQuery += "                             HAUT,";
                selectQuery += "                             BAS,";
                selectQuery += "                             ID_PATIENT)";
                selectQuery += " values (@id, ";
                selectQuery += "                             @DATEDEBUT, ";
                selectQuery += "                             @DATEFIN, ";
                selectQuery += "                             @ID_COMM_DEBUT, ";
                selectQuery += "                             @ID_COMM_FIN, ";
                selectQuery += "                             @ID_APPAREIL,";
                selectQuery += "                             @HAUT,";
                selectQuery += "                             @BAS,";
                selectQuery += "                             @ID_PATIENT)";




                command.CommandText = selectQuery;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", elem.Id);
                command.Parameters.AddWithValue("@ID_PATIENT", elem.IdPatient);

                command.Parameters.AddWithValue("@DATEDEBUT", elem.DateInstallation);
                command.Parameters.AddWithValue("@DATEFIN", elem.Datesuppression);
                command.Parameters.AddWithValue("@ID_COMM_DEBUT", elem.IdCommDebut);
                command.Parameters.AddWithValue("@ID_COMM_FIN", elem.IdCommFin);
                command.Parameters.AddWithValue("@ID_APPAREIL", elem.Appareil == null ? DBNull.Value : (object)elem.Appareil.Id);
                command.Parameters.AddWithValue("@HAUT", elem.Haut);
                command.Parameters.AddWithValue("@BAS", elem.Bas);

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

        public static void RetirerEnbouche(IElementAppareil elem)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "update BAS_ENBOUCHE set DATEFIN=@DATEFIN  ";
                selectQuery += " ,ID_COMM_FIN=@ID_COMM_FIN ";
                selectQuery += " where ID=@ID ";



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                command.CommandText = selectQuery;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@ID", elem.Id);
                command.Parameters.AddWithValue("@DATEFIN", elem.Datesuppression);
                command.Parameters.AddWithValue("@ID_COMM_FIN", elem.IdCommFin);

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


        #endregion
    }
}
