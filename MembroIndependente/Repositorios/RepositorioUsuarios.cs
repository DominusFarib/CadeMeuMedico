using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using MembroIndependente.Models;

namespace MembroIndependente.Repositorios
{
    public class RepositorioUsuarios
    {
        public static bool AutenticarUsuario(string Login, string Senha)
        {
            var SenhaCriptografada = MembroIndependente.Repositorios.RepositorioCriptografia.CriptografaSHA1(Senha);

            try
            {
                using (MembroIndependenteBDEntities db = new MembroIndependenteBDEntities())
                {
                    var QueryAutenticaUsuarios = db.Usuarios.Where(x => x.Login == Login && x.Senha == SenhaCriptografada).SingleOrDefault();

                    if (QueryAutenticaUsuarios == null)
                    {
                        return false;
                    }
                    else
                    {
                        RepositorioCookies.RegistraCookieAutenticacao(QueryAutenticaUsuarios.IDUsuario);
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Usuarios GetUsuarioPorID(long IDUsuario)
        {
            try
            {
                using (MembroIndependenteBDEntities db = new MembroIndependenteBDEntities())
                {
                    var Usuario = db.Usuarios. Where(u => u.IDUsuario == IDUsuario).SingleOrDefault();
                    return Usuario;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Usuarios GetUsuarioPorEmail(string email)
        {
            try
            {
                using (MembroIndependenteBDEntities db = new MembroIndependenteBDEntities())
                {
                    var Usuario = db.Usuarios.Where(u => u.Email == email).SingleOrDefault();
                    return Usuario;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Usuarios VerificaUsuarioLogado()
        {
            var Usuario = HttpContext.Current.Request.Cookies["UserCookieAuthentication"];

            if (Usuario == null)
            {
                return null;
            }
            else
            {
                long IDUsuario = Convert.ToInt64(RepositorioCriptografia.Descriptografar(Usuario.Values["IDUsuario"]));
                var UsuarioRetornado = GetUsuarioPorID(IDUsuario);

                return UsuarioRetornado;
            }
        }
    }
}