using ControleDeBar.Dominio.ModuloProduto;
using ControleDeBar.Dominio.ModuloProduto;
using ControleDeBar.Infraestrutura.Arquivos.Compartilhado;
using ControleDeBar.Infraestrutura.Arquivos.ModuloMesa;
using ControleDeBar.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using static ControleDeBar.WebApp.Models.DetalhesProdutoViewModel;


namespace ControleDeBar.WebApp.Controllers;

public class ProdutoController : Controller
{
    private RepositorioProdutoEmArquivo repositorioProduto;
    public ProdutoController()
    {
        ContextoDados contexto = new ContextoDados(carregarDados: true);
        repositorioProduto = new RepositorioProdutoEmArquivo(contexto);
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<Produto> produto = repositorioProduto.SelecionarRegistros();
        VisualizarProdutoViewModel visualizarVm = new VisualizarProdutoViewModel(produto);
        return View(visualizarVm);
    }
    public IActionResult Cadastrar()
    {
        CadastrarProdutoViewModel cadastrarVm = new CadastrarProdutoViewModel();
        return View(cadastrarVm);
    }
    [HttpPost]
    public IActionResult Cadastrar(CadastrarProdutoViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);
        var Produto = new Produto(cadastrarVm.Nome, cadastrarVm.Valor);

        repositorioProduto.CadastrarRegistro(Produto);
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public IActionResult Editar(int id)
    {
        Produto Produtos = repositorioProduto.SelecionarRegistroPorId(id);
        if (Produtos == null)
        {
            return RedirectToAction(nameof(Index));
        }
        EditarProdutoViewModel editarVm = new EditarProdutoViewModel
            (Produtos.Id,
            Produtos.Nome,
            Produtos.Valor
            );
        return View(editarVm);
    }
    [HttpPost]
    public IActionResult Editar(EditarProdutoViewModel editarVm)
    {
        Produto ProdutoEditado = new Produto(editarVm.Nome, editarVm.Valor);
        bool edicaoConcluida = repositorioProduto.EditarRegistro(editarVm.Id, ProdutoEditado);
        if (!edicaoConcluida)
        {
            ProdutoEditado.Id = editarVm.Id;
            return View(ProdutoEditado);
        }
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public IActionResult Excluir(int id)
    {
        var Produtos = repositorioProduto.SelecionarRegistroPorId(id);

        if (Produtos == null)
            return RedirectToAction(nameof(Index));
        ExcluirProdutoViewModel excluirVm = new ExcluirProdutoViewModel(id, Produtos.Nome);
        return View(excluirVm);
    }
    public IActionResult Excluir(ExcluirProdutoViewModel excluirVm)
    {
        repositorioProduto.ExcluirRegistro(excluirVm.Id);
        return RedirectToAction(nameof(Index));
    }
}

