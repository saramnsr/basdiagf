using BasCommon_BO;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
namespace BasCommon_DAL
{
    public partial class DAC
    {

        public static DataTable getAutres(string from = "")
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
                try
                {
                    //famille_Acte
                    string selectQuery = "select id,libelle from " + from;
                  
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
        }

        public static DataTable getAutre(string type="")
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
                try
                {
                    //famille_Acte
                    string selectQuery = "select id,libelle from";
                    switch (type)
                    {
                        case "1/4 de Tour":
                            selectQuery += "  base_tour";
                            break;
                        case "Chgt":
                            selectQuery += "  base_chgt";
                            break;
                        case "TIM CL":
                            selectQuery += "  base_tim";
                            break;
                        case "TIM N°":
                            selectQuery += "  base_numlogiciel";
                            break;
                        case "TIM Droite":
                            selectQuery += "  base_droit";
                            break;
                        case "TIM Gauche":
                            selectQuery += "  base_gauche";
                            break;
                      //  case "blanchiment":
                      //      selectQuery += "  base_blanchiment";
                       //     break;
                        case "Diaporama":
                            selectQuery += "  base_diaporama";
                            break;
                        case "Accelerateur":
                            selectQuery += "  base_accelerateur order by id desc ";
                            break;

                    }
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
        }
        public static DataRow getAutreById(int id,string type="")
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
                try
                {
                    //famille_Acte
                    string selectQuery = "select id,libelle from";

                    switch (type)
                    {
                        case "1/4 Tour":
                            selectQuery += "  base_tour";
                            break;
                        case "Chgt":
                            selectQuery += "  base_chgt";
                            break;
                        case "TIM CL":
                                selectQuery += "  base_tim";
                                break;
                        case  "TIM N°":
                                selectQuery += "  base_numlogiciel";
                            break;
                        case "TIM Droit":
                                selectQuery += "  base_droit";
                            break;
                        case  "TIM Gauche":
                                selectQuery += "  base_gauche";
                                break;
                        case  "Diaporama":
                             selectQuery += "  base_diaporama";
                             break;
                        case    "Accelerateur":
                             selectQuery += "  base_accelerateur";
                             break;

                    }
                    selectQuery += " where id=@id";
                    MySqlCommand command = new MySqlCommand(selectQuery, connection);
                    command.Parameters.AddWithValue("@id", id);
                    DataSet ds = new DataSet();
                    MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                    adapt.Fill(ds);

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
        }
        public static void AddAutre(Autre tour)
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
                    string selectQuery = "select MAX(ID)+1 as NEWID from TOUR";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.CommandType = CommandType.Text;
                    object res = command.ExecuteScalar();
                    if (res is DBNull)
                        tour.id = 1;
                    else
                        tour.id = Convert.ToInt32(command.ExecuteScalar());

                    selectQuery = "insert into base_tour (id, libelle) values (@id, @libelle)";

                    command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@id", tour.id);
                    command.Parameters.AddWithValue("@libelle", tour.Libelle);
                  

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
}
