using System;
using System.Collections.Generic;
using System.Text;

namespace SimplexMaxMin
{
    public class Algoritmo
    {
        public FuncaoObjetivo FuncaoObjetivo { get; set; }
        public List<Restricao> Restricoes { get; set; }
        public int TipoAlgoritmo { get; set; }

        public Algoritmo(int tipoAlgoritmo, int tamanho = 0)
        {
            TipoAlgoritmo = tipoAlgoritmo;
            Restricoes = new List<Restricao>();
            FuncaoObjetivo = new FuncaoObjetivo(tamanho);
        }

        public void InserirRestricao()
        {
            var tamanho = FuncaoObjetivo.Valores.Length;
            var ordem = Restricoes.Count + 1;

            Restricoes.Add(new Restricao(tamanho, ordem, TipoAlgoritmo));

            Program.PressioneQualquerTeclaParaContinuar();
            Program.Menu();
        }

        public void DefinirTamanho()
        {
            if (FuncaoObjetivo.PodeDefinirTamanho())
            {
                FuncaoObjetivo = new FuncaoObjetivo(0);
                Restricoes.Clear();

                FuncaoObjetivo.DefinirTamanho();
            }
            Program.Menu();
        }
    }
}
