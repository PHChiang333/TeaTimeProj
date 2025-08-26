using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TeaTimeProj.Models
{
    public class Category
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("類別名稱")]
        [MaxLength(30)]
        public string Name { get; set; }

        [DisplayName("顯示順序")]
        [Range(1,100, ErrorMessage ="輸入範圍應該要在1-100")]
        public int DisplayOrder { get; set; }




    }
}
