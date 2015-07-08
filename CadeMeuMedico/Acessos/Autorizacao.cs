using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CadeMeuMedico.Repositorios;

namespace CadeMeuMedico.Acessos
{
    [HandleError]
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method, Inherited = true, AllowMultiple = true)]

    public class Autorizacao : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext FiltroDeContexto)
        {
            // PEGA A CONTROLLER E A ACTION SOLICITADAS
            var Controller = FiltroDeContexto.ActionDescriptor.ControllerDescriptor.ControllerName;
            var Action = FiltroDeContexto.ActionDescriptor.ActionName;

            if (Controller != "Home" || Action != "Login")
            {
                // VERIFICA SE O USUARIO JÁ ESTÁ LOGADO
                if (RepositorioUsuarios.VerificaUsuarioLogado() == null)
                {
                    FiltroDeContexto.RequestContext.HttpContext.Response.Redirect("/Home/Login?Url=" + FiltroDeContexto.HttpContext.Request.Url.LocalPath);
                }
            }
        }
    }
}