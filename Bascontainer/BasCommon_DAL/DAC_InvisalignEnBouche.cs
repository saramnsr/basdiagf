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
        public static DataTable getInvisalignEnbouche(basePatient patient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = @"select id, 
                                       datedebut, 
                                       datefin, 
                                       numaligneur, 
                                       id_patient, 
                                       haut, 
                                       bas
                                from bas_invi_enbouche
                                where id_patient=@id_patient";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@id_patient", patient.Id);

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


        public static void AddInvisalignEnBouche(InvisalignEnBouche ib)
        {

            /*CREATE TABLE BAS_INVI_ENBOUCHE (
    ID           INTEGER NOT NULL,
    DATEDEBUT    TIMESTAMP,
    DATEFIN      TIMESTAMP,
    NUMALIGNEUR  INTEGER,
    ID_PATIENT   INTEGER,
    HAUT         SMALLINT,
    BAS          SMALLINT
);*/

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
                string selectQuery = "select MAX(ID)+1 as NEWID from BAS_INVI_ENBOUCHE";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                try
                {

                    ib.Id = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (System.InvalidCastException)
                {
                    ib.Id = 1;
                }



                selectQuery = @"insert into bas_invi_enbouche (id, 
                                                                   datedebut, 
                                                                   datefin, 
                                                                   numaligneur, 
                                                                   id_patient, 
                                                                   haut)
                                    values (@id, 
                                            @datedebut, 
                                            @datefin, 
                                            @numaligneur, 
                                            @id_patient, 
                                            @haut)";

                command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", ib.Id);
                command.Parameters.AddWithValue("@datedebut", ib.DateDebut);
                command.Parameters.AddWithValue("@datefin", ib.DateFin);
                command.Parameters.AddWithValue("@numaligneur", ib.NumAligneur);
                command.Parameters.AddWithValue("@id_patient", ib.IdPatient);
                command.Parameters.AddWithValue("@haut", ib.IsHaut);

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
        

        public static void UpdateInvisalignEnBouche(InvisalignEnBouche ib)
        {

            /*CREATE TABLE BAS_INVI_ENBOUCHE (
    ID           INTEGER NOT NULL,
    DATEDEBUT    TIMESTAMP,
    DATEFIN      TIMESTAMP,
    NUMALIGNEUR  INTEGER,
    ID_PATIENT   INTEGER,
    HAUT         SMALLINT,
    BAS          SMALLINT
);*/

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




                string selectQuery = @"update bas_invi_enbouche
                                            set datedebut =  @datedebut,
                                                datefin =    @datefin,
                                                numaligneur =@numaligneur,
                                                id_patient = @id_patient,
                                                haut =       @haut
                                            where id =       @id";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                command.Parameters.AddWithValue("@id", ib.Id);
                command.Parameters.AddWithValue("@datedebut", ib.DateDebut);
                command.Parameters.AddWithValue("@datefin", ib.DateFin);
                command.Parameters.AddWithValue("@numaligneur", ib.NumAligneur);
                command.Parameters.AddWithValue("@id_patient", ib.IdPatient);
                command.Parameters.AddWithValue("@haut", ib.IsHaut);

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


        public static void DeleteInvisalignEnBouche(InvisalignEnBouche ib)
        {

            /*CREATE TABLE BAS_INVI_ENBOUCHE (
    ID           INTEGER NOT NULL,
    DATEDEBUT    TIMESTAMP,
    DATEFIN      TIMESTAMP,
    NUMALIGNEUR  INTEGER,
    ID_PATIENT   INTEGER,
    HAUT         SMALLINT,
    BAS          SMALLINT
);*/

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




                string selectQuery = @"delete from bas_invi_enbouche where id = @id";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                command.Parameters.AddWithValue("@id", ib.Id);

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
