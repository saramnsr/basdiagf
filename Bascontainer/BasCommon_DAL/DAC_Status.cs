using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using BasCommon_BO;
using MySql.Data.MySqlClient;
using BasCommon_DAL;
using BASEPractice_BO;



namespace BasCommon_DAL
{
    public static partial class DAC
    {
        public static DataTable getUserStatus()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "select id,code, libelle, absence from rh_status";

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

        public static DataTable getUserStatus(bool IsAbsence)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "select id, libelle,absence,CODE from rh_status";
                selectQuery += " where absence=@abs";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@abs", IsAbsence ? "Y" : "N");

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

        public static Boolean IsExistStatus(Utilisateur p_Utilisateur, UserStatus p_statusToApply)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select count(*) from rh_base_status";
                selectQuery += " where id_utilisateur = @id_utilisateur";
                selectQuery += " and id_status = @id_status";
                selectQuery += " and date_status_start = @date_status_start";
                selectQuery += " and date_status_end = @date_status_end";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandText = selectQuery;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id_utilisateur", p_Utilisateur.Id);
                command.Parameters.AddWithValue("@id_status", p_statusToApply.status.Id);
                command.Parameters.AddWithValue("@date_status_start", p_statusToApply.dateStart);
                command.Parameters.AddWithValue("@date_status_end", p_statusToApply.dateEnd);
                command.Parameters.AddWithValue("@creation_date", DateTime.Now);
                command.ExecuteNonQuery();

                int Id = Convert.ToInt32(command.ExecuteScalar());
                if (Id > 0)
                    return true;
                else
                    return false;
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

        public static void AddStatus(Utilisateur p_Utilisateur, UserStatus p_statusToApply)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select MAX(ID)+1 as NEWID from rh_base_status";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                object res = command.ExecuteScalar();
                int Id;
                if (res is DBNull)
                    Id = 1;
                else
                    Id = Convert.ToInt32(command.ExecuteScalar());


                selectQuery = "insert into rh_base_status (id,id_utilisateur, id_status, date_status_start, date_status_end,creation_date)";
                selectQuery += " values (@id,@id_utilisateur, @id_status, @date_status_start, @date_status_end, @creation_date)";

                command.CommandText = selectQuery;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@id_utilisateur", p_Utilisateur.Id);
                command.Parameters.AddWithValue("@id_status", p_statusToApply.status.Id);
                command.Parameters.AddWithValue("@date_status_start", p_statusToApply.dateStart);
                command.Parameters.AddWithValue("@date_status_end", p_statusToApply.dateEnd);
                command.Parameters.AddWithValue("@creation_date", DateTime.Now);
                command.ExecuteNonQuery();

                transaction.Commit();

            }
            catch (System.SystemException ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool verifstatut(StatusCliniqueManuel s)
        {

            if (connection == null) getConnection(); 

            if (connection.State == ConnectionState.Closed) connection.Open();
            
                MySqlTransaction transaction = connection.BeginTransaction();
             try
                 {

                    string selectQuery = "SELECT count(id_status) from  base_histo_status where id_status =@id_statut ";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.CommandText = selectQuery;

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_statut", s.Id);
                    object res = command.ExecuteScalar();

                    if (Convert.ToInt32( res) == 0)
                        return true;
                    else
                        return false;
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


        public static void Deletestatuts(StatusCliniqueManuel s)
        {
           

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();

           
            try
            {
               
               
                    string selectQuery = @"  delete from statuts";
                    selectQuery += "  where (id_statut = @id_statut) ";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.CommandType = CommandType.Text;

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_statut", s.Id);
                    command.ExecuteNonQuery();


                    transaction.Commit();

              //  }
               // else {  }


            }

            catch (System.SystemException ex)
            {
                transaction.Rollback();
             
              throw ex;

            }

            finally
            {
                connection.Close();
            }

        }




       
    }
}