using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnmyojiHelper.Models.Relations
{
    [Table("BountyClue")]
    public class BountyClue
    {
        public int BountyId { get; set; }
        public Bounty Bounty { get; set; }

        public int ClueId { get; set; }
        public Clue Clue { get; set; }
    }
}
