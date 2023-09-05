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
    #region Contact


    public static partial class DAC
    {


        public static DataTable getMails(string mail)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
           // MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "SELECT";
                selectQuery += " id,";
                selectQuery += " contacttype,";
                selectQuery += " VALUE,";
                selectQuery += " libelle,";
                selectQuery += " pref_order,";
                selectQuery += " Adr1,";
                selectQuery += " Adr2,";
                selectQuery += " CodePostal,";
                selectQuery += " Ville,";
                selectQuery += " PAYS,";
                selectQuery += " id_personne,";
                selectQuery += " ID_contactLIBELLE,id_pays";

                selectQuery += " FROM contact ";
                selectQuery += " where VALUE=@mail and contacttype = 2";


                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@mail", mail);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
              //  transaction.Commit();

                return ds.Tables[0];

            }
            catch (System.IndexOutOfRangeException)
            {
                return null;
            }
            catch (System.Exception e)
            {
               // transaction.Rollback();
                throw e;
            }
            finally
            {
               connection.Close();

            }
        }

        public static DataTable getContactsOf(int Id)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
           // MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "SELECT";
                selectQuery += " id,";
                selectQuery += " contacttype,";
                selectQuery += " VALUE,";
                selectQuery += " libelle,";
                selectQuery += " pref_order,";
                selectQuery += " Adr1,";
                selectQuery += " Adr2,";
                selectQuery += " CodePostal,";
                selectQuery += " Ville,";
                selectQuery += " PAYS,";
                selectQuery += " id_personne,";
                selectQuery += " ID_contactLIBELLE,id_pays,sms";

                selectQuery += " FROM contact ";
                selectQuery += " where id_personne=@id_personne";


                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@id_personne", Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
              //  transaction.Commit();

                return ds.Tables[0];

            }
            catch (System.IndexOutOfRangeException)
            {
                return null;
            }
            catch (System.Exception e)
            {
               // transaction.Rollback();
                throw e;
            }
            finally
            {
               connection.Close();

            }
        }

        public static DataTable getContactLib()
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
             //   MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "select ID, ";
                    selectQuery += "LIBELLE, ";
                    selectQuery += "TYPEcontact, ";
                    selectQuery += "TYPEAFFECTATION, ";
                    selectQuery += "PRIORITE ";
                    selectQuery += "FROM base_contactlibelle ";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection);

                    DataSet ds = new DataSet();
                    MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                    adapt.Fill(ds);
                   // transaction.Commit();


                    return ds.Tables[0];

                }
                catch (System.IndexOutOfRangeException)
                {
                    return null;
                }
                catch (System.Exception e)
                {
                   // transaction.Rollback();
                    throw e;
                }
                finally
                {
                   connection.Close();

                }
            }
        }

        public static void InsertContactTo(Contact contact)
        {

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "select MAX(id)+1 as NEWID from contact";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                object o = command.ExecuteScalar();

                contact.Id = o is DBNull ? 1 : Convert.ToInt32(o);


                selectQuery = "insert into contact (id, ";
                selectQuery += "                      ID_contactLIBELLE, ";
                selectQuery += "                      contacttype, ";
                selectQuery += "                      VALUE, ";
                selectQuery += "                      pref_order, ";
                selectQuery += "                      Adr1, ";
                selectQuery += "                      Adr2, ";
                selectQuery += "                      CodePostal, ";
                selectQuery += "                      Ville, ";
                selectQuery += "                      PAYS,";
                selectQuery += "                      id_personne,id_pays,sms)";
                selectQuery += " values (@id, ";
                selectQuery += "         @ID_contactLIBELLE, ";
                selectQuery += "         @contacttype, ";
                selectQuery += "         @VALUE, ";
                selectQuery += "         @pref_order, ";
                selectQuery += "         @Adr1, ";
                selectQuery += "         @Adr2, ";
                selectQuery += "         @CodePostal, ";
                selectQuery += "         @Ville, ";
                selectQuery += "         @PAYS,";
                selectQuery += "         @id_personne,@id_pays,@sms)";


                command.CommandText = selectQuery;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", contact.Id);
                command.Parameters.AddWithValue("@contacttype", contact.TypeContact);
                command.Parameters.AddWithValue("@ID_contactLIBELLE", contact.Libelle == null ? DBNull.Value : (object)contact.Libelle.Id);
                command.Parameters.AddWithValue("@VALUE", contact.Value);
                command.Parameters.AddWithValue("@pref_order", contact.prefOrder);

                command.Parameters.AddWithValue("@Adr1", contact.adresse == null ? DBNull.Value : (object)contact.adresse.Adr1);
                command.Parameters.AddWithValue("@Adr2", contact.adresse == null ? DBNull.Value : (object)contact.adresse.Adr2);
                command.Parameters.AddWithValue("@CodePostal", contact.adresse == null ? DBNull.Value : (object)contact.adresse.CodePostal);
                command.Parameters.AddWithValue("@Ville", contact.adresse == null ? DBNull.Value : (object)contact.adresse.Ville);
                command.Parameters.AddWithValue("@PAYS", contact.adresse == null ? DBNull.Value : (object)contact.adresse.Pays);
              //  command.Parameters.AddWithValue("@indicatif", contact.indicatif == null ? DBNull.Value : (object)contact.indicatif);
                command.Parameters.AddWithValue("@sms",  (object)contact.isSms);

           //     if (contact.adresse != null)
             //       command.Parameters.AddWithValue("@id_pays", contact.adresse.pays == null ? DBNull.Value : (object)contact.adresse.pays.id);
           //     else
                    command.Parameters.AddWithValue("@id_pays",  (object)contact.id_pays);

                command.Parameters.AddWithValue("@id_personne", contact.IdPersonne);

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

        public static void UpdateContactTo(Contact contact)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "update contact set ";
                selectQuery += " contacttype = @contacttype,";
                selectQuery += " contact.VALUE = @value,";
                selectQuery += " contact.ID_contactLIBELLE = @ID_contactLIBELLE,";
                //selectQuery += " is_main = @is_main,";
                selectQuery += " Adr1 = @Adr1,";
                selectQuery += " Adr2 = @Adr2,";
                selectQuery += " CodePostal = @CodePostal,";
                selectQuery += " Ville = @Ville,";
                selectQuery += " PAYS = @PAYS,";
                selectQuery += " id_pays = @id_pays, ";
                selectQuery += " sms=@sms";
                selectQuery += " where (id_personne = @id_personne) ";
                selectQuery += " and (id = @id_contact) ";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandText = selectQuery;

                command.Parameters.AddWithValue("@id_personne", contact.IdPersonne);
                command.Parameters.AddWithValue("@id_contact", contact.Id);
                command.Parameters.AddWithValue("@contacttype", contact.TypeContact);
                command.Parameters.AddWithValue("@value", contact.Value);
                command.Parameters.AddWithValue("@ID_contactLIBELLE", contact.Libelle == null ? DBNull.Value : (object)contact.Libelle.Id);
                command.Parameters.AddWithValue("@Adr1", contact.adresse == null ? DBNull.Value : (object)contact.adresse.Adr1);
                command.Parameters.AddWithValue("@Adr2", contact.adresse == null ? DBNull.Value : (object)contact.adresse.Adr2);
                command.Parameters.AddWithValue("@CodePostal", contact.adresse == null ? DBNull.Value : (object)contact.adresse.CodePostal);
                command.Parameters.AddWithValue("@Ville", contact.adresse == null ? DBNull.Value : (object)contact.adresse.Ville);
                command.Parameters.AddWithValue("@PAYS", contact.adresse == null ? DBNull.Value : (object)contact.adresse.Pays);
             //   command.Parameters.AddWithValue("@indicatif", contact.indicatif == null ? DBNull.Value : (object)contact.indicatif);
                command.Parameters.AddWithValue("@sms", (object)contact.isSms);

                //if (contact.adresse != null)
                //    command.Parameters.AddWithValue("@id_pays", contact.adresse.pays == null ? DBNull.Value : (object)contact.adresse.pays.id);
                //else
                       command.Parameters.AddWithValue("@id_pays", (object)contact.id_pays);

                /*
                if (contact.isMain)
                    command.Parameters.AddWithValue("@is_main", 'T');

                if (!contact.isMain)
                    command.Parameters.AddWithValue("@is_main", 'F');
                */
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

        public static void deleteContactOf(int idPersonne)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "DELETE from contact WHERE contact.ID_PERSONNE = @ID";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@ID", idPersonne);

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


        public static void deleteContactFromId(int id)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "DELETE from contact WHERE contact.ID = @ID";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@ID", id);

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

    #endregion
    }




}
