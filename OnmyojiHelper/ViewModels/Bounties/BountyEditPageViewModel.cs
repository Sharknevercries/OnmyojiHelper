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
using Windows.UI.Xaml.Navigation;

namespace OnmyojiHelper.ViewModels.Bounties
{
    public class BountyEditPageViewModel : Mvvm.ViewModelBase
    {
        private IDataService _dataService;
 
        public int Id { get; set; }
        public List<Clue> SelectedClues { get; set; }

        private Shikigami _selectedShikigami;
        public Shikigami SelectedShikigami
        {
            get { return _selectedShikigami; }
            set
            {
                Set(ref _selectedShikigami, value);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand DeleteCommand { get; private set; }

        public BountyEditPageViewModel(IDataService dataService)
        {
            this._dataService = dataService;
;
            SaveCommand = new DelegateCommand(Save, SaveCommand_CanExecute);
            DeleteCommand = new DelegateCommand(Delete);
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var bounty = parameter as Bounty;
            this.Id = bounty.Id;
            SelectedShikigami = new Shikigami(bounty.ShikigamiId);
            SelectedClues = (from c in bounty.BountyClues
                             select new Clue() { Id = c.ClueId })
                            .ToList();

            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        public void Save()
        {
            List<BountyClue> bc = new List<BountyClue>();
            foreach (var c in SelectedClues)
            {
                bc.Add(new BountyClue() { BountyId = this.Id, ClueId = c.Id });
            }

            _dataService.EditBounty(new Bounty()
            {
                Id = this.Id,
                ShikigamiId = this.SelectedShikigami.Id,
                BountyClues = bc,
            });

            var nav = WindowWrapper.Current().NavigationServices.FirstOrDefault();
            nav.GoBack();
        }

        public bool SaveCommand_CanExecute()
        {
            return _dataService.IsLegalBounty(new Bounty()
            {
                Shikigami = this.SelectedShikigami,
            });
        }

        public void Delete()
        {
            _dataService.DeleteBounty(new Bounty()
            {
                Id = this.Id,
            });

            var nav = WindowWrapper.Current().NavigationServices.FirstOrDefault();
            nav.GoBack();
        }
    }
}
