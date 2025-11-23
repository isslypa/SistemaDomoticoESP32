using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ControlDomotico.Domain
{
   
    public interface IActuable
    {
         void Encender();
         void Apagar();
         bool EstaEncendido { get; }
    }
}