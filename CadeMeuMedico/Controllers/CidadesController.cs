﻿using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using MembroIndependente.Models;


namespace MembroIndependente.Controllers
{
    public class CidadesController : BaseController
    {
        private MembroIndependenteBDEntities baseDados = new MembroIndependenteBDEntities();

        public ActionResult Index()
        {
            var listaCidades = baseDados.Cidades.ToList();

            return View(listaCidades);
        }
        // 1 - CADASTRO 
        public ActionResult CidadesAdd()
        {
            return View();
        }

        // 2 - CADASTRO: SOBRESCRITA DE METODO PARA PEGAR DADOS VIA POST
        [HttpPost]
        public ActionResult CidadesAdd(Cidades cidade)
        {
            // SE A CONEXÃO NÃO ESTIVER ESTABELECIDA A NAVEGAÇÃO É REDIRECIONADA
            if (ModelState.IsValid)
            {
                baseDados.Cidades.Add(cidade);
                baseDados.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cidade);
        }

        // 3 - EDIÇÃO
        public ActionResult CidadesEdit(long id)
        {
            Cidades cidade = baseDados.Cidades.Find(id);
            return View(cidade);
        }

        // 4 - EDIÇÃO: SOBRECARGA PARA PEGAR DADOS VIA POST
        [HttpPost]
        public ActionResult CidadesEdit(Cidades cidade)
        {
            if (ModelState.IsValid)
            {
                baseDados.Entry(cidade).State = EntityState.Modified;
                baseDados.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cidade);
        }

        // 5 - EXCLUSÃO
        [HttpPost]
        public string CidadesDelet(long id)
        {
            try
            {
                Cidades cidade = baseDados.Cidades.Find(id);
                baseDados.Cidades.Remove(cidade);
                baseDados.SaveChanges();

                return Boolean.TrueString;
            }
            catch
            {
                return Boolean.FalseString;
            }
        }
    }
}
