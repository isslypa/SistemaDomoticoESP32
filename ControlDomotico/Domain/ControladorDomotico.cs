using System;
using System.Linq;
using System.Collections.Generic;

namespace ControlDomotico.Domain
{
    /// <summary>
    /// Representa un controlador general que administra múltiples dispositivos actuables.
    /// </summary>
    public class ControladorDomotico
    {
        private readonly List<IActuable> _dispositivos = new();

        /// <summary>
        /// Agrega un nuevo dispositivo a la lista si no es nulo y no existe uno con el mismo nombre.
        /// Retorna true si se agregó exitosamente, false en caso contrario.
        /// </summary>
        public bool Agregar(IActuable d)
        {
            if (d is null) return false;

            // Evita duplicados comparando por Nombre (propiedad de DispositivoBase)
            bool yaExiste = _dispositivos.Any(x =>
                x is DispositivoBase b &&
                d is DispositivoBase nb &&
                string.Equals(b.Nombre, nb.Nombre, StringComparison.OrdinalIgnoreCase));

            if (yaExiste) return false;

            _dispositivos.Add(d);
            return true;
        }

        /// <summary>
        /// Devuelve una vista de solo lectura de los dispositivos registrados.
        /// </summary>
        public IReadOnlyList<IActuable> Dispositivos => _dispositivos.AsReadOnly();

        /// <summary>
        /// Enciende todos los dispositivos que implementan IActuable.
        /// </summary>
        public void EncenderTodos()
        {
            foreach (var x in _dispositivos) x.Encender();
        }

        /// <summary>
        /// Apaga todos los dispositivos que implementan IActuable.
        /// </summary>
        public void ApagarTodos()
        {
            foreach (var x in _dispositivos) x.Apagar();
        }
    }
}