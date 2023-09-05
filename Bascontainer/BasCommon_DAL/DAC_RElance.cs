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


        public static void ChangeStatusRelance(int idpatient, Relance.ModeRelance niveau)
        {


            int Id = 0;

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();


            try
            {

                string selectQueryupd = "update base_echeance";

                switch (niveau)
                {
                    case Relance.ModeRelance.Releve: selectQueryupd += " set base_echeance.relevedecompte=current_timestamp"; break;
                    case Relance.ModeRelance.Relance1: selectQueryupd += " set base_echeance.relance=current_timestamp"; break;
                    case Relance.ModeRelance.PreContentieux: selectQueryupd += " set base_echeance.precontentieux=current_timestamp"; break;
                    case Relance.ModeRelance.PreContentieux2: selectQueryupd += " set base_echeance.majoration=current_timestamp"; break;
                    case Relance.ModeRelance.Contentieux: selectQueryupd += " set base_echeance.contentieux=current_timestamp"; break;
                }
                selectQueryupd += " where base_echeance.MONTANT>0 and (base_echeance.id_encaissement is null or (base_echeance.id_encaissement < 1) ) and base_echeance.dteecheance<=current_date and base_echeance.typepayeur=0 and ( base_echeance.parprelevement = 'False' or  base_echeance.parprelevement = '0' )";

                MySqlCommand commandupd = new MySqlCommand(selectQueryupd, connection, transaction);

                commandupd.ExecuteNonQuery();

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
