using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BasCommon_BO;
using MySql.Data.MySqlClient;

namespace BasCommon_DAL
{
    public static partial class DAC
    {


        public static DataTable getHistoResponsableByIdPatient(int idpat)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {




                string selectQuery = @"select dateevent, 
                                               userevent, 
                                               idpatient, 
                                               assistante_resp, 
                                               praticien_resp, 
                                               praticien_unique,ASSISTANTE_UNIQUE 
                                        from base_historesp
                                        where idpatient=@idpatient";



                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@idpatient", idpat);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

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
                throw e;
            }
            finally
            {
               connection.Close();

            }

        }


        public static void InsertHistoResponsable(HistoResponsable histo)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                


                string selectQuery = @"insert into base_historesp (dateevent, 
                                                                    userevent, 
                                                                    idpatient, 
                                                                    assistante_resp, 
                                                                    praticien_resp, 
                                                                    praticien_unique,ASSISTANTE_UNIQUE)
                                        values (@dateevent, 
                                                @userevent, 
                                                @idpatient, 
                                                @assistante_resp, 
                                                @praticien_resp, 
                                                @praticien_unique,@ASSISTANTE_UNIQUE)";
                MySqlCommand command = new MySqlCommand(selectQuery,connection,transaction);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@dateevent", histo.DateEvenement);
                command.Parameters.AddWithValue("@userevent", histo.user==null?DBNull.Value:(object)histo.user.Id);
                command.Parameters.AddWithValue("@idpatient", histo.IdPatient);
                command.Parameters.AddWithValue("@assistante_resp", histo.AssistanteResp == null ? DBNull.Value : (object)histo.AssistanteResp.Id);
                command.Parameters.AddWithValue("@praticien_resp", histo.PaticienResp == null ? DBNull.Value : (object)histo.PaticienResp.Id);
                command.Parameters.AddWithValue("@praticien_unique", histo.PraticienUnique);
                command.Parameters.AddWithValue("@ASSISTANTE_UNIQUE", histo.AssistanteUnique);
               

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
    }
}
