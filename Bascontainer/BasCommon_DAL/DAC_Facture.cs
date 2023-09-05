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
        public static void AddFacture(Facture facture, Boolean vAcquitter)
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
                    string selectQueryId ;
                    if (vAcquitter)
                        selectQueryId = " select MAX(ID)+1 as NEWID from  base_facture ";
                    else
                        selectQueryId = "select MAX(ID)+1 as NEWID from base_facture_a_acquitter";
                    MySqlCommand commandid = new MySqlCommand(selectQueryId, connection, transaction);
                    object obj = commandid.ExecuteScalar();
                    if (obj == DBNull.Value)
                        facture.id = 1;
                    else
                        facture.id = Convert.ToInt32(obj);
                    string selectQuery = "insert into ";
                    if (vAcquitter )
                        selectQuery += " base_facture ";
                    else
                        selectQuery += "  base_facture_a_acquitter ";
                    selectQuery += " (id, dateFacture, id_patient,NOMBRE_POINTS, MONTANT, MONTANT_LABO, MONTANT_STER, MONTANT_TOTAL,POINTS,MONTANT_PAYE,MONTANT_RESTANT,DATEDEBUTFACTURE, DATEFINFACTURE,MONTANT_ACHATS)";
                    selectQuery += " values (@id, @dateFacture,@id_patient, @NOMBRE_POINTS, @MONTANT, @MONTANT_LABO, @MONTANT_STER, @MONTANT_TOTAL,@POINTS,@MONTANT_PAYE,@MONTANT_RESTANT,@DATEDEBUTFACTURE, @DATEFINFACTURE,@MONTANT_ACHATS)";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@id", facture.id);
                    command.Parameters.AddWithValue("@dateFacture", facture.DateFacture);
                    command.Parameters.AddWithValue("@id_patient", facture.patientFacture.Id );
                    command.Parameters.AddWithValue("@NOMBRE_POINTS", facture.NombrePoints);
                    command.Parameters.AddWithValue("@MONTANT", facture.Montant );
                    command.Parameters.AddWithValue("@MONTANT_LABO", facture.MontantLabo);
                    command.Parameters.AddWithValue("@MONTANT_STER", facture.MontantSterilisation);
                    command.Parameters.AddWithValue("@MONTANT_ACHATS", facture.MontantAchats);
                    command.Parameters.AddWithValue("@MONTANT_TOTAL", facture.MontantTotal );
                    command.Parameters.AddWithValue("@POINTS", facture.Points);
                    command.Parameters.AddWithValue("@MONTANT_PAYE", facture.MontantPaye);
                    command.Parameters.AddWithValue("@MONTANT_RESTANT", facture.MontantRestant);
                    command.Parameters.AddWithValue("@DATEDEBUTFACTURE", facture.DateDebutFacture);
                    command.Parameters.AddWithValue("@DATEFINFACTURE", facture.DateFinFacture);
                    

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



        public static void AddFactureLigne(FactureLigne factureLigne, Boolean vAcquitter)
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
                    //string selectQueryId = "select MAX(ID_LIGNE)+1 as NEWID from base_facture_ligne";

                    //MySqlCommand commandid = new MySqlCommand(selectQueryId, connection, transaction);
                    //object obj = commandid.ExecuteScalar();
                    //if (obj == DBNull.Value)
                    //    factureLigne.id_Ligne = 1;
                    //else
                    //    factureLigne.id_Ligne = Convert.ToInt32(obj);
            

                    string selectQuery = "insert into ";
                      if (vAcquitter )
                          selectQuery += " base_facture_ligne ";
                    else
                          selectQuery += " base_facture_l_a_acquitter ";
                    selectQuery +=   "  ( ID_FACTURE, ID_LIGNE, ID_ECHEANCE, MONTANTLIGNEFACTURE, ";
                    selectQuery += " NOMENCLATURE, QUANTITE, LIBELLE, COTATION, NOMBRE_POINTS,RABAIS,montantMutuelle,montantSS,montantPatient)";
                    selectQuery +="  values (@ID_FACTURE, @ID_LIGNE, @ID_ECHEANCE, @MONTANTLIGNEFACTURE, ";
                    selectQuery += " @NOMENCLATURE, @QUANTITE, @LIBELLE, @COTATION, @NOMBRE_POINTS,@RABAIS,@montantMutuelle,@montantSS,@montantPatient)";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@ID_FACTURE", factureLigne.id_Facture);
                    command.Parameters.AddWithValue("@ID_LIGNE", factureLigne.id_Ligne);
                    command.Parameters.AddWithValue("@ID_ECHEANCE", factureLigne.id_Echeance);
                    //command.Parameters.AddWithValue("@DATEEXECUTION", factureLigne.date_Execution);
                    command.Parameters.AddWithValue("@MONTANTLIGNEFACTURE", factureLigne.montantLigneFacture);

                    command.Parameters.AddWithValue("@NOMENCLATURE", factureLigne.NomenclatureActe );
                    command.Parameters.AddWithValue("@QUANTITE", factureLigne.QuantiteActe);
                    command.Parameters.AddWithValue("@LIBELLE", factureLigne.LibelleActe);
                    command.Parameters.AddWithValue("@COTATION", factureLigne.CotationActe);
                    command.Parameters.AddWithValue("@NOMBRE_POINTS", Convert.ToDouble(factureLigne.NombrePtsActe));
                    command.Parameters.AddWithValue("@RABAIS", factureLigne.rabais);
                    command.Parameters.AddWithValue("@montantSS", factureLigne.montantSS);
                    command.Parameters.AddWithValue("@montantMutuelle", factureLigne.montantMutuelle);
                    command.Parameters.AddWithValue("@montantPatient", factureLigne.partPatient);

                    command.ExecuteNonQuery();
                    //Affectation du numéro de facture au traitement
                    selectQuery = "update base_traitement set  ID_FACTURE=@ID_FACTURE ";
                      if (vAcquitter )
                          selectQuery += " , FACTUREE = 2 ";
                    else
                          selectQuery += " , FACTUREE = 1 ";
                      selectQuery += " where ID=@ID ";

                    MySqlCommand commandTraitement = new MySqlCommand(selectQuery, connection, transaction);
                    commandTraitement.Parameters.AddWithValue("@ID_FACTURE", factureLigne.id_Facture);
                    commandTraitement.Parameters.AddWithValue("@ID", factureLigne.idTraitement);
                    commandTraitement.ExecuteNonQuery();


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

        public static DataRow  GetFactureById(int id_facture)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = "SELECT a.ID, a.DATEFACTURE, a.ID_PATIENT, a.NOMBRE_POINTS, a.MONTANT, a.MONTANT_LABO, a.MONTANT_STER, a.MONTANT_TOTAL, a.MONTANT_ACHATS,a.POINTS, MONTANT_PAYE,MONTANT_RESTANT,DATEDEBUTFACTURE,DATEFINFACTURE ";
                selectQuery += "  FROM base_facture a";
                selectQuery += " where a.ID=@ID";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@ID", id_facture );

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];


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



        public static DataTable GetFactures(basePatient patient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = "SELECT a.ID, a.DATEFACTURE, a.ID_PATIENT, a.NOMBRE_POINTS,a.MONTANT_ACHATS, a.MONTANT, a.MONTANT_LABO, a.MONTANT_STER, a.MONTANT_TOTAL, a.POINTS, MONTANT_PAYE,MONTANT_RESTANT, DATEDEBUTFACTURE, DATEFINFACTURE ";
                selectQuery += "  FROM base_facture a";
                selectQuery += " where a.ID_PATIENT=@ID_Patient";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@ID_Patient", patient.Id);

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
        public static DataTable GetLigneFacture(int idFacture)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = "SELECT a.ID_FACTURE, a.ID_LIGNE, a.ID_ECHEANCE, a.DATEEXECUTION, a.MONTANTLIGNEFACTURE ";
                selectQuery += "  FROM base_facture_ligne a";
                selectQuery += " where a.ID_FACTURE =@ID_FACTURE";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@ID_FACTURE", idFacture);

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
        public static DataRow GetFactureAcquiter(int idFacture)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = "SELECT a.ID, a.DATEFACTURE, a.ID_PATIENT, a.NOMBRE_POINTS, a.MONTANT,a.MONTANT_ACHATS, a.MONTANT_LABO, a.MONTANT_STER, a.MONTANT_TOTAL, a.POINTS, a.MONTANT_PAYE, a.MONTANT_RESTANT, a.DATEDEBUTFACTURE, a.DATEFINFACTURE";
                    selectQuery += " FROM base_facture_a_acquitter a";
                    selectQuery += " where a.ID =@ID_FACTURE";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@ID_FACTURE", idFacture);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];


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
        #region FinanceFacture
        public static void AddFacture(FinanceFacture facture)
        {
            if (connection == null) getConnection();
            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "insert into bas_facture (id,";
                selectQuery += "                      montant, ";
                selectQuery += "                      date,nom) ";
                selectQuery += " values (@id,@montant, ";
                selectQuery += "         @date,@nom)";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", facture.id);
                command.Parameters.AddWithValue("@montant", facture.montant);
                command.Parameters.AddWithValue("@date", facture.dateFacture);
                command.Parameters.AddWithValue("@nom", facture.nom);
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
        public static void deleteFacture(FinanceFacture facture)
        {
            if (connection == null) getConnection();
            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "delete from bas_facture where id = @id";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", facture.id);
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
        public static int getNextId()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                int _id = 1;
                string selectQueryId = "select max(Id)+1 as ID from bas_facture";
                MySqlCommand commandid = new MySqlCommand(selectQueryId, connection, transaction);
                object id = commandid.ExecuteScalar();
                if (id != DBNull.Value)
                    _id = Convert.ToInt32(id);
                return _id;
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
    }


    
}
