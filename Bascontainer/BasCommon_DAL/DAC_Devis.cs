﻿using System;
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

        public static void UpdateDevis_TK(Devis_TK obj)
        {

            lock (DAC.lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {


                    string selectQuery = "update base_devis set ";
                    selectQuery += "                            dateproposition=@dateproposition, ";
                    selectQuery += "                            dateacceptation=@dateacceptation, ";
                    selectQuery += "                            DATEECHEANCE=@DATEECHEANCE,  ";
                    selectQuery += "                            DATEDEBUTTRAITEMENT=@DATEDEBUTTRAITEMENT,  ";
                    selectQuery += "                            DATEFINTRAITEMENT=@DATEFINTRAITEMENT,  ";
                    selectQuery += "                            DateArchivage=@DateArchivage,  ";
                    selectQuery += "                            MONTANTPROPOSE=@MONTANTPROPOSE,";
                    selectQuery += "                            MONTANTAVANTPROPOSITION=@MONTANTAVANTPROPOSITION,";
                    selectQuery += "                            MONTANTDOCTEUR=@MONTANTDOCTEUR,";
                    selectQuery += "                            REMBMUTUELLE=@REMBMUTUELLE,";
                    selectQuery += "                            PARTPATIENT=@PARTPATIENT";
                    selectQuery += " where (id = @id)";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.CommandType = CommandType.Text;

                    command.Parameters.AddWithValue("@id", obj.Id);
                    command.Parameters.AddWithValue("@DATEACCEPTATION", obj.DateAcceptation == null ? DBNull.Value : (object)obj.DateAcceptation.Value);
                    command.Parameters.AddWithValue("@DATEECHEANCE", obj.DateEcheance == null ? DBNull.Value : (object)obj.DateEcheance.Value);
                    command.Parameters.AddWithValue("@DATEPROPOSITION", obj.DateProposition);
                    command.Parameters.AddWithValue("@DATEDEBUTTRAITEMENT", obj.DatePrevisionnelDeDebutTraitement);
                    command.Parameters.AddWithValue("@DATEFINTRAITEMENT", obj.DatePrevisionnelDeFinTraitement == null ? DBNull.Value : (object)obj.DatePrevisionnelDeFinTraitement.Value);
                    command.Parameters.AddWithValue("@DateArchivage", obj.DateArchivage == null ? DBNull.Value : (object)obj.DateArchivage.Value);
                    command.Parameters.AddWithValue("@MONTANTPROPOSE", obj.Montant == null ? DBNull.Value : (object)obj.Montant);
                    command.Parameters.AddWithValue("@MONTANTAVANTPROPOSITION", obj.MontantAvantRemise == null ? DBNull.Value : (object)obj.MontantAvantRemise);
                    command.Parameters.AddWithValue("@MONTANTDOCTEUR", obj.MontantDocteur == null ? DBNull.Value : (object)obj.MontantDocteur);
                    command.Parameters.AddWithValue("@REMBMUTUELLE", obj.RembMutuelle);
                    command.Parameters.AddWithValue("@PARTPATIENT", obj.partPatient);

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
        public static void UpdateDevis(Devis obj)
        {

            lock (DAC.lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {


                    string selectQuery = "update base_devis set ";
                    selectQuery += "                            dateproposition=@dateproposition, ";
                    selectQuery += "                            dateacceptation=@dateacceptation, ";
                    selectQuery += "                            DATEECHEANCE=@DATEECHEANCE,  ";
                    selectQuery += "                            DATEDEBUTTRAITEMENT=@DATEDEBUTTRAITEMENT,  ";
                    selectQuery += "                            DATEFINTRAITEMENT=@DATEFINTRAITEMENT,  ";
                    selectQuery += "                            DateArchivage=@DateArchivage,  ";
                    selectQuery += "                            id_objet_baseview=@id_objet_baseview,";
                    selectQuery += "                            MONTANTPROPOSE=@MONTANTPROPOSE,";
                    selectQuery += "                            MONTANTAVANTPROPOSITION=@MONTANTAVANTPROPOSITION";
                    selectQuery += " where (id = @id)";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.CommandType = CommandType.Text;

                    command.Parameters.AddWithValue("@id", obj.Id);
                    command.Parameters.AddWithValue("@DATEACCEPTATION", obj.DateAcceptation == null ? DBNull.Value : (object)obj.DateAcceptation.Value);
                    command.Parameters.AddWithValue("@DATEECHEANCE", obj.DateEcheance == null ? DBNull.Value : (object)obj.DateEcheance.Value);
                    command.Parameters.AddWithValue("@DATEPROPOSITION", obj.DateProposition);
                    command.Parameters.AddWithValue("@DATEDEBUTTRAITEMENT", obj.DatePrevisionnelDeDebutTraitement);
                    command.Parameters.AddWithValue("@DATEFINTRAITEMENT", obj.DatePrevisionnelDeFinTraitement == null ? DBNull.Value : (object)obj.DatePrevisionnelDeFinTraitement.Value);
                    command.Parameters.AddWithValue("@DateArchivage", obj.DateArchivage == null ? DBNull.Value : (object)obj.DateArchivage.Value);
                    command.Parameters.AddWithValue("@id_objet_baseview", obj.IdObjetBaseView);
                    command.Parameters.AddWithValue("@MONTANTPROPOSE", obj.Montant == null ? DBNull.Value : (object)obj.Montant);
                    command.Parameters.AddWithValue("@MONTANTAVANTPROPOSITION", obj.MontantAvantRemise == null ? DBNull.Value : (object)obj.MontantAvantRemise);

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

        public static void DelTempEcheances_tk(CommTraitement ct)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = @"delete from bas_echeances_devis
                                        where id in (
                                        select id  from (select * from bas_echeances_devis) as b
                                        where b.id_devis_comment = @Id
                                        )";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@Id", ct.Id);

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
        public static void UpdateEcheancierDocteur(int id_Devis, int Echeancier_Docteur)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = @"update base_devis set ECHEANCIER_DOCTEUR=@ECHEANCIER_DOCTEUR WHERE ID=@ID";
                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@ID", id_Devis);
                command.Parameters.AddWithValue("@ECHEANCIER_DOCTEUR", Echeancier_Docteur);

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

        public static void DelTempEcheances(Proposition p)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = @"delete from bas_echeances_devis
                                        where id in (
                                        select b.Id
                                       	from  (SELECT * FROM bas_echeances_devis) AS b 
                                        inner join base_semestre on base_semestre.id = b.id_sem_propose
                                        inner join base_plan_traitements on base_plan_traitements.id = base_semestre.id_traitement
                                        where base_plan_traitements.id_proposition = @Id
                                        )";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@Id", p.Id);

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



        public static void DeleteEcheanceDevisALaCarte(Devis d)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = @"delete from bas_echeances_devisalacarte
                                        where ID_DEVIS  = @Id ";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@Id", d.Id);

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

        public static void DelTempEcheances(Devis d)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = @"delete from bas_echeances_devis
                                        where id in (
                                        select bas_echeances_devis.Id
                                        from bas_echeances_devis
                                        inner join base_semestre on base_semestre.id = bas_echeances_devis.id_sem_propose
                                        inner join base_plan_traitements on base_plan_traitements.id = base_semestre.id_traitement
                                        inner join base_propositions on base_plan_traitements.id_proposition = base_propositions.id
                                        inner join base_devis on base_propositions.iddevis = base_devis.id
                                        where base_devis.id = @Id
                                        )";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@Id", d.Id);

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

        public static void DelTempEcheances(Semestre s)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = @"delete from bas_echeances_devis
                                         where bas_echeances_devis.ID_SEM_PROPOSE=@Id";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@Id", s.Id);

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

        public static void DelTempEcheance(TempEcheanceDefinition ted)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "delete from bas_echeances_devis where id= @id";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", ted.Id);

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

        public static void AddEcheanceDevisALaCarte(EcheanceDevisALaCarte ted)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQueryId = "select max(id)+1 as ID from bas_echeances_devisalacarte";
                MySqlCommand commandid = new MySqlCommand(selectQueryId, connection, transaction);
                object obj = commandid.ExecuteScalar();
                if (obj == DBNull.Value)
                    ted.Id = 1;
                else
                    ted.Id = Convert.ToInt32(obj);


                String SelectQuery = " insert into bas_echeances_devisalacarte (id, ";
                SelectQuery += "                                   ID_DEVIS, ";
                SelectQuery += "                                   MONTANT, ";
                SelectQuery += "                                   DTEECHEANCE, ";
                SelectQuery += "                                   PARPRELEVEMENT, ";
                SelectQuery += "                                   TYPEPAYEUR, ";
                SelectQuery += "                                   PARVIREMENT, ";
                SelectQuery += "                                   LIBELLE)";
                SelectQuery += " values (@id, ";
                SelectQuery += "        @ID_DEVIS, ";
                SelectQuery += "        @MONTANT, ";
                SelectQuery += "        @DTEECHEANCE, ";
                SelectQuery += "        @PARPRELEVEMENT, ";
                SelectQuery += "        @TYPEPAYEUR, ";
                SelectQuery += "        @PARVIREMENT, ";
                SelectQuery += "        @LIBELLE)";




                MySqlCommand commandt = new MySqlCommand(SelectQuery, connection, transaction);
                commandt.Parameters.Clear();
                commandt.Parameters.AddWithValue("@id", ted.Id);
                commandt.Parameters.AddWithValue("@ID_DEVIS", ted.IdDevis);
                commandt.Parameters.AddWithValue("@MONTANT", ted.Montant);
                commandt.Parameters.AddWithValue("@DTEECHEANCE", ted.DAteEcheance);
                commandt.Parameters.AddWithValue("@PARPRELEVEMENT", ted.ParPrelevement);
                commandt.Parameters.AddWithValue("@PARVIREMENT", ted.ParVirement);
                commandt.Parameters.AddWithValue("@TYPEPAYEUR", ted.payeur);
                commandt.Parameters.AddWithValue("@LIBELLE", ted.Libelle);

                commandt.ExecuteNonQuery();




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



        public static void AddTempEcheance(TempEcheanceDefinition ted)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQueryId = "select max(id)+1 as ID from bas_echeances_devis";
                MySqlCommand commandid = new MySqlCommand(selectQueryId, connection, transaction);
                object obj = commandid.ExecuteScalar();
                if (obj == DBNull.Value)
                    ted.Id = 1;
                else
                    ted.Id = Convert.ToInt32(obj);


                String SelectQuery = " insert into bas_echeances_devis (id, ";
                SelectQuery += "                                   ID_SEM_PROPOSE, ";
                SelectQuery += "                                   MONTANT, ";
                SelectQuery += "                                   DTEECHEANCE, ";
                SelectQuery += "                                   PARPRELEVEMENT, ";
                SelectQuery += "                                   PARVIREMENT, ";
                SelectQuery += "                                   TYPEPAYEUR, ";
                SelectQuery += "                                   LIBELLE)";
                SelectQuery += " values (@id, ";
                SelectQuery += "        @ID_SEM_PROPOSE, ";
                SelectQuery += "        @MONTANT, ";
                SelectQuery += "        @DTEECHEANCE, ";
                SelectQuery += "        @PARPRELEVEMENT, ";
                SelectQuery += "        @PARVIREMENT, ";
                SelectQuery += "        @TYPEPAYEUR, ";
                SelectQuery += "        @LIBELLE)";




                MySqlCommand commandt = new MySqlCommand(SelectQuery, connection, transaction);
                commandt.Parameters.Clear();
                commandt.Parameters.AddWithValue("@id", ted.Id);
                commandt.Parameters.AddWithValue("@ID_SEM_PROPOSE", ted.IdSemestre);
                commandt.Parameters.AddWithValue("@MONTANT", ted.Montant);
                commandt.Parameters.AddWithValue("@DTEECHEANCE", ted.DAteEcheance);
                commandt.Parameters.AddWithValue("@PARPRELEVEMENT", ted.ParPrelevement);
                commandt.Parameters.AddWithValue("@PARVIREMENT", ted.ParVirement);
                commandt.Parameters.AddWithValue("@TYPEPAYEUR", ted.payeur);
                commandt.Parameters.AddWithValue("@LIBELLE", ted.Libelle);

                commandt.ExecuteNonQuery();




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

        public static DataTable get_tempecheances(Proposition p)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            //  MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = @"select 
                                        bas_echeances_devis.id, 
                                        bas_echeances_devis.id_sem_propose, 
                                        bas_echeances_devis.montant, 
                                        bas_echeances_devis.dteecheance, 
                                        bas_echeances_devis.libelle, 
                                        bas_echeances_devis.parprelevement, 
                                        bas_echeances_devis.parvirement, 
                                        bas_echeances_devis.typepayeur
                                        from bas_echeances_devis
                                        inner join base_semestre on base_semestre.id = bas_echeances_devis.id_sem_propose
                                        inner join base_plan_traitements on base_plan_traitements.id = base_semestre.id_traitement
                                        where base_plan_traitements.id_proposition = @ID";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@ID", p.Id);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                //      transaction.Commit();

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


        public static DataTable get_echeancesDevisALaCarte(Devis devis)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            // MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = @"select 
                                        id, 
                                        ID_DEVIS, 
                                        montant, 
                                        dteecheance, 
                                        libelle, 
                                        parprelevement, 
                                        parvirement,
                                        typepayeur
                                        from bas_echeances_devisalacarte
                                        where bas_echeances_devisalacarte.ID_DEVIS =@ID_DEVIS";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@ID_DEVIS", devis.Id);


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


        public static DataTable get_tempecheances(Devis devis)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            //     MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = @"select 
                                        id, 
                                        id_sem_propose, 
                                        montant, 
                                        dteecheance, 
                                        libelle, 
                                        parprelevement, 
                                        parvirement,
                                        typepayeur
                                        from bas_echeances_devis
                                        inner join base_semestre on base_semestre.id = bas_echeances_devis.id_sem_propose
                                        inner join base_plan_traitements on base_plan_traitements.id = base_semestre.id_traitement
                                        inner join base_propositions on base_plan_traitements.id_proposition = base_propositions.id
                                        inner join base_devis on base_propositions.iddevis = base_devis.id
                                        where base_devis.id =@ID_DEVIS";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@ID_DEVIS", devis.Id);


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

        public static DataTable get_tempecheances(Semestre s)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            //  MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = @"select id, 
                                        id_sem_propose, 
                                        montant, 
                                        dteecheance, 
                                        libelle, 
                                        parprelevement, 
                                        typepayeur
                                        from bas_echeances_devis
                                        where bas_echeances_devis.ID_SEM_PROPOSE =@Id";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@Id", s.Id);


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

        public static DataTable get_tempecheances_TK(CommTraitement ct)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            //  MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = @"select id, 
                                        id_sem_propose, 
                                        montant, 
                                        dteecheance, 
                                        libelle, 
                                        parprelevement,
                                        parvirement, 
                                        typepayeur,
                                        id_devis_comment
                                        from bas_echeances_devis
                                        where bas_echeances_devis.ID_DEVIS_COMMENT=@Id";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@Id", ct.Id);


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


        public static DataTable get_tempecheancescc_TK(CommClinique cc)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            //   MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = @"select id, 
                                        id_sem_propose, 
                                        montant, 
                                        dteecheance, 
                                        libelle, 
                                        parprelevement,
                                        parvirement, 
                                        typepayeur,
                                        id_devis_comment
                                        from bas_echeances_devis
                                        where bas_echeances_devis.ID_COMMCLINIQUE=@Id";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@Id", cc.Id);


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
        //seif impression
        public static DataTable getCommImpression(int idDevis)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            // MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "SELECT        a.ID, 'TRAITEMENT' AS LIB_1, ACT.ACTE_LIBELLE AS TRAITEMENT, a.ID_ACTE, ACT.ACTE_LIBELLE,a.MONTANTAVANTPROPOSITION,a.MONTANT";
                selectQuery += " FROM            devis_comments a, actes ACT";
                selectQuery += " WHERE        (a.ID_DEVIS = @idDevis) AND (ACT.id_acte = a.ID_ACTE)";
                selectQuery += " UNION";
                selectQuery += " SELECT        a.ID, 'TRAITEMENT' AS LIB_1, ACT.ACTE_LIBELLE AS TRAITEMENT, BDM.ID_MATERIEL, MAT.MATERIEL_LIBELLE, BDM.MONTANT,  "; selectQuery += "                 BDM.MONTANTAVANTREMISE";
                selectQuery += " FROM            devis_comments a, base_devis_mat BDM, materiels MAT, actes ACT";
                selectQuery += " WHERE        a.ID = BDM.ID AND BDM.ID_MATERIEL = MAT.ID_MATERIEL AND (a.ID_DEVIS = @idDevis) AND (BDM.ID IN";
                selectQuery += "                             (SELECT        ID";
                selectQuery += "                               FROM            devis_comments a";
                selectQuery += "                               WHERE        (ID_DEVIS = @idDevis))) AND (ACT.id_acte = a.ID_ACTE)";
                selectQuery += " UNION";
                selectQuery += " SELECT        a.ID, 'TRAITEMENT' AS LIB_1, ACT2.ACTE_LIBELLE AS TRAITEMENT, a.ID_ACTE, ACT.acte_libelle, a.MONTANTAVANTREMISE, a.MONTANT";
                selectQuery += " FROM            base_devis_actes_tk a, devis_comments DC, actes ACT, actes ACT2";
                selectQuery += " WHERE        a.ID = DC.ID AND a.ID = DC.ID AND (a.ID IN";
                selectQuery += "                            (SELECT        ID";
                selectQuery += "                             FROM            devis_comments a";
                selectQuery += "                             WHERE        (ID_DEVIS = @idDevis))) AND (ACT.id_acte = a.ID_ACTE) AND (ACT2.id_acte = DC.ID_ACTE)";




                MySqlCommand command = new MySqlCommand(selectQuery, connection);



                command.Parameters.AddWithValue("@idDevis", idDevis);



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
        //seif
        public static void update_tempechenaces_tk(TempEcheanceDefinition ted, CommTraitement ct)
        {

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "update bas_echeances_devis";
                selectQuery += " set ID = @ID ";
                selectQuery += " , ID_SEM_PROPOSE = @ID_SEM_PROPOSE ";
                selectQuery += " , MONTANT = @MONTANT ";
                selectQuery += " , DTEECHEANCE = @DTEECHEANCE ";
                selectQuery += " , LIBELLE = @LIBELLE ";
                selectQuery += " , PARPRELEVEMENT = @PARPRELEVEMENT ";
                selectQuery += " , TYPEPAYEUR = @TYPEPAYEUR ";
                selectQuery += " , PARVIREMENT = @PARVIREMENT ";
                selectQuery += " , ID_DEVIS_COMMENT = @ID_DEVIS_COMMENT ";
                selectQuery += " where (id = @id)";




                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;


                command.Parameters.AddWithValue("@id", ted.Id);
                command.Parameters.AddWithValue("@ID_SEM_PROPOSE", ted.IdSemestre);

                command.Parameters.AddWithValue("@MONTANT", ted.Montant);
                command.Parameters.AddWithValue("@DTEECHEANCE", ted.DAteEcheance);
                command.Parameters.AddWithValue("@LIBELLE", ted.Libelle);

                command.Parameters.AddWithValue("@PARPRELEVEMENT", ted.ParPrelevement);
                command.Parameters.AddWithValue("@TYPEPAYEUR", ted.payeur);
                command.Parameters.AddWithValue("@PARVIREMENT", ted.ParVirement);
                command.Parameters.AddWithValue("@ID_DEVIS_COMMENT", ct.Id);


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
        public static void update_tempechenacescc_tk(TempEcheanceDefinition ted, CommClinique cc)
        {

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "update bas_echeances_devis";
                selectQuery += " set ID = @ID ";
                selectQuery += " , ID_SEM_PROPOSE = @ID_SEM_PROPOSE ";
                selectQuery += " , MONTANT = @MONTANT ";
                selectQuery += " , DTEECHEANCE = @DTEECHEANCE ";
                selectQuery += " , LIBELLE = @LIBELLE ";
                selectQuery += " , PARPRELEVEMENT = @PARPRELEVEMENT ";
                selectQuery += " , TYPEPAYEUR = @TYPEPAYEUR ";
                selectQuery += " , PARVIREMENT = @PARVIREMENT ";
                selectQuery += " , ID_COMMCLINIQUE = @ID_COMMCLINIQUE ";
                selectQuery += " where (id = @id)";




                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;


                command.Parameters.AddWithValue("@id", ted.Id);
                command.Parameters.AddWithValue("@ID_SEM_PROPOSE", ted.IdSemestre);

                command.Parameters.AddWithValue("@MONTANT", ted.Montant);
                command.Parameters.AddWithValue("@DTEECHEANCE", ted.DAteEcheance);
                command.Parameters.AddWithValue("@LIBELLE", ted.Libelle);

                command.Parameters.AddWithValue("@PARPRELEVEMENT", ted.ParPrelevement);
                command.Parameters.AddWithValue("@TYPEPAYEUR", ted.payeur);
                command.Parameters.AddWithValue("@PARVIREMENT", ted.ParVirement);
                command.Parameters.AddWithValue("@ID_COMMCLINIQUE", cc.Id);


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
        public static DataTable get_acte_propositions(Devis devis)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            // MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select ID,";
                selectQuery += "       ID_DEVIS, ";
                selectQuery += "       ID_PROPOSITION, ";
                selectQuery += "       DATE_EXECUTION, ";
                selectQuery += "       QTE, ";
                selectQuery += "       OPTIONAL, ";
                selectQuery += "       MONTANT, ";
                selectQuery += "       MONTANTAVANTREMISE, ";
                selectQuery += "       LIBELLE, ";
                selectQuery += "       ID_TEMPLATE_ACTE_GESTION ";
                selectQuery += " from base_devis_actes";
                selectQuery += " where ID_DEVIS=@ID_DEVIS";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@ID_DEVIS", devis.Id);


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


        public static DataTable get_acte_propositions(Proposition p)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            //  MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select ID,";
                selectQuery += "       ID_DEVIS, ";
                selectQuery += "       ID_PROPOSITION, ";
                selectQuery += "       DATE_EXECUTION, ";
                selectQuery += "       QTE, ";
                selectQuery += "       OPTIONAL, ";
                selectQuery += "       MONTANT, ";
                selectQuery += "       MONTANTAVANTREMISE, ";
                selectQuery += "       LIBELLE, ";
                selectQuery += "       ID_TEMPLATE_ACTE_GESTION ";
                selectQuery += " from base_devis_actes";
                selectQuery += " where ID_PROPOSITION=@ID_PROPOSITION";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@ID_PROPOSITION", p.Id);


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

        public static void Delete_acte_propositions(Devis devis)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "DELETE FROM base_devis_actes  ";
                selectQuery += "where id_devis=@id_devis ";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id_devis", devis.Id);

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


        public static void Delete_acte_propositions(Proposition p)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "DELETE FROM base_devis_actes  ";
                selectQuery += "where id_proposition=@id_proposition ";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id_proposition", p.Id);

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


        //public static void Insert_acte_propositions_TK(CommTraitement acte)
        //{
        //    if (connection == null) getConnection(); if (connection == null) return;

        //    if (connection.State == ConnectionState.Closed) connection.Open();
        //    MySqlTransaction transaction = connection.BeginTransaction();
        //    try
        //    {

        //        string selectQuery = "select MAX(ID)+1 as NEWID from base_devis_actes";

        //        MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
        //        command.CommandType = CommandType.Text;

        //        object obj = command.ExecuteScalar();

        //        if (obj == DBNull.Value)
        //            acte.Id = 1;
        //        else
        //            acte.Id = Convert.ToInt32(obj);




        //        selectQuery = "insert into base_devis_actes (";
        //        selectQuery += " id, ";
        //        selectQuery += " id_devis, ";
        //        selectQuery += " id_proposition, ";
        //        selectQuery += " date_execution, ";
        //        selectQuery += " qte, ";
        //        selectQuery += " OPTIONAL, ";
        //        selectQuery += " Libelle, ";
        //        selectQuery += " montant, ";
        //        selectQuery += " MontantAvantRemise, ";
        //        selectQuery += " id_template_acte_gestion)";
        //        selectQuery += " values (@id,";
        //        selectQuery += " @id_devis, ";
        //        selectQuery += " @id_proposition, ";
        //        selectQuery += " @date_execution, ";
        //        selectQuery += " @qte, ";
        //        selectQuery += " @OPTIONAL, ";
        //        selectQuery += " @Libelle, ";
        //        selectQuery += " @montant, ";
        //        selectQuery += " @MontantAvantRemise, ";
        //        selectQuery += " @id_template_acte_gestion)";

        //        command.CommandText = selectQuery;

        //        command.Parameters.Clear();
        //        command.Parameters.AddWithValue("@id", acte.Id);
        //        command.Parameters.AddWithValue("@id_devis", acte.IdDevis);
        //        command.Parameters.AddWithValue("@id_proposition", acte.IdProposition);
        //        command.Parameters.AddWithValue("@date_execution", acte.DateExecution == null ? DBNull.Value : (object)acte.DateExecution.Value);
        //        command.Parameters.AddWithValue("@qte", acte.Qte);
        //        command.Parameters.AddWithValue("@OPTIONAL", acte.Optionnel);
        //        command.Parameters.AddWithValue("@Libelle", acte.Libelle);
        //        command.Parameters.AddWithValue("@montant", acte.Montant);
        //        command.Parameters.AddWithValue("@MontantAvantRemise", acte.MontantAvantRemise);
        //        command.Parameters.AddWithValue("@id_template_acte_gestion", acte.IdTemplateActePG);

        //        command.ExecuteNonQuery();

        //        transaction.Commit();

        //    }
        //    catch (System.Exception e)
        //    {
        //        transaction.Rollback();
        //        throw e;
        //    }
        //    finally
        //    {
        //       connection.Close();

        //    }



        //}
        public static void Insert_acte_propositions(ActePGPropose acte)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "select MAX(ID)+1 as NEWID from base_devis_actes";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                object obj = command.ExecuteScalar();

                if (obj == DBNull.Value)
                    acte.Id = 1;
                else
                    acte.Id = Convert.ToInt32(obj);




                selectQuery = "insert into base_devis_actes (";
                selectQuery += " id, ";
                selectQuery += " id_devis, ";
                selectQuery += " id_proposition, ";
                selectQuery += " date_execution, ";
                selectQuery += " qte, ";
                selectQuery += " OPTIONAL, ";
                selectQuery += " Libelle, ";
                selectQuery += " montant, ";
                selectQuery += " MontantAvantRemise, ";
                selectQuery += " id_template_acte_gestion)";
                selectQuery += " values (@id,";
                selectQuery += " @id_devis, ";
                selectQuery += " @id_proposition, ";
                selectQuery += " @date_execution, ";
                selectQuery += " @qte, ";
                selectQuery += " @OPTIONAL, ";
                selectQuery += " @Libelle, ";
                selectQuery += " @montant, ";
                selectQuery += " @MontantAvantRemise, ";
                selectQuery += " @id_template_acte_gestion)";

                command.CommandText = selectQuery;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", acte.Id);
                command.Parameters.AddWithValue("@id_devis", acte.IdDevis);
                command.Parameters.AddWithValue("@id_proposition", acte.IdProposition);
                command.Parameters.AddWithValue("@date_execution", acte.DateExecution == null ? DBNull.Value : (object)acte.DateExecution.Value);
                command.Parameters.AddWithValue("@qte", acte.Qte);
                command.Parameters.AddWithValue("@OPTIONAL", acte.Optionnel);
                command.Parameters.AddWithValue("@Libelle", acte.Libelle);
                command.Parameters.AddWithValue("@montant", acte.Montant);
                command.Parameters.AddWithValue("@MontantAvantRemise", acte.MontantAvantRemise);
                command.Parameters.AddWithValue("@id_template_acte_gestion", acte.IdTemplateActePG);

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

        public static void updateActePGPropose(ActePGPropose acte)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "";
                MySqlCommand command = new MySqlCommand("", connection, transaction);
                command.CommandType = CommandType.Text;

                selectQuery = "update base_devis_actes set";
                selectQuery += " id_devis= @id_devis,";
                selectQuery += " id_proposition= @id_proposition,";
                selectQuery += " date_execution = @date_execution, ";
                selectQuery += " qte= @qte, ";
                selectQuery += " OPTIONAL= @OPTIONAL, ";
                selectQuery += " Libelle= @Libelle, ";

                selectQuery += " montant = @montant, ";
                selectQuery += " MontantAvantRemise = @MontantAvantRemise, ";
                selectQuery += " id_template_acte_gestion = @id_template_acte_gestion";
                selectQuery += " where id= @id";

                command.CommandText = selectQuery;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", acte.Id);
                command.Parameters.AddWithValue("@id_devis", acte.IdDevis);
                command.Parameters.AddWithValue("@id_proposition", acte.IdProposition);
                command.Parameters.AddWithValue("@date_execution", acte.DateExecution == null ? DBNull.Value : (object)acte.DateExecution.Value);
                command.Parameters.AddWithValue("@qte", acte.Qte);
                command.Parameters.AddWithValue("@OPTIONAL", acte.Optionnel);
                command.Parameters.AddWithValue("@Libelle", acte.Libelle);
                command.Parameters.AddWithValue("@montant", acte.Montant);
                command.Parameters.AddWithValue("@MontantAvantRemise", acte.MontantAvantRemise);
                command.Parameters.AddWithValue("@id_template_acte_gestion", acte.IdTemplateActePG);

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

        public static void DeleteDevis_TK(int Iddevis)
        {

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {





                //Suppression des matériaux
                string selectquery = "delete from base_devis_mat  WHERE base_devis_mat.ID in ";
                selectquery += "(SELECT BC.ID FROM devis_comments BC WHERE BC.ID_DEVIS = @iddevis)";



                MySqlCommand commandt = new MySqlCommand(selectquery, connection, transaction);

                commandt.Parameters.AddWithValue("@iddevis", Iddevis);


                commandt.ExecuteNonQuery();



                //Suppression des autres personne
                selectquery = "delete from base_devis_autrepers  WHERE base_devis_autrepers.ID_COMM_DEVIS in ";
                selectquery += "(SELECT BC.ID FROM devis_comments BC WHERE BC.ID_DEVIS = @iddevis)";

                commandt.CommandText = selectquery;

                commandt.ExecuteNonQuery();

                //Suppression des Actes (Supp, Radio et Photos)
                selectquery = "delete from base_devis_actes_tk WHERE base_devis_actes_tk.ID in ";
                selectquery += "(SELECT BC.ID FROM devis_comments BC WHERE BC.ID_DEVIS = @iddevis)";

                commandt.CommandText = selectquery;

                commandt.ExecuteNonQuery();



                //Suppression des Comments
                selectquery = "delete from devis_comments  WHERE devis_comments.ID_DEVIS  = @iddevis";

                commandt.CommandText = selectquery;

                commandt.ExecuteNonQuery();





                //Suppression du devis
                selectquery = "delete from base_devis";
                selectquery += " where base_devis.id =  @iddevis";


                commandt.CommandText = selectquery;

                commandt.ExecuteNonQuery();


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
        public static void setDevisPrixTotal(Devis_TK dev)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "";
                MySqlCommand command;
                command = new MySqlCommand(selectQuery, connection, transaction);


                selectQuery = "update base_devis set   MONTANTAVANTPROPOSITION=@MONTANTAVANTPROPOSITION, MONTANTPROPOSE=@MONTANTPROPOSE  where ID = @id";

                command.Parameters.AddWithValue("@id", dev.Id);
                command.Parameters.AddWithValue("@MONTANTPROPOSE", dev.Montant);
                command.Parameters.AddWithValue("@MONTANTAVANTPROPOSITION", dev.MontantAvantRemise);
                //command.Parameters.AddWithValue("@REMBMUTUELLE", dev.RembMutuelle);
                //command.Parameters.AddWithValue("@PARTPATIENT", dev.partPatient);
                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
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

        public static void setDevisPrix(CommTraitement comm)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "";
                MySqlCommand command;
                command = new MySqlCommand(selectQuery, connection, transaction);


                selectQuery = "update devis_comments set   MONTANT =@MONTANT,QTE =@QTE,MONTANTAVANTPROPOSITION=@MONTANTAVANTPROPOSITION,PARTPATIENT=@partpatient,REMBMUTUELLE=@rembmutuelle  where ID = @id";

                command.Parameters.AddWithValue("@id", comm.Id);
                command.Parameters.AddWithValue("@MONTANT", comm.Acte.prix_traitement);
                command.Parameters.AddWithValue("@MONTANTAVANTPROPOSITION", comm.Acte.prix_acte);
                command.Parameters.AddWithValue("@QTE", comm.Acte.quantite);
                command.Parameters.AddWithValue("@partpatient", comm.partPatient);
                command.Parameters.AddWithValue("@rembmutuelle", comm.RembMutuelle);
                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                object obj = command.ExecuteNonQuery();
                foreach (CommActesTraitement act in comm.ActesSupp)
                {
                    command = new MySqlCommand(selectQuery, connection, transaction);
                    selectQuery = "update base_devis_actes_tk set   MONTANT =@MONTANT,MONTANTAVANTREMISE=@MONTANTAVANTREMISE,PARTPATIENT=@partpatient,REMBMUTUELLE=@rembmutuelle where  ID_ACTE= @ID_ACTE and ID = @id   and TYPE_ACTE = ''";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", comm.Id);
                    command.Parameters.AddWithValue("@MONTANT", act.prix_traitement);
                    command.Parameters.AddWithValue("@MONTANTAVANTREMISE", act.prix_acte);
                    command.Parameters.AddWithValue("@ID_ACTE", act.IdActe);
                    command.Parameters.AddWithValue("@partpatient", act.partPatient);
                    command.Parameters.AddWithValue("@rembmutuelle", act.RembMutuelle);
                    command.CommandText = selectQuery;
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();

                }
                foreach (CommActesTraitement act in comm.Radios)
                {
                    command = new MySqlCommand(selectQuery, connection, transaction);
                    selectQuery = "update base_devis_actes_tk set   MONTANT =@MONTANT,MONTANTAVANTREMISE=@MONTANTAVANTREMISE,PARTPATIENT=@partpatient,REMBMUTUELLE=@rembmutuelle where  ID_ACTE= @ID_ACTE and ID = @id   and TYPE_ACTE = 'R'";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", comm.Id);
                    command.Parameters.AddWithValue("@MONTANT", act.prix_traitement);
                    command.Parameters.AddWithValue("@MONTANTAVANTREMISE", act.prix_acte);
                    command.Parameters.AddWithValue("@ID_ACTE", act.IdActe);
                    command.Parameters.AddWithValue("@partpatient", act.partPatient);
                    command.Parameters.AddWithValue("@rembmutuelle", act.RembMutuelle);
                    command.CommandText = selectQuery;
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
                foreach (CommActesTraitement act in comm.photos)
                {
                    command = new MySqlCommand(selectQuery, connection, transaction);
                    selectQuery = "update base_devis_actes_tk set   MONTANT =@MONTANT,MONTANTAVANTREMISE=@MONTANTAVANTREMISE,PARTPATIENT=@partpatient,REMBMUTUELLE=@rembmutuelle where  ID_ACTE= @ID_ACTE and ID = @id   and TYPE_ACTE = 'P'";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", comm.Id);
                    command.Parameters.AddWithValue("@MONTANT", act.prix_traitement);
                    command.Parameters.AddWithValue("@MONTANTAVANTREMISE", act.prix_acte);
                    command.Parameters.AddWithValue("@partpatient", act.partPatient);
                    command.Parameters.AddWithValue("@rembmutuelle", act.RembMutuelle);
                    command.Parameters.AddWithValue("@ID_ACTE", act.IdActe);

                    command.CommandText = selectQuery;
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();

                }
                foreach (CommMaterielTraitement act in comm.Materiels)
                {
                    selectQuery = "update base_devis_mat set    MONTANT =@MONTANT,MONTANTAVANTREMISE=@MONTANTAVANTREMISE where  ID_MATERIEL= @ID_MATERIEL and ID = @id  ";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", comm.Id);
                    command.Parameters.AddWithValue("@MONTANT", act.prix_traitement);
                    command.Parameters.AddWithValue("@MONTANTAVANTREMISE", act.prix_materiel);
                    command.Parameters.AddWithValue("@ID_MATERIEL", act.idMateriel);

                    command.CommandText = selectQuery;
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();

                }

                /*
                                selectQuery = "insert into base_traitement_MAT (id, ";
                                selectQuery += "                            qte, ";
                                selectQuery += "                            id_materiel) ";
                                selectQuery += " values (@id, ";
                                selectQuery += "         @qte, ";
                                selectQuery += "         @id_materiel) ";



                                command.CommandText = selectQuery;

                                foreach (CommMateriel cr in comm.Materiels)
                                {
                                    command.Parameters.Clear();
                                    command.Parameters.AddWithValue("@id", comm.Id);
                                    command.Parameters.AddWithValue("@qte", cr.Qte);
                                    command.Parameters.AddWithValue("@id_materiel", cr.idMateriel);


                                    command.ExecuteNonQuery();
                                }

                                */
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
        public static void DeleteDevisOLD(int id)
        {


            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "delete from base_devis";
                selectQuery += " where (ID = @id)";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();


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
        public static void DeleteDevis(int Iddevis)
        {

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {






                //Suppression des surveillances du devis
                string selectquery = "delete from base_surveillance";
                selectquery += @" where base_surveillance.id in (SELECT bs.id
                                    FROM (SELECT * FROM base_surveillance) AS bs 
                                    inner join base_semestre on base_semestre.id = bs.id_semestre
                                    inner join  base_plan_traitements on base_plan_traitements.id = base_semestre.id_traitement
                                    inner join base_propositions on base_propositions.id = base_plan_traitements.id_proposition
                                    inner join base_devis on base_devis.id = base_propositions.iddevis
                                    where base_devis.id =  @iddevis )";

                MySqlCommand commandt = new MySqlCommand(selectquery, connection, transaction);

                commandt.Parameters.AddWithValue("@iddevis", Iddevis);


                commandt.ExecuteNonQuery();



                selectquery = "delete from bas_echeances_devis";
                selectquery += " where bas_echeances_devis.ID_SEM_PROPOSE in (";
                selectquery += @" select base_semestre.id from base_semestre
                                    inner join  base_plan_traitements on base_plan_traitements.id = base_semestre.id_traitement
                                    inner join base_propositions on base_propositions.id = base_plan_traitements.id_proposition
                                    inner join base_devis on base_devis.id = base_propositions.iddevis
                                    where base_devis.id =  @iddevis )";

                commandt.CommandText = selectquery;

                commandt.ExecuteNonQuery();


                //Suppression des semestres
                selectquery = "delete from base_semestre";
                selectquery += " where base_semestre.id_traitement in (";
                selectquery += @" select base_plan_traitements.id from  base_plan_traitements
                                    inner join base_propositions on base_propositions.id = base_plan_traitements.id_proposition
                                    inner join base_devis on base_devis.id = base_propositions.iddevis
                                    where base_devis.id =  @iddevis)";


                commandt.CommandText = selectquery;


                commandt.ExecuteNonQuery();




                //Suppression des traitements
                selectquery = "delete from base_plan_traitements";
                selectquery += " where base_plan_traitements.id_proposition in (";
                selectquery += @"select base_propositions.id from   base_propositions
                                    inner join base_devis on base_devis.id = base_propositions.iddevis
                                    where base_devis.id =  @iddevis)";


                commandt.CommandText = selectquery;

                commandt.ExecuteNonQuery();


                //Suppression des propositions
                selectquery = "delete from base_devis_actes";
                selectquery += @" where id_proposition in (select base_propositions.id from   base_propositions
                                    inner join base_devis on base_devis.id = base_propositions.iddevis
                                    where base_devis.id =  @iddevis)";

                commandt.CommandText = selectquery;

                commandt.ExecuteNonQuery();

                //Suppression des propositions
                selectquery = "delete from base_propositions";
                selectquery += " where base_propositions.iddevis in (";
                selectquery += " ";
                selectquery += " select id from base_devis";
                selectquery += " where base_devis.id =  @iddevis";
                selectquery += " ";
                selectquery += " )";


                commandt.CommandText = selectquery;

                commandt.ExecuteNonQuery();


                //Suppression des propositions
                selectquery = "delete from base_devis_actes";
                selectquery += " where id_devis = @iddevis";


                commandt.CommandText = selectquery;

                commandt.ExecuteNonQuery();

                selectquery = @"delete from bas_echeances_devis
                                        where id in 
                                         (SELECT be.Id
                                    FROM (SELECT * FROM bas_echeances_devis) AS be  
                                        inner join base_semestre on base_semestre.id = be.id_sem_propose
                                        inner join base_plan_traitements on base_plan_traitements.id = base_semestre.id_traitement
                                        inner join base_propositions on base_plan_traitements.id_proposition = base_propositions.id
                                        inner join base_devis on base_propositions.iddevis = base_devis.id
                                        where base_devis.id = @iddevis
                                        )";

                commandt.CommandText = selectquery;

                commandt.ExecuteNonQuery();




                selectquery = @"delete from bas_echeances_devisalacarte
                                        where ID_DEVIS  = @iddevis ";

                commandt.CommandText = selectquery;

                commandt.ExecuteNonQuery();

                //Suppression du devis
                selectquery = "delete from base_devis";
                selectquery += " where base_devis.id =  @iddevis";


                commandt.CommandText = selectquery;

                commandt.ExecuteNonQuery();


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

        public static DataRow getDevis(int Id)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            // MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "select id,  ";
                selectQuery += "       dateproposition,  ";
                selectQuery += "       dateacceptation,  ";
                selectQuery += "       id_patient,  ";
                selectQuery += "       TypeDevis,  ";
                selectQuery += "       DATEECHEANCE,  ";
                selectQuery += "       DATEDEBUTTRAITEMENT,  ";
                selectQuery += "       id_objet_baseview ";
                selectQuery += "       from base_devis ";
                selectQuery += "       where  id = @id";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@id", Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                //  transaction.Commit();

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
        public static DataTable getCommDevis(int Id)
        {
            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
                // MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    //famille_Traitement

                    string selectQuery = "select * FROM devis_comments DC where DC.ID_DEVIS = @ID_DEVIS ORDER BY NBJOURS";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection);
                    command.Parameters.AddWithValue("@ID_DEVIS", Id);
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
        }

        public static DataRow getDevis_TK(int Id)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            // MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "select id,  ";
                selectQuery += "       dateproposition,  ";
                selectQuery += "       dateacceptation,  ";
                selectQuery += "       DATEECHEANCE,  ";
                selectQuery += "       DATEARCHIVAGE,  ";
                selectQuery += "       ARCHIVEPAR,  ";
                selectQuery += "       TypeDevis,  ";
                selectQuery += "       DATEDEBUTTRAITEMENT,  ";
                selectQuery += "       DATEFINTRAITEMENT,  ";
                selectQuery += "       MONTANTPROPOSE,  ";
                selectQuery += "       MONTANTAVANTPROPOSITION,  ";

                selectQuery += "       REMARQUEARCHIVAGE,  ";
                selectQuery += "       EMPLACEMENTARCHIVAGE,  ";
                selectQuery += "       id_patient,  ";
                selectQuery += "       id_objet_baseview, ";
                selectQuery += "       id_traitement, TITRE, MONTANTDOCTEUR,echeancier_docteur,PARTPATIENT,REMBMUTUELLE,localisation";
                selectQuery += "       from base_devis ";
                selectQuery += "       where  id = @id ";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@id", Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                //  transaction.Commit();

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

        public static DataTable getDevis_TK(basePatient patient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            // MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "select id,  ";
                selectQuery += "       dateproposition,  ";
                selectQuery += "       dateacceptation,  ";
                selectQuery += "       DATEECHEANCE,  ";
                selectQuery += "       DATEARCHIVAGE,  ";
                selectQuery += "       ARCHIVEPAR,  ";
                selectQuery += "       TypeDevis,  ";
                selectQuery += "       DATEDEBUTTRAITEMENT,  ";
                selectQuery += "       DATEFINTRAITEMENT,  ";
                selectQuery += "       MONTANTPROPOSE,  ";
                selectQuery += "       MONTANTAVANTPROPOSITION,  ";

                selectQuery += "       REMARQUEARCHIVAGE,  ";
                selectQuery += "       EMPLACEMENTARCHIVAGE,  ";
                selectQuery += "       id_patient,  ";
                selectQuery += "       id_objet_baseview, ";
                selectQuery += "       id_traitement, TITRE,MONTANTDOCTEUR, echeancier_docteur,REMBMUTUELLE,PARTPATIENT,localisation";
                selectQuery += "       from base_devis ";
                selectQuery += "       where  id_patient = @id_patient and id_traitement is not null";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@id_patient", patient.Id);

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
        public static DataTable getDevis(basePatient patient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            //  MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "select id,  ";
                selectQuery += "       dateproposition,  ";
                selectQuery += "       dateacceptation,  ";
                selectQuery += "       DATEECHEANCE,  ";
                selectQuery += "       DATEARCHIVAGE,  ";
                selectQuery += "       ARCHIVEPAR,  ";
                selectQuery += "       TypeDevis,  ";
                selectQuery += "       DATEDEBUTTRAITEMENT,  ";
                selectQuery += "       DATEFINTRAITEMENT,  ";
                selectQuery += "       MONTANTPROPOSE,  ";
                selectQuery += "       MONTANTAVANTPROPOSITION,  ";

                selectQuery += "       REMARQUEARCHIVAGE,  ";
                selectQuery += "       EMPLACEMENTARCHIVAGE,  ";
                selectQuery += "       id_patient,  ";
                selectQuery += "       id_objet_baseview ";
                selectQuery += "       from base_devis ";
                selectQuery += "       where  id_patient = @id_patient  and id_traitement is  null";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@id_patient", patient.Id);

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
        public static void InsertDevis_Actes_TK(List<ActePGPropose> ActeProp)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "";
                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                selectQuery = "insert into base_devis_actes_tk (";
                selectQuery += " id, ";
                selectQuery += " date_execution, ";
                selectQuery += " qte, ";
                selectQuery += " montant, ";
                selectQuery += " MontantAvantRemise, ";
                selectQuery += " id_acte,";
                selectQuery += " TYPE_ACTE,";
                selectQuery += " PARTPATIENT,";
                selectQuery += " REMBMUTUELLE,desactive)";
                selectQuery += " values (@id, ";
                selectQuery += " @date_execution, ";
                selectQuery += " @qte, ";
                selectQuery += " @montant, ";
                selectQuery += " @MontantAvantRemise, ";
                selectQuery += " @id_acte,";
                selectQuery += " @TYPE_ACTE,";
                selectQuery += " @PARTPATIENT,";
                selectQuery += " @REMBMUTUELLE,@desactive)";


                command.CommandText = selectQuery;

                foreach (ActePGPropose cr in ActeProp)
                {
                    command.Parameters.Clear();
                    /// command.Parameters.AddWithValue("@id_devis", cr.IdDevis );
                    command.Parameters.AddWithValue("@id", cr.Id);
                    command.Parameters.AddWithValue("@date_execution", cr.DateExecution == null ? DBNull.Value : (object)cr.DateExecution.Value);
                    command.Parameters.AddWithValue("@qte", cr.Qte);
                    command.Parameters.AddWithValue("@montant", cr.Montant);
                    command.Parameters.AddWithValue("@MontantAvantRemise", cr.MontantAvantRemise);
                    command.Parameters.AddWithValue("@id_acte", cr.IdTemplateActePG);
                    command.Parameters.AddWithValue("@TYPE_ACTE", cr.Type_Acte);
                    command.Parameters.AddWithValue("@PARTPATIENT", cr.partPatient);
                    command.Parameters.AddWithValue("@REMBMUTUELLE", cr.RembMutuelle);
                    command.Parameters.AddWithValue("@desactive", cr.desactive);
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
            /*****/
            /* if (connection == null) getConnection(); if (connection == null) return;

             if (connection.State == ConnectionState.Closed) connection.Open();
             MySqlTransaction transaction = connection.BeginTransaction();
             try
             {

                 string selectQuery;

                 selectQuery = "insert into base_devis_actes (";
                 selectQuery += " id_devis, ";
                 selectQuery += " id, ";
                 selectQuery += " date_execution, ";
                 selectQuery += " qte, ";
                 selectQuery += " Libelle, ";
                 selectQuery += " montant, ";
                 selectQuery += " MontantAvantRemise, ";
                 selectQuery += " id_template_acte_gestion,";
                 selectQuery += " TYPE_ACTE)";
                 selectQuery += " values (@id_devis, ";
                 selectQuery += " @id, ";
                 selectQuery += " @date_execution, ";
                 selectQuery += " @qte, ";
                 selectQuery += " @Libelle, ";
                 selectQuery += " @montant, ";
                 selectQuery += " @MontantAvantRemise, ";
                 selectQuery += " @id_template_acte_gestion,";
                 selectQuery += " @TYPE_ACTE)";
                 MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                 command.CommandText = selectQuery;

                 command.Parameters.Clear();
                 command.Parameters.AddWithValue("@id_devis", ActeProp.IdDevis);
                 command.Parameters.AddWithValue("@id", ActeProp.IdProposition);
                 command.Parameters.AddWithValue("@date_execution", ActeProp.DateExecution == null ? DBNull.Value : (object)ActeProp.DateExecution.Value);
                 command.Parameters.AddWithValue("@qte", ActeProp.Qte);
                 command.Parameters.AddWithValue("@Libelle", ActeProp.Libelle);
                 command.Parameters.AddWithValue("@montant", ActeProp.Montant);
                 command.Parameters.AddWithValue("@MontantAvantRemise", ActeProp.MontantAvantRemise);
                 command.Parameters.AddWithValue("@id_template_acte_gestion", ActeProp.Id);
                 command.Parameters.AddWithValue("@TYPE_ACTE", ActeProp.IdTemplateActePG);

             


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

             }*/
        }

        public static void DeleteDevis_Comment_TK(int Iddevis, CommTraitement p_com)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {





                //Suppression des matériaux
                string selectquery = "delete from base_devis_mat WHERE base_devis_mat.ID =  @idcomm";



                MySqlCommand commandt = new MySqlCommand(selectquery, connection, transaction);

                commandt.Parameters.AddWithValue("@idcomm", p_com.Id);


                commandt.ExecuteNonQuery();



                //Suppression des autres personne
                selectquery = "delete from base_devis_autrepers  WHERE base_devis_autrepers.ID_COMM_DEVIS = @idcomm";

                commandt.CommandText = selectquery;

                commandt.ExecuteNonQuery();

                //Suppression des Actes (Supp, Radio et Photos)
                selectquery = "delete from base_devis_actes_tk WHERE base_devis_actes_tk.ID = @idcomm";

                commandt.CommandText = selectquery;

                commandt.ExecuteNonQuery();



                //Suppression des Comments
                selectquery = "delete from devis_comments WHERE devis_comments.ID  = @idcomm";

                commandt.CommandText = selectquery;

                commandt.ExecuteNonQuery();







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
        public static void insertEcheancesDevis_TK(CommTraitement com, TempEcheanceDefinition ted)
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
                string selectQuery = "select MAX(id)+1 as ID from bas_echeances_devis";
                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                object obj = command.ExecuteScalar();
                if (obj == DBNull.Value)
                    ted.Id = 1;
                else
                    ted.Id = Convert.ToInt32(obj);

                selectQuery = @"insert into bas_echeances_devis (id, 
                                                                 ID_SEM_PROPOSE,
                                                                 MONTANT,
                                                                 DTEECHEANCE,
                                                                 LIBELLE,
                                                                 PARPRELEVEMENT,
                                                                 PARVIREMENT,
                                                                 ID_DEVIS_COMMENT,TYPEPAYEUR)
                                                        values (@id, 
                                                                 @ID_SEM_PROPOSE,
                                                                 @MONTANT,
                                                                 @DTEECHEANCE,
                                                                 @LIBELLE,
                                                                 @PARPRELEVEMENT,
                                                                 @PARVIREMENT,
                                                                 @ID_DEVIS_COMMENT,@TYPEPAYEUR)";


                command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", ted.Id);
                command.Parameters.AddWithValue("@ID_SEM_PROPOSE", 0);
                command.Parameters.AddWithValue("@MONTANT", ted.Montant);
                command.Parameters.AddWithValue("@DTEECHEANCE", ted.DAteEcheance);
                command.Parameters.AddWithValue("@LIBELLE", ted.Libelle);
                command.Parameters.AddWithValue("@PARPRELEVEMENT", ted.ParPrelevement);
                command.Parameters.AddWithValue("@PARVIREMENT", ted.ParVirement);
                command.Parameters.AddWithValue("@ID_DEVIS_COMMENT", com.Id);
                command.Parameters.AddWithValue("@TYPEPAYEUR", ted.payeur);



                command.ExecuteNonQuery();



            }
            catch (System.SystemException ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                transaction.Commit();
                connection.Close();
            }

        }





        public static void InsertDevis_Comment_TK(int id_devis, CommTraitement p_com)
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
                string selectQuery = "select MAX(id)+1 as NEWID from devis_comments";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                object res = command.ExecuteScalar();
                p_com.Id = id_devis;
                if (res is DBNull)
                    p_com.Id = 1;
                else
                    p_com.Id = Convert.ToInt32(command.ExecuteScalar());

                selectQuery = @"insert into devis_comments (id, 
                                            ID_DEVIS, 
                                            id_acte,
                                            ID_PRATICIEN ,
                                            ID_ASSISTANTE ,
                                            ID_SECRETAIRE ,
                                            DATE_COMM,
                                            MONTANT,
                                            MONTANTAVANTPROPOSITION ,NBJOURS, QTE,REMBMUTUELLE,PARTPATIENT,DESACTIVE,dents
                                         )
                        values (@id, 
                                @ID_DEVIS, 
                                @id_acte,
                                @ID_PRATICIEN ,
                                @ID_ASSISTANTE ,
                                @ID_SECRETAIRE ,
                                @DATE_COMM,
                                @MONTANT,
                                @MONTANTAVANTPROPOSITION,@NBJOURS, @QTE,@REMBMUTUELLE,@PARTPATIENT,@DESACTIVE,@dents)";

                command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", p_com.Id);
                command.Parameters.AddWithValue("@ID_DEVIS", id_devis);
                command.Parameters.AddWithValue("@id_acte", p_com.Acte.id_acte);

                command.Parameters.AddWithValue("@ID_PRATICIEN", p_com.IdPraticien);
                command.Parameters.AddWithValue("@ID_ASSISTANTE", p_com.IdAssistante);
                command.Parameters.AddWithValue("@ID_SECRETAIRE", p_com.IdSecretaire);
                command.Parameters.AddWithValue("@DATE_COMM", p_com.DatePrevisionnnelle);
                command.Parameters.AddWithValue("@MONTANT", p_com.Acte.prix_traitement);
                command.Parameters.AddWithValue("@MONTANTAVANTPROPOSITION", p_com.Acte.prix_acte);
                command.Parameters.AddWithValue("@NBJOURS", p_com.NbJours);
                command.Parameters.AddWithValue("@QTE", p_com.Acte.quantite);
                command.Parameters.AddWithValue("@REMBMUTUELLE", p_com.RembMutuelle);
                command.Parameters.AddWithValue("@PARTPATIENT", p_com.partPatient);
                command.Parameters.AddWithValue("@DESACTIVE", p_com.desactive);
                command.Parameters.AddWithValue("@dents", p_com.dents);
                command.ExecuteNonQuery();
            }
            catch (System.SystemException ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                transaction.Commit();
                connection.Close();
            }

        }
        public static void Insert_Devis_Materiels_TK(List<ActePGPropose> ActeProp)
        {



            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "";
                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);



                selectQuery = "insert into base_devis_mat (id, ";
                selectQuery += "                            qte, ";
                selectQuery += "                            id_materiel, ";
                selectQuery += "                            MONTANTAVANTREMISE, ";
                selectQuery += "                            MONTANT,desactive) ";
                selectQuery += " values (@id, ";
                selectQuery += "         @qte, ";
                selectQuery += "         @id_materiel, ";
                selectQuery += "         @MONTANTAVANTREMISE, ";
                selectQuery += "         @MONTANT,@desactive) ";



                command.CommandText = selectQuery;

                foreach (ActePGPropose cr in ActeProp)
                {

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", cr.Id);
                    command.Parameters.AddWithValue("@qte", cr.Qte);
                    command.Parameters.AddWithValue("@id_materiel", cr.IdTemplateActePG);
                    command.Parameters.AddWithValue("@MONTANTAVANTREMISE", cr.MontantAvantRemise);
                    command.Parameters.AddWithValue("@MONTANT", cr.Montant);
                    command.Parameters.AddWithValue("@desactive", cr.desactive);

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
        public static DataTable getActesDevis(int id_devis)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
                // MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    //famille_Traitement

                    string selectQuery = "select * FROM base_devis_actes TC where TC.ID_DEVIS = @ID_DEVIS";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection);
                    command.Parameters.AddWithValue("@ID_DEVIS", id_devis);
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
        }

        public static void InsertDevis_TK(Devis_TK devis)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();


            string selectQueryId = "select max(Id)+1 as ID from base_devis";
            MySqlCommand commandid = new MySqlCommand(selectQueryId, connection, transaction);
            object id = commandid.ExecuteScalar();
            if (id == DBNull.Value)
                devis.Id = 1;
            else
                devis.Id = Convert.ToInt32(id);



            try
            {
                string selectQuery = "insert into base_devis (id, ";
                selectQuery += "                            dateproposition, ";
                selectQuery += "                            dateacceptation, ";
                selectQuery += "                            DATEECHEANCE,  ";
                selectQuery += "                            DATEDEBUTTRAITEMENT,  ";
                selectQuery += "                            DATEFINTRAITEMENT,  ";
                selectQuery += "                            DateArchivage,  ";
                selectQuery += "                            ID_TRAITEMENT,  ";
                selectQuery += "                            id_patient,";
                selectQuery += "                            MONTANTPROPOSE,";
                selectQuery += "                            MONTANTDOCTEUR,";
                selectQuery += "                            MONTANTAVANTPROPOSITION,TITRE,ECHEANCIER_DOCTEUR,REMBMUTUELLE,PARTPATIENT,localisation)";
                selectQuery += " values (@id, ";
                selectQuery += "                            @dateproposition, ";
                selectQuery += "                            @dateacceptation, ";
                selectQuery += "                            @DATEECHEANCE,  ";
                selectQuery += "                            @DATEDEBUTTRAITEMENT,  ";
                selectQuery += "                            @DATEFINTRAITEMENT,  ";
                selectQuery += "                            @DateArchivage,  ";
                selectQuery += "                            @ID_TRAITEMENT,  ";
                selectQuery += "                            @id_patient, ";
                selectQuery += "                            @MONTANTPROPOSE,  ";
                selectQuery += "                            @MONTANTDOCTEUR,  ";
                selectQuery += "                            @MONTANTAVANTPROPOSITION,@TITRE,@ECHEANCIER_DOCTEUR,@REMBMUTUELLE,@PARTPATIENT,@localisation)";




                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id", devis.Id);
                command.Parameters.AddWithValue("@dateproposition", devis.DateProposition);
                command.Parameters.AddWithValue("@DateArchivage", devis.DateArchivage == null ? DBNull.Value : (object)devis.DateArchivage.Value);
                command.Parameters.AddWithValue("@dateacceptation", devis.DateAcceptation == null ? DBNull.Value : (object)devis.DateAcceptation.Value);
                command.Parameters.AddWithValue("@DATEECHEANCE", devis.DateEcheance == null ? DBNull.Value : (object)devis.DateEcheance.Value);
                command.Parameters.AddWithValue("@DATEDEBUTTRAITEMENT", devis.DatePrevisionnelDeDebutTraitement);
                command.Parameters.AddWithValue("@DATEFINTRAITEMENT", devis.DatePrevisionnelDeFinTraitement);
                command.Parameters.AddWithValue("@id_patient", devis.IdPatient);
                command.Parameters.AddWithValue("@ID_TRAITEMENT", devis.Id_Traitement);

                command.Parameters.AddWithValue("@MONTANTPROPOSE", devis.Montant == null ? DBNull.Value : (object)devis.Montant);
                command.Parameters.AddWithValue("@MONTANTDOCTEUR", devis.MontantDocteur);

                command.Parameters.AddWithValue("@MONTANTAVANTPROPOSITION", devis.MontantAvantRemise == null ? DBNull.Value : (object)devis.MontantAvantRemise);
                command.Parameters.AddWithValue("@TITRE", devis.Titre);
                command.Parameters.AddWithValue("@ECHEANCIER_DOCTEUR", devis.EcheancierDocteur);

                command.Parameters.AddWithValue("@REMBMUTUELLE", devis.partPatient);
                command.Parameters.AddWithValue("@PARTPATIENT", devis.RembMutuelle);
                command.Parameters.AddWithValue("@localisation", devis.localisationAnatomiuque);

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
        public static DataTable getActesSupDevisByPatient(int idPatient)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
                // MySqlTransaction transaction = connection.BeginTransaction();
                try
                {


                    string selectQuery = "select tc.ID,tc.TYPE_ACTE,tc.ID_ACTE,a.ACTE_LIBELLE, a.ACTE_COULEUR, a.ACTE_DURESTD, tc.MONTANTAVANTREMISE as prix_acte,tc.REMBMUTUELLE,tc.PARTPATIENT,tc.DESACTIVE ,";
                    selectQuery += " tc.MONTANT, tc.QTE, a.NOMBRE_POINTS, a.NOMENCLATURE, a.COTATION, a.COEFFICIENT ,a.ACTE_LIBELLE_ESTIMATION, a.ACTE_LIBELLE_FACTURE,a.ACTE_BASE_REMBOURSEMENT,a.ACTE_REMBOURSEMENT,a.ACTE_DEPASSEMENT,a.ACTE_CODE_TRANSPOSOTION,a.TARIF ";
                    selectQuery += " FROM base_devis_actes_tk tc ";
                    selectQuery += " LEFT JOIN actes a on a.ID_ACTE = tc.ID_ACTE ";
                    selectQuery += "  left join devis_comments dc on dc.ID = tc.ID ";
                    selectQuery += "  left join base_devis bd on bd.ID = dc.ID_DEVIS ";
                    selectQuery += " where bd.ID_PATIENT = @id ";



                    MySqlCommand command = new MySqlCommand(selectQuery, connection);
                    command.Parameters.AddWithValue("@id", idPatient);
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
        public static DataTable getActesSupDevis(int idComtraitement, string TYPE_ACTE_SUPP = "")
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
                // MySqlTransaction transaction = connection.BeginTransaction();
                try
                {


                    string selectQuery = "select tc.ID_ACTE,a.ACTE_LIBELLE, a.ACTE_COULEUR, a.ACTE_DURESTD, tc.MONTANTAVANTREMISE as prix_acte,tc.REMBMUTUELLE,tc.PARTPATIENT,tc.DESACTIVE ,";
                    selectQuery += " tc.MONTANT, tc.QTE, a.NOMBRE_POINTS, a.NOMENCLATURE, a.COTATION, a.COEFFICIENT ,a.ACTE_LIBELLE_ESTIMATION, a.ACTE_LIBELLE_FACTURE,a.ACTE_BASE_REMBOURSEMENT,a.ACTE_REMBOURSEMENT,a.ACTE_DEPASSEMENT,a.ACTE_CODE_TRANSPOSOTION,a.TARIF ";
                    selectQuery += " FROM base_devis_actes_tk tc ";
                    selectQuery += " LEFT JOIN actes a on a.ID_ACTE = tc.ID_ACTE";
                    selectQuery += " where tc.id = @id";
                    selectQuery += " and tc.TYPE_ACTE = '" + TYPE_ACTE_SUPP + "'";



                    MySqlCommand command = new MySqlCommand(selectQuery, connection);
                    command.Parameters.AddWithValue("@id", idComtraitement);
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

        public static DataTable GetCommDevisMaterielsByPatients(int idPatient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {


                string selectQuery = "select bd.ID,  m.MATERIEL_LIBELLE,  ";
                selectQuery += " BTM.QTE,m.ID_MATERIEL,m.SHORTLIB,m.MATERIEL_COULEUR, BTM.MONTANTAVANTREMISE, BTM.MONTANT , m.ID_FAMILLE_MATERIEL,BTM.PARTPATIENT,BTM.REMBMUTUELLE,m.ACTE_BASE_REMBOURSEMENT,m.ACTE_REMBOURSEMENT,  m.ACTE_DEPASSEMENT,m.ACTE_CODE_TRANSPOSOTION";
                selectQuery += " from base_devis_mat BTM  ";
                selectQuery += " left join materiels m on m.ID_MATERIEL  =  BTM.ID_MATERIEL  ";
                selectQuery += "  left join devis_comments dc on dc.ID = BTM.ID ";
                selectQuery += "  left join base_devis bd on bd.ID = dc.ID_DEVIS ";
                selectQuery += " where bd.ID_PATIENT = @id ";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@id", idPatient);

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

        public static DataTable GetCommDevisMateriels(CommTraitement com)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            // MySqlTransaction transaction = connection.BeginTransaction();
            try
            {


                string selectQuery = "select id,  m.MATERIEL_LIBELLE,  ";
                selectQuery += " BTM.QTE,m.ID_MATERIEL,m.SHORTLIB,m.MATERIEL_COULEUR, BTM.MONTANTAVANTREMISE, BTM.MONTANT , m.ID_FAMILLE_MATERIEL,BTM.PARTPATIENT,BTM.REMBMUTUELLE,m.ACTE_BASE_REMBOURSEMENT,m.ACTE_REMBOURSEMENT,  m.ACTE_DEPASSEMENT,m.ACTE_CODE_TRANSPOSOTION";
                selectQuery += " from base_devis_mat BTM  ";
                selectQuery += " left join materiels m on m.ID_MATERIEL  =  BTM.ID_MATERIEL  ";
                selectQuery += " where id = @ID";
                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@ID", com.Id);

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

        public static void SaveActesSuppDevis(CommTraitement comm, string TYPE_ACTE_SUP = "")
        {
            if (comm.ActesSupp == null) return;
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "";
                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);


                selectQuery = "delete from base_devis_actes_tk where ID = @id";

                selectQuery += " AND TYPE_ACTE='" + TYPE_ACTE_SUP + "'";




                command.Parameters.AddWithValue("@id", comm.Id);
                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                object obj = command.ExecuteNonQuery();

                selectQuery = "insert into base_devis_actes_tk (";
                selectQuery += " id, ";
                selectQuery += " montant, ";
                selectQuery += " MontantAvantRemise, ";
                selectQuery += " id_acte,TYPE_ACTE,date_execution,qte,REMBMUTUELLE,PARTPATIENT,DESACTIVE )";
                selectQuery += " values (@id,";
                selectQuery += " @montant, ";
                selectQuery += " @MontantAvantRemise, ";
                selectQuery += " @id_acte,@TYPE_ACTE,@date_execution,@qte,@REMBMUTUELLE,@PARTPATIENT,@DESACTIVE)";



                command.CommandText = selectQuery;



                List<CommActesTraitement> TmpActesSupp = new List<CommActesTraitement>();
                if (TYPE_ACTE_SUP == "R")
                {
                    TmpActesSupp = comm.Radios;
                }
                else if (TYPE_ACTE_SUP == "P")
                {
                    TmpActesSupp = comm.photos;
                }
                else
                {
                    TmpActesSupp = comm.ActesSupp;
                }
                foreach (CommActesTraitement cr in TmpActesSupp)
                {
                    CommActesDevis rr = new CommActesDevis(cr);
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", comm.Id);
                    command.Parameters.AddWithValue("@MontantAvantRemise", rr.prix_acte);
                    command.Parameters.AddWithValue("@montant", rr.prix_traitement);
                    command.Parameters.AddWithValue("@id_acte", rr.IdActe);
                    command.Parameters.AddWithValue("@TYPE_ACTE", TYPE_ACTE_SUP);
                    command.Parameters.AddWithValue("@date_execution", comm.DatePrevisionnnelle.Value == null ? DBNull.Value : (object)comm.DatePrevisionnnelle.Value);
                    command.Parameters.AddWithValue("@qte", cr.Qte);
                    command.Parameters.AddWithValue("@REMBMUTUELLE", rr.RembMutuelle);
                    command.Parameters.AddWithValue("@PARTPATIENT", rr.partPatient);
                    command.Parameters.AddWithValue("@DESACTIVE", comm.desactive == true ? true : cr.desactive);
                    //command.Parameters.Clear();
                    //command.Parameters.AddWithValue("@id", comm.Id);
                    //command.Parameters.AddWithValue("@ID_ACTE", cr.IdActe);
                    //command.Parameters.AddWithValue("@TYPE_ACTE_SUPP", TYPE_ACTE_SUP);
                    //command.Parameters.AddWithValue("@PRIX_TRAITEMENT", cr.prix_traitement);


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

        public static void UpdateActeDevis_TK(CommTraitement comm)
        {



            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "update devis_comments";
                selectQuery += " set ID_ACTE = @ID_ACTE ";
                selectQuery += " , ID_PRATICIEN = @ID_PRATICIEN ";
                selectQuery += " , ID_ASSISTANTE = @ID_ASSISTANTE ";
                selectQuery += " , ID_SECRETAIRE = @ID_SECRETAIRE ";
                selectQuery += " , NBJOURS = @NBJOURS ";
                selectQuery += " , PARTPATIENT = @PARTPATIENT ";
                selectQuery += " , REMBMUTUELLE = @REMBMUTUELLE ";
                selectQuery += " , DESACTIVE = @DESACTIVE ";
                selectQuery += " where (id = @id)";




                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;


                command.Parameters.AddWithValue("@id", comm.Id);
                command.Parameters.AddWithValue("@id_acte", comm.Acte == null ? DBNull.Value : (object)comm.Acte.id_acte);

                command.Parameters.AddWithValue("@id_praticien", comm.praticien == null ? DBNull.Value : (object)comm.praticien.Id);
                command.Parameters.AddWithValue("@ID_ASSISTANTE", comm.Assistante == null ? DBNull.Value : (object)comm.Assistante.Id);
                command.Parameters.AddWithValue("@ID_SECRETAIRE", comm.Secretaire == null ? DBNull.Value : (object)comm.Secretaire.Id);
                command.Parameters.AddWithValue("@NBJOURS", comm.NbJours == null ? 0 : comm.NbJours);
                command.Parameters.AddWithValue("@PARTPATIENT", comm.partPatient);
                command.Parameters.AddWithValue("@REMBMUTUELLE", comm.RembMutuelle);
                command.Parameters.AddWithValue("@DESACTIVE", comm.desactive);
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
        public static DataTable GetDevisAutrePersonneByPatient(int idPatient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = "select ID_COMM_DEVIS, ";
                selectQuery += "        ID_CORRESPONDANT, ";
                selectQuery += "        personne.per_nom , ";
                selectQuery += "        personne.per_prenom  ";
                selectQuery += " from base_devis_autrepers";
                selectQuery += " inner join personne on personne.ID_PERSONNE=ID_CORRESPONDANT";

                selectQuery += " where  personne.ID_PERSONNE = @id";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@id", idPatient);

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
        public static DataTable GetDevisAutrePersonne(CommTraitement com)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            //  MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select ID_COMM_DEVIS, ";
                selectQuery += "        ID_CORRESPONDANT, ";
                selectQuery += "        personne.per_nom , ";
                selectQuery += "        personne.per_prenom  ";
                selectQuery += " from base_devis_autrepers";
                selectQuery += " inner join personne on personne.ID_PERSONNE=ID_CORRESPONDANT";

                selectQuery += " where ID_COMM_DEVIS = @ID_COMM_DEVIS";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@ID_COMM_DEVIS", com.Id);

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
        public static void setDevisMateriels(CommTraitement comm)
        {

            if (comm.Materiels == null) return;

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "";
                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);


                selectQuery = "delete from base_devis_mat where ID = @id";

                command.Parameters.AddWithValue("@id", comm.Id);
                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                object obj = command.ExecuteNonQuery();



                selectQuery = "insert into base_devis_mat (id, ";
                selectQuery += "                            qte, ";
                selectQuery += "                            id_materiel, ";
                selectQuery += "                            MONTANTAVANTREMISE, ";
                selectQuery += "                            MONTANT,desactive) ";
                selectQuery += " values (@id, ";
                selectQuery += "         @qte, ";
                selectQuery += "         @id_materiel, ";
                selectQuery += "         @MONTANTAVANTREMISE, ";
                selectQuery += "         @MONTANT,@desactive) ";



                command.CommandText = selectQuery;

                foreach (CommMaterielTraitement cr in comm.Materiels)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", comm.Id);
                    command.Parameters.AddWithValue("@qte", cr.Qte);
                    command.Parameters.AddWithValue("@id_materiel", cr.idMateriel);
                    command.Parameters.AddWithValue("@MONTANTAVANTREMISE", cr.prix_materiel);
                    command.Parameters.AddWithValue("@MONTANT", cr.prix_traitement);
                    command.Parameters.AddWithValue("@desactive", cr.desactive);

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
        public static void InsertDevis(Devis devis)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();


            string selectQueryId = "select max(Id)+1 as ID from base_devis";
            MySqlCommand commandid = new MySqlCommand(selectQueryId, connection, transaction);
            object id = commandid.ExecuteScalar();
            if (id == DBNull.Value)
                devis.Id = 1;
            else
                devis.Id = Convert.ToInt32(id);



            try
            {
                string selectQuery = "insert into base_devis (id, ";
                selectQuery += "                            dateproposition, ";
                selectQuery += "                            dateacceptation, ";
                selectQuery += "                            DATEECHEANCE,  ";
                selectQuery += "                            DATEDEBUTTRAITEMENT,  ";
                selectQuery += "                            DATEFINTRAITEMENT,  ";
                selectQuery += "                            DateArchivage,  ";
                selectQuery += "                            TypeDevis,  ";
                selectQuery += "                            id_patient, ";
                selectQuery += "                            id_objet_baseview)";
                selectQuery += " values (@id, ";
                selectQuery += "                            @dateproposition, ";
                selectQuery += "                            @dateacceptation, ";
                selectQuery += "                            @DATEECHEANCE,  ";
                selectQuery += "                            @DATEDEBUTTRAITEMENT,  ";
                selectQuery += "                            @DATEFINTRAITEMENT,  ";
                selectQuery += "                            @DateArchivage,  ";
                selectQuery += "                            @TypeDevis,  ";
                selectQuery += "                            @id_patient, ";
                selectQuery += "                            @id_objet_baseview)";




                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id", devis.Id);
                command.Parameters.AddWithValue("@dateproposition", devis.DateProposition);
                command.Parameters.AddWithValue("@DateArchivage", devis.DateArchivage == null ? DBNull.Value : (object)devis.DateArchivage.Value);
                command.Parameters.AddWithValue("@dateacceptation", devis.DateAcceptation == null ? DBNull.Value : (object)devis.DateAcceptation.Value);
                command.Parameters.AddWithValue("@DATEECHEANCE", devis.DateEcheance == null ? DBNull.Value : (object)devis.DateEcheance.Value);
                command.Parameters.AddWithValue("@DATEDEBUTTRAITEMENT", devis.DatePrevisionnelDeDebutTraitement);
                command.Parameters.AddWithValue("@DATEFINTRAITEMENT", devis.DatePrevisionnelDeFinTraitement);
                command.Parameters.AddWithValue("@id_patient", devis.IdPatient);
                command.Parameters.AddWithValue("@id_objet_baseview", devis.IdObjetBaseView == -1 ? DBNull.Value : (object)devis.IdObjetBaseView);
                command.Parameters.AddWithValue("@TypeDevis", devis.TypeDevis);

                command.Parameters.AddWithValue("@MONTANTPROPOSE", devis.Montant == null ? DBNull.Value : (object)devis.Montant);
                command.Parameters.AddWithValue("@MONTANTAVANTPROPOSITION", devis.MontantAvantRemise == null ? DBNull.Value : (object)devis.MontantAvantRemise);

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

        public static void ArchiverDevis_TK(Devis_TK devis)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();



            try
            {



                string selectQuery = "update base_devis";
                selectQuery += "    set DATEARCHIVAGE = @DATEARCHIVAGE,";
                selectQuery += "    ARCHIVEPAR = @ARCHIVEPAR,";
                selectQuery += "    REMARQUEARCHIVAGE = @REMARQUEARCHIVAGE,";
                selectQuery += "    EMPLACEMENTARCHIVAGE = @EMPLACEMENTARCHIVAGE";
                selectQuery += " where (id = @id)";





                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id", devis.Id);
                command.Parameters.AddWithValue("@DATEARCHIVAGE", devis.DateArchivage);
                command.Parameters.AddWithValue("@ARCHIVEPAR", devis.ArchivePar == null ? DBNull.Value : (object)devis.ArchivePar.Id);
                command.Parameters.AddWithValue("@REMARQUEARCHIVAGE", devis.RemarqueArchivage);
                command.Parameters.AddWithValue("@EMPLACEMENTARCHIVAGE", devis.EmplacementArchivage);

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
        public static void ArchiverDevis(Devis devis)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();



            try
            {



                string selectQuery = "update base_devis";
                selectQuery += "    set DATEARCHIVAGE = @DATEARCHIVAGE,";
                selectQuery += "    ARCHIVEPAR = @ARCHIVEPAR,";
                selectQuery += "    REMARQUEARCHIVAGE = @REMARQUEARCHIVAGE,";
                selectQuery += "    EMPLACEMENTARCHIVAGE = @EMPLACEMENTARCHIVAGE";
                selectQuery += " where (id = @id)";





                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id", devis.Id);
                command.Parameters.AddWithValue("@DATEARCHIVAGE", devis.DateArchivage);
                command.Parameters.AddWithValue("@ARCHIVEPAR", devis.ArchivePar == null ? DBNull.Value : (object)devis.ArchivePar.Id);
                command.Parameters.AddWithValue("@REMARQUEARCHIVAGE", devis.RemarqueArchivage);
                command.Parameters.AddWithValue("@EMPLACEMENTARCHIVAGE", devis.EmplacementArchivage);

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


        public static void AccepterDevis(int IdDevis, DateTime dateAcceptation)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();



            try
            {



                string selectQuery = "update base_devis";
                selectQuery += "    set dateacceptation = @dateacceptation";
                selectQuery += " where (id = @id)";





                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id", IdDevis);
                command.Parameters.AddWithValue("@dateacceptation", dateAcceptation);

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

        public static string FindHistoricFile(Devis devis)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select fichier from base_histo_courrier";
                selectQuery += " inner join base_courrier_attributs on base_histo_courrier.id = base_courrier_attributs.id_histo_courrier";
                selectQuery += " where base_courrier_attributs.nom_attribut = 'ID_Devis' and  base_courrier_attributs.value_attribut = @id";



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", devis.Id);


                object obj = command.ExecuteScalar();


                if (obj == null)
                    return "";
                else
                {
                    return (string)obj;
                }

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


        public static void setDevisAutrePersonnes(CommTraitement comm)
        {

            if (comm.AutrePersonnes == null) return;

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "";
                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);


                selectQuery = "delete from base_devis_autrepers where ID_COMM_DEVIS = @id";

                command.Parameters.AddWithValue("@id", comm.Id);
                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                object obj = command.ExecuteNonQuery();



                selectQuery = "insert into base_devis_autrepers (ID_COMM_DEVIS, ";
                selectQuery += "                            ID_CORRESPONDANT)";
                selectQuery += " values (@id, ";
                selectQuery += "         @ID_CORRESPONDANT)";





                command.CommandText = selectQuery;

                foreach (CommAutrePersonne cr in comm.AutrePersonnes)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", comm.Id);
                    command.Parameters.AddWithValue("@ID_CORRESPONDANT", cr.IdCorrespondant);

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

        public static void AddActeDevis(int id_devis, CommTraitement p_com)
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
                string selectQuery = "select MAX(id)+1 as NEWID from devis_comments";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                object res = command.ExecuteScalar();
                p_com.Id = id_devis;
                if (res is DBNull)
                    p_com.Id = 1;
                else
                    p_com.Id = Convert.ToInt32(command.ExecuteScalar());

                selectQuery = @"insert into devis_comments (id, 
                                            id_devis, 
                                            id_acte,
                                            ID_PRATICIEN ,
                                            ID_ASSISTANTE ,
                                            ID_SECRETAIRE ,
                                            DATE_COMM,
                                            PRIX_ACTE 
                                         )
                        values (@id, 
                                @id_devis, 
                                @id_acte,
                                @ID_PRATICIEN ,
                                @ID_ASSISTANTE ,
                                @ID_SECRETAIRE ,
                                @DATE_COMM,
                                @PRIX_ACTE)";

                command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", p_com.Id);
                command.Parameters.AddWithValue("@id_devis", id_devis);
                command.Parameters.AddWithValue("@id_acte", p_com.Acte.id_acte);

                command.Parameters.AddWithValue("@ID_PRATICIEN", p_com.IdPraticien);
                command.Parameters.AddWithValue("@ID_ASSISTANTE", p_com.IdAssistante);
                command.Parameters.AddWithValue("@ID_SECRETAIRE", p_com.IdSecretaire);
                command.Parameters.AddWithValue("@DATE_COMM", p_com.DatePrevisionnnelle);
                command.Parameters.AddWithValue("@PRIX_ACTE", p_com.Acte.prix_acte);



                command.ExecuteNonQuery();
            }
            catch (System.SystemException ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                transaction.Commit();
                connection.Close();
            }

        }
    }
}
