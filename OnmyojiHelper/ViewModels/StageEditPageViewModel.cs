using OnmyojiHelper.Models;
using OnmyojiHelper.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Common;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace OnmyojiHelper.ViewModels
{
    public class StageEditPageViewModel : Mvvm.ViewModelBase
    {
        private IDataService _dataService;

        private Stage _stage;
        public Stage Stage
        {
            get { return _stage; }
            set { Set(ref _stage, value); SaveCommand.RaiseCanExecuteChanged(); }
        }

        public DelegateCommand SaveCommand { get; private set; }

        public StageEditPageViewModel(IDataService dataService)
        {
            this._dataService = dataService;

            SaveCommand = new DelegateCommand(Save, SaveCommand_CanExecute);
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            Stage = parameter as Stage;

            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        public void Save()
        {
            if (_stage != null)
            {
                _dataService.EditStage(_stage);

                var nav = WindowWrapper.Current().NavigationServices.FirstOrDefault();
                nav.GoBack();
            }
        }

        public bool SaveCommand_CanExecute()
        {
            return _dataService.IsLegalStage(Stage);
        } 
    }
}
