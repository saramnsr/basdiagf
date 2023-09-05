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


        public static void insertLog(string requete, string user, string patient,string ids)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                int id = 0;
                string selectQuery = "select MAX(ID)+1 as NEWID from base_log_echeance";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                object obj = command.ExecuteScalar();

                if (obj == DBNull.Value)
                    id = 1;
                else
                    id = Convert.ToInt32(obj);


                selectQuery = "INSERT INTO base_log_echeance(id,requete,user_,patient,date_,ids) values (@id,@requete,@user,@patient,@date,@ids)";


                command.Parameters.Clear();
                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@ids", ids);
                command.Parameters.AddWithValue("@patient", patient);
                command.Parameters.AddWithValue("@requete", requete);
                command.Parameters.AddWithValue("@user", user);
                command.Parameters.AddWithValue("@date", DateTime.Now);

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
        public static DataRow getEcheances(int id)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = @"select base_echeance.ID,         base_echeance.ID_TRAITEMENT,base_traitement.ID_COMM,         base_echeance.MONTANT,         base_echeance.DTEECHEANCE,         base_echeance.ID_FACTURATION,         base_echeance.LIBELLE,         base_echeance.ID_PATIENT,         base_echeance.ID_ENCAISSEMENT,        base_echeance.ID_MUTUELLE,        base_echeance.TYPEPAYEUR,        base_echeance.TYPEPAYEUR,        base_echeance.PARPRELEVEMENT,         base_echeance.ReleveDeCompte,         base_echeance.Relance,         base_echeance.PreContentieux,         base_echeance.Majoration,         base_echeance.Contentieux,         base_echeance.PARVIREMENT, base_echeance.TYPEACTE  from base_echeance LEFT JOIN base_traitement on base_traitement.ID = base_echeance.ID_TRAITEMENT  LEFT JOIN base_comm on base_traitement.ID_COMM = base_comm.ID    where base_echeance.id=@id  ";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);


                    command.Parameters.AddWithValue("@id",id);
        

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                    return dt.Rows[0];
                else return null;

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
        public static double getTotalPerte(int id)
        {
            double total=0;
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
           // MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = @"select coalesce(SUM(base_echeance.MONTANT),0) as total from base_echeance where base_echeance.ID_PATIENT=@idPatient and base_echeance.TYPEPAYEUR = 5  ";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);


                command.Parameters.AddWithValue("@idPatient", id);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
               // transaction.Commit();

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                    total = Convert.ToDouble(dt.Rows[0]["total"]);
               return total;

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
        public static DataTable GetEcheanceAOrdonnerParUnTiers(object payeur)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
         //   MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = @"select distinct MUTUELLE.MUTUELLE_NOM,
                                        caisse.caisse_nom,
                                        BASE_FEUILLE_DE_SOIN.TYPE_ENVOIS,
                                        base_echeance.ID,
                                        base_echeance.ID_TRAITEMENT, 
                                        base_echeance.MONTANT, 
                                        base_echeance.DTEECHEANCE, 
                                        base_echeance.LIBELLE, 
                                        base_echeance.ID_PATIENT, 
                                        base_echeance.ID_ENCAISSEMENT,
                                        base_echeance.ID_MUTUELLE,
                                        base_echeance.PARPRELEVEMENT ,
                                        base_echeance.PARVIREMENT, 
                                        base_echeance.TYPEPAYEUR,
                                        base_echeance.relevedecompte,
                                        base_echeance.relance,
                                        base_echeance.precontentieux,
                                        base_echeance.majoration,
                                        base_echeance.contentieux,base_echeance.TYPEACTE,
                                        COD_BAN,
                                        ID_ENTITY,
                                        BAS_LNK_PAIEMENT_CTRL.ID_CONTROL,
                                        trim(per_nom)||' '||trim(per_prenom) as NomPatient
                                 from base_echeance
                                 inner join base_traitement on base_traitement.ID = base_echeance.ID_TRAITEMENT
                                 left outer join BASE_FEUILLE_DE_SOIN on BASE_FEUILLE_DE_SOIN.ID = base_traitement.ID_FS

                                 inner join personne on personne.id_personne = base_echeance.ID_PATIENT
                                 inner join basediag_infocomplementaire on basediag_infocomplementaire.idpatient=personne.id_personne
                                 inner join utilisateur on utilisateur.id_personne = basediag_infocomplementaire.praticien_resp
                                 inner join base_lnkentitybque on base_lnkentitybque.id_entity= utilisateur.id_entityjuridique

                                 left join BAS_LNK_PAIEMENT_CTRL on BAS_LNK_PAIEMENT_CTRL.id_echeance=base_echeance.id

                                 left join lienpers lcaisse on lcaisse.id_patient = base_echeance.id_patient and lcaisse.relation='Ca'
                                 left join caisse on caisse.id_caisse = lcaisse.id_personne

                                 left join lienpers lmut on lmut.id_patient = base_echeance.id_patient and lmut.relation='Mu'
                                 left join mutuelle on mutuelle.id_mutuelle = lmut.id_personne";

                if (payeur is Caisse)
                    selectQuery += " where (caisse.id_caisse = @idpayeur) and (base_echeance.typepayeur=2)";
                else
                    if (payeur is Mutuelle)
                        selectQuery += " where (mutuelle.id_mutuelle = @idpayeur) and (base_echeance.typepayeur=1)";
                    else
                        selectQuery += " where (caisse.id_caisse is not null or mutuelle.id_mutuelle is not null) and (base_echeance.typepayeur=1 or base_echeance.typepayeur=2)";

                selectQuery += @"
                                 
                                 and (base_echeance.id_encaissement is null or base_echeance.id_encaissement=-1)
                                 and base_echeance.MONTANT>0
                                 order by per_nom,per_prenom";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);


                if (payeur is Caisse)
                    command.Parameters.AddWithValue("@idpayeur", ((Caisse)payeur).Id);
                else
                    if (payeur is Mutuelle)
                        command.Parameters.AddWithValue("@idpayeur", ((Mutuelle)payeur).Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
             //   transaction.Commit();

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
        public static void AffectEncaissementToEcheance(Echeance echeancePG)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "update base_echeance set ID_ENCAISSEMENT = @ID_ENCAISSEMENT ";
                selectQuery += " where ID=@ID";



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;




                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@ID", echeancePG.Id);
                command.Parameters.AddWithValue("@ID_ENCAISSEMENT", echeancePG.encaissement == null ? DBNull.Value : (object)echeancePG.encaissement.Id);
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

        

        public static DataTable GetEcheanceAOrdonnerParUnTiers()
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = @"select MUTUELLE.MUTUELLE_NOM,
                                        caisse.caisse_nom,
                                        base_echeance.ID,
                                        base_echeance.ID_TRAITEMENT, base_traitement.ID_COMM,
                                        base_echeance.MONTANT, 
                                        base_echeance.DTEECHEANCE, 
                                        base_echeance.LIBELLE, 
                                        base_echeance.ID_PATIENT, 
                                        base_echeance.ID_ENCAISSEMENT,
                                        base_echeance.ID_MUTUELLE,
                                        base_echeance.PARPRELEVEMENT ,
                                        base_echeance.PARVIREMENT, 

                                        base_echeance.ReleveDeCompte, 
                                        base_echeance.Relance, 
                                        base_echeance.PreContentieux, 
                                        base_echeance.Majoration, 
                                        base_echeance.Contentieux, 

                                        base_echeance.TYPEPAYEUR,base_echeance.TYPEACTE
                                 from base_echeance
                                LEFT JOIN base_traitement on base_traitement.ID = base_echeance.ID_TRAITEMENT
                                 left join BAS_LNK_PAIEMENT_CTRL on BAS_LNK_PAIEMENT_CTRL.id_echeance=base_echeance.id

                                 left join lienpers lcaisse on lcaisse.id_patient = base_echeance.id_patient and lcaisse.relation='Ca'
                                 left join caisse on caisse.id_caisse = lcaisse.id_personne

                                 left join lienpers lmut on lmut.id_patient = base_echeance.id_patient and lmut.relation='Mu'
                                 left join mutuelle on mutuelle.id_mutuelle = lmut.id_personne

                                 where (caisse.id_caisse is not null or mutuelle.id_mutuelle is not null) and (base_echeance.typepayeur=1 or base_echeance.typepayeur=2)
                                 and (base_echeance.id_encaissement is null or base_echeance.id_encaissement=-1)
                                 and base_echeance.MONTANT>0 and BAS_LNK_PAIEMENT_CTRL.id_echeance is null
                                 order by DTEECHEANCE";

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

        public static DataTable GetEcheanceARelancerParUnTiers()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
         //   MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select base_echeance.ID, ";
                selectQuery += "        base_echeance.ID_TRAITEMENT,base_traitement.ID_COMM, ";
                selectQuery += "        base_echeance.MONTANT, ";
                selectQuery += "        base_echeance.DTEECHEANCE, ";
                selectQuery += "        base_echeance.LIBELLE, ";
                selectQuery += "        base_echeance.ID_PATIENT, ";
                selectQuery += "        base_echeance.ID_ENCAISSEMENT,";
                selectQuery += "        base_echeance.ID_MUTUELLE,";
                selectQuery += "        base_echeance.PARPRELEVEMENT ,";
                selectQuery += "        base_echeance.PARVIREMENT, ";
                
                selectQuery += "        base_echeance.ReleveDeCompte, ";
                selectQuery += "        base_echeance.Relance, ";
                selectQuery += "        base_echeance.PreContentieux, ";
                selectQuery += "        base_echeance.Majoration, ";
                selectQuery += "        base_echeance.Contentieux,base_echeance.TYPEACTE, ";

                selectQuery += "        base_echeance.TYPEPAYEUR";
                selectQuery += " from base_echeance LEFT JOIN base_traitement on base_traitement.ID = base_echeance.ID_TRAITEMENT ";
                selectQuery += " where base_echeance.TYPEPAYEUR >0 and ";
                selectQuery += " (base_echeance.id_encaissement is null or base_echeance.id_encaissement=-1)";
                selectQuery += " and base_echeance.DTEECHEANCE<current_date and base_echeance.MONTANT>0";
                selectQuery += " order by DTEECHEANCE";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);

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


        public static DataTable GetEcheanceAEncaisserParUnTiers()
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
           // MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select base_echeance.ID, ";
                selectQuery += "        base_echeance.ID_TRAITEMENT,base_traitement.ID_COMM, ";
                selectQuery += "        base_echeance.MONTANT, ";
                selectQuery += "        base_echeance.DTEECHEANCE, ";
                selectQuery += "        base_echeance.LIBELLE, ";
                selectQuery += "        base_echeance.ID_PATIENT, ";
                selectQuery += "        base_echeance.ID_ENCAISSEMENT,";
                selectQuery += "        base_echeance.ID_MUTUELLE,";
                selectQuery += "        base_echeance.PARPRELEVEMENT ,";
                selectQuery += "        base_echeance.PARVIREMENT, ";
                selectQuery += "        base_echeance.TYPEPAYEUR,";

                selectQuery += "        base_echeance.ReleveDeCompte, ";
                selectQuery += "        base_echeance.Relance, ";
                selectQuery += "        base_echeance.PreContentieux, ";
                selectQuery += "        base_echeance.Majoration, ";
                selectQuery += "        base_echeance.Contentieux,base_echeance.TYPEACTE, ";

                selectQuery += "        TRIM(PERSONNE.PER_NOM)||' '||TRIM(PERSONNE.PER_PRENOM) NOMPATIENT";
                selectQuery += " from base_echeance LEFT JOIN base_traitement on base_traitement.ID = base_echeance.ID_TRAITEMENT ";
                selectQuery += " INNER JOIN PERSONNE ON PERSONNE.ID_PERSONNE = base_echeance.ID_PATIENT";
                selectQuery += " where base_echeance.TYPEPAYEUR >0 and  base_echeance.DTEECHEANCE<current_timestamp and";
                selectQuery += " (base_echeance.id_encaissement is null or base_echeance.id_encaissement=-1)";
                selectQuery += " and base_echeance.MONTANT>0";
                selectQuery += " order by DTEECHEANCE";

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



        public static DataTable GetEcheanceEnVirementAPointer(DateTime dte1, DateTime dte2)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
         //   MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select base_echeance.ID, ";
                selectQuery += "        base_echeance.ID_TRAITEMENT, base_traitement.ID_COMM, ";
                selectQuery += "        base_echeance.MONTANT, ";
                selectQuery += "        base_echeance.DTEECHEANCE, ";
                selectQuery += "        base_echeance.LIBELLE, ";
                selectQuery += "        base_echeance.ID_PATIENT, ";
                selectQuery += "        base_echeance.ID_ENCAISSEMENT,";
                selectQuery += "        base_echeance.ID_MUTUELLE,";
                selectQuery += "        base_echeance.PARPRELEVEMENT ,";
                selectQuery += "        base_echeance.PARVIREMENT, ";

                selectQuery += "        base_echeance.ReleveDeCompte, ";
                selectQuery += "        base_echeance.Relance, ";
                selectQuery += "        base_echeance.PreContentieux, ";
                selectQuery += "        base_echeance.Majoration, ";
                selectQuery += "        base_echeance.Contentieux,base_echeance.TYPEACTE, ";

                selectQuery += "        base_echeance.TYPEPAYEUR";
                selectQuery += " from base_echeance LEFT JOIN base_traitement on base_traitement.ID = base_echeance.ID_TRAITEMENT ";
                selectQuery += " where base_echeance.parVirement='True' and";
                selectQuery += " (base_echeance.id_encaissement is null or base_echeance.id_encaissement=-1)  and";
                selectQuery += " base_echeance.dteecheance between @dte1 and @dte2  ";
                selectQuery += " order by DTEECHEANCE";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@dte1", dte1.Date);
                command.Parameters.AddWithValue("@dte2", dte2.Date.AddHours(23));

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




        public static DataTable GetEcheanceAPrelever(DateTime dte, bool uniquementALaDate,BanqueDeRemise bqe)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
          //  MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select base_echeance.ID, ";
                selectQuery += "        base_echeance.ID_TRAITEMENT,base_traitement.ID_COMM, ";
                selectQuery += "        base_echeance.MONTANT, ";
                selectQuery += "        base_echeance.DTEECHEANCE, ";
                selectQuery += "        base_echeance.LIBELLE, ";
                selectQuery += "        base_echeance.ID_PATIENT, ";
                selectQuery += "        base_echeance.ID_ENCAISSEMENT,";
                selectQuery += "        base_echeance.ID_MUTUELLE,";
                selectQuery += "        base_echeance.PARPRELEVEMENT ,";
                selectQuery += "        base_echeance.PARVIREMENT, ";

                selectQuery += "        base_echeance.ReleveDeCompte, ";
                selectQuery += "        base_echeance.Relance, ";
                selectQuery += "        base_echeance.PreContentieux, ";
                selectQuery += "        base_echeance.Majoration, ";
                selectQuery += "        base_echeance.Contentieux,base_echeance.TYPEACTE, ";

                selectQuery += "        TRIM(per_nom)||' '||TRIM(per_prenom) NomPatient, ";
                selectQuery += "        base_echeance.TYPEPAYEUR, base_echeance.ID_FACTURATION";
                selectQuery += " from base_echeance LEFT JOIN base_traitement on base_traitement.ID = base_echeance.ID_TRAITEMENT ";
                selectQuery += @" inner join personne on base_echeance.id_patient = personne.id_personne
                                    inner join basediag_infocomplementaire on basediag_infocomplementaire.idpatient=personne.id_personne
                                    inner join utilisateur on utilisateur.id_personne = basediag_infocomplementaire.praticien_resp
                                    inner join base_lnkentitybque on base_lnkentitybque.id_entity= utilisateur.id_entityjuridique
                                    where base_echeance.parprelevement = 'True' and (base_echeance.id_encaissement is null or base_echeance.id_encaissement=-1)  ";
               if (uniquementALaDate)
                    selectQuery += " and cast( base_echeance.dteecheance as date) = @dte2 and base_lnkentitybque.cod_ban = @bqe";
                else
                   selectQuery += " and base_echeance.dteecheance < @dte2 and base_lnkentitybque.cod_ban = @bqe";
                selectQuery += " order by DTEECHEANCE";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                if (uniquementALaDate)
                    command.Parameters.AddWithValue("@dte2", dte.Date);
                else
                    command.Parameters.AddWithValue("@dte2", dte);

                command.Parameters.AddWithValue("@bqe", bqe.Code);

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

        public static void UpdateEcheanceMontant(int IdecheancePG, double MontantEcheance, double AncienMontant, string LibelleEcheance)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "update base_echeance set ";
                selectQuery += " MONTANT = (MONTANT-@ANCIENMONTANT)+@MONTANTECHEANCE ";
                if (LibelleEcheance != "")
             
                {
                    selectQuery += ",";
            
                selectQuery += " LIBELLE = @LIBELLE ";
                }
                selectQuery += " where ID=@ID";



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;




                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@ID", IdecheancePG);
                if (LibelleEcheance != "")
                command.Parameters.AddWithValue("@LIBELLE", LibelleEcheance);
                //command.Parameters.AddWithValue("@MONTANT", Math.Round(MontantEcheance, 2));
                command.Parameters.AddWithValue("@ANCIENMONTANT", Math.Round(AncienMontant, 2));
                command.Parameters.AddWithValue("@MONTANTECHEANCE", Math.Round(MontantEcheance, 2));

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

        public static void UpdateEcheanceLibelle(int idTraitement,string Libele)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "update base_echeance set";;
                selectQuery += " LIBELLE = @LIBELLE";
                selectQuery += " where  ID_TRAITEMENT = @ID_TRAITEMENT ";



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;




                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@ID_TRAITEMENT", idTraitement);
                command.Parameters.AddWithValue("@LIBELLE", Libele);

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
        public static void UpdateEcheance(Echeance echeancePG)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "update base_echeance set ID_TRAITEMENT = @ID_TRAITEMENT ,";
                selectQuery += " MONTANT = @MONTANT ,";
                selectQuery += " PARPRELEVEMENT = @PARPRELEVEMENT ,";
                selectQuery += " ParVirement = @ParVirement ,";
                selectQuery += " ID_PATIENT = @ID_PATIENT ,";
                selectQuery += " DTEECHEANCE = @DTEECHEANCE ,";
                selectQuery += " ID_ENCAISSEMENT = @ID_ENCAISSEMENT ,";
                selectQuery += " ID_MUTUELLE = @ID_MUTUELLE ,";
                selectQuery += " LIBELLE = @LIBELLE,";
                selectQuery += "        ReleveDeCompte=@ReleveDeCompte, ";
                selectQuery += "        Relance=@Relance, ";
                selectQuery += "        PreContentieux=@PreContentieux, ";
                selectQuery += "        Majoration=@Majoration, ";
                selectQuery += "        Contentieux=@Contentieux, ";

                selectQuery += " TYPEPAYEUR = @TYPEPAYEUR";
                selectQuery += " where ID=@ID";



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;




                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@ID", echeancePG.Id);
                command.Parameters.AddWithValue("@ID_TRAITEMENT", echeancePG.IdActe);
                command.Parameters.AddWithValue("@ID_ENCAISSEMENT", echeancePG.ID_Encaissement);
                command.Parameters.AddWithValue("@MONTANT", Math.Round(echeancePG.Montant,2));
                command.Parameters.AddWithValue("@PARPRELEVEMENT", echeancePG.ParPrelevement);
                command.Parameters.AddWithValue("@ParVirement", echeancePG.ParVirement);
                command.Parameters.AddWithValue("@DTEECHEANCE", echeancePG.DateEcheance);
                command.Parameters.AddWithValue("@LIBELLE", echeancePG.Libelle);
                command.Parameters.AddWithValue("@ID_PATIENT", echeancePG.IdPatient);
                command.Parameters.AddWithValue("@ID_MUTUELLE", echeancePG.mutuelle == null ? DBNull.Value : (object)echeancePG.mutuelle.Id);
                command.Parameters.AddWithValue("@TYPEPAYEUR", echeancePG.payeur);

                command.Parameters.AddWithValue("@ReleveDeCompte", echeancePG.Relances.ReleveDeCompte == null ? DBNull.Value : (object)echeancePG.Relances.ReleveDeCompte);
                command.Parameters.AddWithValue("@Relance", echeancePG.Relances.Relance == null ? DBNull.Value : (object)echeancePG.Relances.Relance);
                command.Parameters.AddWithValue("@PreContentieux", echeancePG.Relances.PreContentieux == null ? DBNull.Value : (object)echeancePG.Relances.PreContentieux);
                command.Parameters.AddWithValue("@Majoration", echeancePG.Relances.Majoration == null ? DBNull.Value : (object)echeancePG.Relances.Majoration);
                command.Parameters.AddWithValue("@Contentieux", echeancePG.Relances.Contentieux == null ? DBNull.Value : (object)echeancePG.Relances.Contentieux);
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





        public static DataTable GetVirementPrevus(BanqueDeRemise bqe, DateTime dt1,DateTime dt2)
        {
            if (connection == null) getConnection(); 

            if (connection.State == ConnectionState.Closed) connection.Open();
          //  MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select base_echeance.ID, ";
                selectQuery += "        base_echeance.ID_TRAITEMENT, ";
                selectQuery += "        base_echeance.MONTANT, ";
                selectQuery += "        base_echeance.DTEECHEANCE, ";
                selectQuery += "        base_echeance.LIBELLE, ";
                selectQuery += "        base_echeance.ID_PATIENT, ";
                selectQuery += "        base_echeance.ID_ENCAISSEMENT,";
                selectQuery += "        base_echeance.ID_MUTUELLE,";
                selectQuery += "        base_echeance.TYPEPAYEUR,";
                selectQuery += "        base_echeance.PARPRELEVEMENT, ";
                selectQuery += "        base_echeance.ReleveDeCompte, ";
                selectQuery += "        base_echeance.Relance, ";
                selectQuery += "        base_echeance.PreContentieux, ";
                selectQuery += "        base_echeance.Majoration, ";
                selectQuery += "        base_echeance.Contentieux, ";
                selectQuery += "        TRIM(personne.per_nom)||' '||TRIM(personne.per_prenom) as NOMPATIENT, ";
                selectQuery += "        base_echeance.PARVIREMENT,base_echeance.TYPEACTE, ";
                selectQuery += "        utilisateur.id_entityjuridique ";

                selectQuery += " from base_echeance";
                selectQuery += " left outer join basediag_infocomplementaire on basediag_infocomplementaire.IDPATIENT = base_echeance.ID_PATIENT";
                selectQuery += " inner join utilisateur on basediag_infocomplementaire.praticien_resp = utilisateur.id_personne";
                selectQuery += " inner join personne on personne.ID_PERSONNE = base_echeance.ID_PATIENT";

                if (bqe != null)
                    selectQuery += @" inner join
                                    (
                                        select min(cod_ban) cod_ban,id_entity FROM base_lnkentitybque
                                        where cod_ban = @bqe
                                         group by id_entity
                                    ) bqe on bqe.id_entity =  utilisateur.id_entityjuridique";
                selectQuery += " where base_echeance.PARVIREMENT ='True' and (base_echeance.TYPEPAYEUR=0 or base_echeance.typepayeur=3) and  (base_echeance.id_encaissement is null or base_echeance.ID_ENCAISSEMENT<0) AND base_echeance.DTEECHEANCE between @dt1 and @dt2 ";
                selectQuery += " order by dteecheance desc";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@dt1", dt1);
                command.Parameters.AddWithValue("@dt2", dt2);
                if (bqe != null) command.Parameters.AddWithValue("@bqe", bqe.Code);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
            //    transaction.Commit();

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


        public static DataTable getEcheances(basePatient pat, bool IncludeCN)
        {
            if (connection == null) getConnection(); 

            if (connection.State == ConnectionState.Closed) connection.Open();
          //  MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select base_echeance.ID, ";
                selectQuery += "        base_echeance.ID_TRAITEMENT,base_traitement.ID_COMM, ";
                selectQuery += "        base_echeance.MONTANT, ";
                selectQuery += "        base_echeance.DTEECHEANCE, ";
                selectQuery += "        base_echeance.ID_FACTURATION, ";
                selectQuery += "        base_echeance.LIBELLE, ";
                selectQuery += "        base_echeance.ID_PATIENT, ";

                if (!IncludeCN)
                {
                    selectQuery += @"        case (
                                            select INCN from base_paiementreel
                                            inner join base_encaissement on base_encaissement.id_paiement_reel=base_paiementreel.id  and base_encaissement.id = base_echeance.id_encaissement
                                            )
                                                     when null then -1
                                                     when 'True' then -1
                                                     else base_echeance.ID_ENCAISSEMENT
                                                   end as ID_ENCAISSEMENT,";
                }
                else
                {

                    selectQuery += "        base_echeance.ID_ENCAISSEMENT,";
                }
                selectQuery += "        base_echeance.ID_MUTUELLE,";
                selectQuery += "        base_echeance.TYPEPAYEUR,";
                selectQuery += "        base_echeance.TYPEPAYEUR,";
                selectQuery += "        base_echeance.PARPRELEVEMENT, ";
                selectQuery += "        base_echeance.ReleveDeCompte, ";
                selectQuery += "        base_echeance.Relance, ";
                selectQuery += "        base_echeance.PreContentieux, ";
                selectQuery += "        base_echeance.Majoration, ";
                selectQuery += "        base_echeance.Contentieux, ";
                selectQuery += "        base_echeance.PARVIREMENT, base_echeance.TYPEACTE ";

                selectQuery += " from base_echeance LEFT JOIN base_traitement on base_traitement.ID = base_echeance.ID_TRAITEMENT ";
                selectQuery += " LEFT JOIN base_comm on base_traitement.ID_COMM = base_comm.ID and (base_comm.desactive='False' or base_comm.desactive is null) ";
                selectQuery += " where base_echeance.ID_PATIENT = @id_patient   ";
                selectQuery += " order by DTEECHEANCE";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@id_patient", pat.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
             //   transaction.Commit();

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
        

        public static DataTable getEcheances(PaiementReel pr)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
        //    MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select base_echeance.ID, ";
                selectQuery += "        base_echeance.ID_TRAITEMENT, base_traitement.ID_COMM, ";
                selectQuery += "        base_echeance.MONTANT, ";
                selectQuery += "        base_echeance.DTEECHEANCE, ";
                selectQuery += "        base_echeance.LIBELLE, ";
                selectQuery += "        base_echeance.ID_PATIENT, ";
                selectQuery += "        base_echeance.ID_ENCAISSEMENT,";
                selectQuery += "        base_echeance.ID_MUTUELLE,";
                selectQuery += "        base_echeance.TYPEPAYEUR,";
                selectQuery += "        base_echeance.TYPEPAYEUR,";
                selectQuery += "        base_echeance.PARPRELEVEMENT, ";
                selectQuery += "        base_echeance.ReleveDeCompte, ";
                selectQuery += "        base_echeance.Relance, ";
                selectQuery += "        base_echeance.PreContentieux, ";
                selectQuery += "        base_echeance.Majoration, ";
                selectQuery += "        base_echeance.Contentieux, ";
                selectQuery += "        base_echeance.PARVIREMENT,base_echeance.TYPEACTE, base_echeance.ID_FACTURATION ";

                selectQuery += " from base_echeance LEFT JOIN base_traitement on base_traitement.ID = base_echeance.ID_TRAITEMENT ";

                selectQuery += " inner join base_encaissement on base_encaissement.id = base_echeance.id_encaissement";
                selectQuery += " inner join base_paiementreel on base_paiementreel.id = base_encaissement.id_paiement_reel";
                selectQuery += " where base_paiementreel.Id = @id ";
                selectQuery += " order by DTEECHEANCE desc";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@id", pr.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
              //  transaction.Commit();

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
        public static DataTable get_Echeances(CommClinique cc, string Type_Com)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            //MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = @"select base_echeance.ID,
base_echeance.ID_TRAITEMENT, 
base_echeance.MONTANT, 
base_echeance.DTEECHEANCE, 
base_echeance.LIBELLE, 
base_echeance.ID_PATIENT, 
base_echeance.ID_ENCAISSEMENT,
base_echeance.ID_MUTUELLE,
base_echeance.PARPRELEVEMENT ,
base_echeance.PARVIREMENT, 
base_echeance.TYPEPAYEUR,
base_echeance.relevedecompte,
base_echeance.relance,
base_echeance.precontentieux,
base_echeance.majoration,
base_echeance.contentieux
from base_echeance
inner join base_traitement on base_traitement.ID = base_echeance.ID_TRAITEMENT
where base_traitement.ID_COMM=@Id AND  base_echeance.ID_ENCAISSEMENT=-1 AND base_traitement.TYPE_COMMENT =@TYPE_COMM ";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@Id", cc.Id);
                command.Parameters.AddWithValue("@TYPE_COMM", Type_Com);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
             //   transaction.Commit();

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

        public static DataTable getEcheances(Encaissement enc)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
         //   MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select base_echeance.ID, ";
                selectQuery += "        base_echeance.ID_TRAITEMENT, base_traitement.ID_COMM, ";
                selectQuery += "        base_echeance.MONTANT, ";
                selectQuery += "        base_echeance.DTEECHEANCE, ";
                selectQuery += "        base_echeance.LIBELLE, ";
                selectQuery += "        base_echeance.ID_PATIENT, ";
                selectQuery += "        base_echeance.ID_ENCAISSEMENT,";
                selectQuery += "        base_echeance.ID_MUTUELLE,";
                selectQuery += "        base_echeance.TYPEPAYEUR,";
                selectQuery += "        base_echeance.TYPEPAYEUR,";
                selectQuery += "        base_echeance.PARPRELEVEMENT, ";
                selectQuery += "        base_echeance.ReleveDeCompte, ";
                selectQuery += "        base_echeance.Relance, ";
                selectQuery += "        base_echeance.PreContentieux, ";
                selectQuery += "        base_echeance.Majoration, ";
                selectQuery += "        base_echeance.Contentieux,base_echeance.TYPEACTE, ";

                selectQuery += "        base_echeance.PARVIREMENT ";

                selectQuery += " from base_echeance LEFT JOIN base_traitement on base_traitement.ID = base_echeance.ID_TRAITEMENT ";
                selectQuery += " where base_echeance.ID_encaissement = @id ";
                selectQuery += " order by DTEECHEANCE desc";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@id", enc.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
             //   transaction.Commit();

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


        public static DataTable getEcheances(Correspondant ResponsableFi,bool IncludeCN)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            //MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select base_echeance.ID, ";
                selectQuery += "        base_echeance.ID_TRAITEMENT, base_traitement.ID_COMM,  ";
                selectQuery += "        base_echeance.MONTANT, ";
                selectQuery += "        base_echeance.DTEECHEANCE, ";
                selectQuery += "        base_echeance.ID_FACTURATION,";
                selectQuery += "        base_echeance.LIBELLE, ";
                selectQuery += "        base_echeance.ID_PATIENT, ";

                if (!IncludeCN)
                {
                    selectQuery += @"        case (
                                            select INCN from base_paiementreel
                                            inner join base_encaissement on base_encaissement.id_paiement_reel=base_paiementreel.id  and base_encaissement.id = base_echeance.id_encaissement
                                            )
                                                     when null then -1
                                                     when 'True' then -1
                                                     else base_echeance.ID_ENCAISSEMENT
                                                   end as ID_ENCAISSEMENT,";
                }
                else
                {

                    selectQuery += "        base_echeance.ID_ENCAISSEMENT,";
                }

                selectQuery += "        base_echeance.ID_MUTUELLE,";
                selectQuery += "        base_echeance.TYPEPAYEUR,";
                selectQuery += "        base_echeance.TYPEPAYEUR,";
                selectQuery += "        base_echeance.PARPRELEVEMENT, ";
                selectQuery += "        base_echeance.ReleveDeCompte, ";
                selectQuery += "        base_echeance.Relance, ";
                selectQuery += "        base_echeance.PreContentieux, ";
                selectQuery += "        base_echeance.Majoration, ";
                selectQuery += "        base_echeance.Contentieux, ";
                selectQuery += "        base_echeance.PARVIREMENT,base_echeance.TYPEACTE ";

                selectQuery += " from base_echeance LEFT JOIN base_traitement on base_traitement.ID = base_echeance.ID_TRAITEMENT ";
                selectQuery += " inner join lienpers on lienpers.ID_PATIENT=base_echeance.ID_PATIENT";
                selectQuery += " where lienpers.ID_PERSONNE = @idpersonne and lienpers.relation='Pa'  ";
                selectQuery += " order by DTEECHEANCE";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@idpersonne", ResponsableFi.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
             //   transaction.Commit();

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


        public static DataTable getEcheances(baseSmallPersonne pat,bool IncludeCN)
        {
            ////
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
          //  MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select base_echeance.ID, ";
                selectQuery += "        base_echeance.ID_TRAITEMENT, base_traitement.ID_COMM,  ";
                selectQuery += "        base_echeance.MONTANT, ";
                selectQuery += "        base_echeance.ID_FACTURATION,";
                selectQuery += "        base_echeance.DTEECHEANCE, ";
                selectQuery += "        base_echeance.LIBELLE, ";
                selectQuery += "        base_echeance.ID_PATIENT, ";

                if (!IncludeCN)
                {
                    selectQuery += @"        case (
                                            select INCN from base_paiementreel
                                            inner join base_encaissement on base_encaissement.id_paiement_reel=base_paiementreel.id  and base_encaissement.id = base_echeance.id_encaissement
                                            )
                                                     when null then -1
                                                     when 'True' then -1
                                                     else base_echeance.ID_ENCAISSEMENT
                                                   end as ID_ENCAISSEMENT,";
                }
                else
                {

                    selectQuery += "        base_echeance.ID_ENCAISSEMENT,";
                }

                selectQuery += "        base_echeance.ID_MUTUELLE,";
                selectQuery += "        base_echeance.TYPEPAYEUR,";
                selectQuery += "        base_echeance.TYPEPAYEUR,";
                selectQuery += "        base_echeance.PARPRELEVEMENT, ";
                selectQuery += "        base_echeance.ReleveDeCompte, ";
                selectQuery += "        base_echeance.Relance, ";
                selectQuery += "        base_echeance.PreContentieux, ";
                selectQuery += "        base_echeance.Majoration, ";
                selectQuery += "        base_echeance.Contentieux,base_echeance.TYPEACTE, ";
                selectQuery += "        base_echeance.PARVIREMENT ";

                selectQuery += " from base_echeance";
                selectQuery += " inner join base_traitement on base_traitement.ID = base_echeance.ID_TRAITEMENT and base_traitement.ID_PATIENT = @id_patient  ";
                selectQuery += " order by DTEECHEANCE desc";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@id_patient", pat.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
              //  transaction.Commit();

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


        public static DataTable GetEcheances(ActePG acte,bool IncludeCN,bool IncludePerte =false)
        {
            return GetEcheances(acte.Id,IncludeCN,IncludePerte);

        }



        public static DataTable GetEcheanceAPrelever(int Idacte)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
         //   MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select base_echeance.ID, ";
                selectQuery += "        base_echeance.ID_TRAITEMENT, base_traitement.ID_COMM,  ";
                selectQuery += "        base_echeance.MONTANT, ";
                selectQuery += "        base_echeance.DTEECHEANCE, ";
                selectQuery += "        base_echeance.LIBELLE, ";
                selectQuery += "        base_echeance.ID_PATIENT, ";
                selectQuery += "        base_echeance.ID_ENCAISSEMENT,";
                selectQuery += "        base_echeance.ID_MUTUELLE,";
                selectQuery += "        base_echeance.TYPEPAYEUR,";
                selectQuery += "        base_echeance.PARPRELEVEMENT, ";
                selectQuery += "        base_echeance.PARVIREMENT, ";
                selectQuery += "        base_echeance.ReleveDeCompte, ";
                selectQuery += "        base_echeance.Relance, ";
                selectQuery += "        base_echeance.PreContentieux,base_echeance.TYPEACTE, ";
                selectQuery += "        base_echeance.Majoration, ";
                selectQuery += "        base_echeance.Contentieux, base_echeance.ID_FACTURATION ";

                selectQuery += " from base_echeance";
                selectQuery += " inner join base_traitement on base_traitement.ID = base_echeance.ID_TRAITEMENT";
                selectQuery += " where base_echeance.ID_TRAITEMENT = @Id and (base_echeance.id_encaissement is null or base_echeance.id_encaissement=-1) and parprelevement = 'True'  ";
                selectQuery += " order by base_echeance.DTEECHEANCE";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@Id", Idacte);

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


        public static DataTable GetEcheancePerte(int Idacte)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
         //   MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select base_echeance.ID, ";
                selectQuery += "        base_echeance.ID_TRAITEMENT, base_traitement.ID_COMM,  ";
                selectQuery += "        base_echeance.MONTANT, ";
                selectQuery += "        base_echeance.DTEECHEANCE, ";
                selectQuery += "        base_echeance.LIBELLE, ";
                selectQuery += "        base_echeance.ID_PATIENT, ";
                selectQuery += "        base_echeance.ID_ENCAISSEMENT,";
                selectQuery += "        base_echeance.ID_MUTUELLE,";
                selectQuery += "        base_echeance.TYPEPAYEUR,";
                selectQuery += "        base_echeance.PARPRELEVEMENT, ";
                selectQuery += "        base_echeance.PARVIREMENT, ";
                selectQuery += "        base_echeance.ReleveDeCompte, ";
                selectQuery += "        base_echeance.Relance, ";
                selectQuery += "        base_echeance.PreContentieux,base_echeance.TYPEACTE, ";
                selectQuery += "        base_echeance.Majoration, ";
                selectQuery += "        base_echeance.Contentieux, base_echeance.ID_FACTURATION ";

                selectQuery += " from base_echeance";
                selectQuery += " inner join base_traitement on base_traitement.ID = base_echeance.ID_TRAITEMENT";
                selectQuery += " where base_echeance.ID_TRAITEMENT = @Id and base_echeance.typepayeur = 5 ";
                selectQuery += " order by base_echeance.DTEECHEANCE";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@Id", Idacte);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
              //  transaction.Commit();

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
        public static DataTable GetEcheances(int Idacte,bool IncludeCN,bool IncludePerte =false)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
       //     MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select base_echeance.id, ";
                selectQuery += "        base_echeance.id_traitement,base_traitement.id_comm, ";
                selectQuery += "        base_echeance.montant, ";
                selectQuery += "        base_echeance.dteecheance, ";
                selectQuery += "        base_echeance.id_facturation, ";
                selectQuery += "        base_echeance.libelle, ";
                selectQuery += "        base_echeance.id_patient, ";


                if (!IncludeCN)
                {
                    selectQuery += @"        case (
                                            select INCN from base_paiementreel
                                            inner join base_encaissement on base_encaissement.id_paiement_reel=base_paiementreel.id  where base_encaissement.id = base_echeance.id_encaissement
                                            )
                                                     when null then -1
                                                     when 'True' then -1
                                                     else base_echeance.id_encaissement
                                                   end as id_encaissement,";
                }
                else
                {

                    selectQuery += "        base_echeance.id_encaissement,";
                }

                selectQuery += "        base_echeance.id_mutuelle,";
                selectQuery += "        base_echeance.typepayeur,";
                selectQuery += "        base_echeance.typepayeur,";
                selectQuery += "        base_echeance.parprelevement, ";
                selectQuery += "        base_echeance.relevedecompte, ";
                selectQuery += "        base_echeance.relance, ";
                selectQuery += "        base_echeance.precontentieux, ";
                selectQuery += "        base_echeance.majoration, ";
                selectQuery += "        base_echeance.contentieux, ";
                selectQuery += "        base_echeance.parvirement, base_echeance.typeacte ";

                selectQuery += " from base_echeance LEFT JOIN base_traitement on base_traitement.ID = base_echeance.id_traitement ";         
                selectQuery += " where base_echeance.id_traitement = @Id";
             
                selectQuery += " order by base_echeance.ID ";




                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@Id", Idacte);

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


        public static double GetSoldeAReglerAvantLe(DateTime dte, int IdPatient)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
         //   MySqlTransaction transaction = connection.BeginTransaction();
            try
            {




                string selectQuery = "select";
                selectQuery += " (";
                selectQuery += " select sum(base_echeance.MONTANT)";
                selectQuery += " from base_echeance";
                selectQuery += " where base_echeance.MONTANT is not null and base_echeance.id_patient = @id_patient and base_echeance.DTEECHEANCE<@dte and (base_echeance.PARPRELEVEMENT <>'True' or (PARPRELEVEMENT is null))  ";
                selectQuery += " )-";
                selectQuery += " (";
                selectQuery += " select sum(base_encaissement.MONTANT)";
                selectQuery += " from base_encaissement";
                selectQuery += " inner join BASE_PAIEMENTREEL on BASE_PAIEMENTREEL.ID=base_encaissement.ID_PAIEMENT_REEL";
                selectQuery += " where base_encaissement.MONTANT is not NULL and BASE_PAIEMENTREEL.dateencaissement<@dte and base_encaissement.id_patient = @id_patient";
                selectQuery += " )";
                selectQuery += " from RDB$DATABASE";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
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
        public static void UpdateCommMaterielEcheance(int idMateriel ,  int id_echeance, int IdComm)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "update base_comm_MAT set ID_ECHEANCE = @ID_ECHEANCE ";
                selectQuery += " where ID_COMM=@ID_COMM AND ID_MATERIEL = @ID_MATERIEL ";



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;




                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@ID_COMM", IdComm);

                command.Parameters.AddWithValue("@ID_MATERIEL",idMateriel );
                

                command.Parameters.AddWithValue("@ID_ECHEANCE", id_echeance);
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
        public static void UpdateCommActeEcheance(CommActes cm, string TypeActe, int id_echeance, int IdComm)
        {
         if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "update base_comm_ACTES set ID_ECHEANCE = @ID_ECHEANCE ";
                selectQuery += " where ID_COMM=@ID_COMM AND ID_ACTE = @ID_ACTE and TYPE_ACTE_SUPP = @TYPE_ACTE_SUPP";



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;




                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@ID_COMM", IdComm);
              
                command.Parameters.AddWithValue("@ID_ACTE", cm.IdActe);
                command.Parameters.AddWithValue("@TYPE_ACTE_SUPP", TypeActe);
               /* switch ( TypeActe )
                    {
                        case 2 :command.Parameters.AddWithValue("@TYPE_ACTE_SUPP", "");break;
                        case 3: command.Parameters.AddWithValue("@TYPE_ACTE_SUPP", "R"); break;
                        case 4: command.Parameters.AddWithValue("@TYPE_ACTE_SUPP", "P"); break; 
                    }*/
                
                command.Parameters.AddWithValue("@ID_ECHEANCE", id_echeance);
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

        public static void InsertEcheance(Echeance echeancePG, int ActePrincipale=0)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "select MAX(ID)+1 as NEWID from base_echeance";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                object obj = command.ExecuteScalar();
                if (obj == DBNull.Value)
                    echeancePG.Id = 1;
                else
                    echeancePG.Id = Convert.ToInt32(command.ExecuteScalar());


                selectQuery = "insert into base_echeance (ID, ";
                selectQuery += "                           ID_TRAITEMENT, ";
                selectQuery += "                           MONTANT, ";
                selectQuery += "                           PARPRELEVEMENT, ";
                selectQuery += "                           ParVirement, ";
                selectQuery += "                           DTEECHEANCE, ";
                selectQuery += "                           ID_PATIENT, ";
                selectQuery += "                           ID_MUTUELLE,";
                selectQuery += "                           ID_ENCAISSEMENT, ";
                selectQuery += "                           TYPEPAYEUR, ";
                selectQuery += "        ReleveDeCompte, ";
                selectQuery += "        Relance, ";
                selectQuery += "        PreContentieux, ";
                selectQuery += "        Majoration, ";
                selectQuery += "        Contentieux, ";
                selectQuery += "                           LIBELLE,TYPEACTE,ID_FACTURATION,DATEFIX) ";
                selectQuery += " values (@ID, ";
                selectQuery += "        @ID_TRAITEMENT, ";
                selectQuery += "        @MONTANT, ";
                selectQuery += "        @PARPRELEVEMENT, ";
                selectQuery += "        @ParVirement, ";
                
                selectQuery += "        @DTEECHEANCE, ";
                selectQuery += "        @ID_PATIENT, ";
                selectQuery += "        @ID_MUTUELLE,";
                selectQuery += "        @ID_ENCAISSEMENT, ";
                selectQuery += "        @TYPEPAYEUR, ";
                selectQuery += "        @ReleveDeCompte, ";
                selectQuery += "        @Relance, ";
                selectQuery += "        @PreContentieux, ";
                selectQuery += "        @Majoration, ";
                selectQuery += "        @Contentieux, ";
                selectQuery += "        @LIBELLE,@TYPEACTE,@ID_FACTURATION,@DATEFIX) ";


                command.CommandText = selectQuery;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@ID", echeancePG.Id);
                command.Parameters.AddWithValue("@ID_TRAITEMENT", echeancePG.IdActe);
                command.Parameters.AddWithValue("@MONTANT", echeancePG.Montant);
                command.Parameters.AddWithValue("@PARPRELEVEMENT", echeancePG.ParPrelevement);
                command.Parameters.AddWithValue("@ParVirement", echeancePG.ParVirement);
                command.Parameters.AddWithValue("@DTEECHEANCE", echeancePG.DateEcheance);
                command.Parameters.AddWithValue("@DATEFIX", echeancePG.DateFix == null ? echeancePG.DateEcheance : echeancePG.DateFix);
                command.Parameters.AddWithValue("@LIBELLE", echeancePG.Libelle);
                command.Parameters.AddWithValue("@ID_PATIENT", echeancePG.IdPatient);
                command.Parameters.AddWithValue("@ID_ENCAISSEMENT", echeancePG.ID_Encaissement);
                command.Parameters.AddWithValue("@ID_MUTUELLE", echeancePG.mutuelle == null ? -1 : (object)echeancePG.mutuelle.Id);
                command.Parameters.AddWithValue("@TYPEPAYEUR", echeancePG.payeur);
                command.Parameters.AddWithValue("@ID_FACTURATION", -1);
                
                command.Parameters.AddWithValue("@ReleveDeCompte", echeancePG.Relances.ReleveDeCompte == null ? DBNull.Value : (object)echeancePG.Relances.ReleveDeCompte);
                command.Parameters.AddWithValue("@Relance", echeancePG.Relances.Relance == null ? DBNull.Value : (object)echeancePG.Relances.Relance);
                command.Parameters.AddWithValue("@PreContentieux", echeancePG.Relances.PreContentieux == null ? DBNull.Value : (object)echeancePG.Relances.PreContentieux);
                command.Parameters.AddWithValue("@Majoration", echeancePG.Relances.Majoration == null ? DBNull.Value : (object)echeancePG.Relances.Majoration);
                command.Parameters.AddWithValue("@Contentieux", echeancePG.Relances.Contentieux == null ? DBNull.Value : (object)echeancePG.Relances.Contentieux);
                command.Parameters.AddWithValue("@TYPEACTE", echeancePG.TypeActe);
              /*  switch (echeancePG.TypeActe)
                {
                    case 1: command.Parameters.AddWithValue("@TYPEACTE", "0"); break;
                    case 2: command.Parameters.AddWithValue("@TYPEACTE", ""); break;
                    case 3: command.Parameters.AddWithValue("@TYPEACTE", "R"); break;
                    case 4: command.Parameters.AddWithValue("@TYPEACTE", "P"); break;
                    case 0: command.Parameters.AddWithValue("@TYPEACTE", "M"); break;
                }*/
               
                command.ExecuteNonQuery();

/*
                 insertion dans Base_Comm_Echeances
               

                selectQuery = "INSERT INTO base_comm_ECHEANCES ( id_comm,ID_ECHEANCE, MONTANT, ID_ACTE, TYPE_ACTE_SUPP) ";
 selectQuery += " VALUES ( ";
 selectQuery += " @id_comm,@ID_ECHEANCE,  ";
selectQuery += " @MONTANT,  ";
selectQuery += " @ID_ACTE,  ";
selectQuery += " @TYPE_ACTE_SUPP ";
selectQuery += " ) ";
               


                command.CommandText = selectQuery;
                command.Parameters.Clear();
                if (echeancePG .TypeActe == "0")
                    command.Parameters.AddWithValue("@ID_COMM", echeancePG.IdComm);
                else
                command.Parameters.AddWithValue("@ID_COMM", echeancePG.acte .IdComm );
                command.Parameters.AddWithValue("@ID_ECHEANCE", echeancePG.Id);
                command.Parameters.AddWithValue("@MONTANT", echeancePG.Montant);
                command.Parameters.AddWithValue("@ID_ACTE", echeancePG.IdActeTraitement);
                command.Parameters.AddWithValue("@TYPE_ACTE_SUPP", echeancePG.TypeActe);
              
              //  command.Parameters.AddWithValue("@TYPE_ACTE_SUPP", echeancePG.TypeActe);
               

                command.ExecuteNonQuery();*/




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

        public static void DeleteEcheance(Echeance echeancePG)
        {
            string selectQuery = "";
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                 selectQuery = "delete from base_echeance";
                selectQuery += " where  id = @Id";



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;




                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@Id", echeancePG.Id);
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
            insertLog(selectQuery, System.Security.Principal.WindowsIdentity.GetCurrent().Name, echeancePG.patient.ToString(),echeancePG.Id.ToString());
         

        }

        public static void DeleteEcheances(ActePG acte)
        
        {
            string selectQuery = "";
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
         
                
                //Supession des Echeances
                 selectQuery = "delete from base_echeance";
                 selectQuery += " where  ID_TRAITEMENT = @Id and (ID_ENCAISSEMENT is null or ID_ENCAISSEMENT<1) ";



                MySqlCommand command2 = new MySqlCommand(selectQuery, connection, transaction);
                command2.CommandType = CommandType.Text;




                command2.CommandText = selectQuery;
                command2.Parameters.AddWithValue("@Id", acte.Id);
                command2.ExecuteNonQuery();

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
            
            insertLog(selectQuery, System.Security.Principal.WindowsIdentity.GetCurrent().Name,acte.patient == null ? "" : acte.patient.ToString(), acte.Id + " :ID_TRAITEMENT");


        }


        public static void UpdateEcheanceF(Echeance echeance)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "update base_echeance set ID_FACTURATION = @ID_FACTURATION ";
                selectQuery += " where ID=@ID";



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;




                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@ID", echeance.Id);
                command.Parameters.AddWithValue("@ID_FACTURATION", echeance.ID_Facturation);
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
        #region Perte

        public static DataTable getPatientwithTotalPerteBetwenDates(DateTime debut, DateTime fin)
        {

            if (connection == null) getConnection();
            if (connection.State == ConnectionState.Closed) connection.Open();

            string sqlQuery = "select trim(p.PER_NOM) as PER_NOM, trim(p.PER_PRENOM) as PER_PRENOM,";
            sqlQuery += " sum(e.MONTANT) total_Perte from base_echeance e";
            sqlQuery += " left join personne p on p.ID_PERSONNE=e.ID_PATIENT";
            sqlQuery += " where e.TYPEPAYEUR=5 and cast(e.DTEECHEANCE as date) between @debut and @fin";
            sqlQuery += " group by p.PER_NOM,p.PER_PRENOM";

            MySqlTransaction transaction = connection.BeginTransaction();

            try
            {
                MySqlCommand command = new MySqlCommand(sqlQuery, connection, transaction);
                command.Parameters.AddWithValue("@debut", debut);
                command.Parameters.AddWithValue("@fin", fin);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];


                return dt;

            }
            catch (FbException ex) { throw ex; }
            finally { 
               connection.Close();
            }

        }
        #endregion
    }
}
