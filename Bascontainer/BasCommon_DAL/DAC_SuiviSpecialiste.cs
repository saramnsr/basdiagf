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


        public static DataTable getSuiviSpecialistes(int IdPatient)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {




                string selectQuery = @"select id, 
                                               id_patient, 
                                               date_envois, 
                                               id_correspondant, 
                                               commentaires,
                                               trim(personne.per_prenom)||' '||trim(personne.per_nom) as NomCorrespondant
                                        from bas_suivispecialiste
                                        inner join personne on personne.id_personne = bas_suivispecialiste.id_correspondant
                                        where id_patient=@id_patient";

                


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id_patient", IdPatient);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();


                return ds.Tables[0];



            }
            catch (System.IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                return null;
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
        
        public static void AddSuiviSpecialistes(SuiviSpecialiste ss)
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
                string selectQuery = "select MAX(ID)+1 as NEWID from BAS_SUIVISPECIALISTE";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                try
                {

                    ss.Id = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (System.InvalidCastException)
                {
                    ss.Id = 1;
                }



                selectQuery = @"insert into bas_suivispecialiste (id, 
                                                              id_patient, 
                                                              date_envois, 
                                                              id_correspondant, 
                                                              commentaires)
                            values (@id, 
                                    @id_patient, 
                                    @date_envois, 
                                    @id_correspondant, 
                                    @commentaires)";


                command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", ss.Id);
                command.Parameters.AddWithValue("@id_patient", ss.IdPatient);
                command.Parameters.AddWithValue("@date_envois", ss.DateEnvois);
                command.Parameters.AddWithValue("@id_correspondant", ss.IdCorrespondant);
                command.Parameters.AddWithValue("@commentaires", ss.Commentaire);

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
        
        public static void UpdateSuiviSpecialistes(SuiviSpecialiste ss)
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




                string selectQuery = @"update bas_suivispecialiste
                                        set id_patient = @id_patient,
                                            date_envois = @date_envois,
                                            id_correspondant = @id_correspondant,
                                            commentaires = @commentaires
                                        where (id = @id)";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", ss.Id);
                command.Parameters.AddWithValue("@id_patient", ss.IdPatient);
                command.Parameters.AddWithValue("@date_envois", ss.DateEnvois);
                command.Parameters.AddWithValue("@id_correspondant", ss.IdCorrespondant);
                command.Parameters.AddWithValue("@commentaires", ss.Commentaire);

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
        
        public static void DeleteSuiviSpecialistes(SuiviSpecialiste ss)
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




                string selectQuery = @"delete from bas_suivispecialiste
                                        where (id = @id)";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", ss.Id);

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
