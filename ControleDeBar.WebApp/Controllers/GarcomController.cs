using ControleDeBar.Dominio.ModuloGarcom;
using ControleDeBar.Dominio.ModuloMesa;
using ControleDeBar.Infraestrutura.Arquivos.Compartilhado;
using ControleDeBar.Infraestrutura.Arquivos.ModuloGarcon;
using ControleDeBar.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using static ControleDeBar.WebApp.Models.DetalhesMesaViewModel;

namespace ControleDeBar.WebApp.Controllers;

public class GarcomController : Controller
{
    private RepositorioGarcomEmArquivo repositorioGarcon;
    public GarcomController()
    {
        ContextoDados contexto = new ContextoDados(carregarDados: true);
        repositorioGarcon = new RepositorioGarcomEmArquivo(contexto);
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<Garcom> garcons = repositorioGarcon.SelecionarRegistros();
        VisualizarGarcomViewModel visualizarVm = new VisualizarGarcomViewModel(garcons);
        return View(visualizarVm);
    }
    [HttpGet]
    public IActionResult Cadastrar()
    {
        CadastrarGarcomViewModel cadastrarVm = new CadastrarGarcomViewModel();
        return View(cadastrarVm);
    }
    [HttpPost]
    public IActionResult Cadastrar(CadastrarGarcomViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);
        var garcom = new Garcom(cadastrarVm.Nome, cadastrarVm.Cpf);

        repositorioGarcon.CadastrarRegistro(garcom);
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public IActionResult Editar(int id)
    {
        Garcom garcons = repositorioGarcon.SelecionarRegistroPorId(id);
        if (garcons == null)
        {
            return RedirectToAction(nameof(Index));
        }
        EditarGarcomViewModel editarVm = new EditarGarcomViewModel
            (garcons.Id,
            garcons.Nome,
            garcons.Cpf
            );
        return View(editarVm);
    }
    [HttpPost]
    public IActionResult Editar(EditarGarcomViewModel editarVm)
    {
        Garcom garconEditado = new Garcom(editarVm.Nome, editarVm.Cpf);
        bool edicaoConcluida = repositorioGarcon.EditarRegistro(editarVm.Id, garconEditado);
        if (!edicaoConcluida)
        {
            garconEditado.Id = editarVm.Id;
            return View(garconEditado);
        }
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public IActionResult Excluir(int id)
    {
        var garcons = repositorioGarcon.SelecionarRegistroPorId(id);

        if(garcons == null)
            return RedirectToAction(nameof(Index));
        ExcluirGarcomViewModel excluirVm = new ExcluirGarcomViewModel(id, garcons.Nome);
        return View(excluirVm);
    }
    public IActionResult Excluir(ExcluirGarcomViewModel excluirVm)
    {
       repositorioGarcon.ExcluirRegistro(excluirVm.Id);
        return RedirectToAction(nameof(Index));
    }
}













