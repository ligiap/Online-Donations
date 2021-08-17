using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Online_Donations.Areas.Identity.Data;
using Online_Donations.Models;

[assembly: HostingStartup(typeof(Online_Donations.Areas.Identity.IdentityHostingStartup))]
namespace Online_Donations.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<Online_DonationsContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("Online_DonationsContextConnection")));

                services.AddDefaultIdentity<Online_DonationsUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<Online_DonationsContext>();
                


            });
            
        }
    }
}