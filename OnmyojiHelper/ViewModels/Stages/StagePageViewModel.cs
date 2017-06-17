using Template10.Mvvm;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using OnmyojiHelper.Models.Groups;
using OnmyojiHelper.Services;
using System.Collections.ObjectModel;
using Windows.System.Threading;
using Template10.Services.LoggingService;
using OnmyojiHelper.Models;

namespace OnmyojiHelper.ViewModels.Stages
{
    public class StagePageViewModel : Mvvm.ViewModelBase
    {
        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { Set(ref _isLoading, value); }
        }

        private IEnumerable<StageGroup> _stageGroups;
        public IEnumerable<StageGroup> StageGroups
        {
            get { return _stageGroups; }
            set { Set(ref _stageGroups, value); }
        }

        private IDataService _dataService;

        public StagePageViewModel(IDataService dataService)
        {
            _dataService = dataService;

            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
            }
            else
            {
            }
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            StageGroups = null;

            try
            {
                IsLoading = true;
                StageGroups = _dataService.GetAllStageGroups();
            }
            finally
            {
                IsLoading = false;
            }

            await Task.CompletedTask;
        }
    }
}
