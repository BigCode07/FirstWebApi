using BibliotecaApi.Datos;
using BibliotecaApi.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public AutoresController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("/listado-de-autores")]
        [HttpGet]
        public async Task<IEnumerable<Autor>> Get()
        {
            return await context.Autores.ToListAsync();
        }

        [HttpGet("primero")]  // api/autores/primero
        public async Task<Autor> GetPrimerAutor()
        {
            return await context.Autores
                .FirstAsync();
        }

        [HttpGet("{id:int}")] //api/autores/id?llave1=valor1&llave2=valor2
        public async Task<ActionResult<Autor>> Get([FromRoute] int id,[FromHeader] bool incluirLibros)
        {
            var autor = await context.Autores
                .Include(x => x.Libros)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (autor is null) return NotFound();

            return autor;
        }


        [HttpGet("{nombre:alpha}")]
        public async Task<IEnumerable<Autor>> Get(String nombre)
        {
            return await context.Autores.Where(autor => autor.Nombre.Contains(nombre)).ToListAsync();
        }

        //[HttpGet("{parametro1}/{parametro2?}")]
        //public ActionResult Get(string parametro1, string parametro2 = "Valor por defecto")
        //{
        //    return Ok(new {parametro1,parametro2});
        //}

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Autor autor)
        {
            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Autor autor)
        {
            if (id != autor.Id) return BadRequest("Los Ids deben de coincidir");

            context.Update(autor);
            await context.SaveChangesAsync();
            return Ok();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var registroBorrado = await context.Autores.Where(x => x.Id == id).ExecuteDeleteAsync();

            if (registroBorrado == 0) return NotFound();

            return Ok();
        }

    }
}
