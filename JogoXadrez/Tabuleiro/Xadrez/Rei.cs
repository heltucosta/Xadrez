using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoXadrez
{
    class Rei : Peca
    {
        private PartidaXadrez partida;
        public Rei(Tabuleiro tab, Cor cor, PartidaXadrez partida)
            :base('R', tab, cor) {
            this.partida = partida;
        }

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

            // #jogadaespecial ROQUE
            if(qteMovimentos == 0)
            {
                if (!partida.xeque)
                {
                    // #jogadaespecial ROQUE PEQUENO
                    Posicao posTorre = new Posicao(posicao.linha, posicao.coluna + 3);
                    if (tabuleiro.existePeca(posTorre) && tabuleiro.peca(posTorre) is Torre && tabuleiro.peca(posTorre).qteMovimentos == 0)
                    {
                        Posicao p1 = new Posicao(posicao.linha, posicao.coluna + 1);
                        Posicao p2 = new Posicao(posicao.linha, posicao.coluna + 2);
                        if (!tabuleiro.existePeca(p1) && !tabuleiro.existePeca(p2)){
                            mat[posicao.linha, posicao.coluna + 2] = true; ;
                        }
                    }

                    // #jogadaespecial ROQUE GRANDE
                    posTorre = new Posicao(posicao.linha, posicao.coluna - 4);
                    if (tabuleiro.existePeca(posTorre) && tabuleiro.peca(posTorre) is Torre && tabuleiro.peca(posTorre).qteMovimentos == 0)
                    {
                        Posicao p1 = new Posicao(posicao.linha, posicao.coluna - 1);
                        Posicao p2 = new Posicao(posicao.linha, posicao.coluna - 2);
                        Posicao p3 = new Posicao(posicao.linha, posicao.coluna - 3);
                        if (!tabuleiro.existePeca(p1) && !tabuleiro.existePeca(p2) && !tabuleiro.existePeca(p3))
                        {
                            mat[posicao.linha, posicao.coluna - 2] = true; ;
                        }
                    }
                }                
            }

            return mat;
        }     
    }
}
