using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using BasCommon_BO;

namespace BasCommon_DAL
{
    public static partial class DAC
    {

        public static DataTable Debiteurs()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {

                string selectquery = @"select personne.id_personne,CONCAT(TRIM(personne.per_nom),' ',TRIM(personne.per_prenom)) as patient,base_echeance.libelle ,base_echeance.DTEECHEANCE, sum(base_echeance.montant) montant,
                                        base_echeance.typepayeur,
                                         case
                                        when (DATEDIFF(current_timestamp,base_echeance.dteecheance) >= 0 and DATEDIFF(current_timestamp,base_echeance.dteecheance) < 182) then  1
                                        when (DATEDIFF(current_timestamp,base_echeance.dteecheance) > 182 and DATEDIFF(current_timestamp,base_echeance.dteecheance) < 365) then  2
                                        when (DATEDIFF(current_timestamp,base_echeance.dteecheance) > 365 and DATEDIFF(current_timestamp,base_echeance.dteecheance) < 1095) then  3
                                        when (DATEDIFF(current_timestamp,base_echeance.dteecheance) > 1095) then  4

                                                                           end rangecalendar
                                  
                                        from base_echeance
                                        inner join personne on personne.id_personne = base_echeance.id_patient
                                        where base_echeance.MONTANT>0 and (base_echeance.id_encaissement is null or (base_echeance.id_encaissement < 1) ) and base_echeance.dteecheance<=current_date and base_echeance.typepayeur is not null and ( base_echeance.parprelevement = 'False' or  base_echeance.parprelevement = '0' )
                                         
                                        group by 1,2,3 , 4 ,6,7";

                MySqlCommand command = new MySqlCommand(selectquery, connection);

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
