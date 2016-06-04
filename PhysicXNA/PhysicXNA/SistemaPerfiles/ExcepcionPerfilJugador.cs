using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhysicXNA.SistemaPerfiles
{
    /// <summary>Excepción que ocurre al realizar operaciones con los perfiles de jugador.</summary>
    [Serializable]
    public class ExcepcionPerfilJugador : Exception
    {
        /// <summary>Crea una instancia de esta clase.</summary>
        public ExcepcionPerfilJugador() { }
        /// <summary>
        /// Crea una instancia de esta clase y especifica en un mensaje la causa del error.
        /// </summary>
        /// <param name="message">Mensaje que causó el error.</param>
        public ExcepcionPerfilJugador(string message) : base(message) { }
        /// <summary>
        /// Crea una instancia de esta clase y especifica en un mensaje la causa del error y una excepción base.
        /// </summary>
        /// <param name="message">Mensaje que causó el error.</param>
        /// <param name="inner">Excepción que causó esta nueva excepción.</param>
        public ExcepcionPerfilJugador(string message, Exception inner) : base(message, inner) { }
    }
}
