using System;
using System.Windows.Forms;

namespace BatalhaNaval
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string jogador1 = Prompt.ShowDialog("Nome do Jogador 1:", "Batalha Naval");
            string jogador2 = Prompt.ShowDialog("Nome do Jogador 2:", "Batalha Naval");

            Application.Run(new Form1(jogador1, jogador2));
        }
    }
}
