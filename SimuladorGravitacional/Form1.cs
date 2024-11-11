using System;
using System.Drawing;
using System.Windows.Forms;

namespace SimuladorGravitacional
{
    public partial class Form1 : Form
    {
        private Universo universo;
        private System.Windows.Forms.Timer timer;
        private const double deltaTime = 0.016; // Intervalo de tempo para a simulação
        private int iteracoes;
        private double fatorDeTamanho = 0.01; // Ajuste conforme necessário 

        public Form1()
        {
            InitializeComponent();
            universo = new Universo();
            timer = new System.Windows.Forms.Timer();
            timer.Interval = (int)(deltaTime * 1000); // Convertendo para milissegundos
            timer.Tick += timer_Tick;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            // Atualiza a simulação, passando as dimensões da tela
            universo.Atualizar(deltaTime, this.ClientSize.Width, this.ClientSize.Height);

            // Redesenha a tela
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DesenharCorpos(e.Graphics);
        }

        private void DesenharCorpos(Graphics g)
        {
            foreach (Corpo corpo in universo.corpos)
            {
                double tamanho = corpo.Massa * fatorDeTamanho; // Tamanho da elipse

                // Calcular as coordenadas de desenho
                float x = (float)(corpo.PosX - tamanho / 2);
                float y = (float)(corpo.PosY - tamanho / 2);

                // Desenhar a elipse
                g.FillEllipse(Brushes.Black, x, y, (float)tamanho, (float)tamanho);
            }
        }

        private void Iniciar_bt_Click(object sender, EventArgs e)
        {
            // Limpa o universo e reinicia as iterações
            universo = new Universo();
            iteracoes = 0;
            Iteracoes_Box.Text = "0"; // Reseta o texto de iterações

            // Lê a quantidade de corpos do TextBox
            if (int.TryParse(Corpos_Box.Text, out int quantidade) && quantidade > 0)
            {
                Random random = new Random();
                for (int i = 0; i < quantidade; i++)
                {
                    // Gerando a massa (valores entre 10 e 1000)
                    double massa = random.Next(10, 1001); // Massa entre 10 e 1000
                    double densidade = random.Next(1000, 10000); // Densidade aleatória
                    double posX = random.Next(50, this.ClientSize.Width - 50); // Posição aleatória
                    double posY = random.Next(50, this.ClientSize.Height - 50); // Posição aleatória

                    // Inicializa velocidades aleatórias
                    double velX = random.NextDouble() * 10 - 5; // Velocidade aleatória em X
                    double velY = random.NextDouble() * 10 - 5; // Velocidade aleatória em Y

                    // Cria um novo corpo com a massa, densidade, posição e velocidades geradas
                    Corpo novoCorpo = new Corpo($"Corpo {i + 1}", massa, densidade, posX, posY)
                    {
                        VelX = velX,
                        VelY = velY
                    };
                    universo.AdicionarCorpo(novoCorpo);
                }

                timer.Start(); // Inicia a simulação
            }
            else
            {
                MessageBox.Show("Por favor, insira um número válido de corpos.");
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            // Verifica se um corpo foi clicado
            foreach (Corpo corpo in universo.corpos)
            {
                // Ajuste para calcular a distância corretamente
                float tamanho = (float)(corpo.Massa * fatorDeTamanho);
                if (Math.Sqrt(Math.Pow(e.X - (corpo.PosX), 2) + Math.Pow(e.Y - (corpo.PosY), 2)) <= tamanho / 2)
                {
                    // Atualiza os TextBoxes com as informações do corpo clicado
                    PosX_Box.Text = corpo.PosX.ToString("F2");
                    PosY_Box.Text = corpo.PosY.ToString("F2");
                    VelX_Box.Text = corpo.VelX.ToString("F2");
                    VelY_Box.Text = corpo.VelY.ToString("F2");
                    break;
                }
            }
        }

        private void Parar_bt_Click(object sender, EventArgs e)
        {
            timer.Stop(); // Para a simulação
        }
    }
}