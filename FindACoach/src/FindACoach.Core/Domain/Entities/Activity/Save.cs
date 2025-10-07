using FindACoach.Core.Domain.IdentityEntities;
using System.ComponentModel.DataAnnotations;

namespace FindACoach.Core.Domain.Entities.Activity
{
    public class Save
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ActivityId { get; set; }
        public Activity? Activity { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
