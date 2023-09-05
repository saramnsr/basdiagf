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

        public static void InsertEntentePrealableWithoutDiag(EntentePrealable ententepre)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQueryId = "select max(Id)+1 as ID from envoi_entente";
                MySqlCommand commandid = new MySqlCommand(selectQueryId, connection, transaction);
                object id = commandid.ExecuteScalar();
                if (id == DBNull.Value)
                    ententepre.IdModele = 1;
                else
                    ententepre.IdModele = Convert.ToInt32(id);



                string selectQuery = "insert into envoi_entente (id, ee_patient, ";
                selectQuery += "  ee_numdate, ";
                selectQuery += "  ee_debuttraitement, ";
                selectQuery += "  ee_surveillance, ";
                selectQuery += "  ee_suite, ";
                selectQuery += "  ee_numsemestre, ";
                selectQuery += "  ee_contention, ";
                selectQuery += "  ee_annee, ";
                selectQuery += "  ee_autre, ";
                selectQuery += "  ee_autretext, ";
                selectQuery += "  ee_id_plan, ";
                selectQuery += "  ee_immat, ";
                selectQuery += "  ee_dateprop, ";
                selectQuery += "  ee_dateimpression, ";
                selectQuery += "  ee_cotation, ";
                selectQuery += "  ee_dateaccord, ";
                selectQuery += "  ee_devis, ";
                selectQuery += "  ee_rmo, ";
                selectQuery += "  ee_traitement1, ";
                selectQuery += "  ee_traitement2, ";
                selectQuery += "  ee_traitement3, ";
                selectQuery += "  ee_commentaire1, ";
                selectQuery += "  ee_commentaire2, ";
                selectQuery += "  ee_commentaire3, ";
                selectQuery += "  ID_MODELE_ENVOI, ";
                selectQuery += "  ID_PRATICIEN, ";
                selectQuery += "  ee_libsemestre)";
                selectQuery += " values (@id, @ee_patient, ";
                selectQuery += "        @ee_numdate, ";
                selectQuery += "        @ee_debuttraitement, ";
                selectQuery += "        @ee_surveillance, ";
                selectQuery += "        @ee_suite, ";
                selectQuery += "        @ee_numsemestre, ";
                selectQuery += "        @ee_contention, ";
                selectQuery += "        @ee_annee, ";
                selectQuery += "        @ee_autre, ";
                selectQuery += "        @ee_autretext, ";
                selectQuery += "        @ee_id_plan, ";
                selectQuery += "        @ee_immat, ";
                selectQuery += "        @ee_dateprop, ";
                selectQuery += "        @ee_dateimpression, ";
                selectQuery += "        @ee_cotation, ";
                selectQuery += "        @ee_dateaccord, ";
                selectQuery += "        @ee_devis, ";
                selectQuery += "        @ee_rmo, ";
                selectQuery += "        @ee_traitement1, ";
                selectQuery += "        @ee_traitement2, ";
                selectQuery += "        @ee_traitement3, ";
                selectQuery += "        @ee_commentaire1, ";
                selectQuery += "        @ee_commentaire2, ";
                selectQuery += "        @ee_commentaire3, ";
                selectQuery += "        @ID_MODELE_ENVOI, ";
                selectQuery += "        @ID_PRATICIEN, ";
                selectQuery += "        @ee_libsemestre)";



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id", ententepre.IdModele);
                command.Parameters.AddWithValue("@ee_patient", ententepre.patient.Id);
                command.Parameters.AddWithValue("@ee_numdate", -1);
                command.Parameters.AddWithValue("@ee_debuttraitement", ententepre.typetraitement == EntentePrealable.TypeDeTraitement.Debut);
                command.Parameters.AddWithValue("@ee_surveillance", ententepre.typetraitement == EntentePrealable.TypeDeTraitement.Surveillance);
                command.Parameters.AddWithValue("@ee_suite", ententepre.typetraitement == EntentePrealable.TypeDeTraitement.Semestre);
                command.Parameters.AddWithValue("@ee_numsemestre", ententepre.Semestre);
                command.Parameters.AddWithValue("@ee_contention", ententepre.typetraitement == EntentePrealable.TypeDeTraitement.Contention);
                command.Parameters.AddWithValue("@ee_annee", ententepre.Contention);
                command.Parameters.AddWithValue("@ee_autre", ententepre.typetraitement == EntentePrealable.TypeDeTraitement.Autre);
                command.Parameters.AddWithValue("@ee_autretext", ententepre.Autre);
                command.Parameters.AddWithValue("@ee_id_plan", -1);
                command.Parameters.AddWithValue("@ee_immat", ententepre.ImmatAssure);
                command.Parameters.AddWithValue("@ee_dateprop", ententepre.dateProposition);
                command.Parameters.AddWithValue("@EE_DATEIMPRESSION", ententepre.DateImpression);
                command.Parameters.AddWithValue("@ee_dateaccord", ententepre.DateAccord == null ? DBNull.Value : (object)ententepre.DateAccord.Value);
                command.Parameters.AddWithValue("@ee_cotation", ententepre.cotationDesActes);

                command.Parameters.AddWithValue("@ee_devis", ententepre.IsDevisSigned);
                if (ententepre.ReferenceNationalOpposable == EntentePrealable.RNO.R)
                    command.Parameters.AddWithValue("@ee_rmo", 0);
                else if (ententepre.ReferenceNationalOpposable == EntentePrealable.RNO.HR)
                    command.Parameters.AddWithValue("@ee_rmo", 1);
                else if (ententepre.ReferenceNationalOpposable == EntentePrealable.RNO.None)
                    command.Parameters.AddWithValue("@ee_rmo", -1);

                command.Parameters.AddWithValue("@ee_traitement1", ententepre.PlanDeTraitement);
                command.Parameters.AddWithValue("@ee_traitement2", "");
                command.Parameters.AddWithValue("@ee_traitement3", "");
                command.Parameters.AddWithValue("@ee_commentaire1", ententepre.Commentaires);
                command.Parameters.AddWithValue("@ee_commentaire2", "");
                command.Parameters.AddWithValue("@ee_commentaire3", "");
                command.Parameters.AddWithValue("@ID_MODELE_ENVOI", ententepre.IdDiag);
                command.Parameters.AddWithValue("@ID_PRATICIEN", ententepre.Praticien.Id);

                command.Parameters.AddWithValue("@ee_libsemestre", ententepre.Semestre);



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

        public static void DeleteEntentePrealableWithoutDiag(EntentePrealable ententepre)
        {



            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "delete from envoi_entente";
                selectQuery += " where id = @id";




                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id", ententepre.IdModele);


                command.ExecuteNonQuery();


                selectQuery = "update base_traitement set ID_DEP = NULL ";
                selectQuery += " where ID_DEP = @id";




                command.CommandText = selectQuery;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", ententepre.IdModele);


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

        public static void UpdateEntentePrealableWithoutDiag(EntentePrealable ententepre)
        {



            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "update envoi_entente";
                selectQuery += " set ee_patient = @ee_patient,";
                selectQuery += "    ee_numdate = @ee_numdate,";
                selectQuery += "    ee_debuttraitement = @ee_debuttraitement,";
                selectQuery += "    ee_surveillance = @ee_surveillance,";
                selectQuery += "    ee_suite = @ee_suite,";
                selectQuery += "    ee_numsemestre = @ee_numsemestre,";
                selectQuery += "    ee_contention = @ee_contention,";
                selectQuery += "    ee_annee = @ee_annee,";
                selectQuery += "    ee_autre = @ee_autre,";
                selectQuery += "    ee_autretext = @ee_autretext,";
                selectQuery += "    ee_id_plan = @ee_id_plan,";
                selectQuery += "    ee_immat = @ee_immat,";
                selectQuery += "    ee_dateprop = @ee_dateprop,";
                selectQuery += "    ee_dateimpression = @ee_dateimpression,";
                selectQuery += "    ee_cotation = @ee_cotation,";
                selectQuery += "    ee_dateaccord = @ee_dateaccord,";
                selectQuery += "    ee_devis = @ee_devis,";
                selectQuery += "    ee_rmo = @ee_rmo,";
                selectQuery += "    ee_traitement1 = @ee_traitement1,";
                selectQuery += "    ee_traitement2 = @ee_traitement2,";
                selectQuery += "    ee_traitement3 = @ee_traitement3,";
                selectQuery += "    ee_commentaire1 = @ee_commentaire1,";
                selectQuery += "    ee_commentaire2 = @ee_commentaire2,";
                selectQuery += "    ee_commentaire3 = @ee_commentaire3,";
                selectQuery += "    ID_MODELE_ENVOI = @ID_MODELE_ENVOI,";
                selectQuery += "    ee_libsemestre = @ee_libsemestre";
                selectQuery += " where id = @id";




                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@ee_patient", ententepre.idPatient);
                command.Parameters.AddWithValue("@id", ententepre.IdModele);
                command.Parameters.AddWithValue("@ee_numdate", -1);
                command.Parameters.AddWithValue("@ee_debuttraitement", ententepre.typetraitement == EntentePrealable.TypeDeTraitement.Debut);
                command.Parameters.AddWithValue("@ee_surveillance", ententepre.typetraitement == EntentePrealable.TypeDeTraitement.Surveillance);
                command.Parameters.AddWithValue("@ee_suite", ententepre.typetraitement == EntentePrealable.TypeDeTraitement.Semestre);
                command.Parameters.AddWithValue("@ee_numsemestre", ententepre.Semestre);
                command.Parameters.AddWithValue("@ee_contention", ententepre.typetraitement == EntentePrealable.TypeDeTraitement.Contention);
                command.Parameters.AddWithValue("@ee_annee", ententepre.Contention);
                command.Parameters.AddWithValue("@ee_autre", ententepre.typetraitement == EntentePrealable.TypeDeTraitement.Autre);
                command.Parameters.AddWithValue("@ee_autretext", ententepre.Autre);
                command.Parameters.AddWithValue("@ee_id_plan", -1);
                command.Parameters.AddWithValue("@ee_immat", ententepre.ImmatAssure);
                command.Parameters.AddWithValue("@ee_dateprop", ententepre.dateProposition);
                command.Parameters.AddWithValue("@ee_dateimpression", ententepre.DateImpression);
                command.Parameters.AddWithValue("@ee_cotation", ententepre.cotationDesActes);
                command.Parameters.AddWithValue("@ee_dateaccord", DBNull.Value);
                command.Parameters.AddWithValue("@ee_devis", ententepre.IsDevisSigned);
                if (ententepre.ReferenceNationalOpposable == EntentePrealable.RNO.R)
                    command.Parameters.AddWithValue("@ee_rmo", 0);
                else if (ententepre.ReferenceNationalOpposable == EntentePrealable.RNO.HR)
                    command.Parameters.AddWithValue("@ee_rmo", 1);
                else if (ententepre.ReferenceNationalOpposable == EntentePrealable.RNO.None)
                    command.Parameters.AddWithValue("@ee_rmo", -1);

                command.Parameters.AddWithValue("@ee_traitement1", ententepre.PlanDeTraitement);
                command.Parameters.AddWithValue("@ee_traitement2", "");
                command.Parameters.AddWithValue("@ee_traitement3", "");
                command.Parameters.AddWithValue("@ee_commentaire1", ententepre.Commentaires);
                command.Parameters.AddWithValue("@ee_commentaire2", "");
                command.Parameters.AddWithValue("@ee_commentaire3", "");
                command.Parameters.AddWithValue("@ID_MODELE_ENVOI", ententepre.IdDiag);
                command.Parameters.AddWithValue("@ee_libsemestre", ententepre.Semestre);


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

        public static DataTable getEntentePrealableWithoutDiag(basePatient pat)
        {
            return getEntentePrealableWithoutDiag(pat.Id);

        }


        public static DataTable getEntentePrealableWithoutDiag(int Idpat)
        {
            if (connection == null) getConnection(); 

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "select id, ee_patient, ";
                selectQuery += "       ee_numdate, ";
                selectQuery += "       ee_debuttraitement, ";
                selectQuery += "       ee_surveillance, ";
                selectQuery += "       ee_suite, ";
                selectQuery += "       ee_numsemestre, ";
                selectQuery += "       ee_contention, ";
                selectQuery += "       ee_annee, ";
                selectQuery += "       ee_autre, ";
                selectQuery += "       ee_autretext, ";
                selectQuery += "       ee_id_plan, ";
                selectQuery += "       ee_immat, ";
                selectQuery += "       ee_dateprop, ";
                selectQuery += "       ee_cotation, ";
                selectQuery += "       ee_dateaccord, ";
                selectQuery += "       ee_devis, ";
                selectQuery += "       ee_rmo, ";
                selectQuery += "       ee_traitement1, ";
                selectQuery += "       ee_traitement2, ";
                selectQuery += "       ee_traitement3, ";
                selectQuery += "       ee_commentaire1, ";
                selectQuery += "       ee_commentaire2, ";
                selectQuery += "       ee_commentaire3, ";
                selectQuery += "       ee_libsemestre, ";
                selectQuery += "       EE_DATEIMPRESSION, ";
                selectQuery += "       ID_PRATICIEN,";
                selectQuery += "       ID_MODELE_ENVOI,";

                selectQuery += "       id";
                selectQuery += " from envoi_entente";
                selectQuery += " where ee_patient=@ee_patient";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@ee_patient", Idpat);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                return dt;

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

        public static DataRow getEntentePrealableWithoutDiag(int IdPatient, EntentePrealable.TypeDeTraitement type)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "select id, ee_patient, ";
                selectQuery += "       ee_numdate, ";
                selectQuery += "       ee_debuttraitement, ";
                selectQuery += "       ee_surveillance, ";
                selectQuery += "       ee_suite, ";
                selectQuery += "       ee_numsemestre, ";
                selectQuery += "       ee_contention, ";
                selectQuery += "       ee_annee, ";
                selectQuery += "       ee_autre, ";
                selectQuery += "       ee_autretext, ";
                selectQuery += "       ee_id_plan, ";
                selectQuery += "       ee_immat, ";
                selectQuery += "       ee_dateprop, ";
                selectQuery += "       ee_cotation, ";
                selectQuery += "       ee_dateaccord, ";
                selectQuery += "       ee_devis, ";
                selectQuery += "       ee_rmo, ";
                selectQuery += "       ee_traitement1, ";
                selectQuery += "       ee_traitement2, ";
                selectQuery += "       ee_traitement3, ";
                selectQuery += "       ee_commentaire1, ";
                selectQuery += "       ee_commentaire2, ";
                selectQuery += "       ee_commentaire3, ";
                selectQuery += "       ee_libsemestre, ";
                selectQuery += "       ee_DateImpression, ";
                selectQuery += "       id,";
                selectQuery += "       ID_PRATICIEN";

                selectQuery += " from envoi_entente";
                selectQuery += " where ee_patient=@ee_patient";

                if (type == EntentePrealable.TypeDeTraitement.Debut)
                    selectQuery += " and ee_debuttraitement=1";
                if (type == EntentePrealable.TypeDeTraitement.Autre)
                    selectQuery += " and ee_autre=1";
                if (type == EntentePrealable.TypeDeTraitement.Contention)
                    selectQuery += " and ee_contention=1";
                if (type == EntentePrealable.TypeDeTraitement.Semestre)
                    selectQuery += " and ee_suite=1";
                if (type == EntentePrealable.TypeDeTraitement.Surveillance)
                    selectQuery += " and ee_surveillance=1";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@ee_patient", IdPatient);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                    return null;
                else
                    return dt.Rows[0];

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

        public static DataRow getEntentePrealableWithoutDiag(int IdEntente, int IdPatient)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "select id, ee_patient, ";
                selectQuery += "       ee_numdate, ";
                selectQuery += "       ee_debuttraitement, ";
                selectQuery += "       ee_surveillance, ";
                selectQuery += "       ee_suite, ";
                selectQuery += "       ee_numsemestre, ";
                selectQuery += "       ee_contention, ";
                selectQuery += "       ee_annee, ";
                selectQuery += "       ee_autre, ";
                selectQuery += "       ee_autretext, ";
                selectQuery += "       ee_id_plan, ";
                selectQuery += "       ee_immat, ";
                selectQuery += "       ee_dateprop, ";
                selectQuery += "       ee_cotation, ";
                selectQuery += "       ee_dateaccord, ";
                selectQuery += "       ee_devis, ";
                selectQuery += "       ee_rmo, ";
                selectQuery += "       ee_traitement1, ";
                selectQuery += "       ee_traitement2, ";
                selectQuery += "       ee_traitement3, ";
                selectQuery += "       ee_commentaire1, ";
                selectQuery += "       ee_commentaire2, ";
                selectQuery += "       ee_commentaire3, ";
                selectQuery += "       ee_libsemestre, ";
                selectQuery += "       ee_DateImpression, ";
                selectQuery += "       id,";
                selectQuery += "       ID_PRATICIEN,";
                selectQuery += "       ID_MODELE_ENVOI";

                selectQuery += " from envoi_entente";
                selectQuery += " where id=@id and ee_patient=@ee_patient";



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", IdEntente);
                command.Parameters.AddWithValue("@ee_patient", IdPatient);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                    return null;
                else
                    return dt.Rows[0];

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

        public static DataRow getDiagEntentePrealableFromId(int IdDiag)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "select  id, me_patient, ";
                selectQuery += "       me_alvemaxpro, ";
                selectQuery += "       me_alvemaxendo, ";
                selectQuery += "       me_alvemaxsupra, ";
                selectQuery += "       me_alvemaxretro, ";
                selectQuery += "       me_alvemaxexo, ";
                selectQuery += "       me_alvemandpro, ";
                selectQuery += "       me_alvemandendo, ";
                selectQuery += "       me_alvemandinfra, ";
                selectQuery += "       me_alvemandretro, ";
                selectQuery += "       me_alvemandexo, ";
                selectQuery += "       me_basmaxpro, ";
                selectQuery += "       me_basmaxendo, ";
                selectQuery += "       me_basmaxhypo, ";
                selectQuery += "       me_basmaxretro, ";
                selectQuery += "       me_basmaxexo, ";
                selectQuery += "       me_basmandpro, ";
                selectQuery += "       me_basmandendo, ";
                selectQuery += "       me_basmandhyper, ";
                selectQuery += "       me_basmandretro, ";
                selectQuery += "       me_basmandexo, ";
                selectQuery += "       me_mol1, ";
                selectQuery += "       me_mol2, ";
                selectQuery += "       me_mol3, ";
                selectQuery += "       me_moltext, ";
                selectQuery += "       me_can1, ";
                selectQuery += "       me_can2, ";
                selectQuery += "       me_can3, ";
                selectQuery += "       me_cantext, ";
                selectQuery += "       me_occludroit, ";
                selectQuery += "       me_occlugauche, ";
                selectQuery += "       me_occluanter, ";
                selectQuery += "       me_agnesie, ";
                selectQuery += "       me_dentincl, ";
                selectQuery += "       me_malpos, ";
                selectQuery += "       me_dysharmo, ";
                selectQuery += "       me_dysharmodd, ";
                selectQuery += "       me_facteurfonc, ";
                selectQuery += "       pat_objectif_trait2, ";
                selectQuery += "       pat_objectif_comm2";
                selectQuery += " from modele_entente";
                selectQuery += " where id = @Id";
                selectQuery += " order by id desc limit 1";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@Id", IdDiag);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                    return null;
                else
                    return dt.Rows[0];

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

        public static DataRow getLastDiagEntentePrealable(int IdPatient)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {



                string selectQuery = "select modele_entente.id, me_patient, ";
                selectQuery += "       me_alvemaxpro, ";
                selectQuery += "       me_alvemaxendo, ";
                selectQuery += "       me_alvemaxsupra, ";
                selectQuery += "       me_alvemaxretro, ";
                selectQuery += "       me_alvemaxexo, ";
                selectQuery += "       me_alvemandpro, ";
                selectQuery += "       me_alvemandendo, ";
                selectQuery += "       me_alvemandinfra, ";
                selectQuery += "       me_alvemandretro, ";
                selectQuery += "       me_alvemandexo, ";
                selectQuery += "       me_basmaxpro, ";
                selectQuery += "       me_basmaxendo, ";
                selectQuery += "       me_basmaxhypo, ";
                selectQuery += "       me_basmaxretro, ";
                selectQuery += "       me_basmaxexo, ";
                selectQuery += "       me_basmandpro, ";
                selectQuery += "       me_basmandendo, ";
                selectQuery += "       me_basmandhyper, ";
                selectQuery += "       me_basmandretro, ";
                selectQuery += "       me_basmandexo, ";
                selectQuery += "       me_mol1, ";
                selectQuery += "       me_mol2, ";
                selectQuery += "       me_mol3, ";
                selectQuery += "       me_moltext, ";
                selectQuery += "       me_can1, ";
                selectQuery += "       me_can2, ";
                selectQuery += "       me_can3, ";
                selectQuery += "       me_cantext, ";
                selectQuery += "       me_occludroit, ";
                selectQuery += "       me_occlugauche, ";
                selectQuery += "       me_occluanter, ";
                selectQuery += "       me_agnesie, ";
                selectQuery += "       me_dentincl, ";
                selectQuery += "       me_malpos, ";
                selectQuery += "       me_dysharmo, ";
                selectQuery += "       me_dysharmodd, ";
                selectQuery += "       me_facteurfonc, ";
                selectQuery += "       pat_objectif_trait2, ";
                selectQuery += "       pat_objectif_comm2";
                selectQuery += " from modele_entente";
                selectQuery += " where me_patient = @Id";
                selectQuery += "   order by id desc limit 1";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@Id", IdPatient);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                    return null;
                else
                    return dt.Rows[0];

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

        public static void InsertDiagEntentePrealable(EntentePrealable ententepre)
        {
            if (connection == null) getConnection(); if (connection == null) return;
            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQueryId = "select max(Id)+1 as ID from modele_entente";
                MySqlCommand commandid = new MySqlCommand(selectQueryId, connection, transaction);
                object id = commandid.ExecuteScalar();
                if (id == DBNull.Value)
                    ententepre.IdDiag = 1;
                else
                    ententepre.IdDiag = Convert.ToInt32(id);

                string selectQuery = "insert into modele_entente (id, me_patient, ";
                selectQuery += "   me_alvemaxpro, ";
                selectQuery += "   me_alvemaxendo, ";
                selectQuery += "   me_alvemaxsupra, ";
                selectQuery += "   me_alvemaxretro, ";
                selectQuery += "   me_alvemaxexo, ";
                selectQuery += "   me_alvemandpro, ";
                selectQuery += "   me_alvemandendo, ";
                selectQuery += "   me_alvemandinfra, ";
                selectQuery += "   me_alvemandretro, ";
                selectQuery += "   me_alvemandexo, ";
                selectQuery += "   me_basmaxpro, ";
                selectQuery += "   me_basmaxendo, ";
                selectQuery += "   me_basmaxhypo, ";
                selectQuery += "   me_basmaxretro, ";
                selectQuery += "   me_basmaxexo, ";
                selectQuery += "   me_basmandpro, ";
                selectQuery += "   me_basmandendo, ";
                selectQuery += "   me_basmandhyper, ";
                selectQuery += "   me_basmandretro, ";
                selectQuery += "   me_basmandexo, ";
                selectQuery += "   me_mol1, ";
                selectQuery += "   me_mol2, ";
                selectQuery += "   me_mol3, ";
                selectQuery += "   me_moltext, ";
                selectQuery += "   me_can1, ";
                selectQuery += "   me_can2, ";
                selectQuery += "   me_can3, ";
                selectQuery += "   me_cantext, ";
                selectQuery += "   me_occludroit, ";
                selectQuery += "   me_occlugauche, ";
                selectQuery += "   me_occluanter, ";
                selectQuery += "   me_agnesie, ";
                selectQuery += "   me_dentincl, ";
                selectQuery += "   me_malpos, ";
                selectQuery += "   me_dysharmo, ";
                selectQuery += "   me_dysharmodd, ";
                selectQuery += "   me_facteurfonc, ";
                selectQuery += "   pat_objectif_trait2, ";
                selectQuery += "   pat_objectif_comm2)";
                selectQuery += "values (@id, @me_patient, ";
                selectQuery += "        @me_alvemaxpro, ";
                selectQuery += "        @me_alvemaxendo, ";
                selectQuery += "        @me_alvemaxsupra, ";
                selectQuery += "        @me_alvemaxretro, ";
                selectQuery += "        @me_alvemaxexo, ";
                selectQuery += "        @me_alvemandpro, ";
                selectQuery += "        @me_alvemandendo, ";
                selectQuery += "        @me_alvemandinfra, ";
                selectQuery += "        @me_alvemandretro, ";
                selectQuery += "        @me_alvemandexo, ";
                selectQuery += "        @me_basmaxpro, ";
                selectQuery += "        @me_basmaxendo, ";
                selectQuery += "        @me_basmaxhypo, ";
                selectQuery += "        @me_basmaxretro, ";
                selectQuery += "        @me_basmaxexo, ";
                selectQuery += "        @me_basmandpro, ";
                selectQuery += "        @me_basmandendo, ";
                selectQuery += "        @me_basmandhyper, ";
                selectQuery += "        @me_basmandretro, ";
                selectQuery += "        @me_basmandexo, ";
                selectQuery += "        @me_mol1, ";
                selectQuery += "        @me_mol2, ";
                selectQuery += "        @me_mol3, ";
                selectQuery += "        @me_moltext, ";
                selectQuery += "        @me_can1, ";
                selectQuery += "        @me_can2, ";
                selectQuery += "        @me_can3, ";
                selectQuery += "        @me_cantext, ";
                selectQuery += "        @me_occludroit, ";
                selectQuery += "        @me_occlugauche, ";
                selectQuery += "        @me_occluanter, ";
                selectQuery += "        @me_agnesie, ";
                selectQuery += "        @me_dentincl, ";
                selectQuery += "        @me_malpos, ";
                selectQuery += "        @me_dysharmo, ";
                selectQuery += "        @me_dysharmodd, ";
                selectQuery += "        @me_facteurfonc, ";
                selectQuery += "        @pat_objectif_trait2, ";
                selectQuery += "        @pat_objectif_comm2)";



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id", ententepre.IdDiag);
                command.Parameters.AddWithValue("@me_patient", ententepre.patient.Id);
                command.Parameters.AddWithValue("@me_alvemaxpro", ententepre.SensSagittalAlveolaireMax == EntentePrealable.en_ProRetro.Pro);
                command.Parameters.AddWithValue("@me_alvemaxendo", ententepre.SensTransversalAlveolaireMax == EntentePrealable.en_DiagAlveolaire.Endoalveolie);
                command.Parameters.AddWithValue("@me_alvemaxsupra", ententepre.SensVerticalAlveolaire == EntentePrealable.en_OccFace.Supraclusion);
                command.Parameters.AddWithValue("@me_alvemaxretro", ententepre.SensSagittalAlveolaireMax == EntentePrealable.en_ProRetro.Retro);
                command.Parameters.AddWithValue("@me_alvemaxexo", ententepre.SensTransversalAlveolaireMax == EntentePrealable.en_DiagAlveolaire.Exoalveolie);
                command.Parameters.AddWithValue("@me_alvemandpro", ententepre.SensSagittalAlveolaireMand == EntentePrealable.en_ProRetro.Pro);
                command.Parameters.AddWithValue("@me_alvemandendo", ententepre.SensTransversalAlveolaireMand == EntentePrealable.en_DiagAlveolaire.Endoalveolie);
                command.Parameters.AddWithValue("@me_alvemandinfra", ententepre.SensVerticalAlveolaire == EntentePrealable.en_OccFace.Infraclusion);
                command.Parameters.AddWithValue("@me_alvemandretro", ententepre.SensSagittalAlveolaireMand == EntentePrealable.en_ProRetro.Retro);
                command.Parameters.AddWithValue("@me_alvemandexo", ententepre.SensTransversalAlveolaireMand == EntentePrealable.en_DiagAlveolaire.Exoalveolie);
                command.Parameters.AddWithValue("@me_basmaxpro", ententepre.SensSagittalBasalMax == EntentePrealable.en_ProRetro.Pro);
                command.Parameters.AddWithValue("@me_basmaxendo", ententepre.SensTransversalBasalMax == EntentePrealable.en_DiagOsseux.Endognatie);
                command.Parameters.AddWithValue("@me_basmaxhypo", ententepre.SensVerticalBasal == EntentePrealable.en_Divergence.Hypodivergent);
                command.Parameters.AddWithValue("@me_basmaxretro", ententepre.SensSagittalBasalMax == EntentePrealable.en_ProRetro.Retro);
                command.Parameters.AddWithValue("@me_basmaxexo", ententepre.SensTransversalBasalMax == EntentePrealable.en_DiagOsseux.Exognatie);
                command.Parameters.AddWithValue("@me_basmandpro", ententepre.SensSagittalBasalMand == EntentePrealable.en_ProRetro.Pro);
                command.Parameters.AddWithValue("@me_basmandendo", ententepre.SensTransversalBasalMand == EntentePrealable.en_DiagOsseux.Endognatie);
                command.Parameters.AddWithValue("@me_basmandhyper", ententepre.SensVerticalBasal == EntentePrealable.en_Divergence.Hyperdivergent);
                command.Parameters.AddWithValue("@me_basmandretro", ententepre.SensSagittalBasalMand == EntentePrealable.en_ProRetro.Retro);
                command.Parameters.AddWithValue("@me_basmandexo", ententepre.SensTransversalBasalMand == EntentePrealable.en_DiagOsseux.Exognatie);
                command.Parameters.AddWithValue("@me_mol1", ententepre.ClasseDentaireMolaire == EntentePrealable.en_Class.Class_I);
                command.Parameters.AddWithValue("@me_mol2", ententepre.ClasseDentaireMolaire == EntentePrealable.en_Class.Class_II);
                command.Parameters.AddWithValue("@me_mol3", ententepre.ClasseDentaireMolaire == EntentePrealable.en_Class.Class_III);
                command.Parameters.AddWithValue("@me_moltext", ententepre.ClasseDentaireMolaireTxt);
                command.Parameters.AddWithValue("@me_can1", ententepre.ClasseDentaireCanine == EntentePrealable.en_Class.Class_I);
                command.Parameters.AddWithValue("@me_can2", ententepre.ClasseDentaireCanine == EntentePrealable.en_Class.Class_II);
                command.Parameters.AddWithValue("@me_can3", ententepre.ClasseDentaireCanine == EntentePrealable.en_Class.Class_III);
                command.Parameters.AddWithValue("@me_cantext", ententepre.ClasseDentaireCanineTxt);
                command.Parameters.AddWithValue("@me_occludroit", (ententepre.occInverse == EntentePrealable.en_OccInverse.Droite) || (ententepre.occInverse == EntentePrealable.en_OccInverse.Droite_Et_Gauche));
                command.Parameters.AddWithValue("@me_occlugauche", (ententepre.occInverse == EntentePrealable.en_OccInverse.Gauche) || (ententepre.occInverse == EntentePrealable.en_OccInverse.Droite_Et_Gauche));
                command.Parameters.AddWithValue("@me_occluanter", ententepre.occInverse == EntentePrealable.en_OccInverse.Anterieur);
                command.Parameters.AddWithValue("@me_agnesie", ententepre.Agenesie);
                command.Parameters.AddWithValue("@me_dentincl", ententepre.DentsIncluseSurnum);
                command.Parameters.AddWithValue("@me_malpos", ententepre.Malposition);
                command.Parameters.AddWithValue("@me_dysharmo", ententepre.DDM);
                command.Parameters.AddWithValue("@me_dysharmodd", ententepre.DDD);
                command.Parameters.AddWithValue("@me_facteurfonc", ententepre.FacteurFonctionnel);
                command.Parameters.AddWithValue("@pat_objectif_trait2", ententepre.PlanDeTraitement);
                command.Parameters.AddWithValue("@pat_objectif_comm2", ententepre.Commentaires);


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

        public static void UpdateDiagEntentePrealable(EntentePrealable ententepre)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "update modele_entente\n";
                selectQuery += "set me_alvemaxpro = @me_alvemaxpro,\n";
                selectQuery += "    me_alvemaxendo = @me_alvemaxendo,\n";
                selectQuery += "    me_alvemaxsupra = @me_alvemaxsupra,\n";
                selectQuery += "    me_alvemaxretro = @me_alvemaxretro,\n";
                selectQuery += "    me_alvemaxexo = @me_alvemaxexo,\n";
                selectQuery += "    me_alvemandpro = @me_alvemandpro,\n";
                selectQuery += "    me_alvemandendo = @me_alvemandendo,\n";
                selectQuery += "    me_alvemandinfra = @me_alvemandinfra,\n";
                selectQuery += "    me_alvemandretro = @me_alvemandretro,\n";
                selectQuery += "    me_alvemandexo = @me_alvemandexo,\n";
                selectQuery += "    me_basmaxpro = @me_basmaxpro,\n";
                selectQuery += "    me_basmaxendo = @me_basmaxendo,\n";
                selectQuery += "    me_basmaxhypo = @me_basmaxhypo,\n";
                selectQuery += "    me_basmaxretro = @me_basmaxretro,\n";
                selectQuery += "    me_basmaxexo = @me_basmaxexo,\n";
                selectQuery += "    me_basmandpro = @me_basmandpro,\n";
                selectQuery += "    me_basmandendo = @me_basmandendo,\n";
                selectQuery += "    me_basmandhyper = @me_basmandhyper,\n";
                selectQuery += "    me_basmandretro = @me_basmandretro,\n";
                selectQuery += "    me_basmandexo = @me_basmandexo,\n";
                selectQuery += "    me_mol1 = @me_mol1,\n";
                selectQuery += "    me_mol2 = @me_mol2,\n";
                selectQuery += "    me_mol3 = @me_mol3,\n";
                selectQuery += "    me_moltext = @me_moltext,\n";
                selectQuery += "    me_can1 = @me_can1,\n";
                selectQuery += "    me_can2 = @me_can2,\n";
                selectQuery += "    me_can3 = @me_can3,\n";
                selectQuery += "    me_cantext = @me_cantext,\n";
                selectQuery += "    me_occludroit = @me_occludroit,\n";
                selectQuery += "    me_occlugauche = @me_occlugauche,\n";
                selectQuery += "    me_occluanter = @me_occluanter,\n";
                selectQuery += "    me_agnesie = @me_agnesie,\n";
                selectQuery += "    me_dentincl = @me_dentincl,\n";
                selectQuery += "    me_malpos = @me_malpos,\n";
                selectQuery += "    me_dysharmo = @me_dysharmo,\n";
                selectQuery += "    me_dysharmodd = @me_dysharmodd,\n";
                selectQuery += "    me_facteurfonc = @me_facteurfonc,\n";
                selectQuery += "    pat_objectif_trait2 = @pat_objectif_trait2,\n";
                selectQuery += "    pat_objectif_comm2 = @pat_objectif_comm2\n";
                selectQuery += "    where me_patient = @me_patient\n";




                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@me_patient", ententepre.patient.Id);
                command.Parameters.AddWithValue("@me_alvemaxpro", ententepre.SensSagittalAlveolaireMax == EntentePrealable.en_ProRetro.Pro);
                command.Parameters.AddWithValue("@me_alvemaxendo", ententepre.SensTransversalAlveolaireMax == EntentePrealable.en_DiagAlveolaire.Endoalveolie);
                command.Parameters.AddWithValue("@me_alvemaxsupra", ententepre.SensVerticalAlveolaire == EntentePrealable.en_OccFace.Supraclusion);
                command.Parameters.AddWithValue("@me_alvemaxretro", ententepre.SensSagittalAlveolaireMax == EntentePrealable.en_ProRetro.Retro);
                command.Parameters.AddWithValue("@me_alvemaxexo", ententepre.SensTransversalAlveolaireMax == EntentePrealable.en_DiagAlveolaire.Exoalveolie);
                command.Parameters.AddWithValue("@me_alvemandpro", ententepre.SensSagittalAlveolaireMand == EntentePrealable.en_ProRetro.Pro);
                command.Parameters.AddWithValue("@me_alvemandendo", ententepre.SensTransversalAlveolaireMand == EntentePrealable.en_DiagAlveolaire.Endoalveolie);
                command.Parameters.AddWithValue("@me_alvemandinfra", ententepre.SensVerticalAlveolaire == EntentePrealable.en_OccFace.Infraclusion);
                command.Parameters.AddWithValue("@me_alvemandretro", ententepre.SensSagittalAlveolaireMand == EntentePrealable.en_ProRetro.Retro);
                command.Parameters.AddWithValue("@me_alvemandexo", ententepre.SensTransversalAlveolaireMand == EntentePrealable.en_DiagAlveolaire.Exoalveolie);
                command.Parameters.AddWithValue("@me_basmaxpro", ententepre.SensSagittalBasalMax == EntentePrealable.en_ProRetro.Pro);
                command.Parameters.AddWithValue("@me_basmaxendo", ententepre.SensTransversalBasalMax == EntentePrealable.en_DiagOsseux.Endognatie);
                command.Parameters.AddWithValue("@me_basmaxhypo", ententepre.SensVerticalBasal == EntentePrealable.en_Divergence.Hypodivergent);
                command.Parameters.AddWithValue("@me_basmaxretro", ententepre.SensSagittalBasalMax == EntentePrealable.en_ProRetro.Retro);
                command.Parameters.AddWithValue("@me_basmaxexo", ententepre.SensTransversalBasalMax == EntentePrealable.en_DiagOsseux.Exognatie);
                command.Parameters.AddWithValue("@me_basmandpro", ententepre.SensSagittalBasalMand == EntentePrealable.en_ProRetro.Pro);
                command.Parameters.AddWithValue("@me_basmandendo", ententepre.SensTransversalBasalMand == EntentePrealable.en_DiagOsseux.Endognatie);
                command.Parameters.AddWithValue("@me_basmandhyper", ententepre.SensVerticalBasal == EntentePrealable.en_Divergence.Hyperdivergent);
                command.Parameters.AddWithValue("@me_basmandretro", ententepre.SensSagittalBasalMand == EntentePrealable.en_ProRetro.Retro);
                command.Parameters.AddWithValue("@me_basmandexo", ententepre.SensTransversalBasalMand == EntentePrealable.en_DiagOsseux.Exognatie);
                command.Parameters.AddWithValue("@me_mol1", ententepre.ClasseDentaireMolaire == EntentePrealable.en_Class.Class_I);
                command.Parameters.AddWithValue("@me_mol2", ententepre.ClasseDentaireMolaire == EntentePrealable.en_Class.Class_II);
                command.Parameters.AddWithValue("@me_mol3", ententepre.ClasseDentaireMolaire == EntentePrealable.en_Class.Class_III);
                command.Parameters.AddWithValue("@me_moltext", ententepre.ClasseDentaireMolaireTxt);
                command.Parameters.AddWithValue("@me_can1", ententepre.ClasseDentaireCanine == EntentePrealable.en_Class.Class_I);
                command.Parameters.AddWithValue("@me_can2", ententepre.ClasseDentaireCanine == EntentePrealable.en_Class.Class_II);
                command.Parameters.AddWithValue("@me_can3", ententepre.ClasseDentaireCanine == EntentePrealable.en_Class.Class_III);
                command.Parameters.AddWithValue("@me_cantext", ententepre.ClasseDentaireCanineTxt);
                command.Parameters.AddWithValue("@me_occludroit", (ententepre.occInverse == EntentePrealable.en_OccInverse.Droite) || (ententepre.occInverse == EntentePrealable.en_OccInverse.Droite_Et_Gauche));
                command.Parameters.AddWithValue("@me_occlugauche", (ententepre.occInverse == EntentePrealable.en_OccInverse.Gauche) || (ententepre.occInverse == EntentePrealable.en_OccInverse.Droite_Et_Gauche));
                command.Parameters.AddWithValue("@me_occluanter", ententepre.occInverse == EntentePrealable.en_OccInverse.Anterieur);
                command.Parameters.AddWithValue("@me_agnesie", ententepre.Agenesie);
                command.Parameters.AddWithValue("@me_dentincl", ententepre.DentsIncluseSurnum);
                command.Parameters.AddWithValue("@me_malpos", ententepre.Malposition);
                command.Parameters.AddWithValue("@me_dysharmo", ententepre.DDM);
                command.Parameters.AddWithValue("@me_dysharmodd", ententepre.DDD);
                command.Parameters.AddWithValue("@me_facteurfonc", ententepre.FacteurFonctionnel);
                command.Parameters.AddWithValue("@pat_objectif_trait2", ententepre.PlanDeTraitement);
                command.Parameters.AddWithValue("@pat_objectif_comm2", ententepre.Commentaires);


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
