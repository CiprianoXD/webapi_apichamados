using Projeto01_ApiChamados.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Projeto01_ApiChamados.Dados
{
    public class Db
    {
        public static string EnviarChamado(Chamado chamado)
        {
            using (var ctx = new ChamadoEntities())
            {
                ctx.TBCHAMADOS.Add(chamado);
                ctx.SaveChanges();
                return "Chamado Recebido com sucesso";
            }
        }
        public static List<Chamado> ListaChamados()
        {
            using (var ctx = new ChamadoEntities())
            {
                return ctx.TBCHAMADOS.ToList();
            }
        }
        public static Chamado BuscarChamado(int id)
        {
            using (var ctx = new ChamadoEntities())
            {
                return ctx.TBCHAMADOS.FirstOrDefault(p => p.ChamadoId == id);
            }
        }
        
        public static Chamado BuscarChamadoRestricao(int id)
        {
            using (var ctx = new ChamadoEntities())
            {
                return ctx.TBCHAMADOS.FirstOrDefault(p => p.ChamadoId == id && p.Resposta.Equals(null));
            }
        }
        public static string ApagarChamado(int id)
        {
            using (var ctx = new ChamadoEntities())
            {
                var chamado = ctx.TBCHAMADOS.FirstOrDefault(p => p.ChamadoId == id);
                ctx.Entry<Chamado>(chamado).State = EntityState.Deleted;
                ctx.SaveChanges();
                return "Chamado Deletado";
            }
        }

        public static string AlterarChamado(Chamado chamado)
        {

            using (var ctx = new ChamadoEntities())
            {
                ctx.Entry<Chamado>(chamado).State = EntityState.Modified;
                ctx.SaveChanges();
                return "Chamado Respondido com sucesso";
            }

        }

    }
}
