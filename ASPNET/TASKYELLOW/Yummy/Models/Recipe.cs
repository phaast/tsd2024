using System.ComponentModel.DataAnnotations;

namespace Yummy.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Recipe Name")]
        public string? Name { get; set; }
        
        [Display(Name = "Time (minutes)")]
        public int Time { get; set; }
        
        public string? Difficulty { get; set; }
        
        [Display(Name = "Number of likes")]
        public int NumberOfLikes { get; set; }
        
        public string? Ingredients { get; set; }
        
        public string? Process { get; set; }
        
        [Display(Name = "Tips and Tricks")]
        public string? TipsAndTricks { get; set; }
    }
}