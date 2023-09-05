using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using BasCommon_BO;
using MySql.Data.MySqlClient;

namespace BasCommon_DAL
{
    public static partial class DAC
    {
        public static DataTable getJourFeries()
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += " libelle, ";
                selectQuery += " dte";
                selectQuery += " from rh_base_joursferie";

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

        public static void DelAffectation(AffectationFauteuil af)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "delete from rh_base_affect_faut_user where id=@id";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", af.Id);
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

        public static void DelAffectation(Utilisateur p_utilisateur, DateTime DteStart, DateTime DteEnd)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "delete from rh_base_affect_faut_user where id_user=@id_personne and AFFECTE_FROM>@DteStart and AFFECTE_TO<@DteEnd";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id_personne", p_utilisateur.Id);
                command.Parameters.AddWithValue("@DteStart", DteStart);
                command.Parameters.AddWithValue("@DteEnd", DteEnd);
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

        public static bool IsFauteuilAffected(Utilisateur user, DateTime dteStart, DateTime dteEnd)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "select count(id) ";
                selectQuery += " from rh_base_affect_faut_user";
                selectQuery += " where @dteStart<rh_base_affect_faut_user.AFFECTE_FROM and AFFECTE_TO<@dteEnd";
                selectQuery += " and ID_USER=@IDUSER";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@dteStart", dteStart);
                command.Parameters.AddWithValue("@dteEnd", dteEnd);
                command.Parameters.AddWithValue("@IDUSER", user.Id);


                object obj = command.ExecuteScalar();

                if (obj == null) return false;
                return (Convert.ToInt32(obj) > 0);



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

        public static void DeleteJoursFerie()
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "delete from rh_base_joursferie";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

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

        public static void AddJourFerie(JourFerie jf)
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
                string selectQuery = "select MAX(ID)+1 as NEWID from rh_base_joursferie";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                try
                {

                    jf.Id = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (System.InvalidCastException)
                {
                    jf.Id = 1;
                }



                selectQuery = "insert into rh_base_joursferie (id, ";
                selectQuery += " libelle, ";
                selectQuery += " dte)";
                selectQuery += " values (@id, ";
                selectQuery += " @libelle, ";
                selectQuery += " @dte)";
                command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", jf.Id);
                command.Parameters.AddWithValue("@libelle", jf.Libelle);
                command.Parameters.AddWithValue("@dte", jf.Dte);

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

        public static DataTable getAffectationFauteuil()
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string tmpQry = "select id, ";
                tmpQry += "        id_user, ";
                tmpQry += "        id_fauteuil, ";
                tmpQry += "        affecte_from,";
                tmpQry += "        affecte_to,";
                tmpQry += "        remarques";
                tmpQry += " from rh_base_affect_faut_user";




                MySqlCommand command = new MySqlCommand(tmpQry, connection, transaction);


                command.Parameters.Clear();

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];


                return dt;


            }
            catch (System.SystemException ex)
            {

                // transaction.Rollback();
                throw ex;
            }
            finally
            {
               connection.Close();

            }


        }

        public static DataTable getAffectationFauteuil(DateTime Dte)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string tmpQry = "select id, ";
                tmpQry += "        id_user, ";
                tmpQry += "        id_fauteuil, ";
                tmpQry += "        affecte_from,";
                tmpQry += "        affecte_to,";
                tmpQry += "        remarques";
                tmpQry += " from rh_base_affect_faut_user";
                tmpQry += " where @date_affecte between affecte_from and affecte_to";




                MySqlCommand command = new MySqlCommand(tmpQry, connection, transaction);


                command.Parameters.Clear();
                command.Parameters.AddWithValue("@date_affecte", Dte);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];


                return dt;


            }
            catch (System.SystemException ex)
            {

                //transaction.Rollback();
                throw ex;
            }
            finally
            {
               connection.Close();

            }


        }

        public static DataTable getAffectationFauteuil(DateTime DteFrom, DateTime DteTo, Utilisateur user)
        {
            if (user == null) return getAffectationFauteuil(DteFrom, DteTo);

            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string tmpQry = "select id, ";
                tmpQry += "        id_user, ";
                tmpQry += "        id_fauteuil, ";
                tmpQry += "        affecte_from,";
                tmpQry += "        affecte_to,";
                tmpQry += "        remarques";
                tmpQry += " from rh_base_affect_faut_user";
                tmpQry += " where @date_affecte_from < affecte_from and @date_affecte_to > affecte_to and id_user=@id_user";
                tmpQry += " order by affecte_from asc";




                MySqlCommand command = new MySqlCommand(tmpQry, connection, transaction);


                command.Parameters.Clear();
                command.Parameters.AddWithValue("@date_affecte_from", DteFrom);
                command.Parameters.AddWithValue("@date_affecte_to", DteTo);
                command.Parameters.AddWithValue("@id_user", user.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];


                return dt;


            }
            catch (System.SystemException ex)
            {

                // transaction.Rollback();
                throw ex;
            }
            finally
            {
               connection.Close();

            }





        }

        public static DataTable getAffectationFauteuil(DateTime DteFrom, DateTime DteTo)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string tmpQry = "select id, ";
                tmpQry += "        id_user, ";
                tmpQry += "        id_fauteuil, ";
                tmpQry += "        affecte_from,";
                tmpQry += "        affecte_to,";
                tmpQry += "        remarques";
                tmpQry += " from rh_base_affect_faut_user";
                tmpQry += " where @date_affecte_from < affecte_from and @date_affecte_to > affecte_to";
                tmpQry += " order by affecte_from asc";




                MySqlCommand command = new MySqlCommand(tmpQry, connection, transaction);


                command.Parameters.Clear();
                command.Parameters.AddWithValue("@date_affecte_from", DteFrom);
                command.Parameters.AddWithValue("@date_affecte_to", DteTo);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];


                return dt;


            }
            catch (System.SystemException ex)
            {

                // transaction.Rollback();
                throw ex;
            }
            finally
            {
               connection.Close();

            }





        }

        public static void InsertAffectationFauteuils(AffectationFauteuil af)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQueryid = "select MAX(ID)+1 as NEWID from rh_base_affect_faut_user";

                MySqlCommand commandid = new MySqlCommand(selectQueryid, connection, transaction);
                commandid.CommandType = CommandType.Text;

                try
                {
                    af.Id = Convert.ToInt32(commandid.ExecuteScalar());
                }
                catch (InvalidCastException)
                {
                    af.Id = 1;
                }


                string selectQuery = " insert into rh_base_affect_faut_user (id, ";
                selectQuery += "                                       id_user, ";
                selectQuery += "                                       id_fauteuil, ";
                selectQuery += "                                       affecte_from, ";
                selectQuery += "                                       affecte_to, ";
                selectQuery += "                                       remarques)";
                selectQuery += " values (@id, ";
                selectQuery += "         @id_user, ";
                selectQuery += "         @id_fauteuil, ";
                selectQuery += "         @affecte_from, ";
                selectQuery += "         @affecte_to, ";
                selectQuery += "         @remarques)";



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id", af.Id);
                command.Parameters.AddWithValue("@id_user", af.Iduser);
                command.Parameters.AddWithValue("@id_fauteuil", af.fauteuil.Id);
                command.Parameters.AddWithValue("@affecte_from", af.DteFrom);
                command.Parameters.AddWithValue("@affecte_to", af.DteTo);
                command.Parameters.AddWithValue("@remarques", af.Remarque);



                command.ExecuteNonQuery();
                transaction.Commit();



            }
            catch (System.SystemException)
            {
                transaction.Rollback();
                //Le RDV à deja été annulé
            }
            finally
            {
               connection.Close();

            }





        }

        public static void UpdateAffectationFauteuils(AffectationFauteuil af)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = " update rh_base_affect_faut_user";
                selectQuery += " set id_user = @id_user,";
                selectQuery += "     id_fauteuil = @id_fauteuil,";
                selectQuery += "     affecte_from = @affecte_from,";
                selectQuery += "     affecte_to = @affecte_to,";
                selectQuery += "     remarques = @remarques";
                selectQuery += " where (id = @id)";




                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id", af.Id);
                command.Parameters.AddWithValue("@id_user", af.Iduser);
                command.Parameters.AddWithValue("@id_fauteuil", af.fauteuil.Id);
                command.Parameters.AddWithValue("@affecte_from", af.DteFrom);
                command.Parameters.AddWithValue("@affecte_to", af.DteTo);
                command.Parameters.AddWithValue("@remarques", af.Remarque);



                command.ExecuteNonQuery();
                transaction.Commit();



            }
            catch (System.SystemException)
            {
                transaction.Rollback();
                //Le RDV à deja été annulé
            }
            finally
            {
               connection.Close();

            }

        }

        public static DataTable getAffectationFauteuilById(int id,DateTime DteFrom, DateTime DteTo)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {

                string tmpQry = "select id, ";
                tmpQry += "        id_user, ";
                tmpQry += "        id_fauteuil, ";
                tmpQry += "        affecte_from,";
                tmpQry += "        affecte_to,";
                tmpQry += "        remarques";
                tmpQry += " from rh_base_affect_faut_user";
                tmpQry += " where @date_affecte_from < affecte_from and @date_affecte_to > affecte_to and id_fauteuil=@id";
                tmpQry += " order by affecte_from asc";




                MySqlCommand command = new MySqlCommand(tmpQry, connection);


                command.Parameters.Clear();
                command.Parameters.AddWithValue("@date_affecte_from", DteFrom);
                command.Parameters.AddWithValue("@date_affecte_to", DteTo);
                command.Parameters.AddWithValue("@id", id);
                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];


                return dt;


            }
            catch (System.SystemException ex)
            {

                // transaction.Rollback();
                throw ex;
            }
            finally
            {
               connection.Close();

            }





        }
    }
}
