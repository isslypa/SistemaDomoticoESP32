using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDomotico.Domain
{
    public abstract class DispositivoBase : IActuable
    {
        public string Nombre { get; }
        protected bool encendido;
        protected DispositivoBase(string nombre)
        {
            Nombre = string.IsNullOrWhiteSpace(nombre) ? "Dispositivo" : nombre.Trim();
            encendido = false;
        }
        public virtual void Encender() => encendido = true;
        public virtual void Apagar() => encendido = false;
        public bool EstaEncendido => encendido;
        public override string ToString() => $"{Nombre} (On: {encendido})";
    }
}