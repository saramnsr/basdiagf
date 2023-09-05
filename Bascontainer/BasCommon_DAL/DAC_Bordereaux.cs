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



        public static void suppressionPaiementfrombordereau(BordereauFinance bf,PaiementReel pr)
        {

            bool cancontinue = false;
            foreach (PaiementReel bfpr in bf.paiements)
            {
                if (bfpr.typeencaissement == PaiementReel.TypeEncaissement.Especes) return;
                if (pr.Id == bfpr.Id)
                {
                    cancontinue = true;
                }
            }

            if (!cancontinue) return;
            
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                

                string selectQuery = "update base_paiementreel";
                selectQuery += " set base_paiementreel.datevaleurbanque = null,";
                selectQuery += "  base_paiementreel.DATEREMISEENBANQUE = null,";
                selectQuery += "  base_paiementreel.REMISENBANQUE = 0";
                selectQuery += " where base_paiementreel.id =@idpaiement";
                



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                command.Parameters.AddWithValue("@idpaiement", pr.Id);
                

                command.ExecuteNonQuery();



                selectQuery = "update bas_bordereau_finance";
                selectQuery += " set NB_CHEQUE=@NB_CHEQUE,";
                selectQuery += " MONTANT_TOTAL=@MONTANT_TOTAL,";
                selectQuery += " NB_PRELEVEMENT=@NB_PRELEVEMENT,";
                selectQuery += " NB_VIREMENT=@NB_VIREMENT,";
                selectQuery += " NB_CB=@NB_CB";
                selectQuery += " where ID=@ID_BORDEREAU ";


                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@NB_CHEQUE", bf.NbCheques);
                command.Parameters.AddWithValue("@MONTANT_TOTAL", bf.Montant);
                command.Parameters.AddWithValue("@NB_CB", bf.NbCBs);
                command.Parameters.AddWithValue("@NB_PRELEVEMENT", bf.NbPrelevements);
                command.Parameters.AddWithValue("@NB_VIREMENT", bf.NbVirements);
                command.Parameters.AddWithValue("@ID_BORDEREAU", bf.Id);

                command.ExecuteNonQuery();



                selectQuery = "delete from bas_lnk_bordereau_paiement";
                selectQuery += " where ID_PAIEMENT = @ID_PAIEMENT and ID_BORDEREAU=@ID_BORDEREAU ";

                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@ID_PAIEMENT", pr.Id);
                command.Parameters.AddWithValue("@ID_BORDEREAU", bf.Id);

                command.ExecuteNonQuery();

                selectQuery = "delete from bas_lnk_paiement_ctrl";
                selectQuery += " where ID_PAIEMENT = @ID_PAIEMENT";

                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@ID_PAIEMENT", pr.Id);

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



        public static void validateBordereau(BordereauFinance bf)
        {

            if (bf.DateValeur == null) 
                throw new System.Exception("Impossible de valider un bordereau qui n'a pas de date de validation");

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                

                string selectQuery = "update base_paiementreel";
                selectQuery += " set base_paiementreel.datevaleurbanque = @dtevaleur,";
                selectQuery += "  base_paiementreel.DATEREMISEENBANQUE = @DATEREMISE,";
                selectQuery += "  base_paiementreel.REMISENBANQUE = 1,";
                selectQuery += "  base_paiementreel.ID_BANQUE_REMISE = @ID_BANQUE_REMISE";
                selectQuery += " where base_paiementreel.id in";
                selectQuery += " (";
                selectQuery += "     select bas_lnk_bordereau_paiement.id_paiement";
                selectQuery += "     from bas_lnk_bordereau_paiement";
                selectQuery += "     where bas_lnk_bordereau_paiement.id_bordereau=@IdBordereau";
                selectQuery += " )";
                



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                command.Parameters.AddWithValue("@IdBordereau", bf.Id);
                command.Parameters.AddWithValue("@dtevaleur", bf.DateValeur.Value);
                command.Parameters.AddWithValue("@DATEREMISE", bf.DateRemise.Value);
                command.Parameters.AddWithValue("@ID_BANQUE_REMISE", bf.BanqueDeRemise.Code);
                

                command.ExecuteNonQuery();

                
                selectQuery = "update bas_bordereau_finance";
                selectQuery += " set DATEDEVALIDATION = @dtevaleur,";
                selectQuery += " DATEREMISE = @DATEREMISE,";
                selectQuery += " COD_BAN = @COD_BAN,";
                selectQuery += " NUM_BORDEREAU_BQE = @NUM_BORDEREAU_BQE";
                selectQuery += " where bas_bordereau_finance.id = @IdBordereau ";

                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@NUM_BORDEREAU_BQE", bf.NumBordereauBancaire);
                command.Parameters.AddWithValue("@COD_BAN", bf.BanqueDeRemise.Code);

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



        public static DataTable GetPaiementsToCheck(BanqueDeRemise bqe, DateTime dte1, DateTime dte2)
        {
            List<Caisse> lst = new List<Caisse>();
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {




                string selectQuery = "select base_paiementreel.id, ";
                selectQuery += "       base_paiementreel.montant, ";
                selectQuery += "       base_paiementreel.NUMCHEQUE, ";
                selectQuery += "       base_paiementreel.moyenpaiement,";
                selectQuery += "       base_paiementreel.DATEENCAISSEMENT,";
                selectQuery += "       base_paiementreel.DATEVALEURBANQUE,";
                selectQuery += "       base_paiementreel.ID_BANQUE_EMETRICE,";
                selectQuery += "       base_paiementreel.ID_MUTUELLE,";
                selectQuery += "       base_paiementreel.REMISENBANQUE,";
                selectQuery += "       base_paiementreel.IDPAYEUR,";
                selectQuery += "       base_paiementreel.INCN,";                
                selectQuery += "       base_paiementreel.PAYEUR,";
                selectQuery += "       base_paiementreel.ISPNF,";

                selectQuery += "       base_paiementreel.EspecesRecu,";
                selectQuery += "       base_paiementreel.EspecesRendus,";
                selectQuery += "       base_paiementreel.EspecesMisEncaisse,";

                selectQuery += "       TRIM(personne.per_nom)||' '||TRIM(personne.per_prenom) PAYEUR,";
                selectQuery += "       patient.id_personne IDPATIENT,";
                selectQuery += "       TRIM(patient.PER_NOM)||' '||TRIM(patient.PER_PRENOM) PATIENT,";
                selectQuery += "       base_paiementreel.DATEREMISEENBANQUE,";
                selectQuery += "       base_paiementreel.STATUS,";
                selectQuery += "       base_paiementreel.LIBELLEINCIDENT,";
                selectQuery += "       base_paiementreel.MONTANTREMIS,";
                selectQuery += "       base_paiementreel.DATEECHEANCE,";
                selectQuery += "       base_paiementreel.ID_BANQUE_REMISE,";
                selectQuery += "       base_paiementreel.MONTANT_EN_BANQUE,";


                selectQuery += "       base_paiementreel.ID_ENTITYJURIDIQUE";
                selectQuery += " from base_paiementreel";
                selectQuery += " inner join bas_lnk_paiement_ctrl lnk on lnk.ID_PAIEMENT = base_paiementreel.id";

                selectQuery += " inner join base_encaissement on base_encaissement.id_paiement_reel = base_paiementreel.id";
                selectQuery += " inner join base_echeance on base_echeance.ID_ENCAISSEMENT = base_encaissement.id and base_echeance.id_patient=base_encaissement.id_patient and base_echeance.typepayeur=0";
                selectQuery += " inner join personne patient on base_encaissement.id_patient = patient.id_personne";

                selectQuery += " LEFT OUTER JOIN personne ON personne.ID_PERSONNE=base_paiementreel.IDPAYEUR";
                selectQuery += " where datevaleurbanque is NULL and DATEREMISEENBANQUE is not null and base_paiementreel.ID_BANQUE_REMISE<>''";
                if (bqe != null) selectQuery += " and  base_paiementreel.ID_BANQUE_REMISE = @idbqe";
                selectQuery += " and base_paiementreel.DATEENCAISSEMENT between @dte1 and @dte2";

                selectQuery += " and ((INCN is null) or (INCN<>'True')) ";
                
                selectQuery += " order by base_paiementreel.DATEECHEANCE desc";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@dte1", dte1.Date);
                command.Parameters.AddWithValue("@dte2", dte2.Date.AddDays(1));
                
                
                if (bqe != null) command.Parameters.AddWithValue( "@idbqe",bqe.Code);
                
                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];
                return dt;

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


        public static DataTable GetBordereauToCheck(BanqueDeRemise bqe,DateTime dte1,DateTime dte2)
        {
            List<Caisse> lst = new List<Caisse>();
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {




                string selectQuery = "select";
                selectQuery += " ID,";
                selectQuery += " NUM_BORDEREAU,";
                selectQuery += " COD_BAN,";
                selectQuery += " NB_CHEQUE,";
                selectQuery += " NB_CB,";
                selectQuery += " NB_PRELEVEMENT,";
                selectQuery += " NB_VIREMENT,";
                
                selectQuery += " NB_billets5,";
                selectQuery += " NB_billets10,";
                selectQuery += " NB_billets20,";
                selectQuery += " NB_billets50,";
                selectQuery += " NB_billets100,";
                selectQuery += " NB_billets200,";
                selectQuery += " NB_billets500,";

                selectQuery += " NUM_BORDEREAU_BQE,";

                

                selectQuery += " ID_CONTROL,";
                selectQuery += " MONTANT_TOTAL,";
                selectQuery += " DATEREMISE,";
                selectQuery += " DATEDEVALIDATION";
                selectQuery += " from bas_bordereau_finance";
                selectQuery += " where DATEDEVALIDATION is NULL";
                selectQuery += " and DATEREMISE between @dte1 and @dte2";
                if (bqe != null) selectQuery += " and COD_BAN = @idbqe";


                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@dte1", dte1.Date);
                command.Parameters.AddWithValue("@dte2", dte2.Date.AddDays(1));
                if (bqe != null) command.Parameters.AddWithValue( "@idbqe",bqe.Code);
                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];
                return dt;

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


        
        public static void InsertBordereaufinance(BordereauFinance bf)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();


            string selectQueryId = "select max(Id)+1 as ID from bas_bordereau_finance";
            MySqlCommand commandid = new MySqlCommand(selectQueryId, connection, transaction);
            object id = commandid.ExecuteScalar();
            if (id == DBNull.Value)
                bf.Id = 1;
            else
                bf.Id = Convert.ToInt32(id);



            try
            {


                string selectQuery = "insert into bas_bordereau_finance (ID, ";
                selectQuery += "                            NUM_BORDEREAU, ";
                selectQuery += "                            NUM_BORDEREAU_BQE, ";
                selectQuery += "                            COD_BAN, ";
                selectQuery += "                            NB_CHEQUE, ";
                selectQuery += "                            NB_CB, ";
                selectQuery += "                            NB_PRELEVEMENT, ";
                selectQuery += "                            NB_VIREMENT, ";



                
                selectQuery += "                            NB_billets5,";
                selectQuery += "                            NB_billets10,";
                selectQuery += "                            NB_billets20,";
                selectQuery += "                            NB_billets50,";
                selectQuery += "                            NB_billets100,";
                selectQuery += "                            NB_billets200,";
                selectQuery += "                            NB_billets500,";

                selectQuery += "                            ID_CONTROL, ";
                selectQuery += "                            DATEREMISE, ";
                selectQuery += "                            DATEDEVALIDATION, ";
                selectQuery += "                            MONTANT_TOTAL)";
                selectQuery += " values (@ID, ";
                selectQuery += "                            @NUM_BORDEREAU, ";
                selectQuery += "                            @NUM_BORDEREAU_BQE, ";
                selectQuery += "                            @COD_BAN, ";
                selectQuery += "                            @NB_CHEQUE, ";
                selectQuery += "                            @NB_CB, ";
                selectQuery += "                            @NB_PRELEVEMENT, ";
                selectQuery += "                            @NB_VIREMENT, ";

                selectQuery += "                            @NB_billets5,";
                selectQuery += "                            @NB_billets10,";
                selectQuery += "                            @NB_billets20,";
                selectQuery += "                            @NB_billets50,";
                selectQuery += "                            @NB_billets100,";
                selectQuery += "                            @NB_billets200,";
                selectQuery += "                            @NB_billets500,";

                selectQuery += "                            @ID_CONTROL, ";
                selectQuery += "                            @DATEREMISE, ";
                selectQuery += "                            @DATEDEVALIDATION, ";
                selectQuery += "                            @MONTANT_TOTAL)";




                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@ID", bf.Id);
                command.Parameters.AddWithValue("@NUM_BORDEREAU", bf.NumBordereau);
                command.Parameters.AddWithValue("@NUM_BORDEREAU_BQE", bf.NumBordereauBancaire);
                command.Parameters.AddWithValue("@COD_BAN", bf.BanqueDeRemise.Code);
                command.Parameters.AddWithValue("@NB_CHEQUE", bf.NbCheques);
                command.Parameters.AddWithValue("@NB_CB", bf.NbCBs);
                command.Parameters.AddWithValue("@NB_PRELEVEMENT", bf.NbPrelevements);
                command.Parameters.AddWithValue("@NB_VIREMENT", bf.NbVirements);
                
                command.Parameters.AddWithValue("@NB_billets5", bf.Nbbillets5);
                command.Parameters.AddWithValue("@NB_billets10", bf.Nbbillets10);
                command.Parameters.AddWithValue("@NB_billets20", bf.Nbbillets20);
                command.Parameters.AddWithValue("@NB_billets50", bf.Nbbillets50);
                command.Parameters.AddWithValue("@NB_billets100", bf.Nbbillets100);
                command.Parameters.AddWithValue("@NB_billets200", bf.Nbbillets200);
                command.Parameters.AddWithValue("@NB_billets500", bf.Nbbillets500);
                
                
                command.Parameters.AddWithValue("@ID_CONTROL", bf.IdControlFinancier);
                command.Parameters.AddWithValue("@DATEREMISE", bf.DateRemise);
                command.Parameters.AddWithValue("@DATEDEVALIDATION", bf.DateValeur==null?DBNull.Value:(object)bf.DateValeur.Value);
                command.Parameters.AddWithValue("@MONTANT_TOTAL", bf.Montant);

                command.ExecuteNonQuery();


                selectQuery = "insert into bas_lnk_bordereau_paiement (ID_PAIEMENT, ";
                selectQuery += "                            ID_BORDEREAU)";
                selectQuery += " values (               @ID_PAIEMENT, ";
                selectQuery += "                            @ID_BORDEREAU)";

                command.CommandText = selectQuery;

                foreach (PaiementReel pr in bf.paiements)
                {
                    

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@ID_PAIEMENT", pr.Id);
                    command.Parameters.AddWithValue("@ID_BORDEREAU", bf.Id);               

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
}
