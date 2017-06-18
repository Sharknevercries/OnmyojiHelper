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

namespace OnmyojiHelper.ViewModels.Clues
{
    public class ClueEditPageViewModel : Mvvm.ViewModelBase
    {
        private IDataService _dataService;

        public int Id { get; set; }

        private string _keyword;
        public string Keyword
        {
            get { return _keyword; }
            set
            {
                Set(ref _keyword, value);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand DeleteCommand { get; private set; }

        public ClueEditPageViewModel(IDataService dataService)
        {
            this._dataService = dataService;

            SaveCommand = new DelegateCommand(Save, SaveCommand_CanExecute);
            DeleteCommand = new DelegateCommand(Delete);
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var clue = parameter as Clue;

            Id = clue.Id;
            Keyword = clue.Keyword;

            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        public void Save()
        {
            _dataService.EditClue(new Clue()
            {
                Id = this.Id,
                Keyword = this.Keyword,
            });

            var nav = WindowWrapper.Current().NavigationServices.FirstOrDefault();
            nav.GoBack();
        }

        public bool SaveCommand_CanExecute()
        {
            return _dataService.IsLegalClue(new Clue()
            {
                Keyword = this.Keyword,
            });
        }

        public void Delete()
        {
            _dataService.DeleteClue(new Clue()
            {
                Keyword = this.Keyword,
            });

            var nav = WindowWrapper.Current().NavigationServices.FirstOrDefault();
            nav.GoBack();
        }
    }
}
