using Microsoft.AspNet.Identity;
using MVC.CMN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MVC.CMN.Extensions {
    public static class IdentityExtensions {
        public static async Task<ApplicationUser> FindByEmailAsync(this UserManager<ApplicationUser> userManager, string userEmail, string password) {
            string username = null;
            var userForEmail = await userManager.FindByEmailAsync(userEmail);            
            if (userEmail != null && userForEmail != null) {
                username = userForEmail.UserName;
                return await userManager.FindAsync(username, password);
            }
            return null;
        }
    }
}