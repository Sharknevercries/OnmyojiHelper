using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnmyojiHelper.Models
{
    [Table("Shikigami")]
    public class Shikigami
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Enums.Rarity Rarity { get; set; }

        public ICollection<Relations.ShikigamiBattle> ShikigamiBattles { get; set; }
    }
}
