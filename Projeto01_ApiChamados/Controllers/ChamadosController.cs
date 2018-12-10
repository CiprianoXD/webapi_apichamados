using Projeto01_ApiChamados.Dados;
using Projeto01_ApiChamados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Projeto01_ApiChamados.Controllers
{
    public class ChamadosController : ApiController
    {

        [EnableCors (origins:"*" , methods:"*", headers:"*")]
        //HTTP GET - Lista todos os Chamados
        public IEnumerable<Chamado> GetChamados()
        {
            return Db.ListaChamados();
        }
        //HTTP GET - Lista todos os Chamados
        public Chamado GetChamado(int id)
        {
            return Db.BuscarChamado(id);
        }

        [HttpGet]
        [Route("buscaChamadoDescricao/{id}")]
        public Chamado buscaChamadoDescricao(int id)
        {
            return Db.BuscarChamadoRestricao(id);
        }
        //HTTP POST - Inclusão de Chamados
        public HttpResponseMessage PostChamados(Chamado chamado)
        {
            string resultado = Db.EnviarChamado(chamado);

            if (resultado == "Chamado Recebido com sucesso")
            {
                var response = Request.CreateResponse<Chamado>(
                    HttpStatusCode.Created, chamado);

                string uri = Url.Link("DefaultApi", new { id = chamado.ChamadoId });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            else
            {
                string mensagem = "Erro Inesperado";
                var erro = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Erro no servidor"),
                    ReasonPhrase = mensagem
                };
                throw new HttpResponseException(erro);
            }
        }
        //HTTP PUT - Alteração de Pagamento
        public HttpResponseMessage PutChamado(int id, Chamado chamado)
        {
         
            string resultado = Db.AlterarChamado(chamado);

            if (resultado.Equals("Chamado Respondido com sucesso"))
            {
                var response = Request.CreateResponse<Chamado>(
                    HttpStatusCode.Created , chamado);

                string uri = Url.Link("DefaultApi", new { id = chamado.ChamadoId });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            else
            {
                string mensagem = "Erro inesperado";

                var erro = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Erro no servidor"),
                    ReasonPhrase = mensagem
                };
                throw new HttpResponseException(erro);
            }
        }

        public HttpResponseMessage DeleteChamado(int id)
        {
            string resultadoChamado = Db.ApagarChamado(id);
            Chamado chamado = Db.BuscarChamado(id);
            if (resultadoChamado.Equals("Chamado Deletado"))
            {
                var response = Request.CreateResponse<Chamado>(
                    HttpStatusCode.Created, chamado);

                string uri = Url.Link("DefaultApi", new { id = id});
                response.Headers.Location = new Uri(uri);
                return response;
            }
            else
            {
                var erro = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Erro no servidor"),
                    ReasonPhrase = "Nenhum Chamado a ser removido"
                };
                throw new HttpResponseException(erro);
            }
        }
    }
}
