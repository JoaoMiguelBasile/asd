using System;
using System.Drawing;
using System.Windows.Forms;

namespace BatalhaNaval
{
    public partial class Form1 : Form
    {
        const int LADO_MATRIZ = 8;
        PictureBox[,] tabuleiroPictureBoxes;
        int[,] gameBoardDefensor = new int[LADO_MATRIZ, LADO_MATRIZ];
        int[,] gameBoardAtacante = new int[LADO_MATRIZ, LADO_MATRIZ];
        int partesBarcoDefensor = 0;
        const int META_ATACANTE = 3;
        int pontosAtacante = 0;
        string jogadorDefensor;
        string jogadorAtacante;
        int tirosRestantes = 5;
        bool ehVezDoAtacante = true;
        PictureBox lastClicked;
        Label lblStatus;

        public Form1(string jogador1, string jogador2)
        {
            InitializeComponent();
            jogadorDefensor = jogador1;
            jogadorAtacante = jogador2;
            InicializarJogo();
        }

        private void InicializarJogo()
        {
            InicializarMatriz(gameBoardDefensor);
            tabuleiroPictureBoxes = new PictureBox[LADO_MATRIZ, LADO_MATRIZ];
            CriarTabuleiro();
            CriarBotaoAtacar();
            CriarStatusLabel();
            PosicionarBarcos(jogadorDefensor); // Jogador Defensor posiciona barcos
        }

        private void CriarTabuleiro()
        {
            // Criar indicadores de posição na lateral
            for (int i = 0; i < LADO_MATRIZ; i++)
            {
                Label lblLinha = new Label
                {
                    Text = (i + 1).ToString(),
                    Location = new Point(20, 80 + i * 50),
                    Size = new Size(20, 50),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                this.Controls.Add(lblLinha);
            }

            char letraColuna = 'A';
            for (int j = 0; j < LADO_MATRIZ; j++)
            {
                Label lblColuna = new Label
                {
                    Text = letraColuna.ToString(),
                    Location = new Point(50 + j * 50, 50),
                    Size = new Size(50, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                this.Controls.Add(lblColuna);
                letraColuna++;
            }

            // Criar o tabuleiro
            for (int i = 0; i < LADO_MATRIZ; i++)
            {
                for (int j = 0; j < LADO_MATRIZ; j++)
                {
                    PictureBox pictureBox = new PictureBox
                    {
                        Size = new Size(50, 50),
                        Location = new Point(50 + j * 50, 80 + i * 50),
                        BackColor = Color.LightBlue,
                        BorderStyle = BorderStyle.FixedSingle,
                        Tag = $"{i},{j}" // Armazenar a posição
                    };

                    pictureBox.Click += new EventHandler(PictureBox_Click);
                    tabuleiroPictureBoxes[i, j] = pictureBox;
                    this.Controls.Add(pictureBox);
                }
            }
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = sender as PictureBox;

            // Se for a vez do atacante, marcar posição de ataque
            if (ehVezDoAtacante)
            {
                if (lastClicked != null)
                {
                    lastClicked.BorderStyle = BorderStyle.FixedSingle; // Resetar a borda anterior
                }

                lastClicked = clickedPictureBox;
                clickedPictureBox.BorderStyle = BorderStyle.Fixed3D; // Destacar a posição selecionada
            }
            else // Se for a vez do defensor, marcar posição de barco
            {
                if (lastClicked != null)
                {
                    lastClicked.BorderStyle = BorderStyle.FixedSingle; // Resetar a borda anterior
                }

                lastClicked = clickedPictureBox;
                clickedPictureBox.BorderStyle = BorderStyle.Fixed3D; // Destacar a posição selecionada

                // Posicionar barco
                if (gameBoardDefensor[GetPosicao(lastClicked).Item1, GetPosicao(lastClicked).Item2] == 0)
                {
                    gameBoardDefensor[GetPosicao(lastClicked).Item1, GetPosicao(lastClicked).Item2] = 1; // 1 para indicar que tem barco
                    partesBarcoDefensor++;
                }
                else
                {
                    MessageBox.Show("Já existe um barco nessa posição!");
                }

                // Verifica se o defensor posicionou todos os barcos
                if (partesBarcoDefensor == 3)
                {
                    MessageBox.Show($"{jogadorDefensor} posicionou todos os barcos! Agora é a vez de {jogadorAtacante} atacar.");
                    ehVezDoAtacante = true; // Muda a vez para o atacante
                    AtualizarStatus();
                }
            }
        }

        private (int, int) GetPosicao(PictureBox pictureBox)
        {
            string[] coords = pictureBox.Tag.ToString().Split(',');
            return (int.Parse(coords[0]), int.Parse(coords[1]));
        }

        private void CriarBotaoAtacar()
        {
            Button btnAtacar = new Button
            {
                Text = "Atacar",
                Location = new Point(50, 80 + LADO_MATRIZ * 50 + 20),
                Size = new Size(100, 30)
            };
            btnAtacar.Click += new EventHandler(BtnAtacar_Click);
            this.Controls.Add(btnAtacar);
        }

        private void BtnAtacar_Click(object sender, EventArgs e)
        {
            if (lastClicked != null && ehVezDoAtacante)
            {
                string[] coords = lastClicked.Tag.ToString().Split(',');
                int linha = int.Parse(coords[0]);
                int coluna = int.Parse(coords[1]);

                // Implementar a lógica de ataque aqui
                if (gameBoardDefensor[linha, coluna] == 1) // Se acertou um barco
                {
                    MessageBox.Show("Acertou um barco!");
                    lastClicked.BackColor = Color.Red; // Mudar a cor para indicar um acerto
                    pontosAtacante++;
                }
                else
                {
                    MessageBox.Show("Errou!");
                    lastClicked.BackColor = Color.Gray; // Mudar a cor para indicar um erro
                }

                lastClicked.BorderStyle = BorderStyle.FixedSingle; // Retornar ao estilo normal
                lastClicked = null; // Resetar a última posição clicada

                // Verificar se o atacante ganhou
                if (pontosAtacante == META_ATACANTE)
                {
                    MessageBox.Show($"{jogadorAtacante} venceu!");
                    this.Close(); // Fechar o jogo
                }

                // Alterar a vez do jogador
                ehVezDoAtacante = false; // Passa a vez para o defensor
                AtualizarStatus();
            }
            else
            {
                MessageBox.Show("Selecione uma posição válida antes de atacar!");
            }
        }

        private void AtualizarStatus()
        {
            lblStatus.Text = $"É a vez de: {(ehVezDoAtacante ? jogadorAtacante : jogadorDefensor)}";
        }

        private void CriarStatusLabel()
        {
            lblStatus = new Label
            {
                Location = new Point(200, 80 + LADO_MATRIZ * 50 + 20),
                Size = new Size(200, 30),
                Text = $"É a vez de: {jogadorDefensor}",
                Font = new Font("Arial", 12)
            };
            this.Controls.Add(lblStatus);
        }

        private void InicializarMatriz(int[,] matriz)
        {
            for (int i = 0; i < LADO_MATRIZ; i++)
            {
                for (int j = 0; j < LADO_MATRIZ; j++)
                {
                    matriz[i, j] = 0; // Inicializa o tabuleiro com 0 (sem barcos)
                }
            }
        }

        private void PosicionarBarcos(string jogador)
        {
            MessageBox.Show($"{jogador}, você deve posicionar seus barcos clicando nas posições do tabuleiro.");
        }
    }
}
