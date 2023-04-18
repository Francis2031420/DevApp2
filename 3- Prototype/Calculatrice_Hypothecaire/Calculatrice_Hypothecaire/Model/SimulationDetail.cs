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
        public double MontantFinancer { get; set; }
        [XmlElement("InteretAnnuel")]
        public double InteretAnnuel { get; set; }
        [XmlElement("PeriodeAmortis")]
        public string PeriodeAmortis { get; set; }
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
        public SimulationDetail(int id,double montantFinancer, double interetAnnuel, string periodeAmortis, 
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

        public double TotalCapital// poser question au prof pour les trois
        {
            get
            {
                double totalCapital = 0;
                return totalCapital;
            }
        }

        public double TotalInteret
        {
            get
            {
                double totalInteret = 0;
                return totalInteret;
            }
        }

        public double CoutTotal
        {
            get 
            {
                double coutTotal = 0;
                return coutTotal; 
            }
        }

        
      /* A changer et a réfléchir comment approcher l'historique de paiements
       * public List<double>  GetHistoriqueVersement()
        {
            List <double>historiqueVersement = new List<double>();


        }*/

    }
}
