using System;
using System.Collections.Generic;
using System.Text;

namespace SimplexMaxMin
{
    public class Restricao
    {
        public double[] Valores { get; set; }

        public Restricao(int tamanho, int tipoAlgoritmo, int quantidade, int posicao)
        {
            Valores = new double[tamanho + quantidade + 1];

            //Console.Clear();

            Console.WriteLine("### Inserir a " + (posicao + 1) + "ª restrição ###");
            Console.WriteLine("");
            Console.WriteLine("");

            int count = 1;
            while (count <= tamanho)
            {
                Console.Write($"Insira o valor de x{count}: ");
                if (int.TryParse(Console.ReadLine(), out int valor))
                {
                    Valores[count - 1] = valor;
                    count++;
                }
                else
                {
                    Console.WriteLine("Valor inválido...");
                    Console.WriteLine("");
                }
            }

            //Letra da Determinada restrição
            Valores[tamanho + posicao] = 1;

            //Letras de outras restrições
            //for (int j = tamanho + posicao + 1; j < Valores.Length - 1; j++)
            //{
            //    Valores[j] = 0;
            //}

            bool valorCorreto = false;
            while (!valorCorreto)
            {
                Console.Write("Insira o valor da coluna L: ");
                if (int.TryParse(Console.ReadLine(), out int valor))
                {
                    Valores[Valores.Length - 1] = valor;
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
            Console.WriteLine("Sua " + (posicao + 1) + "ª restrição ficou assim:");
            Console.WriteLine("");

            ImprimirRestricao(tamanho, tipoAlgoritmo);
            Console.WriteLine("");
            Console.WriteLine("");

        }

        public void ImprimirRestricao(int tamanho, int tipoAlgoritmo, int? forcarLoop = null)
        {
            for (int i = 1; i <= tamanho; i++)
            {
                Console.Write($"{Valores[i - 1]}x{i}");

                if (i < tamanho)
                    Console.Write(" + ");
            }

            var alfabeto = new Alfabeto();

            //var length = forcarLoop ?? Valores.Length - tamanho - 1;
            var length = forcarLoop;

            for (int i = 0; i < length; i++)
            {
                int valor = (i + tamanho + 1) < Valores.Length ? (int)Valores[i + tamanho] : 0;

                Console.Write($"{valor}{alfabeto.Letras[i]}");

                if (i < length - 1)
                    Console.Write(" + ");
            }

            string simbolo = tipoAlgoritmo == TipoAlgoritmo.Maximizacao ? "<" : ">";

            Console.Write($" {simbolo}= {Valores[Valores.Length - 1]} ");
        }
    }
}
