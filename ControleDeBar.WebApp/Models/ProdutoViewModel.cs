using System.ComponentModel.DataAnnotations;
using ControleDeBar.Dominio.ModuloGarcom;
using ControleDeBar.Dominio.ModuloProduto;

namespace ControleDeBar.WebApp.Models;

public class CadastrarProdutoViewModel
{
    public string Nome { get; set; }
    public decimal Valor { get; set; }
    public CadastrarProdutoViewModel()
    {
    }
}
public class VisualizarProdutoViewModel
{
    public List<DetalhesProdutoViewModel> Registros { get; set; } = new List<DetalhesProdutoViewModel>();
    public VisualizarProdutoViewModel(List<Produto> produto)
    {
        foreach (Produto p in produto)
        {
            if (p == null)
                continue;
            DetalhesProdutoViewModel detalhesVm = new DetalhesProdutoViewModel(
                p.Id,
                p.Nome,
                p.Valor
            );
            Registros.Add(detalhesVm);
        }
    }
}
public class DetalhesProdutoViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public decimal Valor { get; set; }
    public DetalhesProdutoViewModel(int id,string nome, decimal valor)
    {
        Id = id;
        Nome = nome;
        Valor = valor;
    }
}
public class EditarProdutoViewModel
{
    public int Id { get; set; }

    public string Nome { get; set; }

    public decimal Valor { get; set; }


    public EditarProdutoViewModel()
    {
    }
    public EditarProdutoViewModel(int id, string nome, decimal valor)
    {
        Id = id;
        Nome = nome;
        Valor = valor;
    }
}
public class ExcluirProdutoViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public ExcluirProdutoViewModel()
    { }
    public ExcluirProdutoViewModel(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}

