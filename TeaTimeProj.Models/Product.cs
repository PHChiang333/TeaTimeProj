using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeaTimeProj.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        [Required]
        public string Size { get; set; }
        [Required]
        [Range(1,10000)]
        public double Price { get; set; }
        public string Description { get; set; }

    }
}
