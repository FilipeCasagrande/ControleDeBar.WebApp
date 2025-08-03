using ControleDeBar.ConsoleApp.Compartilhado;
using ControleDeBar.Dominio.ModuloGarcom;
using ControleDeBar.Infraestrutura.Memoria.ModuloGarcom;

namespace ControleDeBar.ConsoleApp.ModuloGarcom;
public class TelaGarcom : TelaBase<Garcom>, ITela
{
    public TelaGarcom(RepositorioGarcom repositorio) : base("Garçom", repositorio)
    {
    }

    public override void VisualizarRegistros(bool exibirCabecalho)
    {
        if (exibirCabecalho)
            ExibirCabecalho();

        Console.WriteLine("Visualização de Garçons");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -10} | {1, -30} | {2, -30}", "Id", "Nome", "CPF");

        Garcom[] garcons = repositorio.SelecionarRegistros();

        for (int i = 0; i < garcons.Length; i++)
        {
            Garcom g = garcons[i];

            if (g == null)
                continue;

            Console.WriteLine("{0, -10} | {1, -30} | {2, -30}", g.Id, g.Nome, g.Cpf);
        }

        ApresentarMensagem("Digite ENTER para continuar...", ConsoleColor.DarkYellow);
    }

    protected override Garcom ObterDados()
    {
        string nome = string.Empty;

        while (string.IsNullOrWhiteSpace(nome))
        {
            Console.Write("Digite o nome do garçom: ");
            nome = Console.ReadLine()!;

            if (string.IsNullOrWhiteSpace(nome))
            {
                ApresentarMensagem("Digite um nome válido!", ConsoleColor.DarkYellow);
                Console.Clear();
            }
        }

        string cpf = string.Empty;

        while (string.IsNullOrWhiteSpace(cpf))
        {
            Console.Write("Digite o CPF do garçom: ");
            cpf = Console.ReadLine()!;

            if (string.IsNullOrWhiteSpace(cpf))
            {
                ApresentarMensagem("Digite um CPF válido!", ConsoleColor.DarkYellow);
                Console.Clear();
            }
        }

        return new Garcom(nome, cpf);
    }
}
