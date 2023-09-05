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
        public static DataTable getMateriels()
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
                try
                {
                    //famille_Materiel
                    string selectQuery = "select materiels.*,rh_base_famillemateriel.*  from materiels";
                    selectQuery += " left join rh_base_famillemateriel on rh_base_famillemateriel.id= materiels.id_famille_materiel";
                    selectQuery += "  order by  rh_base_famillemateriel.ORDRE, materiels.ORDRE";


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
        }

        public static DataTable getFamillesMaterielsByIdMateriel(int id)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
                try
                {
                    //famille_Materiel
                    string selectQuery = "SELECT a.*";
                    selectQuery += " FROM rh_base_famillemateriel a ";
                    selectQuery += " left join materiels m on m.ID_FAMILLE_MATERIEL = a.ID ";
                    selectQuery += " where m.ID_MATERIEL = @id ";
                    selectQuery += " order by ordre";




                    MySqlCommand command = new MySqlCommand(selectQuery, connection);

                    command.Parameters.AddWithValue("@id", id);

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
        }

        public static DataTable getFamillesMateriels()
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
                try
                {
                    //famille_Materiel
                    string selectQuery = "select * from rh_base_famillemateriel ";

                    selectQuery += " order by ordre";
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
        }

        public static void AddFamille(FamillesMateriels p_famille)
        {

            lock (lockobj)
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
                    string selectQuery = "select MAX(ID)+1 as NEWID from rh_base_famillemateriel";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.CommandType = CommandType.Text;
                    object res = command.ExecuteScalar();
                    if (res is DBNull)
                        p_famille.Id = 1;
                    else
                        p_famille.Id = Convert.ToInt32(command.ExecuteScalar());

                    selectQuery = "insert into rh_base_famillemateriel (id, nom, couleur, parent, ordre) values (@id, @nom, @couleur, @parent, @ordre)";

                    command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@id", p_famille.Id);
                    command.Parameters.AddWithValue("@nom", p_famille.libelle);
                    command.Parameters.AddWithValue("@couleur", p_famille.couleur.ToArgb());
                    command.Parameters.AddWithValue("@parent", p_famille.ParentFamillesMaterielId);
                    command.Parameters.AddWithValue("@ordre", p_famille.ordre);

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

        public static void UpdateFamille(FamillesMateriels p_famille)
        {

            lock (lockobj)
            {
                if (p_famille.Id < 0) return;

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


                    string selectQuery = "update rh_base_famillemateriel set nom = @nom, couleur = @couleur, parent = @parent, ordre = @ordre where id=@id";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@id", p_famille.Id);
                    command.Parameters.AddWithValue("@nom", p_famille.libelle);
                    command.Parameters.AddWithValue("@couleur", p_famille.couleur.ToArgb());
                    command.Parameters.AddWithValue("@parent", p_famille.ParentFamillesMaterielId);
                    command.Parameters.AddWithValue("@ordre", p_famille.ordre);

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
        public static void ReorderFamille(FamillesMateriels p_famille, int NewPos)
        {

            lock (lockobj)
            {
                if (p_famille.Id < 0) return;

                if (NewPos > p_famille.ordre) NewPos++;

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


                    string selectQuery = "update rh_base_famillemateriel  set ordre = ordre+1 where ordre>=@order and parent = @parent";
                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@order", NewPos);
                    command.Parameters.AddWithValue("@parent", p_famille.ParentFamillesMaterielId);
                    command.ExecuteNonQuery();

                    selectQuery = "update rh_base_famillemateriel  set ordre = @order where Id=@Id";
                    command.CommandText = selectQuery;
                    command.Parameters.AddWithValue("@Id", p_famille.Id);
                    command.ExecuteNonQuery();

                    selectQuery = "SET GENERATOR rh_base_famillemateriel_ORDER TO 0";
                    command.CommandText = selectQuery;
                    command.ExecuteNonQuery();

                    selectQuery = "update rh_base_famillemateriel  set ordre = GEN_ID(rh_base_famillemateriel_ORDER, 1 )  where parent = @parent order by ordre";
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
        }

        public static void DelFamille(FamillesMateriels p_famille)
        {
            lock (lockobj)
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


                    string selectQuery = "delete from rh_base_famillemateriel where id=@id";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@id", p_famille.Id);
                    command.ExecuteNonQuery();


                    selectQuery = "update materiels set ID_FAMILLE_MATERIEL = null where (ID_FAMILLE_MATERIEL = @id)";
                    command.CommandText = selectQuery;
                    command.ExecuteNonQuery();

                    selectQuery = "update rh_base_famillemateriel set Parent = null where (Parent = @id)";
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
        }

        public static void UpdateFamilyMateriel(Materiel p_materiel, FamillesMateriels p_familleMateriel)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "update materiels set ID_FAMILLE_MATERIEL = @ID_FAMILLE_MATERIEL where (ID_MATERIEL = @ID_MATERIEL)";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@ID_FAMILLE_MATERIEL", p_familleMateriel.Id);
                    command.Parameters.AddWithValue("@ID_MATERIEL", p_materiel.id_materiel);
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

        public static void AddMateriel(Materiel p_materiel)
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
                string selectQuery = "select MAX(ID_MATERIEL)+1 as NEWID from materiels";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                object res = command.ExecuteScalar();
                if (res is DBNull)
                    p_materiel.id_materiel = 1;
                else
                    p_materiel.id_materiel = Convert.ToInt32(command.ExecuteScalar());

                selectQuery = @"insert into materiels (id_materiel, 
                                            materiel_libelle, 
                                            MATERIEL_COULEUR,
                                            ID_FAMILLE_MATERIEL, 
                                            PRIX_MATERIEL,
                                            SHORTLIB,
                                            MATERIEL_LIBELLE_ESTIMATION ,
                                            NOMENCLATURE,
                                            COEFFICIENT ,
                                            COTATION ,
                                            MATERIEL_LIBELLE_FACTURE,
QUANTITE_MATERIEL, code_barres)
                        values (@id_materiel, 
                                @materiel_libelle, 
                                @materiel_couleur, 
                                @idfammateriel, 
                                @prix_materiel,
                                @materiel_nom_court,
                                            @MATERIEL_LIBELLE_ESTIMATION ,
                                            @NOMENCLATURE,
                                            @COEFFICIENT ,
                                            @COTATION ,
                                            @MATERIEL_LIBELLE_FACTURE,
@QUANTITE_MATERIEL, @code_barres)";

                command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id_materiel", p_materiel.id_materiel);
                command.Parameters.AddWithValue("@materiel_libelle", p_materiel.materiel_libelle);
                command.Parameters.AddWithValue("@materiel_nom_court", p_materiel.materiel_shortlib);
                command.Parameters.AddWithValue("@materiel_couleur", System.Drawing.ColorTranslator.ToWin32(p_materiel.materiel_couleur));
                command.Parameters.AddWithValue("@prix_materiel", p_materiel.prix_materiel);
                command.Parameters.AddWithValue("@MATERIEL_LIBELLE_ESTIMATION", p_materiel.materiel_libelle_estimation);
                command.Parameters.AddWithValue("@NOMENCLATURE", p_materiel.nomenclature);
                command.Parameters.AddWithValue("@COEFFICIENT", p_materiel.coefficient);
                command.Parameters.AddWithValue("@COTATION", p_materiel.cotation);
                command.Parameters.AddWithValue("@MATERIEL_LIBELLE_FACTURE", p_materiel.materiel_libelle_facture);
                command.Parameters.AddWithValue("@QUANTITE_MATERIEL", p_materiel.Qte);
                command.Parameters.AddWithValue("@code_barres", p_materiel.codeBarres);

                if (p_materiel.famille_Materiel == null)
                    command.Parameters.AddWithValue("@idfammateriel", -1);
                else
                    command.Parameters.AddWithValue("@idfammateriel", p_materiel.famille_Materiel.Id);

                command.ExecuteNonQuery();
            }
            catch (System.SystemException ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                transaction.Commit();
                connection.Close();
            }

        }

        public static void UpdateMateriel(Materiel p_materiel)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = @" update materiels
                                    set MATERIEL_LIBELLE = @MATERIEL_LIBELLE,
                                        SHORTLIB = @MATERIEL_NOMCOURT,
                                        MATERIEL_COULEUR = @MATERIEL_COULEUR,
                                        ID_FAMILLE_MATERIEL = @ID_FAMILLE_MATERIEL,
                                        PRIX_MATERIEL= @PRIX_MATERIEL,
                                          MATERIEL_LIBELLE_ESTIMATION = @MATERIEL_LIBELLE_ESTIMATION ,
                                          NOMENCLATURE=  @NOMENCLATURE,
                                          COEFFICIENT =  @COEFFICIENT ,
                                           COTATION = @COTATION ,
                                          MATERIEL_LIBELLE_FACTURE=  @MATERIEL_LIBELLE_FACTURE,
                                            QUANTITE_MATERIEL = @QUANTITE_MATERIEL,
                                            
                                            code_barres = @code_barres
                                    where (ID_MATERIEL = @ID_MATERIEL)";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;



                command.Parameters.AddWithValue("@ID_MATERIEL", p_materiel.id_materiel);
                command.Parameters.AddWithValue("@MATERIEL_LIBELLE", p_materiel.materiel_libelle);
                command.Parameters.AddWithValue("@MATERIEL_NOMCOURT", p_materiel.materiel_shortlib);

                command.Parameters.AddWithValue("@MATERIEL_COULEUR", System.Drawing.ColorTranslator.ToWin32(p_materiel.materiel_couleur));
                command.Parameters.AddWithValue("@prix_materiel", p_materiel.prix_materiel);

                command.Parameters.AddWithValue("@MATERIEL_LIBELLE_ESTIMATION", p_materiel.materiel_libelle_estimation);
                command.Parameters.AddWithValue("@NOMENCLATURE", p_materiel.nomenclature);
                command.Parameters.AddWithValue("@COEFFICIENT", p_materiel.coefficient);
                command.Parameters.AddWithValue("@COTATION", p_materiel.cotation);
                command.Parameters.AddWithValue("@MATERIEL_LIBELLE_FACTURE", p_materiel.materiel_libelle_facture);
                command.Parameters.AddWithValue("@QUANTITE_MATERIEL", p_materiel.Qte);

                command.Parameters.AddWithValue("@code_barres", p_materiel.codeBarres);

                if (p_materiel.famille_Materiel == null)
                    command.Parameters.AddWithValue("@ID_FAMILLE_MATERIEL", -1);
                else
                    command.Parameters.AddWithValue("@ID_FAMILLE_MATERIEL", p_materiel.famille_Materiel.Id);


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

        public static void DeleteMateriel(Materiel p_materiel)
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
                string selectQuery = "delete from materiels where ID_MATERIEL=@id";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", p_materiel.id_materiel);
                command.ExecuteNonQuery();
            }
            catch (System.SystemException ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                transaction.Commit();
                connection.Close();

            }
        }

        public static Boolean SearchNameMateriel(string s_Materiel)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "select count(*) from materiels";
                    selectQuery += " where MATERIEL_LIBELLE=@MATERIEL_LIBELLE";
                    selectQuery += " Group by MATERIEL_LIBELLE";
                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                    command.Parameters.AddWithValue("@MATERIEL_LIBELLE", s_Materiel);
                    //command.ExecuteNonQuery();
                    int nb_acte = Convert.ToInt32(command.ExecuteScalar());
                    if (nb_acte > 0)
                    {
                        return false;
                    }
                    else
                        return true;

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

        //public static Boolean VerifRdv(Materiel p_materiel)
        //{
        //    lock (lockobj)
        //    {
        //        if (connection == null) getConnection();

        //        try
        //        {
        //            if (connection.State == ConnectionState.Closed) connection.Open();
        //        }
        //        catch (System.Exception e)
        //        {
        //            throw e;
        //        }

        //        MySqlTransaction transaction = connection.BeginTransaction();

        //        try
        //        {
        //            string selectQuery = "select count(*) from rendez_vous where id_acte=@id";

        //            MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
        //            command.Parameters.AddWithValue("@id", p_acte.id_acte);
        //            command.CommandType = CommandType.Text;

        //            int ret = Convert.ToInt32(command.ExecuteScalar());
        //            if (ret > 0)
        //                return false;
        //            else
        //                return true;
        //        }
        //        catch (System.SystemException ex)
        //        {
        //            transaction.Rollback();
        //            throw ex;
        //        }
        //        finally
        //        {
        //           connection.Close();
        //        }
        //    }
        //}
    }
}
