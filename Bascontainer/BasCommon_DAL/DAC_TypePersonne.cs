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
        public static DataTable getTypePersonnes()
        {
            lock (lockobj)
            {
                if (connection == null) getConnection();  

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {

                    string selectQuery = "SELECT NOM, ";
                    selectQuery += " ID_TYPE as ID, ";
                    selectQuery += " CATEGORIE, ";                    
                    selectQuery += " DisplayOrder ";                    
                    selectQuery += " FROM type_pers ";
                    selectQuery += " Order by NOM";


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
