using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using BasCommon_BO;
using System.Diagnostics;
using MySql.Data.MySqlClient;
namespace BasCommon_DAL
{
    public static partial class DAC
    {
        public static DataTable getHorairesPointeuse(Utilisateur p_utilisateur)
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


            try
            {
                string selectQuery = "select id, ";
                selectQuery += "        date_pt, ";
                selectQuery += "        nom, ";
                selectQuery += "        prenom, ";
                selectQuery += "        departement,";
                selectQuery += "        entree, ";
                selectQuery += "        sortie, ";
                selectQuery += "        verification, ";
                selectQuery += "        modifiepar, ";
                selectQuery += "        inerror";
                selectQuery += " from rh_base_pointeuse_cleaned";
                selectQuery += " where upper(nom)=@nom and upper(prenom)=@prenom";
                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@nom", p_utilisateur.Nom.ToUpper());
                command.Parameters.AddWithValue("@prenom", p_utilisateur.Prenom.ToUpper());


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

        public static DataTable getHorairesReel(Utilisateur p_utilisateur,int weekNum)
        {
#if TRACE
            //   Debug.Write("getHorairesDeTravail " + p_utilisateur.ToString());
#endif
            if (connection == null) getConnection();

            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
            }
            catch (System.Exception e)
            {
                throw e;
            }


            try
            {
                string selectQuery = "select id_utilisateur, ";
                selectQuery += "        id, ";
                selectQuery += "        annee, ";
                selectQuery += "        weeknum, ";
                selectQuery += "        starttime, ";
                selectQuery += "        endtime, ";
                selectQuery += "        daynum";
                selectQuery += " from rh_base_horaires_reel_saisie";
                selectQuery += " where id_utilisateur=@idut and weeknum=@weeknum";
                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@idut", p_utilisateur.Id);
                command.Parameters.AddWithValue("@weeknum", weekNum);


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


        public static DataTable getHorairesReel(Utilisateur p_utilisateur)
        {
#if TRACE
            //   Debug.Write("getHorairesDeTravail " + p_utilisateur.ToString());
#endif
            if (connection == null) getConnection();  

            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
            }
            catch (System.Exception e)
            {
                throw e;
            }


            try
            {
                string selectQuery = "select id_utilisateur, ";
                selectQuery += "        id, ";
                selectQuery += "        annee, ";
                selectQuery += "        weeknum, ";
                selectQuery += "        starttime, ";
                selectQuery += "        endtime, ";
                selectQuery += "        daynum";
                selectQuery += " from rh_base_horaires_reel_saisie";
                selectQuery += " where id_utilisateur=@idut";
                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@idut", p_utilisateur.Id);


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

        public static DataTable getHorairesDeTravail(Utilisateur p_utilisateur)
        {
#if TRACE
            //   Debug.Write("getHorairesDeTravail " + p_utilisateur.ToString());
#endif
            if (connection == null) getConnection();  

            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
            }
            catch (System.Exception e)
            {
                throw e;
            }


            try
            {
                string selectQuery = "select id_utilisateur, week_day, starttime, endtime,PERIODICITY,daynum,FirstDate";
                selectQuery += " from rh_base_horaires_travail";
                selectQuery += " where id_utilisateur=@idut";
                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@idut", p_utilisateur.Id);


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

        public static DataTable getHolidays(Utilisateur p_utilisateur)
        {
#if TRACE
            // Debug.Write("getHolidays " + p_utilisateur.ToString());
#endif
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {

                string selectQuery = "select  id, id_personne, startdate, enddate, holiday_name";
                selectQuery += " from rh_base_holidays";
                selectQuery += " where id_personne=@id_personne";
                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@id_personne", p_utilisateur.Id);


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

        public static void updateHoliday(Holiday holiday)
        {

#if TRACE
            Debug.Write("updateholiday ");

#endif
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "update rh_base_holidays";
                selectQuery += " set id_personne = @id_personne,";
                selectQuery += " startdate = @startdate,";
                selectQuery += " enddate=@enddate,";
                selectQuery += " holiday_name=@holiday_name";
                selectQuery += " where id=@id";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);


                command.Parameters.AddWithValue("@id", holiday.Id);
                command.Parameters.AddWithValue("@id_personne", holiday.personne.Id);
                command.Parameters.AddWithValue("@startdate", holiday.startdate);
                command.Parameters.AddWithValue("@enddate", holiday.enddate);
                command.Parameters.AddWithValue("@holiday_name", holiday.holiday_name);
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

        public static void AddHoliday(Utilisateur p_utilisateur, Holiday holiday)
        {

#if TRACE
            Debug.Write("AddHoliday " + p_utilisateur.ToString());

#endif
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select MAX(ID)+1 as NEWID from rh_base_holidays";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                object res = command.ExecuteScalar();
                if (res is DBNull)
                    holiday.Id = 1;
                else
                    holiday.Id = Convert.ToInt32(command.ExecuteScalar());


                selectQuery = "insert into rh_base_holidays (id,id_personne, startdate, enddate, holiday_name)";
                selectQuery += " values (@id,@id_personne,@startdate, @enddate, @holiday_name)";

                command.CommandText = selectQuery;


                command.Parameters.AddWithValue("@id", holiday.Id);
                command.Parameters.AddWithValue("@id_personne", holiday.personne.Id);
                command.Parameters.AddWithValue("@startdate", holiday.startdate);
                command.Parameters.AddWithValue("@enddate", holiday.enddate);
                command.Parameters.AddWithValue("@holiday_name", holiday.holiday_name);
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

        public static void DelHoliday(Holiday holiday)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "delete from rh_base_holidays where id=@id";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", holiday.Id);
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

        public static void SaveHorairesDeTravail(Utilisateur p_utilisateur)
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
                string selectQuery = " delete from rh_base_horaires_travail";
                selectQuery += " where id_utilisateur=@id_utilisateur";
                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id_utilisateur", p_utilisateur.Id);

                command.ExecuteNonQuery();

                selectQuery = "insert into rh_base_horaires_travail (id_utilisateur, week_day, starttime, endtime,PERIODICITY,daynum,FirstDate)";
                selectQuery += " values (@id_utilisateur, @week_day, @starttime, @endtime,@PERIODICITY,@day,@FirstDate)";
                command = new MySqlCommand(selectQuery, connection, transaction);

                if (p_utilisateur.horairesDeTravail!=null)
                    foreach (BasCommon_BO.HorairesDeTravail ht in p_utilisateur.horairesDeTravail)
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@id_utilisateur", p_utilisateur.Id);
                        command.Parameters.AddWithValue("@week_day", ht.week_day);
                        command.Parameters.AddWithValue("@starttime", ht.starttime);
                        command.Parameters.AddWithValue("@endtime", ht.endtime);
                        command.Parameters.AddWithValue("@PERIODICITY", ht.period.MonthPeriodicityNum);

                        if (ht.period.tpeperiod == BasCommon_BO.HoraireTrPeriodicity.TypePeriodicity.XSemainesSur)
                        {
                            command.Parameters.AddWithValue("@day", DBNull.Value);
                            command.Parameters.AddWithValue("@FirstDate", ht.period.FirstDate);
                        }

                        if (ht.period.tpeperiod == BasCommon_BO.HoraireTrPeriodicity.TypePeriodicity.JourParMois)
                        {
                            command.Parameters.AddWithValue("@day", ht.period.MonthPeriodicityDay);
                            command.Parameters.AddWithValue("@FirstDate", DBNull.Value);
                        }

                        command.ExecuteNonQuery();
                    }
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



        public static void DeleteHorairesReel(int Startyear, int Startweeknum, int Startdaynum, DateTime Starttime,
                                         int Endyear, int Endweeknum, int Enddaynum, DateTime Endtime,Utilisateur user)
        {
            if (connection == null) getConnection(); if (connection == null) return;


            if (connection.State == ConnectionState.Closed) connection.Open();


            MySqlTransaction transaction = connection.BeginTransaction();

            try
            {
                string selectQuery = "delete from rh_base_horaires_reel_saisie  ";
                selectQuery += " where ANNEE>=@Startyear and WEEKNUM>=@Startweeknum and STARTTIME>=@Starttime and DAYNUM>=@Startdaynum";
                selectQuery += " and ANNEE<=@Endyear and WEEKNUM<=@Endweeknum and ENDTIME<=@Endtime and DAYNUM<=@Enddaynum and ID_UTILISATEUR = @user";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);




                command.Parameters.Clear();
                command.Parameters.AddWithValue("@user", user.Id);
                command.Parameters.AddWithValue("@Startyear", Startyear);
                command.Parameters.AddWithValue("@Startweeknum", Startweeknum);
                command.Parameters.AddWithValue("@Starttime", Starttime);
                command.Parameters.AddWithValue("@Startdaynum", Startdaynum);
                command.Parameters.AddWithValue("@Endyear", Endyear);
                command.Parameters.AddWithValue("@Endweeknum", Endweeknum);
                command.Parameters.AddWithValue("@Endtime", Endtime);
                command.Parameters.AddWithValue("@Enddaynum", Enddaynum);

                command.ExecuteNonQuery();

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

        public static void DeleteHorairePlanning(int dayWeek,int weekNum,int year,int id)
        {
            if (connection == null) getConnection(); if (connection == null) return;


            if (connection.State == ConnectionState.Closed) connection.Open();


            MySqlTransaction transaction = connection.BeginTransaction();

            try
            {
                string selectQuery = " delete from rh_base_horaires_reel_saisie";
                selectQuery += " where id_utilisateur=@id_utilisateur and WEEKNUM=@WEEKNUM and DAYNUM=@DAYNUM and ANNEE=@year";
                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@DAYNUM", dayWeek);
                command.Parameters.AddWithValue("@WEEKNUM", weekNum);
                command.Parameters.AddWithValue("@year", year);
                command.Parameters.AddWithValue("@id_utilisateur", id);
                command.ExecuteNonQuery();
                command.ExecuteNonQuery();

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
        public static void DeleteHoraire(HoraireReel hr)
        {
            if (connection == null) getConnection(); if (connection == null) return;

           
           if (connection.State == ConnectionState.Closed) connection.Open();
          

            MySqlTransaction transaction = connection.BeginTransaction();

            try
            {
                string selectQuery = "delete from rh_base_horaires_reel_saisie  ";
                selectQuery += " where id=@id ";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                



                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", hr.id);

                command.ExecuteNonQuery();

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


        public static void UpdateHorairesReel(HoraireReel hr)
        {
            if (connection == null) getConnection(); if (connection == null) return;

           
           if (connection.State == ConnectionState.Closed) connection.Open();
          

            MySqlTransaction transaction = connection.BeginTransaction();

            try
            {
                string selectQuery = "update rh_base_horaires_reel_saisie set annee=@annee, ";
                selectQuery += "                           weeknum=@weeknum, ";
                selectQuery += "                           starttime=@starttime, ";
                selectQuery += "                           endtime=@endtime, ";
                selectQuery += "                           daynum=@daynum,";
                selectQuery += "                           id_utilisateur=@id_utilisateur";
                selectQuery += " where id=@id ";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                



                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", hr.id);
                command.Parameters.AddWithValue("@id_utilisateur", hr.id_utilisateur);
                command.Parameters.AddWithValue("@annee", hr.Year);
                command.Parameters.AddWithValue("@weeknum", hr.WeekNum);
                command.Parameters.AddWithValue("@daynum", hr.week_day);
                command.Parameters.AddWithValue("@starttime", hr.starttime);
                command.Parameters.AddWithValue("@endtime", hr.endtime);


                command.ExecuteNonQuery();

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

        public static void AddHorairesReel(HoraireReel hr)
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
                string selectQuery = "select max(id)+1 as m from  rh_base_horaires_reel_saisie";
                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                object o = command.ExecuteScalar();
                if (o is DBNull) hr.id = 1; else hr.id = Convert.ToInt32(o);


                selectQuery = "insert into rh_base_horaires_reel_saisie (id,";
                selectQuery += "                          id_utilisateur, ";
                selectQuery += "                          annee, ";
                selectQuery += "                           weeknum, ";
                selectQuery += "                           starttime, ";
                selectQuery += "                           endtime, ";
                selectQuery += "                           daynum)";
                selectQuery += " values (@id,";
                selectQuery += " @id_utilisateur, ";
                selectQuery += "         @annee, ";
                selectQuery += "         @weeknum, ";
                selectQuery += "         @starttime, ";
                selectQuery += "         @endtime, ";
                selectQuery += "         @daynum)";

                command.CommandText = selectQuery;



                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", hr.id);
                command.Parameters.AddWithValue("@id_utilisateur", hr.id_utilisateur);
                command.Parameters.AddWithValue("@annee", hr.Year);
                command.Parameters.AddWithValue("@weeknum", hr.WeekNum);
                command.Parameters.AddWithValue("@daynum", hr.week_day);
                command.Parameters.AddWithValue("@starttime", hr.starttime);
                command.Parameters.AddWithValue("@endtime", hr.endtime);


                command.ExecuteNonQuery();

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

    }
}
