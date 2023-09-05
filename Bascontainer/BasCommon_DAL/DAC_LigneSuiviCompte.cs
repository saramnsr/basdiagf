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
        public static void InsertLigneSuiviCompte(LigneSuiviCompte ligne)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select gen_id(gen_suivicompte,1) as NEWID from rdb$DATABASE";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                ligne.Id = Convert.ToInt32(command.ExecuteScalar());

               




                selectQuery = @"insert into bas_suivicompte (id, 
                                                             id_entite, 
                                                             id_banque, 
                                                             libelle, 
                                                             datecabinet, 
                                                             datebanque, 
                                                             recette, 
                                                             depense, 
                                                             id_paiement, 
                                                             id_depense)
                                values (@id, 
                                        @id_entite, 
                                        @id_banque, 
                                        @libelle, 
                                        @datecabinet, 
                                        @datebanque, 
                                        @recette, 
                                        @depense, 
                                        @id_paiement, 
                                        @id_depense)";

                command.CommandText = selectQuery;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", ligne.Id);
                command.Parameters.AddWithValue("@id_entite", ligne.entite.Id);
                command.Parameters.AddWithValue("@id_banque", ligne.BanqueDeRemise==null?-1:(object)ligne.BanqueDeRemise.Code);
                command.Parameters.AddWithValue("@libelle", ligne.Libelle);
                command.Parameters.AddWithValue("@datecabinet", ligne.DateCabinet == null ? DBNull.Value : (object)ligne.DateCabinet.Value);
                command.Parameters.AddWithValue("@datebanque", ligne.DateBanque == null ? DBNull.Value : (object)ligne.DateBanque.Value);
                command.Parameters.AddWithValue("@recette", ligne.Recette == null ? DBNull.Value : (object)ligne.Recette.Value);
                command.Parameters.AddWithValue("@depense", ligne.Depense == null ? DBNull.Value : (object)ligne.Depense.Value);
                command.Parameters.AddWithValue("@id_paiement", ligne.IdPaiement);
                command.Parameters.AddWithValue("@id_depense", ligne.IdDepense);

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


        public static DataTable GetDepenses(DateTime dtedebut, DateTime dtefin, EntiteJuridique entite)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = @"select id, 
                                           id_entite, 
                                           id_banque, 
                                           libelle, 
                                           datecabinet, 
                                           datebanque, 
                                           recette, 
                                           depense, 
                                           id_paiement, 
                                           id_depense
                                    from bas_suivicompte";
                selectQuery += " where datecabinet between @datecabinetdebut and @datecabinetfin and depense is not null";
                if (entite != null) selectQuery += " and id_entite=@id_entite";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                if (entite!=null)command.Parameters.AddWithValue("@id_entite", entite.Id);
                command.Parameters.AddWithValue("@datecabinetdebut", dtedebut.Date);
                command.Parameters.AddWithValue("@datecabinetfin", dtefin.Date.AddDays(1));


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

    }


}
