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

        public static int GetEntiteJuridiqueOf(int idpat)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                //famille_Acte
                string selectQuery = "select utilisateur.id_entityjuridique";
                selectQuery += " from utilisateur";
                selectQuery += "  inner join  basediag_infocomplementaire on basediag_infocomplementaire.praticien_resp = utilisateur.id_personne";
                selectQuery += "  where basediag_infocomplementaire.idpatient = @idPat";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@idPat", idpat);


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


        public static DataTable getEntitesJuridique()
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                //famille_Acte
                string selectQuery = "select id, ";
                selectQuery += " nom, ";
                selectQuery += " adresse1, ";
                selectQuery += " adresse2, ";
                selectQuery += " codepostal, ";
                selectQuery += " ville,";

                selectQuery += " Telephone,";
                selectQuery += " Mail,";
                selectQuery += " SiteWeb,";
                selectQuery += " FormeSocial,";
                selectQuery += " DateCreation,";
                selectQuery += " NumSIRET,";
                selectQuery += " RCS,";
                selectQuery += " NumOrdre,";
                selectQuery += " OrdreDe,";
                selectQuery += " Gerant,";
                selectQuery += " Collaborateur";
                
                
                selectQuery += " from entite_juridique";

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



        public static DataTable NbPatientsParEntite()
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();  

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {



                    string selectQuery = @" select count(patient.id_personne) nbPatient,u.id_entityjuridique  
                                            from (
                                                          select personne.id_personne, per_nom, per_prenom
                                                            from personne
                                                            inner join patient on personne.id_personne=patient.id_personne
                                                            left outer join base_histo_status on base_histo_status.id_patient=patient.id_personne and base_histo_status.datefin is null left outer join statuts on statuts.id_statut=base_histo_status.id_status
                                                            where  patient.test_bp=1 and (statuts.FAMILLE_STATUT is null or statuts.FAMILLE_STATUT<>1)
                                                            order by per_nom, per_prenom
                                            ) patient
                                            left join basediag_infocomplementaire on basediag_infocomplementaire.idpatient = patient.id_personne 
                                            left join utilisateur u on u.id_personne =  basediag_infocomplementaire.praticien_resp
                                            group by u.id_entityjuridique";

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


        }

        public static List<int> GetEntitesJuridique(BanqueDeRemise bdr)
        {


            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = " select ID_ENTITY ";
                selectQuery += " from base_lnkentitybque";
                selectQuery += " where COD_BAN = @COD_BAN";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@COD_BAN", bdr.Code);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                List<int> lst = new List<int>();
                foreach (DataRow dr in dt.Rows)
                    lst.Add(Convert.ToInt32(dr["ID_ENTITY"]));


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

        public static void AddEntiteJuridique(EntiteJuridique en)
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
                string selectQuery = "select MAX(ID)+1 as NEWID from entite_juridique";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                try
                {
                    en.Id = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (System.Exception)
                {
                    en.Id = 1;
                }
                selectQuery = "insert into entite_juridique (";
                selectQuery += "ID, ";
                selectQuery += "NOM, ";
                selectQuery += "ADRESSE1,  ";
                selectQuery += "ADRESSE2, ";
                selectQuery += "CODEPOSTAL, ";
                selectQuery += "VILLE,";

                selectQuery += " Telephone,";
                selectQuery += " Mail,";
                selectQuery += " SiteWeb,";
                selectQuery += " FormeSocial,";
                selectQuery += " DateCreation,";
                selectQuery += " NumSIRET,";
                selectQuery += " RCS,";
                selectQuery += " NumOrdre,";
                selectQuery += " OrdreDe,";
                selectQuery += " Gerant,";
                selectQuery += " Collaborateur";


                selectQuery += ")";
                selectQuery += " values (@ID, @NOM, @ADRESSE1, @ADRESSE2, @CODEPOSTAL, @VILLE,";
                    selectQuery += " @Telephone,";
                selectQuery += " @Mail,";
                selectQuery += " @SiteWeb,";
                selectQuery += " @FormeSocial,";
                selectQuery += " @DateCreation,";
                selectQuery += " @NumSIRET,";
                selectQuery += " @RCS,";
                selectQuery += " @NumOrdre,";
                selectQuery += " @OrdreDe,";
                selectQuery += " @Gerant,";
                selectQuery += " @Collaborateur)";
                
                
                
                command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@ID", en.Id);
                command.Parameters.AddWithValue("@NOM", en.Nom);
                command.Parameters.AddWithValue("@ADRESSE1", en.Adresse1);
                command.Parameters.AddWithValue("@ADRESSE2", en.Adresse2);
                command.Parameters.AddWithValue("@CODEPOSTAL", en.CodePostal);
                command.Parameters.AddWithValue("@VILLE", en.Ville);

                command.Parameters.AddWithValue("@Telephone", en.Telephone);
                command.Parameters.AddWithValue("@Mail", en.Mail);
                command.Parameters.AddWithValue("@SiteWeb", en.SiteWeb);
                command.Parameters.AddWithValue("@FormeSocial", en.FormeSocial);
                command.Parameters.AddWithValue("@DateCreation", en.DateCreation);
                command.Parameters.AddWithValue("@NumSIRET", en.NumSIRET);
                command.Parameters.AddWithValue("@RCS", en.RCS);
                command.Parameters.AddWithValue("@NumOrdre", en.NumOrdre);
                command.Parameters.AddWithValue("@OrdreDe", en.OrdreDe);
                command.Parameters.AddWithValue("@Gerant", en.Gerant);
                command.Parameters.AddWithValue("@Collaborateur", en.Collaborateur);

              


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

        public static void UpdateEntiteJuridique(EntiteJuridique en)
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
                string selectQuery = "update entite_juridique";
                selectQuery += " set NOM = @NOM,";
                selectQuery += "    ADRESSE1 = @ADRESSE1,";
                selectQuery += "    ADRESSE2 = @ADRESSE2,";
                selectQuery += "    CODEPOSTAL = @CODEPOSTAL,";
                selectQuery += "    VILLE = @VILLE,";

                selectQuery += " Telephone=@Telephone ,";
                selectQuery += " Mail=@Mail ,";
                selectQuery += " SiteWeb=@SiteWeb ,";
                selectQuery += " FormeSocial=@FormeSocial ,";
                selectQuery += " DateCreation=@DateCreation ,";
                selectQuery += " NumSIRET=@NumSIRET ,";
                selectQuery += " RCS=@RCS ,";
                selectQuery += " NumOrdre=@NumOrdre ,";
                selectQuery += " OrdreDe=@OrdreDe ,";
                selectQuery += " Gerant=@Gerant ,";
                selectQuery += " Collaborateur=@Collaborateur";


                selectQuery += " where (ID = @ID)";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@ID", en.Id);
                command.Parameters.AddWithValue("@NOM", en.Nom);
                command.Parameters.AddWithValue("@ADRESSE1", en.Adresse1);
                command.Parameters.AddWithValue("@ADRESSE2", en.Adresse2);
                command.Parameters.AddWithValue("@CODEPOSTAL", en.CodePostal);
                command.Parameters.AddWithValue("@VILLE", en.Ville);

                command.Parameters.AddWithValue("@Telephone", en.Telephone);
                command.Parameters.AddWithValue("@Mail", en.Mail);
                command.Parameters.AddWithValue("@SiteWeb", en.SiteWeb);
                command.Parameters.AddWithValue("@FormeSocial", en.FormeSocial);
                command.Parameters.AddWithValue("@DateCreation", en.DateCreation);
                command.Parameters.AddWithValue("@NumSIRET", en.NumSIRET);
                command.Parameters.AddWithValue("@RCS", en.RCS);
                command.Parameters.AddWithValue("@NumOrdre", en.NumOrdre);
                command.Parameters.AddWithValue("@OrdreDe", en.OrdreDe);
                command.Parameters.AddWithValue("@Gerant", en.Gerant);
                command.Parameters.AddWithValue("@Collaborateur", en.Collaborateur);


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
