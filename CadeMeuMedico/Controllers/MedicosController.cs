using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using CadeMeuMedico.Models;

namespace CadeMeuMedico.Controllers
{
    public class MedicosController : BaseController
    {
        private CadeMeuMedicoBDEntities baseDados = new CadeMeuMedicoBDEntities();

        public ActionResult Index()
        {
            var listaMedicos = baseDados.Medicos.Include("Cidades").Include("Especialidades").ToList();

            return View(listaMedicos);
        }
        // 1 - CADASTRO 
        public ActionResult MedicosAdd()
        {
            // Passa para o front via ViewBag as listas de cidades e especialidade para popularem um ComboBox
            ViewBag.FKCidade = new SelectList(baseDados.Cidades, "IDCidade", "Nome");

            ViewBag.FKEspecialidade = new SelectList(baseDados.Especialidades, "IDEspecialidade", "Nome");
            return View();
        }

        // 2 - CADASTRO: SOBRESCRITA DE METODO PARA PEGAR DADOS VIA POST
        [HttpPost]
        public ActionResult MedicosAdd(Medicos medico)
        {
            // SE A CONEXÃO NÃO ESTIVER ESTABELECIDA A NAVEGAÇÃO É REDIRECIONADA
            if (ModelState.IsValid)
            {
                baseDados.Medicos.Add(medico);
                baseDados.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FKCidade = new SelectList(baseDados.Cidades, "IDCidade", "Nome", medico.FKCidade);
            ViewBag.FKEspecialidade = new SelectList(baseDados.Especialidades, "IDEspecialidade", "Nome", medico.FKEspecialidade);

            return View(medico);
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
            if (ModelState.IsValid)
            {
                baseDados.Entry(medico).State = EntityState.Modified;
                baseDados.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDCidade = new SelectList(baseDados.Cidades, "IDCidade", "Nome", medico.FKCidade);
            ViewBag.IDEspecialidade = new SelectList(baseDados.Especialidades, "IDEspecialidade", "Nome", medico.FKEspecialidade);
            return View(medico);
        }

        // 5 - EXCLUSÃO
        [HttpPost]
        public string MedicosDelet(long id)
        {
            try
            {
                Medicos medico = baseDados.Medicos.Find(id);
                baseDados.Medicos.Remove(medico);
                baseDados.SaveChanges();

                return Boolean.TrueString;
            }
            catch
            {
                return Boolean.FalseString;
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

        //public ActionResult Index()
        //{
        //    var listaMedicos = baseDados.Medicos.Include("Cidades").Include("Especialidades").ToList();

        //    return View(listaMedicos);
        //}



    }
}
