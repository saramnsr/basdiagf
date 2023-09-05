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

        public static void AddMaterielCabinet(baseMaterielCabinet matcab)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select MAX(id_personne)+1 as NEWID from personne"; ;

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                object obj = command.ExecuteScalar();

                if (obj == DBNull.Value) matcab.Id = 1; else matcab.Id = Convert.ToInt32(obj);


                selectQuery = "insert into personne (id_personne, ";
                selectQuery += "                        per_nom, ";
                selectQuery += "                        per_prenom,PER_DATNAISS,per_type,Type_Materiel)";
                selectQuery += " values (@id_personne, ";
                selectQuery += "         @per_nom, ";
                selectQuery += "         @per_prenom,@PER_DATNAISS,@per_type,@Type_Materiel)";



                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@id_personne", matcab.Id);
                command.Parameters.AddWithValue("@per_nom", matcab.Libelle);
                command.Parameters.AddWithValue("@per_prenom", matcab.Description);
                command.Parameters.AddWithValue("@PER_DATNAISS", matcab.DateAchat  );
                command.Parameters.AddWithValue("@per_type", 1);
                command.Parameters.AddWithValue("@Type_Materiel", 1);

                command.ExecuteNonQuery();


                selectQuery = "insert into patient (id_personne, ";
                selectQuery += "                        per_id_personne, ";
                selectQuery += "                        pat_numdossier)";
                selectQuery += " values (@id_personne, ";
                selectQuery += "         @per_id_personne, ";
                selectQuery += "         @pat_numdossier)";



                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@per_id_personne", matcab.Id);
                command.Parameters.AddWithValue("@pat_numdossier", -1);

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

        public static void DeleteMatCab(int id)
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


                    string selectQuery = "delete from personne where id_personne=@id_personne";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@id_personne", id);
                    command.ExecuteNonQuery();

                    selectQuery = "delete from patient where id_personne=@id_personne";
                    command.CommandText = selectQuery;
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
