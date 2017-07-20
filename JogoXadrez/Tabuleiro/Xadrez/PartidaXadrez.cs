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
        public Peca EnPassantVulneravel = null;

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
             * 8 T C B Q R B C T
             * 7 P P P P P P P P
             * 6 - - - - - - - -
             * 5 - - - - - - - -
             * 4 - - - - - - - -
             * 3 - - - - - - - -
             * 2 P P P P P P P P
             * 1 T C B Q R B C T
             *   a b c d e f g h
             */

            colocarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('c', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Rainha(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(tab, Cor.Branca, this));
            colocarNovaPeca('f', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('a', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('b', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('c', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('d', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('e', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('f', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('g', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('h', 2, new Peao(tab, Cor.Branca, this));


            colocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Rainha(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Rei(tab, Cor.Preta, this));
            colocarNovaPeca('f', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('a', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('b', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('c', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('d', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('e', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('f', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('g', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('h', 7, new Peao(tab, Cor.Preta, this));

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

            // #jogadaespecial ROQUE PEQUENO
            if(tab.peca(destino) is Rei && origem.coluna == destino.coluna - 2)
            {
                Posicao posTorre = new Posicao(origem.linha, origem.coluna + 3);
                Peca T = tab.removerPeca(posTorre);
                T.incrementarMovimentos();
                tab.colocarPeca(T, new Posicao(origem.linha, origem.coluna + 1));
            }

            // #jogadaespecial ROQUE GRANDE
            if (tab.peca(destino) is Rei && origem.coluna == destino.coluna + 2)
            {
                Posicao posTorre = new Posicao(origem.linha, origem.coluna - 4);
                Peca T = tab.removerPeca(posTorre);
                T.incrementarMovimentos();
                tab.colocarPeca(T, new Posicao(origem.linha, origem.coluna - 1));
            }

            // #jogadaespecial ENPASSANT
            if (aux is Peao)
            {
                if (origem.coluna != destino.coluna && capturada == null)
                {
                    Posicao posP;
                    if (aux.cor == Cor.Branca)
                    {
                        posP = new Posicao(destino.linha + 1, destino.coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.linha - 1, destino.coluna);
                    }
                    capturada = tab.removerPeca(posP);
                    capturadas.Add(capturada);
                }
            }

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

            // #jogadaespecial ROQUE PEQUENO
            if (tab.peca(origem) is Rei && origem.coluna == destino.coluna - 2)
            {
                Posicao posTorre = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tab.removerPeca(posTorre);
                T.decrementarMovimentos();
                tab.colocarPeca(T, new Posicao(origem.linha, origem.coluna + 3));
            }

            // #jogadaespecial ROQUE GRANDE
            if (tab.peca(origem) is Rei && origem.coluna == destino.coluna + 2)
            {
                Posicao posTorre = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tab.removerPeca(posTorre);
                T.decrementarMovimentos();
                tab.colocarPeca(T, new Posicao(origem.linha, origem.coluna - 4));
            }            

            // #jogadaespecial ENPASSANT
            if(p is Peao)
            {
                if(origem.coluna != destino.coluna && peca == EnPassantVulneravel)
                {
                    Peca peao = tab.removerPeca(destino);
                    Posicao posP;
                    if(p.cor == Cor.Branca)
                    {
                        posP = new Posicao(3, destino.coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.coluna); 
                    }
                    tab.colocarPeca(peao, posP);
                }
            }
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = executaMovimento(origem, destino);
            if (EstaEmXeque(jogadorAtual)){
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            Peca p = tab.peca(destino);

            // #jogada especial promoção
            if (p is Peao)
            {
                Peca promovido;
                if((p.cor == Cor.Branca && destino.linha == 0) || (p.cor == Cor.Preta && destino.linha == 7))
                {
                    p = tab.removerPeca(destino);
                    pecas.Remove(p);
                    Console.WriteLine("Digite para qual tipo de peça deseja promover o seu peão: (R)ainha, (B)ispo, (C)avalo, (T)orre. (Padrão será rainha)");
                    string tipo = Console.ReadLine();
                    switch (tipo[0])
                    {
                        case 'C':
                            promovido = new Cavalo(tab, p.cor);
                            break;
                        case 'B':
                            promovido = new Bispo(tab, p.cor);
                            break;
                        case 'T':
                            promovido = new Torre(tab, p.cor);
                            break;
                        default:
                            promovido = new Rainha(tab, p.cor);
                            break;

                    }                    
                    tab.colocarPeca(promovido, destino);
                    pecas.Add(promovido);
                }
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
            
            // # jogadaespecial ENPASSANT
            if(p is Peao && (destino.linha == origem.linha - 2 || destino.linha == origem.linha + 2))
            {
                EnPassantVulneravel = p;
            }
            else
            {
                EnPassantVulneravel = null;
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
