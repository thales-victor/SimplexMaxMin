using System;
using System.Collections.Generic;
using System.Text;

namespace SimplexMaxMin
{
    public class Algoritmo
    {
        public Alfabeto Alfabeto = new Alfabeto();
        public int TamanhoFuncaoObjetiva { get; set; }
        public int QuantidaDeRestricoes { get; set; }
        public bool JaFoiCadastradoAFuncaoObjetiva { get; set; }
        public bool JaFoiInseridaAlgumaRestricao { get; set; }

        //public FuncaoObjetivo FuncaoObjetivo { get; set; }
        //public List<Restricao> Restricoes { get; set; }

        public double[,] Problema { get; set; }
        public string[,] LetrasNaBase { get; set; }

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
            while (count <= TamanhoFuncaoObjetiva)
            {
                Console.Write($"Insira o valor de x{count}: ");
                if (int.TryParse(Console.ReadLine(), out int valor))
                {
                    if (TipoDeAlgoritmo == TipoAlgoritmo.Maximizacao)
                        Problema[QuantidaDeRestricoes, count - 1] = valor * -1;
                    else
                        Problema[QuantidaDeRestricoes, count - 1] = valor;

                    count++;
                }
            }

            JaFoiCadastradoAFuncaoObjetiva = true;

            Console.WriteLine("");
            Console.WriteLine("OK. Tudo certo!");
            Console.WriteLine("Sua função objetivo ficou assim:");
            Console.WriteLine("");
            ImprimirFuncaoObjetiva();
            Program.PressioneQualquerTeclaParaContinuar();
            Program.Menu();
        }

        public void ImprimirFuncaoObjetiva()
        {
            if (TipoDeAlgoritmo == TipoAlgoritmo.Maximizacao)
                Console.Write($"Max Z = ");
            else
                Console.Write($"Min Z = ");

            for (int i = 1; i <= TamanhoFuncaoObjetiva; i++)
            {
                //qtd de Restricoes + 1 pega a última linha, que é a linha Z
                if (TipoDeAlgoritmo == TipoAlgoritmo.Maximizacao)
                    Console.Write($"{Problema[QuantidaDeRestricoes, i - 1] * -1}x{i}");
                else
                    Console.Write($"{Problema[QuantidaDeRestricoes, i - 1]}x{i}");

                if (i < TamanhoFuncaoObjetiva)
                    Console.Write(" + ");
                else
                    Console.WriteLine("");
            }
        }

        public void InserirRestricao()
        {
            // salvando o array de letras que será utilizado para resolver o problema
            for (int i = 0; i < QuantidaDeRestricoes; i++)
            {
                LetrasNaBase[i, 0] = Alfabeto.Letras[i];
            }

            Console.Clear();
            for (int linha = 0; linha < QuantidaDeRestricoes; linha++)
            {
                Console.WriteLine("### Inserir a " + (linha + 1) + "ª restrição ###");
                Console.WriteLine("");
                Console.WriteLine("");

                int count = 1;
                while (count <= TamanhoFuncaoObjetiva)
                {
                    Console.Write($"Insira o valor de x{count}: ");
                    if (int.TryParse(Console.ReadLine(), out int valor))
                    {
                        if (TipoDeAlgoritmo == TipoAlgoritmo.Maximizacao)
                            Problema[linha, count - 1] = valor;
                        else
                            Problema[linha, count - 1] = valor * -1;

                        count++;
                    }
                    else
                    {
                        Console.WriteLine("Valor inválido...");
                        Console.WriteLine("");
                    }
                }

                //Letra da Determinada restrição
                Problema[linha, (count - 1) + linha] = 1;

                bool valorCorreto = false;
                while (!valorCorreto)
                {
                    Console.Write("Insira o valor da coluna L: ");
                    if (int.TryParse(Console.ReadLine(), out int valor))
                    {
                        if (TipoDeAlgoritmo == TipoAlgoritmo.Maximizacao)
                            Problema[linha, TamanhoFuncaoObjetiva + QuantidaDeRestricoes] = valor;
                        else
                            Problema[linha, TamanhoFuncaoObjetiva + QuantidaDeRestricoes] = valor * -1;

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
                Console.WriteLine("");
                Console.WriteLine("");

            }

            JaFoiInseridaAlgumaRestricao = true;

            Program.PressioneQualquerTeclaParaContinuar();
            Program.Menu();
        }

        public void ImprimirRestricao(int linha, int? forcarLoop = null)
        {
            for (int i = 1; i <= TamanhoFuncaoObjetiva; i++)
            {
                if (TipoDeAlgoritmo == TipoAlgoritmo.Maximizacao)
                    Console.Write($"{Problema[linha, i - 1]}x{i}");
                else
                    Console.Write($"{Problema[linha, i - 1] * -1}x{i}");

                if (i < TamanhoFuncaoObjetiva)
                    Console.Write(" + ");
            }

            string simbolo = TipoDeAlgoritmo == TipoAlgoritmo.Maximizacao ? "<" : ">";


            if (TipoDeAlgoritmo == TipoAlgoritmo.Maximizacao)
                Console.Write($" {simbolo}= {Problema[linha, TamanhoFuncaoObjetiva + QuantidaDeRestricoes]} ");
            else
                Console.Write($" {simbolo}= {Problema[linha, TamanhoFuncaoObjetiva + QuantidaDeRestricoes] * -1} ");
        }

        public void DefinirTamanhoFuncaoObjetiva()
        {
            if (PodeDefinirTamanhoFuncaoObjetiva())
            {
                Console.Clear();

                Console.Write("Quantas variáveis a função objetiva terá? (ex: 2 => x¹ e x²): ");

                if (int.TryParse(Console.ReadLine(), out int qtdVariaveis))
                {
                    Problema = new double[QuantidaDeRestricoes + 1, qtdVariaveis + QuantidaDeRestricoes + 1];
                    TamanhoFuncaoObjetiva = qtdVariaveis;
                }
                else
                {
                    Console.WriteLine("Ops... Tente novamente com um valor válido!");
                    Program.PressioneQualquerTeclaParaContinuar();
                    DefinirTamanhoFuncaoObjetiva();
                }
            }
            Program.Menu();
        }

        public bool PodeDefinirTamanhoFuncaoObjetiva()
        {
            if (JaFoiCadastradoAFuncaoObjetiva)
            {
                Console.Clear();
                Console.WriteLine("ATENÇÃO!");
                Console.WriteLine("Definir o tamanho da função objetiva após a mesma já ter sido inserida, irá resetar a Função Objetiva e todas as Restrições!");
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
                    Problema = new double[qtdRestricoes + 1, qtdRestricoes + TamanhoFuncaoObjetiva + 1];
                    LetrasNaBase = new string[qtdRestricoes, 1];
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

        public void ResolverProblema()
        {
            if (TipoDeAlgoritmo == TipoAlgoritmo.Maximizacao)
                ResolverComMaximizacao();
            else
                ResolverComMinimizacao();
        }

        private void ResolverComMaximizacao()
        {
            double menorNegativo = 0;
            int posicaoColunaDoMenorNegativoEmZ = -1;

            //procurando o menor valor negativo em Z
            for (int i = 0; i < Problema.GetLength(1); i++)
            {
                double valor = Problema[QuantidaDeRestricoes, i];
                if (valor < menorNegativo)
                {
                    menorNegativo = valor;
                    posicaoColunaDoMenorNegativoEmZ = i;
                }
            }

            //se a posicao for -1, não achou número negativo e está pronto
            if (posicaoColunaDoMenorNegativoEmZ == -1)
            {
                ImprimirSolucao();
                return;
            }

            //  Identificando a linha que conterá o pivô, dividindo os elementos do termo independente (coluna L) pelos
            //  elementos na mesma linha e na coluna que teve o menor valor negativo em Z
            double menorValorPositivoDaColunaLimiteDivididoPeloElementoNaColunaSelecionada = Double.MaxValue;
            int linhaQueContemOPivo = -1;
            double pivo = -1;
            for (int i = 0; i < QuantidaDeRestricoes; i++)
            {
                double valor = Problema[i, posicaoColunaDoMenorNegativoEmZ];
                double colunaL = Problema[i, TamanhoFuncaoObjetiva + QuantidaDeRestricoes];
                double resultado = colunaL / valor;

                if (resultado > 0 && resultado < menorValorPositivoDaColunaLimiteDivididoPeloElementoNaColunaSelecionada)
                {
                    menorValorPositivoDaColunaLimiteDivididoPeloElementoNaColunaSelecionada = resultado;
                    linhaQueContemOPivo = i;
                    pivo = valor;
                }
            }

            ImprimirTabela();

            //armazenando a letra ou variavel que está entrando na base
            if (posicaoColunaDoMenorNegativoEmZ - TamanhoFuncaoObjetiva >= 0)
                LetrasNaBase[linhaQueContemOPivo, 0] = Alfabeto.Letras[posicaoColunaDoMenorNegativoEmZ - TamanhoFuncaoObjetiva];
            else
                LetrasNaBase[linhaQueContemOPivo, 0] = $"x{posicaoColunaDoMenorNegativoEmZ + 1}";

            // dividindo a linha que contém o pivo, para que ele fique igual a 1.
            for (int i = 0; i < Problema.GetLength(1); i++)
            {
                Problema[linhaQueContemOPivo, i] /= pivo;
            }

            // Aplicando o Gauss para as outras linhas
            for (int i = 0; i <= QuantidaDeRestricoes; i++)
            {
                if (i != linhaQueContemOPivo)
                {
                    //valor que será utilizado para zerar o elemento da mesma coluna do pivô
                    double aux = Problema[i, posicaoColunaDoMenorNegativoEmZ] * -1;
                    for (int j = 0; j < Problema.GetLength(1); j++)
                    {
                        double elementoDaLinhaDoPivo = Problema[linhaQueContemOPivo, j];
                        Problema[i, j] += aux * elementoDaLinhaDoPivo;
                    }
                }
            }

            ResolverComMaximizacao();
        }
        private void ResolverComMinimizacao()
        {
            double menorNegativo = 0;
            int linhaMenorNegativoEmL = -1;

            //procurando o menor valor negativo na coluna L
            for (int i = 0; i < Problema.GetLength(0) - 1; i++)
            {
                double valor = Problema[i, QuantidaDeRestricoes + TamanhoFuncaoObjetiva];
                if (valor < menorNegativo)
                {
                    menorNegativo = valor;
                    linhaMenorNegativoEmL = i;
                }
            }

            //se a posicao for -1, não achou número negativo e está pronto
            if (linhaMenorNegativoEmL == -1)
            {
                ImprimirSolucao();
                return;
            }

            //  Identificando a linha que conterá o pivô, dividindo os elementos em Z pelos elementos do termo independente (coluna L)
            //  na coluna que teve o menor valor negativo
            double menorValorEmZDivididoPeloElementoNaLinhaSelecionada = Double.MaxValue;
            int colunaQueContemOPivo = -1;
            double pivo = -1;
            for (int i = 0; i < QuantidaDeRestricoes; i++)
            {
                double valor = Problema[linhaMenorNegativoEmL, i];
                double elementoZ = Problema[QuantidaDeRestricoes, i];
                double resultado = Math.Abs(elementoZ / valor);

                if (resultado < menorValorEmZDivididoPeloElementoNaLinhaSelecionada)
                {
                    menorValorEmZDivididoPeloElementoNaLinhaSelecionada = resultado;
                    colunaQueContemOPivo = i;
                    pivo = valor;
                }
            }

            ImprimirTabela();

            //armazenando a letra ou variavel que está entrando na base
            if (colunaQueContemOPivo - TamanhoFuncaoObjetiva >= 0)
                LetrasNaBase[linhaMenorNegativoEmL, 0] = Alfabeto.Letras[colunaQueContemOPivo - TamanhoFuncaoObjetiva];
            else
                LetrasNaBase[linhaMenorNegativoEmL, 0] = $"x{colunaQueContemOPivo + 1}";

            // dividindo a linha que contém o pivo, para que ele fique igual a 1.
            for (int i = 0; i < Problema.GetLength(1); i++)
            {
                double aux = Problema[linhaMenorNegativoEmL, i];
                Problema[linhaMenorNegativoEmL, i] = Math.Round(aux / pivo,3);
            }

            // Aplicando o Gauss para as outras linhas
            for (int i = 0; i <= QuantidaDeRestricoes; i++)
            {
                if (i != linhaMenorNegativoEmL)
                {
                    //valor que será utilizado para zerar o elemento da mesma coluna do pivô
                    double aux = Problema[i, colunaQueContemOPivo] * -1;
                    for (int j = 0; j < Problema.GetLength(1); j++)
                    {
                        double elementoDaLinhaDoPivo = Problema[linhaMenorNegativoEmL, j];

                        Problema[i, j] += aux * elementoDaLinhaDoPivo;
                    }
                }
            }

            ResolverComMinimizacao();
        }

        private void ImprimirSolucao()
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");

            int linhas = Problema.GetLength(0);
            int colunas = Problema.GetLength(1);

            ColocarEspacamento("");

            for (int i = 1; i <= TamanhoFuncaoObjetiva; i++)
            {
                var caracter = $"x{i}";
                Console.Write(caracter);
                ColocarEspacamento(caracter);
            }

            for (int i = 0; i < LetrasNaBase.GetLength(0); i++)
            {
                var caracter = LetrasNaBase[i, 0];
                Console.Write(caracter);
                ColocarEspacamento(caracter);
            }
            Console.Write("L");
            Console.WriteLine("");

            for (int linha = 0; linha < linhas; linha++)
            {
                var letra = linha + 1 > TamanhoFuncaoObjetiva ? "Z" : $"{LetrasNaBase[linha, 0]}";
                Console.Write(letra);
                ColocarEspacamento(letra);

                for (int coluna = 0; coluna < colunas; coluna++)
                {
                    var caracter = Problema[linha, coluna];
                    Console.Write(caracter);
                    ColocarEspacamento(caracter);
                }
                Console.WriteLine("");
            }

            Console.WriteLine($"\n\n### VARIÁVEIS BÁSICAS:");

            for (int i = 0; i < QuantidaDeRestricoes; i++)
            {
                Console.WriteLine($"\t\t{LetrasNaBase[i, 0]} = {Problema[i, QuantidaDeRestricoes + TamanhoFuncaoObjetiva]}");
            }

            Console.WriteLine($"\n### SOLUÇÃO ÓTIMA: Z = {Problema[QuantidaDeRestricoes, QuantidaDeRestricoes + TamanhoFuncaoObjetiva]}");
        }


        private void ImprimirTabela()
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");

            int linhas = Problema.GetLength(0);
            int colunas = Problema.GetLength(1);

            ColocarEspacamento("");

            for (int i = 1; i <= TamanhoFuncaoObjetiva; i++)
            {
                var caracter = $"x{i}";
                Console.Write(caracter);
                ColocarEspacamento(caracter);
            }

            for (int i = 0; i < LetrasNaBase.GetLength(0); i++)
            {
                var caracter = LetrasNaBase[i, 0];
                Console.Write(caracter);
                ColocarEspacamento(caracter);
            }
            Console.Write("L");
            Console.WriteLine("");

            for (int linha = 0; linha < linhas; linha++)
            {
                var letra = linha + 1 > TamanhoFuncaoObjetiva ? "Z" : $"{LetrasNaBase[linha, 0]}";
                Console.Write(letra);
                ColocarEspacamento(letra);

                for (int coluna = 0; coluna < colunas; coluna++)
                {
                    var caracter = Problema[linha, coluna];
                    Console.Write(caracter);
                    ColocarEspacamento(caracter);
                }
                Console.WriteLine("");
            }


            Console.WriteLine("\n\n\t### PRÓXIMA ITERAÇÃO ###");
        }

        private void ColocarEspacamento(double caracter)
        {
            ColocarEspacamento(caracter.ToString());
        }

        private void ColocarEspacamento(string caracter)
        {
            int espacamento = 8;
            int tamanhoDoCaracter = caracter.Length;

            for (int espaco = 0; espaco < espacamento - tamanhoDoCaracter; espaco++)
            {
                Console.Write(" ");
            }
        }
    }
}
