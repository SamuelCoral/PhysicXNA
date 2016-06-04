using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PhysicXNA.Nucleo
{
    /// <summary>Clase que contiene un textura y todas sus propiedades que tiene la intención de no mostrar ninguna animación.</summary>
    public class DuendeEstatico : DuendeBasico
    {
        /// <summary>
        /// Construye una instancia de esta clase dadas todas sus propiedades.
        /// </summary>
        /// <param name="rutaContenido"><seealso cref="DuendeBasico.RutaContenido"/></param>
        /// <param name="posicion"><seealso cref="DuendeBasico.Posicion"/></param>
        /// <param name="color"><seealso cref="DuendeBasico.Color"/></param>
        /// <param name="escala"><seealso cref="DuendeBasico.Escala"/></param>
        /// <param name="efecto"><seealso cref="DuendeBasico.Efecto"/></param>
        /// <param name="anguloGiro"><seealso cref="DuendeBasico.AnguloGiro"/></param>
        /// <param name="origenGiro"><seealso cref="DuendeBasico.OrigenGiro"/></param>
        public DuendeEstatico(String rutaContenido, Vector2 posicion, Color color = new Color(), Vector2 escala = new Vector2(), SpriteEffects efecto = SpriteEffects.None, float anguloGiro = 0, Vector2 origenGiro = new Vector2())
            : base(rutaContenido, posicion, color, escala, efecto, anguloGiro, origenGiro)
        {
            
        }

        /// <summary>
        /// Carga el contenido a partir de la ruta dada en la contrucción del objeto de esta clase.
        /// </summary>
        /// <param name="contenido">Administrador de contenidos del juego para cargar el contenido.</param>
        public override void CargarContenido(ContentManager contenido)
        {
            textura = contenido.Load<Texture2D>(RutaContenido);
        }

        /// <summary>
        /// Dibuja la textura.
        /// </summary>
        /// <param name="lotesDuende">SpriteBatch.</param>
        /// <param name="tiempoJuego">Tiempo del juego.</param>
        public override void Dibujar(SpriteBatch lotesDuende, GameTime tiempoJuego)
        {
            lotesDuende.Draw(textura, Posicion, new Rectangle(0, 0, textura.Width, textura.Height), Color, AnguloGiro, OrigenGiro, Escala, Efecto, 1f);

            base.Dibujar(lotesDuende, tiempoJuego);
        }
    }
}
