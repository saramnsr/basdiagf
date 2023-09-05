
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using BasCommon_BO;

namespace BasCommon_DAL
{
     public static partial class DAC
    {
         public static DataTable getAllLaboratoire()
         {
             if (connection == null) getConnection();

             if (connection.State == ConnectionState.Closed) connection.Open();
             MySqlTransaction transaction = connection.BeginTransaction();
             try
             {
                 string selectQuery = "select * from laboratoire ";

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
         public static DataRow getBaseLaboratoire(int idPatient)
         {
             if (connection == null) getConnection();

             if (connection.State == ConnectionState.Closed) connection.Open();
             try
             {
                 string selectQuery = "select * from base_laboratoire WHERE ID_PATIENT=@ID ";

                 MySqlCommand command = new MySqlCommand(selectQuery, connection);

                 DataSet ds = new DataSet();
                 MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                 command.Parameters.AddWithValue("@ID", idPatient);
                 adapt.Fill(ds);

                 DataTable dt = ds.Tables[0];

                 if (dt.Rows.Count > 0)
                     return dt.Rows[0];
                 else
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
         public static DataTable getBaseLaboratoires(DateTime datedebut,DateTime datefin,int idlab,bool All=false)
         {
             if (connection == null) getConnection();

             if (connection.State == ConnectionState.Closed) connection.Open();
             MySqlTransaction transaction = connection.BeginTransaction();
             try
             {
                 string selectQuery = "select * from base_laboratoire a WHERE  (CAST(a.DATE as date) between @dt1 and @dte2)"; 
                 if(!All)
               selectQuery += "  and a.id_laboratoire =@id";
                 MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                 DataSet ds = new DataSet();
                 MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                 command.Parameters.AddWithValue("@dt1", datedebut);
                 command.Parameters.AddWithValue("@dte2", datefin);
                 if(!All)
                 command.Parameters.AddWithValue("id", idlab);

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
         public static void InsertBaseLaboratoire(BaseLaboratoire blab)
         {
             if (connection == null) getConnection(); if (connection == null) return;

             if (connection.State == ConnectionState.Closed) connection.Open();
             MySqlTransaction transaction = connection.BeginTransaction();
             try
             {

                 string selectQuery = "select MAX(ID)+1 as NEWID from base_laboratoire";

                 MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                 command.CommandType = CommandType.Text;

                 object obj = command.ExecuteScalar();

                 if (obj == DBNull.Value)
                     blab.id = 1;
                 else
                     blab.id = Convert.ToInt32(obj);


                 selectQuery = "insert into base_laboratoire (ID, ID_LABORATOIRE, ID_PATIENT, MONTANT, DATE) ";

                 selectQuery += " values (@ID, @ID_LABORATOIRE, @ID_PATIENT, @MONTANT,@DATE) ";
                
                 command.CommandText = selectQuery;
                 command.Parameters.Clear();
                 command.Parameters.AddWithValue("@ID", blab.id);
                 command.Parameters.AddWithValue("@ID_LABORATOIRE", blab.idLaboratoire);
                 command.Parameters.AddWithValue("@DATE", DateTime.Now);
                 command.Parameters.AddWithValue("@ID_PATIENT", blab.idpatient);
                 command.Parameters.AddWithValue("@MONTANT", blab.montant);
                 


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
         public static void updateBaseLaboratoire(BaseLaboratoire blab)
         {
             if (connection == null) getConnection(); if (connection == null) return;

             if (connection.State == ConnectionState.Closed) connection.Open();
             MySqlTransaction transaction = connection.BeginTransaction();
             try
             {
                 string selectQuery = "update base_laboratoire set ";
                 selectQuery += " ID_LABORATOIRE = @ID_LABORATOIRE, ";
                 selectQuery += " MONTANT = @MONTANT, ";
                 selectQuery += " DATE = @DATE ";
                 selectQuery += " where  id = @id";



                 MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                 command.Parameters.AddWithValue("@ID", blab.id);
                 command.Parameters.AddWithValue("@ID_LABORATOIRE", blab.idLaboratoire);
                 command.Parameters.AddWithValue("@MONTANT", blab.montant);
                 command.Parameters.AddWithValue("@DATE", DateTime.Now);
                 command.CommandType = CommandType.Text;

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
