using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using BasCommon_BO;
using MySql.Data.MySqlClient;

namespace BasCommon_DAL
{
    public static partial class DAC
    {

        public static void ReaffectStatus(StatusCliniqueManuel oldstatus, StatusCliniqueManuel newstatus)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = @"update BASE_HISTO_STATUS set ID_STATUS=@newstatus
                                        where (id_statut = @oldstatus)";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;


                command.Parameters.Clear();
                command.Parameters.AddWithValue("@newstatus", newstatus.Id);
                command.Parameters.AddWithValue("@oldstatus", oldstatus.Id);
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

        public static void deletestatuts(StatusCliniqueManuel s)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = @"delete from statuts
                                        where (id_statut = @id_statut)";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;


                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id_statut", s.Id);
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


        public static void updatestatuts(StatusCliniqueManuel s)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = @"update statuts
                                        set libelle = @libelle,
                                            organisation = @organisation,
                                            couleur = @couleur,
                                            famille_statut = @famille_statut
                                        where (id_statut = @id_statut)";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id_statut", s.Id);
                command.Parameters.AddWithValue("@libelle", s.Libelle);
                command.Parameters.AddWithValue("@organisation", s.Organisation);
                if (System.Drawing.ColorTranslator.ToHtml(s.couleur).Length > 6)
                    command.Parameters.AddWithValue("@couleur", System.Drawing.ColorTranslator.ToHtml(s.couleur).Substring(0, 6));
                else
                    command.Parameters.AddWithValue("@couleur", System.Drawing.ColorTranslator.ToHtml(s.couleur));
               // command.Parameters.AddWithValue("@couleur", System.Drawing.ColorTranslator.ToHtml(s.couleur));
                command.Parameters.AddWithValue("@famille_statut", s.FamilleStatut);
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

        public static void insertstatuts(StatusCliniqueManuel s)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select MAX(ID_STATUT)+1 as NEWID from statuts";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                object res = command.ExecuteScalar();
                if (res is DBNull)
                    s.Id = 1;
                else
                    s.Id = Convert.ToInt32(command.ExecuteScalar());


                selectQuery = @"insert into statuts (id_statut, 
                                                     libelle, 
                                                     organisation, 
                                                     couleur, 
                                                     famille_statut)
                                values (@id_statut, 
                                        @libelle, 
                                        @organisation, 
                                        @couleur, 
                                        @famille_statut)";

                command.CommandText = selectQuery;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id_statut", s.Id);
                command.Parameters.AddWithValue("@libelle", s.Libelle);
                command.Parameters.AddWithValue("@organisation", s.Organisation);
                if(s.couleur.ToArgb().ToString("X").Length > 6)
                command.Parameters.AddWithValue("@couleur", s.couleur.ToArgb().ToString("X").Substring(0,6));
                else
                    command.Parameters.AddWithValue("@couleur", s.couleur.ToArgb().ToString("X"));
                command.Parameters.AddWithValue("@famille_statut", s.FamilleStatut);
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
    }
}
