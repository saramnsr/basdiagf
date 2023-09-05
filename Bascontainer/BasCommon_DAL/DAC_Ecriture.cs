using BasCommon_BO.Compta;
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

        public static DataTable GetMdlEcritures(MdlPieceComptable pc)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                //famille_Acte
                string selectQuery = @"select id, 
                                       codecompta, 
                                       id_mdl_piece_compta, 
                                       id_taxevaleurajoutee, 
                                       debit, 
                                       credit, 
                                       libelle
                                from bas_compta_mdl_ecriture
                                where id_mdl_piece_compta=@id ";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@id", pc.Id);

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


        public static DataTable GetEcritures(DateTime dte1, DateTime dte2, bool FromFog)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                //famille_Acte
                string selectQuery = @"select bas_compta_ecriture.id, 
                                       bas_compta_ecriture.codecompta, 
                                       bas_compta_ecriture.id_piece_compta, 
                                       bas_compta_ecriture.id_taxevaleurajoutee, 
                                       bas_compta_ecriture. debit, 
                                       bas_compta_ecriture.credit, 
                                       bas_compta_ecriture.libelle,
                                       bas_compta_ecriture.TYPEREGLEMENT
                                from bas_compta_ecriture
                                inner join bas_compta_piece on bas_compta_piece.ID = bas_compta_ecriture.id_piece_compta
                                where bas_compta_piece.DATEOPERATION between @dt1 and @dt2";
                
                if (!FromFog)
                    selectQuery += " and Fog<>'T'";
                else
                    selectQuery += " and Fog='T'";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@dt1", dte1.Date);
                command.Parameters.AddWithValue("@dt2", dte2.Date.AddDays(1));

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


        public static void AddMdlEcriture(MdlEcriture piece)
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
                string selectQuery = "select MAX(ID)+1 as NEWID from bas_compta_mdl_ecriture";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                object res = command.ExecuteScalar();
                if (res is DBNull)
                    piece.Id = 1;
                else
                    piece.Id = Convert.ToInt32(command.ExecuteScalar());

                selectQuery = @"insert into bas_compta_mdl_ecriture (id, codecompta, id_mdl_piece_compta, id_taxevaleurajoutee, debit, credit, libelle) values (@id, @codecompta, @id_piece_compta, @id_taxevaleurajoutee, @debit, @credit, @libelle)";

                command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", piece.Id);
                command.Parameters.AddWithValue("@codecompta", piece.Idcodecompta);
                command.Parameters.AddWithValue("@id_piece_compta", piece.IdPieceComptable);
                command.Parameters.AddWithValue("@id_taxevaleurajoutee", piece.taxe == null ? DBNull.Value : (object)piece.taxe.Code);
                command.Parameters.AddWithValue("@debit", piece.Debit == null ? DBNull.Value : (object)piece.Debit);
                command.Parameters.AddWithValue("@credit", piece.Credit == null ? DBNull.Value : (object)piece.Credit);
                command.Parameters.AddWithValue("@libelle", piece.Libelle);

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

        public static void UpdateMdlEcriture(MdlEcriture piece)
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


                string selectQuery = @"update bas_compta_mdl_ecriture
                                        set codecompta = @codecompta,
                                            id_mdl_piece_compta = @id_piece_compta,
                                            id_taxevaleurajoutee = @id_taxevaleurajoutee,
                                            debit = @debit,
                                            credit = @credit,
                                            libelle = @libelle
                                        where (id = @id)";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", piece.Id);
                command.Parameters.AddWithValue("@codecompta", piece.codecompta.Code);
                command.Parameters.AddWithValue("@id_piece_compta", piece.IdPieceComptable);
                command.Parameters.AddWithValue("@id_taxevaleurajoutee", piece.taxe.Code);
                command.Parameters.AddWithValue("@debit", piece.Debit);
                command.Parameters.AddWithValue("@credit", piece.Credit);
                command.Parameters.AddWithValue("@libelle", piece.Libelle);


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

        public static void DeleteMdlEcriture(MdlEcriture piece)
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


                string selectQuery = @"delete from bas_compta_mdl_ecriture
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





        public static DataTable GetEcritures(PieceComptable pc)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                //famille_Acte
                string selectQuery = @"select id, 
                                       codecompta, 
                                       id_piece_compta, 
                                       id_taxevaleurajoutee, 
                                       debit,  
                                       TYPEREGLEMENT, 
                                       credit, 
                                       libelle
                                from bas_compta_ecriture
                                where bas_compta_ecriture.id_piece_compta=@id ";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", pc.Id);

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


        public static DataRow GetEcriture(int id)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                //famille_Acte
                string selectQuery = @"select id, 
                                       codecompta, 
                                       id_piece_compta, 
                                       id_taxevaleurajoutee, 
                                       debit, 
                                       credit, 
                                       libelle
                                from bas_compta_ecriture
                                where bas_compta_ecriture.id=@id ";

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

        public static void DeleteEcriture(Ecriture ecriture)
        {
            if (ecriture.Id < 0) return;

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


                string selectQuery = @"delete from BAS_COMPTA_ECRITURE
                                        where (id = @id)";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", ecriture.Id);


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


        public static void DeleteEcritures(PieceComptable pc)
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


                string selectQuery = @"delete from BAS_COMPTA_ECRITURE
                                        where (ID_PIECE_COMPTA = @id)";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", pc.Id);


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


        public static void AddEcriture(Ecriture ecriture)
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
                string selectQuery = "select MAX(ID)+1 as NEWID from bas_compta_ecriture";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                object res = command.ExecuteScalar();
                if (res is DBNull)
                    ecriture.Id = 1;
                else
                    ecriture.Id = Convert.ToInt32(command.ExecuteScalar());

                selectQuery = @"insert into bas_compta_ecriture (id, codecompta, id_piece_compta, id_taxevaleurajoutee, debit, credit, libelle,TYPEREGLEMENT) values (@id, @codecompta, @id_piece_compta, @id_taxevaleurajoutee, @debit, @credit, @libelle,@TYPEREGLEMENT)";

                command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", ecriture.Id);
                command.Parameters.AddWithValue("@codecompta", ecriture.Idcodecompta.Trim());
                command.Parameters.AddWithValue("@id_piece_compta", ecriture.IdPieceComptable);
                command.Parameters.AddWithValue("@id_taxevaleurajoutee", ecriture.taxe==null?DBNull.Value:(object)ecriture.taxe.Code);
                command.Parameters.AddWithValue("@debit", ecriture.Debit==null?DBNull.Value:(object) ecriture.Debit);
                command.Parameters.AddWithValue("@credit", ecriture.Credit == null ? DBNull.Value : (object)ecriture.Credit);
                command.Parameters.AddWithValue("@libelle", ecriture.Libelle == null ? "" : ecriture.Libelle);
                command.Parameters.AddWithValue("@TYPEREGLEMENT", ecriture.Type);
                
                
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

        public static void UpdateEcriture(Ecriture ecriture)
        {
            if (ecriture.Id < 0) return;

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


                string selectQuery = @"update bas_compta_ecriture
                                        set codecompta = @codecompta,
                                            id_piece_compta = @id_piece_compta,
                                            id_taxevaleurajoutee = @id_taxevaleurajoutee,
                                            debit = @debit,
                                            credit = @credit,
                                            libelle = @libelle,
                                            TYPEREGLEMENT = @TYPEREGLEMENT
                                        where (id = @id)";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", ecriture.Id);
                command.Parameters.AddWithValue("@codecompta", ecriture.codecompta.Code);
                command.Parameters.AddWithValue("@id_piece_compta", ecriture.IdPieceComptable);
                command.Parameters.AddWithValue("@id_taxevaleurajoutee", ecriture.taxe.Code);
                command.Parameters.AddWithValue("@debit", ecriture.Debit);
                command.Parameters.AddWithValue("@credit", ecriture.Credit);
                command.Parameters.AddWithValue("@libelle", ecriture.Libelle);
                command.Parameters.AddWithValue("@TYPEREGLEMENT", ecriture.Type);
                

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
