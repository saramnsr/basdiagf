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

        #region Commentaires Historisables


        public static DataTable getCommentHistoInScenarios()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            //   MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = " select BASE_SCEN_COMMENT_DETAILS.ID, ";
                selectQuery += "        BASE_SCEN_COMMENT_DETAILS.TYPESCENARIO, ";
                selectQuery += "        BASE_SCEN_COMMENT_DETAILS.CODE, ";
                selectQuery += "        BASE_SCEN_COMMENT_DETAILS.IMPORTANCE, ";
                selectQuery += "        BASE_SCEN_COMMENT_DETAILS.PARENT, ";
                selectQuery += "        BASE_SCEN_COMMENT_DETAILS.ORDRE, ";
                selectQuery += "        BASE_SCEN_COMMENT_DETAILS.COMMENTAIRE, ";
                selectQuery += "        BASE_SCEN_COMMENT_DETAILS.ID_SCENARIO, ";
                selectQuery += "        BASE_SCENARIOS_COMMENT.LIBELLE, ";
                selectQuery += "        BASE_SCENARIOS_COMMENT.TYPE_COMMENT TYPE_SCENARIO ";
                selectQuery += " from BASE_SCEN_COMMENT_DETAILS";
                selectQuery += " inner join BASE_SCENARIOS_COMMENT on BASE_SCENARIOS_COMMENT.ID=BASE_SCEN_COMMENT_DETAILS.ID_SCENARIO";
                selectQuery += " where ID_SCENARIO is not null";
                selectQuery += " order by BASE_SCEN_COMMENT_DETAILS.ID_SCENARIO, BASE_SCEN_COMMENT_DETAILS.ORDRE asc";


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


        public static DataTable GetCommentairesWithoutPatient()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = " select base_comments.ID, ";
                selectQuery += "        base_comments.ID_PATIENT, ";
                selectQuery += "        base_comments.TYPE_COMMENT, ";
                selectQuery += "        base_comments.DATE_COMMENT, ";
                selectQuery += "        base_comments.COMMENT, ";
                selectQuery += "        base_comments.CODECOMMENTAIRE, ";
                selectQuery += "        base_comments.IMPORTANCE, ";
                selectQuery += "        base_comments.PARENT, ";
                selectQuery += "        base_comments.DATEFIN, ";
                selectQuery += "        base_comments.DATEDEBUT, ";
                selectQuery += "        base_comments.ID_WRITER ";
                selectQuery += " from base_comments";
                selectQuery += " where ID_PATIENT is null";


                MySqlCommand command = new MySqlCommand(selectQuery, connection);

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


        public static DataTable GetCommentaires(int idpat)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = " select base_comments.ID, ";
                selectQuery += "        base_comments.ID_PATIENT, ";
                selectQuery += "        base_comments.TYPE_COMMENT, ";
                selectQuery += "        base_comments.DATE_COMMENT, ";
                selectQuery += "        base_comments.COMMENT, ";
                selectQuery += "        base_comments.CODECOMMENTAIRE, ";
                selectQuery += "        base_comments.IMPORTANCE, ";
                selectQuery += "        base_comments.PARENT, ";
                selectQuery += "        base_comments.DATEFIN, ";
                selectQuery += "        base_comments.DATEDEBUT, ";
                selectQuery += "        base_comments.ID_WRITER ";
                selectQuery += " from base_comments";
                selectQuery += " where ID_PATIENT = @ID_PATIENT";


                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@ID_PATIENT", idpat);

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


        public static DataTable GettAllLastCommentaires(basePatient pat)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = " select base_comments.ID, ";
                selectQuery += "        nb_comments, ";
                selectQuery += "        base_comments.ID_PATIENT, ";
                selectQuery += "        base_comments.TYPE_COMMENT, ";
                selectQuery += "        base_comments.DATE_COMMENT, ";
                selectQuery += "        base_comments.COMMENT, ";
                selectQuery += "        base_comments.CODECOMMENTAIRE, ";
                selectQuery += "        base_comments.IMPORTANCE, ";
                selectQuery += "        base_comments.PARENT, ";
                selectQuery += "        base_comments.DATEFIN, ";
                selectQuery += "        base_comments.ID_WRITER ";
                selectQuery += " from base_comments";
                selectQuery += " inner join (";
                selectQuery += " select max(DATE_COMMENT) DATE_COMMENT,count(ID) nb_comments,TYPE_COMMENT";
                selectQuery += " from base_comments";
                selectQuery += " where ID_PATIENT = @ID_PATIENT";
                selectQuery += " group by TYPE_COMMENT";
                selectQuery += " ) lastthem on  lastthem.DATE_COMMENT= base_comments.date_comment and lastthem.TYPE_COMMENT= base_comments.TYPE_COMMENT";


                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@ID_PATIENT", pat.Id);

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


        public static DataTable GettAllCommentaires(basePatient pat)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = " select ID, ";
                selectQuery += "        ID_PATIENT, ";
                selectQuery += "        TYPE_COMMENT, ";
                selectQuery += "        DATE_COMMENT, ";
                selectQuery += "        COMMENT, ";
                selectQuery += "        CODECOMMENTAIRE, ";
                selectQuery += "        IMPORTANCE, ";
                selectQuery += "        PARENT, ";
                selectQuery += "        DATEFIN, ";
                selectQuery += "        ID_WRITER ";
                selectQuery += " from base_comments";
                selectQuery += " where ID_PATIENT = @ID_PATIENT";
                selectQuery += " order by DATE_COMMENT desc";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@ID_PATIENT", pat.Id);

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

        public static DataTable GettAllCommentaires(basePatient pat, CommentHisto.CommentHistoType type)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = " select ID, ";
                selectQuery += "        ID_PATIENT, ";
                selectQuery += "        TYPE_COMMENT, ";
                selectQuery += "        DATE_COMMENT, ";
                selectQuery += "        COMMENT, ";
                selectQuery += "        CODECOMMENTAIRE, ";
                selectQuery += "        IMPORTANCE, ";
                selectQuery += "        PARENT, ";
                selectQuery += "        DATEFIN, ";
                selectQuery += "        ID_WRITER ";
                selectQuery += " from base_comments";
                selectQuery += " where ID_PATIENT = @ID_PATIENT";
                selectQuery += " and TYPE_COMMENT = @TYPE_COMMENT";
                selectQuery += " order by DATE_COMMENT desc";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@ID_PATIENT", pat.Id);
                command.Parameters.AddWithValue("@TYPE_COMMENT", (int)type);

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

        public static DataRow GetLastCommentaire(basePatient pat, CommentHisto.CommentHistoType type)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = " select First 1 ID, ";
                selectQuery += "        ID_PATIENT, ";
                selectQuery += "        TYPE_COMMENT, ";
                selectQuery += "        DATE_COMMENT, ";
                selectQuery += "        COMMENT, ";
                selectQuery += "        CODECOMMENTAIRE, ";
                selectQuery += "        IMPORTANCE, ";
                selectQuery += "        PARENT, ";
                selectQuery += "        DATEFIN, ";
                selectQuery += "        ID_WRITER ";
                selectQuery += " from base_comments";
                selectQuery += " where ID_PATIENT = @ID_PATIENT";
                selectQuery += " and TYPE_COMMENT = @TYPE_COMMENT";
                selectQuery += " order by DATE_COMMENT desc";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@ID_PATIENT", pat.Id);
                command.Parameters.AddWithValue("@TYPE_COMMENT", (int)type);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count < 1) return null;

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


        public static void InsertCommentaires(CommentHisto value)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "insert into base_comments (";
                selectQuery += "                             id_patient, ";
                selectQuery += "                             type_comment, ";
                selectQuery += "                             comment, ";
                selectQuery += "                             DATE_COMMENT, ";
                selectQuery += "                             codecommentaire, ";
                selectQuery += "                             importance, ";
                selectQuery += "                             parent, ";
                selectQuery += "                             datefin, ";
                selectQuery += "                             datedebut, ";
                selectQuery += "                             id_writer, sender,isread)";
                selectQuery += " values (";
                selectQuery += "        @id_patient, ";
                selectQuery += "        @type_comment, ";
                selectQuery += "        @comment, ";
                selectQuery += "        @date_comment, ";
                selectQuery += "        @codecommentaire, ";
                selectQuery += "        @importance, ";
                selectQuery += "        @parent, ";
                selectQuery += "        @datefin, ";
                selectQuery += "        @datedebut, ";
                selectQuery += "        @id_writer,@sender,@isread)";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                command.CommandText = selectQuery;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id_patient", value.IdPatient);
                command.Parameters.AddWithValue("@type_comment", value.typecomment);
                command.Parameters.AddWithValue("@comment", value.comment);
                command.Parameters.AddWithValue("@codecommentaire", value.Code);
                command.Parameters.AddWithValue("@sender", value.sender);
                command.Parameters.AddWithValue("@importance", value.Importance);
                command.Parameters.AddWithValue("@parent", value.IdParent);
                command.Parameters.AddWithValue("@datefin", value.DateDeFin);
                command.Parameters.AddWithValue("@datedebut", value.DateDeDebut);
                command.Parameters.AddWithValue("@date_comment", value.DateCommentaire);
                command.Parameters.AddWithValue("@id_writer", value.Id_Ecrivain);
                command.Parameters.AddWithValue("@isread", 0);


                command.ExecuteNonQuery();
                int ID = (int)command.LastInsertedId;
                value.Id = ID;
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
        public static void UpdateCommentairePatient(int IdPatient,CommentHisto.CommentHistoType type)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();



            try
            {
                string selectQuery = "update base_comments";
                selectQuery += " set isread = 1";
                selectQuery += " where id_patient = @id_patient and type_comment = @type";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id_patient", IdPatient);
                command.Parameters.AddWithValue("@type", type);


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
        public static void UpdateCommentHisto(CommentHisto value)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();



            try
            {
                string selectQuery = "update base_comments";
                selectQuery += " set id_patient = @id_patient,";
                selectQuery += "    TYPE_COMMENT = @TYPE_COMMENT,";
                selectQuery += "    COMMENT = @COMMENT,";
                selectQuery += "    DATE_COMMENT = @DATE_COMMENT,";
                selectQuery += "    CODECOMMENTAIRE = @CODECOMMENTAIRE,";
                selectQuery += "    IMPORTANCE = @IMPORTANCE,";
                selectQuery += "    PARENT = @PARENT,";
                selectQuery += "    DATEFIN = @DATEFIN,";
                selectQuery += "    DATEDEBUT = @DATEDEBUT,";
                selectQuery += "    ID_WRITER = @ID_WRITER";
                selectQuery += " where (id = @id)";





                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", value.Id);
                command.Parameters.AddWithValue("@id_patient", value.IdPatient);

                command.Parameters.AddWithValue("@TYPE_COMMENT", value.typecomment);
                command.Parameters.AddWithValue("@COMMENT", value.comment);
                command.Parameters.AddWithValue("@DATE_COMMENT", value.DateCommentaire);
                command.Parameters.AddWithValue("@ID_WRITER", value.Id_Ecrivain);

                command.Parameters.AddWithValue("@CODECOMMENTAIRE", value.Code);
                command.Parameters.AddWithValue("@IMPORTANCE", value.Importance);
                command.Parameters.AddWithValue("@PARENT", value.IdParent);
                command.Parameters.AddWithValue("@DATEFIN", value.DateDeFin);
                command.Parameters.AddWithValue("@DATEDEBUT", value.DateDeDebut);




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

        public static void DeleteAchatsMateriels(int idcom, int vIdActe)
        {
            string selectQuery = "";
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                /*
                //suppression des echéances COMM
                string selectQuery = "DELETE FROM base_comm_ECHEANCES BCE where ";
                selectQuery += " bce.ID_COMM = @IDCOMM and bce.TYPE_ACTE_SUPP = 'M' and bce.ID_ACTE = @ID_ACTE";


                MySqlCommand vcommand = new MySqlCommand(selectQuery, connection, transaction);
                vcommand.CommandType = CommandType.Text;

                vcommand.CommandText = selectQuery;
                vcommand.Parameters.AddWithValue("@IDCOMM", idcom);
               
                vcommand.Parameters.AddWithValue("@ID_ACTE", vIdActe);
                vcommand.ExecuteNonQuery();
                */
                //suppression des échéances


                selectQuery = "DELETE FROM base_echeance  WHERE base_echeance.ID_TRAITEMENT in ( ";
                selectQuery += " select base_traitement.ID ";
                selectQuery += "  from base_traitement";
                selectQuery += " where base_traitement.ID_COMM = @idComm";
                selectQuery += " and base_traitement.ID_ACTE = @ID_ACTE";
                selectQuery += " )";





                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@idComm", idcom);


                command.Parameters.AddWithValue("@ID_ACTE", vIdActe);
                command.ExecuteNonQuery();

                //Suppression des achats (base_traitement)
                selectQuery = "Delete from base_traitement ";
                selectQuery += " where ID_COMM = @idComm AND  ID_ACTE = @ID_ACTE ";




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

            insertLog(selectQuery, System.Security.Principal.WindowsIdentity.GetCurrent().Name, "".ToString(), idcom.ToString() + ":ID_COMM");


        }
        public static void DeleteAchats(int idcom, int Type, int vIdActe, double PrixActe)
        {
            string selectQuery = "";
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {




                //récupération des échéances 
                selectQuery = "SELECT * FROM base_echeance BE WHERE BE.ID_TRAITEMENT in ( ";
                selectQuery += " SELECT a.id ";
                selectQuery += " FROM base_traitement a ";
                selectQuery += " where a.ID_COMM = @idComm ";
                selectQuery += " ) ";
                selectQuery += " order by MONTANT DESC ";




                MySqlCommand command0 = new MySqlCommand(selectQuery, connection, transaction);
                command0.CommandType = CommandType.Text;
                command0.Parameters.AddWithValue("@idComm", idcom);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command0);
                adapt.Fill(ds);


                DataTable dt = ds.Tables[0];

                List<int> lst = new List<int>();
                double TmpPrixActe = PrixActe;
                if (dt.Rows.Count > 0)
                {
                    if (TmpPrixActe == 0)
                    {

                    }
                    else
                        while (TmpPrixActe > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (TmpPrixActe >= Convert.ToDouble(dr["MONTANT"]))
                                {
                                    selectQuery = "DELETE FROM base_echeance BE where BE.ID = @ID_ECHEANCE  ";


                                    MySqlCommand vcommandSupp = new MySqlCommand(selectQuery, connection, transaction);
                                    vcommandSupp.CommandType = CommandType.Text;

                                    vcommandSupp.CommandText = selectQuery;
                                    vcommandSupp.Parameters.AddWithValue("@ID_ECHEANCE", dr["ID"]);

                                    vcommandSupp.ExecuteNonQuery();
                                    TmpPrixActe = TmpPrixActe - Convert.ToDouble(dr["MONTANT"]);
                                }
                                else
                                // soustraction du prix de la ligne du prix de l'echéance
                                {
                                    selectQuery = "UPDATE  base_echeance BE SET MONTANT= (MONTANT-@MONTANT) where BE.ID = @ID_ECHEANCE  ";


                                    MySqlCommand vcommandSupp = new MySqlCommand(selectQuery, connection, transaction);
                                    vcommandSupp.CommandType = CommandType.Text;

                                    vcommandSupp.CommandText = selectQuery;
                                    vcommandSupp.Parameters.AddWithValue("@ID_ECHEANCE", dr["ID"]);
                                    vcommandSupp.Parameters.AddWithValue("@MONTANT", TmpPrixActe);

                                    vcommandSupp.ExecuteNonQuery();
                                    break;
                                }

                                if (TmpPrixActe == 0)
                                    break;
                            }

                        }

                }

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                if (Type == 0 || Type == 5)
                {
                    //Suppression des achats (base_traitement)
                    selectQuery = "Delete from base_traitement ";
                    selectQuery += " where ID_COMM = @idComm AND TYPE_COMMENT = @TypeCom AND  ID_ACTE = @ID_ACTE ";

                    command.CommandText = selectQuery;
                    command.Parameters.AddWithValue("@idComm", idcom);
                    if (Type == 5)
                        command.Parameters.AddWithValue("@TypeCom", "M");
                    else
                        command.Parameters.AddWithValue("@TypeCom", Type);

                    command.Parameters.AddWithValue("@ID_ACTE", vIdActe);

                    command.ExecuteNonQuery();

                }
                else
                {
                    selectQuery = "update base_traitement";
                    selectQuery += "  set montant = (montant -@montant)";
                    selectQuery += " where (ID_COMM = @ID_COMM) and TYPE_COMMENT ='0'";



                    //selectQuery = "update base_traitement ";
                    //selectQuery += " where ID_COMM = @idComm AND TYPE_COMMENT = @TypeCom AND  ID_ACTE = @ID_ACTE ";
                    command.CommandText = selectQuery;
                    command.Parameters.AddWithValue("@ID_COMM", idcom);
                    command.Parameters.AddWithValue("@montant", PrixActe);
                    //command.Parameters.AddWithValue("@ID_ACTE", vIdActe);
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

            insertLog(selectQuery, System.Security.Principal.WindowsIdentity.GetCurrent().Name, "", idcom.ToString() + ":ID_COMM");


        }
        public static void DeleteCommentaires(CommentHisto value)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "Delete from base_comments ";
                selectQuery += " where id = @id ";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);


                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@id", value.Id);

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


        #endregion
    }
}
