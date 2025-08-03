using ControleDeBar.Dominio.ModuloGarcom;
using ControleDeBar.Infraestrutura.Arquivos.Compartilhado;
using ControleDeBar.Infraestrutura.Arquivos.ModuloGarcon;
using ControleDeBar.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

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
        if(!ModelState.IsValid)
            return View(cadastrarVm);
        var garcom = new Garcom(cadastrarVm.Nome, cadastrarVm.Cpf);

        repositorioGarcon.CadastrarRegistro(garcom);
        return RedirectToAction(nameof(Index));
    }









}



