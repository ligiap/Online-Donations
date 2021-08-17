using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Online_Donations.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the Online_DonationsUser class
    public class Online_DonationsUser : IdentityUser
    {

    [PersonalData]
    public string Name { get; set; }

    public bool IsAdmin { get; set; }
   
    }

	public class AdditionalUserClaimsPrincipalFactory
		: UserClaimsPrincipalFactory<Online_DonationsUser, IdentityRole>
	{
		public AdditionalUserClaimsPrincipalFactory(
			UserManager<Online_DonationsUser> userManager,
			RoleManager<IdentityRole> roleManager,
			IOptions<IdentityOptions> optionsAccessor)
			: base(userManager, roleManager, optionsAccessor)
		{ }

		public async override Task<ClaimsPrincipal> CreateAsync(Online_DonationsUser user)
		{
			var principal = await base.CreateAsync(user);
			var identity = (ClaimsIdentity)principal.Identity;

			var claims = new List<Claim>();
			if (user.IsAdmin)
			{
				claims.Add(new Claim(ClaimTypes.Role, "admin"));
			}
			else
			{
				claims.Add(new Claim(ClaimTypes.Role, "user"));
			}

			identity.AddClaims(claims);
			return principal;
		}
	}
}
