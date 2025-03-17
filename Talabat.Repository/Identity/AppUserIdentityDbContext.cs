using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity.Identity;

namespace Talabat.Repository.Identity
{
    public class AppUserIdentityDbContext :IdentityDbContext<AppUser>
    {
        public AppUserIdentityDbContext(DbContextOptions<AppUserIdentityDbContext> option):base(option)
        {
            
        }
    }
}
