using Microsoft.AspNetCore.Identity;
using System;

namespace storedetail.Model.Domain
{
    public class ApplicationUser :IdentityUser
    {
        public DateTime RegisterDate { get; set; } = DateTime.UtcNow;

    }
}
