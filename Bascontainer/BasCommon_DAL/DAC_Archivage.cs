using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using FirebirdSql.Data.Isql;
using System.Configuration;
using BasCommon_BO;
using MySql.Data.MySqlClient;
namespace BasCommon_DAL
{
    public static partial class DAC
    {



        public static void ExecuteBlockScript(string scriptfile, string ConnectionString, bool createDatabase)
        {
           

           try
            {

                if (createDatabase)
                    FbConnection.CreateDatabase(ConnectionString);

                FbConnection conn = new FbConnection(ConnectionString);
               
                FbScript script = new FbScript(scriptfile);
                script.Parse();
                

                FbBatchExecution fbe = new FirebirdSql.Data.Isql.FbBatchExecution(conn, script);
                
                fbe.Execute(true);

            }
           catch (Exception e)
           {
               throw e;
           } 
            

        }


        public static void TransfertQuery(string query, string connectionstring)
        {
            if (query == "") return;
            MySqlConnection insertconnection = new MySqlConnection(connectionstring);
            insertconnection.Open();


            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = query;

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];





                MySqlCommandBuilder _fbcb = new MySqlCommandBuilder(adapt);
                MySqlCommand fbcinsert = _fbcb.GetInsertCommand();
                fbcinsert.Connection = insertconnection;
                fbcinsert.Transaction = insertconnection.BeginTransaction();

                MySqlDataAdapter InsertAdapt = new MySqlDataAdapter(fbcinsert);
                InsertAdapt.InsertCommand = fbcinsert;


                foreach (DataRow dr in dt.Rows)
                    dr.SetAdded();

                int ret = InsertAdapt.Update(dt);

                fbcinsert.Transaction.Commit();





            }
            catch (System.SystemException ex)
            {
                throw ex;
            }
            finally
            {
               connection.Close();

               connection.Close();
            }

        }


        public static void TransfertTable(string tableName, string connectionstring)
        {

            MySqlConnection insertconnection = new MySqlConnection(connectionstring);
            insertconnection.Open();


            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                //famille_Acte
                string selectQuery = "select * from " + tableName;

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];





                MySqlCommandBuilder _fbcb = new MySqlCommandBuilder(adapt);
                MySqlCommand fbcinsert = _fbcb.GetInsertCommand();
                fbcinsert.Connection = insertconnection;
                transaction = insertconnection.BeginTransaction();

                 MySqlDataAdapter InsertAdapt = new MySqlDataAdapter(fbcinsert);
                 InsertAdapt.InsertCommand = fbcinsert;


                 foreach (DataRow dr in dt.Rows)
                     dr.SetAdded();

                 int ret = InsertAdapt.Update(dt);

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
