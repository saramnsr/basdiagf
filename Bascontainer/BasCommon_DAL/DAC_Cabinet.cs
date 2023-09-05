using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BasCommon_DAL
{
    public static partial class DAC
    {
        public static DataTable getAllCabinet()
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
                //   MySqlTransaction transaction = connection.BeginTransaction();
                try
                {

                    //famille_Acte
                    string selectQuery = " select * from cabinet ";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection);
                    DataSet ds = new DataSet();
                    MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                    adapt.Fill(ds);
                    //  transaction.Commit();

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
}
