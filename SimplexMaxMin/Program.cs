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

            Console.WriteLine("Pressione F1 para alterar o tamanho da função objetiva");
            Console.WriteLine("Pressione F2 para inserir a função objetiva");
            Console.WriteLine("Pressione F3 para alterar a quantidade das restrições");
            Console.WriteLine("Pressione F4 para inserir as restrições");
            Console.WriteLine("Pressione F5 para solucionar o Simplex passo-a-passo");
            Console.WriteLine("Pressione F6 para solucionar o Simplex até o final");

            if (Algoritmo.JaFoiCadastradoAFuncaoObjetiva)
            {
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("Função Objetiva:");
                Algoritmo.ImprimirFuncaoObjetiva();
            }

            //se tiver pelo menos uma restrição (no caso, verifico se já tem a primeira)
            if (Algoritmo.Problema[0, 0] != 0)
            {
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("Restrições:");
                Console.WriteLine("");
                Console.WriteLine("");

                for (int i = 0; i < Algoritmo.Problema.GetLength(0); i++)
                {
                    for (int j = 0; j < Algoritmo.Problema.GetLength(1); j++)
                    {
                        if (Algoritmo.Problema[i, j] != 0)
                            Algoritmo.ImprimirRestricao(i);
                    }
                }

            }

            try
            {
                SelecionarOpcaoMenu(Console.ReadKey().Key);
            }
            catch
            {
                Console.WriteLine("");
                Console.WriteLine("Ocorreu algum erro! Tente novamente!");
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

            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("Agora, você deve determinar quantas restrições o problema terá");
            PressioneQualquerTeclaParaContinuar();
            SelecionarOpcaoMenu(ConsoleKey.F3);

            Console.WriteLine("");
            Console.WriteLine("Agora, você deve determinar qual será o tamanho da função objetiva");
            PressioneQualquerTeclaParaContinuar();
            SelecionarOpcaoMenu(ConsoleKey.F1);
        }

        private static void SelecionarOpcaoMenu(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.F1:
                    {
                        Algoritmo.DefinirTamanhoFuncaoObjetiva();
                    }
                    break;
                case ConsoleKey.F2:
                    {
                        Algoritmo.InserirFuncaoObjetiva();
                    }
                    break;
                case ConsoleKey.F3:
                    {
                        Algoritmo.DefinirQuantidadeDeRestricoes();
                    }
                    break;
                case ConsoleKey.F4:
                    {
                        Algoritmo.InserirRestricao();
                    }
                    break;
                case ConsoleKey.F6:
                    {
                        ResolverProblema resolverProblema = new ResolverProblema();
                        //resolverProblema.Resolver(Algoritmo);
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
