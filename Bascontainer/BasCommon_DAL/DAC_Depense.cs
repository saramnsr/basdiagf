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
        public static void InsertDepense(Depense depense)
        {

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "select MAX(id)+1 as NEWID from BASE_DEPENSE ";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                object o = command.ExecuteScalar();

                depense.Id = o is DBNull ? 1 : Convert.ToInt32(o);


                selectQuery = "insert into BASE_DEPENSE (id, ";
                selectQuery += "                      DATE_DEPENSE, ";
                selectQuery += "                      DATE_VALEURBQUE, ";
                selectQuery += "                      CODE_DEPENSE, ";
                selectQuery += "                      MONTANT, ";
                selectQuery += "                      MODE_REGLEMENT, ";
                selectQuery += "                      id_banque_remise, ";
                selectQuery += "                      DETAILS)";
                selectQuery += " values (@id, ";
                selectQuery += "                      @DATE_DEPENSE, ";
                selectQuery += "                      @DATE_VALEURBQUE, ";
                selectQuery += "                      @CODE_DEPENSE, ";
                selectQuery += "                      @MONTANT, ";
                selectQuery += "                      @MODE_REGLEMENT, ";
                selectQuery += "                      @id_banque_remise, ";
                selectQuery += "                      @DETAILS)";


                command.CommandText = selectQuery;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", depense.Id);
                command.Parameters.AddWithValue("@DATE_DEPENSE", depense.DateDepense);
                command.Parameters.AddWithValue("@DATE_VALEURBQUE", depense.DateValeurBque);
                command.Parameters.AddWithValue("@CODE_DEPENSE", depense.Code);
                command.Parameters.AddWithValue("@MONTANT", depense.Montant);
                command.Parameters.AddWithValue("@MODE_REGLEMENT", depense.ModeReglement);
                command.Parameters.AddWithValue("@id_banque_remise", depense.banqueDeRemise.Code);
                command.Parameters.AddWithValue("@DETAILS", depense.Details);

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


        public static void UpdateDepense(Depense depense)
        {

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "select MAX(id)+1 as NEWID from BASE_DEPENSE ";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                object o = command.ExecuteScalar();

                depense.Id = o is DBNull ? 1 : Convert.ToInt32(o);


                selectQuery = @"update base_depense
                                set date_depense = @date_depense,
                                    date_valeurbque = @date_valeurbque,
                                    code_depense = @code_depense,
                                    montant = @montant,
                                    mode_reglement = @mode_reglement,
                                    id_entityjuridique = @id_entityjuridique,
                                    id_banque_remise = @id_banque_remise,
                                    details = @details
                                where (id = @id)";


                command.CommandText = selectQuery;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", depense.Id);
                command.Parameters.AddWithValue("@DATE_DEPENSE", depense.DateDepense);
                command.Parameters.AddWithValue("@DATE_VALEURBQUE", depense.DateValeurBque);
                command.Parameters.AddWithValue("@CODE_DEPENSE", depense.Code);
                command.Parameters.AddWithValue("@MONTANT", depense.Montant);
                command.Parameters.AddWithValue("@MODE_REGLEMENT", depense.ModeReglement);
                command.Parameters.AddWithValue("@id_banque_remise", depense.banqueDeRemise.Code);
                command.Parameters.AddWithValue("@DETAILS", depense.Details);

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


        public static DataTable GetDepenses(DateTime dte1,DateTime dte2,EntiteJuridique en)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = @"select id, date_depense, date_valeurbque, code_depense, montant, mode_reglement, id_banque_remise, details
                                        from base_depense";
                selectQuery += "        where date_depense between @dte1 and @dte2 ";
                if (en!=null)
                    selectQuery += "        and  id_entityjuridique=@ien";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@dte1",dte1);
                command.Parameters.AddWithValue("@dte2", dte2);
                if (en != null)
                    command.Parameters.AddWithValue("@ien", en.Id);


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
