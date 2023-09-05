using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;

namespace BasCommon_BO.ElementsEnBouche.BO
{
    public abstract class ElementDent:IElementDent
    {

        public abstract string Dents
        {
            get;
            set;
        }

        public abstract List<int> LstDent
        {
            get;


        }

        private int _IdCommFin = -1;
        public int IdCommFin
        {
            get
            {
                return _IdCommFin;
            }
            set
            {
                _IdCommFin = value;
            }
        }

        private int _IdCommDebut = -1;
        public int IdCommDebut
        {
            get
            {
                return _IdCommDebut;
            }
            set
            {
                _IdCommDebut = value;
            }
        }

        public bool Haut { get { return IsDentEnHaut(); } }
        public bool Bas { get { return IsDentEnBas(); } }
        

        public abstract ElementDent.Materials typeMaterial { get; }
        public abstract string  ShortLib { get; } 


        public enum Materials
        {
            Aucun = 0,
            TIM = 1,
            Kobayashi = 2,
            Ligature = 3,
            LigatureM = 4,
            chainette = 5,
            MiniVis = 6,
            Arc = 7,
            Ressort = 8

        }

        private int _Id = -1;
        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }


        private basePatient _patient;
        public basePatient patient
        {
            get
            {
                return _patient;
            }
            set
            {
                _patient = value;
            }
        }

        private int _IdPatient;
        public int IdPatient
        {
            get
            {
                if (patient != null) _IdPatient = patient.Id;
                return _IdPatient;
            }
            set
            {
                _IdPatient = value;
            }
        }

        private DateTime? _Datesuppression;
        public DateTime? Datesuppression
        {
            get
            {
                return _Datesuppression;
            }
            set
            {
                _Datesuppression = value;
            }
        }

        private DateTime? _DateInstallation = null;
        public DateTime? DateInstallation
        {
            get
            {
                return _DateInstallation;
            }
            set
            {
                _DateInstallation = value;
            }
        }


        protected bool IsDentEnHaut()
        {
            foreach (int dent in this.LstDent)
                if ((new int[] { 11, 12, 13, 14, 15, 16, 17, 18, 21, 22, 23, 24, 25, 26, 27, 28 }).Contains(dent))
                    return true;
            return false;
        }

        protected bool IsDentEnBas()
        {
            foreach (int dent in this.LstDent)
                if ((new int[] { 41, 42, 43, 44, 45, 46, 47, 48, 31, 32, 33, 34, 35, 36, 37, 38 }).Contains(dent))
                    return true;
            return false;
        }

    }
}
