using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDomotico.Domain
{ 
    public sealed class Lampara : DispositivoBase
    {
        public Lampara(string nombre) : base(nombre) { }
        public override void Encender()
        {
            // Aquí podría añadirse lógica adicional (ej.: notificar a GUI o enviar comando serial)
            base.Encender();
        }
        public override void Apagar()
        {
            // Aquí podría añadirse lógica adicional (ej.: notificar a GUI o enviar comando serial)
            base.Apagar();
        }
    }
}