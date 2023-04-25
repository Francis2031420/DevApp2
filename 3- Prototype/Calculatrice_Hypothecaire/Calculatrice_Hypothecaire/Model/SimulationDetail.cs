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

        [XmlElement("MontantFinancer")]
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
            return new SimulationDetail(0, 0, 0, "", "", "", "", "");
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
           List<historiquePaiements> historiquePaiement = new List<historiquePaiements>(PeriodeAmortis * 12);

            decimal totalCapital = 0, totalInteretAnnuel = 0, coutTotalv = 0, paiement = 0, capital = 0, interet = 0, balance = 0;
            int idMA =1;

            /*
             Calcul : 
            Paiement
            Capital
            Interet
            Balance
             */
            for (int i=0; i < historiquePaiement.Count; i++, idMA++)
            {
                
                
                
                historiquePaiement[i].TotalCapital = totalCapital;
                historiquePaiement[i].TotalInteretAnnuel = totalInteretAnnuel;
                historiquePaiement[i].CoutTotal = coutTotalv;
                historiquePaiement[i].Paiement = paiement;
                historiquePaiement[i].Capital = capital;
                historiquePaiement[i].Interet = interet;
                historiquePaiement[i].Balance = balance;
                historiquePaiement[i].idMA = idMA;
            }

            return historiquePaiement;
        }

    }
}
