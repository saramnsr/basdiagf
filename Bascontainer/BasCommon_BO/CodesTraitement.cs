using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public static class CodesTraitement
    {

      
        public static bool IsOrthopedie(string code)
        {
            return (code == ORTHOPEDIE);
        }
       
        public static bool IsOrthodontieEnfant(string code)
        {
            return ((code == ORTHODONTIEMULTIBAGUEMETAL) ||
                (code == ORTHODONTIEMULTIBAGUELINGUAL) ||
                (code == ORTHODONTIEMULTIBAGUECERAMIQUE) ||
                (code == ORTHODONTIEINVISALIGN)
                );
        }

        public static bool IsContention(string code)
        {
            return ((code == CONTENTION1) ||
                (code == CONTENTION2) ||
                (code == CONTENTIONINVISALIGN1) ||
                (code == CONTENTIONINVISALIGN2)
                );
        }

        public static bool IsContention1(string code)
        {
            return ((code == CONTENTION1) ||
                (code == CONTENTIONINVISALIGN1) 
                );
        }

        public static bool IsContention2(string code)
        {
            return ((code == CONTENTION2) ||
                (code == CONTENTIONINVISALIGN2)
                );
        }

        public static bool IsAdulte(string code)
        {
            return ((code == ORTHODONTIEADULTEMULTIBAGUEMETAL) ||
                (code == ORTHODONTIEADULTEMULTIBAGUECERAMIQUE) ||
                (code == ORTHODONTIEADULTEMULTIBAGUELINGUAL) ||
                (code == ORTHODONTIEADULTEINVARCADE) ||
                (code == ORTHODONTIEADULTEINVLIGHT) ||
                (code == ORTHODONTIEADULTEINVCOMPLET) ||
                (code == ORTHODONTIEADULTEINVCOMPLETCORR) ||
                (code == ORTHODONTIEADULTEINVCOMPLETCORRTIM) ||                
                (code == ORTHODONTIEADULTEINVCOMPLETDISJ) ||
                (code == ORTHODONTIEADULTEINVCOMPLETCHIR) ||
                (code == ORTHODONTIEADULTEINVCOMPLETDISJCHIR)||
                (code == ORTHODONTIEADULTEFINITION) ||
                (code == CONTENTIONADULTE) ||
                (code == ISEVEN) 
                
                );
        }

        public static bool IsFinitionAdulte(string code)
        {
            return (
                (code == ORTHODONTIEADULTEFINITION) 
                );
         }

        

        public static bool IsInvisalign(string code)
        {
            return ((code == ORTHODONTIEADULTEINVARCADE) ||
                (code == ORTHODONTIEADULTEINVLIGHT) ||
                (code == ORTHODONTIEADULTEINVCOMPLET) ||
                (code == ORTHODONTIEADULTEINVCOMPLETCORR) ||
                (code == ORTHODONTIEADULTEINVCOMPLETCORRTIM) || 
                (code == ORTHODONTIEADULTEINVCOMPLETDISJ) ||
                (code == ORTHODONTIEADULTEINVCOMPLETCHIR) ||
                (code == ORTHODONTIEADULTEINVCOMPLETDISJCHIR)||
                (code == ORTHODONTIEINVISALIGN) ||
                (code == ORTHODONTIEADULTEFINITION) ||
                (code == ORTHODONTIEINVISALIGNHN) ||
                (code == ISEVEN) 
                );
        }



        public static bool IsMBAdulte(string code)
        {
            return ((code == ORTHODONTIEADULTEMULTIBAGUEMETAL) ||
                (code == ORTHODONTIEADULTEMULTIBAGUECERAMIQUE) ||
                (code == ORTHODONTIEADULTEMULTIBAGUELINGUAL)
                );
        }

        public static bool IsInvisalignAdulte(string code)
        {
            return ((code == ORTHODONTIEADULTEINVARCADE) ||
                (code == ORTHODONTIEADULTEINVLIGHT) ||
                (code == ORTHODONTIEADULTEINVCOMPLET) ||
                (code == ORTHODONTIEADULTEINVCOMPLETCORR) ||
                (code == ORTHODONTIEADULTEINVCOMPLETCORRTIM) || 
                (code == ORTHODONTIEADULTEINVCOMPLETDISJ) ||
                (code == ORTHODONTIEADULTEINVCOMPLETCHIR) ||
                (code == ORTHODONTIEADULTEINVCOMPLETDISJCHIR) ||
                (code == ISEVEN) ||
                (code == ORTHODONTIEADULTEFINITION) 
                
                );
        }

        public static bool IsInvisalignLight(string code)
        {
            return ((code == ORTHODONTIEADULTEINVARCADE) ||
                (code == ORTHODONTIEADULTEINVLIGHT) 
                );
        }

        public static bool IsInvisalignFull(string code)
        {
            return ((code == ORTHODONTIEADULTEINVCOMPLET) ||
                (code == ORTHODONTIEADULTEINVCOMPLETCORR) ||
                (code == ORTHODONTIEADULTEINVCOMPLETCORRTIM) || 
                (code == ORTHODONTIEADULTEINVCOMPLETDISJ) ||
                (code == ORTHODONTIEADULTEINVCOMPLETCHIR) ||
                (code == ORTHODONTIEADULTEINVCOMPLETDISJCHIR) ||
                (code == ORTHODONTIEADULTEFINITION)||
                 (code == ISEVEN) 
                
                );
        }

        public static bool IsPediatrie(string code)
        {
            return (code == PEDIATRIE);
        }

        public static bool IsOrthodontie(string code)
        {
            return ((code == ORTHODONTIEADULTEMULTIBAGUEMETAL) ||
                (code == ORTHODONTIEADULTEMULTIBAGUECERAMIQUE) ||
                (code == ORTHODONTIEADULTEMULTIBAGUELINGUAL) ||
                (code == ORTHODONTIEADULTEINVARCADE) ||
                (code == ORTHODONTIEADULTEINVLIGHT) ||
                (code == ORTHODONTIEADULTEINVCOMPLET) ||
                (code == ORTHODONTIEADULTEINVCOMPLETCORR) ||
                (code == ORTHODONTIEADULTEINVCOMPLETCORRTIM) || 
                (code == ORTHODONTIEADULTEINVCOMPLETDISJ) ||
                (code == ORTHODONTIEADULTEINVCOMPLETCHIR) ||
                (code == ORTHODONTIEADULTEINVCOMPLETDISJCHIR) ||
                (code == ORTHODONTIEMULTIBAGUEMETAL) ||
                (code == ORTHODONTIEMULTIBAGUECERAMIQUE) ||
                (code == ORTHODONTIEMULTIBAGUELINGUAL) ||
                (code == ORTHODONTIEINVISALIGN) ||
                (code == ORTHODONTIEMULTIBAGUEMETALHN) ||
                (code == ORTHODONTIEMULTIBAGUECERAMIQUEHN) ||
                (code == ORTHODONTIEMULTIBAGUELINGUALHN) ||
                (code == ORTHODONTIEINVISALIGNHN)||
                (code == ORTHODONTIEADULTEFINITION) 
                );
        }

        public const string PEDIATRIE = "PEDIATRIE";

        public const string ORTHOPEDIE = "ORTHP";
        public const string ORTHOPEDIEHN = "ORTHP_HN";
                        
        public const string ORTHODONTIEADULTEMULTIBAGUEMETAL = "ODHNMBM";
        public const string ORTHODONTIEADULTEMULTIBAGUECERAMIQUE = "ODHNMBC";
        public const string ORTHODONTIEADULTEMULTIBAGUELINGUAL = "ODHNMBL";

        public const string ORTHODONTIEADULTEINVARCADE = "ODHNINVARC";
        public const string ORTHODONTIEADULTEINVLIGHT = "ODHNINVLIG";
        public const string ORTHODONTIEADULTEINVCOMPLET = "ODHNINVC";
        public const string ORTHODONTIEADULTEINVCOMPLETCORR = "ODHNINVCC";
        public const string ORTHODONTIEADULTEINVCOMPLETCORRTIM = "ODHNINVCCT";
        
        public const string ORTHODONTIEADULTEINVCOMPLETDISJ = "ODHNINVCD";
        public const string ORTHODONTIEADULTEINVCOMPLETCHIR = "ODHNINVCCH";
        public const string ORTHODONTIEADULTEINVCOMPLETDISJCHIR = "ODHNINVCDC";
        public const string ORTHODONTIEADULTEFINITION = "ODHNINVFIN";
        

        public const string ORTHODONTIEMULTIBAGUEMETAL = "MBMETAL";
        public const string ORTHODONTIEMULTIBAGUECERAMIQUE = "MBCERAM";
        public const string ORTHODONTIEMULTIBAGUELINGUAL = "MBLING";
        public const string ORTHODONTIEINVISALIGN = "INVTEEN";

        public const string ORTHODONTIEMULTIBAGUEMETALHN = "MBMETAL_HN";
        public const string ORTHODONTIEMULTIBAGUECERAMIQUEHN = "MBCERAM_HN";
        public const string ORTHODONTIEMULTIBAGUELINGUALHN = "MBLING_HN";
        public const string ORTHODONTIEINVISALIGNHN = "INVTEEN_HN";

        public const string CONTENTION1 = "CONT1";
        public const string CONTENTION2 = "CONT2";

        public const string CONTENTIONINVISALIGN1 = "CONTINVI1";
        public const string CONTENTIONINVISALIGN2 = "CONTINVI2";

        public const string CONTENTIONADULTE = "CONTADULTE";

        public const string SETUP3D = "SETUP3D";
        public const string ISEVEN = "ISEVEN";
      
        


    }
}
