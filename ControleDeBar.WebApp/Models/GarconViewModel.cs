using System.ComponentModel.DataAnnotations;
using ControleDeBar.Dominio.ModuloGarcom;
using Microsoft.AspNetCore.Components.Forms;

namespace ControleDeBar.WebApp.Models;

public class CadastrarGarcomViewModel
{
    [MinLength(2, ErrorMessage = "O Campo \"Nome\" de conter mais de dois caracteres.")]
    public string Nome { get; set; }

    [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "O campo \"CPF\" deve seguir o formato XXX.XXX.XXX-XX.")]
    public string Cpf { get; set; }

    public CadastrarGarcomViewModel()
    {
    }
}
public class VisualizarGarcomViewModel
{
    public List<DetalhesGarcomViewModel> Registros { get; set; } = new List<DetalhesGarcomViewModel>();
    public VisualizarGarcomViewModel(List<Garcom> Garcoms)
    {
        foreach (Garcom g in Garcoms)
        {
            if (g == null)
                continue;
            DetalhesGarcomViewModel detalhesVm = new DetalhesGarcomViewModel(
                g.Id,
                g.Nome,
                g.Cpf
            );
            Registros.Add(detalhesVm);
        }
    }
}
public class DetalhesGarcomViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }

    public DetalhesGarcomViewModel(int id, string nome, string cpf)
    {
        Id = id;
        Nome = nome;
        Cpf = cpf;
    }
}
public class EditarGarcomViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
    [MinLength(2, ErrorMessage = "O Campo \"Nome\" de conter mais de dois caracteres.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo \"CPF\" é obrigatório.")]
    [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "O campo \"CPF\" deve seguir o formato XXX.XXX.XXX-XX.")]
    public string Cpf { get; set; }
    
    
    public EditarGarcomViewModel()
    {
    }
    public EditarGarcomViewModel(int id, string nome, string cpf)
    {
        Id = id;
        Nome = nome;
        Cpf = cpf;
    }
}
public class ExcluirGarcomViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public ExcluirGarcomViewModel()
    {}
    public ExcluirGarcomViewModel(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}




