using System;
using Contracts;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Entity.Domain.Identity
{
    public class Role : IdentityRole<int, UserRole>, ITimeStamp, IEntity
    { 
        public Role()
        {
            CreatedUtc = DateTime.UtcNow;
        }

        public Role(string name)
        {
            Name = name;
        }

        public DateTime CreatedUtc { get; set; }
    }
}