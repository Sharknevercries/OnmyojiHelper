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

namespace OnmyojiHelper.ViewModels.Stages
{
    public class StageEditPageViewModel : Mvvm.ViewModelBase
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

        public Models.Enums.StageCategory Category { get; set; }

        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand DeleteCommand { get; private set; }

        public StageEditPageViewModel(IDataService dataService)
        {
            this._dataService = dataService;

            SaveCommand = new DelegateCommand(Save, SaveCommand_CanExecute);
            DeleteCommand = new DelegateCommand(Delete);
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var stage = parameter as Stage;

            Id = stage.Id;
            Title = stage.Title;
            Category = stage.Category;

            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        public void Save()
        {
            _dataService.EditStage(new Stage()
            {
                Id = this.Id,
                Title = this.Title,
                Category = this.Category,
            });

            var nav = WindowWrapper.Current().NavigationServices.FirstOrDefault();
            nav.GoBack();
        } 

        public bool SaveCommand_CanExecute()
        {
            return _dataService.IsLegalStage(new Stage()
            {
                Title = this.Title,
                Category = this.Category,
            });
        }

        public void Delete()
        {
            _dataService.DeleteStage(new Stage()
            {
                Id = this.Id,
            });

            var nav = WindowWrapper.Current().NavigationServices.FirstOrDefault();
            nav.GoBack();
        }
    }
}
