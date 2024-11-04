using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorGravitacional
{
    internal class Corpo
    {
        protected string nome;
        protected double massa;
        protected double raio;
        protected double densidade;
        protected double PosX;
        protected double PosY;
        protected double VelX;
        protected double VelY;

        public Corpo(string nome, double massa, double raio, double densidade, double posX, double posY, double velX, double velY)
        {
            this.nome = nome;
            this.massa = massa;
            this.raio = raio;
            this.densidade = densidade;
            this.PosX = posX;
            this.PosY = posY;
            this.VelX = velX;
            this.VelY = velY;
        }

        public Corpo()
        {
            this.nome = "Desconhecido";
            this.massa = 0.0;
            this.raio = 0.0;
            this.densidade = 0.0;
            this.PosX = 0.0;
            this.PosY = 0.0;
            this.VelX = 0.0;
            this.VelY = 0.0;
        }

        public string GetNome()
        {
            return nome;
        }

        public double GetMassa()
        {
            return massa;
        }

        public double GetRaio()
        {
            return raio;
        }

        public double GetDensidade()
        {
            return densidade;
        }

        public double GetPosX()
        {
            return PosX;
        }

        public double GetPosY()
        {
            return PosY;
        }

        public double GetVelX()
        {
            return VelX;
        }

        public double GetVelY()
        {
            return VelY;
        }
    }
}
