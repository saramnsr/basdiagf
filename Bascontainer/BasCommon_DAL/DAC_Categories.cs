using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using FirebirdSql.Data.FirebirdClient;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;

namespace BasCommon_DAL
{
    public static partial class DAC
    {

        public static void updateNote(CustomCategorie custo)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "update base_histo_categorie set ";
                    selectQuery += "date_fin_categ = @date_fin_categ";
                    selectQuery += " where (id_categorie = @id_categorie)";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.CommandType = CommandType.Text;
                    command.CommandText = selectQuery;

                    command.Parameters.AddWithValue("@id_categorie", custo.IdCateg);
                    command.Parameters.AddWithValue("@date_fin_categ", DateTime.Now);

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

        public static void AffectNote(int IdPersonne, int note)
        {
            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "update base_histo_categorie set ";
                    selectQuery += " date_fin_categ = @date_fin_categ";
                    selectQuery += " where base_histo_categorie.ID_PERSONNE = @id";
                    selectQuery += " and id_categorie is null ";
                    selectQuery += " and date_fin_categ is null";
                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.CommandType = CommandType.Text;
                    command.CommandText = selectQuery;

                    command.Parameters.AddWithValue("@id", IdPersonne);
                    command.Parameters.AddWithValue("@date_fin_categ", DateTime.Now);

                    command.ExecuteNonQuery();


                    selectQuery = "select MAX(ID)+1 as NEWID from base_histo_categorie";

                    command.CommandText = selectQuery;


                    object obj = command.ExecuteScalar();
                    int id = 1;

                    if (!(obj is DBNull))
                        id = Convert.ToInt32(obj);


                    selectQuery = "insert into base_histo_categorie (";
                    selectQuery += " ID,";
                    selectQuery += " ID_PERSONNE,";
                    selectQuery += " DATE_CATEG,";
                    selectQuery += " DATE_FIN_CATEG,";
                    selectQuery += " ID_CATEGORIE,";
                    selectQuery += " NOTE";
                    selectQuery += ") values (";
                    selectQuery += " @ID,";
                    selectQuery += " @ID_PERSONNE,";
                    selectQuery += " @DATE_CATEG,";
                    selectQuery += " NULL,";
                    selectQuery += " NULL,";
                    selectQuery += " @Note)";

                    command.CommandText = selectQuery;

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@ID_PERSONNE", IdPersonne);
                    command.Parameters.AddWithValue("@DATE_CATEG", DateTime.Now);
                    command.Parameters.AddWithValue("@Note", note);

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
        public static int GetCurrentNotesFromIdPersonne(int id_personne)
        {
            string json = DAC.getMethodeJsonString("/CurrentNotesFromIdPersonne/" + id_personne); 
            if(json == null || json == "") return 0;

            return Convert.ToInt32(json);
        }
        public static int GetCurrentNotesFromIdPersonneOLD(int id_personne)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection(); 

                if (connection.State == ConnectionState.Closed) connection.Open();             
                try
                {
                    string selectQuery = "SELECT base_histo_categorie.NOTE ";
                    selectQuery += "FROM base_histo_categorie ";
                    selectQuery += "WHERE base_histo_categorie.id_personne = @id_personne ";
                    selectQuery += "AND ID_CATEGORIE is null ";
                    selectQuery += "AND DATE_FIN_CATEG is null";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection);
                    command.Parameters.AddWithValue("id_personne", id_personne);

                    object obj = command.ExecuteScalar();
                    if (obj is DBNull) return 0;
                    else return (Convert.ToInt32(obj));

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

        public static DataTable getNotesFromIdPersonne(int id_personne)
        {
            lock (lockobj)
            {
                if (connection == null) getConnection();  

                if (connection.State == ConnectionState.Closed) connection.Open();
                try
                {
                    string selectQuery = "SELECT base_histo_categorie.id, ";
                    selectQuery += "base_histo_categorie.id_personne, ";
                    selectQuery += "base_histo_categorie.date_categ, ";
                    selectQuery += "base_histo_categorie.date_fin_categ, ";
                    selectQuery += "base_histo_categorie.id_categorie, ";
                    selectQuery += "base_histo_categorie.NOTE, ";
                    selectQuery += "'' CATEG ";
                    selectQuery += "FROM base_histo_categorie ";
                    selectQuery += "WHERE base_histo_categorie.id_personne = @id_personne and base_histo_categorie.ID_CATEGORIE is null";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection);
                    command.Parameters.AddWithValue("id_personne", id_personne);

                    DataSet ds = new DataSet();
                    MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                    adapt.Fill(ds);

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
                
        public static DataTable getNotes()
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();  

                if (connection.State == ConnectionState.Closed) connection.Open();
                try
                {
                    string selectQuery = "SELECT base_histo_categorie.id, ";
                    selectQuery += "base_histo_categorie.id_personne, ";
                    selectQuery += "base_histo_categorie.date_categ, ";
                    selectQuery += "base_histo_categorie.date_fin_categ, ";
                    selectQuery += "base_histo_categorie.id_categorie, ";
                    selectQuery += "base_histo_categorie.NOTE ";
                    selectQuery += "categories.id_categ, ";
                    selectQuery += "categories.categ ";
                    selectQuery += "FROM base_histo_categorie ";
                    selectQuery += "INNER JOIN categories on categories.id_categ = base_histo_categorie.id_categorie ";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection);

                    DataSet ds = new DataSet();
                    MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                    adapt.Fill(ds);

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

        public static DataTable getCategoriesFromIdPersonne(int id_personne)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();  

                if (connection.State == ConnectionState.Closed) connection.Open();
                try
                {
                    string selectQuery = "SELECT base_histo_categorie.id, ";
                    selectQuery += "base_histo_categorie.id_personne, ";
                    selectQuery += "base_histo_categorie.date_categ, ";
                    selectQuery += "base_histo_categorie.date_fin_categ, ";
                    selectQuery += "base_histo_categorie.id_categorie, ";
                    selectQuery += "base_histo_categorie.NOTE, ";
                    selectQuery += "categories.id_categ, ";
                    selectQuery += "categories.categ ";
                    selectQuery += "FROM base_histo_categorie ";
                    selectQuery += "INNER JOIN categories on categories.id_categ = base_histo_categorie.id_categorie ";
                    selectQuery += "WHERE base_histo_categorie.id_personne = @id_personne ";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection);
                    command.Parameters.AddWithValue("id_personne", id_personne);

                    DataSet ds = new DataSet();
                    MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                    adapt.Fill(ds);

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

        public static DataTable getCategories()
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();  

                if (connection.State == ConnectionState.Closed) connection.Open();
                try
                {
                    string selectQuery = "SELECT categories.id_categ, ";
                    selectQuery += "categories.categ ";
                    selectQuery += "FROM categories ";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection);

                    DataSet ds = new DataSet();
                    MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                    adapt.Fill(ds);

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
        
        public static DataTable getHistoCategories()
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();  

                if (connection.State == ConnectionState.Closed) connection.Open();
                try
                {
                    string selectQuery = "SELECT id, ";
                    selectQuery += "id_personne, ";
                    selectQuery += "date_categ, ";
                    selectQuery += "date_fin_categ, ";
                    selectQuery += "id_categorie, ";
                    selectQuery += "FROM base_histo_categorie ";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection);

                    DataSet ds = new DataSet();
                    MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                    adapt.Fill(ds);

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

        public static void updateCategorieBeToWas(CustomCategorie custo)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "update base_histo_categorie set ";
                    selectQuery += "date_fin_categ = @date_fin_categ";
                    selectQuery += " where (id = @id)";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.CommandType = CommandType.Text;
                    command.CommandText = selectQuery;

                    command.Parameters.AddWithValue("@id", custo.Id);
                    command.Parameters.AddWithValue("@date_fin_categ", DateTime.Now);

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

        public static void updateCategorieWasToBe(CustomCategorie custo)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "select MAX(ID)+1 as NEWID from base_histo_categorie";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.CommandType = CommandType.Text;
                    int id = Convert.ToInt32(command.ExecuteScalar());

                    selectQuery = "insert into base_histo_categorie (";
                    selectQuery += "id,";
                    selectQuery += "id_personne,";
                    selectQuery += "date_categ,";
                    selectQuery += "date_fin_categ,";
                    selectQuery += "id_categorie";
                    selectQuery += ") values ( ";
                    selectQuery += "@id,";
                    selectQuery += "@id_personne,";
                    selectQuery += "@date_categ,";
                    selectQuery += "@date_fin_categ,";
                    selectQuery += "@id_categorie)";

                    command.CommandText = selectQuery;
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@id_personne", custo.IdPersonne);
                    command.Parameters.AddWithValue("@date_categ", DateTime.Now);
                    command.Parameters.AddWithValue("@date_fin_categ", null);
                    command.Parameters.AddWithValue("@id_categorie", custo.IdCateg);

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

        public static void DeleteCategorie(Categorie cat)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {




                    string selectQuery = "delete from base_histo_categorie where base_histo_categorie.ID_categorie = @id_categ";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@id_categ", cat.IdCateg);
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                    transaction.Commit();



                    selectQuery = "delete from CATEGORIES where CATEGORIES.ID_categ = @id_categ";

                    command.CommandText = selectQuery;
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


        public static void InsertCategorie( Categorie cat)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "select MAX(id_categ)+1 as NEWID from CATEGORIES";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.CommandType = CommandType.Text;
                    object o = command.ExecuteScalar();

                    if (o != DBNull.Value)
                        cat.IdCateg = Convert.ToInt32(o);
                    else
                        cat.IdCateg = 1;

                    selectQuery = "insert into CATEGORIES (";
                    selectQuery += "id_categ,";
                    selectQuery += "categ,";
                    selectQuery += "type_categ";
                    selectQuery += ") values ( ";
                    selectQuery += "@id_categ,";
                    selectQuery += "@categ,";
                    selectQuery += "@type_categ";
                    selectQuery += ")";

                    command.CommandText = selectQuery;
                    command.Parameters.AddWithValue("@id_categ", cat.IdCateg);
                    command.Parameters.AddWithValue("@categ", cat.Nom);
                    command.Parameters.AddWithValue("@type_categ", 2);

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


        public static void InsertCategorieToBe(int Idcorres, Categorie cat)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "select MAX(ID)+1 as NEWID from base_histo_categorie";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.CommandType = CommandType.Text;

                    int id = 1;
                    try
                    {
                        id = Convert.ToInt32(command.ExecuteScalar());
                    }
                    catch (System.Exception)
                    {
                        id = 1;
                    }

                    selectQuery = "insert into base_histo_categorie (";
                    selectQuery += "id,";
                    selectQuery += "id_personne,";
                    selectQuery += "date_categ,";
                    selectQuery += "date_fin_categ,";
                    selectQuery += "id_categorie";
                    selectQuery += ") values ( ";
                    selectQuery += "@id,";
                    selectQuery += "@id_personne,";
                    selectQuery += "@date_categ,";
                    selectQuery += "@date_fin_categ,";
                    selectQuery += "@id_categorie)";

                    command.CommandText = selectQuery;
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@id_personne", Idcorres);
                    command.Parameters.AddWithValue("@date_categ", DateTime.Now);
                    command.Parameters.AddWithValue("@date_fin_categ", null);
                    command.Parameters.AddWithValue("@id_categorie", cat.IdCateg);

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

        public static DataTable getCurrentCustomCategories(int id)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection(); 

                if (connection.State == ConnectionState.Closed) connection.Open();
                try
                {
                    string selectQuery = "SELECT base_histo_categorie.id, ";
                    selectQuery += " base_histo_categorie.id_personne, ";
                    selectQuery += " base_histo_categorie.date_categ, ";
                    selectQuery += " base_histo_categorie.date_fin_categ, ";
                    selectQuery += " base_histo_categorie.id_categorie, ";
                    selectQuery += " base_histo_categorie.note, ";
                    selectQuery += " categories.id_categ, ";
                    selectQuery += " categories.categ ";
                    selectQuery += " FROM base_histo_categorie ";
                    selectQuery += " INNER JOIN categories on categories.id_categ = base_histo_categorie.id_categorie ";
                    selectQuery += " where  base_histo_categorie.date_fin_categ is null and base_histo_categorie.id_personne=@id";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection);

                    command.Parameters.AddWithValue("@id", id);

                    DataSet ds = new DataSet();
                    MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                    adapt.Fill(ds);

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

        public static DataTable getCustomCategories()
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();  

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "SELECT base_histo_categorie.id, ";
                    selectQuery += "base_histo_categorie.id_personne, ";
                    selectQuery += "base_histo_categorie.date_categ, ";
                    selectQuery += "base_histo_categorie.date_fin_categ, ";
                    selectQuery += "base_histo_categorie.id_categorie, ";
                    selectQuery += "categories.id_categ, ";
                    selectQuery += "categories.categ, ";
                    selectQuery += "categories.type_categ ";
                    selectQuery += "FROM base_histo_categorie ";
                    selectQuery += "INNER JOIN categories on categories.id_categ = base_histo_categorie.id_categorie ";

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


    }
}
