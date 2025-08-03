using ControleDeBar.ConsoleApp.ModuloConta;
using ControleDeBar.ConsoleApp.ModuloGarcom;
using ControleDeBar.ConsoleApp.ModuloMesa;
using ControleDeBar.ConsoleApp.ModuloProduto;
using ControleDeBar.Dominio.ModuloGarcom;
using ControleDeBar.Dominio.ModuloMesa;
using ControleDeBar.Dominio.ModuloProduto;
using ControleDeBar.Infraestrutura.Memoria.ModuloConta;
using ControleDeBar.Infraestrutura.Memoria.ModuloGarcom;
using ControleDeBar.Infraestrutura.Memoria.ModuloMesa;
using ControleDeBar.Infraestrutura.Memoria.ModuloProduto;

namespace ControleDeBar.ConsoleApp.Compartilhado;

public class TelaPrincipal
{
    private char opcaoEscolhida;

    private RepositorioMesa repositorioMesa;
    private TelaMesa telaMesa;

    private RepositorioGarcom repositorioGarcom;
    private TelaGarcom telaGarcom;

    private RepositorioProduto repositorioProduto;
    private TelaProduto telaProduto;

    private RepositorioConta repositorioConta;
    private TelaConta telaConta;

    public TelaPrincipal()
    {
        repositorioMesa = new RepositorioMesa();
        repositorioGarcom = new RepositorioGarcom();
        repositorioProduto = new RepositorioProduto();
        repositorioConta = new RepositorioConta();

        telaMesa = new TelaMesa(repositorioMesa);
        telaGarcom = new TelaGarcom(repositorioGarcom);
        telaProduto = new TelaProduto(repositorioProduto);

        telaConta = new TelaConta(
            repositorioConta,
            repositorioProduto,
            repositorioMesa,
            repositorioGarcom
        );

        // Dados de teste
        Mesa mesa = new Mesa(1, 3);
        Garcom garcom = new Garcom("Gerson", "012.333.322-23");
        Produto produto = new Produto("Cerveja Brahma 350ml", 6.50m);

        repositorioMesa.CadastrarRegistro(mesa);
        repositorioGarcom.CadastrarRegistro(garcom);
        repositorioProduto.CadastrarRegistro(produto);
    }

    public void ApresentarMenuPrincipal()
    {
        Console.Clear();

        Console.WriteLine("----------------------------------------");
        Console.WriteLine("|           Controle de Bar            |");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        Console.WriteLine("1 - Controle de Mesas");
        Console.WriteLine("2 - Controle de Garçons");
        Console.WriteLine("3 - Controle de Produtos");
        Console.WriteLine("4 - Controle de Contas");
        Console.WriteLine("S - Sair");

        Console.WriteLine();

        Console.Write("Escolha uma das opções: ");
        opcaoEscolhida = Console.ReadLine()![0];
    }

    public ITela ObterTela()
    {
        if (opcaoEscolhida == '1')
            return telaMesa;

        if (opcaoEscolhida == '2')
            return telaGarcom;

        if (opcaoEscolhida == '3')
            return telaProduto;

        if (opcaoEscolhida == '4')
            return telaConta;

        return null;
    }
}
