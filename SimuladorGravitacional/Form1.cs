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

            // Imprime na tela a cada 4 iterações
            if (iteracoes % 4 == 0)
            {
                // Armazena os corpos atuais na lista
                corposParaDesenhar.Clear(); // Limpa a lista anterior
                corposParaDesenhar.AddRange(universo.corpos); // Adiciona os corpos atuais
                Iteracoes_Box.Text = iteracoes.ToString();


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
                    double velX = random.NextDouble() * 1 - 0.5; // Velocidade aleatória em X
                    double velY = random.NextDouble() * 1 - 0.5; // Velocidade aleatória em Y

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

        private void button1_Click(object sender, EventArgs e)
        {
            // Cria uma nova instância de OpenFileDialog
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Configura o filtro para mostrar apenas arquivos .ini
                openFileDialog.Filter = "Arquivos INI (*.ini)|*.ini|Todos os arquivos (*.*)|*.*";
                openFileDialog.Title = "Selecione um arquivo .ini";

                // Exibe o diálogo e verifica se o usuário selecionou um arquivo
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Obtém o caminho do arquivo selecionado
                    string filePath = openFileDialog.FileName;

                    // Aqui você pode carregar e processar o arquivo .ini
                    CarregarArquivoIni(filePath);
                }
            }
        }

        private void CarregarArquivoIni(string filePath)
        {            
            try
            {
                // Lê todas as linhas do arquivo
                string[] linhas = System.IO.File.ReadAllLines(filePath);

                // Processa as linhas
                foreach (string linha in linhas)
                {
                    // Aqui você pode dividir a linha e processar os dados conforme necessário
                    // Exemplo: supondo que o arquivo tenha linhas no formato "chave=valor"
                    var partes = linha.Split('=');
                    if (partes.Length == 2)
                    {
                        string chave = partes[0].Trim();
                        string valor = partes[1].Trim();

                        // Faça algo com a chave e o valor
                        // Por exemplo, você pode armazenar esses valores em um dicionário
                        Console.WriteLine($"Chave: {chave}, Valor: {valor}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Lida com exceções, como arquivos não encontrados ou problemas de leitura
                MessageBox.Show($"Erro ao ler o arquivo: {ex.Message}");
            }
        }
    }
}
