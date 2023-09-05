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


        public static DataTable getDentistes()
        {
            if (connection == null) getConnection();   

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select ID_personne, per_nom, per_prenom,";
                selectQuery += " PER_TELPRINC,";
                selectQuery += " PER_EMAIL";
                selectQuery += " FROM personne";
                selectQuery += " WHERE PER_TYPE = 8";
                selectQuery += " order by PER_NOM,PER_PRENOM";

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
                transaction.Rollback();
                throw e;
            }
            finally
            {
               connection.Close();

            }

        }


        public static void RemoveLienCorrespondant(int IdPersonne, int Idpatient)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "delete from lienpers where ID_personne = @ID_personne and id_patient=@id_patient";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;




                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@ID_personne", IdPersonne);
                command.Parameters.AddWithValue("@id_patient", Idpatient);

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

        public static void RemoveLienCorrespondant(string Relation, int Idpatient)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "delete from lienpers where RELATION = @RELATION and id_patient=@id_patient";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;




                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@RELATION", Relation);
                command.Parameters.AddWithValue("@id_patient", Idpatient);

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

        public static DataRow getSmallPersonneFromId(int Id)
        {

            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {




                string selectQuery = "SELECT personne.ID_personne,";
                selectQuery += " PER_NOM,";
                selectQuery += " PER_PRENOM, ";
                selectQuery += " PER_PRENOM, ";
                selectQuery += " PER_TELPRINC,";
                selectQuery += " PER_EMAIL";
                selectQuery += " FROM personne ";
                selectQuery += " Where ID_personne=@ID_personne";
                
                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@ID_personne",Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();


                return (ds.Tables[0].Rows.Count == 0 ? null : ds.Tables[0].Rows[0]);



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


        public static DataTable getSmallPersonneFromName(string nom, string prenom)
        {

            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {




                string selectQuery = "SELECT personne.ID_personne,";
                selectQuery += " PER_NOM,";
                selectQuery += " PER_PRENOM ";
                selectQuery += " personne.PER_TELPRINC,";
                selectQuery += " personne.PER_EMAIL";
                selectQuery += " FROM personne ";
                selectQuery += " Where upper(PER_NOM) LIKE '%"+nom.ToUpperInvariant()+"'";
                if ((prenom != null)&&(prenom != "")) selectQuery += " and upper(PER_PRENOM) LIKE '%"+prenom.ToUpperInvariant()+"'";
                
  
                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
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


        public static DataTable getSmallPersonneFromName(string nom)
        {

            return getSmallPersonneFromName(nom, null);
        }


        public static DataRow getSmallPersonneFromExactName(string nom, string prenom)
        {

            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {




                string selectQuery = "SELECT personne.ID_personne,";
                selectQuery += " PER_NOM,";
                selectQuery += " PER_PRENOM ";
                selectQuery += " personne.PER_TELPRINC,";
                selectQuery += " personne.PER_EMAIL";
                selectQuery += " FROM personne ";
                selectQuery += " Where upper(PER_NOM) = '" + nom.ToUpperInvariant() + "'";
                selectQuery += " and upper(PER_PRENOM) = '" + prenom.ToUpperInvariant() + "'";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();


                if (ds.Tables[0].Rows.Count == 0) return null;

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


        public static DataRow getSmallCorrespondant(int Id)
        {

            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {




                string selectQuery = "SELECT personne.ID_personne,";
                selectQuery += " PER_NOM,";
                selectQuery += " PER_PRENOM,";
                selectQuery += " PER_TELPRINC,";
                selectQuery += " PER_EMAIL,";
                selectQuery += " lnk.Relation";
                selectQuery += " FROM personne ";
                selectQuery += " left outer JOIN lienpers lnk on lnk.ID_personne=personne.ID_personne ";

                selectQuery += " Where personne.ID_personne=@ID_personne";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@ID_personne", Id);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
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
        public static DataRow getUserMonDentisteById(int Id)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {




                string selectQuery = "SELECT * ";
                selectQuery += " from user u where u.id = @id";


                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@id", Id);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                return ds.Tables[0].Rows[0];



            }
            catch (System.IndexOutOfRangeException e)
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
        public static DataRow getUserByEmail(string email)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {




                string selectQuery = "SELECT * ";
                selectQuery += " from user u where u.email = @email";


                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@email", email);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                return ds.Tables[0].Rows[0];



            }
            catch (System.IndexOutOfRangeException e)
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
        public static void UpdateProfilInternet(Correspondant corres)
        {

            lock (lockobj)
            {

                if (connection == null) getConnection(); if (connection == null) return;

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "update personne set ";
                    selectQuery += " oi_login = @login,";
                    selectQuery += " oi_mdp = @password,";
                    selectQuery += " oi_profil = @profil,";
                    selectQuery += " oi_autorisation = @publication";
                    selectQuery += " where personne.ID_personne = @id";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.CommandType = CommandType.Text;
                    command.CommandText = selectQuery;

                    command.Parameters.AddWithValue("@id", corres.Id);
                    command.Parameters.AddWithValue("@login", corres.IOlogin);
                    command.Parameters.AddWithValue("@password", corres.password);
                    command.Parameters.AddWithValue("@profil", corres.Idprofile);
                    command.Parameters.AddWithValue("@publication", corres.publication);

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


        public static DataRow getCorrespondant(int Id)
        {

            if (connection == null) getConnection(); 

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {




                string selectQuery = "SELECT personne.ID_personne,";
                selectQuery += "PER_NOM,";
                selectQuery += "PER_PRENOM,";
                selectQuery += "PER_GENRE,";
                selectQuery += "PER_SECU,";
                selectQuery += "PER_DATNAISS,";
                selectQuery += "PER_NOTES,";
                selectQuery += "PROFESSION,";
                selectQuery += "AutreProfession,";                
                selectQuery += "PERS_TITRE,";
                selectQuery += "TUVOUS,";
                selectQuery += "PREF_COM,";
                selectQuery += "OI_LOGIN,";
                selectQuery += "OI_MDP,";
                selectQuery += "OI_PROFIL,";
                selectQuery += "OI_AUTORISATION,";
                selectQuery += "type_pers.NOM as PROFESSION, ";
                selectQuery += "type_pers.ID_TYPE as TYPE, ";
                selectQuery += "NOTE ";
                selectQuery += "FROM personne ";
                selectQuery += "INNER JOIN type_pers on type_pers.ID_TYPE=personne.PER_TYPE ";
                selectQuery += " LEFT JOIN base_histo_categorie on base_histo_categorie.ID_personne = personne.ID_personne and base_histo_categorie.DATE_FIN_CATEG is null and ID_CATEGORIE is null";
                selectQuery += " Where personne.ID_personne=@ID_personne";


                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@ID_personne", Id);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);


                return ds.Tables[0].Rows[0];



            }
            catch (System.IndexOutOfRangeException e)
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

        public static DataTable getCorrespondantsbyName(string Nom)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {




                string selectQuery = "SELECT personne.ID_personne,";
                selectQuery += "PER_NOM,";
                selectQuery += "PER_PRENOM, ";
                selectQuery += " PER_TELPRINC,";
                selectQuery += " PER_EMAIL ";
                selectQuery += "FROM personne ";
                selectQuery += " where (upper(PER_NOM)=upper(@nom)) ";



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@nom",Nom);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
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
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {




                string selectQuery = "SELECT personne.ID_personne,";
                selectQuery += "PER_NOM,";
                selectQuery += "PER_PRENOM, ";
                selectQuery += " PER_TELPRINC,";
                selectQuery += " PER_EMAIL ";
                selectQuery += "FROM personne ";
                selectQuery += " LEFT JOIN base_histo_categorie on base_histo_categorie.ID_personne = personne.ID_personne and base_histo_categorie.DATE_FIN_CATEG is null and ID_CATEGORIE is null";
                selectQuery += " INNER JOIN type_pers on type_pers.ID_TYPE = personne.PER_TYPE ";
                selectQuery += " where (PER_TYPE<>1) ";

                string paramselectQuery = "";

                if (Param != "")
                {

                    foreach (string s in Param.Split(';'))
                    {
                        
                        paramselectQuery += " and (UPPER(PER_EMAIL) LIKE '" + s.Trim().ToUpper() + "%'";
                        paramselectQuery += " or UPPER(type_pers.NOM) LIKE '" + s.Trim().ToUpper() + "%'";
                        paramselectQuery += " or UPPER(personne.ID_personne) LIKE '" + s.Trim().ToUpper() + "%')";
                    }
                }

                selectQuery += paramselectQuery;

                

                selectQuery += " order by PER_NOM,PER_PRENOM";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
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


        public static void DeleteCorrespondant(int Idp_corres)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "select count(1) from patient where patient.id_personne = @ID";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@ID", Idp_corres);
                int nbpat = 0;
                try
                {
                     nbpat = (int)command.ExecuteScalar();
                }catch(Exception exx)
                {
                }
                if (nbpat != 0)
                {
                    throw new Exception("Ce correspondant est un Patient!");
                    transaction.Rollback();
                }



                selectQuery = "DELETE from lienpers WHERE lienpers.id_personne = @ID";

                command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@ID", Idp_corres);

                command.ExecuteNonQuery();

                selectQuery = "DELETE from personne WHERE personne.ID_personne = @ID";

                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
              //  command.Parameters.AddWithValue("@ID", Idp_corres);

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


        public static DataTable getCorrespondantsSugested(string profession,string Param)
        {

            if (connection == null) getConnection(); 

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();

            try
            {




                string selectQuery = "SELECT personne.ID_personne,";
                selectQuery += " PER_NOM,";
                selectQuery += " PER_PRENOM, ";
                selectQuery += " PER_TELPRINC,";
                selectQuery += " PER_EMAIL";
                selectQuery += " FROM personne ";
                selectQuery += " where CHAR_LENGTH(TRIM(PER_NOM))>3 AND PROFESSION=@prof";
                selectQuery += " and upper(PER_NOM) LIKE '"+Param.ToUpperInvariant()+"%'";
                selectQuery += " order by PER_NOM,PER_PRENOM";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@prof",profession);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
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
               connection.Close();

            }





        }


        public static DataTable getCorrespondantsSugested(string Param)
        {

            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();

            try
            {




                string selectQuery = "SELECT personne.ID_personne,";
                selectQuery += " PER_NOM,";
                selectQuery += " PER_PRENOM,";
                selectQuery += " PER_TELPRINC,";
                selectQuery += " PER_EMAIL";
                selectQuery += " FROM personne ";
                selectQuery += " where CHAR_LENGTH(TRIM(PER_NOM))>3";
                selectQuery += " order by PER_NOM,PER_PRENOM";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
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
               connection.Close();

            }





        }


        public static int UpdateCorrespondant(Correspondant p_corres)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select MAX(ID_personne)+1 as NEWID from personne";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                int id = Convert.ToInt32(command.ExecuteScalar());

                selectQuery = "update personne set ";
                //selectQuery += " id_adresse = @id_adresse,";
                //selectQuery += " id_util = @id_util,";
                //selectQuery += "     id_caisse = @id_caisse,";
                //selectQuery += "     adr_id_adresse = @adr_id_adresse,";
                selectQuery += "     per_nom = @per_nom,";
                //selectQuery += "     per_nomjf = @per_nomjf,";
                selectQuery += "     per_prenom = @per_prenom,";
                //selectQuery += "     per_genre = @per_genre,";
                //selectQuery += "     per_secu = @per_secu,";
                selectQuery += "     per_type = @per_type,";
                //selectQuery += "     per_telprinc = @per_telprinc,";
                //selectQuery += "     per_teltrav1 = @per_teltrav1,";
                //selectQuery += "     per_teltrav2 = @per_teltrav2,";
                //selectQuery += "     per_telecopie = @per_telecopie,";
                //selectQuery += "     per_email = @per_email,";
                //selectQuery += "     per_reception = @per_reception,";
                selectQuery += "     per_notes = @per_notes,";
                selectQuery += "     per_secu = @per_numsecu,";
                //selectQuery += "     per_poste = @per_poste,";
                //selectQuery += "     pcom = @pcom,";

                //selectQuery += "     per_adr1_prof = @per_adr1p,";
                //selectQuery += "     per_adr2_prof = @per_adr2p,";
                //selectQuery += "     per_ville_prof = @per_villep,";
                //selectQuery += "     per_cpostal_prof = @per_cpostalp,";

                //selectQuery += "     per_adr1 = @per_adr1h,";
                //selectQuery += "     per_adr2 = @per_adr2h,";
                //selectQuery += "     per_ville = @per_villeh,";
                //selectQuery += "     per_cpostal = @per_cpostalh,";

                selectQuery += "     profession = @profession,";
                selectQuery += "     AutreProfession = @AutreProfession,";
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
                selectQuery += "     pers_titre = @pers_titre,";
                //selectQuery += "     pers_siteweb = @pers_siteweb,";
                //selectQuery += "     per_ville_naissance = @per_ville_naissance,";
                //selectQuery += "     per_pays_naissance = @per_pays_naissance,";
                //selectQuery += "     per_langue_parle = @per_langue_parle,";
                //selectQuery += "     per_population_ref = @per_population_ref,";
                //selectQuery += "     nom_rep_image = @nom_rep_image,";
                selectQuery += "     oi_login = @oi_login,";
                selectQuery += "     oi_mdp = @oi_mdp,";
                selectQuery += "     oi_profil = @oi_profil,";
                selectQuery += "     oi_autorisation = @oi_autorisation,";
                //selectQuery += "     categories = @categories,";
                selectQuery += "     pref_com = @pref_com,";
                selectQuery += "     PER_GENRE = @PER_GENRE";

                selectQuery += " where (id_personne = @id_personne)";
                command.CommandText = selectQuery;

                command.Parameters.AddWithValue("@id_personne", p_corres.Id);
                command.Parameters.AddWithValue("@per_nom", p_corres.Nom);
                command.Parameters.AddWithValue("@per_prenom", p_corres.Prenom);


                command.Parameters.AddWithValue("@profession", p_corres.Profession);
                command.Parameters.AddWithValue("@AutreProfession", p_corres.AutreProfession);
                command.Parameters.AddWithValue("@per_notes", p_corres.Notes);


                if (p_corres.TuToiement)
                    command.Parameters.AddWithValue("@tuvous", 0);
                else
                    command.Parameters.AddWithValue("@tuvous", 1);



                command.Parameters.AddWithValue("@pref_com", ((char)p_corres.PrefCom).ToString());

                command.Parameters.AddWithValue("@pers_titre", p_corres.Titre);
                command.Parameters.AddWithValue("@per_type", p_corres.Type);

                if (p_corres.GenreFeminin)
                    command.Parameters.AddWithValue("@PER_GENRE", "F");
                else
                    command.Parameters.AddWithValue("@PER_GENRE", "M");


                command.Parameters.AddWithValue("@PER_DATNAISS", p_corres.DateNaissance == null ? DBNull.Value : (object)p_corres.DateNaissance.Value);
                command.Parameters.AddWithValue("@PER_NUMSECU", p_corres.numSecu);


                command.Parameters.AddWithValue("@oi_login", p_corres.IOlogin);
                command.Parameters.AddWithValue("@oi_mdp", p_corres.password);
                command.Parameters.AddWithValue("@oi_profil", p_corres.Idprofile);
                command.Parameters.AddWithValue("@oi_autorisation", p_corres.publication);


                command.ExecuteNonQuery();

                transaction.Commit();
                return id;

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


        
        /*
        public static DataTable getCorrespondantsOf(basePatient patient)
        {

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {




                string selectQuery = "SELECT personne.ID_personne,";
                selectQuery += "PER_NOM,";
                selectQuery += "PER_PRENOM,";
                selectQuery += "PER_GENRE,";
                selectQuery += "PER_NOTES,";
                selectQuery += "PERS_TITRE,";
                selectQuery += "PER_SECU,";
                selectQuery += "PER_DATNAISS,";
                selectQuery += "TUVOUS,";
                selectQuery += "PREF_COM,";
                selectQuery += "lnk.RELATION, ";
                selectQuery += "lnk.TYPELIEN, ";
                selectQuery += "lnk.ID_PATIENT, ";
                selectQuery += "type_pers.NOM as PROFESSION, ";
                selectQuery += "type_pers.ID_TYPE as TYPE, ";
                selectQuery += "NOTE ";
                selectQuery += " FROM personne ";
                selectQuery += " INNER JOIN type_pers on type_pers.ID_TYPE=personne.PER_TYPE ";
                selectQuery += " LEFT JOIN base_histo_categorie on base_histo_categorie.ID_personne = personne.ID_personne and base_histo_categorie.DATE_FIN_CATEG is null and ID_CATEGORIE is null";

                selectQuery += " INNER JOIN lienpers lnk on lnk.ID_personne=personne.ID_personne ";

                selectQuery += " Where lnk.id_patient = @IdPatient";
                selectQuery += " order by PER_NOM,PER_PRENOM";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@IdPatient", patient.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
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
        */


        public static DataTable getCorrespondantsOf(int idpatient)
        {

            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {


                

                string selectQuery = "SELECT lnk.ID_personne,";
                selectQuery += "lnk.RELATION, ";
                selectQuery += "lnk.TYPELIEN, ";
                selectQuery += "lnk.ID_PATIENT ";
                selectQuery += " FROM lienpers lnk ";
                selectQuery += " Where lnk.id_patient = @IdPatient and lnk.ID_personne <>-1 and lnk.ID_personne is not null";


                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@IdPatient", idpatient);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);


                return ds.Tables[0];



            }
            catch (System.IndexOutOfRangeException e)
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
        public static DataTable getTypeCorrespondants()
        {

            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {



                string selectQuery = "SELECT NOM, ";
                selectQuery += " ID_TYPE as ID ";
                selectQuery += " FROM type_pers ";
                selectQuery += " Order by NOM";


                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);



                return ds.Tables[0];



            }
            catch (System.IndexOutOfRangeException e)
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




        public static bool CheckNomPrenom(string nom, string prenom)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = "select 1 from personne where upper(per_nom)=@nom and upper(per_prenom)=@prenom";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@nom", nom.ToUpper());
                command.Parameters.AddWithValue("@prenom", prenom.ToUpper());
                return command.ExecuteScalar() is DBNull;
                
                

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


        public static void InsertCorrespondant(Correspondant p_corres)
        {
            if (connection == null) getConnection();
            if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select MAX(ID_personne)+1 as NEWID from personne";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                object o = command.ExecuteScalar();
                if (o==DBNull.Value)
                    p_corres.Id = 1;
                else
                    p_corres.Id = Convert.ToInt32(o);

                selectQuery = "insert into  personne (";
                selectQuery += "id_personne,";
                selectQuery += "per_nom,";
                selectQuery += "per_prenom,";

                selectQuery += "profession,";
                selectQuery += "autreprofession,";
                selectQuery += "per_notes,";
                selectQuery += "tuvous,";
                selectQuery += "pref_com,";
                selectQuery += "pers_titre,";
                selectQuery += "per_type,";
                selectQuery += "per_secu,";
                selectQuery += "per_datnaiss,";

                selectQuery += "oi_login,";
                selectQuery += "oi_mdp,";
                selectQuery += "oi_profil,";
                selectQuery += "oi_autorisation,";
                
                
                selectQuery += "per_genre";
                selectQuery += ") values (";
                selectQuery += "@ID_personne,";
                selectQuery += "@PER_NOM,";
                selectQuery += "@PER_PRENOM,";
                selectQuery += "@PROFESSION,";
                selectQuery += "@AutreProfession,";
                selectQuery += "@PER_NOTES,";
                selectQuery += "@TUVOUS,";
                selectQuery += "@PREF_COM,";
                selectQuery += "@PERS_TITRE,";
                selectQuery += "@PER_TYPE,";
                selectQuery += "@PER_SECU,";
                selectQuery += "@PER_DATNAISS,";

                selectQuery += "@oi_login,";
                selectQuery += "@oi_mdp,";
                selectQuery += "@oi_profil,";
                selectQuery += "@oi_autorisation,";

                selectQuery += "@PER_GENRE)";


                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@ID_personne", p_corres.Id);
                command.Parameters.AddWithValue("@PER_NOM", p_corres.Nom);
                command.Parameters.AddWithValue("@PER_PRENOM", p_corres.Prenom);


                command.Parameters.AddWithValue("@PROFESSION", p_corres.Profession);
                command.Parameters.AddWithValue("@AutreProfession", p_corres.AutreProfession);
                command.Parameters.AddWithValue("@PER_NOTES", p_corres.Notes);
                if (p_corres.TuToiement)
                    command.Parameters.AddWithValue("@TUVOUS", 0);
                else
                    command.Parameters.AddWithValue("@TUVOUS", 1);


                command.Parameters.AddWithValue("@PERS_TITRE", p_corres.Titre);
                command.Parameters.AddWithValue("@PREF_COM", ((char)p_corres.PrefCom).ToString());
                command.Parameters.AddWithValue("@PER_TYPE", p_corres.Type);
                if (p_corres.GenreFeminin)
                    command.Parameters.AddWithValue("@PER_GENRE", "F");
                else
                    command.Parameters.AddWithValue("@PER_GENRE", "M");

                command.Parameters.AddWithValue("@PER_DATNAISS", p_corres.DateNaissance == null ? DBNull.Value : (object)p_corres.DateNaissance.Value);
                command.Parameters.AddWithValue("@PER_SECU", p_corres.numSecu);

                command.Parameters.AddWithValue("@oi_login", p_corres.IOlogin);
                command.Parameters.AddWithValue("@oi_mdp", p_corres.password);
                command.Parameters.AddWithValue("@oi_profil", p_corres.Idprofile);
                command.Parameters.AddWithValue("@oi_autorisation", p_corres.publication);


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



        public static void InsertUpdateLienCorrespondant(LienCorrespondant lcorres)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "update or insert into lienpers (id_personne, id_patient, typelien, relation)";
                selectQuery += "values (@id_personne, @id_patient, @typelien, @relation)";
                selectQuery += "matching (id_patient, relation)";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;




                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@id_personne", lcorres.correspondant.Id);
                command.Parameters.AddWithValue("@id_patient", lcorres.IdPatient);
                command.Parameters.AddWithValue("@typelien", lcorres.LienLibelle);
                command.Parameters.AddWithValue("@relation", lcorres.TypeDeLien);
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


        public static void UpdateLienCorrespondant(LienCorrespondant lcorres)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "update lienpers  set id_personne=@id_personne, typelien = @typelien)";
                selectQuery += " where id_patient=@id_patient and relation = @relation";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;




                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@id_personne", lcorres.correspondant.Id);
                command.Parameters.AddWithValue("@id_patient", lcorres.IdPatient);
                command.Parameters.AddWithValue("@typelien", lcorres.LienLibelle);
                command.Parameters.AddWithValue("@relation", lcorres.TypeDeLien);
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


        public static void InsertLienCorrespondant(LienCorrespondant corres)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "insert into lienpers (id_personne, id_patient, typelien, relation)";
                selectQuery += "values (@id_personne, @id_patient, @typelien, @relation)";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;




                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@id_personne", corres.IdCorrespondance);
                command.Parameters.AddWithValue("@id_patient", corres.IdPatient);
                command.Parameters.AddWithValue("@typelien", corres.LienLibelle);
                command.Parameters.AddWithValue("@relation", corres.TypeDeLien);
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
