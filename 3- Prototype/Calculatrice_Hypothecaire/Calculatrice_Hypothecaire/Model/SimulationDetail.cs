using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Calculatrice_Hypothecaire.Model
{
    [Serializable]
    class SimulationDetail
    {
        //Données simulations
        [XmlElement("id")]
        public int Id { get; set; }
        public decimal MontantFinancer { get; set; }
        [XmlElement("InteretAnnuel")]
        public decimal InteretAnnuel { get; set; }
        [XmlElement("PeriodeAmortis")]
        public int PeriodeAmortis { get; set; }
        [XmlElement("FreqPaiement")]
        public string FreqPaiement { get; set; }
        [XmlElement("NomCli")]
        public string NomCli { get; set; }
        [XmlElement("PrenomCli")]
        public string PrenomCli { get; set; }
        [XmlElement("Description")]
        public string Description { get; set; }
        
        public static SimulationDetail CreateEmpty()
        {
            return new SimulationDetail(0, 0, 0, 0, "", "", "", "");
        }
        public SimulationDetail(int id, decimal montantFinancer, decimal interetAnnuel, int periodeAmortis, 
            string freqPaiement, string nomCli, string prenomCli, string description ) 
        {
            Id = id;
            MontantFinancer = montantFinancer;
            InteretAnnuel = interetAnnuel;
            PeriodeAmortis = periodeAmortis;
            FreqPaiement = freqPaiement;
            NomCli = nomCli;
            PrenomCli = prenomCli;
            Description = description;
        }
        //pour deserilisation
        public SimulationDetail()
        {
            
        }

        //TODO: Calcul historiqueSimulation 
        public List<historiquePaiements> GetHistoriquePaiement()
        {
            double nbPaiementTotal = 12 * PeriodeAmortis;
           List<historiquePaiements> historiquePaiement = new List<historiquePaiements>();
            for(int i=0;i<nbPaiementTotal;i++)
            {
                historiquePaiement.Add(new historiquePaiements());
            }
            decimal totalCapital = 0, totalInteretAnnuel = 0, coutTotalv = 0, paiement = 0, capital = 0, interet = 0, balance = 0;
            int idMA =1;
            decimal interetPeriode = (InteretAnnuel / 100)/12;
            
            /*
             Calcul : 
            cout total = cout de la maison
            Paiement = mensualite
            Capital = déduire somme finale
            Interet = montant intérêt qui se rajoute au montant total
            Balance = ce qui reste 
             */
            historiquePaiement[0].TotalCapital = MontantFinancer; // donne valeur du total de capital 
            totalCapital = MontantFinancer;
            paiement = (decimal)((double)(totalCapital * interetPeriode) / (1 - Math.Pow(1 + (double)interetPeriode, -nbPaiementTotal)));// donne la valeurs des paiements
            balance = MontantFinancer;
            for (int i=0; i < historiquePaiement.Count; i++, idMA++)
            {
                historiquePaiement[i].Paiement = paiement;
                historiquePaiement[i].Interet = interet = totalCapital * interetPeriode; //calcul intéret
                historiquePaiement[i].Capital = paiement - interet;// calcul le capital
                balance -=historiquePaiement[i].Capital;
                historiquePaiement[i].Balance = balance; // calcul balance
                historiquePaiement[i].idMA = idMA;// donne valeur de l'id du paiements
            }
            for(int i =0; i<historiquePaiement.Count; i++)
            {
                historiquePaiement[0].TotalInteretAnnuel += historiquePaiement[i].Interet;// donne cout annuel en interet
            }
            historiquePaiement[0].CoutTotal = MontantFinancer + historiquePaiement[0].TotalInteretAnnuel;// donne le cout total
            return historiquePaiement;
        }

    }
}
