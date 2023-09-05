using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using FirebirdSql.Data.FirebirdClient;
using BasCommon_BO.ElementsEnBouche.BO;

using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using BasCommon_BO;


namespace BASEPractice_DAL
{
    public static class DAC
    {

        private static object lockobj = new object();

        private static string connectionString = "";
        private static FbConnection connection = null;

        #region connection

        private static FbConnection getLocalConnection()
        {
            //    If the connection string is null, use a default.

            if (connectionString == "")
            {
                FbConnectionStringBuilder cs = new FbConnectionStringBuilder();

                cs.DataSource = ConfigurationManager.AppSettings["DataSource"];
                cs.Database = ConfigurationManager.AppSettings["Database"];
                cs.UserID = ConfigurationManager.AppSettings["UserID"];
                cs.Password = ConfigurationManager.AppSettings["Password"];
                cs.Dialect = Convert.ToInt32(ConfigurationManager.AppSettings["Dialect"]);

                connectionString = cs.ToString();
            }

            return new FbConnection(connectionString);
        }

        private static void getConnection()
        {

            connection = getLocalConnection();
        }

        #endregion




        public static DataTable getPatientsEnStatus(StatusCliniqueManuel status, Utilisateur AssResponsable)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectquery = " select  personne.id_personne ,per_nom ,per_prenom";
                selectquery += " from personne";
                selectquery += " inner join  (";
                selectquery += "   select ID_PATIENT as id_personne";
                selectquery += "   FROM base_histo_status";
                selectquery += "   WHERE ((DATEDEBUT is null) or (DATEDEBUT<current_date)) and ((datefin is null) or (datefin>current_date))";
                selectquery += "   AND ID_STATUS=@ID_STATUS";
                selectquery += " ) filteredRDVs on personne.id_personne=   filteredRDVs.id_personne ";
                if (AssResponsable != null) selectquery += " inner join basediag_infocomplementaire on basediag_infocomplementaire.idpatient = personne.id_personne and (basediag_infocomplementaire.assistante_resp =@idass or basediag_infocomplementaire.praticien_resp =@idass)";



                FbCommand command = new FbCommand(selectquery, connection, transaction);
                command.Parameters.AddWithValue("@ID_STATUS", status.Id);
                if (AssResponsable != null)
                    command.Parameters.AddWithValue("@idass", AssResponsable.Id);


                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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

        public static DataTable getPatientEnRecontact(Utilisateur AssResponsable)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectquery = " select BASE_RECONTACT.ID_PATIENT,";
                selectquery += " per_nom,";
                selectquery += " per_prenom,";
                selectquery += " BASE_RECONTACT.MOTIF ,";
                selectquery += " BASE_RECONTACT.ARECONTACTERDEPUISLE,";
                selectquery += " BASE_RECONTACT.DATEPROCHAINETENTATIVE,";
                selectquery += " BASE_RECONTACT.DATETENTATIVE,";
                selectquery += " BASE_RECONTACT.NUMTENTATIVE,";
                selectquery += " BASE_RECONTACT.ID_USERTENTATIVE,";
                selectquery += " patient.TEST_BP,";
                selectquery += " DateRDV";
                selectquery += " from BASE_RECONTACT";
                selectquery += " inner join (";
                selectquery += "   select rendez_vous.id_personne,max(RENDEZ_VOUS.rdv_date) as DateRDV ";
                selectquery += "   from REndez_vous";
                selectquery += "   where RENDEZ_VOUS.rdv_date<current_date";
                selectquery += "   group by id_personne";
                selectquery += "   order by 2  ) filteredRDVs on BASE_RECONTACT.ID_PATIENT=   filteredRDVs.id_personne";

                selectquery += " inner join personne on personne.id_personne = BASE_RECONTACT.ID_PATIENT";
                selectquery += " inner join patient on personne.id_personne = patient.id_personne";
                if (AssResponsable != null) selectquery += " inner join basediag_infocomplementaire on basediag_infocomplementaire.idpatient = personne.id_personne and (basediag_infocomplementaire.assistante_resp =@idass or basediag_infocomplementaire.praticien_resp =@idass)";
                selectquery += " where LATEST_FLAG = 'Y' and (BASE_RECONTACT.DATEPROCHAINETENTATIVE<=current_date or BASE_RECONTACT.DATEPROCHAINETENTATIVE is null)";
                selectquery += " order by BASE_RECONTACT.ARECONTACTERDEPUISLE asc";

                FbCommand command = new FbCommand(selectquery, connection, transaction);
                if (AssResponsable != null)
                    command.Parameters.AddWithValue("@idass", AssResponsable.Id);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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



         public static DataTable getPatientSansProchainRDV()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectquery = " select rendez_vous.id_personne, per_nom,per_prenom,DateRDV";
                selectquery += " from rendez_vous";
                selectquery += " inner join (";
                selectquery += "   select rendez_vous.id_personne,max(RENDEZ_VOUS.rdv_date) as DateRDV from REndez_vous";
                selectquery += "   group by id_personne";
                selectquery += "   having max(RENDEZ_VOUS.rdv_date)<current_date";
                selectquery += "   order by 2  ) filteredRDVs on rendez_vous.id_personne=   filteredRDVs.id_personne and  filteredRDVs.DateRDV=rendez_vous.rdv_date";
                selectquery += " inner join personne on personne.id_personne= filteredRDVs.id_personne";


                FbCommand command = new FbCommand(selectquery, connection, transaction);


                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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


        public static DataTable GetPatientsSoldeNegatifCeJour(Utilisateur AssResponsable)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectquery = "select sum(BASE_ECHEANCE.MONTANT) MONTANT,";
                selectquery += " min(BASE_ECHEANCE.dteecheance) DTEECHEANCE,";
                selectquery += " personne.id_personne IDPATIENT,";
                selectquery += " personne.per_nom NOMPATIENT,";
                selectquery += " personne.per_prenom PRENOMPATIENT";
                selectquery += " from BASE_ECHEANCE";
                selectquery += " inner join BASE_TRAITEMENT on BASE_TRAITEMENT.ID = BASE_ECHEANCE.ID_TRAITEMENT";
                selectquery += " left outer join BASE_RELANCE on BASE_RELANCE.ID_PATIENT = BASE_ECHEANCE.ID_PATIENT and BASE_RELANCE.LATEST_FLAG='Y'";
                selectquery += " inner join Personne on personne.id_personne = base_echeance.id_patient";
                if (AssResponsable != null) selectquery += " inner join basediag_infocomplementaire on basediag_infocomplementaire.idpatient = personne.id_personne and (basediag_infocomplementaire.assistante_resp =@idass or basediag_infocomplementaire.praticien_resp =@idass)";
                selectquery += " inner join rendez_vous on rendez_vous.id_personne=personne.id_personne and rendez_vous.rdv_date between current_date and current_date+1";
                selectquery += " left outer join base_encaissement on BASE_ECHEANCE.id_encaissement = base_encaissement.id";
                selectquery += " where base_encaissement.id is null and BASE_ECHEANCE.dteecheance<=current_date";
                selectquery += " group by personne.id_personne,personne.per_nom,personne.per_prenom";


                FbCommand command = new FbCommand(selectquery, connection, transaction);
                if (AssResponsable != null)
                    command.Parameters.AddWithValue("@idass", AssResponsable.Id);


                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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



        public static DataTable GetPatientsARelancer()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select sum(BASE_ECHEANCE.MONTANT) MONTANT,";
                selectQuery += " min(BASE_ECHEANCE.dteecheance) DTEECHEANCE,";
                selectQuery += " personne.id_personne IDPATIENT,";
                selectQuery += " personne.per_nom NOMPATIENT,";
                selectQuery += " personne.per_prenom PRENOMPATIENT,";
                selectQuery += " REspFi.per_nom NOMRESPFI,";
                selectQuery += " REspFi.per_prenom PRENOMRESPFI,";
                selectQuery += " REspFi.id_personne IDRESPFI,";
                selectQuery += " NIVEAU_RELANCE";

                selectQuery += " from BASE_ECHEANCE";
                selectQuery += " inner join BASE_TRAITEMENT on BASE_TRAITEMENT.ID = BASE_ECHEANCE.ID_TRAITEMENT";
                selectQuery += " left outer join BASE_RELANCE on BASE_RELANCE.ID_PATIENT = BASE_ECHEANCE.ID_PATIENT and BASE_RELANCE.LATEST_FLAG='Y'";
                selectQuery += " inner join Personne on personne.id_personne = base_echeance.id_patient";
                selectQuery += " left outer join lienpers RespFilnk on RespFilnk.id_patient = base_echeance.id_patient and  RespFilnk.relation = 'Rs'";
                selectQuery += " left outer join personne REspFi on RespFi.id_personne = RespFilnk.id_personne";
                selectQuery += " left outer join base_encaissement on BASE_ECHEANCE.id_encaissement = base_encaissement.id";
                selectQuery += " where base_encaissement.id is null and BASE_ECHEANCE.dteecheance<=current_date";
                selectQuery += " group by personne.id_personne,personne.per_nom,personne.per_prenom,REspFi.per_nom,REspFi.per_prenom,REspFi.id_personne,NIVEAU_RELANCE";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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



        /*

        #region patient

        public static DataRow getinfocomplementaire(int Idpat)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "select idpatient, ";

                selectQuery += " assistante_resp, ";
                selectQuery += " SEMESTRESENTAMES, ";
                selectQuery += " PRATICIEN_UNIQUE, ";

                selectQuery += " AMELIORATIONS, ";
                selectQuery += " DEBUTTRAITEMENTENVISAGE, ";

                selectQuery += " praticien_resp ";
                selectQuery += " from basediag_infocomplementaire";
                selectQuery += " where idpatient = @idpatient";


                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@idpatient", Idpat);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

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













        public static DataRow getBeneficiaire(basePatient pat)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "select id_patsv, ";
                selectQuery += "       nom, ";
                selectQuery += "       prenom, ";
                selectQuery += "       date_naissance, ";
                selectQuery += "       qualite, ";
                selectQuery += "       droits_amo, ";
                selectQuery += "       droits_amc, ";
                selectQuery += "       assure, ";
                selectQuery += "       num_secu, ";
                selectQuery += "       cle_secu, ";
                selectQuery += "       num_mutuelle, ";
                selectQuery += "       code_regime, ";
                selectQuery += "       caisse_gestionnaire, ";
                selectQuery += "       centre_gestionnaire, ";
                selectQuery += "       code_gestion, ";
                selectQuery += "       frontiere, ";
                selectQuery += "       adresse, ";
                selectQuery += "       regime, ";
                selectQuery += "       centre_traitement, ";
                selectQuery += "       nom_assure, ";
                selectQuery += "       prenom_assure, ";
                selectQuery += "       adresse_assure, ";
                selectQuery += "       date_naiss_assure, ";
                selectQuery += "       id_personne, ";
                selectQuery += "       RANGGEMELAIRE, ";

                selectQuery += "       rawdata ";
                selectQuery += " from sv_personne";

                selectQuery += " where id_personne=@id_personne";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id_personne", pat.Id);


                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                if (ds.Tables[0].Rows.Count == 0) return null;

                return ds.Tables[0].Rows[0];

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

        public static DataRow getBeneficiaire(string Nom, string prenom, DateTime dateNaiss, int Rang)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "select id_patsv, ";
                selectQuery += "       nom, ";
                selectQuery += "       prenom, ";
                selectQuery += "       date_naissance, ";
                selectQuery += "       qualite, ";
                selectQuery += "       droits_amo, ";
                selectQuery += "       droits_amc, ";
                selectQuery += "       assure, ";
                selectQuery += "       num_secu, ";
                selectQuery += "       cle_secu, ";
                selectQuery += "       num_mutuelle, ";
                selectQuery += "       code_regime, ";
                selectQuery += "       caisse_gestionnaire, ";
                selectQuery += "       centre_gestionnaire, ";
                selectQuery += "       code_gestion, ";
                selectQuery += "       frontiere, ";
                selectQuery += "       adresse, ";
                selectQuery += "       regime, ";
                selectQuery += "       centre_traitement, ";
                selectQuery += "       nom_assure, ";
                selectQuery += "       prenom_assure, ";
                selectQuery += "       adresse_assure, ";
                selectQuery += "       date_naiss_assure, ";
                selectQuery += "       id_personne, ";
                selectQuery += "       RANGGEMELAIRE, ";
                selectQuery += "       rawdata ";
                selectQuery += " from sv_personne";

                selectQuery += " where nom=@nom";
                selectQuery += " and prenom=@prenom";
                selectQuery += " and date_naissance=@date_naissance";
                selectQuery += " and RANGGEMELAIRE=@RANGGEMELAIRE";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@nom", Nom);
                command.Parameters.AddWithValue("@prenom", prenom);
                command.Parameters.AddWithValue("@date_naissance", dateNaiss);
                command.Parameters.AddWithValue("@RANGGEMELAIRE", Rang);


                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                if (ds.Tables[0].Rows.Count == 0) return null;

                return ds.Tables[0].Rows[0];

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


        public static void DeleteBeneficiaire(string Nom, string prenom, DateTime dateNaiss, int Rang)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "delete ";
                selectQuery += " from sv_personne";
                selectQuery += " where nom=@nom";
                selectQuery += " and prenom=@prenom";
                selectQuery += " and date_naissance=@date_naissance";
                selectQuery += " and RANGGEMELAIRE=@RANGGEMELAIRE";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@nom", Nom);
                command.Parameters.AddWithValue("@prenom", prenom);
                command.Parameters.AddWithValue("@date_naissance", dateNaiss);
                command.Parameters.AddWithValue("@RANGGEMELAIRE", Rang);


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


        public static int getIdPatientFromBeneficiaire(BeneficiaireVitale beneficiaire)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "select  ";
                selectQuery += "       id_personne ";
                selectQuery += " from sv_personne";

                selectQuery += " where upper(nom)=upper(@nom)";
                selectQuery += " and upper(prenom)=upper(@prenom)";
                selectQuery += " and date_naissance=@date_naissance";
                selectQuery += " and RANGGEMELAIRE=@RANGGEMELAIRE";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@nom", beneficiaire.Nom);
                command.Parameters.AddWithValue("@prenom", beneficiaire.prenom);
                command.Parameters.AddWithValue("@date_naissance", beneficiaire.DateNaiss);
                command.Parameters.AddWithValue("@RANGGEMELAIRE", beneficiaire.RangGemelaire);


                object obj = command.ExecuteScalar();
                if (obj is DBNull)
                    return -1;
                else
                    return Convert.ToInt32(obj);


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


        public static int getIdPatientFromNumSecu(string numsecu)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "select personne.id_personne";
                selectQuery += " from personne";
                selectQuery += " inner join patient p on personne.id_personne=p.id_personne";
                selectQuery += " where PER_SECU=@PER_SECU ";



                selectQuery += " order by personne.PER_NOM, personne.PER_PRENOM";
                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@PER_SECU", numsecu);


                object obj = command.ExecuteScalar();
                if (obj == null)
                    return -1;
                else
                    return Convert.ToInt32(obj);


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

        public static int getIdPatientByNomPrenomDateNaiss(string nom, string prenom, DateTime dateNaiss)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "select personne.id_personne";
                selectQuery += " from personne";
                selectQuery += " inner join patient p on personne.id_personne=p.id_personne";
                selectQuery += " where upper(PER_nom)=upper(@PER_NOM) ";
                selectQuery += " and upper(PER_PRENOM)=upper(@PER_PRENOM) ";
                selectQuery += " and (PER_DATNAISS)=(@PER_DATNAISS) ";



                selectQuery += " order by personne.PER_NOM, personne.PER_PRENOM";
                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@PER_NOM", nom);
                command.Parameters.AddWithValue("@PER_PRENOM", prenom);
                command.Parameters.AddWithValue("@PER_DATNAISS", dateNaiss);


                object obj = command.ExecuteScalar();
                if (obj == null)
                    return -1;
                else
                    return Convert.ToInt32(obj);


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








        public static void UpdateAllergiePatient(basePatient p_patient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {


                string selectQuery = "update patient set ";
                selectQuery += "     allergie =@allergie ";
                selectQuery += "     where id_personne =@id_personne";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id_personne", p_patient.Id);
                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                byte[] array = encoding.GetBytes(p_patient.Allergie);

                command.Parameters.AddWithValue("@allergie", array);

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


        #region Contacts

        public static void InsertContactTo(int Idpersonne, Contact contact)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "select MAX(id)+1 as NEWID from contact";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                contact.Id = 0;
                try
                {
                    contact.Id = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (System.Exception) { }

                selectQuery = "insert into contact (id, ";
                selectQuery += "                      ID_CONTACTLIBELLE, ";
                selectQuery += "                      contacttype, ";
                selectQuery += "                      \"VALUE\", ";
                ///selectQuery += "                      is_main, ";
                selectQuery += "                      Adr1, ";
                selectQuery += "                      Adr2, ";
                selectQuery += "                      CodePostal, ";
                selectQuery += "                      Ville, ";
                selectQuery += "                      id_personne)";
                selectQuery += " values (@id, ";
                selectQuery += "         @ID_CONTACTLIBELLE, ";
                selectQuery += "         @contacttype, ";
                selectQuery += "         @VALUE, ";
                //selectQuery += "         @is_main, ";
                selectQuery += "         @Adr1, ";
                selectQuery += "         @Adr2, ";
                selectQuery += "         @CodePostal, ";
                selectQuery += "         @Ville, ";
                selectQuery += "         @id_personne)";


                command.CommandText = selectQuery;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", contact.Id);
                command.Parameters.AddWithValue("@contacttype", contact.TypeContact);
                command.Parameters.AddWithValue("@ID_CONTACTLIBELLE", contact.Libelle == null ? DBNull.Value : (object)contact.Libelle.Id);
                command.Parameters.AddWithValue("@VALUE", contact.Value);

              
                command.Parameters.AddWithValue("@Adr1", contact.adresse == null ? DBNull.Value : (object)contact.adresse.Adr1);
                command.Parameters.AddWithValue("@Adr2", contact.adresse == null ? DBNull.Value : (object)contact.adresse.Adr2);
                command.Parameters.AddWithValue("@CodePostal", contact.adresse == null ? DBNull.Value : (object)contact.adresse.CodePostal);
                command.Parameters.AddWithValue("@Ville", contact.adresse == null ? DBNull.Value : (object)contact.adresse.Ville);

                command.Parameters.AddWithValue("@id_personne", Idpersonne);

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

        public static void UpdateContactTo(int IdPersonne, Contact contact)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "update contact set ";
                selectQuery += " contacttype = @contacttype,";
                selectQuery += " contact.\"VALUE\" = @value,";
                selectQuery += " contact.ID_CONTACTLIBELLE = @ID_CONTACTLIBELLE,";
                //selectQuery += " is_main = @is_main,";
                selectQuery += " Adr1 = @Adr1,";
                selectQuery += " Adr2 = @Adr2,";
                selectQuery += " CodePostal = @CodePostal,";
                selectQuery += " Ville = @Ville";
                selectQuery += " where (id_personne = @id_personne) ";
                selectQuery += " and (id = @id_contact) ";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.CommandText = selectQuery;

                command.Parameters.AddWithValue("@id_personne", IdPersonne);
                command.Parameters.AddWithValue("@id_contact", contact.Id);
                command.Parameters.AddWithValue("@contacttype", contact.TypeContact);
                command.Parameters.AddWithValue("@value", contact.Value);
                command.Parameters.AddWithValue("@ID_CONTACTLIBELLE", contact.Libelle == null ? DBNull.Value : (object)contact.Libelle.Id);
                command.Parameters.AddWithValue("@Adr1", contact.adresse == null ? DBNull.Value : (object)contact.adresse.Adr1);
                command.Parameters.AddWithValue("@Adr2", contact.adresse == null ? DBNull.Value : (object)contact.adresse.Adr2);
                command.Parameters.AddWithValue("@CodePostal", contact.adresse == null ? DBNull.Value : (object)contact.adresse.CodePostal);
                command.Parameters.AddWithValue("@Ville", contact.adresse == null ? DBNull.Value : (object)contact.adresse.Ville);
               
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

        public static DataTable getContactsOf(int Id)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "SELECT ";
                selectQuery += " id, ";
                selectQuery += " contacttype, ";
                selectQuery += " \"VALUE\", ";
                selectQuery += " libelle, ";
                //selectQuery += " is_main, ";
                selectQuery += " Adr1, ";
                selectQuery += " Adr2, ";
                selectQuery += " CodePostal, ";
                selectQuery += " Ville, ";
                selectQuery += " Pays, ";
                selectQuery += " id_personne, ";
                selectQuery += " ID_CONTACTLIBELLE ";

                selectQuery += " from CONTACT ";
                selectQuery += " where id_personne=@id_personne";


                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id_personne", Id);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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
                transaction.Rollback();
                throw e;
            }
            finally
            {
                connection.Close();

            }
        }

        public static void deleteContactsFromCorrespondant(Correspondant corres)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "DELETE from CONTACT WHERE CONTACT.ID_PERSONNE = @ID";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@ID", corres.Id);

                command.ExecuteNonQuery();

                selectQuery = "DELETE from CONTACT WHERE CONTACT.ID_PERSONNE = @ID";

                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@ID", corres.Id);

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

        public static void deleteContactFromId(int id)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "DELETE from CONTACT WHERE CONTACT.ID = @ID";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@ID", id);

                command.ExecuteNonQuery();

                selectQuery = "DELETE from CONTACT WHERE CONTACT.ID = @ID";

                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@ID", id);

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


        public static void UpdateStatusManuelPatient(basePatient p_patient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "update patient set ID_STATUT=@ID_STATUT";
                selectQuery += "     where id_personne =@id_personne";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id_personne", p_patient.Id);
                command.Parameters.AddWithValue("@ID_STATUT", p_patient.statusManuel.Id);


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


        public static void UpdateCasiersPatient(basePatient p_patient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "update patient set pat_refdossier=@pat_refdossier,";
                selectQuery += "     num_Moulage =@numMoulage";
                selectQuery += "     where id_personne =@id_personne";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id_personne", p_patient.Id);
                command.Parameters.AddWithValue("@pat_refdossier", p_patient.CasierInvisalign);
                command.Parameters.AddWithValue("@numMoulage", p_patient.numMoulage);


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


        public static void UpdatePatient(basePatient p_patient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {


                string selectQuery = "update personne set ";
                //selectQuery += " id_adresse = @id_adresse,";
                //selectQuery += " id_util = @id_util,";
                //selectQuery += "     id_caisse = @id_caisse,";
                //selectQuery += "     adr_id_adresse = @adr_id_adresse,";
                selectQuery += "     per_nom = @per_nom,";
                selectQuery += "     per_nomjf = @per_nomjf,";
                selectQuery += "     per_prenom = @per_prenom,";
                //selectQuery += "     per_genre = @per_genre,";
                //selectQuery += "     per_secu = @per_secu,";
                //selectQuery += "     per_type = 1,";
                //selectQuery += "     per_telprinc = @per_telprinc,";
                //selectQuery += "     per_teltrav1 = @per_teltrav1,";
                //selectQuery += "     per_teltrav2 = @per_teltrav2,";
                //selectQuery += "     per_telecopie = @per_telecopie,";
                //selectQuery += "     per_email = @per_email,";
                //selectQuery += "     per_reception = @per_reception,";
                selectQuery += "     per_notes = @per_notes,";
                //selectQuery += "     per_poste = @per_poste,";
                //selectQuery += "     pcom = @pcom,";
                //selectQuery += "     per_adr1 = @per_adr1,";
                //selectQuery += "     per_adr2 = @per_adr2,";
                //selectQuery += "     per_ville = @per_ville,";
                //selectQuery += "     per_cpostal = @per_cpostal,";
                //selectQuery += "     per_adr1_prof = @per_adr1_prof,";
                //selectQuery += "     per_adr2_prof = @per_adr2_prof,";
                //selectQuery += "     per_cpostal_prof = @per_cpostal_prof,";
                //selectQuery += "     per_ville_prof = @per_ville_prof,";
                selectQuery += "     profession = @profession,";
                //selectQuery += "     mutuelle = @mutuelle,";
                selectQuery += "     per_datnaiss = @per_datnaiss,";
                selectQuery += "     tuvous = @tuvous,";
                //selectQuery += "     poid = @poid,";
                //selectQuery += "     email2 = @email2,";
                //selectQuery += "     gsm = @gsm,";
                //selectQuery += "     icq = @icq,";
                //selectQuery += "     im1 = @im1,";
                //selectQuery += "     im2 = @im2,";
                //selectQuery += "     lastmodif = @lastmodif,";
                //selectQuery += "     telsup0 = @telsup0,";
                //selectQuery += "     telsup3 = @telsup3,";
                //selectQuery += "     telsup4 = @telsup4,";
                //selectQuery += "     telsup5 = @telsup5,";
                //selectQuery += "     telsup6 = @telsup6,";
                //selectQuery += "     telsup8 = @telsup8,";
                //selectQuery += "     telsup10 = @telsup10,";
                //selectQuery += "     telsup11 = @telsup11,";
                //selectQuery += "     telsup12 = @telsup12,";
                //selectQuery += "     telsup13 = @telsup13,";
                //selectQuery += "     telsup14 = @telsup14,";
                //selectQuery += "     telsup15 = @telsup15,";
                //selectQuery += "     telsup16 = @telsup16,";
                //selectQuery += "     telsup17 = @telsup17,";
                //selectQuery += "     telsup18 = @telsup18,";
                //selectQuery += "     indicetel1 = @indicetel1,";
                //selectQuery += "     indicetel2 = @indicetel2,";
                //selectQuery += "     indicetel3 = @indicetel3,";
                //selectQuery += "     indicetel4 = @indicetel4,";
                //selectQuery += "     email3 = @email3,";
                //selectQuery += "     indiceemail = @indiceemail,";
                //selectQuery += "     indiceadr = @indiceadr,";
                //selectQuery += "     pays_dom = @pays_dom,";
                //selectQuery += "     pays_trav = @pays_trav,";
                selectQuery += "     pers_titre = @pers_titre ";

                //selectQuery += "     pers_siteweb = @pers_siteweb,";
                //selectQuery += "     per_ville_naissance = @per_ville_naissance,";
                //selectQuery += "     per_pays_naissance = @per_pays_naissance,";
                //selectQuery += "     per_langue_parle = @per_langue_parle,";
                //selectQuery += "     per_population_ref = @per_population_ref,";
                //selectQuery += "     nom_rep_image = @nom_rep_image,";
                //selectQuery += "     oi_login = @oi_login,";
                //selectQuery += "     oi_mdp = @oi_mdp,";
                //selectQuery += "     oi_profil = @oi_profil,";
                //selectQuery += "     oi_autorisation = @oi_autorisation,";
                // selectQuery += "     categories = @categories,";
                // selectQuery += "     pref_com = @pref_com";
                selectQuery += " where (id_personne = @id_personne)";


                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id_personne", p_patient.Id);
                command.Parameters.AddWithValue("@per_nom", p_patient.Nom);
                command.Parameters.AddWithValue("@per_nomjf", p_patient.NomJF);
                command.Parameters.AddWithValue("@per_datnaiss", p_patient.DateNaissance);
                command.Parameters.AddWithValue("@per_prenom", p_patient.Prenom);
                command.Parameters.AddWithValue("@profession", p_patient.Profession);


                command.Parameters.AddWithValue("@per_notes", p_patient.Notes);


                if (p_patient.Tutoiement)
                    command.Parameters.AddWithValue("@tuvous", 0);
                else
                    command.Parameters.AddWithValue("@tuvous", 1);


                command.Parameters.AddWithValue("@pref_com", ((char)p_patient.PrefCom).ToString());

                command.Parameters.AddWithValue("@pers_titre", p_patient.Civilite);


                command.ExecuteNonQuery();


                selectQuery = "update patient set ";

                // selectQuery += "     per_id_personne =@per_id_personne ,";
                //selectQuery += "     per2_id_personne =@per2_id_personne ,";
                //selectQuery += "     per3_id_personne =@per3_id_personne ,";
                //selectQuery += "     per4_id_personne =@per4_id_personne ,";
                //selectQuery += "     per5_id_personne =@per5_id_personne ,";
                selectQuery += "     id_statut =@id_statut ,";
                selectQuery += "     pat_numdossier =@pat_numdossier ,";
                selectQuery += "     num_moulage =@nummoulage ,";
                selectQuery += "     pat_refdossier = @pat_refdossier,";
                //selectQuery += "     pat_datecreation =@pat_datecreation ,";
                //selectQuery += "     pat_classdiag =@pat_classdiag ,";
                //selectQuery += "     pat_divisiondiag =@pat_divisiondiag ,";
                //selectQuery += "     pat_daterelance =@pat_daterelance ,";
                //selectQuery += "     pat_remarques =@pat_remarques ,";
                //selectQuery += "     pat_plan =@pat_plan ,";
                //selectQuery += "     pat_accord =@pat_accord ,";
                //selectQuery += "     lien_payeur =@lien_payeur ,";
                //selectQuery += "     lien_assure =@lien_assure ,";
                //selectQuery += "     pat_cd =@pat_cd ,";
                //selectQuery += "     pat_refdossier =@pat_refdossier ,";
                //selectQuery += "     date_reco =@date_reco ,";
                //selectQuery += "     num_reco =@num_reco ,";
                //selectQuery += "     pat_diag =@pat_diag ,";
                //selectQuery += "     pcom =@pcom ,";
                //selectQuery += "     tydossier =@tydossier ,";
                //selectQuery += "     phase_trait =@phase_trait ,";
                //selectQuery += "     pat_daterdv =@pat_daterdv ,";
                //selectQuery += "     pat_solde =@pat_solde ,";
                //selectQuery += "     pat_statlastrdv =@pat_statlastrdv ,";
                //selectQuery += "     pat_lastrdv =@pat_lastrdv ,";
                //selectQuery += "     pat_dureerdv =@pat_dureerdv ,";
                //selectQuery += "     pat_msgaudio =@pat_msgaudio ,";
                //selectQuery += "     rem_visible =@rem_visible ,";
                //selectQuery += "     debut_trait =@debut_trait ,";
                //selectQuery += "     pat_solde_euro =@pat_solde_euro ,";
                //selectQuery += "     cmu =@cmu ,";
                //selectQuery += "     tierspayant =@tierspayant ,";
                //selectQuery += "     securite =@securite ,";
                selectQuery += "     allergie =@allergie ,";
                //selectQuery += "     NUM_MOULAGE =@NUM_MOULAGE, ";
                //selectQuery += "     rep =@rep ,";
                //selectQuery += "     libelle_rdv =@libelle_rdv ,";
                //selectQuery += "     pat_premierrdv =@pat_premierrdv ,";
                //selectQuery += "     semestre_encours =@semestre_encours ,";
                //selectQuery += "     dateprop =@dateprop ,";
                //selectQuery += "     dateaccord =@dateaccord ,";
                //selectQuery += "     nextpaiement =@nextpaiement ,";
                //selectQuery += "     nextpaiementeuro =@nextpaiementeuro ,";
                //selectQuery += "     datenextpaiement =@datenextpaiement ,";
                //selectQuery += "     semestre_encours_dep =@semestre_encours_dep ,";
                //selectQuery += "     pat_objectif_trait =@pat_objectif_trait ,";
                //selectQuery += "     nom_plan =@nom_plan ,";
                //selectQuery += "     comm_next_rdv =@comm_next_rdv ,";
                //selectQuery += "     id_radio =@id_radio ,";
                //selectQuery += "     moulage =@moulage ,";
                //selectQuery += "     derniertypepaiement =@derniertypepaiement ,";
                //selectQuery += "     datenextdep =@datenextdep ,";
                //selectQuery += "     semestre_next_dep =@semestre_next_dep ,";
                //selectQuery += "     pat_rem_plg =@pat_rem_plg ,";
                //selectQuery += "     lastrdv_heure_arrive =@lastrdv_heure_arrive ,";
                //selectQuery += "     pat_open_bnotes=@pat_open_bnotes";
                selectQuery += "     DATEABANDON=@DATEABANDON,";
                selectQuery += "     StatusClinique=@StatusClinique";


                selectQuery += "     where id_personne =@id_personne";

                command.CommandText = selectQuery;

                command.Parameters.AddWithValue("@id_personne", p_patient.Id);
                //command.Parameters.AddWithValue("@per_id_personne", p_patient.per_id_personne);
                //command.Parameters.AddWithValue("@per2_id_personne", p_patient.per2_id_personne);
                //command.Parameters.AddWithValue("@per3_id_personne", p_patient.per3_id_personne);
                //command.Parameters.AddWithValue("@per4_id_personne", p_patient.per4_id_personne);
                //command.Parameters.AddWithValue("@per5_id_personne", p_patient.per5_id_personne);
                //command.Parameters.AddWithValue("@id_statut", p_patient.id_statut);
                command.Parameters.AddWithValue("@pat_numdossier", p_patient.Dossier);
                command.Parameters.AddWithValue("@pat_refdossier", p_patient.CasierInvisalign);
                command.Parameters.AddWithValue("@nummoulage", p_patient.numMoulage);
                //command.Parameters.AddWithValue("@pat_datecreation", p_patient.pat_datecreation);
                //command.Parameters.AddWithValue("@pat_classdiag", p_patient.pat_classdiag);
                //command.Parameters.AddWithValue("@pat_divisiondiag", p_patient.pat_divisiondiag);
                //command.Parameters.AddWithValue("@pat_daterelance", p_patient.pat_daterelance);
                //command.Parameters.AddWithValue("@pat_remarques", p_patient.pat_remarques);
                //command.Parameters.AddWithValue("@pat_plan", p_patient.pat_plan);
                //command.Parameters.AddWithValue("@pat_accord", p_patient.pat_accord);
                //command.Parameters.AddWithValue("@lien_payeur", p_patient.lien_payeur);
                //command.Parameters.AddWithValue("@lien_assure", p_patient.lien_assure);
                //command.Parameters.AddWithValue("@pat_cd", p_patient.pat_cd);
                //command.Parameters.AddWithValue("@pat_refdossier", p_patient.pat_refdossier);
                //command.Parameters.AddWithValue("@date_reco", p_patient.date_reco);
                //command.Parameters.AddWithValue("@num_reco", p_patient.num_reco);
                //command.Parameters.AddWithValue("@pat_diag", p_patient.pat_diag);
                //command.Parameters.AddWithValue("@pcom", p_patient.pcom);
                //command.Parameters.AddWithValue("@tydossier", p_patient.tydossier);
                //command.Parameters.AddWithValue("@phase_trait", p_patient.phase_trait);
                //command.Parameters.AddWithValue("@pat_daterdv", p_patient.pat_daterdv);
                //command.Parameters.AddWithValue("@pat_solde", p_patient.pat_solde);
                //command.Parameters.AddWithValue("@pat_statlastrdv", p_patient.pat_statlastrdv);
                //command.Parameters.AddWithValue("@pat_lastrdv", p_patient.pat_lastrdv);
                //command.Parameters.AddWithValue("@pat_dureerdv", p_patient.pat_dureerdv);
                //command.Parameters.AddWithValue("@pat_msgaudio", p_patient.pat_msgaudio);
                //command.Parameters.AddWithValue("@rem_visible", p_patient.rem_visible);
                //command.Parameters.AddWithValue("@debut_trait", p_patient.debut_trait);
                //command.Parameters.AddWithValue("@pat_solde_euro", p_patient.pat_solde_euro);
                //command.Parameters.AddWithValue("@cmu", p_patient.cmu);
                //command.Parameters.AddWithValue("@tierspayant", p_patient.tierspayant);
                //command.Parameters.AddWithValue("@securite", p_patient.securite);

                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                byte[] array = encoding.GetBytes(p_patient.Allergie);

                command.Parameters.AddWithValue("@allergie", array);
                command.Parameters.AddWithValue("@NUM_MOULAGE", p_patient.Moulage);
                command.Parameters.AddWithValue("@DATEABANDON", p_patient.DateAbandon);
                command.Parameters.AddWithValue("@StatusClinique", p_patient.statusClinique);
                command.Parameters.AddWithValue("@id_statut", p_patient.statusManuel == null ? DBNull.Value : (object)p_patient.statusManuel.Id);

                //command.Parameters.AddWithValue("@rep", p_patient.rep);
                //command.Parameters.AddWithValue("@libelle_rdv", p_patient.libelle_rdv);
                //command.Parameters.AddWithValue("@pat_premierrdv", p_patient.pat_premierrdv);
                //command.Parameters.AddWithValue("@semestre_encours", p_patient.semestre_encours);
                //command.Parameters.AddWithValue("@dateprop", p_patient.dateprop);
                //command.Parameters.AddWithValue("@dateaccord", p_patient.dateaccord);
                //command.Parameters.AddWithValue("@nextpaiement", p_patient.nextpaiement);
                //command.Parameters.AddWithValue("@nextpaiementeuro", p_patient.nextpaiementeuro);
                //command.Parameters.AddWithValue("@datenextpaiement", p_patient.datenextpaiement);
                //command.Parameters.AddWithValue("@semestre_encours_dep", p_patient.semestre_encours_dep);
                //command.Parameters.AddWithValue("@pat_objectif_trait", p_patient.pat_objectif_trait);
                //command.Parameters.AddWithValue("@nom_plan", p_patient.nom_plan);
                //command.Parameters.AddWithValue("@comm_next_rdv", p_patient.comm_next_rdv);
                //command.Parameters.AddWithValue("@id_radio", p_patient.id_radio);
                //command.Parameters.AddWithValue("@moulage", p_patient.moulage);
                //command.Parameters.AddWithValue("@derniertypepaiement", p_patient.derniertypepaiement);
                //command.Parameters.AddWithValue("@datenextdep", p_patient.datenextdep);
                //command.Parameters.AddWithValue("@semestre_next_dep", p_patient.semestre_next_dep);
                //command.Parameters.AddWithValue("@pat_rem_plg", p_patient.pat_rem_plg);
                //command.Parameters.AddWithValue("@lastrdv_heure_arrive", p_patient.lastrdv_heure_arrive);
                //command.Parameters.AddWithValue("@pat_open_bnotes", p_patient.pat_open_bnotes);



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

        public static void InsertPatient(basePatient patient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select MAX(ID_PERSONNE)+1 as NEWID from PERSONNE";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                patient.Id = Convert.ToInt32(command.ExecuteScalar());

                // Insertion dans la table Personne
                selectQuery = "insert into personne (";
                selectQuery += " id_personne,";
                //selectQuery += " id_adresse,"; 
                //selectQuery += " id_util,"; 
                //selectQuery += " id_caisse,"; 
                //selectQuery += " adr_id_adresse,"; 
                selectQuery += " per_nom,";
                selectQuery += " per_nomjf,";
                selectQuery += " per_prenom,";
                //selectQuery += " per_genre,"; 
                //selectQuery += " per_secu,"; 
                selectQuery += " per_type,";
                selectQuery += " per_telprinc,";
                selectQuery += " per_teltrav1,";
                selectQuery += " per_teltrav2,";
                selectQuery += " per_telecopie,";
                selectQuery += " per_email,";
                //selectQuery += " per_reception,"; 
                selectQuery += " per_notes,";
                //selectQuery += " per_poste,"; 
                //selectQuery += " pcom,"; 
                selectQuery += " per_adr1,";
                selectQuery += " per_adr2,";
                selectQuery += " per_ville,";
                selectQuery += " per_cpostal,";
                selectQuery += " per_adr1_prof,";
                selectQuery += " per_adr2_prof,";
                selectQuery += " per_cpostal_prof,";
                selectQuery += " per_ville_prof,";
                selectQuery += " profession,";
                //selectQuery += " mutuelle,"; 
                selectQuery += " per_datnaiss,";
                //selectQuery += " tuvous,"; 
                //selectQuery += " poid,"; 
                //selectQuery += " email2,"; 
                //selectQuery += " gsm,"; 
                //selectQuery += " icq,"; 
                //selectQuery += " im1,"; 
                //selectQuery += " im2,"; 
                //selectQuery += " lastmodif,"; 
                //selectQuery += " telsup0,"; 
                //selectQuery += " telsup3,"; 
                //selectQuery += " telsup4,"; 
                //selectQuery += " telsup5,"; 
                //selectQuery += " telsup6,"; 
                //selectQuery += " telsup8,"; 
                //selectQuery += " telsup10,"; 
                //selectQuery += " telsup11,"; 
                //selectQuery += " telsup12,"; 
                //selectQuery += " telsup13,"; 
                //selectQuery += " telsup14,"; 
                //selectQuery += " telsup15,"; 
                //selectQuery += " telsup16,"; 
                //selectQuery += " telsup17,"; 
                //selectQuery += " telsup18,"; 
                //selectQuery += " indicetel1,"; 
                //selectQuery += " indicetel2,"; 
                //selectQuery += " indicetel3,"; 
                //selectQuery += " indicetel4,"; 
                //selectQuery += " email3,"; 
                //selectQuery += " indiceemail,"; 
                //selectQuery += " indiceadr,"; 
                //selectQuery += " pays_dom,"; 
                //selectQuery += " pays_trav,"; 
                selectQuery += " pers_titre,";
                //selectQuery += " pers_siteweb,"; 
                //selectQuery += " per_ville_naissance,"; 
                //selectQuery += " per_pays_naissance,"; 
                //selectQuery += " per_langue_parle,"; 
                //selectQuery += " per_population_ref,"; 
                //selectQuery += " nom_rep_image,"; 
                selectQuery += " oi_login,";
                selectQuery += " oi_mdp,";
                selectQuery += " oi_profil,";
                selectQuery += " oi_autorisation,";
                //selectQuery += " categories,";
                selectQuery += " pref_com";

                selectQuery += ") values (";

                selectQuery += " @id_personne,";
                //selectQuery += " @id_adresse,"; 
                //selectQuery += " @id_util,"; 
                //selectQuery += " @id_caisse,"; 
                //selectQuery += " @adr_id_adresse,"; 
                selectQuery += " @per_nom,";
                selectQuery += " @per_nomjf,";
                selectQuery += " @per_prenom,";
                //selectQuery += " @per_genre,"; 
                //selectQuery += " @per_secu,"; 
                selectQuery += " @per_type,";
                selectQuery += " @per_telprinc,";
                selectQuery += " @per_teltrav1,";
                selectQuery += " @per_teltrav2,";
                selectQuery += " @per_telecopie,";
                selectQuery += " @per_email,";
                //selectQuery += " @per_reception,"; 
                selectQuery += " @per_notes,";
                //selectQuery += " @per_poste,"; 
                //selectQuery += " @pcom,"; 
                selectQuery += " @per_adr1,";
                selectQuery += " @per_adr2,";
                selectQuery += " @per_ville,";
                selectQuery += " @per_cpostal,";
                selectQuery += " @per_adr1_prof,";
                selectQuery += " @per_adr2_prof,";
                selectQuery += " @per_cpostal_prof,";
                selectQuery += " @per_ville_prof,";
                selectQuery += " @profession,";
                //selectQuery += " @mutuelle,"; 
                selectQuery += " @per_datnaiss,";
                //selectQuery += " @tuvous,"; 
                //selectQuery += " @poid,"; 
                //selectQuery += " @email2,"; 
                //selectQuery += " @gsm,"; 
                //selectQuery += " @icq,"; 
                //selectQuery += " @im1,"; 
                //selectQuery += " @im2,"; 
                //selectQuery += " @lastmodif,"; 
                //selectQuery += " @telsup0,"; 
                //selectQuery += " @telsup3,"; 
                //selectQuery += " @telsup4,"; 
                //selectQuery += " @telsup5,"; 
                //selectQuery += " @telsup6,"; 
                //selectQuery += " @telsup8,"; 
                //selectQuery += " @telsup10,"; 
                //selectQuery += " @telsup11,"; 
                //selectQuery += " @telsup12,"; 
                //selectQuery += " @telsup13,"; 
                //selectQuery += " @telsup14,"; 
                //selectQuery += " @telsup15,"; 
                //selectQuery += " @telsup16,"; 
                //selectQuery += " @telsup17,"; 
                //selectQuery += " @telsup18,"; 
                //selectQuery += " @indicetel1,"; 
                //selectQuery += " @indicetel2,"; 
                //selectQuery += " @indicetel3,"; 
                //selectQuery += " @indicetel4,"; 
                //selectQuery += " @email3,"; 
                //selectQuery += " @indiceemail,"; 
                //selectQuery += " @indiceadr,"; 
                //selectQuery += " @pays_dom,"; 
                //selectQuery += " @pays_trav,"; 
                selectQuery += " @pers_titre,";
                //selectQuery += " @pers_siteweb,"; 
                //selectQuery += " @per_ville_naissance,"; 
                //selectQuery += " @per_pays_naissance,"; 
                //selectQuery += " @per_langue_parle,"; 
                //selectQuery += " @per_population_ref,"; 
                //selectQuery += " @nom_rep_image,"; 
                selectQuery += " @oi_login,";
                selectQuery += " @oi_mdp,";
                selectQuery += " @oi_profil,";
                selectQuery += " @oi_autorisation,";
                //selectQuery += " @categories,"; 
                selectQuery += " @pref_com";
                selectQuery += " )";

                string per_notes = "";



                command = new FbCommand(selectQuery, connection, transaction);
                //command.Parameters.AddWithValue("@per_adr1", patient.Adresse1 == null ? "" : patient.Adresse1);
                command.Parameters.AddWithValue("@per_type", 1);
                //command.Parameters.AddWithValue("@per_adr1_prof", "");
                //command.Parameters.AddWithValue("@per_adr2", patient.Adresse2 == null ? "" : patient.Adresse2);
                //command.Parameters.AddWithValue("@per_adr2_prof", "");
                command.Parameters.AddWithValue("@pers_titre", patient.Civilite == null ? "" : patient.Civilite);
                //command.Parameters.AddWithValue("@per_cpostal", patient.CodePostal == null ? "" : patient.CodePostal);
                //command.Parameters.AddWithValue("@per_cpostal_prof", "");
                command.Parameters.AddWithValue("@per_datnaiss", patient.DateNaissance == null ? (object)DBNull.Value : (object)patient.DateNaissance);
                //command.Parameters.AddWithValue("@dossier", patient.Dossier);
                //command.Parameters.AddWithValue("@per_telecopie", patient.Fax == null ? "" : patient.Fax);
                command.Parameters.AddWithValue("@id_personne", patient.Id);
                //command.Parameters.AddWithValue("@per_email", patient.Mail == null ? "" : patient.Mail);
                command.Parameters.AddWithValue("@per_notes", per_notes);
                command.Parameters.AddWithValue("@per_nom", patient.Nom == null ? "" : patient.Nom);
                command.Parameters.AddWithValue("@per_nomjf", patient.NomJF == null ? "" : patient.NomJF);
                command.Parameters.AddWithValue("@per_prenom", patient.Prenom == null ? "" : patient.Prenom);
                command.Parameters.AddWithValue("@profession", patient.Profession == null ? "" : patient.Profession);
                //command.Parameters.AddWithValue("@per_telprinc", patient.TelFixe == null ? "" : patient.TelFixe);
                //command.Parameters.AddWithValue("@per_teltrav2", patient.TelPortable == null ? "" : patient.TelPortable);
                //command.Parameters.AddWithValue("@per_teltrav1", patient.TelProfessionnel == null ? "" : patient.TelProfessionnel);
                //command.Parameters.AddWithValue("@per_ville", patient.Ville == null ? "" : patient.Ville);
                command.Parameters.AddWithValue("@per_ville_prof", "");
                command.Parameters.AddWithValue("@oi_login", patient.Nom == null ? "" : patient.Nom + patient.Id);
                command.Parameters.AddWithValue("@oi_mdp", patient.Id);
                command.Parameters.AddWithValue("@oi_profil", -1);
                command.Parameters.AddWithValue("@oi_autorisation", 1);
                command.Parameters.AddWithValue("@pref_com", patient.PrefCom);

                command.ExecuteNonQuery();



                // Insertion dans la table Patient
                selectQuery = "insert into patient (id_personne,";
                selectQuery += " per_id_personne,";
                selectQuery += " per2_id_personne,";
                selectQuery += " per5_id_personne, ";
                selectQuery += " id_statut, ";
                selectQuery += " pat_numdossier, ";
                selectQuery += " NumMoulage, ";
                selectQuery += " pat_datecreation, ";
                selectQuery += " lien_payeur, ";
                selectQuery += " lien_assure, ";
                selectQuery += " pat_solde, ";
                selectQuery += " pat_refdossier, ";
                selectQuery += " debut_trait, ";
                selectQuery += " pat_solde_euro, ";
                selectQuery += " rep)";
                selectQuery += " values (@id_personne, ";
                selectQuery += " @per_id_personne, ";
                selectQuery += " @per2_id_personne, ";
                selectQuery += " 2, ";
                selectQuery += " @idstatus, ";
                selectQuery += " @pat_NumDossier, ";
                selectQuery += " @NumMoulage, ";
                selectQuery += " @pat_datecreation,";
                selectQuery += " '', ";
                selectQuery += " '', ";
                selectQuery += " 0, ";
                selectQuery += " @pat_refdossier, ";
                selectQuery += " @debut_trait, ";
                selectQuery += " 0, ";
                selectQuery += " @rep)";

                command.CommandType = CommandType.Text;

                command.CommandText = selectQuery;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id_personne", patient.Id);
                command.Parameters.AddWithValue("@per_id_personne", patient.Id);
                command.Parameters.AddWithValue("@per2_id_personne", patient.Id);
                command.Parameters.AddWithValue("@pat_NumDossier", patient.Dossier);
                command.Parameters.AddWithValue("@idstatus", patient.statusManuel == null ? DBNull.Value : (object)patient.statusManuel.Id);

                command.Parameters.AddWithValue("@NumMoulage", patient.numMoulage);
                command.Parameters.AddWithValue("@pat_datecreation", DateTime.Now);
                command.Parameters.AddWithValue("@debut_trait", DBNull.Value);
                command.Parameters.AddWithValue("@pat_refdossier", patient.CasierInvisalign);
                command.Parameters.AddWithValue("@rep", patient.Nom + " " + patient.Prenom + " " + patient.Dossier.ToString());

                command.ExecuteNonQuery();
                command.Transaction.Commit();
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

        public static void InsertBeneficiaire(BeneficiaireVitale beneficiaire)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQueryid = "select MAX(id_patsv)+1 as NEWID from sv_personne";

                FbCommand command = new FbCommand(selectQueryid, connection, transaction);
                command.CommandType = CommandType.Text;
                beneficiaire.Id = Convert.ToInt32(command.ExecuteScalar());

                string selectQuery = "insert into sv_personne (id_patsv, ";
                selectQuery += "                         nom, ";
                selectQuery += "                         prenom, ";
                selectQuery += "                         date_naissance, ";
                selectQuery += "                         qualite, ";
                selectQuery += "                         droits_amo, ";
                selectQuery += "                         droits_amc, ";
                selectQuery += "                         assure, ";
                selectQuery += "                         num_secu, ";
                selectQuery += "                         cle_secu, ";
                selectQuery += "                         num_mutuelle, ";
                selectQuery += "                         code_regime, ";
                selectQuery += "                         caisse_gestionnaire, ";
                selectQuery += "                         centre_gestionnaire, ";
                selectQuery += "                         code_gestion, ";
                selectQuery += "                         frontiere, ";
                selectQuery += "                         adresse, ";
                selectQuery += "                         regime, ";
                selectQuery += "                         centre_traitement, ";
                selectQuery += "                         nom_assure, ";
                selectQuery += "                         prenom_assure, ";
                selectQuery += "                         adresse_assure, ";
                selectQuery += "                         date_naiss_assure, ";
                selectQuery += "                         id_personne, ";
                selectQuery += "                         RANGGEMELAIRE, ";
                selectQuery += "                         rawdata)";
                selectQuery += " values (@id_patsv, ";
                selectQuery += "        @nom, ";
                selectQuery += "        @prenom, ";
                selectQuery += "        @date_naissance, ";
                selectQuery += "        @qualite, ";
                selectQuery += "        @droits_amo, ";
                selectQuery += "        @droits_amc, ";
                selectQuery += "        null, ";
                selectQuery += "        @num_secu, ";
                selectQuery += "        @cle_secu, ";
                selectQuery += "        @num_mutuelle, ";
                selectQuery += "        @code_regime, ";
                selectQuery += "        @caisse_gestionnaire, ";
                selectQuery += "        @centre_gestionnaire, ";
                selectQuery += "        @code_gestion, ";
                selectQuery += "        null, ";
                selectQuery += "        @adresse, ";
                selectQuery += "        @regime, ";
                selectQuery += "        @centre_traitement, ";
                selectQuery += "        @nom_assure, ";
                selectQuery += "        @prenom_assure, ";
                selectQuery += "        @adresse_assure, ";
                selectQuery += "        @date_naiss_assure, ";
                selectQuery += "        @id_personne, ";
                selectQuery += "        @RANGGEMELAIRE, ";
                selectQuery += "        @rawdata)";






                command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id_patsv", beneficiaire.Id);
                command.Parameters.AddWithValue("@nom", beneficiaire.Nom);
                command.Parameters.AddWithValue("@prenom", beneficiaire.prenom);
                command.Parameters.AddWithValue("@date_naissance", beneficiaire.DateNaiss);
                command.Parameters.AddWithValue("@qualite", beneficiaire.Qualite);
                command.Parameters.AddWithValue("@droits_amo", beneficiaire.DroitAMO);
                command.Parameters.AddWithValue("@droits_amc", beneficiaire.DroitAMC);
                command.Parameters.AddWithValue("@num_secu", beneficiaire.NumSecu);
                command.Parameters.AddWithValue("@cle_secu", beneficiaire.CleSecu);
                command.Parameters.AddWithValue("@num_mutuelle", beneficiaire.NumMutuelle);
                command.Parameters.AddWithValue("@code_regime", beneficiaire.CodeRegime);
                command.Parameters.AddWithValue("@caisse_gestionnaire", beneficiaire.CaisseGestionnaire);
                command.Parameters.AddWithValue("@centre_gestionnaire", beneficiaire.CentreGestionnaire);
                command.Parameters.AddWithValue("@code_gestion", beneficiaire.CodeGestion);
                command.Parameters.AddWithValue("@adresse", beneficiaire.Adresse);
                command.Parameters.AddWithValue("@regime", beneficiaire.Regime);
                command.Parameters.AddWithValue("@centre_traitement", beneficiaire.CentreTraitement);
                command.Parameters.AddWithValue("@nom_assure", beneficiaire.NomAssure);
                command.Parameters.AddWithValue("@prenom_assure", beneficiaire.PrenomAssure);
                command.Parameters.AddWithValue("@adresse_assure", beneficiaire.AdresseAssure);
                command.Parameters.AddWithValue("@date_naiss_assure", beneficiaire.DateNaissAssure);
                command.Parameters.AddWithValue("@id_personne", beneficiaire.patient == null ? -1 : beneficiaire.patient.Id);
                command.Parameters.AddWithValue("@RANGGEMELAIRE", beneficiaire.RangGemelaire);
                command.Parameters.AddWithValue("@rawdata", beneficiaire.RawDataCarteVital);


                command.ExecuteNonQuery();

                command.Transaction.Commit();
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


        public static void UpdateDiagPatient(basePatient p_patient, string Diag)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {


                string selectQuery = "update patient set ";
                selectQuery += "     pat_diag =@pat_diag";

                selectQuery += "     where id_personne =@id_personne";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);



                command.Parameters.AddWithValue("@id_personne", p_patient.Id);
                command.Parameters.AddWithValue("@pat_diag", Diag);

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

        public static void UpdateAppPatient(basePatient p_patient, string App)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {


                string selectQuery = "update patient set ";
                selectQuery += "     PAT_APPAREIL =@PAT_APPAREIL";

                selectQuery += "     where id_personne =@id_personne";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);



                command.Parameters.AddWithValue("@id_personne", p_patient.Id);
                command.Parameters.AddWithValue("@PAT_APPAREIL", App);

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

        public static void UpdateObjPatient(basePatient p_patient, string Obj)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {


                string selectQuery = "update patient set ";
                selectQuery += "     PAT_OBJECTIF_TRAIT =@PAT_OBJECTIF_TRAIT";

                selectQuery += "     where id_personne =@id_personne";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);



                command.Parameters.AddWithValue("@id_personne", p_patient.Id);
                command.Parameters.AddWithValue("@PAT_OBJECTIF_TRAIT", Obj);

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

        public static void UpdateTraitPatient(basePatient p_patient, string Trait)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {


                string selectQuery = "update patient set ";
                selectQuery += "     pat_plan =@pat_plan";

                selectQuery += "     where id_personne =@id_personne";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);



                command.Parameters.AddWithValue("@id_personne", p_patient.Id);
                command.Parameters.AddWithValue("@pat_plan", Trait);

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

        #region contacts

        public static void SaveContactsTo(int Idpersonne, List<Contact> contacts)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "select MAX(id)+1 as NEWID from contact";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                int Id = 0;
                try
                {
                    Id = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (System.Exception) { }




                selectQuery = "Delete ";
                selectQuery += " from CONTACT ";
                selectQuery += " where id_personne=@id_personne";

                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@id_personne", Idpersonne);

                command.ExecuteNonQuery();




                selectQuery = "insert into contact (id, ";
                selectQuery += "                      contacttype, ";
                selectQuery += "                      \"VALUE\", ";
                selectQuery += "                      libelle, ";
                //selectQuery += "                      is_main, ";
                selectQuery += "                      id_personne)";
                selectQuery += " values (@id, ";
                selectQuery += "         @contacttype, ";
                selectQuery += "         @VALUE, ";
                selectQuery += "         @libelle, ";
                //selectQuery += "         @is_main, ";
                selectQuery += "         @id_personne)";


                command.CommandText = selectQuery;



                foreach (Contact c in contacts)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", Id);
                    command.Parameters.AddWithValue("@contacttype", c.TypeContact);
                    command.Parameters.AddWithValue("@VALUE", c.Value);
                    command.Parameters.AddWithValue("@libelle", c.Libelle);
                    // command.Parameters.AddWithValue("@is_main", false);
                    command.Parameters.AddWithValue("@id_personne", Idpersonne);

                    command.ExecuteNonQuery();

                    Id++;
                }

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


        public static DataTable getContactLib()
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select ID, ";

                selectQuery += "LIBELLE, ";
                selectQuery += "TYPECONTACT, ";
                selectQuery += "TYPEAFFECTATION ";
                selectQuery += "FROM BASE_CONTACTLIBELLE ";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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
                transaction.Rollback();
                throw e;
            }
            finally
            {
                connection.Close();

            }

        }



        #endregion

        #region correspondants


        public static void InsertHistorique(IHistorique Histo)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "";
                FbCommand command = new FbCommand("", connection, transaction);
                command.CommandType = CommandType.Text;

                selectQuery = "update histo_pat_blobs set latestflag='N' ";
                selectQuery += "  where ID_PATIENT=@ID_PATIENT and fieldname=@fieldname";

                command.CommandText = selectQuery;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@fieldname", Histo.FieldName);
                command.Parameters.AddWithValue("@ID_PATIENT", Histo.IdPatient);

                command.ExecuteNonQuery();

                selectQuery = "insert into histo_pat_blobs (dte, ";
                selectQuery += "  fieldname,";
                selectQuery += "  text, ";
                selectQuery += "  ID_PATIENT, ";
                selectQuery += "  latestflag)";
                selectQuery += "  values (@dte,";
                selectQuery += "  @fieldname,";
                selectQuery += "  @text, ";
                selectQuery += "  @ID_PATIENT, ";
                selectQuery += "  'Y')";


                command.CommandText = selectQuery;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@dte", Histo.Dte);
                command.Parameters.AddWithValue("@fieldname", Histo.FieldName);
                command.Parameters.AddWithValue("@text", Histo.Text);
                command.Parameters.AddWithValue("@ID_PATIENT", Histo.IdPatient);

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




        public static void InsertLienCaisse(LienCaisse lnkcaisse)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "insert into lienpers (id_personne, id_patient, typelien, relation)";
                selectQuery += "values (@id_personne, @id_patient, @typelien, @relation)";


                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;




                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@id_personne", lnkcaisse.caisse.Id);
                command.Parameters.AddWithValue("@id_patient", lnkcaisse.IdPatient);
                command.Parameters.AddWithValue("@typelien", "Caisse");
                command.Parameters.AddWithValue("@relation", "Ca");
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

        public static void InsertLienMutuelle(LienMutuelle lnkmutuelle)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "insert into lienpers (id_personne, id_patient, typelien, relation)";
                selectQuery += "values (@id_personne, @id_patient, @typelien, @relation)";


                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;




                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@id_personne", lnkmutuelle.mutuelle.Id);
                command.Parameters.AddWithValue("@id_patient", lnkmutuelle.IdPatient);
                command.Parameters.AddWithValue("@typelien", "Mutuelle");
                command.Parameters.AddWithValue("@relation", "Mu");
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


        #region Caisses

        

        public static DataRow getCaisse(int p_ID)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {




                string selectQuery = "select";
                selectQuery += " ID_CAISSE,";
                selectQuery += " CAISSE_NOM,";
                selectQuery += " CAISSE_TEL,";
                selectQuery += " ISCMU,";
                selectQuery += " ISTIERPAYANT,";
                selectQuery += " TAUXREMBPARDEFAUT,";
                selectQuery += " v.ville_nom,";
                selectQuery += " v.ville_cpostal,";
                selectQuery += " a.ID_Ville,";
                selectQuery += " a.ID_ADRESSE,";
                selectQuery += " a.adr_numero,";
                selectQuery += " a.adr_typevoie,";
                selectQuery += " a.adr_nomvoie,";
                selectQuery += " a.adr_complement";
                selectQuery += " from CAISSE  c";
                selectQuery += " LEFT OUTER JOIN ADRESSE a on c.id_adresse=a.id_adresse";
                selectQuery += " LEFT OUTER JOIN VILLE v ON v.id_ville=a.id_ville";
                selectQuery += " where ID_CAISSE = '" + p_ID.ToString() + "'";



                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                return (dt.Rows[0]);

            }
            catch (System.IndexOutOfRangeException e)
            {
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





        public static DataTable getPlanTraitementsDEP()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {




                string selectQuery = "select";
                selectQuery += " ID_KEY,";
                selectQuery += " LIBELLE";
                selectQuery += " from PARAMTRAIT";



                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];
                return dt;

            }
            catch (System.IndexOutOfRangeException e)
            {
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




        #endregion


        public static DataRow getSmallCorrespondant(int Id)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {




                string selectQuery = "SELECT PERSONNE.ID_PERSONNE,";
                selectQuery += " PER_NOM,";
                selectQuery += " PER_PRENOM,";
                selectQuery += " lnk.Relation";
                selectQuery += " FROM PERSONNE ";
                selectQuery += " left outer JOIN lienpers lnk on lnk.ID_PERSONNE=PERSONNE.ID_PERSONNE ";

                selectQuery += " Where PERSONNE.ID_PERSONNE=@ID_PERSONNE";


                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@ID_PERSONNE", Id);


                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();


                return ds.Tables[0].Rows[0];



            }
            catch (System.IndexOutOfRangeException e)
            {
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


        public static DataRow getCorrespondant(int Id)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {




                string selectQuery = "SELECT PERSONNE.ID_PERSONNE,";
                selectQuery += "PER_NOM,";
                selectQuery += "PER_PRENOM,";
                selectQuery += "PER_EMAIL,";
                selectQuery += "PER_TELPRINC,";
                selectQuery += "PER_TELTRAV1,";
                selectQuery += "PER_GENRE,";
                selectQuery += "PER_TELECOPIE,";
                selectQuery += "PER_ADR1,";
                selectQuery += "PER_ADR2,";
                selectQuery += "PER_SECU,";
                selectQuery += "PER_DATNAISS,";

                selectQuery += "PER_VILLE,";
                selectQuery += "PER_CPOSTAL,";
                selectQuery += "PER_ADR1_prof,";
                selectQuery += "PER_ADR2_prof,";
                selectQuery += "PER_VILLE_prof,";
                selectQuery += "PER_CPOSTAL_prof,";
                selectQuery += "PER_NOTES,";
                selectQuery += "PERS_TITRE,";
                selectQuery += "TUVOUS,";
                selectQuery += "PREF_COM,";
                selectQuery += "TYPE_PERS.NOM as PROFESSION, ";
                selectQuery += "TYPE_PERS.ID_TYPE as TYPE, ";
                selectQuery += "NOTE ";
                selectQuery += "FROM PERSONNE ";
                selectQuery += "INNER JOIN TYPE_PERS on TYPE_PERS.ID_TYPE=PERSONNE.PER_TYPE ";
                selectQuery += " LEFT JOIN BASE_HISTO_CATEGORIE on BASE_HISTO_CATEGORIE.ID_PERSONNE = PERSONNE.ID_PERSONNE and BASE_HISTO_CATEGORIE.DATE_FIN_CATEG is null and ID_CATEGORIE is null";
                selectQuery += " Where PERSONNE.ID_PERSONNE=@ID_PERSONNE";


                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@ID_PERSONNE", Id);


                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();


                return ds.Tables[0].Rows[0];



            }
            catch (System.IndexOutOfRangeException e)
            {
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

        public static DataTable getSmallCorrespondants(string Param)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {




                string selectQuery = "SELECT PERSONNE.ID_PERSONNE,";
                selectQuery += " PER_NOM,";
                selectQuery += " PER_PRENOM ";
                selectQuery += " FROM PERSONNE ";

                selectQuery += " Where 1=1 ";


                if (Param != "")
                {

                    foreach (string s in Param.Split(';'))
                    {
                        selectQuery += " and (UPPER(PER_VILLE) LIKE '" + s.Trim().ToUpper() + "%'";
                        selectQuery += " or UPPER(PER_NOM) LIKE '" + s.Trim().ToUpper() + "%'";
                        selectQuery += " or UPPER(PER_PRENOM) LIKE '" + s.Trim().ToUpper() + "%'";
                        selectQuery += " or UPPER(PER_EMAIL) LIKE '" + s.Trim().ToUpper() + "%'";
                        selectQuery += " or UPPER(PER_TELPRINC) LIKE '" + s.Trim().ToUpper() + "%'";
                        selectQuery += " or UPPER(PER_TELTRAV1) LIKE '" + s.Trim().ToUpper() + "%'";
                        selectQuery += " or UPPER(PER_CPOSTAL) LIKE '" + s.Trim().ToUpper() + "%')";
                    }
                }
                selectQuery += " order by PER_NOM,PER_PRENOM";


                FbCommand command = new FbCommand(selectQuery, connection, transaction);


                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();


                return ds.Tables[0];



            }
            catch (System.IndexOutOfRangeException e)
            {
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


        public static DataTable getCorrespondants(string Param)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {




                string selectQuery = "SELECT PERSONNE.ID_PERSONNE,";
                selectQuery += "PER_NOM,";
                selectQuery += "PER_PRENOM,";
                selectQuery += "PER_EMAIL,";
                selectQuery += "PER_TELPRINC,";
                selectQuery += "PER_TELTRAV1,";
                selectQuery += "PER_GENRE,";
                selectQuery += "PER_TELECOPIE,";
                selectQuery += "PER_ADR1,";
                selectQuery += "PER_ADR2,";
                selectQuery += "PER_VILLE,";
                selectQuery += "PER_CPOSTAL,";
                selectQuery += "PER_ADR1_prof,";
                selectQuery += "PER_ADR2_prof,";
                selectQuery += "PER_VILLE_prof,";
                selectQuery += "PER_CPOSTAL_prof,";
                selectQuery += "PER_NOTES,";
                selectQuery += "PERS_TITRE,";
                selectQuery += "TUVOUS,";
                selectQuery += "PREF_COM,";
                selectQuery += "NOTE,";
                selectQuery += "TYPE_PERS.NOM as PROFESSION, ";
                selectQuery += "TYPE_PERS.ID_TYPE as TYPE ";
                selectQuery += "FROM PERSONNE ";
                selectQuery += "INNER JOIN TYPE_PERS on TYPE_PERS.ID_TYPE=PERSONNE.PER_TYPE ";


                if (Param != "")
                {

                    foreach (string s in Param.Split(';'))
                    {
                        selectQuery += " and (UPPER(PER_VILLE) LIKE '" + s.Trim().ToUpper() + "%'";
                        selectQuery += " or UPPER(PER_NOM) LIKE '" + s.Trim().ToUpper() + "%'";
                        selectQuery += " or UPPER(PER_PRENOM) LIKE '" + s.Trim().ToUpper() + "%'";
                        selectQuery += " or UPPER(PER_EMAIL) LIKE '" + s.Trim().ToUpper() + "%'";
                        selectQuery += " or UPPER(TYPE_PERS.NOM) LIKE '" + s.Trim().ToUpper() + "%'";
                        selectQuery += " or UPPER(PER_TELPRINC) LIKE '" + s.Trim().ToUpper() + "%'";
                        selectQuery += " or UPPER(ID_PERSONNE) LIKE '" + s.Trim().ToUpper() + "%'";
                        selectQuery += " or UPPER(PER_TELTRAV1) LIKE '" + s.Trim().ToUpper() + "%'";
                        selectQuery += " or UPPER(PER_CPOSTAL) LIKE '" + s.Trim().ToUpper() + "%')";
                    }
                }
                selectQuery += " LEFT JOIN BASE_HISTO_CATEGORIE on BASE_HISTO_CATEGORIE.ID_PERSONNE = PERSONNE.ID_PERSONNE and BASE_HISTO_CATEGORIE.DATE_FIN_CATEG is null and ID_CATEGORIE is null";


                selectQuery += " Where PER_TYPE<>1";
                selectQuery += " order by PER_NOM,PER_PRENOM";


                FbCommand command = new FbCommand(selectQuery, connection, transaction);


                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();


                return ds.Tables[0];



            }
            catch (System.IndexOutOfRangeException e)
            {
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

        public static DataTable getVillesSugested()
        {

            FbConnection localconnection = getLocalConnection();

            localconnection.Open();
            FbTransaction transaction = localconnection.BeginTransaction();
            try
            {




                string selectQuery = "SELECT ID_VILLE,";
                selectQuery += " VILLE_NOM,";
                selectQuery += " VILLE_CPOSTAL";
                selectQuery += " FROM VILLE";
                selectQuery += " order by VILLE_NOM";


                FbCommand command = new FbCommand(selectQuery, localconnection, transaction);


                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                return ds.Tables[0];




            }

            catch (System.Exception e)
            {
                transaction.Rollback();
                throw e;
            }
            finally
            {
                localconnection.Close();

            }





        }








        #endregion

        #region RDVs


        public static DataRow getCurrentAppointement(int IdPatient)
        {
            return getCurrentAppointement(IdPatient, -1);
        }

        public static DataRow getCurrentAppointement(int IdPatient, int fauteuilID)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select ID_NEXT_ACTE, id_rdv, p.id_personne,rdv_date, rdv_duree, rdv_statut, rdv_arrivee, rdv_comm, rdv_quand, heure_fauteuil, heure_salleattente, heure_secretariat, heure_sorti, isexported, faut_utilise, localisation,";
                selectQuery += " id_adresse, id_util, id_caisse, adr_id_adresse,per_nom||' '|| per_prenom PATNAME, per_nom, per_nomjf, per_prenom, per_genre, per_secu, per_type, per_telprinc, per_teltrav1, per_teltrav2, per_telecopie, per_email, per_reception, per_notes, per_poste, p.pcom, per_adr1, per_adr2, per_ville, per_cpostal, per_adr1_prof, per_adr2_prof, per_cpostal_prof, per_ville_prof, profession, mutuelle, per_datnaiss, tuvous, poid, email2, gsm, icq, im1, im2, telsup0, telsup3, telsup4, telsup5, telsup6, telsup8, telsup10, telsup11, telsup12, telsup13, telsup14, telsup15, telsup16, telsup17, telsup18, indicetel1, indicetel2, indicetel3, indicetel4, email3, indiceemail, indiceadr, pays_dom, pays_trav, pers_titre, pers_siteweb, per_ville_naissance, per_pays_naissance, per_langue_parle, per_population_ref, nom_rep_image, oi_login, oi_mdp, oi_profil, oi_autorisation, categories, pref_com,";
                selectQuery += " f.id_fauteuil, faut_libelle,FAUT_UTILISE,";
                selectQuery += " a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono,";
                selectQuery += " PAT_NUMDOSSIER,";
                // selectQuery += " (select first 1 rdv_date from rendez_vous r3 where r3.rdv_date<r.rdv_date and r.id_personne=r3.id_personne order by r3.rdv_date desc) as PreviousRDV,";
                selectQuery += " (select first 1 rdv_date from rendez_vous r2 where r2.rdv_date>r.rdv_date and r.id_personne=r2.id_personne order by r2.rdv_date asc) as NextRDV";
                selectQuery += " from rendez_vous r";
                selectQuery += " inner join personne p on r.id_personne=p.id_personne";
                selectQuery += " inner join patient pa on p.id_personne=pa.id_personne";
                selectQuery += " inner join fauteuil f on r.id_fauteuil=f.id_fauteuil";
                selectQuery += " inner join actes a on r.id_acte=a.id_acte";
                selectQuery += " where r.rdv_date between @rdv_date1 and @rdv_date2 and r.id_personne = @IdPatient";
                if (fauteuilID >= 0) selectQuery += " and f.id_fauteuil = @Idfauteuil";

                selectQuery += " order by rdv_date,r.id_fauteuil";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@rdv_date1", DateTime.Now.AddHours(-3));
                command.Parameters.AddWithValue("@rdv_date2", DateTime.Now.AddHours(3));
                command.Parameters.AddWithValue("@IdPatient", IdPatient);
                if (fauteuilID >= 0) command.Parameters.AddWithValue("@Idfauteuil", fauteuilID);


                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                    return dt.Rows[0];
                else
                    return null;

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






        public static void UpdateNextActeAppointment(RHAppointment appointment)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "update rendez_vous";
                selectQuery += "    set id_next_acte = @id_next_acte,";
                selectQuery += "    ID_COMMCLINIQUE = @ID_COMMCLINIQUE";
                selectQuery += "    where (id_rdv = @id_rdv)";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                if (appointment.NextActe == null)
                    command.Parameters.AddWithValue("@id_next_acte", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@id_next_acte", appointment.NextActe.id_acte);

                if (appointment.IdNextCommentaireClinique <= 0)
                    command.Parameters.AddWithValue("@ID_COMMCLINIQUE", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@ID_COMMCLINIQUE", appointment.IdNextCommentaireClinique);


                command.Parameters.AddWithValue("@id_rdv", appointment.Id);

                command.ExecuteNonQuery();
                command.Transaction.Commit();



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




        public static DataTable getCurrentAppointments()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id_rdv, ";
                selectQuery += "       rendez_vous.id_personne, ";
                selectQuery += "       rendez_vous.per_id_personne, ";
                selectQuery += "       id_acte, ";
                selectQuery += "       id_fauteuil, ";
                selectQuery += "       rdv_date, ";
                selectQuery += "       rdv_duree, ";
                selectQuery += "       rdv_statut, ";
                selectQuery += "       rdv_arrivee, ";
                selectQuery += "       rdv_comm, ";
                selectQuery += "       rdv_quand, ";
                selectQuery += "       rendez_vous.lastmodif, ";
                selectQuery += "       heure_fauteuil, ";
                selectQuery += "       heure_salleattente, ";
                selectQuery += "       heure_secretariat, ";
                selectQuery += "       heure_sorti, ";
                selectQuery += "       isexported, ";
                selectQuery += "       faut_utilise, ";
                selectQuery += "       localisation, ";
                selectQuery += "       ID_COMMCLINIQUE, ";

                selectQuery += "       rendez_vous.id_next_acte,";
                selectQuery += "       trim(personne.per_nom)||' '||trim(personne.per_prenom)  PATNAME";

                selectQuery += " from rendez_vous";
                selectQuery += " inner join personne on personne.Id_personne = rendez_vous.id_personne";
                selectQuery += " where rdv_date between current_date and (current_date+1)";
                selectQuery += " and ((rdv_arrivee>current_date) and (rdv_arrivee is not null))";
                selectQuery += " and ((heure_sorti<current_date) or (heure_sorti is null))";
                selectQuery += " order by heure_salleattente";


                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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


        public static void InsertRDVTrace(string comments, RHAppointment appointment, Utilisateur CreatedBy)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
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



                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id_rdv", appointment.Id);
                command.Parameters.AddWithValue("@id_utilisateur", CreatedBy.Id);
                command.Parameters.AddWithValue("@trace_date", DateTime.Now);
                command.Parameters.AddWithValue("@trace_comment", comments);

                command.ExecuteNonQuery();
                command.Transaction.Commit();



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



        public static void UpdateHeureSecretariatAppointment(RHAppointment appointment)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "update rendez_vous";
                selectQuery += "    set heure_secretariat = @heure_secretariat,";
                selectQuery += "    localisation = @localisation,";
                selectQuery += "    ID_NEXT_ACTE = @ID_NEXT_ACTE,";
                selectQuery += "    ID_COMMCLINIQUE = @ID_COMMCLINIQUE,";
                selectQuery += "    rdv_statut = @rdv_statut";
                selectQuery += "    where (id_rdv = @id_rdv)";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@rdv_statut", (int)appointment.Status);
                command.Parameters.AddWithValue("@localisation", (int)appointment.Localisation);
                command.Parameters.AddWithValue("@heure_secretariat", appointment.DateSecretariat == null ? DBNull.Value : (object)appointment.DateSecretariat.Value);
                command.Parameters.AddWithValue("@ID_NEXT_ACTE", appointment.NextActe == null ? DBNull.Value : (object)appointment.NextActe.id_acte);
                command.Parameters.AddWithValue("@id_rdv", appointment.Id);


                if (appointment.IdNextCommentaireClinique <= 0)
                    command.Parameters.AddWithValue("@ID_COMMCLINIQUE", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@ID_COMMCLINIQUE", appointment.IdNextCommentaireClinique);


                command.ExecuteNonQuery();
                command.Transaction.Commit();



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


        public static DataRow getAppointment(int IdRDV)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select ID_NEXT_ACTE,ID_COMMCLINIQUE, id_rdv, p.id_personne,rdv_date, rdv_duree, rdv_statut, rdv_arrivee, rdv_comm, rdv_quand, heure_fauteuil, heure_salleattente, heure_secretariat, heure_sorti, isexported, faut_utilise, localisation,";
                selectQuery += " id_adresse, id_util, id_caisse, adr_id_adresse,per_nom ||' '|| per_prenom patname, per_nom, per_nomjf, per_prenom, per_genre, per_secu, per_type, per_telprinc, per_teltrav1, per_teltrav2, per_telecopie, per_email, per_reception, per_notes, per_poste, p.pcom, per_adr1, per_adr2, per_ville, per_cpostal, per_adr1_prof, per_adr2_prof, per_cpostal_prof, per_ville_prof, profession, mutuelle, per_datnaiss, tuvous, poid, email2, gsm, icq, im1, im2, telsup0, telsup3, telsup4, telsup5, telsup6, telsup8, telsup10, telsup11, telsup12, telsup13, telsup14, telsup15, telsup16, telsup17, telsup18, indicetel1, indicetel2, indicetel3, indicetel4, email3, indiceemail, indiceadr, pays_dom, pays_trav, pers_titre, pers_siteweb, per_ville_naissance, per_pays_naissance, per_langue_parle, per_population_ref, nom_rep_image, oi_login, oi_mdp, oi_profil, oi_autorisation, categories, pref_com,";
                selectQuery += " f.id_fauteuil, faut_libelle,FAUT_UTILISE,";
                selectQuery += " a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono,";
                selectQuery += " PAT_NUMDOSSIER,";
                // selectQuery += " (select first 1 rdv_date from rendez_vous r3 where r3.rdv_date<r.rdv_date and r.id_personne=r3.id_personne order by r3.rdv_date desc) as PreviousRDV,";
                selectQuery += " (select first 1 rdv_date from rendez_vous r2 where r2.rdv_date>r.rdv_date and r.id_personne=r2.id_personne order by r2.rdv_date asc) as NextRDV";
                selectQuery += " from rendez_vous r";
                selectQuery += " inner join personne p on r.id_personne=p.id_personne";
                selectQuery += " inner join patient pa on p.id_personne=pa.id_personne";
                selectQuery += " inner join fauteuil f on r.id_fauteuil=f.id_fauteuil";
                selectQuery += " inner join actes a on r.id_acte=a.id_acte";
                selectQuery += " where r.id_rdv = @id_rdv";
                selectQuery += " order by rdv_date,r.id_fauteuil";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id_rdv", IdRDV);


                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                return dt.Rows.Count == 0 ? null : dt.Rows[0];

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

        public static DataTable getAppointments(int Idpatient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select ID_NEXT_ACTE,ID_COMMCLINIQUE, id_rdv, p.id_personne,rdv_date, rdv_duree, rdv_statut, rdv_arrivee, rdv_comm, rdv_quand, heure_fauteuil, heure_salleattente, heure_secretariat, heure_sorti, isexported, faut_utilise, localisation,";
                selectQuery += " id_adresse, id_util, id_caisse, adr_id_adresse, per_nom||' '|| per_prenom PATNAME, per_nom, per_nomjf, per_prenom, per_genre, per_secu, per_type, per_telprinc, per_teltrav1, per_teltrav2, per_telecopie, per_email, per_reception, per_notes, per_poste, p.pcom, per_adr1, per_adr2, per_ville, per_cpostal, per_adr1_prof, per_adr2_prof, per_cpostal_prof, per_ville_prof, profession, mutuelle, per_datnaiss, tuvous, poid, email2, gsm, icq, im1, im2, telsup0, telsup3, telsup4, telsup5, telsup6, telsup8, telsup10, telsup11, telsup12, telsup13, telsup14, telsup15, telsup16, telsup17, telsup18, indicetel1, indicetel2, indicetel3, indicetel4, email3, indiceemail, indiceadr, pays_dom, pays_trav, pers_titre, pers_siteweb, per_ville_naissance, per_pays_naissance, per_langue_parle, per_population_ref, nom_rep_image, oi_login, oi_mdp, oi_profil, oi_autorisation, categories, pref_com,";
                selectQuery += " f.id_fauteuil, faut_libelle,FAUT_UTILISE,";
                selectQuery += " a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono,";
                selectQuery += " PAT_NUMDOSSIER,";
                // selectQuery += " (select first 1 rdv_date from rendez_vous r3 where r3.rdv_date<r.rdv_date and r.id_personne=r3.id_personne order by r3.rdv_date desc) as PreviousRDV,";
                selectQuery += " (select first 1 rdv_date from rendez_vous r2 where r2.rdv_date>r.rdv_date and r.id_personne=r2.id_personne order by r2.rdv_date asc) as NextRDV";
                selectQuery += " from rendez_vous r";
                selectQuery += " inner join personne p on r.id_personne=p.id_personne";
                selectQuery += " inner join patient pa on p.id_personne=pa.id_personne";
                selectQuery += " inner join fauteuil f on r.id_fauteuil=f.id_fauteuil";
                selectQuery += " inner join actes a on r.id_acte=a.id_acte";
                selectQuery += " where p.id_personne = @Idpat";
                selectQuery += " order by rdv_date,r.id_fauteuil";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@Idpat", Idpatient);


                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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



        public static DataTable getCancelledAppointments(basePatient p_patient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select r.PER_ID_PERSONNE, -1 as ID_NEXT_ACTE, r.id_rdv, p.id_personne,r.rdv_date, r.rdv_duree, r.rdv_statut, r.rdv_arrivee, '' as rdv_comm, r.rdv_quand, null as heure_fauteuil, null as heure_salleattente, null as heure_secretariat, null as heure_sorti, null as isexported, null as faut_utilise, null as localisation,";
                selectQuery += " id_adresse, id_util, id_caisse, adr_id_adresse, per_nom||' '|| per_prenom PATNAME, per_nom, per_nomjf, per_prenom, per_genre, per_secu, per_type, per_telprinc, per_teltrav1, per_teltrav2, per_telecopie, per_email, per_reception, per_notes, per_poste, p.pcom, per_adr1, per_adr2, per_ville, per_cpostal, per_adr1_prof, per_adr2_prof, per_cpostal_prof, per_ville_prof, profession, mutuelle, per_datnaiss, tuvous, poid, email2, gsm, icq, im1, im2, telsup0, telsup3, telsup4, telsup5, telsup6, telsup8, telsup10, telsup11, telsup12, telsup13, telsup14, telsup15, telsup16, telsup17, telsup18, indicetel1, indicetel2, indicetel3, indicetel4, email3, indiceemail, indiceadr, pays_dom, pays_trav, pers_titre, pers_siteweb, per_ville_naissance, per_pays_naissance,per_langue_parle, per_population_ref, nom_rep_image, oi_login, oi_mdp, oi_profil, oi_autorisation, categories, pref_com,";
                selectQuery += " f.id_fauteuil, faut_libelle,null as  FAUT_UTILISE, a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono,";
                selectQuery += " PAT_NUMDOSSIER,";
                // selectQuery += " (select first 1 rdv_date from rendez_vous r3 where r3.rdv_date<r.rdv_date and r.id_personne=r3.id_personne order by r3.rdv_date desc) as PreviousRDV,";
                selectQuery += " (select first 1 rdv_date from rendez_vous r2 where r2.rdv_date>r.rdv_date and r.id_personne=r2.id_personne order by r2.rdv_date asc) as NextRDV";
                selectQuery += " from RENDEZ_VOUS_ANU r";
                selectQuery += " inner join personne p on r.id_personne=p.id_personne";
                selectQuery += " inner join patient pa on p.id_personne=pa.id_personne";
                selectQuery += " inner join fauteuil f on r.id_fauteuil=f.id_fauteuil";
                selectQuery += " inner join actes a on r.id_acte=a.id_acte";
                selectQuery += " where p.id_personne = @Idpat";
                selectQuery += " order by rdv_date,r.id_fauteuil";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@Idpat", p_patient.Id);


                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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



       


        
       





        public static DataTable GetDiagHistory(int PatientId, Historique.HistoFor hf)
        {
            if (connection == null) getConnection();

            string fieldname = "";

            switch (hf)
            {
                case Historique.HistoFor.Appareillage:
                    fieldname = "PAT_APPAREIL";
                    break;
                case Historique.HistoFor.Diagnostique:
                    fieldname = "PAT_DIAG";
                    break;
                case Historique.HistoFor.Objectif:
                    fieldname = "PAT_OBJECTIF_TRAIT";
                    break;
                case Historique.HistoFor.Traitement:
                    fieldname = "PAT_PLAN";
                    break;
            }

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select dte, ";
                selectQuery += "  fieldname,";
                selectQuery += "  text, ";
                selectQuery += "  latestflag, ";
                selectQuery += "  id_patient";
                selectQuery += "  from histo_pat_blobs";
                selectQuery += "  where id_patient=@idpat";
                selectQuery += "  and fieldname=@fieldname";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@idpat", PatientId);
                command.Parameters.AddWithValue("@fieldname", fieldname);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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



        #endregion

        #region Scenario Commentaires clinique

        public static DataTable GetScenariosCommClinique()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "        Libelle, ";
                selectQuery += "        NbSemestres, ";
                selectQuery += "        TypeTtmnt ";
                selectQuery += " from SCENARIOS_COMM";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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




        public static DataTable GetScenariosCommCliniqueDetails(ScenarioCommClinique scenar)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "        id_acte, ";
                selectQuery += "        commentaires,";
                selectQuery += "        commentairesafaire,";
                selectQuery += "        ID_SCENARIO,";
                selectQuery += "        NBJOURS,";
                selectQuery += "        NBMOIS,";
                selectQuery += "        num_semestre,";
                selectQuery += "        id_parentcomment,";
                selectQuery += "        ordre,";
                selectQuery += "        refdate";
                selectQuery += " from scenarios_comm_detail";
                selectQuery += " where ID_SCENARIO = @ID";
                selectQuery += " order by ID_SCENARIO asc,ordre asc";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@ID", scenar.Id);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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


        #endregion

        #region Commentaires clinique


        public static void DeleteCommentaire(CommClinique comm)
        {



            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "delete from  BASE_COMM_RADIOS";
                selectQuery += " where (ID_COMM = @id)";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@id", comm.Id);
                command.ExecuteNonQuery();

                selectQuery = "delete from  BASE_COMM_PHOTOS";
                selectQuery += " where (ID_COMM = @id)";

                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                selectQuery = "delete from  BASE_COMM_MAT";
                selectQuery += " where (ID_COMM = @id)";

                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                selectQuery = "delete from  BASE_COMM_AUTREPERS";
                selectQuery += " where (ID_COMM = @id)";

                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                selectQuery = "delete from  BASE_COMM_AEXTRAIRE";
                selectQuery += " where (ID_COMM = @id)";

                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                selectQuery = "delete from  BASE_COMM";
                selectQuery += " where (ID = @id)";

                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                command.Transaction.Commit();



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


        public static void UpdateCommentaire(CommClinique comm)
        {



            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "update base_comm";
                selectQuery += " set id_praticien = @id_praticien,";
                selectQuery += "     id_assistante = @id_assistante,";
                selectQuery += "     id_secretaire = @id_secretaire,";
                selectQuery += "     id_acte = @id_acte,";
                selectQuery += "     id_rdv = @id_rdv,";
                selectQuery += "     id_patient = @id_patient,";
                selectQuery += "     hygiene = @hygiene,";
                selectQuery += "     nbJours = @nbJours,";
                selectQuery += "     nbMois = @nbMois,";
                selectQuery += "     modecreation = @modecreation,";
                selectQuery += "     Id_Scenario = @Id_Scenario,";
                selectQuery += "     Etat = @Etat,";
                selectQuery += "     DATEPREVISIONNELLE = @DATEPREVISIONNELLE,";
                selectQuery += "     Id_Semestre = @Id_Semestre,";
                selectQuery += "     date_comm = @date_comm,";
                selectQuery += "     commentaires = @commentaires,";
                selectQuery += "     commentairesafaire = @commentairesafaire,";
                selectQuery += "     IsDateDeRef = @IsDateDeRef,";
                selectQuery += "     Id_ParentComment = @Id_ParentComment";
                selectQuery += " where (id = @id)";




                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;


                command.Parameters.AddWithValue("@id", comm.Id);
                command.Parameters.AddWithValue("@id_praticien", comm.praticien == null ? DBNull.Value : (object)comm.praticien.Id);
                command.Parameters.AddWithValue("@id_assistante", comm.Assistante == null ? DBNull.Value : (object)comm.Assistante.Id);
                command.Parameters.AddWithValue("@id_secretaire", comm.Secretaire == null ? DBNull.Value : (object)comm.Secretaire.Id);
                command.Parameters.AddWithValue("@id_acte", comm.Acte == null ? DBNull.Value : (object)comm.Acte.id_acte);
                command.Parameters.AddWithValue("@id_rdv", comm.Appointement == null ? DBNull.Value : (object)comm.Appointement.Id);
                command.Parameters.AddWithValue("@id_patient", comm.IdPatient);
                command.Parameters.AddWithValue("@nbJours", comm.NbJours);
                command.Parameters.AddWithValue("@nbMois", comm.NbMois); command.Parameters.AddWithValue("@modecreation", comm.modecreation);
                command.Parameters.AddWithValue("@modecreation", comm.modecreation);
                command.Parameters.AddWithValue("@Id_Scenario", comm.IdScenario);
                command.Parameters.AddWithValue("@Etat", comm.etat);
                command.Parameters.AddWithValue("@Id_Semestre", comm.IdSemestre);
                command.Parameters.AddWithValue("@hygiene", comm.Hygiene);
                command.Parameters.AddWithValue("@date_comm", comm.date == null ? DBNull.Value : (object)comm.date.Value);
                command.Parameters.AddWithValue("@commentaires", comm.Commentaire);
                command.Parameters.AddWithValue("@commentairesafaire", comm.CommentaireAFaire);
                command.Parameters.AddWithValue("@Id_ParentComment", comm.IdParentComment);
                command.Parameters.AddWithValue("@IsDateDeRef", comm.IsDateDeRef);
                command.Parameters.AddWithValue("@DATEPREVISIONNELLE", comm.DatePrevisionnnelle);



                command.ExecuteNonQuery();
                command.Transaction.Commit();



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

        public static void InsertCommentaire(CommClinique comm)
        {



            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "select MAX(id)+1 as NEWID from base_comm";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                object obj = command.ExecuteScalar();

                if (obj == DBNull.Value) comm.Id = 1; else comm.Id = Convert.ToInt32(obj);


                selectQuery = "insert into base_comm (id, ";
                selectQuery += "                        id_praticien, ";
                selectQuery += "                        id_assistante, ";
                selectQuery += "                        id_secretaire, ";
                selectQuery += "                        id_acte, ";
                selectQuery += "                        id_rdv, ";
                selectQuery += "                        id_patient, ";
                selectQuery += "                        nbJours, ";
                selectQuery += "                        nbMois, ";
                selectQuery += "                        modecreation, ";
                selectQuery += "                        DATEPREVISIONNELLE, ";

                selectQuery += "                        Id_Scenario, ";
                selectQuery += "                        Etat, ";
                selectQuery += "                        Id_Semestre, ";
                selectQuery += "                        hygiene, ";
                selectQuery += "                        date_comm, ";
                selectQuery += "                        commentairesafaire, ";
                selectQuery += "                        Id_ParentComment,";
                selectQuery += "                        IsDateDeRef,";
                selectQuery += "                        commentaires)";
                selectQuery += " values (@id, ";
                selectQuery += "         @id_praticien, ";
                selectQuery += "         @id_assistante, ";
                selectQuery += "         @id_secretaire, ";
                selectQuery += "         @id_acte, ";
                selectQuery += "         @id_rdv, ";
                selectQuery += "         @id_patient, ";
                selectQuery += "         @nbJours, ";
                selectQuery += "         @nbMois, ";
                selectQuery += "         @modecreation, ";
                selectQuery += "         @DATEPREVISIONNELLE, ";

                selectQuery += "         @IdScenario, ";
                selectQuery += "         @Etat, ";
                selectQuery += "         @IdSemestre, ";
                selectQuery += "         @hygiene, ";
                selectQuery += "         @date_comm, ";
                selectQuery += "         @commentairesafaire, ";
                selectQuery += "         @Id_ParentComment,";
                selectQuery += "         @IsDateDeRef,";

                selectQuery += "         @commentaires)";



                command.CommandText = selectQuery;

                command.Parameters.AddWithValue("@id", comm.Id);
                command.Parameters.AddWithValue("@id_praticien", comm.IdPraticien);
                command.Parameters.AddWithValue("@id_assistante", comm.IdAssistante);
                command.Parameters.AddWithValue("@id_secretaire", comm.IdSecretaire);
                command.Parameters.AddWithValue("@id_acte", comm.IdActe);
                command.Parameters.AddWithValue("@id_rdv", comm.IdRDV);
                command.Parameters.AddWithValue("@id_patient", comm.IdPatient);
                command.Parameters.AddWithValue("@nbJours", comm.NbJours);
                command.Parameters.AddWithValue("@nbMois", comm.NbMois);
                command.Parameters.AddWithValue("@IdScenario", comm.IdScenario);
                command.Parameters.AddWithValue("@Etat", comm.etat);
                command.Parameters.AddWithValue("@IdSemestre", comm.IdSemestre);
                command.Parameters.AddWithValue("@hygiene", comm.Hygiene);
                command.Parameters.AddWithValue("@date_comm", comm.date == null ? DBNull.Value : (object)comm.date.Value);
                command.Parameters.AddWithValue("@commentaires", comm.Commentaire);
                command.Parameters.AddWithValue("@commentairesafaire", comm.CommentaireAFaire);
                command.Parameters.AddWithValue("@Id_ParentComment", comm.IdParentComment);
                command.Parameters.AddWithValue("@IsDateDeRef", comm.IsDateDeRef);
                command.Parameters.AddWithValue("@modecreation", comm.modecreation);
                command.Parameters.AddWithValue("@DATEPREVISIONNELLE", comm.DatePrevisionnnelle);

                command.ExecuteNonQuery();
                command.Transaction.Commit();



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

        public static DataRow GetCommClinique(int Id)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "        id_praticien, ";
                selectQuery += "        id_assistante, ";
                selectQuery += "        id_secretaire, ";
                selectQuery += "        id_acte, ";
                selectQuery += "        id_rdv, ";
                selectQuery += "        Hygiene, ";
                selectQuery += "        id_patient, ";
                selectQuery += "        date_comm, ";
                selectQuery += "        NBJours, ";
                selectQuery += "        NBMois, ";
                selectQuery += "        ID_SCENARIO, ";
                selectQuery += "        Etat, ";
                selectQuery += "        DATEPREVISIONNELLE, ";

                selectQuery += "        ISDATEDEREF, ";
                selectQuery += "        modecreation, ";
                selectQuery += "        Id_Semestre, ";
                selectQuery += "        commentaires,";
                selectQuery += "        Id_ParentComment,";
                selectQuery += "        commentairesafaire";
                selectQuery += " from base_comm";
                selectQuery += " where base_comm.id=@id";


                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id", Id);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];
                return dt.Rows.Count > 0 ? dt.Rows[0] : null;

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


        public static DataTable GetCommClinique(basePatient pat)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "        id_praticien, ";
                selectQuery += "        id_assistante, ";
                selectQuery += "        id_secretaire, ";
                selectQuery += "        id_acte, ";
                selectQuery += "        id_rdv, ";
                selectQuery += "        Hygiene, ";
                selectQuery += "        id_patient, ";
                selectQuery += "        date_comm, ";
                selectQuery += "        NBJours, ";
                selectQuery += "        NBMois, ";
                selectQuery += "        ID_SCENARIO, ";
                selectQuery += "        Etat, ";
                selectQuery += "        DATEPREVISIONNELLE, ";
                selectQuery += "        ISDATEDEREF, ";
                selectQuery += "        Id_Semestre, ";
                selectQuery += "        modecreation, ";

                selectQuery += "        commentaires,";
                selectQuery += "        Id_ParentComment,";
                selectQuery += "        commentairesafaire";
                selectQuery += " from ";


                selectQuery += " (select * from";

                selectQuery += " (";

                selectQuery += " select 1 orderCol,id,ISDATEDEREF,modecreation,dateprevisionnelle,         id_praticien,         id_assistante,         id_secretaire,         id_acte,         id_rdv,         Hygiene,         id_patient,         date_comm,         NBJours,         NBMois,         ID_SCENARIO,Etat,         Id_Semestre,         commentaires,Id_ParentComment,        commentairesafaire";
                selectQuery += " from base_comm";
                selectQuery += " where id_patient = @IDPAT and date_comm is not null";
                selectQuery += " order by date_comm desc";

                selectQuery += " )";
                selectQuery += " union";
                selectQuery += " select * from (";

                selectQuery += " select 2 orderCol,id, ISDATEDEREF,modecreation,dateprevisionnelle,        id_praticien,         id_assistante,         id_secretaire,         id_acte,         id_rdv,         Hygiene,         id_patient,         date_comm,         NBJours,         NBMois,         ID_SCENARIO,Etat,         Id_Semestre,         commentaires,Id_ParentComment,        commentairesafaire";
                selectQuery += " from base_comm";
                selectQuery += " where id_patient = @IDPAT and date_comm is null";

                selectQuery += " ))";


                selectQuery += "order by orderCol,date_comm desc,(30*NBMOIS)+NBJOURS asc,Id_ParentComment asc";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@IDPAT", pat.Id);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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




        public static DataTable GetCommCliniqueMateriels(CommClinique com)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id_comm, ";
                selectQuery += "        id_baseproduit, ";
                selectQuery += "        libelle, ";
                selectQuery += "        qte, ";
                selectQuery += "        shortlib";
                selectQuery += " from base_comm_mat";

                selectQuery += " where id_comm = @IDCOMM";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@IDCOMM", com.Id);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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

        public static DataTable GetCommCliniqueRadios(CommClinique com)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id_comm, ";
                selectQuery += "        typeradio ";
                selectQuery += " from base_comm_radios";

                selectQuery += " where id_comm = @IDCOMM";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@IDCOMM", com.Id);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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

        public static DataTable GetCommCliniqueScenarMateriels(CommCliniqueDetailsScenario com)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id_comm, ";
                selectQuery += "        id_baseproduit, ";
                selectQuery += "        libelle, ";
                selectQuery += "        qte, ";
                selectQuery += "        shortlib";
                selectQuery += " from BASE_SCENAR_COMM_MAT";

                selectQuery += " where id_comm = @IDCOMM";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@IDCOMM", com.Id);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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

        public static DataTable GetCommCliniqueScenarRadios(CommCliniqueDetailsScenario com)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id_comm, ";
                selectQuery += "        typeradio ";
                selectQuery += " from BASE_SCENAR_COMM_RADIOS";

                selectQuery += " where id_comm = @IDCOMM";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@IDCOMM", com.Id);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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


        public static DataTable GetCommCliniqueScenarPhotos(CommCliniqueDetailsScenario com)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id_comm, ";
                selectQuery += "        typephoto ";
                selectQuery += " from BASE_SCENAR_COMM_PHOTOS";

                selectQuery += " where id_comm = @IDCOMM";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@IDCOMM", com.Id);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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


        public static DataTable GetCommCliniquePhotos(CommClinique com)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id_comm, ";
                selectQuery += "        typephoto ";
                selectQuery += " from BASE_COMM_PHOTOS";

                selectQuery += " where id_comm = @IDCOMM";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@IDCOMM", com.Id);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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

        public static DataTable GetCommCliniqueAutrePersonne(CommClinique com)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id_comm, ";
                selectQuery += "        ID_CORRESPONDANT, ";
                selectQuery += "        personne.per_nom , ";
                selectQuery += "        personne.per_prenom  ";
                selectQuery += " from BASE_COMM_AUTREPERS";
                selectQuery += " inner join personne on personne.ID_PERSONNE=ID_CORRESPONDANT";

                selectQuery += " where id_comm = @IDCOMM";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@IDCOMM", com.Id);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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

        public static DataTable GetCommCliniqueDentAExtraire(CommClinique com)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id_comm, ";
                selectQuery += "        DENTS ";
                selectQuery += " from BASE_COMM_AEXTRAIRE";

                selectQuery += " where id_comm = @IDCOMM";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@IDCOMM", com.Id);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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

        public static void setComCliniqueDentsAExtraire(CommClinique comm)
        {

            if (comm.DentsAExtraire == null) return;

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "";
                FbCommand command = new FbCommand(selectQuery, connection, transaction);


                selectQuery = "delete from BASE_COMM_AEXTRAIRE where ID_COMM = @id";

                command.Parameters.AddWithValue("@id", comm.Id);
                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                object obj = command.ExecuteNonQuery();



                selectQuery = "insert into BASE_COMM_AEXTRAIRE (id_comm, ";
                selectQuery += "                            DENTS)";
                selectQuery += " values (@id_comm, ";
                selectQuery += "         @DENTS)";





                command.CommandText = selectQuery;

                foreach (CommDentAExtraire cr in comm.DentsAExtraire)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_comm", comm.Id);
                    command.Parameters.AddWithValue("@DENTS", cr.dents);

                    command.ExecuteNonQuery();
                }


                command.Transaction.Commit();



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


        public static void setComCliniqueAutrePersonnes(CommClinique comm)
        {

            if (comm.AutrePersonnes == null) return;

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "";
                FbCommand command = new FbCommand(selectQuery, connection, transaction);


                selectQuery = "delete from BASE_COMM_AUTREPERS where ID_COMM = @id";

                command.Parameters.AddWithValue("@id", comm.Id);
                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                object obj = command.ExecuteNonQuery();



                selectQuery = "insert into BASE_COMM_AUTREPERS (id_comm, ";
                selectQuery += "                            ID_CORRESPONDANT)";
                selectQuery += " values (@id_comm, ";
                selectQuery += "         @ID_CORRESPONDANT)";





                command.CommandText = selectQuery;

                foreach (CommAutrePersonne cr in comm.AutrePersonnes)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_comm", comm.Id);
                    command.Parameters.AddWithValue("@ID_CORRESPONDANT", cr.IdCorrespondant);

                    command.ExecuteNonQuery();
                }


                command.Transaction.Commit();



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

        public static void setComCliniqueMateriels(CommClinique comm)
        {

            if (comm.Materiels == null) return;

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "";
                FbCommand command = new FbCommand(selectQuery, connection, transaction);


                selectQuery = "delete from BASE_COMM_MAT where ID_COMM = @id";

                command.Parameters.AddWithValue("@id", comm.Id);
                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                object obj = command.ExecuteNonQuery();



                selectQuery = "insert into base_comm_mat (id_comm, ";
                selectQuery += "                            id_baseproduit, ";
                selectQuery += "                            libelle, ";
                selectQuery += "                            qte, ";
                selectQuery += "                            shortlib)";
                selectQuery += " values (@id_comm, ";
                selectQuery += "         @id_baseproduit, ";
                selectQuery += "         @libelle, ";
                selectQuery += "         @qte, ";
                selectQuery += "         @shortlib)";





                command.CommandText = selectQuery;

                foreach (CommMateriel cr in comm.Materiels)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_comm", comm.Id);
                    command.Parameters.AddWithValue("@id_baseproduit", cr.IdBaseProduit);
                    command.Parameters.AddWithValue("@libelle", cr.Libelle);
                    command.Parameters.AddWithValue("@qte", cr.Qte);
                    command.Parameters.AddWithValue("@shortlib", cr.ShortLib);

                    command.ExecuteNonQuery();
                }


                command.Transaction.Commit();



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

        public static void setComCliniquePhotos(CommClinique comm)
        {


            if (comm.photos == null) return;

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "";
                FbCommand command = new FbCommand(selectQuery, connection, transaction);


                selectQuery = "delete from BASE_COMM_PHOTOS where ID_COMM = @id";

                command.Parameters.AddWithValue("@id", comm.Id);
                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                object obj = command.ExecuteNonQuery();



                selectQuery = "insert into BASE_COMM_PHOTOS (id_comm, ";
                selectQuery += "                               typephoto)";
                selectQuery += " values (@id_comm, ";
                selectQuery += "         @typephoto)";




                command.CommandText = selectQuery;

                foreach (CommPhoto cr in comm.photos)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_comm", comm.Id);
                    command.Parameters.AddWithValue("@typephoto", cr.typephoto);

                    command.ExecuteNonQuery();
                }


                command.Transaction.Commit();



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

        public static void setComCliniqueRadio(CommClinique comm)
        {

            if (comm.Radios == null) return;

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "";
                FbCommand command = new FbCommand(selectQuery, connection, transaction);


                selectQuery = "delete from BASE_COMM_RADIOS where ID_COMM = @id";

                command.Parameters.AddWithValue("@id", comm.Id);
                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                object obj = command.ExecuteNonQuery();



                selectQuery = "insert into base_comm_radios (id_comm, ";
                selectQuery += "                               typeradio)";
                selectQuery += " values (@id_comm, ";
                selectQuery += "         @typeradio)";




                command.CommandText = selectQuery;

                foreach (CommRadio cr in comm.Radios)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_comm", comm.Id);
                    command.Parameters.AddWithValue("@typeradio", cr.typeradio);

                    command.ExecuteNonQuery();
                }


                command.Transaction.Commit();



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


        #endregion

        #region Utilisateurs




        #endregion

        #region Alertes

        public static DataTable getAlertes()
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "select id, ";
                selectQuery += "       typealerte, ";
                selectQuery += "       requete, ";
                selectQuery += "       libelle, ";
                selectQuery += "       seuil_haut, ";
                selectQuery += "       seuil_bas, ";
                selectQuery += "       message_alerte";
                selectQuery += " from base_alerts";



                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();



                return ds.Tables[0];



            }
            catch (System.IndexOutOfRangeException e)
            {
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

        public static object ExecuteAlerte(Alert alert, basePatient patient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = alert.requete;

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                if ((patient != null) && (alert.typeAlerte == Alert.TypeAlerte.Patient))
                    command.Parameters.AddWithValue("@IdPATIENT", patient.Id);


                return command.ExecuteScalar();

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

        #region Plan de gestion


        #region Facture/echeances








        public static Double GetRestantDue(basePatient patient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select sum(BASE_ECHEANCE.MONTANT) ";
                selectQuery += " from BASE_ECHEANCE";
                selectQuery += " inner join BASE_TRAITEMENT on BASE_TRAITEMENT.ID = BASE_ECHEANCE.ID_TRAITEMENT and BASE_TRAITEMENT.ID_PATIENT = @id_patient";
                selectQuery += " left outer join base_encaissement on BASE_ECHEANCE.id_encaissement = base_encaissement.id";
                selectQuery += " where base_encaissement.id is null";


                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id_patient", patient.Id);


                object obj = command.ExecuteScalar();
                if (obj == DBNull.Value)
                    return 0;

                return Convert.ToDouble(obj);

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


        public static double GetSoldeAReglerAvantLe(DateTime dte, int IdPatient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {




                string selectQuery = "select";
                selectQuery += " (";
                selectQuery += " select sum(BASE_ECHEANCE.MONTANT)";
                selectQuery += " from BASE_ECHEANCE";
                selectQuery += " where BASE_ECHEANCE.MONTANT is not null and BASE_ECHEANCE.id_patient = @id_patient and BASE_ECHEANCE.DTEECHEANCE<@dte and (BASE_ECHEANCE.PARPRELEVEMENT <>'True' or (PARPRELEVEMENT is null))";
                selectQuery += " )-";
                selectQuery += " (";
                selectQuery += " select sum(base_encaissement.MONTANT)";
                selectQuery += " from base_encaissement";
                selectQuery += " inner join BASE_PAIEMENTREEL on BASE_PAIEMENTREEL.ID=base_encaissement.ID_PAIEMENT_REEL";
                selectQuery += " where base_encaissement.MONTANT is not NULL and BASE_PAIEMENTREEL.dateencaissement<@dte and base_encaissement.id_patient = @id_patient";
                selectQuery += " )";
                selectQuery += " from RDB$DATABASE";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id_patient", IdPatient);
                command.Parameters.AddWithValue("@dte", dte);

                object obj = command.ExecuteScalar();

                if ((obj == null) || (obj is DBNull)) return 0;
                else return Convert.ToInt32(obj);

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

        public static DataTable GetEcheancesAReglerAvantLe(DateTime dte, int IdPatient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select BASE_ECHEANCE.ID, ";
                selectQuery += "        BASE_ECHEANCE.ID_TRAITEMENT, ";
                selectQuery += "        BASE_ECHEANCE.MONTANT, ";
                selectQuery += "        BASE_ECHEANCE.DTEECHEANCE, ";
                selectQuery += "        BASE_ECHEANCE.LIBELLE, ";
                selectQuery += "        BASE_ECHEANCE.ID_PATIENT, ";
                selectQuery += "        BASE_ECHEANCE.PARPRELEVEMENT, ";
                selectQuery += "        BASE_ECHEANCE.ID_MUTUELLE,";
                selectQuery += "        BASE_ECHEANCE.TYPEPAYEUR,";

                selectQuery += "        base_encaissement.id as ID_ENCAISSEMENT ";

                selectQuery += " from BASE_ECHEANCE";
                selectQuery += " inner join BASE_TRAITEMENT on BASE_TRAITEMENT.ID = BASE_ECHEANCE.ID_TRAITEMENT and BASE_TRAITEMENT.ID_PATIENT = @id_patient";
                selectQuery += " left outer join base_encaissement on BASE_ECHEANCE.id_encaissement = base_encaissement.id";
                selectQuery += " where BASE_ECHEANCE.DTEECHEANCE<@dte and (BASE_ECHEANCE.PARPRELEVEMENT <>'True' or (PARPRELEVEMENT is null)) and base_encaissement.id is null";
                selectQuery += " order by DTEECHEANCE";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id_patient", IdPatient);
                command.Parameters.AddWithValue("@dte", dte);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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


        public static DataRow GetNextEcheances(DateTime dte, int IdPatient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select BASE_ECHEANCE.ID, ";
                selectQuery += "        BASE_ECHEANCE.ID_TRAITEMENT, ";
                selectQuery += "        BASE_ECHEANCE.MONTANT, ";
                selectQuery += "        BASE_ECHEANCE.DTEECHEANCE, ";
                selectQuery += "        BASE_ECHEANCE.LIBELLE, ";
                selectQuery += "        BASE_ECHEANCE.ID_PATIENT, ";
                selectQuery += "        BASE_ECHEANCE.PARPRELEVEMENT, ";
                selectQuery += "        BASE_ECHEANCE.ID_MUTUELLE,";
                selectQuery += "        BASE_ECHEANCE.TYPEPAYEUR,";

                selectQuery += "        base_encaissement.id as ID_ENCAISSEMENT ";

                selectQuery += " from BASE_ECHEANCE";
                selectQuery += " inner join BASE_TRAITEMENT on BASE_TRAITEMENT.ID = BASE_ECHEANCE.ID_TRAITEMENT and BASE_TRAITEMENT.ID_PATIENT = @id_patient";
                selectQuery += " left outer join base_encaissement on BASE_ECHEANCE.id_encaissement = base_encaissement.id";
                selectQuery += " where BASE_ECHEANCE.DTEECHEANCE>@dte and (BASE_ECHEANCE.PARPRELEVEMENT <>'True' or (PARPRELEVEMENT is null)) and base_encaissement.id is null";
                selectQuery += " order by DTEECHEANCE";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id_patient", IdPatient);
                command.Parameters.AddWithValue("@dte", dte);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

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

        public static DateTime? GetNextDEP(int IdPatient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select min(BASE_TRAITEMENT.date_debut) from BASE_TRAITEMENT";
                selectQuery += " where base_traitement.id_patient = @idpatient";
                selectQuery += " and base_traitement.need_dep = 1";
                selectQuery += " and BASE_TRAITEMENT.id_dep is null";
                selectQuery += " and BASE_TRAITEMENT.date_debut>current_date";

                selectQuery += "        base_encaissement.id as ID_ENCAISSEMENT ";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id_patient", IdPatient);

                object obj = command.ExecuteScalar();

                if (obj == null) return null;
                else return Convert.ToDateTime(obj);



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

        public static DateTime? GetNextFS(int IdPatient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select min(BASE_TRAITEMENT.date_debut) from BASE_TRAITEMENT";
                selectQuery += " where base_traitement.id_patient = @idpatient";
                selectQuery += " and base_traitement.need_fse = 1";
                selectQuery += " and BASE_TRAITEMENT.id_fs is null";
                selectQuery += " and BASE_TRAITEMENT.date_debut>current_date";

                selectQuery += "        base_encaissement.id as ID_ENCAISSEMENT ";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id_patient", IdPatient);

                object obj = command.ExecuteScalar();

                if (obj == null) return null;
                else return Convert.ToDateTime(obj);



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

        public static DateTime? GetCurrentDEP(int IdPatient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select max(BASE_TRAITEMENT.date_debut) from BASE_TRAITEMENT";
                selectQuery += " where base_traitement.id_patient = @idpatient";
                selectQuery += " and base_traitement.need_dep = 1";
                selectQuery += " and BASE_TRAITEMENT.id_dep is null";
                selectQuery += " and BASE_TRAITEMENT.date_debut<current_date";

                selectQuery += "        base_encaissement.id as ID_ENCAISSEMENT ";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id_patient", IdPatient);

                object obj = command.ExecuteScalar();

                if (obj == null) return null;
                else return Convert.ToDateTime(obj);



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

        public static DateTime? GetCurrentFS(int IdPatient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select max(BASE_TRAITEMENT.date_debut) from BASE_TRAITEMENT";
                selectQuery += " where base_traitement.id_patient = @idpatient";
                selectQuery += " and base_traitement.need_fse = 1";
                selectQuery += " and BASE_TRAITEMENT.id_fs is null";
                selectQuery += " and BASE_TRAITEMENT.date_debut<current_date";

                selectQuery += "        base_encaissement.id as ID_ENCAISSEMENT ";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id_patient", IdPatient);

                object obj = command.ExecuteScalar();

                if (obj == null) return null;
                else return Convert.ToDateTime(obj);



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


        #endregion


        #region Encaissement


        #endregion


        #region Plan de gestion


        public static DataTable getTypesPG()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "        code, ";
                selectQuery += "        libelle ";
                selectQuery += " from BASE_TYPEPLANGESTION";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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

        public static DataTable getPlanDeGestion()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select CODEPLAN, ";
                selectQuery += "        libelle ";
                selectQuery += " from BASE_PLANTITRE";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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

        public static DataTable getDetailsPlanDeGestion(int IdPlan)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select ID_ACTE, ";
                selectQuery += "        ORDRE, ";
                selectQuery += "        ID_CODEPLAN, ";
                selectQuery += "        LIBELLE ";
                selectQuery += " from BASE_PLANDETAIL ";
                selectQuery += " where BASE_plandetail.ID_CODEPLAN = @IdPlan";
                selectQuery += " order by ID_CODEPLAN,ORDRE asc";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@IdPlan", IdPlan);
                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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



        #region ActePG
        public static void DecalerSemestre(DateTime APartirDe, Traitement traitmnt, int NbDaysToMove)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "update BASE_SEMESTRE set ";
                selectQuery += " DATEDEBUT = DATEDEBUT + " + NbDaysToMove.ToString() + ",";
                selectQuery += " DATEFIN = DATEFIN + " + NbDaysToMove.ToString();
                selectQuery += " where  ID_TRAITEMENT = @idTrmnt and DATEDEBUT > @APartirDe";



                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@idTrmnt", traitmnt.Id);
                command.Parameters.AddWithValue("@APartirDe", APartirDe);

                command.CommandType = CommandType.Text;

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







        public static int getIdSurveillance(ActePG act)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select IDSEM_PTA ";
                selectQuery += " from base_traitement";
                selectQuery += " where ID = @ID ";
                selectQuery += " order by date_debut";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@ID", act.Id);


                object obj = command.ExecuteScalar();
                if (obj != null) return Convert.ToInt32(obj);

                return -1;

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








        public static DataRow getFirstActesPG(int IdPatient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select first 1 id, ";
                selectQuery += "       id_patient, ";
                selectQuery += "       date_debut, ";
                selectQuery += "       nb_jours, ";
                selectQuery += "       nb_mois, ";
                selectQuery += "       id_plan, ";
                selectQuery += "       tarif, ";
                selectQuery += "       id_actegestion, ";
                selectQuery += "       code, ";
                selectQuery += "       libelle, ";
                selectQuery += "       id_prestation, ";
                selectQuery += "       coeff, ";
                selectQuery += "       montant, ";
                selectQuery += "       need_dep, ";
                selectQuery += "       need_fse, ";
                selectQuery += "       motifdepassement, ";
                selectQuery += "       ald, ";
                selectQuery += "       nbkilometre, ";
                selectQuery += "       rno, ";
                selectQuery += "       lieuexecution, ";
                selectQuery += "       exoneration, ";
                selectQuery += "       dimancheetjf, ";
                selectQuery += "       nuit, ";
                selectQuery += "       numdent, ";
                selectQuery += "       accident, ";
                selectQuery += "       dateaccident,";
                selectQuery += "       ID_DEP, ";
                selectQuery += "       ID_FS, ";
                selectQuery += "       NUM_SEMESTRE, ";
                selectQuery += "       NUM_CONTENTION, ";
                selectQuery += "       ISDECOMPOSED, ";
                selectQuery += "       DECOMPOSED, ";
                selectQuery += "       SaleDate, ";
                selectQuery += "       IDSEM_PTA, ";
                selectQuery += "       NumMutuelle, ";
                selectQuery += "       ActeCMU ";



                selectQuery += " from base_traitement";
                selectQuery += " where id_patient = @id_patient ";
                selectQuery += " order by date_debut asc";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id_patient", IdPatient);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

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

        public static DataRow getLastActesPG(int IdPatient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select first 1 id, ";
                selectQuery += "       id_patient, ";
                selectQuery += "       date_debut, ";
                selectQuery += "       nb_jours, ";
                selectQuery += "       nb_mois, ";
                selectQuery += "       id_plan, ";
                selectQuery += "       tarif, ";
                selectQuery += "       id_actegestion, ";
                selectQuery += "       code, ";
                selectQuery += "       libelle, ";
                selectQuery += "       id_prestation, ";
                selectQuery += "       coeff, ";
                selectQuery += "       montant, ";
                selectQuery += "       need_dep, ";
                selectQuery += "       need_fse, ";
                selectQuery += "       motifdepassement, ";
                selectQuery += "       ald, ";
                selectQuery += "       nbkilometre, ";
                selectQuery += "       rno, ";
                selectQuery += "       lieuexecution, ";
                selectQuery += "       exoneration, ";
                selectQuery += "       dimancheetjf, ";
                selectQuery += "       nuit, ";
                selectQuery += "       numdent, ";
                selectQuery += "       accident, ";
                selectQuery += "       dateaccident,";
                selectQuery += "       ID_DEP, ";
                selectQuery += "       ID_FS, ";
                selectQuery += "       NUM_SEMESTRE, ";
                selectQuery += "       NUM_CONTENTION, ";
                selectQuery += "       ISDECOMPOSED, ";
                selectQuery += "       DECOMPOSED, ";
                selectQuery += "       SaleDate, ";
                selectQuery += "       IDSEM_PTA, ";
                selectQuery += "       NumMutuelle, ";
                selectQuery += "       ActeCMU ";
                selectQuery += " from base_traitement";
                selectQuery += " where id_patient = @id_patient ";
                selectQuery += " order by date_debut desc";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id_patient", IdPatient);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

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

        public static DataRow GetPreviousActePG(int IdPatient, DateTime dte)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select first 1 id, ";
                selectQuery += "       id_patient, ";
                selectQuery += "       date_debut, ";
                selectQuery += "       nb_jours, ";
                selectQuery += "       nb_mois, ";
                selectQuery += "       id_plan, ";
                selectQuery += "       tarif, ";
                selectQuery += "       id_actegestion, ";
                selectQuery += "       code, ";
                selectQuery += "       libelle, ";
                selectQuery += "       id_prestation, ";
                selectQuery += "       coeff, ";
                selectQuery += "       montant, ";
                selectQuery += "       need_dep, ";
                selectQuery += "       need_fse, ";
                selectQuery += "       motifdepassement, ";
                selectQuery += "       ald, ";
                selectQuery += "       nbkilometre, ";
                selectQuery += "       rno, ";
                selectQuery += "       lieuexecution, ";
                selectQuery += "       exoneration, ";
                selectQuery += "       dimancheetjf, ";
                selectQuery += "       nuit, ";
                selectQuery += "       numdent, ";
                selectQuery += "       accident, ";
                selectQuery += "       dateaccident,";
                selectQuery += "       ID_DEP, ";
                selectQuery += "       ID_FS, ";
                selectQuery += "       NUM_SEMESTRE, ";
                selectQuery += "       NUM_CONTENTION, ";
                selectQuery += "       ISDECOMPOSED, ";
                selectQuery += "       DECOMPOSED, ";
                selectQuery += "       SaleDate, ";
                selectQuery += "       IDSEM_PTA, ";
                selectQuery += "       NumMutuelle, ";
                selectQuery += "       ActeCMU ";
                selectQuery += " from base_traitement";
                selectQuery += " where id_patient = @id_patient and date_debut<@dte";
                selectQuery += " order by date_debut desc";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id_patient", IdPatient);
                command.Parameters.AddWithValue("@dte", dte);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

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


        public static List<int> getIdSemestres(PoseAppareil pa)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectquery = "select ID_SEMESTRE";
                selectquery += " from BASE_POSE_APP_SEMESTRE";
                selectquery += " where ID_POSE_APPAREIL = @id";


                FbCommand command = new FbCommand(selectquery, connection, transaction);
                command.Parameters.AddWithValue("@id", pa.Id);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                List<int> lst = new List<int>();

                foreach (DataRow dr in dt.Rows)
                    lst.Add(Convert.ToInt32(dr["ID_SEMESTRE"]));

                return lst;

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


        public static DataTable getPoseAppareils(Proposition proposition)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectquery = "select id, ";
                selectquery += "       id_proposition, ";
                selectquery += "       id_appareil";
                selectquery += " from base_pose_appareil";
                selectquery += " where id_proposition = @id";


                FbCommand command = new FbCommand(selectquery, connection, transaction);
                command.Parameters.AddWithValue("@id", proposition.Id);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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


        public static void AddPoseAppareil(PoseAppareil poseAppareil)
        {


            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQueryId = "select max(id)+1 as ID from BASE_POSE_APPAREIL";
                FbCommand commandid = new FbCommand(selectQueryId, connection, transaction);
                object obj = commandid.ExecuteScalar();
                if (obj == DBNull.Value)
                    poseAppareil.Id = 1;
                else
                    poseAppareil.Id = Convert.ToInt32(obj);

                string selectquery = "insert into base_pose_appareil (id, ";
                selectquery += "                                id_proposition, ";
                selectquery += "                                id_appareil)";
                selectquery += " values (@id, ";
                selectquery += "        @id_proposition, ";
                selectquery += "        @id_appareil)";




                FbCommand command = new FbCommand(selectquery, connection, transaction);


                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", poseAppareil.Id);
                command.Parameters.AddWithValue("@id_proposition", poseAppareil.Parent.Id);
                command.Parameters.AddWithValue("@id_appareil", poseAppareil.appareil.Id);


                command.ExecuteNonQuery();

                foreach (Semestre s in poseAppareil.semestres)
                {
                    string selectquerysub = "insert into base_pose_app_semestre (id_pose_appareil, ";
                    selectquerysub += "                                    id_semestre)";
                    selectquerysub += "values (@id_pose_appareil, ";
                    selectquerysub += "        @id_semestre)";


                    FbCommand commandsub = new FbCommand(selectquerysub, connection, transaction);
                    commandsub.Parameters.AddWithValue("@id_pose_appareil", poseAppareil.Id);
                    commandsub.Parameters.AddWithValue("@id_semestre", s.Id);

                    commandsub.ExecuteNonQuery();

                }


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



        #endregion


        #region Feuilles de Soin




        #endregion


        #endregion




        public static DataTable GetTierpayants()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = " select ID_CAISSE, ";
                selectQuery += "        ID_COSESECU, ";
                selectQuery += "        POURCENTAGE ";
                selectQuery += " from TIERS";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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















        #endregion





        #region Plan de traitement



        #endregion

        #region Commentaires Historisables



        public static DataTable getCommentHistoInScenarios()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = " select BASE_SCEN_COMMENT_DETAILS.ID, ";
                selectQuery += "        BASE_SCEN_COMMENT_DETAILS.TYPESCENARIO, ";
                selectQuery += "        BASE_SCEN_COMMENT_DETAILS.CODE, ";
                selectQuery += "        BASE_SCEN_COMMENT_DETAILS.IMPORTANCE, ";
                selectQuery += "        BASE_SCEN_COMMENT_DETAILS.PARENT, ";
                selectQuery += "        BASE_SCEN_COMMENT_DETAILS.ORDRE, ";
                selectQuery += "        BASE_SCEN_COMMENT_DETAILS.COMMENTAIRE, ";
                selectQuery += "        BASE_SCEN_COMMENT_DETAILS.ID_SCENARIO, ";
                selectQuery += "        BASE_SCENARIOS_COMMENT.LIBELLE, ";
                selectQuery += "        BASE_SCENARIOS_COMMENT.TYPE_COMMENT TYPE_SCENARIO ";
                selectQuery += " from BASE_SCEN_COMMENT_DETAILS";
                selectQuery += " inner join BASE_SCENARIOS_COMMENT on BASE_SCENARIOS_COMMENT.ID=BASE_SCEN_COMMENT_DETAILS.ID_SCENARIO";
                selectQuery += " where ID_SCENARIO is not null";
                selectQuery += " order by BASE_SCEN_COMMENT_DETAILS.ID_SCENARIO, BASE_SCEN_COMMENT_DETAILS.ORDRE asc";


                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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


        public static DataTable GetCommentairesWithoutPatient()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = " select BASE_COMMENTS.ID, ";
                selectQuery += "        BASE_COMMENTS.ID_PATIENT, ";
                selectQuery += "        BASE_COMMENTS.TYPE_COMMENT, ";
                selectQuery += "        BASE_COMMENTS.DATE_COMMENT, ";
                selectQuery += "        BASE_COMMENTS.COMMENT, ";
                selectQuery += "        BASE_COMMENTS.CODECOMMENTAIRE, ";
                selectQuery += "        BASE_COMMENTS.IMPORTANCE, ";
                selectQuery += "        BASE_COMMENTS.PARENT, ";
                selectQuery += "        BASE_COMMENTS.DATEFIN, ";
                selectQuery += "        BASE_COMMENTS.ID_WRITER ";
                selectQuery += " from BASE_COMMENTS";
                selectQuery += " where ID_PATIENT is null";


                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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


        public static DataTable GetCommentaires(int idpat)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = " select BASE_COMMENTS.ID, ";
                selectQuery += "        BASE_COMMENTS.ID_PATIENT, ";
                selectQuery += "        BASE_COMMENTS.TYPE_COMMENT, ";
                selectQuery += "        BASE_COMMENTS.DATE_COMMENT, ";
                selectQuery += "        BASE_COMMENTS.COMMENT, ";
                selectQuery += "        BASE_COMMENTS.CODECOMMENTAIRE, ";
                selectQuery += "        BASE_COMMENTS.IMPORTANCE, ";
                selectQuery += "        BASE_COMMENTS.PARENT, ";
                selectQuery += "        BASE_COMMENTS.DATEFIN, ";
                selectQuery += "        BASE_COMMENTS.DATEDEBUT, ";
                selectQuery += "        BASE_COMMENTS.ID_WRITER ";
                selectQuery += " from BASE_COMMENTS";
                selectQuery += " where ID_PATIENT = @ID_PATIENT";


                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@ID_PATIENT", idpat);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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


        public static DataTable GettAllLastCommentaires(basePatient pat)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = " select BASE_COMMENTS.ID, ";
                selectQuery += "        nb_comments, ";
                selectQuery += "        BASE_COMMENTS.ID_PATIENT, ";
                selectQuery += "        BASE_COMMENTS.TYPE_COMMENT, ";
                selectQuery += "        BASE_COMMENTS.DATE_COMMENT, ";
                selectQuery += "        BASE_COMMENTS.COMMENT, ";
                selectQuery += "        BASE_COMMENTS.CODECOMMENTAIRE, ";
                selectQuery += "        BASE_COMMENTS.IMPORTANCE, ";
                selectQuery += "        BASE_COMMENTS.PARENT, ";
                selectQuery += "        BASE_COMMENTS.DATEFIN, ";
                selectQuery += "        BASE_COMMENTS.ID_WRITER ";
                selectQuery += " from BASE_COMMENTS";
                selectQuery += " inner join (";
                selectQuery += " select max(DATE_COMMENT) DATE_COMMENT,count(ID) nb_comments,TYPE_COMMENT";
                selectQuery += " from BASE_COMMENTS";
                selectQuery += " where ID_PATIENT = @ID_PATIENT";
                selectQuery += " group by TYPE_COMMENT";
                selectQuery += " ) lastthem on  lastthem.DATE_COMMENT= BASE_COMMENTS.date_comment and lastthem.TYPE_COMMENT= BASE_COMMENTS.TYPE_COMMENT";


                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@ID_PATIENT", pat.Id);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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


        public static DataTable GettAllCommentaires(basePatient pat)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = " select ID, ";
                selectQuery += "        ID_PATIENT, ";
                selectQuery += "        TYPE_COMMENT, ";
                selectQuery += "        DATE_COMMENT, ";
                selectQuery += "        COMMENT, ";
                selectQuery += "        CODECOMMENTAIRE, ";
                selectQuery += "        IMPORTANCE, ";
                selectQuery += "        PARENT, ";
                selectQuery += "        DATEFIN, ";
                selectQuery += "        ID_WRITER ";
                selectQuery += " from BASE_COMMENTS";
                selectQuery += " where ID_PATIENT = @ID_PATIENT";
                selectQuery += " order by DATE_COMMENT desc";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@ID_PATIENT", pat.Id);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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

        public static DataTable GettAllCommentaires(basePatient pat, CommentHisto.CommentHistoType type)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = " select ID, ";
                selectQuery += "        ID_PATIENT, ";
                selectQuery += "        TYPE_COMMENT, ";
                selectQuery += "        DATE_COMMENT, ";
                selectQuery += "        COMMENT, ";
                selectQuery += "        CODECOMMENTAIRE, ";
                selectQuery += "        IMPORTANCE, ";
                selectQuery += "        PARENT, ";
                selectQuery += "        DATEFIN, ";
                selectQuery += "        ID_WRITER ";
                selectQuery += " from BASE_COMMENTS";
                selectQuery += " where ID_PATIENT = @ID_PATIENT";
                selectQuery += " and TYPE_COMMENT = @TYPE_COMMENT";
                selectQuery += " order by DATE_COMMENT desc";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@ID_PATIENT", pat.Id);
                command.Parameters.AddWithValue("@TYPE_COMMENT", (int)type);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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

        public static DataRow GetLastCommentaire(basePatient pat, CommentHisto.CommentHistoType type)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = " select First 1 ID, ";
                selectQuery += "        ID_PATIENT, ";
                selectQuery += "        TYPE_COMMENT, ";
                selectQuery += "        DATE_COMMENT, ";
                selectQuery += "        COMMENT, ";
                selectQuery += "        CODECOMMENTAIRE, ";
                selectQuery += "        IMPORTANCE, ";
                selectQuery += "        PARENT, ";
                selectQuery += "        DATEFIN, ";
                selectQuery += "        ID_WRITER ";
                selectQuery += " from BASE_COMMENTS";
                selectQuery += " where ID_PATIENT = @ID_PATIENT";
                selectQuery += " and TYPE_COMMENT = @TYPE_COMMENT";
                selectQuery += " order by DATE_COMMENT desc";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@ID_PATIENT", pat.Id);
                command.Parameters.AddWithValue("@TYPE_COMMENT", (int)type);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count < 1) return null;

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


        public static void InsertCommentaires(CommentHisto value)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "select MAX(ID)+1 as NEWID from BASE_COMMENTS";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                object obj = command.ExecuteScalar();

                if (obj == DBNull.Value)
                    value.Id = 1;
                else
                    value.Id = Convert.ToInt32(obj);


                selectQuery = "insert into BASE_COMMENTS (id, ";
                selectQuery += "                             id_patient, ";
                selectQuery += "                             TYPE_COMMENT, ";
                selectQuery += "                             COMMENT, ";
                selectQuery += "                             DATE_COMMENT, ";
                selectQuery += "                             CODECOMMENTAIRE, ";
                selectQuery += "                             IMPORTANCE, ";
                selectQuery += "                             PARENT, ";
                selectQuery += "                             DATEFIN, ";
                selectQuery += "                             DATEDEBUT, ";
                selectQuery += "                             ID_WRITER)";
                selectQuery += " values (@id, ";
                selectQuery += "        @id_patient, ";
                selectQuery += "        @TYPE_COMMENT, ";
                selectQuery += "        @COMMENT, ";
                selectQuery += "        @DATE_COMMENT, ";
                selectQuery += "        @CODECOMMENTAIRE, ";
                selectQuery += "        @IMPORTANCE, ";
                selectQuery += "        @PARENT, ";
                selectQuery += "        @DATEFIN, ";
                selectQuery += "        @DATEDEBUT, ";
                selectQuery += "        @ID_WRITER)";




                command.CommandText = selectQuery;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", value.Id);
                command.Parameters.AddWithValue("@id_patient", value.IdPatient);

                command.Parameters.AddWithValue("@TYPE_COMMENT", value.typecomment);
                command.Parameters.AddWithValue("@COMMENT", value.comment);
                command.Parameters.AddWithValue("@CODECOMMENTAIRE", value.Code);
                command.Parameters.AddWithValue("@IMPORTANCE", value.Importance);
                command.Parameters.AddWithValue("@PARENT", value.IdParent);
                command.Parameters.AddWithValue("@DATEFIN", value.DateDeFin);
                command.Parameters.AddWithValue("@DATEDEBUT", value.DateDeDebut);
                command.Parameters.AddWithValue("@DATE_COMMENT", value.DateCommentaire);
                command.Parameters.AddWithValue("@ID_WRITER", value.Id_Ecrivain);


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

        public static void UpdateCommentHisto(CommentHisto value)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();



            try
            {
                string selectQuery = "update BASE_COMMENTS";
                selectQuery += " set id_patient = @id_patient,";
                selectQuery += "    TYPE_COMMENT = @TYPE_COMMENT,";
                selectQuery += "    COMMENT = @COMMENT,";
                selectQuery += "    DATE_COMMENT = @DATE_COMMENT,";
                selectQuery += "    CODECOMMENTAIRE = @CODECOMMENTAIRE,";
                selectQuery += "    IMPORTANCE = @IMPORTANCE,";
                selectQuery += "    PARENT = @PARENT,";
                selectQuery += "    DATEFIN = @DATEFIN,";
                selectQuery += "    DATEDEBUT = @DATEDEBUT,";
                selectQuery += "    ID_WRITER = @ID_WRITER";
                selectQuery += " where (id = @id)";





                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", value.Id);
                command.Parameters.AddWithValue("@id_patient", value.IdPatient);

                command.Parameters.AddWithValue("@TYPE_COMMENT", value.typecomment);
                command.Parameters.AddWithValue("@COMMENT", value.comment);
                command.Parameters.AddWithValue("@DATE_COMMENT", value.DateCommentaire);
                command.Parameters.AddWithValue("@ID_WRITER", value.Id_Ecrivain);

                command.Parameters.AddWithValue("@CODECOMMENTAIRE", value.Code);
                command.Parameters.AddWithValue("@IMPORTANCE", value.Importance);
                command.Parameters.AddWithValue("@PARENT", value.IdParent);
                command.Parameters.AddWithValue("@DATEFIN", value.DateDeFin);
                command.Parameters.AddWithValue("@DATEDEBUT", value.DateDeDebut);




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

        public static void DeleteCommentaires(CommentHisto value)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "Delete from BASE_COMMENTS ";
                selectQuery += " where id = @id ";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);


                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@id", value.Id);

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

        #region Suivi contact

        public static DataTable GetSuiviContactEnfants(SuiviContact suivi)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "              ID_PATIENT, ";
                selectQuery += "              ID_PARENT, ";
                selectQuery += "              CONTACT_FROM,";
                selectQuery += "              CONTACT_TO,";
                selectQuery += "              CONTACT_FROMID,";
                selectQuery += "              CONTACT_TOID,";
                selectQuery += "              COMMENT,";
                selectQuery += "              DATE_CONTACT,";
                selectQuery += "              CONTACT_TYPE";
                selectQuery += "       from BAS_CONTACT_WORKFLOW";
                selectQuery += "       where ID_PARENT=@ID_PARENT";
                selectQuery += " order by  DATE_CONTACT desc";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@ID_PARENT", suivi.IdParent);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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

        public static DataRow GetSuiviContact(int id)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select bas_contact_workflow.id, ";
                selectQuery += "              bas_contact_workflow.ID_PATIENT, ";
                selectQuery += "              bas_contact_workflow.ID_PARENT, ";
                selectQuery += "              bas_contact_workflow.CONTACT_FROM,";
                selectQuery += "              bas_contact_workflow.CONTACT_TO,";
                selectQuery += "              bas_contact_workflow.CONTACT_FROMID,";
                selectQuery += "              bas_contact_workflow.CONTACT_TOID,";
                selectQuery += "              bas_contact_workflow.COMMENT,";
                selectQuery += "              bas_contact_workflow.DATE_CONTACT,";
                selectQuery += "              bas_contact_workflow.CONTACT_TYPE";

                selectQuery += "              from bas_contact_workflow";
                selectQuery += "             where ID=@ID"; FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@ID", id);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

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

        public static DataTable GetSuiviContacts(basePatient pat)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select bas_contact_workflow.id, ";
                selectQuery += "              bas_contact_workflow.ID_PATIENT, ";
                selectQuery += "              bas_contact_workflow.ID_PARENT, ";
                selectQuery += "              bas_contact_workflow.CONTACT_FROM,";
                selectQuery += "              bas_contact_workflow.CONTACT_TO,";
                selectQuery += "              bas_contact_workflow.CONTACT_FROMID,";
                selectQuery += "              bas_contact_workflow.CONTACT_TOID,";
                selectQuery += "              bas_contact_workflow.COMMENT,";
                selectQuery += "              bas_contact_workflow.DATE_CONTACT,";
                selectQuery += "              bas_contact_workflow.CONTACT_TYPE";

                selectQuery += "              from bas_contact_workflow";
                selectQuery += "              inner join";
                selectQuery += "              ( ";

                selectQuery += "              select id_Parent,";
                selectQuery += "              max(date_contact) date_contact";
                selectQuery += "              from bas_contact_workflow ";
                selectQuery += "             where ID_PATIENT=@ID_PATIENT";
                selectQuery += "              group by 1 ";
                selectQuery += "              ) filtre on filtre.id_Parent =  bas_contact_workflow.id_Parent and filtre.date_contact = bas_contact_workflow.date_contact ";



                selectQuery += " order by  DATE_CONTACT desc";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@ID_PATIENT", pat.Id);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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

        public static void InsertSuiviContacts(SuiviContact suivi)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();


            string selectQueryId = "select max(Id)+1 as ID from BAS_CONTACT_WORKFLOW";
            FbCommand commandid = new FbCommand(selectQueryId, connection, transaction);
            object id = commandid.ExecuteScalar();
            if (id == DBNull.Value)
                suivi.Id = 1;
            else
                suivi.Id = Convert.ToInt32(id);



            try
            {
                string selectQuery = "insert into BAS_CONTACT_WORKFLOW (ID, ";
                selectQuery += "                            ID_PATIENT, ";
                selectQuery += "                            ID_PARENT, ";
                selectQuery += "                            CONTACT_FROM, ";
                selectQuery += "                            CONTACT_TO, ";
                selectQuery += "                            CONTACT_FROMID, ";
                selectQuery += "                            CONTACT_TOID, ";
                selectQuery += "                            COMMENT, ";
                selectQuery += "                            DATE_CONTACT, ";
                selectQuery += "                            CONTACT_TYPE)";
                selectQuery += " values (@ID, ";
                selectQuery += "        @ID_PATIENT, ";
                selectQuery += "        @ID_PARENT, ";
                selectQuery += "        @CONTACT_FROM, ";
                selectQuery += "        @CONTACT_TO, ";
                selectQuery += "        @CONTACT_FROMID, ";
                selectQuery += "        @CONTACT_TOID, ";
                selectQuery += "        @COMMENT, ";
                selectQuery += "        @DATE_CONTACT, ";
                selectQuery += "        @CONTACT_TYPE)";




                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@ID", suivi.Id);
                command.Parameters.AddWithValue("@ID_PATIENT", suivi.IdPatient);
                command.Parameters.AddWithValue("@ID_PARENT", suivi.IdParent < 0 ? suivi.Id : suivi.IdParent);
                command.Parameters.AddWithValue("@CONTACT_FROM", suivi.ContactFrom);
                command.Parameters.AddWithValue("@CONTACT_TO", suivi.ContactPrisPar);
                command.Parameters.AddWithValue("@CONTACT_FROMID", suivi.IdContactFrom);
                command.Parameters.AddWithValue("@CONTACT_TOID", suivi.IdContactPrisPar);
                command.Parameters.AddWithValue("@COMMENT", suivi.Message);
                command.Parameters.AddWithValue("@DATE_CONTACT", suivi.DateDuContact);
                command.Parameters.AddWithValue("@CONTACT_TYPE", suivi.typeContact);



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

        public static void UpdateSuiviContacts(SuiviContact suivi)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();


            string selectQueryId = "select max(Id)+1 as ID from BAS_CONTACT_WORKFLOW";
            FbCommand commandid = new FbCommand(selectQueryId, connection, transaction);
            object id = commandid.ExecuteScalar();
            if (id == DBNull.Value)
                suivi.Id = 1;
            else
                suivi.Id = Convert.ToInt32(id);



            try
            {


                string selectQuery = "update bas_contact_workflow";
                selectQuery += "     set id_patient = @id_patient,";
                selectQuery += "     ID_PARENT = @ID_PARENT,";
                selectQuery += "     contact_from = @contact_from,";
                selectQuery += "     contact_to = @contact_to,";
                selectQuery += "     contact_fromid = @contact_fromid,";
                selectQuery += "     contact_toid = @contact_toid,";
                selectQuery += "     comment = @comment,";
                selectQuery += "     date_contact = @date_contact,";
                selectQuery += "     contact_type = @contact_type";
                selectQuery += "     where (id = @id)";




                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@ID", suivi.Id);
                command.Parameters.AddWithValue("@ID_PATIENT", suivi.IdPatient);
                command.Parameters.AddWithValue("@ID_PARENT", suivi.IdParent < 0 ? suivi.Id : suivi.IdParent);
                command.Parameters.AddWithValue("@CONTACT_FROM", suivi.ContactFrom);
                command.Parameters.AddWithValue("@CONTACT_TO", suivi.ContactPrisPar);
                command.Parameters.AddWithValue("@CONTACT_FROMID", suivi.IdContactFrom);
                command.Parameters.AddWithValue("@CONTACT_TOID", suivi.IdContactPrisPar);
                command.Parameters.AddWithValue("@COMMENT", suivi.Message);
                command.Parameters.AddWithValue("@DATE_CONTACT", suivi.DateDuContact);
                command.Parameters.AddWithValue("@CONTACT_TYPE", suivi.typeContact);



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

        public static void DeleteSuiviContacts(SuiviContact suivi)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "delete ";
                selectQuery += " from BAS_CONTACT_WORKFLOW";
                selectQuery += " where id=@id";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", suivi.Id);


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

        #region Categories









        public static DataTable getHistoCategories()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "SELECT id, ";
                selectQuery += "id_personne, ";
                selectQuery += "date_categ, ";
                selectQuery += "date_fin_categ, ";
                selectQuery += "id_categorie, ";
                selectQuery += "FROM base_histo_categorie ";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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

        public static void AffectNote(int IdPersonne, int note)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "update base_histo_categorie set ";
                selectQuery += " date_fin_categ = @date_fin_categ";
                selectQuery += " where base_histo_categorie.ID_PERSONNE = @id and date_fin_categ is null and ID_CATEGORIE is null";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                command.CommandText = selectQuery;

                command.Parameters.AddWithValue("@id", IdPersonne);
                command.Parameters.AddWithValue("@date_fin_categ", DateTime.Now);

                command.ExecuteNonQuery();


                selectQuery = "select MAX(ID)+1 as NEWID from BASE_HISTO_CATEGORIE";

                command.CommandText = selectQuery;


                object obj = command.ExecuteScalar();
                int id = 1;

                if (!(obj is DBNull))
                    id = Convert.ToInt32(obj);


                selectQuery = "insert into base_histo_categorie (";
                selectQuery += " ID,";
                selectQuery += " ID_PERSONNE,";
                selectQuery += " DATE_CATEG,";
                selectQuery += " DATE_FIN_CATEG,";
                selectQuery += " ID_CATEGORIE,";
                selectQuery += " NOTE";
                selectQuery += ") values (";
                selectQuery += " @ID,";
                selectQuery += " @ID_PERSONNE,";
                selectQuery += " @DATE_CATEG,";
                selectQuery += " NULL,";
                selectQuery += " NULL,";
                selectQuery += " @Note)";

                command.CommandText = selectQuery;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@ID_PERSONNE", IdPersonne);
                command.Parameters.AddWithValue("@DATE_CATEG", DateTime.Now);
                command.Parameters.AddWithValue("@Note", note);

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


        public static void updateCategorieBeToWas(CustomCategorie custo)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "update base_histo_categorie set ";
                selectQuery += "date_fin_categ = @date_fin_categ";
                selectQuery += " where (id = @id)";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                command.CommandText = selectQuery;

                command.Parameters.AddWithValue("@id", custo.Id);
                command.Parameters.AddWithValue("@date_fin_categ", DateTime.Now);

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

        public static void updateCategorieWasToBe(CustomCategorie custo)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select MAX(ID)+1 as NEWID from BASE_HISTO_CATEGORIE";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;


                object obj = command.ExecuteScalar();

                if (obj is DBNull)
                    custo.Id = 1;
                else
                    custo.Id = Convert.ToInt32(obj);


                selectQuery = "insert into base_histo_categorie (";
                selectQuery += "id,";
                selectQuery += "id_personne,";
                selectQuery += "date_categ,";
                selectQuery += "date_fin_categ,";
                selectQuery += "id_categorie";
                selectQuery += ") values ( ";
                selectQuery += "@id,";
                selectQuery += "@id_personne,";
                selectQuery += "@date_categ,";
                selectQuery += "@date_fin_categ,";
                selectQuery += "@id_categorie)";

                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@id", custo.Id);
                command.Parameters.AddWithValue("@id_personne", custo.IdPersonne);
                command.Parameters.AddWithValue("@date_categ", DateTime.Now);
                command.Parameters.AddWithValue("@date_fin_categ", DBNull.Value);
                command.Parameters.AddWithValue("@id_categorie", custo.IdCateg);

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

        #region En Bouche

        public static DataTable getCommCliniqueScenarEnbouche(CommCliniqueDetailsScenario com)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += " typematerial, ";
                selectQuery += " dents,";
                selectQuery += " ID_APPAREIL,";
                selectQuery += " ID_COMM_DEBUT,";
                selectQuery += " ID_COMM_FIN,";
                selectQuery += " ID_APPAREIL,";
                selectQuery += " HAUT,";
                selectQuery += " BAS";
                selectQuery += " from BASE_SCENAR_ENBOUCHE";
                selectQuery += " where ID_COMM_DEBUT=@ID_COMM or ID_COMM_FIN=@ID_COMM";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@ID_COMM", com.Id);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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



        public static DataTable getAccessoiresEnbouche(int Id_Patient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
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

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@Id", Id_Patient);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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

        public static DataTable getAppareilsEnbouche(int Id_Patient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
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

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@Id", Id_Patient);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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

        public static void InsertEnbouche(IElementDent elem)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "select MAX(ID)+1 as NEWID from BAS_ENBOUCHE";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                object obj = command.ExecuteScalar();

                if (obj == DBNull.Value)
                    elem.Id = 1;
                else
                    elem.Id = Convert.ToInt32(obj);


                selectQuery = "insert into BAS_ENBOUCHE (id, ";
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
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "update BAS_ENBOUCHE set DATEFIN=@DATEFIN  ";
                selectQuery += " ,ID_COMM_FIN=@ID_COMM_FIN ";
                selectQuery += " where ID=@ID ";



                FbCommand command = new FbCommand(selectQuery, connection, transaction);
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
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "select MAX(ID)+1 as NEWID from BAS_ENBOUCHE";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                object obj = command.ExecuteScalar();

                if (obj == DBNull.Value)
                    elem.Id = 1;
                else
                    elem.Id = Convert.ToInt32(obj);


                selectQuery = "insert into BAS_ENBOUCHE (id, ";
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
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "update BAS_ENBOUCHE set DATEFIN=@DATEFIN  ";
                selectQuery += " ,ID_COMM_FIN=@ID_COMM_FIN ";
                selectQuery += " where ID=@ID ";



                FbCommand command = new FbCommand(selectQuery, connection, transaction);
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



        #region Historiques courriers

        public static DataRow getHistoriqueInSuiviDoc(int id)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "";


                selectQuery = " select ID_KEY Id,DATE_JOUR DATE_ENVOIS,null APERCUS,NOM_DOC FICHIER,2 TYPEENVOIS,'' TAG";
                selectQuery += " ,pat.per_nom NOMPATIENT,pat.per_prenom PRENOMPATIENT,corres.per_nom NOMCORRESPONDANT,corres.per_prenom PRENOMCORRESPONDANT, prat.per_nom NOMPRATICIEN, prat.per_prenom PRENOMPRATICIEN";
                selectQuery += " from SUIVI_DOC";
                selectQuery += " inner join personne pat on pat.id_personne = SUIVI_DOC.ID_PATIENT ";
                selectQuery += " inner join personne corres on corres.id_personne = SUIVI_DOC.ID_PATIENT ";
                selectQuery += " inner join personne prat on prat.id_personne = SUIVI_DOC.ID_PATIENT ";

                selectQuery += " where Suivi_doc.id_key = @id";


                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", id);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

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

        public static DataRow getHistoriqueDevis(int idDevis)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "";


                selectQuery = " select first 1 ID_KEY Id,ID_DEVIS, DATE_JOUR DATE_ENVOIS,null APERCUS,NOM_DOC FICHIER,2 TYPEENVOIS,'' TAG";
                selectQuery += " ,pat.per_nom NOMPATIENT,pat.per_prenom PRENOMPATIENT,corres.per_nom NOMCORRESPONDANT,corres.per_prenom PRENOMCORRESPONDANT, prat.per_nom NOMPRATICIEN, prat.per_prenom PRENOMPRATICIEN";
                selectQuery += " from SUIVI_DOC";
                selectQuery += " inner join personne pat on pat.id_personne = SUIVI_DOC.ID_PATIENT ";
                selectQuery += " inner join personne corres on corres.id_personne = SUIVI_DOC.ID_PATIENT ";
                selectQuery += " inner join personne prat on prat.id_personne = SUIVI_DOC.ID_PATIENT ";

                selectQuery += " where Suivi_doc.ID_DEVIS = @id";
                selectQuery += " order by DATE_JOUR desc";


                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", idDevis);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

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

        public static DataTable getHistoriqueByPersonne(DateTime dteStart, DateTime DteEnd, int IdPersonne)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "";


                selectQuery = " select ID_KEY Id,2 TYPEENVOIS,DATE_JOUR DATE_ENVOIS,'' NOMSATTRIBUTS,'' VALEURSATTRIBUTS,NOM_DOC";
                selectQuery += " ,pat.per_nom NOMPATIENT,pat.per_prenom PRENOMPATIENT,corres.per_nom NOMCORRESPONDANT,corres.per_prenom PRENOMCORRESPONDANT, prat.per_nom NOMPRATICIEN, prat.per_prenom PRENOMPRATICIEN";
                selectQuery += " from SUIVI_DOC";
                selectQuery += " left join personne pat on pat.id_personne = SUIVI_DOC.ID_PATIENT ";
                selectQuery += " left join personne corres on corres.id_personne = SUIVI_DOC.ID_CORRESPONDANT ";
                selectQuery += " left join personne prat on prat.id_personne = SUIVI_DOC.ID_PRATICIEN ";
                selectQuery += " where (pat.id_personne=@id) ";
                selectQuery += " and DATE_JOUR between @start and @end";
                selectQuery += " order by  DATE_JOUR desc";




                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@start", dteStart);
                command.Parameters.AddWithValue("@end", DteEnd);
                command.Parameters.AddWithValue("@id", IdPersonne);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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


        public static DataTable getHistoriqueByPersonne(DateTime dteStart, DateTime DteEnd, string search)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "";


                selectQuery = " select ID_KEY Id,2 TYPEENVOIS,DATE_JOUR DATE_ENVOIS,'' NOMSATTRIBUTS,'' VALEURSATTRIBUTS,NOM_DOC";
                selectQuery += " ,pat.per_nom NOMPATIENT,pat.per_prenom PRENOMPATIENT,corres.per_nom NOMCORRESPONDANT,corres.per_prenom PRENOMCORRESPONDANT, prat.per_nom NOMPRATICIEN, prat.per_prenom PRENOMPRATICIEN";
                selectQuery += " from SUIVI_DOC";
                selectQuery += " left join personne pat on pat.id_personne = SUIVI_DOC.ID_PATIENT ";
                selectQuery += " left join personne corres on corres.id_personne = SUIVI_DOC.ID_CORRESPONDANT ";
                selectQuery += " left join personne prat on prat.id_personne = SUIVI_DOC.ID_PRATICIEN ";
                selectQuery += " where (1=1) ";

                foreach (string s in search.Split(' '))
                {
                    selectQuery += " and (upper(pat.PER_NOM) LIKE '" + s.ToUpper() + "%'";
                    selectQuery += " or upper(pat.PER_PRENOM) LIKE '" + s.ToUpper() + "%'";
                    selectQuery += " or upper(corres.PER_NOM) LIKE '" + s.ToUpper() + "%'";
                    selectQuery += " or upper(corres.PER_PRENOM) LIKE '" + s.ToUpper() + "%'";
                    selectQuery += " or upper(prat.PER_NOM) LIKE '" + s.ToUpper() + "%'";
                    selectQuery += " or upper(prat.PER_PRENOM) LIKE '" + s.ToUpper() + "%'";
                    selectQuery += " or upper(corres.PER_VILLE) LIKE '" + s.ToUpper() + "%'";
                    selectQuery += " or upper(corres.PER_CPOSTAL) LIKE '" + s.ToUpper() + "%'";
                    selectQuery += " or upper(prat.PER_VILLE) LIKE '" + s.ToUpper() + "%'";
                    selectQuery += " or upper(prat.PER_CPOSTAL) LIKE '" + s.ToUpper() + "%')";
                }
                selectQuery += " and DATE_JOUR between @start and @end";
                selectQuery += " order by  DATE_JOUR desc";




                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@start", dteStart);
                command.Parameters.AddWithValue("@end", DteEnd);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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


        public static void InsertAttributsHistoriqueCourrier(AttributCourrier attribut)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();


            string selectQueryId = "select gen_id(BASE_COURRIER_ATTRIBUTS,1)  from rdb$database";
            FbCommand commandid = new FbCommand(selectQueryId, connection, transaction);
            object id = commandid.ExecuteScalar();
            if (id == DBNull.Value)
                attribut.Id = 1;
            else
                attribut.Id = Convert.ToInt32(id);



            try
            {
                string selectQuery = "insert into base_courrier_attributs (id, ";
                selectQuery += "                                     id_histo_courrier, ";
                selectQuery += "                                     nom_attribut, ";
                selectQuery += "                                     value_attribut)";
                selectQuery += "values (@id, ";
                selectQuery += "        @id_histo_courrier, ";
                selectQuery += "        @nom_attribut, ";
                selectQuery += "        @value_attribut)";






                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id", attribut.Id);
                command.Parameters.AddWithValue("@nom_attribut", attribut.NomAttribut);
                command.Parameters.AddWithValue("@value_attribut", attribut.ValueAttribut == null ? DBNull.Value : (object)attribut.ValueAttribut);
                command.Parameters.AddWithValue("@id_histo_courrier", attribut.IdHistoCourrier);



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

        public static void InsertHistoCourrierForPersonne(int? IdPatient, int? IdPersonne, int? IdPraticien, HistoCourrier courrier)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();


            string selectQueryId = "select max(SUIVI_DOC.ID_KEY)  from SUIVI_DOC";
            FbCommand commandid = new FbCommand(selectQueryId, connection, transaction);
            object id = commandid.ExecuteScalar();
            if (id == DBNull.Value)
                courrier.Id = 1;
            else
                courrier.Id = Convert.ToInt32(id);



            try
            {
                string selectQuery = "insert into SUIVI_DOC (ID_KEY, ";
                selectQuery += "                                 ID_PATIENT, ";
                selectQuery += "                                 ID_CORRESPONDANT, ";
                selectQuery += "                                 ID_PRATICIEN, ";
                selectQuery += "                                 DATE_JOUR, ";
                selectQuery += "                                 NOM_DOC, ";
                selectQuery += "                                 MAILING)";
                selectQuery += "values (@ID_KEY, ";
                selectQuery += "        @ID_PATIENT, ";
                selectQuery += "        @ID_CORRESPONDANT, ";
                selectQuery += "        @ID_PRATICIEN, ";
                selectQuery += "        @DATE_JOUR, ";
                selectQuery += "        @NOM_DOC, ";
                selectQuery += "        0)";




                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@ID_KEY", courrier.Id);
                command.Parameters.AddWithValue("@ID_PATIENT", IdPatient == null ? DBNull.Value : (object)IdPatient);
                command.Parameters.AddWithValue("@ID_CORRESPONDANT", IdPersonne == null ? DBNull.Value : (object)IdPersonne);
                command.Parameters.AddWithValue("@ID_PRATICIEN", IdPraticien == null ? DBNull.Value : (object)IdPraticien);

                command.Parameters.AddWithValue("@DATE_JOUR", courrier.DateEnvois);
                command.Parameters.AddWithValue("@NOM_DOC", courrier.Fichier);

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

        public static void InsertHistorique(HistoCourrier histo)
        {
            try
            {
                DAC.InsertHistoCourrier(histo);


                int? IdPatient = null;
                int? IdPraticien = null;
                int? IdPCorres = null;

                foreach (AttributCourrier ac in histo.attributs)
                {
                    ac.IdHistoCourrier = histo.Id;
                    DAC.InsertAttributsHistoriqueCourrier(ac);
                    if (ac.NomAttribut.ToUpper() == "ID_PATIENT") IdPatient = Convert.ToInt32(ac.ValueAttribut);
                    if (ac.NomAttribut.ToUpper() == "ID_PRATICIEN") IdPraticien = Convert.ToInt32(ac.ValueAttribut);
                    if (ac.NomAttribut.ToUpper() == "ID_CORRESPONDANT") IdPCorres = Convert.ToInt32(ac.ValueAttribut);

                }

                DAC.InsertHistoCourrierForPersonne(IdPatient, IdPCorres, IdPraticien, histo);
            }
            catch (System.Exception)
            {
            }

        }

        public static void InsertHistoCourrier(HistoCourrier courrier)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();


            string selectQueryId = "select gen_id(base_histo_courrier,1)  from rdb$database";
            FbCommand commandid = new FbCommand(selectQueryId, connection, transaction);
            object id = commandid.ExecuteScalar();
            if (id == DBNull.Value)
                courrier.Id = 1;
            else
                courrier.Id = Convert.ToInt32(id);



            try
            {
                string selectQuery = "insert into base_histo_courrier (id, ";
                selectQuery += "                                 date_envois, ";
                selectQuery += "                                 apercus, ";
                selectQuery += "                                 fichier, ";
                selectQuery += "                                 TYPEENVOIS, ";
                selectQuery += "                                 tag)";
                selectQuery += "values (@id, ";
                selectQuery += "        @date_envois, ";
                selectQuery += "        @apercus, ";
                selectQuery += "        @fichier, ";
                selectQuery += "        @TYPEENVOIS, ";
                selectQuery += "        @tag)";




                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id", courrier.Id);
                command.Parameters.AddWithValue("@date_envois", courrier.DateEnvois);

                if (courrier.Apercus != null)
                {
                    byte[] array = imageToByteArray(courrier.Apercus);
                    command.Parameters.AddWithValue("@apercus", array);
                }
                else
                    command.Parameters.AddWithValue("@apercus", DBNull.Value);

                command.Parameters.AddWithValue("@fichier", courrier.Fichier);
                command.Parameters.AddWithValue("@TYPEENVOIS", courrier.typeenvois);
                command.Parameters.AddWithValue("@tag", courrier.Tag);



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

        #endregion

        #region Commentaire Secretaire



        public static void UpdateCommentaire(CommentaireSecretaire comm)
        {



            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "update ZONE_COMM_UTILISATEUR ";
                selectQuery += " set ID_ZONE_COM = @ID_ZONE_COM,";
                selectQuery += "     ID_PATIENT = @ID_PATIENT,";
                selectQuery += "     ID_USER = @ID_USER,";
                selectQuery += "     DATE_COM = @DATE_COM,";
                selectQuery += "     COMMENTAIRE = @COMMENTAIRE";
                selectQuery += " where (ID_ZONE_COM = @ID_ZONE_COM)";




                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;


                command.Parameters.AddWithValue("@ID_ZONE_COM", comm.Id);
                command.Parameters.AddWithValue("@ID_PATIENT", comm.IdPatient);
                command.Parameters.AddWithValue("@ID_USER", comm.IdUtilisateur);
                command.Parameters.AddWithValue("@DATE_COM", comm.DateCommentaire);
                command.Parameters.AddWithValue("@COMMENTAIRE", comm.Commentaire);


                command.ExecuteNonQuery();
                command.Transaction.Commit();



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

        public static void InsertCommentaire(CommentaireSecretaire comm)
        {



            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "select MAX(ID_ZONE_COM)+1 as NEWID from ZONE_COMM_UTILISATEUR";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                object obj = command.ExecuteScalar();

                if (obj == DBNull.Value) comm.Id = 1; else comm.Id = Convert.ToInt32(obj);


                selectQuery = "insert into ZONE_COMM_UTILISATEUR (ID_ZONE_COM, ";
                selectQuery += "                        ID_PATIENT, ";
                selectQuery += "                        ID_USER, ";
                selectQuery += "                        DATE_COM, ";
                selectQuery += "                        COMMENTAIRE)";
                selectQuery += " values (@ID_ZONE_COM, ";
                selectQuery += "         @ID_PATIENT, ";
                selectQuery += "         @ID_USER, ";
                selectQuery += "         @DATE_COM, ";
                selectQuery += "         @COMMENTAIRE)";



                command.CommandText = selectQuery;

                command.Parameters.AddWithValue("@ID_ZONE_COM", comm.Id);
                command.Parameters.AddWithValue("@ID_PATIENT", comm.IdPatient);
                command.Parameters.AddWithValue("@ID_USER", comm.IdUtilisateur);
                command.Parameters.AddWithValue("@DATE_COM", comm.DateCommentaire);
                command.Parameters.AddWithValue("@COMMENTAIRE", comm.Commentaire);

                command.ExecuteNonQuery();
                command.Transaction.Commit();



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

        public static DataTable getCommentairesSecretaire(basePatient pat)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select ID_ZONE_COM, ";
                selectQuery += "        ID_PATIENT, ";
                selectQuery += "        ID_USER, ";
                selectQuery += "        DATE_COM, ";
                selectQuery += "        COMMENTAIRE";
                selectQuery += " from ZONE_COMM_UTILISATEUR";
                selectQuery += " where id_patient = @IDPAT";
                selectQuery += " order by date_com desc";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@IDPAT", pat.Id);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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


        public static DataTable getCommentairesOrthalis(basePatient pat)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            FbTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id_comm, ";
                selectQuery += " id_pat, ";
                selectQuery += " date_comm, ";
                selectQuery += " code_hyg, ";
                selectQuery += " code_libre, ";
                selectQuery += " commentaire, ";
                selectQuery += " fait, ";
                selectQuery += " afaire, ";
                selectQuery += " comm_prat as ID_PRATICIEN, ";
                selectQuery += " TRIM(personne.per_NOM)||' '||TRIM(personne.per_prenom) as PRATICIEN ";
                selectQuery += " from zone_comm";
                selectQuery += " left outer join personne on zone_comm.comm_prat=personne.id_personne";
                selectQuery += " where id_pat=@IDPAT";
                selectQuery += " order by date_comm desc";

                FbCommand command = new FbCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@IDPAT", pat.Id);

                DataSet ds = new DataSet();
                FbDataAdapter adapt = new FbDataAdapter(command);
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

        #endregion


        #region Recontact







        #endregion

        */

    }
}
