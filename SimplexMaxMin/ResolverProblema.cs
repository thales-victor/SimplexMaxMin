using System;
using System.Collections.Generic;
using System.Text;

namespace SimplexMaxMin
{
    class ResolverProblema
    {
        private Algoritmo problema;

        public Algoritmo Resolver(Algoritmo algoritmo)
        {
            if (algoritmo.TipoDeAlgoritmo == TipoAlgoritmo.Maximizacao)
                return ResolverMaximizacao(algoritmo);
            else
                return ResolverMinimizacao(algoritmo);
        }

        private Algoritmo ResolverMaximizacao(Algoritmo algoritmo)
        {
            this.problema = algoritmo;
            /**
             * Identificando na linha Z o elemento negativo de menor valor
             */
            int menorNegativo = 0;
            int posicaoColunaDoMenorNegativoEmZ = -1;
            for (int i = 0; i < problema.FuncaoObjetivo.Valores.Length; i++)
            {
                int valor = problema.FuncaoObjetivo.Valores[i];
                if (valor < menorNegativo)
                {
                    menorNegativo = valor;
                    posicaoColunaDoMenorNegativoEmZ = i;
                }
            }

            //se a posicao for -1, não achou número negativo e está pronto
            if (posicaoColunaDoMenorNegativoEmZ != -1)
            {
                /**
                 *  Identificando a linha que conterá o pivô, dividindo os elementos do termo independente (coluna L) pelos
                 *  elementos na mesma linha e na coluna que teve o menor valor negativo em Z
                 */
                double menorValorPositivoDaColunaLDivididoPeloElementoNaColunaSelecionada = Double.MaxValue;
                int linhaQueContemOPivo = -1;
                double pivo = -1;
                for (int i = 0; i < problema.Restricoes.Count; i++)
                {
                    double valor = problema.Restricoes[i].Valores[posicaoColunaDoMenorNegativoEmZ];
                    double colunaL = problema.Restricoes[i].Valores[problema.Restricoes[i].Valores.Length - 1];
                    double resultado = colunaL / valor;

                    if (resultado > 0 && resultado < menorValorPositivoDaColunaLDivididoPeloElementoNaColunaSelecionada)
                    {
                        menorValorPositivoDaColunaLDivididoPeloElementoNaColunaSelecionada = resultado;
                        linhaQueContemOPivo = i;
                        pivo = valor;
                    }
                }

                /*
                 *  dividindo a linha para achar quem contem o pivo para que ele fique igual a 1.
                 */
                for (int i = 0; i < problema.Restricoes[linhaQueContemOPivo].Valores.Length; i++)
                {
                    problema.Restricoes[linhaQueContemOPivo].Valores[i] /= pivo;
                }

                /**
                 * Fazendo o Gauss para as outras linhas
                 */
                for (int i = 0; i < problema.Restricoes.Count; i++)
                {
                    /*
                     * Aplicando Gauss Nas linhas
                     */
                    if (i != linhaQueContemOPivo)
                    {
                        //valor que será utilizado para zerar o elemento da mesma coluna do pivô
                        double aux = problema.Restricoes[i].Valores[posicaoColunaDoMenorNegativoEmZ] * -1;
                        for (int j = 0; j < problema.Restricoes[i].Valores.Length; j++)
                        {
                            double elementoDaLinhaDoPivo = problema.Restricoes[linhaQueContemOPivo].Valores[j];
                            problema.Restricoes[i].Valores[j] += aux * elementoDaLinhaDoPivo;
                        }
                    }
                }
            }

            return problema;
        }
        private Algoritmo ResolverMinimizacao(Algoritmo algoritmo)
        {
            this.problema = algoritmo;
            return problema;
        }
    }
}
