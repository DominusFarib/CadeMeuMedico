using CadeMeuMedico.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CadeMeuMedico.Controllers
{
    public class UsuariosController : BaseController
    {
        //
        // GET: /Usuarios/
        [HttpGet]
        public JsonResult AutenticaUsuario(string login, string senha)
        {
            if(RepositorioUsuarios.AutenticarUsuario(login, senha))
                return Json(new { OK = true, Mensagem = "Usuario autenticado. Redirecionando..." }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { OK = false, Mensagem = "Usuário não encontrando. Tente novamente."}, JsonRequestBehavior.AllowGet);
        }
    }
}
