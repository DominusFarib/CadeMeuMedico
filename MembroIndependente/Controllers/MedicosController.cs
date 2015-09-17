using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using MembroIndependente.Models;
using System.Web;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

namespace MembroIndependente.Controllers
{
    public class MedicosController : BaseController
    {
        private MembroIndependenteBDEntities baseDados = new MembroIndependenteBDEntities();

        public ActionResult Index()
        {
            var listaMedicos = baseDados.Medicos.Include("Cidades").Include("Especialidades").ToList();

            return View(listaMedicos);
        }
        // 1 - CADASTRO 
        public ActionResult MedicosAdd()
        {
            ViewData["FotoErro"] = "";
            ViewBag.ErrorName = "";

            // Passa para o front via ViewBag as listas de cidades e especialidade para popularem um ComboBox
            ViewBag.FKCidade = new SelectList(baseDados.Cidades, "IDCidade", "Nome");

            ViewBag.FKEspecialidade = new SelectList(baseDados.Especialidades, "IDEspecialidade", "Nome");
            return View();
        }

        // 2 - CADASTRO: SOBRESCRITA DE METODO PARA PEGAR DADOS VIA POST
        [HttpPost]
        public ActionResult MedicosAdd(MembrosModel membro)
        {
            // SE A CONEXÃO NÃO ESTIVER ESTABELECIDA A NAVEGAÇÃO É REDIRECIONADA
            Medicos medico = new Medicos();

            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                var ImagemMetadados = Request.Form[1];
                string resposta = LoadImage(ImagemMetadados.ToString());
                
                if(resposta != string.Empty)
                {
                    if (resposta.Split(':')[0] == "sucesso")
                        medico.Foto = resposta.Split(':')[1];
                    else
                    {
                        //ViewData["FotoErro"] = resposta.Split(':')[1];
                        //ViewBag.ErrorName = "ArquivoIncorreto";
                        ModelState.AddModelError("Imagem", resposta.Split(':')[1]);
                    }
                }
            } 

            if (ModelState.IsValid)
            {
                medico.AtendePorConvenio = membro.AtendePorConvenio;
                medico.Bairro = membro.Bairro;
                medico.CRM = membro.CRM;
                medico.Email = membro.Email;
                medico.Endereco = membro.Endereco;
                medico.FKCidade = membro.FKCidade;
                medico.FKEspecialidade = membro.FKEspecialidade;
                medico.Nome = membro.Nome;
                medico.TemClinica = membro.TemClinica;
                medico.WebsiteBlog = membro.WebsiteBlog;
                
                baseDados.Medicos.Add(medico);
                baseDados.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.FKCidade = new SelectList(baseDados.Cidades, "IDCidade", "Nome", membro.FKCidade);
            ViewBag.FKEspecialidade = new SelectList(baseDados.Especialidades, "IDEspecialidade", "Nome", membro.FKEspecialidade);

            return View(membro);
        }

        // 3 - EDIÇÃO
        public ActionResult MedicosEdit(long id)
        {
            Medicos medico = baseDados.Medicos.Find(id);

            ViewBag.FKCidade = new SelectList(baseDados.Cidades, "IDCidade", "Nome", medico.FKCidade);
            ViewBag.FKEspecialidade = new SelectList(baseDados.Especialidades, "IDEspecialidade", "Nome", medico.FKEspecialidade);

            return View(medico);
        }

        // 4 - EDIÇÃO: SOBRECARGA PARA PEGAR DADOS VIA POST
        [HttpPost]
        public ActionResult MedicosEdit(Medicos medico)
        {
            Medicos antigo = baseDados.Medicos.Find(medico.IDMedico);
            string novaFoto = string.Empty;
            
            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                var ImagemMetadados = Request.Form[1];
                string resposta = LoadImage(ImagemMetadados.ToString());


                if (!String.IsNullOrEmpty(antigo.Foto))
                {
                    string caminhoFoto = Request.MapPath(antigo.Foto);
                    System.IO.File.Delete(caminhoFoto);
                }

                if (resposta != string.Empty)
                {
                    if (resposta.Split(':')[0] == "sucesso")
                        novaFoto = resposta.Split(':')[1];
                    else
                    {
                        ModelState.AddModelError("Imagem", resposta.Split(':')[1]);
                    }
                }
            } 

            if (ModelState.IsValid)
            {
                antigo.AtendePorConvenio = medico.AtendePorConvenio;
                antigo.Bairro = medico.Bairro;
                antigo.CRM = medico.CRM;
                antigo.Email = medico.Email;
                antigo.Endereco = medico.Endereco;
                antigo.FKCidade = medico.FKCidade;
                antigo.FKEspecialidade = medico.FKEspecialidade;
                antigo.Nome = medico.Nome;
                antigo.WebsiteBlog = medico.WebsiteBlog;

                antigo.Foto = novaFoto != string.Empty ? novaFoto : medico.Foto; 
                //baseDados.Entry(medico).State = EntityState.Modified;
                baseDados.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDCidade = new SelectList(baseDados.Cidades, "IDCidade", "Nome", medico.FKCidade);
            ViewBag.IDEspecialidade = new SelectList(baseDados.Especialidades, "IDEspecialidade", "Nome", medico.FKEspecialidade);
            return View(medico);
        }

        // 5 - EXCLUSÃO
        [HttpPost]
        public ActionResult MedicosDelet(long id)
        {
            try
            {
                Medicos medico = baseDados.Medicos.Find(id);
                if (!String.IsNullOrEmpty(medico.Foto))
                {
                    string caminhoFoto = Request.MapPath(medico.Foto);
                    System.IO.File.Delete(caminhoFoto);
                }
                baseDados.Medicos.Remove(medico);
                baseDados.SaveChanges();
                return Json(new { msg = "Exclusão finalizada", erro = ""});
                //return RedirectToAction("Index");
                //return Boolean.TrueString;
            }
            catch(Exception e)
            {
                return Json(new { msg = "", erro = "Não foi possível realizar exclusão, tente mais tarde"});
            }
        }

        // 6 - DETALHE
        [HttpGet]
        public ActionResult MedicosDetalhe(long id)
        {
            Medicos medico = baseDados.Medicos.Find(id);

            ViewBag.FKCidade = new SelectList(baseDados.Cidades, "IDCidade", "Nome", medico.FKCidade);
            ViewBag.FKEspecialidade = new SelectList(baseDados.Especialidades, "IDEspecialidade", "Nome", medico.FKEspecialidade);

            return View(medico);
        }

        public static string uploadFile(HttpPostedFileBase file, string UploadPath)
        {
            string imgPath = null;
            string fileName = System.IO.Path.GetRandomFileName();
            fileName = fileName.Replace(".", "");
            var ext = System.IO.Path.GetExtension(file.FileName);
            fileName += ext;
            System.IO.DirectoryInfo dr = new System.IO.DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~" + UploadPath));
            System.IO.FileInfo[] files = dr.GetFiles();
            for(;true;)
            {
                if (!files.Where(o => o.Name.Equals(fileName)).Any())
                    break;
                else
                {
                    fileName = System.IO.Path.GetRandomFileName();
                    fileName = fileName.Replace(".", "");
                    fileName += ext;
                }
            }
            var path = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~" + UploadPath), fileName);
            file.SaveAs(path);
            imgPath = UploadPath + fileName;
            return imgPath;
        }

        public string LoadImage(string baseImagem)
        {

            string[] extensoes = new string[] {"bmp", "gif", "jpg", "jpeg", "png"};

            // 1. VALIDA A EXTENSÃO DO ARQUIVO
            var tipo = Regex.Match(baseImagem, @"data:(?<tipo>.+?)/(?<data>.+)").Groups["tipo"].Value;

            if (tipo.Equals("image"))
            {
                var ext = Regex.Match(baseImagem, @"data:(?<tipo>.+?)/(?<ext>.+?);base64,(?<data>.+)").Groups["ext"].Value;

                if (Array.Exists(extensoes, element => element == ext))
                {
                    var base64Data = Regex.Match(baseImagem, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
                    byte[] bytes = Convert.FromBase64String(base64Data);

                    Image image;

                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        image = Image.FromStream(ms);
                        // 2. VALIDA O TAMANHO DO ARQUIVO
                        if ((bytes.Count() < 100000) && (bytes.Count() > 10000))
                        {
                            string fileName = System.IO.Path.GetRandomFileName();
                            fileName = fileName.Replace(".", "");

                            fileName += "." + ext;

                            System.IO.DirectoryInfo dr = new System.IO.DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~/Content/Perfis/"));
                            System.IO.FileInfo[] files = dr.GetFiles();
                            for (; true; )
                            {
                                if (!files.Where(o => o.Name.Equals(fileName)).Any())
                                    break;
                                else
                                {
                                    fileName = System.IO.Path.GetRandomFileName();
                                    fileName = fileName.Replace(".", "");
                                    fileName += ext;
                                }
                            }

                            var path = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/Perfis/"), fileName);
                            image.Save(path, System.Drawing.Imaging.ImageFormat.Png);
                            return "sucesso:/Content/Perfis/" + fileName;

                        }
                        else return "Erro:O tamanho do arquivo deve ser maior que Tipo de arquivo não é uma extensão válida (.bmp, .gif, .jpg, .jpeg, .png)";
                    }
                }
                else return "Erro:Tipo de imgem enviada não tem uma extensão válida: .bmp, .gif, .jpg, .jpeg, .png";
            }
            else return "Erro:O Tipo de arquivo enviado não é uma imagem válida";

            return string.Empty;
        }
    }
}
