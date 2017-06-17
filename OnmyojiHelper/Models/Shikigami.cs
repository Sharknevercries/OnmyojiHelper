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

        public List<Relations.ShikigamiBattle> ShikigamiBattles { get; set; }

        public Shikigami() : this(0, default(string), default(Enums.Rarity)) { }

        public Shikigami(string name, Enums.Rarity rarity) : this(0, name, rarity) { }

        public Shikigami(int id, string name, Enums.Rarity rarity)
        {
            this.Id = id;
            this.Name = name;
            this.Rarity = rarity;
        }
    }
}
