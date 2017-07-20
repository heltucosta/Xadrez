using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoXadrez
{
    class Peao : Peca
    {
        private PartidaXadrez partida;
        public Peao(Tabuleiro tab, Cor cor, PartidaXadrez partida) : base('P', tab, cor) {
            this.partida = partida;
        }

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

                // #jogadaespecial ENPASSANT
                if(posicao.linha == 3)
                {
                    Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
                    if(tabuleiro.posicaoValida(esquerda) && tabuleiro.existePeca(esquerda) && tabuleiro.peca(esquerda).cor != cor && tabuleiro.peca(esquerda) == partida.EnPassantVulneravel)
                    {
                        mat[esquerda.linha - 1, esquerda.coluna] = true;
                    }

                    Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
                    if (tabuleiro.posicaoValida(direita) && tabuleiro.existePeca(direita) && tabuleiro.peca(direita).cor != cor && tabuleiro.peca(direita) == partida.EnPassantVulneravel)
                    {
                        mat[direita.linha - 1 , direita.coluna] = true;
                    }
                }
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

                // #jogadaespecial ENPASSANT
                if (posicao.linha == 4)
                {
                    Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
                    if (tabuleiro.posicaoValida(esquerda) && tabuleiro.existePeca(esquerda) && tabuleiro.peca(esquerda).cor != cor && tabuleiro.peca(esquerda) == partida.EnPassantVulneravel)
                    {
                        mat[esquerda.linha + 1, esquerda.coluna] = true;
                    }

                    Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
                    if (tabuleiro.posicaoValida(direita) && tabuleiro.existePeca(direita) && tabuleiro.peca(direita).cor != cor && tabuleiro.peca(direita) == partida.EnPassantVulneravel)
                    {
                        mat[direita.linha + 1, direita.coluna] = true;
                    }
                }
            }

            return mat;
        }
    }
}
