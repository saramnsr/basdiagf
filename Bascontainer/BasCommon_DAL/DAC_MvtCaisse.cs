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



        public static DataTable getMvtsCaisse()
        {
            lock (lockobj)
            {
                if (connection == null) getConnection();  

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "SELECT DATE_MVT, ";
                    selectQuery += "MONTANT ";
                    selectQuery += "FROM bas_mvt_caisse ";
                    selectQuery += "order by DATE_MVT";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                    DataSet ds = new DataSet();
                    MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                    adapt.Fill(ds);
                    transaction.Commit();

                    return ds.Tables[0];

                }
                catch (System.IndexOutOfRangeException)
                {
                    return null;
                }
                catch (System.Exception e)
                {
                    throw e;
                }
                finally
                {
                   connection.Close();

                }
            }
        }
        

        public static double getMontantEnCaisse()
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select sum(MONTANT) from bas_mvt_caisse";
                
                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                object o = command.ExecuteScalar();

                return o is DBNull ? 0 : Convert.ToDouble(o);

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


        public static void InsertMvtCaisse(double Montant, Utilisateur utilisateur)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "INSERT INTO  bas_mvt_caisse (DATE_MVT,MONTANT,ID_UTILISATEUR,POSTE) VALUES ";
                selectQuery += "(current_timestamp,@MONTANT,@ID_UTILISATEUR,@POSTE)";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandText = selectQuery;
                command.Parameters.Clear();

                command.Parameters.AddWithValue("@MONTANT", Montant);
                command.Parameters.AddWithValue("@ID_UTILISATEUR", utilisateur==null?DBNull.Value:(object)utilisateur.Id);
                command.Parameters.AddWithValue("@POSTE", System.Environment.MachineName);
                

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
