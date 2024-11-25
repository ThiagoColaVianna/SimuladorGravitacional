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
        private double fatorDeTamanho = 0.01; // Ajuste conforme necess�rio 
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
            // Atualiza a simula��o, passando as dimens�es da tela
            universo.Atualizar(this.ClientSize.Width, this.ClientSize.Height);

            // Incrementa o contador de itera��es
            iteracoes++;

            // Imprime na tela a cada 4 itera��es
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

        
        // M�todo alternativo ao de cima
        //public void DesenharCorpos(Graphics g)
        //{
        //    const double tamanhoMaximo = 100.0;
        //    const double fatorDeTamanho = 0.01;

        //    // Calcular um fator de zoom
        //    double zoom = 1.0;
        //    // Ajustar o zoom com base na posi��o dos corpos ou de forma din�mica
        //    foreach (Corpo corpo in corposParaDesenhar)
        //    {
        //        if (corpo.PosX < 100 || corpo.PosY < 100 || corpo.PosX > this.ClientSize.Width - 100 || corpo.PosY > this.ClientSize.Height - 100)
        //        {
        //            zoom = 0.5; // Reduz o zoom quando o corpo estiver perto da borda
        //        }
        //    }

        //    // Desenha os corpos armazenados na lista
        //    foreach (Corpo corpo in corposParaDesenhar)
        //    {
        //        // Calcular o tamanho da elipse com o fator de zoom
        //        double tamanho = corpo.Massa * fatorDeTamanho * zoom;

        //        // Limitar o tamanho para evitar overflow
        //        if (tamanho > tamanhoMaximo)
        //        {
        //            tamanho = tamanhoMaximo;
        //        }
        //        else if (tamanho < 1.0) // Evitar que o tamanho seja muito pequeno
        //        {
        //            tamanho = 1.0;
        //        }

        //        // Calcular as coordenadas de desenho com o fator de zoom
        //        float x = (float)((corpo.PosX - tamanho / 2) * zoom);
        //        float y = (float)((corpo.PosY - tamanho / 2) * zoom);

        //        // Desenhar a elipse
        //        g.FillEllipse(Brushes.Yellow, x, y, (float)tamanho, (float)tamanho);
        //    }
        //}


        public void Iniciar_bt_Click(object sender, EventArgs e)
        {
            // Limpa o universo e reinicia as itera��es
            universo = new Universo();
            iteracoes = 0;
            Iteracoes_Box.Text = "0"; // Reseta o texto de itera��es

            // Aqui ele vai transformar o valor do textbox para int
            if (int.TryParse(Corpos_Box.Text, out int quantidade) && quantidade > 0)
            {
                Random random = new Random();
                for (int i = 0; i < quantidade; i++)
                {
                    // Gerando a massa (valores entre 10 e 1000)
                    double massa = random.Next(10, 1001); // Massa entre 10 e 1000
                    double densidade = random.Next(1000, 10000); // Densidade aleat�ria
                    double posX = random.Next(50, this.ClientSize.Width - 50); // Posi��o aleat�ria
                    double posY = random.Next(50, this.ClientSize.Height - 50); // Posi��o aleat�ria

                    // Inicializa velocidades aleat�rias
                    double velX = random.NextDouble() * 1 - 0.5; // Velocidade aleat�ria em X
                    double velY = random.NextDouble() * 1 - 0.5; // Velocidade aleat�ria em Y

                    // Cria um novo corpo com a massa, densidade, posi��o e velocidades geradas
                    Corpo novoCorpo = new Corpo($"Corpo {i + 1}", massa, densidade, posX, posY)
                    {
                        VelX = velX,
                        VelY = velY
                    };
                    universo.AdicionarCorpo(novoCorpo);
                }

                // Inicia a simula��o manualmente
                simulacaoTimer.Interval = 35; // Define um intervalo muito curto para simular a atualiza��o cont�nua
                simulacaoTimer.Tick += (s, args) =>
                {
                    AtualizarEDesenhar();
                };
                simulacaoTimer.Start(); // Inicia o timer para atualizar a simula��o
            }
            else
            {
                MessageBox.Show("Por favor, insira um n�mero v�lido de corpos.");
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            // Verifica se um corpo foi clicado
            foreach (Corpo corpo in universo.corpos)
            {
                // Ajuste para calcular a dist�ncia corretamente
                float tamanho = (float)(corpo.Massa * fatorDeTamanho);
                if (Math.Sqrt(Math.Pow(e.X - (corpo.PosX), 2) + Math.Pow(e.Y - (corpo.PosY), 2)) <= tamanho / 2)
                {
                    // Atualiza os TextBoxes com as informa��es do corpo clicado
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
            // Para a simula��o
            simulacaoTimer.Stop(); // Para a simula��o
        }

        public void button1_Click(object sender, EventArgs e)
        {
            // Cria uma nova inst�ncia de OpenFileDialog
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Configura o filtro para mostrar apenas arquivos .ini
                openFileDialog.Filter = "Arquivos INI (*.uni)|*.uni|Todos os arquivos (*.*)|*.*";
                openFileDialog.Title = "Selecione um arquivo .uni";

                // Exibe o di�logo e verifica se o usu�rio selecionou um arquivo
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Obt�m o caminho do arquivo selecionado
                    string filePath = openFileDialog.FileName;

                    // Aqui voc� pode carregar e processar o arquivo .ini
                    CarregarArquivoIni(filePath);
                }
            }
        }

        public void CarregarArquivoIni(string filePath)
        {
            try
            {
                // L� todas as linhas do arquivo
                string[] linhas = System.IO.File.ReadAllLines(filePath);

                // Verifica se h� pelo menos uma linha no arquivo
                if (linhas.Length == 0)
                {
                    MessageBox.Show("O arquivo est� vazio.");
                    return;
                }

                // Tenta converter o primeiro n�mero para a quantidade de corpos
                if (!int.TryParse(linhas[0].Trim(), out int quantidadeCorpos) || quantidadeCorpos <= 0)
                {
                    MessageBox.Show("A quantidade de corpos n�o � v�lida.");
                    return;
                }

                // Processa as linhas, come�ando da segunda linha
                for (int i = 1; i <= quantidadeCorpos && i < linhas.Length; i++)
                {
                    string linha = linhas[i];
                    // Divide a linha em partes usando o delimitador ';'
                    var partes = linha.Split(';');
                    if (partes.Length >= 5)
                    {
                        string nome = partes[0].Trim();
                        double massa = double.Parse(partes[1].Trim());
                        double densidade = double.Parse(partes[2].Trim());
                        double posX = double.Parse(partes[3].Trim());
                        double posY = double.Parse(partes[4].Trim());

                        // Cria uma nova inst�ncia de Corpo
                        Corpo corpo = new Corpo(nome, massa, densidade, posX, posY);

                        // Adiciona o corpo ao universo
                        universo.AdicionarCorpo(corpo);
                    }
                    else
                    {
                        MessageBox.Show($"Formato inv�lido na linha {i + 1}.");
                    }
                }

                // Inicia a simula��o manualmente
                simulacaoTimer.Interval = 35; // Define um intervalo muito curto para simular a atualiza��o cont�nua
                simulacaoTimer.Tick += (s, args) =>
                {
                    AtualizarEDesenhar();
                };
                simulacaoTimer.Start(); // Inicia o timer para atualizar a simula��o
            }
            catch (Exception ex)
            {
                // Lida com exce��es, como arquivos n�o encontrados ou problemas de leitura
                MessageBox.Show($"Erro ao ler o arquivo: {ex.Message}");
            }
        }
    }
}
