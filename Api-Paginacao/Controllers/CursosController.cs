using Api_Paginacao.Models;
using Api_Paginacao.Models.BaseContext;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.OData;

namespace Api_Paginacao.Controllers
{
    public class CursosController : ApiController
    {

        
        CursoDbContext db = new CursoDbContext();

        public IHttpActionResult PostCurso(Curso curso)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            db.Cursos.Add(curso);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = curso.Id }, curso);

        }

        public IHttpActionResult GetCurso(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id deve ser um número maior que 0");
            }

            var curso = db.Cursos.Find(id);

            if (curso == null)
            {
                return BadRequest("Curso não encontrado");
            }

            return Ok(curso);

        }

        public IHttpActionResult PutCurso (int id, Curso curso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            } 
            
            if( id != curso.Id)
            {
                return BadRequest("Id informado na URL é diferente do corpo da requisição");
            }

            if (db.Cursos.Count(c => c.Id == id) == 0)
            {
                return NotFound();
            }

            db.Entry(curso).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);

        } 
        
        public IHttpActionResult DeleteCurso (int id)
        {
            if( id <= 0)
            {
                return BadRequest("Id deve ser maior que zero");
            }

            var curso = db.Cursos.Find(id);

            if(curso == null)
            {
                return NotFound();
            }

            db.Cursos.Remove(curso);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [EnableQuery]
        public IHttpActionResult GetCursos(int pagina = 1, int tamanhoPagina = 10)
        {
            //Validações
            if(pagina <= 0 || tamanhoPagina <= 0)
            {
                return BadRequest("Os parametros devem ser maior que 0");
            }
            if(tamanhoPagina > 10)
            {
                return BadRequest("O tamanho máximo de pagina é 10");
            }

            //Calculando o tatoal de páginas
            int totalPaginas = (int)Math.Ceiling(db.Cursos.Count() / Convert.ToDecimal(tamanhoPagina));

            if(pagina > totalPaginas)
            {
                return BadRequest("A página solicitada não existe");
            }

            //Customização do Header
            System.Web.HttpContext.Current.Response.AddHeader("X - Pagination - TotalPge", totalPaginas.ToString());

            if(pagina > 1)
            System.Web.HttpContext.Current.Response.AddHeader("X - Pagination - PreviousPage",
                Url.Link("DefaultApi", new { pagina = pagina - 1, tamanhoPagina = tamanhoPagina }));   

            if(pagina < totalPaginas)
            System.Web.HttpContext.Current.Response.AddHeader("X - Pagination - NextPage",
                Url.Link("DefaultApi", new { pagina = pagina + 1, tamanhoPagina = tamanhoPagina }));


            //Paginação
            var cursos = db.Cursos.OrderBy(c => c.DataPublicacao).Skip(tamanhoPagina * (pagina -1)). Take(tamanhoPagina);

            return Ok (cursos);
        }
    }
}