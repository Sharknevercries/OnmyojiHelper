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

        IEnumerable<Stage> GetAllStages();
        void EditStage(Stage s);
        void AddStage(Stage s);
        void DeleteStage(Stage s);
        bool IsLegalStage(Stage s);

        IEnumerable<Shikigami> GetAllShikigamis();
        void EditShikigami(Shikigami s);
        void AddShikigami(Shikigami s);
        void DeleteShikigami(Shikigami s);
        bool IsLegalShikigami(Shikigami s);

    }
}
