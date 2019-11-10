using System;
using System.Collections.Generic;
using System.Text;

namespace SimplexMaxMin
{
    class ResolverProblema
    {
        private Algoritmo problema;

        public ResolverProblema(Algoritmo algoritmo)
        {
            this.problema = algoritmo;
        }

        private Algoritmo resolverMaximizacao()
        {
            int menorNegativo = 0;
            int posicaoColunaDoMenorNegativo = -1;
            for (int i = 0; i < problema.FuncaoObjetivo.Valores.Length; i++)
            {
                int valor = problema.FuncaoObjetivo.Valores[i];
                if (valor < menorNegativo)
                {
                    menorNegativo = valor;
                    posicaoColunaDoMenorNegativo = i;
                }
            }

            //se a posicao for -1, não achou número negativo e está pronto
            if (posicaoColunaDoMenorNegativo != -1)
            {
                /**
                 *  Identificando a linha que conterá o pivô
                 */
                double menorValorPositivoDoLDivididoPeloElementoNaColunaSelecionada = Double.MaxValue;
                int posicaoRestricaoDoPivo = -1;
                int pivo = -1;
                for (int i = 0; i < problema.Restricoes.Count; i++)
                {
                    int valor = problema.Restricoes[i].Valores[posicaoColunaDoMenorNegativo];
                    int colunaL = problema.Restricoes[i].Valores[problema.Restricoes[i].Valores.Length - 1];
                    double resultado = colunaL / valor;

                    if (resultado > 0 && resultado < menorValorPositivoDoLDivididoPeloElementoNaColunaSelecionada)
                    {
                        menorValorPositivoDoLDivididoPeloElementoNaColunaSelecionada = resultado;
                        posicaoRestricaoDoPivo = i;
                        pivo = valor;
                    }
                }

                for (int i = 0; i < problema.Restricoes[posicaoRestricaoDoPivo].Valores.Length; i++)
                {
                    /*
                     *  dividindo a linha para achar o pivo igual a 1.
                     */
                    int valor = problema.Restricoes[posicaoRestricaoDoPivo].Valores[i];
                    //problema.Restricoes[posicaoRestricaoDoPivo].Valores[i] = (double)(valor / pivo); //deve ser alterado para aceitar double
                }
            }

            return problema;
        }
        private void resolverMinimizacao()
        {

        }
    }
}
