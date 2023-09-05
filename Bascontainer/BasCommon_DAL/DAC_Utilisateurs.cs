using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using System.Configuration;
using BasCommon_BO;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;


namespace BasCommon_DAL
{
    public static partial class DAC
    {



        public static DataTable GetPointages(Utilisateur user)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = @"select id, 
                                               datepointage, 
                                               id_utilisateur, 
                                               entresortie
                                        from base_pointage
                                        where   id_utilisateur=@id_utilisateur";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@id_utilisateur", user.Id);


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
               connection = null;

            }

        }


        public static DataTable GetPointages(Utilisateur user, DateTime dte)
        {
            return GetPointages(user, dte, dte);
        }

        public static DataTable GetPointages(Utilisateur user, DateTime dte, DateTime dte2)
        {
            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
                try
                {
                    string selectQuery = @"select id, 
                                               datepointage, 
                                               id_utilisateur, 
                                               entresortie
                                        from base_pointage
                                        where   id_utilisateur=@id_utilisateur and cast(datepointage as date) between @dte and @dte2";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection);

                    command.Parameters.AddWithValue("@id_utilisateur", user.Id);
                    command.Parameters.AddWithValue("@dte", dte.Date);
                    command.Parameters.AddWithValue("@dte2", dte2.Date.AddDays(1));


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



        public static DataTable getUtilisateursInFauteuil(Fauteuil f, DateTime dte)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
                try
                {
                    string selectQuery = "select ID_USER from rh_base_affect_faut_user";
                    selectQuery += " where rh_base_affect_faut_user.id_fauteuil = @idfauteuil";
                    selectQuery += " and @dte between rh_base_affect_faut_user.affecte_from and rh_base_affect_faut_user.affecte_to";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection);

                    command.Parameters.AddWithValue("@idfauteuil", f.Id);
                    command.Parameters.AddWithValue("@dte", dte);


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

        public static DataTable getUtilisateurs()
        {
            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
                try
                {

                    string selectQuery = "select u.util_actif,u.dateembauche,b.password as util_pwd,u.id_entityjuridique,u.datefincontrat , tp.nom as nomtype, p.id_personne, pers_titre, profession, per_nom, per_prenom, per_genre, per_type, per_email,per_notes, per_poste, pcom, per_telprinc,per_adr1,per_adr2,per_ville,per_cpostal,u.dateembauche ";
                    selectQuery += " ,AssMaladie";
                    selectQuery += " ,per_secu as numsecu";
                    selectQuery += " ,NumOrdre";
                    selectQuery += " ,DIPLOMENATIONAL";
                    selectQuery += " ,DiplomeUniversitaire";
                    selectQuery += " ,DiplomeOptionNATIONAL";
                    selectQuery += " ,UTIL_TYPE";
                    selectQuery += " ,per_datnaiss";
                    selectQuery += " ,per_nomjf";

                    selectQuery += " ,google_login";
                    selectQuery += " ,GOOGLE_PASSWD";
                    selectQuery += " ,GOOGLE_LASTSYNCHRO";
                   

                    selectQuery += " from personne p";
                    selectQuery += " inner join utilisateur u on u.id_personne = p.id_personne";
                    selectQuery += " inner join type_pers tp on tp.id_type = p.PER_TYPE";
                    selectQuery += " left join base_access b on b.IDUTILISATEUR = p.ID_PERSONNE";
                    selectQuery += " where UTIL_ACTIF='Y'";

                    selectQuery += " order by  per_nom, per_prenom";

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
                   connection.Close();
                }
            }
        }




        public static DataTable getUtilisateursStatus(Utilisateur p_utilisateur)
        {
            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
                try
                {

                    string selectQuery = "select rh_base_status.id,code,id_status,id_utilisateur,libelle, date_status_start, date_status_end,rh_status.absence from rh_base_status";
                    selectQuery += " inner join rh_status on rh_status.id=rh_base_status.id_status";
                    selectQuery += " where id_utilisateur=@id_personne";
                    selectQuery += " order by date_status_start";
                    MySqlCommand command = new MySqlCommand(selectQuery, connection);

                    command.Parameters.AddWithValue("@id_personne", p_utilisateur.Id);


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

        public static void AddUtilisateursStatus(UserStatus p_UserStatus)
        {

            lock (lockobj)
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


                    selectQuery = "insert into rh_base_status (id,id_utilisateur, id_status, date_status_start, date_status_end, creation_date)";
                    selectQuery += "values (@id,@id_utilisateur, @id_status, @date_status_start, @date_status_end, @creation_date)";

                    command.CommandText = selectQuery;
                    command.Parameters.AddWithValue("@id", Id);
                    command.Parameters.AddWithValue("@id_utilisateur", p_UserStatus.utilisateur.Id);
                    command.Parameters.AddWithValue("@id_status", p_UserStatus.status.Id);
                    command.Parameters.AddWithValue("@date_status_start", p_UserStatus.dateStart);
                    command.Parameters.AddWithValue("@date_status_end", p_UserStatus.dateEnd);
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


        }

        public static DateTime GetLastSynchroGoogle(Utilisateur u) {

            

            JObject obj = BasCommon_DAL.DAC.getMethodeJsonObjet("/getLastSynchroGoogleByIdUser/"+u.Id);
            if (obj == null) return DateTime.MinValue;
            return obj["lasteDateSycro"] == null ? DateTime.MinValue : Convert.ToDateTime(obj["google_LASTSYNCHRO"]);

        }



        public static DateTime GetLastSynchroGoogleOld(Utilisateur u)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection == null) return DateTime.MinValue;

                if (connection.State == ConnectionState.Closed) connection.Open();
                try
                {

                    string selectQuery = "select GOOGLE_LASTSYNCHRO FROM utilisateur where ID_PERSONNE=@Id";


                    MySqlCommand command = new MySqlCommand(selectQuery, connection);
                    command.Parameters.AddWithValue("@Id", u.Id);

                    object o = command.ExecuteScalar();


                    if ((o != null) && (!(o is DBNull)))
                        return Convert.ToDateTime(o);


                    return DateTime.MinValue;


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

        public static void UpdateSynchroGoogle(Utilisateur u, DateTime dte)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {

                    string selectQuery = "update utilisateur set GOOGLE_LASTSYNCHRO = @GOOGLE_LASTSYNCHRO where ID_PERSONNE=@Id";


                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@Id", u.Id);
                    command.Parameters.AddWithValue("@GOOGLE_LASTSYNCHRO", dte);

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


        public static void UpdateUtilisateursStatus(UserStatus p_UserStatus)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {

                    string selectQuery = "update rh_base_status set id_utilisateur = @id_utilisateur,id_status = @id_status,date_status_start = @date_status_start, date_status_end = @date_status_end where Id=@Id";


                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@id_utilisateur", p_UserStatus.utilisateur.Id);
                    command.Parameters.AddWithValue("@id_status", p_UserStatus.status.Id);
                    command.Parameters.AddWithValue("@date_status_start", p_UserStatus.dateStart);
                    command.Parameters.AddWithValue("@date_status_end", p_UserStatus.dateEnd);
                    command.Parameters.AddWithValue("@Id", p_UserStatus.Id);


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

        public static void DelUtilisateur(Utilisateur p_utilisateur)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "delete from utilisateur where id_util= @id";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@id", p_utilisateur.Id);

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

        public static void DelUtilisateursStatus(UserStatus p_UserStatus)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "delete from rh_base_status  where id= @id";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@id", p_UserStatus.Id);


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

        public static int GetNbJoursDeConges(Utilisateur p_utilisateur)
        {
            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
                try
                {

                    string selectQuery = "select pref_valeur from preference";
                    selectQuery += " where id_util=@id_personne and pref_cle='NbJourConge'";
                    MySqlCommand command = new MySqlCommand(selectQuery, connection);

                    command.Parameters.AddWithValue("@id_personne", p_utilisateur.Id);

                    int nbj = command.ExecuteScalar() == null ? 25 : Convert.ToInt32(command.ExecuteScalar());
                    p_utilisateur.NbJoursDeCongés = nbj;
                    return nbj;

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




        public static void AddPointage(Pointage p )
        {
            lock (lockobj)
            {
                if (connection == null) getConnection();
                if (connection == null) return;

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();


                string selectQuery = "select MAX(id)+1 as NEWID from base_pointage";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                object o = command.ExecuteScalar();
                p.Id = o is DBNull ? 1 : Convert.ToInt32(o);


                try
                {

                    selectQuery = @"insert into base_pointage (id, 
                                                                   datepointage, 
                                                                   id_utilisateur, 
                                                                   entresortie)
                                        values (@id, 
                                                @datepointage, 
                                                @id_utilisateur, 
                                                @entresortie)";
                    command = new MySqlCommand(selectQuery, connection, transaction);

                    command.Parameters.AddWithValue("@id", p.Id);
                    command.Parameters.AddWithValue("@datepointage", p.DateTimePointage);
                    command.Parameters.AddWithValue("@id_utilisateur", p.IdUser);
                    command.Parameters.AddWithValue("@entresortie", p.sens);
                    int nblinesaffected = command.ExecuteNonQuery();


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


        public static void SetNbJoursDeConges(Utilisateur p_utilisateur, int NBJours)
        {
            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {

                    string selectQuery = "update preference set pref_valeur = @pref_valeur where id_util=@id_util and pref_cle = @pref_cle";
                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                    command.Parameters.AddWithValue("@id_util", p_utilisateur.Id);
                    command.Parameters.AddWithValue("@pref_cle", "NbJourConge");
                    command.Parameters.AddWithValue("@pref_valeur", NBJours);
                    int nblinesaffected = command.ExecuteNonQuery();


                    if (nblinesaffected == 0)
                    {
                        selectQuery = "insert into preference (id_util, pref_cle, pref_valeur) values (@id_util, @pref_cle, @pref_valeur)";
                        command.CommandText = selectQuery;

                        command.ExecuteNonQuery();

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

        }

        public static void UpdateUtilisateur(Utilisateur p_utilisateur)
        {

            lock (lockobj)
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


                    string selectQuery = "update personne";
                    selectQuery += " set per_nom = @per_nom,";
                    selectQuery += "    per_prenom = @per_prenom,";
                    selectQuery += "    per_email = @per_email,";
                    selectQuery += "    profession = @profession,";
                    selectQuery += "    pers_titre = @pers_titre,";
                    selectQuery += "    per_adr1 = @per_adr1,";
                    selectQuery += "    per_adr2 = @per_adr2,";
                    selectQuery += "    per_ville = @per_ville,";
                    selectQuery += "    per_cpostal = @per_cpostal,";
                    selectQuery += "    per_telprinc = @per_tel,";
                    selectQuery += "    PER_NOMJF = @PER_NOMJF,";
                    selectQuery += "    PER_DATNAISS = @PER_DATNAISS,";
                    selectQuery += "    per_secu = @per_secu";

                    selectQuery += " where (id_personne = @id_personne)";


                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@id_personne", p_utilisateur.Id);
                    command.Parameters.AddWithValue("@per_nom", p_utilisateur.Nom);
                    command.Parameters.AddWithValue("@per_prenom", p_utilisateur.Prenom);
                    command.Parameters.AddWithValue("@per_email", p_utilisateur.Mail);
                    command.Parameters.AddWithValue("@profession", p_utilisateur.Profession);
                    command.Parameters.AddWithValue("@pers_titre", p_utilisateur.Civilite);
                    command.Parameters.AddWithValue("@per_adr1", p_utilisateur.Adresse.Adress1);
                    command.Parameters.AddWithValue("@per_adr2", p_utilisateur.Adresse.Adress2);
                    command.Parameters.AddWithValue("@per_cpostal", p_utilisateur.Adresse.CP);
                    command.Parameters.AddWithValue("@per_ville", p_utilisateur.Adresse.Ville);
                    command.Parameters.AddWithValue("@per_tel", p_utilisateur.Tel);
                    command.Parameters.AddWithValue("@PER_DATNAISS", p_utilisateur.DateNaiss);
                    command.Parameters.AddWithValue("@PER_NOMJF", p_utilisateur.NomJeuneFille);
                    command.Parameters.AddWithValue("@per_secu", p_utilisateur.NumSecu);
             









                    command.ExecuteNonQuery();



                    selectQuery = "update utilisateur";
                    selectQuery += " set util_ident = @util_ident,";
                    selectQuery += "    dateEmbauche = @dateEmbauche,";
                    selectQuery += "    DATEFINCONTRAT = @DATEFINCONTRAT,";
                    selectQuery += "    UTIL_TYPE = @UTIL_TYPE,";

                    selectQuery += "    AssMaladie = @AssMaladie,";
                    selectQuery += "    NumOrdre = @NumOrdre,";
                    selectQuery += "    DIPLOMENATIONAL = @DIPLOMENATIONAL,";
                    selectQuery += "    DiplomeUniversitaire = @DiplomeUniversitaire,";
                    selectQuery += "    DiplomeOptionNATIONAL = @DiplomeOptionNATIONAL,";

                    selectQuery += "    GOOGLE_LOGIN = @GOOGLE_LOGIN,";
                    selectQuery += "    GOOGLE_PASSWD = @GOOGLE_PASSWD,";

                    selectQuery += "     util_actif = @util_actif,";
                    selectQuery += "    ID_ENTITYJURIDIQUE= @ID_ENTITYJURIDIQUE";
                    selectQuery += " where (id_personne = @id_personne)";

                    command.CommandText = selectQuery;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_personne", p_utilisateur.Id);
                    command.Parameters.AddWithValue("@util_ident", p_utilisateur.Nom);
                    command.Parameters.AddWithValue("@util_actif", p_utilisateur.Actif == true ? "Y" : "N");
                    command.Parameters.AddWithValue("@dateEmbauche", p_utilisateur.DateEmbauche);
                    command.Parameters.AddWithValue("@DATEFINCONTRAT", p_utilisateur.DateFinContrat);
                    command.Parameters.AddWithValue("@UTIL_TYPE", (int)p_utilisateur.type);

                    command.Parameters.AddWithValue("@AssMaladie", p_utilisateur.AssMaladie);
                    command.Parameters.AddWithValue("@NumOrdre", p_utilisateur.NumOrdre);
                    command.Parameters.AddWithValue("@DIPLOMENATIONAL", p_utilisateur.DiplomeNational);
                    command.Parameters.AddWithValue("@DiplomeUniversitaire", p_utilisateur.DiplomeUniversitaire);
                    command.Parameters.AddWithValue("@DiplomeOptionNATIONAL", p_utilisateur.DiplomeOptionNational);

                    command.Parameters.AddWithValue("@GOOGLE_LOGIN", p_utilisateur.Google_Login);
                    command.Parameters.AddWithValue("@GOOGLE_PASSWD", p_utilisateur.Google_Password);
                    command.Parameters.AddWithValue("@ID_ENTITYJURIDIQUE", p_utilisateur.EntiteJuridique.Id);

                    command.ExecuteNonQuery();


                    selectQuery = "update base_access ";
                    selectQuery += " set PASSWORD = @passwprd ";
                    selectQuery += " where (IDUTILISATEUR = @id_personne)";
                    command.CommandText = selectQuery;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_personne", p_utilisateur.Id);
                    command.Parameters.AddWithValue("@passwprd", p_utilisateur .password  );
                    command.ExecuteNonQuery();
                   

                    selectQuery = "delete from rh_base_fauteuil_defaut";
                    selectQuery += " where id_utilisateur = @id_utilisateur";
                    command.CommandText = selectQuery;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_utilisateur", p_utilisateur.Id);
                    command.ExecuteNonQuery();


                    selectQuery = "insert into rh_base_fauteuil_defaut (id_utilisateur, id_fauteuil)";
                    selectQuery += " values (@id_utilisateur, @id_fauteuil)";

                    foreach (Fauteuil f in p_utilisateur.Fauteuils)
                    {
                        command.CommandText = selectQuery;
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@id_utilisateur", p_utilisateur.Id);
                        command.Parameters.AddWithValue("@id_fauteuil", f.Id);
                        command.ExecuteNonQuery();
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
        }

        public static void AddUtilisateur(Utilisateur p_utilisateur)
        {

            lock (lockobj)
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
                        p_utilisateur.Id = Convert.ToInt32(command.ExecuteScalar());
                    }
                    catch (Exception)
                    {

                        p_utilisateur.Id = 1;
                    }
                    



                    selectQuery = "insert into personne (id_personne,per_secu, per_nom,  per_prenom, per_type, per_email, Profession, pers_titre,PER_DATNAISS,PER_NOMJF) ";
                    selectQuery += " values (@id_personne,@per_secu, @per_nom,  @per_prenom, 2,@per_email,@Prof,@titre,@PER_DATNAISS,@PER_NOMJF)";
                    command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@id_personne", p_utilisateur.Id);
                    command.Parameters.AddWithValue("@per_nom", p_utilisateur.Nom);
                    command.Parameters.AddWithValue("@per_prenom", p_utilisateur.Prenom);
                    command.Parameters.AddWithValue("@per_email", p_utilisateur.Mail);
                    command.Parameters.AddWithValue("@prof", p_utilisateur.Profession);
                    command.Parameters.AddWithValue("@titre", p_utilisateur.Civilite);
                    command.Parameters.AddWithValue("@per_secu", p_utilisateur.NumSecu);
                    command.Parameters.AddWithValue("@PER_DATNAISS", p_utilisateur.DateNaiss);
                    command.Parameters.AddWithValue("@PER_NOMJF", p_utilisateur.NomJeuneFille);
                    command.Parameters.AddWithValue("@PER_TYPE",(int) p_utilisateur.type);
                    command.ExecuteNonQuery();

                    selectQuery = "insert into utilisateur (UTIL_TYPE,DATEFINCONTRAT, id_util, id_personne, util_ident, util_pwd,AssMaladie,NumOrdre,DIPLOMENATIONAL,DiplomeUniversitaire,DiplomeOptionNATIONAL,GOOGLE_LOGIN,GOOGLE_PASSWD)";
                    selectQuery += " values (@UTIL_TYPE,@DATEFINCONTRAT, @id_util, @id_personne, @util_ident, @util_pwd,@AssMaladie,@NumOrdre,@DIPLOMENATIONAL,@DiplomeUniversitaire,@DiplomeOptionNATIONAL,@GOOGLE_LOGIN,@GOOGLE_PASSWD)";

                    command.CommandText = selectQuery;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_util", p_utilisateur.Id);
                    command.Parameters.AddWithValue("@id_personne", p_utilisateur.Id);
                    command.Parameters.AddWithValue("@util_ident", p_utilisateur.Nom);
                    command.Parameters.AddWithValue("@DATEFINCONTRAT", p_utilisateur.DateFinContrat);
                    command.Parameters.AddWithValue("@UTIL_TYPE", (int)p_utilisateur.type);
                    command.Parameters.AddWithValue("@util_pwd", "");

                    command.Parameters.AddWithValue("@AssMaladie", p_utilisateur.AssMaladie);
                    command.Parameters.AddWithValue("@NumOrdre", p_utilisateur.NumOrdre);
                    command.Parameters.AddWithValue("@DIPLOMENATIONAL", p_utilisateur.DiplomeNational);
                    command.Parameters.AddWithValue("@DiplomeUniversitaire", p_utilisateur.DiplomeUniversitaire);
                    command.Parameters.AddWithValue("@DiplomeOptionNATIONAL", p_utilisateur.DiplomeOptionNational);

                    command.Parameters.AddWithValue("@GOOGLE_LOGIN", p_utilisateur.Google_Login);
                    command.Parameters.AddWithValue("@GOOGLE_PASSWD", p_utilisateur.Google_Password);


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

        public static void setPrivate(byte[] data, int p_UtilisateurId)
        {

            lock (lockobj)
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


                    string selectQuery = "update utilisateur";
                    selectQuery += " set private = @private ";
                    selectQuery += " where (id_personne = @id_personne)";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_personne", p_UtilisateurId);

                    command.Parameters.AddWithValue("@private", data);

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

        public static byte[] getPrivate(int p_UtilisateurId)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "select private";
                    selectQuery += " from utilisateur";
                    selectQuery += " where id_personne=@id";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                    command.Parameters.AddWithValue("@id", p_UtilisateurId);

                    object o = command.ExecuteScalar();

                    if (o is DBNull)
                        return null;
                    else
                        return (byte[])o;

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

        public static bool AccessIsAllow(string p_user, string p_password)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
                try
                {

                    string selectQuery = "select 1 from utilisateur u where u.util_ident=@User and u.util_pwd=@Password and util_type=-1";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection);


                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@User", p_user);
                    command.Parameters.AddWithValue("@Password", p_password);
                    return (command.ExecuteScalar() != null);



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

        public static DataTable getAffectedUser(DateTime Dte)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
                try
                {
                    string selectQuery = "select r.ISAFFECTED, r.id_fauteuil, tp.NOM as NOMTYPE, r.id_personne, id_adresse, id_util, id_caisse, adr_id_adresse, per_nom, per_nomjf, per_prenom, per_genre, per_secu, per_type, per_telprinc, per_teltrav1, per_teltrav2, per_telecopie, per_email, per_reception, per_notes, per_poste, pcom, per_adr1, per_adr2, per_ville, per_cpostal, per_adr1_prof, per_adr2_prof, per_cpostal_prof, per_ville_prof, profession, mutuelle, per_datnaiss, tuvous, poid, email2, gsm, icq, im1, im2, lastmodif, telsup0, telsup3, telsup4, telsup5, telsup6, telsup8, telsup10, telsup11, telsup12, telsup13, telsup14, telsup15, telsup16, telsup17, telsup18, indicetel1, indicetel2, indicetel3, indicetel4, email3, indiceemail, indiceadr, pays_dom, pays_trav, pers_titre, pers_siteweb, per_ville_naissance, per_pays_naissance, per_langue_parle, per_population_ref, nom_rep_image, oi_login, oi_mdp, oi_profil, oi_autorisation, categories, pref_com";
                    selectQuery += " from rh_base_user_faut r";
                    selectQuery += " inner join personne p on p.id_personne=r.id_personne";
                    selectQuery += " inner join TYPE_PERS tp on tp.id_type = p.PER_TYPE";
                    selectQuery += " where r.date_affecte=@date_affecte";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection);


                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@date_affecte", Dte);

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
    }
}
