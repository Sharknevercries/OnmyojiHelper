using OnmyojiHelper.Models;
using OnmyojiHelper.Models.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnmyojiHelper.Services
{
    public interface IDataService
    {
        IEnumerable<StageGroup> GetAllStageGroups();
    }
}
