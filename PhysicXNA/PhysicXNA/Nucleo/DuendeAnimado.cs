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
    /// <summary>Clase que contiene una textura y su información necesaria con el fin de ser utilizada como sprite y animarlo.</summary>
    public class DuendeAnimado : DuendeBasico
    {
        /// <summary>Obtiene el ancho de un fotograma.</summary>
        public override int Ancho
        {
            get { return base.Ancho / NumCuadros.X; }
        }

        /// <summary>Obtiene el alto de un fotograma.</summary>
        public override int Alto
        {
            get { return base.Alto / NumCuadros.Y; }
        }

        /// <summary>Obtiene el ancho de un fotograma (sin aplicar escala).</summary>
        public override int AnchoReal
        {
            get { return base.AnchoReal / NumCuadros.X; }
        }

        /// <summary>Obtiene el alto de un fotograma (sin aplicar escala).</summary>
        public override int AltoReal
        {
            get { return base.AltoReal / NumCuadros.Y; }
        }

        /// <summary><seealso cref="NumCuadros"/></summary>
        protected Point __NumCuadros;
        /// <summary>Número de fotogramas en la imágen.</summary>
        public Point NumCuadros
        {
            get { return __NumCuadros; }
        }

        /// <summary><seealso cref="TamCuadros"/></summary>
        protected Point __TamCuadros;
        /// <summary>Tamaño de cada fotograma.</summary>
        public Point TamCuadros
        {
            get { return __TamCuadros; }
        }

        /// <summary><seealso cref="CuadroActual"/></summary>
        protected Point __CuadroActual;
        /// <summary>Fotograma siendo dibujado actualmente.</summary>
        public Point CuadroActual
        {
            get { return __CuadroActual; }
            set { __CuadroActual = value; }
        }

        /// <summary><seealso cref="CuadrosPorSegundo"/></summary>
        protected int __CuadrosPorSegundo;
        /// <summary>Velocidad de reproducción de fotogramas de la imagen.</summary>
        public int CuadrosPorSegundo
        {
            get { return __CuadrosPorSegundo; }
        }

        /// <summary>Tiempo transcurrido desde la última actualización de fotograma.</summary>
        protected int TiempoJuego = 0;


        /// <summary>
        /// Construye una instancia de esta clase dadas todas las propiedades del sprite.
        /// </summary>
        /// <remarks>
        /// Para dibujar un sprite se tomarán todos los fotogramas de ancho y de alto en orden de arriba hacia abajo y de izquierda a derecha.
        /// No debe haber huecos entre fotogramas.
        /// </remarks>
        /// <param name="rutaContenido"><seealso cref="DuendeBasico.RutaContenido"/></param>
        /// <param name="posicion"><seealso cref="DuendeBasico.Posicion"/></param>
        /// <param name="tamCuadros"><seealso cref="TamCuadros"/></param>
        /// <param name="numCuadros"><seealso cref="NumCuadros"/></param>
        /// <param name="cuadrosPorSegundo"><seealso cref="CuadrosPorSegundo"/></param>
        /// <param name="color"><seealso cref="Color"/></param>
        /// <param name="escala"><seealso cref="DuendeBasico.Escala"/></param>
        /// <param name="efecto"><seealso cref="DuendeBasico.Efecto"/></param>
        /// <param name="anguloGiro"><seealso cref="DuendeBasico.AnguloGiro"/></param>
        /// <param name="origenGiro"><seealso cref="DuendeBasico.OrigenGiro"/></param>
        public DuendeAnimado(String rutaContenido, Vector2 posicion, Point tamCuadros, Point numCuadros, int cuadrosPorSegundo, Color color = new Color(), Vector2 escala = new Vector2(), SpriteEffects efecto = SpriteEffects.None, float anguloGiro = 0, Vector2 origenGiro = new Vector2())
            : base(rutaContenido, posicion, color, escala, efecto, anguloGiro, origenGiro)
        {
            this.__TamCuadros = new Point(tamCuadros.X, tamCuadros.Y);
            this.__NumCuadros = numCuadros;
            this.__CuadrosPorSegundo = cuadrosPorSegundo;
            this.OrigenGiro = origenGiro;
            this.AnguloGiro = anguloGiro;
            this.__CuadroActual = new Point(0, 0);
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
        /// Actualiza el fotograma actual.
        /// </summary>
        /// <param name="tiempoJuego">Tiempo del juego.</param>
        /// <param name="graficos">Dimensión de ventana.</param>
        /// <param name="teclado">Estado actual del teclado.</param>
        public override void Actualizar(GameTime tiempoJuego, Viewport graficos, KeyboardState teclado)
        {
            TiempoJuego += tiempoJuego.ElapsedGameTime.Milliseconds;
            if (TiempoJuego >= 1000 / CuadrosPorSegundo)
            {
                TiempoJuego %= 1000 / CuadrosPorSegundo;

                __CuadroActual.X++;
                if (__CuadroActual.X >= __NumCuadros.X) { __CuadroActual.X = 0; __CuadroActual.Y++; }
                if (__CuadroActual.Y >= __NumCuadros.Y) { __CuadroActual.X = __CuadroActual.Y = 0; }
            }

            base.Actualizar(tiempoJuego, graficos, teclado);
        }

        /// <summary>
        /// Dibuja el fotograma actual.
        /// </summary>
        /// <param name="lotesDuende">SpriteBatch.</param>
        /// <param name="tiempoJuego">Tiempo del juego.</param>
        public override void Dibujar(SpriteBatch lotesDuende, GameTime tiempoJuego)
        {
            lotesDuende.Draw(textura, Posicion, new Rectangle(TamCuadros.X * CuadroActual.X, TamCuadros.Y * CuadroActual.Y, TamCuadros.X, TamCuadros.Y), Color, AnguloGiro, OrigenGiro, Escala, Efecto, 1f);
            
            base.Dibujar(lotesDuende, tiempoJuego);
        }
    }
}
