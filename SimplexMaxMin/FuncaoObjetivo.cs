using System;
using System.Collections.Generic;
using System.Text;

namespace SimplexMaxMin
{
    public class FuncaoObjetivo
    {
        public int[] Valores { get; set; }
        public int quantidadeVariaveis { get; set; }
        public bool JaFoiCadastrado { get; set; }

        public FuncaoObjetivo(int tamanho)
        {
            Valores = new int[tamanho];
            JaFoiCadastrado = false;
        }


        public void Inserir()
        {
            Console.Clear();
            Console.WriteLine("### Inserir função objetivo ###");
            Console.WriteLine("");
            Console.WriteLine("");

            int count = 1;
            while (count <= quantidadeVariaveis)
            {
                Console.Write($"Insira o valor de x{count}: ");
                if (int.TryParse(Console.ReadLine(), out int valor))
                {
                    Valores[count - 1] = valor;
                    count++;
                }
            }

            JaFoiCadastrado = true;

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
            Console.Write($"Z = ");

            if (EstaNoInicio)
            {
                for (int i = 1; i <= quantidadeVariaveis; i++)
                {
                    Console.Write($"{Valores[i - 1]}x{i}");

                    if (i < quantidadeVariaveis)
                        Console.Write(" + ");
                    else
                        Console.WriteLine("");
                }
            }
            else
            {
                for (int i = 1; i <= Valores.Length; i++)
                {
                    Console.Write($"{Valores[i - 1]}x{i}");

                    if (i < Valores.Length)
                        Console.Write(" + ");
                    else
                        Console.WriteLine("");
                }
            }
        }

        public bool PodeDefinirTamanho()
        {
            if (JaFoiCadastrado)
            {
                Console.Clear();
                Console.WriteLine("ATENÇÃO!");
                Console.WriteLine("Definir o tamanho após já ter sido definido anteriormente, irá resetar a Função Objetivo e todas as restrições.");
                Console.WriteLine("");
                Console.Write("Deseja continuar?");
                Console.WriteLine("");
                Console.WriteLine("Pressione F1 para Continuar");
                Console.WriteLine("Pressione F2 para Voltar");

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.F1:
                        JaFoiCadastrado = false;
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

        public void DefinirTamanho(int qtdAlfabeto)
        {
            Console.Clear();

            Console.Write("Quantas variáveis a função objetivo terá? (ex: 2 => x¹ e x²): ");

            if (int.TryParse(Console.ReadLine(), out int tamanho))
            {
                Valores = new int[tamanho + qtdAlfabeto + 1];
                quantidadeVariaveis = tamanho;
            }
            else
            {
                Console.WriteLine("Ops... Tente novamente com um valor válido!");
                Program.PressioneQualquerTeclaParaContinuar();
                DefinirTamanho(qtdAlfabeto);
            }

        }

    }
}
