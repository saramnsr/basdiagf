using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using BasCommon_BO;
using MySql.Data.MySqlClient;

namespace BasCommon_DAL
{
    public static partial class DAC
    {

        public static void AffectEchanceToEncaissement(Echeance ech, Encaissement enc)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {




                string selectQuery = "update base_echeance";
                selectQuery += " set ID_ENCAISSEMENT = @ID_ENCAISSEMENT";
                selectQuery += " where (id = @id)";




                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", ech.Id);
                command.Parameters.AddWithValue("@ID_ENCAISSEMENT", enc.Id);

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
    

        public static DataTable getActesPG(FeuilleDeSoin fs)
        {
            if (connection == null) getConnection(); 

            if (connection.State == ConnectionState.Closed) connection.Open();
            //MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "       id_patient, ";
                selectQuery += "       date_debut, ";
                selectQuery += "       nb_jours, ";
                selectQuery += "       nb_mois, ";
                selectQuery += "       id_plan, ";
                selectQuery += "       tarif, ";
                selectQuery += "       id_actegestion, ";
                selectQuery += "       code, ";
                selectQuery += "       libelle, ";
                selectQuery += "       id_prestation, ";
                selectQuery += "       coeff, ";
                selectQuery += "       montant, ";
                selectQuery += "       need_dep, ";
                selectQuery += "       need_fse, ";
                selectQuery += "       motifdepassement, ";
                selectQuery += "       ald, ";
                selectQuery += "       nbkilometre, ";
                selectQuery += "       rno, ";
                selectQuery += "       lieuexecution, ";
                selectQuery += "       exoneration, ";
                selectQuery += "       dimancheetjf, ";
                selectQuery += "       nuit, ";
                selectQuery += "       numdent, ";
                selectQuery += "       accident, ";
                selectQuery += "       dateaccident,";
                selectQuery += "       ID_DEP, ";
                selectQuery += "       ID_FS, ";
                selectQuery += "       NUM_SEMESTRE, ";
                selectQuery += "       NUM_CONTENTION, ";
                selectQuery += "       ISDECOMPOSED, ";
                selectQuery += "       DECOMPOSED, ";
                selectQuery += "       IDSEM_PTA, ";
                selectQuery += "       IDSURV_PTA, ";
                selectQuery += "       IDDEVIS_PTA, ";    
                selectQuery += "       NumMutuelle, ";
                selectQuery += "       ActeCMU, ";
                selectQuery += "       SaleDate,ID_COMM,FACTUREE, ID_FACTURE,ID_ACTE,TYPE_COMMENT,RABAIS  ";
                selectQuery += " from base_traitement";
                selectQuery += " where ID_FS = @ID_FS ";
                selectQuery += " order by date_debut";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@ID_FS", fs.Id);

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
        public static DataTable getActesPGByIdDevis(int id)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
          //  MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "       id_patient, ";
                selectQuery += "       date_debut, ";
                selectQuery += "       nb_jours, ";
                selectQuery += "       nb_mois, ";
                selectQuery += "       id_plan, ";
                selectQuery += "       tarif, ";
                selectQuery += "       id_actegestion, ";
                selectQuery += "       code, ";
                selectQuery += "       libelle, ";
                selectQuery += "       id_prestation, ";
                selectQuery += "       coeff, ";
                selectQuery += "       montant, ";
                selectQuery += "       need_dep, ";
                selectQuery += "       need_fse, ";
                selectQuery += "       motifdepassement, ";
                selectQuery += "       ald, ";
                selectQuery += "       nbkilometre, ";
                selectQuery += "       rno, ";
                selectQuery += "       lieuexecution, ";
                selectQuery += "       exoneration, ";
                selectQuery += "       dimancheetjf, ";
                selectQuery += "       nuit, ";
                selectQuery += "       numdent, ";
                selectQuery += "       accident, ";
                selectQuery += "       dateaccident,";
                selectQuery += "       ID_DEP, ";
                selectQuery += "       ID_FS, ";
                selectQuery += "       NUM_SEMESTRE, ";
                selectQuery += "       NUM_CONTENTION, ";
                selectQuery += "       ISDECOMPOSED, ";
                selectQuery += "       DECOMPOSED, ";
                selectQuery += "       IDSEM_PTA, ";
                selectQuery += "       IDSURV_PTA, ";
                selectQuery += "       IDDEVIS_PTA, ";
                selectQuery += "       NumMutuelle, ";
                selectQuery += "       ActeCMU, ";
                selectQuery += "       SaleDate,ID_COMM,FACTUREE, ID_FACTURE,ID_ACTE,TYPE_COMMENT,RABAIS  ";
                selectQuery += " from base_traitement";
                selectQuery += " where IDDEVIS_PTA = @ID_devis ";
                

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@ID_devis", id);

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
        public static DataTable getActesPG(Semestre sem)
        {
            if (connection == null) getConnection(); 

            if (connection.State == ConnectionState.Closed) connection.Open();
         //   MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "       id_patient, ";
                selectQuery += "       date_debut, ";
                selectQuery += "       nb_jours, ";
                selectQuery += "       nb_mois, ";
                selectQuery += "       id_plan, ";
                selectQuery += "       tarif, ";
                selectQuery += "       id_actegestion, ";
                selectQuery += "       code, ";
                selectQuery += "       libelle, ";
                selectQuery += "       id_prestation, ";
                selectQuery += "       coeff, ";
                selectQuery += "       montant, ";
                selectQuery += "       need_dep, ";
                selectQuery += "       need_fse, ";
                selectQuery += "       motifdepassement, ";
                selectQuery += "       ald, ";
                selectQuery += "       nbkilometre, ";
                selectQuery += "       rno, ";
                selectQuery += "       lieuexecution, ";
                selectQuery += "       exoneration, ";
                selectQuery += "       dimancheetjf, ";
                selectQuery += "       nuit, ";
                selectQuery += "       numdent, ";
                selectQuery += "       accident, ";
                selectQuery += "       dateaccident,";
                selectQuery += "       ID_DEP, ";
                selectQuery += "       ID_FS, ";
                selectQuery += "       NUM_SEMESTRE, ";
                selectQuery += "       NUM_CONTENTION, ";
                selectQuery += "       ISDECOMPOSED, ";
                selectQuery += "       DECOMPOSED, ";
                selectQuery += "       IDSEM_PTA, ";
                selectQuery += "       IDSURV_PTA, ";
                selectQuery += "       IDDEVIS_PTA, ";    
                selectQuery += "       SaleDate, ";
                selectQuery += "       NumMutuelle, ";
                selectQuery += "       ActeCMU, SaleDate,ID_COMM,FACTUREE, ID_FACTURE,ID_ACTE,TYPE_COMMENT,RABAIS ";
                selectQuery += " from base_traitement";
                selectQuery += " where IDSEM_PTA = @IDSEM_PTA ";
                selectQuery += " order by date_debut";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@IDSEM_PTA", sem.Id);

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
        public static DataRow getActesPGByCommentMateriel(int idComment, int IdMateriel)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
           // MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "       id_patient, ";
                selectQuery += "       date_debut, ";
                selectQuery += "       nb_jours, ";
                selectQuery += "       nb_mois, ";
                selectQuery += "       id_plan, ";
                selectQuery += "       tarif, ";
                selectQuery += "       id_actegestion, ";
                selectQuery += "       code, ";
                selectQuery += "       libelle, ";
                selectQuery += "       id_prestation, ";
                selectQuery += "       coeff, ";
                selectQuery += "       montant, ";
                selectQuery += "       need_dep, ";
                selectQuery += "       need_fse, ";
                selectQuery += "       motifdepassement, ";
                selectQuery += "       ald, ";
                selectQuery += "       nbkilometre, ";
                selectQuery += "       rno, ";
                selectQuery += "       lieuexecution, ";
                selectQuery += "       exoneration, ";
                selectQuery += "       dimancheetjf, ";
                selectQuery += "       nuit, ";
                selectQuery += "       numdent, ";
                selectQuery += "       accident, ";
                selectQuery += "       dateaccident,";
                selectQuery += "       ID_DEP, ";
                selectQuery += "       ID_FS, ";
                selectQuery += "       NUM_SEMESTRE, ";
                selectQuery += "       NUM_CONTENTION, ";
                selectQuery += "       ISDECOMPOSED, ";
                selectQuery += "       DECOMPOSED, ";
                selectQuery += "       IDSEM_PTA, ";
                selectQuery += "       IDSURV_PTA, ";
                selectQuery += "       IDDEVIS_PTA, ";
                selectQuery += "       NumMutuelle, ";
                selectQuery += "       ActeCMU, ";
                selectQuery += "       SaleDate,ID_COMM,FACTUREE, ID_FACTURE, ID_ACTE,TYPE_COMMENT,RABAIS ";
                selectQuery += " from base_traitement";
                selectQuery += " where ID_COMM = @idComment and ID_ACTE = @IDMATERIEL AND TYPE_COMMENT = 'M'";


                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@idComment", idComment);
                command.Parameters.AddWithValue("@IDMATERIEL", IdMateriel );


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
              //  transaction.Commit();

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count == 0) return null;

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
        public static DataRow getActesPGByComment(int idComment)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
        //    MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "       id_patient, ";
                selectQuery += "       date_debut, ";
                selectQuery += "       nb_jours, ";
                selectQuery += "       nb_mois, ";
                selectQuery += "       id_plan, ";
                selectQuery += "       tarif, ";
                selectQuery += "       id_actegestion, ";
                selectQuery += "       code, ";
                selectQuery += "       libelle, ";
                selectQuery += "       id_prestation, ";
                selectQuery += "       coeff, ";
                selectQuery += "       montant, ";
                selectQuery += "       need_dep, ";
                selectQuery += "       need_fse, ";
                selectQuery += "       motifdepassement, ";
                selectQuery += "       ald, ";
                selectQuery += "       nbkilometre, ";
                selectQuery += "       rno, ";
                selectQuery += "       lieuexecution, ";
                selectQuery += "       exoneration, ";
                selectQuery += "       dimancheetjf, ";
                selectQuery += "       nuit, ";
                selectQuery += "       numdent, ";
                selectQuery += "       accident, ";
                selectQuery += "       dateaccident,";
                selectQuery += "       ID_DEP, ";
                selectQuery += "       ID_FS, ";
                selectQuery += "       NUM_SEMESTRE, ";
                selectQuery += "       NUM_CONTENTION, ";
                selectQuery += "       ISDECOMPOSED, ";
                selectQuery += "       DECOMPOSED, ";
                selectQuery += "       IDSEM_PTA, ";
                selectQuery += "       IDSURV_PTA, ";
                selectQuery += "       IDDEVIS_PTA, ";
                selectQuery += "       NumMutuelle, ";
                selectQuery += "       ActeCMU, ";
                selectQuery += "       SaleDate,ID_COMM,FACTUREE, ID_FACTURE,ID_ACTE,TYPE_COMMENT,RABAIS ";
                selectQuery += " from base_traitement";
                selectQuery += " where ID_COMM = @idComment ";


                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@idComment", idComment);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
              ///  transaction.Commit();

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count == 0) return null;

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


        public static DataRow getActesPG(int Id)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
           // MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "       id_patient, ";
                selectQuery += "       date_debut, ";
                selectQuery += "       nb_jours, ";
                selectQuery += "       nb_mois, ";
                selectQuery += "       id_plan, ";
                selectQuery += "       tarif, ";
                selectQuery += "       id_actegestion, ";
                selectQuery += "       code, ";
                selectQuery += "       libelle, ";
                selectQuery += "       id_prestation, ";
                selectQuery += "       coeff, ";
                selectQuery += "       montant, ";
                selectQuery += "       need_dep, ";
                selectQuery += "       need_fse, ";
                selectQuery += "       motifdepassement, ";
                selectQuery += "       ald, ";
                selectQuery += "       nbkilometre, ";
                selectQuery += "       rno, ";
                selectQuery += "       lieuexecution, ";
                selectQuery += "       exoneration, ";
                selectQuery += "       dimancheetjf, ";
                selectQuery += "       nuit, ";
                selectQuery += "       numdent, ";
                selectQuery += "       accident, ";
                selectQuery += "       dateaccident,";
                selectQuery += "       ID_DEP, ";
                selectQuery += "       ID_FS, ";
                selectQuery += "       NUM_SEMESTRE, ";
                selectQuery += "       NUM_CONTENTION, ";
                selectQuery += "       ISDECOMPOSED, ";
                selectQuery += "       DECOMPOSED, ";
                selectQuery += "       IDSEM_PTA, ";
                selectQuery += "       IDSURV_PTA, ";
                selectQuery += "       IDDEVIS_PTA, ";    
                selectQuery += "       NumMutuelle, ";
                selectQuery += "       ActeCMU, ";
                selectQuery += "       SaleDate, ID_COMM, FACTUREE, ID_FACTURE, ID_ACTE,TYPE_COMMENT,RABAIS ";
                selectQuery += " from base_traitement";
                selectQuery += " where id = @id ";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@id", Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
               // transaction.Commit();

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count == 0) return null;

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

        public static DataTable getActesPGByPAtient(int IdPatient)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
           // MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
              /*  string selectQuery = "select id, ";
                selectQuery += "       id_patient, ";
                selectQuery += "       date_debut, ";
                selectQuery += "       nb_jours, ";
                selectQuery += "       nb_mois, ";
                selectQuery += "       id_plan, ";
                selectQuery += "       tarif, ";
                selectQuery += "       id_actegestion, ";
                selectQuery += "       code, ";
                selectQuery += "       libelle, ";
                selectQuery += "       id_prestation, ";
                selectQuery += "       coeff, ";
                selectQuery += "       montant, ";
                selectQuery += "       need_dep, ";
                selectQuery += "       need_fse, ";
                selectQuery += "       motifdepassement, ";
                selectQuery += "       ald, ";
                selectQuery += "       nbkilometre, ";
                selectQuery += "       rno, ";
                selectQuery += "       lieuexecution, ";
                selectQuery += "       exoneration, ";
                selectQuery += "       dimancheetjf, ";
                selectQuery += "       nuit, ";
                selectQuery += "       numdent, ";
                selectQuery += "       accident, ";
                selectQuery += "       dateaccident,";
                selectQuery += "       ID_DEP, ";
                selectQuery += "       ID_FS, ";
                selectQuery += "       NUM_SEMESTRE, ";
                selectQuery += "       NUM_CONTENTION, ";
                selectQuery += "       ISDECOMPOSED, ";
                selectQuery += "       DECOMPOSED, ";
                selectQuery += "       IDSEM_PTA, ";
                selectQuery += "       IDSURV_PTA, ";
                selectQuery += "       IDDEVIS_PTA, ";                
                selectQuery += "       NumMutuelle, ";
                selectQuery += "       ActeCMU, ";
                selectQuery += "       SaleDate, ID_COMM, FACTUREE, ID_FACTURE ";
                selectQuery += " from base_traitement";
                selectQuery += " where id_patient = @id_patient ";
                selectQuery += " order by date_debut desc";*/




                string selectQuery = " SELECT * from   (select id,        id_patient,        date_debut,        ";
                selectQuery += " nb_jours,        nb_mois,        id_plan,   tarif,        id_actegestion,   ";
                selectQuery += " code,        libelle,        id_prestation,        coeff,   montant,   ";
                selectQuery += " need_dep,        need_fse,        motifdepassement,        ald,  ";
                selectQuery += " nbkilometre,        rno,        lieuexecution,         ";
                selectQuery += " exoneration,        dimancheetjf,    nuit,        numdent,  ";
                selectQuery += " accident,        dateaccident,       ID_DEP,        ID_FS,   ";
                selectQuery += " NUM_SEMESTRE,        NUM_CONTENTION,        ISDECOMPOSED,   DECOMPOSED,   ";
                selectQuery += " IDSEM_PTA,        IDSURV_PTA,        IDDEVIS_PTA,    NumMutuelle,        ActeCMU,       ";
                selectQuery += " SaleDate, ID_COMM, FACTUREE, ID_FACTURE,TYPE_COMMENT,ID_ACTE  ,rabais  from base_traitement    where id_patient = @id_patient     and id_comm is null  ";
                selectQuery += " union   ";
                selectQuery += " select  COALESCE((SELECT t.ID FROM base_traitement t where TYPE_COMMENT = '0' and ID_COMM =base_traitement.ID_COMM  ),null,  ";
                selectQuery += " (SELECT MIN(t.ID) FROM base_traitement t where  ID_COMM =base_traitement.ID_COMM)) id,    ";
                selectQuery += " id_patient,   coalesce( (SELECT t.date_debut FROM base_traitement t where TYPE_COMMENT = '0' and ID_COMM =base_traitement.ID_COMM  ) ,  ";
                selectQuery += " null,  ";
                selectQuery += " (SELECT MIN(t.DATE_DEBUT) FROM base_traitement t where  ID_COMM =base_traitement.ID_COMM))      date_debut,   ";
                selectQuery += " nb_jours,   nb_mois,  coalesce(  ";
                selectQuery += " (SELECT t.id_plan FROM base_traitement t where TYPE_COMMENT = '0' and ID_COMM =base_traitement.ID_COMM  ) ,  ";
                selectQuery += " null,  ";
                selectQuery += " (SELECT MIN(t.ID_PLAN) FROM base_traitement t where  ID_COMM =base_traitement.ID_COMM))    id_plan,  ";
                selectQuery += " tarif,        id_actegestion,        code,     (SELECT t.libelle FROM base_traitement t where TYPE_COMMENT = '0' and ID_COMM =base_traitement.ID_COMM  ";
                selectQuery += " )      libelle,        id_prestation,        coeff,  (sum(montant)+(select coalesce(sum(bca.MONTANT),null,0) from base_comm_actes bca where bca.ID_COMM = base_traitement.id_comm)";
                selectQuery += " +(select coalesce(sum(BCM.MONTANT),null,0) ";
                selectQuery += " from base_comm_mat BCM ";
                selectQuery += " left join materiels M on M.ID_MATERIEL = BCM.ID_MATERIEL ";
                selectQuery += " LEFT JOIN rh_base_famillemateriel FM on FM.ID = M.ID_FAMILLE_MATERIEL ";
                selectQuery += " where BCM.ID_COMM =  base_traitement.id_comm and  (locate ('laboratoire', lower(FM.NOM))>0 OR  locate ('stérilisation', lower(FM.NOM))>0))),        need_dep,        need_fse,        motifdepassement,        ald,      ";
                selectQuery += " nbkilometre,        rno,        lieuexecution,        exoneration,        dimancheetjf,    nuit,        numdent,        accident,        dateaccident,  ";
                selectQuery += " ID_DEP,        ID_FS,        NUM_SEMESTRE,        NUM_CONTENTION,        ISDECOMPOSED,   DECOMPOSED,        IDSEM_PTA,        IDSURV_PTA,    ";
                selectQuery += " IDDEVIS_PTA,    NumMutuelle,        ActeCMU,        SaleDate, ID_COMM,";
                selectQuery += " COALESCE((SELECT t.FACTUREE FROM base_traitement t where TYPE_COMMENT = '0' and ID_COMM =base_traitement.ID_COMM  ),null,(SELECT MIN(t.FACTUREE) FROM base_traitement t where  ID_COMM =base_traitement.ID_COMM)) FACTUREE ,";
                selectQuery += " COALESCE((SELECT t.ID_FACTURE FROM base_traitement t where TYPE_COMMENT = '0' and ID_COMM =base_traitement.ID_COMM  ),null,(SELECT MIN(t.ID_FACTURE) FROM base_traitement t where  ID_COMM =base_traitement.ID_COMM)) ID_FACTURE,TYPE_COMMENT, ID_ACTE ,rabais  ";
                selectQuery += " from base_traitement    where id_patient = @id_patient   ";
                selectQuery += " and id_comm is not null  and type_comment != 'M'  ";
                selectQuery += " GROUP BY  id,        id_patient,    date_debut,        nb_jours,   nb_mois,        id_plan,   tarif,        id_actegestion,      ";
                selectQuery += " code,        libelle,        id_prestation,        coeff,   need_dep,        need_fse,        motifdepassement,        ald,   ";
                selectQuery += " nbkilometre,        rno,        lieuexecution,        exoneration,        dimancheetjf,    nuit,        numdent,      ";
                selectQuery += " accident,        dateaccident,       ID_DEP,        ID_FS,        NUM_SEMESTRE,        NUM_CONTENTION,       ";
                selectQuery += " ISDECOMPOSED,   DECOMPOSED,        IDSEM_PTA,        IDSURV_PTA,        IDDEVIS_PTA,    NumMutuelle,      ";
                selectQuery += " ActeCMU,        SaleDate, ID_COMM, FACTUREE, ID_FACTURE,TYPE_COMMENT, ID_ACTE ,rabais";
                selectQuery += " union ";
                selectQuery += "  select  id,     id_patient,  ";
                selectQuery += "      date_debut,  ";
                selectQuery += " nb_jours,   nb_mois,     id_plan,   tarif,        id_actegestion,        code, ";
                selectQuery += "     libelle,  ";
                selectQuery += " id_prestation,        coeff,   montant,        need_dep,        need_fse,        motifdepassement,        ald,  ";
                selectQuery += "       nbkilometre,        rno,   ";
                selectQuery += " lieuexecution,        exoneration,        dimancheetjf,    nuit,        numdent,        accident,        dateaccident,  ";
                selectQuery += "   ID_DEP,        ID_FS,  ";
                selectQuery += " NUM_SEMESTRE,        NUM_CONTENTION,        ISDECOMPOSED,   DECOMPOSED,        IDSEM_PTA,        IDSURV_PTA,     IDDEVIS_PTA,   ";
                selectQuery += " NumMutuelle,        ActeCMU,        SaleDate, ID_COMM,  FACTUREE ,  ID_FACTURE,TYPE_COMMENT, ID_ACTE,rabais ";
                selectQuery += "  from base_traitement where id_patient = @id_patient    ";
                selectQuery += " and id_comm is not null and TYPE_COMMENT = 'M' "; 

                selectQuery += " ) as T ";
                //selectQuery += " WHERE (FACTUREE != 2 OR FACTUREE IS NULL) ";
                selectQuery += " ORDER BY 3 DESC    ";


                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@id_patient", IdPatient);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
               // transaction.Commit();

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

        public static double getSolde(Surveillance s)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();  

                if (connection.State == ConnectionState.Closed) connection.Open();
               // MySqlTransaction transaction = connection.BeginTransaction();
                try
                {

                    string selectQuery = "select coalesce(sum(base_echeance.montant),0)";
                    selectQuery += " from base_traitement";
                    selectQuery += " inner join base_echeance on base_echeance.id_traitement = base_traitement.id";
                    selectQuery += " where base_traitement.IDSURV_PTA=@id and  (base_echeance.id_encaissement is null or base_echeance.id_encaissement=-1)";



                    MySqlCommand command = new MySqlCommand(selectQuery, connection);
                    command.Parameters.AddWithValue("@id", s.Id);

                    object o = command.ExecuteScalar();

                    if (o != null) return Convert.ToDouble(o);

                    return 0;

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


        public static double getSolde(Semestre s)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Connecting) connection.Close();
                    connection.Open();
            //    MySqlTransaction transaction = connection.BeginTransaction();
                try
                {

                    string selectQuery = "select coalesce(sum(base_echeance.montant),0)";
                    selectQuery += " from base_traitement";
                    selectQuery += " inner join base_echeance on base_echeance.id_traitement = base_traitement.id";
                    selectQuery += " where base_traitement.IDSEM_PTA=@id and  (base_echeance.id_encaissement is null or base_echeance.id_encaissement=-1)";



                    MySqlCommand command = new MySqlCommand(selectQuery, connection);
                    command.Parameters.AddWithValue("@id", s.Id);

                    object o = command.ExecuteScalar();

                    if (o != null) return Convert.ToDouble(o);

                    return 0;

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


        public static double getSolde(ActePG acte)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();  

                if (connection.State == ConnectionState.Closed) connection.Open();
               // MySqlTransaction transaction = connection.BeginTransaction();
                try
                {

                    string selectQuery = "select coalesce(sum(base_echeance.montant),0)";
                    selectQuery += " from base_traitement";
                    selectQuery += " inner join base_echeance on base_echeance.id_traitement = base_traitement.id";
                    selectQuery += " where base_traitement.id=@id and  (base_echeance.id_encaissement is null or base_echeance.id_encaissement=-1)";



                    MySqlCommand command = new MySqlCommand(selectQuery, connection);
                    command.Parameters.AddWithValue("@id", acte.Id);

                    object o = command.ExecuteScalar();

                    if (o != null) return Convert.ToDouble(o);

                    return 0;

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


        public static void RemoveDEPReference(int IdDEP)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "update base_traitement";
                selectQuery += "  set ";
                selectQuery += "    ID_DEP = NULL";
                selectQuery += " where (ID_DEP = @ID_DEP)";




                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;




                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@ID_DEP", IdDEP);



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

        public static void AddDEPReference(int IdDEP, int IdActe)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "update base_traitement";
                selectQuery += "  set ";
                selectQuery += "    ID_DEP = @ID_DEP";
                selectQuery += " where (ID = @ID)";




                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;




                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@ID_DEP", IdDEP);
                command.Parameters.AddWithValue("@ID", IdActe);



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

        public static void InsertActePG(ActePG actepg)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "select MAX(ID)+1 as NEWID from base_traitement";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                object obj = command.ExecuteScalar();

                if (obj == DBNull.Value)
                    actepg.Id = 1;
                else
                    actepg.Id = Convert.ToInt32(obj);


                selectQuery = "insert into base_traitement (id, ";
                selectQuery += "                             id_patient, ";
                selectQuery += "                             date_debut, ";
                selectQuery += "                             nb_jours, ";
                selectQuery += "                             nb_mois, ";
                selectQuery += "                             id_plan, ";
                selectQuery += "                             tarif, ";
                selectQuery += "                             id_actegestion, ";
                selectQuery += "                             code, ";
                selectQuery += "                             libelle, ";
                selectQuery += "                             id_prestation, ";
                selectQuery += "                             coeff, ";
                selectQuery += "                             montant, ";
                selectQuery += "                             need_dep, ";
                selectQuery += "                             need_fse, ";
                selectQuery += "                             motifdepassement, ";
                selectQuery += "                             ald, ";
                selectQuery += "                             nbkilometre, ";
                selectQuery += "                             rno, ";
                selectQuery += "                             lieuexecution, ";
                selectQuery += "                             exoneration, ";
                selectQuery += "                             dimancheetjf, ";
                selectQuery += "                             nuit, ";
                selectQuery += "                             numdent, ";
                selectQuery += "                             accident, ";
                selectQuery += "                             ID_DEP, ";
                selectQuery += "                             NUM_SEMESTRE, ";
                selectQuery += "                             NUM_CONTENTION, ";
                selectQuery += "                             ISDECOMPOSED, ";
                selectQuery += "                             DECOMPOSED, ";
                selectQuery += "                             ID_FS, ";
                selectQuery += "                             NumMutuelle, ";
                selectQuery += "                             ActeCMU, ";
                selectQuery += "                             IDSEM_PTA, ";
                selectQuery += "                             IDSURV_PTA, ";
                selectQuery += "                             IDDEVIS_PTA, ";
                selectQuery += "                             dateaccident)";
                selectQuery += " values (@id, ";
                selectQuery += "        @id_patient, ";
                selectQuery += "        @date_debut, ";
                selectQuery += "        @nb_jours, ";
                selectQuery += "        @nb_mois, ";
                selectQuery += "        @id_plan, ";
                selectQuery += "        @tarif, ";
                selectQuery += "        @id_actegestion, ";
                selectQuery += "        @code, ";
                selectQuery += "        @libelle, ";
                selectQuery += "        @id_prestation, ";
                selectQuery += "        @coeff, ";
                selectQuery += "        @montant, ";
                selectQuery += "        @need_dep, ";
                selectQuery += "        @need_fse, ";
                selectQuery += "        @motifdepassement, ";
                selectQuery += "        @ald, ";
                selectQuery += "        @nbkilometre, ";
                selectQuery += "        @rno, ";
                selectQuery += "        @lieuexecution, ";
                selectQuery += "        @exoneration, ";
                selectQuery += "        @dimanche_ferie, ";
                selectQuery += "        @nuit, ";
                selectQuery += "        @numdent, ";
                selectQuery += "        @accident, ";
                selectQuery += "        @ID_DEP, ";
                selectQuery += "        @NUM_SEMESTRE, ";
                selectQuery += "        @NUM_CONTENTION, ";
                selectQuery += "        @ISDECOMPOSED, ";
                selectQuery += "        @DECOMPOSED, ";
                selectQuery += "        @ID_FS, ";
                selectQuery += "        @NumMutuelle, ";
                selectQuery += "        @ActeCMU, ";
                selectQuery += "        @IDSEM_PTA, ";
                selectQuery += "        @IDSURV_PTA, ";
                selectQuery += "        @IDDEVIS_PTA, ";
                selectQuery += "        @dateaccident)";




                command.CommandText = selectQuery;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", actepg.Id);
                command.Parameters.AddWithValue("@id_patient", actepg.IdPatient);

                command.Parameters.AddWithValue("@DATE_DEBUT", actepg.DateExecution);
                command.Parameters.AddWithValue("@NB_JOURS", actepg.NbJours == null ? DBNull.Value : (object)actepg.NbJours);
                command.Parameters.AddWithValue("@NB_MOIS", actepg.NbMois == null ? DBNull.Value : (object)actepg.NbMois);
                command.Parameters.AddWithValue("@ID_PLAN", actepg.Id);
                command.Parameters.AddWithValue("@TARIF", actepg.Template.Libelle);
                command.Parameters.AddWithValue("@ID_ACTEGESTION", actepg.Template.Id);
                command.Parameters.AddWithValue("@IDSEM_PTA", actepg.IdSemestrePlanGestionAssocie);
                command.Parameters.AddWithValue("@IDSURV_PTA", actepg.IdSurvPlanGestionAssocie);
                command.Parameters.AddWithValue("@IDDEVIS_PTA", actepg.IdDevisAssociate);

                if (actepg.Template.IsDecomposed)
                    command.Parameters.AddWithValue("@CODE", actepg.Template.Code.Code + actepg.Template.CoeffDecompose);
                else
                    command.Parameters.AddWithValue("@CODE", actepg.Template.Code.Code + ((int)actepg.Template.Coeff).ToString());

                command.Parameters.AddWithValue("@LIBELLE", actepg.Libelle);
                command.Parameters.AddWithValue("@ID_PRESTATION", actepg.Template.Code.Code);
                command.Parameters.AddWithValue("@COEFF", actepg.Template.Coeff);
                command.Parameters.AddWithValue("@MONTANT", actepg.Montant_Honoraire);

                command.Parameters.AddWithValue("@NEED_DEP", actepg.NeedDEP);
                command.Parameters.AddWithValue("@NEED_FSE", actepg.NeedFSE);

                command.Parameters.AddWithValue("@ISDECOMPOSED", actepg.IsDecomposed);
                command.Parameters.AddWithValue("@DECOMPOSED", actepg.CoeffDecompose);

                command.Parameters.AddWithValue("@motifdepassement", actepg.motifdepassement);
                command.Parameters.AddWithValue("@ald", actepg.ald);
                command.Parameters.AddWithValue("@nbkilometre", actepg.Quantite);
                command.Parameters.AddWithValue("@rno", actepg.rno);
                command.Parameters.AddWithValue("@lieuexecution", actepg.domicile);
                command.Parameters.AddWithValue("@exoneration", actepg.Exoneration);
                command.Parameters.AddWithValue("@dimanche_ferie", actepg.DimancheEtJF);
                command.Parameters.AddWithValue("@nuit", actepg.nuit);
                command.Parameters.AddWithValue("@numdent", actepg.numdent);

                command.Parameters.AddWithValue("@NumMutuelle", actepg.NumMutuelle);
                command.Parameters.AddWithValue("@ActeCMU", actepg.ActeCMU);

                command.Parameters.AddWithValue("@accident", actepg.accident);
                command.Parameters.AddWithValue("@dateaccident", actepg.DateAccident == null ? DBNull.Value : (object)actepg.DateAccident.Value);

                command.Parameters.AddWithValue("@ID_DEP", actepg.Id_DEP == -1 ? DBNull.Value : (object)actepg.Id_DEP);
                command.Parameters.AddWithValue("@ID_FS", actepg.Id_FS == -1 ? DBNull.Value : (object)actepg.Id_FS);

                command.Parameters.AddWithValue("@NUM_SEMESTRE", actepg.NumSemestre);
                command.Parameters.AddWithValue("@NUM_CONTENTION", actepg.NumContention);
               



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
        public static void InsertActePG_TK(ActePG actepg)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "select MAX(ID)+1 as NEWID from base_traitement";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                object obj = command.ExecuteScalar();

                if (obj == DBNull.Value)
                    actepg.Id = 1;
                else
                    actepg.Id = Convert.ToInt32(obj);


                selectQuery = "insert into base_traitement (id, ";
                selectQuery += "                             id_patient, ";
                selectQuery += "                             date_debut, ";
                selectQuery += "                             nb_jours, ";
                selectQuery += "                             nb_mois, ";
                selectQuery += "                             id_plan, ";
            //    selectQuery += "                             tarif, ";
                selectQuery += "                             id_actegestion, ";
              //  selectQuery += "                             code, ";
                selectQuery += "                             libelle, ";
             //   selectQuery += "                             id_prestation, ";
                selectQuery += "                             coeff, ";
                selectQuery += "                             montant, ";
                selectQuery += "                             need_dep, ";
                selectQuery += "                             need_fse, ";
                selectQuery += "                             motifdepassement, ";
                selectQuery += "                             ald, ";
                selectQuery += "                             nbkilometre, ";
                selectQuery += "                             rno, ";
                selectQuery += "                             lieuexecution, ";
                selectQuery += "                             exoneration, ";
                selectQuery += "                             dimancheetjf, ";
                selectQuery += "                             nuit, ";
                selectQuery += "                             numdent, ";
                selectQuery += "                             accident, ";
                selectQuery += "                             ID_DEP, ";
                selectQuery += "                             NUM_SEMESTRE, ";
                selectQuery += "                             NUM_CONTENTION, ";
                selectQuery += "                             ISDECOMPOSED, ";
                selectQuery += "                             DECOMPOSED, ";
                selectQuery += "                             ID_FS, ";
                selectQuery += "                             NumMutuelle, ";
                selectQuery += "                             ActeCMU, ";
                selectQuery += "                             IDSEM_PTA, ";
                selectQuery += "                             IDSURV_PTA, ";
                selectQuery += "                             IDDEVIS_PTA, ";
                selectQuery += "                             dateaccident,ID_COMM, TYPE_COMMENT,ID_ACTE,RABAIS)";
                selectQuery += " values (@id, ";
                selectQuery += "        @id_patient, ";
                selectQuery += "        @date_debut, ";
                selectQuery += "        @nb_jours, ";
                selectQuery += "        @nb_mois, ";
                selectQuery += "        @id_plan, ";
              //  selectQuery += "        @tarif, ";
                selectQuery += "        @id_actegestion, ";
              //  selectQuery += "        @code, ";
                selectQuery += "        @libelle, ";
              //  selectQuery += "        @id_prestation, ";
                selectQuery += "        @coeff, ";
                selectQuery += "        @montant, ";
                selectQuery += "        @need_dep, ";
                selectQuery += "        @need_fse, ";
                selectQuery += "        @motifdepassement, ";
                selectQuery += "        @ald, ";
                selectQuery += "        @nbkilometre, ";
                selectQuery += "        @rno, ";
                selectQuery += "        @lieuexecution, ";
                selectQuery += "        @exoneration, ";
                selectQuery += "        @dimanche_ferie, ";
                selectQuery += "        @nuit, ";
                selectQuery += "        @numdent, ";
                selectQuery += "        @accident, ";
                selectQuery += "        @ID_DEP, ";
                selectQuery += "        @NUM_SEMESTRE, ";
                selectQuery += "        @NUM_CONTENTION, ";
                selectQuery += "        @ISDECOMPOSED, ";
                selectQuery += "        @DECOMPOSED, ";
                selectQuery += "        @ID_FS, ";
                selectQuery += "        @NumMutuelle, ";
                selectQuery += "        @ActeCMU, ";
                selectQuery += "        @IDSEM_PTA, ";
                selectQuery += "        @IDSURV_PTA, ";
                selectQuery += "        @IDDEVIS_PTA, ";
                selectQuery += "        @dateaccident,@ID_COMM,@TYPE_COMMENT,@ID_ACTE,@RABAIS)";




                command.CommandText = selectQuery;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", actepg.Id);
                command.Parameters.AddWithValue("@id_patient", actepg.IdPatient);

                command.Parameters.AddWithValue("@DATE_DEBUT", actepg.DateExecution);
                command.Parameters.AddWithValue("@NB_JOURS", actepg.NbJours == null ? 0 : (object)actepg.NbJours);
                command.Parameters.AddWithValue("@NB_MOIS", actepg.NbMois == null ? 0 : (object)actepg.NbMois);
                command.Parameters.AddWithValue("@ID_PLAN", actepg.Id);
               // command.Parameters.AddWithValue("@TARIF", actepg.Template.Libelle);
                command.Parameters.AddWithValue("@ID_ACTEGESTION", -1);
                //command.Parameters.AddWithValue("@ID_ACTEGESTION", actepg.Template.Id);
                command.Parameters.AddWithValue("@IDSEM_PTA", actepg.IdSemestrePlanGestionAssocie);
                command.Parameters.AddWithValue("@IDSURV_PTA", actepg.IdSurvPlanGestionAssocie);
                command.Parameters.AddWithValue("@IDDEVIS_PTA", actepg.IdDevisAssociate);
                command.Parameters.AddWithValue("@ID_COMM", actepg.IdComm);
                if (actepg .TypeActe == "M")
                    command.Parameters.AddWithValue("@ID_ACTE", actepg.IdActe );
                else
                command.Parameters.AddWithValue("@ID_ACTE", actepg.lstEcheances [0].IdActeTraitement );
                

                //if (actepg.Template.IsDecomposed)
                //    command.Parameters.AddWithValue("@CODE", actepg.Template.Code.Code + actepg.Template.CoeffDecompose);
                //else
                //    command.Parameters.AddWithValue("@CODE", actepg.Template.Code.Code + ((int)actepg.Template.Coeff).ToString());


                command.Parameters.AddWithValue("@LIBELLE", actepg.Libelle);
                //command.Parameters.AddWithValue("@ID_PRESTATION", actepg.Template.Code.Code);
                command.Parameters.AddWithValue("@COEFF", 1);
                command.Parameters.AddWithValue("@MONTANT", actepg.Montant_Honoraire);

                command.Parameters.AddWithValue("@NEED_DEP", actepg.NeedDEP);
                command.Parameters.AddWithValue("@NEED_FSE", actepg.NeedFSE);

                command.Parameters.AddWithValue("@ISDECOMPOSED", actepg.IsDecomposed);
                command.Parameters.AddWithValue("@DECOMPOSED", actepg.CoeffDecompose == null ? "" : actepg.CoeffDecompose);

                command.Parameters.AddWithValue("@motifdepassement", actepg.motifdepassement);
                command.Parameters.AddWithValue("@ald", actepg.ald);
                command.Parameters.AddWithValue("@nbkilometre", actepg.Quantite);
                command.Parameters.AddWithValue("@rno", actepg.rno);
                command.Parameters.AddWithValue("@lieuexecution", actepg.domicile);
                command.Parameters.AddWithValue("@exoneration", actepg.Exoneration);
                command.Parameters.AddWithValue("@dimanche_ferie", actepg.DimancheEtJF);
                command.Parameters.AddWithValue("@nuit", actepg.nuit);
                command.Parameters.AddWithValue("@numdent", actepg.numdent);

                command.Parameters.AddWithValue("@NumMutuelle", actepg.NumMutuelle);
                command.Parameters.AddWithValue("@ActeCMU", actepg.ActeCMU);

                command.Parameters.AddWithValue("@accident", actepg.accident);
                command.Parameters.AddWithValue("@dateaccident", actepg.DateAccident == null ? DBNull.Value : (object)actepg.DateAccident.Value);

                command.Parameters.AddWithValue("@ID_DEP", actepg.Id_DEP == -1 ? 0 : (object)actepg.Id_DEP);
                command.Parameters.AddWithValue("@ID_FS", actepg.Id_FS == -1 ? 0 : (object)actepg.Id_FS);

                command.Parameters.AddWithValue("@NUM_SEMESTRE", actepg.NumSemestre);
                command.Parameters.AddWithValue("@NUM_CONTENTION", actepg.NumContention);
                command.Parameters.AddWithValue("@TYPE_COMMENT", actepg.TypeActe );

                command.Parameters.AddWithValue("@RABAIS", actepg.rabais);

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
        public static void DeleteActePGAndEcheance(ActePG actepg)
        {
            string selectQuery = "";
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                 selectQuery = "delete from base_echeance";
                selectQuery += " where  ID_TRAITEMENT = @Id";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@Id", actepg.Id);
                command.ExecuteNonQuery();



                selectQuery = "delete from base_traitement";
                selectQuery += " where  ID = @Id ";


                command.CommandText = selectQuery;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Id", actepg.Id);
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

            insertLog(selectQuery, System.Security.Principal.WindowsIdentity.GetCurrent().Name,actepg.patient == null ? "" : actepg.patient.ToString(), actepg.Id + " :ID_TRAITEMENT");


        }
        public static void DeleteActePGMat(ActePG actepg)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "delete from base_traitement";
                selectQuery += " where  ID_TRAITEMENT = @Id";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@Id", actepg.Id);
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

        public static void DeleteActePGAndEcheance(basePatient patient)
        {
            string selectQuery = "";
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                 selectQuery = "delete from base_echeance";
                selectQuery += " where  ID_TRAITEMENT in (SELECT ID FROM base_traitement where  base_traitement.ID_PATIENT = @Id and DATE_DEBUT>current_date)";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@Id", patient.Id);
                command.ExecuteNonQuery();



                selectQuery = "delete from base_traitement";
                selectQuery += " where  ID_PATIENT = @Id  and DATE_DEBUT>current_date";


                command.CommandText = selectQuery;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Id", patient.Id);
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

            insertLog(selectQuery, System.Security.Principal.WindowsIdentity.GetCurrent().Name, patient.ToString(), "");


        }

        public static void UpdateDateActePGWithEcheance(ActePG acte, bool UpdateEcheance  = true )
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "update base_traitement set ";
                selectQuery += " DATE_DEBUT = @DATE_DEBUT ";
                selectQuery += " where  id = @id";



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", acte.Id);
                command.Parameters.AddWithValue("@DATE_DEBUT", acte.DateExecution);

                command.CommandType = CommandType.Text;

                command.ExecuteNonQuery();

                if (UpdateEcheance)
                {
                    selectQuery = "update base_echeance set ";
                    selectQuery += " DTEECHEANCE = @DTEECHEANCE ";
                    selectQuery += " where  ID_TRAITEMENT = @ID_TRAITEMENT";
                    command.CommandText = selectQuery;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@ID_TRAITEMENT", acte.Id);
                    //command.Parameters.AddWithValue("@DTEECHEANCE", (acte.NbMois == null) ? DBNull.Value : (object)acte.DateExecution.AddMonths(acte.NbMois.Value).AddDays(acte.NbJours.Value));
                    command.Parameters.AddWithValue("@DTEECHEANCE",(object)acte.DateExecution);
                    command.ExecuteNonQuery();
                }

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

        public static void UpdateActePG(ActePG actepg)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "update base_traitement";
                selectQuery += "  set id_patient = @id_patient,";
                selectQuery += "    date_debut = @date_debut,";
                selectQuery += "    nb_jours = @nb_jours,";
                selectQuery += "    nb_mois = @nb_mois,";
                selectQuery += "    id_plan = @id_plan,";
                selectQuery += "    tarif = @tarif,";
                selectQuery += "    id_actegestion = @id_actegestion,";
                selectQuery += "    code = @code,";
                selectQuery += "    libelle = @libelle,";
                selectQuery += "    id_prestation = @id_prestation,";
                selectQuery += "    coeff = @coeff,";
                selectQuery += "    montant = @montant,";
                selectQuery += "    need_dep = @need_dep,";
                selectQuery += "    need_fse = @need_fse,";
                selectQuery += "    motifdepassement = @motifdepassement,";
                selectQuery += "    ald = @ald,";
                selectQuery += "    nbkilometre = @nbkilometre,";
                selectQuery += "    rno = @rno,";
                selectQuery += "    lieuexecution = @lieuexecution,";
                selectQuery += "    exoneration = @exoneration,";
                selectQuery += "    dimancheetjf = @dimancheetjf,";
                selectQuery += "    nuit = @nuit,";
                selectQuery += "    numdent = @numdent,";
                selectQuery += "    accident = @accident,";
                selectQuery += "    ID_DEP = @ID_DEP,";
                selectQuery += "    ID_FS = @ID_FS,";
                selectQuery += "    NUM_SEMESTRE = @NUM_SEMESTRE,";
                selectQuery += "    NUM_CONTENTION = @NUM_CONTENTION,";
                selectQuery += "    DECOMPOSED = @DECOMPOSED,";
                selectQuery += "    ISDECOMPOSED = @ISDECOMPOSED,";
                selectQuery += "    NumMutuelle = @NumMutuelle,";
                selectQuery += "    ActeCMU = @ActeCMU,";
                selectQuery += "    IDSEM_PTA = @IDSEM_PTA,";
                selectQuery += "    IDDEVIS_PTA = @IDDEVIS_PTA,";
                selectQuery += "    IDSURV_PTA = @IDSURV_PTA,";

                selectQuery += "    dateaccident = @dateaccident";
                selectQuery += " where (id = @id)";




                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;




                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@id_patient", actepg.IdPatient);
                command.Parameters.AddWithValue("@id", actepg.Id);

                command.Parameters.AddWithValue("@DATE_DEBUT", actepg.DateExecution);
                command.Parameters.AddWithValue("@NB_JOURS", actepg.NbJours);
                command.Parameters.AddWithValue("@NB_MOIS", actepg.NbMois);
                command.Parameters.AddWithValue("@ID_PLAN", actepg.Id);
                if (actepg.Template != null)
                {
                    command.Parameters.AddWithValue("@TARIF", actepg.Template.Libelle);
                    command.Parameters.AddWithValue("@ID_ACTEGESTION", actepg.Template.Id);
                    command.Parameters.AddWithValue("@COEFF", actepg.Template.Coeff);
                    command.Parameters.AddWithValue("@id_codesecu", actepg.Template.Id);
                    if (actepg.Template.IsDecomposed)
                        command.Parameters.AddWithValue("@CODE", actepg.Template.Code.Code + actepg.Template.CoeffDecompose);
                    else
                        command.Parameters.AddWithValue("@CODE", actepg.Template.Code.Code + ((int)actepg.Template.Coeff).ToString());
                    command.Parameters.AddWithValue("@ID_PRESTATION", actepg.Template.Code.Code);
                }
                else
                {
                    command.Parameters.AddWithValue("@TARIF", "");
                    command.Parameters.AddWithValue("@ID_ACTEGESTION", 0);
                    command.Parameters.AddWithValue("@COEFF",0);
                    command.Parameters.AddWithValue("@id_codesecu", 0);
                    command.Parameters.AddWithValue("@CODE", "0");
                    command.Parameters.AddWithValue("@ID_PRESTATION", 0);
                }
              
                command.Parameters.AddWithValue("@IDSEM_PTA", actepg.IdSemestrePlanGestionAssocie);
                command.Parameters.AddWithValue("@IDSURV_PTA", actepg.IdSurvPlanGestionAssocie);
                command.Parameters.AddWithValue("@IDDEVIS_PTA", actepg.IdDevisAssociate);
                
           

                command.Parameters.AddWithValue("@LIBELLE", actepg.Libelle);
               

                command.Parameters.AddWithValue("@MONTANT", actepg.Montant_Honoraire);

                command.Parameters.AddWithValue("@ISDECOMPOSED", actepg.IsDecomposed);
                command.Parameters.AddWithValue("@DECOMPOSED", actepg.CoeffDecompose);

                command.Parameters.AddWithValue("@NEED_DEP", actepg.NeedDEP);
                command.Parameters.AddWithValue("@NEED_FSE", actepg.NeedFSE);


               

                command.Parameters.AddWithValue("@qualifdepense", actepg.motifdepassement);
                command.Parameters.AddWithValue("@ald", actepg.ald);
                command.Parameters.AddWithValue("@nbkilometre", actepg.Quantite);
                command.Parameters.AddWithValue("@rno", actepg.rno);
                command.Parameters.AddWithValue("@lieuexecution", actepg.domicile);
                command.Parameters.AddWithValue("@exoneration", actepg.Exoneration);
                command.Parameters.AddWithValue("@dimanche_ferie", actepg.DimancheEtJF);
                command.Parameters.AddWithValue("@nuit", actepg.nuit);
                command.Parameters.AddWithValue("@numdent", actepg.numdent);
                command.Parameters.AddWithValue("@motifdepassement", actepg.motifdepassement);
                command.Parameters.AddWithValue("@dimancheetjf", actepg.DimancheEtJF);

                command.Parameters.AddWithValue("@accident", actepg.accident);
                command.Parameters.AddWithValue("@dateaccident", actepg.DateAccident == null ? DBNull.Value : (object)actepg.DateAccident.Value);

                command.Parameters.AddWithValue("@ID_DEP", actepg.Id_DEP == -1 ? DBNull.Value : (object)actepg.Id_DEP);
                command.Parameters.AddWithValue("@ID_FS", actepg.Id_FS == null ? DBNull.Value : (object)actepg.Id_FS);

                command.Parameters.AddWithValue("@NUM_SEMESTRE", actepg.NumSemestre);
                command.Parameters.AddWithValue("@NUM_CONTENTION", actepg.NumContention);

                command.Parameters.AddWithValue("@NumMutuelle", actepg.NumMutuelle);
                command.Parameters.AddWithValue("@ActeCMU", actepg.ActeCMU);


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

  

        public static void UpdateActePGActes_TK(ActePG vActePg, Acte vActe, double vMontant,int TraitemetMateriel=0)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "update base_traitement";
                selectQuery += "  set libelle = @libelle,";
                selectQuery += "    montant = @montant,";
                selectQuery += "    id_acte = @id_acte";
                selectQuery += " where (id = @id)";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                command.CommandText = selectQuery;

            

                command.Parameters.AddWithValue("@id", vActePg.Id);
                if (TraitemetMateriel == 0)
                {
                    command.Parameters.AddWithValue("@LIBELLE", vActe.acte_libelle);
                    command.Parameters.AddWithValue("@id_acte", vActe.id_acte);
                }
                else
                {
                    command.Parameters.AddWithValue("@LIBELLE", vActePg .Libelle );
                    command.Parameters.AddWithValue("@id_acte", vActePg.IdActe);
                }
                command.Parameters.AddWithValue("@MONTANT",  vMontant);
           

              





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


        public static void UpdateActePG_TK(ActePG actepg)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "update base_traitement";
                selectQuery += "  set id_patient = @id_patient,";
                selectQuery += "    date_debut = @date_debut,";
                selectQuery += "    nb_jours = @nb_jours,";
                selectQuery += "    nb_mois = @nb_mois,";
                selectQuery += "    id_plan = @id_plan,";
                //selectQuery += "    tarif = @tarif,";
                //selectQuery += "    id_actegestion = @id_actegestion,";
                //selectQuery += "    code = @code,";
                selectQuery += "    libelle = @libelle,";
                //selectQuery += "    id_prestation = @id_prestation,";
                //selectQuery += "    coeff = @coeff,";
                selectQuery += "    montant = @montant,";
                selectQuery += "    need_dep = @need_dep,";
                selectQuery += "    need_fse = @need_fse,";
                selectQuery += "    motifdepassement = @motifdepassement,";
                selectQuery += "    ald = @ald,";
                selectQuery += "    nbkilometre = @nbkilometre,";
                selectQuery += "    rno = @rno,";
                selectQuery += "    lieuexecution = @lieuexecution,";
                selectQuery += "    exoneration = @exoneration,";
                selectQuery += "    dimancheetjf = @dimancheetjf,";
                selectQuery += "    nuit = @nuit,";
                selectQuery += "    numdent = @numdent,";
                selectQuery += "    accident = @accident,";
                selectQuery += "    ID_DEP = @ID_DEP,";
                selectQuery += "    ID_FS = @ID_FS,";
                selectQuery += "    NUM_SEMESTRE = @NUM_SEMESTRE,";
                selectQuery += "    NUM_CONTENTION = @NUM_CONTENTION,";
                selectQuery += "    DECOMPOSED = @DECOMPOSED,";
                selectQuery += "    ISDECOMPOSED = @ISDECOMPOSED,";
                selectQuery += "    NumMutuelle = @NumMutuelle,";
                selectQuery += "    ActeCMU = @ActeCMU,";
                selectQuery += "    IDSEM_PTA = @IDSEM_PTA,";
                selectQuery += "    IDDEVIS_PTA = @IDDEVIS_PTA,";
                selectQuery += "    IDSURV_PTA = @IDSURV_PTA,";
                selectQuery += "    dateaccident = @dateaccident";
                selectQuery += " where (id = @id)";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                command.CommandText = selectQuery;

                command.Parameters.AddWithValue("@id_patient", actepg.IdPatient);
                command.Parameters.AddWithValue("@id", actepg.Id);

                command.Parameters.AddWithValue("@DATE_DEBUT", actepg.DateExecution);
                command.Parameters.AddWithValue("@NB_JOURS", actepg.NbJours);
                command.Parameters.AddWithValue("@NB_MOIS", actepg.NbMois);
                command.Parameters.AddWithValue("@ID_PLAN", actepg.Id);
                //               command.Parameters.AddWithValue("@TARIF", actepg.Template.Libelle);
                //               command.Parameters.AddWithValue("@ID_ACTEGESTION", actepg.Template.Id);
                command.Parameters.AddWithValue("@IDSEM_PTA", actepg.IdSemestrePlanGestionAssocie);
                command.Parameters.AddWithValue("@IDSURV_PTA", actepg.IdSurvPlanGestionAssocie);
                command.Parameters.AddWithValue("@IDDEVIS_PTA", actepg.IdDevisAssociate);


                //              if (actepg.Template.IsDecomposed)
                //                command.Parameters.AddWithValue("@CODE", actepg.Template.Code.Code + actepg.Template.CoeffDecompose);

                //          else
                //            command.Parameters.AddWithValue("@CODE", actepg.Template.Code.Code + ((int)actepg.Template.Coeff).ToString());

                command.Parameters.AddWithValue("@LIBELLE", actepg.Libelle);
                //      command.Parameters.AddWithValue("@ID_PRESTATION", actepg.Template.Code.Code);
                //    command.Parameters.AddWithValue("@COEFF", actepg.Template.Coeff);
                command.Parameters.AddWithValue("@MONTANT", actepg.Montant_Honoraire);

                command.Parameters.AddWithValue("@ISDECOMPOSED", actepg.IsDecomposed);
                command.Parameters.AddWithValue("@DECOMPOSED", actepg.CoeffDecompose);

                command.Parameters.AddWithValue("@NEED_DEP", actepg.NeedDEP);
                command.Parameters.AddWithValue("@NEED_FSE", actepg.NeedFSE);





                //command.Parameters.AddWithValue("@id_codesecu", actepg.Template.Id);

                command.Parameters.AddWithValue("@qualifdepense", actepg.motifdepassement);
                command.Parameters.AddWithValue("@ald", actepg.ald);
                command.Parameters.AddWithValue("@nbkilometre", actepg.Quantite);
                command.Parameters.AddWithValue("@rno", actepg.rno);
                command.Parameters.AddWithValue("@lieuexecution", actepg.domicile);
                command.Parameters.AddWithValue("@exoneration", actepg.Exoneration);
                command.Parameters.AddWithValue("@dimanche_ferie", actepg.DimancheEtJF);
                command.Parameters.AddWithValue("@nuit", actepg.nuit);
                command.Parameters.AddWithValue("@numdent", actepg.numdent);



                command.Parameters.AddWithValue("@motifdepassement", actepg.motifdepassement);
                command.Parameters.AddWithValue("@dimancheetjf", actepg.DimancheEtJF);

                command.Parameters.AddWithValue("@accident", actepg.accident);
                command.Parameters.AddWithValue("@dateaccident", actepg.DateAccident == null ? DBNull.Value : (object)actepg.DateAccident.Value);

                command.Parameters.AddWithValue("@ID_DEP", actepg.Id_DEP == -1 ? DBNull.Value : (object)actepg.Id_DEP);
                command.Parameters.AddWithValue("@ID_FS", actepg.Id_FS == null ? DBNull.Value : (object)actepg.Id_FS);

                command.Parameters.AddWithValue("@NUM_SEMESTRE", actepg.NumSemestre);
                command.Parameters.AddWithValue("@NUM_CONTENTION", actepg.NumContention);

                command.Parameters.AddWithValue("@NumMutuelle", actepg.NumMutuelle);
                command.Parameters.AddWithValue("@ActeCMU", actepg.ActeCMU);


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
        public static void DecalerPlanDeGestion(DateTime APartirDe, basePatient patient, int NbDaysToMove, int NbMonthsToMove)
        {
            if ((NbDaysToMove == 0) && (NbMonthsToMove == 0)) return;

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                /*
                string selectQuery = "update base_traitement set ";
                selectQuery += " DATE_DEBUT = DATE_DEBUT + " + NbDaysToMove.ToString();
                selectQuery += " where  id_patient = @id_patient and DATE_DEBUT > @APartirDe";
                */
                string selectQuery = "update base_traitement set ";
                selectQuery += " DATE_DEBUT = DATEADD(month, " + NbMonthsToMove.ToString() + ",DATE_DEBUT)  + " + NbDaysToMove.ToString();
                selectQuery += " where  id_patient = @id_patient and DATE_DEBUT > @APartirDe";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id_patient", patient.Id);
                command.Parameters.AddWithValue("@APartirDe", APartirDe);

                command.CommandType = CommandType.Text;

                command.ExecuteNonQuery();


                selectQuery = "update base_semestre set ";
                selectQuery += " DATEDEBUT = DATEADD(month, " + NbMonthsToMove.ToString() + ",DATEDEBUT)  + " + NbDaysToMove.ToString();
                selectQuery += " ,DATEFIN = DATEADD(month, " + NbMonthsToMove.ToString() + ",DATEFIN)  + " + NbDaysToMove.ToString();
                //selectQuery += " DATEDEBUT = DATEDEBUT + " + NbDaysToMove.ToString();
                //selectQuery += " ,DATEFIN = DATEFIN + " + NbDaysToMove.ToString();
                selectQuery += " where  ID in (";
                selectQuery += " select base_semestre.id from base_semestre";
                selectQuery += " inner join base_traitement on base_traitement.idsem_pta = base_semestre.id and base_semestre.DATEDEBUT > @APartirDe";
                selectQuery += " where base_traitement.id_patient = @id_patient ";
                selectQuery += " )";


                command.CommandText = selectQuery;


                command.ExecuteNonQuery();


                selectQuery = "update base_surveillance set ";
                selectQuery += " DATEDEBUT = DATEADD(month, " + NbMonthsToMove.ToString() + ",DATEDEBUT)  + " + NbDaysToMove.ToString();
                selectQuery += " ,DATEFIN = DATEADD(month, " + NbMonthsToMove.ToString() + ",DATEFIN)  + " + NbDaysToMove.ToString();
                //selectQuery += " DATEDEBUT = DATEDEBUT + " + NbDaysToMove.ToString();
                //selectQuery += " ,DATEFIN = DATEFIN + " + NbDaysToMove.ToString();
                selectQuery += " where  ID in (";
                selectQuery += " select base_surveillance.id from base_surveillance";
                selectQuery += " inner join base_traitement on base_traitement.idsurv_pta = base_surveillance.id and base_surveillance.DATEDEBUT > @APartirDe";
                selectQuery += " where base_traitement.id_patient = @id_patient ";
                selectQuery += " )";


                command.CommandText = selectQuery;


                command.ExecuteNonQuery();


                selectQuery = "update base_echeance set ";
                selectQuery += " DTEECHEANCE = DATEADD(month, " + NbMonthsToMove.ToString() + ",DTEECHEANCE)  + " + NbDaysToMove.ToString();
                //selectQuery += " DTEECHEANCE = DTEECHEANCE + " + NbDaysToMove.ToString();
                selectQuery += " where  ID_TRAITEMENT in (select ID FROM base_traitement where id_patient = @id_patient and DATE_DEBUT > @APartirDe)";


                command.CommandText = selectQuery;


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


        public static DataTable GetCodesPresta()
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
          //  MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = " select ID_PRESTATION, ";
                selectQuery += "        LIBELLE, ";
                selectQuery += "        VALEUR_CLE_EURO ";
                selectQuery += " from code_prestation";
                selectQuery += " order by  ID_PRESTATION";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
               // transaction.Commit();

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


        public static DataTable get_acte_gestion()
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
          //  MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "       code, ";
                selectQuery += "       libelle, ";
                selectQuery += "       code_prestation, ";
                selectQuery += "       acte_coeff, ";
                selectQuery += "       decompisvisible, ";
                selectQuery += "       decomp, ";
                selectQuery += "       valeur, ";
                selectQuery += "       need_dep, ";
                selectQuery += "       need_fse, ";
                selectQuery += "       nb_jours, ";
                selectQuery += "       nb_mois,";
                selectQuery += "       phase,";
                selectQuery += "       organisation,";
                selectQuery += "       VALEUR_CMU,";
                selectQuery += "       TYPEDEREGLEMENT";
                selectQuery += " from base_acte_gestion";
                selectQuery += " order by  id asc";

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

        public static DataTable getAchatsByPeriode(DateTime datedebut, DateTime datefin, string ids)
        {

            string[] lstid = ids.Split(',');
           

             int c =0;
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = " SELECT a.ID, a.ID_PATIENT, a.DATE_DEBUT, a.NB_JOURS, a.NB_MOIS, a.ID_PLAN, a.TARIF, a.ID_ACTEGESTION, a.CODE, a.LIBELLE, a.ID_PRESTATION, a.COEFF, a.MONTANT, a.NEED_DEP, a.NEED_FSE, a.MOTIFDEPASSEMENT, a.ALD, a.NBKILOMETRE, a.RNO, a.LIEUEXECUTION, a.EXONERATION, a.DIMANCHEETJF, a.NUIT, a.NUMDENT, a.ACCIDENT, a.DATEACCIDENT, a.ID_DEP, a.ID_FS, a.SALEDATE, a.NUM_SEMESTRE, a.NUM_CONTENTION, a.ISDECOMPOSED, a.DECOMPOSED, a.IDSEM_PTA, a.ACTECMU, a.NUMMUTUELLE, a.IDSURV_PTA, a.IDDEVIS_PTA, a.ID_COMM, a.ID_ACTE, a.FACTUREE, a.ID_FACTURE, a.TYPE_COMMENT, a.IDMATERIEL, a.RABAIS";
                selectQuery += " FROM base_traitement a where (a.TYPE_COMMENT = 'M' or a.ID_COMM is null or a.ID_COMM = -1 )";
                selectQuery += " and (a.ID_ACTE is not null or a.ID_ACTE  not in(-1)) and (cast(a.DATE_DEBUT as date) between @datedebut and @datefin) ";
             
                selectQuery += "and a.ID_ACTE in ( ";
               
                foreach(string s in lstid)
                {
                    c++;
                      selectQuery += " @id" + c + ",";
                }
                selectQuery = selectQuery.Substring(0, selectQuery.LastIndexOf(','));
                selectQuery += "  ) ";



                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                command.Parameters.AddWithValue("@datedebut", datedebut);
                command.Parameters.AddWithValue("@datefin", datefin);
                //command.Parameters.AddWithValue("@ids_mat", ids);
                c = 0;
                foreach (string s in lstid)
                {
                    c++;
                   command.Parameters.AddWithValue("@id"+c, s);
                }
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


        public static void insert(TemplateActePG tmp)
        {

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "select MAX(ID)+1 as NEWID from base_acte_gestion";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                object obj = command.ExecuteScalar();

                if (obj == DBNull.Value)
                    tmp.Id = 1;
                else
                    tmp.Id = Convert.ToInt32(obj);

           
               

                        selectQuery = "insert into base_acte_gestion (id, ";
                        selectQuery += "                             CODE, ";
                        selectQuery += "                             LIBELLE, ";
                        selectQuery += "                             code_prestation, ";
                        selectQuery += "                             ACTE_COEFF, ";
                        selectQuery += "                             DECOMPISVISIBLE, ";
                        selectQuery += "                             DECOMP, ";
                        selectQuery += "                             VALEUR, ";
                        selectQuery += "                             NEED_DEP, ";
                        selectQuery += "                             NEED_FSE, ";
                        selectQuery += "                             NB_JOURS, ";
                        selectQuery += "                             NB_MOIS, ";
                        selectQuery += "                             ORGANISATION, ";
                        selectQuery += "                             TYPEDEREGLEMENT,phase,VALEUR_CMU,ID_ACTE_ORTHALIS)";
                        selectQuery += " values (@id, ";
                        selectQuery += "        @CODE, ";
                        selectQuery += "        @LIBELLE, ";
                        selectQuery += "        @code_prestation, ";
                        selectQuery += "        @ACTE_COEFF, ";
                        selectQuery += "        @DECOMPISVISIBLE, ";
                        selectQuery += "        @DECOMP, ";
                        selectQuery += "        @VALEUR, ";
                        selectQuery += "        @NEED_DEP, ";
                        selectQuery += "        @NEED_FSE, ";
                        selectQuery += "        @NB_JOURS, ";
                        selectQuery += "        @NB_MOIS, ";
                        selectQuery += "        @ORGANISATION, ";
                        selectQuery += "        @TYPEDEREGLEMENT,@phase,@VALEUR_CMU,@ID_ACTE_ORTHALIS)";



                    command.Parameters.Clear();
                    command.CommandText = selectQuery;               
                    command.Parameters.AddWithValue("@id", tmp.Id);
                    command.Parameters.AddWithValue("@CODE", tmp.Nom);
                    command.Parameters.AddWithValue("@LIBELLE", tmp.Libelle);
                    command.Parameters.AddWithValue("@code_prestation", tmp.Code);
                    command.Parameters.AddWithValue("@ACTE_COEFF", tmp.Coeff);
                    command.Parameters.AddWithValue("@DECOMPISVISIBLE", tmp.IsDecomposed);
                    command.Parameters.AddWithValue("@DECOMP", tmp.CoeffDecompose);
                    command.Parameters.AddWithValue("@VALEUR", tmp.Valeur);
                    command.Parameters.AddWithValue("@NEED_DEP", tmp.NeedDEP);
                    command.Parameters.AddWithValue("@NEED_FSE", tmp.NeedFS);
                    command.Parameters.AddWithValue("@NB_MOIS", tmp.NBMois);
                    command.Parameters.AddWithValue("@NB_JOURS", tmp.NBJours);    
                    command.Parameters.AddWithValue("@ORGANISATION", tmp.Organisation);
                    command.Parameters.AddWithValue("@TYPEDEREGLEMENT", tmp.TypeDeReglement);
                    command.Parameters.AddWithValue("@phase", tmp.phase);
                    command.Parameters.AddWithValue("@VALEUR_CMU", tmp.VALEUR_CMU);
                    command.Parameters.AddWithValue("@ID_ACTE_ORTHALIS", -1);
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
