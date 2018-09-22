using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models.ViewModels
{
    public class BuscaViewModel
    {
        public IList<Produto> Itens { get; private set; }
        public string Pesquisa { get; private set; }

        public BuscaViewModel(IList<Produto> itens)
        {
            this.Itens = itens;
        }
        public BuscaViewModel(IList<Produto> itens, string pesquisa) : this(itens)
        {
            Pesquisa = pesquisa;
        }
        public bool IsSearchFound(List<Categoria> categorias)
        {
            bool found = categorias
            .Where(c => c.Nome.ContainsSearch(Pesquisa))
            .Any() ||
            Itens
            .Where(p => p.Nome.ContainsSearch(Pesquisa))
            .Any();
            return found;
        }
        public List<Produto> GetProdutosPorCategoria(Categoria categoria)
        {
            return Itens.Where(p => p.Categoria.Nome == categoria.Nome).ToList();
        }
    }
}
