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
    #region Villes


    public static partial class DAC
    {

        public static DataTable getVilles()
        {
            lock (lockobj)
            {
                if (connection == null) getConnection();  

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "SELECT";
                    selectQuery += " id_ville, ";
                    selectQuery += " nomville, ";
                    selectQuery += " codepostal, ";
                    selectQuery += " latitude, ";
                    selectQuery += " longitude ";
                    selectQuery += " FROM villes ";
                    selectQuery += " order by nomville ";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                    DataSet ds = new DataSet();
                    MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                    adapt.Fill(ds);
                    transaction.Commit();

                    DataTable dt = ds.Tables[0];

                    return dt;

                }
                catch (System.IndexOutOfRangeException)
                {
                    return null;
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


        public static void UpdateEloignementVille(Ville v)
        {
            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "Update villes set eloignement=@eloignement where ID_VILLE=@id";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@eloignement", v.Eloignement);
                    command.Parameters.AddWithValue("@id", v.Id);


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


        public static DataTable getVilleNames()
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();  

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "SELECT distinct";
                    selectQuery += " nomville ";
                    selectQuery += " FROM villes ";


                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                    DataSet ds = new DataSet();
                    MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                    adapt.Fill(ds);
                    transaction.Commit();


                    return ds.Tables[0];

                }
                catch (System.IndexOutOfRangeException)
                {
                    return null;
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
        }

        public static DataTable getVillesFromParam(string param)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();  

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "SELECT";
                    selectQuery += " id_ville, ";
                    selectQuery += " nomville, ";
                    selectQuery += " Longitude, ";
                    selectQuery += " Latitude, ";
                    selectQuery += " codepostal";
                    selectQuery += " FROM villes ";

                    if (param != "")
                    {
                        selectQuery += " WHERE UPPER(nomville) LIKE '" + param.Trim().ToUpper() + "%'";
                        selectQuery += " OR codepostal LIKE '" + param.Trim() + "%'";
                    }
                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                    DataSet ds = new DataSet();
                    MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                    adapt.Fill(ds);
                    transaction.Commit();


                    return ds.Tables[0];

                }
                catch (System.IndexOutOfRangeException)
                {
                    return null;
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
        }

        public static string getVilleFromIdPersonne(int id)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();  

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "SELECT contact.ville";
                    selectQuery += " FROM contact";
                    selectQuery += " where contact.id_personne = @id";
                    selectQuery += " and contact.id_contactlibelle = 20";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@id", id);

                    command.CommandType = CommandType.Text;
                    string ville = Convert.ToString(command.ExecuteScalar());

                    command.ExecuteNonQuery();

                    transaction.Commit();
                    return ville;

                }
                catch (System.IndexOutOfRangeException)
                {
                    return null;
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

        public static void addVille(Ville ville)
        {
            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {

                    string selectQuery = "select MAX(id_ville)+1 as NEWID from villes";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    int Id = 1;
                    try
                    {
                        Id = Convert.ToInt32(command.ExecuteScalar());
                    }
                    catch (System.Exception) { Id = 1; }

                    selectQuery = "insert into villes (";
                    selectQuery += "id_ville, ";
                    selectQuery += "nomville, ";
                    selectQuery += "majnomville, ";
                    selectQuery += "codepostal, ";
                    selectQuery += "codeinsee,";
                    selectQuery += "coderegion,";
                    selectQuery += "longitude,";
                    selectQuery += "latitude";
                    selectQuery += ") ";
                    selectQuery += "values (";
                    selectQuery += "@id_ville, ";
                    selectQuery += "@nomville, ";
                    selectQuery += "@majnomville, ";
                    selectQuery += "@codepostal, ";
                    selectQuery += "@codeinsee, ";
                    selectQuery += "@coderegion, ";
                    selectQuery += "@longitude,";
                    selectQuery += "@latitude";
                    selectQuery += ")";

                    command.CommandText = selectQuery;

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_ville", Id);
                    command.Parameters.AddWithValue("@nomville", ville.NomVille);
                    command.Parameters.AddWithValue("@majnomville", ville.NomVille.ToUpper());
                    command.Parameters.AddWithValue("@codepostal", ville.CodePostal);
                    command.Parameters.AddWithValue("@codeinsee", "");
                    command.Parameters.AddWithValue("@coderegion", "");
                    command.Parameters.AddWithValue("@longitude", 0);
                    command.Parameters.AddWithValue("@latitude", 0);

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

        public static DataRow isInDb(Ville ville)
        {
            lock (lockobj)
            {
                if (connection == null) getConnection();  

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "SELECT count(*) Nb";
                    selectQuery += " FROM villes";
                    selectQuery += " where nomville = @nomville ";
                    selectQuery += " and codepostal = @cp";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@nomville", ville.NomVille);
                    command.Parameters.AddWithValue("@cp", ville.CodePostal);

                    DataSet ds = new DataSet();
                    MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                    adapt.Fill(ds);
                    transaction.Commit();

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
        }

    }

#endregion
}
