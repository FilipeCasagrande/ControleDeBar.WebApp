using ControleDeBar.Dominio.ModuloConta;
using ControleDeBar.Dominio.ModuloGarcom;
using ControleDeBar.Dominio.ModuloMesa;
using ControleDeBar.Infraestrutura.Arquivos.Compartilhado;
using ControleDeBar.Infraestrutura.Arquivos.ModuloConta;
using ControleDeBar.Infraestrutura.Arquivos.ModuloGarcon;
using ControleDeBar.Infraestrutura.Arquivos.ModuloMesa;
using ControleDeBar.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace ControleDeBar.WebApp.Controllers;

public class ContaController : Controller
{
    private readonly ContextoDados contexto;
    private readonly RepositorioContaEmArquivo repositorioConta;
    private readonly RepositorioMesaEmArquivo repositorioMesa;
    private readonly RepositorioGarcomEmArquivo repositorioGarcom;
    private readonly RepositorioProdutoEmArquivo repositorioProduto;

    public ContaController()
    {
        ContextoDados contexto = new ContextoDados(true);
        repositorioConta = new RepositorioContaEmArquivo(contexto);
        repositorioMesa = new RepositorioMesaEmArquivo(contexto);
        repositorioGarcom = new RepositorioGarcomEmArquivo(contexto);
        repositorioProduto = new RepositorioProdutoEmArquivo(contexto);
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<Conta> contas = repositorioConta.SelecionarRegistros();
        VisualizarContaViewModel visualizarVm = new VisualizarContaViewModel(contas);
        return View(visualizarVm);
    }

    [HttpGet]
    public IActionResult Abrir()
    {
        List<Mesa> mesas = repositorioMesa.SelecionarRegistros();
        List<Garcom> garcons = repositorioGarcom.SelecionarRegistros();
        AbrirContaViewModel abrirVm = new AbrirContaViewModel(mesas, garcons);
        return View(abrirVm);
    }

    [HttpPost]
    public IActionResult Abrir(AbrirContaViewModel abrirVm)
    { 
        if(!ModelState.IsValid)
        {
            return View(abrirVm);
        }
        Mesa mesaSelecionada = repositorioMesa.SelecionarRegistroPorId(abrirVm.MesaId);
        Garcom garcomSelecionado = repositorioGarcom.SelecionarRegistroPorId(abrirVm.GarcomId);

        Conta contas = new Conta
            (abrirVm.Titular,
            mesaSelecionada,
            garcomSelecionado
            );
        repositorioConta.CadastrarRegistro(contas);
        return RedirectToAction(nameof(Index));
    }
}

