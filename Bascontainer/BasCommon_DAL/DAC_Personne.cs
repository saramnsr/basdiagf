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



        public static DataTable getSmallCorrespondants(string Param, TypePersonne typefiltre)
        {

            if (connection == null) getConnection(); 

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {




                string selectQuery = "SELECT distinct personne.ID_PERSONNE,";
                selectQuery += " PER_NOM,";
                selectQuery += " PER_PRENOM, ";
                selectQuery += " PER_TELPRINC,";
                selectQuery += " PER_EMAIL";
                selectQuery += " FROM personne ";                 
                selectQuery += " Where 1=1 AND TYPE_MATERIEL IS NULL";

                
                if ((typefiltre != null))
                    selectQuery += " and personne.per_type = @type";

                if (Param != "")
                {

                    foreach (string s in Param.Split(';'))
                    {
                        selectQuery += " and (UPPER(PER_VILLE) LIKE '" + s.Trim().ToUpper().Replace ("'","''") + "%'";
                        selectQuery += " or UPPER(PER_NOM) LIKE '" + s.Trim().ToUpper().Replace("'", "''") + "%'";
                        selectQuery += " or UPPER(PER_PRENOM) LIKE '" + s.Trim().ToUpper().Replace("'", "''") + "%'";
                        selectQuery += " or UPPER(PER_EMAIL) LIKE '" + s.Trim().ToUpper().Replace("'", "''") + "%'";
                        selectQuery += " or UPPER(PER_TELPRINC) LIKE '" + s.Trim().ToUpper().Replace("'", "''") + "%'";
                        selectQuery += " or UPPER(PER_TELTRAV1) LIKE '" + s.Trim().ToUpper().Replace("'", "''") + "%'";
                        selectQuery += " or UPPER(PER_CPOSTAL) LIKE '" + s.Trim().ToUpper().Replace("'", "''") + "%')";
                    }
                }

               
                selectQuery += " order by PER_NOM,PER_PRENOM";


                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                if ((typefiltre != null))
                    command.Parameters.AddWithValue("@type", typefiltre.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);


                return ds.Tables[0];



            }
            catch (System.IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
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


        public static DataTable getSmallCorrespondants(string Param)
        {
            return getSmallCorrespondants(Param, null);
        }

      


        public static DataTable getSmallPatientsWiththeSameCorrespondant(LienCorrespondant lc)
        {
            if (connection == null) getConnection(); 

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {


                string selectQuery = "select personne.id_personne,";
                selectQuery += " personne.per_nom,";
                selectQuery += " personne.per_prenom,";
                selectQuery += " personne.PER_TELPRINC,";
                selectQuery += " personne.PER_EMAIL";
                selectQuery += " from lienpers";
                selectQuery += " inner join personne on lienpers.id_patient=personne.id_personne";
                selectQuery += " where  lienpers.id_personne = @id";
                selectQuery += " and relation = @relation";
                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", lc.IdCorrespondance);
                command.Parameters.AddWithValue("@relation", lc.TypeDeLien);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                return dt;

            }
            catch (System.IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
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
