using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace BasCommon_DAL
{

    public static partial class DAC
    {

        public static DataTable getTaxeValeurAjoutee()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                //famille_Acte
                string selectQuery = @"select  code, libelle, taux from bas_compta_taxevaleurajoutee";

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


    }
}
