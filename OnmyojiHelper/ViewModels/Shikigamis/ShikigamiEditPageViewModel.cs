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

namespace OnmyojiHelper.ViewModels.Shikigamis
{
    public class ShikigamiEditPageViewModel : Mvvm.ViewModelBase
    {
        private IDataService _dataService;

        public int Id { get; private set; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                Set(ref _name, value);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public Models.Enums.Rarity Rarity { get; set; }

        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand DeleteCommand { get; private set; }

        public ShikigamiEditPageViewModel(IDataService dataService)
        {
            this._dataService = dataService;

            SaveCommand = new DelegateCommand(Save, SaveCommand_CanExecute);
            DeleteCommand = new DelegateCommand(Delete);
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var shikigami = parameter as Shikigami;

            Id = shikigami.Id;
            Name = shikigami.Name;
            Rarity = shikigami.Rarity;

            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        public void Save()
        {
            _dataService.EditShikigami(new Shikigami(Id, Name, Rarity));

            var nav = WindowWrapper.Current().NavigationServices.FirstOrDefault();
            nav.GoBack();
        }

        public bool SaveCommand_CanExecute()
        {
            return _dataService.IsLegalShikigami(new Shikigami(Id, Name, Rarity));
        }

        public void Delete()
        {
            _dataService.DeleteShikigami(new Shikigami(Id));

            var nav = WindowWrapper.Current().NavigationServices.FirstOrDefault();
            nav.GoBack();
        }
    }
}
