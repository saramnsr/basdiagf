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

        public static DataTable getAllAccessObject()
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
               // MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = " select PASSWORD, ";
                    selectQuery += "        RHBASE_ALLOWDELETERDV, ";
                    selectQuery += "        RHBASE_ALLOWMOVERDV, ";
                    selectQuery += "        BASEPRACT_ALLOWLISTFINANCE, ";
                    selectQuery += "        BASEPRACT_ALLOWHISTOFINANCE, ";
                    selectQuery += "        BASE_STAT_ALLOWFINANCE, ";
                    selectQuery += "        BASEPRACT_COMPTABILITE, ";
                    selectQuery += "        BASEPRACT_LISTFINANCE, ";
                    selectQuery += "        IDUtilisateur, ";
                    selectQuery += "        BASEPRACT_CanDeleteEncaissement, ";
                    selectQuery += "        BASEPRACT_CanDeleteActe, ";
                    selectQuery += "        BPTRANSFERT, ";
                    selectQuery += "        STATUS_CLINIQUE, ";
                    selectQuery += "        RHBASE_ACCES_RH, SUPER_USER ,RHBas_AllowToRaccourcirRDV";

                    selectQuery += " from base_access";



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

              

        }

        public static void AddNewAO(AccessObject access)
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








                    string selectQuery = "INSERT INTO base_access (PASSWORD, RHBASE_ALLOWDELETERDV, RHBASE_ALLOWMOVERDV,";
                    //BASE_STAT_ALLOWFINANCE,";
                    selectQuery += " BASEPRACT_ALLOWLISTFINANCE, BASEPRACT_ALLOWHISTOFINANCE, BPTRANSFERT, BASEPRACT_COMPTABILITE, BASEPRACT_CANDELETEENCAISSEMENT,";
                    selectQuery += " BASEPRACT_CANDELETEACTE, BASEPRACT_LISTFINANCE, IDUTILISATEUR, RHBASE_ACCES_RH, STATUS_CLINIQUE, SUPER_USER,RHBas_AllowToRaccourcirRDV)";
                    selectQuery += " VALUES (";
                    selectQuery += "    @PASSWORD, ";
                    selectQuery += "    @RHBASE_ALLOWDELETERDV, ";
                    selectQuery += "    @RHBASE_ALLOWMOVERDV, ";
                    //   selectQuery += "    @BASE_STAT_ALLOWFINANCE, ";
                    selectQuery += "    @BASEPRACT_ALLOWLISTFINANCE, ";
                    selectQuery += "    @BASEPRACT_ALLOWHISTOFINANCE, ";
                    selectQuery += "    @BPTRANSFERT, ";
                    selectQuery += "    @BASEPRACT_COMPTABILITE, ";
                    selectQuery += "    @BASEPRACT_CANDELETEENCAISSEMENT, ";
                    selectQuery += "    @BASEPRACT_CANDELETEACTE, ";
                    selectQuery += "    @BASEPRACT_LISTFINANCE, ";
                    selectQuery += "    @IDUTILISATEUR, ";
                    selectQuery += "    @RHBASE_ACCES_RH, ";
                    selectQuery += "    @STATUS_CLINIQUE, ";
                    selectQuery += "    @RHBas_AllowToRaccourcirRDV, ";
                    selectQuery += "    @SUPER_USER ";
                    selectQuery += "    )";
                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@PASSWORD", access.Password);
                    command.Parameters.AddWithValue("@RHBASE_ALLOWDELETERDV", access.RHBas_AllowToDeleteRDV);
                    command.Parameters.AddWithValue("@RHBASE_ALLOWMOVERDV", access.RHBas_AllowToMoveRDV);
                    //command.Parameters.AddWithValue("@BASE_STAT_ALLOWFINANCE", access.Bas_Stat_AllowFinances);
                    command.Parameters.AddWithValue("@BASEPRACT_ALLOWLISTFINANCE", access.BasPract_ListControles);
                    command.Parameters.AddWithValue("@BASEPRACT_ALLOWHISTOFINANCE", access.BasPract_HistoriqueFinances);
                    command.Parameters.AddWithValue("@BPTRANSFERT", access.Bas_Stat_AllowBPTransfert);
                    command.Parameters.AddWithValue("@BASEPRACT_COMPTABILITE", access.BasPract_Comptabilite);
                    command.Parameters.AddWithValue("@BASEPRACT_CANDELETEENCAISSEMENT", access.CanDeleteEncaissement);
                    command.Parameters.AddWithValue("@BASEPRACT_CANDELETEACTE", access.CanDeleteActe);
                    command.Parameters.AddWithValue("@BASEPRACT_LISTFINANCE", access.BasPract_ListFinancieres);
                    command.Parameters.AddWithValue("@IDUTILISATEUR", access.Utilisateur.Id);
                    command.Parameters.AddWithValue("@RHBASE_ACCES_RH", access.RHBas_AllowAccessRH);
                    command.Parameters.AddWithValue("@STATUS_CLINIQUE", access.RHBas_AllowAccessStatusClinique);
                    command.Parameters.AddWithValue("@RHBas_AllowToRaccourcirRDV", access.RHBas_AllowToRaccourcirRDV);
                    command.Parameters.AddWithValue("@SUPER_USER", access.SUPER_ADMIN);

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


        public static void updateAccessObject(AccessObject access)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "update base_access set ";
                selectQuery += "PASSWORD=@PASSWORD , ";
                selectQuery += "RHBASE_ALLOWDELETERDV =@RHBASE_ALLOWDELETERDV ,";
                selectQuery += "RHBASE_ALLOWMOVERDV =@RHBASE_ALLOWMOVERDV, ";
                //   selectQuery += "BASE_STAT_ALLOWFINANCE =@BASE_STAT_ALLOWFINANCE, ";
                selectQuery += "BASEPRACT_ALLOWLISTFINANCE=@BASEPRACT_ALLOWLISTFINANCE,";
                selectQuery += "BASEPRACT_ALLOWHISTOFINANCE=@BASEPRACT_ALLOWHISTOFINANCE,";
                selectQuery += "BPTRANSFERT=@BPTRANSFERT,";
                selectQuery += "BASEPRACT_COMPTABILITE=@BASEPRACT_COMPTABILITE, ";
                selectQuery += "BASEPRACT_CANDELETEENCAISSEMENT=@BASEPRACT_CANDELETEENCAISSEMENT, ";
                selectQuery += "BASEPRACT_CANDELETEACTE=@BASEPRACT_CANDELETEACTE, ";
                selectQuery += "BASEPRACT_LISTFINANCE=@BASEPRACT_LISTFINANCE, ";
                selectQuery += "IDUTILISATEUR=@IDUTILISATEUR, ";
                selectQuery += "RHBASE_ACCES_RH=@RHBASE_ACCES_RH,";
                selectQuery += "SUPER_USER=@SUPER_USER,";
                selectQuery += "STATUS_CLINIQUE=@STATUS_CLINIQUE,";
                selectQuery += "RHBas_AllowToRaccourcirRDV=@RHBas_AllowToRaccourcirRDV";
                selectQuery += " where IDUTILISATEUR =  @IDUTILISATEUR";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@IDUTILISATEUR", access.Utilisateur.Id);
                command.Parameters.AddWithValue("@PASSWORD", access.Password);
                command.Parameters.AddWithValue("@RHBASE_ALLOWDELETERDV", access.RHBas_AllowToDeleteRDV);
                command.Parameters.AddWithValue("@RHBASE_ALLOWMOVERDV", access.RHBas_AllowToMoveRDV);
                // command.Parameters.AddWithValue("@BASE_STAT_ALLOWFINANCE", false);
                command.Parameters.AddWithValue("@BASEPRACT_ALLOWLISTFINANCE", access.BasPract_ListControles);
                command.Parameters.AddWithValue("@BASEPRACT_ALLOWHISTOFINANCE", access.BasPract_HistoriqueFinances);
                command.Parameters.AddWithValue("@BPTRANSFERT", access.Bas_Stat_AllowBPTransfert);
                command.Parameters.AddWithValue("@BASEPRACT_COMPTABILITE", access.BasPract_Comptabilite);
                command.Parameters.AddWithValue("@BASEPRACT_CANDELETEENCAISSEMENT", access.CanDeleteEncaissement);
                command.Parameters.AddWithValue("@BASEPRACT_CANDELETEACTE", access.CanDeleteActe);
                command.Parameters.AddWithValue("@BASEPRACT_LISTFINANCE", access.BasPract_ListFinancieres);
                command.Parameters.AddWithValue("@SUPER_USER", access.SUPER_ADMIN);
                command.Parameters.AddWithValue("@RHBASE_ACCES_RH", access.RHBas_AllowAccessRH);
                command.Parameters.AddWithValue("@RHBas_AllowToRaccourcirRDV", access.RHBas_AllowToRaccourcirRDV);
                command.Parameters.AddWithValue("@STATUS_CLINIQUE", access.RHBas_AllowAccessStatusClinique);


                Object obj = command.ExecuteNonQuery();

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
