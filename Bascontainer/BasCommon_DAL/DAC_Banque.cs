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
        public static DataTable GetBanquesEmetrices()
        {
            if (connection == null) getConnection(); 

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = " select id_banquenom, ";
                selectQuery += "        NOM ";
                selectQuery += " from banquenom";
                selectQuery += " order by nom";

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


        public static void AddBanqueEmetrices(Banque bnk)
        {
            if (connection == null) getConnection();

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
                string selectQuery = "select max(ID_BANQUENOM)+1 as NEWID from banquenom";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                try
                {
                    bnk.Id = Convert.ToInt32(command.ExecuteScalar());
                }catch(System.Exception)
                {
                    bnk.Id = 1;
                }

                selectQuery = "insert into banquenom (";
                selectQuery += "ID_BANQUENOM, ";
                selectQuery += "NOM)";
                selectQuery += " values (@ID_BANQUENOM, @NOM)";
                command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@ID_BANQUENOM", bnk.Id);
                command.Parameters.AddWithValue("@NOM", bnk.Libelle);

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



        public static List<string> GetBanquesDeRemise(EntiteJuridique ej)
        {


            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = " select cod_ban ";
                selectQuery += " from base_lnkentitybque";
                selectQuery += " where id_entity = @ID_ENTITY";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@ID_ENTITY",ej.Id);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];

                List<string> lst = new List<string>();
                foreach (DataRow dr in dt.Rows)
                    lst.Add(Convert.ToString(dr["COD_BAN"]).Trim());


                    return lst;

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
       

        public static DataTable GetBanquesDeRemise()
        {


            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = " select COD_BAN, ";
                selectQuery += "        LIB_BAN, ";
                selectQuery += "        ADR1_BAN, ";
                selectQuery += "        ADR2_BAN, ";
                selectQuery += "        CP_BAN, ";
                selectQuery += "        VIL_BAN, ";
                selectQuery += "        NUMA_BAN, ";
                selectQuery += "        NUMCPT_BAN, ";
                selectQuery += "        NUMGUI_BAN, ";
                selectQuery += "        NUMCLE_BAN, ";
                selectQuery += "        TITULAIRE, ";
                selectQuery += "        CODEBIC, ";
                selectQuery += "        CODEPAYS, ";
                selectQuery += "        journalComptable, ";
                selectQuery += "        CompteComptable, ";
                selectQuery += "        NUM_NNE ";
                selectQuery += " from banque";
                selectQuery += " order by LIB_BAN";

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



        public static void DeleteBanqueDeRemise(BanqueDeRemise bnk)
        {
            if (connection == null) getConnection();  

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


                string selectQuery = "select count(1) from base_paiementreel where ID_BANQUE_REMISE =  @COD_BAN";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@COD_BAN", bnk.Code);

                int nb = Convert.ToInt32(command.ExecuteScalar());

                if (nb > 0) 
                    throw new System.Exception("Des encaissements sont liés à ce compte !");


                selectQuery = "delete from base_lnkentitybque where  cod_ban = @COD_BAN ";
                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@COD_BAN", bnk.Code);

                command.ExecuteNonQuery();

                selectQuery = "delete from banque where  cod_ban = @COD_BAN ";
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
        }


        public static string AddBanqueDeRemise(EntiteJuridique en, BanqueDeRemise bnk)
        {
            if (connection == null) getConnection();  

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
                string selectQuery = "select count(*)+1 as NEWID from banque";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                    int id = 1;
                try
                {
                    id = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (System.Exception)
                {
                }
                string cod = "B" + id;

                selectQuery = "insert into banque (";
                selectQuery += "cod_ban, ";
                selectQuery += "LIB_BAN,  ";
                selectQuery += "ADR1_BAN, ";
                selectQuery += "ADR2_BAN, ";
                selectQuery += "CP_BAN, ";
                selectQuery += "VIL_BAN, ";
                selectQuery += "NUMCPT_BAN, ";
                selectQuery += "NUMGUI_BAN, ";
                selectQuery += "TITULAIRE, ";
                selectQuery += "journalComptable, ";
                selectQuery += "CODEBIC, ";
                selectQuery += "CODEPAYS, ";
                selectQuery += "CompteComptable, ";
                selectQuery += "NUMCLE_BAN, ";
                selectQuery += "NUM_NNE";
                selectQuery += ")";
                selectQuery += " values (@COD_BAN, @LIB_BAN, @ADR1_BAN, @ADR2_BAN, @CP_BAN, @VIL_BAN, @NUMCPT_BAN, @NUMGUI_BAN, @TITULAIRE,@journalComptable,@CODEBIC,@CODEPAYS,@CompteComptable,@NUMCLE_BAN,@NUM_NNE)";
                command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@COD_BAN", cod);
                command.Parameters.AddWithValue("@CODEBIC", bnk.CodeBIC);
                command.Parameters.AddWithValue("@CODEPAYS", bnk.CodePays);
                command.Parameters.AddWithValue("@LIB_BAN", bnk.Libelle);
                command.Parameters.AddWithValue("@ADR1_BAN", bnk.AddrBAN1);
                command.Parameters.AddWithValue("@ADR2_BAN", bnk.AddrBAN2);
                command.Parameters.AddWithValue("@CP_BAN", bnk.CodePostal);
                command.Parameters.AddWithValue("@VIL_BAN", bnk.Ville);

                command.Parameters.AddWithValue("@journalComptable", bnk.journalComptable==null?DBNull.Value:(object)bnk.journalComptable.NumJournal);
                command.Parameters.AddWithValue("@CompteComptable", bnk.CompteComptable);

                command.Parameters.AddWithValue("@NUMCPT_BAN", bnk.NumCPT);
                command.Parameters.AddWithValue("@NUMGUI_BAN", bnk.NumGui);
                command.Parameters.AddWithValue("@TITULAIRE", bnk.Titulaire);
                command.Parameters.AddWithValue("@NUMCLE_BAN", bnk.NumCle);
                command.Parameters.AddWithValue("@NUM_NNE", bnk.NumNNE);

                command.ExecuteNonQuery();

                selectQuery = "insert into base_lnkentitybque (ID_ENTITY, cod_ban)";
                selectQuery += " values (@ID_ENTITY, @COD_BAN)";

                command.CommandText = selectQuery;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@ID_ENTITY", en.Id);
                command.Parameters.AddWithValue("@COD_BAN", cod);

                command.ExecuteNonQuery();

                
                transaction.Commit();

                return cod;

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

        public static void UpdateBanqueDeRemise(BanqueDeRemise bank)
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

                string selectQuery = "update BANQUE";
                selectQuery += " set cod_ban = @COD_BAN,";
                selectQuery += "    LIB_BAN = @LIB_BAN,";
                selectQuery += "    ADR1_BAN = @ADR1_BAN,";
                selectQuery += "    ADR2_BAN = @ADR2_BAN,";
                selectQuery += "    CP_BAN = @CP_BAN,";
                selectQuery += "    VIL_BAN = @VIL_BAN,";
                selectQuery += "    NUMA_BAN = @NUMA_BAN,";
                selectQuery += "    NUMCPT_BAN = @NUMCPT_BAN,";
                selectQuery += "    NUMGUI_BAN = @NUMGUI_BAN,";
                selectQuery += "    TITULAIRE = @TITULAIRE,";
                selectQuery += "    NUMCLE_BAN = @NUMCLE_BAN,";
                selectQuery += "    CODEBIC = @CODEBIC,";
                selectQuery += "    CODEPAYS = @CODEPAYS,";
                selectQuery += "    journalComptable = @journalComptable,";
                selectQuery += "    CompteComptable = @CompteComptable,";
                selectQuery += "    NUM_NNE = @NUM_NNE";

                selectQuery += " where (cod_ban = @COD_BAN)";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@COD_BAN", bank.Code);
                command.Parameters.AddWithValue("@CODEBIC", bank.CodeBIC);
                command.Parameters.AddWithValue("@CODEPAYS", bank.CodePays);
                command.Parameters.AddWithValue("@LIB_BAN", bank.Libelle);
                command.Parameters.AddWithValue("@ADR1_BAN", bank.AddrBAN1);
                command.Parameters.AddWithValue("@ADR2_BAN", bank.AddrBAN2);
                command.Parameters.AddWithValue("@CP_BAN", bank.CodePostal);
                command.Parameters.AddWithValue("@VIL_BAN", bank.Ville);
                command.Parameters.AddWithValue("@NUMA_BAN", bank.NumA);
                command.Parameters.AddWithValue("@NUMCPT_BAN", bank.NumCPT);
                command.Parameters.AddWithValue("@NUMGUI_BAN", bank.NumGui);
                command.Parameters.AddWithValue("@TITULAIRE", bank.Titulaire);
                command.Parameters.AddWithValue("@NUMCLE_BAN", bank.NumCle);
                command.Parameters.AddWithValue("@NUM_NNE", bank.NumNNE);
                command.Parameters.AddWithValue("@journalComptable", bank.journalComptable == null ? DBNull.Value : (object)bank.journalComptable.NumJournal);
                command.Parameters.AddWithValue("@CompteComptable", bank.CompteComptable);

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
