using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoXadrez
{
    class Tabuleiro
    {
        public int linhas { get; private set; }
        public int colunas { get; private set; }
        public Peca[,] pecas { get; private set; }

        public Tabuleiro(int linhas, int colunas)
        {
            this.pecas = new Peca[linhas, colunas];
            this.linhas = linhas;
            this.colunas = colunas;
        }

        public Peca peca(Posicao pos)
        {
            return this.pecas[pos.linha, pos.coluna];
        }

        public void colocarPeca(Peca peca, Posicao posicao)
        {
            ValidarPosicao(posicao);
            this.pecas[posicao.linha, posicao.coluna] = peca;
            this.pecas[posicao.linha, posicao.coluna].posicao = posicao;
        }

        public Peca removerPeca(Posicao destino)
        {
            ValidarPosicao(destino);
            if (this.peca(destino) == null)
                return null;        
            Peca aux = pecas[destino.linha, destino.coluna];
            pecas[destino.linha, destino.coluna] = null;
            return aux;            
        }

        public bool posicaoValida(Posicao pos)
        {
            if (pos.linha < 0 || pos.linha >= this.linhas || pos.coluna < 0 || pos.coluna >= this.colunas)
                return false;
            return true;
        }

        public void ValidarPosicao(Posicao pos)
        {
            if (!posicaoValida(pos))
                throw new TabuleiroException("Posição inválida!");
        }

        public bool existePeca(Posicao pos)
        {
            ValidarPosicao(pos);
            if (peca(pos) != null)
                return true;
            return false;
        }
    }
}
