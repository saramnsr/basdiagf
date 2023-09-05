using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BasCommon_BO
{
    public class NewTraitement
    {



        public NewTraitement()
        {
            id_Traitement = -1;
        }
public override string ToString()
{
    return Traitement_libelle;
}
        //nadhem
        public enum typeScenario 
        {

            Prothése = 0,
            Prothése_CMUC = 1,
            Invisalign = 2,
            RCC = 3,
            Contention = 4,
            Invisalign_Teen = 5,
            RCC_CMUC = 6,
            Invisalign_S3FULL = 7,
            prothése_santéclair
        }
        private typeScenario _typeScenario = typeScenario.Prothése;
        [PropertyCanBeSerialized]
        public typeScenario TypeScenario
        {
            get
            {
                return _typeScenario;
            }
            set
            {
                _typeScenario = value;
            }
        }
private double? _Montant_Scenario;
public double? Montant_Scenario
{
    get
    {

        return _Montant_Scenario;



    }
    set
    {
        _Montant_Scenario = value;
    }
}
private int _id_Traitement;
[PropertyCanBeSerialized]
public int id_Traitement
{
    get
    {
        return _id_Traitement;
    }
    set
    {
        _id_Traitement = value;
    }
}

private string _Traitement_libelle;
[PropertyCanBeSerialized]
public string Traitement_libelle
{
    get
    {
        return _Traitement_libelle;
    }
    set
    {
        _Traitement_libelle = value;
    }
}


private string _Traitement_shortlib;
[PropertyCanBeSerialized]
public string Traitement_shortlib
{
    get
    {
        return _Traitement_shortlib;
    }
    set
    {
        _Traitement_shortlib = value;
    }
}

private Color _Traitement_couleur;
[PropertyCanBeSerialized]
public Color Traitement_couleur
{
    get
    {
        return _Traitement_couleur;
    }
    set
    {
        _Traitement_couleur = value;
    }
}

private FamillesTraitements _famille_Traitement;
[PropertyCanBeSerialized]
public FamillesTraitements famille_Traitement
{
    get
    {
        return _famille_Traitement;
    }
    set
    {
        _famille_Traitement = value;
    }
}
//hadhemi
private string _Traitement_commentaire;
[PropertyCanBeSerialized]
public string Traitement_commentaire
{
    get
    {
        return _Traitement_commentaire;
    }
    set
    {
        _Traitement_commentaire = value;
    }
}
private int _id_famille;
[PropertyCanBeSerialized]
public int id_famille
{
    get
    {
        return _id_famille;
    }
    set
    {
        _id_famille = value;
    }
}



private int _ordre;
[PropertyCanBeSerialized]
public int ordre
{
    get
    {
        return _ordre;
    }
    set
    {
        _ordre = value;
    }
}

        //

private double? _Montant;
public double? Montant
{
    get
    {
        double vMontantLigne = 0;

        foreach (CommTraitement act in _CommTraitement )
        {
            vMontantLigne = vMontantLigne + act.MontantLigne;
        }

        return vMontantLigne;



    }
    set
    {
        _Montant = value;
    }
}

private double? _MontantAvantRemise;
public double? MontantAvantRemise
{
    get
    {
        double vMontantLigneAvantRemise = 0;

        foreach (CommTraitement act in _CommTraitement)
        {
            vMontantLigneAvantRemise = vMontantLigneAvantRemise + act.MontantLigneAvantRemise;
        }

        return vMontantLigneAvantRemise;


    }
    set
    {
        _MontantAvantRemise = value;
    }
}

        //

private int? _IdProposition = null;
public int? IdProposition
{
    get
    {
        if (Parent != null) _IdProposition = Parent.Id;
        return _IdProposition;
    }
    set
    {
        _IdProposition = value;
    }
}

private Boolean _contention = false;
public Boolean contention
{
    get
    {
        return _contention;
    }
    set
    {
        _contention = value;
    }
}
private Proposition _Parent;
[PropertyCanBeSerialized]
public Proposition Parent
{
    get
    {
        return _Parent;
    }
    set
    {
        _Parent = value;
    }
}

private List<Semestre> _semestres = new List<Semestre>();
[PropertyCanBeSerialized]
public List<Semestre> semestres
{
    get
    {
        return _semestres;
    }
    set
    {
        _semestres = value;
    }
}

private List<CommTraitement> _CommTraitement = new List<CommTraitement>();
[PropertyCanBeSerialized]
public List<CommTraitement> CommTraitement
{
    get
    {
        return _CommTraitement;
    }
    set
    {
        _CommTraitement = value;
    }
}
    }
}
