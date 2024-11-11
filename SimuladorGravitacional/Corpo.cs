﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorGravitacional
{
    internal class Corpo
    {
        public string Nome { get; set; }
        public double Massa { get; set; }
        public double Raio { get; set; }
        public double Densidade { get; set; }
        public double PosX { get; set; }
        public double PosY { get; set; }
        public double VelX { get; set; }
        public double VelY { get; set; }

        public Corpo(string nome, double massa, double densidade, double posX, double posY)
        {
            Nome = nome;
            Massa = massa;
            Densidade = densidade;
            Raio = Math.Pow((3 * massa) / (4 * Math.PI * densidade), 1.0 / 3.0);
            PosX = posX;
            PosY = posY;
            VelX = 0.0;
            VelY = 0.0;
        }

        public static Corpo operator +(Corpo a, Corpo b)
        {
            double novaMassa = a.Massa + b.Massa;
            double novaPosX = (a.PosX * a.Massa + b.PosX * b.Massa) / novaMassa; // Posição média ponderada
            double novaPosY = (a.PosY * a.Massa + b.PosY * b.Massa) / novaMassa; // Posição média ponderada

            // Cálculo da nova velocidade considerando a quantidade de movimento
            double novaVelX = (a.VelX * a.Massa + b.VelX * b.Massa) / novaMassa;
            double novaVelY = (a.VelY * a.Massa + b.VelY * b.Massa) / novaMassa;

            return new Corpo("Corpo Colidido", novaMassa, (a.Densidade + b.Densidade) / 2, novaPosX, novaPosY)
            {
                VelX = novaVelX,
                VelY = novaVelY
            };
        }

        public double Distancia(Corpo outro)
        {
            return Math.Sqrt(Math.Pow(this.PosX - outro.PosX, 2) + Math.Pow(this.PosY - outro.PosY, 2));
        }

        public bool Colidiu(Corpo outro)
        {
            return Distancia(outro) < (this.Raio + outro.Raio);
        }

        public void AtualizarPosicao(double deltaTime, int larguraTela, int alturaTela)
        {
            PosX += VelX * deltaTime;
            PosY += VelY * deltaTime;

            // Verifica limites da tela e inverte a velocidade se necessário
            if (PosX - Raio < 0 || PosX + Raio > larguraTela)
            {
                VelX = -VelX; // Inverte a velocidade em X
                PosX = Math.Clamp(PosX, Raio, larguraTela - Raio); // Mantém dentro dos limites
            }

            if (PosY - Raio < 0 || PosY + Raio > alturaTela)
            {
                VelY = -VelY; // Inverte a velocidade em Y
                PosY = Math.Clamp(PosY, Raio, alturaTela - Raio); // Mantém dentro dos limites
            }
        }
    }
}