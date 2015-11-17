using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LL_Lab6.Models
{
    public class MyInitializer : DropCreateDatabaseAlways<MyDbContext>
     {
         protected override void Seed(MyDbContext dc)
         {
             InitializeIdentity(dc);
             InitializeDomainData(dc);
         }

         private void InitializeIdentity(MyDbContext dc)
         {
             var UserManager = new UserManager<MyUser>(new UserStore<MyUser>(dc));
             var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dc));

             string adminRole = "Administrator";
             string adminEmail = "admin@company.ca";
             string password = "123456";

             if (!RoleManager.RoleExists(adminRole))
             {
                 var roleResult = RoleManager.Create(new IdentityRole(adminRole));
             }

             var adminUser = new MyUser();
             adminUser.UserName = adminEmail;
             adminUser.Email = adminEmail;
             var adminResult = UserManager.Create(adminUser, password);

             if (adminResult.Succeeded)
             {
                 var result = UserManager.AddToRole(adminUser.Id, adminRole);
                 UserManager.CreateIdentity(adminUser, DefaultAuthenticationTypes.ApplicationCookie);
                 UserManager.AddClaim(adminUser.Id, new Claim(ClaimTypes.Email, adminUser.Email));
                 UserManager.AddClaim(adminUser.Id, new Claim(ClaimTypes.Role, adminRole));
             }

             string memberName = "mark@company.ca";
             string memberRole = "Member";
             var member = new MyUser();

             if (!RoleManager.RoleExists(memberRole))
             {
                 var roleResult = RoleManager.Create(new IdentityRole(memberRole));
             }

             member.UserName = memberName;
             member.Email = memberName;
             var userResult = UserManager.Create(member, password);

             if (userResult.Succeeded)
             {
                 var result = UserManager.AddToRole(member.Id, memberRole);
             }
         }
     
         private void InitializeDomainData(MyDbContext dc)
         {
             Team DBA = new Team();
             DBA.Id = 1;
             DBA.Name = "Database Aministrator";
             DBA.Availability = Availability.ONCALL;
     
             Team ETL = new Team();
             ETL.Id = 2;
             ETL.Name = "ETL Specialist";
             ETL.Availability = Availability.BUSINESS_TIME;
     
             Team Designer = new Team();
             Designer.Id = 3;
             Designer.Name = "BI Designer";
             Designer.Availability = Availability.BUSINESS_TIME;
     
             Incident incident = new Incident();
             incident.Id = 1;
             incident.Name = "Database Shortage";
             incident.Description = "The usage of database exceed 80% of its capacity";
             incident.RTO = RTO.REGULAR;
             incident.Teams.Add(DBA);
             dc.Incidents.Add(incident);
             incident = null;
     
             incident = new Incident();
             incident.Id = 2;
             incident.Name = "Source Absent";
             incident.Description = "The source file of sales records on June 20, 2015 has not arrived";
             incident.RTO = RTO.IMPORTANT;
             incident.Teams.Add(ETL);
             dc.Incidents.Add(incident);
             incident = null;
     
             incident = new Incident();
             incident.Id = 3;
             incident.Name = "Report Complaint";
             incident.Description = "The report generated on June 20, 2015 has fault data";
             incident.RTO = RTO.EMERGENCY;
             incident.Teams.Add(ETL);
             incident.Teams.Add(Designer);
             dc.Incidents.Add(incident);
             incident = null;
     
             dc.SaveChanges();
         }     
    }
}
