using KingOfNation.Code;
using System;
using System.ComponentModel;

public class Lourd : Soldat,INotifyPropertyChanged  {


    #region Attributes

    public event PropertyChangedEventHandler PropertyChanged;

    private string nom;
    private string description;
    private int nb;

    #endregion

    #region Properties

    public override string Nom
    {
        get
        {
            return nom;
        }
    }

    public override string Description
    {
        get
        {
            return description;
        }
    }

    public override int Nb
    {
        get
        {
            return nb;
        }
        set
        {
            if (nb != value)
            {
                nb = value;
                OnPropertyChanged(nameof(Nb));
            }
        }
    }

    #endregion

    #region Constructor

    public Lourd(string nom, int nb)
    {
        this.nom = nom;
        this.description = description;
        this.nb = nb;
    }

    #endregion

    #region Operations

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion

}
