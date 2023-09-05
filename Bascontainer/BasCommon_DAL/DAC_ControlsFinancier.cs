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

        

        public static DataTable getControlsFinancier(PaiementReel pr)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {


                string selectQuery = "select bas_controlsfinance.id, ";
                selectQuery += "                            bas_controlsfinance.ETAT, ";
                selectQuery += "                            bas_controlsfinance.REMARQUE, ";
                selectQuery += "                            bas_controlsfinance.CODECONTROL,  ";
                selectQuery += "                            bas_controlsfinance.LIBELLE,  ";
                selectQuery += "                            bas_controlsfinance.ID_CONTROLEUR, ";
                selectQuery += "                            DATE ";
                selectQuery += "                            from bas_controlsfinance";
                selectQuery += "                            inner join bas_lnk_paiement_ctrl on bas_controlsfinance.id=bas_lnk_paiement_ctrl.id_control";
                selectQuery += "                            inner join base_paiementreel on bas_lnk_paiement_ctrl.id_paiement=base_paiementreel.id";
                selectQuery += "                            where base_paiementreel.ID = @id";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", pr.Id);

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


        public static DataRow getControlFinancier(BordereauFinance bf)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {


                string selectQuery = "select bas_controlsfinance.id, ";
                selectQuery += "                            ETAT, ";
                selectQuery += "                            REMARQUE, ";
                selectQuery += "                            CODECONTROL,  ";
                selectQuery += "                            LIBELLE,  ";
                selectQuery += "                            ID_CONTROLEUR, ";
                selectQuery += "                            DATE ";
                selectQuery += "                            from bas_controlsfinance";
                selectQuery += "                            inner join bas_bordereau_finance on bas_bordereau_finance.ID_CONTROL = bas_controlsfinance.ID";
                selectQuery += "                            where bas_bordereau_finance.ID = @id";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id",bf.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count > 0) return dt.Rows[0];
                return null;

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


        public static DataTable getControlFinancier()
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
               

                string selectQuery = "select id, ";
                selectQuery += "                            ETAT, ";
                selectQuery += "                            REMARQUE, ";
                selectQuery += "                            CODECONTROL,  ";
                selectQuery += "                            LIBELLE,  ";
                selectQuery += "                            ID_CONTROLEUR, ";
                selectQuery += "                            DATE ";
                selectQuery += "                            from bas_controlsfinance";

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

        public static void DeleteControlFinancier(ControlFinancier ctrl)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();






            try
            {



                string selectQuery = "Delete from bas_erreur_ctrl";
                selectQuery += " where ID_CONTROL = @id ";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", ctrl.Id);
                command.ExecuteNonQuery();

                selectQuery = "Delete from lnk_paiement_ctrl";
                selectQuery += " where ID_CONTROL = @id ";

                command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", ctrl.Id);
                command.ExecuteNonQuery();
                

                selectQuery = "Delete from bas_controlsfinance";
                selectQuery += " where id = @id ";

               command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", ctrl.Id);
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
        
        public static void UpdateControlFinancier(ControlFinancier ctrl)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();


           



            try
            {
                string selectQuery = "update bas_controlsfinance  set ";
                selectQuery += "                            ETAT=@ETAT, ";
                selectQuery += "                            REMARQUE=@REMARQUE, ";
                selectQuery += "                            CODECONTROL=@CODECONTROL,  ";
                selectQuery += "                            LIBELLE=@LIBELLE,  ";
                selectQuery += "                            ID_CONTROLEUR=@ID_CONTROLEUR, ";
                selectQuery += "                            DATE=@dte";
                selectQuery += " where id = @id ";




                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id", ctrl.Id);
                command.Parameters.AddWithValue("@ETAT", ctrl.Etat);
                command.Parameters.AddWithValue("@REMARQUE", ctrl.Remarques);
                command.Parameters.AddWithValue("@CODECONTROL", ctrl.CodeControl);
                command.Parameters.AddWithValue("@LIBELLE", ctrl.Libelle);
                command.Parameters.AddWithValue("@ID_CONTROLEUR", ctrl.IdControleur);
                command.Parameters.AddWithValue("@dte", ctrl.DateControl);

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
        
        public static void InsertControlFinancier(ControlFinancier ctrl)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();


            string selectQueryId = "select max(Id)+1 as ID from bas_controlsfinance";
            MySqlCommand commandid = new MySqlCommand(selectQueryId, connection, transaction);
            object id = commandid.ExecuteScalar();
            if (id == DBNull.Value)
                ctrl.Id = 1;
            else
                ctrl.Id = Convert.ToInt32(id);



            try
            {
                string selectQuery = "insert into bas_controlsfinance (id, ";
                selectQuery += "                            ETAT, ";
                selectQuery += "                            REMARQUE, ";
                selectQuery += "                            CODECONTROL,  ";
                selectQuery += "                            LIBELLE,  ";
                selectQuery += "                            ID_CONTROLEUR, ";
                selectQuery += "                            DATE)";
                selectQuery += " values (@id, ";
                selectQuery += "                            @ETAT, ";
                selectQuery += "                            @REMARQUE, ";
                selectQuery += "                            @CODECONTROL,  ";
                selectQuery += "                            @LIBELLE,  ";
                selectQuery += "                            @ID_CONTROLEUR, ";
                selectQuery += "                            @dte)";




                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id", ctrl.Id);
                command.Parameters.AddWithValue("@ETAT", ctrl.Etat);
                command.Parameters.AddWithValue("@REMARQUE", ctrl.Remarques);
                command.Parameters.AddWithValue("@CODECONTROL", ctrl.CodeControl);
                command.Parameters.AddWithValue("@LIBELLE", ctrl.Libelle);
                command.Parameters.AddWithValue("@ID_CONTROLEUR", ctrl.IdControleur);
                command.Parameters.AddWithValue("@dte", ctrl.DateControl);

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
        



        public static DataTable getlnkControlFinancier()
        {
            if (connection == null) getConnection(); 

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {


                string selectQuery = "select ID_PAIEMENT, ";
                selectQuery += "                            ID_CONTROL, ";
                selectQuery += "                            REMARQUE, ";
                selectQuery += "                            ETAT, ";
                selectQuery += "                            UDATEDATE  ";
                selectQuery += "                            from bas_lnk_paiement_ctrl";

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
                
             

        public static void InsertlnkControlFinancier(lnkControlFinancier ctrl)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();




            try
            {

               
                string selectQuery = "insert into bas_lnk_paiement_ctrl (ID_PAIEMENT, ";
                selectQuery += "                            ID_ECHEANCE, ";
                selectQuery += "                            ID_CONTROL, ";
                selectQuery += "                            REMARQUE, ";
                selectQuery += "                            CODE_ERREUR, ";
                selectQuery += "                            LATEST_FLAG, ";
                selectQuery += "                            ETAT, ";
                selectQuery += "                            UDATEDATE)";
                selectQuery += " values (@ID_PAIEMENT, ";
                selectQuery += "                            @ID_ECHEANCE, ";
                selectQuery += "                            @ID_CONTROL, ";
                selectQuery += "                            @REMARQUE, ";
                selectQuery += "                            @CODE_ERREUR, ";
                selectQuery += "                            'Y', ";
                selectQuery += "                            @ETAT, ";
                selectQuery += "                            @UDATEDATE)";




                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@ID_PAIEMENT", ctrl.IdPaiementReel);
                command.Parameters.AddWithValue("@ID_ECHEANCE", ctrl.IdEcheance);
                command.Parameters.AddWithValue("@CODE_ERREUR", ctrl.CodeErreur);
                command.Parameters.AddWithValue("@REMARQUE", ctrl.Remarques);
                command.Parameters.AddWithValue("@ETAT", (int)ctrl.Etat);
                command.Parameters.AddWithValue("@ID_CONTROL", ctrl.IdControlFinancier);
                command.Parameters.AddWithValue("@UDATEDATE", ctrl.UpdateDate);

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
