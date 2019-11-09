using System;
using System.Linq;

namespace SimplexMaxMin
{
    class Program
    {
        static void Main(string[] args)
        {
            Init();
        }

        private static Algoritmo Algoritmo;

        #region ------------ PUBLIC METHODS ------------
        public static void Menu()
        {
            Console.Clear();

            Console.WriteLine("Pressione F1 para alterar o tamanho da função objetivo");
            Console.WriteLine("Pressione F2 para inserir a função objetivo");
            Console.WriteLine("Pressione F3 para inserir as restrições");
            Console.WriteLine("Pressione F4 para solucionar o Simplex passo-a-passo");
            Console.WriteLine("Pressione F5 para solucionar o Simplex até o final");

            if (Algoritmo.FuncaoObjetivo.JaFoiCadastrado)
            {
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("Funcao Objetivo:");
                Algoritmo.FuncaoObjetivo.ImprimirFuncaoObjetivo();
            }

            if (Algoritmo.Restricoes.Any())
            {
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("Restrições:");

                foreach (var restricao in Algoritmo.Restricoes)
                {
                    restricao.ImprimirRestricao(Algoritmo.FuncaoObjetivo.Valores.Length, Algoritmo.TipoAlgoritmo, Algoritmo.Restricoes.Count);
                    Console.WriteLine("");
                }
            }

            try
            {
                SelecionarOpcaoMenu(Console.ReadKey().Key);
            }
            catch
            {
                Console.WriteLine("");
                Console.WriteLine("Ocorreu algum erro!");
                PressioneQualquerTeclaParaContinuar();
                Menu();
            }
        }

        public static void PressioneQualquerTeclaParaContinuar()
        {
            Console.WriteLine(Environment.NewLine);
            Console.Write("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        #endregion ------------ PUBLIC METHODS ------------

        #region ------------ PRIVATE METHODS ------------
        private static void Init()
        {
            Console.Clear();

            Console.WriteLine("Para começar, você deve selecionar o tipo de Simplex que será usado");
            Console.WriteLine("");
            Console.WriteLine("Pressione F1 para Maximização");
            Console.WriteLine("Pressione F2 para Minimização");

            SelecionarTipoSimplex(Console.ReadKey().Key);
            Console.WriteLine("");
            Console.WriteLine("Agora, você deve determinar quantas variáveis a função objetivo terá");
            PressioneQualquerTeclaParaContinuar();
            SelecionarOpcaoMenu(ConsoleKey.F1);
        }

        private static void SelecionarOpcaoMenu(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.F1:
                    {
                        Algoritmo.DefinirTamanho();
                    }
                    break;
                case ConsoleKey.F2:
                    {
                        Algoritmo.FuncaoObjetivo.Inserir();
                    }
                    break;
                case ConsoleKey.F3:
                    {
                        Algoritmo.InserirRestricao();
                    }
                    break;

                default:
                    {
                        Menu();
                    }
                    break;
            }
        }

        private static void SelecionarTipoSimplex(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.F1:
                    {
                        Algoritmo = new Algoritmo(TipoAlgoritmo.Maximizacao);
                    }
                    break;
                case ConsoleKey.F2:
                    {
                        Algoritmo = new Algoritmo(TipoAlgoritmo.Minimizacao);
                    }
                    break;

                default:
                    {
                        throw new Exception();
                    }
            }
        }

        #endregion ------------ PRIVATE METHODS ------------
    }
}
