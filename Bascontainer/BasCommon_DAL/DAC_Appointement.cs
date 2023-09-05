using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using BasCommon_BO;
using MySql.Data.MySqlClient;
using BasCommon_DAL;
namespace BasCommon_DAL
{
    public static partial class DAC
    {

        public static void SetAAvancerFlagOnly(RHAppointment appointment)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "update rendez_vous";
                    selectQuery += "    set aavancer = @aavancer";
                    selectQuery += "    where (id_rdv = @id_rdv)";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);


                    command.Parameters.AddWithValue("@aavancer", appointment.AAvancer);
                    command.Parameters.AddWithValue("@id_rdv", appointment.Id);

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


        public static void UpdateAppointment(RHAppointment appointment)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "update rendez_vous";
                    selectQuery += "    set id_personne = @id_personne,";
                    selectQuery += "    per_id_personne = @per_id_personne,";
                    selectQuery += "    id_acte = @id_acte,";
                    selectQuery += "    id_fauteuil = @id_fauteuil,";
                    selectQuery += "    rdv_date = @rdv_date,";
                    selectQuery += "    rdv_duree = @rdv_duree,";
                    selectQuery += "    rdv_comm = @rdv_comm,";
                    selectQuery += "    aavancer = @aavancer,";
                    selectQuery += "    rdv_arrivee = @heure_salleattente,";

                    selectQuery += "    heure_fauteuil = @heure_fauteuil,";
                    selectQuery += "    heure_salleattente = @heure_salleattente,";
                    selectQuery += "    heure_secretariat = @heure_secretariat,";
                    selectQuery += "    heure_sorti = @heure_sorti,";
                    selectQuery += "    localisation = @localisation,";
                    selectQuery += "    rdv_statut = @rdv_statut,";
                    selectQuery += "    id_next_acte = @id_next_acte,";
                    if (appointment.FauteuilReel != null) selectQuery += "    FAUT_UTILISE = @FAUT_UTILISE,";


                    selectQuery += "    isexported = @isexported ";
                    selectQuery += "    where (id_rdv = @id_rdv)";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                    if (appointment.patient != null)
                        command.Parameters.AddWithValue("@id_personne", appointment.patient.Id);
                    else
                        command.Parameters.AddWithValue("@id_personne", -1);

                    command.Parameters.AddWithValue("@per_id_personne", appointment.perIdPersonne);
                    command.Parameters.AddWithValue("@id_acte", appointment.acte.id_acte);
                    command.Parameters.AddWithValue("@aavancer", appointment.AAvancer);



                    command.Parameters.AddWithValue("@id_fauteuil", ((Fauteuil)appointment.Resource).Id);
                    command.Parameters.AddWithValue("@rdv_date", appointment.StartDate);
                    command.Parameters.AddWithValue("@rdv_duree", appointment.EndDate.Subtract(appointment.StartDate).TotalMinutes);
                    command.Parameters.AddWithValue("@rdv_arrivee", appointment.DateArrive == null ? DBNull.Value : (object)appointment.DateArrive.Value);
                    if (appointment.FauteuilReel != null) command.Parameters.AddWithValue("@FAUT_UTILISE", appointment.FauteuilReel.Id);



                    command.Parameters.AddWithValue("@heure_fauteuil", appointment.DateFauteuil == null ? DBNull.Value : (object)appointment.DateFauteuil.Value);
                    command.Parameters.AddWithValue("@heure_salleattente", appointment.DateArrive == null ? DBNull.Value : (object)appointment.DateArrive.Value);
                    command.Parameters.AddWithValue("@heure_arrive", appointment.DateArrive == null ? DBNull.Value : (object)appointment.DateArrive.Value);
                    command.Parameters.AddWithValue("@heure_secretariat", appointment.DateSecretariat == null ? DBNull.Value : (object)appointment.DateSecretariat.Value);
                    command.Parameters.AddWithValue("@heure_sorti", appointment.DateSortie == null ? DBNull.Value : (object)appointment.DateSortie.Value);
                    command.Parameters.AddWithValue("@localisation", (int)appointment.Localisation);
                    command.Parameters.AddWithValue("@rdv_statut", (int)appointment.Status);
                    command.Parameters.AddWithValue("@id_next_acte", (int)appointment.idNextact);

                   command.Parameters.AddWithValue("@rdv_comm", appointment.Comment);
                    command.Parameters.AddWithValue("@isexported", "N");                    
                    command.Parameters.AddWithValue("@id_rdv", appointment.Id);

                    command.ExecuteNonQuery();

                    selectQuery = "update base_comm set id_acte = @id_acte";
                    selectQuery += " where id = @idcommclinique";
                   // command.Parameters.AddWithValue("@id_acte", appointment.acte.id_acte);
                    command.Parameters.AddWithValue("@idcommclinique", appointment.IdCommClinique);

                    command.CommandText = selectQuery;

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

        public static void UpdateHeureArriveAppointment(RHAppointment appointment)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "update rendez_vous";
                    selectQuery += "    set rdv_arrivee = @heure_salleattente,";
                    selectQuery += "    heure_salleattente = @heure_salleattente,";
                    selectQuery += "    localisation = @localisation,";
                    selectQuery += "    rdv_statut = @rdv_statut ";
                    selectQuery += "    where (id_rdv = @id_rdv)";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                    command.Parameters.AddWithValue("@rdv_statut", (int)appointment.Status);
                    command.Parameters.AddWithValue("@localisation", (int)appointment.Localisation);
                    command.Parameters.AddWithValue("@heure_salleattente", appointment.DateArrive == null ? DBNull.Value : (object)appointment.DateArrive.Value);
                    command.Parameters.AddWithValue("@id_rdv", appointment.Id);

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

        public static void UpdateHeureFauteuilAppointment(RHAppointment appointment)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "update rendez_vous";
                    selectQuery += "    set heure_fauteuil = @heure_fauteuil,";
                    selectQuery += "    localisation = @localisation,";
                    selectQuery += "    rdv_statut = @rdv_statut";
                    if (appointment.FauteuilReel != null) selectQuery += "    ,FAUT_UTILISE = @FAUT_UTILISE ";
                    selectQuery += "    where (id_rdv = @id_rdv)";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                    command.Parameters.AddWithValue("@rdv_statut", (int)appointment.Status);
                    command.Parameters.AddWithValue("@localisation", (int)appointment.Localisation);
                    command.Parameters.AddWithValue("@heure_fauteuil", appointment.DateFauteuil == null ? DBNull.Value : (object)appointment.DateFauteuil.Value);
                    command.Parameters.AddWithValue("@id_rdv", appointment.Id);
                    if (appointment.FauteuilReel != null) command.Parameters.AddWithValue("@FAUT_UTILISE", appointment.FauteuilReel.Id);

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

        public static void UpdateHeureSecretariatAppointment(RHAppointment appointment)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "update rendez_vous";
                    selectQuery += "    set heure_secretariat = @heure_secretariat,";
                    selectQuery += "    localisation = @localisation,";
                    selectQuery += "    rdv_statut = @rdv_statut";
                    selectQuery += "    where (id_rdv = @id_rdv)";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                    command.Parameters.AddWithValue("@rdv_statut", (int)appointment.Status);
                    command.Parameters.AddWithValue("@localisation", (int)appointment.Localisation);
                    command.Parameters.AddWithValue("@heure_secretariat", appointment.DateSecretariat == null ? DBNull.Value : (object)appointment.DateSecretariat.Value);
                    command.Parameters.AddWithValue("@id_rdv", appointment.Id);

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

        public static void UpdateHeureSortieAppointment(RHAppointment appointment)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "update rendez_vous";
                    selectQuery += "    set heure_sorti = @heure_sorti,";
                    selectQuery += "    localisation = @localisation,";
                    selectQuery += "    rdv_statut = @rdv_statut";
                    selectQuery += "    where (id_rdv = @id_rdv)";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                    command.Parameters.AddWithValue("@rdv_statut", (int)appointment.Status);
                    command.Parameters.AddWithValue("@localisation", (int)appointment.Localisation);
                    command.Parameters.AddWithValue("@heure_sorti", appointment.DateSortie == null ? DBNull.Value : (object)appointment.DateSortie.Value);
                    command.Parameters.AddWithValue("@id_rdv", appointment.Id);

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

        public static void InsertAnnulationTrace(RHAppointment appointment, int Annulation)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {



                    string selectQuery = "insert into rendez_vous_anu (id_rdv, ";
                    selectQuery += "                               id_personne, ";
                    selectQuery += "                               per_id_personne, ";
                    selectQuery += "                               id_acte, ";
                    selectQuery += "                               id_fauteuil, ";
                    selectQuery += "                               rdv_date, ";
                    selectQuery += "                               rdv_duree, ";
                    selectQuery += "                               rdv_statut, ";
                    selectQuery += "                               rdv_arrivee, ";
                    selectQuery += "                               rdv_quand) ";
                    selectQuery += "values (@id_rdv, ";
                    selectQuery += "        @id_personne, ";
                    selectQuery += "        @per_id_personne, ";
                    selectQuery += "        @id_acte, ";
                    selectQuery += "        @id_fauteuil, ";
                    selectQuery += "        @rdv_date, ";
                    selectQuery += "        @rdv_duree, ";
                    selectQuery += "        @rdv_statut, ";
                    selectQuery += "        @rdv_arrivee, ";
                    selectQuery += "        @rdv_quand)";



                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@id_rdv", appointment.Id);
                    if (appointment.patient == null)
                        command.Parameters.AddWithValue("@id_personne", -1);
                    else
                        command.Parameters.AddWithValue("@id_personne", appointment.patient.Id);
                    command.Parameters.AddWithValue("@per_id_personne", Annulation);
                    command.Parameters.AddWithValue("@id_acte", appointment.acte.id_acte);
                    command.Parameters.AddWithValue("@id_fauteuil", ((Fauteuil)appointment.Resource).Id);
                    command.Parameters.AddWithValue("@rdv_date", appointment.StartDate);
                    command.Parameters.AddWithValue("@rdv_duree", appointment.EndDate.Subtract(appointment.StartDate).Minutes);
                    command.Parameters.AddWithValue("@rdv_statut", (int)appointment.Status);
                    command.Parameters.AddWithValue("@rdv_arrivee", DateTime.Now);
                    command.Parameters.AddWithValue("@rdv_quand", DBNull.Value);


                    command.ExecuteNonQuery();
                    transaction.Commit();



                }
                catch (System.SystemException)
                {
                    transaction.Rollback();
                    //Le RDV à deja été annulé
                }
                finally
                {
                   connection.Close();

                }
            }
        }

        public static DataTable getCurrentAppointments()
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
              //  MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "select id_rdv, ";
                    selectQuery += "       id_personne, ";
                    selectQuery += "       per_id_personne, ";
                    selectQuery += "       id_acte, ";
                    selectQuery += "       id_fauteuil, ";
                    selectQuery += "       rdv_date, ";
                    selectQuery += "       rdv_duree, ";
                    selectQuery += "       rdv_statut, ";
                    selectQuery += "       rdv_arrivee, ";
                    selectQuery += "       rdv_comm, ";
                    selectQuery += "       AAvancer, ";
                    selectQuery += "       rdv_quand, ";
                    selectQuery += "       lastmodif, ";
                    selectQuery += "       heure_fauteuil, ";
                    selectQuery += "       heure_salleattente, ";
                    selectQuery += "       heure_secretariat, ";
                    selectQuery += "       heure_sorti, ";
                    selectQuery += "       isexported, ";
                    selectQuery += "       faut_utilise, ";
                    selectQuery += "       localisation, ";
                    selectQuery += "       id_next_acte,";
                    selectQuery += "       PRATICIEN_RESP,";
                    selectQuery += "       PRATICIEN_UNIQUE,ASSISTANTE_UNIQUE,";
                    selectQuery += "       ID_COMMCLINIQUE";
                    selectQuery += " from rendez_vous";
                    selectQuery += " left outer join basediag_infocomplementaire on basediag_infocomplementaire.IDPATIENT = id_personne ";

                    selectQuery += " where rdv_date between current_date and (current_date+1)";
                    selectQuery += " and ((rdv_arrivee>current_date) and (rdv_arrivee is not null))";
                    selectQuery += " and ((heure_sorti<current_date) or (heure_sorti is null))";
                    selectQuery += " order by heure_salleattente";


                    MySqlCommand command = new MySqlCommand(selectQuery, connection);

                    DataSet ds = new DataSet();
                    MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                    adapt.Fill(ds);
                   // transaction.Commit();

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
        


        public static DataTable getCurrentAppointments(int IdPatient)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
               // MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "select id_rdv, ";
                    selectQuery += "       id_personne, ";
                    selectQuery += "       per_id_personne, ";
                    selectQuery += "       id_acte, ";
                    selectQuery += "       id_fauteuil, ";
                    selectQuery += "       rdv_date, ";
                    selectQuery += "       rdv_duree, ";
                    selectQuery += "       rdv_statut, ";
                    selectQuery += "       rdv_arrivee, ";
                    selectQuery += "       rdv_comm, ";
                    selectQuery += "       AAvancer, ";
                    selectQuery += "       rdv_quand, ";
                    selectQuery += "       lastmodif, ";
                    selectQuery += "       heure_fauteuil, ";
                    selectQuery += "       heure_salleattente, ";
                    selectQuery += "       heure_secretariat, ";
                    selectQuery += "       heure_sorti, ";
                    selectQuery += "       isexported, ";
                    selectQuery += "       faut_utilise, ";
                    selectQuery += "       localisation, ";
                    selectQuery += "       id_next_acte,";
                    selectQuery += "       PRATICIEN_RESP,";
                    selectQuery += "       PRATICIEN_UNIQUE,ASSISTANTE_UNIQUE,";
                    selectQuery += "       ID_COMMCLINIQUE ";
                    selectQuery += " from rendez_vous";
                    selectQuery += " left outer join basediag_infocomplementaire on basediag_infocomplementaire.IDPATIENT = id_personne ";

                    selectQuery += " where rdv_date between current_date and (current_date+1)";
                    selectQuery += " and id_personne=@id_personne";


                    MySqlCommand command = new MySqlCommand(selectQuery, connection);
                    command.Parameters.AddWithValue("@id_personne", IdPatient);

                    DataSet ds = new DataSet();
                    MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                    adapt.Fill(ds);
                   // transaction.Commit();

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

        
        public static DataTable getRDVTrace(RHAppointment app, List<Acte> p_Actes)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
               // MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "select rendez_vous_trace.id_rdv, ";
                    selectQuery += "       rendez_vous_trace.id_personne, ";
                    selectQuery += "       rendez_vous_trace.per_id_personne, ";
                    selectQuery += "       rendez_vous_trace.id_acte, ";
                    selectQuery += "       rendez_vous_trace.id_fauteuil, ";
                    selectQuery += "       rendez_vous_trace.rdv_date, ";
                    selectQuery += "       rendez_vous_trace.rdv_duree, ";
                    selectQuery += "      rendez_vous_trace.rdv_statut, ";
                    selectQuery += "      rendez_vous_trace.rdv_arrivee, ";
                    selectQuery += "      rendez_vous_trace.rdv_comm, ";
                    selectQuery += "      rendez_vous_trace.rdv_quand, ";
                    selectQuery += "      rendez_vous_trace.heure_fauteuil, ";
                    selectQuery += "       rendez_vous_trace.heure_salleattente, ";
                    selectQuery += "      rendez_vous_trace.heure_secretariat, ";
                    selectQuery += "      rendez_vous_trace.heure_sorti, ";
                    selectQuery += "      rendez_vous_trace.faut_utilise, ";
                    selectQuery += "       rendez_vous_trace.localisation, ";
                    selectQuery += "       rendez_vous_trace.id_utilisateur,";
                    selectQuery += "       rendez_vous_trace.trace_date, ";
                    selectQuery += "        rendez_vous_trace.trace_comment,";
                    selectQuery += "        trim(p.per_nom)||' '||trim(p.per_prenom) NOMPATIENT";
                    selectQuery += " from rendez_vous_trace";
                    selectQuery += " inner join personne p on rendez_vous_trace.id_personne=p.id_personne";
                    selectQuery += " where rendez_vous_trace.id_rdv = @ID";
                    selectQuery += " order by trace_date";
                    MySqlCommand command = new MySqlCommand(selectQuery, connection);

                    command.Parameters.AddWithValue("@ID", app.Id);

                    DataSet ds = new DataSet();
                    MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                    adapt.Fill(ds);
                   // transaction.Commit();


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

        public static void InsertRDVTrace(string comments, RHAppointment appointment, Utilisateur CreatedBy)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;
               
                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "insert into rendez_vous_trace (id_rdv, ";
                    selectQuery += "                               id_personne, ";
                    selectQuery += "                               per_id_personne, ";
                    selectQuery += "                               id_acte, ";
                    selectQuery += "                               id_fauteuil, ";
                    selectQuery += "                               rdv_date, ";
                    selectQuery += "                               rdv_duree, ";
                    selectQuery += "                               rdv_statut, ";
                    selectQuery += "                               rdv_arrivee, ";
                    selectQuery += "                               rdv_comm, ";
                    selectQuery += "                               heure_fauteuil, ";
                    selectQuery += "                               heure_salleattente, ";
                    selectQuery += "                               heure_secretariat, ";
                    selectQuery += "                               heure_sorti, ";
                    selectQuery += "                               faut_utilise, ";
                    selectQuery += "                               localisation, ";
                    selectQuery += "                               id_utilisateur, ";
                    selectQuery += "                               trace_date, ";
                    selectQuery += "                               trace_comment)";
                    selectQuery += "select id_rdv, ";
                    selectQuery += "        id_personne, ";
                    selectQuery += "        per_id_personne, ";
                    selectQuery += "        id_acte, ";
                    selectQuery += "        id_fauteuil, ";
                    selectQuery += "        rdv_date, ";
                    selectQuery += "        rdv_duree, ";
                    selectQuery += "        rdv_statut, ";
                    selectQuery += "        rdv_arrivee, ";
                    selectQuery += "        rdv_comm, ";
                    selectQuery += "        heure_fauteuil, ";
                    selectQuery += "        heure_salleattente, ";
                    selectQuery += "        heure_secretariat, ";
                    selectQuery += "        heure_sorti, ";
                    selectQuery += "        faut_utilise, ";
                    selectQuery += "        localisation, ";
                    selectQuery += "        @id_utilisateur, ";
                    selectQuery += "        @trace_date, ";
                    selectQuery += "        @trace_comment ";
                    selectQuery += "        from rendez_vous ";
                    selectQuery += "        where id_rdv=@id_rdv";



                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@id_rdv", appointment.Id);
                    command.Parameters.AddWithValue("@id_utilisateur", CreatedBy == null ? 0 : CreatedBy.Id);
                    command.Parameters.AddWithValue("@trace_date", DateTime.Now);
                    command.Parameters.AddWithValue("@trace_comment", comments);

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

        public static void InsertAppointment(RHAppointment appointment)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {

                    string selectQuery = "select MAX(id_rdv)+1 as NEWID from rendez_vous";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.CommandType = CommandType.Text;
                    try
                    {

                        appointment.Id = Convert.ToInt32(command.ExecuteScalar());
                    }
                    catch (System.Exception)
                    {
                        appointment.Id = 1;
                    }

                    selectQuery = "insert into rendez_vous ( id_rdv, id_personne, per_id_personne, id_acte, id_fauteuil, rdv_date, rdv_duree, rdv_statut, rdv_comm, isexported,aavancer,ID_COMMCLINIQUE)";
                    selectQuery += "values ( @id_rdv, @id_personne, @per_id_personne, @id_acte, @id_fauteuil, @rdv_date, @rdv_duree, @rdv_statut, @rdv_comm, @isexported,@aavancer,@ID_COMMCLINIQUE)";

                    command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@id_rdv", appointment.Id);
                    if (appointment.patient != null)
                        command.Parameters.AddWithValue("@id_personne", appointment.patient.Id);
                    else
                        command.Parameters.AddWithValue("@id_personne", appointment.IdPatient);
                    command.Parameters.AddWithValue("@per_id_personne",appointment.perIdPersonne);
                    command.Parameters.AddWithValue("@id_acte", appointment.acte.id_acte);
                    command.Parameters.AddWithValue("@id_fauteuil", ((Fauteuil)appointment.Resource).Id);
                    command.Parameters.AddWithValue("@rdv_date", appointment.StartDate);
                  
                    command.Parameters.AddWithValue("@rdv_duree", appointment.EndDate.Subtract(appointment.StartDate).TotalMinutes);
                      

                    command.Parameters.AddWithValue("@rdv_statut", 0);
                    command.Parameters.AddWithValue("@rdv_comm", appointment.Comment);
                    command.Parameters.AddWithValue("@isexported", "N");
                  //  command.Parameters.AddWithValue("@id_rdv", appointment.Id);
                    command.Parameters.AddWithValue("@aavancer", appointment.AAvancer);
                    command.Parameters.AddWithValue("@ID_COMMCLINIQUE", appointment.IdCommClinique);

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

        public static int CheckAppointmentsupp(RHAppointment appointement)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
               // MySqlTransaction transaction = connection.BeginTransaction();
                try
                {

                    /*
                     select count(1) from rendez_vous r
     where
     ((rdv_date between '04/29/2010' and '04/30/2010') or
      (DATEADD(r.rdv_duree minute to rdv_date) between '04/29/2010' and '04/30/2010') or
       ('04/29/2010' between rdv_date and DATEADD(r.rdv_duree minute to rdv_date))
      ) and r.id_fauteuil = 7 
                      */
                    string selectQuery = "select count(1) from rendez_vous r";
                    selectQuery += " where ";
                    selectQuery += " ((rdv_date between @startDate and @EndDate) or ";
                    selectQuery += " (DATEADD(r.rdv_duree minute to rdv_date) between @startDate and @EndDate) or ";
                    selectQuery += " (@startDate between rdv_date and DATEADD(r.rdv_duree minute to rdv_date)) ";
                    selectQuery += " ) and r.id_fauteuil = @IdFauteuil and r.Id_rdv<>@IdRdv  and r.Id_acte>-1";


                    MySqlCommand command = new MySqlCommand(selectQuery, connection);

                    command.Parameters.AddWithValue("@IdFauteuil", ((Fauteuil)appointement.Resource).Id);
                    command.Parameters.AddWithValue("@EndDate", appointement.EndDate.AddSeconds(-1));
                    command.Parameters.AddWithValue("@startDate", appointement.StartDate.AddSeconds(1));
                    command.Parameters.AddWithValue("@IdRdv", appointement.Id);

                    int NbRDVs = (int)command.ExecuteScalar();

                    return NbRDVs;

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

        public static DataRow getAppointment(int IdRdv)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
              //  MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    #region Old Wrong Query
                    //string selectQuery = "select TEST_BP,ID_NEXT_ACTE, id_rdv, p.id_personne,rdv_date, rdv_duree, rdv_statut, rdv_arrivee, rdv_comm, rdv_quand, heure_fauteuil, heure_salleattente, heure_secretariat, heure_sorti, isexported, faut_utilise, localisation,";
                    //selectQuery += "       PRATICIEN_RESP,";
                    //selectQuery += "       PRATICIEN_UNIQUE,ASSISTANTE_UNIQUE,";
                    //selectQuery += "       ID_COMMCLINIQUE,";
                    //selectQuery += "      AAvancer, ";
                    //selectQuery += "       TRIM(p.PER_NOM)||' '||TRIM(p.PER_PRENOM) PATNAME,";

                    //selectQuery += " id_adresse, id_util, id_caisse, adr_id_adresse, per_nom, per_nomjf, per_prenom, per_genre, per_secu, per_type, per_telprinc, per_teltrav1, per_teltrav2, per_telecopie, per_email, per_reception, per_notes, per_poste, p.pcom, per_adr1, per_adr2, per_ville, per_cpostal, per_adr1_prof, per_adr2_prof, per_cpostal_prof, per_ville_prof, profession, mutuelle, per_datnaiss, tuvous, poid, email2, gsm, icq, im1, im2, telsup0, telsup3, telsup4, telsup5, telsup6, telsup8, telsup10, telsup11, telsup12, telsup13, telsup14, telsup15, telsup16, telsup17, telsup18, indicetel1, indicetel2, indicetel3, indicetel4, email3, indiceemail, indiceadr, pays_dom, pays_trav, pers_titre, pers_siteweb, per_ville_naissance, per_pays_naissance, per_langue_parle, per_population_ref, nom_rep_image, oi_login, oi_mdp, oi_profil, oi_autorisation, categories, pref_com,";
                    //selectQuery += " f.id_fauteuil, faut_libelle,FAUT_UTILISE,";
                    //selectQuery += " a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono,";
                    //selectQuery += " PAT_NUMDOSSIER,";
                    //// selectQuery += " (select first 1 rdv_date from rendez_vous r3 where r3.rdv_date<r.rdv_date and r.id_personne=r3.id_personne order by r3.rdv_date desc) as PreviousRDV,";
                    //selectQuery += " (select first 1 rdv_date from rendez_vous r2 where r2.rdv_date>r.rdv_date and r.id_personne=r2.id_personne order by r2.rdv_date asc) as NextRDV";
                    //selectQuery += " from rendez_vous r";
                    //selectQuery += " left outer join personne p on r.id_personne=p.id_personne";
                    //selectQuery += " left outer join patient pa on p.id_personne=pa.id_personne";
                    //selectQuery += " left outer join basediag_infocomplementaire on basediag_infocomplementaire.IDPATIENT = pa.id_personne ";
                    //selectQuery += " inner join fauteuil f on r.id_fauteuil=f.id_fauteuil";
                    //selectQuery += " inner join actes a on r.id_acte=a.id_acte";
                    //selectQuery += " where r.id_rdv = @Idrdv";
                    //selectQuery += " order by rdv_date,r.id_fauteuil";
                    #endregion

                    string mySqlQuery = @"select ID_COMMCLINIQUE, f.id_fauteuil, ID_NEXT_ACTE, a.id_acte, id_rdv, p.id_personne ,
                                          rdv_date, rdv_duree, RDV_STATUT, RDV_ARRIVEE, rdv_comm, rdv_quand, HEURE_FAUTEUIL,
                                          heure_salleattente, HEURE_SECRETARIAT, HEURE_SORTI, FAUT_UTILISE, LOCALISATION, aavancer,
                                          concat(TRIM(p.PER_NOM),' ',TRIM(p.PER_PRENOM)) PATNAME,  
                                          (select  rdv_date from rendez_vous r2 where r2.rdv_date>r.rdv_date and r.id_personne=r2.id_personne order by r2.rdv_date asc limit 1) as NextRDV 
                                          from rendez_vous r left outer join personne p on r.id_personne=p.id_personne left outer join patient 
                                          pa on p.id_personne=pa.id_personne left outer join basediag_infocomplementaire on basediag_infocomplementaire.IDPATIENT
                                          = pa.id_personne  inner join fauteuil f on r.id_fauteuil=f.id_fauteuil
                                          inner join actes a on r.id_acte=a.id_acte where r.id_rdv = 4504 order by rdv_date,r.id_fauteuil";

                    MySqlCommand command = new MySqlCommand(mySqlQuery, connection);

                    command.Parameters.AddWithValue("@Idrdv", IdRdv);


                    DataSet ds = new DataSet();
                    MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                    adapt.Fill(ds);
                   // transaction.Commit();

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


        }
        
        public static DataRow VerifNewValidAppointment(DateTime StartDate,DateTime endDate,int faut)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
                try
                {
                    string selectQuery = "select  a.* ,bdi.* from rendez_vous a  "
                    + " left outer join basediag_infocomplementaire bdi on bdi.IDPATIENT = a.ID_PERSONNE "
                    + " where  a.ID_FAUTEUIL = @faut and a.RDV_DATE between @RDVDATE and @RDVEND "
                    + " or  dateadd(20 minute to a.RDV_DATE) between @RDVDATE and @RDVEND "
                    + " order by a.RDV_DATE";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection);
                    command.Parameters.AddWithValue("@faut", faut);
                    command.Parameters.AddWithValue("@RDVDATE", StartDate);
                    command.Parameters.AddWithValue("@RDVEND", endDate.AddMinutes(-2));
                    DataSet ds = new DataSet();
                    MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                    adapt.Fill(ds);

                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count == 0) return null;

                    return dt.Rows[dt.Rows.Count -1];

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
       
        public static bool VerifNewValidAppointment(RHAppointment app)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
                try
                {
                    string selectQuery = "select  * from rendez_vous a where a.ID_RDV <> @idRDV and a.ID_FAUTEUIL = @faut and a.RDV_DATE between @RDVDATE and @RDVEND";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection);
                    command.Parameters.AddWithValue("@idRDV", app.Id);
                    command.Parameters.AddWithValue("@faut", ((Fauteuil)app.Resource).Id);
                    command.Parameters.AddWithValue("@RDVDATE", app.StartDate.AddMilliseconds(-1));
                    command.Parameters.AddWithValue("@RDVEND", app.EndDate.AddMilliseconds(-1));
                    DataSet ds = new DataSet();
                    MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                    adapt.Fill(ds);

                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                        return false;
                    else return true;

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
       
        public static DataTable getAppointments(DateTime p_date)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
               // MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "select pa.TEST_BP,ID_NEXT_ACTE, id_rdv, p.id_personne,rdv_date, rdv_duree, rdv_statut, rdv_arrivee, rdv_comm, rdv_quand, heure_fauteuil, heure_salleattente, heure_secretariat, heure_sorti, isexported, faut_utilise, localisation,";
                    selectQuery += "       PRATICIEN_RESP,";
                    selectQuery += "       PRATICIEN_UNIQUE,ASSISTANTE_UNIQUE,";
                    selectQuery += "       ID_COMMCLINIQUE,";
                    selectQuery += "       AAvancer,";

                    selectQuery += "       TRIM(p.PER_NOM)||' '||TRIM(p.PER_PRENOM) PATNAME,";

                    selectQuery += " id_adresse, id_util, id_caisse, adr_id_adresse, per_nom, per_nomjf, per_prenom, per_genre, per_secu, per_type, per_telprinc, per_teltrav1, per_teltrav2, per_telecopie, per_email, per_reception, per_notes, per_poste, p.pcom, per_adr1, per_adr2, per_ville, per_cpostal, per_adr1_prof, per_adr2_prof, per_cpostal_prof, per_ville_prof, profession, mutuelle, per_datnaiss, tuvous, poid, email2, gsm, icq, im1, im2, telsup0, telsup3, telsup4, telsup5, telsup6, telsup8, telsup10, telsup11, telsup12, telsup13, telsup14, telsup15, telsup16, telsup17, telsup18, indicetel1, indicetel2, indicetel3, indicetel4, email3, indiceemail, indiceadr, pays_dom, pays_trav, pers_titre, pers_siteweb, per_ville_naissance, per_pays_naissance, per_langue_parle, per_population_ref, nom_rep_image, oi_login, oi_mdp, oi_profil, oi_autorisation, categories, pref_com,";
                    selectQuery += " f.id_fauteuil, faut_libelle,FAUT_UTILISE,";
                    selectQuery += " a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono,";
                    selectQuery += " PAT_NUMDOSSIER,";
                    // selectQuery += " (select first 1 rdv_date from rendez_vous r3 where r3.rdv_date<r.rdv_date and r.id_personne=r3.id_personne order by r3.rdv_date desc) as PreviousRDV,";
                    selectQuery += " (select first 1 rdv_date from rendez_vous r2 where r2.rdv_date>r.rdv_date and r.id_personne=r2.id_personne order by r2.rdv_date asc) as NextRDV";
                    //selectQuery += " (select first 1 r2.rdv_date from rendez_vous_trace r2 where r2.rdv_date>r.rdv_date and p.id_personne=r2.id_personne  and TRACE_COMMENT = 'Insert'  and   r2.trace_date> r.rdv_date order by r2.rdv_date asc) as NextRDV";
                    selectQuery += " from rendez_vous r";
                    selectQuery += " left outer join personne p on r.id_personne=p.id_personne";
                    selectQuery += " left outer join patient pa on p.id_personne=pa.id_personne";
                    selectQuery += " inner join fauteuil f on r.id_fauteuil=f.id_fauteuil";
                    selectQuery += " inner join actes a on r.id_acte=a.id_acte";
                    selectQuery += " left outer join basediag_infocomplementaire on basediag_infocomplementaire.IDPATIENT = pa.id_personne ";

                    selectQuery += " where rdv_date between @date1 and @date2";
                    selectQuery += " order by rdv_date,r.id_fauteuil";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection);

                    command.Parameters.AddWithValue("@date1", p_date.Date);
                    command.Parameters.AddWithValue("@date2", p_date.Date.AddHours(23).AddMinutes(59));


                    DataSet ds = new DataSet();
                    MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                    adapt.Fill(ds);
                   // transaction.Commit();

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

        public static DataTable getAppointments(DateTime p_dateS, DateTime p_dateE)
        {
            return getAppointments(p_dateS, p_dateE, null);
        }

        public static DataTable getAppointments(DateTime p_dateS, DateTime p_dateE, Fauteuil fauteuil)
        {
            // Done in Java by Wael          
            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
               // MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    #region Old Code
                    //string selectQuery = "select ID_COMMCLINIQUE,id_fauteuil,ID_NEXT_ACTE,id_acte, id_rdv, r.id_personne,rdv_date, rdv_duree, rdv_statut, rdv_arrivee, rdv_comm, rdv_quand, heure_fauteuil, heure_salleattente, heure_secretariat, heure_sorti,  faut_utilise, localisation,";
                    //selectQuery += "      AAvancer, ";
                    //selectQuery += "       TRIM(p.PER_NOM)||' '||TRIM(p.PER_PRENOM) PATNAME,";
                    //// selectQuery += " (select first 1 rdv_date from rendez_vous r3 where r3.rdv_date<r.rdv_date and r.id_personne=r3.id_personne order by r3.rdv_date desc) as PreviousRDV,";
                    //selectQuery += " (select first 1 rdv_date from rendez_vous r2 where r2.rdv_date>r.rdv_date and r.id_personne=r2.id_personne order by r2.rdv_date asc) as NextRDV";
                    //selectQuery += " from rendez_vous r";
                    //selectQuery += " inner join personne p on p.id_personne=r.id_personne";

                    //selectQuery += " where rdv_date between @date1 and @date2";
                    //if (fauteuil != null)
                    //    selectQuery += " and id_fauteuil=@id_fauteuil";

                    //selectQuery += " order by rdv_date,r.id_fauteuil";

                    //MySqlCommand command = new MySqlCommand(selectQuery, connection);
                    #endregion

                    string mySqlQuery = @"select ID_COMMCLINIQUE,id_fauteuil,ID_NEXT_ACTE,id_acte, id_rdv, r.id_personne,rdv_date, rdv_duree, rdv_statut,
                                        rdv_arrivee, rdv_comm, rdv_quand, heure_fauteuil, heure_salleattente, heure_secretariat, heure_sorti,  faut_utilise, localisation,
                                        AAvancer, concat (TRIM(p.PER_NOM),' ',TRIM(p.PER_PRENOM)) PATNAME,
                                        (select rdv_date from rendez_vous r2 where r2.rdv_date>r.rdv_date and r.id_personne=r2.id_personne order by r2.rdv_date asc limit 1)
                                        as NextRDV from rendez_vous r inner join personne p on p.id_personne=r.id_personne where rdv_date between @date1 and @date2";
                    
                    if (fauteuil != null)                    
                        mySqlQuery += " and id_fauteuil=@id_fauteuil";

                    mySqlQuery += " order by rdv_date,r.id_fauteuil";

                    MySqlCommand command = new MySqlCommand(mySqlQuery, connection);


                    command.Parameters.AddWithValue("@date1", p_dateS.Date);
                    command.Parameters.AddWithValue("@date2", p_dateE.Date.AddHours(23).AddMinutes(59));
                    if (fauteuil != null) 
                        command.Parameters.AddWithValue("@id_fauteuil", fauteuil.Id);


                    DataSet ds = new DataSet();
                    MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                    adapt.Fill(ds);
                    //transaction.Commit();

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


        public static DataTable getAppointments(basePatient p_patient)
        {
            return getAppointments(p_patient.Id);
        }

        public static DataTable getAppointments(int IdPatient)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
                try
                {
                    string selectQuery = "select ID_COMMCLINIQUE,id_fauteuil,ID_NEXT_ACTE,id_acte, id_rdv, r.id_personne,rdv_date, rdv_duree, rdv_statut, rdv_arrivee, rdv_comm, rdv_quand, heure_fauteuil, heure_salleattente, heure_secretariat, heure_sorti,  faut_utilise, localisation";
                    selectQuery += "      AAvancer, ";
                    selectQuery += "       TRIM(p.PER_NOM)||' '||TRIM(p.PER_PRENOM) PATNAME,";
                    selectQuery += " (select rdv_date from rendez_vous r2 where r2.rdv_date>r.rdv_date and r.id_personne=r2.id_personne order by r2.rdv_date asc  LIMIT 1) as NextRDV";
                    selectQuery += " from rendez_vous r";
                    selectQuery += " inner join personne p on p.id_personne=r.id_personne";
                    selectQuery += " where r.id_personne = @Idpat";
                    selectQuery += " order by rdv_date,r.id_fauteuil";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection);

                    command.Parameters.AddWithValue("@Idpat", IdPatient);


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
        }

        public static void DeleteAppointment(RHAppointment appointment)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "delete from rendez_vous";
                    selectQuery += " where id_rdv = @id_rdv";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@id_rdv", appointment.Id);

                    command.ExecuteNonQuery();

                    selectQuery = "update base_comm set ID_RDV = NULL";
                    selectQuery += " where id_rdv = @id_rdv";

                    command.CommandText = selectQuery;

                    command.ExecuteNonQuery();

                    transaction.Commit();



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

        public static void UpdateAbsenceAuto(DateTime dte, bool AllTimes)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                if (connection.State == ConnectionState.Closed) connection.Open();               
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {


                    string selectQuery = "insert into rendez_vous_trace (id_rdv, ";
                    selectQuery += "                               id_personne, ";
                    selectQuery += "                               per_id_personne, ";
                    selectQuery += "                               id_acte, ";
                    selectQuery += "                               id_fauteuil, ";
                    selectQuery += "                               rdv_date, ";
                    selectQuery += "                               rdv_duree, ";
                    selectQuery += "                               rdv_statut, ";
                    selectQuery += "                               rdv_arrivee, ";
                    selectQuery += "                               rdv_comm, ";
                    selectQuery += "                               heure_fauteuil, ";
                    selectQuery += "                               heure_salleattente, ";
                    selectQuery += "                               heure_secretariat, ";
                    selectQuery += "                               heure_sorti, ";
                    selectQuery += "                               faut_utilise, ";
                    selectQuery += "                               localisation, ";
                    selectQuery += "                               id_utilisateur, ";
                    selectQuery += "                               trace_date, ";
                    selectQuery += "                               trace_comment)";
                    selectQuery += "select id_rdv, ";
                    selectQuery += "        id_personne, ";
                    selectQuery += "        per_id_personne, ";
                    selectQuery += "        id_acte, ";
                    selectQuery += "        id_fauteuil, ";
                    selectQuery += "        rdv_date, ";
                    selectQuery += "        rdv_duree, ";
                    selectQuery += "        rdv_statut, ";
                    selectQuery += "        rdv_arrivee, ";
                    selectQuery += "        rdv_comm, ";
                    selectQuery += "        heure_fauteuil, ";
                    selectQuery += "        heure_salleattente, ";
                    selectQuery += "        heure_secretariat, ";
                    selectQuery += "        heure_sorti, ";
                    selectQuery += "        faut_utilise, ";
                    selectQuery += "        localisation, ";
                    selectQuery += "        -1, ";
                    selectQuery += "        @trace_date, ";
                    selectQuery += "        'Update' ";
                    selectQuery += "        from rendez_vous ";
                    selectQuery += "    where";
                    if (!AllTimes) selectQuery += "    (rdv_date > @dteFrom) and (rdv_date < @dteTo)";
                    selectQuery += "    and (rdv_date < @checkDate) and";
                    selectQuery += "    coalesce(localisation,0)=0 and coalesce(rdv_statut,0)=0";



                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@trace_date", DateTime.Now);
                    if (!AllTimes)
                    {
                        command.Parameters.AddWithValue("@dteFrom", dte.Date);
                        command.Parameters.AddWithValue("@dteTo", dte.Date.AddDays(1));
                    }
                    command.Parameters.AddWithValue("@checkDate", DateTime.Now.AddHours(-1));
                    command.ExecuteNonQuery();
                    transaction.Commit();


                    selectQuery = "update rendez_vous";
                    selectQuery += "    set localisation = @localisation,";
                    selectQuery += "    rdv_statut = @rdv_statut ";
                    selectQuery += "    where";
                    if (!AllTimes) selectQuery += "    (rdv_date > @dteFrom) and (rdv_date < @dteTo)";
                    selectQuery += "    and (rdv_date < @checkDate) and";
                    selectQuery += "    coalesce(localisation,0)=0 and coalesce(rdv_statut,0)=0";

                    command.CommandText = selectQuery;

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@localisation", (int)RHAppointment.EnumLocalisation.Aucune);
                    command.Parameters.AddWithValue("@rdv_statut", (int)RHAppointment.EnumStatus.Absent);
                    if (!AllTimes)
                    {
                        command.Parameters.AddWithValue("@dteFrom", dte.Date);
                        command.Parameters.AddWithValue("@dteTo", dte.Date.AddDays(1));
                    }
                    command.Parameters.AddWithValue("@checkDate", DateTime.Now.AddHours(-1));

                    command.ExecuteNonQuery();


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

        public static void UpdateDatePoseLaboRequest(int PatientId)
        {
            lock (lockobj)
            {
                if (connectionBaseProduct == null) getBaseProductConnection();

                if (connectionBaseProduct.State == ConnectionState.Closed) connectionBaseProduct.Open();
                MySqlTransaction transaction = connectionBaseProduct.BeginTransaction();

                try
                {
                    

                    string selectQuery = "update base_labo_suivi set POSE_APP = @POSE_APP";
                    selectQuery += " where base_labo_suivi.PATIENTID =  @IdPatient and RECEPTION_CAB is not null";

                    MySqlCommand command = new MySqlCommand(selectQuery, connectionBaseProduct, transaction);

                    command.Parameters.AddWithValue("@IdPatient", PatientId);
                    command.Parameters.AddWithValue("@POSE_APP", DateTime.Now);

                    Object obj = command.ExecuteNonQuery();

                    transaction.Commit();


                }
                catch (System.SystemException ex)
                {
                    transaction.Rollback(); ;
                }
                finally
                {
                    connectionBaseProduct.Close();

                }
            }

        }
    }
}
