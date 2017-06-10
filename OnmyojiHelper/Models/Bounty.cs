﻿using System;
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

        public Shikigami Shikigami { get; set; }

        public ICollection<Relations.BountyClue> BountyClues { get; set; }
    }
}
