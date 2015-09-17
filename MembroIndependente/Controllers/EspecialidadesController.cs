using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using MembroIndependente.Models;

namespace MembroIndependente.Controllers
{
    public class EspecialidadesController : BaseController
    {
        private MembroIndependenteBDEntities baseDados = new MembroIndependenteBDEntities();
        
        // GET: /Especialidades/
        public ActionResult Index()
        {
            var listaEspecialidades = baseDados.Especialidades.ToList();

            return View(listaEspecialidades);
        }

        // 1 - CADASTRO 
        public ActionResult EspecialidadesAdd()
        {
            return View();
        }

        // 2 - CADASTRO: SOBRESCRITA DE METODO PARA PEGAR DADOS VIA POST
        [HttpPost]
        public ActionResult EspecialidadesAdd(Especialidades especialidade)
        {
            // SE A CONEXÃO NÃO ESTIVER ESTABELECIDA A NAVEGAÇÃO É REDIRECIONADA
            if (ModelState.IsValid)
            {
                baseDados.Especialidades.Add(especialidade);
                baseDados.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(especialidade);
        }

        // 3 - EDIÇÃO
        public ActionResult EspecialidadesEdit(long id)
        {
            Especialidades especialidade = baseDados.Especialidades.Find(id);
            return View(especialidade);
        }

        // 4 - EDIÇÃO: SOBRECARGA PARA PEGAR DADOS VIA POST
        [HttpPost]
        public ActionResult EspecialidadesEdit(Especialidades especialidade)
        {
            if (ModelState.IsValid)
            {
                baseDados.Entry(especialidade).State = EntityState.Modified;
                baseDados.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(especialidade);
        }

        // 5 - EXCLUSÃO
        [HttpPost]
        public string EspecialidadesDelet(long id)
        {
            try
            {
                Especialidades especialidade = baseDados.Especialidades.Find(id);
                baseDados.Especialidades.Remove(especialidade);
                baseDados.SaveChanges();

                return Boolean.TrueString;
            }
            catch
            {
                return Boolean.FalseString;
            }
        }

        // 3 - DETALHES
        [HttpPost]
        public ActionResult EspecialidadesDetalhe(long id)
        {
            Especialidades especialidade = baseDados.Especialidades.Find(id);
            return View(especialidade);
        }

    }
}
