using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using System.Configuration;
using BasCommon_BO;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Net;
using System.IO;
namespace BasCommon_DAL
{
    public static partial class DAC
    {




        public static DataRow GetInvisalignInfos(basePatient pat)
        {
            if (connection == null) getConnection();

            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
            }
            catch (System.Exception e)
            {
                throw e;
            }


            try
            {



                string selectQuery = @"select id_orthalis, 
                                        id_invisalign, 
                                        nom_invisalign, 
                                        FreqChangemnt,
                                        FreqRDV,
                                        DateFinInvisalign,
                                        TpeTrmnt,
                                        prenom_invisalign
                                from patient_invisalign
                                where id_orthalis=@id";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@id", pat.Id);
                command.CommandType = CommandType.Text;

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

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


        public static void SetInfosInvisalign(basePatient patient)
        {

            if (patient.infosinvisalign == null)
                return;


            MySqlCommand myCmd = null;
            MySqlTransaction transaction = null;

            int Int_id_invisalign = -1;
            try
            {
                Int_id_invisalign = Convert.ToInt32(patient.infosinvisalign.IdInvisalign);
            }
            catch (System.FormatException)
            {
                Int_id_invisalign = -1;
            }

            if (connection == null) getConnection();
            if (connection.State == ConnectionState.Closed) connection.Open();

            try
            {


                string query = @"insert into patient_invisalign 
                            (id_invisalign,
                            nom_invisalign,
                            prenom_invisalign,
                            DateFinInvisalign,
                            TpeTrmnt,
                            FreqRDV,
                            FreqChangemnt,
                            id_orthalis)
                values (
                            @id_invisalign,
                            @nom_invisalign,
                            @prenom_invisalign,
                            @DateFinInvisalign,
                            @TpeTrmnt,
                            @FreqRDV,
                            @FreqChangemnt,
                            @id_orthalis)
                ON DUPLICATE KEY UPDATE nom_invisalign=@nom_invisalign,prenom_invisalign=@prenom_invisalign,datefininvisalign=@datefininvisalign,tpetrmnt=@tpetrmnt,freqrdv=@freqrdv,freqchangemnt=@freqchangemnt";

                transaction = connection.BeginTransaction();

                myCmd = new MySqlCommand(query, connection, transaction);

                myCmd.CommandText = query;

                myCmd.Parameters.AddWithValue("@id_orthalis", patient.Id);

                if (Int_id_invisalign == -1)
                    myCmd.Parameters.AddWithValue("@id_invisalign", null);
                else
                    myCmd.Parameters.AddWithValue("@id_invisalign", patient.infosinvisalign.IdInvisalign);

                myCmd.Parameters.AddWithValue("@nom_invisalign", patient.infosinvisalign.NomInvisalign);
                myCmd.Parameters.AddWithValue("@prenom_invisalign", patient.infosinvisalign.PrenomInvisalign);


                myCmd.Parameters.AddWithValue("@datefininvisalign", patient.infosinvisalign.DateFinInvisalign);
                myCmd.Parameters.AddWithValue("@tpetrmnt", patient.infosinvisalign.TpeTrmnt);
                myCmd.Parameters.AddWithValue("@freqrdv", patient.infosinvisalign.FreqRDV);
                myCmd.Parameters.AddWithValue("@freqchangemnt", patient.infosinvisalign.FreqChangemnt);

                myCmd.CommandType = CommandType.Text;

                myCmd.ExecuteNonQuery();

                transaction.Commit();

            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public static void UpdateAsInvisalign(string id_invisalign, int id_orthalis, string nom, string prenom)
        {


            MySqlCommand myCmd = null;
            MySqlTransaction transaction = null;

            int Int_id_invisalign = -1;
            try
            {
                Int_id_invisalign = Convert.ToInt32(id_invisalign);
            }
            catch (System.FormatException)
            {
                return;
            }

            if (connection == null) getConnection();
            if (connection.State == ConnectionState.Closed) connection.Open();

            try
            {


                string query = @"insert into patient_invisalign (id_invisalign,nom_invisalign,prenom_invisalign,id_orthalis)
                values (@id_invisalign,@nom_invisalign,@prenom_invisalign,@id_orthalis)
                ON DUPLICATE KEY UPDATE  id_invisalign = @id_invisalign , nom_invisalign=@nom_invisalign, prenom_invisalign=@prenom_invisalign,id_orthalis=@id_orthalis";

                transaction = connection.BeginTransaction();

                myCmd = new MySqlCommand(query, connection, transaction);

                myCmd.CommandText = query;

                myCmd.Parameters.AddWithValue("@id_orthalis", id_orthalis);

                if (Int_id_invisalign == -1)
                    myCmd.Parameters.AddWithValue("@id_invisalign", null);
                else
                    myCmd.Parameters.AddWithValue("@id_invisalign", id_invisalign);

                myCmd.Parameters.AddWithValue("@nom_invisalign", nom);
                myCmd.Parameters.AddWithValue("@prenom_invisalign", prenom);



                myCmd.CommandType = CommandType.Text;

                myCmd.ExecuteNonQuery();

                transaction.Commit();

            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }



        public static void UpdateOpeningPatWithOrth(int IdPat)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "update PATIENT SET TEST_BP = NULL";
                selectQuery += " where (id_personne = @id)";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", IdPat);

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


        public static void UpdateOpeningPatWithBP(int IdPat)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "update PATIENT SET TEST_BP = 1";
                selectQuery += " where (id_personne = @id)";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", IdPat);

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


        public static int GetNextNumDossier()
        {
            if (connection == null) getConnection();

            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
            }
            catch (System.Exception e)
            {
                throw e;
            }


            try
            {

                string selectQuery = @"select t1.pat_numdossier +1 From patient t1
                                       where not exists (select * from patient t2 where t1.pat_numdossier +1 = t2.pat_numdossier);";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.CommandType = CommandType.Text;
                object o = command.ExecuteScalar();
                int res = o is DBNull ? 1 : Convert.ToInt32(o);


                return res;

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

        public static void updateLinkToQ1cs(int IdPat)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "insert into  authQ1CS values (@id) ";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", IdPat);

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
        public static void deleteLinkQ1Cs(int IdPat)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "delete from  authQ1CS where id=@id";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", IdPat);

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

        
        public static DataTable getPatientsInAttente()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {



                string selectQuery = "select personne.id_personne,";
                //selectQuery += " personne.id_adresse,"; 
                //selectQuery += " personne.id_util,"; 
                //selectQuery += " personne.id_caisse,"; 
                //selectQuery += " personne.adr_id_adresse,"; 
                selectQuery += " personne.per_nom,";
                //selectQuery += " personne.per_nomjf,"; 
                selectQuery += " personne.per_prenom,";
                selectQuery += " personne.PER_PRENOM, ";
                selectQuery += " personne.PER_TELPRINC,";
                selectQuery += " rendez_vous.ID_FAUTEUIL,";
                selectQuery += " personne.PER_EMAIL ";
                selectQuery += " from rendez_vous";
                selectQuery += " inner join personne on personne.id_personne = rendez_vous.id_personne";
                selectQuery += " inner join patient p on personne.id_personne=p.id_personne";
                selectQuery += " where rdv_date between @date1 and @date2";
                selectQuery += " and rendez_vous.rdv_arrivee between @date1 and @date2";
                selectQuery += " and rdv_statut = 1 and (localisation = 1 or localisation = 2)";

                //Patients sur fauteuil exclus
                //selectQuery += " and (coalesce(rendez_vous.heure_fauteuil, '01/01/01') < coalesce(rendez_vous.rdv_arrivee, '01/01/01'))";

                // selectQuery += " and (coalesce(rendez_vous.heure_sorti, '01/01/01') <= coalesce(rendez_vous.rdv_arrivee, '01/01/01') ";
                //selectQuery += " or coalesce(rendez_vous.heure_sorti, '01/01/01') <= coalesce(rendez_vous.heure_salleattente, '01/01/01')";
                //selectQuery += " or coalesce(rendez_vous.heure_sorti, '01/01/01') <= coalesce(rendez_vous.heure_fauteuil, '01/01/01') ";
                //selectQuery += " or coalesce(rendez_vous.heure_sorti, '01/01/01') <= coalesce(rendez_vous.heure_secretariat, '01/01/01')) ";
                selectQuery += " order by localisation,rendez_vous.rdv_arrivee";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@date1", DateTime.Now.Date);
                command.Parameters.AddWithValue("@date2", DateTime.Now.Date.AddHours(24));

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
                connection.Close();

            }





        }


        public static DataTable getPatients(string param)
        {

            if (param == "") return null;
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {



                string selectQuery = "select personne.id_personne,";
                //selectQuery += " personne.id_adresse,"; 
                //selectQuery += " personne.id_util,"; 
                //selectQuery += " personne.id_caisse,"; 
                //selectQuery += " personne.adr_id_adresse,"; 
                selectQuery += " personne.per_nom,";
                //selectQuery += " personne.per_nomjf,"; 
                selectQuery += " personne.per_prenom";


                selectQuery += " from personne";
                selectQuery += " inner join patient p on personne.id_personne=p.id_personne and p.TEST_BP=1";
                selectQuery += " where UPPER(per_nom) Like '" + param.Replace("'", "''").ToUpper() + "%'";

                //Patients sur fauteuil exclus
                //selectQuery += " and (coalesce(rendez_vous.heure_fauteuil, '01/01/01') < coalesce(rendez_vous.rdv_arrivee, '01/01/01'))";

                // selectQuery += " and (coalesce(rendez_vous.heure_sorti, '01/01/01') <= coalesce(rendez_vous.rdv_arrivee, '01/01/01') ";
                //selectQuery += " or coalesce(rendez_vous.heure_sorti, '01/01/01') <= coalesce(rendez_vous.heure_salleattente, '01/01/01')";
                //selectQuery += " or coalesce(rendez_vous.heure_sorti, '01/01/01') <= coalesce(rendez_vous.heure_fauteuil, '01/01/01') ";
                //selectQuery += " or coalesce(rendez_vous.heure_sorti, '01/01/01') <= coalesce(rendez_vous.heure_secretariat, '01/01/01')) ";
                selectQuery += " order by personne.per_nom,personne.per_prenom";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@date1", DateTime.Now.Date);
                command.Parameters.AddWithValue("@date2", DateTime.Now.Date.AddHours(24));

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];

                return dt;

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



        public static DataTable getPatientsInAttenteFor(Fauteuil faut)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {



                string selectQuery = "select personne.id_personne,";
                //selectQuery += " personne.id_adresse,"; 
                //selectQuery += " personne.id_util,"; 
                //selectQuery += " personne.id_caisse,"; 
                //selectQuery += " personne.adr_id_adresse,"; 
                selectQuery += " personne.per_nom,";
                //selectQuery += " personne.per_nomjf,"; 
                selectQuery += " personne.per_prenom,";
                selectQuery += " personne.PER_PRENOM, ";
                selectQuery += " personne.PER_TELPRINC,";
                selectQuery += " rendez_vous.ID_FAUTEUIL, personne.PER_EMAIL";


                selectQuery += " from rendez_vous";
                selectQuery += " inner join personne on personne.id_personne = rendez_vous.id_personne";
                selectQuery += " inner join patient p on personne.id_personne=p.id_personne";
                selectQuery += " where rdv_date between @date1 and @date2";
                selectQuery += " and rendez_vous.rdv_arrivee between @date1 and @date2";
                selectQuery += " and rdv_statut = 1 and (localisation = 1)";
                selectQuery += " and rendez_vous.ID_FAUTEUIL = @idfaut";

                //Patients sur fauteuil exclus
                //selectQuery += " and (coalesce(rendez_vous.heure_fauteuil, '01/01/01') < coalesce(rendez_vous.rdv_arrivee, '01/01/01'))";

                // selectQuery += " and (coalesce(rendez_vous.heure_sorti, '01/01/01') <= coalesce(rendez_vous.rdv_arrivee, '01/01/01') ";
                //selectQuery += " or coalesce(rendez_vous.heure_sorti, '01/01/01') <= coalesce(rendez_vous.heure_salleattente, '01/01/01')";
                //selectQuery += " or coalesce(rendez_vous.heure_sorti, '01/01/01') <= coalesce(rendez_vous.heure_fauteuil, '01/01/01') ";
                //selectQuery += " or coalesce(rendez_vous.heure_sorti, '01/01/01') <= coalesce(rendez_vous.heure_secretariat, '01/01/01')) ";
                selectQuery += " order by rendez_vous.rdv_arrivee";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@date1", DateTime.Now.Date);
                command.Parameters.AddWithValue("@date2", DateTime.Now.Date.AddHours(24));
                command.Parameters.AddWithValue("@idfaut", faut.Id);

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
                connection.Close();

            }





        }



        /*
        public static DataTable getPatientsEnAttenteOuSurFauteuil()
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "select personne.id_personne,";
                //selectQuery += " personne.id_adresse,"; 
                //selectQuery += " personne.id_util,"; 
                //selectQuery += " personne.id_caisse,"; 
                //selectQuery += " personne.adr_id_adresse,"; 
                selectQuery += " personne.per_nom,";
                //selectQuery += " personne.per_nomjf,"; 
                selectQuery += " personne.per_prenom,";
                selectQuery += " rendez_vous.ID_FAUTEUIL";


                selectQuery += " from rendez_vous";
                selectQuery += " inner join personne on personne.id_personne = rendez_vous.id_personne";
                selectQuery += " inner join patient p on personne.id_personne=p.id_personne";
                selectQuery += " where rdv_date between @date1 and @date2";
                selectQuery += " and rendez_vous.rdv_arrivee between @date1 and @date2";

                //Patients sur fauteuil exclus
                //selectQuery += " and (coalesce(rendez_vous.heure_fauteuil, '01/01/01') < coalesce(rendez_vous.rdv_arrivee, '01/01/01'))";

                selectQuery += " and (coalesce(rendez_vous.heure_sorti, '01/01/01') <= coalesce(rendez_vous.rdv_arrivee, '01/01/01') ";
                selectQuery += " or coalesce(rendez_vous.heure_sorti, '01/01/01') <= coalesce(rendez_vous.heure_salleattente, '01/01/01')";
                selectQuery += " or coalesce(rendez_vous.heure_sorti, '01/01/01') <= coalesce(rendez_vous.heure_fauteuil, '01/01/01') ";
                selectQuery += " or coalesce(rendez_vous.heure_sorti, '01/01/01') <= coalesce(rendez_vous.heure_secretariat, '01/01/01')) ";
                selectQuery += " order by personne.per_nom,personne.per_prenom";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@date1", DateTime.Now.Date);
                command.Parameters.AddWithValue("@date2", DateTime.Now.Date.AddHours(24));

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                return dt;

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
        */


        public static DataTable getPatientsFamillyMembers(basePatient pat)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {



                string selectQuery = "select linkedpers.id_personne,";
                selectQuery += " linkedpers.per_nom,";
                selectQuery += " linkedpers.per_prenom,";
                selectQuery += " linkedpers.PER_PRENOM, ";
                selectQuery += " linkedpers.PER_TELPRINC,";
                selectQuery += " linkedpers.PER_EMAIL";
                selectQuery += " from personne linkedpers";
                selectQuery += " inner join patient p on linkedpers.id_personne=p.id_personne";
                selectQuery += " inner join lienpers lp1 on lp1.id_patient=linkedpers.id_personne and lp1.relation = 'Rs'";
                selectQuery += " inner join lienpers lp2 on lp2.id_personne=lp1.id_personne and lp2.relation = 'Rs' and lp2.id_personne<>-1";
                selectQuery += " where lp2.id_patient =  @id_patient and linkedpers.id_personne<>@id_patient ";
                selectQuery += " order by linkedpers.PER_NOM, linkedpers.PER_PRENOM";
                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@id_patient", pat.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];

                return dt;

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


        public static DataTable getPersonnesAContacter(basePatient patient)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
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
                selectQuery += "PER_ADR1_prof,";
                selectQuery += "PER_ADR2_prof,";
                selectQuery += "PER_VILLE_prof,";
                selectQuery += "PER_CPOSTAL_prof,";
                selectQuery += "PER_ADR1,";
                selectQuery += "PER_ADR2,";
                selectQuery += "PER_VILLE,";
                selectQuery += "PER_CPOSTAL,";
                selectQuery += "PER_NOTES,";
                selectQuery += "PERS_TITRE,";
                selectQuery += "per_secu,";
                selectQuery += "TUVOUS,";
                selectQuery += "OI_LOGIN,";
                selectQuery += "OI_MDP,";
                selectQuery += "PREF_COM,";
                selectQuery += "CATEGORIES,";
                selectQuery += "lnk.RELATION, ";
                selectQuery += "lnk.TYPELIEN, ";
                selectQuery += "lnk.ID_PATIENT, ";
                selectQuery += "TYPE_PERS.NOM as PROFESSION, ";
                selectQuery += "AutreProfession, ";
                selectQuery += "TYPE_PERS.ID_TYPE as TYPE ";
                selectQuery += "FROM PERSONNE ";
                selectQuery += "INNER JOIN TYPE_PERS on TYPE_PERS.ID_TYPE=PERSONNE.PER_TYPE ";
                selectQuery += "INNER JOIN lienpers lnk on lnk.ID_PERSONNE=PERSONNE.ID_PERSONNE ";

                selectQuery += " Where lnk.id_patient = @IdPatient and lnk.Relation = 'Ac'";
                selectQuery += " order by PER_NOM,PER_PRENOM";


                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@IdPatient", patient.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);


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




        public static DataRow getinfocomplementaire(int IdPat)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {



                string selectQuery = "select idpatient, ";

                selectQuery += " assistante_resp, ";
                selectQuery += " SEMESTRESENTAMES, ";

                selectQuery += " AMELIORATIONS, ";
                selectQuery += " DEBUTTRAITEMENTENVISAGE, ";
                selectQuery += " PRATICIEN_UNIQUE,ASSISTANTE_UNIQUE, ";

                selectQuery += " praticien_resp ";
                selectQuery += " from basediag_infocomplementaire";
                selectQuery += " where idpatient = @idpatient";


                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@idpatient", IdPat);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

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

        public static void setinfocomplementaire(InfoPatientComplementaire infocompl)
        {
            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "INSERT INTO basediag_infocomplementaire (idpatient, ";
                    selectQuery += "                                      assistante_resp, ";
                    selectQuery += "                                      SEMESTRESENTAMES, ";
                    selectQuery += "                                      AMELIORATIONS, ";
                    selectQuery += "                                      PRATICIEN_UNIQUE,ASSISTANTE_UNIQUE, ";
                    selectQuery += "                                      DEBUTTRAITEMENTENVISAGE, ";
                    selectQuery += "                                          praticien_resp)";
                    selectQuery += " values (@idpatient, ";
                    selectQuery += "         @assistante_resp, ";
                    selectQuery += "         @SEMESTRESENTAMES,";
                    selectQuery += "         @AMELIORATIONS,";
                    selectQuery += "         @PRATICIEN_UNIQUE,@ASSISTANTE_UNIQUE,";
                    selectQuery += "         @DEBUTTRAITEMENTENVISAGE,";
                    selectQuery += "         @praticien_resp) ON DUPLICATE KEY UPDATE ";
                    selectQuery += "                                      idpatient = @idpatient, ";
                    selectQuery += "                                      assistante_resp = @assistante_resp, ";
                    selectQuery += "                                      SEMESTRESENTAMES = @SEMESTRESENTAMES, ";
                    selectQuery += "                                      AMELIORATIONS = @AMELIORATIONS, ";
                    selectQuery += "                                      PRATICIEN_UNIQUE= @PRATICIEN_UNIQUE, ASSISTANTE_UNIQUE= @ASSISTANTE_UNIQUE, ";
                    selectQuery += "                                      DEBUTTRAITEMENTENVISAGE = @DEBUTTRAITEMENTENVISAGE, ";
                    selectQuery += "                                          praticien_resp = @praticien_resp ";
                    //   selectQuery += " where idpatient=@idpat";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@idpatient", infocompl.IdPatient);
                    command.Parameters.AddWithValue("@idpat", infocompl.IdPatient);
                    command.Parameters.AddWithValue("@assistante_resp", infocompl.AssistanteResponsable == null ? DBNull.Value : (object)infocompl.AssistanteResponsable.Id);
                    command.Parameters.AddWithValue("@praticien_resp", infocompl.PraticienResponsable == null ? DBNull.Value : (object)infocompl.PraticienResponsable.Id);
                    command.Parameters.AddWithValue("@PRATICIEN_UNIQUE", infocompl.PraticienUnique);

                    command.Parameters.AddWithValue("@ASSISTANTE_UNIQUE", infocompl.AssistanteUnique);

                    command.Parameters.AddWithValue("@SEMESTRESENTAMES", infocompl.NbSemestresEntame);
                    command.Parameters.AddWithValue("@AMELIORATIONS", infocompl.Ameliorations);
                    command.Parameters.AddWithValue("@DEBUTTRAITEMENTENVISAGE", infocompl.DateDebutTraitement);


                    DataSet ds = new DataSet();
                    MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                    adapt.Fill(ds);
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


        public static List<String> getRisques(basePatient pat)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {



                string selectQuery = "select ";
                selectQuery += " RISQUES ";
                selectQuery += " from basediag_infocomplementaire";
                selectQuery += " where idpatient = @idpatient";


                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@idpatient", pat.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];

                List<String> lst = new List<string>();

                if (dt.Rows.Count == 0) return lst;

                lst = Convert.ToString(dt.Rows[0]["RISQUES"]).Split('\n').ToList();

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

        public static void setRisques(basePatient pat)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "insert into basediag_infocomplementaire (idpatient, risques)";
                selectQuery += " values (@idpatient, @RISQUES)";
                selectQuery += " on duplicate key update risques = @RISQUES";
/*
                string selectQuery = "update or insert into basediag_infocomplementaire (idpatient,";
                selectQuery += "                                      risques)";
                selectQuery += " values (@idpatient, ";
                selectQuery += "         @RISQUES)";
                selectQuery += "        MATCHING (idpatient)";
*/
                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@idpatient", pat.Id);
                command.Parameters.AddWithValue("@RISQUES", pat.Risques.Count > 0 ? pat.Risques.Aggregate((i, j) => i + "\n" + j) : "");


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
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



        public static DataTable GetPatientsEnRDVAt(DateTime dte1, DateTime dte2)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {



                string selectQuery = "select personne.id_personne,";
                //selectQuery += " personne.id_adresse,"; 
                //selectQuery += " personne.id_util,"; 
                //selectQuery += " personne.id_caisse,"; 
                //selectQuery += " personne.adr_id_adresse,"; 
                selectQuery += " personne.per_nom,";
                selectQuery += " personne.per_nomjf,";
                selectQuery += " personne.per_prenom";


                selectQuery += ", personne.per_genre,";
                selectQuery += " personne.per_secu,";
                selectQuery += " personne.per_type,";
                //selectQuery += " personne.per_telprinc,";
                //selectQuery += " personne.per_teltrav1,";
                //selectQuery += " personne.per_teltrav2,"; 
                //selectQuery += " personne.per_telecopie,";
                //selectQuery += " personne.per_email,";
                // selectQuery += " personne.per_reception,"; 
                selectQuery += " personne.per_notes,";
                //selectQuery += " personne.per_poste,"; 
                selectQuery += " personne.pcom,";
                //selectQuery += " personne.per_adr1,";
                //selectQuery += " personne.per_adr2,";
                //selectQuery += " personne.per_ville,";
                //selectQuery += " personne.per_cpostal,";
                //selectQuery += " personne.per_adr1_prof,"; 
                //selectQuery += " personne.per_adr2_prof,"; 
                //selectQuery += " personne.per_cpostal_prof,"; 
                //selectQuery += " personne.per_ville_prof,"; 
                selectQuery += " personne.profession,";
                // selectQuery += " personne.mutuelle,"; 
                selectQuery += " personne.per_datnaiss,";
                selectQuery += " personne.tuvous,";
                //selectQuery += " personne.poid,"; 
                //selectQuery += " personne.email2,"; 
                //selectQuery += " personne.gsm,"; 
                //selectQuery += " personne.icq,"; 
                //selectQuery += " personne.im1,"; 
                // selectQuery += " personne.im2,"; 
                //selectQuery += " personne.lastmodif,"; 
                // selectQuery += " personne.telsup0,"; 
                // selectQuery += " personne.telsup3,"; 
                // selectQuery += " personne.telsup4,"; 
                // selectQuery += " personne.telsup5,"; 
                // selectQuery += " personne.telsup6,"; 
                //selectQuery += " personne.telsup8,"; 
                // selectQuery += " personne.telsup10,"; 
                //selectQuery += " personne.telsup11,"; 
                //selectQuery += " personne.telsup12,"; 
                //selectQuery += " personne.telsup13,"; 
                //selectQuery += " personne.telsup14,"; 
                //selectQuery += " personne.telsup15,"; 
                //selectQuery += " personne.telsup16,"; 
                //selectQuery += " personne.telsup17,"; 
                //selectQuery += " personne.telsup18,"; 
                //selectQuery += " personne.indicetel1,"; 
                //selectQuery += " personne.indicetel2,"; 
                //selectQuery += " personne.indicetel3,"; 
                //selectQuery += " personne.indicetel4,"; 
                //selectQuery += " personne.email3,"; 
                //selectQuery += " personne.indiceemail,"; 
                //selectQuery += " personne.indiceadr,"; 
                //selectQuery += " personne.pays_dom,"; 
                //selectQuery += " personne.pays_trav,"; 
                selectQuery += " personne.pers_titre,";
                //selectQuery += " personne.pers_siteweb,"; 
                //selectQuery += " personne.per_ville_naissance,"; 
                selectQuery += " personne.per_pays_naissance,";
                //selectQuery += " personne.per_langue_parle,"; 
                //selectQuery += " personne.per_population_ref,"; 
                //selectQuery += " personne.nom_rep_image,"; 
                selectQuery += "        personne.oi_login,";
                selectQuery += "        personne.oi_mdp,";
                selectQuery += "        personne.oi_profil,";
                selectQuery += "        personne.oi_autorisation,";
                selectQuery += "        personne.categories,";
                selectQuery += "        personne.pref_com,";


                selectQuery += " p.code_banque, ";
                selectQuery += " p.code_guichet, ";
                selectQuery += " p.num_compte, ";
                selectQuery += " p.cle_rib, ";
                selectQuery += " p.nom_banque, ";
                selectQuery += " p.Titulaire, ";

                selectQuery += " PAT_DIAG,";
                selectQuery += " PAT_PLAN,";
                selectQuery += " ID_STATUT,";

                selectQuery += " PAT_REFDOSSIER,";
                selectQuery += " PERCENTAGEMUTUELLE,";


                selectQuery += " PAT_OBJECTIF_TRAIT,";
                selectQuery += " p.ALLERGIE,";
                selectQuery += " p.StatusClinique,";
                selectQuery += " p.NUM_MOULAGE,";
                selectQuery += " p.DATEABANDON,";
                selectQuery += " p.PAT_APPAREIL,";
                selectQuery += " p.TEST_BP,";


                selectQuery += " p.REFARCHIVE,";
                selectQuery += " p.PAT_NUMDOSSIER,";
                selectQuery += "(select First 1 rdv_date from rendez_vous where personne.ID_PERSONNE  = rendez_vous.ID_PERSONNE and RDV_ARRIVEE is null and RDV_DATE>current_timestamp  order by rdv_date) as NextRDV";


                selectQuery += " from personne";
                selectQuery += " inner join patient p on personne.id_personne=p.id_personne";
                selectQuery += " inner join rendez_vous rdv on rdv.id_personne=p.id_personne and rdv.RDV_DATE between @dte1 and @dte2";

                selectQuery += " order by personne.PER_NOM, personne.PER_PRENOM";
                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@dte1", dte1);
                command.Parameters.AddWithValue("@dte2", dte2);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];

                return dt;

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


        public static DataRow getPatient(int Id)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {



                string selectQuery = "select personne.id_personne,";
                //selectQuery += " personne.id_adresse,"; 
                //selectQuery += " personne.id_util,"; 
                //selectQuery += " personne.id_caisse,"; 
                //selectQuery += " personne.adr_id_adresse,"; 
                selectQuery += " personne.per_nom,";
                selectQuery += " personne.per_nomjf,";
                selectQuery += " personne.per_prenom";


                selectQuery += ", personne.per_genre,";
                selectQuery += " personne.per_secu,";
                selectQuery += " personne.per_type,";
                //selectQuery += " personne.per_telprinc,";
                //selectQuery += " personne.per_teltrav1,";
                //selectQuery += " personne.per_teltrav2,"; 
                //selectQuery += " personne.per_telecopie,";
                //selectQuery += " personne.per_email,";
                // selectQuery += " personne.per_reception,"; 
                selectQuery += " personne.per_notes,";
                //selectQuery += " personne.per_poste,"; 
                selectQuery += " personne.pcom,";
                //selectQuery += " personne.per_adr1,";
                //selectQuery += " personne.per_adr2,";
                //selectQuery += " personne.per_ville,";
                //selectQuery += " personne.per_cpostal,";
                //selectQuery += " personne.per_adr1_prof,"; 
                //selectQuery += " personne.per_adr2_prof,"; 
                //selectQuery += " personne.per_cpostal_prof,"; 
                //selectQuery += " personne.per_ville_prof,"; 
                selectQuery += " personne.profession,";
                // selectQuery += " personne.mutuelle,"; 
                selectQuery += " personne.per_datnaiss,";
                selectQuery += " personne.tuvous,";
                //selectQuery += " personne.poid,"; 
                //selectQuery += " personne.email2,"; 
                //selectQuery += " personne.gsm,"; 
                //selectQuery += " personne.icq,"; 
                //selectQuery += " personne.im1,"; 
                // selectQuery += " personne.im2,"; 
                //selectQuery += " personne.lastmodif,"; 
                // selectQuery += " personne.telsup0,"; 
                // selectQuery += " personne.telsup3,"; 
                // selectQuery += " personne.telsup4,"; 
                // selectQuery += " personne.telsup5,"; 
                // selectQuery += " personne.telsup6,"; 
                //selectQuery += " personne.telsup8,"; 
                // selectQuery += " personne.telsup10,"; 
                //selectQuery += " personne.telsup11,"; 
                //selectQuery += " personne.telsup12,"; 
                //selectQuery += " personne.telsup13,"; 
                //selectQuery += " personne.telsup14,"; 
                //selectQuery += " personne.telsup15,"; 
                //selectQuery += " personne.telsup16,"; 
                //selectQuery += " personne.telsup17,"; 
                //selectQuery += " personne.telsup18,"; 
                //selectQuery += " personne.indicetel1,"; 
                //selectQuery += " personne.indicetel2,"; 
                //selectQuery += " personne.indicetel3,"; 
                //selectQuery += " personne.indicetel4,"; 
                //selectQuery += " personne.email3,"; 
                //selectQuery += " personne.indiceemail,"; 
                //selectQuery += " personne.indiceadr,"; 
                //selectQuery += " personne.pays_dom,"; 
                //selectQuery += " personne.pays_trav,"; 
                selectQuery += " personne.pers_titre,";
                //selectQuery += " personne.pers_siteweb,"; 
                //selectQuery += " personne.per_ville_naissance,"; 
                selectQuery += " personne.per_pays_naissance,";
                //selectQuery += " personne.per_langue_parle,"; 
                //selectQuery += " personne.per_population_ref,"; 
                //selectQuery += " personne.nom_rep_image,"; 
                selectQuery += "        personne.oi_login,";
                selectQuery += "        personne.oi_mdp,";
                selectQuery += "        personne.oi_profil,";
                selectQuery += "        personne.oi_autorisation,";
                selectQuery += "        personne.categories,";
                selectQuery += "        personne.pref_com,";


                selectQuery += " p.code_banque, ";
                selectQuery += " p.code_guichet, ";
                selectQuery += " p.num_compte, ";
                selectQuery += " p.cle_rib, ";
                selectQuery += " p.nom_banque, ";
                selectQuery += " p.Titulaire, ";
                //            selectQuery += " p.RefArchive,";

                selectQuery += " p.PAT_DIAG,";
                selectQuery += " p.PAT_PLAN,";
                selectQuery += " p.ID_STATUT,";

                selectQuery += " p.PAT_REFDOSSIER,";
                selectQuery += " p.PERCENTAGEMUTUELLE,";


                selectQuery += " p.PAT_OBJECTIF_TRAIT,";
                selectQuery += " p.ALLERGIE,";
                selectQuery += " p.StatusClinique,";
                selectQuery += " p.NUM_MOULAGE,";
                selectQuery += " p.DATEABANDON,";
                selectQuery += " p.PAT_APPAREIL,";
                selectQuery += " p.TEST_BP,";
                selectQuery += " p.REFARCHIVE,";


                selectQuery += " p.PAT_NUMDOSSIER,";
                selectQuery += "(select rdv_date from rendez_vous where personne.ID_PERSONNE  = rendez_vous.ID_PERSONNE and RDV_ARRIVEE is null and RDV_DATE>current_timestamp  order by rdv_date  limit 1) as NextRDV";


                selectQuery += " from personne";
                selectQuery += " inner join patient p on personne.id_personne=p.id_personne";
                selectQuery += " where personne.id_personne=@Id ";



                selectQuery += " order by personne.PER_NOM, personne.PER_PRENOM";
                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@Id", Id);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];

                return dt.Rows[0];

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


        public static int getIdPersonneByLoginMDP(string login, string mdp)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {

                string selectQuery = "select personne.id_personne";
                selectQuery += " from personne";
                selectQuery += " where personne.oi_login=@oi_login and personne.oi_mdp=@oi_mdp  ";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@oi_login", login);
                command.Parameters.AddWithValue("@oi_mdp", mdp);

                object o = command.ExecuteScalar();
                if ((o is DBNull) || (o == null))
                    return -1;
                else
                    return Convert.ToInt32(o);



            }
            catch (System.IndexOutOfRangeException)
            {
                return -1;
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



        public static void UpdateReglement(basePatient p_patient)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {




                string selectQuery = "update patient set ";
                selectQuery += "     reglement =@reglement ";
                selectQuery += "     where id_personne =@id_personne";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id_personne", p_patient.Id);
                command.Parameters.AddWithValue("@reglement", p_patient.regelement);

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
        public static void UpdatePercentageMutuelle(basePatient p_patient)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {




                string selectQuery = "update patient set ";
                selectQuery += "     PERCENTAGEMUTUELLE =@PERCENTAGEMUTUELLE ";
                selectQuery += "     where id_personne =@id_personne";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id_personne", p_patient.Id);
                command.Parameters.AddWithValue("@PERCENTAGEMUTUELLE", p_patient.PourcentageMutuelle);

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

        public static void UpdatecoordoneesbancairePatient(basePatient p_patient)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {


                string selectQuery = "update patient set ";

                selectQuery += "     cle_rib = @cle_rib,";
                selectQuery += "     nom_banque = @nom_banque,";
                selectQuery += "     code_banque = @code_banque,";
                selectQuery += "     code_guichet = @code_guichet,";
                selectQuery += "     num_compte = @num_compte";
                selectQuery += "     Titulaire = @Titulaire";

                selectQuery += "     where id_personne =@id_personne";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);


                command.Parameters.AddWithValue("@id_personne", p_patient.Id);
                command.Parameters.AddWithValue("@cle_rib", p_patient.CleRIB);
                command.Parameters.AddWithValue("@nom_banque", p_patient.NomBanque);
                command.Parameters.AddWithValue("@code_banque", p_patient.CodeBanque);
                command.Parameters.AddWithValue("@code_guichet", p_patient.CodeGuichet);
                command.Parameters.AddWithValue("@num_compte", p_patient.NumCompte);
                command.Parameters.AddWithValue("@Titulaire", p_patient.Titulaire);



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
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {


                string selectQuery = " UPDATE personne SET ";
                //selectQuery += " id_adresse = @id_adresse,";
                //selectQuery += " id_util = @id_util,";
                //selectQuery += "     id_caisse = @id_caisse,";
                //selectQuery += "     adr_id_adresse = @adr_id_adresse,";
                selectQuery += "     per_nom = @per_nom,";
                selectQuery += "     per_nomjf = @per_nomjf,";
                selectQuery += "     per_prenom = @per_prenom,";
                selectQuery += "     per_genre = @per_genre,";
                selectQuery += "     per_secu = @per_secu,";
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
                selectQuery += "     pers_titre = @pers_titre, ";

                //selectQuery += "     pers_siteweb = @pers_siteweb,";
                //selectQuery += "     per_ville_naissance = @per_ville_naissance,";
                //selectQuery += "     per_pays_naissance = @per_pays_naissance,";
                //selectQuery += "     per_langue_parle = @per_langue_parle,";
                //selectQuery += "     per_population_ref = @per_population_ref,";
                //selectQuery += "     nom_rep_image = @nom_rep_image,";
                selectQuery += "     oi_login = @oi_login,";
                selectQuery += "     oi_mdp = @oi_mdp,";
                selectQuery += "     oi_profil = @oi_profil,";



                selectQuery += "     oi_autorisation = @oi_autorisation, ";
                // selectQuery += "     categories = @categories,";
                selectQuery += "     pref_com = @pref_com";
                selectQuery += " where (id_personne = @id_personne)";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id_personne", p_patient.Id);
                command.Parameters.AddWithValue("@per_nom", p_patient.Nom);
                command.Parameters.AddWithValue("@per_nomjf", p_patient.NomJF);
                command.Parameters.AddWithValue("@per_prenom", p_patient.Prenom);
                if(p_patient.Genre == basePatient.Sexe.Feminin)
                command.Parameters.AddWithValue("@per_genre", "F");
                else
                    command.Parameters.AddWithValue("@per_genre", "M");

                command.Parameters.AddWithValue("@per_secu", p_patient.NumSecu);
                command.Parameters.AddWithValue("@per_notes", p_patient.Notes);
                command.Parameters.AddWithValue("@profession", p_patient.Profession);
                command.Parameters.AddWithValue("@per_datnaiss", p_patient.DateNaissance);
                if (p_patient.Tutoiement)
                    command.Parameters.AddWithValue("@tuvous", 1);
                else
                    command.Parameters.AddWithValue("@tuvous", 0);
                command.Parameters.AddWithValue("@pers_titre", p_patient.Civilite);
                command.Parameters.AddWithValue("@oi_login", p_patient.IOlogin);
                command.Parameters.AddWithValue("@oi_mdp", p_patient.password);
                command.Parameters.AddWithValue("@oi_profil", p_patient.Idprofile);
                command.Parameters.AddWithValue("@oi_autorisation", p_patient.publication);
                command.Parameters.AddWithValue("@pref_com", ((char)p_patient.PrefCom).ToString());
                //   command.Parameters.AddWithValue("@PREF_COM", ((char)p_patient.PrefCom).ToString());












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
                selectQuery += "     REFARCHIVE = @pat_refarchive, ";
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
                selectQuery += "     StatusClinique=@StatusClinique,";

                selectQuery += "     cle_rib = @cle_rib,";
                selectQuery += "     nom_banque = @nom_banque,";
                selectQuery += "     code_banque = @code_banque,";
                selectQuery += "     code_guichet = @code_guichet,";
                selectQuery += "     num_compte = @num_compte,";
                selectQuery += "     Titulaire = @Titulaire";

                selectQuery += "     where id_personne =@id_personne";

                command.CommandText = selectQuery;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id_personne", p_patient.Id);
                //command.Parameters.AddWithValue("@per_id_personne", p_patient.per_id_personne);
                //command.Parameters.AddWithValue("@per2_id_personne", p_patient.per2_id_personne);
                //command.Parameters.AddWithValue("@per3_id_personne", p_patient.per3_id_personne);
                //command.Parameters.AddWithValue("@per4_id_personne", p_patient.per4_id_personne);
                //command.Parameters.AddWithValue("@per5_id_personne", p_patient.per5_id_personne);
                //command.Parameters.AddWithValue("@id_statut", p_patient.id_statut);
                command.Parameters.AddWithValue("@pat_numdossier", p_patient.Dossier);
                command.Parameters.AddWithValue("@pat_refdossier", p_patient.CasierInvisalign);
                command.Parameters.AddWithValue("@pat_refarchive", p_patient.RefArchive);
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

                command.Parameters.AddWithValue("@cle_rib", p_patient.CleRIB);
                command.Parameters.AddWithValue("@nom_banque", p_patient.NomBanque);
                command.Parameters.AddWithValue("@code_banque", p_patient.CodeBanque);
                command.Parameters.AddWithValue("@code_guichet", p_patient.CodeGuichet);
                command.Parameters.AddWithValue("@num_compte", p_patient.NumCompte);
                command.Parameters.AddWithValue("@Titulaire", p_patient.Titulaire);

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
    
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select max(id_personne)+1 as newid from personne";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                object o = command.ExecuteScalar();
                patient.Id = (o is DBNull) ? 1 : Convert.ToInt32(o);

                selectQuery = "insert into personne (";
                selectQuery += "id_personne,";
                selectQuery += "per_nom,";
                selectQuery += "per_prenom,";
                selectQuery += "per_telprinc,";
                selectQuery += "per_email,";
                selectQuery += "per_adr1,";
                selectQuery += "per_adr2,";
                selectQuery += "per_cpostal,";
                selectQuery += "per_ville,";
                selectQuery += "per_secu,";
                selectQuery += "per_datnaiss,";
                selectQuery += "per_genre,";
                selectQuery += "pers_titre,";
                selectQuery += "pref_com,";
                selectQuery += "profession,";

                selectQuery += "oi_login,";
                selectQuery += "oi_mdp,";
                selectQuery += "oi_profil,";
                selectQuery += "oi_autorisation,";

                /*
                selectQuery += "cle_rib, ";
                selectQuery += "nom_banque, ";
                selectQuery += "code_banque, ";
                selectQuery += "code_guichet, ";
                selectQuery += "num_compte, ";*/
                //selectQuery += "TEST_BP, ";

                selectQuery += "per_type";
                selectQuery += ") values (";
                selectQuery += "@ID_PERSONNE,";
                selectQuery += "@PER_NOM,";
                selectQuery += "@PER_PRENOM,";
                selectQuery += "@PER_TELPRINC,";
                selectQuery += "@PER_EMAIL,";
                selectQuery += "@PER_ADR1,";
                selectQuery += "@PER_ADR2,";
                selectQuery += "@PER_CPOSTAL,";
                selectQuery += "@PER_VILLE,";
                selectQuery += "@PER_SECU,";
                selectQuery += "@PER_DATNAISS,";
                selectQuery += "@PER_GENRE,";
                selectQuery += "@PERS_TITRE,";
                selectQuery += "@PREF_COM,";
                selectQuery += "@PROFESSION,";

                selectQuery += "@oi_login,";
                selectQuery += "@oi_mdp,";
                selectQuery += "@oi_profil,";
                selectQuery += "@oi_autorisation,";

                //selectQuery += "@testbp,";

                selectQuery += "@PER_TYPE)";

                command.CommandText = selectQuery;

                command.Parameters.AddWithValue("@ID_PERSONNE", patient.Id);
                command.Parameters.AddWithValue("@PER_NOM", patient.Nom);
                command.Parameters.AddWithValue("@PER_PRENOM", patient.Prenom);
                command.Parameters.AddWithValue("@PER_TELPRINC", patient.Tel);
                command.Parameters.AddWithValue("@PER_EMAIL", patient.Mail);
                command.Parameters.AddWithValue("@PROFESSION", patient.Profession);



                command.Parameters.AddWithValue("@PER_ADR1", patient.Adresse1);
                command.Parameters.AddWithValue("@PER_ADR2", patient.Adresse2);
                command.Parameters.AddWithValue("@PER_CPOSTAL", patient.CodePostal);
                command.Parameters.AddWithValue("@PER_VILLE", patient.Ville);


                command.Parameters.AddWithValue("@oi_login", patient.IOlogin);
                command.Parameters.AddWithValue("@oi_mdp", patient.password);
                command.Parameters.AddWithValue("@oi_profil", patient.Idprofile);
                command.Parameters.AddWithValue("@oi_autorisation", patient.publication);


                command.Parameters.AddWithValue("@PER_SECU", patient.NumSecu);
                command.Parameters.AddWithValue("@PER_DATNAISS", patient.DateNaissance);
                command.Parameters.AddWithValue("@PER_GENRE", patient.Genre == basePatient.Sexe.Feminin ? "F" : "M");
                command.Parameters.AddWithValue("@PERS_TITRE", patient.Civilite);
                command.Parameters.AddWithValue("@PREF_COM", ((char)patient.PrefCom).ToString());
                command.Parameters.AddWithValue("@PER_TYPE", 1);


                command.ExecuteNonQuery();


                selectQuery = "insert into patient (id_personne,per5_id_personne, per_id_personne, id_statut, pat_numdossier, pat_datecreation, pat_refdossier,test_bp,";

                selectQuery += "cle_rib, ";
                selectQuery += "nom_banque, ";
                selectQuery += "code_banque, ";
                selectQuery += "code_guichet, ";
                selectQuery += "num_compte, ";
                selectQuery += "titulaire, ";
                selectQuery += "refarchive";


                selectQuery += ")";
                selectQuery += "values (@id_personne,2, @per_id_personne,  0, @pat_numdossier, @pat_datecreation, @pat_refdossier,1,";

                selectQuery += "@cle_rib, ";
                selectQuery += "@nom_banque, ";
                selectQuery += "@code_banque, ";
                selectQuery += "@code_guichet, ";
                selectQuery += "@num_compte, ";
                selectQuery += "@Titulaire, ";
                selectQuery += "@pat_refarchive";

                selectQuery += ")";

                command.CommandText = selectQuery;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id_personne", patient.Id);
                command.Parameters.AddWithValue("@per_id_personne", patient.Id);
                command.Parameters.AddWithValue("@pat_numdossier", patient.Dossier);
                command.Parameters.AddWithValue("@pat_refdossier", patient.CasierInvisalign);
                command.Parameters.AddWithValue("@pat_datecreation", DateTime.Now);

                command.Parameters.AddWithValue("@cle_rib", patient.CleRIB);
                command.Parameters.AddWithValue("@nom_banque", patient.NomBanque);
                command.Parameters.AddWithValue("@code_banque", patient.CodeBanque);
                command.Parameters.AddWithValue("@code_guichet", patient.CodeGuichet);
                command.Parameters.AddWithValue("@num_compte", patient.NumCompte);
                command.Parameters.AddWithValue("@Titulaire", patient.Titulaire);

                command.Parameters.AddWithValue("@pat_refarchive", patient.RefArchive);


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

        public static DataTable getAllPatientsOf(Correspondant corres)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {

                string selectQuery = "select distinct ";
                selectQuery += " p.id_personne,";
                selectQuery += " p.per_nom,";
                selectQuery += " p.per_prenom, ";
                selectQuery += " p.PER_TELPRINC,";
                selectQuery += " p.PER_EMAIL";
                selectQuery += " from lienpers";
                selectQuery += " INNER JOIN personne p on p.id_personne=lienpers.id_patient";
                selectQuery += " INNER JOIN patient pat on pat.id_personne=lienpers.id_patient";
                selectQuery += " where lienpers.id_personne = @ID";
                selectQuery += " order by per_nom,per_prenom desc";
                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@ID", corres.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];

                return dt;

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


        public static DataTable getPatientsOf(Correspondant corres, DateTime dteValue)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {

                string selectQuery = "select ";
                selectQuery += " lienpers.Relation,";
                selectQuery += " lienpers.typelien,";
                selectQuery += " lienpers.id_patient,";
                selectQuery += " lienpers.id_personne";
                selectQuery += " from lienpers";
                selectQuery += " INNER JOIN patient p on p.id_personne=lienpers.id_patient";
                selectQuery += " where lienpers.id_personne = @ID";
                selectQuery += " and p.PAT_DATECREATION>=@creaDate";
                selectQuery += " order by p.PAT_DATECREATION";
                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@ID", corres.Id);
                command.Parameters.AddWithValue("@creaDate", dteValue);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];

                return dt;

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

        public static DataTable getAllPatients()
        {
            return getAllPatients(false, false);
        }


        public static DataTable getAllMateriels(bool includeOrthalis, bool includeArchived)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = "select personne.id_personne,";
                selectQuery += " per_nom,";
                selectQuery += " per_prenom, TYPE_MATERIEL ";
                selectQuery += " from personne";
                selectQuery += " inner join patient on personne.id_personne=patient.id_personne";
                if (!includeArchived)
                {
                    selectQuery += " left outer join base_histo_status on base_histo_status.id_patient=patient.id_personne and base_histo_status.datefin is null";
                    selectQuery += " left outer join statuts on statuts.id_statut=base_histo_status.id_status";
                }


                selectQuery += " where 1=1 and TYPE_MATERIEL = 1";
                if (!includeOrthalis)
                    selectQuery += " and patient.test_bp=1";

                if (!includeArchived)
                    selectQuery += " and (statuts.FAMILLE_STATUT is null or statuts.FAMILLE_STATUT<>1)";

                selectQuery += " order by per_nom, per_prenom";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];

                return dt;

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

        public static DataRow getMateriel(int id)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = "select id_personne,";
                selectQuery += " per_nom,";
                selectQuery += " per_prenom, per_datnaiss,lastmodif ";
                selectQuery += " from personne";
                selectQuery += " where id_personne = @id_personne";
                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@id_personne", id);
                command.CommandType = CommandType.Text;

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];


                if (dt.Rows.Count > 0)
                    return dt.Rows[0];
                else
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
        public static void updateMateriel(baseMaterielCabinet mat)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {                

                string selectQuery = "update personne set PER_NOM = @PER_NOM ,PER_PRENOM=@PER_PRENOM";
                selectQuery += " ,PER_DATNAISS=@PER_DATNAISS ,LASTMODIF=@LASTMODIF";
                selectQuery += " where ID_PERSONNE =  @ID_PERSONNE";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@ID_PERSONNE", mat.Id);
                command.Parameters.AddWithValue("@PER_NOM", mat.Libelle);
                command.Parameters.AddWithValue("@PER_PRENOM", mat.Description);
                command.Parameters.AddWithValue("@PER_DATNAISS", mat.DateAchat);
                command.Parameters.AddWithValue("@LASTMODIF", DateTime.Now);


                Object obj = command.ExecuteNonQuery();

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
        public static DataTable getAllPatients(bool includeOrthalis, bool includeArchived)
        {
            if (connection == null) getConnection();
            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = "select personne.id_personne, personne.PER_TELPRINC, personne.PER_EMAIL,";
                selectQuery += " per_nom,";
                selectQuery += " per_prenom ";
                selectQuery += " from personne";
                selectQuery += " inner join patient on personne.id_personne=patient.id_personne";
                if (!includeArchived)
                {
                    selectQuery += " left outer join base_histo_status on base_histo_status.id_patient=patient.id_personne and base_histo_status.datefin is null";
                    selectQuery += " left outer join statuts on statuts.id_statut = base_histo_status.id_status";
                }


                selectQuery += " where personne.TYPE_MATERIEL is NULL ";
                if (!includeOrthalis)
                    selectQuery += " and patient.test_bp=1";

                if (!includeArchived)
                    selectQuery += " and (statuts.FAMILLE_STATUT is null or statuts.FAMILLE_STATUT<>1)";

                selectQuery += " order by per_nom, per_prenom";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];
                return dt;

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
        public static DataTable getAllPatientsV2(string nom, string prenom, bool usePatientOrthalis, bool IncludeArchived, string email, string Tel, string ville, string cdPostal, string adresse)
        {
            if (connection == null) getConnection();
            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = "select personne.id_personne, personne.PER_TELPRINC, personne.PER_EMAIL,";
                selectQuery += " per_nom,";
                selectQuery += " per_prenom ";
                selectQuery += " from personne";
                selectQuery += " inner join patient on personne.id_personne=patient.id_personne";
                if (!IncludeArchived)
                {
                    selectQuery += " left outer join base_histo_status on base_histo_status.id_patient=patient.id_personne and base_histo_status.datefin is null";
                    selectQuery += " left outer join statuts on statuts.id_statut=base_histo_status.id_status";
                }


                selectQuery += " where personne.TYPE_MATERIEL is NULL ";
                if (!usePatientOrthalis)
                    selectQuery += " and patient.test_bp=1";

                if (!IncludeArchived)
                    selectQuery += " and (statuts.FAMILLE_STATUT is null or statuts.FAMILLE_STATUT<>1)";
                if (nom != string.Empty)
                    selectQuery += " and UPPER(per_nom) like @nom '%'";
                if (prenom != string.Empty)
                    selectQuery += " and UPPER(per_prenom) like @per_prenom '%'";
                if (email != string.Empty)
                    selectQuery += " and UPPER(PER_EMAIL) like @email '%'";
                if (Tel != string.Empty)
                    selectQuery += " and UPPER(PER_TELPRINC) like @Tel '%'";
                if (ville != string.Empty)
                    selectQuery += "  and personne.id_personne in (select c.ID_PERSONNE from contact c where UPPER(c.VILLE) like @ville '%')";
                if (cdPostal != string.Empty)
                    selectQuery += "  and personne.id_personne in (select c.ID_PERSONNE from contact c where UPPER(c.CODEPOSTAL) like @CODEPOSTAL '%')";
                if (adresse != string.Empty)
                    selectQuery += "  and personne.id_personne in (select c.ID_PERSONNE from contact c where UPPER(c.ADR1) like @ADR1 '%')";
                selectQuery += " order by per_nom, per_prenom";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                if (nom != string.Empty)
                    command.Parameters.AddWithValue("@nom", nom.ToUpper());
                if (prenom != string.Empty)
                    command.Parameters.AddWithValue("@per_prenom", prenom.ToUpper());
                if (email != string.Empty)
                    command.Parameters.AddWithValue("@email", email.ToUpper());
                if (Tel != string.Empty)
                    command.Parameters.AddWithValue("@Tel", Tel.ToUpper());
                if (ville != string.Empty)
                    command.Parameters.AddWithValue("@ville", ville.ToUpper());
                if (cdPostal != string.Empty)
                    command.Parameters.AddWithValue("@CODEPOSTAL", cdPostal.ToUpper());
                if (adresse != string.Empty)
                    command.Parameters.AddWithValue("@ADR1", adresse.ToUpper());
                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];
                return dt;

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


        public static DataTable getPatientsFromName(string nom, string prenom)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = "select personne.id_personne,";
                selectQuery += " per_nom,";
                selectQuery += " per_prenom,";
                selectQuery += " per_type,";
                selectQuery += " PER_PRENOM, ";
                selectQuery += " PER_TELPRINC,";
                selectQuery += " PER_EMAIL";
                selectQuery += " from personne";
                selectQuery += " inner join patient on personne.id_personne=patient.id_personne";
                selectQuery += " where";
                selectQuery += " upper(personne.per_nom) = @nom OR upper(personne.per_prenom) = @prenom";
                selectQuery += " order by personne.per_nom";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@nom", nom.ToUpper());
                command.Parameters.AddWithValue("@prenom", nom.ToUpper());

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];

                return dt;

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



        public static DataTable getPatientsFromName(string name)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = "select personne.id_personne,";
                selectQuery += " per_nom,";
                selectQuery += " per_prenom,";
                selectQuery += " per_type";
                selectQuery += " from personne";
                selectQuery += " inner join patient on personne.id_personne=patient.id_personne";
                selectQuery += " where";
                selectQuery += " and lower(personne.per_nom) LIKE '%" + @name + "%' OR upper(personne.per_nom) LIKE '%" + @name + "%'";
                selectQuery += " order by personne.per_nom";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@name", name);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];

                return dt;

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

        public static void addPatient(basePatient p_patient)
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
                string selectQuery = "select MAX(id_personne)+1 as NEWID from personne";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                try
                {
                    p_patient.Id = Convert.ToInt32(command.ExecuteScalar());
                }catch(System.Exception)
                {
                    p_patient.Id = 1;
                }



                selectQuery = "insert into personne (id_personne, per_nom,  per_prenom, per_type)";
                selectQuery += "values (@id_personne, @per_nom,  @per_prenom, 1)";
                command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id_personne", p_patient.Id);
                command.Parameters.AddWithValue("@per_nom", p_patient.Nom);
                command.Parameters.AddWithValue("@per_prenom", p_patient.Prenom);

                command.ExecuteNonQuery();

                selectQuery = "insert into patient (id_personne,per5_id_personne, per_id_personne, id_statut, pat_numdossier, pat_datecreation, pat_refdossier)";
                selectQuery += "values (@id_personne,2, @per_id_personne,  0, @pat_numdossier, @pat_datecreation, @pat_refdossier)";

                command.CommandText = selectQuery;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id_personne", p_patient.Id);
                command.Parameters.AddWithValue("@per_id_personne", p_patient.Id);
                command.Parameters.AddWithValue("@pat_numdossier", p_patient.Dossier);
                command.Parameters.AddWithValue("@pat_refdossier", p_patient.Dossier);
                command.Parameters.AddWithValue("@pat_datecreation", DateTime.Now);

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

        public static void UpdateTESTBasePractice(basePatient pat)
        {

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "update PATIENT set TEST_BP = @TEST_BP";
                selectQuery += " where ID_PERSONNE =  @IdPatient";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@IdPatient", pat.Id);
                command.Parameters.AddWithValue("@TEST_BP", pat.BasePracticeState);

                Object obj = command.ExecuteNonQuery();

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

        public static void updateTraitemnt(basePatient pat, StatusClinique sc)
        {

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "update PATIENT set ID_STATUT = @ID_STATUT";
                selectQuery += " where ID_PERSONNE =  @IdPatient";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@IdPatient", pat.Id);
                command.Parameters.AddWithValue("@ID_STATUT", sc.IdStatus);

                Object obj = command.ExecuteNonQuery();

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
