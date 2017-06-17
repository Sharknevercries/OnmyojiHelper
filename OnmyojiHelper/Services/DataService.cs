using Microsoft.EntityFrameworkCore;
using OnmyojiHelper.Models;
using OnmyojiHelper.Models.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Services.LoggingService;

namespace OnmyojiHelper.Services
{
    public class DataService : IDataService
    {
#region Stage
        public IEnumerable<StageGroup> GetAllStageGroups()
        {
            using (var db = new OnmyojiContext())
            {
                var stages = db.Stages
                                .Include(stage => stage.Battles)
                                    .ThenInclude(b => b.ShikigamiBattles)
                                        .ThenInclude(sb => sb.Shikigami)
                               .ToList();

                foreach (var stage in stages)
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

        public IEnumerable<Stage> GetAllStages()
        {
            using (var db = new OnmyojiContext())
            {
                return from s in db.Stages.ToList()
                       orderby s.Id ascending
                       select s;
            }
        }

        public void EditStage(Stage s)
        {
            using (var db = new OnmyojiContext())
            {
                var stage = db.Stages.FirstOrDefault(a => a.Id == s.Id);

                if(stage == null)
                {
                    LoggingService.WriteLine($"[Edit] Stage { s.Id } is null.", Severities.Warning);
                    return;
                }

                stage.Title = s.Title;
                stage.Category = s.Category;
                db.SaveChanges();

                LoggingService.WriteLine($"[Edit] Stage { s.Id }.", Severities.Info);
            }
        }

        public void AddStage(Stage s)
        {
            using (var db = new OnmyojiContext())
            {
                db.Stages.Add(s);
                db.SaveChanges();

                LoggingService.WriteLine($"[Add] Stage {s.Id}.", Severities.Info);
            }
        }

        public void DeleteStage(Stage s)
        {
            using (var db = new OnmyojiContext())
            {
                var stage = db.Stages.FirstOrDefault(a => a.Id == s.Id);

                db.Stages.Remove(stage);
                db.SaveChanges();

                LoggingService.WriteLine($"[Delete] Stage { s.Id }", Severities.Info);
            }
        }

        public bool IsLegalStage(Stage s)
        {
            return s != null && !string.IsNullOrEmpty(s.Title);
        }
#endregion

        public IEnumerable<Shikigami> GetAllShikigamis()
        {
            throw new NotImplementedException();
        }

        public void EditShikigami(Shikigami s)
        {
            throw new NotImplementedException();
        }

        public void AddShikigami(Shikigami s)
        {
            throw new NotImplementedException();
        }

        public void DeleteShikigami(Shikigami s)
        {
            throw new NotImplementedException();
        }

        public bool IsLegalShikigami(Shikigami s)
        {
            throw new NotImplementedException();
        }
    }
}
