using Calculatrice_Hypothecaire.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculatrice_Hypothecaire.ViewModel
{
    class VMListSimulation : INotifyPropertyChanged
    {
        int id = 0;
        public int PeriodeAmortis
        {
            get { return _selectedSimulation.PeriodeAmortis; }
            set
            {
                _selectedSimulation.PeriodeAmortis = value;
                ChangedValue("PeriodeAmortis");
                ChangedValue("SelectedSimulation");

            }
        }

        public decimal MontantFinancer
        {
            get { return _selectedSimulation.MontantFinancer; }
            set
            {
                _selectedSimulation.MontantFinancer = value;
                ChangedValue("MontantFinancer");
                ChangedValue("SelectedSimulation");

            }
        }

        public decimal InteretAnnuel
        {
            get { return _selectedSimulation.InteretAnnuel; }
            set
            {
                _selectedSimulation.InteretAnnuel = value;
                ChangedValue("InteretAnnuel");
                ChangedValue("SelectedSimulation");

            }
        }

        public string FrequencePaiement
        {
            get { return _selectedSimulation.FreqPaiement; }
            set
            {
                _selectedSimulation.FreqPaiement = value;
                ChangedValue("FrequencePaiement");
                ChangedValue("SelectedSimulation");

            }
        }

        public string NomCli
        {
            get { return _selectedSimulation.NomCli; }
            set
            {
                _selectedSimulation.NomCli = value;
                ChangedValue("NomCli");
                ChangedValue("SelectedSimulation");

            }
        }

        public string PrenomCli
        {
            get { return _selectedSimulation.PrenomCli; }
            set{
                _selectedSimulation.PrenomCli = value;
                ChangedValue("PrenomCli");
                ChangedValue("SelectedSimulation");
                ChangedValue("ListSimulations");

            }
        }

        public string Description
        {
            get { return _selectedSimulation.Description; }
            set {
                _selectedSimulation.Description = value;
                ChangedValue("Description");
                ChangedValue("SelectedSimulation");

            }
        }

        List<historiquePaiements> _historiquePaiements;
            //TODO: S'assurer que lorsque selectedsimulation change que l'historique aussi
              public List<historiquePaiements> HistoriquePaiements
              {
                  set{
                  }
                  get { return _historiquePaiements; }
              }
        List<SimulationDetail> _listSimulations;
        public List<SimulationDetail> ListSimulations
        {
            get
            {
                //return _listSimulations ;
                return new List<SimulationDetail>(_listSimulations)  ;
            }
            set
            {
                _listSimulations = value;
                ChangedValue("ListSimulations");
            }
        }
        
        

        SimulationDetail _selectedSimulation;
        public SimulationDetail SelectedSimulation { get { return _selectedSimulation; } 
           set{
                _selectedSimulation = value;
                ChangedValue("SelectedSimulation");
                ChangedValue("InteretAnnuel");
                ChangedValue("PrenomCli");
                ChangedValue("Description");
                ChangedValue("NomCli");
                ChangedValue("FrequencePaiement");
                ChangedValue("MontantFinancer");
                ChangedValue("PeriodeAmortis");
            } 
        }

        //propriete historique paiement



        public VMListSimulation()
        {
            _historiquePaiements = new List<historiquePaiements>();
            _selectedSimulation = SimulationDetail.CreateEmpty();
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).ToString() + "\\data.xml";
            if (File.Exists(path))
            {
                _listSimulations = Serializer.DeserializeFromXML<List<SimulationDetail>>(path);
            }
            else
            {
                _listSimulations = new List<SimulationDetail>();
            }
            SimulationDetail simulationHardcoder1 = new SimulationDetail(0, 230000, (decimal)6.5, 25, "Mensuelle", "Francis", "Blais", "BLALBLABLA");
            SimulationDetail simulationHardcoder2 = new SimulationDetail(1, 230000, (decimal)6.5, 25, "Mensuelle", "Emmi", "Gagnon", "FRUE");
            _listSimulations.Add(simulationHardcoder1);
            _listSimulations.Add(simulationHardcoder2);

            this.Save = new CommandRelay(EnregistrerSimulation_Execute, EnregistrerSimulation_CanExecute);
            this.Delete = new CommandRelay(DeleteSimulation_Execute  , DeleteSimulation_CanExecute);
            this.Create = new CommandRelay(CreateSimulation_Execute);
            this.calculer = new CommandRelay(Calculer_Execute, Calculer_CanExecute);
              
        }


        public ICommand calculer { get; }
        public bool Calculer_CanExecute(object parameter)
        {
            if (_selectedSimulation == null)
                return false;
            return true;
        }
        // TODO: complété fonction
        public void Calculer_Execute(object parameter)
        {
             _historiquePaiements = _selectedSimulation.GetHistoriquePaiement();
            ChangedValue("HistoriquePaiements");
            ChangedValue("TotalCapital");
            ChangedValue("TauxInteretAnnuel");
            ChangedValue("CoutTotal");

        }
        //a rajouter pt un autre ICOMMAND
        public ICommand Save { get; }
        //TODO: s'assurer que rien est vide lors de l'enregistrement
        public bool EnregistrerSimulation_CanExecute(object parameter)
        {
            if(_selectedSimulation== null)
                return false;
            return true;
        }
        // TODO: complété fonction
        public void EnregistrerSimulation_Execute(object parameter)
        {
            _listSimulations.Add(_selectedSimulation);
            ChangedValue("ListSimulations");
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).ToString() + "\\data.xml";
            if (File.Exists(path))
            {
                _listSimulations = Serializer.DeserializeFromXML<List<SimulationDetail>>(path);
            }
            else
            {
                _listSimulations = new List<SimulationDetail>();
            }

        }

        public ICommand Delete { get; }
        public bool DeleteSimulation_CanExecute(object parameter)
        {
            if (_selectedSimulation == null)
                return false;
            return true;
        }
        public void DeleteSimulation_Execute(object parameter)
        {
            _listSimulations.Remove(_selectedSimulation);
            _selectedSimulation = _listSimulations[_listSimulations.Count-1];
            ChangedValue("SelectedSimulation");
            ChangedValue("ListSimulations");
            ChangedValue("InteretAnnuel");
            ChangedValue("PrenomCli");
            ChangedValue("Description");
            ChangedValue("NomCli");
            ChangedValue("FrequencePaiement");
            ChangedValue("MontantFinancer");
            ChangedValue("PeriodeAmortis");
        }
        public ICommand Create { get; }
        //rajouter un can execute dans produit final
        public void CreateSimulation_Execute(object parameter)
        {
            _selectedSimulation = SimulationDetail.CreateEmpty();
            _listSimulations.Add(_selectedSimulation);
            ChangedValue("ListSimulations");
            ChangedValue("InteretAnnuel");
            ChangedValue("PrenomCli");
            ChangedValue("Description");
            ChangedValue("NomCli");
            ChangedValue("FrequencePaiement");
            ChangedValue("MontantFinancer");
            ChangedValue("PeriodeAmortis");
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void ChangedValue(string property)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        //propriete de l'historique

        public decimal TotalCapital
        {
            get
            {
                if (_historiquePaiements?.Count > 0)
                {
                    return _historiquePaiements[0].TotalCapital;
                }
                else
                    return 0;
            }
        }
        public decimal TauxInteretAnnuel
        {
            get
            {
                if (_historiquePaiements.Count > 0)
                {
                    return _historiquePaiements[0].TotalInteretAnnuel;

                }
                else
                    return 0;
            }
        }

        public decimal CoutTotal
        {
            get
            {
                if (_historiquePaiements.Count > 0)
                {
                    return _historiquePaiements[0].CoutTotal;
                }
                else
                    return 0;
            }
        }







    }
}
