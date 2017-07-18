using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoXadrez
{
    class Rei : Peca
    {
        public Rei(Tabuleiro tab, Cor cor)
            :base('R', tab, cor) { }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tabuleiro.linhas, tabuleiro.colunas];
            Posicao pos = new Posicao(0, 0);

            // NW
            pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                mat[pos.linha, pos.coluna] = true;

            // N
            pos.definirValores(posicao.linha - 1, posicao.coluna);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                mat[pos.linha, pos.coluna] = true;

            // NE
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                mat[pos.linha, pos.coluna] = true;

            // W
            pos.definirValores(posicao.linha, posicao.coluna -1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                mat[pos.linha, pos.coluna] = true;

            // E
            pos.definirValores(posicao.linha, posicao.coluna + 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                mat[pos.linha, pos.coluna] = true;

            // SW
            pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                mat[pos.linha, pos.coluna] = true;

            // S
            pos.definirValores(posicao.linha + 1, posicao.coluna);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                mat[pos.linha, pos.coluna] = true;

            // SE
            pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                mat[pos.linha, pos.coluna] = true;

            return mat;
        }     
    }
}
