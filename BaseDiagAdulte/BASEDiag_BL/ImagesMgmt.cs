using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using BASEDiag_BO;
using BASEDiag_DAL;
using BasCommon_BO;

namespace BASEDiag_BL
{
    static class ImagesMgmt
    {

        private static void AffectAttributsToImage(ObjImage img)
        {
            DataTable dt = DAC_BASeView.getAttribut(img);

            List<ObjImage> lst = new List<ObjImage>();

            foreach (DataRow r in dt.Rows)
            {
                Attribut att = Builders.BuildAttribut(r);
                img.attributs.Add(att);
            }

        }

        private static List<ObjImage> getObjectOf(basePatient p_pat)
        {
            DataTable dt = DAC_BASeView.getObjectOf(p_pat);

            List<ObjImage> lst = new List<ObjImage>();

            foreach (DataRow r in dt.Rows)
            {
                ObjImage image = Builders.BuildObjImage(r);
                AffectAttributsToImage(image);

                lst.Add(image);
            }

            return lst;

        }

        public static void AffectImageToPatient(ref basePatient p_pat)
        {
            if (p_pat.lstObjFrmKitView == null)
                p_pat.lstObjFrmKitView = new lstObjFrmKitView();
            List<ObjImage> lst = getObjectOf(p_pat);
            foreach (ObjImage obj in lst)
            {
                p_pat.lstObjFrmKitView.Add(obj);
                // Portrail du profil : portrait + profil
                //
                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Portrait"]) &&
                    obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Profil"]) &&
                    !obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Sourire"]))
                    p_pat.Img_Ext_Profile = basePatient.RepertoireImage + "\\" + p_pat.Repertoire + "\\" + obj.fichier;

                // Portrait du face : portrait + face
                //
                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Portrait"]) &&
                    obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Face"]) &&
                    !obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Sourire"]))
                    p_pat.Img_Ext_Face = basePatient.RepertoireImage + "\\" + p_pat.Repertoire + "\\" + obj.fichier;


                // Portrait face sourire : portrait + face + sourire
                //
                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Portrait"]) &&
                    obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Face"]) &&
                    obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Sourire"]))
                    p_pat.Img_Ext_Face_Sourire = basePatient.RepertoireImage + "\\" + p_pat.Repertoire + "\\" + obj.fichier;

                // Portrait profil sourire : portrait + face + sourire
                //
                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Portrait"]) &&
                    obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Profil"]) &&
                    obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Sourire"]))
                    p_pat.Img_Ext_Profile_Sourire = basePatient.RepertoireImage + "\\" + p_pat.Repertoire + "\\" + obj.fichier;

                // Sourire face : sourire + portrait
                //
                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Portrait"]) &&
                    !obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Face"]) &&
                    !obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Profil"]) &&
                    obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Sourire"]))
                    p_pat.Img_Ext_Sourire = basePatient.RepertoireImage + "\\" + p_pat.Repertoire + "\\" + obj.fichier;

                // Maxillaire : intrabuccale + maxillaire
                //
                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Intrabuccale"]) &&
                    obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Maxillaire"]))
                    p_pat.Img_Int_Max = basePatient.RepertoireImage + "\\" + p_pat.Repertoire + "\\" + obj.fichier;


                // Mandibulaire : intrabuccale + mandibulaire
                //
                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Intrabuccale"]) &&
                    obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Mandibulaire"]))
                    p_pat.Img_Int_Man = basePatient.RepertoireImage + "\\" + p_pat.Repertoire + "\\" + obj.fichier;

                // Intra droite : intrabuccale + droite
                //
                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Intrabuccale"]) &&
                    obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Droite"]))
                    p_pat.Img_Int_Droit = basePatient.RepertoireImage + "\\" + p_pat.Repertoire + "\\" + obj.fichier;

                // Intra surplomb : intrabuccale + surplomb
                //
                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Intrabuccale"]) &&
                    obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Surplomb"]))
                    p_pat.Img_Int_SurPlomb = basePatient.RepertoireImage + "\\" + p_pat.Repertoire + "\\" + obj.fichier;


                // Intra face : intrabuccale + face
                //

                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Intrabuccale"]) &&
                    obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Face"]))
                    p_pat.Img_Int_Face = basePatient.RepertoireImage + "\\" + p_pat.Repertoire + "\\" + obj.fichier;

                // Intra gauche : intrabuccale + gauche
                //
                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Intrabuccale"]) &&
                    obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Gauche"]))
                    p_pat.Img_Int_Gauche = basePatient.RepertoireImage + "\\" + p_pat.Repertoire + "\\" + obj.fichier;

                // Radio profil : Radio + Profil
                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Radio"]) &&
                    obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Profil"]))
                    p_pat.Img_Rad_Profile = basePatient.RepertoireImage + "\\" + p_pat.Repertoire + "\\" + obj.fichier;


                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Radio"]) &&
                    obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Panoramique"]))
                    p_pat.Img_Rad_Pano = basePatient.RepertoireImage + "\\" + p_pat.Repertoire + "\\" + obj.fichier;

                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Radio"]) &&
                     obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Face"]))
                    p_pat.Img_Rad_Face = basePatient.RepertoireImage + "\\" + p_pat.Repertoire + "\\" + obj.fichier;

                //moulages
                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Moulage"]) &&
                     obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Maxillaire"]))
                    p_pat.Img_Moul_Max = basePatient.RepertoireImage + "\\" + p_pat.Repertoire + "\\" + obj.fichier;

                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Moulage"]) &&
                     obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Mandibulaire"]))
                    p_pat.Img_Moul_Man = basePatient.RepertoireImage + "\\" + p_pat.Repertoire + "\\" + obj.fichier;

                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Moulage"]) &&
                     obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Droite"]))
                    p_pat.Img_Moul_Droit = basePatient.RepertoireImage + "\\" + p_pat.Repertoire + "\\" + obj.fichier;

                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Moulage"]) &&
                     obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Gauche"]))
                    p_pat.Img_Moul_Gauche = basePatient.RepertoireImage + "\\" + p_pat.Repertoire + "\\" + obj.fichier;


                if (obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Moulage"]) &&
                     obj.HasAttribut(ConfigurationManager.AppSettings["Attr_Face"]))
                    p_pat.Img_Moul_Face = basePatient.RepertoireImage + "\\" + p_pat.Repertoire + "\\" + obj.fichier;




                /*
                if (obj.HasAttribut(Config.ReadParams("Attr_Portrait")))
                {
                    if (obj.HasAttribut(Config.ReadParams("Attr_Face")))
                    {
                        if (obj.HasAttribut(Config.ReadParams("Attr_Sourire")))
                        {
                            p_pat.Img_Ext_Sourire = RepertoireImage + "\\" + obj.Repertoire + "\\" + obj.fichier;
                        }
                        else p_pat.Img_Ext_Face = RepertoireImage + "\\" + obj.Repertoire + "\\" + obj.fichier;
                        
                    }
                    if (obj.HasAttribut(Config.ReadParams("Attr_Profil")))
                    {
                        if (obj.HasAttribut(Config.ReadParams("Attr_Sourire")))
                        {
                            p_pat.Img_Ext_Profile_Sourire = RepertoireImage + "\\" + obj.Repertoire + "\\" + obj.fichier;
                        }
                        else p_pat.Img_Ext_Profile = RepertoireImage + "\\" + obj.Repertoire + "\\" + obj.fichier;
                        
                    }
                }
                    
                
                if (obj.HasAttribut(Config.ReadParams("Attr_Intrabuccale")))
                {
                    if (obj.HasAttribut(Config.ReadParams("Attr_Maxillaire")))
                    {
                        p_pat.Img_Int_Max = RepertoireImage + "\\" + obj.Repertoire + "\\" + obj.fichier;
                    }
                    if (obj.HasAttribut(Config.ReadParams("Attr_Mandibulaire")))
                    {
                        p_pat.Img_Int_Man = RepertoireImage + "\\" + obj.Repertoire + "\\" + obj.fichier;
                    }
                    if (obj.HasAttribut(Config.ReadParams("Attr_Droite")))
                    {
                        p_pat.Img_Int_Droit = RepertoireImage + "\\" + obj.Repertoire + "\\" + obj.fichier;
                    }
                    if (obj.HasAttribut(Config.ReadParams("Attr_Gauche")))
                    {
                        p_pat.Img_Int_Gauche = RepertoireImage + "\\" + obj.Repertoire + "\\" + obj.fichier;
                    }
                    if (obj.HasAttribut(Config.ReadParams("Attr_Face")))
                    {
                        p_pat.Img_Int_Face = RepertoireImage + "\\" + obj.Repertoire + "\\" + obj.fichier;
                    }
                }
                if (obj.HasAttribut(Config.ReadParams("Attr_Moulage")))
                {
                    if (obj.HasAttribut(Config.ReadParams("Attr_Maxillaire")))
                    {
                        p_pat.Img_Moul_Max = RepertoireImage + "\\" + obj.Repertoire + "\\" + obj.fichier;
                    }
                    if (obj.HasAttribut(Config.ReadParams("Attr_Mandibulaire")))
                    {
                        p_pat.Img_Moul_Man = RepertoireImage + "\\" + obj.Repertoire + "\\" + obj.fichier;
                    }
                    if (obj.HasAttribut(Config.ReadParams("Attr_Droite")))
                    {
                        p_pat.Img_Moul_Droit = RepertoireImage + "\\" + obj.Repertoire + "\\" + obj.fichier;
                    }
                    if (obj.HasAttribut(Config.ReadParams("Attr_Gauche")))
                    {
                        p_pat.Img_Moul_Gauche = RepertoireImage + "\\" + obj.Repertoire + "\\" + obj.fichier;
                    }
                    if (obj.HasAttribut(Config.ReadParams("Attr_Face")))
                    {
                        p_pat.Img_Moul_Face = RepertoireImage + "\\" + obj.Repertoire + "\\" + obj.fichier;
                    }
                }

                if (obj.HasAttribut(Config.ReadParams("Attr_Radio")))
                {
                    if (obj.HasAttribut(Config.ReadParams("Attr_Panoramique")))
                    {
                        p_pat.Img_Rad_Pano = RepertoireImage + "\\" + obj.Repertoire + "\\" + obj.fichier;
                    }
                    if (obj.HasAttribut(Config.ReadParams("Attr_Profil")))
                    {
                        p_pat.Img_Rad_Profile = RepertoireImage + "\\" + obj.Repertoire + "\\" + obj.fichier;
                    }
                    if (obj.HasAttribut(Config.ReadParams("Attr_Face")))
                    {
                        p_pat.Img_Rad_Face = RepertoireImage + "\\" + obj.Repertoire + "\\" + obj.fichier;
                    }                    
                }
                 */
            }

        }
    }

}
