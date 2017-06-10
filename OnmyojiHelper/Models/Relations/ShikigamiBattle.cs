using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnmyojiHelper.Models.Relations
{
    [Table("ShikigamiBattle")]
    public class ShikigamiBattle
    {
        public int ShikigamiId { get; set; }
        public Shikigami Shikigami { get; set; }

        public int BattleId { get; set; }
        public Battle Battle { get; set; }

        public int Count { get; set; }
    }
}
