using System;
using System.Drawing;
using System.Windows.Forms;

namespace BatalhaNaval
{
    public partial class SelectionForm : Form
    {
        const int LADO_MATRIZ = 8;
        const int QUANTIDADE_NAVIOS = 5;

        private int[,] matrizP1 = new int[LADO_MATRIZ, LADO_MATRIZ];
        private int[,] matrizP2 = new int[LADO_MATRIZ, LADO_MATRIZ];

        private int naviosP1 = 0;
        private int naviosP2 = 0;

        private Button[,] buttons = new Button[LADO_MATRIZ, LADO_MATRIZ];

        private string jogador1;
        private string jogador2;
        private bool isPlayer1Turn = true;

        public SelectionForm(string p1, string p2)
        {
            InitializeComponent();
            jogador1 = p1;
            jogador2 = p2;
            InicializarTabuleiro();
        }

        private void InicializarTabuleiro()
        {
            for (int i = 0; i < LADO_MATRIZ; i++)
            {
                for (int j = 0; j < LADO_MATRIZ; j++)
                {
                    buttons[i, j] = new Button();
                    buttons[i, j].Size = new Size(40, 40);
                    buttons[i, j].Location = new Point(j * 40, i * 40);
                    buttons[i, j].Click += new EventHandler(Botao_Click);
                    buttons[i, j].Tag = new Point(i, j);
                    Controls.Add(buttons[i, j]);
                }
            }

            Button btnPronto = new Button();
            btnPronto.Text = "Pronto";
            btnPronto.Location = new Point(0, LADO_MATRIZ * 40 + 10);
            btnPronto.Click += new EventHandler(BtnPronto_Click);
            Controls.Add(btnPronto);
        }

        private void Botao_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Point posicao = (Point)btn.Tag;

            if (isPlayer1Turn)
            {
                if (naviosP1 < QUANTIDADE_NAVIOS && matrizP1[posicao.X, posicao.Y] == 0)
                {
                    matrizP1[posicao.X, posicao.Y] = 1; // Marca o navio
                    btn.BackColor = Color.Blue; // Cor para representar o navio
                    naviosP1++;
                }
            }
            else
            {
                if (naviosP2 < QUANTIDADE_NAVIOS && matrizP2[posicao.X, posicao.Y] == 0)
                {
                    matrizP2[posicao.X, posicao.Y] = 1; // Marca o navio
                    btn.BackColor = Color.Red; // Cor para representar o navio
                    naviosP2++;
                }
            }

            // Verificar se ambos os jogadores colocaram todos os navios
            if (naviosP1 == QUANTIDADE_NAVIOS && naviosP2 == QUANTIDADE_NAVIOS)
            {
                MessageBox.Show("Ambos os jogadores colocaram todos os navios! Clique em Pronto para começar o jogo.");
            }
        }

        private void BtnPronto_Click(object sender, EventArgs e)
        {
            if (isPlayer1Turn)
            {
                isPlayer1Turn = false; // Troca para o jogador 2
                MessageBox.Show($"{jogador1} está pronto! É a vez do {jogador2}.");
                DesativarBotoes(matrizP1);
            }
            else
            {
                MessageBox.Show($"{jogador2} está pronto!");
                IniciarJogo();
            }
        }

        private void IniciarJogo()
        {
            GameForm gameForm = new GameForm(matrizP1, matrizP2, jogador1, jogador2);
            gameForm.Show();
            this.Hide();
        }

        private void DesativarBotoes(int[,] matriz)
        {
            foreach (var btn in buttons)
            {
                btn.Enabled = false; // Desativar todos os botões
            }
            // Reativar os botões do jogador que não está pronto
            if (isPlayer1Turn)
            {
                foreach (var btn in buttons)
                {
                    Point pos = (Point)btn.Tag;
                    if (matriz[pos.X, pos.Y] == 0) // Somente os que não possuem navios
                    {
                        btn.Enabled = true;
                    }
                }
            }
        }
    }
}
