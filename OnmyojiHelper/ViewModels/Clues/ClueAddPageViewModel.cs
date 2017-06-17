using OnmyojiHelper.Models;
using OnmyojiHelper.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Common;
using Template10.Mvvm;

namespace OnmyojiHelper.ViewModels.Clues
{
    public class ClueAddPageViewModel : Mvvm.ViewModelBase
    {
        private IDataService _dataService;

        private string _keyword;
        public string Keyword
        {
            get { return _keyword; }
            set
            {
                Set(ref _keyword, value);
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateCommand AddCommand { get; private set; }

        public ClueAddPageViewModel(IDataService dataService)
        {
            this._dataService = dataService;

            AddCommand = new DelegateCommand(Add, AddCommand_CanExecute);
        }

        public void Add()
        {
            _dataService.AddClue(new Clue()
            {
                Keyword = this.Keyword,
            });

            var nav = WindowWrapper.Current().NavigationServices.FirstOrDefault();
            nav.GoBack();
        }

        public bool AddCommand_CanExecute()
        {
            return _dataService.IsLegalClue(new Clue()
            {
                Keyword = this.Keyword,
            });
        }
    }
}
