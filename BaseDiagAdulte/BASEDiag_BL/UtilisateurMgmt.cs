using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BASEDiag_BO;
using BASEDiag_DAL;

namespace BASEDiag_BL
{
    public static class UtilisateursMgt
    {
        private static List<Utilisateur> _utilisateurs;
        public static List<Utilisateur> utilisateurs
        {
            get
            {
                if (_utilisateurs != null)
                    return _utilisateurs;
                else
                    return getUtilisateurs();
            }

        }


        public static void ForceRefreshUserList()
        {
            _utilisateurs.Clear();
            getUtilisateurs();
        }

        private static List<Utilisateur> getUtilisateurs()
        {

            DataTable dt = DAC.getUtilisateurs();

            _utilisateurs = new List<Utilisateur>();

            foreach (DataRow r in dt.Rows)
                _utilisateurs.Add(Builders.BuildUtilisateur(r));


            return _utilisateurs;
        }


        public static Utilisateur getUtilisateur(int Id)
        {
            foreach (Utilisateur ut in utilisateurs)
                if (ut.Id == Id) return ut;

            return null;
        }

        public static Utilisateur getUtilisateur(string nom)
        {
            foreach (Utilisateur ut in utilisateurs)
                if (ut.Nom.ToUpper() == nom.ToUpper()) return ut;

            return null;
        }

        public static List<Utilisateur> getUtilisateurInFauteuil(Fauteuil f, DateTime dte)
        {
            DataTable dt = DAC.getUtilisateursInFauteuil(f, dte);

            List<Utilisateur> res = new List<Utilisateur>();

            foreach (DataRow r in dt.Rows)
                foreach (Utilisateur u in utilisateurs)
                    if (Convert.ToInt32(r["ID_USER"]) == u.Id)
                        res.Add(u);

            return res;
        }

    }
}
