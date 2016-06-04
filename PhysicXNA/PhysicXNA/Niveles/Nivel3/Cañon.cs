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

namespace PhysicXNA.Niveles.Nivel3
{
    /// <summary>
    /// Pequeño cañón que se anima al lanzar un proyectil del nivel 3.
    /// </summary>
    public class Cañon : DuendeEstatico
    {
        /// <summary>Indica si se debe aplicar la animación.</summary>
        public bool disparando;
        /// <summary>Indica la velocidad d ela animación.</summary>
        public float velocidad;

        /// <summary>
        /// Construye un cañón para el nivel 2.
        /// </summary>
        /// <param name="velocidad"><seealso cref="velocidad"/></param>
        public Cañon(float velocidad)
            : base("Recursos/Nivel3/cañon", Vector2.Zero, escala: new Vector2(0.4f))
        {
            disparando = false;
            this.velocidad = velocidad;
        }

        /// <summary>
        /// Actualiza la animación del cañón.
        /// </summary>
        /// <param name="tiempoJuego">Tiempo del juego.</param>
        /// <param name="graficos">Resolución de pantalla.</param>
        /// <param name="teclado">Estado del teclado.</param>
        public override void Actualizar(GameTime tiempoJuego, Viewport graficos, KeyboardState teclado)
        {
            base.Actualizar(tiempoJuego, graficos, teclado);

            if(disparando)
                Posicion = new Vector2(Posicion.X - (float)Math.Cos(AnguloGiro) * velocidad * tiempoJuego.ElapsedGameTime.Milliseconds / 1000f,
                Posicion.Y - (float)Math.Sin(AnguloGiro) * velocidad * tiempoJuego.ElapsedGameTime.Milliseconds / 1000f);
        }
    }
}
