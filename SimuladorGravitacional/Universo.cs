using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorGravitacional
{
    internal class Universo
    {
        public List<Corpo> corpos;

        public Universo()
        {
            corpos = new List<Corpo>();
        }

        public void AdicionarCorpo(Corpo corpo)
        {
            corpos.Add(corpo);
        }

        public void Atualizar(double deltaTime, int larguraTela, int alturaTela)
        {
            List<Corpo> novosCorpos = new List<Corpo>(corpos); // Cria uma nova lista para armazenar os novos corpos

            // Calcular forças entre os corpos
            Parallel.For(0, corpos.Count, i =>
            {
                for (int j = i + 1; j < corpos.Count; j++)
                {
                    if (corpos[i].Colidiu(corpos[j]))
                    {
                        // Tratamento de colisão
                        lock (novosCorpos) // Lock para evitar condições de corrida
                        {
                            Corpo novoCorpo = corpos[i] + corpos[j];
                            novosCorpos.Remove(corpos[i]);
                            novosCorpos.Remove(corpos[j]);
                            novosCorpos.Add(novoCorpo);
                        }
                    }
                    else
                    {
                        // Cálculo da força gravitacional
                        double G = 6.67430e-11; // Constante gravitacional
                        double distancia = corpos[i].Distancia(corpos[j]);

                        // Evitar divisão por zero
                        if (distancia > 0)
                        {
                            double forca = G * (corpos[i].Massa * corpos[j].Massa) / (distancia * distancia);

                            // Direção da força
                            double forcax = forca * (corpos[j].PosX - corpos[i].PosX) / distancia;
                            double forcay = forca * (corpos[j].PosY - corpos[i].PosY) / distancia;

                            // Aplicar forças
                            lock (corpos) // Lock para evitar condições de corrida
                            {
                                // Atualizar velocidades
                                corpos[i].VelX += forcax / corpos[i].Massa * deltaTime;
                                corpos[i].VelY += forcay / corpos[i].Massa * deltaTime;

                                // Atualiza as velocidades do corpo j (força oposta)
                                corpos[j].VelX -= forcax / corpos[j].Massa * deltaTime;
                                corpos[j].VelY -= forcay / corpos[j].Massa * deltaTime;
                            }
                        }
                    }
                }
            });

            // Atualizar posições
            Parallel.For(0, novosCorpos.Count, i =>
            {
                novosCorpos[i].AtualizarPosicao(deltaTime, larguraTela, alturaTela);
            });

            // Substituir a lista original de corpos pela nova lista
            lock (corpos) // Lock para evitar condições de corrida
            {
                corpos = novosCorpos;
            }
        }

        private void TratamentoColisao(Corpo a, Corpo b)
        {
            Corpo novoCorpo = a + b; // Usa o operador sobrecarregado
            lock (corpos) // Lock para evitar condições de corrida
            {
                corpos.Remove(a);
                corpos.Remove(b);
                corpos.Add(novoCorpo);
            }
        }
    }
}
