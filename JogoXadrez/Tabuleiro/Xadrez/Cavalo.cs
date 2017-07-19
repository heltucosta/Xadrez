using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoXadrez
{
    class Cavalo : Peca
    {
        public Cavalo(Tabuleiro tab, Cor cor) : base('C', tab, cor) { }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tabuleiro.linhas, tabuleiro.colunas];
            Posicao pos = new Posicao(0, 0);

            /* - - - - - - - -
             * - - 1 - 8 - - -
             * - 2 - - - 7 - -
             * - - - C - - - -
             * - 3 - - - 6 - -
             * - - 4 - 5 - - -
             * - - - - - - - -
             * - - - - - - - - 
             */

            // 1
            pos.definirValores(posicao.linha - 2, posicao.coluna - 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                mat[pos.linha, pos.coluna] = true;

            // 2
            pos.definirValores(posicao.linha - 1, posicao.coluna - 2);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                mat[pos.linha, pos.coluna] = true;

            // 3
            pos.definirValores(posicao.linha + 1, posicao.coluna - 2);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                mat[pos.linha, pos.coluna] = true;

            // 4
            pos.definirValores(posicao.linha + 2, posicao.coluna - 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                mat[pos.linha, pos.coluna] = true;

            // 5
            pos.definirValores(posicao.linha + 2, posicao.coluna + 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                mat[pos.linha, pos.coluna] = true;

            // 6
            pos.definirValores(posicao.linha + 1, posicao.coluna + 2);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                mat[pos.linha, pos.coluna] = true;

            // 7
            pos.definirValores(posicao.linha - 1, posicao.coluna + 2);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                mat[pos.linha, pos.coluna] = true;

            // 8
            pos.definirValores(posicao.linha - 2, posicao.coluna + 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                mat[pos.linha, pos.coluna] = true;

            return mat;
        }
    }
}
