using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        private readonly ICategoriaRepository categoriaRepository;
        public ProdutoRepository(ApplicationContext contexto,
            ICategoriaRepository categoriaRepository) : base(contexto)
        {
            this.categoriaRepository = categoriaRepository;
        }

        public IList<Produto> GetProdutos()
        {
            return dbSet
                .Include(p => p.Categoria)
                .ToList();
        }

        public IList<Produto> GetProdutos(string pesquisa)
        {
            var produtos = dbSet
                .Include(p => p.Categoria)
                .Where(p => p.Nome.ContainsSearch(pesquisa) || p.Categoria.Nome.ContainsSearch(pesquisa))
                .ToList();
            if (produtos != null)
            {
                return produtos;
            }
            return GetProdutos();
        }

        public async Task SaveProdutos(List<Livro> livros)
        {
            foreach (var livro in livros)
            {
                if (!dbSet.Where(p => p.Codigo == livro.Codigo).Any())
                {
                    var nomeCategoria = livro.Categoria + "/" + livro.Subcategoria;
                    await categoriaRepository.AddCategoria(nomeCategoria);
                    var produto = new Produto(livro.Codigo, livro.Nome, livro.Preco, categoriaRepository.GetCategoria(nomeCategoria));
                    dbSet.Add(produto);
                }
            }
            await contexto.SaveChangesAsync();
        }




    }


    public class Livro
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Subcategoria { get; set; }
        public decimal Preco { get; set; }
    }
}
