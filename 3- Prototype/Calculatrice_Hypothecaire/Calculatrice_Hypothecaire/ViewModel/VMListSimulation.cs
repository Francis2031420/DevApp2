using Calculatrice_Hypothecaire.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
            }
        }

        public decimal MontantFinancer
        {
            get { return _selectedSimulation.MontantFinancer; }
            set
            {
                _selectedSimulation.MontantFinancer = value;
                ChangedValue("MontantFinancer");
            }
        }

        public decimal InteretAnnuel
        {
            get { return _selectedSimulation.InteretAnnuel; }
            set
            {
                _selectedSimulation.InteretAnnuel = value;
                ChangedValue("InteretAnnuel");
            }
        }

        public string FrequencePaiement
        {
            get { return _selectedSimulation.FreqPaiement; }
            set
            {
                _selectedSimulation.FreqPaiement = value;
                ChangedValue("FrequencePaiement");
            }
        }

        public string NomCli
        {
            get { return _selectedSimulation.NomCli; }
            set
            {
                _selectedSimulation.NomCli = value;
                ChangedValue("NomCli");
            }
        }

        public string PrenomCli
        {
            get { return _selectedSimulation.PrenomCli; }
            set{
                _selectedSimulation.PrenomCli = value;
                ChangedValue("PrenomCli");
            }
        }

        public string Description
        {
            get { return _selectedSimulation.Description; }
            set {
                _selectedSimulation.Description = value;
                ChangedValue("Description");
            }
        }

        List<historiquePaiements> _historiquePaiements;
            //TODO: S'assurer que lorsque selectedsimulation change que l'historique aussi
              public List<historiquePaiements> HistoriquePaiement
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
                return _listSimulations;
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
           } 
        }

        //propriete historique paiement



        public VMListSimulation()
        {
            this.Save = new CommandRelay(EnregistrerSimulation_Execute, EnregistrerSimulation_CanExecute);
            this.Delete = new CommandRelay(DeleteSimulation_Execute  , DeleteSimulation_CanExecute);
            this.Create = new CommandRelay(CreateSimulation_Execute);
            this.calculer = new CommandRelay(Calculer_Execute, Calculer_CanExecute);
            _listSimulations = new List<SimulationDetail>();
            if(_listSimulations.Count == 0)
            {
                _listSimulations.Add(SimulationDetail.CreateEmpty());
            }
            _selectedSimulation = new SimulationDetail();
            _historiquePaiements = new List<historiquePaiements>();
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

        }
        //a rajouter pt un autre ICOMMAND
        public ICommand Save { get; }
        //TODO: s'assurer que rien est vide lors de l'enregistrement
        public bool EnregistrerSimulation_CanExecute(object parameter)
        {
            if(_selectedSimulation == null)
                return false;
            return true;
        }
        // TODO: complété fonction
        public void EnregistrerSimulation_Execute(object parameter)
        {
            _listSimulations.Add(_selectedSimulation);
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
        }
        public ICommand Create { get; }
        //rajouter un can execute dans produit final
        public void CreateSimulation_Execute(object parameter)
        {
            _selectedSimulation = SimulationDetail.CreateEmpty();
            ChangedValue("ListSimulations");
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
                if (_historiquePaiements.Count > 0)
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
