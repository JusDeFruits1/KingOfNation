using System;
using System.ComponentModel;

public class Machine_de_guerre : Soldat, INotifyPropertyChanged  {
    /// <summary>
    /// Le nom du soldat
    /// </summary>
    private string nom;
    private string description;
    private int nb;

    public event PropertyChangedEventHandler PropertyChanged;

    public Machine_de_guerre(string nom, int nb)
    {
        this.nom = nom;        
        this.nb = nb;
    }

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

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
