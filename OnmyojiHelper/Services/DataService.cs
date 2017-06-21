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

                if (stage == null)
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

        #region Shikigami

        public IEnumerable<Shikigami> GetAllShikigamis()
        {
            using (var db = new OnmyojiContext())
            {
                return from s in db.Shikigamis.ToList()
                       orderby s.Id ascending
                       select s;
            }
        }

        public void EditShikigami(Shikigami s)
        {
            using (var db = new OnmyojiContext())
            {
                var shikigami = db.Shikigamis.FirstOrDefault(a => a.Id == s.Id);

                if (shikigami == null)
                {
                    LoggingService.WriteLine($"[Edit] Shikigami { s.Id } is null.", Severities.Warning);
                    return;
                }

                shikigami.Name = s.Name;
                shikigami.Rarity = s.Rarity;
                db.SaveChanges();

                LoggingService.WriteLine($"[Edit] Shikigami { s.Id }.", Severities.Info);
            }
        }

        public void AddShikigami(Shikigami s)
        {
            using (var db = new OnmyojiContext())
            {
                db.Shikigamis.Add(s);
                db.SaveChanges();

                LoggingService.WriteLine($"[Add] Shikigami {s.Id}.", Severities.Info);
            }
        }

        public void DeleteShikigami(Shikigami s)
        {
            using (var db = new OnmyojiContext())
            {
                var shikigami = db.Shikigamis.FirstOrDefault(a => a.Id == s.Id);
                db.Shikigamis.Remove(shikigami);
                db.SaveChanges();

                LoggingService.WriteLine($"[Delete] Shikigami { s.Id }", Severities.Info);
            }
        }

        public bool IsLegalShikigami(Shikigami s)
        {
            return s != null && !string.IsNullOrEmpty(s.Name);
        }

        #endregion

        #region Clue

        public IEnumerable<Clue> GetAllClues()
        {
            using (var db = new OnmyojiContext())
            {

                return from c in db.Clues.ToList()
                       orderby c.Id ascending
                       select c;
            }
        }

        public void EditClue(Clue c)
        {
            using (var db = new OnmyojiContext())
            {
                var clue = db.Clues.FirstOrDefault(a => a.Id == c.Id);

                if (clue == null)
                {
                    LoggingService.WriteLine($"[Edit] Clue { c.Id } is null.", Severities.Warning);
                    return;
                }

                clue.Keyword = c.Keyword;
                db.SaveChanges();
                LoggingService.WriteLine($"[Edit] Clue { c.Id }.", Severities.Info);
            } 
        }

        public void AddClue(Clue c)
        {
            using (var db = new OnmyojiContext())
            {
                db.Clues.Add(c);
                db.SaveChanges();

                LoggingService.WriteLine($"[Add] Clue {c.Id}.", Severities.Info);
            }
        }

        public void DeleteClue(Clue c)
        {
            using (var db = new OnmyojiContext())
            {
                var clue = db.Clues.FirstOrDefault(a => a.Id == c.Id);
                db.Clues.Remove(clue);
                db.SaveChanges();

                LoggingService.WriteLine($"[Delete] Clue { c.Id }", Severities.Info);
            }
        }

        public bool IsLegalClue(Clue c)
        {
            return !string.IsNullOrEmpty(c.Keyword);
        }

        #endregion

        #region Bounty

        public IEnumerable<Bounty> GetAllBounties()
        {
            using (var db = new OnmyojiContext())
            {
                var bounties = db.Bounties
                    .Include(b => b.Shikigami)
                    .Include(b => b.BountyClues)
                    .ToList();

                return from b in bounties
                       orderby b.Id ascending
                       select b;
            }
        }

        public void EditBounty(Bounty b)
        {
            using (var db = new OnmyojiContext())
            {
                var bounty = db.Bounties
                    .Include(a => a.BountyClues)
                    .FirstOrDefault(a => a.Id == b.Id);

                if (bounty == null)
                {
                    LoggingService.WriteLine($"[Edit] Bounty { b.Id } is null.", Severities.Warning);
                    return;
                }

                bounty.BountyClues.Clear();
                db.SaveChanges();
                foreach (var item in b.BountyClues)
                {
                    bounty.BountyClues.Add(item);
                }
                bounty.ShikigamiId = b.ShikigamiId;
                db.SaveChanges();
                LoggingService.WriteLine($"[Edit] Bounty { b.Id }.", Severities.Info);
            }
        }

        public void AddBounty(Bounty b)
        {
            using (var db = new OnmyojiContext())
            {
                db.Bounties.Add(b);
                db.SaveChanges();

                LoggingService.WriteLine($"[Add] Bounty {b.Id}.", Severities.Info);
            }
        }

        public void DeleteBounty(Bounty b)
        {
            using (var db = new OnmyojiContext())
            {
                var bounty = db.Bounties.FirstOrDefault(a => a.Id == b.Id);
                db.Bounties.Remove(bounty);
                db.SaveChanges();

                LoggingService.WriteLine($"[Delete] Bounty { b.Id }", Severities.Info);
            }
        }

        public bool IsLegalBounty(Bounty b)
        {
            return b.Shikigami != null;
        }

        #endregion

        #region Battle

        public IEnumerable<Battle> GetAllBattles()
        {
            using (var db = new OnmyojiContext())
            {
                return db.Battles
                    .Include(b => b.ShikigamiBattles)
                    .Include(b => b.Stage)
                    .OrderBy(b => b.Id)
                    .ToList();
            }
        }

        public void EditBattle(Battle b)
        {
            using (var db = new OnmyojiContext())
            {
                var battle = db.Battles.First(a => a.Id == b.Id);

                if (battle == null)
                {
                    LoggingService.WriteLine($"[Edit] Battle { b.Id } is null.", Severities.Warning);
                    return;
                }

                battle.ShikigamiBattles.Clear();
                db.SaveChanges();
                battle.StageId = b.StageId;
                battle.Title = b.Title;
                battle.ShikigamiBattles = b.ShikigamiBattles;
                db.SaveChanges();

                LoggingService.WriteLine($"[Edit] Battle { b.Id }.", Severities.Info);
            }
        }

        public void AddBattle(Battle b)
        {
            using (var db = new OnmyojiContext())
            {
                db.Battles.Add(b);
                db.SaveChanges();

                LoggingService.WriteLine($"[Add] Battle {b.Id}.", Severities.Info);
            }
        }

        public void DeleteBattle(Battle b)
        {
            using (var db = new OnmyojiContext())
            {
                var battle = db.Battles.FirstOrDefault(a => a.Id == b.Id);
                db.Battles.Remove(battle);
                db.SaveChanges();

                LoggingService.WriteLine($"[Delete] Battle { b.Id }", Severities.Info);
            }
        }

        #endregion
    }
}