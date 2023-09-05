using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class InfosInvisalign
    {


        private DateTime _DateFinInvisalign;
        public DateTime DateFinInvisalign
        {
            get
            {
                return _DateFinInvisalign;
            }
            set
            {
                _DateFinInvisalign = value;
            }
        }
        

        private InvisalignPrescriptionFullObj.InvisalignType _TpeTrmnt;
        public InvisalignPrescriptionFullObj.InvisalignType TpeTrmnt
        {
            get
            {
                return _TpeTrmnt;
            }
            set
            {
                _TpeTrmnt = value;
            }
        }
        
        

        private InvisalignEnBouche.RDVFrequency _FreqRDV = InvisalignEnBouche.RDVFrequency._6Mois;
        public InvisalignEnBouche.RDVFrequency FreqRDV
        {
            get
            {
                return _FreqRDV;
            }
            set
            {
                _FreqRDV = value;
            }
        }
        

        private InvisalignEnBouche.ChangeFrequency _FreqChangemnt = InvisalignEnBouche.ChangeFrequency.S1;
        public InvisalignEnBouche.ChangeFrequency FreqChangemnt
        {
            get
            {
                return _FreqChangemnt;
            }
            set
            {
                _FreqChangemnt = value;
            }
        }
        


        private string _PrenomInvisalign;
        public string PrenomInvisalign
        {
            get
            {
                return _PrenomInvisalign;
            }
            set
            {
                _PrenomInvisalign = value;
            }
        }
        

        private string _NomInvisalign;
        public string NomInvisalign
        {
            get
            {
                return _NomInvisalign;
            }
            set
            {
                _NomInvisalign = value;
            }
        }
        

        private string _IdInvisalign;
        public string IdInvisalign
        {
            get
            {
                return _IdInvisalign;
            }
            set
            {
                _IdInvisalign = value;
            }
        }
        
    }
}
