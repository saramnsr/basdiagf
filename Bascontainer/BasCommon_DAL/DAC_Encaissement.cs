using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using System.Configuration;
using BasCommon_BO;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;

namespace BasCommon_DAL
{
    public static partial class DAC
    {


        public static DataTable GetEncaissements(PaiementReel paymnt)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = "select distinct base_encaissement.id, ";
                selectQuery += "       id_patient, ";
                selectQuery += "       MONTANT_ENCAISSE, ";
                selectQuery += "       ID_PAIEMENT_REEL";
                selectQuery += " from base_encaissement";
                selectQuery += " inner join base_paiementreel on base_paiementreel.ID=base_encaissement.ID_PAIEMENT_REEL";

                selectQuery += " where base_encaissement.ID_PAIEMENT_REEL = @ID_PAIEMENT_REEL";
                selectQuery += " order by base_paiementreel.DATEENCAISSEMENT desc";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@ID_PAIEMENT_REEL", paymnt.Id);

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


        public static void DeleteEncaissement(Encaissement encaissement, bool DeletePaiementReel,bool ForceDeletionIfControled)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();

            try
            {
                MySqlCommand command;
                string selectQuery;





                if (DeletePaiementReel)
                {

                    if (!ForceDeletionIfControled)
                    {
                        selectQuery = @"select base_encaissement.ID_PAIEMENT_REEL,bas_lnk_paiement_ctrl.id_paiement, base_paiementreel.dateremiseenbanque
                                    from base_encaissement
                                    inner join base_paiementreel on base_paiementreel.id =  base_encaissement.ID_PAIEMENT_REEL
                                    left join bas_lnk_paiement_ctrl on bas_lnk_paiement_ctrl.id_paiement=  base_encaissement.ID_PAIEMENT_REEL
                                    where  bas_lnk_paiement_ctrl.id_paiement is null and  base_encaissement.id= @id and base_paiementreel.dateremiseenbanque is null";
                    }
                    else
                    {
                        selectQuery = @"select base_encaissement.ID_PAIEMENT_REEL
                                    from base_encaissement
                                    inner join base_paiementreel on base_paiementreel.id =  base_encaissement.ID_PAIEMENT_REEL
                                    where  base_encaissement.id= @id";
                    
                    }
                    command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@id", encaissement.Id);

                    Object obj = command.ExecuteScalar();
                    if (obj == null)
                        throw new System.Exception("Suppression impossible, l'encaissement à été controlé ou remis en banque!");

                    int NumPaiement = (int)obj;


                    selectQuery = " update base_echeance set  ID_ENCAISSEMENT = NULL where ID_ENCAISSEMENT in (select base_encaissement.ID from base_encaissement where ID_PAIEMENT_REEL=@idpreel)";
                    command = new MySqlCommand(selectQuery, connection, transaction);
                    command.CommandText = selectQuery;
                    command.Parameters.AddWithValue("@id", encaissement.Id);
                    command.Parameters.AddWithValue("@idpreel", NumPaiement);
                    command.ExecuteNonQuery();

                    selectQuery = " delete from base_encaissement where ID_PAIEMENT_REEL=@idpreel";
                    command.CommandText = selectQuery;
                    command.ExecuteNonQuery();

                    selectQuery = " delete from bas_lnk_paiement_ctrl where ID_PAIEMENT=@idpreel";
                    command.CommandText = selectQuery;
                    command.ExecuteNonQuery();

                    selectQuery = " delete from bas_lnk_bordereau_paiement where ID_PAIEMENT=@idpreel";
                    command.CommandText = selectQuery;
                    command.ExecuteNonQuery();

                    selectQuery = " delete from base_paiementreel where ID=@idpreel";

                    command.CommandText = selectQuery;
                    command.ExecuteNonQuery();


                }
                else
                {

                    selectQuery = " update base_echeance set  ID_ENCAISSEMENT = null";
                    selectQuery += " where ID_ENCAISSEMENT= @id ";
                    command = new MySqlCommand(selectQuery, connection, transaction);
                    command.CommandText = selectQuery;
                    command.Parameters.AddWithValue("@id", encaissement.Id);
                    command.ExecuteNonQuery();

                    selectQuery = " delete from base_encaissement ";
                    selectQuery += " where id= @id ";

                    command = new MySqlCommand(selectQuery, connection, transaction);

                    command.Parameters.AddWithValue("@id", encaissement.Id);

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

        public static void InsertPaiementReel(PaiementReel encaissement)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();


            string selectQueryId = "select max(Id)+1 as ID from base_paiementreel";
            MySqlCommand commandid = new MySqlCommand(selectQueryId, connection, transaction);
            object id = commandid.ExecuteScalar();
            if (id == DBNull.Value)
                encaissement.Id = 1;
            else
                encaissement.Id = Convert.ToInt32(id);



            try
            {

                string selectQuery = " insert into base_paiementreel (id, ";
                selectQuery += "                               montant, ";
                selectQuery += "                               moyenpaiement, ";
                selectQuery += "                               DATEENCAISSEMENT, ";
                selectQuery += "                               DATEVALEURBANQUE, ";
                selectQuery += "                               NUMCHEQUE, ";
                selectQuery += "                               ID_BANQUE_EMETRICE, ";
                selectQuery += "                               REMISENBANQUE, ";
                selectQuery += "                               DATEREMISEENBANQUE, ";
                selectQuery += "                               ID_ENTITYJURIDIQUE, ";
                selectQuery += "                               IDPAYEUR, ";
                selectQuery += "                               PAYEUR, ";
                selectQuery += "                               montant_en_banque, ";
                selectQuery += "                               ID_BANQUE_REMISE, ";
                selectQuery += "                               DATEECHEANCE, ";
                selectQuery += "                               ESPECESRECU, ";
                selectQuery += "                               ESPECESRENDUS, ";
                selectQuery += "                               ESPECESMISENCAISSE, ";
                selectQuery += "                               ID_MUTUELLE, ";
                selectQuery += "                               STATUS, ";
                selectQuery += "                               ISPNF, ";
                selectQuery += "                               MONTANTREMIS, ID_PRATICIEN)";
                selectQuery += " values (@id, ";

                selectQuery += "                               @montant, ";
                selectQuery += "                               @moyenpaiement, ";
                selectQuery += "                               @DATEENCAISSEMENT, ";
                selectQuery += "                               @DATEVALEURBANQUE, ";
                selectQuery += "                               @NUMCHEQUE, ";
                selectQuery += "                               @ID_BANQUE_EMETRICE, ";
                selectQuery += "                               @REMISENBANQUE, ";
                selectQuery += "                               @DATEREMISEENBANQUE, ";
                selectQuery += "                               @ID_ENTITYJURIDIQUE, ";
                selectQuery += "                               @IDPAYEUR, ";
                selectQuery += "                               @PAYEUR, ";
                selectQuery += "                               @montant_en_banque, ";
                selectQuery += "                               @ID_BANQUE_REMISE, ";
                selectQuery += "                               @DATEECHEANCE, ";
                selectQuery += "                               @ESPECESRECU, ";
                selectQuery += "                               @ESPECESRENDUS, ";
                selectQuery += "                               @ESPECESMISENCAISSE, ";
                selectQuery += "                               @ID_MUTUELLE, ";
                selectQuery += "                               @STATUS, ";
                selectQuery += "                               @ISPNF, ";
                selectQuery += "        @MONTANTREMIS,@ID_PRATICIEN)";




                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id", encaissement.Id);
                command.Parameters.AddWithValue("@montant", encaissement.Montant);
                command.Parameters.AddWithValue("@moyenpaiement", encaissement.typeencaissement);
                command.Parameters.AddWithValue("@DATEENCAISSEMENT", encaissement.DateEncaissement);
                command.Parameters.AddWithValue("@DATEVALEURBANQUE", encaissement.DateValeurBqe == null ? DBNull.Value : (object)encaissement.DateValeurBqe.Value);
                command.Parameters.AddWithValue("@NUMCHEQUE", encaissement.NumCheque);
                command.Parameters.AddWithValue("@ID_BANQUE_EMETRICE", encaissement.BanqueEmetrice == null ? DBNull.Value : (object)encaissement.BanqueEmetrice.Id);
                command.Parameters.AddWithValue("@REMISENBANQUE", encaissement.EstRemisEnBanque);
                command.Parameters.AddWithValue("@DATEREMISEENBANQUE", encaissement.DateRemiseEnBanque == null ? DBNull.Value : (object)encaissement.DateRemiseEnBanque.Value);
                command.Parameters.AddWithValue("@ID_ENTITYJURIDIQUE", encaissement.EntiteJuridique == null ? DBNull.Value : (object)encaissement.EntiteJuridique.Id);
                command.Parameters.AddWithValue("@IDPAYEUR", encaissement.IdPayeur);
                command.Parameters.AddWithValue("@PAYEUR", encaissement.payeur);
                command.Parameters.AddWithValue("@MONTANT_EN_BANQUE", encaissement.MontantEnBanque);
                command.Parameters.AddWithValue("@ID_BANQUE_REMISE", encaissement.BanqueDeRemise == null ? DBNull.Value : (object)encaissement.BanqueDeRemise.Code);
                command.Parameters.AddWithValue("@DATEECHEANCE", encaissement.DateEcheance==null?DBNull.Value:(object)encaissement.DateEcheance.Value);

                command.Parameters.AddWithValue("@ESPECESRECU", encaissement.EspecesRecu);
                command.Parameters.AddWithValue("@ESPECESRENDUS", encaissement.EspecesRendus);
                command.Parameters.AddWithValue("@ESPECESMISENCAISSE", encaissement.EspecesMisEncaisse);

                command.Parameters.AddWithValue("@ID_MUTUELLE", encaissement.Mutuelle == null ? DBNull.Value : (object)encaissement.Mutuelle.Id);

                command.Parameters.AddWithValue("@STATUS", encaissement.Status);
                command.Parameters.AddWithValue("@ISPNF", encaissement.IsPnf);
                command.Parameters.AddWithValue("@MONTANTREMIS", encaissement.MontantRemis);
                command.Parameters.AddWithValue("@ID_PRATICIEN", encaissement.IdPraticien);



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


        
        public static void ADdPnFCheck(PnFCheck pnf)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();


            string selectQueryId = "select max(Id)+1 as ID from bas_pnf_check";
            MySqlCommand commandid = new MySqlCommand(selectQueryId, connection, transaction);
            object id = commandid.ExecuteScalar();
            if (id == DBNull.Value)
                pnf.Id = 1;
            else
                pnf.Id = Convert.ToInt32(id);



            try
            {

                string selectQuery = @"insert into bas_pnf_check (id, id_part_patient, id_part_banque, ""TYPE"", ""DATE"", libelle,Montant_total)
                                    values (@id, @id_part_patient, @id_part_banque, @TPE, @DTE, @libelle,@Montant)";
            




                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id", pnf.Id);
                command.Parameters.AddWithValue("@id_part_patient", pnf.IdPartPatient);
                command.Parameters.AddWithValue("@id_part_banque", pnf.IdPartBanque);
                command.Parameters.AddWithValue("@TPE", pnf.Type);
                command.Parameters.AddWithValue("@DTE", pnf.Date);
                command.Parameters.AddWithValue("@Montant", pnf.Montant);
                command.Parameters.AddWithValue("@libelle", pnf.Libelle);
                

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


        public static void InsertEncaissement(Encaissement encaissement)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();


            string selectQueryId = "select max(Id)+1 as ID from base_encaissement";
            MySqlCommand commandid = new MySqlCommand(selectQueryId, connection, transaction);
            object id = commandid.ExecuteScalar();
            if (id == DBNull.Value)
                encaissement.Id = 1;
            else
                encaissement.Id = Convert.ToInt32(id);



            try
            {

                string selectQuery = " insert into base_encaissement (id, ";
                selectQuery += "                               id_patient, ";
                selectQuery += "                               MONTANT_ENCAISSE, ";
                selectQuery += "                               ID_PAIEMENT_REEL)";
                selectQuery += " values (@id, ";
                selectQuery += "        @id_patient, ";
                selectQuery += "        @MONTANT_ENCAISSE, ";
                selectQuery += "        @ID_PAIEMENT_REEL)";




                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id", encaissement.Id);
                command.Parameters.AddWithValue("@id_patient", encaissement.IdPatient);
                command.Parameters.AddWithValue("@MONTANT_ENCAISSE", encaissement.MontantEncaisse);
                command.Parameters.AddWithValue("@ID_PAIEMENT_REEL", encaissement.IdPaiementReel);


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

        public static void UpdateEncaissement(Encaissement encaissement)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();






            try
            {

                string selectQuery = "update base_encaissement";
                selectQuery += " set id_patient = @id_patient,";
                selectQuery += "    MONTANT_ENCAISSE = @MONTANT_ENCAISSE,";
                selectQuery += "    ID_PAIEMENT_REEL = @ID_PAIEMENT_REEL";
                selectQuery += " where (id = @id)";





                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id", encaissement.Id);
                command.Parameters.AddWithValue("@id_patient", encaissement.IdPatient);
                command.Parameters.AddWithValue("@MONTANT_ENCAISSE", encaissement.MontantEncaisse);
                command.Parameters.AddWithValue("@ID_PAIEMENT_REEL", encaissement.IdPaiementReel);

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


        public static void UpdateEnCaisseNoire(PaiementReel pr,bool IsAdded)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();






            try
            {

                string selectQuery = "update base_paiementreel";
                selectQuery += " set INCN = @INCN";
                selectQuery += " where (id = @id)";





                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id", pr.Id);
                command.Parameters.AddWithValue("@INCN", IsAdded);
                
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


        public static void UpdatePaiementReel(PaiementReel encaissement)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();






            try
            {

                string selectQuery = "update base_paiementreel";
                selectQuery += " set MONTANT = @MONTANT,";
                selectQuery += "    MOYENPAIEMENT = @MOYENPAIEMENT,";
                selectQuery += "    DATEENCAISSEMENT = @DATEENCAISSEMENT,";
                selectQuery += "    DATEVALEURBANQUE = @DATEVALEURBANQUE,";

                
                selectQuery += "    NUMCHEQUE = @NUMCHEQUE,";
                selectQuery += "    ID_BANQUE_EMETRICE = @ID_BANQUE_EMETRICE,";
                selectQuery += "    REMISENBANQUE = @REMISENBANQUE,";
                selectQuery += "    DATEREMISEENBANQUE = @DATEREMISEENBANQUE,";

                selectQuery += "    ID_ENTITYJURIDIQUE = @ID_ENTITYJURIDIQUE,";
                selectQuery += "    IDPAYEUR = @IDPAYEUR,";
                selectQuery += "    PAYEUR = @PAYEUR,";

                selectQuery += "    MONTANT_EN_BANQUE = @MONTANT_EN_BANQUE,";
                selectQuery += "    ID_BANQUE_REMISE = @ID_BANQUE_REMISE,";
                selectQuery += "    DATEECHEANCE = @DATEECHEANCE,";

                selectQuery += "    ESPECESRECU = @ESPECESRECU,";
                selectQuery += "    ESPECESRENDUS = @ESPECESRENDUS,";
                selectQuery += "    ESPECESMISENCAISSE = @ESPECESMISENCAISSE,";
                

                selectQuery += "    ID_MUTUELLE = @ID_MUTUELLE,";
                selectQuery += "    STATUS = @STATUS,";
                selectQuery += "    ISPNF = @ISPNF,";
                selectQuery += "    MONTANTREMIS = @MONTANTREMIS ";

                selectQuery += " where (id = @id)";





                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);


                command.Parameters.AddWithValue("@id", encaissement.Id);
                command.Parameters.AddWithValue("@montant", encaissement.Montant);
                command.Parameters.AddWithValue("@moyenpaiement", encaissement.typeencaissement);
                command.Parameters.AddWithValue("@DATEENCAISSEMENT", encaissement.DateEncaissement);
                command.Parameters.AddWithValue("@NUMCHEQUE", encaissement.NumCheque);
                command.Parameters.AddWithValue("@ID_BANQUE_EMETRICE", encaissement.BanqueEmetrice == null ? DBNull.Value : (object)encaissement.BanqueEmetrice.Id);
                command.Parameters.AddWithValue("@REMISENBANQUE", encaissement.EstRemisEnBanque);
                command.Parameters.AddWithValue("@DATEREMISEENBANQUE", encaissement.DateRemiseEnBanque == null ? DBNull.Value : (object)encaissement.DateRemiseEnBanque.Value);
                command.Parameters.AddWithValue("@DATEVALEURBANQUE", encaissement.DateValeurBqe == null ? DBNull.Value : (object)encaissement.DateValeurBqe.Value);
                command.Parameters.AddWithValue("@ID_ENTITYJURIDIQUE", encaissement.EntiteJuridique == null ? DBNull.Value : (object)encaissement.EntiteJuridique.Id);
                command.Parameters.AddWithValue("@IDPAYEUR", encaissement.IdPayeur);
                command.Parameters.AddWithValue("@PAYEUR", encaissement.payeur);
                command.Parameters.AddWithValue("@MONTANT_EN_BANQUE", encaissement.MontantEnBanque);
                command.Parameters.AddWithValue("@ID_BANQUE_REMISE", encaissement.BanqueDeRemise == null ? DBNull.Value : (object)encaissement.BanqueDeRemise.Code);
                command.Parameters.AddWithValue("@DATEECHEANCE", encaissement.DateEcheance == null ? DBNull.Value : (object)encaissement.DateEcheance.Value);

                command.Parameters.AddWithValue("@ESPECESRECU", encaissement.EspecesRecu);
                command.Parameters.AddWithValue("@ESPECESRENDUS", encaissement.EspecesRendus);
                command.Parameters.AddWithValue("@ESPECESMISENCAISSE", encaissement.EspecesMisEncaisse);
                command.Parameters.AddWithValue("@ISPNF", encaissement.IsPnf);
                

                command.Parameters.AddWithValue("@ID_MUTUELLE", encaissement.Mutuelle == null ? DBNull.Value : (object)encaissement.Mutuelle.Id);

                command.Parameters.AddWithValue("@STATUS", encaissement.Status);
                command.Parameters.AddWithValue("@MONTANTREMIS", encaissement.MontantRemis);



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

        public static DataTable GetEncaissementsARemettreEnBanque()
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = "select distinct base_encaissement.id, ";
                selectQuery += "       base_encaissement.id_patient, ";
                selectQuery += "       base_encaissement.montant, ";
                selectQuery += "       base_encaissement.COMMENTAIRES, ";
                selectQuery += "       base_encaissement.NUMCHEQUE, ";
                selectQuery += "       base_encaissement.moyenpaiement,";
                selectQuery += "       base_encaissement.DATEENCAISSEMENT,";
                selectQuery += "       base_encaissement.ID_BANQUE,";
                selectQuery += "       base_encaissement.REMISENBANQUE,";
                selectQuery += "       base_encaissement.IDPAYEUR,";
                selectQuery += "       base_encaissement.ID_MUTUELLE,";
                selectQuery += "       base_encaissement.PAYEUR,";
                selectQuery += "       base_encaissement.DATEREMISEENBANQUE,";

                selectQuery += "       base_encaissement.STATUS,";
                selectQuery += "       base_encaissement.LIBELLE,";
                selectQuery += "       base_encaissement.MONTANTREMIS,";
                selectQuery += "       base_encaissement.DATEECHEANCE,";


                selectQuery += "       base_encaissement.ID_ENTITYJURIDIQUE";
                selectQuery += " from base_encaissement";
                selectQuery += " where base_encaissement.moyenpaiement in (0,1) and base_encaissement.dateremiseenbanque is null and (base_encaissement.montant>0)";
                //0 ou 1 = Espece ou Cheque

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

        public static DataRow GetEncaissement(int Id)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = "select distinct id, ";
                selectQuery += "       id_patient, ";
                selectQuery += "       MONTANT_ENCAISSE, ";
                selectQuery += "       ID_PAIEMENT_REEL";
                selectQuery += " from base_encaissement";
                selectQuery += " where id = @id";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@id", Id);

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

        public static List<int> GetListPatientsAffectedByPaiement(PaiementReel pmt)
        {

            JArray json = DAC.getMethodeJsonArray("/ListPatientsAffectedByPaiement/"+pmt.Id);
                List<int> lst = new List<int>();
                foreach (JValue dr in json)
                {
                    int id = Convert.ToInt32(dr.Value);
                    lst.Add(id);
                }


                return lst;

        }
        public static List<int> GetListPatientsAffectedByPaiementOLD(PaiementReel pmt)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = "select base_encaissement.id_patient IDPATIENT";
               
                selectQuery += " from base_paiementreel";
                selectQuery += " inner join base_encaissement on base_encaissement.id_paiement_reel = base_paiementreel.id";
                selectQuery += " where base_paiementreel.ID=@id";
                

                MySqlCommand command = new MySqlCommand(selectQuery, connection);


                command.Parameters.AddWithValue("@id", pmt.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];

                List<int> lst = new List<int>();
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr[0]);
                    lst.Add(id);
                }


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



        public static DataTable GetPaiementReelsRemisEnBanque(DateTime dte1, DateTime dte2, PaiementReel.TypeEncaissement type, EntiteJuridique entity)
        {
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
                selectQuery += "       base_paiementreel.PAYEUR,";
                selectQuery += "       base_paiementreel.INCN,";

                selectQuery += "       base_paiementreel.EspecesRecu,";
                selectQuery += "       base_paiementreel.EspecesRendus,";
                selectQuery += "       base_paiementreel.EspecesMisEncaisse,";
                selectQuery += "       TRIM(personne.PER_NOM)||' '||TRIM(personne.PER_PRENOM) PAYEUR,";
                selectQuery += "       base_paiementreel.DATEREMISEENBANQUE,";
                selectQuery += "       patient.id_personne IDPATIENT,";
                selectQuery += "       TRIM(patient.PER_NOM)||' '||TRIM(patient.PER_PRENOM) PATIENT,";

                selectQuery += "       base_paiementreel.STATUS,";
                selectQuery += "       base_paiementreel.ISPNF,";
                selectQuery += "       base_paiementreel.MONTANTREMIS,";
                selectQuery += "       base_paiementreel.DATEECHEANCE,";
                selectQuery += "       base_paiementreel.ID_BANQUE_REMISE,";
                selectQuery += "       base_paiementreel.MONTANT_EN_BANQUE,";


                selectQuery += "       base_paiementreel.ID_ENTITYJURIDIQUE";
                selectQuery += " from base_paiementreel";
                 selectQuery += " inner join base_encaissement on base_encaissement.id_paiement_reel = base_paiementreel.id";
                selectQuery += " inner join personne patient on base_encaissement.id_patient = patient.id_personne";
                selectQuery += " LEFT OUTER JOIN personne ON personne.ID_PERSONNE=base_paiementreel.IDPAYEUR";
                selectQuery += " where REMISENBANQUE = 1 AND DATEREMISEENBANQUE between @dte1 and @dte2 and (base_paiementreel.montant>0)";
                if (type != PaiementReel.TypeEncaissement.Tous)
                    selectQuery += " AND MOYENPAIEMENT = @MOYENPAIEMENT";

                if (entity != null)
                    selectQuery += " AND ID_ENTITYJURIDIQUE = @ID_ENTITYJURIDIQUE";

                selectQuery += " and ((INCN is null) or (INCN<>'True')) ";
                selectQuery += " order by base_paiementreel.DATEREMISEENBANQUE desc";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);


                command.Parameters.AddWithValue("@dte1", dte1.Date);
                command.Parameters.AddWithValue("@dte2", dte2.Date.AddDays(1));
                command.Parameters.AddWithValue("@MOYENPAIEMENT", (int)type);
                command.Parameters.AddWithValue("@ID_ENTITYJURIDIQUE", entity == null ? -1 : entity.Id);

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



        public static DataTable GetPaiementADiffererDuJour(PaiementReel.TypeEncaissement type)
        {
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
                selectQuery += "       base_paiementreel.PAYEUR,";
                selectQuery += "       base_paiementreel.INCN,";
                selectQuery += "       base_paiementreel.EspecesRecu,";
                selectQuery += "       base_paiementreel.EspecesRendus,";
                selectQuery += "       base_paiementreel.EspecesMisEncaisse,";
                selectQuery += "       TRIM(personne.PER_NOM)||' '||TRIM(personne.PER_PRENOM) PAYEUR,";
                selectQuery += "       patient.id_personne IDPATIENT,";
                selectQuery += "       TRIM(patient.PER_NOM)||' '||TRIM(patient.PER_PRENOM) PATIENT,";
                selectQuery += "       base_paiementreel.DATEREMISEENBANQUE,";
                selectQuery += "       base_paiementreel.STATUS,";
                selectQuery += "       base_paiementreel.ISPNF,";
                selectQuery += "       base_paiementreel.MONTANTREMIS,";
                selectQuery += "       base_paiementreel.DATEECHEANCE,";
                selectQuery += "       base_paiementreel.ID_BANQUE_REMISE,";
                selectQuery += "       base_paiementreel.MONTANT_EN_BANQUE,";
                selectQuery += "       base_paiementreel.ID_ENTITYJURIDIQUE";

                selectQuery += " from base_paiementreel";
                selectQuery += " inner join base_encaissement on base_encaissement.id_paiement_reel = base_paiementreel.id";
                selectQuery += " inner join personne patient on base_encaissement.id_patient = patient.id_personne";
                selectQuery += " LEFT OUTER JOIN personne ON personne.ID_PERSONNE=base_paiementreel.IDPAYEUR";



                selectQuery += " left join";
                selectQuery += " (";

                selectQuery += " select bas_lnk_paiement_ctrl.ID_PAIEMENT ID_PAIEMENT";
                selectQuery += " from bas_lnk_paiement_ctrl";
                selectQuery += " inner join bas_controlsfinance on bas_controlsfinance.ID = bas_lnk_paiement_ctrl.id_control";
                selectQuery += " and bas_lnk_paiement_ctrl.latest_flag='Y'";
                selectQuery += " and bas_lnk_paiement_ctrl.etat<>0"; 
                selectQuery += " ) lstrglmntctrl on lstrglmntctrl.ID_PAIEMENT = base_paiementreel.id";
                selectQuery += " where lstrglmntctrl.ID_PAIEMENT is null";


                selectQuery += " and DATEREMISEENBANQUE is NULL AND cast(DATEECHEANCE as date) > cast(DATEENCAISSEMENT as date)";
                selectQuery += " and cast(DATEECHEANCE as date) > @dte ";
                
                if (type != PaiementReel.TypeEncaissement.Tous)
                    selectQuery += " AND MOYENPAIEMENT = @MOYENPAIEMENT and (base_paiementreel.montant>0)";

                selectQuery += " and ((INCN is null) or (INCN<>'True')) ";
                selectQuery += " order by base_paiementreel.DATEECHEANCE desc";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);


                command.Parameters.AddWithValue("@dte", DateTime.Now.Date);
                command.Parameters.AddWithValue("@MOYENPAIEMENT", (int)type);
                
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


       

        public static DataTable GetPaiementReelsDuJour( PaiementReel.TypeEncaissement type,DateTime dte1,DateTime dte2,string codeControlToFiltre)
        {
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
                selectQuery += "       base_paiementreel.EspecesRecu,";
                selectQuery += "       base_paiementreel.EspecesRendus,";
                selectQuery += "       base_paiementreel.EspecesMisEncaisse,";              

                selectQuery += "       CONCAT(TRIM(personne.PER_NOM),' ',TRIM(personne.PER_PRENOM)) PAYEUR,";
                selectQuery += "       patient.id_personne IDPATIENT,";
                selectQuery += "       CONCAT(TRIM(patient.PER_NOM),' ',TRIM(patient.PER_PRENOM)) PATIENT,";
                selectQuery += "       base_paiementreel.DATEREMISEENBANQUE,";
                selectQuery += "       base_paiementreel.STATUS,";
                selectQuery += "       base_paiementreel.ISPNF,";
                selectQuery += "       base_paiementreel.MONTANTREMIS,";
                selectQuery += "       base_paiementreel.DATEECHEANCE,";
                selectQuery += "       base_paiementreel.ID_BANQUE_REMISE,";
                selectQuery += "       base_paiementreel.MONTANT_EN_BANQUE,";


                selectQuery += "       base_paiementreel.ID_ENTITYJURIDIQUE";
                selectQuery += " from base_paiementreel";
                selectQuery += " inner join base_encaissement on base_encaissement.id_paiement_reel = base_paiementreel.id";
                selectQuery += " inner join personne patient on base_encaissement.id_patient = patient.id_personne";
                selectQuery += " LEFT OUTER JOIN personne ON personne.ID_PERSONNE=base_paiementreel.IDPAYEUR";
                


                selectQuery += " left join";
                selectQuery += " (";

//--CHQDUJOUR
//--CHQDDUJOUR
//--CBDUJOUR
//--ESPDUJOUR
//--PREDUJOUR

                selectQuery += " select bas_lnk_paiement_ctrl.ID_PAIEMENT ID_PAIEMENT";
                selectQuery += " from bas_lnk_paiement_ctrl";
                selectQuery += " inner join bas_controlsfinance on bas_controlsfinance.ID = bas_lnk_paiement_ctrl.id_control and bas_controlsfinance.CODECONTROL='" + codeControlToFiltre + "'";
                selectQuery += " and bas_lnk_paiement_ctrl.latest_flag='Y'";
                selectQuery += " and bas_lnk_paiement_ctrl.etat<>0";
                selectQuery += " ) lstrglmntctrl on lstrglmntctrl.ID_PAIEMENT = base_paiementreel.id";
                selectQuery += " where lstrglmntctrl.ID_PAIEMENT is null";

                selectQuery += " and DATEREMISEENBANQUE is NULL AND ((cast(DATEECHEANCE as date) between @dte1 and @dte2) or (DATEECHEANCE is NULL))";
                if (type != PaiementReel.TypeEncaissement.Tous)
                    selectQuery += " AND MOYENPAIEMENT = @MOYENPAIEMENT and (base_paiementreel.montant>0)";

                selectQuery += " and ((INCN is null) or (INCN<>'True')) ";
                selectQuery += " order by base_paiementreel.DATEECHEANCE desc";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);


                command.Parameters.AddWithValue("@dte1", dte1);
                command.Parameters.AddWithValue("@dte2", dte2);
                command.Parameters.AddWithValue("@MOYENPAIEMENT", (int)type);
                
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




        public static DataTable GetPrelevementGroupeAPreleverNonControler()
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = @" select personne.id_personne,
                                        base_traitement.Libelle traitement,
                                        base_traitement.id idtraitement,
                                        trim(personne.per_nom)||' '||trim(personne.per_prenom) as NomPatient,
                                        GROUP_CONCAT(cast(base_echeance.montant as DECIMAL(5,2))) as montants,
                                        GROUP_CONCAT(extract(day from base_echeance.dteecheance)) as days,
                                        GROUP_CONCAT(base_echeance.id)  as ids,
                                        min(base_echeance.dteecheance) datepremierprelevement
                                        from base_traitement
                                        inner join personne on personne.id_personne = base_traitement.id_patient
                                        inner join base_echeance on base_echeance.id_traitement=base_traitement.id  and base_echeance.parprelevement = 'True' and (base_echeance.id_encaissement<0 or base_echeance.id_encaissement is null ) 
                                        left join bas_lnk_paiement_ctrl on bas_lnk_paiement_ctrl.ID_ECHEANCE = base_echeance.id 
                                        where bas_lnk_paiement_ctrl.id_echeance is null 
                                        group by 1,2,3,4";

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


        public static DataTable GetPrelevementGroupeAPrelever()
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = @" select personne.id_personne,
                                        base_traitement.Libelle traitement,
                                        base_traitement.id idtraitement,
                                        trim(personne.per_nom)||' '||trim(personne.per_prenom) as NomPatient,
                                        list(cast(base_echeance.montant as numeric(5,2))) as montants,
                                        list(extract(day from base_echeance.dteecheance)) as days,
                                        list(base_echeance.id)  as ids,
                                        min(base_echeance.dteecheance) DatePremierPrelevement
                                        from base_traitement
                                        inner join personne on personne.id_personne = base_traitement.id_patient
                                        inner join base_echeance on base_echeance.id_traitement=base_traitement.id  and base_echeance.parprelevement = 'True' and base_echeance.id_encaissement<0   
                                        inner join bas_lnk_paiement_ctrl on bas_lnk_paiement_ctrl.ID_ECHEANCE = base_echeance.id
                                        group by 1,2,3,4";

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



        public static DataTable GetPaiementFrac(string codeControlToFiltre,DateTime dt1, DateTime dt2)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {/*
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
                selectQuery += "       base_paiementreel.PAYEUR,";
                selectQuery += "       base_paiementreel.EspecesRecu,";
                selectQuery += "       base_paiementreel.EspecesRendus,";
                selectQuery += "       base_paiementreel.EspecesMisEncaisse,";

                selectQuery += "       TRIM(personne.PER_NOM)||' '||TRIM(personne.PER_PRENOM) PAYEUR,";
                selectQuery += "       patient.id_personne IDPATIENT,";
                selectQuery += "       TRIM(patient.PER_NOM)||' '||TRIM(patient.PER_PRENOM) PATIENT,";
                selectQuery += "       base_paiementreel.DATEREMISEENBANQUE,";
                selectQuery += "       base_paiementreel.STATUS,";
                selectQuery += "       base_paiementreel.IsPnf,";
                selectQuery += "       base_paiementreel.MONTANTREMIS,";
                selectQuery += "       base_paiementreel.DATEECHEANCE,";
                selectQuery += "       base_paiementreel.ID_BANQUE_REMISE,";
                selectQuery += "       base_paiementreel.MONTANT_EN_BANQUE,";


                selectQuery += "       base_paiementreel.ID_ENTITYJURIDIQUE";
                selectQuery += " from base_paiementreel";
                selectQuery += " inner join base_encaissement on base_encaissement.id_paiement_reel = base_paiementreel.id";
                selectQuery += " inner join personne patient on base_encaissement.id_patient = patient.id_personne";
                selectQuery += " LEFT OUTER JOIN personne ON personne.ID_PERSONNE=base_paiementreel.IDPAYEUR";



                selectQuery += " left join";
                selectQuery += " (";

                //--CHQDUJOUR
                //--CHQDDUJOUR
                //--CBDUJOUR
                //--ESPDUJOUR

                selectQuery += " select bas_lnk_paiement_ctrl.ID_PAIEMENT ID_PAIEMENT";
                selectQuery += " from bas_lnk_paiement_ctrl";
                selectQuery += " inner join bas_controlsfinance on bas_controlsfinance.ID = bas_lnk_paiement_ctrl.id_control and bas_controlsfinance.CODECONTROL='" + codeControlToFiltre + "'";
                selectQuery += " ) lstrglmntctrl on lstrglmntctrl.ID_PAIEMENT = base_paiementreel.id";
                selectQuery += " where lstrglmntctrl.ID_PAIEMENT is null";

                selectQuery += " and cast(DATEECHEANCE as date) between @dte1 and @dte2";
                selectQuery += " AND (MOYENPAIEMENT = 4 or MOYENPAIEMENT = 8) and (base_paiementreel.montant>0)"; //Optalion ou Pnf

                selectQuery += " order by base_paiementreel.DATEECHEANCE desc";
                */

                string selectQuery = @"select lstrglmntctrl.*,base_paiementreel.Id,
                                        TRIM(patient.PER_NOM)||' '||TRIM(patient.PER_PRENOM) PATIENT,
                                        patient.id_personne IDPATIENT,
                                        base_paiementreel.payeur,
                                        base_paiementreel.dateencaissement,
                                        bas_pnf_check.montant_total,
                                        base_paiementreel.ID_ENTITYJURIDIQUE,
                                        bas_pnf_check.id_part_patient,
                                        bas_pnf_check.id_part_banque,
                                        bas_pnf_check.libelle,
                                        bas_pnf_check.TYPE,
                                        bas_pnf_check.DATE

                                        from bas_pnf_check
                                        inner join base_echeance on  bas_pnf_check.id_part_patient = base_echeance.id
                                        inner join personne patient on patient.id_personne = base_echeance.id_patient
                                        inner join base_encaissement on  base_encaissement.id = base_echeance.id_encaissement
                                        inner join base_paiementreel on  base_paiementreel.id = base_encaissement.id_paiement_reel

                                        left join (
                                            select bas_lnk_paiement_ctrl.id_echeance id_echeance,bas_lnk_paiement_ctrl.id_paiement id_paiement
                                          from bas_lnk_paiement_ctrl
                                          inner join bas_controlsfinance on bas_controlsfinance.ID = bas_lnk_paiement_ctrl.id_control and bas_controlsfinance.CODECONTROL='" + codeControlToFiltre+"'";
                            selectQuery += " and bas_lnk_paiement_ctrl.latest_flag='Y'";
                            selectQuery += " and bas_lnk_paiement_ctrl.etat<>0";
                             selectQuery += @"
                                        ) lstrglmntctrl on lstrglmntctrl.id_echeance = base_echeance.id
                                        where lstrglmntctrl.id_echeance is null and lstrglmntctrl.id_paiement is null
                                        order by base_paiementreel.DATEECHEANCE desc";
                MySqlCommand command = new MySqlCommand(selectQuery, connection);


                command.Parameters.AddWithValue("@dte1", dt1.Date);
                command.Parameters.AddWithValue("@dte2", dt2.Date);

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



        public static DataTable GetCBDuJour(PaiementReel.TypeEncaissement type, DateTime dte1, DateTime dte2, string codeControlToFiltre)
        {
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
                selectQuery += "       base_paiementreel.INCN,";
                selectQuery += "       base_paiementreel.ID_MUTUELLE,";
                selectQuery += "       base_paiementreel.REMISENBANQUE,";
                selectQuery += "       base_paiementreel.IDPAYEUR,";
                selectQuery += "       base_paiementreel.PAYEUR,";
                selectQuery += "       base_paiementreel.EspecesRecu,";
                selectQuery += "       base_paiementreel.EspecesRendus,";
                selectQuery += "       base_paiementreel.EspecesMisEncaisse,";

                selectQuery += "       TRIM(personne.PER_NOM)||' '||TRIM(personne.PER_PRENOM) PAYEUR,";
                selectQuery += "       patient.id_personne IDPATIENT,";
                selectQuery += "       TRIM(patient.PER_NOM)||' '||TRIM(patient.PER_PRENOM) PATIENT,";
                selectQuery += "       base_paiementreel.DATEREMISEENBANQUE,";
                selectQuery += "       base_paiementreel.STATUS,";
                selectQuery += "       base_paiementreel.ISPNF,";
                selectQuery += "       base_paiementreel.MONTANTREMIS,";
                selectQuery += "       base_paiementreel.DATEECHEANCE,";
                selectQuery += "       base_paiementreel.ID_BANQUE_REMISE,";
                selectQuery += "       base_paiementreel.MONTANT_EN_BANQUE,";


                selectQuery += "       base_paiementreel.ID_ENTITYJURIDIQUE";
                selectQuery += " from base_paiementreel";
                selectQuery += " inner join base_encaissement on base_encaissement.id_paiement_reel = base_paiementreel.id";
                selectQuery += " inner join personne patient on base_encaissement.id_patient = patient.id_personne";
                selectQuery += " left outer join personne on personne.id_personne=base_paiementreel.idpayeur";



                selectQuery += " left join";
                selectQuery += " (";

                //--CHQDUJOUR
                //--CHQDDUJOUR
                //--CBDUJOUR
                //--ESPDUJOUR

                selectQuery += " select bas_lnk_paiement_ctrl.ID_PAIEMENT ID_PAIEMENT";
                selectQuery += " from bas_lnk_paiement_ctrl";
                selectQuery += " inner join bas_controlsfinance on bas_controlsfinance.ID = bas_lnk_paiement_ctrl.id_control and bas_controlsfinance.CODECONTROL='" + codeControlToFiltre + "'";
                selectQuery += " and bas_lnk_paiement_ctrl.latest_flag='Y'";
                selectQuery += " and bas_lnk_paiement_ctrl.etat<>0";
                selectQuery += " ) lstrglmntctrl on lstrglmntctrl.ID_PAIEMENT = base_paiementreel.id";
                selectQuery += " where lstrglmntctrl.ID_PAIEMENT is null";

                selectQuery += " and cast(DATEECHEANCE as date) between @dte1 and @dte2";
                if (type != PaiementReel.TypeEncaissement.Tous)
                    selectQuery += " AND (MOYENPAIEMENT = 3 or MOYENPAIEMENT = 9) and (base_paiementreel.montant>0)"; //CB ou AMEX

                selectQuery += " and ((INCN is null) or (INCN<>'True')) ";
                selectQuery += " order by base_paiementreel.DATEECHEANCE desc";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);


                command.Parameters.AddWithValue("@dte1", dte1.Date);
                command.Parameters.AddWithValue("@dte2", dte2.Date);
                command.Parameters.AddWithValue("@MOYENPAIEMENT", (int)type);

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


        public static DataTable GetPaiementReelsARemettreEnBanque(DateTime dte1, DateTime dte2, PaiementReel.TypeEncaissement type, EntiteJuridique entity)
        {
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
                selectQuery += "       base_paiementreel.PAYEUR,";
                selectQuery += "       base_paiementreel.INCN,";

                selectQuery += "       base_paiementreel.EspecesRecu,";
                selectQuery += "       base_paiementreel.EspecesRendus,";
                selectQuery += "       base_paiementreel.EspecesMisEncaisse,";

                selectQuery += "       TRIM(personne.PER_NOM)||' '||TRIM(personne.PER_PRENOM) PAYEUR,";
                selectQuery += "       patient.id_personne IDPATIENT,";
                selectQuery += "       TRIM(patient.PER_NOM)||' '||TRIM(patient.PER_PRENOM) PATIENT,";
                selectQuery += "       base_paiementreel.DATEREMISEENBANQUE,";
                selectQuery += "       base_paiementreel.STATUS,";
                selectQuery += "       base_paiementreel.ISPNF,";
                selectQuery += "       base_paiementreel.MONTANTREMIS,";
                selectQuery += "       base_paiementreel.DATEECHEANCE,";
                selectQuery += "       base_paiementreel.ID_BANQUE_REMISE,";
                selectQuery += "       base_paiementreel.MONTANT_EN_BANQUE,";


                selectQuery += "       base_paiementreel.ID_ENTITYJURIDIQUE";
                selectQuery += " from base_paiementreel";
                selectQuery += " left outer join BAS_PNF_CHECK on BAS_PNF_CHECK.ID_PART_PATIENT = base_paiementreel.id";
                selectQuery += " inner join base_encaissement on base_encaissement.id_paiement_reel = base_paiementreel.id";
                selectQuery += " inner join personne patient on base_encaissement.id_patient = patient.id_personne";
                selectQuery += " LEFT OUTER JOIN personne ON personne.ID_PERSONNE=base_paiementreel.IDPAYEUR";
                selectQuery += " where DATEREMISEENBANQUE is NULL AND DATEECHEANCE between @dte1 and @dte2";
                if (type != PaiementReel.TypeEncaissement.Tous)
                    selectQuery += " AND MOYENPAIEMENT = @MOYENPAIEMENT and (base_paiementreel.montant>0)";


                if (entity != null)
                    selectQuery += " AND ID_ENTITYJURIDIQUE = @ID_ENTITYJURIDIQUE";

                selectQuery += " and ((INCN is null) or (INCN<>'True')) ";
                selectQuery += " order by base_paiementreel.DATEECHEANCE desc";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);


                command.Parameters.AddWithValue("@dte1", dte1.Date);
                command.Parameters.AddWithValue("@dte2", dte2.Date.AddDays(1));
                command.Parameters.AddWithValue("@MOYENPAIEMENT", (int)type);
                command.Parameters.AddWithValue("@ID_ENTITYJURIDIQUE", entity == null ? -1 : entity.Id);

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
        
        public static DataTable GetPaiementsReels(DateTime dateDebut, DateTime datefin, EntiteJuridique entite, PaiementReel.TypeEncaissement typepaiement, Utilisateur praticien)
        {
            return GetPaiementsReels(dateDebut, datefin, entite, typepaiement, false, praticien);
        }

        public static DataTable GetPaiementsReels(DateTime dateDebut, DateTime datefin, EntiteJuridique entite, PaiementReel.TypeEncaissement typepaiement, bool IncludeCN, Utilisateur praticien)
        {
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
                selectQuery += "       base_paiementreel.DATEREMISEENBANQUE,";
                // selectQuery += "       patient.id_personne IDPATIENT,";
                
                selectQuery += "       base_paiementreel.EspecesRecu,";
                selectQuery += "       base_paiementreel.EspecesRendus,";
                selectQuery += "       base_paiementreel.EspecesMisEncaisse,";
                selectQuery += "       base_paiementreel.STATUS,";
                selectQuery += "       base_paiementreel.ISPNF,";
                selectQuery += "       base_paiementreel.MONTANTREMIS,";
                selectQuery += "       base_paiementreel.MONTANT_EN_BANQUE,";
                selectQuery += "       base_paiementreel.DATEECHEANCE,";
                selectQuery += "       base_paiementreel.ID_BANQUE_REMISE,";

                selectQuery += "       base_paiementreel.ID_ENTITYJURIDIQUE,";
                selectQuery += @"       (
                            select LIST(TRIM(PER_NOM)||' '||TRIM(PER_PRENOM)) as PATIENT 
                            from personne
                            inner join base_encaissement on personne.id_personne=base_encaissement.id_patient and base_encaissement.id_paiement_reel = base_paiementreel.id
                            ) PATIENTS,";
                selectQuery += @"       (
                            select LIST(ID_PERSONNE) as IDPATIENT 
                            from personne
                            inner join base_encaissement on personne.id_personne=base_encaissement.id_patient and base_encaissement.id_paiement_reel = base_paiementreel.id
                            ) IDPATIENTS";
                selectQuery += " from base_paiementreel";
                //selectQuery += " inner join base_encaissement on base_encaissement.id_paiement_reel = base_paiementreel.id";
                //selectQuery += " inner join personne patient on base_encaissement.id_patient = patient.id_personne";
                selectQuery += " where base_paiementreel.DATEENCAISSEMENT between @dt1 and @dt2";
                
                if (entite != null)
                    selectQuery += " and base_paiementreel.ID_ENTITYJURIDIQUE = @identite";

                if (typepaiement != PaiementReel.TypeEncaissement.Tous)
                    selectQuery += " and base_paiementreel.moyenpaiement = @typepaiement";
                if (praticien != null)
                    selectQuery += " and ID_PRATICIEN = @idpraticien ";
                if (!IncludeCN)
                    selectQuery += " and ((INCN is null) or (INCN<>'True')) ";

                selectQuery += " order by base_paiementreel.DATEENCAISSEMENT desc";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@dt1", dateDebut.Date);
                command.Parameters.AddWithValue("@dt2", datefin.Date.AddDays(1));

                if (entite != null)
                    command.Parameters.AddWithValue("@identite", entite.Id);
               
                if (praticien != null)
                    command.Parameters.AddWithValue("@idpraticien", praticien.Id );
               
                if (typepaiement!=PaiementReel.TypeEncaissement.Tous)
                    command.Parameters.AddWithValue("@typepaiement", typepaiement);
                
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


        public static DataTable GetPaiementsReels(BordereauFinance bf)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = "select base_paiementreel.id, ";
                selectQuery += "       base_paiementreel.montant, ";
                selectQuery += "       base_paiementreel.NUMCHEQUE, ";
                selectQuery += "       base_paiementreel.INCN,";
                selectQuery += "       base_paiementreel.moyenpaiement,";
                selectQuery += "       base_paiementreel.DATEENCAISSEMENT,";
                selectQuery += "       base_paiementreel.DATEVALEURBANQUE,";
                selectQuery += "       base_paiementreel.ID_BANQUE_EMETRICE,";
                selectQuery += "       base_paiementreel.ID_MUTUELLE,";
                selectQuery += "       base_paiementreel.REMISENBANQUE,";
                selectQuery += "       base_paiementreel.IDPAYEUR,";
                selectQuery += "       base_paiementreel.PAYEUR,";
                selectQuery += "       base_paiementreel.DATEREMISEENBANQUE,";

                selectQuery += "       base_paiementreel.EspecesRecu,";
                selectQuery += "       base_paiementreel.EspecesRendus,";
                selectQuery += "       base_paiementreel.EspecesMisEncaisse,";
                selectQuery += "       base_paiementreel.STATUS,";
                selectQuery += "       base_paiementreel.ISPNF,";
                selectQuery += "       base_paiementreel.MONTANTREMIS,";
                selectQuery += "       base_paiementreel.MONTANT_EN_BANQUE,";
                selectQuery += "       base_paiementreel.DATEECHEANCE,";
                selectQuery += "       base_paiementreel.ID_BANQUE_REMISE,";

                selectQuery += "       base_paiementreel.ID_ENTITYJURIDIQUE";
                selectQuery += " from base_paiementreel";
                selectQuery += " inner join BAS_LNK_BORDEREAU_PAIEMENT on BAS_LNK_BORDEREAU_PAIEMENT.ID_PAIEMENT = base_paiementreel.ID and  BAS_LNK_BORDEREAU_PAIEMENT.ID_BORDEREAU = @IDBORDEREAU";
                selectQuery += " and (base_paiementreel.montant>0)";

                selectQuery += " and ((INCN is null) or (INCN<>'True')) ";
                selectQuery += " order by base_paiementreel.DATEENCAISSEMENT desc";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@IDBORDEREAU", bf.Id);

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


        public static DataTable GetPaiementsReels(int IdPatient,bool includeCN)
        {
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
                selectQuery += "       base_paiementreel.PAYEUR,";
                selectQuery += "       base_paiementreel.INCN,";
                selectQuery += "       base_paiementreel.DATEREMISEENBANQUE,";

                selectQuery += "       base_paiementreel.EspecesRecu,";
                selectQuery += "       base_paiementreel.EspecesRendus,";
                selectQuery += "       base_paiementreel.EspecesMisEncaisse,";
                selectQuery += "       base_paiementreel.STATUS,";
                selectQuery += "       base_paiementreel.ISPNF,";
                selectQuery += "       base_paiementreel.MONTANTREMIS,";
                selectQuery += "       base_paiementreel.MONTANT_EN_BANQUE,";
                selectQuery += "       base_paiementreel.DATEECHEANCE,";
                selectQuery += "       base_paiementreel.ID_BANQUE_REMISE,";

                selectQuery += "       base_paiementreel.ID_ENTITYJURIDIQUE";
                selectQuery += " from base_paiementreel";
                selectQuery += " inner join base_encaissement on base_encaissement.ID_PAIEMENT_REEL = base_paiementreel.ID and  base_encaissement.id_patient = @id_patient";
                if (!includeCN) 
                    selectQuery += " and ((INCN is null) or (INCN<>'True')) ";
                selectQuery += " order by base_paiementreel.DATEENCAISSEMENT desc";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@id_patient", IdPatient);

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

        public static DataRow GetPaiementsReel(int Id)
        {
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
                selectQuery += "       base_paiementreel.PAYEUR,";
                selectQuery += "       base_paiementreel.INCN,";
                selectQuery += "       base_paiementreel.DATEREMISEENBANQUE,";
                selectQuery += "       base_paiementreel.EspecesRecu,";
                selectQuery += "       base_paiementreel.EspecesRendus,";
                selectQuery += "       base_paiementreel.EspecesMisEncaisse,";

                selectQuery += "       base_paiementreel.STATUS,";
                selectQuery += "       base_paiementreel.ISPNF,";
                selectQuery += "       base_paiementreel.MONTANTREMIS,";
                selectQuery += "       base_paiementreel.DATEECHEANCE,";
                selectQuery += "       base_paiementreel.ID_BANQUE_REMISE,";
                selectQuery += "       base_paiementreel.MONTANT_EN_BANQUE,";


                selectQuery += "       base_paiementreel.ID_ENTITYJURIDIQUE";
                selectQuery += " from base_paiementreel";
                selectQuery += " where id = @id";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@id", Id);

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



        public static double GetTotalEncaissements(basePatient patient)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = "select sum(MONTANT_ENCAISSE) ";
                selectQuery += " from base_encaissement";
                selectQuery += " where base_encaissement.id_patient = @id_patient";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@id_patient", patient.Id);

                object obj = command.ExecuteScalar();

                if (obj is DBNull) return 0;

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


        public static DataTable FindCheques(string numcheque)
        {
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
                selectQuery += "       base_paiementreel.PAYEUR,";
                selectQuery += "       base_paiementreel.INCN,";

                selectQuery += "       base_paiementreel.EspecesRecu,";
                selectQuery += "       base_paiementreel.EspecesRendus,";
                selectQuery += "       base_paiementreel.EspecesMisEncaisse,";
                selectQuery += "       base_paiementreel.DATEREMISEENBANQUE,";
                selectQuery += "       base_encaissement.ID_PATIENT IDPATIENT,";
                selectQuery += "       trim(patient.per_nom)||' '||trim(patient.per_prenom) patient,";
                
                selectQuery += "       base_paiementreel.STATUS,";
                selectQuery += "       base_paiementreel.ISPNF,";
                selectQuery += "       base_paiementreel.MONTANTREMIS,";
                selectQuery += "       base_paiementreel.DATEECHEANCE,";
                selectQuery += "       base_paiementreel.ID_BANQUE_REMISE,";
                selectQuery += "       base_paiementreel.MONTANT_EN_BANQUE,";


                selectQuery += "       base_paiementreel.ID_ENTITYJURIDIQUE";
                selectQuery += " from base_paiementreel";
               selectQuery += " inner join base_encaissement on base_encaissement.ID_PAIEMENT_REEL = base_paiementreel.id";
               selectQuery += " inner join personne patient on patient.ID_personne = base_encaissement.ID_patient";
                
                selectQuery += " where base_paiementreel.NUMCHEQUE = '" + numcheque + "'";
                selectQuery += " and ((INCN is null) or (INCN<>'True')) ";
                selectQuery += " order by base_paiementreel.DATEENCAISSEMENT desc";

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

        


        public static DataTable FindEncaissements(string numcheque)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = "select distinct base_encaissement.id, ";
                selectQuery += "       id_patient, ";
                selectQuery += "       MONTANT_ENCAISSE, ";
                selectQuery += "       ID_PAIEMENT_REEL";
                selectQuery += " from base_encaissement";
                selectQuery += " inner join base_paiementreel on base_paiementreel.ID=base_encaissement.ID_PAIEMENT_REEL";

                selectQuery += " where base_paiementreel.NUMCHEQUE LIKE '%" + numcheque + "%'";
                selectQuery += " order by base_paiementreel.DATEENCAISSEMENT desc";

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


        public static DataTable GetEncaissements(int Idpat, bool IncludeCN)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select distinct base_encaissement.id, base_paiementreel.DATEENCAISSEMENT ,";
                selectQuery += "       id_patient, ";
                selectQuery += "       MONTANT_ENCAISSE, ";
                selectQuery += "       ID_PAIEMENT_REEL";
                selectQuery += " from base_encaissement";
                selectQuery += " inner join base_paiementreel on base_paiementreel.ID=base_encaissement.ID_PAIEMENT_REEL";

                selectQuery += " where base_encaissement.id_patient = @id_patient";
                if (!IncludeCN)
                    selectQuery += " and ((INCN is null) or (INCN<>'True')) "; 
                selectQuery += " order by base_paiementreel.DATEENCAISSEMENT desc";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id_patient", Idpat);

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

        public static DataTable GetEncaissements(bool IncludeRemisEnBanque, DateTime dte1, DateTime dte2, PaiementReel.TypeEncaissement tpeencaissement, EntiteJuridique entity,bool IncludeCN)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select distinct base_encaissement.id, ";
                selectQuery += "       id_patient, ";
                selectQuery += "       MONTANT_ENCAISSE, ";
                selectQuery += "       ID_PAIEMENT_REEL";
                selectQuery += " from base_encaissement";
                selectQuery += " inner join base_paiementreel on base_paiementreel.ID=base_encaissement.ID_PAIEMENT_REEL";


               
                selectQuery += " where DATEENCAISSEMENT between @dte1 and @dte2";
                if (!IncludeCN)
                    selectQuery += " and ((INCN is null) or (INCN<>'True')) "; 

                if (!IncludeRemisEnBanque)
                    selectQuery += " AND REMISENBANQUE = 0";

                if (tpeencaissement != PaiementReel.TypeEncaissement.Tous)
                    selectQuery += " AND MOYENPAIEMENT = @MOYENPAIEMENT";

                if (entity != null)
                    selectQuery += " AND ID_ENTITYJURIDIQUE = @ID_ENTITYJURIDIQUE";



                selectQuery += " order by base_paiementreel.DATEENCAISSEMENT desc";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);


                command.Parameters.AddWithValue("@dte1", dte1);
                command.Parameters.AddWithValue("@dte2", dte2);
                command.Parameters.AddWithValue("@MOYENPAIEMENT", (int)tpeencaissement);
                command.Parameters.AddWithValue("@ID_ENTITYJURIDIQUE", entity == null ? -1 : entity.Id);

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
}
