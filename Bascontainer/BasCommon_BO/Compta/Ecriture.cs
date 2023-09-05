using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO.Compta
{

    

    public class Ecriture
    {

        public enum TypeReglement
        {
            Inconnus = -2,
            Tous = -1,
            Cheque = 0,
            Especes = 1,
            CB = 3,
            Prelevement = 6,
            Virement = 7,
            AMEX = 9
        }


        int _IdPieceComptable = -1;

        public int IdPieceComptable { 
            get { if (piece != null)_IdPieceComptable = piece.Id; return _IdPieceComptable; } 
            set { _IdPieceComptable = value; } 
        }
        
        PieceComptable _piece = null;
        public PieceComptable piece { 
            get { return _piece; } 
            set { _piece = value; }
        }

        string _Idcodecompta = "";
        public string Idcodecompta {
            get { if (codecompta != null)_Idcodecompta = codecompta.Code; return _Idcodecompta; } 
            set { _Idcodecompta = value; }
        }
       

        public TaxeValeurAjoutee taxe { get; set; }
        public double? Credit { get; set; }
        public double? Debit { get; set; }
        public string Libelle { get; set; }
        public CodeComptable codecompta { get; set; }

        public TypeReglement Type { get; set; }

        int _Id = -1;
        public int Id { get { return _Id; } set { _Id = value; } }

        
    }

    public class MdlEcriture
    {

        int _IdMdlPieceComptable = -1;
        public int IdPieceComptable { get { if (piece != null)_IdMdlPieceComptable = piece.Id; return _IdMdlPieceComptable; } set { _IdMdlPieceComptable = value; } }
        MdlPieceComptable _piece = null;
        public MdlPieceComptable piece { get { return _piece; } set { _piece = value; } }

        string _Idcodecompta = "";
        public string Idcodecompta { get { if (codecompta != null)_Idcodecompta = codecompta.Code; return _Idcodecompta; } set { _Idcodecompta = value; } }
        
        public CodeComptable codecompta { get; set; }


        public TaxeValeurAjoutee taxe { get; set; }
        public double? Credit { get; set; }
        public double? Debit { get; set; }
        public string Libelle { get; set; }
        
        int _Id = -1;
        public int Id { get { return _Id; } set { _Id = value; } }


    }
}
