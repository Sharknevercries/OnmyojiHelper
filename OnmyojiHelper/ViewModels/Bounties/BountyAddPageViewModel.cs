using OnmyojiHelper.Models;
using OnmyojiHelper.Models.Relations;
using OnmyojiHelper.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Common;
using Template10.Mvvm;
using Template10.Services.LoggingService;
using Windows.UI.Xaml.Controls;

namespace OnmyojiHelper.ViewModels.Bounties
{
    public class BountyAddPageViewModel : Mvvm.ViewModelBase
    {
        private IDataService _dataService;

        public ObservableCollection<Shikigami> Shikigamis { get; set; }

        private Shikigami _selectedShikigami;
        public Shikigami SelectedShikigami
        {
            get { return _selectedShikigami; }
            set
            {
                Set(ref _selectedShikigami, value);
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        private List<Clue> _selectedClues;

        private ObservableCollection<Clue> _clues;
        public ObservableCollection<Clue> Clues
        {
            get { return _clues; }
            set { Set(ref _clues, value); }
        }

        public DelegateCommand AddCommand { get; private set; }
        public DelegateCommand<object> ClueSelectionChangedCommand { get; private set; } 

        public BountyAddPageViewModel(IDataService dataService)
        {
            this._dataService = dataService;

            Shikigamis = new ObservableCollection<Shikigami>(_dataService.GetAllShikigamis());
            Clues = new ObservableCollection<Clue>(_dataService.GetAllClues());
            _selectedClues = new List<Clue>();

            AddCommand = new DelegateCommand(Add, AddCommand_CanExecute);
            ClueSelectionChangedCommand = new DelegateCommand<object>(ClueSelectionChanged);
        }


        public void Add()
        {
            List<BountyClue> bc = new List<BountyClue>();
            foreach(var c in _selectedClues)
            {
                bc.Add(new BountyClue() { ClueId = c.Id });
            }

            _dataService.AddBounty(new Bounty()
            {
                ShikigamiId = this.SelectedShikigami.Id,
                BountyClues = bc,
            });

            var nav = WindowWrapper.Current().NavigationServices.FirstOrDefault();
            nav.GoBack();
        }

        public bool AddCommand_CanExecute()
        {
            return _dataService.IsLegalBounty(new Bounty()
            {
                Shikigami = this.SelectedShikigami,
            });
        }

        public void ClueSelectionChanged(object e)
        {
            _selectedClues.Clear();
            foreach(Clue c in (e as IList<object>))
            {
                _selectedClues.Add(c);
            }
        }
    }
}
