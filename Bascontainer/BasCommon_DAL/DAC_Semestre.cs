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


        public static DataTable getSemestres(basePatient patient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectquery = "select base_semestre.id, ";
                selectquery += "       base_semestre.id_traitement, ";
                selectquery += "       base_semestre.codesemestre, ";
                selectquery += "       base_semestre.id_acte_gestion, ";
                selectquery += "       base_semestre.montant_semestre, ";
                selectquery += "       base_semestre.datedebut, ";
                selectquery += "       base_semestre.id_dep, ";
                selectquery += "       base_semestre.datefin, ";
                selectquery += "       base_semestre.MONTANT_SEMESTRE_AvantRemise, ";                
                selectquery += "       base_semestre.numsemestre";
                selectquery += " from base_semestre";
                selectquery += " inner join base_plan_traitements on base_semestre.ID_TRAITEMENT = base_plan_traitements.ID";
                selectquery += " inner join base_propositions on base_propositions.ID = base_plan_traitements.ID_PROPOSITION";
                selectquery += " where base_propositions.ID_PATIENT = @id";
                selectquery += " order by base_semestre.datedebut";


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



        public static void DeleteSemestre(int id)
        {


            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {


                string selectQuery = "delete from BASE_Semestre";
                selectQuery += " where (id = @id)";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                command.Parameters.AddWithValue("@id", id);

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


        public static void AssocierDEP(Semestre sem, EntentePrealable dep)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "update base_Semestre";
                selectQuery += "  set ID_DEP = @ID_DEP";
                selectQuery += " where (id = @id)";




                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;




                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@ID_DEP", dep.IdModele);
                command.Parameters.AddWithValue("@id", sem.Id);
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


        public static int getIdSemestre(ActePG act)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select IDSEM_PTA ";
                selectQuery += " from base_traitement";
                selectQuery += " where ID = @ID ";
                selectQuery += " order by date_debut";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@ID", act.Id);


                object obj = command.ExecuteScalar();
                if (obj != null) return Convert.ToInt32(obj);

                return -1;

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


        public static void UpdateSemestre(Semestre sem)
        {

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {


                string selectquery = "update base_semestre set ";
                selectquery += "                           id = @id,";
                selectquery += "                           id_traitement=@id_traitement, ";
                selectquery += "                           codesemestre=@codesemestre, ";
                selectquery += "                           id_acte_gestion=@id_acte_gestion, ";
                selectquery += "                           montant_semestre=@montant_semestre, ";
                selectquery += "                           MONTANT_SEMESTRE_AvantRemise=@MONTANT_SEMESTRE_AvantRemise, ";
                selectquery += "                           datedebut=@datedebut, ";
                selectquery += "                           datefin=@datefin, ";
                selectquery += "                           numsemestre=@numsemestre ";
                selectquery += " where id = @id";
                

                MySqlCommand commandt = new MySqlCommand(selectquery, connection, transaction);
                commandt.Parameters.Clear();
                commandt.Parameters.AddWithValue("@id", sem.Id);
                commandt.Parameters.AddWithValue("@id_traitement", sem.Parent.Id);
                commandt.Parameters.AddWithValue("@codesemestre", sem.CodeSemestre);
                commandt.Parameters.AddWithValue("@id_acte_gestion", sem.traitementSecu.Id);
                commandt.Parameters.AddWithValue("@montant_semestre", sem.Montant_Honoraire);
                commandt.Parameters.AddWithValue("@MONTANT_SEMESTRE_AvantRemise", sem.Montant_AvantRemise);
                
                commandt.Parameters.AddWithValue("@datedebut", sem.DateDebut);
                commandt.Parameters.AddWithValue("@datefin", sem.DateFin);
                commandt.Parameters.AddWithValue("@numsemestre", sem.NumSemestre);

                commandt.ExecuteNonQuery();




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

        public static void DeleteSemestre(Semestre sem)
        {

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectquery = "update base_semestre";
                selectquery += " set numsemestre=numsemestre-1 ";
                selectquery += " where numsemestre>@numsemestre and id_traitement in (select id_traitement from base_semestre where id = @id)";

                MySqlCommand commandt = new MySqlCommand(selectquery, connection, transaction);

                commandt.Parameters.AddWithValue("@numsemestre", sem.NumSemestre);
                commandt.Parameters.AddWithValue("@id", sem.Id);


                commandt.ExecuteNonQuery();

                selectquery = "delete from base_semestre where id = @id";


                commandt.CommandText = selectquery;


                commandt.ExecuteNonQuery();


                selectquery = "delete from BAS_ECHEANCES_DEVIS";
                selectquery += " where BAS_ECHEANCES_DEVIS.ID_SEM_PROPOSE = @id";

                commandt.CommandText = selectquery;


                commandt.ExecuteNonQuery();


                selectquery = "delete from base_plan_traitements";
                selectquery += " where id in (";
                selectquery += " select base_plan_traitements.id";
                selectquery += " from base_plan_traitements";
                selectquery += " left outer join base_semestre on base_semestre.id_traitement = base_plan_traitements.id";
                selectquery += " inner join base_propositions on base_propositions.id = base_plan_traitements.id_proposition";
                selectquery += " group by base_plan_traitements.id";
                selectquery += " having count(base_semestre.id)=0";
                selectquery += " )";


                commandt.CommandText = selectquery;

                commandt.ExecuteNonQuery();

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

        public static void AddSemestre(Semestre sem)
        {

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQueryId = "select max(id)+1 as ID from base_semestre";
                MySqlCommand commandid = new MySqlCommand(selectQueryId, connection, transaction);
                object obj = commandid.ExecuteScalar();
                if (obj == DBNull.Value)
                    sem.Id = 1;
                else
                    sem.Id = Convert.ToInt32(obj);


                string selectquery = "insert into base_semestre (";
                selectquery += "                           id,";
                selectquery += "                           id_traitement, ";
                selectquery += "                           codesemestre, ";
                selectquery += "                           id_acte_gestion, ";
                selectquery += "                           montant_semestre, ";
                selectquery += "                           MONTANT_SEMESTRE_AvantRemise, ";
                selectquery += "                           datedebut, ";
                selectquery += "                           datefin, ";
                selectquery += "                           numsemestre)";
                selectquery += "values (@id, ";
                selectquery += "        @id_traitement, ";
                selectquery += "        @codesemestre, ";
                selectquery += "        @id_acte_gestion, ";
                selectquery += "        @montant_semestre, ";
                selectquery += "        @MONTANT_SEMESTRE_AvantRemise, ";
                selectquery += "        @datedebut, ";
                selectquery += "        @datefin, ";
                selectquery += "        @numsemestre)";


                



                MySqlCommand commandt = new MySqlCommand(selectquery, connection, transaction);
                commandt.Parameters.Clear();
                commandt.Parameters.AddWithValue("@id", sem.Id);
                commandt.Parameters.AddWithValue("@id_traitement", sem.Parent.Id);
                commandt.Parameters.AddWithValue("@codesemestre", sem.CodeSemestre);
                commandt.Parameters.AddWithValue("@id_acte_gestion", sem.traitementSecu.Id);
                commandt.Parameters.AddWithValue("@montant_semestre", sem.Montant_Honoraire);
                commandt.Parameters.AddWithValue("@MONTANT_SEMESTRE_AvantRemise", sem.Montant_AvantRemise);
                commandt.Parameters.AddWithValue("@datedebut", sem.DateDebut);
                commandt.Parameters.AddWithValue("@datefin", sem.DateFin);
                commandt.Parameters.AddWithValue("@numsemestre", sem.NumSemestre);

                commandt.ExecuteNonQuery();




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

        public static DataTable getSemestres(Traitement traitement)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectquery = "select id, ";
                selectquery += "       id_traitement, ";
                selectquery += "       codesemestre, ";
                selectquery += "       id_acte_gestion, ";
                selectquery += "       montant_semestre, ";
                selectquery += "       MONTANT_SEMESTRE_AvantRemise, ";
                selectquery += "       datedebut, ";
                selectquery += "       id_dep, ";
                selectquery += "       datefin, ";
                selectquery += "       numsemestre";
                selectquery += " from base_semestre";
                selectquery += " where base_semestre.id_traitement = @id";


                MySqlCommand command = new MySqlCommand(selectquery, connection, transaction);
                command.Parameters.AddWithValue("@id", traitement.Id);

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
