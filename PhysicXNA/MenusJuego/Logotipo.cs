using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhysicXNA.Nucleo;

namespace PhysicXNA.MenusJuego
{
    /// <summary>
    /// Logotipo animado.
    /// </summary>
    public class Logotipo : DuendeEstatico
    {
        Vector2 escalaInicial;
        bool incremento;
        int tiempoFotograma;
        int CuadrosPorSegundo;

        /// <summary>
        /// Crea una instancia del logotipo.
        /// </summary>
        /// <param name="cuadrosPorSegundo">Velocidad de actualización de la animación.</param>
        /// <param name="rutaContenido">Ruta de la animación.</param>
        /// <param name="escala">Escala de la animación.</param>
        public Logotipo(String rutaContenido, int cuadrosPorSegundo, Vector2 escala)
            : base(rutaContenido, Vector2.Zero, escala: escala)
        {
            CuadrosPorSegundo = cuadrosPorSegundo;
            tiempoFotograma = 0;
            escalaInicial = Escala;
            incremento = false;
        }

        /// <summary>
        /// Actualiza la animación del logotipo.
        /// </summary>
        /// <param name="tiempoJuego">Tiempo del juego.</param>
        /// <param name="graficos">Resolución de pantalla.</param>
        /// <param name="teclado">Estado del teclado.</param>
        public override void Actualizar(GameTime tiempoJuego, Viewport graficos, KeyboardState teclado)
        {
            base.Actualizar(tiempoJuego, graficos, teclado);
            tiempoFotograma += tiempoJuego.ElapsedGameTime.Milliseconds;
            if (tiempoFotograma >= 1000 / CuadrosPorSegundo)
            {
                tiempoFotograma %= 1000 / CuadrosPorSegundo;
                if (incremento)
                {
                    Escala += new Vector2(0.001f);
                    if (Escala.X > escalaInicial.X * 1.1f) incremento = false;
                }
                else
                {
                    Escala -= new Vector2(0.001f);
                    if (Escala.X < escalaInicial.X * 0.9f) incremento = true;
                }
            }
        }
    }
}
