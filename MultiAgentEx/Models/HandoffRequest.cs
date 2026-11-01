using System.ComponentModel.DataAnnotations;

namespace MultiAgentEx.Models
{
    public class HandoffRequest
    {
        [Required]
        [StringLength(5000, MinimumLength = 5)]
        public string Question { get; set; } = string.Empty;

    }
}
