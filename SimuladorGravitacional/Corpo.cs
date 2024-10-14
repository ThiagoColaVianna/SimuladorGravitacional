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

        public Corpo()
        {
            this.nome = nome;
            this.massa = massa;
            this.raio = raio;
            this.densidade = densidade;
            this.PosX = PosX;
            this.PosY = PosY;
            this.VelX = VelX;
            this.VelY = VelY;
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
