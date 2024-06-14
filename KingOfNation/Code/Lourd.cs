using System;

public class Lourd : Soldat  {
    /// <summary>
    /// Le nom du soldat
    /// </summary>
    private string nom;
    private string description;
    private int nb;

    public Lourd(string nom, string description, int nb)
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
