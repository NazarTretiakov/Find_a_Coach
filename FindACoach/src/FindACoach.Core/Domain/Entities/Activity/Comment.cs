using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.Domain.Entities.Activity
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public IdentityEntities.User? User { get; set; }

        public Guid ActivityId { get; set; }
        public Activity? Activity { get; set; }


        [StringLength(500, MinimumLength = 3)]
        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime DateOfCreation { get; set; }
    }
}
