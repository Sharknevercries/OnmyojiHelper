using OnmyojiHelper.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Common;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace OnmyojiHelper.ViewModels.Shikigamis
{
    public class ShikigamiAddPageViewModel : Mvvm.ViewModelBase
    {
        private IDataService _dataService;

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                Set(ref _name, value);
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        public Models.Enums.Rarity Rarity { get; set; }

        public DelegateCommand AddCommand { get; private set; }

        public ShikigamiAddPageViewModel(IDataService dataService)
        {
            this._dataService = dataService;

            AddCommand = new DelegateCommand(Add, AddCommand_CanExecute);

            Clear();
        }

        public void Clear()
        {
            Name = "";
            Rarity = Models.Enums.Rarity.SSR;
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            Clear();

            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        public void Add()
        {
            _dataService.AddShikigami(new Models.Shikigami(Name, Rarity));

            var nav = WindowWrapper.Current().NavigationServices.FirstOrDefault();
            nav.GoBack();
        }

        public bool AddCommand_CanExecute()
        {
            return _dataService.IsLegalShikigami(new Models.Shikigami(Name, Rarity));
        }
    }
}
