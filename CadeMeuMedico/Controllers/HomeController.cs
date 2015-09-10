using System.Web;
using System.Web.Mvc;

namespace MembroIndependente.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var Usuario = System.Web.HttpContext.Current.Request.Cookies["UserCookieAuthentication"];
            ViewData["UserName"] = Usuario != null ? Usuario.Values["Usuario"] : string.Empty;
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Title = "Seja bem vindo(a)";
            return View();
        }

        public ActionResult Esqueci()
        {
            ViewBag.Title = "Recuparação de acesso";
            return View();
        }
    }
}
