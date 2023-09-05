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

        public static DataTable getMutuelles()
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {




                string selectQuery = "select";
                selectQuery += " ID_mutuelle,";
                selectQuery += " a.ID_adresse,";
                selectQuery += " mutuelle_NOM,";
                selectQuery += " mutuelle_TEL,";
                selectQuery += " NUM_mutuelle,";
                selectQuery += " NEEDORDER,";
                
                selectQuery += " ISCMU,";
                selectQuery += " ISTIERPAYANT,";
                selectQuery += " TAUXREMBPARDEFAUT,";
                selectQuery += " MONTANTPLAFOND,";
                
                selectQuery += " v.ville_nom,";
                selectQuery += " v.ville_cpostal,";
                selectQuery += " a.ID_Ville,";
                selectQuery += " ISCMU,";
                selectQuery += " a.ID_adresse,";
                selectQuery += " a.adr_numero,";
                selectQuery += " a.adr_typevoie,";
                selectQuery += " a.adr_nomvoie,";
                selectQuery += " a.adr_complement";

                selectQuery += " from mutuelle";
                selectQuery += " LEFT OUTER JOIN adresse a on mutuelle.id_adresse=a.id_adresse";
                selectQuery += " LEFT OUTER JOIN ville v ON v.id_ville=a.id_ville";



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];
                return dt;

            }
            catch (System.IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                return null;
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




        public static void UpdateMutuelle(Mutuelle p_mutuelle)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectedQuery = "update adresse ";
                selectedQuery += " set id_ville = (select first 1 ID_ville from ville where ville_NOM = @ville),";
                selectedQuery += " ADR_NUMERO = @numvoi,";
                selectedQuery += " adr_nomvoie = @adrnomvoie1,";
                selectedQuery += " ADR_TYPEVOIE = @ADR_TYPEVOIE,";
                selectedQuery += " adr_complement = @adr_complement";
                selectedQuery += " where (id_adresse = @id_adresse)";

                MySqlCommand command = new MySqlCommand(selectedQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@numvoi", p_mutuelle.Adresse_Num);
                command.Parameters.AddWithValue("@adrnomvoie1", p_mutuelle.Adresse_Nom_Voie);
                command.Parameters.AddWithValue("@adr_complement", p_mutuelle.Adresse2);
                command.Parameters.AddWithValue("@ADR_TYPEVOIE", p_mutuelle.Adresse_Type_Voie);
                command.Parameters.AddWithValue("@ville", p_mutuelle.Ville);
                command.Parameters.AddWithValue("@id_adresse", p_mutuelle.IdAdresse);
                command.ExecuteNonQuery();


                selectedQuery = "update mutuelle ";
                selectedQuery += " set mutuelle_nom = @mutuelle_nom,";
                selectedQuery += " mutuelle_tel = @mutuelle_tel,";
                selectedQuery += " ISCMU = @ISCMU,";
                selectedQuery += " ISTIERPAYANT = @ISTIERPAYANT,";

                selectedQuery += " MONTANTPLAFOND = @MONTANTPLAFOND,";
                selectedQuery += " TAUXREMBPARDEFAUT = @TAUXREMBPARDEFAUT,";
                selectedQuery += " NUM_mutuelle = @NUM_mutuelle,";
                selectedQuery += " NEEDORDER = @NEEDORDER";

                selectedQuery += " where (id_mutuelle = @id_mutuelle)";

                command.CommandText = selectedQuery;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@mutuelle_nom", p_mutuelle.Nom);
                command.Parameters.AddWithValue("@mutuelle_tel", p_mutuelle.Telephone);
                command.Parameters.AddWithValue("@id_mutuelle", p_mutuelle.Id);
                command.Parameters.AddWithValue("@ISCMU", p_mutuelle.IsCMU);


                command.Parameters.AddWithValue("@MONTANTPLAFOND", p_mutuelle.MontantPlafond);
                command.Parameters.AddWithValue("@ISTIERPAYANT", p_mutuelle.IsTiersPayant);
                command.Parameters.AddWithValue("@TAUXREMBPARDEFAUT", p_mutuelle.TauxParDefaut);
                command.Parameters.AddWithValue("@NUM_mutuelle", p_mutuelle.NumMutuelle);
                command.Parameters.AddWithValue("@NEEDORDER", p_mutuelle.NeedOrder);

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


        public static int InsertMutuelle(Mutuelle p_mutuelle)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select MAX(ID_mutuelle)+1 as NEWID from mutuelle";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                int id = 1;

                try
                {
                    id = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (System.Exception) { id = 1; }

                selectQuery = "select MAX(id_adresse)+1 as NEWID from adresse";

                command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                int idadresse = 1;
                try
                {
                     idadresse = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (System.Exception) { idadresse = 1; }
                
                selectQuery = "select MAX(id_ville)+1 as NEWID from ville";

                command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                int idVille = 1;
                try
                {

                 idVille = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (System.Exception) { idVille = 1; }

                if (p_mutuelle.IdVille == -1)
                {
                    try
                    {
                        selectQuery = "select id_ville as ID from ville where upper(ville_nom)='" + p_mutuelle.Ville.ToUpper() + "'";
                        command = new MySqlCommand(selectQuery, connection, transaction);
                        command.CommandType = CommandType.Text;
                        object ret = command.ExecuteScalar();
                        if (ret != null)
                            p_mutuelle.IdVille = Convert.ToInt32(ret);
                    }
                    catch (System.Exception)
                    {
                        p_mutuelle.IdVille = -1;
                    }
                }




                selectQuery = "insert into  mutuelle (id_mutuelle,id_adresse,mutuelle_nom,mutuelle_tel,iscmu, montantplafond,istierpayant,tauxrembpardefaut,num_mutuelle,needorder) values ";
                selectQuery += "(@ID_mutuelle,@id_adresse,@mutuelle_NOM,@mutuelle_TEL,@ISCMU, @MONTANTPLAFOND,@ISTIERPAYANT,@TAUXREMBPARDEFAUT,@NUM_mutuelle,@NEEDORDER)";
                command.CommandText = selectQuery;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@ID_mutuelle", id);
                if (p_mutuelle.IdAdresse != -1)
                    command.Parameters.AddWithValue("@id_adresse", p_mutuelle.IdAdresse);
                else
                    command.Parameters.AddWithValue("@id_adresse", idadresse);

                command.Parameters.AddWithValue("@mutuelle_NOM", p_mutuelle.Nom);
                command.Parameters.AddWithValue("@mutuelle_TEL", p_mutuelle.Telephone);

                command.Parameters.AddWithValue("@MONTANTPLAFOND", p_mutuelle.MontantPlafond);
                command.Parameters.AddWithValue("@ISTIERPAYANT", p_mutuelle.IsTiersPayant);
                command.Parameters.AddWithValue("@TAUXREMBPARDEFAUT", p_mutuelle.TauxParDefaut);
                command.Parameters.AddWithValue("@NUM_mutuelle", p_mutuelle.NumMutuelle);
                command.Parameters.AddWithValue("@NEEDORDER", p_mutuelle.NeedOrder);
                

                command.Parameters.AddWithValue("@ISCMU", p_mutuelle.IsCMU);

                command.ExecuteNonQuery();
                if (p_mutuelle.IdAdresse == -1)
                {
                    command.Parameters.Clear();

                    selectQuery = "insert into  adresse (id_adresse,id_ville,adr_numero,adr_typevoie,adr_nomvoie,adr_complement) values ";
                    selectQuery += "(@ID_adresse,@ID_ville,@ADR_NUMERO,@ADR_TYPEVOIE,@ADR_NOMVOIE,@ADR_COMPLEMENT)";

                    command.Parameters.AddWithValue("@ID_adresse", idadresse);
                    if (p_mutuelle.IdVille != -1)
                        command.Parameters.AddWithValue("@ID_ville", p_mutuelle.IdVille);
                    else
                        command.Parameters.AddWithValue("@ID_ville", idVille);
                    command.Parameters.AddWithValue("@ADR_NUMERO", p_mutuelle.Adresse_Num);
                    command.Parameters.AddWithValue("@ADR_TYPEVOIE", p_mutuelle.Adresse_Type_Voie);
                    command.Parameters.AddWithValue("@ADR_NOMVOIE", p_mutuelle.Adresse_Nom_Voie);
                    command.Parameters.AddWithValue("@ADR_COMPLEMENT", p_mutuelle.Adresse2);
                    command.CommandText = selectQuery;
                    command.ExecuteNonQuery();
                }

                if (p_mutuelle.IdVille == -1)
                {
                    command.Parameters.Clear();
                    selectQuery = "insert into  ville (id_ville,ville_nom,ville_cpostal) values ";
                    selectQuery += "(@ID_ville,@ville_NOM,@ville_CPOSTAL)";
                    command.Parameters.AddWithValue("@ID_ville", idVille);
                    command.Parameters.AddWithValue("@ville_NOM", p_mutuelle.Ville);
                    command.Parameters.AddWithValue("@ville_CPOSTAL", p_mutuelle.CodePostal);
                    command.CommandText = selectQuery;
                    command.ExecuteNonQuery();
                }

                transaction.Commit();
                return id;

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
