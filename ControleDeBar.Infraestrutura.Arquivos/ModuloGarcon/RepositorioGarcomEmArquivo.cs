using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleDeBar.Dominio.ModuloGarcom;
using ControleDeBar.Infraestrutura.Arquivos.Compartilhado;

namespace ControleDeBar.Infraestrutura.Arquivos.ModuloGarcon;

public class RepositorioGarcomEmArquivo : RepositorioBaseEmArquivo<Garcom>
{
    public RepositorioGarcomEmArquivo(ContextoDados contexto) : base(contexto)
    {
    }


    protected override List<Garcom> ObterRegistros()
    {
        return contextoDados.Garcons;
    }







}
