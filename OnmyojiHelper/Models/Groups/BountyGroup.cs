using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnmyojiHelper.Models.Groups
{
    public class BountyGroup
    {
        public Enums.Rarity Rarity { get; set; }

        public List<Bounty> Bounties { get; set; }
    }
}
