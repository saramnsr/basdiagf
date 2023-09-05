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

        public static DataRow getCaisseByIdPatient(int idpat)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {




                string selectQuery = "select";
                selectQuery += " id_caisse,";
                selectQuery += " caisse_nom,";
                selectQuery += " caisse_tel,";
                selectQuery += " iscmu,";
                selectQuery += " v.ville_nom,";
                selectQuery += " v.ville_cpostal,";
                selectQuery += " a.id_ville,";
                selectQuery += " a.id_adresse,";
                selectQuery += " a.adr_numero,";
                selectQuery += " a.adr_typevoie,";
                selectQuery += " a.adr_nomvoie,";
                selectQuery += " a.adr_complement";
                selectQuery += " from caisse  c";
                selectQuery += " LEFT OUTER JOIN ADRESSE a on c.id_adresse=a.id_adresse";
                selectQuery += " LEFT OUTER JOIN VILLE v ON v.id_ville=a.id_ville";

                selectQuery += " inner join lienpers on lienpers.id_personne=c.id_caisse and lienpers.relation = 'Ca'";

                selectQuery += " where lienpers.id_patient = @idpatient";



                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@idpatient", idpat);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];

                return (dt.Rows[0]);

            }
            catch (System.IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
               connection.Close();

            }

        }


        public static DataTable getCaisses()
        {
            List<Caisse> lst = new List<Caisse>();
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {




                string selectQuery = "select";
                selectQuery += " id_caisse,";
                selectQuery += " caisse_nom,";
                selectQuery += " caisse_tel,";
                selectQuery += " v.ville_nom,";
                selectQuery += " v.ville_cpostal,";
                selectQuery += " a.ID_Ville,";
                selectQuery += " ISCMU,";
                selectQuery += " a.ID_ADRESSE,";
                selectQuery += " a.adr_numero,";
                selectQuery += " a.adr_typevoie,";
                selectQuery += " a.adr_nomvoie,";
                selectQuery += " a.adr_complement,";
                selectQuery += " needorder";
                selectQuery += " from caisse  c";
                selectQuery += " LEFT OUTER JOIN adresse a on c.id_adresse=a.id_adresse";
                selectQuery += " LEFT OUTER JOIN ville v ON v.id_ville=a.id_ville";
                selectQuery += " order by caisse_nom desc";



                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

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
                throw e;
            }
            finally
            {
               connection.Close();

            }

        }

        public static void UpdateCaisse(Caisse p_caisse)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectedQuery = "update adresse ";
                selectedQuery += " set id_ville = @ville,";
                selectedQuery += " adr_nomvoie = @adr1,";
                selectedQuery += " adr_complement = @adr2";
                selectedQuery += " where (id_adresse = @id_adresse)";

                MySqlCommand command = new MySqlCommand(selectedQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@adr1", p_caisse.Adresse_Nom_Voie);
                command.Parameters.AddWithValue("@ville", p_caisse.IdVille);
                command.Parameters.AddWithValue("@adr2", p_caisse.Adresse2);
                command.Parameters.AddWithValue("@id_adresse", p_caisse.IdAdresse);
                command.ExecuteNonQuery();


                selectedQuery = "update caisse ";
                selectedQuery += " set caisse_nom = @caisse_nom,";
                selectedQuery += " caisse_tel = @caisse_tel,";
                selectedQuery += " ISCMU = @ISCMU, ";
                selectedQuery += " NeedOrder = @NeedOrder ";
                selectedQuery += " where (id_caisse = @id_caisse)";

                command.CommandText = selectedQuery;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@caisse_nom", p_caisse.Nom);
                command.Parameters.AddWithValue("@caisse_tel", p_caisse.TelFixe);
                command.Parameters.AddWithValue("@id_caisse", p_caisse.Id);
                command.Parameters.AddWithValue("@ISCMU", p_caisse.IsCMU);
                command.Parameters.AddWithValue("@NeedOrder", p_caisse.NeedOrder);
                
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


        public static int InsertCaisse(Caisse p_caisse)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select MAX(ID_CAISSE)+1 as NEWID from caisse";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                int id = 0;
                object o = command.ExecuteScalar();
                if (o == DBNull.Value)
                    id = 1;
                else
                    id = Convert.ToInt32(o);

                selectQuery = "select MAX(id_adresse)+1 as NEWID from adresse";

                command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                int idadresse = 0;
                o = command.ExecuteScalar();
                if (o == DBNull.Value)
                    idadresse = 1;
                else
                    idadresse = Convert.ToInt32(o);

                selectQuery = "select MAX(id_VILLE)+1 as NEWID from ville";

                command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                int idVille = 0;
                o = command.ExecuteScalar();
                if (o == DBNull.Value)
                    idVille = 1;
                else
                    idVille = Convert.ToInt32(o);

                if (p_caisse.IdVille == -1)
                {
                    try
                    {
                        selectQuery = "select id_ville as ID from ville where upper(ville_nom)='" + p_caisse.Ville.ToUpper() + "'";
                        command = new MySqlCommand(selectQuery, connection, transaction);
                        command.CommandType = CommandType.Text;
                        object ret = command.ExecuteScalar();
                        if (ret == DBNull.Value)
                            p_caisse.IdVille = 1;
                        else
                            p_caisse.IdVille = Convert.ToInt32(ret);
                    }
                    catch (System.Exception)
                    {
                        p_caisse.IdVille = -1;
                    }
                }




                selectQuery = "insert into  caisse (id_caisse,id_adresse,caisse_nom,caisse_tel,iscmu,needorder) values ";
                selectQuery += "(@ID_CAISSE,@id_adresse,@CAISSE_NOM,@CAISSE_TEL,@ISCMU,@NEEDORDER)";
                command.CommandText = selectQuery;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@ID_CAISSE", id);

                if (p_caisse.IdAdresse != -1)
                    command.Parameters.AddWithValue("@id_adresse", p_caisse.IdAdresse);
                else
                    command.Parameters.AddWithValue("@id_adresse", idadresse);

                command.Parameters.AddWithValue("@CAISSE_NOM", p_caisse.Nom);
                command.Parameters.AddWithValue("@CAISSE_TEL", p_caisse.TelFixe);

                command.Parameters.AddWithValue("@ISCMU", p_caisse.IsCMU);
                command.Parameters.AddWithValue("@NEEDORDER", p_caisse.NeedOrder);

                command.ExecuteNonQuery();

                if (p_caisse.IdAdresse == -1)
                {
                    selectQuery = "insert into  adresse (id_adresse,id_ville,adr_numero,adr_typevoie,adr_nomvoie,adr_complement) values ";
                    selectQuery += "(@ID_ADRESSEe,@ID_VILLE,@ADR_NUMERO,@ADR_TYPEVOIE,@ADR_NOMVOIE,@ADR_COMPLEMENT)";

                    command.Parameters.AddWithValue("@ID_ADRESSEe", idadresse);
                    if (p_caisse.IdVille != -1)
                        command.Parameters.AddWithValue("@ID_VILLE", p_caisse.IdVille);
                    else
                        command.Parameters.AddWithValue("@ID_VILLE", idVille);

                    command.Parameters.AddWithValue("@ADR_NUMERO", p_caisse.Adresse_Num);
                    command.Parameters.AddWithValue("@ADR_TYPEVOIE", p_caisse.Adresse_Type_Voie);
                    command.Parameters.AddWithValue("@ADR_NOMVOIE", p_caisse.Adresse_Nom_Voie);
                    command.Parameters.AddWithValue("@ADR_COMPLEMENT", p_caisse.Adresse2);
                    command.CommandText = selectQuery;
                    command.ExecuteNonQuery();
                }

                if (p_caisse.IdVille == -1)
                {
                    selectQuery = "insert into  ville (id_ville,ville_nom,ville_cpostal) values ";
                    selectQuery += "(@ID_VILLE,@VILLE_NOM,@VILLE_CPOSTAL)";

                    if (p_caisse.IdVille != -1)
                        command.Parameters.AddWithValue("@ID_VILLE", p_caisse.IdVille);
                    else
                        command.Parameters.AddWithValue("@ID_VILLE", idVille);

                    command.Parameters.AddWithValue("@VILLE_NOM", p_caisse.Ville);
                    command.Parameters.AddWithValue("@VILLE_CPOSTAL", p_caisse.CodePostal);
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
