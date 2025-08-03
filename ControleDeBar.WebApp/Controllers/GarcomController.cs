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
    








}



