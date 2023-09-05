using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Drawing.Printing;
using System.Configuration;
using FirebirdSql.Data.FirebirdClient;
using BASEDiag_BO;
using BasCommon_BO;
using MySql.Data.MySqlClient;
using Microsoft.Win32;

namespace BASEDiag_DAL
{
    public class DAC
    {
        private static string connectionString = "";
        private static MySqlConnection connection  = null;

        #region connection

        private static MySqlConnection getLocalConnection()
        {
            //    If the connection string is null, use a default.

            if (connectionString == "")
            {
                MySqlConnectionStringBuilder cs = new MySqlConnectionStringBuilder();

                cs.Server = ConfigurationManager.AppSettings["DataSource" + prefix] ;
                cs.Database = ConfigurationManager.AppSettings["Database" + prefix];
                cs.UserID = ConfigurationManager.AppSettings["UserID" + prefix];
                cs.Password = ConfigurationManager.AppSettings["Password" + prefix];
                //cs.Dialect = Convert.ToInt32(ConfigurationManager.AppSettings["Dialect"]);
                //cs.Charset = "ISO8859_1";
                 cs.Port = Convert.ToUInt32(ConfigurationManager.AppSettings["PORT"]);
                connectionString = cs.ToString();
            }

            return new MySqlConnection(connectionString);
        }
        public static string PathRest
        {
            get
            {

                return System.Configuration.ConfigurationManager.AppSettings["PathRest" + prefix];

            }

        }
        public static string token
        {
            get
            {

                return System.Configuration.ConfigurationManager.AppSettings["token" + prefix];

            }
        }
        private static string _RegistryKey = "Software\\BASE\\BASEPractice";

        private static string _RegistryKeyPref = _RegistryKey + "\\Preferences";
        private static string _CurrentCabRegistryKey = _RegistryKeyPref + "\\CurrentCab";

        public static void GetCurrentCabOnRegistry()
        {

            RegistryKey key = Registry.CurrentUser.OpenSubKey(_CurrentCabRegistryKey);

            // If the return value is null, the key doesn't exist
            if (key == null) return;

            string objValidityDate = (string)key.GetValue("ValidityDate");
            string objValidityUser = (string)key.GetValue("ValidityCab");

            key.Close();

            DateTime ValidityDate;
            if (DateTime.TryParse(objValidityDate, out ValidityDate))
            {
                int idCabinet = Convert.ToInt32(objValidityUser);
                prefix = Cabinets.Find(c => c.Id == idCabinet).prefix;
            }
        }
        private static List<Cabinet> _lstCabinet;
        public static List<Cabinet> Cabinets
        {
            get
            {
                if (_lstCabinet == null)
                    _lstCabinet = getAllCabinets();
                return _lstCabinet;
            }
            set
            {
                _lstCabinet = value;
            }
        }
        public static List<Cabinet> getAllCabinets()
        {
            List<Cabinet> lst = new List<Cabinet>();
            string FILE_PATH = System.Configuration.ConfigurationManager.AppSettings["cabinets"];
            var xmlString = File.ReadAllText(FILE_PATH);
            var stringReader = new StringReader(xmlString);
            var dsSet = new DataSet();
            dsSet.ReadXml(stringReader);
            DataTable dt = dsSet.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Cabinet cab = new Cabinet();
                cab.Id = dr["Id"] is DBNull ? -1 : Convert.ToInt32(dr["Id"]);
                cab.nomCabinet = dr["nomcabinet"] is DBNull ? "" : Convert.ToString(dr["nomcabinet"]).Trim();
                cab.prefix = dr["prefix"] is DBNull ? "" : Convert.ToString(dr["prefix"]).Trim();
                lst.Add(cab);
            }

            return lst;
        }
        private static string _prefix = "";
        public static string prefix
        {
            get
            {
                if (_prefix == null || _prefix == "")
                    GetCurrentCabOnRegistry();
                return _prefix;
            }
            set
            {
                _prefix = "_" + value;
            }


        }
        private static void getConnection()
        {

            connection = getLocalConnection();
        }

        #endregion



        #region Entite Juridiques


        public static DataTable getEntitesJuridique()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                //famille_Acte
                string selectQuery = "select id, ";
                selectQuery += " nom, ";
                selectQuery += " adresse1, ";
                selectQuery += " adresse2, ";
                selectQuery += " codepostal, ";
                selectQuery += " ville";
                selectQuery += " from entite_juridique";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

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



        public static DataTable NbPatientsParEntite()
        {

            
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {



                    string selectQuery = " select count(patient.id_personne) nbPatient,u.id_entityjuridique  ";
                    selectQuery += "        from patient ";
                    selectQuery += "        left join basediag_infocomplementaire on basediag_infocomplementaire.idpatient = patient.id_personne ";
                    selectQuery += "        left join utilisateur u on u.id_personne =  basediag_infocomplementaire.praticien_resp";
                    selectQuery += "        group by u.id_entityjuridique";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

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



        #endregion

        public static DataTable getPlanTraitementsDEP()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {




                string selectQuery = "select";
                selectQuery += " ID_KEY,";
                selectQuery += " LIBELLE";
                selectQuery += " from PARAMTRAIT";



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


      

        

        public static void DeleteDevis(Devis devis)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {


                //Suppression des surveillances du devis
                string selectquery = "delete from base_surveillance";
                selectquery += " where base_surveillance.id_semestre in (";
                selectquery += " ";
                selectquery += " select Id from base_semestre";
                selectquery += " where base_semestre.id_traitement in (";
                selectquery += "    ";
                selectquery += " select id from base_plan_traitements";
                selectquery += " where base_plan_traitements.id_proposition in (";
                selectquery += "";
                selectquery += " select id from base_propositions";
                selectquery += " where base_propositions.iddevis in (";
                selectquery += "";
                selectquery += " select id from base_devis";
                selectquery += " where base_devis.id =  @iddevis";
                selectquery += "";
                selectquery += " )";
                selectquery += " )";
                selectquery += " )";
                selectquery += " )";

                MySqlCommand commandt = new MySqlCommand(selectquery, connection, transaction);

                commandt.Parameters.AddWithValue("@iddevis", devis.Id);


                commandt.ExecuteNonQuery();



                //Suppression des semestres
                selectquery = "delete from base_semestre";
                selectquery += " where base_semestre.id_traitement in (";
                selectquery += "    ";
                selectquery += " select id from base_plan_traitements";
                selectquery += " where base_plan_traitements.id_proposition in (";
                selectquery += " ";
                selectquery += " select id from base_propositions";
                selectquery += " where base_propositions.iddevis in (";
                selectquery += " ";
                selectquery += " select id from base_devis";
                selectquery += " where base_devis.id =  @iddevis";
                selectquery += " ";
                selectquery += " )";
                selectquery += " )";
                selectquery += " )";


                commandt.CommandText = selectquery;


                commandt.ExecuteNonQuery();




                //Suppression des traitements
                selectquery = "delete from base_plan_traitements";
                selectquery += " where base_plan_traitements.id_proposition in (";
                selectquery += " ";
                selectquery += " select id from base_propositions";
                selectquery += " where base_propositions.iddevis in (";
                selectquery += " ";
                selectquery += " select id from base_devis";
                selectquery += " where base_devis.id =  @iddevis";
                selectquery += " ";
                selectquery += " )";
                selectquery += " )";


                commandt.CommandText = selectquery;

                commandt.ExecuteNonQuery();




                //Suppression des propositions
                selectquery = "delete from base_propositions";
                selectquery += " where base_propositions.iddevis in (";
                selectquery += " ";
                selectquery += " select id from base_devis";
                selectquery += " where base_devis.id =  @iddevis";
                selectquery += " ";
                selectquery += " )";


                commandt.CommandText = selectquery;

                commandt.ExecuteNonQuery();


                //Suppression des propositions
                selectquery = "delete from BASE_DEVIS_ACTES";
                selectquery += " where id_devis = @iddevis";


                commandt.CommandText = selectquery;

                commandt.ExecuteNonQuery();



                //Suppression du devis
                selectquery = "delete from base_devis";
                selectquery += " where base_devis.id =  @iddevis";


                commandt.CommandText = selectquery;

                commandt.ExecuteNonQuery();

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


        public static void Insert_acte_propositions(ActePGPropose acte)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "select MAX(ID)+1 as NEWID from BASE_DEVIS_ACTES";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                object obj = command.ExecuteScalar();

                if (obj == DBNull.Value)
                    acte.Id = 1;
                else
                    acte.Id = Convert.ToInt32(obj);




                selectQuery = "insert into BASE_DEVIS_ACTES (";
                selectQuery += " id, ";
                selectQuery += " id_devis, ";
                selectQuery += " date_execution, ";
                selectQuery += " qte, ";
                selectQuery += " OPTIONAL, ";
                selectQuery += " Libelle, ";

                selectQuery += " montant, ";
                selectQuery += " id_template_acte_gestion)";
                selectQuery += " values (@id,";
                selectQuery += " @id_devis, ";
                selectQuery += " @date_execution, ";
                selectQuery += " @qte, ";
                selectQuery += " @OPTIONAL, ";
                selectQuery += " @Libelle, ";
                selectQuery += " @montant, ";
                selectQuery += " @id_template_acte_gestion)";

                command.CommandText = selectQuery;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", acte.Id);
                command.Parameters.AddWithValue("@id_devis", acte.IdDevis);
                command.Parameters.AddWithValue("@date_execution", acte.DateExecution == null ? DBNull.Value : (object)acte.DateExecution.Value);
                command.Parameters.AddWithValue("@qte", acte.Qte);
                command.Parameters.AddWithValue("@OPTIONAL", acte.Optionnel);
                command.Parameters.AddWithValue("@Libelle", acte.Libelle);
                command.Parameters.AddWithValue("@montant", acte.Montant);
                command.Parameters.AddWithValue("@id_template_acte_gestion", acte.IdTemplateActePG);

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


        public static DataTable get_acte_propositions(Devis devis)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select ID,ID_DEVIS, ";
                selectQuery += "       DATE_EXECUTION, ";
                selectQuery += "       QTE, ";
                selectQuery += "       OPTIONAL, ";
                selectQuery += "       MONTANT, ";
                selectQuery += "       LIBELLE, ";
                selectQuery += "       ID_TEMPLATE_ACTE_GESTION ";
                selectQuery += " from BASE_DEVIS_ACTES";
                selectQuery += " where ID_DEVIS=@ID_DEVIS";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@ID_DEVIS", devis.Id);


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


        public static bool IsTraitementEnCours(basePatient patient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {


                string selectQuery = "select FIRST 1 ID from BASE_TRAITEMENT";
                selectQuery += " where  ID_PATIENT = @Id and DATE_DEBUT>current_date";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.CommandText = selectQuery;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Id", patient.Id);
                object obj = command.ExecuteScalar();
                return (obj != null);



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

        public static void DeleteActePGAndEcheance(basePatient patient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "delete from BASE_ECHEANCE";
                selectQuery += " where  ID_TRAITEMENT in (SELECT ID FROM BASE_TRAITEMENT where  BASE_TRAITEMENT.ID_PATIENT = @Id and DATE_DEBUT>current_date)";
                


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@Id", patient.Id);
                command.ExecuteNonQuery();



                selectQuery = "delete from BASE_TRAITEMENT";
                selectQuery += " where  ID_PATIENT = @Id and DATE_DEBUT>current_date";


                command.CommandText = selectQuery;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Id", patient.Id);
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



        public static DataTable getSurveillances(Semestre semestre)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectquery = "select id, ";
                selectquery += "       ID_SEMESTRE, ";
                selectquery += "       ID_TRAITMNTSECU, ";
                selectquery += "       MONTANT, ";
                selectquery += "       DATEDEBUT, ";
                selectquery += "       DATEFIN ";
                selectquery += " from BASE_SURVEILLANCE";
                selectquery += " where BASE_SURVEILLANCE.ID_SEMESTRE = @id";


                MySqlCommand command = new MySqlCommand(selectquery, connection, transaction);
                command.Parameters.AddWithValue("@id", semestre.Id);

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


        public static int InsertSurveillance(Surveillance surv)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQueryId = "select max(id)+1 as ID from base_surveillance";
                MySqlCommand commandid = new MySqlCommand(selectQueryId, connection, transaction);
                object obj = commandid.ExecuteScalar();
                if (obj == DBNull.Value)
                    surv.Id = 1;
                else
                    surv.Id = Convert.ToInt32(obj);



                string selectQuery = "insert into base_surveillance (id, ";
                selectQuery += " id_semestre, ";
                selectQuery += " id_traitmntsecu, ";
                selectQuery += " montant, ";
                selectQuery += " datedebut, ";
                selectQuery += " datefin)";
                selectQuery += " values (@id, ";
                selectQuery += " @id_semestre, ";
                selectQuery += " @id_traitmntsecu, ";
                selectQuery += " @montant, ";
                selectQuery += " @datedebut, ";
                selectQuery += " @datefin)";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", surv.Id);
                command.Parameters.AddWithValue("@id_semestre", surv.Semestre.Id);
                command.Parameters.AddWithValue("@id_traitmntsecu", surv.traitementSecu.Id);
                command.Parameters.AddWithValue("@montant", surv.Montant_Honoraire);
                command.Parameters.AddWithValue("@datedebut", surv.DateDebut);
                command.Parameters.AddWithValue("@datefin", surv.DateFin);


                object oj = command.ExecuteScalar();
                if (oj != null) return Convert.ToInt32(oj);


                transaction.Commit();

                return -1;

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


        public static void DeleteSurveillance(Surveillance surv)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "delete  ";
                selectQuery += " from base_surveillance";
                selectQuery += " where ID = @ID ";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@ID", surv.Id);


                object obj = command.ExecuteNonQuery();

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


        public static DataTable getAppareilsFromObjectifs(List<CommonObjectif> objs)
        {
            if (connection == null) getConnection();

            if ((objs == null) || (objs.Count == 0)) return null;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string Idobjs = "";

                foreach (CommonObjectif d in objs)
                {
                    if (d == null) continue;
                    if (Idobjs != "") Idobjs += ",";
                    Idobjs += d.Id.ToString();
                }

                string selectQuery = "select ID_OBJECTIF, ";
                selectQuery += "              ID_APPAREIL, ";
                selectQuery += "              DESCRIPTION ";
                selectQuery += "       from BAS_COMMONOBJECTIF_APPAREIL";

                selectQuery += " where ID_OBJECTIF in (" + Idobjs + ")";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

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

        public static void InsertPropositions(Proposition prop)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQueryId = "select max(id)+1 as ID from base_propositions";
                MySqlCommand commandid = new MySqlCommand(selectQueryId, connection, transaction);
                object obj = commandid.ExecuteScalar();
                if (obj == DBNull.Value)
                    prop.Id = 1;
                else
                    prop.Id = Convert.ToInt32(obj);





                string selectQuery = "insert into base_propositions (id, ";
                selectQuery += "                               etat, ";
                selectQuery += "                               dateevent, ";
                selectQuery += "                               risques, ";
                selectQuery += "                               iddevis, ";
                selectQuery += "                               id_patient, ";
                selectQuery += "                               DATE_PROPOSITION, ";
                selectQuery += "                               libelle, ";
                selectQuery += "                               date_acceptation)";
                selectQuery += "values (@id, ";
                selectQuery += "        @etat, ";
                selectQuery += "        @dateevent, ";
                selectQuery += "        @risques, ";
                selectQuery += "        @devis, ";
                selectQuery += "        @id_patient, ";
                selectQuery += "        @DATE_PROPOSITION, ";
                selectQuery += "        @libelle, ";
                selectQuery += "        @date_acceptation)";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);


                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", prop.Id);
                command.Parameters.AddWithValue("@etat", prop.Etat);
                command.Parameters.AddWithValue("@dateevent", prop.DateEvenement);
                command.Parameters.AddWithValue("@risques", "");
                command.Parameters.AddWithValue("@devis", prop.IdDevis);
                command.Parameters.AddWithValue("@id_patient", prop.IdPatient);
                command.Parameters.AddWithValue("@DATE_PROPOSITION", prop.DateProposition);
                command.Parameters.AddWithValue("@libelle", prop.libelle);
                command.Parameters.AddWithValue("@date_acceptation", prop.DateAcceptation == null ? (object)DBNull.Value : prop.DateAcceptation.Value);


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


        public static DataTable getObjectifsFromDiagnostics(List<CommonDiagnostic> diags)
        {
            if (connection == null) getConnection();

            if ((diags==null)||(diags.Count == 0)) return null;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string IdDiags = "";

                foreach (CommonDiagnostic d in diags)
                {
                    if (d != null)
                    {
                        if (IdDiags != "") IdDiags += ",";
                        IdDiags += d.Id.ToString();
                    }
                }

                string selectQuery = "select ID_DIAG, ";
                selectQuery += "              ID_OBJ, ";
                selectQuery += "              INVISALIGNTXT, ";
                selectQuery += "              NUM_DEVIS, ";
                selectQuery += "              DiagCanceled, ";                
                selectQuery += "              NUM_OPTION, ";
                selectQuery += "              DISPLAYORDER, ";
                selectQuery += "              NumDiag, ";
                selectQuery += "              SPECIALINSTRUCTIONS, ";
                selectQuery += "              DESCRIPTION ";
                selectQuery += "       from bas_commondiag_commonobj";
                
                selectQuery += " where ID_DIAG in (" + IdDiags+")";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

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

        public static DataTable getObjectifsFromDiagnostics(CommonDiagnostic diag)
        {
            if (connection == null) getConnection();

            if (diag == null) return null;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string IdDiags = "";

                
                string selectQuery = "select ID_DIAG, ";
                selectQuery += "              ID_OBJ, ";
                selectQuery += "              INVISALIGNTXT, ";
                selectQuery += "              NUM_DEVIS, ";
                selectQuery += "              NUM_OPTION, ";
                selectQuery += "              DISPLAYORDER, ";
                selectQuery += "              DiagCanceled, ";
                selectQuery += "              NumDiag, ";
                selectQuery += "              SPECIALINSTRUCTIONS, ";
                selectQuery += "              DESCRIPTION ";
                selectQuery += "       from bas_commondiag_commonobj";

                selectQuery += " where ID_DIAG = @id";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.Add("@id", diag.Id);

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
        

        public static DataTable getAppareils()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "              libelle, ";
                selectQuery += "              infodep, ";
                selectQuery += "              risques, ";
                selectQuery += "              code";
                selectQuery += "       from base_acte_appareil";
                selectQuery += " order by  libelle";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

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
        

        public static DataTable get_acte_gestion()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "       code, ";
                selectQuery += "       libelle, ";
                selectQuery += "       code_prestation, ";
                selectQuery += "       acte_coeff, ";
                selectQuery += "       decompisvisible, ";
                selectQuery += "       decomp, ";
                selectQuery += "       valeur, ";
                selectQuery += "       need_dep, ";
                selectQuery += "       need_fse, ";
                selectQuery += "       nb_jours, ";
                selectQuery += "       nb_mois,";
                selectQuery += "       phase,";
                selectQuery += "       ID_ACTE_ORTHALIS";
                selectQuery += " from base_acte_gestion";
                selectQuery += " order by  libelle";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

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
        

        public static DataTable GetCodesPresta()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = " select ID_PRESTATION, ";
                selectQuery += "        LIBELLE, ";
                selectQuery += "        VALEUR_CLE_EURO ";
                selectQuery += " from CODE_PRESTATION";
                selectQuery += " order by  ID_PRESTATION";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

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


        
        public static DataTable getFauteuils()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id_fauteuil, faut_libelle  from fauteuil order by faut_libelle";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

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
        
        public static DataRow getPatient(int Id)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "select first 1 personne.id_personne,";
                //selectQuery += " personne.id_adresse,"; 
                //selectQuery += " personne.id_util,"; 
                //selectQuery += " personne.id_caisse,"; 
                //selectQuery += " personne.adr_id_adresse,"; 
                selectQuery += " personne.per_nom,";
                selectQuery += " personne.per_nomjf,";
                selectQuery += " personne.per_prenom";


                selectQuery +=", personne.per_genre,";
                selectQuery += " personne.per_secu,";
                selectQuery += " personne.per_type,";
                //selectQuery += " personne.per_telprinc,";
                //selectQuery += " personne.per_teltrav1,";
                //selectQuery += " personne.per_teltrav2,";
                //selectQuery += " personne.per_telecopie,";
                //selectQuery += " personne.per_email,";
                // selectQuery += " personne.per_reception,"; 
                selectQuery += " personne.per_notes,";
                //selectQuery += " personne.per_poste,"; 
                selectQuery += " personne.pcom,";
                //selectQuery += " personne.per_adr1,";
                //selectQuery += " personne.per_adr2,";
                //selectQuery += " personne.per_ville,";
                //selectQuery += " personne.per_cpostal,";
                //selectQuery += " personne.per_adr1_prof,";
                //selectQuery += " personne.per_adr2_prof,";
                //selectQuery += " personne.per_cpostal_prof,";
                //selectQuery += " personne.per_ville_prof,";
                selectQuery += " personne.profession,";
                // selectQuery += " personne.mutuelle,"; 
                selectQuery += " personne.per_datnaiss,";
                selectQuery += " personne.tuvous,";
                //selectQuery += " personne.poid,"; 
                //selectQuery += " personne.email2,"; 
                //selectQuery += " personne.gsm,"; 
                //selectQuery += " personne.icq,"; 
                //selectQuery += " personne.im1,"; 
                // selectQuery += " personne.im2,"; 
                //selectQuery += " personne.lastmodif,"; 
                // selectQuery += " personne.telsup0,"; 
                // selectQuery += " personne.telsup3,"; 
                // selectQuery += " personne.telsup4,"; 
                // selectQuery += " personne.telsup5,"; 
                // selectQuery += " personne.telsup6,"; 
                //selectQuery += " personne.telsup8,"; 
                // selectQuery += " personne.telsup10,"; 
                //selectQuery += " personne.telsup11,"; 
                //selectQuery += " personne.telsup12,"; 
                //selectQuery += " personne.telsup13,"; 
                //selectQuery += " personne.telsup14,"; 
                //selectQuery += " personne.telsup15,"; 
                //selectQuery += " personne.telsup16,"; 
                //selectQuery += " personne.telsup17,"; 
                //selectQuery += " personne.telsup18,"; 
                //selectQuery += " personne.indicetel1,"; 
                //selectQuery += " personne.indicetel2,"; 
                //selectQuery += " personne.indicetel3,"; 
                //selectQuery += " personne.indicetel4,"; 
                //selectQuery += " personne.email3,"; 
                //selectQuery += " personne.indiceemail,"; 
                //selectQuery += " personne.indiceadr,"; 
                //selectQuery += " personne.pays_dom,";
                //selectQuery += " personne.pays_trav,";
                selectQuery += " personne.pers_titre,";
                //selectQuery += " personne.pers_siteweb,"; 
                //selectQuery += " personne.per_ville_naissance,"; 
                selectQuery += " personne.per_pays_naissance,";
                //selectQuery += " personne.per_langue_parle,"; 
                //selectQuery += " personne.per_population_ref,"; 
                //selectQuery += " personne.nom_rep_image,"; 
                //selectQuery += "        personne.oi_login,"; 
                //selectQuery += "        personne.oi_mdp,"; 
                //selectQuery += "        personne.oi_profil,"; 
                //selectQuery += "        personne.oi_autorisation,"; 
                selectQuery += "        personne.categories,";
                selectQuery += "        personne.pref_com,";
                selectQuery += " PAT_DIAG,";
                selectQuery += " PAT_PLAN,";
                selectQuery += " PAT_OBJECTIF_TRAIT,";
                selectQuery += " PAT_SOLDE,";                
                selectQuery += " p.ALLERGIE,";
                selectQuery += " p.StatusClinique,";
                selectQuery += " p.NUM_MOULAGE,";
                selectQuery += " p.DATEABANDON,";
                selectQuery += " p.PAT_APPAREIL,";
                selectQuery += " Q1CS.RESUME,";
                selectQuery += " Lastrdv.rdv_date as LastRDV,";
                selectQuery += " Nextrdv.rdv_date as NextRDV,";


                selectQuery += " p.PAT_NUMDOSSIER";


                selectQuery += " from personne";
                selectQuery += " inner join patient p on personne.id_personne=p.id_personne";
                selectQuery += " left outer join Q1CS on \"USER\"=p.id_personne";
                selectQuery += " left outer join rendez_vous Lastrdv on Lastrdv.id_personne = personne.id_personne and Lastrdv.rdv_date<current_date";
                selectQuery += " left outer join rendez_vous Nextrdv on Nextrdv.id_personne = personne.id_personne and Nextrdv.rdv_date>current_date";

                selectQuery += " where   personne.TYPE_MATERIEL IS NULL AND personne.id_personne=@Id ";
                selectQuery += "order by Lastrdv.rdv_date desc,Nextrdv.rdv_date asc";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@Id", Id);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                return dt.Rows[0];

            }
            catch (System.IndexOutOfRangeException )
            {
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

        public static DataRow getAssure(basePatient pat)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "SELECT PERSONNE.ID_PERSONNE,";
                selectQuery += "PER_NOM,";
                selectQuery += "PER_PRENOM,";
                selectQuery += "PER_EMAIL,";
                selectQuery += "PER_TELPRINC,";
                selectQuery += "PER_TELTRAV1,";
                selectQuery += "PER_GENRE,";
                selectQuery += "PER_TELECOPIE,";
                selectQuery += "PER_ADR1_prof,";
                selectQuery += "PER_ADR2_prof,";
                selectQuery += "PER_VILLE_prof,";
                selectQuery += "PER_CPOSTAL_prof,";
                selectQuery += "PER_NOTES,";
                selectQuery += "PERS_TITRE,";
                selectQuery += "TUVOUS,";
                selectQuery += "PREF_COM,";
                selectQuery += "CATEGORIES,";
                selectQuery += "lnk.RELATION, ";
                selectQuery += "lnk.TYPELIEN, ";
                selectQuery += "lnk.ID_PATIENT, ";
                selectQuery += "TYPE_PERS.NOM as PROFESSION, ";
                selectQuery += "TYPE_PERS.ID_TYPE as TYPE ";
                selectQuery += "FROM PERSONNE ";
                selectQuery += "INNER JOIN TYPE_PERS on TYPE_PERS.ID_TYPE=PERSONNE.PER_TYPE ";
                selectQuery += "INNER JOIN lienpers lnk on lnk.ID_PERSONNE=PERSONNE.ID_PERSONNE ";

                selectQuery += " Where  personne.TYPE_MATERIEL IS NULL AND lnk.id_patient = @Id and lnk.relation = 'As'";
                selectQuery += " order by PER_NOM,PER_PRENOM";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@Id", pat.Id);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                return dt.Rows[0];

            }
            catch (System.IndexOutOfRangeException )
            {
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

        public static DataTable getCategories()
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "SELECT CATEG, ";
                selectQuery += " ID_CATEG ";
                selectQuery += " FROM CATEGORIES ";
                selectQuery += " Order by CATEG";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();



                return ds.Tables[0];



            }
            catch (System.IndexOutOfRangeException )
            {
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
        
        public static DataTable getPraticiens()
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {




                string selectQuery = "select PERSONNE.ID_PERSONNE,per_nom, per_prenom from PERSONNE";
                selectQuery += " INNER JOIN UTILISATEUR on PERSONNE.ID_PERSONNE = utilisateur.ID_PERSONNE";
                selectQuery += " where  personne.TYPE_MATERIEL IS NULL AND UTIL_TYPE=4 and UTIL_ACTIF='Y'";
                selectQuery += " order by PER_NOM,PER_PRENOM";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();


                return ds.Tables[0];



            }
            catch (System.IndexOutOfRangeException )
            {
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
        
        public static DataTable getSmallCorrespondants(string search)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {




                string selectQuery = "SELECT PERSONNE.ID_PERSONNE,";
                selectQuery += "PER_NOM,";
                selectQuery += "PER_PRENOM ";
                selectQuery += "FROM PERSONNE ";
                selectQuery += "INNER JOIN TYPE_PERS on TYPE_PERS.ID_TYPE=PERSONNE.PER_TYPE ";
                selectQuery += "Where  personne.TYPE_MATERIEL IS NULL AND upper(PER_NOM) LIKE '" + search.ToUpper() + "%'";
                selectQuery += "or upper(PER_PRENOM) LIKE '" + search.ToUpper() + "%'";
                selectQuery += "or upper(PER_VILLE) LIKE '" + search.ToUpper() + "%'";
                selectQuery += "or upper(PER_CPOSTAL) LIKE '" + search.ToUpper() + "%'";
                selectQuery += "or upper(TYPE_PERS.NOM) LIKE '" + search.ToUpper() + "%'";
                
                selectQuery += " order by PER_NOM,PER_PRENOM";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();


                return ds.Tables[0];



            }
            catch (System.IndexOutOfRangeException )
            {
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

        public static DataTable getVillesSugested()
        {

            MySqlConnection localconnection = getLocalConnection();

            localconnection.Open();
            MySqlTransaction transaction = localconnection.BeginTransaction();
            try
            {




                string selectQuery = "SELECT ID_VILLE,";
                selectQuery += " VILLE_NOM,";
                selectQuery += " VILLE_CPOSTAL";
                selectQuery += " FROM VILLE";
                selectQuery += " order by VILLE_NOM";


                MySqlCommand command = new MySqlCommand(selectQuery, localconnection, transaction);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                return ds.Tables[0];




            }

            catch (System.Exception e)
            {
                transaction.Rollback();
                throw e;
            }
            finally
            {
                localconnection.Close();

            }





        }

        #region TypePers


        public static DataTable getAllTypePerss()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select ";
                selectQuery += " ID_TYPE,";
                selectQuery += " NOM";
                selectQuery += " from TYPE_PERS p";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

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





        #endregion




        
        public static DataRow getCorrespondant(int id)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {




                string selectQuery = "SELECT PERSONNE.ID_PERSONNE,";
                selectQuery += "PER_NOM,";
                selectQuery += "PER_PRENOM,";
                selectQuery += "PER_EMAIL,";
                selectQuery += "PER_TELPRINC,";
                selectQuery += "PER_TELTRAV1,";
                selectQuery += "PER_GENRE,";
                selectQuery += "PER_TELECOPIE,";
                selectQuery += "PER_ADR1_prof,";
                selectQuery += "PER_ADR2_prof,";
                selectQuery += "PER_VILLE_prof,";
                selectQuery += "PER_CPOSTAL_prof,";
                selectQuery += "PER_ADR1,";
                selectQuery += "PER_ADR2,";
                selectQuery += "PER_VILLE,";
                selectQuery += "PER_CPOSTAL,";
                selectQuery += "PER_NOTES,";
                selectQuery += "PERS_TITRE,";
                selectQuery += "per_secu,";
                selectQuery += "TUVOUS,";
                selectQuery += "OI_LOGIN,";
                selectQuery += "OI_MDP,";
                selectQuery += "PREF_COM,";
                selectQuery += "CATEGORIES,";
                selectQuery += "TYPE_PERS.NOM as PROFESSION, ";
                selectQuery += "TYPE_PERS.ID_TYPE as TYPE ";
                selectQuery += "FROM PERSONNE ";
                selectQuery += "INNER JOIN TYPE_PERS on TYPE_PERS.ID_TYPE=PERSONNE.PER_TYPE ";
                selectQuery += "Where  personne.TYPE_MATERIEL IS NULL AND ID_PERSONNE = @Id";



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();


                return ds.Tables[0].Rows[0] ;



            }
            catch (System.IndexOutOfRangeException )
            {
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

        

        public static DataTable getCorrespondantsSugestedByProfNParam(string Profession, string param)
        {

            MySqlConnection localconnection = getLocalConnection();

            localconnection.Open();
            MySqlTransaction transaction = localconnection.BeginTransaction();
            try
            {


                string selectQuery = "SELECT PERSONNE.ID_PERSONNE,PER_VILLE_PROF, PER_NOM, PER_PRENOM,LOWER(PROFESSION),LOWER(TYPE_PERS.NOM)";
                selectQuery += " FROM PERSONNE";
                selectQuery += " INNER JOIN TYPE_PERS ON TYPE_PERS.ID_TYPE =  PERSONNE.PER_TYPE";
                selectQuery += " where  personne.TYPE_MATERIEL IS NULL AND CHAR_LENGTH(TRIM(PER_NOM))>3 and (LOWER(PROFESSION) LIKE '%" + Profession.ToLower() + "%' or";
                if (Profession!=null) selectQuery += " LOWER(TYPE_PERS.NOM) LIKE '%" + Profession.ToLower() + "%')";
                if (param != null)
                {
                    selectQuery += " AND (UPPER(PER_NOM) LIKE '" + param.ToUpper() + "%'";
                    selectQuery += " OR UPPER(PER_PRENOM) LIKE '" + param.ToUpper() + "%'";
                    selectQuery += " OR UPPER(PER_VILLE_PROF) LIKE '" + param.ToUpper() + "%')";
                }
                selectQuery += " order by PER_NOM,PER_PRENOM";


                


                MySqlCommand command = new MySqlCommand(selectQuery, localconnection, transaction);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                return ds.Tables[0];




            }

            catch (System.Exception e)
            {
                transaction.Rollback();
                throw e;
            }
            finally
            {
                localconnection.Close();

            }





        }


        

        public static DataTable getCorrespondantsSugestedByProf(string Profession)
        {

            MySqlConnection localconnection = getLocalConnection();

            localconnection.Open();
            MySqlTransaction transaction = localconnection.BeginTransaction();
            try
            {


                string selectQuery = "SELECT PERSONNE.ID_PERSONNE,PER_VILLE_PROF, PER_NOM, PER_PRENOM,LOWER(PROFESSION),LOWER(TYPE_PERS.NOM)";
                selectQuery += " FROM PERSONNE";
                selectQuery += " INNER JOIN TYPE_PERS ON TYPE_PERS.ID_TYPE =  PERSONNE.PER_TYPE";
                selectQuery += " where  personne.TYPE_MATERIEL IS NULL AND CHAR_LENGTH(TRIM(PER_NOM))>3 and (LOWER(PROFESSION) LIKE '%" + Profession.ToLower() + "%' or";
                selectQuery += " LOWER(TYPE_PERS.NOM) LIKE '%" + Profession.ToLower() + "%')";
                selectQuery += " order by PER_NOM,PER_PRENOM";


                


                MySqlCommand command = new MySqlCommand(selectQuery, localconnection, transaction);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                return ds.Tables[0];




            }

            catch (System.Exception e)
            {
                transaction.Rollback();
                throw e;
            }
            finally
            {
                localconnection.Close();

            }





        }


        public static DataTable getCorrespondantsSugested()
        {

            MySqlConnection localconnection = getLocalConnection();

            localconnection.Open();
            MySqlTransaction transaction = localconnection.BeginTransaction();
            try
            {




                string selectQuery = "SELECT PERSONNE.ID_PERSONNE,";
                selectQuery += " PER_NOM,";
                selectQuery += " PER_VILLE_PROF,";
                selectQuery += " PER_PRENOM";
                selectQuery += " FROM PERSONNE ";
                selectQuery += " where  personne.TYPE_MATERIEL IS NULL AND CHAR_LENGTH(TRIM(PER_NOM))>3";
                selectQuery += " order by PER_NOM,PER_PRENOM";


                MySqlCommand command = new MySqlCommand(selectQuery, localconnection, transaction);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                return ds.Tables[0];




            }

            catch (System.Exception e)
            {
                transaction.Rollback();
                throw e;
            }
            finally
            {
                localconnection.Close();

            }





        }

        public static DataTable getPersonnesAContacter(basePatient patient)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {




                string selectQuery = "SELECT PERSONNE.ID_PERSONNE,";
                selectQuery += "PER_NOM,";
                selectQuery += "PER_PRENOM,";
                selectQuery += "PER_EMAIL,";
                selectQuery += "PER_TELPRINC,";
                selectQuery += "PER_TELTRAV1,";
                selectQuery += "PER_GENRE,";
                selectQuery += "PER_TELECOPIE,";
                selectQuery += "PER_ADR1_prof,";
                selectQuery += "PER_ADR2_prof,";
                selectQuery += "PER_VILLE_prof,";
                selectQuery += "PER_CPOSTAL_prof,";
                selectQuery += "PER_ADR1,";
                selectQuery += "PER_ADR2,";
                selectQuery += "PER_VILLE,";
                selectQuery += "PER_CPOSTAL,";
                selectQuery += "PER_NOTES,";
                selectQuery += "PERS_TITRE,";
                selectQuery += "per_secu,";
                selectQuery += "TUVOUS,";
                selectQuery += "OI_LOGIN,";
                selectQuery += "OI_MDP,";
                selectQuery += "PREF_COM,";
                selectQuery += "CATEGORIES,";
                selectQuery += "lnk.RELATION, ";
                selectQuery += "lnk.TYPELIEN, ";
                selectQuery += "lnk.ID_PATIENT, ";
                selectQuery += "TYPE_PERS.NOM as PROFESSION, ";
                selectQuery += "TYPE_PERS.ID_TYPE as TYPE ";
                selectQuery += "FROM PERSONNE ";
                selectQuery += "INNER JOIN TYPE_PERS on TYPE_PERS.ID_TYPE=PERSONNE.PER_TYPE ";
                selectQuery += "INNER JOIN lienpers lnk on lnk.ID_PERSONNE=PERSONNE.ID_PERSONNE ";

                selectQuery += " Where  personne.TYPE_MATERIEL IS NULL AND lnk.id_patient = @IdPatient and lnk.Relation = 'Ac'";
                selectQuery += " order by PER_NOM,PER_PRENOM";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@IdPatient", patient.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();


                return ds.Tables[0];



            }
            catch (System.IndexOutOfRangeException )
            {
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

        public static int InsertCorrespondant(Correspondant p_corres)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select MAX(ID_PERSONNE)+1 as NEWID from PERSONNE";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                p_corres.Id = Convert.ToInt32(command.ExecuteScalar());

                selectQuery = "INSERT INTO  PERSONNE (";
                selectQuery += "ID_PERSONNE,";
                selectQuery += "PER_NOM,";
                selectQuery += "PER_PRENOM,";


                selectQuery += "PROFESSION,";
                selectQuery += "PER_NOTES,";
                selectQuery += "TUVOUS,";
                selectQuery += "CATEGORIES,";
                selectQuery += "PREF_COM,";
                selectQuery += "PERS_TITRE,";
                selectQuery += "PER_TYPE,";
                selectQuery += "PER_SECU,";
                selectQuery += "PER_GENRE";
                selectQuery += ") values (";
                selectQuery += "@ID_PERSONNE,";
                selectQuery += "@PER_NOM,";
                selectQuery += "@PER_PRENOM,";

                selectQuery += "@PROFESSION,";
                selectQuery += "@PER_NOTES,";
                selectQuery += "@TUVOUS,";
                //selectQuery += "@CATEGORIES,";
                selectQuery += "@PREF_COM,";
                selectQuery += "@PERS_TITRE,";
                selectQuery += "@PER_TYPE,";
                selectQuery += "@PER_SECU,";
                selectQuery += "@PER_GENRE)";


                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@ID_PERSONNE", p_corres.Id);
                command.Parameters.AddWithValue("@PER_NOM", p_corres.Nom);
                command.Parameters.AddWithValue("@PER_PRENOM", p_corres.Prenom);

                command.Parameters.AddWithValue("@PROFESSION", p_corres.Profession);
                command.Parameters.AddWithValue("@PER_NOTES", p_corres.Notes);
                if (p_corres.TuToiement)
                    command.Parameters.AddWithValue("@TUVOUS", 0);
                else
                    command.Parameters.AddWithValue("@TUVOUS", 1);

               
                command.Parameters.AddWithValue("@PERS_TITRE", p_corres.Titre);
                command.Parameters.AddWithValue("@PREF_COM", ((char)p_corres.PrefCom).ToString());
                command.Parameters.AddWithValue("@PER_TYPE", p_corres.Type);
                if (p_corres.GenreFeminin)
                    command.Parameters.AddWithValue("@PER_GENRE", "F");
                else
                    command.Parameters.AddWithValue("@PER_GENRE", "M");

               command.Parameters.AddWithValue("@PER_SECU", p_corres.numSecu);

                command.ExecuteNonQuery();

                transaction.Commit();
                return p_corres.Id;

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

        public static int UpdateCorrespondant(Correspondant p_corres)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select MAX(ID_PERSONNE)+1 as NEWID from PERSONNE";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                int id = Convert.ToInt32(command.ExecuteScalar());

                selectQuery = "update personne set ";
                //selectQuery += " id_adresse = @id_adresse,";
                //selectQuery += " id_util = @id_util,";
                //selectQuery += "     id_caisse = @id_caisse,";
                //selectQuery += "     adr_id_adresse = @adr_id_adresse,";
                selectQuery += "     per_nom = @per_nom,";
                //selectQuery += "     per_nomjf = @per_nomjf,";
                selectQuery += "     per_prenom = @per_prenom,";
                //selectQuery += "     per_genre = @per_genre,";
                //selectQuery += "     per_secu = @per_secu,";
                //selectQuery += "     per_type = @per_type,";
                //selectQuery += "     per_telprinc = @per_telprinc,";
                //selectQuery += "     per_teltrav1 = @per_teltrav1,";
                ////selectQuery += "     per_teltrav2 = @per_teltrav2,";
                //selectQuery += "     per_telecopie = @per_telecopie,";
                //selectQuery += "     per_email = @per_email,";
                //selectQuery += "     per_reception = @per_reception,";
                selectQuery += "     per_notes = @per_notes,";
                selectQuery += "     per_numsecu = @per_numsecu,";
                //selectQuery += "     per_poste = @per_poste,";
                //selectQuery += "     pcom = @pcom,";

                //selectQuery += "     per_adr1_prof = @per_adr1p,";
                //selectQuery += "     per_adr2_prof = @per_adr2p,";
                //selectQuery += "     per_ville_prof = @per_villep,";
                //selectQuery += "     per_cpostal = @per_cpostalp,";

                //selectQuery += "     per_adr1 = @per_adr1h,";
                //selectQuery += "     per_adr2 = @per_adr2h,";
                //selectQuery += "     per_ville = @per_villeh,";
                //selectQuery += "     per_cpostal = @per_cpostalh,";

                selectQuery += "     profession = @profession,";
                //selectQuery += "     mutuelle = @mutuelle,";
                //selectQuery += "     per_datnaiss = @per_datnaiss,";
                selectQuery += "     tuvous = @tuvous,";
                //selectQuery += "     poid = @poid,";
                //selectQuery += "     email2 = @email2,";
                //selectQuery += "     gsm = @gsm,";
                //selectQuery += "     icq = @icq,";
                //selectQuery += "     im1 = @im1,";
                //selectQuery += "     im2 = @im2,";
                //selectQuery += "     lastmodif = @lastmodif,";
                //selectQuery += "     telsup0 = @telsup0,";
                //selectQuery += "     telsup3 = @telsup3,";
                //selectQuery += "     telsup4 = @telsup4,";
                //selectQuery += "     telsup5 = @telsup5,";
                //selectQuery += "     telsup6 = @telsup6,";
                //selectQuery += "     telsup8 = @telsup8,";
                //selectQuery += "     telsup10 = @telsup10,";
                //selectQuery += "     telsup11 = @telsup11,";
                //selectQuery += "     telsup12 = @telsup12,";
                //selectQuery += "     telsup13 = @telsup13,";
                //selectQuery += "     telsup14 = @telsup14,";
                //selectQuery += "     telsup15 = @telsup15,";
                //selectQuery += "     telsup16 = @telsup16,";
                //selectQuery += "     telsup17 = @telsup17,";
                //selectQuery += "     telsup18 = @telsup18,";
                //selectQuery += "     indicetel1 = @indicetel1,";
                //selectQuery += "     indicetel2 = @indicetel2,";
                //selectQuery += "     indicetel3 = @indicetel3,";
                //selectQuery += "     indicetel4 = @indicetel4,";
                //selectQuery += "     email3 = @email3,";
                //selectQuery += "     indiceemail = @indiceemail,";
                //selectQuery += "     indiceadr = @indiceadr,";
                //selectQuery += "     pays_dom = @pays_dom,";
                //selectQuery += "     pays_trav = @pays_trav,";
                selectQuery += "     pers_titre = @pers_titre,";
                //selectQuery += "     pers_siteweb = @pers_siteweb,";
                //selectQuery += "     per_ville_naissance = @per_ville_naissance,";
                //selectQuery += "     per_pays_naissance = @per_pays_naissance,";
                //selectQuery += "     per_langue_parle = @per_langue_parle,";
                //selectQuery += "     per_population_ref = @per_population_ref,";
                //selectQuery += "     nom_rep_image = @nom_rep_image,";
                //selectQuery += "     oi_login = @oi_login,";
                //selectQuery += "     oi_mdp = @oi_mdp,";
                //selectQuery += "     oi_profil = @oi_profil,";
                //selectQuery += "     oi_autorisation = @oi_autorisation,";
                //selectQuery += "     categories = @categories,";
                selectQuery += "     pref_com = @pref_com,";
                selectQuery += "     PER_GENRE = @PER_GENRE";

                selectQuery += " where (id_personne = @id_personne)";
                command.CommandText = selectQuery;

                command.Parameters.AddWithValue("@id_personne", p_corres.Id);
                command.Parameters.AddWithValue("@per_nom", p_corres.Nom);
                command.Parameters.AddWithValue("@per_prenom", p_corres.Prenom);

                command.Parameters.AddWithValue("@profession", p_corres.Profession);
                command.Parameters.AddWithValue("@per_notes", p_corres.Notes);


                if (p_corres.TuToiement)
                    command.Parameters.AddWithValue("@tuvous", 0);
                else
                    command.Parameters.AddWithValue("@tuvous", 1);



                command.Parameters.AddWithValue("@pref_com", ((char)p_corres.PrefCom).ToString());

                command.Parameters.AddWithValue("@pers_titre", p_corres.Titre);
                command.Parameters.AddWithValue("@per_type", p_corres.Type);

                if (p_corres.GenreFeminin)
                    command.Parameters.AddWithValue("@PER_GENRE", "F");
                else
                    command.Parameters.AddWithValue("@PER_GENRE", "M");


                command.Parameters.AddWithValue("@PER_SECU", p_corres.numSecu);


                command.ExecuteNonQuery();

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


        public static DataTable getCorrespondantsOf(basePatient patient)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {




                string selectQuery = "SELECT PERSONNE.ID_PERSONNE,";
                selectQuery += "PER_NOM,";
                selectQuery += "PER_PRENOM,";
                selectQuery += "PER_EMAIL,";
                selectQuery += "PER_TELPRINC,";
                selectQuery += "PER_TELTRAV1,";
                selectQuery += "PER_GENRE,";
                selectQuery += "PER_TELECOPIE,";
                selectQuery += "PER_ADR1_prof,";
                selectQuery += "PER_ADR2_prof,";
                selectQuery += "PER_VILLE_prof,";
                selectQuery += "PER_CPOSTAL_prof,";
                selectQuery += "PER_ADR1,";
                selectQuery += "PER_ADR2,";
                selectQuery += "PER_VILLE,";
                selectQuery += "PER_CPOSTAL,";
                selectQuery += "PER_NOTES,";
                selectQuery += "PERS_TITRE,";
                selectQuery += "per_secu,";
                selectQuery += "TUVOUS,";
                selectQuery += "OI_LOGIN,";
                selectQuery += "OI_MDP,";
                selectQuery += "PREF_COM,";
                selectQuery += "CATEGORIES,";
                selectQuery += "lnk.RELATION, ";
                selectQuery += "lnk.TYPELIEN, ";
                selectQuery += "lnk.ID_PATIENT, ";
                selectQuery += "TYPE_PERS.NOM as PROFESSION, ";
                selectQuery += "TYPE_PERS.ID_TYPE as TYPE ";
                selectQuery += "FROM PERSONNE ";
                selectQuery += "INNER JOIN TYPE_PERS on TYPE_PERS.ID_TYPE=PERSONNE.PER_TYPE ";
                selectQuery += "INNER JOIN lienpers lnk on lnk.ID_PERSONNE=PERSONNE.ID_PERSONNE ";

                selectQuery += " Where  personne.TYPE_MATERIEL IS NULL AND lnk.id_patient = @IdPatient";
                selectQuery += " order by PER_NOM,PER_PRENOM";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@IdPatient", patient.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();


                return ds.Tables[0];



            }
            catch (System.IndexOutOfRangeException )
            {
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
        
        public static DataTable getRestrictedPatients(string nom)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "select personne.id_personne,";
                selectQuery += " personne.per_nom,";
                selectQuery += " personne.per_prenom";
                selectQuery += " from personne";
                selectQuery += " inner join patient on patient.Id_personne = personne.id_personne ";
                selectQuery += " WHERE   personne.TYPE_MATERIEL IS NULL ";
                if (nom != "") selectQuery += " and upper(personne.per_nom) like '" + nom.ToUpper() + "%' ";
                selectQuery += " order by personne.PER_NOM, personne.PER_PRENOM";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                return dt;

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




        public static DataTable getAllObjectifsSuggested()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "select id_key, ";
                selectQuery += "       libelle ";
                selectQuery += " from paramobjectif";
                selectQuery += " order by libelle";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                return dt;

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


        public static DataTable getCommonObjectifs()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "select id, ";
                selectQuery += "       libelle, ";
                selectQuery += "       Categorie, ";                
                selectQuery += "       description ";
                selectQuery += " from bas_commonobjectif";
                selectQuery += " order by libelle";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                return dt;

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
        
        public static DataTable getCommonDiagnostics()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "select id, ";
                selectQuery += "       libelle, ";
                selectQuery += "       photos, ";
                selectQuery += "       question, ";
                selectQuery += "       DisplayOrder ";    
                
                selectQuery += " from bas_commondiag";
                selectQuery += " order by libelle";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                return dt;

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



        public static DataTable FindAppareilsFor(TemplateActePG template)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "select ID_APPAREIL ";
                selectQuery += " from BAS_APPAREIL_ACTEGESTION";
                selectQuery += " where ID_ACTE=@ID_ACTE";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@ID_ACTE", template.Id);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                return dt;

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


        public static DataTable getAllDiagnostiqueSuggested()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "select id_key, ";
                selectQuery += "       libelle ";
                selectQuery += " from PARAMDIAG";
                selectQuery += " order by libelle";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                return dt;

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


        public static void InsertObjectifsSuggested(ObjectifSuggests obj)
        {
            if (connection == null) getConnection();
            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQueryId = "select max(Id)+1 as ID from paramobjectif";
                MySqlCommand commandid = new MySqlCommand(selectQueryId, connection, transaction);
                object id = commandid.ExecuteScalar();
                if (id == DBNull.Value)
                    obj.Id = 1;
                else
                    obj.Id = Convert.ToInt32(id);

                string selectQuery = "insert into paramobjectif (id, libelle) ";
                selectQuery += "   values (@id, @libelle) ";



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id", obj.Id);
                command.Parameters.AddWithValue("@libelle", obj.Libelle);


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

        public static void UpdateObjectifsSuggested(ObjectifSuggests obj)
        {
            if (connection == null) getConnection();
            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                

                string selectQuery = "update paramobjectif set libelle = @libelle where id=@id ";



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id", obj.Id);
                command.Parameters.AddWithValue("@libelle", obj.Libelle);


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

        public static void DeleteObjectifsSuggested(ObjectifSuggests obj)
        {
            if (connection == null) getConnection();
            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {


                string selectQuery = "delete from paramobjectif where id=@id ";



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id", obj.Id);
                command.Parameters.AddWithValue("@libelle", obj.Libelle);


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


        public static bool FindFirstDEPFromOrthalis(basePatient pat, out int numdate, out int idplan, out string Cotation)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                //Recupere le numdate d'orthalis pour l'associer à l'entente
                string selectQueryId = "select id_plan,num_date,tarif from plan_applique";
                selectQueryId += " where num_date = (select min(num_date) from plan_applique where id_patient = @idpatient and type_regle=3) and id_patient = @idpatient";

                MySqlCommand commandid = new MySqlCommand(selectQueryId, connection, transaction);
                commandid.Parameters.AddWithValue("@idpatient", pat.Id);


                
                    MySqlDataReader dr = commandid.ExecuteReader();
                    dr.Read();
                    numdate = Convert.ToInt32(dr["num_date"]);
                    idplan = Convert.ToInt32(dr["id_plan"]);
                    Cotation = Convert.ToString(dr["tarif"]).Trim();
                    return true;
                

            }

            catch (System.Exception)
            {
                numdate = 0;
                idplan = 0;
                Cotation = "TO90";
                transaction.Rollback();
                return false;
            }
            finally
            {
                connection.Close();

            }
        }


        public static void InsertEntentePrealableWithoutDiag(EntentePrealable ententepre)
        {
            InsertEntentePrealableWithoutDiag(ententepre, -1, -1);
        }

        public static void InsertEntentePrealableWithoutDiag(EntentePrealable ententepre, int numdate, int idplan)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                
               
                string selectQueryId = "select max(Id)+1 as ID from envoi_entente";
                MySqlCommand commandid = new MySqlCommand(selectQueryId, connection, transaction);
                object id = commandid.ExecuteScalar();
                if (id == DBNull.Value)
                    ententepre.IdModele = 1;
                else
                    ententepre.IdModele = Convert.ToInt32(id);



                string selectQuery = "insert into envoi_entente (id,ID_PRATICIEN, ee_patient, ";
                selectQuery += "  ee_numdate, ";
                selectQuery += "  ee_debuttraitement, ";
                selectQuery += "  ee_surveillance, ";
                selectQuery += "  ee_suite, ";
                selectQuery += "  ee_numsemestre, ";
                selectQuery += "  ee_contention, ";
                selectQuery += "  ee_annee, ";
                selectQuery += "  ee_autre, ";
                selectQuery += "  ee_autretext, ";
                selectQuery += "  ee_id_plan, ";
                selectQuery += "  ee_immat, ";
                selectQuery += "  ee_dateprop, ";
                selectQuery += "  EE_DATEIMPRESSION, ";
                selectQuery += "  ee_cotation, ";
                selectQuery += "  ee_dateaccord, ";
                selectQuery += "  ee_devis, ";
                selectQuery += "  ee_rmo, ";
                selectQuery += "  ee_traitement1, ";
                selectQuery += "  ee_traitement2, ";
                selectQuery += "  ee_traitement3, ";
                selectQuery += "  ee_commentaire1, ";
                selectQuery += "  ee_commentaire2, ";
                selectQuery += "  ee_commentaire3, ";
                selectQuery += "  ID_MODELE_ENVOI, ";
                selectQuery += "  ee_libsemestre)";
                selectQuery += " values (@id,@ID_PRATICIEN, @ee_patient, ";
                selectQuery += "        @ee_numdate, ";
                selectQuery += "        @ee_debuttraitement, ";
                selectQuery += "        @ee_surveillance, ";
                selectQuery += "        @ee_suite, ";
                selectQuery += "        @ee_numsemestre, ";
                selectQuery += "        @ee_contention, ";
                selectQuery += "        @ee_annee, ";
                selectQuery += "        @ee_autre, ";
                selectQuery += "        @ee_autretext, ";
                selectQuery += "        @ee_id_plan, ";
                selectQuery += "        @ee_immat, ";
                selectQuery += "        @ee_dateprop, ";
                selectQuery += "        @EE_DATEIMPRESSION, ";
                selectQuery += "        @ee_cotation, ";
                selectQuery += "        @ee_dateaccord, ";
                selectQuery += "        @ee_devis, ";
                selectQuery += "        @ee_rmo, ";
                selectQuery += "        @ee_traitement1, ";
                selectQuery += "        @ee_traitement2, ";
                selectQuery += "        @ee_traitement3, ";
                selectQuery += "        @ee_commentaire1, ";
                selectQuery += "        @ee_commentaire2, ";
                selectQuery += "        @ee_commentaire3, ";
                selectQuery += "        @ID_MODELE_ENVOI, ";
                
                selectQuery += "        @ee_libsemestre)";



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id", ententepre.IdModele);
                command.Parameters.AddWithValue("@ee_patient", ententepre.patient.Id);
                command.Parameters.AddWithValue("@ee_numdate", numdate);
                command.Parameters.AddWithValue("@ee_debuttraitement", ententepre.typetraitement == EntentePrealable.TypeDeTraitement.Debut);
                command.Parameters.AddWithValue("@ee_surveillance", ententepre.typetraitement == EntentePrealable.TypeDeTraitement.Surveillance);
                command.Parameters.AddWithValue("@ee_suite", ententepre.typetraitement == EntentePrealable.TypeDeTraitement.Semestre);
                command.Parameters.AddWithValue("@ee_numsemestre", ententepre.Semestre);
                command.Parameters.AddWithValue("@ee_contention", ententepre.typetraitement == EntentePrealable.TypeDeTraitement.Contention);
                command.Parameters.AddWithValue("@ee_annee", ententepre.Contention);
                command.Parameters.AddWithValue("@ee_autre", ententepre.typetraitement == EntentePrealable.TypeDeTraitement.Autre);
                command.Parameters.AddWithValue("@ee_autretext", ententepre.Autre);
                command.Parameters.AddWithValue("@ee_id_plan", idplan);
                command.Parameters.AddWithValue("@ee_immat", ententepre.ImmatAssure);
                command.Parameters.AddWithValue("@ee_dateprop", ententepre.dateProposition);
                command.Parameters.AddWithValue("@ee_cotation", ententepre.cotationDesActes);
                command.Parameters.AddWithValue("@EE_DATEIMPRESSION", ententepre.DateImpression);
                command.Parameters.AddWithValue("@ee_dateaccord", ententepre.DateAccord == null ? DBNull.Value : (object)ententepre.DateAccord.Value);
                command.Parameters.AddWithValue("@ee_devis", ententepre.IsDevisSigned);
                if (ententepre.ReferenceNationalOpposable == EntentePrealable.RNO.R)
                    command.Parameters.AddWithValue("@ee_rmo", 0);
                else if (ententepre.ReferenceNationalOpposable == EntentePrealable.RNO.HR)
                    command.Parameters.AddWithValue("@ee_rmo", 1);
                else if (ententepre.ReferenceNationalOpposable == EntentePrealable.RNO.None)
                    command.Parameters.AddWithValue("@ee_rmo", -1);

                command.Parameters.AddWithValue("@ee_traitement1", ententepre.PlanDeTraitement);
                command.Parameters.AddWithValue("@ee_traitement2", "");
                command.Parameters.AddWithValue("@ee_traitement3", "");
                command.Parameters.AddWithValue("@ee_commentaire1", ententepre.Commentaires);
                command.Parameters.AddWithValue("@ee_commentaire2", "");
                command.Parameters.AddWithValue("@ee_commentaire3", "");
                command.Parameters.AddWithValue("@ee_libsemestre", "1er SEMESTRE");
                command.Parameters.AddWithValue("@ID_PRATICIEN", ententepre.Praticien==null?DBNull.Value:(object)ententepre.Praticien.Id);

                command.Parameters.AddWithValue("@ID_MODELE_ENVOI", ententepre.IdDiag);


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


        public static DataTable getContactLib()
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select ID, ";

                selectQuery += "LIBELLE, ";
                selectQuery += "TYPECONTACT, ";
                selectQuery += "TYPEAFFECTATION ";
                selectQuery += "FROM BASE_CONTACTLIBELLE ";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();


                return ds.Tables[0];

            }
            catch (System.IndexOutOfRangeException)
            {
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


        public static void UpdateEntentePrealableWithoutDiag(EntentePrealable ententepre)
        {

            

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                if (ententepre.IdDiag == 0)
                {
                    string selectQueryId = "select max(Id)+1 as ID from envoi_entente";
                    MySqlCommand commandid = new MySqlCommand(selectQueryId, connection, transaction);
                    object id = commandid.ExecuteScalar();
                    if (id == DBNull.Value)
                        ententepre.IdModele = 1;
                    else
                        ententepre.IdModele = Convert.ToInt32(id);
                }



                string selectQuery = "update envoi_entente";
                selectQuery += " set ee_patient = @ee_patient,";
                selectQuery += "    ee_debuttraitement = @ee_debuttraitement,";
                selectQuery += "    ee_surveillance = @ee_surveillance,";
                selectQuery += "    ee_suite = @ee_suite,";
                selectQuery += "    ee_numsemestre = @ee_numsemestre,";
                selectQuery += "    ee_contention = @ee_contention,";
                selectQuery += "    ee_annee = @ee_annee,";
                selectQuery += "    ee_autre = @ee_autre,";
                selectQuery += "    ee_autretext = @ee_autretext,";
                selectQuery += "    ee_immat = @ee_immat,";
                selectQuery += "    ee_dateprop = @ee_dateprop,";
                selectQuery += "    ee_cotation = @ee_cotation,";
                selectQuery += "    ee_dateaccord = @ee_dateaccord,";
                selectQuery += "    ee_devis = @ee_devis,";
                selectQuery += "    ee_rmo = @ee_rmo,";
                selectQuery += "    ee_traitement1 = @ee_traitement1,";
                selectQuery += "    ee_traitement2 = @ee_traitement2,";
                selectQuery += "    ee_traitement3 = @ee_traitement3,";
                selectQuery += "    ee_commentaire1 = @ee_commentaire1,";
                selectQuery += "    ee_commentaire2 = @ee_commentaire2,";
                selectQuery += "    ee_commentaire3 = @ee_commentaire3,";
                selectQuery += "    ID_MODELE_ENVOI = @ID_MODELE_ENVOI,";          
                selectQuery += "    id = @id";
                
                if (ententepre.IdDiag > 0)
                    selectQuery += " where id = @id";
                else
                    selectQuery += " where ee_patient = @ee_patient and ee_debuttraitement = @ee_debuttraitement";



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@ee_patient", ententepre.patient.Id);
                command.Parameters.AddWithValue("@id", ententepre.IdModele);
                command.Parameters.AddWithValue("@ee_debuttraitement", ententepre.typetraitement == EntentePrealable.TypeDeTraitement.Debut);
                command.Parameters.AddWithValue("@ee_surveillance", ententepre.typetraitement == EntentePrealable.TypeDeTraitement.Surveillance);
                command.Parameters.AddWithValue("@ee_suite", ententepre.typetraitement == EntentePrealable.TypeDeTraitement.Semestre);
                command.Parameters.AddWithValue("@ee_numsemestre", ententepre.Semestre);
                command.Parameters.AddWithValue("@ee_contention", ententepre.typetraitement == EntentePrealable.TypeDeTraitement.Contention);
                command.Parameters.AddWithValue("@ee_annee", ententepre.Contention);
                command.Parameters.AddWithValue("@ee_autre", ententepre.typetraitement == EntentePrealable.TypeDeTraitement.Autre);
                command.Parameters.AddWithValue("@ee_autretext", ententepre.Autre);
                command.Parameters.AddWithValue("@ee_immat", ententepre.ImmatAssure);
                command.Parameters.AddWithValue("@ee_dateprop", ententepre.dateProposition);
                command.Parameters.AddWithValue("@ee_cotation", ententepre.cotationDesActes);
                command.Parameters.AddWithValue("@ee_dateaccord", DBNull.Value);
                command.Parameters.AddWithValue("@ee_devis", ententepre.IsDevisSigned);
                if (ententepre.ReferenceNationalOpposable == EntentePrealable.RNO.R)
                    command.Parameters.AddWithValue("@ee_rmo", 0);
                else if (ententepre.ReferenceNationalOpposable == EntentePrealable.RNO.HR)
                    command.Parameters.AddWithValue("@ee_rmo", 1);
                else if (ententepre.ReferenceNationalOpposable == EntentePrealable.RNO.None)
                    command.Parameters.AddWithValue("@ee_rmo", -1);

                command.Parameters.AddWithValue("@ee_traitement1", ententepre.PlanDeTraitement);
                command.Parameters.AddWithValue("@ee_traitement2", "");
                command.Parameters.AddWithValue("@ee_traitement3", "");
                command.Parameters.AddWithValue("@ee_commentaire1", ententepre.Commentaires);
                command.Parameters.AddWithValue("@ee_commentaire2", "");
                command.Parameters.AddWithValue("@ee_commentaire3", "");
                command.Parameters.AddWithValue("@ID_MODELE_ENVOI", ententepre.IdDiag);
                

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

        public static DataRow getEntentePrealableWithoutDiag(int IdModele)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "select id, ee_patient, ";
                selectQuery += "       ee_numdate, ";
                selectQuery += "       ee_debuttraitement, ";
                selectQuery += "       ee_surveillance, ";
                selectQuery += "       ee_suite, ";
                selectQuery += "       ee_numsemestre, ";
                selectQuery += "       ee_contention, ";
                selectQuery += "       ee_annee, ";
                selectQuery += "       ee_autre, ";
                selectQuery += "       ee_autretext, ";
                selectQuery += "       ee_id_plan, ";
                selectQuery += "       ee_immat, ";
                selectQuery += "       ee_dateprop, ";
                selectQuery += "       EE_DATEIMPRESSION, ";
                selectQuery += "       ee_cotation, ";
                selectQuery += "       ee_dateaccord, ";
                selectQuery += "       ee_devis, ";
                selectQuery += "       ee_rmo, ";
                selectQuery += "       ee_traitement1, ";
                selectQuery += "       ee_traitement2, ";
                selectQuery += "       ee_traitement3, ";
                selectQuery += "       ee_commentaire1, ";
                selectQuery += "       ee_commentaire2, ";
                selectQuery += "       ee_commentaire3, ";
                selectQuery += "       ee_libsemestre, ";
                selectQuery += "       id,";
                selectQuery += "       ID_PRATICIEN,";
                selectQuery += "       ID_MODELE_ENVOI";

                selectQuery += " from envoi_entente";
                selectQuery += " where id=@id";

               

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", IdModele);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                    return null;
                else
                    return dt.Rows[0];

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


        public static DataRow getEntentePrealableWithoutDiag(int IdPatient, EntentePrealable.TypeDeTraitement type)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "select id, ee_patient, ";
                selectQuery += "       ee_numdate, ";
                selectQuery += "       ee_debuttraitement, ";
                selectQuery += "       ee_surveillance, ";
                selectQuery += "       ee_suite, ";
                selectQuery += "       ee_numsemestre, ";
                selectQuery += "       ee_contention, ";
                selectQuery += "       ee_annee, ";
                selectQuery += "       ee_autre, ";
                selectQuery += "       ee_autretext, ";
                selectQuery += "       ee_id_plan, ";
                selectQuery += "       ee_immat, ";
                selectQuery += "       ee_dateprop, ";
                selectQuery += "       EE_DATEIMPRESSION, ";
                selectQuery += "       ee_cotation, ";
                selectQuery += "       ee_dateaccord, ";
                selectQuery += "       ee_devis, ";
                selectQuery += "       ee_rmo, ";
                selectQuery += "       ee_traitement1, ";
                selectQuery += "       ee_traitement2, ";
                selectQuery += "       ee_traitement3, ";
                selectQuery += "       ee_commentaire1, ";
                selectQuery += "       ee_commentaire2, ";
                selectQuery += "       ee_commentaire3, ";
                selectQuery += "       ee_libsemestre, ";
                selectQuery += "       id,";
                selectQuery += "       ID_PRATICIEN";
                
                selectQuery += " from envoi_entente";
                selectQuery += " where ee_patient=@ee_patient";

                if (type == EntentePrealable.TypeDeTraitement.Debut)
                    selectQuery += " and ee_debuttraitement=1";
                if (type == EntentePrealable.TypeDeTraitement.Autre)
                    selectQuery += " and ee_autre=1";
                if (type == EntentePrealable.TypeDeTraitement.Contention)
                    selectQuery += " and ee_contention=1";
                if (type == EntentePrealable.TypeDeTraitement.Semestre)
                    selectQuery += " and ee_suite=1";
                if (type == EntentePrealable.TypeDeTraitement.Surveillance)
                    selectQuery += " and ee_surveillance=1";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@ee_patient", IdPatient);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                    return null;
                else
                    return dt.Rows[0];

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

        public static DataRow getDiagEntentePrealable(int IdDiag)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "select id, me_patient, ";
                selectQuery += "       me_alvemaxpro, ";
                selectQuery += "       me_alvemaxendo, ";
                selectQuery += "       me_alvemaxsupra, ";
                selectQuery += "       me_alvemaxretro, ";
                selectQuery += "       me_alvemaxexo, ";
                selectQuery += "       me_alvemandpro, ";
                selectQuery += "       me_alvemandendo, ";
                selectQuery += "       me_alvemandinfra, ";
                selectQuery += "       me_alvemandretro, ";
                selectQuery += "       me_alvemandexo, ";
                selectQuery += "       me_basmaxpro, ";
                selectQuery += "       me_basmaxendo, ";
                selectQuery += "       me_basmaxhypo, ";
                selectQuery += "       me_basmaxretro, ";
                selectQuery += "       me_basmaxexo, ";
                selectQuery += "       me_basmandpro, ";
                selectQuery += "       me_basmandendo, ";
                selectQuery += "       me_basmandhyper, ";
                selectQuery += "       me_basmandretro, ";
                selectQuery += "       me_basmandexo, ";
                selectQuery += "       me_mol1, ";
                selectQuery += "       me_mol2, ";
                selectQuery += "       me_mol3, ";
                selectQuery += "       me_moltext, ";
                selectQuery += "       me_can1, ";
                selectQuery += "       me_can2, ";
                selectQuery += "       me_can3, ";
                selectQuery += "       me_cantext, ";
                selectQuery += "       me_occludroit, ";
                selectQuery += "       me_occlugauche, ";
                selectQuery += "       me_occluanter, ";
                selectQuery += "       me_agnesie, ";
                selectQuery += "       me_dentincl, ";
                selectQuery += "       me_malpos, ";
                selectQuery += "       me_dysharmo, ";
                selectQuery += "       me_dysharmodd, ";
                selectQuery += "       me_facteurfonc, ";
                selectQuery += "       pat_objectif_trait2, ";
                selectQuery += "       pat_objectif_comm2";
                selectQuery += " from modele_entente";
                selectQuery += " where id = @Id";
                
                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@Id", IdDiag);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count == 0) 
                    return null;
                else
                    return dt.Rows[0];

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
        
        public static void InsertDiagEntentePrealable(EntentePrealable ententepre)
        {
            if (connection == null) getConnection();
            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQueryId = "select max(Id)+1 as ID from modele_entente";
                MySqlCommand commandid = new MySqlCommand(selectQueryId, connection, transaction);
                object id = commandid.ExecuteScalar();
                if (id == DBNull.Value)
                    ententepre.IdDiag = 1;
                else
                    ententepre.IdDiag = Convert.ToInt32(id);

                string selectQuery = "insert into modele_entente (id, me_patient, ";
                selectQuery += "   me_alvemaxpro, ";
                selectQuery += "   me_alvemaxendo, ";
                selectQuery += "   me_alvemaxsupra, ";
                selectQuery += "   me_alvemaxretro, ";
                selectQuery += "   me_alvemaxexo, ";
                selectQuery += "   me_alvemandpro, ";
                selectQuery += "   me_alvemandendo, ";
                selectQuery += "   me_alvemandinfra, ";
                selectQuery += "   me_alvemandretro, ";
                selectQuery += "   me_alvemandexo, ";
                selectQuery += "   me_basmaxpro, ";
                selectQuery += "   me_basmaxendo, ";
                selectQuery += "   me_basmaxhypo, ";
                selectQuery += "   me_basmaxretro, ";
                selectQuery += "   me_basmaxexo, ";
                selectQuery += "   me_basmandpro, ";
                selectQuery += "   me_basmandendo, ";
                selectQuery += "   me_basmandhyper, ";
                selectQuery += "   me_basmandretro, ";
                selectQuery += "   me_basmandexo, ";
                selectQuery += "   me_mol1, ";
                selectQuery += "   me_mol2, ";
                selectQuery += "   me_mol3, ";
                selectQuery += "   me_moltext, ";
                selectQuery += "   me_can1, ";
                selectQuery += "   me_can2, ";
                selectQuery += "   me_can3, ";
                selectQuery += "   me_cantext, ";
                selectQuery += "   me_occludroit, ";
                selectQuery += "   me_occlugauche, ";
                selectQuery += "   me_occluanter, ";
                selectQuery += "   me_agnesie, ";
                selectQuery += "   me_dentincl, ";
                selectQuery += "   me_malpos, ";
                selectQuery += "   me_dysharmo, ";
                selectQuery += "   me_dysharmodd, ";
                selectQuery += "   me_facteurfonc, ";
                selectQuery += "   pat_objectif_trait2, ";
                selectQuery += "   pat_objectif_comm2)";
                selectQuery += "values (@id, @me_patient, ";
                selectQuery += "        @me_alvemaxpro, ";
                selectQuery += "        @me_alvemaxendo, ";
                selectQuery += "        @me_alvemaxsupra, ";
                selectQuery += "        @me_alvemaxretro, ";
                selectQuery += "        @me_alvemaxexo, ";
                selectQuery += "        @me_alvemandpro, ";
                selectQuery += "        @me_alvemandendo, ";
                selectQuery += "        @me_alvemandinfra, ";
                selectQuery += "        @me_alvemandretro, ";
                selectQuery += "        @me_alvemandexo, ";
                selectQuery += "        @me_basmaxpro, ";
                selectQuery += "        @me_basmaxendo, ";
                selectQuery += "        @me_basmaxhypo, ";
                selectQuery += "        @me_basmaxretro, ";
                selectQuery += "        @me_basmaxexo, ";
                selectQuery += "        @me_basmandpro, ";
                selectQuery += "        @me_basmandendo, ";
                selectQuery += "        @me_basmandhyper, ";
                selectQuery += "        @me_basmandretro, ";
                selectQuery += "        @me_basmandexo, ";
                selectQuery += "        @me_mol1, ";
                selectQuery += "        @me_mol2, ";
                selectQuery += "        @me_mol3, ";
                selectQuery += "        @me_moltext, ";
                selectQuery += "        @me_can1, ";
                selectQuery += "        @me_can2, ";
                selectQuery += "        @me_can3, ";
                selectQuery += "        @me_cantext, ";
                selectQuery += "        @me_occludroit, ";
                selectQuery += "        @me_occlugauche, ";
                selectQuery += "        @me_occluanter, ";
                selectQuery += "        @me_agnesie, ";
                selectQuery += "        @me_dentincl, ";
                selectQuery += "        @me_malpos, ";
                selectQuery += "        @me_dysharmo, ";
                selectQuery += "        @me_dysharmodd, ";
                selectQuery += "        @me_facteurfonc, ";
                
                selectQuery += "        @pat_objectif_trait2, ";
                selectQuery += "        @pat_objectif_comm2)";



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id", ententepre.IdDiag);
                command.Parameters.AddWithValue("@me_patient", ententepre.patient.Id);
                command.Parameters.AddWithValue("@me_alvemaxpro", ententepre.SensSagittalAlveolaireMax == EntentePrealable.en_ProRetro.Pro);
                command.Parameters.AddWithValue("@me_alvemaxendo", ententepre.SensTransversalAlveolaireMax == EntentePrealable.en_DiagAlveolaire.Endoalveolie);
                command.Parameters.AddWithValue("@me_alvemaxsupra", ententepre.SensVerticalAlveolaire == EntentePrealable.en_OccFace.Supraclusion);
                command.Parameters.AddWithValue("@me_alvemaxretro", ententepre.SensSagittalAlveolaireMax == EntentePrealable.en_ProRetro.Retro);
                command.Parameters.AddWithValue("@me_alvemaxexo", ententepre.SensTransversalAlveolaireMax == EntentePrealable.en_DiagAlveolaire.Exoalveolie);
                command.Parameters.AddWithValue("@me_alvemandpro", ententepre.SensSagittalAlveolaireMand == EntentePrealable.en_ProRetro.Pro);
                command.Parameters.AddWithValue("@me_alvemandendo", ententepre.SensTransversalAlveolaireMand == EntentePrealable.en_DiagAlveolaire.Endoalveolie);
                command.Parameters.AddWithValue("@me_alvemandinfra", ententepre.SensVerticalAlveolaire == EntentePrealable.en_OccFace.Infraclusion);
                command.Parameters.AddWithValue("@me_alvemandretro", ententepre.SensSagittalAlveolaireMand == EntentePrealable.en_ProRetro.Retro);
                command.Parameters.AddWithValue("@me_alvemandexo", ententepre.SensTransversalAlveolaireMand == EntentePrealable.en_DiagAlveolaire.Exoalveolie);
                command.Parameters.AddWithValue("@me_basmaxpro", ententepre.SensSagittalBasalMax == EntentePrealable.en_ProRetro.Pro);
                command.Parameters.AddWithValue("@me_basmaxendo", ententepre.SensTransversalBasalMax == EntentePrealable.en_DiagOsseux.Endognatie);
                command.Parameters.AddWithValue("@me_basmaxhypo", ententepre.SensVerticalBasal == EntentePrealable.en_Divergence.Hypodivergent);
                command.Parameters.AddWithValue("@me_basmaxretro", ententepre.SensSagittalBasalMax == EntentePrealable.en_ProRetro.Retro);
                command.Parameters.AddWithValue("@me_basmaxexo", ententepre.SensTransversalBasalMax == EntentePrealable.en_DiagOsseux.Exognatie);
                command.Parameters.AddWithValue("@me_basmandpro", ententepre.SensSagittalBasalMand == EntentePrealable.en_ProRetro.Pro);
                command.Parameters.AddWithValue("@me_basmandendo", ententepre.SensTransversalBasalMand == EntentePrealable.en_DiagOsseux.Endognatie);
                command.Parameters.AddWithValue("@me_basmandhyper", ententepre.SensVerticalBasal == EntentePrealable.en_Divergence.Hyperdivergent);
                command.Parameters.AddWithValue("@me_basmandretro", ententepre.SensSagittalBasalMand == EntentePrealable.en_ProRetro.Retro);
                command.Parameters.AddWithValue("@me_basmandexo", ententepre.SensTransversalBasalMand == EntentePrealable.en_DiagOsseux.Exognatie);
                command.Parameters.AddWithValue("@me_mol1", ententepre.ClasseDentaireMolaire == EntentePrealable.en_Class.Class_I);
                command.Parameters.AddWithValue("@me_mol2", ententepre.ClasseDentaireMolaire == EntentePrealable.en_Class.Class_II);
                command.Parameters.AddWithValue("@me_mol3", ententepre.ClasseDentaireMolaire == EntentePrealable.en_Class.Class_III);
                command.Parameters.AddWithValue("@me_moltext", ententepre.ClasseDentaireMolaireTxt);
                command.Parameters.AddWithValue("@me_can1", ententepre.ClasseDentaireCanine == EntentePrealable.en_Class.Class_I);
                command.Parameters.AddWithValue("@me_can2", ententepre.ClasseDentaireCanine == EntentePrealable.en_Class.Class_II);
                command.Parameters.AddWithValue("@me_can3", ententepre.ClasseDentaireCanine == EntentePrealable.en_Class.Class_III);
                command.Parameters.AddWithValue("@me_cantext", ententepre.ClasseDentaireCanineTxt);
                command.Parameters.AddWithValue("@me_occludroit", (ententepre.occInverse == EntentePrealable.en_OccInverse.Droite) || (ententepre.occInverse == EntentePrealable.en_OccInverse.Droite_Et_Gauche));
                command.Parameters.AddWithValue("@me_occlugauche", (ententepre.occInverse == EntentePrealable.en_OccInverse.Gauche) || (ententepre.occInverse == EntentePrealable.en_OccInverse.Droite_Et_Gauche));
                command.Parameters.AddWithValue("@me_occluanter", ententepre.occInverse == EntentePrealable.en_OccInverse.Anterieur);
                command.Parameters.AddWithValue("@me_agnesie", ententepre.Agenesie);
                command.Parameters.AddWithValue("@me_dentincl", ententepre.DentsIncluseSurnum);
                command.Parameters.AddWithValue("@me_malpos", ententepre.Malposition);
                command.Parameters.AddWithValue("@me_dysharmo", ententepre.DDM);
                command.Parameters.AddWithValue("@me_dysharmodd", ententepre.DDD);
                command.Parameters.AddWithValue("@me_facteurfonc", ententepre.FacteurFonctionnel);
                command.Parameters.AddWithValue("@pat_objectif_trait2", ententepre.PlanDeTraitement);
                command.Parameters.AddWithValue("@pat_objectif_comm2", ententepre.Commentaires);


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
        
        public static void UpdateDiagEntentePrealable(EntentePrealable ententepre)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                if (ententepre.IdDiag == 0)
                {
                    string selectQueryId = "select max(Id)+1 as ID from modele_entente";
                    MySqlCommand commandid = new MySqlCommand(selectQueryId, connection, transaction);
                    object id = commandid.ExecuteScalar();
                    if (id == DBNull.Value)
                        ententepre.IdDiag = 1;
                    else
                        ententepre.IdDiag = Convert.ToInt32(id);
                }

                string selectQuery = "update modele_entente\n";
                selectQuery += "set me_alvemaxpro = @me_alvemaxpro,\n";
                selectQuery += "    id = @id,\n";
                selectQuery += "    me_alvemaxendo = @me_alvemaxendo,\n";
                selectQuery += "    me_alvemaxsupra = @me_alvemaxsupra,\n";
                selectQuery += "    me_alvemaxretro = @me_alvemaxretro,\n";
                selectQuery += "    me_alvemaxexo = @me_alvemaxexo,\n";
                selectQuery += "    me_alvemandpro = @me_alvemandpro,\n";
                selectQuery += "    me_alvemandendo = @me_alvemandendo,\n";
                selectQuery += "    me_alvemandinfra = @me_alvemandinfra,\n";
                selectQuery += "    me_alvemandretro = @me_alvemandretro,\n";
                selectQuery += "    me_alvemandexo = @me_alvemandexo,\n";
                selectQuery += "    me_basmaxpro = @me_basmaxpro,\n";
                selectQuery += "    me_basmaxendo = @me_basmaxendo,\n";
                selectQuery += "    me_basmaxhypo = @me_basmaxhypo,\n";
                selectQuery += "    me_basmaxretro = @me_basmaxretro,\n";
                selectQuery += "    me_basmaxexo = @me_basmaxexo,\n";
                selectQuery += "    me_basmandpro = @me_basmandpro,\n";
                selectQuery += "    me_basmandendo = @me_basmandendo,\n";
                selectQuery += "    me_basmandhyper = @me_basmandhyper,\n";
                selectQuery += "    me_basmandretro = @me_basmandretro,\n";
                selectQuery += "    me_basmandexo = @me_basmandexo,\n";
                selectQuery += "    me_mol1 = @me_mol1,\n";
                selectQuery += "    me_mol2 = @me_mol2,\n";
                selectQuery += "    me_mol3 = @me_mol3,\n";
                selectQuery += "    me_moltext = @me_moltext,\n";
                selectQuery += "    me_can1 = @me_can1,\n";
                selectQuery += "    me_can2 = @me_can2,\n";
                selectQuery += "    me_can3 = @me_can3,\n";
                selectQuery += "    me_cantext = @me_cantext,\n";
                selectQuery += "    me_occludroit = @me_occludroit,\n";
                selectQuery += "    me_occlugauche = @me_occlugauche,\n";
                selectQuery += "    me_occluanter = @me_occluanter,\n";
                selectQuery += "    me_agnesie = @me_agnesie,\n";
                selectQuery += "    me_dentincl = @me_dentincl,\n";
                selectQuery += "    me_malpos = @me_malpos,\n";
                selectQuery += "    me_dysharmo = @me_dysharmo,\n";
                selectQuery += "    me_dysharmodd = @me_dysharmodd,\n";
                selectQuery += "    me_facteurfonc = @me_facteurfonc,\n";
                selectQuery += "    pat_objectif_trait2 = @pat_objectif_trait2,\n";
                selectQuery += "    pat_objectif_comm2 = @pat_objectif_comm2\n";      
                selectQuery += "    where me_patient = @me_patient\n";




                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@me_patient", ententepre.patient.Id);
                command.Parameters.AddWithValue("@id", ententepre.IdDiag);
                command.Parameters.AddWithValue("@me_alvemaxpro", ententepre.SensSagittalAlveolaireMax == EntentePrealable.en_ProRetro.Pro);
                command.Parameters.AddWithValue("@me_alvemaxendo", ententepre.SensTransversalAlveolaireMax == EntentePrealable.en_DiagAlveolaire.Endoalveolie);
                command.Parameters.AddWithValue("@me_alvemaxsupra", ententepre.SensVerticalAlveolaire == EntentePrealable.en_OccFace.Supraclusion);
                command.Parameters.AddWithValue("@me_alvemaxretro", ententepre.SensSagittalAlveolaireMax == EntentePrealable.en_ProRetro.Retro);
                command.Parameters.AddWithValue("@me_alvemaxexo", ententepre.SensTransversalAlveolaireMax == EntentePrealable.en_DiagAlveolaire.Exoalveolie);
                command.Parameters.AddWithValue("@me_alvemandpro", ententepre.SensSagittalAlveolaireMand == EntentePrealable.en_ProRetro.Pro);
                command.Parameters.AddWithValue("@me_alvemandendo", ententepre.SensTransversalAlveolaireMand == EntentePrealable.en_DiagAlveolaire.Endoalveolie);
                command.Parameters.AddWithValue("@me_alvemandinfra", ententepre.SensVerticalAlveolaire == EntentePrealable.en_OccFace.Infraclusion);
                command.Parameters.AddWithValue("@me_alvemandretro", ententepre.SensSagittalAlveolaireMand == EntentePrealable.en_ProRetro.Retro);
                command.Parameters.AddWithValue("@me_alvemandexo", ententepre.SensTransversalAlveolaireMand == EntentePrealable.en_DiagAlveolaire.Exoalveolie);
                command.Parameters.AddWithValue("@me_basmaxpro", ententepre.SensSagittalBasalMax == EntentePrealable.en_ProRetro.Pro);
                command.Parameters.AddWithValue("@me_basmaxendo", ententepre.SensTransversalBasalMax == EntentePrealable.en_DiagOsseux.Endognatie);
                command.Parameters.AddWithValue("@me_basmaxhypo", ententepre.SensVerticalBasal == EntentePrealable.en_Divergence.Hypodivergent);
                command.Parameters.AddWithValue("@me_basmaxretro", ententepre.SensSagittalBasalMax == EntentePrealable.en_ProRetro.Retro);
                command.Parameters.AddWithValue("@me_basmaxexo", ententepre.SensTransversalBasalMax == EntentePrealable.en_DiagOsseux.Exognatie);
                command.Parameters.AddWithValue("@me_basmandpro", ententepre.SensSagittalBasalMand == EntentePrealable.en_ProRetro.Pro);
                command.Parameters.AddWithValue("@me_basmandendo", ententepre.SensTransversalBasalMand == EntentePrealable.en_DiagOsseux.Endognatie);
                command.Parameters.AddWithValue("@me_basmandhyper", ententepre.SensVerticalBasal == EntentePrealable.en_Divergence.Hyperdivergent);
                command.Parameters.AddWithValue("@me_basmandretro", ententepre.SensSagittalBasalMand == EntentePrealable.en_ProRetro.Retro);
                command.Parameters.AddWithValue("@me_basmandexo", ententepre.SensTransversalBasalMand == EntentePrealable.en_DiagOsseux.Exognatie);
                command.Parameters.AddWithValue("@me_mol1", ententepre.ClasseDentaireMolaire == EntentePrealable.en_Class.Class_I);
                command.Parameters.AddWithValue("@me_mol2", ententepre.ClasseDentaireMolaire == EntentePrealable.en_Class.Class_II);
                command.Parameters.AddWithValue("@me_mol3", ententepre.ClasseDentaireMolaire == EntentePrealable.en_Class.Class_III);
                command.Parameters.AddWithValue("@me_moltext", ententepre.ClasseDentaireMolaireTxt);
                command.Parameters.AddWithValue("@me_can1", ententepre.ClasseDentaireCanine == EntentePrealable.en_Class.Class_I);
                command.Parameters.AddWithValue("@me_can2", ententepre.ClasseDentaireCanine == EntentePrealable.en_Class.Class_II);
                command.Parameters.AddWithValue("@me_can3", ententepre.ClasseDentaireCanine == EntentePrealable.en_Class.Class_III);
                command.Parameters.AddWithValue("@me_cantext", ententepre.ClasseDentaireCanineTxt);
                command.Parameters.AddWithValue("@me_occludroit", (ententepre.occInverse == EntentePrealable.en_OccInverse.Droite) || (ententepre.occInverse == EntentePrealable.en_OccInverse.Droite_Et_Gauche));
                command.Parameters.AddWithValue("@me_occlugauche", (ententepre.occInverse == EntentePrealable.en_OccInverse.Gauche) || (ententepre.occInverse == EntentePrealable.en_OccInverse.Droite_Et_Gauche));
                command.Parameters.AddWithValue("@me_occluanter", ententepre.occInverse == EntentePrealable.en_OccInverse.Anterieur);
                command.Parameters.AddWithValue("@me_agnesie", ententepre.Agenesie);
                command.Parameters.AddWithValue("@me_dentincl", ententepre.DentsIncluseSurnum);
                command.Parameters.AddWithValue("@me_malpos", ententepre.Malposition);
                command.Parameters.AddWithValue("@me_dysharmo", ententepre.DDM);
                command.Parameters.AddWithValue("@me_dysharmodd", ententepre.DDD);
                command.Parameters.AddWithValue("@me_facteurfonc", ententepre.FacteurFonctionnel);
                command.Parameters.AddWithValue("@pat_objectif_trait2", ententepre.PlanDeTraitement);
                command.Parameters.AddWithValue("@pat_objectif_comm2", ententepre.Commentaires);
                
                
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


        public static void InsertPlanTraitementVisuel(PlanTraitementObject o)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();




            try
            {

             
                string selectQuery = @"insert into basediag_plantrmnt (id_diag, 
                                                                        ctrlkey, 
                                                                        resourcename, 
                                                                        x1, 
                                                                        y1, 
                                                                        x2, 
                                                                        y2)
                                        values (@id_diag, 
                                                @ctrlkey, 
                                                @resourcename, 
                                                @x1, 
                                                @y1, 
                                                @x2, 
                                                @y2)";




                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id_diag", o.IdResumclinique);
                command.Parameters.AddWithValue("@ctrlkey", o.CtrlKey);
                command.Parameters.AddWithValue("@resourcename", o.ResourceName);
                command.Parameters.AddWithValue("@x1", o.Point1.X);
                command.Parameters.AddWithValue("@y1", o.Point1.Y);
                command.Parameters.AddWithValue("@x2", o.Point2.X);
                command.Parameters.AddWithValue("@y2", o.Point2.Y);

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

        public static void DeleteFullPlanTraitementVisuel(ResumeClinique r)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();




            try
            {


                string selectQuery = @"Delete from basediag_plantrmnt where id_diag=@id_diag";




                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id_diag", r.Id);

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
        

        public static void InsertResumeClinique(ResumeClinique resume)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();



            try
            {

                string selectQueryId = "select max(Id)+1 as ID from basediag_diagnostic";
                MySqlCommand commandid = new MySqlCommand(selectQueryId, connection, transaction);
                object id = commandid.ExecuteScalar();
                if (id == DBNull.Value)
                    resume.Id = 1;
                else
                    resume.Id = Convert.ToInt32(id);





                string selectQuery = "insert into basediag_diagnostic (id, ";
                selectQuery += "        dateresume, ";
                selectQuery += "        id_patient, ";
                selectQuery += "        deviationlevreinf, ";
                selectQuery += "        deviationmenton, ";
                selectQuery += "        etageinf, ";
                selectQuery += "        souriredentaire, ";
                selectQuery += "        diagalveolaire, ";
                selectQuery += "        tnl, ";
                selectQuery += "        tnlDroit, ";
                selectQuery += "        tnlGauche, ";
                selectQuery += "        decalageinterincisivedg, ";
                selectQuery += "        decalageinterincisivehb, ";
                selectQuery += "        classecand, ";
                selectQuery += "        classecang, ";
                selectQuery += "        classemold, ";
                selectQuery += "        classemolg, ";
                selectQuery += "        senstransvmand, ";
                selectQuery += "        senstransvmax, ";
                selectQuery += "        diagmand, ";
                selectQuery += "        diagmax, ";
                selectQuery += "        occlusioninverse, ";
                selectQuery += "        SautArticule, ";

                selectQuery += "        occlusionvalue, ";
                selectQuery += "        occlusionface, ";
                selectQuery += "        interpositonlingual, ";
                selectQuery += "        formearcade, ";
                selectQuery += "        surplombvalue, ";
                selectQuery += "        freinlabial, ";
                selectQuery += "        freinlingual, ";
                selectQuery += "        languebasse, ";
                selectQuery += "        ddd, ";
                selectQuery += "        ddm, ";
                selectQuery += "        Diasteme, ";
                
                selectQuery += "        souriregingivalsup, ";
                selectQuery += "        labialvalue, ";
                selectQuery += "        gingivalinfvalue, ";
                selectQuery += "        gingivalsupvalue, ";
                selectQuery += "        souriregingivalinf, ";
                selectQuery += "        sourirelabial, ";
                selectQuery += "        inclinaisonincisivesupvalue, ";
                selectQuery += "        incisivesuperieur, ";
                selectQuery += "        menton, ";
                selectQuery += "        levreinferieur, ";
                selectQuery += "        levresuperieur, ";
                selectQuery += "        senssagittalmandbasal, ";
                selectQuery += "        senssagittalmaxbasal, ";
                selectQuery += "        incisiveinferieur, ";
                selectQuery += "        senssagittal, ";
                selectQuery += "        sensvertical, ";
                selectQuery += "        evolgermesdesdentssur, ";
                selectQuery += "        evolgermesdesdents, ";
                selectQuery += "        dentsdesagesse, ";
                selectQuery += "        dentssurnumeraires, ";
                selectQuery += "        dentsincluses, ";
                selectQuery += "        agenesie, ";
                selectQuery += "        Controle, ";
                selectQuery += "        img_rad_face, ";
                selectQuery += "        img_rad_pano, ";
                selectQuery += "        img_rad_profile, ";
                selectQuery += "        img_ext_face, ";
                selectQuery += "        img_ext_profile, ";
                selectQuery += "        img_ext_profile_sourire, ";
                selectQuery += "        img_ext_face_sourire, ";
                selectQuery += "        img_ext_sourire, ";
                selectQuery += "        img_int_droit, ";
                selectQuery += "        img_int_surplomb, ";
                selectQuery += "        img_int_face, ";
                selectQuery += "        img_int_gauche, ";
                selectQuery += "        img_int_max, ";
                selectQuery += "        img_int_man, ";
                selectQuery += "        img_moul_droit, ";
                selectQuery += "        img_moul_face, ";
                selectQuery += "        img_moul_gauche, ";
                selectQuery += "        img_moul_max, ";
                selectQuery += "        img_moul_man,";
                selectQuery += "        laterodeviation,"; 
                selectQuery += "        formerespiration,";
                selectQuery += "        LISTOFPOINTSAN1,";
                selectQuery += "        LISTOFPOINTSAN2,";
                selectQuery += "        LISTOFPOINTSAN6,";
                selectQuery += "        LISTOFPOINTSAN7,";
                selectQuery += "        LISTOFPOINTSANOCCD,";
                selectQuery += "        LISTOFPOINTSANOCCF,";
                selectQuery += "        LISTOFPOINTSANOCCG,";
                selectQuery += "        LISTOFPOINTSSOURIRE,";
                selectQuery += "        SYNCRO_X,";
                selectQuery += "        SYNCRO_Y,";
                selectQuery += "        SYNCRO_ZOOM,";
                selectQuery += "        SYNCRO_ROTATION,";
                selectQuery += "        NoTaquets,";
                selectQuery += "        NoMvts,spp,";
                selectQuery += "        ID_MODELE_ENTENTE";
                selectQuery += "        )";
                selectQuery += " values (@id, ";
                selectQuery += "        @dateresume, ";
                selectQuery += "        @id_patient, ";
                selectQuery += "        @deviationlevreinf, ";
                selectQuery += "        @deviationmenton, ";
                selectQuery += "        @etageinf, ";
                selectQuery += "        @souriredentaire, ";
                selectQuery += "        @diagalveolaire, ";
                selectQuery += "        @tnl, ";
                selectQuery += "        @tnlDroit, ";
                selectQuery += "        @tnlGauche, ";
                selectQuery += "        @decalageinterincisivedg, ";
                selectQuery += "        @decalageinterincisivehb, ";
                selectQuery += "        @classecand, ";
                selectQuery += "        @classecang, ";
                selectQuery += "        @classemold, ";
                selectQuery += "        @classemolg, ";
                selectQuery += "        @senstransvmand, ";
                selectQuery += "        @senstransvmax, ";
                selectQuery += "        @diagmand, ";
                selectQuery += "        @diagmax, ";
                selectQuery += "        @occlusioninverse, ";
                selectQuery += "        @SautArticule, ";
                selectQuery += "        @occlusionvalue, ";
                selectQuery += "        @occlusionface, ";
                selectQuery += "        @interpositonlingual, ";
                selectQuery += "        @formearcade, ";
                selectQuery += "        @surplombvalue, ";
                selectQuery += "        @freinlabial, ";
                selectQuery += "        @freinlingual, ";
                selectQuery += "        @languebasse, ";
                selectQuery += "        @ddd, ";
                selectQuery += "        @ddm, ";
                selectQuery += "        @Diasteme, ";
                selectQuery += "        @souriregingivalsup, ";
                selectQuery += "        @labialvalue, ";
                selectQuery += "        @gingivalinfvalue, ";
                selectQuery += "        @gingivalsupvalue, ";
                selectQuery += "        @souriregingivalinf, ";
                selectQuery += "        @sourirelabial, ";
                selectQuery += "        @inclinaisonincisivesupvalue, ";
                selectQuery += "        @incisivesuperieur, ";
                selectQuery += "        @menton, ";
                selectQuery += "        @levreinferieur, ";
                selectQuery += "        @levresuperieur, ";
                selectQuery += "        @senssagittalmandbasal, ";
                selectQuery += "        @senssagittalmaxbasal, ";
                selectQuery += "        @incisiveinferieur, ";
                selectQuery += "        @senssagittal, ";
                selectQuery += "        @sensvertical, ";
                selectQuery += "        @evolgermesdesdentssur, ";
                selectQuery += "        @evolgermesdesdents, ";
                selectQuery += "        @dentsdesagesse, ";
                selectQuery += "        @dentssurnumeraires, ";
                selectQuery += "        @dentsincluses, ";
                selectQuery += "        @agenesie, ";
                selectQuery += "        @Controle, ";
                selectQuery += "        @img_rad_face, ";
                selectQuery += "        @img_rad_pano, ";
                selectQuery += "        @img_rad_profile, ";
                selectQuery += "        @img_ext_face, ";
                selectQuery += "        @img_ext_profile, ";
                selectQuery += "        @img_ext_profile_sourire, ";
                selectQuery += "        @img_ext_face_sourire, ";
                selectQuery += "        @img_ext_sourire, ";
                selectQuery += "        @img_int_droit, ";
                selectQuery += "        @img_int_surplomb, ";
                selectQuery += "        @img_int_face, ";
                selectQuery += "        @img_int_gauche, ";
                selectQuery += "        @img_int_max, ";
                selectQuery += "        @img_int_man, ";
                selectQuery += "        @img_moul_droit, ";
                selectQuery += "        @img_moul_face, ";
                selectQuery += "        @img_moul_gauche, ";
                selectQuery += "        @img_moul_max, ";
                selectQuery += "        @img_moul_man,";
                selectQuery += "        @laterodeviation,";
                selectQuery += "        @formerespiration,";
                selectQuery += "        @LISTOFPOINTSAN1,";
                selectQuery += "        @LISTOFPOINTSAN2,";
                selectQuery += "        @LISTOFPOINTSAN6,";
                selectQuery += "        @LISTOFPOINTSAN7,";
                selectQuery += "        @LISTOFPOINTSANOCCD,";
                selectQuery += "        @LISTOFPOINTSANOCCF,";
                selectQuery += "        @LISTOFPOINTSANOCCG,";
                selectQuery += "        @LISTOFPOINTSSOURIRE,";                
                selectQuery += "        @SYNCRO_X,";
                selectQuery += "        @SYNCRO_Y,";
                selectQuery += "        @SYNCRO_ZOOM,";
                selectQuery += "        @SYNCRO_ROTATION,";
                selectQuery += "        @NoTaquets,";
                selectQuery += "        @NoMvts,@spp,";
                selectQuery += "        @ID_MODELE_ENTENTE";
                selectQuery += " )";




                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@ID", resume.Id);
                command.Parameters.AddWithValue("@DateResume", resume.dateResume);
                command.Parameters.AddWithValue("@ID_PATIENT", resume.patient.Id);


                command.Parameters.AddWithValue("@DeviationLevreInf", resume.DeviationLevreInf);
                command.Parameters.AddWithValue("@DeviationMenton", resume.DeviationMenton);
                command.Parameters.AddWithValue("@EtageInf", resume.EtageInf);


                command.Parameters.AddWithValue("@sourireDentaire", resume.sourireDentaire);
                command.Parameters.AddWithValue("@DiagAlveolaire", resume.DiagAlveolaire);
                command.Parameters.AddWithValue("@TNL", DBNull.Value);
                command.Parameters.AddWithValue("@tnlDroit", resume.TNLDroit);
                command.Parameters.AddWithValue("@tnlGauche", resume.TNLGauche);
                command.Parameters.AddWithValue("@DecalageInterIncisiveDG", resume.DecalageInterIncisiveHaut);
                command.Parameters.AddWithValue("@DecalageInterIncisiveHB", resume.DecalageInterIncisiveBas);


                command.Parameters.AddWithValue("@ClasseCanD", resume.ClasseCanD);
                command.Parameters.AddWithValue("@ClasseCanG", resume.ClasseCanG);
                command.Parameters.AddWithValue("@ClasseMolD", resume.ClasseMolD);
                command.Parameters.AddWithValue("@ClasseMolG", resume.ClasseMolG);
                command.Parameters.AddWithValue("@SensTransvMand", resume.SensTransvMand);
                command.Parameters.AddWithValue("@SensTransvMax", resume.SensTransvMax);
                command.Parameters.AddWithValue("@DiagMand", resume.DiagMand);
                command.Parameters.AddWithValue("@DiagMax", resume.DiagMax);
                command.Parameters.AddWithValue("@OcclusionInverse", resume.OcclusionInverse);
                command.Parameters.AddWithValue("@SautArticule", resume.SautArticule);
                command.Parameters.AddWithValue("@OcclusionValue", resume.OcclusionValue);
                command.Parameters.AddWithValue("@OcclusionFace", resume.OcclusionFace);

                
                command.Parameters.AddWithValue("@interpositonlingual", resume.InterpositonLingual);
                command.Parameters.AddWithValue("@FormeArcade", resume.FormeArcade);
                command.Parameters.AddWithValue("@SurplombValue", resume.SurplombValue);
                command.Parameters.AddWithValue("@FreinLabial", resume.FreinLabial);
                command.Parameters.AddWithValue("@FreinLingual", resume.FreinLingual);
                command.Parameters.AddWithValue("@LangueBasse", resume.LangueBasse);
                command.Parameters.AddWithValue("@DDD", resume.DDD);
                command.Parameters.AddWithValue("@DDM", resume.DDM);
                command.Parameters.AddWithValue("@Diasteme", resume.Diasteme);
                

                command.Parameters.AddWithValue("@SourireGingivalSup", resume.SourireGingivalSup);
                command.Parameters.AddWithValue("@LabialValue", resume.LabialValue);
                command.Parameters.AddWithValue("@GingivalInfValue", resume.GingivalInfValue);
                command.Parameters.AddWithValue("@GingivalSupValue", resume.GingivalSupValue);
                command.Parameters.AddWithValue("@SourireGingivalInf", resume.SourireGingivalInf);
                command.Parameters.AddWithValue("@SourireLabial", resume.SourireLabial);


                command.Parameters.AddWithValue("@InclinaisonIncisiveSupValue", resume.InclinaisonIncisiveSupValue);
                command.Parameters.AddWithValue("@IncisiveSuperieur", resume.IncisiveSuperieur);
                command.Parameters.AddWithValue("@Menton", resume.Menton);
                command.Parameters.AddWithValue("@LevreInferieur", resume.LevreInferieur);
                command.Parameters.AddWithValue("@LevreSuperieur", resume.LevreSuperieur);


                command.Parameters.AddWithValue("@SensSagittalMandBasal", resume.SensSagittalMandBasal);
                command.Parameters.AddWithValue("@SensSagittalMaxBasal", resume.SensSagittalMaxBasal);
                command.Parameters.AddWithValue("@IncisiveInferieur", resume.IncisiveInferieur);
                command.Parameters.AddWithValue("@SensSagittal", resume.SensSagittal);
                command.Parameters.AddWithValue("@SensVertical", resume.SensVertical);
                command.Parameters.AddWithValue("@spp", resume.SPP_SPA);

                command.Parameters.AddWithValue("@EvolGermesDesDentsSur", resume.EvolGermesDesDentsSur);
                command.Parameters.AddWithValue("@EvolGermesDesDents", resume.EvolGermesDesDents);
                command.Parameters.AddWithValue("@DentsDeSagesse", resume.DentsDeSagesse);
                command.Parameters.AddWithValue("@NoTaquets", resume.NoTaquets);
                command.Parameters.AddWithValue("@NoMvts", resume.NoMvts);
                command.Parameters.AddWithValue("@DentsSurnumeraires", resume.DentsSurnumeraires);
                command.Parameters.AddWithValue("@DentsIncluses", resume.DentsIncluses);
                command.Parameters.AddWithValue("@Agenesie", resume.Agenesie);
                command.Parameters.AddWithValue("@Controle", resume.Controle);
                command.Parameters.AddWithValue("@formerespiration", resume.FormeRespiration);
                command.Parameters.AddWithValue("@laterodeviation", resume.Laterodeviation);

                command.Parameters.AddWithValue("@Img_Rad_Face", resume.Img_Rad_Face == null ? "" : resume.Img_Rad_Face.Replace(basePatient.RepertoireImage, ""));
                command.Parameters.AddWithValue("@Img_Rad_Pano", resume.Img_Rad_Pano == null ? "" : resume.Img_Rad_Pano.Replace(basePatient.RepertoireImage, ""));
                command.Parameters.AddWithValue("@Img_Rad_Profile", resume.Img_Rad_Profile == null ? "" : resume.Img_Rad_Profile.Replace(basePatient.RepertoireImage, ""));
                command.Parameters.AddWithValue("@Img_Ext_Face", resume.Img_Ext_Face == null ? "" : resume.Img_Ext_Face.Replace(basePatient.RepertoireImage, ""));
                command.Parameters.AddWithValue("@Img_Ext_Profile", resume.Img_Ext_Profile == null ? "" : resume.Img_Ext_Profile.Replace(basePatient.RepertoireImage, ""));
                command.Parameters.AddWithValue("@Img_Ext_Profile_Sourire", resume.Img_Ext_Profile_Sourire == null ? "" : resume.Img_Ext_Profile_Sourire.Replace(basePatient.RepertoireImage, ""));
                command.Parameters.AddWithValue("@Img_Ext_Face_Sourire", resume.Img_Ext_Face_Sourire == null ? "" : resume.Img_Ext_Face_Sourire.Replace(basePatient.RepertoireImage, ""));
                command.Parameters.AddWithValue("@Img_Ext_Sourire", resume.Img_Ext_Sourire == null ? "" : resume.Img_Ext_Sourire.Replace(basePatient.RepertoireImage, ""));
                command.Parameters.AddWithValue("@Img_Int_Droit", resume.Img_Int_Droit == null ? "" : resume.Img_Int_Droit.Replace(basePatient.RepertoireImage, ""));
                command.Parameters.AddWithValue("@Img_Int_SurPlomb", resume.Img_Int_SurPlomb == null ? "" : resume.Img_Int_SurPlomb.Replace(basePatient.RepertoireImage, ""));
                command.Parameters.AddWithValue("@Img_Int_Face", resume.Img_Int_Face == null ? "" : resume.Img_Int_Face.Replace(basePatient.RepertoireImage, ""));
                command.Parameters.AddWithValue("@Img_Int_Gauche", resume.Img_Int_Gauche == null ? "" : resume.Img_Int_Gauche.Replace(basePatient.RepertoireImage, ""));
                command.Parameters.AddWithValue("@Img_Int_Max", resume.Img_Int_Max == null ? "" : resume.Img_Int_Max.Replace(basePatient.RepertoireImage, ""));
                command.Parameters.AddWithValue("@Img_Int_Man", resume.Img_Int_Man == null ? "" : resume.Img_Int_Man.Replace(basePatient.RepertoireImage, ""));
                command.Parameters.AddWithValue("@Img_Moul_Droit", resume.Img_Moul_Droit == null ? "" : resume.Img_Moul_Droit.Replace(basePatient.RepertoireImage, ""));
                command.Parameters.AddWithValue("@Img_Moul_Face", resume.Img_Moul_Face == null ? "" : resume.Img_Moul_Face.Replace(basePatient.RepertoireImage, ""));
                command.Parameters.AddWithValue("@Img_Moul_Gauche", resume.Img_Moul_Gauche == null ? "" : resume.Img_Moul_Gauche.Replace(basePatient.RepertoireImage, ""));
                command.Parameters.AddWithValue("@Img_Moul_Max", resume.Img_Moul_Max == null ? "" : resume.Img_Moul_Max.Replace(basePatient.RepertoireImage, ""));
                command.Parameters.AddWithValue("@Img_Moul_Man", resume.Img_Moul_Man == null ? "" : resume.Img_Moul_Man.Replace(basePatient.RepertoireImage, ""));
                command.Parameters.AddWithValue("@ID_MODELE_ENTENTE", resume.IdModelEntente);



                MemoryStream stream = new MemoryStream();
                BinaryFormatter serializer = new BinaryFormatter();

                serializer.Serialize(stream, resume.LstPtAnalyse1);
                stream.Position = 0;
                command.Parameters.Add("@LISTOFPOINTSAN1", stream.ToArray());


                serializer.Serialize(stream, resume.LstPtAnalyse2);
                stream.Position = 0;
                command.Parameters.Add("@LISTOFPOINTSAN2", stream.ToArray());


                serializer.Serialize(stream, resume.LstPtAnalyse62);
                stream.Position = 0;
                command.Parameters.Add("@LISTOFPOINTSAN6", stream.ToArray());


                serializer.Serialize(stream, resume.LstPtAnalyse7);
                stream.Position = 0;
                command.Parameters.Add("@LISTOFPOINTSAN7", stream.ToArray());

                serializer.Serialize(stream, resume.LstPtAnalyseOccD);
                stream.Position = 0;
                command.Parameters.Add("@LISTOFPOINTSANOCCD", stream.ToArray());

                serializer.Serialize(stream, resume.LstPtAnalyseOccF);
                stream.Position = 0;
                command.Parameters.Add("@LISTOFPOINTSANOCCF", stream.ToArray());

                serializer.Serialize(stream, resume.LstPtAnalyseOccG);
                stream.Position = 0;
                command.Parameters.Add("@LISTOFPOINTSANOCCG", stream.ToArray());

                serializer.Serialize(stream, resume.LstPtAnalyseSourire);
                stream.Position = 0;
                command.Parameters.Add("@LISTOFPOINTSSOURIRE", stream.ToArray());



                command.Parameters.AddWithValue("@SYNCRO_X", resume.synchrooffset.X);
                command.Parameters.AddWithValue("@SYNCRO_Y", resume.synchrooffset.Y);
                command.Parameters.AddWithValue("@SYNCRO_ZOOM", resume.synchrozoom);
                command.Parameters.AddWithValue("@SYNCRO_ROTATION", resume.synchroangle);


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
        

        public static void UpdateIdModeleEntente(int IdResume, int IdModele)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();




            try
            {

                string selectQuery = "update basediag_diagnostic set ";
                selectQuery += "        ID_MODELE_ENTENTE = @ID_MODELE_ENTENTE ";
                selectQuery += "        where id=@id ";
                



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@ID", IdResume);
                command.Parameters.AddWithValue("@ID_MODELE_ENTENTE", IdModele);
                
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
        
        public static void UpdateResumeClinique(ResumeClinique resume)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();




            try
            {


                string selectQuery = "update basediag_diagnostic";
                selectQuery += " set dateresume = @dateresume,";
                selectQuery += "    id_patient = @id_patient,";
                selectQuery += "    deviationlevreinf = @deviationlevreinf,";
                selectQuery += "    deviationmenton = @deviationmenton,";
                selectQuery += "    etageinf = @etageinf,";
                selectQuery += "    souriredentaire = @souriredentaire,";
                selectQuery += "    diagalveolaire = @diagalveolaire,";
                selectQuery += "    tnl = @tnl,";
                selectQuery += "    tnlDroit = @tnlDroit,";
                selectQuery += "    tnlGauche = @tnlGauche,";
                selectQuery += "    decalageinterincisivedg = @decalageinterincisivedg,";
                selectQuery += "    decalageinterincisivehb = @decalageinterincisivehb,";
                selectQuery += "    classecand = @classecand,";
                selectQuery += "    classecang = @classecang,";
                selectQuery += "    classemold = @classemold,";
                selectQuery += "    classemolg = @classemolg,";
                selectQuery += "    senstransvmand = @senstransvmand,";
                selectQuery += "    senstransvmax = @senstransvmax,";
                selectQuery += "    diagmand = @diagmand,";
                selectQuery += "    diagmax = @diagmax,";
                selectQuery += "    occlusioninverse = @occlusioninverse,";
                selectQuery += "    SautArticule = @SautArticule,";                
                selectQuery += "    occlusionvalue = @occlusionvalue,";
                selectQuery += "    occlusionface = @occlusionface,";
                selectQuery += "    interpositonlingual = @interpositonlingual,";
                selectQuery += "    formearcade = @formearcade,";
                selectQuery += "    surplombvalue = @surplombvalue,";
                selectQuery += "    freinlabial = @freinlabial,";
                selectQuery += "    freinlingual = @freinlingual,";
                selectQuery += "    languebasse = @languebasse,";
                selectQuery += "    ddd = @ddd,";
                selectQuery += "    Diasteme = @Diasteme,";
                selectQuery += "    ddm = @ddm,";
                selectQuery += "    souriregingivalsup = @souriregingivalsup,";
                selectQuery += "    labialvalue = @labialvalue,";
                selectQuery += "    gingivalinfvalue = @gingivalinfvalue,";
                selectQuery += "    gingivalsupvalue = @gingivalsupvalue,";
                selectQuery += "    souriregingivalinf = @souriregingivalinf,";
                selectQuery += "    sourirelabial = @sourirelabial,";
                selectQuery += "    inclinaisonincisivesupvalue = @inclinaisonincisivesupvalue,";
                selectQuery += "    incisivesuperieur = @incisivesuperieur,";
                selectQuery += "    menton = @menton,";
                selectQuery += "    levreinferieur = @levreinferieur,";
                selectQuery += "    levresuperieur = @levresuperieur,";
                selectQuery += "    senssagittalmandbasal = @senssagittalmandbasal,";
                selectQuery += "    senssagittalmaxbasal = @senssagittalmaxbasal,";
                selectQuery += "    incisiveinferieur = @incisiveinferieur,";
                selectQuery += "    senssagittal = @senssagittal,";
                selectQuery += "    sensvertical = @sensvertical,";
                selectQuery += "    evolgermesdesdentssur = @evolgermesdesdentssur,";
                selectQuery += "    evolgermesdesdents = @evolgermesdesdents,";
                selectQuery += "    dentsdesagesse = @dentsdesagesse,";
                selectQuery += "    dentssurnumeraires = @dentssurnumeraires,";
                selectQuery += "    dentsincluses = @dentsincluses,";
                selectQuery += "    agenesie = @agenesie,";
                selectQuery += "    Controle = @Controle,";
                selectQuery += "    img_rad_face = @img_rad_face,";
                selectQuery += "    img_rad_pano = @img_rad_pano,";
                selectQuery += "    img_rad_profile = @img_rad_profile,";
                selectQuery += "    img_ext_face = @img_ext_face,";
                selectQuery += "    img_ext_profile = @img_ext_profile,";
                selectQuery += "    img_ext_profile_sourire = @img_ext_profile_sourire,";
                selectQuery += "    img_ext_face_sourire = @img_ext_face_sourire,";
                selectQuery += "    img_ext_sourire = @img_ext_sourire,";
                selectQuery += "    img_int_droit = @img_int_droit,";
                selectQuery += "    img_int_surplomb = @img_int_surplomb,";
                selectQuery += "    img_int_face = @img_int_face,";
                selectQuery += "    img_int_gauche = @img_int_gauche,";
                selectQuery += "    img_int_max = @img_int_max,";
                selectQuery += "    img_int_man = @img_int_man,";
                selectQuery += "    img_moul_droit = @img_moul_droit,";
                selectQuery += "    img_moul_face = @img_moul_face,";
                selectQuery += "    img_moul_gauche = @img_moul_gauche,";
                selectQuery += "    img_moul_max = @img_moul_max,";
                selectQuery += "    img_moul_man = @img_moul_man,";
                selectQuery += "    laterodeviation = @laterodeviation,";
                selectQuery += "    formerespiration = @formerespiration,";
                selectQuery += "    LISTOFPOINTSAN1 = @LISTOFPOINTSAN1,";
                selectQuery += "    LISTOFPOINTSAN2 = @LISTOFPOINTSAN2,";
                selectQuery += "    LISTOFPOINTSAN6 = @LISTOFPOINTSAN6,";
                selectQuery += "    LISTOFPOINTSAN7 = @LISTOFPOINTSAN7,";
                selectQuery += "    LISTOFPOINTSANOCCD = @LISTOFPOINTSANOCCD,";
                selectQuery += "    LISTOFPOINTSANOCCF = @LISTOFPOINTSANOCCF,";
                selectQuery += "    LISTOFPOINTSANOCCG = @LISTOFPOINTSANOCCG,";
                selectQuery += "    LISTOFPOINTSSOURIRE = @LISTOFPOINTSSOURIRE,";
                selectQuery += "    SYNCRO_X = @SYNCRO_X,";
                selectQuery += "    SYNCRO_Y = @SYNCRO_Y,";
                selectQuery += "    SYNCRO_ZOOM = @SYNCRO_ZOOM,";
                selectQuery += "    SYNCRO_ROTATION = @SYNCRO_ROTATION,";
                selectQuery += "    NoTaquets = @NoTaquets,";
                selectQuery += "    NoMvts = @NoMvts,";
                selectQuery += "    ID_MODELE_ENTENTE = @ID_MODELE_ENTENTE,";
                selectQuery += "    SPP = @SPP";
                selectQuery += " where (id = @id)";





                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@ID", resume.Id);
                command.Parameters.AddWithValue("@DateResume", resume.dateResume);
                command.Parameters.AddWithValue("@ID_PATIENT", resume.IdPatient);


                command.Parameters.AddWithValue("@DeviationLevreInf", resume.DeviationLevreInf);
                command.Parameters.AddWithValue("@DeviationMenton", resume.DeviationMenton);
                command.Parameters.AddWithValue("@EtageInf", resume.EtageInf);


                command.Parameters.AddWithValue("@sourireDentaire", resume.sourireDentaire);
                command.Parameters.AddWithValue("@DiagAlveolaire", resume.DiagAlveolaire);
                command.Parameters.AddWithValue("@TNL", DBNull.Value);
                command.Parameters.AddWithValue("@tnlDroit", resume.TNLDroit);
                command.Parameters.AddWithValue("@tnlGauche", resume.TNLGauche);
                command.Parameters.AddWithValue("@DecalageInterIncisiveDG", resume.DecalageInterIncisiveHaut);
                command.Parameters.AddWithValue("@DecalageInterIncisiveHB", resume.DecalageInterIncisiveBas);
                

                command.Parameters.AddWithValue("@ClasseCanD", resume.ClasseCanD);
                command.Parameters.AddWithValue("@ClasseCanG", resume.ClasseCanG);
                command.Parameters.AddWithValue("@ClasseMolD", resume.ClasseMolD);
                command.Parameters.AddWithValue("@ClasseMolG", resume.ClasseMolG);
                command.Parameters.AddWithValue("@SensTransvMand", resume.SensTransvMand);
                command.Parameters.AddWithValue("@SensTransvMax", resume.SensTransvMax);
                command.Parameters.AddWithValue("@DiagMand", resume.DiagMand);
                command.Parameters.AddWithValue("@DiagMax", resume.DiagMax);
                command.Parameters.AddWithValue("@OcclusionInverse", resume.OcclusionInverse);
                command.Parameters.AddWithValue("@SautArticule", resume.SautArticule);
                command.Parameters.AddWithValue("@OcclusionValue", resume.OcclusionValue);
                command.Parameters.AddWithValue("@OcclusionFace", resume.OcclusionFace);

                command.Parameters.AddWithValue("@SPP", resume.SPP_SPA);
                command.Parameters.AddWithValue("@interpositonlingual", resume.InterpositonLingual);
                command.Parameters.AddWithValue("@FormeArcade", resume.FormeArcade);
                command.Parameters.AddWithValue("@SurplombValue", resume.SurplombValue);
                command.Parameters.AddWithValue("@FreinLabial", resume.FreinLabial);
                command.Parameters.AddWithValue("@FreinLingual", resume.FreinLingual);
                command.Parameters.AddWithValue("@LangueBasse", resume.LangueBasse);
                command.Parameters.AddWithValue("@DDD", resume.DDD);
                command.Parameters.AddWithValue("@Diasteme", resume.Diasteme);
                command.Parameters.AddWithValue("@DDM", resume.DDM);


                command.Parameters.AddWithValue("@SourireGingivalSup", resume.SourireGingivalSup);
                command.Parameters.AddWithValue("@LabialValue", resume.LabialValue);
                command.Parameters.AddWithValue("@GingivalInfValue", resume.GingivalInfValue);
                command.Parameters.AddWithValue("@GingivalSupValue", resume.GingivalSupValue);
                command.Parameters.AddWithValue("@SourireGingivalInf", resume.SourireGingivalInf);
                command.Parameters.AddWithValue("@SourireLabial", resume.SourireLabial);


                command.Parameters.AddWithValue("@InclinaisonIncisiveSupValue", resume.InclinaisonIncisiveSupValue);
                command.Parameters.AddWithValue("@IncisiveSuperieur", resume.IncisiveSuperieur);
                command.Parameters.AddWithValue("@Menton", resume.Menton);
                command.Parameters.AddWithValue("@LevreInferieur", resume.LevreInferieur);
                command.Parameters.AddWithValue("@LevreSuperieur", resume.LevreSuperieur);


                command.Parameters.AddWithValue("@SensSagittalMandBasal", resume.SensSagittalMandBasal);
                command.Parameters.AddWithValue("@SensSagittalMaxBasal", resume.SensSagittalMaxBasal);
                command.Parameters.AddWithValue("@IncisiveInferieur", resume.IncisiveInferieur);
                command.Parameters.AddWithValue("@SensSagittal", resume.SensSagittal);
                command.Parameters.AddWithValue("@SensVertical", resume.SensVertical);

                command.Parameters.AddWithValue("@NoTaquets", resume.NoTaquets);
                command.Parameters.AddWithValue("@NoMvts", resume.NoMvts);

                command.Parameters.AddWithValue("@EvolGermesDesDentsSur", resume.EvolGermesDesDentsSur);
                command.Parameters.AddWithValue("@EvolGermesDesDents", resume.EvolGermesDesDents);
                command.Parameters.AddWithValue("@DentsDeSagesse", resume.DentsDeSagesse);
                command.Parameters.AddWithValue("@DentsSurnumeraires", resume.DentsSurnumeraires);
                command.Parameters.AddWithValue("@DentsIncluses", resume.DentsIncluses);
                command.Parameters.AddWithValue("@Agenesie", resume.Agenesie);
                command.Parameters.AddWithValue("@Controle", resume.Controle);


                command.Parameters.AddWithValue("@Img_Rad_Face", resume.Img_Rad_Face);
                command.Parameters.AddWithValue("@Img_Rad_Pano", resume.Img_Rad_Pano);
                command.Parameters.AddWithValue("@Img_Rad_Profile", resume.Img_Rad_Profile);
                command.Parameters.AddWithValue("@Img_Ext_Face", resume.Img_Ext_Face);
                command.Parameters.AddWithValue("@Img_Ext_Profile", resume.Img_Ext_Profile);
                command.Parameters.AddWithValue("@Img_Ext_Profile_Sourire", resume.Img_Ext_Profile_Sourire);
                command.Parameters.AddWithValue("@Img_Ext_Face_Sourire", resume.Img_Ext_Face_Sourire);
                command.Parameters.AddWithValue("@Img_Ext_Sourire", resume.Img_Ext_Sourire);
                command.Parameters.AddWithValue("@Img_Int_Droit", resume.Img_Int_Droit);
                command.Parameters.AddWithValue("@Img_Int_SurPlomb", resume.Img_Int_SurPlomb);
                command.Parameters.AddWithValue("@Img_Int_Face", resume.Img_Int_Face);
                command.Parameters.AddWithValue("@Img_Int_Gauche", resume.Img_Int_Gauche);
                command.Parameters.AddWithValue("@Img_Int_Max", resume.Img_Int_Max);
                command.Parameters.AddWithValue("@Img_Int_Man", resume.Img_Int_Man);
                command.Parameters.AddWithValue("@Img_Moul_Droit", resume.Img_Moul_Droit);
                command.Parameters.AddWithValue("@Img_Moul_Face", resume.Img_Moul_Face);
                command.Parameters.AddWithValue("@Img_Moul_Gauche", resume.Img_Moul_Gauche);
                command.Parameters.AddWithValue("@Img_Moul_Max", resume.Img_Moul_Max);
                command.Parameters.AddWithValue("@Img_Moul_Man", resume.Img_Moul_Man);
                command.Parameters.AddWithValue("@laterodeviation", resume.Laterodeviation);
                command.Parameters.AddWithValue("@formerespiration", resume.FormeRespiration);
                
                
                command.Parameters.AddWithValue("@ID_MODELE_ENTENTE", resume.IdModelEntente);

                MemoryStream stream = new MemoryStream();
                BinaryFormatter serializer = new BinaryFormatter();

                serializer.Serialize(stream, resume.LstPtAnalyse1);
                stream.Position = 0;
                command.Parameters.Add("@LISTOFPOINTSAN1", stream.ToArray());


                serializer.Serialize(stream, resume.LstPtAnalyse2);
                stream.Position = 0;
                command.Parameters.Add("@LISTOFPOINTSAN2", stream.ToArray());


                serializer.Serialize(stream, resume.LstPtAnalyse62);
                stream.Position = 0;
                command.Parameters.Add("@LISTOFPOINTSAN6", stream.ToArray());


                serializer.Serialize(stream, resume.LstPtAnalyse7);
                stream.Position = 0;
                command.Parameters.Add("@LISTOFPOINTSAN7", stream.ToArray());

                serializer.Serialize(stream, resume.LstPtAnalyseOccD);
                stream.Position = 0;
                command.Parameters.Add("@LISTOFPOINTSANOCCD", stream.ToArray());

                serializer.Serialize(stream, resume.LstPtAnalyseOccF);
                stream.Position = 0;
                command.Parameters.Add("@LISTOFPOINTSANOCCF", stream.ToArray());

                serializer.Serialize(stream, resume.LstPtAnalyseOccG);
                stream.Position = 0;
                command.Parameters.Add("@LISTOFPOINTSANOCCG", stream.ToArray());

                serializer.Serialize(stream, resume.LstPtAnalyseSourire);
                stream.Position = 0;
                command.Parameters.Add("@LISTOFPOINTSSOURIRE", stream.ToArray());


                

                command.Parameters.AddWithValue("@SYNCRO_X", resume.synchrooffset.X);
                command.Parameters.AddWithValue("@SYNCRO_Y", resume.synchrooffset.Y);
                command.Parameters.AddWithValue("@SYNCRO_ZOOM", resume.synchrozoom);
                command.Parameters.AddWithValue("@SYNCRO_ROTATION", resume.synchroangle);


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


        public static DataTable GetPlanTraitementObjects(int IdResume)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = @"select id_diag, 
                                               ctrlkey, 
                                               resourcename, 
                                               x1, 
                                               y1, 
                                               x2, 
                                               y2
                                        from basediag_plantrmnt
                                        where id_diag=@id_diag";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id_diag", IdResume);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                return dt;

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


        public static DataTable getResumesCliniqueByPatient(int IdPatient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = @"select id, 
                               dateresume, 
                               id_patient, 
                               deviationlevreinf, 
                               deviationmenton, 
                               etageinf, 
                               souriredentaire, 
                               diagalveolaire, 
                               tnl, 
                               decalageinterincisivedg, 
                               decalageinterincisivehb, 
                               classecand, 
                               classecang, 
                               classemold, 
                               classemolg, 
                               senstransvmand, 
                               senstransvmax, 
                               diagmand, 
                               diagmax, 
                               occlusioninverse, 
                               occlusionvalue, 
                               occlusionface, 
                               interpositonlingual, 
                               formearcade, 
                               surplombvalue, 
                               freinlabial, 
                               languebasse, 
                               ddd, 
                               ddm, 
                               souriregingivalsup, 
                               labialvalue, 
                               gingivalinfvalue, 
                               gingivalsupvalue, 
                               souriregingivalinf, 
                               sourirelabial, 
                               inclinaisonincisivesupvalue, 
                               incisivesuperieur, 
                               menton, 
                               levreinferieur, 
                               levresuperieur, 
                               senssagittalmandbasal, 
                               senssagittalmaxbasal, 
                               incisiveinferieur, 
                               senssagittal, 
                               sensvertical, 
                               laterodeviation, 
                               formerespiration,
                               evolgermesdesdentssur, 
                               evolgermesdesdents, 
                               dentsdesagesse, 
                               dentssurnumeraires, 
                               dentsincluses, 
                               agenesie, 
                               img_rad_face, 
                               img_rad_pano, 
                               img_rad_profile, 
                               img_ext_face, 
                               img_ext_profile, 
                               img_ext_profile_sourire, 
                               img_ext_face_sourire, 
                               img_ext_sourire, 
                               img_int_droit, 
                               img_int_surplomb, 
                               img_int_face, 
                               img_int_gauche, 
                               img_int_max, 
                               img_int_man, 
                               img_moul_droit, 
                               img_moul_face, 
                               img_moul_gauche, 
                               img_moul_max, 
                               img_moul_man, 
                               listofpointsan1, 
                               listofpointsan2, 
                               listofpointsan6, 
                               listofpointsan7, 
                               freinlingual, 
                               sautarticule, 
                               syncro_x, 
                               syncro_y, 
                               syncro_zoom, 
                               syncro_rotation, 
                               id_modele_entente, 
                               tnldroit, 
                               tnlgauche, 
                               listofpointsanoccd, 
                               listofpointsanoccf, 
                               listofpointsanoccg, 
                               listofpointssourire, 
                               controle, 
                               articuleinvant, 
                               NoTaquets,
                               NoMvts,
                                spp,
                               diasteme
                        from basediag_diagnostic 
                        where id_patient=@id
                        order by dateresume asc";

                
                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", IdPatient);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                return dt;

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

        #region Devis
        /*
        public static DataTable getTypeDevis()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, libelle,CATEGORIE from basediag_typedevis order by libelle";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

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
        
        public static DataTable getDevis(Patient patient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "       id_patient, ";
                selectQuery += "       type_devis, ";
                selectQuery += "       dateproposition, ";
                selectQuery += "       datesignature, ";
                selectQuery += "       montant_devis, ";
                selectQuery += "       duree_devis, ";
                selectQuery += "       opt_facette, ";
                selectQuery += "       opt_kit_eclair, ";
                selectQuery += "       opt_kit_traction, ";
                selectQuery += "       opt_cont_bas_1arc, ";
                selectQuery += "       opt_cont_bas_2arc, ";
                selectQuery += "       opt_cont_bas_jeu, ";
                selectQuery += "       opt_cont_vivera_1arc, ";
                selectQuery += "       opt_cont_vivera_2arc, ";
                selectQuery += "       opt_cont_vivera_jeu, ";
                selectQuery += "       opt_cont_filmetal_1arc, ";
                selectQuery += "       opt_cont_filmetal_2arc, ";
                selectQuery += "       opt_cont_filor_1arc, ";
                selectQuery += "       opt_cont_filor_2arc, ";
                selectQuery += "       opt_cont_filfibre_1arc, ";
                selectQuery += "       opt_cont_filfibre_2arc, ";
                selectQuery += "       opt_nbminivis";
                selectQuery += " from basediag_devis";
                selectQuery += " where id_patient = @idpatient";

                
                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@idpatient", patient.Id);

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

        public static DataRow getLastDevis(Patient patient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "       id_patient, ";
                selectQuery += "       type_devis, ";
                selectQuery += "       dateproposition, ";
                selectQuery += "       datesignature, ";
                selectQuery += "       montant_devis, ";
                selectQuery += "       duree_devis, ";
                selectQuery += "       opt_facette, ";
                selectQuery += "       opt_kit_eclair, ";
                selectQuery += "       opt_kit_traction, ";
                selectQuery += "       opt_cont_bas_1arc, ";
                selectQuery += "       opt_cont_bas_2arc, ";
                selectQuery += "       opt_cont_bas_jeu, ";
                selectQuery += "       opt_cont_vivera_1arc, ";
                selectQuery += "       opt_cont_vivera_2arc, ";
                selectQuery += "       opt_cont_vivera_jeu, ";
                selectQuery += "       opt_cont_filmetal_1arc, ";
                selectQuery += "       opt_cont_filmetal_2arc, ";
                selectQuery += "       opt_cont_filor_1arc, ";
                selectQuery += "       opt_cont_filor_2arc, ";
                selectQuery += "       opt_cont_filfibre_1arc, ";
                selectQuery += "       opt_cont_filfibre_2arc, ";
                selectQuery += "       opt_nbminivis";
                selectQuery += " from basediag_devis";
                selectQuery += " where id_patient = @idpatient and datesignature is null";
                selectQuery += " order by dateproposition desc";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@idpatient", patient.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count == 0) return null;else return dt.Rows[0];

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
        
        public static void InsertDevis(Devis devis)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();


            string selectQueryId = "select max(Id)+1 as ID from basediag_devis";
            MySqlCommand commandid = new MySqlCommand(selectQueryId, connection, transaction);
            object id = commandid.ExecuteScalar();
            if (id == DBNull.Value)
                devis.Id = 1;
            else
                devis.Id = Convert.ToInt32(id);



            try
            {
                string selectQuery = "insert into basediag_devis (id, ";
                selectQuery += "                            id_patient, ";
                selectQuery += "                            type_devis, ";
                selectQuery += "                            dateproposition, ";
                selectQuery += "                            datesignature, ";
                selectQuery += "                            montant_devis, ";
                selectQuery += "                            duree_devis, ";
                selectQuery += "                            opt_facette, ";
                selectQuery += "                            opt_kit_eclair, ";
                selectQuery += "                            opt_kit_traction, ";
                selectQuery += "                            opt_cont_bas_1arc, ";
                selectQuery += "                            opt_cont_bas_2arc, ";
                selectQuery += "                            opt_cont_bas_jeu, ";
                selectQuery += "                            opt_cont_vivera_1arc, ";
                selectQuery += "                            opt_cont_vivera_2arc, ";
                selectQuery += "                            opt_cont_vivera_jeu, ";
                selectQuery += "                            opt_cont_filmetal_1arc, ";
                selectQuery += "                            opt_cont_filmetal_2arc, ";
                selectQuery += "                            opt_cont_filor_1arc, ";
                selectQuery += "                            opt_cont_filor_2arc, ";
                selectQuery += "                            opt_cont_filfibre_1arc, ";
                selectQuery += "                            opt_cont_filfibre_2arc, ";
                selectQuery += "                            opt_nbminivis)";
                selectQuery += " values (@id, ";
                selectQuery += "        @id_patient, ";
                selectQuery += "        @type_devis, ";
                selectQuery += "        @dateproposition, ";
                selectQuery += "        @datesignature, ";
                selectQuery += "        @montant_devis, ";
                selectQuery += "        @duree_devis, ";
                selectQuery += "        @opt_facette, ";
                selectQuery += "        @opt_kit_eclair, ";
                selectQuery += "        @opt_kit_traction, ";
                selectQuery += "        @opt_cont_bas_1arc, ";
                selectQuery += "        @opt_cont_bas_2arc, ";
                selectQuery += "        @opt_cont_bas_jeu, ";
                selectQuery += "        @opt_cont_vivera_1arc, ";
                selectQuery += "        @opt_cont_vivera_2arc, ";
                selectQuery += "        @opt_cont_vivera_jeu, ";
                selectQuery += "        @opt_cont_filmetal_1arc, ";
                selectQuery += "        @opt_cont_filmetal_2arc, ";
                selectQuery += "        @opt_cont_filor_1arc, ";
                selectQuery += "        @opt_cont_filor_2arc, ";
                selectQuery += "        @opt_cont_filfibre_1arc, ";
                selectQuery += "        @opt_cont_filfibre_2arc, ";
                selectQuery += "        @opt_nbminivis)";




                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id", devis.Id);
                command.Parameters.AddWithValue("@id_patient", devis.IdPatient);
                command.Parameters.AddWithValue("@type_devis", devis.typedevis.Id);
                command.Parameters.AddWithValue("@dateproposition", devis.DateProposition == null ? DBNull.Value : (object)devis.DateProposition.Value);
                command.Parameters.AddWithValue("@datesignature", devis.DateSignature==null?DBNull.Value:(object)devis.DateSignature.Value);
                command.Parameters.AddWithValue("@montant_devis", devis.Montant);
                command.Parameters.AddWithValue("@duree_devis", devis.Duree);
                command.Parameters.AddWithValue("@opt_facette", devis.FacetteEsthetique);
                command.Parameters.AddWithValue("@opt_kit_eclair", devis.KitEclaircissement);
                command.Parameters.AddWithValue("@opt_kit_traction", devis.KitTractionSurMiniVis);
                command.Parameters.AddWithValue("@opt_cont_bas_1arc", devis.ContentionBAS1Arcade);
                command.Parameters.AddWithValue("@opt_cont_bas_2arc", devis.ContentionBAS2Arcades);
                command.Parameters.AddWithValue("@opt_cont_bas_jeu", devis.ContentionBASJeu);
                command.Parameters.AddWithValue("@opt_cont_vivera_1arc", devis.ContentionVIVERA1Arcade);
                command.Parameters.AddWithValue("@opt_cont_vivera_2arc", devis.ContentionVIVERA2Arcades);
                command.Parameters.AddWithValue("@opt_cont_vivera_jeu", devis.ContentionVIVERAJeu);
                command.Parameters.AddWithValue("@opt_cont_filmetal_1arc", devis.ContentionFilMetal1Arcade);
                command.Parameters.AddWithValue("@opt_cont_filmetal_2arc", devis.ContentionFilMetal2Arcade);
                command.Parameters.AddWithValue("@opt_cont_filor_1arc", devis.ContentionFilOr1Arcade);
                command.Parameters.AddWithValue("@opt_cont_filor_2arc", devis.ContentionFilOr2Arcades);
                command.Parameters.AddWithValue("@opt_cont_filfibre_1arc", devis.ContentionFilFibre1Arcade);
                command.Parameters.AddWithValue("@opt_cont_filfibre_2arc", devis.ContentionFilFibre2Arcades);
                command.Parameters.AddWithValue("@opt_nbminivis", devis.NbMiniVis);



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

        public static void UpdateDevis(Devis devis)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();


            string selectQueryId = "select max(Id)+1 as ID from basediag_devis";
            MySqlCommand commandid = new MySqlCommand(selectQueryId, connection, transaction);
            object id = commandid.ExecuteScalar();
            if (id == DBNull.Value)
                devis.Id = 1;
            else
                devis.Id = Convert.ToInt32(id);



            try
            {
                string selectQuery = "update basediag_devis";
                selectQuery += "set id_patient = @id_patient,";
                selectQuery += "    type_devis = @type_devis,";
                selectQuery += "    dateproposition = @dateproposition,";
                selectQuery += "    datesignature = @datesignature,";
                selectQuery += "    montant_devis = @montant_devis,";
                selectQuery += "    duree_devis = @duree_devis,";
                selectQuery += "    opt_facette = @opt_facette,";
                selectQuery += "    opt_kit_eclair = @opt_kit_eclair,";
                selectQuery += "    opt_kit_traction = @opt_kit_traction,";
                selectQuery += "    opt_cont_bas_1arc = @opt_cont_bas_1arc,";
                selectQuery += "    opt_cont_bas_2arc = @opt_cont_bas_2arc,";
                selectQuery += "    opt_cont_bas_jeu = @opt_cont_bas_jeu,";
                selectQuery += "    opt_cont_vivera_1arc = @opt_cont_vivera_1arc,";
                selectQuery += "    opt_cont_vivera_2arc = @opt_cont_vivera_2arc,";
                selectQuery += "    opt_cont_vivera_jeu = @opt_cont_vivera_jeu,";
                selectQuery += "    opt_cont_filmetal_1arc = @opt_cont_filmetal_1arc,";
                selectQuery += "    opt_cont_filmetal_2arc = @opt_cont_filmetal_2arc,";
                selectQuery += "    opt_cont_filor_1arc = @opt_cont_filor_1arc,";
                selectQuery += "    opt_cont_filor_2arc = @opt_cont_filor_2arc,";
                selectQuery += "    opt_cont_filfibre_1arc = @opt_cont_filfibre_1arc,";
                selectQuery += "    opt_cont_filfibre_2arc = @opt_cont_filfibre_2arc,";
                selectQuery += "    opt_nbminivis = @opt_nbminivis";
                selectQuery += "where (id = @id)";





                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id", devis.Id);
                command.Parameters.AddWithValue("@id_patient", devis.IdPatient);
                command.Parameters.AddWithValue("@type_devis", devis.typedevis.Id);
                command.Parameters.AddWithValue("@dateproposition", devis.DateProposition == null ? DBNull.Value : (object)devis.DateProposition.Value);
                command.Parameters.AddWithValue("@datesignature", devis.DateSignature == null ? DBNull.Value : (object)devis.DateSignature.Value);
                command.Parameters.AddWithValue("@montant_devis", devis.Montant);
                command.Parameters.AddWithValue("@duree_devis", devis.Duree);
                command.Parameters.AddWithValue("@opt_facette", devis.FacetteEsthetique);
                command.Parameters.AddWithValue("@opt_kit_eclair", devis.KitEclaircissement);
                command.Parameters.AddWithValue("@opt_kit_traction", devis.KitTractionSurMiniVis);
                command.Parameters.AddWithValue("@opt_cont_bas_1arc", devis.ContentionBAS1Arcade);
                command.Parameters.AddWithValue("@opt_cont_bas_2arc", devis.ContentionBAS2Arcades);
                command.Parameters.AddWithValue("@opt_cont_bas_jeu", devis.ContentionBASJeu);
                command.Parameters.AddWithValue("@opt_cont_vivera_1arc", devis.ContentionVIVERA1Arcade);
                command.Parameters.AddWithValue("@opt_cont_vivera_2arc", devis.ContentionVIVERA2Arcades);
                command.Parameters.AddWithValue("@opt_cont_vivera_jeu", devis.ContentionVIVERAJeu);
                command.Parameters.AddWithValue("@opt_cont_filmetal_1arc", devis.ContentionFilMetal1Arcade);
                command.Parameters.AddWithValue("@opt_cont_filmetal_2arc", devis.ContentionFilMetal2Arcade);
                command.Parameters.AddWithValue("@opt_cont_filor_1arc", devis.ContentionFilOr1Arcade);
                command.Parameters.AddWithValue("@opt_cont_filor_2arc", devis.ContentionFilOr2Arcades);
                command.Parameters.AddWithValue("@opt_cont_filfibre_1arc", devis.ContentionFilFibre1Arcade);
                command.Parameters.AddWithValue("@opt_cont_filfibre_2arc", devis.ContentionFilFibre2Arcades);
                command.Parameters.AddWithValue("@opt_nbminivis", devis.NbMiniVis);




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
        */
        #endregion

        #region Utilisateurs

        public static DataTable getUtilisateursInFauteuil(Fauteuil f, DateTime dte)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select ID_USER from RH_BASE_AFFECT_FAUT_USER";
                selectQuery += " where RH_BASE_AFFECT_FAUT_USER.id_fauteuil = @idfauteuil";
                selectQuery += " and @dte between RH_BASE_AFFECT_FAUT_USER.affecte_from and RH_BASE_AFFECT_FAUT_USER.affecte_to";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@idfauteuil", f.Id);
                command.Parameters.AddWithValue("@dte", dte);


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
        
        public static DataTable getUtilisateurs()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select u.util_actif,u.DATEFINCONTRAT , tp.NOM as NOMTYPE, p.id_personne, pers_titre, profession, per_nom, per_prenom, per_genre, per_type, per_email,per_notes, per_poste, pcom, PER_TELPRINC,PER_ADR1,PER_ADR2,PER_VILLE,PER_CPOSTAL,u.DATEEMBAUCHE ";
                selectQuery += " from personne p";
                selectQuery += " inner join utilisateur u on u.id_personne = p.id_personne";
                selectQuery += " inner join TYPE_PERS tp on tp.id_type = u.UTIL_TYPE";
                selectQuery += " WHERE   personne.TYPE_MATERIEL IS NULL ";
                selectQuery += " order by  per_type, per_nom, per_prenom";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

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
        
        #endregion

        #region Propositions

        public static DataTable getPropositions(basePatient patient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "       etat, ";
                selectQuery += "       dateevent, ";
                selectQuery += "       id_patient, ";
                selectQuery += "       ID_MODELE, ";
                selectQuery += "       libelle, ";
                selectQuery += "       IDDEVIS, ";
                selectQuery += "       date_acceptation,";
                selectQuery += "       DATE_PROPOSITION,";
                selectQuery += "       IDSCENARIO";
                
                selectQuery += " from base_propositions";
                selectQuery += " where id_patient = @id";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", patient.Id);

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

        public static DataRow getModeleDeProposition(int id)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";                
                selectQuery += "       libelle ";
                selectQuery += " from BASE_MODEL_PROPOSITION";
                selectQuery += " where id = @id";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                    return null;

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

        public static DataTable getModeleDePropositions()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "       libelle ";
                selectQuery += " from BASE_MODEL_PROPOSITION";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

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


        public static DataTable GetScenariosCommClinique()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "        Libelle, ";
                selectQuery += "        NbSemestres, ";
                selectQuery += "        TypeTtmnt ";
                selectQuery += " from SCENARIOS_COMM";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

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




        public static DataTable GetScenariosCommCliniqueDetails(ScenarioCommClinique scenar)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "        id_acte, ";
                selectQuery += "        commentaires,";
                selectQuery += "        commentairesafaire,";
                selectQuery += "        ID_SCENARIO,";
                selectQuery += "        NBJOURS,";
                selectQuery += "        NBMOIS,";
                selectQuery += "        num_semestre,";
                selectQuery += "        id_parentcomment,";
                selectQuery += "        ordre,";
                selectQuery += "        refdate";
                selectQuery += " from scenarios_comm_detail";
                selectQuery += " where ID_SCENARIO = @ID";
                selectQuery += " order by ID_SCENARIO asc,ordre asc";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@ID", scenar.Id);

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

        public static DataTable GetCommCliniqueScenarMateriels(CommCliniqueDetailsScenario com)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id_comm, ";
                selectQuery += "        id_baseproduit, ";
                selectQuery += "        libelle, ";
                selectQuery += "        qte, ";
                selectQuery += "        shortlib";
                selectQuery += " from BASE_SCENAR_COMM_MAT";

                selectQuery += " where id_comm = @IDCOMM";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@IDCOMM", com.Id);

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

        public static DataTable GetCommCliniqueScenarRadios(CommCliniqueDetailsScenario com)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id_comm, ";
                selectQuery += "        typeradio ";
                selectQuery += " from BASE_SCENAR_COMM_RADIOS";

                selectQuery += " where id_comm = @IDCOMM";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@IDCOMM", com.Id);

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


        public static DataTable GetCommCliniqueScenarPhotos(CommCliniqueDetailsScenario com)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id_comm, ";
                selectQuery += "        typephoto ";
                selectQuery += " from BASE_SCENAR_COMM_PHOTOS";

                selectQuery += " where id_comm = @IDCOMM";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@IDCOMM", com.Id);

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



        public static DataTable getCommCliniqueScenarEnbouche(CommCliniqueDetailsScenario com)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += " typematerial, ";
                selectQuery += " dents,";
                selectQuery += " ID_APPAREIL,";
                selectQuery += " ID_COMM_DEBUT,";
                selectQuery += " ID_COMM_FIN,";
                selectQuery += " ID_APPAREIL,";
                selectQuery += " HAUT,";
                selectQuery += " BAS";
                selectQuery += " from BASE_SCENAR_ENBOUCHE";
                selectQuery += " where ID_COMM_DEBUT=@ID_COMM or ID_COMM_FIN=@ID_COMM";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@ID_COMM", com.Id);

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
 

        public static DataTable getPropositions(ModeleDePropositions mdl)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "       etat, ";
                selectQuery += "       dateevent, ";
                selectQuery += "       id_patient, ";
                selectQuery += "       ID_MODELE, ";
                selectQuery += "       IDDEVIS, ";
                selectQuery += "       libelle, ";
                selectQuery += "       IDSCENARIO, ";
                
                selectQuery += "       date_acceptation,";
                selectQuery += "       DATE_PROPOSITION";
                
                selectQuery += " from base_propositions";
                selectQuery += " where ID_MODELE = @id";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", mdl.Id);

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

        public static DataTable getPropositions(Devis devis)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "       etat, ";
                selectQuery += "       dateevent, ";
                selectQuery += "       id_patient, ";
                selectQuery += "       ID_MODELE, ";
                selectQuery += "       IDDEVIS, ";
                selectQuery += "       libelle, ";
                selectQuery += "       IdScenario, ";
                selectQuery += "       date_acceptation,";
                selectQuery += "       DATE_PROPOSITION,";
                selectQuery += "       IDSCENARIO";
                
                selectQuery += " from base_propositions";
                selectQuery += " where iddevis = @id";
                selectQuery += " order by dateevent asc";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", devis.Id);

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

        public static DataTable getSignedPropositions(basePatient patient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "       etat, ";
                selectQuery += "       dateevent, ";
                selectQuery += "       id_patient, ";
                selectQuery += "       ID_MODELE, ";
                selectQuery += "       IDDEVIS, ";
                selectQuery += "       libelle, ";
                selectQuery += "       IdScenario, ";
                selectQuery += "       date_acceptation,";
                selectQuery += "       DATE_PROPOSITION,";
                selectQuery += "       IDSCENARIO";
                
                selectQuery += " from base_propositions";
                selectQuery += " where id_patient = @id and etat = 1";
                selectQuery += " order by date_acceptation";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", patient.Id);

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


        public static DataRow getProposition(int id )
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "       etat, ";
                selectQuery += "       dateevent, ";
                selectQuery += "       tarif, ";
                selectQuery += "       id_patient, ";
                selectQuery += "       ID_MODELE, ";
                selectQuery += "       IDDEVIS, ";
                selectQuery += "       DATE_PROPOSITION, ";
                
                selectQuery += "       libelle, ";
                selectQuery += "       date_acceptation";
                selectQuery += " from base_propositions";
                selectQuery += " where id = @id";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                    return null;

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

        public static DataTable getTraitements(Proposition proposition)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string SelectQuery = " select id, ";
                SelectQuery += "        Libelle, ";
                SelectQuery += "        Phase, ";
                SelectQuery += "        CodeTraitement, ";                
                SelectQuery += "        id_proposition ";
                SelectQuery += " from base_plan_traitements";
                SelectQuery += " where id_proposition = @id";

                MySqlCommand command = new MySqlCommand(SelectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", proposition.Id);

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


        public static void DeleteSemestre(Semestre sem)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectquery = "update base_semestre";
                selectquery += " set numsemestre=numsemestre-1 ";
                selectquery += " where numsemestre>@numsemestre and id_traitement in (select id_traitement from base_semestre where id = @id)";

                MySqlCommand commandt = new MySqlCommand(selectquery, connection, transaction);

                commandt.Parameters.AddWithValue("@numsemestre", sem.NumSemestre);
                commandt.Parameters.AddWithValue("@id", sem.Id);


                commandt.ExecuteNonQuery();

                selectquery = "delete from base_semestre where id = @id";


                commandt.CommandText = selectquery;


                commandt.ExecuteNonQuery();




                selectquery = "delete from base_plan_traitements";
                selectquery += " where id in (";
                selectquery += " select base_plan_traitements.id";
                selectquery += " from base_plan_traitements";
                selectquery += " left outer join base_semestre on base_semestre.id_traitement = base_plan_traitements.id";
                selectquery += " inner join base_propositions on base_propositions.id = base_plan_traitements.id_proposition";
                selectquery += " group by base_plan_traitements.id";
                selectquery += " having count(base_semestre.id)=0";
                selectquery += " )";


                commandt.CommandText = selectquery;

                commandt.ExecuteNonQuery();

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

        /*
        public static void UpdateSemestre(Semestre sem)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {


                string selectquery = "update base_semestre set ";
                selectquery += "                           id = @id,";
                selectquery += "                           id_traitement=@id_traitement, ";
                selectquery += "                           codesemestre=@codesemestre, ";
                selectquery += "                           id_acte_gestion=@id_acte_gestion, ";
                selectquery += "                           montant_semestre=@montant_semestre, ";
                selectquery += "                           datedebut=@datedebut, ";
                selectquery += "                           datefin=@datefin, ";
                selectquery += "                           numsemestre=@numsemestre ";
                selectquery += " where id = @id";


                MySqlCommand commandt = new MySqlCommand(selectquery, connection, transaction);
                commandt.Parameters.Clear();
                commandt.Parameters.AddWithValue("@id", sem.Id);
                commandt.Parameters.AddWithValue("@id_traitement", sem.Parent.Id);
                commandt.Parameters.AddWithValue("@codesemestre", sem.CodeSemestre);
                commandt.Parameters.AddWithValue("@id_acte_gestion", sem.traitementSecu.Id);
                commandt.Parameters.AddWithValue("@montant_semestre", sem.Montant_Honoraire);
                commandt.Parameters.AddWithValue("@datedebut", sem.DateDebut == null ? DBNull.Value : (object)sem.DateDebut.Value);
                commandt.Parameters.AddWithValue("@datefin", sem.DateFin == null ? DBNull.Value : (object)sem.DateFin.Value);
                commandt.Parameters.AddWithValue("@numsemestre", sem.NumSemestre);

                commandt.ExecuteNonQuery();




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
        */

        public static DataTable getSemestres(Traitement traitement)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectquery = "select id, ";
                selectquery += "       id_traitement, ";
                selectquery += "       codesemestre, ";
                selectquery += "       id_acte_gestion, ";
                selectquery += "       montant_semestre, ";
                selectquery += "       nb_surveillance, ";
                selectquery += "       id_acte_gestion_surv, ";
                selectquery += "       montant_surveillance, ";
                selectquery += "       id_dep, ";
                
                selectquery += "       datedebut, ";
                selectquery += "       datefin, ";
                selectquery += "       numsemestre";
                selectquery += " from base_semestre";
                selectquery += " where base_semestre.id_traitement = @id";


                MySqlCommand command = new MySqlCommand(selectquery, connection, transaction);
                command.Parameters.AddWithValue("@id", traitement.Id);

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

        public static DataTable getPoseAppareils(Proposition proposition)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectquery = "select id, ";
                selectquery += "       id_proposition, ";
                selectquery += "       id_appareil";
                selectquery += " from base_pose_appareil";
                selectquery += " where id_proposition = @id";


                MySqlCommand command = new MySqlCommand(selectquery, connection, transaction);
                command.Parameters.AddWithValue("@id", proposition.Id);

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
        /*
        public static List<int> getIdSemestres(PoseAppareil pa)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectquery = "select ID_SEMESTRE";
                selectquery += " from BASE_POSE_APP_SEMESTRE";
                selectquery += " where ID_POSE_APPAREIL = @id";


                MySqlCommand command = new MySqlCommand(selectquery, connection, transaction);
                command.Parameters.AddWithValue("@id", pa.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                List<int> lst = new List<int>();

                foreach (DataRow dr in dt.Rows)
                    lst.Add(Convert.ToInt32(dr["ID_SEMESTRE"]));

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
          */ 

        public static void AddModele(ModeleDePropositions mdl)
        {


            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQueryId = "select max(id)+1 as ID from BASE_MODEL_PROPOSITION";
                MySqlCommand commandid = new MySqlCommand(selectQueryId, connection, transaction);
                object obj = commandid.ExecuteScalar();
                if (obj == DBNull.Value)
                    mdl.Id = 1;
                else
                    mdl.Id = Convert.ToInt32(obj);

                string selectQuery = "insert into BASE_MODEL_PROPOSITION (id, ";
                selectQuery += "                               libelle)";
                selectQuery += "values (@id, ";
                selectQuery += "        @libelle)";



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);


                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", mdl.Id);
                command.Parameters.AddWithValue("@libelle", mdl.Nom);

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


        public static void AddTraitement(Traitement traitement)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQueryId = "select max(id)+1 as ID from base_plan_traitements";
                MySqlCommand commandid = new MySqlCommand(selectQueryId, connection, transaction);
                object obj = commandid.ExecuteScalar();
                if (obj == DBNull.Value)
                    traitement.Id = 1;
                else
                    traitement.Id = Convert.ToInt32(obj);


                String SelectQuery = " insert into base_plan_traitements (id, ";
                SelectQuery += "                                   Libelle, ";
                SelectQuery += "                                   Phase, ";
                SelectQuery += "                                   id_proposition)";
                SelectQuery += " values (@id, ";
                SelectQuery += "        @Libelle, ";
                SelectQuery += "        @Phase, ";
                SelectQuery += "        @id_proposition)";




                MySqlCommand commandt = new MySqlCommand(SelectQuery, connection, transaction);
                commandt.Parameters.Clear();
                commandt.Parameters.AddWithValue("@id", traitement.Id);
                commandt.Parameters.AddWithValue("@Libelle", traitement.Libelle);
                commandt.Parameters.AddWithValue("@Phase", (int)traitement.Phase);
                commandt.Parameters.AddWithValue("@id_proposition", traitement.Parent.Id);

                commandt.ExecuteNonQuery();




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
        /*
        public static void AddSemestre(Semestre sem)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQueryId = "select max(id)+1 as ID from base_semestre";
                MySqlCommand commandid = new MySqlCommand(selectQueryId, connection, transaction);
                object obj = commandid.ExecuteScalar();
                if (obj == DBNull.Value)
                    sem.Id = 1;
                else
                    sem.Id = Convert.ToInt32(obj);


                string selectquery = "insert into base_semestre (";
                selectquery += "                           id,";
                selectquery += "                           id_traitement, ";
                selectquery += "                           codesemestre, ";
                selectquery += "                           id_acte_gestion, ";
                selectquery += "                           montant_semestre, ";
                selectquery += "                           datedebut, ";
                selectquery += "                           datefin, ";
                selectquery += "                           numsemestre)";
                selectquery += "values (@id, ";
                selectquery += "        @id_traitement, ";
                selectquery += "        @codesemestre, ";
                selectquery += "        @id_acte_gestion, ";
                selectquery += "        @montant_semestre, ";
                selectquery += "        @datedebut, ";
                selectquery += "        @datefin, ";
                selectquery += "        @numsemestre)";






                MySqlCommand commandt = new MySqlCommand(selectquery, connection, transaction);
                commandt.Parameters.Clear();
                commandt.Parameters.AddWithValue("@id", sem.Id);
                commandt.Parameters.AddWithValue("@id_traitement", sem.Parent.Id);
                commandt.Parameters.AddWithValue("@codesemestre", sem.CodeSemestre);
                commandt.Parameters.AddWithValue("@id_acte_gestion", sem.traitementSecu.Id);
                commandt.Parameters.AddWithValue("@montant_semestre", sem.Montant_Honoraire);
                commandt.Parameters.AddWithValue("@datedebut", sem.DateDebut == null ? DBNull.Value : (object)sem.DateDebut.Value);
                commandt.Parameters.AddWithValue("@datefin", sem.DateFin == null ? DBNull.Value : (object)sem.DateFin.Value);
                commandt.Parameters.AddWithValue("@numsemestre", sem.NumSemestre);

                commandt.ExecuteNonQuery();




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

        
        public static void AddPoseAppareil(PoseAppareil poseAppareil)
        {


            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQueryId = "select max(id)+1 as ID from BASE_POSE_APPAREIL";
                MySqlCommand commandid = new MySqlCommand(selectQueryId, connection, transaction);
                object obj = commandid.ExecuteScalar();
                if (obj == DBNull.Value)
                    poseAppareil.Id = 1;
                else
                    poseAppareil.Id = Convert.ToInt32(obj);

                string selectquery = "insert into base_pose_appareil (id, ";
                selectquery += "                                id_proposition, ";
                selectquery += "                                id_appareil)";
                selectquery += " values (@id, ";
                selectquery += "        @id_proposition, ";
                selectquery += "        @id_appareil)";




                MySqlCommand command = new MySqlCommand(selectquery, connection, transaction);


                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", poseAppareil.Id);
                command.Parameters.AddWithValue("@id_proposition", poseAppareil.Parent.Id);
                command.Parameters.AddWithValue("@id_appareil", poseAppareil.appareil.Id);
                

                command.ExecuteNonQuery();

                foreach (Semestre s in poseAppareil.semestres)
                {
                    string selectquerysub = "insert into base_pose_app_semestre (id_pose_appareil, ";
                    selectquerysub += "                                    id_semestre)";
                    selectquerysub += "values (@id_pose_appareil, ";
                    selectquerysub += "        @id_semestre)";


                    MySqlCommand commandsub = new MySqlCommand(selectquerysub, connection, transaction);
                    commandsub.Parameters.AddWithValue("@id_pose_appareil", poseAppareil.Id);
                    commandsub.Parameters.AddWithValue("@id_semestre", s.Id);

                    commandsub.ExecuteNonQuery();
                
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
        */

        public static void CleanAllPropositions(basePatient patient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                //Suppression des Semestres
                string selectQuerydel = "delete from BASE_SEMESTRE where ID_TRAITEMENT in (";
                selectQuerydel += " select id from base_plan_traitements where ID_PROPOSITION in (";
                selectQuerydel += " select id from base_propositions where id_patient=@id_patient";
                selectQuerydel += ")";
                selectQuerydel += ")";
                MySqlCommand commanddel = new MySqlCommand(selectQuerydel, connection, transaction);
                commanddel.Parameters.AddWithValue("@id_patient", patient.Id);
                commanddel.ExecuteNonQuery();

                //Suppression des plans de traitement
                selectQuerydel = "delete from base_plan_traitements where id_proposition in (select id from base_propositions where id_patient=@id_patient)";
                commanddel.CommandText = selectQuerydel;
                commanddel.ExecuteNonQuery();

                //Suppression des Semestres lié à la pose d'appareils
                selectQuerydel = "delete from BASE_POSE_APP_SEMESTRE where ID_POSE_APPAREIL in (";
                selectQuerydel += "select id from BASE_POSE_APPAREIL where ID_PROPOSITION in (";
                selectQuerydel += "select id from base_propositions where id_patient=@id_patient";
                selectQuerydel += ")";
                selectQuerydel += ")";
                commanddel.CommandText = selectQuerydel;
                commanddel.ExecuteNonQuery();

                //Suppression des pose d'appareils
                selectQuerydel = "delete from BASE_POSE_APPAREIL where ID_PROPOSITION in (";
                selectQuerydel += "select id from base_propositions where id_patient=@id_patient";
                selectQuerydel += ")";
                commanddel.CommandText = selectQuerydel;
                commanddel.ExecuteNonQuery();



                //Suppression des propositions
                selectQuerydel = "delete from base_propositions where id_patient=@id_patient";
                commanddel.CommandText = selectQuerydel;
                commanddel.ExecuteNonQuery();

                
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

        public static void CleanAllPropositions(ModeleDePropositions mdl)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                //Suppression des Semestres
                string selectQuerydel = "delete from BASE_SEMESTRE where ID_TRAITEMENT in (";
                selectQuerydel += " select id from base_plan_traitements where ID_PROPOSITION in (";
                selectQuerydel += " select id from base_propositions where ID_MODELE=@ID_MODELE";
                selectQuerydel += ")";
                selectQuerydel += ")";
                MySqlCommand commanddel = new MySqlCommand(selectQuerydel, connection, transaction);
                commanddel.Parameters.AddWithValue("@ID_MODELE", mdl.Id);
                commanddel.ExecuteNonQuery();

                //Suppression des plans de traitement
                selectQuerydel = "delete from base_plan_traitements where id_proposition in (select id from base_propositions where ID_MODELE=@ID_MODELE)";
                commanddel.CommandText = selectQuerydel;
                commanddel.ExecuteNonQuery();

                //Suppression des Semestres lié à la pose d'appareils
                selectQuerydel = "delete from BASE_POSE_APP_SEMESTRE where ID_POSE_APPAREIL in (";
                selectQuerydel += "select id from BASE_POSE_APPAREIL where ID_PROPOSITION in (";
                selectQuerydel += "select id from base_propositions where ID_MODELE=@ID_MODELE";
                selectQuerydel += ")";
                selectQuerydel += ")";
                commanddel.CommandText = selectQuerydel;
                commanddel.ExecuteNonQuery();

                //Suppression des pose d'appareils
                selectQuerydel = "delete from BASE_POSE_APPAREIL where ID_PROPOSITION in (";
                selectQuerydel += "select id from base_propositions where ID_MODELE=@ID_MODELE";
                selectQuerydel += ")";
                commanddel.CommandText = selectQuerydel;
                commanddel.ExecuteNonQuery();



                //Suppression des propositions
                selectQuerydel = "delete from base_propositions where ID_MODELE=@ID_MODELE";
                commanddel.CommandText = selectQuerydel;
                commanddel.ExecuteNonQuery();


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


        
        public static void UpdatePropositions(Proposition proposition)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = " update base_propositions";
                selectQuery += " set etat = @etat,";
                selectQuery += "    date_proposition = @date_proposition,";
                selectQuery += "    dateevent = @dateevent,";
                selectQuery += "    libelle = @libelle,";
                selectQuery += "    iddevis = @devis,";                
                selectQuery += "    date_acceptation = @date_acceptation";
                selectQuery += " where (id = @id)";



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", proposition.Id);
                    command.Parameters.AddWithValue("@etat", proposition.Etat);
                    command.Parameters.AddWithValue("@dateevent", proposition.DateEvenement);
                    command.Parameters.AddWithValue("@libelle", proposition.libelle);
                    command.Parameters.AddWithValue("@devis", proposition.IdDevis);
                    command.Parameters.AddWithValue("@date_acceptation", proposition.DateAcceptation == null ? (object)DBNull.Value : proposition.DateAcceptation.Value);
                    command.Parameters.AddWithValue("@date_proposition", proposition.DateProposition);


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

                
        #endregion


        #region Devis

        public static DataRow getDevis(int Id)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "select id,  ";
                selectQuery += "       dateproposition,  ";
                selectQuery += "       dateacceptation,  ";
                selectQuery += "       id_patient,  ";
                selectQuery += "       DATEECHEANCE,  ";
                selectQuery += "       id_objet_baseview ";
                selectQuery += "       from base_devis ";
                selectQuery += "       where  id = @id";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count == 0) return null;

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

        public static DataTable getDevis(basePatient patient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "select id,  ";
                selectQuery += "       dateproposition,  ";
                selectQuery += "       dateacceptation,  ";
                selectQuery += "       DATEECHEANCE,  ";
                selectQuery += "       id_patient,  ";
                selectQuery += "       id_objet_baseview ";
                selectQuery += "       from base_devis ";
                selectQuery += "       where  id_patient = @id_patient";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id_patient", patient.Id);

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

        public static void InsertDevis(Devis devis)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();


            string selectQueryId = "select max(Id)+1 as ID from BASE_DEVIS";
            MySqlCommand commandid = new MySqlCommand(selectQueryId, connection, transaction);
            object id = commandid.ExecuteScalar();
            if (id == DBNull.Value)
                devis.Id = 1;
            else
                devis.Id = Convert.ToInt32(id);



            try
            {
                string selectQuery = "insert into BASE_DEVIS (id, ";
                selectQuery += "                            dateproposition, ";
                selectQuery += "                            dateacceptation, ";
                selectQuery += "                            DATEECHEANCE,  ";
                selectQuery += "                            id_patient, ";
                selectQuery += "                            id_objet_baseview)";
                selectQuery += " values (@id, ";
                selectQuery += "                            @dateproposition, ";
                selectQuery += "                            @dateacceptation, ";
                selectQuery += "                            @DATEECHEANCE,  ";
                selectQuery += "                            @id_patient, ";
                selectQuery += "                            @id_objet_baseview)";




                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id", devis.Id);
                command.Parameters.AddWithValue("@dateproposition", devis.DateProposition);
                command.Parameters.AddWithValue("@dateacceptation", devis.DateAcceptation == null ? DBNull.Value : (object)devis.DateAcceptation.Value);
                command.Parameters.AddWithValue("@DATEECHEANCE", devis.DateEcheance == null ? DBNull.Value : (object)devis.DateEcheance.Value);
                command.Parameters.AddWithValue("@id_patient", devis.IdPatient);
                command.Parameters.AddWithValue("@id_objet_baseview", devis.IdObjetBaseView == -1 ? DBNull.Value : (object)devis.IdObjetBaseView);

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

        public static void AccepterDevis(int IdDevis, DateTime dateAcceptation)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();



            try
            {



                string selectQuery = "update BASE_DEVIS";
                selectQuery += "    set dateacceptation = @dateacceptation";
                selectQuery += " where (id = @id)";





                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id", IdDevis);
                command.Parameters.AddWithValue("@dateacceptation", dateAcceptation);

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



        public static string FindHistoricFile(Devis devis)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select fichier from base_histo_courrier";
                selectQuery += " inner join base_courrier_attributs on base_histo_courrier.id = base_courrier_attributs.id_histo_courrier";
                selectQuery += " where base_courrier_attributs.nom_attribut = 'ID_Devis' and  base_courrier_attributs.value_attribut = @id";



                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", devis.Id);


                object obj = command.ExecuteScalar();


                if (obj == null)
                    return "";
                else
                {
                    return (string)obj;
                }

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

       
        #endregion

        /*
        public static DataRow getinfocomplementaire(basePatient pat)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "select idpatient, ";

                selectQuery += " assistante_resp, ";
                selectQuery += " SEMESTRESENTAMES, ";

                selectQuery += " AMELIORATIONS, ";
                selectQuery += " DEBUTTRAITEMENTENVISAGE, ";

                selectQuery += " praticien_resp ";
                selectQuery += " from basediag_infocomplementaire";
                selectQuery += " where idpatient = @idpatient";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@idpatient", pat.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count == 0) return null;

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
        */
        
        /*
        public static void setRisques(basePatient pat)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "UPDATE OR INSERT INTO basediag_infocomplementaire (idpatient,";
                selectQuery += "                                      RISQUES)";
                selectQuery += " values (@idpatient, ";
                selectQuery += "         @RISQUES)";
                selectQuery += "        MATCHING (idpatient)";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@idpatient", pat.Id);
                command.Parameters.AddWithValue("@RISQUES", pat.Risques.Count > 0 ? pat.Risques.Aggregate((i, j) => i + "\n" + j) : "");
                

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
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
        */
        /*
        public static List<String> getRisques(basePatient pat)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {


                
                string selectQuery = "select ";
                selectQuery += " RISQUES ";
                selectQuery += " from basediag_infocomplementaire";
                selectQuery += " where idpatient = @idpatient";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@idpatient", pat.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                List<String> lst = new List<string>();

                if (dt.Rows.Count == 0) return lst;

                lst = Convert.ToString(dt.Rows[0]["RISQUES"]).Split('\n').ToList();

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
        */



        public static void ChangePraticienResponsable(basePatient patient, Utilisateur praticien)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "UPDATE basediag_infocomplementaire set praticien_resp = @praticien_resp";
                selectQuery += "        where (idpatient = @idpatient)";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@idpatient", patient.Id);
                command.Parameters.AddWithValue("@praticien_resp", praticien.Id);
                 

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
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

        public static void ChangeAssistanteResponsable(basePatient patient, Utilisateur assistante)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "UPDATE basediag_infocomplementaire set assistante_resp = @assistante_resp";
                selectQuery += "        where (idpatient = @idpatient)";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@idpatient", patient.Id);
                command.Parameters.AddWithValue("@assistante_resp", assistante.Id);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
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

        public static void setSelectedObjectifs(ResumeClinique res)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuerydel = "delete from base_selected_objectifs where ID_RESUME=@ID_RESUME";
                MySqlCommand commanddel = new MySqlCommand(selectQuerydel, connection, transaction);
                commanddel.Parameters.AddWithValue("@ID_RESUME", res.Id);
                commanddel.ExecuteNonQuery();


                string selectQuery = "INSERT INTO base_selected_objectifs (";
                selectQuery += "                                      ID_COMMONOBJECTIF, ";
                selectQuery += "                                      ID_PATIENT, ";
                selectQuery += "                                      ID_RESUME, ";
                selectQuery += "                                      REMARQUES)";
                selectQuery += " values (@ID_COMMONOBJECTIF, ";
                selectQuery += "         @ID_PATIENT, ";
                selectQuery += "         @ID_RESUME, ";
                selectQuery += "         @REMARQUES)";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                foreach (CommonObjectif co in res.patient.SelectedObjectifs)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@ID_PATIENT", res.IdPatient);
                    command.Parameters.AddWithValue("@ID_RESUME", res.Id);
                    command.Parameters.AddWithValue("@ID_COMMONOBJECTIF", co.Id);
                    command.Parameters.AddWithValue("@REMARQUES", "");


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

        //public static void setSelectedAppareils(ResumeClinique res)
        //{
        //    if (connection == null) getConnection();

        //    if (connection.State == ConnectionState.Closed) connection.Open();
        //    MySqlTransaction transaction = connection.BeginTransaction();
        //    try
        //    {

        //        string selectQuerydel = "delete from BASE_SELECTED_APPAREIL  where ID_PATIENT=@id_patient";
        //        MySqlCommand commanddel = new MySqlCommand(selectQuerydel, connection, transaction);
        //        commanddel.Parameters.AddWithValue("@id_patient", res.patient.Id);
        //        commanddel.ExecuteNonQuery();


        //        string selectQuery = "INSERT INTO BASE_SELECTED_APPAREIL  (";
        //        selectQuery += "                                      ID_COMMONAPPAREIL, ";
        //        selectQuery += "                                      ID_PATIENT, ";
        //        selectQuery += "                                      ID_RESUME, ";
        //        selectQuery += "                                      REMARQUES)";
        //        selectQuery += " values (@ID_COMMONAPPAREIL, ";
        //        selectQuery += "         @ID_PATIENT, ";
        //        selectQuery += "         @ID_RESUME, ";
        //        selectQuery += "         @REMARQUES)";


        //        MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

        //        foreach (Appareil ap in res.patient.SelectedAppareils)
        //        {
        //            command.Parameters.Clear();
        //            command.Parameters.AddWithValue("@ID_PATIENT", res.patient.Id);
        //            command.Parameters.AddWithValue("@ID_RESUME", res.Id);
        //            command.Parameters.AddWithValue("@ID_COMMONAPPAREIL", ap.Id);
        //            command.Parameters.AddWithValue("@REMARQUES", "");


        //            command.ExecuteNonQuery();
        //        }

        //        transaction.Commit();

        //    }
        //    catch (System.SystemException ex)
        //    {
        //        transaction.Rollback();
        //        throw ex;
        //    }
        //    finally
        //    {
        //        connection.Close();

        //    }





        //}


        public static DataTable getSelectedObjectifs(ResumeClinique res)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = " select ";
                selectQuery += "       ID_COMMONOBJECTIF ID ";
                selectQuery += " from base_selected_objectifs";
                selectQuery += " where ID_RESUME=@ID_RESUME";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@ID_RESUME", res.Id);

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

        //public static DataTable getSelectedAppareils(ResumeClinique res)
        //{
        //    if (connection == null) getConnection();

        //    if (connection.State == ConnectionState.Closed) connection.Open();
        //    MySqlTransaction transaction = connection.BeginTransaction();
        //    try
        //    {
        //        string selectQuery = " select ";
        //        selectQuery += "       ID_COMMONAPPAREIL ID ";
        //        selectQuery += " from BASE_SELECTED_APPAREIL";
        //        selectQuery += " where ID_RESUME=@ID_RESUME";


        //        MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
        //        command.Parameters.AddWithValue("@ID_RESUME", res.Id);

        //        DataSet ds = new DataSet();
        //        MySqlDataAdapter adapt = new MySqlDataAdapter(command);
        //        adapt.Fill(ds);
        //        transaction.Commit();

        //        DataTable dt = ds.Tables[0];


        //        return dt;

        //    }
        //    catch (System.SystemException ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        connection.Close();

        //    }





        //}


        public static void setinfocomplementaire(basePatient pat)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "UPDATE OR INSERT INTO basediag_infocomplementaire (idpatient, ";
                selectQuery += "                                      assistante_resp, ";
                selectQuery += "                                      RISQUES, ";
                selectQuery += "                                      SEMESTRESENTAMES, ";
                selectQuery += "                                      AMELIORATIONS, ";
                selectQuery += "                                      DEBUTTRAITEMENTENVISAGE, ";
                selectQuery += "                                          praticien_resp)";
                selectQuery += " values (@idpatient, ";
                selectQuery += "         @assistante_resp, ";
                selectQuery += "         @RISQUES,";
                selectQuery += "         @SEMESTRESENTAMES,";
                selectQuery += "         @AMELIORATIONS,";
                selectQuery += "         @DEBUTTRAITEMENTENVISAGE,";
                selectQuery += "         @praticien_resp)";
                selectQuery += "        MATCHING (idpatient)";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@idpatient", pat.Id);
                command.Parameters.AddWithValue("@assistante_resp", pat.infoscomplementaire.AssistanteResponsable == null ? DBNull.Value : (object)pat.infoscomplementaire.AssistanteResponsable.Id);
                command.Parameters.AddWithValue("@praticien_resp", pat.infoscomplementaire.PraticienResponsable == null ? DBNull.Value : (object)pat.infoscomplementaire.PraticienResponsable.Id);
                command.Parameters.AddWithValue("@RISQUES", pat.Risques.Count > 0 ? pat.Risques.Aggregate((i, j) => i + "\n" + j) : "");
                command.Parameters.AddWithValue("@SEMESTRESENTAMES", pat.infoscomplementaire.NbSemestresEntame);
                command.Parameters.AddWithValue("@AMELIORATIONS", pat.infoscomplementaire.Ameliorations);
                command.Parameters.AddWithValue("@DEBUTTRAITEMENTENVISAGE", pat.infoscomplementaire.DateDebutTraitement);
                

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
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
        
        public static DataTable getPhases(Proposition proposition)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = " select id, ";
                selectQuery += "       ID_PROPOSITON, ";
                selectQuery += "       duree, ";
                selectQuery += "       Libelle, ";
                selectQuery += "       TARIFSEMESTRE, ";
                selectQuery += "       ID_BASE_ACTE_GESTION, ";
                selectQuery += "       TYPEDEPHASE ";
                selectQuery += " from base_diag_phase";
                selectQuery += " where ID_PROPOSITON=@id_proposition";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id_proposition", proposition.Id);

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

       
        /*
        #region PrinterSettings



        public static void DeletePrintSettingNames(string name)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "delete from base_printersettings where LIBELLE=@LIBELLE";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@LIBELLE", name);

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


        public static List<string> getPrintSettingNames()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select  ";
                selectQuery += "       libelle ";
                selectQuery += " from base_printersettings";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                
                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                List<string> names = new List<string>();

                foreach (DataRow dr in dt.Rows)
                    names.Add(Convert.ToString(dr["libelle"]));

                return names;

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

        public static DataTable getPrintSettings()
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select  ";
                selectQuery += "       libelle, ";
                selectQuery += "       DESCRIPTIF, ";
                selectQuery += "       SETTINGS ";
                selectQuery += " from base_printersettings";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

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


        public static DataRow getPrintSettings(string name)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select  ";
                selectQuery += "       libelle, ";
                selectQuery += "       Descriptif, ";
                selectQuery += "       settings";
                selectQuery += " from base_printersettings";
                selectQuery += " where libelle=@libelle";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@libelle", name);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count == 0) return null;
               
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

        public static void DelPrintSettings(string name)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();


            try
            {
                string selectQuery = "DELETE FROM base_printersettings where libelle = @libelle ";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);


                command.Parameters.AddWithValue("@libelle", name);
                

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
        
        public static void setPrintSettings(BasePrinterSetting printsetting)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();


            try
            {
                string selectQuery = "UPDATE OR INSERT INTO base_printersettings ( ";
                selectQuery += "                                  libelle, ";
                selectQuery += "                                  Descriptif, ";
                selectQuery += "                                  settings)";
                selectQuery += "values ( ";
                selectQuery += "        @libelle, ";
                selectQuery += "        @Descriptif, ";
                selectQuery += "        @settings)";
                selectQuery += " MATCHING (libelle)";





                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                
                MemoryStream stream = new MemoryStream();
                BinaryFormatter bformatter = new BinaryFormatter();

                bformatter.Serialize(stream, printsetting.settings);
                stream.Close();

                command.Parameters.AddWithValue("@libelle", printsetting.Libelle);
                command.Parameters.AddWithValue("@Descriptif", printsetting.Descriptif);
                command.Parameters.AddWithValue("@settings", stream.ToArray());
                

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

        #endregion
        */

        public static DataTable getContactsOf(int Id)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "SELECT ";
                selectQuery += " id, ";
                selectQuery += " contacttype, ";
                selectQuery += " \"VALUE\", ";
                selectQuery += " libelle, ";
                //selectQuery += " is_main, ";
                selectQuery += " Adr1, ";
                selectQuery += " Adr2, ";
                selectQuery += " CodePostal, ";
                selectQuery += " Ville, ";
                selectQuery += " Pays, ";
                selectQuery += " id_personne, ";
                selectQuery += " ID_CONTACTLIBELLE ";

                selectQuery += " from CONTACT ";
                selectQuery += " where id_personne=@id_personne";


                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id_personne", Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                return ds.Tables[0];

            }
            catch (System.IndexOutOfRangeException)
            {
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


        #region Commentaires Historisables

        public static DataTable GettAllCommentaires(basePatient pat)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = " select ID, ";
                selectQuery += "        ID_PATIENT, ";
                selectQuery += "        TYPE_COMMENT, ";
                selectQuery += "        DATE_COMMENT, ";
                selectQuery += "        COMMENT, ";
                selectQuery += "        ID_WRITER ";
                selectQuery += " from BASE_COMMENTS";
                selectQuery += " where ID_PATIENT = @ID_PATIENT";
                selectQuery += " order by DATE_COMMENT desc";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@ID_PATIENT", pat.Id);

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

        public static DataTable GettAllCommentaires(basePatient pat, CommentHisto.CommentHistoType type)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = " select ID, ";
                selectQuery += "        ID_PATIENT, ";
                selectQuery += "        TYPE_COMMENT, ";
                selectQuery += "        DATE_COMMENT, ";
                selectQuery += "        COMMENT, ";
                selectQuery += "        ID_WRITER ";
                selectQuery += " from BASE_COMMENTS";
                selectQuery += " where ID_PATIENT = @ID_PATIENT";
                selectQuery += " and TYPE_COMMENT = @TYPE_COMMENT";
                selectQuery += " order by DATE_COMMENT desc";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@ID_PATIENT", pat.Id);
                command.Parameters.AddWithValue("@TYPE_COMMENT", (int)type);

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

        public static DataRow GetLastCommentaire(basePatient pat, CommentHisto.CommentHistoType type)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = " select First 1 ID, ";
                selectQuery += "        ID_PATIENT, ";
                selectQuery += "        TYPE_COMMENT, ";
                selectQuery += "        DATE_COMMENT, ";
                selectQuery += "        COMMENT, ";
                selectQuery += "        ID_WRITER ";
                selectQuery += " from BASE_COMMENTS";
                selectQuery += " where ID_PATIENT = @ID_PATIENT";
                selectQuery += " and TYPE_COMMENT = @TYPE_COMMENT";
                selectQuery += " order by DATE_COMMENT desc";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@ID_PATIENT", pat.Id);
                command.Parameters.AddWithValue("@TYPE_COMMENT", (int)type);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

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
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "select MAX(ID)+1 as NEWID from BASE_COMMENTS";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                object obj = command.ExecuteScalar();

                if (obj == DBNull.Value)
                    value.Id = 1;
                else
                    value.Id = Convert.ToInt32(obj);


                selectQuery = "insert into BASE_COMMENTS (id, ";
                selectQuery += "                             id_patient, ";
                selectQuery += "                             TYPE_COMMENT, ";
                selectQuery += "                             COMMENT, ";
                selectQuery += "                             DATE_COMMENT, ";
                selectQuery += "                             ID_WRITER)";
                selectQuery += " values (@id, ";
                selectQuery += "        @id_patient, ";
                selectQuery += "        @TYPE_COMMENT, ";
                selectQuery += "        @COMMENT, ";
                selectQuery += "        @DATE_COMMENT, ";
                selectQuery += "        @ID_WRITER)";




                command.CommandText = selectQuery;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", value.Id);
                command.Parameters.AddWithValue("@id_patient", value.IdPatient);

                command.Parameters.AddWithValue("@TYPE_COMMENT", value.typecomment);
                command.Parameters.AddWithValue("@COMMENT", value.comment);
                command.Parameters.AddWithValue("@DATE_COMMENT", value.DateCommentaire);
                command.Parameters.AddWithValue("@ID_WRITER", value.Id_Ecrivain);


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

        public static void UpdateCommentHisto(CommentHisto value)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();



            try
            {
                string selectQuery = "update basediag_devis";
                selectQuery += " set id_patient = @id_patient,";
                selectQuery += "    TYPE_COMMENT = @TYPE_COMMENT,";
                selectQuery += "    COMMENT = @COMMENT,";
                selectQuery += "    DATE_COMMENT = @DATE_COMMENT,";
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


        #endregion
    }
}
