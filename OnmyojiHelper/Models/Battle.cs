using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnmyojiHelper.Models
{
    [Table("Battle")]
    public class Battle
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public Stage Stage { get; set; }

        public List<Relations.ShikigamiBattle> ShikigamiBattles { get; set; } 
    }
}
