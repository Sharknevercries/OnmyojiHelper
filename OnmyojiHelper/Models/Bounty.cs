using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnmyojiHelper.Models
{
    [Table("Bounty")]
    public class Bounty
    {
        public int Id { get; set; }

        public int ShikigamiId { get; set; }
        public Shikigami Shikigami { get; set; }

        public List<Relations.BountyClue> BountyClues { get; set; }
    }
}
