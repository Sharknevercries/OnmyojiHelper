using OnmyojiHelper.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnmyojiHelper.ViewModels.Bounties
{
    public class BountyAddPageViewModel : Mvvm.ViewModelBase
    {
        private IDataService _dataService;

        public BountyAddPageViewModel(IDataService dataService)
        {
            this._dataService = dataService;
        }
    }
}
