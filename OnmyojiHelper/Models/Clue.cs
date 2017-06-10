using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnmyojiHelper.Models
{
    [Table("Clue")]
    public class Clue
    {
        public int Id { get; set; }

        public string Keyword { get; set; }

        public ICollection<Relations.BountyClue> BountyClues { get; set; }
    }
}
