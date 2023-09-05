using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using System.Configuration;
using BasCommon_BO;

namespace BasCommon_DAL
{
    public static partial class DAC
    {
        public static void InsertRecette(Recette recette)
        {

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "select MAX(id)+1 as NEWID from BASE_RECETTE ";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                object o = command.ExecuteScalar();

                recette.Id = o is DBNull ? 1 : Convert.ToInt32(o);


                selectQuery = "insert into BASE_Recette (id, ";
                selectQuery += "                      DATE_REMISEENBQUE, ";
                selectQuery += "                      DATE_VALEURBQUE, ";
                selectQuery += "                      CODE_Recette, ";
                selectQuery += "                      MONTANT, ";
                selectQuery += "                      id_paiementreel, ";
                selectQuery += "                      ID_BORDEREAU, ";
                selectQuery += "                      id_banque_remise, ";
                selectQuery += "                      DETAILS)";
                selectQuery += " values (@id, ";
                selectQuery += "                      @DATE_REMISEENBQUE, ";
                selectQuery += "                      @DATE_VALEURBQUE, ";
                selectQuery += "                      @CODE_DEPENSE, ";
                selectQuery += "                      @MONTANT, ";
                selectQuery += "                      @idpaiementreel, ";
                selectQuery += "                      @ID_BORDEREAU, ";
                selectQuery += "                      @id_banque_remise, ";
                selectQuery += "                      @DETAILS)";


                command.CommandText = selectQuery;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", recette.Id);
                command.Parameters.AddWithValue("@DATE_REMISEENBQUE", recette.DateRemiseEnBanque);
                command.Parameters.AddWithValue("@DATE_VALEURBQUE", recette.DateValeurBque);
                command.Parameters.AddWithValue("@CODE_DEPENSE", recette.Code);
                command.Parameters.AddWithValue("@MONTANT", recette.Montant);
                command.Parameters.AddWithValue("@idpaiementreel", recette.IDPaiementReel <= 0 ? DBNull.Value : (object)recette.IDPaiementReel);
                command.Parameters.AddWithValue("@ID_BORDEREAU", recette.IDBordereau <= 0 ? DBNull.Value : (object)recette.IDBordereau);
                command.Parameters.AddWithValue("@id_banque_remise", recette.banqueDeRemise.Code);
                command.Parameters.AddWithValue("@DETAILS", recette.Details);

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


        public static void UpdateRecette(Recette recette)
        {

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "select MAX(id)+1 as NEWID from BASE_RECETTE ";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                object o = command.ExecuteScalar();

                recette.Id = o is DBNull ? 1 : Convert.ToInt32(o);


                selectQuery = @"update base_recette
                                set DATE_REMISEENBQUE = @DATE_REMISEENBQUE,
                                    date_valeurbque = @date_valeurbque,
                                    code_recette = @code_recette,
                                    montant = @montant,
                                    idpaiementreel = @idpaiementreel,
                                    idbordereau = @idbordereau,
                                    id_banque_remise = @id_banque_remise,
                                    details = @details
                                where (id = @id)";


                command.CommandText = selectQuery;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", recette.Id);
                command.Parameters.AddWithValue("@DATE_REMISEENBQUE", recette.DateRemiseEnBanque);
                command.Parameters.AddWithValue("@DATE_VALEURBQUE", recette.DateValeurBque);
                command.Parameters.AddWithValue("@CODE_DEPENSE", recette.Code);
                command.Parameters.AddWithValue("@MONTANT", recette.Montant);
                command.Parameters.AddWithValue("@idpaiementreel", recette.IDPaiementReel <= 0 ? DBNull.Value : (object)recette.IDPaiementReel);
                command.Parameters.AddWithValue("@id_banque_remise", recette.banqueDeRemise.Code);
                command.Parameters.AddWithValue("@idbordereau", recette.IDBordereau <= 0 ? DBNull.Value : (object)recette.IDBordereau);
                command.Parameters.AddWithValue("@DETAILS", recette.Details);
                
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


        public static DataTable GetRecettes(DateTime dte1, DateTime dte2, BanqueDeRemise en)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = @"select id, date_remiseenbque, date_valeurbque, code_recette, montant,ID_Bordereau, id_paiementreel, id_banque_remise, details
                                        from base_recette";
                selectQuery += "        where date_remiseenbque between @dte1 and @dte2 ";
                if (en!=null)
                    selectQuery += "        and  id_banque_remise=@ien";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@dte1",dte1);
                command.Parameters.AddWithValue("@dte2", dte2);
                if (en != null)
                    command.Parameters.AddWithValue("@ien", en.Code);


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
