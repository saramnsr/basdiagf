using BasCommon_BO.Compta;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace BasCommon_DAL
{
    public static partial class DAC
    {




        public static void DeleteCodeComptable(CodeComptable cc)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
            }
            catch (System.Exception e)
            {
                throw e;
            }

            MySqlTransaction transaction = connection.BeginTransaction();

            try
            {
                
                string selectQuery = "delete fromBAS_CODE_COMPTABLE where CODECOMPTA=@CODECOMPTA";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@CODECOMPTA", cc.Code);
              

                command.ExecuteNonQuery();
            }
            catch (System.SystemException ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                transaction.Commit();
               connection.Close();
            }

        }


        public static void AddCodeComptable(CodeComptable cc)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
            }
            catch (System.Exception e)
            {
                throw e;
            }

            MySqlTransaction transaction = connection.BeginTransaction();

            try
            {

                string selectQuery = "insert into bas_code_comptable (CODECOMPTA, LIBELLE_COMPTA) values (@CODECOMPTA, @LIBELLE_COMPTA)";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@CODECOMPTA", cc.Code);
                command.Parameters.AddWithValue("@LIBELLE_COMPTA", cc.Libelle);
              

                command.ExecuteNonQuery();
            }
            catch (System.SystemException ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                transaction.Commit();
               connection.Close();
            }

        }


        public static DataTable getCodesComptables()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                //famille_Acte
                string selectQuery = "select codecompta, libelle_compta from bas_code_comptable order by codecompta";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

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
    }

        
}
