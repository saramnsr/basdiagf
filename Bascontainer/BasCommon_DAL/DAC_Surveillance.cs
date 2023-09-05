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


        public static DataTable getSurveillances(basePatient patient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectquery = "select base_surveillance.id, ";
                selectquery += "       base_surveillance.ID_SEMESTRE, ";
                selectquery += "       base_surveillance.ID_TRAITMNTSECU, ";
                selectquery += "       base_surveillance.MONTANT, ";
                selectquery += "       base_surveillance.DATEDEBUT, ";
                selectquery += "       base_surveillance.DATEFIN ";
                selectquery += " from base_surveillance";
                selectquery += " inner join base_semestre on base_semestre.ID = base_surveillance.ID_SEMESTRE";
                selectquery += " inner join base_plan_traitements on base_semestre.ID_TRAITEMENT = base_plan_traitements.ID";
                selectquery += " inner join base_propositions on base_propositions.ID = base_plan_traitements.ID_PROPOSITION";
                selectquery += " where base_propositions.ID_PATIENT = @id";

                MySqlCommand command = new MySqlCommand(selectquery, connection);
                command.Parameters.AddWithValue("@id", patient.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

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



        public static DataTable getSurveillances(Semestre semestre)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectquery = "select id, ";
                selectquery += "       ID_SEMESTRE, ";
                selectquery += "       ID_TRAITMNTSECU, ";
                selectquery += "       MONTANT, ";
                selectquery += "       DATEDEBUT, ";
                selectquery += "       DATEFIN ";
                selectquery += " from base_surveillance";
                selectquery += " where base_surveillance.ID_SEMESTRE = @id";
                selectquery += " order by DATEDEBUT asc";


                MySqlCommand command = new MySqlCommand(selectquery, connection);
                command.Parameters.AddWithValue("@id", semestre.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

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
