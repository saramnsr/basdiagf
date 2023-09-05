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

        public static DataTable getProfils()
        {
            lock (lockobj)
            {
                if (connection == null) getConnection();  

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "SELECT id_profil, nom_profil";
                    selectQuery += " FROM profil_internet";

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
