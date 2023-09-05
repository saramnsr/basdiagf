﻿using System;
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


        #region Commentaires clinique


        public static DataTable getCommentairesOrthalis(basePatient pat)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = "select id_comm, ";
                selectQuery += " id_pat, ";
                selectQuery += " date_comm, ";
                selectQuery += " code_hyg, ";
                selectQuery += " code_libre, ";
                selectQuery += " commentaire, ";
                selectQuery += " fait, ";
                selectQuery += " afaire, ";
                selectQuery += " comm_prat as ID_PRATICIEN, ";
                selectQuery += " TRIM(personne.per_NOM)||' '||TRIM(personne.per_prenom) as PRATICIEN ";
                selectQuery += " from zone_comm";
                selectQuery += " left outer join personne on zone_comm.comm_prat=personne.id_personne";
                selectQuery += " where id_pat=@IDPAT";
                selectQuery += " order by date_comm desc";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@IDPAT", pat.Id);

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


        public static void updateCommCliniqueIdRDV(BasCommon_BO.CommClinique comm)
        {



            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "update base_comm";
                selectQuery += " set id_rdv = @id_rdv";
                selectQuery += " where (id = @id)";




                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;


                command.Parameters.AddWithValue("@id", comm.Id);
                command.Parameters.AddWithValue("@id_rdv", comm.IdRDV);


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


     

        public static void DeleteCommentaire(CommClinique comm, bool withEcheance)
        {

            string selectQuery = "";

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                if (withEcheance)
                {
                    selectQuery = "DELETE FROM base_echeance  ";
                    selectQuery += " where base_echeance.id in (SELECT * FROM (SELECT f.ID FROM base_echeance f ";
                    selectQuery += " JOIN base_traitement t  ";
                    selectQuery += " on t.id = f.ID_TRAITEMENT  ";
                    selectQuery += " AND t.ID_COMM = @id) as tabEcheance) ";



                    MySqlCommand com = new MySqlCommand(selectQuery, connection, transaction);
                    com.CommandType = CommandType.Text;
                    com.Parameters.AddWithValue("@id", comm.Id);
                    com.ExecuteNonQuery();

                    selectQuery = "DELETE FROM base_traitement  ";
                    selectQuery += " WHERE ID_COMM = @id";


                    com.CommandText = selectQuery;
                    com.CommandType = CommandType.Text;
                    com.ExecuteNonQuery();
                }




                selectQuery = "delete from  base_comm_radios";
                selectQuery += " where (ID_COMM = @id)";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", comm.Id);
                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                selectQuery = "delete from  base_comm_photos";
                selectQuery += " where (ID_COMM = @id)";

                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                selectQuery = "delete from  base_comm_mat";
                selectQuery += " where (ID_COMM = @id)";

                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                selectQuery = "delete from  base_comm_actes";
                selectQuery += " where (ID_COMM = @id)";

                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                selectQuery = "delete from  base_comm_autrepers";
                selectQuery += " where (ID_COMM = @id)";

                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                selectQuery = "delete from  rendez_vous";
                selectQuery += " where (ID_COMMCLINIQUE = @id)";
                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                selectQuery = "delete from  base_comm_aextraire";
                selectQuery += " where (ID_COMM = @id)";

                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();


                selectQuery = "delete from  base_comm";
                selectQuery += " where (ID = @id)";

                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                selectQuery = "update bas_echeances_devis set ID_COMMCLINIQUE = null";
                selectQuery += " where (ID_COMMCLINIQUE = @id)";

                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
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
            insertLog(selectQuery, System.Security.Principal.WindowsIdentity.GetCurrent().Name, comm.patient == null ? "" : comm.patient.ToString(), comm.Id + " :ID_COMM");

        }
        public static void UpdateCommentaire(CommClinique comm)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "update base_comm";
                selectQuery += " set id_praticien = @id_praticien,";
                selectQuery += "     id_assistante = @id_assistante,";
                selectQuery += "     id_secretaire = @id_secretaire,";
                selectQuery += "     id_acte = @id_acte,";
                selectQuery += "     id_rdv = @id_rdv,";
                selectQuery += "     id_patient = @id_patient,";
                selectQuery += "     hygiene = @hygiene,";
                selectQuery += "     nbJours = @nbJours,";
                selectQuery += "     nbMois = @nbMois,";
                selectQuery += "     modecreation = @modecreation,";
                selectQuery += "     Id_Scenario = @Id_Scenario,";
                selectQuery += "     Etat = @Etat,";
                selectQuery += "     DATEPREVISIONNELLE = @DATEPREVISIONNELLE,";
                selectQuery += "     Id_Semestre = @Id_Semestre,";
                selectQuery += "     date_comm = @date_comm,";
                selectQuery += "     commentaires = @commentaires,";
                selectQuery += "     commentairesafaire = @commentairesafaire,";
                selectQuery += "     IsDateDeRef = @IsDateDeRef,";
                selectQuery += "     MONTANT = @MONTANT,";
                selectQuery += "     MONTANTAVANTREMISE = @MONTANTAVANTREMISE,";
                selectQuery += "     QTE_ACTE = @QTE_ACTE,";
                selectQuery += "     id_tour = @id_tour,";
                selectQuery += "     id_port = @id_port,";
                selectQuery += "     id_tim = @id_tim,";
                selectQuery += "     id_chgt = @id_chgt,";
                selectQuery += "     idNumLogiciel = @idNumLogiciel,";
                selectQuery += "     id_droit = @id_droit,";
                selectQuery += "     id_gauche = @id_gauche,";
                selectQuery += "     id_blanchiment = @id_blanchiment,";
                selectQuery += "     id_diaporama = @id_diaporama,";
                selectQuery += "     id_accelerateur = @id_accelerateur,";
                selectQuery += "     vu = @vu,";
                selectQuery += "     donne = @donne,";
                selectQuery += "     ECHEANCE = @ECHEANCE,";
                selectQuery += "     rabais = @rabais,";
                selectQuery += "     desactive = @desactive, ";
                selectQuery += "     dents = @dents, ";
                selectQuery += "     isfulltime = @isfulltime ";
                selectQuery += "     where (id = @id)";




                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;


                command.Parameters.AddWithValue("@id", comm.Id);
                command.Parameters.AddWithValue("@id_praticien", comm.praticien == null ? DBNull.Value : (object)comm.praticien.Id);
                command.Parameters.AddWithValue("@id_assistante", comm.Assistante == null ? DBNull.Value : (object)comm.Assistante.Id);
                command.Parameters.AddWithValue("@id_secretaire", comm.Secretaire == null ? DBNull.Value : (object)comm.Secretaire.Id);
                command.Parameters.AddWithValue("@id_acte", comm.Acte == null ? DBNull.Value : (object)comm.Acte.id_acte);
                command.Parameters.AddWithValue("@id_rdv", comm.Appointement == null ? DBNull.Value : (object)comm.Appointement.Id);
                command.Parameters.AddWithValue("@id_patient", comm.IdPatient);
                command.Parameters.AddWithValue("@nbJours", comm.NbJours);
                command.Parameters.AddWithValue("@nbMois", comm.NbMois);
                command.Parameters.AddWithValue("@modecreation", comm.modecreation);
                command.Parameters.AddWithValue("@Id_Scenario", comm.IdScenario);
                command.Parameters.AddWithValue("@Etat", comm.etat);
                command.Parameters.AddWithValue("@Id_Semestre", comm.IdSemestre);
                command.Parameters.AddWithValue("@hygiene", comm.Hygiene);
                command.Parameters.AddWithValue("@date_comm", comm.date == null ? DBNull.Value : (object)comm.date.Value);
                command.Parameters.AddWithValue("@commentaires", comm.Commentaire);
                command.Parameters.AddWithValue("@commentairesafaire", comm.CommentaireAFaire);
                command.Parameters.AddWithValue("@Id_ParentComment", comm.IdParentComment);
                command.Parameters.AddWithValue("@IsDateDeRef", comm.IsDateDeRef);
                command.Parameters.AddWithValue("@DATEPREVISIONNELLE", comm.DatePrevisionnnelle);

                command.Parameters.AddWithValue("@MONTANT", comm.prix_traitement);
                command.Parameters.AddWithValue("@MONTANTAVANTREMISE", comm.Acte.prix_acte);
                command.Parameters.AddWithValue("@QTE_ACTE", comm.Acte.quantite);
                command.Parameters.AddWithValue("@ECHEANCE", comm.echeance);
                command.Parameters.AddWithValue("@id_tour", comm.idTour);
                command.Parameters.AddWithValue("@id_port", comm.iDPortGouttier);
                command.Parameters.AddWithValue("@id_tim", comm.idTim);
                command.Parameters.AddWithValue("@id_chgt", comm.idChgt);
                command.Parameters.AddWithValue("@idNumLogiciel", comm.idNumLogiciel);
                command.Parameters.AddWithValue("@id_droit", comm.idDroit);
                command.Parameters.AddWithValue("@id_blanchiment", comm.idBlanchiment);
                command.Parameters.AddWithValue("@id_diaporama", comm.idDiaporama);
                command.Parameters.AddWithValue("@id_accelerateur", comm.idAccelerateur);
                command.Parameters.AddWithValue("@id_gauche", comm.idGauche);
                command.Parameters.AddWithValue("@donne", comm.Donne);
                command.Parameters.AddWithValue("@vu", comm.Vu);
                command.Parameters.AddWithValue("@desactive", comm.desactive);
                command.Parameters.AddWithValue("@rabais", comm.rabais);
                command.Parameters.AddWithValue("@dents", comm.dents);
                command.Parameters.AddWithValue("@isfulltime", comm.isfulltime);
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
        public static void InsertCommentaire(CommClinique comm)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select MAX(id)+1 as NEWID from base_comm";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                object obj = command.ExecuteScalar();

                if (obj == DBNull.Value) comm.Id = 1; else comm.Id = Convert.ToInt32(obj);


                selectQuery = "insert into base_comm (id, ";
                selectQuery += "                        id_praticien, ";
                selectQuery += "                        id_assistante, ";
                selectQuery += "                        id_secretaire, ";
                selectQuery += "                        id_acte, ";
                selectQuery += "                        id_rdv, ";
                selectQuery += "                        id_patient, ";
                selectQuery += "                        nbjours, ";
                selectQuery += "                        nbmois, ";
                selectQuery += "                        modecreation, ";
                selectQuery += "                        dateprevisionnelle, ";

                selectQuery += "                        id_scenario, ";
                selectQuery += "                        etat, ";
                selectQuery += "                        id_semestre, ";
                selectQuery += "                        hygiene, ";
                selectQuery += "                        date_comm, ";
                selectQuery += "                        commentairesafaire, ";
                selectQuery += "                        id_parentcomment,";
                selectQuery += "                        isdatederef,";
                selectQuery += "                        commentaires,montantavantremise, montant, qte_acte, id_devis,id_tour,id_tim,id_chgt,idnumlogiciel,id_droit,id_blanchiment,id_diaporama,id_accelerateur,id_gauche,vu,donne,desactive,rabais, ";
                selectQuery += "                        dents,isfulltime) ";
                selectQuery += " values (@id, ";
                selectQuery += "         @id_praticien, ";
                selectQuery += "         @id_assistante, ";
                selectQuery += "         @id_secretaire, ";
                selectQuery += "         @id_acte, ";
                selectQuery += "         @id_rdv, ";
                selectQuery += "         @id_patient, ";
                selectQuery += "         @nbJours, ";
                selectQuery += "         @nbMois, ";
                selectQuery += "         @modecreation, ";
                selectQuery += "         @DATEPREVISIONNELLE, ";

                selectQuery += "         @IdScenario, ";
                selectQuery += "         @Etat, ";
                selectQuery += "         @IdSemestre, ";
                selectQuery += "         @hygiene, ";
                selectQuery += "         @date_comm, ";
                selectQuery += "         @commentairesafaire, ";
                selectQuery += "         @Id_ParentComment,";
                selectQuery += "         @IsDateDeRef,";

                selectQuery += "         @commentaires,@MONTANTAVANTREMISE, @MONTANT,@QTE_ACTE,@ID_DEVIS,@id_tour,@id_tim,@id_chgt,@idNumLogiciel,@id_droit,@id_blanchiment,@id_diaporama,@id_accelerateur,@id_gauche,@vu,@donne,@desactive,@rabais, ";
                selectQuery += "         @dents,@isfulltime)";


                command.CommandText = selectQuery;

                command.Parameters.AddWithValue("@id", comm.Id);
                command.Parameters.AddWithValue("@id_praticien", comm.IdPraticien);
                command.Parameters.AddWithValue("@id_assistante", comm.IdAssistante);
                command.Parameters.AddWithValue("@id_secretaire", comm.IdSecretaire);
                command.Parameters.AddWithValue("@id_acte", comm.IdActe);
                command.Parameters.AddWithValue("@id_rdv", comm.IdRDV);
                command.Parameters.AddWithValue("@id_patient", comm.IdPatient);
                command.Parameters.AddWithValue("@nbJours", comm.NbJours);
                command.Parameters.AddWithValue("@nbMois", comm.NbMois);
                command.Parameters.AddWithValue("@IdScenario", comm.IdScenario);
                command.Parameters.AddWithValue("@Etat", comm.etat);
                command.Parameters.AddWithValue("@IdSemestre", comm.IdSemestre);
                command.Parameters.AddWithValue("@hygiene", comm.Hygiene);
                command.Parameters.AddWithValue("@date_comm", comm.date == null ? DBNull.Value : (object)comm.date.Value);
                command.Parameters.AddWithValue("@commentaires", comm.Commentaire);
                command.Parameters.AddWithValue("@commentairesafaire", comm.CommentaireAFaire);
                command.Parameters.AddWithValue("@Id_ParentComment", comm.IdParentComment);
                command.Parameters.AddWithValue("@IsDateDeRef", comm.IsDateDeRef);
                command.Parameters.AddWithValue("@modecreation", comm.modecreation);
                command.Parameters.AddWithValue("@DATEPREVISIONNELLE", comm.DatePrevisionnnelle);
                if (comm.Acte != null)
                {
                    command.Parameters.AddWithValue("@MONTANTAVANTREMISE", comm.Acte.prix_acte);
                    command.Parameters.AddWithValue("@QTE_ACTE", comm.Acte.quantite);
                   

                }
                else
                {
                    command.Parameters.AddWithValue("@MONTANTAVANTREMISE", 0);
                    command.Parameters.AddWithValue("@QTE_ACTE", 0);
                   

                }
                command.Parameters.AddWithValue("@MONTANT", comm.prix_traitement);

                command.Parameters.AddWithValue("@ID_DEVIS", comm.Id_devis);
                command.Parameters.AddWithValue("@id_tour", comm.idTour);
                command.Parameters.AddWithValue("@id_tim", comm.idTim);
                command.Parameters.AddWithValue("@id_chgt", comm.idChgt);
                command.Parameters.AddWithValue("@idNumLogiciel", comm.idNumLogiciel);
                command.Parameters.AddWithValue("@id_droit", comm.idDroit);
                command.Parameters.AddWithValue("@id_blanchiment", comm.idBlanchiment);
                command.Parameters.AddWithValue("@id_diaporama", comm.idDiaporama);
                command.Parameters.AddWithValue("@id_accelerateur", comm.idAccelerateur);
                command.Parameters.AddWithValue("@id_gauche", comm.idGauche);
                command.Parameters.AddWithValue("@donne", comm.Donne);
                command.Parameters.AddWithValue("@desactive", comm.desactive);
                command.Parameters.AddWithValue("@rabais", comm.rabais);

                command.Parameters.AddWithValue("@vu", comm.Vu);
                command.Parameters.AddWithValue("@dents", comm.dents);
            
               
                    command.Parameters.AddWithValue("@isfulltime", comm.isfulltime);
              

                command.ExecuteNonQuery();
                transaction.Commit();
                
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

        public static DataRow GetCommClinique(int Id)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "        id_praticien, ";
                selectQuery += "        id_assistante, ";
                selectQuery += "        id_secretaire, ";
                selectQuery += "        id_acte, ";
                selectQuery += "        id_rdv, ";
                selectQuery += "        Hygiene, ";
                selectQuery += "        id_patient, ";
                selectQuery += "        date_comm, ";
                selectQuery += "        nbjours, ";
                selectQuery += "        nbmois, ";
                selectQuery += "        id_scenario, ";
                selectQuery += "        etat, ";
                selectQuery += "        dateprevisionnelle, ";

                selectQuery += "        isdatederef, ";
                selectQuery += "        modecreation, ";
                selectQuery += "        id_semestre, ";
                selectQuery += "        commentaires,";
                selectQuery += "        id_parentcomment,";
                selectQuery += "        commentairesafaire,montant, montantavantremise,echeance, qte_acte,id_tour,id_tim,id_chgt,idnumlogiciel,id_droit,id_blanchiment,id_diaporama,id_accelerateur,id_gauche,vu,donne,desactive,rabais,isfulltime";
                selectQuery += " from base_comm";
                selectQuery += " where base_comm.id=@id";


                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@id", Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];
                return dt.Rows.Count > 0 ? dt.Rows[0] : null;

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




        public static DataTable GetCommClinique(basePatient pat)
        {
            return GetCommCliniquesByIdPatient(pat.Id);
        }

        public static DataTable GetCommCliniquesByIdPatient(int IdPatient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "        id_praticien, ";
                selectQuery += "        id_assistante, ";
                selectQuery += "        id_secretaire, ";
                selectQuery += "        id_acte, ";
                selectQuery += "        id_rdv, ";
                selectQuery += "        Hygiene, ";
                selectQuery += "        id_patient, ";
                selectQuery += "        date_comm, ";
                selectQuery += "        NBJours, ";
                selectQuery += "        NBMois, ";
                selectQuery += "        ID_SCENARIO, ";
                selectQuery += "        Etat, ";
                selectQuery += "        DATEPREVISIONNELLE, ";
                selectQuery += "        ISDATEDEREF, ";
                selectQuery += "        Id_Semestre, ";
                selectQuery += "        modecreation, ";

                selectQuery += "        commentaires,";
                selectQuery += "        Id_ParentComment,";
                selectQuery += "        commentairesafaire, MONTANT,MONTANTAVANTREMISE,ECHEANCE, qte_acte,id_tour,id_tim,id_chgt,idNumLogiciel,id_droit,id_blanchiment,id_diaporama,id_accelerateur,id_gauche,vu,donne,DESACTIVE,RABAIS,isfulltime";
                /*    selectQuery += " from ";


                    selectQuery += " (select * from";

                    selectQuery += " (";

                    selectQuery += " select 1 orderCol,id,ISDATEDEREF,modecreation,dateprevisionnelle,         id_praticien,         id_assistante,         id_secretaire,         id_acte,         id_rdv,         Hygiene,         id_patient,         date_comm,         NBJours,         NBMois,         ID_SCENARIO,Etat,         Id_Semestre,         commentaires,Id_ParentComment,        commentairesafaire, MONTANT,MONTANTAVANTREMISE,ECHEANCE, qte_acte,id_tour,id_tim,id_chgt,idNumLogiciel,id_droit,id_blanchiment,id_diaporama,id_accelerateur,id_gauche,vu,donne,DESACTIVE,RABAIS";*/
                selectQuery += " from base_comm";
                selectQuery += " where id_patient = @IDPAT ";
                //and date_comm is not null";
                /*     selectQuery += " order by date_comm desc";

                     selectQuery += " ) as t1";
                     selectQuery += " union";
                     selectQuery += " select * from (";

                     selectQuery += " select 2 orderCol,id, ISDATEDEREF,modecreation,dateprevisionnelle,        id_praticien,         id_assistante,         id_secretaire,         id_acte,         id_rdv,         Hygiene,         id_patient,         date_comm,         NBJours,         NBMois,         ID_SCENARIO,Etat,         Id_Semestre,         commentaires,Id_ParentComment,        commentairesafaire, MONTANT,MONTANTAVANTREMISE,ECHEANCE, qte_acte,id_tour,id_tim,id_chgt,idNumLogiciel,id_droit,id_blanchiment,id_diaporama,id_accelerateur,id_gauche,vu,donne,DESACTIVE,RABAIS";
                     selectQuery += " from base_comm";
                     selectQuery += " where id_patient = @IDPAT and date_comm is null";

                     selectQuery += " ) as t2) as tp ";*/


                selectQuery += " order by  date_comm desc,(30*NBMOIS)+NBJOURS asc,Id_ParentComment asc";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@IDPAT", IdPatient);

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





        public static DataTable GetCommCliniqueMateriels(basePatient pat)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = "select id_comm, ";
                selectQuery += " id_baseproduit, ";
                selectQuery += " m.MATERIEL_LIBELLE, ";
                selectQuery += " qte, ";
                selectQuery += " base_comm_mat.id_materiel,base_comm_mat.DESACTIVE, ";
                selectQuery += " m.shortlib, m.MATERIEL_COULEUR,base_comm_mat.MONTANTAVANTREMISE,base_comm_mat.MONTANT, base_comm_mat.ECHEANCE, base_comm_mat.ID_ENCAISSEMENT   from base_comm_mat ";
                selectQuery += " left join materiels M on m.ID_MATERIEL = base_comm_mat.ID_MATERIEL ";
                selectQuery += " inner join base_comm ";
                selectQuery += " on base_comm_mat.id_comm=base_comm.ID ";
                selectQuery += " where ID_PATIENT = @ID_PATIENT ";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@ID_PATIENT", pat.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                //   transaction.Commit();

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


        public static DataTable GetCommCliniqueMateriels(CommClinique com)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = "select id_comm,         id_baseproduit,         m.MATERIEL_LIBELLE, ";
                selectQuery += " bcm.QTE,bcm.ID_MATERIEL,m.SHORTLIB,m.MATERIEL_COULEUR, bcm.MONTANT, bcm.MONTANTAVANTREMISE, bcm.ECHEANCE, bcm.ID_ENCAISSEMENT,bcm.DESACTIVE,bcm.RABAIS ";
                selectQuery += " from base_comm_mat bcm ";
                selectQuery += " left join materiels m on m.ID_MATERIEL  =  bcm.ID_MATERIEL ";
                selectQuery += " where id_comm = @IDCOMM";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@IDCOMM", com.Id);

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


        public static DataTable GetCommCliniqueActes(basePatient pat, string Type_Acte = "")
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = "select base_comm_actes.*,   actes.*  ";
                selectQuery += " from base_comm_actes";
                selectQuery += " inner join base_comm b on b.ID = base_comm_actes.ID_COMM";
                selectQuery += " left join base_traitement bt on bt.ID_COMM = base_comm_actes.ID_COMM ";
                //      selectQuery += " left join base_echeance BE on BE.ID_TRAITEMENT = bt.ID ";
                selectQuery += " inner join actes on base_comm_actes.ID_ACTE=actes.ID_ACTE";
                selectQuery += " where b.ID_PATIENT = @ID_PATIENT";
                selectQuery += " and base_comm_actes.TYPE_ACTE_SUPP = '" + Type_Acte + "'";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@ID_PATIENT", pat.Id);

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

        //seif
        public static DataTable GetCommCliniqueActesSupp(int IdCom, int IdPatient, string Type_Acte = "")
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            //  MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select BE.ID_ENCAISSEMENT,BE.ID ID_ECHEANCE,base_comm_actes.*,   actes.*  ";
                selectQuery += " from base_comm_actes";
                selectQuery += " inner join base_comm on base_comm_actes.id_comm=base_comm.ID";
                selectQuery += " inner join actes on base_comm_actes.ID_ACTE=actes.ID_ACTE";
                selectQuery += " left join base_traitement bt on bt.ID_COMM = base_comm_actes.ID_COMM AND  bt.TYPE_COMMENT = '0'";
                selectQuery += " left join base_echeance BE on BE.ID_TRAITEMENT = bt.ID ";
                selectQuery += " where base_comm.ID_PATIENT = @ID_PATIENT";
                selectQuery += " and base_comm_actes.id_comm = @ID_COMM";
                selectQuery += " and base_comm_actes.TYPE_ACTE_SUPP = '" + Type_Acte + "'";

                //string selectQuery = "select BE.ID_ENCAISSEMENT,be.ID ID_ECHEANCE,base_comm_actes.*,   actes.*  ";
                //selectQuery += " from base_comm_actes";
                //selectQuery += " inner join base_comm on base_comm_actes.id_comm=base_comm.ID";
                //selectQuery += " left join base_traitement bt on bt.ID_COMM = base_comm_actes.ID_COMM ";
                //selectQuery += " inner join actes on base_comm_actes.ID_ACTE=actes.ID_ACTE";
                //selectQuery += " left join base_echeance BE on BE.ID_TRAITEMENT = bt.ID ";
                //selectQuery += " where base_comm.ID_PATIENT = @ID_PATIENT";
                //selectQuery += " and base_comm_actes.id_comm = @ID_COMM";
                //selectQuery += " and base_comm_actes.TYPE_ACTE_SUPP = '" + Type_Acte + "'";

                // Modification NADHEMMMMM



                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@ID_PATIENT", IdPatient);
                command.Parameters.AddWithValue("@ID_COMM", IdCom);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                //transaction.Commit();

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

        public static DataTable GetCommCliniqueActes(CommClinique com, string Type_Acte = "")
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            //  MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                /*string selectQuery = "select * ";
                selectQuery += " from base_comm_actes";*/






                string selectQuery = "SELECT  a.*, b.*  ";
                selectQuery += " FROM base_comm_actes a  ";
                selectQuery += " inner join base_comm bc on bc.ID = a.ID_COMM  ";
                selectQuery += " left join base_traitement bt on bt.ID_COMM = bc.ID  and bt.TYPE_COMMENT = '0'";
                //    selectQuery += " left join base_echeance BE on BE.ID_TRAITEMENT = bt.ID ";
                selectQuery += " LEFT JOIN actes b on a.ID_ACTE = b.ID_ACTE  ";
                selectQuery += " where a.id_comm =  @IDCOMM and a.TYPE_ACTE_SUPP = '" + Type_Acte + "' ";



                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@IDCOMM", com.Id);

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





        public static DataTable GetCommCliniqueRadios(basePatient pat)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            //  MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id_comm, ";
                selectQuery += "        typeradio ";
                selectQuery += " from base_comm_radios";
                selectQuery += " inner join base_comm on base_comm_radios.id_comm=base_comm.ID";
                selectQuery += " where ID_PATIENT = @ID_PATIENT";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@ID_PATIENT", pat.Id);

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


        public static DataTable GetCommCliniqueRadios(CommClinique com)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            //  MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id_comm, ";
                selectQuery += "        typeradio ";
                selectQuery += " from base_comm_radios";

                selectQuery += " where id_comm = @IDCOMM";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@IDCOMM", com.Id);

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





        public static DataTable GetCommCliniquePhotos(basePatient pat)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            //   MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id_comm, ";
                selectQuery += "        typephoto ";
                selectQuery += " from base_comm_photos";
                selectQuery += " inner join base_comm on base_comm_photos.id_comm=base_comm.ID";
                selectQuery += " where ID_PATIENT = @ID_PATIENT";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@ID_PATIENT", pat.Id);

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


        public static DataTable GetCommCliniquePhotos(CommClinique com)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            // MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id_comm, ";
                selectQuery += "        typephoto ";
                selectQuery += " from base_comm_photos";

                selectQuery += " where id_comm = @IDCOMM";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@IDCOMM", com.Id);

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

        public static DataTable GetCommCliniqueAutrePersonne(basePatient pat)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            //MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id_comm, ";
                selectQuery += "        ID_CORRESPONDANT, ";
                selectQuery += "        personne.per_nom , ";
                selectQuery += "        personne.per_prenom  ";
                selectQuery += " from base_comm_autrepers";
                selectQuery += " inner join personne on personne.ID_PERSONNE=ID_CORRESPONDANT";
                selectQuery += " inner join base_comm on base_comm_autrepers.id_comm=base_comm.ID";
                selectQuery += " where ID_PATIENT = @ID_PATIENT";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@ID_PATIENT", pat.Id);

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


        public static DataTable GetCommCliniqueAutrePersonne(CommClinique com)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            // MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id_comm, ";
                selectQuery += "        ID_CORRESPONDANT, ";
                selectQuery += "        personne.per_nom , ";
                selectQuery += "        personne.per_prenom  ";
                selectQuery += " from base_comm_autrepers";
                selectQuery += " inner join personne on personne.ID_PERSONNE=ID_CORRESPONDANT";

                selectQuery += " where id_comm = @IDCOMM";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@IDCOMM", com.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                //transaction.Commit();

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





        public static DataTable GetCommCliniqueDentAExtraire(basePatient pat)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            //  MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id_comm, ";
                selectQuery += "        DENTS ";
                selectQuery += " from base_comm_aextraire";
                selectQuery += " inner join base_comm on base_comm_aextraire.id_comm=base_comm.ID";
                selectQuery += " where ID_PATIENT = @ID_PATIENT";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@ID_PATIENT", pat.Id);

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


        public static DataTable GetCommCliniqueDentAExtraire(CommClinique com)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = "select id_comm, ";
                selectQuery += "        DENTS ";
                selectQuery += " from base_comm_aextraire";

                selectQuery += " where id_comm = @IDCOMM";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@IDCOMM", com.Id);

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

        public static void setComCliniqueDentsAExtraire(CommClinique comm)
        {

            if (comm.DentsAExtraire == null) return;

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "";
                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);


                selectQuery = "delete from base_comm_aextraire where ID_COMM = @id";

                command.Parameters.AddWithValue("@id", comm.Id);
                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                object obj = command.ExecuteNonQuery();



                selectQuery = "insert into base_comm_aextraire (id_comm, ";
                selectQuery += "                            DENTS)";
                selectQuery += " values (@id_comm, ";
                selectQuery += "         @DENTS)";





                command.CommandText = selectQuery;

                foreach (CommDentAExtraire cr in comm.DentsAExtraire)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_comm", comm.Id);
                    command.Parameters.AddWithValue("@DENTS", cr.dents);

                    command.ExecuteNonQuery();
                }


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


        public static void setComCliniqueAutrePersonnes(CommClinique comm)
        {

            if (comm.AutrePersonnes == null) return;

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "";
                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);


                selectQuery = "delete from base_comm_autrepers where ID_COMM = @id";

                command.Parameters.AddWithValue("@id", comm.Id);
                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                object obj = command.ExecuteNonQuery();



                selectQuery = "insert into base_comm_autrepers (id_comm, ";
                selectQuery += "                            ID_CORRESPONDANT)";
                selectQuery += " values (@id_comm, ";
                selectQuery += "         @ID_CORRESPONDANT)";





                command.CommandText = selectQuery;

                foreach (CommAutrePersonne cr in comm.AutrePersonnes)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_comm", comm.Id);
                    command.Parameters.AddWithValue("@ID_CORRESPONDANT", cr.IdCorrespondant);

                    command.ExecuteNonQuery();
                }


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

        public static void setComCliniqueMateriels(CommClinique comm)
        {

            if (comm.Materiels == null) return;

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "";
                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);


                selectQuery = "delete from base_comm_mat where ID_COMM = @id";

                command.Parameters.AddWithValue("@id", comm.Id);
                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                object obj = command.ExecuteNonQuery();



                selectQuery = "insert into base_comm_mat (id_comm, ";
                selectQuery += "                            id_baseproduit, ";
                selectQuery += "                            libelle, ";
                selectQuery += "                            qte, ";
                selectQuery += "                            id_materiel, ";
                selectQuery += "                            shortlib,MONTANT, MONTANTAVANTREMISE,ECHEANCE, ID_ENCAISSEMENT,DESACTIVE)";
                selectQuery += " values (@id_comm, ";
                selectQuery += "         @id_baseproduit, ";
                selectQuery += "         @libelle, ";
                selectQuery += "         @qte, ";
                selectQuery += "         @id_materiel, ";
                selectQuery += "         @shortlib,@MONTANT, @MONTANTAVANTREMISE,@ECHEANCE, @ID_ENCAISSEMENT,@DESACTIVE)";





                command.CommandText = selectQuery;

                foreach (CommMateriel cr in comm.Materiels)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_comm", comm.Id);
                    command.Parameters.AddWithValue("@id_baseproduit", cr.IdBaseProduit);
                    command.Parameters.AddWithValue("@libelle", cr.Libelle);
                    command.Parameters.AddWithValue("@qte", cr.Qte);
                    command.Parameters.AddWithValue("@id_materiel", cr.idMateriel);
                    if (cr.ShortLib.Length > 10)
                        command.Parameters.AddWithValue("@shortlib", cr.ShortLib.Substring(0, 10));
                    else
                        command.Parameters.AddWithValue("@shortlib", cr.ShortLib);
                    command.Parameters.AddWithValue("@MONTANT", cr.prix_materiel_traitement * cr.Qte);
                    command.Parameters.AddWithValue("@MONTANTAVANTREMISE", cr.prix_materiel);
                    command.Parameters.AddWithValue("@ECHEANCE", cr.echeancestemp);
                    command.Parameters.AddWithValue("@ID_ENCAISSEMENT", cr.idencaissement);
                    command.Parameters.AddWithValue("@DESACTIVE", cr.desactive);
                    command.ExecuteNonQuery();
                }


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



        public static void setComCliniqueActes(CommClinique comm, string TYPE_ACTE_SUP = "")
        {
            // if (comm.ActesSupp == null) return;
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "";
                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);


                selectQuery = "delete from base_comm_actes where ID_COMM = @id";
                selectQuery += " AND TYPE_ACTE_SUPP='" + TYPE_ACTE_SUP + "'";
                //if (TYPE_ACTE_SUP != "")
                //{
                //    selectQuery += " AND TYPE_ACTE_SUPP='" + TYPE_ACTE_SUP + "'";
                //}



                command.Parameters.AddWithValue("@id", comm.Id);
                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                object obj = command.ExecuteNonQuery();



                selectQuery = "insert into base_comm_actes (id_comm, ";
                selectQuery += "                               ID_ACTE, TYPE_ACTE_SUPP, QTE,MONTANT, MONTANTAVANTREMISE,ECHEANCE,DESACTIVE,RABAIS)";
                selectQuery += " values (@id_comm, ";
                selectQuery += "         @ID_ACTE,";
                selectQuery += "         @TYPE_ACTE_SUPP, @QTE,@MONTANT, @MONTANTAVANTREMISE,@ECHEANCE,@DESACTIVE,@RABAIS)";

                command.CommandText = selectQuery;
                if (TYPE_ACTE_SUP == "R")
                {
                    foreach (CommActes car in comm.Radios)
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@id_comm", comm.Id);
                        command.Parameters.AddWithValue("@ID_ACTE", car.IdActe);
                        command.Parameters.AddWithValue("@TYPE_ACTE_SUPP", "R");
                        command.Parameters.AddWithValue("@QTE", car.Qte);
                        command.Parameters.AddWithValue("@MONTANT", car.prix_traitement * car.Qte);
                        command.Parameters.AddWithValue("@MONTANTAVANTREMISE", car.prix_acte);
                        command.Parameters.AddWithValue("@ECHEANCE", car.echeancestemp);
                        command.Parameters.AddWithValue("@DESACTIVE", car.desactive);
                        command.Parameters.AddWithValue("@RABAIS", car.rabais);
                        command.ExecuteNonQuery();
                    }
                }
                if (TYPE_ACTE_SUP == "P")
                {
                    foreach (CommActes cap in comm.photos)
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@id_comm", comm.Id);
                        command.Parameters.AddWithValue("@ID_ACTE", cap.IdActe);
                        command.Parameters.AddWithValue("@TYPE_ACTE_SUPP", "P");
                        command.Parameters.AddWithValue("@QTE", cap.Qte);
                        command.Parameters.AddWithValue("@MONTANT", cap.prix_traitement * cap.Qte);
                        command.Parameters.AddWithValue("@MONTANTAVANTREMISE", cap.prix_acte);
                        command.Parameters.AddWithValue("@DESACTIVE", cap.desactive);
                        command.Parameters.AddWithValue("@ECHEANCE", cap.echeancestemp);
                        command.Parameters.AddWithValue("@RABAIS", cap.rabais);
                     
                        command.ExecuteNonQuery();
                    }
                }
                if (TYPE_ACTE_SUP == "")
                {
                    if (comm.ActesSupp != null)
                    {
                        foreach (CommActes cas in comm.ActesSupp)
                        {
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@id_comm", comm.Id);
                            command.Parameters.AddWithValue("@ID_ACTE", cas.IdActe);
                            command.Parameters.AddWithValue("@TYPE_ACTE_SUPP", "");
                            command.Parameters.AddWithValue("@QTE", cas.Qte);
                            command.Parameters.AddWithValue("@MONTANT", cas.prix_traitement * cas.Qte);
                            command.Parameters.AddWithValue("@MONTANTAVANTREMISE", cas.prix_acte);
                            command.Parameters.AddWithValue("@ECHEANCE", cas.echeancestemp);
                            command.Parameters.AddWithValue("@DESACTIVE", cas.desactive);
                            command.Parameters.AddWithValue("@RABAIS", cas.rabais);
                            command.ExecuteNonQuery();
                        }
                    }
                }
                //foreach (CommActes cr in TmpActesSupp)
                //{
                //    command.Parameters.Clear();
                //    command.Parameters.AddWithValue("@id_comm", comm.Id);
                //    command.Parameters.AddWithValue("@ID_ACTE", cr.IdActe);
                //    command.Parameters.AddWithValue("@TYPE_ACTE_SUPP", TYPE_ACTE_SUP);

                //    command.ExecuteNonQuery();
                //}


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

        public static void setComCliniqueIsActive(CommClinique comm)
        {

            if (comm == null) return;

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "";

                selectQuery = "update base_comm set DESACTIVE = @DESACTIVE where ID=@id_comm";



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandText = selectQuery;

                command.Parameters.AddWithValue("@DESACTIVE", comm.desactive);
                command.Parameters.AddWithValue("@id_comm", comm.Id);

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

        //public static void setComCliniquePhotos(CommClinique comm)
        //{


        //    if (comm.photos == null) return;

        //    if (connection == null) getConnection(); if (connection == null) return;

        //    if (connection.State == ConnectionState.Closed) connection.Open();
        //    MySqlTransaction transaction = connection.BeginTransaction();
        //    try
        //    {

        //        string selectQuery = "";
        //        MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);


        //        selectQuery = "delete from base_comm_photos where ID_COMM = @id";

        //        command.Parameters.AddWithValue("@id", comm.Id);
        //        command.CommandText = selectQuery;
        //        command.CommandType = CommandType.Text;
        //        object obj = command.ExecuteNonQuery();



        //        selectQuery = "insert into base_comm_photos (id_comm, ";
        //        selectQuery += "                               typephoto)";
        //        selectQuery += " values (@id_comm, ";
        //        selectQuery += "         @typephoto)";




        //        command.CommandText = selectQuery;

        //        foreach (CommPhoto cr in comm.photos)
        //        {
        //            command.Parameters.Clear();
        //            command.Parameters.AddWithValue("@id_comm", comm.Id);
        //            command.Parameters.AddWithValue("@typephoto", cr.typephoto);

        //            command.ExecuteNonQuery();
        //        }


        //        command.Transaction.Commit();



        //    }
        //    catch (System.SystemException ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //       connection.Close();

        //    }
        //}

        //public static void setComCliniqueRadio(CommClinique comm)
        //{

        //    if (comm.Radios == null) return;

        //    if (connection == null) getConnection(); if (connection == null) return;

        //    if (connection.State == ConnectionState.Closed) connection.Open();
        //    MySqlTransaction transaction = connection.BeginTransaction();
        //    try
        //    {

        //        string selectQuery = "";
        //        MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);


        //        selectQuery = "delete from base_comm_radios where ID_COMM = @id";

        //        command.Parameters.AddWithValue("@id", comm.Id);
        //        command.CommandText = selectQuery;
        //        command.CommandType = CommandType.Text;
        //        object obj = command.ExecuteNonQuery();



        //        selectQuery = "insert into base_comm_radios (id_comm, ";
        //        selectQuery += "                               typeradio)";
        //        selectQuery += " values (@id_comm, ";
        //        selectQuery += "         @typeradio)";




        //        command.CommandText = selectQuery;

        //        foreach (CommRadio cr in comm.Radios)
        //        {
        //            command.Parameters.Clear();
        //            command.Parameters.AddWithValue("@id_comm", comm.Id);
        //            command.Parameters.AddWithValue("@typeradio", cr.typeradio);

        //            command.ExecuteNonQuery();
        //        }


        //        command.Transaction.Commit();



        //    }
        //    catch (System.SystemException ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //       connection.Close();

        //    }
        //}
        #endregion
    }
}
