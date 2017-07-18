using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoXadrez
{
    abstract class Peca
    {
        public char Nome { get; private set; }
        public Tabuleiro tabuleiro {get; private set; }
        public Posicao posicao { get; set; }
        public Cor cor { get; private set; }

        public Peca(char nome, Tabuleiro tab, Cor cor)
        {
            this.Nome = nome;
            this.posicao = null;
            this.tabuleiro = tab;
            this.cor = cor;
        }

        public override string ToString()
        {
            return this.Nome.ToString();
        }

        public bool podeMover(Posicao destino)
        {            
            if (tabuleiro.peca(destino) == null || tabuleiro.peca(destino).cor != cor)
                return true;
            return false;
        }

        public abstract bool[,] movimentosPossiveis();
    }
}
