using ControleDeBar.Dominio.ModuloMesa;
using ControleDeBar.Infraestrutura.Arquivos.Compartilhado;
using ControleDeBar.Infraestrutura.Arquivos.ModuloMesa;
using ControleDeBar.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using static ControleDeBar.WebApp.Models.DetalhesMesaViewModel;

namespace ControleDeBar.WebApp.Controllers;

public class MesaController : Controller
{
    private RepositorioMesaEmArquivo repositorioMesa;

    public MesaController()
    {
        ContextoDados contexto = new ContextoDados(carregarDados: true);

        repositorioMesa = new RepositorioMesaEmArquivo(contexto);
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<Mesa> mesas = repositorioMesa.SelecionarRegistros();

        VisualizarMesasViewModel visualizarVm = new VisualizarMesasViewModel(mesas);

        return View(visualizarVm);
    }

    [HttpGet]
    public IActionResult Cadastrar()
    {
        CadastrarMesaViewModel cadastrarVm = new CadastrarMesaViewModel();

        return View(cadastrarVm);
    }

    [HttpPost]
    public IActionResult Cadastrar(CadastrarMesaViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        var entidade = new Mesa(cadastrarVm.Numero, cadastrarVm.Capacidade);

        repositorioMesa.CadastrarRegistro(entidade);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Editar(int id)
    {
        Mesa mesa = repositorioMesa.SelecionarRegistroPorId(id);

        if (mesa == null)
        {
            return RedirectToAction(nameof(Index));
        }

        EditarMesaViewModel editarVm = new EditarMesaViewModel
        {
            Id = mesa.Id,
            Numero = mesa.Numero,
            Capacidade = mesa.Capacidade
        };

        return View(editarVm);
    }
    [HttpPost]
    public IActionResult Editar(EditarMesaViewModel editarVm)
    {
        Mesa mesaEditada = new Mesa(editarVm.Numero, editarVm.Capacidade);
        bool edicaoConcluida = repositorioMesa.EditarRegistro(editarVm.Id, mesaEditada);
        if (!edicaoConcluida)
        {
            mesaEditada.Id = editarVm.Id;
            return View(mesaEditada);
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Excluir(int id)
    {
        var mesa = repositorioMesa.SelecionarRegistroPorId(id);

        if (mesa == null) 
        {
            return RedirectToAction(nameof(Index));
        }

        ExcluirMesaViewModel excluirVm = new ExcluirMesaViewModel(id, mesa.Numero);
       
        return View(excluirVm);
    }
    [HttpPost]
    public IActionResult Excluir(ExcluirMesaViewModel excluirVm)
    {
        repositorioMesa.ExcluirRegistro(excluirVm.Id);
        return View(nameof(Index));
    }
}
