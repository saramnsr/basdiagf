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

        public static string GetResumeQ1CS(int IdPatient)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                        string selectQuery = @"select resume from personne
                                                inner join patient on patient.id_personne = personne.id_personne and patient.id_personne=@IdPATIENT
                                                inner join q1cs on q1cs.""USER"" = patient.pat_numdossier ";

                //string selectQuery = @"select resume from q1cs where USER=@IdPATIENT";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@IdPATIENT", IdPatient);


                object o = command.ExecuteScalar();

                return o == null ? "" : (string)o;

               

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
