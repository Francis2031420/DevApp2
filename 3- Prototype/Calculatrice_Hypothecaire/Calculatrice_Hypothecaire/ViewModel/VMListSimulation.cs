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
        string _nom, _prenom, _freqPaiement, _description, _perioAmortissement;
        double _montantFinancer, _interetAnnuel;
        
        List<historiquePaiements> _historiquePaiements;
        //TODO: S'assurer que lorsque selectedsimulation change que l'historique aussi
        public List<historiquePaiements> HistoriquePaiement
        {
            set{
                ChangedValue("HistoriquePaiements");
            }
            get { return _historiquePaiements; }
        }

        SimulationDetail _selectedSimulation;
        public SimulationDetail SelectedSimulation { get { return _selectedSimulation; } 
           set{
                _selectedSimulation = value;
                _historiquePaiements = _selectedSimulation.GetHistoriquePaiement();
                ChangedValue("SelectedSimulation");
           } 
        }

        //propriete historique paiement




        public VMListSimulation()
        {
            this.Save = new CommandRelay(EnregistrerSimulation_Execute, EnregistrerSimulation_CanExecute);
            this.Delete = new CommandRelay(DeleteSimulation_Execute  , DeleteSimulation_CanExecute);
            this.Create = new CommandRelay(CreateSimulation_Execute);
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
        { }

        public ICommand Delete { get; }
        public bool DeleteSimulation_CanExecute(object parameter)
        {
            if (_selectedSimulation == null)
                return false;
            return true;
        }
        public void DeleteSimulation_Execute(object parameter)
        {
        }
        public ICommand Create { get; }
        public void CreateSimulation_Execute(object parameter)
        { }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void ChangedValue(string property)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
