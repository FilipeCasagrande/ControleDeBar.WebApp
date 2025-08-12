using ControleDeBar.Dominio.ModuloConta;
using ControleDeBar.Dominio.ModuloGarcom;
using ControleDeBar.Dominio.ModuloMesa;
using ControleDeBar.Dominio.ModuloProduto;
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
        contexto = new ContextoDados(true);
        repositorioConta = new RepositorioContaEmArquivo(contexto);
        repositorioMesa = new RepositorioMesaEmArquivo(contexto);
        repositorioGarcom = new RepositorioGarcomEmArquivo(contexto);
        repositorioProduto = new RepositorioProdutoEmArquivo(contexto);
    }

    [HttpGet]
    public IActionResult Index(string? status)
    {
        List<Conta> contas;

        switch (status)
        {
            case "abertas": contas = repositorioConta.SelecionarContasEmAberto(); break;
            case "fechadas": contas = repositorioConta.SelecionarContasFechadas(); break;
            default: contas = repositorioConta.SelecionarRegistros(); break;
        }

        VisualizarContaViewModel visualizarContasVm = new VisualizarContaViewModel(contas);

        return View(visualizarContasVm);
    }
    

    [HttpGet]
    public IActionResult Abrir()
    {
        List<Mesa> mesas = repositorioMesa.SelecionarRegistros();
        List<Garcom> garcons = repositorioGarcom.SelecionarRegistros();
        AbrirContaViewModel abrirVm = new AbrirContaViewModel(mesas, garcons);
        return View(abrirVm);
    }
    public IActionResult Fechar(int id)
    {
        Conta contaSelecionada = repositorioConta.SelecionarRegistroPorId(id);

        FecharContaViewModel fecharContaVm = new FecharContaViewModel(
            contaSelecionada.Id,
            contaSelecionada.Titular,
            contaSelecionada.Mesa.Numero,
            contaSelecionada.Garcom.Nome,
            contaSelecionada.CalcularValorTotal(),
            contaSelecionada.Pedidos
        );

        return View(fecharContaVm);
    }

    [HttpPost]
    public IActionResult FecharConfirmado(int id)
    {
        Conta contaSelecionada = repositorioConta.SelecionarRegistroPorId(id);

        contaSelecionada.Fechar();

        contexto.Salvar();

        return RedirectToAction(nameof(Index));
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
    [HttpGet]
    public IActionResult GerenciarPedidos(int id)
    {
        Conta contaSelecionada = repositorioConta.SelecionarRegistroPorId(id);

        List<Produto> produtos = repositorioProduto.SelecionarRegistros();

        GerenciarPedidosViewModel gerenciarPedidosVm = new GerenciarPedidosViewModel(
            contaSelecionada,
            produtos
        );

        return View(gerenciarPedidosVm);
    }

    [HttpPost]
    public IActionResult AdicionarPedido(int id, AdicionarPedidoViewModel adicionarPedidoVm)
    {
        Conta contaSelecionada = repositorioConta.SelecionarRegistroPorId(id);

        Produto produtoSelecionado = repositorioProduto.SelecionarRegistroPorId(adicionarPedidoVm.IdProduto);

        Pedido pedido = contaSelecionada.RegistrarPedido(produtoSelecionado, adicionarPedidoVm.QuantidadeSolicitada);

        contexto.Salvar();

        List<Produto> produtos = repositorioProduto.SelecionarRegistros();

        GerenciarPedidosViewModel gerenciarPedidosVm = new GerenciarPedidosViewModel(
            contaSelecionada,
            produtos
        );

        return View(nameof(GerenciarPedidos), gerenciarPedidosVm);
    }
    [HttpPost]
    public IActionResult RemoverPedido(int id, int idPedido)
    {
        Conta contaSelecionada = repositorioConta.SelecionarRegistroPorId(id);

        contaSelecionada.RemoverPedido(idPedido);

        contexto.Salvar();

        List<Produto> produtos = repositorioProduto.SelecionarRegistros();

        GerenciarPedidosViewModel gerenciarPedidosVm = new GerenciarPedidosViewModel(
            contaSelecionada,
            produtos
        );

        return View(nameof(GerenciarPedidos), gerenciarPedidosVm);
    }

    [HttpGet]
    public IActionResult Detalhes(int id)
    {
        Conta contaSelecionada = repositorioConta.SelecionarRegistroPorId(id);

        DetalhesContaViewModel detalhesContaVm = new DetalhesContaViewModel(
            contaSelecionada.Id,
            contaSelecionada.Titular,
            contaSelecionada.Mesa.Numero,
            contaSelecionada.Garcom.Nome,
            contaSelecionada.EstaAberta,
            contaSelecionada.CalcularValorTotal(),
            contaSelecionada.Pedidos
        );

        return View(detalhesContaVm);
    }
}

