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
    /// <summary>Clase que contiene una configuración de fuente y un texto a ser mostrado en pantalla con efecto 3D.</summary>
    public class Texto3D
    {
        private SpriteFont fuente;
        private String rutaContenido;

        private String __Texto;
        /// <summary>Regresa el tamaño de la fuente.</summary>
        public Vector2 TamTexto
        {
            get { return fuente.MeasureString(__Texto) * Escala; }
        }

        /// <summary>Regresa el tamaño original de la fuente sin aplicar la escala.</summary>
        public Vector2 TamRealTexto
        {
            get { return fuente.MeasureString(__Texto); }
        }

        /// <summary>Texto a mostrar.</summary>
        public String Texto
        {
            get { return __Texto; }
            set { __Texto = value; }
        }

        private Vector2 __Posicion;
        /// <summary>Posición en pantalla del texto.</summary>
        public Vector2 Posicion
        {
            get { return __Posicion; }
            set { __Posicion = value; }
        }

        private Color __Color;
        /// <summary>Color del texto.</summary>
        public Color Color
        {
            get { return __Color; }
            set { __Color = colorInicio = value; }
        }

        private int __Profundidad;
        /// <summary>Profundidad del efecto 3D del texto.</summary>
        public int Profundidad
        {
            get { return __Profundidad; }
            set { __Profundidad = value; }
        }

        private float __AnguloGiro;
        /// <summary>Ángulo de giro en radianes en sentido anti-horario.</summary>
        public float AnguloGiro
        {
            get { return __AnguloGiro; }
            set { __AnguloGiro = value; }
        }

        private Vector2 __OrigenGiro;
        /// <summary>Origen de giro del texto.</summary>
        public Vector2 OrigenGiro
        {
            get { return __OrigenGiro; }
            set { __OrigenGiro = value; }
        }

        private SpriteEffects __Efecto;
        /// <summary>Efecto del texto. Véase <see cref="SpriteEffects"/></summary>
        public SpriteEffects Efecto
        {
            get { return __Efecto; }
            set { __Efecto = value; }
        }

        private Vector2 __Escala;
        /// <summary>Escala con que se dibujará el texto.</summary>
        public Vector2 Escala
        {
            get { return __Escala; }
            set { __Escala = escalaInicio = value; }
        }

        private int __CuadrosPorSegundo;
        /// <summary>Velocidad de actualización del texto en cuadros por segundo.</summary>
        public int CuadrosPorSegundo
        {
            get { return __CuadrosPorSegundo; }
        }


        private int c, tiempoFotograma;
        private bool incremento;
        private Color colorInicio;
        private Vector2 escalaInicio;


        /// <summary>
        /// Crea una instancia de esta clase a partir de todas las propiedades de la fuente y del texto.
        /// </summary>
        /// <param name="rutaContenido">Ruta de la configuración de la fuente del texto.</param>
        /// <param name="texto"><seealso cref="Texto"/></param>
        /// <param name="posicion"><seealso cref="Posicion"/></param>
        /// <param name="cuadrosPorSegundo"></param>
        /// <param name="color"><seealso cref="Color"/></param>
        /// <param name="profundidad"><seealso cref="Profundidad"/></param>
        /// <param name="escala"></param>
        /// <param name="efecto"></param>
        /// <param name="anguloGiro"></param>
        /// <param name="origenGiro"></param>
        public Texto3D(String rutaContenido, String texto, Vector2 posicion, int cuadrosPorSegundo, Color color = new Color(), int profundidad = 5, Vector2 escala = new Vector2(), SpriteEffects efecto = SpriteEffects.None, float anguloGiro = 0, Vector2 origenGiro = new Vector2())
        {
            this.rutaContenido = rutaContenido;
            this.Texto = texto;
            this.Posicion = posicion;
            this.__CuadrosPorSegundo = cuadrosPorSegundo;
            this.Profundidad = profundidad;
            if (color == Color.Transparent)
                this.Color = Color.White;
            else
                this.Color = color;
            this.Escala = escala == Vector2.Zero ? new Vector2(1f) : escala;
            this.Efecto = efecto;
            this.AnguloGiro = anguloGiro;
            this.OrigenGiro = origenGiro;
            tiempoFotograma = 0;
            incremento = false;
            colorInicio = color;
        }

        /// <summary>
        /// Reinicia el color y la escala del texto a sus valores iniciales.
        /// </summary>
        public void Reiniciar()
        {
            __Color = colorInicio;
            __Escala = escalaInicio;
        }

        /// <summary>
        /// Carga el contenido a partir de la ruta dada en la contrucción del objeto de esta clase.
        /// </summary>
        /// <param name="contenido">Administrador de contenidos del juego para cargar el contenido.</param>
        public void CargarContenido(ContentManager contenido)
        {
            fuente = contenido.Load<SpriteFont>(rutaContenido);
        }

        /// <summary>Libera los recursos utilizados por esa clase.</summary>
        public void LiberarContenido()
        {
            if (fuente != null)
            {
                fuente = null;
            }
        }


        /// <summary>
        /// Actualiza la animación del texto.
        /// </summary>
        /// <param name="tiempoJuego">Tiempo del juego.</param>
        public void Actualizar(GameTime tiempoJuego)
        {
            tiempoFotograma += tiempoJuego.ElapsedGameTime.Milliseconds;
            if (tiempoFotograma >= 1000 / CuadrosPorSegundo)
            {
                tiempoFotograma %= 1000 / CuadrosPorSegundo;
                if (colorInicio == Color.White) return;
                if (incremento)
                {
                    __Color = new Color(__Color.R + 10, __Color.G + 10, __Color.B + 10);
                    if (__Color == Color.White) incremento = false;
                }
                else
                {
                    __Color = new Color(__Color.R - 10, __Color.G - 10, __Color.B - 10);
                    if (__Color.R < colorInicio.R) __Color.R = colorInicio.R;
                    if (__Color.G < colorInicio.G) __Color.G = colorInicio.G;
                    if (__Color.B < colorInicio.B) __Color.B = colorInicio.B;
                    if (__Color == colorInicio) incremento = true;
                }

                __Escala = escalaInicio + new Vector2(-0.1f + 0.2f * (
                    (colorInicio.R == 255 ? 0 : (__Color.R - colorInicio.R) / (255f - colorInicio.R)) +
                    (colorInicio.G == 255 ? 0 : (__Color.G - colorInicio.G) / (255f - colorInicio.G)) +
                    (colorInicio.B == 255 ? 0 : (__Color.B - colorInicio.B) / (255f - colorInicio.B))) /
                    (3f - (colorInicio.R == 255 ? 1 : 0) - (colorInicio.G == 255 ? 1 : 0) - (colorInicio.B == 255 ? 1 : 0)));
            }
        }


        /// <summary>
        /// Dibuja la textura.
        /// </summary>
        /// <param name="lotesDuende">SpriteBatch.</param>
        /// <param name="tiempoJuego">Tiempo del juego.</param>
        public void Dibujar(SpriteBatch lotesDuende, GameTime tiempoJuego)
        {
            for(c = 0; c < Profundidad; c++)
                lotesDuende.DrawString(fuente, Texto, new Vector2(Posicion.X - c, Posicion.Y - c), Color.Black, AnguloGiro, OrigenGiro, Escala, Efecto, 1f);
            lotesDuende.DrawString(fuente, Texto, Posicion, Color, AnguloGiro, OrigenGiro, Escala, Efecto, 1f);
        }
    }
}
