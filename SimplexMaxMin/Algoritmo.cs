using System;
using System.Collections.Generic;
using System.Text;

namespace SimplexMaxMin
{
    public class Algoritmo
    {
        public bool JaFoiInseridaAlgumaRestricao { get; set; }
        public FuncaoObjetivo FuncaoObjetivo { get; set; }
        public List<Restricao> Restricoes { get; set; }
        public int TipoAlgoritmo { get; set; }

        public Algoritmo(int tipoAlgoritmo, int tamanho = 0, int qtdRestricoes = 0)
        {
            TipoAlgoritmo = tipoAlgoritmo;
            Restricoes = new List<Restricao>(qtdRestricoes);
            FuncaoObjetivo = new FuncaoObjetivo(tamanho);
            JaFoiInseridaAlgumaRestricao = false;
        }

        public void InserirRestricao()
        {
            var tamanho = FuncaoObjetivo.Valores.Length;

            for (int posicao = 0; posicao < Restricoes.Capacity; posicao++)
            {
                Restricoes.Add(new Restricao(tamanho, TipoAlgoritmo, Restricoes.Capacity, posicao));
            }

            JaFoiInseridaAlgumaRestricao = true;

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
            //Program.Menu();
        }

        public void DefinirQuantidadeRestricoes()
        {
            if (PodeDefinirQuantidade())
            {
                Console.Clear();

                Console.Write("Quantas restrições o problema terá? ");

                if (int.TryParse(Console.ReadLine(), out int tamanho))
                {
                    Restricoes = new List<Restricao>(tamanho);
                }
                else
                {
                    Console.WriteLine("Ops... Tente novamente com um valor válido!");
                    Program.PressioneQualquerTeclaParaContinuar();
                    DefinirQuantidadeRestricoes();
                }
            }
            Program.Menu();
        }

        public bool PodeDefinirQuantidade()
        {
            if (JaFoiInseridaAlgumaRestricao)
            {
                Console.Clear();
                Console.WriteLine("ATENÇÃO!");
                Console.WriteLine("Definir a quantidade após já ter sido definida anteriormente, irá resetar todas as restrições.");
                Console.WriteLine("");
                Console.Write("Deseja continuar?");
                Console.WriteLine("");
                Console.WriteLine("Pressione F1 para Continuar");
                Console.WriteLine("Pressione F2 para Voltar");

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.F1:
                        JaFoiInseridaAlgumaRestricao = false;
                        return true;
                    case ConsoleKey.F2:
                        return false;
                    default:
                        {
                            Console.WriteLine("Opção Inválida!");
                            Program.PressioneQualquerTeclaParaContinuar();
                            Program.Menu();
                        }
                        break;
                }
            }
            return true;
        }
    }
}
