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

namespace OnmyojiHelper.ViewModels.Battles
{
    public class BattleAddPageViewModel : Mvvm.ViewModelBase
    {
        private IDataService _dataService;

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                Set(ref _title, value);
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        private Stage _selectedStage;
        public Stage SelectedStage
        {
            get { return _selectedStage; }
            set
            {
                Set(ref _selectedStage, value);
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        public List<ShikigamiBattle> SelectedShikigamiBattles { get; set; }
  
        public DelegateCommand AddCommand { get; private set; }

        public BattleAddPageViewModel(IDataService dataService)
        {
            this._dataService = dataService;

            AddCommand = new DelegateCommand(Add, AddCommand_CanExecute);

            SelectedShikigamiBattles = new List<ShikigamiBattle>();
        }

        public void Add()
        {
            _dataService.AddBattle(new Battle()
            {
                Title = this.Title,
                StageId = this.SelectedStage.Id,
                ShikigamiBattles = SelectedShikigamiBattles,
            });

            var nav = WindowWrapper.Current().NavigationServices.FirstOrDefault();
            nav.GoBack();
        }

        public bool AddCommand_CanExecute()
        {
            return _dataService.IsLegalBattle(new Battle()
            {
                Title = this.Title,
                Stage = this.SelectedStage,
            });
        }
    }
}
