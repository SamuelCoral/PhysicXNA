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
    /// <summary>Clase que contiene una textura con propiedades y métodos para ser dibujada.</summary>
    public abstract class DuendeBasico
    {
        /// <summary>Textura cargada a partir de un archivo de imagen.</summary>
        protected Texture2D textura;

        /// <summary>Ancho de la textura como se mostrará en la pantalla.</summary>
        public virtual int Ancho
        {
            get { return (int)(textura.Width * Escala.X); }
        }

        /// <summary>Alto de la textura como se mostrará en la pantalla.</summary>
        public virtual int Alto
        {
            get { return (int)(textura.Height * Escala.Y); }
        }

        /// <summary>Ancho real de la imagen (sin aplicar la escala).</summary>
        public virtual int AnchoReal
        {
            get { return textura.Width; }
        }

        /// <summary>Alto real de la imagen (sin aplicar la escala).</summary>
        public virtual int AltoReal
        {
            get { return textura.Height; }
        }

        /// <summary><seealso cref="RutaContenido"/></summary>
        protected string __RutaContenido;
        /// <summary>Ruta de donde se intentó cargar la imagen.</summary>
        public string RutaContenido
        {
            get { return __RutaContenido; }
        }

        /// <summary><seealso cref="Posicion"/></summary>
        protected Vector2 __Posicion;
        /// <summary>Posición en pantalla de la imagen.</summary>
        public Vector2 Posicion
        {
            get { return __Posicion; }
            set { __Posicion = value; }
        }

        /// <summary><seealso cref="Color"/></summary>
        protected Color __Color;
        /// <summary>Color de filtro de la imagen.</summary>
        public Color Color
        {
            get { return __Color; }
            set { __Color = value; }
        }

        /// <summary><seealso cref="AnguloGiro"/></summary>
        protected float __AnguloGiro;
        /// <summary>Ángulo de giro en radianes en sentido horario.</summary>
        public float AnguloGiro
        {
            get { return __AnguloGiro; }
            set { __AnguloGiro = value; }
        }

        /// <summary><seealso cref="OrigenGiro"/></summary>
        protected Vector2 __OrigenGiro;
        /// <summary>Origen de giro del fragmento de imagen a mostrar.</summary>
        public Vector2 OrigenGiro
        {
            get { return __OrigenGiro; }
            set { __OrigenGiro = value; }
        }

        /// <summary><seealso cref="Efecto"/></summary>
        protected SpriteEffects __Efecto;
        /// <summary>Efecto de imagen. Véase <see cref="SpriteEffects"/></summary>
        public SpriteEffects Efecto
        {
            get { return __Efecto; }
            set { __Efecto = value; }
        }

        /// <summary><seealso cref="Escala"/></summary>
        protected Vector2 __Escala;
        /// <summary>Escala con que se dibujará la imagen.</summary>
        public Vector2 Escala
        {
            get { return __Escala; }
            set { __Escala = value; }
        }

        /// <summary>
        /// Crea una instancia de esta clase dadas todas las propiedades de la imagen.
        /// </summary>
        /// <param name="rutaContenido"><seealso cref="RutaContenido"/></param>
        /// <param name="posicion"><seealso cref="Posicion"/></param>
        /// <param name="color"><seealso cref="Color"/></param>
        /// <param name="escala"><seealso cref="Escala"/></param>
        /// <param name="efecto"><seealso cref="Efecto"/></param>
        /// <param name="anguloGiro"><seealso cref="AnguloGiro"/></param>
        /// <param name="origenGiro"><seealso cref="OrigenGiro"/></param>
        public DuendeBasico(string rutaContenido, Vector2 posicion, Color color = new Color(), Vector2 escala = new Vector2(), SpriteEffects efecto = SpriteEffects.None, float anguloGiro = 0, Vector2 origenGiro = new Vector2())
        {
            this.__RutaContenido = rutaContenido;
            this.Posicion = posicion;
            if (color == Color.Transparent)
                this.Color = Color.White;
            else
                this.Color = color;
            this.AnguloGiro = anguloGiro;
            this.OrigenGiro = origenGiro;
            this.Efecto = efecto;
            if (escala == Vector2.Zero)
                this.Escala = new Vector2(1, 1);
            else
                this.Escala = escala;
        }

        /// <summary>
        /// Carga el contenido a partir de la ruta dada en la contrucción del objeto de esta clase.
        /// </summary>
        /// <param name="contenido">Administrador de contenidos del juego para cargar el contenido.</param>
        abstract public void CargarContenido(ContentManager contenido);

        /// <summary>Libera los recursos utilizados por esa clase.</summary>
        virtual public void LiberarContenido()
        {
            if (textura != null)
            {
                textura.Dispose();
                textura = null;
            }
        }

        /// <summary>
        /// Actualiza la textura a mostrar.
        /// </summary>
        /// <param name="tiempoJuego">Tiempo del juego.</param>
        /// <param name="graficos">Dimensión de ventana.</param>
        /// <param name="teclado">Estado actual del teclado.</param>
        virtual public void Actualizar(GameTime tiempoJuego, Viewport graficos, KeyboardState teclado)
        {

        }

        /// <summary>
        /// Dibuja la textura.
        /// </summary>
        /// <param name="lotesDuende">SpriteBatch.</param>
        /// <param name="tiempoJuego">Tiempo del juego.</param>
        virtual public void Dibujar(SpriteBatch lotesDuende, GameTime tiempoJuego)
        {

        }
    }
}
