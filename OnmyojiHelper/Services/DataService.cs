using Microsoft.EntityFrameworkCore;
using OnmyojiHelper.Models;
using OnmyojiHelper.Models.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnmyojiHelper.Services
{
    public class DataService : IDataService
    {
        public IEnumerable<StageGroup> GetAllStageGroups()
        {
            using (var db = new OnmyojiContext())
            {
                var stages = db.Stages
                                .Include(stage => stage.Battles)
                                    .ThenInclude(b => b.ShikigamiBattles)
                                        .ThenInclude(sb => sb.Shikigami)
                               .ToList();

                foreach(var stage in stages)
                {
                    stage.Battles = (from battle in stage.Battles
                                     orderby battle.Id ascending
                                     select battle).ToList();
                }

                return (from stage in stages
                        group stage by stage.Category into g
                        select new StageGroup(g.Key, g.ToList().OrderBy(s => s.Id)));
            }
        }
    }
}
