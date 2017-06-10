using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnmyojiHelper.Models
{
    [Table("Stage")]
    public class Stage
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public Enums.StageCategory Category { get; set; }

        public List<Battle> Battles { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
