using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoXadrez
{
    static class Tela
    {
        public static void imprimirPartida(PartidaXadrez partida)
        {
            Console.Clear();
            Tabuleiro tab = partida.tab;
            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write((8-i) + " ");
                for (int j = 0; j < tab.colunas; j++)
                {
                    Peca peca = tab.peca(new Posicao(i, j));
                    if (peca != null)
                    {
                        if(peca.cor == Cor.Preta)
                        {
                            ConsoleColor bgColor = Console.ForegroundColor;
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(peca);
                            Console.ForegroundColor = bgColor;
                        }else
                            Console.Write(peca);                       
                    }
                    else
                        Console.Write("-");
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");

            imprimirPecasCapturadas(partida);

            Console.WriteLine("\nTurno: " + partida.turno);            
            Console.WriteLine("Jogador atual: " + partida.jogadorAtual);

            if (partida.xeque)
                Console.WriteLine("XEQUE!");
        }

        public static void imprimirPecasCapturadas(PartidaXadrez partida)
        {
            Console.WriteLine("\nPeças capturadas: ");
            Console.Write("Brancas: ");
            imprimirConjunto(partida.PecasCapturadas(Cor.Branca));            
            Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            imprimirConjunto(partida.PecasCapturadas(Cor.Preta));
            Console.ForegroundColor = aux;
        }

        public static void imprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (Peca peca in conjunto)
            {
                Console.Write(peca + " ");
            }
            Console.WriteLine("]");
        }

        public static Posicao LerMovimento(string tipo)
        {
            if (tipo == "origem")
                Console.Write("Origem: ");
            else if (tipo == "destino")
                Console.Write("Destino: ");
            else
                throw new Exception("Tipo de movimento desconhecido");
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1].ToString());
            return new PosicaoXadrez(coluna, linha).toPosicao();
        }

        public static void imprimirMovimentosPossiveis(Tabuleiro tab, bool[,] mat)
        {
            Console.Clear();

            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write((8 - i) + " ");
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (mat[i, j])                                            
                        Console.BackgroundColor = ConsoleColor.DarkGray;

                    Peca peca = tab.peca(new Posicao(i, j));
                    if (peca != null)
                        if (peca.cor == Cor.Preta)
                        {
                            ConsoleColor bgColor = Console.ForegroundColor;
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(peca);
                            Console.ForegroundColor = bgColor;
                        }
                        else
                            Console.Write(peca);
                    else
                        Console.Write("-");
                    Console.Write(" ");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.WriteLine();                
            }
            Console.WriteLine("  A B C D E F G H");
        }
    }
}
