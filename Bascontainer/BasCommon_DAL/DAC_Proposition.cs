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

        public static DataRow getProposition(int id)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "       etat, ";
                selectQuery += "       dateevent, ";
                selectQuery += "       tarif, ";
                selectQuery += "       id_patient, ";
                selectQuery += "       ID_MODELE, ";
                selectQuery += "       IDDEVIS, ";

                selectQuery += "       libelle, ";
                selectQuery += "       date_acceptation";
                selectQuery += " from base_propositions";
                selectQuery += " where id = @id";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                    return null;

                return dt.Rows[0];

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


        public static void AccepterProposition(Proposition proposition)
        {

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = " update base_propositions";
                selectQuery += " set etat = @etat,";
                selectQuery += "    dateevent = @dateevent";
                selectQuery += " where etat<1 and IDDEVIS = @IDDEVIS and ID_PATIENT=@ID_PATIENT";



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);


                command.Parameters.Clear();
                command.Parameters.AddWithValue("@ID_PATIENT", proposition.IdPatient);
                command.Parameters.AddWithValue("@etat", Proposition.EtatProposition.Refusé);
                command.Parameters.AddWithValue("@dateevent", DateTime.Now);
                command.Parameters.AddWithValue("@IDDEVIS", proposition.IdDevis);


                command.ExecuteNonQuery();


                selectQuery = " update base_propositions";
                selectQuery += " set etat = @etat,";
                selectQuery += "    dateevent = @dateevent,";
                selectQuery += "    DATE_ACCEPTATION = @DATE_ACCEPTATION";
                selectQuery += " where (ID = @ID)";




                command.CommandText = selectQuery;


                command.Parameters.Clear();
                command.Parameters.AddWithValue("@ID", proposition.Id);
                command.Parameters.AddWithValue("@etat", Proposition.EtatProposition.Accepté);
                command.Parameters.AddWithValue("@dateevent", DateTime.Now);
                command.Parameters.AddWithValue("@DATE_ACCEPTATION", DateTime.Now);


                command.ExecuteNonQuery();


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



        public static void InsertPropositions(Proposition prop)
        {

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQueryId = "select max(id)+1 as ID from base_propositions";
                MySqlCommand commandid = new MySqlCommand(selectQueryId, connection, transaction);
                object obj = commandid.ExecuteScalar();
                if (obj == DBNull.Value)
                    prop.Id = 1;
                else
                    prop.Id = Convert.ToInt32(obj);





                string selectQuery = "insert into base_propositions (id, ";
                selectQuery += "                               etat, ";
                selectQuery += "                               dateevent, ";
                selectQuery += "                               risques, ";
                selectQuery += "                               id_patient, ";
                selectQuery += "                               IDSCENARIO, ";
                selectQuery += "                               DATE_PROPOSITION, ";
                selectQuery += "                               IdDevis, ";
                selectQuery += "                               libelle, ";
                selectQuery += "                               date_acceptation)";
                selectQuery += "values (@id, ";
                selectQuery += "        @etat, ";
                selectQuery += "        @dateevent, ";
                selectQuery += "        @risques, ";
                selectQuery += "        @id_patient, ";
                selectQuery += "        @IDSCENARIO, ";
                selectQuery += "        @DATE_PROPOSITION, ";
                selectQuery += "        @IdDevis, ";
                selectQuery += "        @libelle, ";
                selectQuery += "        @date_acceptation)";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);


                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", prop.Id);
                command.Parameters.AddWithValue("@etat", prop.Etat);
                command.Parameters.AddWithValue("@dateevent", prop.DateEvenement);
                command.Parameters.AddWithValue("@risques", "");
                command.Parameters.AddWithValue("@id_patient", prop.IdPatient);
                command.Parameters.AddWithValue("@DATE_PROPOSITION", prop.DateProposition);
                command.Parameters.AddWithValue("@libelle", prop.libelle);
                command.Parameters.AddWithValue("@date_acceptation", prop.DateAcceptation == null ? (object)DBNull.Value : prop.DateAcceptation.Value);
                command.Parameters.AddWithValue("@IDSCENARIO", prop.IdScenario);

                command.Parameters.AddWithValue("@IdDevis", prop.IdDevis == -1 || prop.IdDevis == 0 ? DBNull.Value : (object)prop.IdDevis);

                command.ExecuteNonQuery();





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


        public static void UpdatePropositions(Proposition proposition)
        {

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = " update base_propositions";
                selectQuery += " set etat = @etat,";
                selectQuery += "    dateevent = @dateevent,";
                selectQuery += "    libelle = @libelle,";
                selectQuery += "    IdDevis = @IdDevis,";
                selectQuery += "    IDSCENARIO = @IDSCENARIO,";
                selectQuery += "    date_acceptation = @date_acceptation,";
                selectQuery += "    DATE_PROPOSITION = @DATE_PROPOSITION";
                selectQuery += " where (id = @id)";



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);


                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", proposition.Id);
                command.Parameters.AddWithValue("@etat", proposition.Etat);
                command.Parameters.AddWithValue("@dateevent", proposition.DateEvenement);
                command.Parameters.AddWithValue("@libelle", proposition.libelle);
                command.Parameters.AddWithValue("@IdDevis", proposition.IdDevis);
                command.Parameters.AddWithValue("@date_acceptation", proposition.DateAcceptation == null ? (object)DBNull.Value : proposition.DateAcceptation.Value);

                command.Parameters.AddWithValue("@DATE_PROPOSITION", proposition.DateProposition);
                command.Parameters.AddWithValue("@IDSCENARIO", proposition.IdScenario);


                command.ExecuteNonQuery();




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

        public static DataTable getPropositions(ModeleDePropositions mdl)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "       etat, ";
                selectQuery += "       dateevent, ";
                selectQuery += "       id_patient, ";
                selectQuery += "       ID_MODELE, ";
                selectQuery += "       IDDEVIS, ";
                selectQuery += "       libelle, ";
                selectQuery += "       IdScenario, ";
                selectQuery += "       date_acceptation,";
                selectQuery += "       DATE_PROPOSITION";
                selectQuery += " from base_propositions";
                selectQuery += " where ID_MODELE = @id";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", mdl.Id);

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


        public static void AddTraitement(Traitement traitement)
        {

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQueryId = "select max(id)+1 as ID from base_plan_traitements";
                MySqlCommand commandid = new MySqlCommand(selectQueryId, connection, transaction);
                object obj = commandid.ExecuteScalar();
                if (obj == DBNull.Value)
                    traitement.Id = 1;
                else
                    traitement.Id = Convert.ToInt32(obj);


                String SelectQuery = " insert into base_plan_traitements (id, ";
                SelectQuery += "                                   Libelle, ";
                SelectQuery += "                                   Phase, ";
                SelectQuery += "                                   CodeTraitement, ";
                SelectQuery += "                                   id_proposition)";
                SelectQuery += " values (@id, ";
                SelectQuery += "        @Libelle, ";
                SelectQuery += "        @Phase, ";
                SelectQuery += "        @CodeTraitement, ";
                SelectQuery += "        @id_proposition)";




                MySqlCommand commandt = new MySqlCommand(SelectQuery, connection, transaction);
                commandt.Parameters.Clear();
                commandt.Parameters.AddWithValue("@id", traitement.Id);
                commandt.Parameters.AddWithValue("@Libelle", traitement.Libelle);
                commandt.Parameters.AddWithValue("@Phase", (int)traitement.Phase);
                commandt.Parameters.AddWithValue("@CodeTraitement", traitement.CodeTraitement);
                commandt.Parameters.AddWithValue("@id_proposition", traitement.IdProposition);

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


        public static void DeleteTraitment(int id)
        {


            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {


                string selectQuery = "delete from BASE_PLAN_TRAITEMENTS";
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

        public static void UpdateTraitement(Traitement traitement)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {


                String SelectQuery = " update base_plan_traitements set ";
                SelectQuery += "                                   Libelle=@Libelle, ";
                SelectQuery += "                                   Phase=@Phase, ";
                SelectQuery += "                                   CodeTraitement=@CodeTraitement, ";
                SelectQuery += "                                   id_proposition=@id_proposition";
                SelectQuery += " where id=@id";




                MySqlCommand commandt = new MySqlCommand(SelectQuery, connection, transaction);
                commandt.Parameters.Clear();
                commandt.Parameters.AddWithValue("@id", traitement.Id);
                commandt.Parameters.AddWithValue("@Libelle", traitement.Libelle);
                commandt.Parameters.AddWithValue("@Phase", (int)traitement.Phase);
                commandt.Parameters.AddWithValue("@CodeTraitement", traitement.CodeTraitement);
                commandt.Parameters.AddWithValue("@id_proposition", traitement.IdProposition);

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



        public static void DeleteProposition(int id)
        {


            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "delete from base_devis_actes";
                selectQuery += " where (ID_PROPOSITION = @id)";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();

                selectQuery = "delete from base_propositions";
                selectQuery += " where (id = @id)";

                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;


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

       







        public static void UpdateProposition(Proposition prop)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {




                string selectQuery = "update base_propositions set ";
                selectQuery += "                               etat = @etat, ";
                selectQuery += "                               dateevent = @dateevent, ";
                selectQuery += "                               risques = @risques, ";
                selectQuery += "                               id_patient = @id_patient, ";
                selectQuery += "                               DATE_PROPOSITION = @DATE_PROPOSITION, ";
                selectQuery += "                               libelle = @libelle, ";
                selectQuery += "                               IDDEVIS = @IDDEVIS, ";

                selectQuery += "                               date_acceptation = @date_acceptation";
                selectQuery += " where id = @id";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);


                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", prop.Id);
                command.Parameters.AddWithValue("@etat", prop.Etat);
                command.Parameters.AddWithValue("@dateevent", prop.DateEvenement);
                command.Parameters.AddWithValue("@risques", "");
                command.Parameters.AddWithValue("@id_patient", prop.IdPatient);
                command.Parameters.AddWithValue("@IDDEVIS", prop.IdDevis);

                command.Parameters.AddWithValue("@DATE_PROPOSITION", prop.DateProposition);
                command.Parameters.AddWithValue("@libelle", prop.libelle);
                command.Parameters.AddWithValue("@date_acceptation", prop.DateAcceptation == null ? (object)DBNull.Value : prop.DateAcceptation.Value);


                command.ExecuteNonQuery();





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



        public static DataTable getTraitements(basePatient pat)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string SelectQuery = " select base_plan_traitements.id, ";
                SelectQuery += "        base_plan_traitements.Libelle, ";
                SelectQuery += "        base_plan_traitements.Phase, ";
                SelectQuery += "        base_plan_traitements.CodeTraitement, ";
                SelectQuery += "        base_plan_traitements.id_proposition ";
                SelectQuery += " from base_plan_traitements";
                SelectQuery += " inner join base_propositions on base_plan_traitements.id_proposition=base_propositions.Id";
                SelectQuery += " where base_propositions.ID_PATIENT = @id";

                MySqlCommand command = new MySqlCommand(SelectQuery, connection);
                command.Parameters.AddWithValue("@id", pat.Id);

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



        public static DataTable getTraitements(Proposition proposition)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string SelectQuery = " select id, ";
                SelectQuery += "        Libelle, ";
                SelectQuery += "        Phase, ";
                SelectQuery += "        CodeTraitement, ";
                SelectQuery += "        id_proposition ";
                SelectQuery += " from base_plan_traitements";
                SelectQuery += " where id_proposition = @id";

                MySqlCommand command = new MySqlCommand(SelectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", proposition.Id);

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


        public static DataRow getModeleDeProposition(int id)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "       libelle ";
                selectQuery += " from BASE_MODEL_PROPOSITION";
                selectQuery += " where id = @id";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                    return null;

                return dt.Rows[0];

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

        public static DataTable getModeleDePropositions()
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "       libelle ";
                selectQuery += " from BASE_MODEL_PROPOSITION";

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

        public static DataTable getPropositions(Devis devis)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "       etat, ";
                selectQuery += "       dateevent, ";
                selectQuery += "       id_patient, ";
                selectQuery += "       ID_MODELE, ";
                selectQuery += "       IDDEVIS, ";
                selectQuery += "       libelle, ";
                selectQuery += "       IdScenario, ";
                selectQuery += "       date_acceptation,";
                selectQuery += "       DATE_PROPOSITION";
                selectQuery += " from base_propositions";
                selectQuery += " where iddevis = @id";
                selectQuery += " order by dateevent asc";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", devis.Id);

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


        public static DataTable getPropositions(basePatient patient)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = "select base_propositions.id, ";
                selectQuery += "       etat, ";
                selectQuery += "       dateevent, ";
                selectQuery += "       base_propositions.id_patient, ";
                selectQuery += "       ID_MODELE, ";
                selectQuery += "       IdScenario, ";
                selectQuery += "       IDDEVIS, ";

                selectQuery += "       libelle, ";
                selectQuery += "       date_acceptation,";
                selectQuery += "       DATE_PROPOSITION";
                selectQuery += " from base_propositions";
                selectQuery += " left outer join base_devis on base_devis.Id=IDDEVIS and base_devis.DateArchivage is null";
                selectQuery += " where base_propositions.id_patient = @id";
                selectQuery += " order by base_propositions.dateevent asc";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
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

        public static DataTable getAllTraitements()
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select * ";

                selectQuery += " from TRAITEMENTS";
          

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


        public static DataTable getSignedPropositions(basePatient patient)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "       etat, ";
                selectQuery += "       dateevent, ";
                selectQuery += "       id_patient, ";
                selectQuery += "       ID_MODELE, ";
                selectQuery += "       IDDEVIS, ";
                selectQuery += "       libelle, ";
                selectQuery += "       IdScenario, ";
                selectQuery += "       date_acceptation,";
                selectQuery += "       DATE_PROPOSITION";

                selectQuery += " from base_propositions";
                selectQuery += " where id_patient = @id and etat = 1";
                selectQuery += " order by date_acceptation";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", patient.Id);

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
