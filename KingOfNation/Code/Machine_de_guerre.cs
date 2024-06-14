using System;

public class Machine_de_guerre : Soldat  {
    /// <summary>
    /// Le nom du soldat
    /// </summary>
    private string nom;
    private string description;
    private int nb;

    public Machine_de_guerre(string nom, string description, int nb)
    {
        this.nom = nom;
        this.description = description;
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
            nb = value;
        }
    }

}
