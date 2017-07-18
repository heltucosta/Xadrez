using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoXadrez
{
    class Peao : Peca
    {
        public Peao(Tabuleiro tab, Cor cor) : base('P', tab, cor) { }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tabuleiro.linhas, tabuleiro.colunas];
            Posicao pos = new Posicao(0, 0);

            if(cor == Cor.Branca)
            {
                // checa possibilidade de movimentação dupla
                if (qteMovimentos == 0)
                {
                    pos.definirValores(posicao.linha - 2, posicao.coluna);
                    if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                        mat[pos.linha, pos.coluna] = true;
                }

                // checa possibilidade de andar na diagonal para
                pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
                if (tabuleiro.posicaoValida(pos) && tabuleiro.existePeca(pos) && tabuleiro.peca(pos).cor != cor)
                    mat[pos.linha, pos.coluna] = true;

                pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
                if (tabuleiro.posicaoValida(pos) && tabuleiro.existePeca(pos) && tabuleiro.peca(pos).cor != cor)
                    mat[pos.linha, pos.coluna] = true;

                // checa movimentacao padrão do peão
                pos.definirValores(posicao.linha - 1, posicao.coluna);
                if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                    mat[pos.linha, pos.coluna] = true;
            }
            else
            {
                // checa possibilidade de movimentação dupla
                if (qteMovimentos == 0)
                {
                    pos.definirValores(posicao.linha + 2, posicao.coluna);
                    if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                        mat[pos.linha, pos.coluna] = true;
                }

                // checa possibilidade de andar na diagonal para
                pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
                if (tabuleiro.posicaoValida(pos) && tabuleiro.existePeca(pos) && tabuleiro.peca(pos).cor != cor)
                    mat[pos.linha, pos.coluna] = true;

                pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
                if (tabuleiro.posicaoValida(pos) && tabuleiro.existePeca(pos) && tabuleiro.peca(pos).cor != cor)
                    mat[pos.linha, pos.coluna] = true;

                // checa movimentacao padrão do peão
                pos.definirValores(posicao.linha + 1, posicao.coluna);
                if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                    mat[pos.linha, pos.coluna] = true;
            }


            return mat;
        }
    }
}
