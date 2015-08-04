using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using CadeMeuMedico.Models;
using CadeMeuMedico.Repositorios;
using System.Net.Mail;

namespace CadeMeuMedico.Controllers
{
    public class ContasController : BaseController
    {
        private CadeMeuMedicoBDEntities baseDados = new CadeMeuMedicoBDEntities();

        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Index(ContasModel dados)
        {
            // SE A CONEXÃO NÃO ESTIVER ESTABELECIDA A NAVEGAÇÃO É REDIRECIONADA
            if (ModelState.IsValid)
            {
                Usuarios usuario = new Usuarios();

                usuario.Email = dados.Email;
                usuario.Login = dados.Login;
                usuario.Nome = dados.Nome;
                usuario.Senha = RepositorioCriptografia.Criptografar(dados.Senha);

                baseDados.Usuarios.Add(usuario);
                baseDados.SaveChanges();
                ViewBag.Mensagem = "Usuario cadastrado com sucesso";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public JsonResult RecuperaConta(string email)
        {
            Usuarios usuario = new Usuarios();
            usuario = RepositorioUsuarios.GetUsuarioPorEmail(email);
            string msg = string.Empty;
            try
            {
                if (usuario != null)
                {
                    SmtpClient emailClient = new SmtpClient();

                    emailClient.EnableSsl = true;
                    //emailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    emailClient.Credentials = new System.Net.NetworkCredential("domingos.ribeiro@emepar.com.br", "dfr123654");
                    emailClient.UseDefaultCredentials = true;
                    //emailClient.Host = "smtp.gmail.com";
                    emailClient.Host = "mail.emepar.com.br";
                    emailClient.Port = 995;

                    MailAddress SendFrom = new MailAddress("domingos.ribeiro@emepar.com.br");
                    MailAddress SendTo = new MailAddress(usuario.Email.ToString());

                    MailMessage MyMessage = new MailMessage(SendFrom, SendTo);
 
                    MyMessage.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
                    MyMessage.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
                    MyMessage.Priority = System.Net.Mail.MailPriority.High;
                    MyMessage.IsBodyHtml = true;
                    MyMessage.Subject = "Recuperação de senha";
                    MyMessage.Body = "Caro uruário,<br> segue o link para a renovação de seus dados de acesso";
                    // Attachment attachFile = new Attachment(txtAttachmentPath.Text);
                    // MyMessage.Attachments.Add(attachFile);
 

                    emailClient.Send(MyMessage);

                    msg = usuario.Nome.ToString() + @", dados de recuperação de senha encaminhados para:" + usuario.Email.ToString();
                    return Json(new { OK = true, Mensagem = msg }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    msg = "Usuário não cadastrado, verifique o e-mail informado";
                    return Json(new { OK = false, Mensagem = msg }, JsonRequestBehavior.AllowGet);
                }  
            }
            catch (Exception ex)
            {
                return Json(new { OK = false, Mensagem = @" 001: Falha ao acessar seridor[" + ex.ToString() + @"]" }, JsonRequestBehavior.AllowGet);
            }

        }

    }
}
