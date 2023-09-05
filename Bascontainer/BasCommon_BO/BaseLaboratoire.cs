using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
  public  class BaseLaboratoire
    {
      private Laboratoire _laboratoire;
      public Laboratoire laboratoire
      {
          get { return _laboratoire;}
          set { _laboratoire = value; }
      }
      private int _id;
      public int id
      {
          get { return _id; }
          set { _id = value; }
      }
      private int _idLaboratoire;
      public int idLaboratoire
      {
          get { return _idLaboratoire; }
          set { _idLaboratoire = value; }
      }
      private int _idpatient;
      public int idpatient
      {
          get { return _idpatient; }
          set { _idpatient = value; }
      }
      private basePatient _patient;
      public basePatient patient
      {
          get { return _patient; }
          set { _patient = value; }
      }
       private double _montant;
       public double montant
      {
          get { return _montant; }
          set { _montant = value; }
      }
       private DateTime _date;
       public DateTime date
       {
           get { return _date; }
           set { _date = value; }
       }
    }
}
