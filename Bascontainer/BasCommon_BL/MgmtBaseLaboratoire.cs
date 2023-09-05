using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;
using BasCommon_DAL;
using System.Data;
using Newtonsoft.Json.Linq;
namespace BasCommon_BL
{
  public static class MgmtBaseLaboratoire
    {
      private static List<Laboratoire> _lstLaboratoires;
      public static List<Laboratoire> lstLaboratoires
        {
            get
            {
                if (_lstLaboratoires == null || _lstLaboratoires.Count == 0) _lstLaboratoires = getlstLaboratoires();
                return _lstLaboratoires;
            }
        }
      public static List<Laboratoire> getlstLaboratoires()
      {
          List<Laboratoire> _listeLabs = new List<Laboratoire>();


          JArray json = DAC.getMethodeJsonArray("/GetAllLaboratoire");

          foreach (JObject r in json)
          {
              Laboratoire lab = Builders.BuildLaboratoire.BuildJ(r);
              _listeLabs.Add(lab);
          }

          return _listeLabs;
      }
      public static List<Laboratoire> getlstLaboratoiresOLD()
      {
          List<Laboratoire> _listeLabs = new List<Laboratoire>();


          DataTable dt = DAC.getAllLaboratoire();

          foreach (DataRow r in dt.Rows)
          {
              Laboratoire lab = Builders.BuildLaboratoire.Build(r);
              _listeLabs.Add(lab);
          }

          return _listeLabs;
      }
      public static BaseLaboratoire getBaseLaboratoire(int idPatient)
      {

          DataRow r = DAC.getBaseLaboratoire(idPatient);
          if (r != null)
          {
              BaseLaboratoire balb = Builders.BuildBaseLaboratoire.Build(r);
              return balb;
          }
          else
              return null;
      }

      public static List<BaseLaboratoire> getBaseLaboratoires(DateTime dtdebut,DateTime datefin ,int idlabs)
      {
          JArray json = DAC.getMethodeJsonArray("/BaseLaboratoirebyDate/" + dtdebut.Date.ToString("yyyy-MM-dd HH:mm:ss") + "&" + datefin.Date.ToString("yyyy-MM-dd HH:mm:ss") + "&" + idlabs);
          List<BaseLaboratoire> _listeLabs = new List<BaseLaboratoire>();
          foreach (JObject r in json)
          {
              BaseLaboratoire balb = Builders.BuildBaseLaboratoire.BuildJ(r);
              _listeLabs.Add(balb);
          }
          return _listeLabs;
      }
      public static List<BaseLaboratoire> getBaseLaboratoiresOLD(DateTime dtdebut, DateTime datefin, int idlabs, bool All = false)
      {

          List<BaseLaboratoire> _listeLabs = new List<BaseLaboratoire>();
          DataTable dt = DAC.getBaseLaboratoires(dtdebut, datefin, idlabs, All);
          foreach (DataRow r in dt.Rows)
          {
              BaseLaboratoire balb = Builders.BuildBaseLaboratoire.Build(r);
              _listeLabs.Add(balb);
          }
          return _listeLabs;
      }
      public static Laboratoire getLaboratoires(int id)
      {
          
          if (lstLaboratoires.Count == 0)
              _lstLaboratoires = getlstLaboratoires();
          return lstLaboratoires.Find(w => w.id == id);
      }
      public static void insertBaseLab(BaseLaboratoire basl)
      {
          DAC.InsertBaseLaboratoire(basl);
      }
      public static void updateBaseLab(BaseLaboratoire basl)
      {
          DAC.updateBaseLaboratoire(basl);
      }
    }
}
