using OnmyojiHelper.Models;
using OnmyojiHelper.Models.Relations;
using OnmyojiHelper.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Common;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace OnmyojiHelper.ViewModels.Battles
{
    public class BattleEditPageViewModel : Mvvm.ViewModelBase
    {
        private IDataService _dataService;

        public int Id { get; set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                Set(ref _title, value);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        private Stage _selectedStage;
        public Stage SelectedStage
        {
            get { return _selectedStage; }
            set
            {
                Set(ref _selectedStage, value);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public List<ShikigamiBattle> SelectedShikigamiBattles { get; set; }

        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand DeleteCommand { get; private set; }

        public BattleEditPageViewModel(IDataService dataService)
        {
            this._dataService = dataService;

            SaveCommand = new DelegateCommand(Save, SaveCommand_CanExecute);
            DeleteCommand = new DelegateCommand(Delete);
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var battle = parameter as Battle;

            Id = battle.Id;
            Title = battle.Title;
            SelectedStage = battle.Stage;
            SelectedShikigamiBattles = battle.ShikigamiBattles;

            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        public void Save()
        {
            _dataService.EditBattle(new Battle()
            {
                Id = this.Id,
                StageId = this.SelectedStage.Id,
                ShikigamiBattles = this.SelectedShikigamiBattles,
                Title = this.Title,
            });

            var nav = WindowWrapper.Current().NavigationServices.FirstOrDefault();
            nav.GoBack();
        }

        public bool SaveCommand_CanExecute()
        {
            return _dataService.IsLegalBattle(new Battle()
            {
                Title = this.Title,
                Stage = this.SelectedStage,
            });
        }

        public void Delete()
        {
            _dataService.DeleteBattle(new Battle()
            {
                Id = this.Id,
            });

            var nav = WindowWrapper.Current().NavigationServices.FirstOrDefault();
            nav.GoBack();
        }
    }
}
