using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoXadrez
{
    class PartidaXadrez
    {
        public Tabuleiro tab { get; private set; }
        public bool acabouPartida { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public int turno { get; set; }

        public PartidaXadrez()
        {
            tab = new Tabuleiro(8, 8);
            this.jogadorAtual = Cor.Branca;
            turno = 1;
            PosicionarPecas();

            acabouPartida = false;

        }

        public void IniciarJogo()
        {
            Posicao origem, destino;
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            while (!acabouPartida)
            {
                try
                {
                    Tela.imprimirPartida(this);

                    origem = Tela.LerMovimento("origem");
                    ValidarPosicaoOrigem(origem);

                    mat = tab.peca(origem).movimentosPossiveis();

                    Tela.imprimirMovimentosPossiveis(this.tab, mat);


                    destino = Tela.LerMovimento("destino");
                    ValidarPosicaoDestino(origem, destino);

                    RealizaJogada(origem, destino);
                }
                catch(TabuleiroException e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }                
            }
        }

        public void PosicionarPecas()
        {
            /*
             * 8 - - T R T - - -
             * 7 - - T T T - - -
             * 6 - - - - - - - -
             * 5 - - - - - - - -
             * 4 - - - - - - - -
             * 3 - - - - - - - -
             * 2 - - T T T - - -
             * 1 - - T R T - - -
             *   a b c d e f g h
             */
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('c', 1).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('c', 2).toPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Branca), new PosicaoXadrez('d', 1).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('d', 2).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('e', 1).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('e', 2).toPosicao());

            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('c', 8).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('c', 7).toPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Preta), new PosicaoXadrez('d', 8).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('d', 7).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('e', 8).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('e', 7).toPosicao());
        }

        public void RealizarJogada() {
            Console.Write("Origem: ");
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = s[1];

            Peca pecaSelecionada = tab.peca(new PosicaoXadrez(coluna, linha).toPosicao());
            if(pecaSelecionada != null)
                Console.WriteLine(pecaSelecionada);            
        }

        public void AlterarTurno()
        {
            if (jogadorAtual == Cor.Branca)
                jogadorAtual = Cor.Preta;
            else
                jogadorAtual = Cor.Branca;

            turno++;
        }

        public void executaMovimento(Posicao origem, Posicao destino)
        {
            Peca aux, capturada;
            if (tab.existePeca(origem))
            {
                aux = tab.removerPeca(origem);
                if (tab.existePeca(destino))
                {
                    capturada = tab.removerPeca(destino);
                }
                tab.colocarPeca(aux, destino);
            }
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            executaMovimento(origem, destino);
            turno++;
            mudaJogador();                    
        }

        public void mudaJogador()
        {
            if (jogadorAtual == Cor.Branca)
                jogadorAtual = Cor.Preta;
            else
                jogadorAtual = Cor.Branca;
        }

        public void ValidarPosicaoOrigem(Posicao pos)
        {
            if(tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem");
            }

            if (tab.peca(pos).cor != jogadorAtual)
                throw new TabuleiroException("A peça escolhida não é sua!");

            if (!tab.peca(pos).existeMovimentosPossiveis())
                throw new TabuleiroException("Não existe movimentos possiveis para essa peça.");
        }

        public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).podeMoverPara(destino))
                throw new TabuleiroException("Você não pode mover para a posição de destino.");
        }
    }
}
