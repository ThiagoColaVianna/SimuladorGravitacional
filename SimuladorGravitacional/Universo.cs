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
                        TratamentoColisao(corposCopia[i], corposCopia[j]);
                        corposParaRemover.Add(corposCopia[i]);
                        corposParaRemover.Add(corposCopia[j]);
                    }
                    else
                    {
                        // Cálculo da força gravitacional
                        double G = 6.67430e-11; // Constante gravitacional
                        double distancia = corposCopia[i].Distancia(corposCopia[j]);

                        if (distancia > 0)
                        {
                            double forca = G * (corposCopia[i].Massa * corposCopia[j].Massa) / (distancia * distancia);
                            double forcax = forca * (corposCopia[j].PosX - corposCopia[i].PosX) / distancia;
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

            // Remover corpos que colidiram
            lock (corpos)
            {
                foreach (var corpo in corposParaRemover)
                {
                    corpos.Remove(corpo);
                }
            }

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
        }

        private void TratamentoColisao(Corpo a, Corpo b)
        {
            Corpo novoCorpo = a + b; // Usa o operador sobrecarregado
            lock (corpos)
            {
                corpos.Remove(a);
                corpos.Remove(b);
                corpos.Add(novoCorpo);
            }
        }
    }
}