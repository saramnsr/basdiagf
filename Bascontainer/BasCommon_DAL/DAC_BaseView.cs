using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using System.Configuration;
using BasCommon_BO;
using System.Drawing;
using System.IO;
using MySql.Data.MySqlClient;
using System.Net;
namespace BasCommon_DAL
{
    public static partial class DAC
    {

        public static void setPersonnesAContacter(basePatient patient)
        {

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuerydel = "delete from LIENPERS where id_patient=@id_patient and RELATION='Ac'";
                MySqlCommand commanddel = new MySqlCommand(selectQuerydel, connection, transaction);
                commanddel.Parameters.AddWithValue("@id_patient", patient.Id);
                commanddel.ExecuteNonQuery();

                string selectQuery = "insert into LIENPERS (ID_PERSONNE, ";
                selectQuery += "                               ID_PATIENT, ";
                selectQuery += "                               TYPELIEN, ";
                selectQuery += "                               RELATION) ";
                selectQuery += "values (@ID_PERSONNE, ";
                selectQuery += "        @ID_PATIENT, ";
                selectQuery += "        @TYPELIEN, ";
                selectQuery += "        @RELATION) ";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                foreach (LienCorrespondant p in patient.PersonnesAContacter)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@ID_PERSONNE", p.correspondant.Id);
                    command.Parameters.AddWithValue("@ID_PATIENT", patient.Id);
                    command.Parameters.AddWithValue("@TYPELIEN", p.LienLibelle);
                    command.Parameters.AddWithValue("@RELATION", p.TypeDeLien);


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



        public static string getOldRepertoireName(int Id)
        {

            if (connectionBaseView == null) getBaseViewConnection();

            if (connectionBaseView.State == ConnectionState.Closed) connectionBaseView.Open();
            //   MySqlTransaction transaction = connectionBaseView.BeginTransaction();
            try
            {


                string selectQuery = " select CHEMIN ";
                selectQuery += " from personne_classeur";
                selectQuery += " where id_orthalis=@id_patient_orthalis";

                MySqlCommand command = new MySqlCommand(selectQuery, connectionBaseView);

                command.Parameters.AddWithValue("@id_patient_orthalis", Id);

                return Convert.ToString(command.ExecuteScalar());


            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                connectionBaseView.Close();

            }
        }

        public static DataTable getAttributsByPatients(int idPatient)
        {

            if (connectionBaseView == null) getBaseViewConnection();

            if (connectionBaseView.State == ConnectionState.Closed) connectionBaseView.Open();
            try
            {


                string selectQuery = " select objet.PK_OBJET,pk_attribut, ";
                selectQuery += "       a.nom, ";
                selectQuery += "       a.typeattribut, ";
                selectQuery += "       a.groupe,";
                selectQuery += "       lnk_attributs_objets.Valeur,";
                selectQuery += "       lnk_attributs_objets.Valeur_date,";
                selectQuery += "       lnk_attributs_objets.valeur_bool,";
                selectQuery += "       lnk_attributs_objets.valeur_string";
                selectQuery += " from attributs a";
                selectQuery += " inner join lnk_attributs_objets on lnk_attributs_objets.ID_ATTRIBUT=a.pk_attribut";
                selectQuery += "  left join objet on objet.PK_OBJET = lnk_attributs_objets.ID_OBJET ";
                selectQuery += "  where objet.ID_PATIENT = @id";

                MySqlCommand command = new MySqlCommand(selectQuery, connectionBaseView);

                command.Parameters.AddWithValue("@id", idPatient);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                // transaction.Commit();

                DataTable dt = ds.Tables[0];

                return dt;


            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                connectionBaseView.Close();

            }
        }
        public static DataTable getAttribut(ObjImage img)
        {

            if (connectionBaseView == null) getBaseViewConnection();

            if (connectionBaseView.State == ConnectionState.Closed) connectionBaseView.Open();
            try
            {


                string selectQuery = " select pk_attribut, ";
                selectQuery += "       nom, ";
                selectQuery += "       typeattribut, ";
                selectQuery += "       groupe,";
                selectQuery += "       Valeur,";
                selectQuery += "       Valeur_date,";
                selectQuery += "       valeur_bool,";
                selectQuery += "       valeur_string";
                selectQuery += " from attributs";
                selectQuery += " inner join lnk_attributs_objets on lnk_attributs_objets.ID_ATTRIBUT=attributs.pk_attribut";
                selectQuery += " where lnk_attributs_objets.ID_OBJET = @ID_OBJET";

                MySqlCommand command = new MySqlCommand(selectQuery, connectionBaseView);

                command.Parameters.AddWithValue("@ID_OBJET", img.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                // transaction.Commit();

                DataTable dt = ds.Tables[0];

                return dt;


            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                connectionBaseView.Close();

            }
        }


        public static DataTable getObjectOf(basePatient p_Pat)
        {

            if (connectionBaseView == null) getBaseViewConnection();

            if (connectionBaseView.State == ConnectionState.Closed) connectionBaseView.Open();
            try
            {


                string selectQuery = " select pk_objet, ";
                selectQuery += "       nom, ";
                selectQuery += "       extension, ";
                selectQuery += "       width, ";
                selectQuery += "       height, ";
                selectQuery += "       taille, ";
                selectQuery += "       estidentite, ";
                selectQuery += "       datecreation, ";
                selectQuery += "       echelle, ";
                selectQuery += "       fichier, ";
                selectQuery += "       last_modif, ";
                selectQuery += "       rep_stockage, ";
                selectQuery += "       syncpath, ";
                selectQuery += "       dateinsertion, ";
                selectQuery += "       auteur, ";
                selectQuery += "       id_gabarit, ";
                selectQuery += "       id_patient_orthalis";
                selectQuery += " from objet";
                selectQuery += " where ID_PATIENT_ORTHALIS=@id_patient_orthalis";
                selectQuery += " order by DATECREATION";

                MySqlCommand command = new MySqlCommand(selectQuery, connectionBaseView);

                command.Parameters.AddWithValue("@id_patient_orthalis", p_Pat.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);


                DataTable dt = ds.Tables[0];

                return dt;


            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                connectionBaseView.Close();

            }
        }


        public static int ReadFully(string FullPath)
        {
            WebClient wc = new WebClient();
            wc.CachePolicy = new System.Net.Cache.RequestCachePolicy();
            Stream input = (Stream)wc.OpenRead(FullPath);

            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return (int)ms.Length;
            }
            //return 0;

        }


        public static int Insert(string nom,
                          string Repertoire,
                          DateTime DatePrise,
                          DateTime DateApercus,
                          int IdPatient,
                          Image Apercus,
                          string fullpath)
        {
            MySqlTransaction tr = null;
            connectionBaseView = null;
            try
            {


                if (connectionBaseView == null) getBaseViewConnection();

                if (connectionBaseView.State == ConnectionState.Closed) connectionBaseView.Open();
                tr = connectionBaseView.BeginTransaction();

                string query = "insert into objet (nom, extension,id_patient, id_patient_orthalis, datas, vignette, width, height, taille, estidentite, datecreation, echelle, fichier, last_modif,  syncpath, dateinsertion, auteur, id_gabarit, REP_STOCKAGE)";
                query += " values (@obj_nom, @extension, @id_patient,@id_patient_orthalis, null, @vignette, @width, @height, @taille, 0, @datecreation, 1, @fichier, @last_modif, '', @dateinsertion, '', 0, @REP_STOCKAGE)";


                MySqlCommand myCmd = new MySqlCommand(query, connectionBaseView, tr);
                myCmd.CommandType = CommandType.Text;

                myCmd.CommandText = query;


                byte[] bytes = imageToByteArray(Apercus);


                //System.IO.FileInfo nfo = new System.IO.FileInfo(fullpath);


                // myCmd.Parameters.AddWithValue("@id_object", id);

                myCmd.Parameters.AddWithValue("@obj_nom", nom);
                myCmd.Parameters.AddWithValue("@extension", System.IO.Path.GetExtension(fullpath));
                myCmd.Parameters.AddWithValue("@id_patient_orthalis", IdPatient);
                myCmd.Parameters.AddWithValue("@id_patient", IdPatient);
                myCmd.Parameters.Add("@vignette", MySqlDbType.Binary).Value = bytes;
                myCmd.Parameters.AddWithValue("@width", Apercus.Width);
                myCmd.Parameters.AddWithValue("@height", Apercus.Height);           
                myCmd.Parameters.AddWithValue("@taille", bytes.Length);
                myCmd.Parameters.AddWithValue("@datecreation", DateTime.Now);
                myCmd.Parameters.AddWithValue("@fichier", System.IO.Path.GetFileName(fullpath));
                myCmd.Parameters.AddWithValue("@last_modif", DateTime.Now);
                myCmd.Parameters.AddWithValue("@dateinsertion", DateTime.Now);
                myCmd.Parameters.AddWithValue("@REP_STOCKAGE", Repertoire);

                myCmd.ExecuteNonQuery();
                int ID = (int)myCmd.LastInsertedId;
                tr.Commit();

                return ID;

            }
            catch (System.Exception ex)
            {

                tr.Rollback();
                throw ex;
            }
            finally
            {
                connectionBaseView.Close();
            }
        }
        private static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        private static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;

        }
        public static string GetOldRepertoireFromKitView(int idPatient)
        {

            try
            {
                if (connectionBaseView == null) getBaseViewConnection();

                if (connectionBaseView.State == ConnectionState.Closed) connectionBaseView.Open();


                string query = "select chemin ";
                query += "from personne_classeur ";
                query += " WHERE personne.id_orthalis = @ID_PATIENT";


                MySqlCommand myCmd = new MySqlCommand(query, connectionBaseView);
                myCmd.CommandType = CommandType.Text;
                myCmd.Parameters.AddWithValue("@ID_PATIENT", idPatient);

                string res = Convert.ToString(myCmd.ExecuteScalar());

                return res;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                connectionBaseView.Close();
            }
        }

        public static DataTable SelectPatientById(int idPatient)
        {
            if (connection == null) getConnection();
            DataSet myDs = new DataSet();
            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();




                string query = "SELECT per_nom,per_prenom,per_datnaiss";
                query += " FROM personne_classeur ";
                query += " WHERE personne_classeur.TYPE_MATERIEL IS NULL AND personne_classeur.id_personne = @ID_PATIENT";


                MySqlCommand myCmd = new MySqlCommand(query, connection);
                myCmd.CommandType = CommandType.Text;
                myCmd.Parameters.AddWithValue("@ID_PATIENT", idPatient);

                MySqlDataAdapter myAdapter = new MySqlDataAdapter(myCmd);

                myAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                myAdapter.Fill(myDs);

                return myDs.Tables[0];

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
