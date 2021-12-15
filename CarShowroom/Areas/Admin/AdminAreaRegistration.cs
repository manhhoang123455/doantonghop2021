using System.Web.Mvc;

namespace CarShowroom.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { Controller = "Xe", action = "Index", id = UrlParameter.Optional },
                new[] { "CarShowroom.Areas.Admin.Controllers" }
            );
        }
    }
}