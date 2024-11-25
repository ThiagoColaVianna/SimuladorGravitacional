using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimuladorGravitacional
{
    internal class Universo
    {
        public List<Corpo> corpos;
        public ConcurrentBag<Corpo> corposParaRemover;
        public int QuantidadeColididos;

        public Universo()
        {
            corpos = new List<Corpo>();
            corposParaRemover = new ConcurrentBag<Corpo>();
            QuantidadeColididos = 0;
        }

        public void AdicionarCorpo(Corpo corpo)
        {
            corpos.Add(corpo);
        }

        public void Atualizar(int larguraTela, int alturaTela)
        {
            // Resetar as forças antes de calcular
            foreach (var corpo in corpos)
            {
                corpo.ForcaX = 0.0;
                corpo.ForcaY = 0.0;
            }

            // Calcular forças entre os corpos
            var corposCopia = corpos.ToList();
            Parallel.For(0, corposCopia.Count, i =>
            {
                for (int j = i + 1; j < corposCopia.Count; j++)
                {
                    if (corposCopia[i].Colidiu(corposCopia[j]))
                    {
                        // Tratamento de colisão
                        lock (corposParaRemover)
                        {
                            corposParaRemover.Add(corposCopia[i]);
                            corposParaRemover.Add(corposCopia[j]);
                        }
                    }
                    else
                    {
                        // Cálculo da força gravitacional
                        double G = 0.1; // Constante gravitacional
                        double distancia = Math.Sqrt(Math.Pow(corposCopia[i].PosX - corposCopia[j].PosX, 2) +
                              Math.Pow(corposCopia[i].PosY - corposCopia[j].PosY, 2));

                        // Evitar divisão por zero
                        if (distancia > 0)
                        {
                            // Calcula a força gravitacional total entre os corpos i e j
                            double forca = G * (corposCopia[i].Massa * corposCopia[j].Massa) / (distancia * distancia);

                            // Calcula a componente X da força gravitacional
                            // A diferença de posição entre os corpos é normalizada pela distância total
                            double forcax = forca * (corposCopia[j].PosX - corposCopia[i].PosX) / distancia;

                            // Calcula a componente Y da força gravitacional
                            // A diferença de posição entre os corpos é normalizada pela distância total
                            double forcay = forca * (corposCopia[j].PosY - corposCopia[i].PosY) / distancia;

                            lock (corposCopia[i])
                            {
                                corposCopia[i].ForcaX += forcax;
                                corposCopia[i].ForcaY += forcay;
                            }

                            lock (corposCopia[j])
                            {
                                corposCopia[j].ForcaX -= forcax;
                                corposCopia[j].ForcaY -= forcay;
                            }
                        }
                    }
                }
            });

            // Atualizar velocidades baseado nas forças
            Parallel.For(0, corpos.Count, i =>
            {
                if (corpos[i].Massa > 0)
                {
                    corpos[i].VelX += corpos[i].ForcaX / corpos[i].Massa;
                    corpos[i].VelY += corpos[i].ForcaY / corpos[i].Massa;
                }
            });

            // Atualizar posições
            Parallel.For(0, corpos.Count, i =>
            {
                corpos[i].PosX += corpos[i].VelX; // Atualiza a posição X
                corpos[i].PosY += corpos[i].VelY; // Atualiza a posição Y

                // Limitar os corpos dentro da tela
                if (corpos[i].PosX < 0) corpos[i].PosX = 0;
                if (corpos[i].PosX > larguraTela) corpos[i].PosX = larguraTela;
                if (corpos[i].PosY < 0) corpos[i].PosY = 0;
                if (corpos[i].PosY > alturaTela) corpos[i].PosY = alturaTela;
            });

            // Remover corpos colididos e adicionar novos corpos fora do loop principal
            lock (corpos)
            {
                foreach (var corpo in corposParaRemover)
                {
                    corpos.Remove(corpo);
                }
                corposParaRemover = new ConcurrentBag<Corpo>(); // Limpar a bag de corpos para remover
            }
        }

        private void TratamentoColisao(Corpo a, Corpo b)
        {
            Corpo novoCorpo = a + b; // Usa o operador sobrecarregado
            lock (corpos)
            {
                corposParaRemover.Add(a);
                corposParaRemover.Add(b);
                corpos.Add(novoCorpo); // Adiciona o novo corpo à lista
            }
        }
    }
}