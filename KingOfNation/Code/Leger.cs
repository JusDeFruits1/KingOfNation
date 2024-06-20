using System;
using System.ComponentModel;

namespace KingOfNation.Code
{
    public class Leger : Soldat, INotifyPropertyChanged
    {

        #region Attributes

        private string nom;
        private string description;
        private int nb;

        #endregion

        #region Properties

        public event PropertyChangedEventHandler PropertyChanged;

       
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

        public Leger(string nom, int nb)
        {
            this.nom = nom;
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
}