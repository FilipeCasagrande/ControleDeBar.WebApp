using ControleDeBar.Dominio.ModuloMesa;
using ControleDeBar.Dominio.ModuloProduto;
using ControleDeBar.Infraestrutura.Arquivos.Compartilhado;

namespace ControleDeBar.Infraestrutura.Arquivos.ModuloMesa;

public class RepositorioProdutoEmArquivo : RepositorioBaseEmArquivo<Produto>
{
    public RepositorioProdutoEmArquivo(ContextoDados contextoDados) : base(contextoDados)
    {
    }

    protected override List<Produto> ObterRegistros()
    {
        return contextoDados.Produtos;
    }
}
