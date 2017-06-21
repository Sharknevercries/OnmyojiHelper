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
using Windows.UI.Xaml.Navigation;

namespace OnmyojiHelper.ViewModels.Bounties
{
    public class BountyAddPageViewModel : Mvvm.ViewModelBase
    {
        private IDataService _dataService;

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

        public List<Clue> SelectedClues { get; set; }

        public DelegateCommand AddCommand { get; private set; }

        public BountyAddPageViewModel(IDataService dataService)
        {
            this._dataService = dataService;
            
            AddCommand = new DelegateCommand(Add, AddCommand_CanExecute);
        }

        public void Add()
        {
            List<BountyClue> bc = new List<BountyClue>();
            foreach(var c in SelectedClues)
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
    }
}
