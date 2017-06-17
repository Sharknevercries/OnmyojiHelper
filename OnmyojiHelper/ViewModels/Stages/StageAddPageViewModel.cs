using OnmyojiHelper.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnmyojiHelper.Models;
using Template10.Mvvm;
using Template10.Common;
using System.ComponentModel;
using Windows.UI.Xaml.Navigation;
using Template10.Services.NavigationService;

namespace OnmyojiHelper.ViewModels.Stages
{
    public class StageAddPageViewModel : Mvvm.ViewModelBase
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
        public Models.Enums.StageCategory Category { get; set; }

        public DelegateCommand AddCommand { get; private set; }

        public StageAddPageViewModel(IDataService dataService)
        {
            this._dataService = dataService;

            AddCommand = new DelegateCommand(Add, AddCommand_CanExecute);

            Clear();
        }

        public void Clear()
        {
            Title = "";
            Category = Models.Enums.StageCategory.NormalDiscovery;
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            Clear();

            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        public bool AddCommand_CanExecute()
        {
            return _dataService.IsLegalStage(new Stage()
            {
                Title = this.Title,
                Category = this.Category,
            });
        }

        public void Add()
        {
            _dataService.AddStage(new Stage()
            {
                Title = this.Title,
                Category = this.Category,
            });

            var nav = WindowWrapper.Current().NavigationServices.FirstOrDefault();
            nav.GoBack();
        }
    }
}
