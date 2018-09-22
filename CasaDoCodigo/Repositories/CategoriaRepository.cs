using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasaDoCodigo.Models;

namespace CasaDoCodigo.Repositories
{
    public interface ICategoriaRepository
    {
        Task AddCategoria(string nomeCategoria);
        Categoria GetCategoria(string nomeCategoria);
    }
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationContext contexto) : base(contexto)
        {
        }
        public async Task AddCategoria(string nomeCategoria)
        {
            Categoria categoria = new Categoria(nomeCategoria);
            if (!dbSet.Where(c => c.Nome == nomeCategoria).Any())
            {
                dbSet.Add(new Categoria(nomeCategoria));
            }
            await contexto.SaveChangesAsync();
            
        }

        public Categoria GetCategoria(string nomeCategoria)
        {
            var categoria = dbSet.Where(c => c.Nome == nomeCategoria).SingleOrDefault();
            if (categoria == null)
            {
                throw new ArgumentException("Categoria não existe no DB");
            }
            return categoria;
        }

    }
}
