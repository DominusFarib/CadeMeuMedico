using System.Web.Mvc;

namespace CadeMeuMedico.Areas.MobilesArea
{
    public class MobilesAreaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "MobilesArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "MobilesArea",
                "MobilesArea/{controller}/{action}/{id}",
                new { controller = "home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
