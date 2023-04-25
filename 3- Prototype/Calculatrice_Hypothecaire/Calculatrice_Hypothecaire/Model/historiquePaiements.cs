using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculatrice_Hypothecaire.Model
{
    public class historiquePaiements
    {
        // a vérifier si changement nécessaire
        public decimal TotalCapital { get; set; }
        public decimal TotalInteretAnnuel { get; set; }
        public decimal CoutTotal { get; set; }
        /// <summary>
        /// Id mois années
        /// </summary>
        public int idMA { get; set; }
        public decimal Paiement { get; set; }
        public decimal Capital { get; set; }
        public decimal Interet { get; set; }
        public decimal Balance { get; set; }
        public historiquePaiements()
        {
        }
    }
}
