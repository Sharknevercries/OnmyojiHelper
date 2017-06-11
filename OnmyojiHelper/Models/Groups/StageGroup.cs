using OnmyojiHelper.Models.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Windows.UI.Xaml.Data;
using Windows.Foundation.Collections;

namespace OnmyojiHelper.Models.Groups
{
    public class StageGroup
    {
        public StageCategory Category { get; private set; }

        public ObservableCollection<Stage> Stages { get; private set; }

        public StageGroup(StageCategory category, IEnumerable<Stage> stages) 
        {
            this.Category = category;
            this.Stages = new ObservableCollection<Stage>(stages);
        }

        public override string ToString()
        {
            return Category.ToString();
        }
    }
}
