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

        public static DataTable getFeuillesDeSoin(basePatient pat)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "       date_paiment_honoraire, ";
                selectQuery += "       id_patient, ";
                selectQuery += "       numagreement_radionisation, ";
                selectQuery += "       numagreement_panoramique, ";
                selectQuery += "       numagreement_teleradio, ";
                selectQuery += "       MONTANT_SOUMISEP, ";
                selectQuery += "       MONTANT_NON_SOUMISEP, ";
                selectQuery += "       TYPE_ENVOIS, ";
                selectQuery += "       date_edition";
                selectQuery += " from base_feuille_de_soin";
                selectQuery += " where id_patient = @id_patient";
                selectQuery += " order by date_edition";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@id_patient", pat.Id);

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

        public static DataRow getFeuillesDeSoin(int Id)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "       date_paiment_honoraire, ";
                selectQuery += "       id_patient, ";
                selectQuery += "       numagreement_radionisation, ";
                selectQuery += "       numagreement_panoramique, ";
                selectQuery += "       numagreement_teleradio, ";
                selectQuery += "       MONTANT_SOUMISEP, ";
                selectQuery += "       MONTANT_NON_SOUMISEP, ";
                selectQuery += "       TYPE_ENVOIS, ";
                selectQuery += "       date_edition";
                selectQuery += " from base_feuille_de_soin";
                selectQuery += " where id = @id";
                selectQuery += " order by date_edition";

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

        public static void InsertFeuilleDeSoin(FeuilleDeSoin fs)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQueryid = "select MAX(id)+1 as NEWID from base_feuille_de_soin";

                MySqlCommand command = new MySqlCommand(selectQueryid, connection, transaction);
                command.CommandType = CommandType.Text;

                object obj = command.ExecuteScalar();

                if (obj == DBNull.Value)
                    fs.Id = 1;
                else
                    fs.Id = Convert.ToInt32(obj);


                string selectQuery = "insert into base_feuille_de_soin (id, ";
                selectQuery += "                                  date_paiment_honoraire, ";
                selectQuery += "                                  id_patient, ";
                selectQuery += "                                  numagreement_radionisation, ";
                selectQuery += "                                  numagreement_panoramique, ";
                selectQuery += "                                  numagreement_teleradio, ";
                selectQuery += "                                  MONTANT_SOUMISEP, ";
                selectQuery += "                                  MONTANT_NON_SOUMISEP, ";
                selectQuery += "                                  TYPE_ENVOIS, ";

                selectQuery += "                                  date_edition)";
                selectQuery += " values (@id, ";
                selectQuery += "        @date_paiment_honoraire, ";
                selectQuery += "        @id_patient, ";
                selectQuery += "        @numagreement_radionisation, ";
                selectQuery += "        @numagreement_panoramique, ";
                selectQuery += "        @numagreement_teleradio, ";
                selectQuery += "        @MONTANT_SOUMISEP, ";
                selectQuery += "        @MONTANT_NON_SOUMISEP, ";
                selectQuery += "        @TYPE_ENVOIS, ";
                selectQuery += "        @date_edition)";



                command.CommandText = selectQuery;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", fs.Id);
                command.Parameters.AddWithValue("@date_paiment_honoraire", fs.datePaiementHonoraire);

                command.Parameters.AddWithValue("@date_edition", fs.dateEdition);
                command.Parameters.AddWithValue("@id_patient", fs.patient.Id);
                command.Parameters.AddWithValue("@numagreement_radionisation", fs.AgremmentRadiation);
                command.Parameters.AddWithValue("@numagreement_panoramique", fs.AgrementPanoramique);
                command.Parameters.AddWithValue("@numagreement_teleradio", fs.AgrementTeleradio);
                command.Parameters.AddWithValue("@MONTANT_SOUMISEP", fs.TotalMontantSoumisAEntente);
                command.Parameters.AddWithValue("@MONTANT_NON_SOUMISEP", fs.TotalMontantNonSoumisAEntente);
                command.Parameters.AddWithValue("@TYPE_ENVOIS", fs.typedenvois);

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

        public static void UpdateFeuilleDeSoin(FeuilleDeSoin fs)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {




                string selectQuery = "update base_feuille_de_soin";
                selectQuery += " set date_paiment_honoraire = @date_paiment_honoraire,";
                selectQuery += "    id_patient = @id_patient,";
                selectQuery += "    numagreement_radionisation = @numagreement_radionisation,";
                selectQuery += "    numagreement_panoramique = @numagreement_panoramique,";
                selectQuery += "    numagreement_teleradio = @numagreement_teleradio,";
                selectQuery += "    MONTANT_SOUMISEP = @MONTANT_SOUMISEP,";
                selectQuery += "    MONTANT_NON_SOUMISEP = @MONTANT_NON_SOUMISEP,";
                selectQuery += "    TYPE_ENVOIS = @TYPE_ENVOIS,";
                selectQuery += "    date_edition = @date_edition";
                selectQuery += " where (id = @id)";




                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", fs.Id);
                command.Parameters.AddWithValue("@date_paiment_honoraire", fs.datePaiementHonoraire);

                command.Parameters.AddWithValue("@date_edition", fs.dateEdition);
                command.Parameters.AddWithValue("@id_patient", fs.patient.Id);
                command.Parameters.AddWithValue("@numagreement_radionisation", fs.AgremmentRadiation);
                command.Parameters.AddWithValue("@numagreement_panoramique", fs.AgrementPanoramique);
                command.Parameters.AddWithValue("@numagreement_teleradio", fs.AgrementTeleradio);
                command.Parameters.AddWithValue("@MONTANT_SOUMISEP", fs.TotalMontantSoumisAEntente);
                command.Parameters.AddWithValue("@MONTANT_NON_SOUMISEP", fs.TotalMontantNonSoumisAEntente);
                command.Parameters.AddWithValue("@TYPE_ENVOIS", fs.typedenvois);

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

        public static void DeleteFeuilleDeSoin(FeuilleDeSoin fs)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "delete from base_feuille_de_soin";
                selectQuery += " where  id = @Id";



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;




                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@Id", fs.Id);
                command.ExecuteNonQuery();


                selectQuery = "update base_traitement set ID_FS = NULL ";
                selectQuery += " where ID_FS = @id";

                command.CommandText = selectQuery;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", fs.Id);


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
