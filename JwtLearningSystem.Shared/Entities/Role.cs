using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtLearningSystem.Shared.Entities
{
    public class Role
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string NormalizedName { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public List<User> Users { get; set; } = new();
    }
}
