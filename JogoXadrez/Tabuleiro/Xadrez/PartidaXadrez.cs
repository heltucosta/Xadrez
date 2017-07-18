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
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool xeque;

        public PartidaXadrez()
        {
            xeque = false;
            tab = new Tabuleiro(8, 8);
            this.jogadorAtual = Cor.Branca;
            turno = 1;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
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

            if (acabouPartida)
            {
                Console.WriteLine("XEQUE-MATE");
                Console.WriteLine(jogadorAtual + " venceu o jogo!");
                Console.ReadLine();
            }
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        public void PosicionarPecas()
        {
            /*
             * 8 - - T R T - - -
             * 7 - - B T T - - -
             * 6 - - - - - - - -
             * 5 - - - - - - - -
             * 4 - - - - - - - -
             * 3 - - - - - - - -
             * 2 - - B T T - - -
             * 1 - - T R T - - -
             *   a b c d e f g h
             */
            colocarNovaPeca('c', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('c', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));
            colocarNovaPeca('d', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 2, new Rainha(tab, Cor.Branca));

            colocarNovaPeca('c', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('c', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Rei(tab, Cor.Preta));
            colocarNovaPeca('d', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 7, new Rainha(tab, Cor.Preta));
        }

        public void AlterarTurno()
        {
            if (jogadorAtual == Cor.Branca)
                jogadorAtual = Cor.Preta;
            else
                jogadorAtual = Cor.Branca;

            turno++;
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();

            foreach (Peca peca in capturadas)
            {
                if (peca.cor == cor)
                    aux.Add(peca);
            }

            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca peca in pecas)
            {
                if (peca.cor == cor)
                    aux.Add(peca);
            }

            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        public Peca executaMovimento(Posicao origem, Posicao destino)
        {
            Peca aux = null, capturada = null;
            if (tab.existePeca(origem))
            {
                aux = tab.removerPeca(origem);
                if (tab.existePeca(destino))
                {
                    capturada = tab.removerPeca(destino);
                    capturadas.Add(capturada);
                }
                tab.colocarPeca(aux, destino);
            }
            aux.incrementarMovimentos();
            return capturada;
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca peca)
        {
            Peca p = tab.removerPeca(destino);
            p.decrementarMovimentos();
            if(peca != null)
            {
                tab.colocarPeca(peca, destino);
                capturadas.Remove(peca);
            }
            tab.colocarPeca(p, origem);
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = executaMovimento(origem, destino);
            if (EstaEmXeque(jogadorAtual)){
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            if (EstaEmXeque(adversario(jogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }

            if (testeXequeMate(adversario(jogadorAtual)))
                acabouPartida = true;
            else
            {
                turno++;
                mudaJogador();
            }                     
        }

        public bool testeXequeMate(Cor cor)
        {
            Peca Rei = rei(cor);

            if (!EstaEmXeque(cor))
                return false;

            foreach (Peca peca in pecasEmJogo(cor))
            {
                bool[,] mat = peca.movimentosPossiveis();
                for (int i = 0; i < tab.linhas; i++)
                {
                    for (int j = 0; j < tab.colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = peca.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pCapturada = executaMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            desfazMovimento(origem, new Posicao(i, j), pCapturada);

                            if (!testeXeque)
                                return false;
                        }
                    }
                }
            }
            return true;
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

        private Cor adversario(Cor cor)
        {
            if (cor == Cor.Preta)
                return Cor.Branca;
            return Cor.Preta;
        }

        private Peca rei(Cor cor)
        {
            foreach (Peca peca in pecasEmJogo(cor))
            {
                if(peca is Rei)
                {
                    return peca;
                }
            }
            return null;
        }

        private bool EstaEmXeque(Cor cor)
        {
            Peca Rei = rei(cor);
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            if (Rei == null)
                throw new TabuleiroException("Não existe rei da cor " + cor);
            
            foreach (Peca peca in pecasEmJogo(adversario(cor)))
            {
                mat = peca.movimentosPossiveis();
                if (mat[Rei.posicao.linha, Rei.posicao.coluna])
                    return true;
            }

            return false;
        }
    }
}
