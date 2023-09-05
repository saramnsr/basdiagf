using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using BasCommon_BO;

namespace BasCommon_DAL
{
    public static partial class DAC
    {

        public static DataRow getCurrentRecontactPatient(int IdPatient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {




                string selectQuery = "select coalesce(base_recontact.id,-1),";
                selectQuery += " rendez_vous.id_personne as ID_PATIENT,";
                selectQuery += " coalesce(base_recontact.MOTIF,";
                selectQuery += " case";
                //selectQuery += "   when rendez_vous_anu.id_rdv is not null then 'Rendez vous annulé'";
                selectQuery += "   when 1<>1 then 'Rendez vous annulé'";
                selectQuery += "   else";
                selectQuery += "         case  rendez_vous.rdv_statut";
                selectQuery += "             when 4 then 'Pas venu'";
                selectQuery += "             else 'Pas de prochain RDV'";
                selectQuery += "         end";
                selectQuery += " end) as MOTIF,";
                selectQuery += " filteredRDVs.DateRDV  as AREcontactERDEPUISLE,";
                selectQuery += " base_recontact.numtentative,";
                selectQuery += " base_recontact.MOYEN,";
                selectQuery += " base_recontact.MOYENPROCHAINETENTATIVE,";

                selectQuery += " base_recontact.id_usertentative,";
                selectQuery += " coalesce(base_recontact.datetentative,filteredRDVs.DateRDV) as DATETENTATIVE,";
                selectQuery += " 'Y',";
                selectQuery += " coalesce(base_recontact.dateprochainetentative,filteredRDVs.DateRDV+5) as DATEPROCHAINETENTATIVE";
                selectQuery += " from rendez_vous";
                //selectQuery += " left join rendez_vous_anu on rendez_vous_anu.id_rdv=rendez_vous.id_rdv";
                selectQuery += " inner join (";
                selectQuery += "        select rendez_vous.id_personne,max(rendez_vous.rdv_date) as DateRDV";
                selectQuery += "        from rendez_vous";
                selectQuery += "        where rendez_vous.id_personne = @Id";
                selectQuery += "        group by id_personne   having max(rendez_vous.rdv_date)<current_date   order by 2";
                selectQuery += "     ) filteredRDVs on rendez_vous.id_personne=   filteredRDVs.id_personne and  filteredRDVs.DateRDV=rendez_vous.rdv_date";
                selectQuery += "     left outer join base_recontact on base_recontact.id_patient=rendez_vous.id_personne and base_recontact.latest_flag='Y'";
                selectQuery += "     order by base_recontact.datetentative desc ";



                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@Id", IdPatient);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];

                return (dt.Rows[0]);

            }
            catch (System.IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
               connection.Close(); ;

            }

        }

        public static DataTable getAllRecontacts(int IdPatient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {

                string selectQuery = @"select id, 
                                           id_patient, 
                                           motif, 
                                           arecontacterdepuisle, 
                                           numtentative, 
                                           id_usertentative, 
                                           datetentative, 
                                           latest_flag, 
                                           dateprochainetentative, 
                                           creator, 
                                           code, 
                                           moyen,
                                           MOYENPROCHAINETENTATIVE
                                    from base_recontact";
                selectQuery += " where base_recontact.id_patient = @Id ";
                selectQuery += " and base_recontact.numtentative > 0";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@Id", IdPatient);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];

                return dt;

            }
            catch (System.IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
               connection.Close(); ;

            }

        }
        public static void updateRecontact(Recontact rec)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "update base_recontact set LATEST_FLAG='N' where ID_PATIENT=@ID_PATIENT and LATEST_FLAG='Y' ";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@ID_PATIENT", rec.IdPatient);

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
                connection.Close(); ;

            }



        }
        public static void AddRecontact(Recontact rec)
        {
            updateRecontact(rec);

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();

            try
            {
            
              string   selectQuery = "select max(base_recontact.id)+1 as id from base_recontact";
              MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];


                rec.Id = dt.Rows[0]["id"] is DBNull ? 1 : Convert.ToInt32(dt.Rows[0]["id"]);

                selectQuery = "insert into base_recontact (id, ";
                selectQuery += " id_patient, ";
                selectQuery += " motif, ";
                selectQuery += " arecontacterdepuisle, ";
                selectQuery += " numtentative, ";
                selectQuery += " id_usertentative,";
                selectQuery += " datetentative, ";
                selectQuery += " latest_flag, ";
                selectQuery += " dateprochainetentative, ";
                selectQuery += " creator, ";
                selectQuery += " moyen, ";
                selectQuery += " MOYENPROCHAINETENTATIVE)";
                selectQuery += " values (@id, ";
                selectQuery += " @id_patient, ";
                selectQuery += " @motif, ";
                selectQuery += " @arecontacterdepuisle, ";
                selectQuery += " @numtentative, ";
                selectQuery += " @id_usertentative,";
                selectQuery += " @datetentative, ";
                selectQuery += " 'Y', ";
                selectQuery += " @dateprochainetentative, ";
                selectQuery += " @creator, ";
                selectQuery += " @moyen, ";
                selectQuery += " @MOYENPROCHAINETENTATIVE)";

                command.CommandText = selectQuery;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", rec.Id);
                command.Parameters.AddWithValue("@id_patient", rec.IdPatient);
                command.Parameters.AddWithValue("@motif", rec.Motif);
                command.Parameters.AddWithValue("@arecontacterdepuisle", rec.ARecontacterDepuisLe);

                if (rec.NumTentative <= 0)
                {
                    command.Parameters.AddWithValue("@numtentative", DBNull.Value);
                    command.Parameters.AddWithValue("@id_usertentative", DBNull.Value);
                    command.Parameters.AddWithValue("@datetentative", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@numtentative", rec.NumTentative);
                    command.Parameters.AddWithValue("@id_usertentative", rec.IdUserTentative);
                    command.Parameters.AddWithValue("@datetentative", rec.DateTentative == null ? DBNull.Value : (object)rec.DateTentative.Value);
                }
                command.Parameters.AddWithValue("@dateprochainetentative", rec.DateProchaineTentative == null ? DBNull.Value : (object)rec.DateProchaineTentative.Value);
                command.Parameters.AddWithValue("@creator", rec.Creator);
                command.Parameters.AddWithValue("@moyen", rec.moyen);
                command.Parameters.AddWithValue("@MOYENPROCHAINETENTATIVE", rec.moyenProchaineTentative);



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
               connection.Close(); ;

            }



        }


        public static void ValidateRecontacts(int IdPatient)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "";
                MySqlCommand command = new MySqlCommand("", connection, transaction);
                command.CommandType = CommandType.Text;

                selectQuery = "update base_recontact  set";
                selectQuery += " LATEST_FLAG= 'C'";
                selectQuery += " where ID_PATIENT= @ID_PATIENT";

                command.CommandText = selectQuery;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@ID_PATIENT", IdPatient);


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
               connection.Close(); ;

            }



        }


        public static DataTable getRecontactLib()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {

                string selectQuery = "select CODE_RECO,LIB_RECO from REcontact order by CODE_RECO";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);

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
               connection.Close(); ;

            }
        }



    }
}
