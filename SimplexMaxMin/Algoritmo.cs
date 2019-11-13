using System;
using System.Collections.Generic;
using System.Text;

namespace SimplexMaxMin
{
    public class Algoritmo
    {
        public Alfabeto Alfabeto = new Alfabeto();
        public int QuantidaDeVariaveis { get; set; }
        public int QuantidaDeRestricoes { get; set; }
        public bool JaFoiCadastradoAFuncaoObjetiva { get; set; }
        public bool JaFoiInseridaAlgumaRestricao { get; set; }

        //public FuncaoObjetivo FuncaoObjetivo { get; set; }
        //public List<Restricao> Restricoes { get; set; }

        public int[,] Problema { get; set; }

        public int TipoDeAlgoritmo { get; set; }

        //public Algoritmo(int tipoAlgoritmo, int tamanho = 0, int qtdRestricoes = 0)
        //{
        //    TipoAlgoritmo = tipoAlgoritmo;
        //    Restricoes = new List<Restricao>(qtdRestricoes);
        //    FuncaoObjetivo = new FuncaoObjetivo(tamanho);
        //    JaFoiInseridaAlgumaRestricao = false;
        //}

        public string RetornarLetra(int posicao)
        {
            return Alfabeto.Letras[posicao];
        }

        public Algoritmo(int tipoAlgoritmo, int qtdRestricoes = 0)
        {
            TipoDeAlgoritmo = tipoAlgoritmo;
            JaFoiCadastradoAFuncaoObjetiva = false;
            JaFoiInseridaAlgumaRestricao = false;
            QuantidaDeRestricoes = qtdRestricoes;
        }

        public void InserirFuncaoObjetiva()
        {
            Console.Clear();
            Console.WriteLine("### Inserir função objetiva ###");
            Console.WriteLine("");
            Console.WriteLine("");

            int count = 1;
            while (count <= QuantidaDeVariaveis)
            {
                Console.Write($"Insira o valor de x{count}: ");
                if (int.TryParse(Console.ReadLine(), out int valor))
                {
                    Problema[QuantidaDeRestricoes + 1, count - 1] = valor;
                    count++;
                }
            }

            JaFoiCadastradoAFuncaoObjetiva = true;

            Console.WriteLine("");
            Console.WriteLine("OK. Tudo certo!");
            Console.WriteLine("Sua função objetivo ficou assim:");
            Console.WriteLine("");
            ImprimirFuncaoObjetivo(true);
            Program.PressioneQualquerTeclaParaContinuar();
            Program.Menu();
        }

        public void ImprimirFuncaoObjetivo(bool EstaNoInicio)
        {
            if (EstaNoInicio)
            {
                if (TipoDeAlgoritmo == TipoAlgoritmo.Maximizacao)
                    Console.Write($"Max Z = ");
                else
                    Console.Write($"Min Z = ");

                for (int i = 1; i <= QuantidaDeVariaveis; i++)
                {
                    
                    Console.Write($"{Problema[QuantidaDeRestricoes + 1, i - 1]}x{i}");

                    if (i < QuantidaDeVariaveis)
                        Console.Write(" + ");
                    else
                        Console.WriteLine("");
                }
            }
            else
            {
                Console.Write($"Z = ");

                for (int i = 1; i <= QuantidaDeVariaveis; i++)
                {
                    Console.Write($"{Problema[QuantidaDeVariaveis + 1, i - 1]}x{i}");

                    if (i <= QuantidaDeVariaveis)
                        Console.Write(" + ");
                    else
                        Console.WriteLine("");
                }

                for (int i = QuantidaDeVariaveis; i <= QuantidaDeRestricoes; i++)
                {
                    Console.Write($"{Problema[QuantidaDeVariaveis + 1, i - 1]}x{i}");

                    if (i < QuantidaDeVariaveis)
                        Console.Write(" + ");
                    else
                        Console.WriteLine("");
                }
            }
        }

        public void InserirRestricao()
        {
            for (int linha = 0; linha < QuantidaDeRestricoes; linha++)
            {

                Console.WriteLine("### Inserir a " + (linha + 1) + "ª restrição ###");
                Console.WriteLine("");
                Console.WriteLine("");

                int count = 1;
                while (count <= QuantidaDeVariaveis)
                {
                    Console.Write($"Insira o valor de x{count}: ");
                    if (int.TryParse(Console.ReadLine(), out int valor))
                    {
                        Problema[linha, count - 1] = valor;
                        count++;
                    }
                    else
                    {
                        Console.WriteLine("Valor inválido...");
                        Console.WriteLine("");
                    }
                }

                //Letra da Determinada restrição
                Problema[linha, count + linha] = 1;

                bool valorCorreto = false;
                while (!valorCorreto)
                {
                    Console.Write("Insira o valor da coluna L: ");
                    if (int.TryParse(Console.ReadLine(), out int valor))
                    {
                        Problema[linha, QuantidaDeVariaveis + QuantidaDeRestricoes] = valor;
                        valorCorreto = true;
                    }
                    else
                    {
                        Console.WriteLine("Valor inválido...");
                        Console.WriteLine("");
                    }
                }

                Console.WriteLine("");
                Console.WriteLine("OK. Tudo certo!");
                Console.WriteLine("Sua " + (linha + 1) + "ª restrição ficou assim:");
                Console.WriteLine("");

                ImprimirRestricao(linha);
                Console.WriteLine("");
                Console.WriteLine("");

            }

            JaFoiInseridaAlgumaRestricao = true;

            Program.PressioneQualquerTeclaParaContinuar();
            Program.Menu();
        }

        public void ImprimirRestricao(int linha, int? forcarLoop = null)
        {
            for (int i = 1; i <= QuantidaDeVariaveis; i++)
            {
                Console.Write($"{Problema[linha, i - 1]}x{i}");

                if (i < QuantidaDeVariaveis)
                    Console.Write(" + ");
            }

            var alfabeto = new Alfabeto();

            //var length = forcarLoop ?? Valores.Length - tamanho - 1;
            var length = forcarLoop;

            //for (int i = 0; i < length; i++)
            //{
            //    int valor = (i + QuantidaDeVariaveis + 1) < Valores.Length ? Valores[i + tamanho] : 0;

            //    Console.Write($"{valor}{alfabeto.Letras[i]}");

            //    if (i < length - 1)
            //        Console.Write(" + ");
            //}

            string simbolo = TipoDeAlgoritmo == TipoAlgoritmo.Maximizacao ? "<" : ">";

            Console.Write($" {simbolo}= {Problema[linha, QuantidaDeVariaveis + QuantidaDeRestricoes]} ");
        }

        public void DefinirQuantidadeDeVariaveis()
        {
            if (PodeDefinirQuantidadeDeVariaveis())
            {
                Console.Clear();

                Console.Write("Quantas variáveis a função objetivo terá? (ex: 2 => x¹ e x²): ");

                if (int.TryParse(Console.ReadLine(), out int qtdVariaveis))
                {
                    Problema = new int[QuantidaDeRestricoes + 1, qtdVariaveis + QuantidaDeRestricoes + 1];
                    QuantidaDeVariaveis = qtdVariaveis;
                }
                else
                {
                    Console.WriteLine("Ops... Tente novamente com um valor válido!");
                    Program.PressioneQualquerTeclaParaContinuar();
                    DefinirQuantidadeDeVariaveis();
                }
            }
            Program.Menu();
        }

        public bool PodeDefinirQuantidadeDeVariaveis()
        {
            if (JaFoiCadastradoAFuncaoObjetiva)
            {
                Console.Clear();
                Console.WriteLine("ATENÇÃO!");
                Console.WriteLine("Definir a quantidade de variáveis após já ter sido definido anteriormente, irá resetar a Função Objetiva e todas as restrições!");
                Console.WriteLine("");
                Console.Write("Deseja continuar?");
                Console.WriteLine("");
                Console.WriteLine("Pressione F1 para Continuar");
                Console.WriteLine("Pressione F2 para Voltar");

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.F1:
                        JaFoiCadastradoAFuncaoObjetiva = false;
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

        public void DefinirQuantidadeDeRestricoes()
        {
            if (PodeDefinirQuantidadeDeRestricoes())
            {
                Console.Clear();

                Console.Write("Quantas restrições o problema terá? ");

                if (int.TryParse(Console.ReadLine(), out int qtdRestricoes))
                {
                    Problema = new int[qtdRestricoes + 1, qtdRestricoes + QuantidaDeVariaveis + 1];
                    QuantidaDeRestricoes = qtdRestricoes;
                }
                else
                {
                    Console.WriteLine("Ops... Tente novamente com um valor válido!");
                    Program.PressioneQualquerTeclaParaContinuar();
                    DefinirQuantidadeDeRestricoes();
                }
            }
            //Program.Menu();
        }

        public bool PodeDefinirQuantidadeDeRestricoes()
        {
            if (JaFoiInseridaAlgumaRestricao)
            {
                Console.Clear();
                Console.WriteLine("ATENÇÃO!");
                Console.WriteLine("Definir a quantidade após já ter sido definida anteriormente, irá resetar a função objetiva e todas as restrições!");
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
