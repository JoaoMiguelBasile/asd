namespace BatalhaNaval
{
    public class BatalhaNavalGame
    {
        public const int LADO_MATRIZ = 8;
        public const int META_ATACANTE = 3;

        public int[,] gameBoard;
        public int[,] copyBoard;
        public int pontosAtacante = 0;
        public int tirosRestantes = 5;

        public BatalhaNavalGame()
        {
            gameBoard = new int[LADO_MATRIZ, LADO_MATRIZ];
            copyBoard = new int[LADO_MATRIZ, LADO_MATRIZ];
            InicializarMatriz(gameBoard);
            InicializarMatriz(copyBoard);
        }

        public void InicializarMatriz(int[,] matriz)
        {
            for (int i = 0; i < LADO_MATRIZ; i++)
            {
                for (int j = 0; j < LADO_MATRIZ; j++)
                {
                    matriz[i, j] = 0;
                }
            }
        }

        public bool PosicionarBarco(int linha, int coluna)
        {
            if (EhPosicaoValida(linha, coluna) && gameBoard[linha, coluna] == 0)
            {
                gameBoard[linha, coluna] = 1;
                return true;
            }
            return false;
        }

        public bool Atacar(int linha, int coluna)
        {
            if (EhPosicaoValida(linha, coluna))
            {
                if (gameBoard[linha, coluna] == 1)
                {
                    copyBoard[linha, coluna] = 1; // Acertou
                    gameBoard[linha, coluna] = 0; // Remove a parte do barco
                    pontosAtacante++;
                    return true; // Hit
                }
                else
                {
                    tirosRestantes--;
                    return false; // Miss
                }
            }
            return false; // Posição inválida
        }

        public bool EhPosicaoValida(int linha, int coluna)
        {
            return linha >= 0 && linha < LADO_MATRIZ && coluna >= 0 && coluna < LADO_MATRIZ;
        }

        public int GetNumeroColuna(string decisao)
        {
            char[] opcoesColuna = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
            char coluna = char.ToUpper(decisao[0]);

            for (int i = 0; i < LADO_MATRIZ; i++)
            {
                if (opcoesColuna[i] == coluna)
                    return i;
            }
            return -1;
        }

        public int GetNumeroLinha(string decisao)
        {
            char[] opcoesLinha = { '1', '2', '3', '4', '5', '6', '7', '8' };
            char linha = decisao[1];

            for (int i = 0; i < LADO_MATRIZ; i++)
            {
                if (opcoesLinha[i] == linha)
                    return i;
            }
            return -1;
        }
    }
}
