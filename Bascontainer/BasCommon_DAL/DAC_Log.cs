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


        public static DataTable getLogs(basePatient pat)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                //famille_Acte
                string selectQuery = @"select dte_log, 
                                       id_patient, 
                                       id_user, 
                                       commentaire, 
                                       codeaction, 
                                       category, 
                                       nommachine
                                from base_log
                                where id_patient=@idpat
                                order by dte_log asc";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@idpat",pat.Id);

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


        public static void Insert_Log(BaseLog log)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "";
                MySqlCommand command = new MySqlCommand("", connection, transaction);
                command.CommandType = CommandType.Text;




                selectQuery = "insert into base_log (";
                selectQuery += " DTE_LOG, ";
                selectQuery += " ID_PATIENT, ";
                selectQuery += " ID_USER, ";
                selectQuery += " COMMENTAIRE, ";
                selectQuery += " NOMMACHINE, ";
                selectQuery += " CATEGORY, ";
                selectQuery += " CODEACTION)";
                selectQuery += " values (@DTE_LOG, ";
                selectQuery += " @ID_PATIENT, ";
                selectQuery += " @ID_USER, ";
                selectQuery += " @COMMENTAIRE, ";
                selectQuery += " @NOMMACHINE, ";
                selectQuery += " @CATEGORY, ";
                selectQuery += " @CODEACTION)";

                command.CommandText = selectQuery;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@DTE_LOG", log.DteLog);
                command.Parameters.AddWithValue("@ID_PATIENT", log.IdPatient);
                command.Parameters.AddWithValue("@COMMENTAIRE", log.Commentaires);
                command.Parameters.AddWithValue("@NOMMACHINE", log.NomMachine);
                command.Parameters.AddWithValue("@CODEACTION", log.CodeAction);
                if(log.Category.Length > 10)
                    command.Parameters.AddWithValue("@CATEGORY", log.Category.Substring(0,10));
                else
                command.Parameters.AddWithValue("@CATEGORY", log.Category);
                command.Parameters.AddWithValue("@ID_USER", log.IdUser);

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
