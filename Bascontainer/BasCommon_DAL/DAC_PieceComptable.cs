using BasCommon_BO.Compta;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace BasCommon_DAL
{

    public static partial class DAC
    {




        public static DataTable GetMdlPieceComptable()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                //famille_Acte
                string selectQuery = @"select id, 
                                               libellemdl, 
                                               numjournal, 
                                               devise, 
                                               numpiece, 
                                               Organisation,
                                               libelle
                                        from bas_compta_mdl_piece";

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



        public static void DeleteMdlPieceComptable(MdlPieceComptable piece)
        {
            if (piece.Id < 0) return;

            if (connection == null) getConnection(); if (connection == null) return;

            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
            }
            catch (System.Exception e)
            {
                throw e;
            }

            MySqlTransaction transaction = connection.BeginTransaction();

            try
            {

                
                string selectQuery = @"delete from BAS_COMPTA_MDL_ECRITURE
                                        where (ID_MDL_PIECE_COMPTA = @id)";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", piece.Id);


                command.ExecuteNonQuery();

                 selectQuery = @"delete from bas_compta_mdl_piece
                                        where (id = @id)";


                command.CommandText = selectQuery;
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

        public static void AddMdlPieceComptable(MdlPieceComptable piece)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
            }
            catch (System.Exception e)
            {
                throw e;
            }

            MySqlTransaction transaction = connection.BeginTransaction();

            try
            {
                string selectQuery = "select MAX(ID)+1 as NEWID from bas_compta_mdl_piece";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                object res = command.ExecuteScalar();
                if (res is DBNull)
                    piece.Id = 1;
                else
                    piece.Id = Convert.ToInt32(command.ExecuteScalar());

                selectQuery = @"insert into bas_compta_mdl_piece (id, numjournal, devise,  numpiece, libelle, libellemdl,Organisation)
                                values (@id, @numjournal, @devise,  @numpiece, @libelle, @libellemdl, @Organisation)";

                command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", piece.Id);
                command.Parameters.AddWithValue("@numjournal", piece.journal.NumJournal);
                command.Parameters.AddWithValue("@numpiece", piece.NumPiece);
                command.Parameters.AddWithValue("@libelle", piece.Libelle);
                command.Parameters.AddWithValue("@libellemdl", piece.LibelleMdl);
                command.Parameters.AddWithValue("@devise", piece.devise.CodeMonnaie);
                command.Parameters.AddWithValue("@Organisation", piece.Organisation);

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

        public static void UpdateMdlPieceComptable(MdlPieceComptable piece)
        {
            if (piece.Id < 0) return;

            if (connection == null) getConnection(); if (connection == null) return;

            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
            }
            catch (System.Exception e)
            {
                throw e;
            }

            MySqlTransaction transaction = connection.BeginTransaction();

            try
            {


                string selectQuery = @"update bas_compta_mdl_piece
                                        set numjournal = @numjournal,
                                            devise = @devise,
                                            numpiece = @numpiece,
                                            libelle = @libelle
                                            libellemdl = @libellemdl;
                                            Organisation = @Organisation
                                        where (id = @id)";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", piece.Id);
                command.Parameters.AddWithValue("@numjournal", piece.journal.NumJournal);
                command.Parameters.AddWithValue("@numpiece", piece.NumPiece);
                command.Parameters.AddWithValue("@libelle", piece.Libelle);
                command.Parameters.AddWithValue("@libellemdl", piece.LibelleMdl);
                command.Parameters.AddWithValue("@devise", piece.devise.CodeMonnaie);
                command.Parameters.AddWithValue("@Organisation", piece.Organisation);

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







        public static DataTable GetPieceComptablesInTheFog(string text, DateTime? dteStart, DateTime? dteEnd)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                //famille_Acte
                string selectQuery = @"select id, numjournal, devise, dateoperation,DATEECHEANCE, numsaisie, numpiece, libelle,Fog
                                        from bas_compta_piece
                                        where Fog = 'T'";

                if (text!=null)
                    selectQuery += " and upper(libelle) LIKE '%" + text.ToUpper() + "%'";
                if ((dteStart  != null)&&(dteEnd !=null))
                    selectQuery += " and dateoperation between @dteS and @dteE";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                if (dteStart.HasValue) command.Parameters.AddWithValue("@dteS", dteStart.Value);
                if (dteEnd.HasValue) command.Parameters.AddWithValue("@dteE", dteEnd.Value);

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



        public static DataTable GetAllPieceComptables(DateTime dteStart, DateTime dteEnd,Journal jrnl)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                //famille_Acte
                string selectQuery = @"select id, numjournal, devise, dateoperation,DATEECHEANCE, numsaisie, numpiece, libelle,Fog
                                        from bas_compta_piece
                                        where bas_compta_piece.DATEOPERATION between @dteS and @dteE and NUMJOURNAL=@NUMJOURNAL ";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@dteS", dteStart);
                command.Parameters.AddWithValue("@dteE", dteEnd);
                command.Parameters.AddWithValue("@NUMJOURNAL", jrnl.NumJournal);

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




        public static DataTable GetPieceComptables(DateTime dteStart, DateTime dteEnd)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                //famille_Acte
                string selectQuery = @"select id, numjournal, devise, dateoperation,DATEECHEANCE, numsaisie, numpiece, libelle,Fog
                                        from bas_compta_piece
                                        where bas_compta_piece.DATEOPERATION between @dteS and @dteE and Fog <>'T'";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@dteS", dteStart);
                command.Parameters.AddWithValue("@dteE", dteEnd);

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



        public static DataRow GetPieceComptable(int id)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                //famille_Acte
                string selectQuery = @"select id, numjournal, devise, dateoperation,
                                       DATEECHEANCE, numsaisie, numpiece, 0 as TYPEREGLEMENT,
                                        libelle,Fog  from bas_compta_piece
                                        where bas_compta_piece.id=@id ";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count > 0) return dt.Rows[0];

                return null;

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

        
        public static int GetNextNumSaisie()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                //famille_Acte
                string selectQuery = @"select MAX(NUMSAISIE)+1 as NEWID from bas_compta_piece ";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                object obj = command.ExecuteScalar();
                if (obj is DBNull) return 1;
                else return Convert.ToInt32(obj);
                     


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

        public static bool CheckPieceIsOk(PieceComptable pc)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                //famille_Acte
                string selectQuery = @"select count(1) from bas_compta_piece where numpiece=@NUMPIECE and id<>@id";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@NUMPIECE", pc.NumPiece);
                command.Parameters.AddWithValue("@id", pc.Id);

                object obj = command.ExecuteScalar();
                if ((obj is DBNull) || (((int)obj)==0)) return true;
                else return false;
                     


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
        
        
        public static void DeletePieceComptable(PieceComptable piece)
        {
            if (piece.Id < 0) return;

            if (connection == null) getConnection(); if (connection == null) return;

            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
            }
            catch (System.Exception e)
            {
                throw e;
            }

            MySqlTransaction transaction = connection.BeginTransaction();

            try
            {


                string selectQuery = @"delete from bas_compta_piece
                                        where (id = @id)";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", piece.Id);


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
        
        public static void AddPieceComptable(PieceComptable piece)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
            }
            catch (System.Exception e)
            {
                string erreur = e.Message;
                throw e;
            }

            MySqlTransaction transaction = connection.BeginTransaction();

            try
            {
                string selectQuery = "select MAX(ID)+1 as NEWID from bas_compta_piece";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                object res = command.ExecuteScalar();
                if (res is DBNull)
                    piece.Id = 1;
                else
                    piece.Id = Convert.ToInt32(command.ExecuteScalar());

                selectQuery = @"insert into bas_compta_piece (id, numjournal, devise, dateoperation,DateEcheance, numsaisie, numpiece, libelle,Fog)
                                values (@id, @numjournal, @devise, @dateoperation,@DateEcheance, @numsaisie, @numpiece, @libelle,@Fog)";

                command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", piece.Id);
                command.Parameters.AddWithValue("@numjournal", piece.journal.NumJournal);
                command.Parameters.AddWithValue("@dateoperation", piece.DateOperation);
                command.Parameters.AddWithValue("@DateEcheance", piece.DateEcheance);
                
                command.Parameters.AddWithValue("@numsaisie", piece.NumSaisie);
                command.Parameters.AddWithValue("@numpiece", piece.NumPiece);
                command.Parameters.AddWithValue("@libelle", piece.Libelle);
                command.Parameters.AddWithValue("@devise", piece.devise.CodeMonnaie);
                command.Parameters.AddWithValue("@Fog", piece.Fog);
                
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

        public static void UpdatePieceComptable(PieceComptable piece)
        {
            if (piece.Id < 0) return;

            if (connection == null) getConnection(); if (connection == null) return;

            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
            }
            catch (System.Exception e)
            {
                throw e;
            }

            MySqlTransaction transaction = connection.BeginTransaction();

            try
            {


                string selectQuery = @"update bas_compta_piece
                                        set numjournal = @numjournal,
                                            devise = @devise,
                                            dateoperation = @dateoperation,
                                            DateEcheance = @DateEcheance,
                                            numsaisie = @numsaisie,
                                            numpiece = @numpiece,
                                            libelle = @libelle,
                                            Fog=@Fog
                                        where (id = @id)";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", piece.Id);
                command.Parameters.AddWithValue("@numjournal", piece.journal.NumJournal);
                command.Parameters.AddWithValue("@dateoperation", piece.DateOperation);
                command.Parameters.AddWithValue("@numsaisie", piece.NumSaisie);
                command.Parameters.AddWithValue("@numpiece", piece.NumPiece);
                command.Parameters.AddWithValue("@libelle", piece.Libelle);
                command.Parameters.AddWithValue("@DateEcheance", piece.DateEcheance);
                command.Parameters.AddWithValue("@devise", piece.devise.CodeMonnaie);
                command.Parameters.AddWithValue("@Fog", piece.Fog);
                

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


        public static void SortirDuBrouillard(PieceComptable piece)
        {
            if (piece.Id < 0) return;

            if (connection == null) getConnection(); if (connection == null) return;

            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
            }
            catch (System.Exception e)
            {
                throw e;
            }

            MySqlTransaction transaction = connection.BeginTransaction();

            try
            {


                string selectQuery = @"update bas_compta_piece
                                        set Fog='F'
                                        where (id = @id)";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", piece.Id);

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
    
    }
        
        
}
