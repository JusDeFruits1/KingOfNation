using System;
using System.ComponentModel;

namespace KingOfNation.Code
{
    public class Leger : Soldat, INotifyPropertyChanged
    {
        /// <summary>
        /// Le nom du soldat
        /// </summary>
        private string nom;
        private string description;
        private int nb;

        public event PropertyChangedEventHandler PropertyChanged;

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
}