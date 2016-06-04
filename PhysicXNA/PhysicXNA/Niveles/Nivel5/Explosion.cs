using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using PhysicXNA.Nucleo;

namespace PhysicXNA.Niveles.Nivel5
{
    /// <summary>
    /// Pequeña animación de explosión al destruirse un meteorito.
    /// </summary>
    public class Explosion : DuendeAnimado
    {
        Point cuadroAnterior;
        /// <summary>Indica si la animación ha terminado.</summary>
        public bool desaparecer;
        internal Vector2 posicionAbsoluta;
        Nivel5 nivelOrigen;

        /// <summary>
        /// Crea una nueva explosión para el nivel dado especificando su posición en el escenario.
        /// </summary>
        /// <param name="nivelOrigen">Nivel en el que se dibujará la explosión.</param>
        /// <param name="posicionAbsoluta">Posición absoluta en el escenario de la explosión.</param>
        public Explosion(Nivel5 nivelOrigen, Vector2 posicionAbsoluta)
            : base("Recursos/Nivel5/explosion", Vector2.Zero, new Point(256, 128), new Point(3, 4), 15)
        {
            this.posicionAbsoluta = posicionAbsoluta;
            this.nivelOrigen = nivelOrigen;
            cuadroAnterior = Point.Zero;
            desaparecer = false;
        }

        /// <summary>
        /// Coloca la explosión en el escenario y determina si terminó la animación.
        /// </summary>
        /// <param name="tiempoJuego">Tiempo del juego.</param>
        /// <param name="graficos">Resolución de pantalla.</param>
        /// <param name="teclado">Estado del teclado.</param>
        public override void Actualizar(GameTime tiempoJuego, Viewport graficos, KeyboardState teclado)
        {
            base.Actualizar(tiempoJuego, graficos, teclado);
            Posicion = new Vector2(posicionAbsoluta.X - nivelOrigen.nave.posicionAbsoluta.X + nivelOrigen.nave.Posicion.X, posicionAbsoluta.Y - nivelOrigen.nave.posicionAbsoluta.Y + nivelOrigen.nave.Posicion.Y);
            if (CuadroActual == Point.Zero && cuadroAnterior != CuadroActual) desaparecer = true;
            cuadroAnterior = CuadroActual;
        }
    }
}
