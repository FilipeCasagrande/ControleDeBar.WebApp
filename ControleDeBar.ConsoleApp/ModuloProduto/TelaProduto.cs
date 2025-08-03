using ControleDeBar.ConsoleApp.Compartilhado;
using ControleDeBar.Dominio.ModuloProduto;
using ControleDeBar.Infraestrutura.Memoria.ModuloProduto;

namespace ControleDeBar.ConsoleApp.ModuloProduto;

public class TelaProduto : TelaBase<Produto>, ITela
{
    public TelaProduto(RepositorioProduto repositorio) : base("Produto", repositorio)
    {
    }

    public override void VisualizarRegistros(bool exibirCabecalho)
    {
        if (exibirCabecalho)
            ExibirCabecalho();

        Console.WriteLine("Visualização de Produtos");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -10} | {1, -30} | {2, -30}", "Id", "Nome", "Valor");

        Produto[] produtos = repositorio.SelecionarRegistros();

        for (int i = 0; i < produtos.Length; i++)
        {
            Produto p = produtos[i];

            if (p == null)
                continue;

            Console.WriteLine("{0, -10} | {1, -30} | {2, -30}", p.Id, p.Nome, p.Valor.ToString("C2"));
        }

        ApresentarMensagem("Digite ENTER para continuar...", ConsoleColor.DarkYellow);
    }

    protected override Produto ObterDados()
    {
        string nome = string.Empty;

        while (string.IsNullOrWhiteSpace(nome))
        {
            Console.Write("Digite o nome do produto: ");
            nome = Console.ReadLine()!;

            if (string.IsNullOrWhiteSpace(nome))
            {
                ApresentarMensagem("Digite um nome válido!", ConsoleColor.DarkYellow);
                Console.Clear();
            }
        }

        bool conseguiuConverterValor = false;

        decimal valor = 0.0m;

        while (!conseguiuConverterValor)
        {
            Console.Write("Digite o valor do produto: ");
            conseguiuConverterValor = decimal.TryParse(Console.ReadLine(), out valor);

            if (!conseguiuConverterValor)
            {
                ApresentarMensagem("Digite um valor numérico válido!", ConsoleColor.DarkYellow);
                Console.Clear();
            }
        }

        return new Produto(nome, valor);
    }
}