using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace BasCommon_DAL
{
    public static class DACExcel
    {

        private static string connectionString = "";


        #region connection

        public static void getExcelConnection()
        {
            //    If the connection string is null, use a default.


        }

        #endregion



        public static void CreateEncaissementTiers(string file)
        {
            try
            {
                System.Data.OleDb.OleDbConnection MyConnection;
                System.Data.OleDb.OleDbCommand myCommand = new System.Data.OleDb.OleDbCommand();
                MyConnection = new System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + file + "';Extended Properties=Excel 8.0;");
                MyConnection.Open();


                string createTableScript = "CREATE TABLE EncaissementTiers(";
                createTableScript += "[payeur] VARCHAR(255),";
                createTableScript += "[Montant] FLOAT,";
                createTableScript += "[patient] VARCHAR(255),";
                createTableScript += "[Entité] VARCHAR(255))";

                System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand(createTableScript, MyConnection);

                cmd.ExecuteNonQuery();




                MyConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        public static void CreateEncaissement(string file)
        {
            try
            {
                System.Data.OleDb.OleDbConnection MyConnection;
                System.Data.OleDb.OleDbCommand myCommand = new System.Data.OleDb.OleDbCommand();
                MyConnection = new System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + file + "';Extended Properties=Excel 8.0;");
                MyConnection.Open();


                string createTableScript = "CREATE TABLE Encaissement(";
                createTableScript += "[payeur] VARCHAR(255),";
                createTableScript += "[patient] VARCHAR(255),";
                createTableScript += "[Type] VARCHAR(255),";
                createTableScript += "[NumCheque] VARCHAR(255),";
                createTableScript += "[BanqueEmetrice] VARCHAR(255),";
                createTableScript += "[DateEncaissement] VARCHAR(255),";
                createTableScript += "[Montant] FLOAT,";
                createTableScript += "[Entité] VARCHAR(255),";
                createTableScript += "[BanqueDeRemise] VARCHAR(255))";


                System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand(createTableScript, MyConnection);

                cmd.ExecuteNonQuery();




                MyConnection.Close();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static void InsertEncaissement(object[] array, string file)
        {
            try
            {
                System.Data.OleDb.OleDbConnection MyConnection;
                System.Data.OleDb.OleDbCommand myCommand;

                MyConnection = new System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + file + "';Extended Properties=Excel 8.0;");
                MyConnection.Open();



                //sql = "Insert into [Feuil1$] (date,numDossier,nom,app,option,qté,réparation,tarifht,tva,totalttc) values(@date,@numDossier,@nom,@app,@option,@qté,@réparation,@tarifht,@tva,@totalttc)";
                string insertScript = "insert into Encaissement (";

                insertScript += "[payeur],";
                insertScript += "[patient],";
                insertScript += "[Type],";
                insertScript += "[NumCheque],";
                insertScript += "[BanqueEmetrice],";
                insertScript += "[DateEncaissement],";
                insertScript += "[Montant],";
                insertScript += "[Entité],";
                insertScript += "[BanqueDeRemise])";

                insertScript += "values (";

                insertScript += "@payeur,";
                insertScript += "@patient,";
                insertScript += "@Type,";
                insertScript += "@NumCheque,";
                insertScript += "@BanqueEmetrice,";
                insertScript += "@DateEncaissement,";
                insertScript += "@Montant,";
                insertScript += "@Entité,";
                insertScript += "@BanqueDeRemise)";

                myCommand = new System.Data.OleDb.OleDbCommand(insertScript, MyConnection);



                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@payeur", array[0]));
                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@patient", array[1]));
                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Type", array[2]));
                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@NumCheque", array[3]));
                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@BanqueEmetrice", array[4]));
                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@DateEncaissement", array[5]));
                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Montant", array[6]));
                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Entité", array[7]));
                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@BanqueDeRemise", array[8]));



                myCommand.ExecuteNonQuery();


                MyConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }


        public static void InsertEncaissementTiers(object[] array, string file)
        {
            try
            {
                System.Data.OleDb.OleDbConnection MyConnection;
                System.Data.OleDb.OleDbCommand myCommand;

                MyConnection = new System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + file + "';Extended Properties=Excel 8.0;");
                MyConnection.Open();



                //sql = "Insert into [Feuil1$] (date,numDossier,nom,app,option,qté,réparation,tarifht,tva,totalttc) values(@date,@numDossier,@nom,@app,@option,@qté,@réparation,@tarifht,@tva,@totalttc)";
                string insertScript = "insert into EncaissementTiers (";

                insertScript += "[payeur],";
                insertScript += "[Montant],";
                insertScript += "[patient],";
                insertScript += "[Entité])";

                insertScript += "values (";

                insertScript += "@payeur,";
                insertScript += "@Montant,";
                insertScript += "@patient,";
                insertScript += "@Entité)";

                myCommand = new System.Data.OleDb.OleDbCommand(insertScript, MyConnection);



                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@payeur", array[0]));
                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Montant", array[1]));
                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@patient", array[2]));
                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Entité", array[3]));



                myCommand.ExecuteNonQuery();


                MyConnection.Close();
            }
            catch (Exception)
            {
                throw;
            }

        }



        public static void CreateAremettreEnBanque(string file)
        {
            try
            {
                System.Data.OleDb.OleDbConnection MyConnection;
                System.Data.OleDb.OleDbCommand myCommand = new System.Data.OleDb.OleDbCommand();
                MyConnection = new System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + file + "';Extended Properties=Excel 8.0;");
                MyConnection.Open();


                string createTableScript = "CREATE TABLE AremettreEnBanque(";
                createTableScript += "[payeur] VARCHAR(255),";
                createTableScript += "[patients] VARCHAR(255),";
                createTableScript += "[type] VARCHAR(20),";
                createTableScript += "[numcheque] VARCHAR(100),";
                createTableScript += "[Banque du cheque] VARCHAR(20),";
                createTableScript += "[Date encaissement] DATETIME,";
                createTableScript += "[Date de remise] DATE,";
                createTableScript += "[Montant] FLOAT,";
                createTableScript += "[Entité] VARCHAR(255))";

                System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand(createTableScript, MyConnection);

                cmd.ExecuteNonQuery();




                MyConnection.Close();
            }
            catch (Exception)
            {
                throw;
            }

        }


        public static void InsertAremettreEnBanque(object[] array, string file)
        {
            try
            {
                System.Data.OleDb.OleDbConnection MyConnection;
                System.Data.OleDb.OleDbCommand myCommand;

                MyConnection = new System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + file + "';Extended Properties=Excel 8.0;");
                MyConnection.Open();



                //sql = "Insert into [Feuil1$] (date,numDossier,nom,app,option,qté,réparation,tarifht,tva,totalttc) values(@date,@numDossier,@nom,@app,@option,@qté,@réparation,@tarifht,@tva,@totalttc)";
                string insertScript = "insert into AremettreEnBanque (";

                insertScript += "[payeur],";
                insertScript += "[patients],";
                insertScript += "[type],";
                insertScript += "[numcheque],";
                insertScript += "[Banque du cheque],";
                insertScript += "[Date encaissement],";
                insertScript += "[Date de remise],";
                insertScript += "[Montant],";
                insertScript += "[Entité])";

                insertScript += "values (";

                insertScript += "@payeur,";
                insertScript += "@patients,";

                insertScript += "@type,";
                insertScript += "@numcheque,";
                insertScript += "@Banque,";
                insertScript += "@DateEnc,";
                insertScript += "@Date,";
                insertScript += "@Montant,";
                insertScript += "@Entité)";

                myCommand = new System.Data.OleDb.OleDbCommand(insertScript, MyConnection);



                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@payeur", array[0] == null ? "" : array[0]));
                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@patients", array[1] == null ? "" : array[1]));
                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@type", array[2] == null ? "" : array[2]));
                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@numcheque", array[3] == null ? "" : array[3]));

                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Banque", array[4] == null ? "" : array[4]));
                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@DateEnc", array[5] == null ? "" : array[5]));
                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Date", array[6] == null ? "" : array[6]));
                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Montant", array[7] == null ? "" : array[7]));
                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Entité", array[8] == null ? "" : array[8]));



                myCommand.ExecuteNonQuery();


                MyConnection.Close();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static void CreateAPrelever(string file)
        {
            try
            {
                System.Data.OleDb.OleDbConnection MyConnection;
                System.Data.OleDb.OleDbCommand myCommand = new System.Data.OleDb.OleDbCommand();
                MyConnection = new System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + file + "';Extended Properties=Excel 8.0;");
                MyConnection.Open();


                string createTableScript = "CREATE TABLE APrelever(";
                createTableScript += "[Patient] VARCHAR(255),";
                createTableScript += "[Montant] FLOAT,";
                createTableScript += "[Date] DATETIME,";
                createTableScript += "[Entité] VARCHAR(255))";

                System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand(createTableScript, MyConnection);

                cmd.ExecuteNonQuery();




                MyConnection.Close();
            }
            catch (Exception)
            {
                throw;
            }

        }


        public static void InsertAPrelever(object[] array, string file)
        {
            try
            {
                System.Data.OleDb.OleDbConnection MyConnection;
                System.Data.OleDb.OleDbCommand myCommand;

                MyConnection = new System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + file + "';Extended Properties=Excel 8.0;");
                MyConnection.Open();



                //sql = "Insert into [Feuil1$] (date,numDossier,nom,app,option,qté,réparation,tarifht,tva,totalttc) values(@date,@numDossier,@nom,@app,@option,@qté,@réparation,@tarifht,@tva,@totalttc)";
                string insertScript = "insert into APrelever (";
                insertScript += "[Patient],";
                insertScript += "[Montant],";
                insertScript += "[Date],";
                insertScript += "[Entité])";

                insertScript += "values (";

                insertScript += "@Patient,";
                insertScript += "@Montant,";
                insertScript += "@Date,";
                insertScript += "@Entité)";

                myCommand = new System.Data.OleDb.OleDbCommand(insertScript, MyConnection);



                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Patient", array[0]));
                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Montant", array[1]));
                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Date", array[2]));

                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Entité", array[3]));

                myCommand.ExecuteNonQuery();


                MyConnection.Close();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static void CreateRemisEnBanque(string file)
        {
            try
            {
                System.Data.OleDb.OleDbConnection MyConnection;
                System.Data.OleDb.OleDbCommand myCommand = new System.Data.OleDb.OleDbCommand();
                MyConnection = new System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + file + "';Extended Properties=Excel 8.0;");
                MyConnection.Open();


                string createTableScript = "CREATE TABLE RemisEnBanque(";
                createTableScript += "[payeur] VARCHAR(255),";
                createTableScript += "[patients] VARCHAR(255),";
                createTableScript += "[type] VARCHAR(20),";
                createTableScript += "[numcheque] VARCHAR(100),";
                createTableScript += "[Banque du cheque] VARCHAR(20),";
                createTableScript += "[Date d'encaissement] DATETIME,";
                createTableScript += "[Date de remise] DATE,";
                createTableScript += "[Montant] FLOAT,";
                createTableScript += "[Entité] VARCHAR(255),";
                createTableScript += "[BanqueRemis] VARCHAR(255))";

                System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand(createTableScript, MyConnection);

                cmd.ExecuteNonQuery();




                MyConnection.Close();
            }
            catch (Exception)
            {
                throw;
            }

        }


        public static void InsertRemisEnBanque(object[] array, string file)
        {
            try
            {
                System.Data.OleDb.OleDbConnection MyConnection;
                System.Data.OleDb.OleDbCommand myCommand;

                MyConnection = new System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + file + "';Extended Properties=Excel 8.0;");
                MyConnection.Open();



                //sql = "Insert into [Feuil1$] (date,numDossier,nom,app,option,qté,réparation,tarifht,tva,totalttc) values(@date,@numDossier,@nom,@app,@option,@qté,@réparation,@tarifht,@tva,@totalttc)";
                string insertScript = "insert into RemisEnBanque (";

                insertScript += "[payeur],";
                insertScript += "[patients],";
                insertScript += "[type],";
                insertScript += "[numcheque],";
                insertScript += "[Banque du cheque],";
                insertScript += "[Date d'encaissement],";
                insertScript += "[Date de remise],";
                insertScript += "[Montant],";
                insertScript += "[Entité],";
                insertScript += "[BanqueRemis])";

                insertScript += "values (";

                insertScript += "@payeur,";
                insertScript += "@patients,";
                insertScript += "@type,";
                insertScript += "@numcheque,";
                insertScript += "@Banque,";
                insertScript += "@DateEnc,";
                insertScript += "@Date,";
                insertScript += "@Montant,";
                insertScript += "@Entité,";
                insertScript += "@BanqueRemis)";

                myCommand = new System.Data.OleDb.OleDbCommand(insertScript, MyConnection);



                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@payeur", array[0] == null ? "" : array[0]));
                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@patients", array[1] == null ? "" : array[1]));
                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@type", array[2] == null ? "" : array[2]));
                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@numcheque", array[3] == null ? "" : array[3]));

                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Banque", array[4] == null ? "" : array[4]));
                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Date", array[5] == null ? "" : array[5]));
                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@DateEnc", array[6] == null ? "" : array[6]));
                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Montant", array[7] == null ? "" : array[7]));
                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Entité", array[8] == null ? "" : array[8]));
                myCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@BanqueRemis", array[9] == null ? "" : array[9]));



                myCommand.ExecuteNonQuery();


                MyConnection.Close();
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
