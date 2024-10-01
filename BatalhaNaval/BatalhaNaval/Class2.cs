using System;
using System.Windows.Forms;

namespace BatalhaNaval
{
    public partial class SelectionForm : Form
    {
        private int[,] gameBoardP1 = new int[LADO_MATRIZ, LADO_MATRIZ];
        private int[,] gameBoardP2 = new int[LADO_MATRIZ, LADO_MATRIZ];
        private string player1, player2;
        private bool isPlayer1Turn = true; // Alterna entre os jogadores

        public SelectionForm(string p1, string p2)
        {
            InitializeComponent();
            player1 = p1;
            player2 = p2;
            InitializeGameBoards();
        }

        private void InitializeGameBoards()
        {
            InicializarMatriz(gameBoardP1);
            InicializarMatriz(gameBoardP2);
        }

        private void Cell_Click(object sender, EventArgs e)
        {
            // O evento de clique em uma célula deve adicionar um barco
            Button cellButton = sender as Button;
            if (cellButton != null)
            {
                int row = Convert.ToInt32(cellButton.Tag.ToString().Split(',')[0]);
                int col = Convert.ToInt32(cellButton.Tag.ToString().Split(',')[1]);

                if (isPlayer1Turn)
                {
                    if (gameBoardP1[row, col] == 0)
                    {
                        gameBoardP1[row, col] = 1; // Marca a célula como ocupada
                        cellButton.BackgroundImage = Properties.Resources.BoatImage; // Substitua por sua imagem de barco
                        cellButton.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                }
                else
                {
                    if (gameBoardP2[row, col] == 0)
                    {
                        gameBoardP2[row, col] = 1; // Marca a célula como ocupada
                        cellButton.BackgroundImage = Properties.Resources.BoatImage; // Substitua por sua imagem de barco
                        cellButton.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                }
            }
        }

        private void StartGameButton_Click(object sender, EventArgs e)
        {
            // Verifica se ambos os jogadores posicionaram os barcos
            if (BothPlayersReady())
            {
                GameForm gameForm = new GameForm(gameBoardP1, gameBoardP2, player1, player2);
                gameForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Ambos os jogadores devem posicionar seus barcos antes de começar.");
            }
        }

        private bool BothPlayersReady()
        {
            // Lógica para verificar se ambos os jogadores estão prontos
            // Aqui você pode contar quantos barcos foram posicionados
            return true; // Substitua pela lógica real
        }

        private void InitializeComponent()
        {
            // Criação da interface gráfica com botões que representam o tabuleiro.
            // Adicione um botão "Iniciar Jogo" e os botões do tabuleiro de 8x8
        }
    }
}

