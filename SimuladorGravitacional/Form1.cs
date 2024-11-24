using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SimuladorGravitacional
{
    public partial class Form1 : Form
    {
        private Universo universo;
        private int iteracoes;
        private double fatorDeTamanho = 0.01; // Ajuste conforme necessário 
        private List<Corpo> corposParaDesenhar;
        System.Windows.Forms.Timer simulacaoTimer = new System.Windows.Forms.Timer();

        public Form1()
        {
            InitializeComponent();
            universo = new Universo();
            corposParaDesenhar = new List<Corpo>();
        }

        public void AtualizarEDesenhar()
        {
            // Atualiza a simulação, passando as dimensões da tela
            universo.Atualizar(this.ClientSize.Width, this.ClientSize.Height);

            // Incrementa o contador de iterações
            iteracoes++;

            // Verifica se é uma iteração múltipla de 100
            if (iteracoes % 4 == 0)
            {
                // Armazena os corpos atuais na lista
                corposParaDesenhar.Clear(); // Limpa a lista anterior
                corposParaDesenhar.AddRange(universo.corpos); // Adiciona os corpos atuais
                Iteracoes_Box.Text = iteracoes.ToString();

                // Atualiza o TextBox com a quantidade de corpos colididos
                CP_colididos.Text = universo.QuantidadeColididos.ToString();

                // Redesenha a tela
                this.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DesenharCorpos(e.Graphics);
        }

        public void DesenharCorpos(Graphics g)
        {
            // Defina um tamanho máximo para evitar overflow
            const double tamanhoMaximo = 100.0;
            const double fatorDeTamanho = 0.01;

            // Desenha os corpos armazenados na lista
            foreach (Corpo corpo in corposParaDesenhar)
            {
                // Calcular o tamanho da elipse
                double tamanho = corpo.Massa * fatorDeTamanho;

                // Limitar o tamanho para evitar overflow
                if (tamanho > tamanhoMaximo)
                {
                    tamanho = tamanhoMaximo;
                }
                else if (tamanho < 1.0) // Evitar que o tamanho seja muito pequeno
                {
                    tamanho = 1.0;
                }

                // Calcular as coordenadas de desenho
                float x = (float)(corpo.PosX - tamanho / 2);
                float y = (float)(corpo.PosY - tamanho / 2);

                // Desenhar a elipse
                g.FillEllipse(Brushes.Yellow, x, y, (float)tamanho, (float)tamanho);
            }
        }

        public void Iniciar_bt_Click(object sender, EventArgs e)
        {
            // Limpa o universo e reinicia as iterações
            universo = new Universo();
            iteracoes = 0;
            Iteracoes_Box.Text = "0"; // Reseta o texto de iterações

            // Aqui ele vai transformar o valor do textbox para int
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

                // Inicia a simulação manualmente
                simulacaoTimer.Interval = 1; // Define um intervalo muito curto para simular a atualização contínua
                simulacaoTimer.Tick += (s, args) =>
                {
                    AtualizarEDesenhar();
                };
                simulacaoTimer.Start(); // Inicia o timer para atualizar a simulação
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

        public void Parar_bt_Click(object sender, EventArgs e)
        {
            // Para a simulação
            simulacaoTimer.Stop(); // Para a simulação
        }
    }
}
