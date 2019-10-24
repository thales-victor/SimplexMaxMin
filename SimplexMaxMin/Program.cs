using System;

namespace SimplexMaxMin
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }

        private static void Menu()
        {
            Console.Clear();

            Console.WriteLine("Pressione F1 para inserir a coluna Z");
            Console.WriteLine("Pressione F2 para inserir as demais colunas");
            Console.WriteLine("Pressione F3 para solucionar o Simplex passo-a-passo");
            Console.WriteLine("Pressione F5 para solucionar o Simplex até o final");

            try
            {
                var cki = Console.ReadKey();

                switch (cki.Key)
                {
                    case ConsoleKey.F1:
                        {
                            InserirColunaZ();
                        }
                        break;
                }
            }
            catch
            {
                Menu();
            }
        }

        private static void InserirColunaZ ()
        {
            Console.Clear();

            Console.WriteLine("Inserindo coluna Z");

            Console.ReadKey();

            Menu();
        }
    }
}
