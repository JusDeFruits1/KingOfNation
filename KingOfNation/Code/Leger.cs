using System;

namespace KingOfNation.Code
{
    public class Leger : Soldat
    {
        /// <summary>
        /// Le nom du soldat
        /// </summary>
        private string nom;
        private string description;
        private int nb;

        public Leger(string nom, int nb)
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
                nb = value;
            }
        }
    }
}