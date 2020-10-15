using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PhysicXNA.Nucleo
{
    /// <summary>Personaje animado en niveles 2D estilo plataforma.</summary>
    public class PersonajeOriginal
    {
        private SoundEffect salta;

        DuendeAnimado caminando;
        DuendeEstatico parado;
        DuendeEstatico saltando;
        bool estaCaminando = false;
        bool IA;
        /// <summary><seealso cref="Nivel.opciones"/></summary>
        //public SistemaPerfiles.OpcionesJuego opciones;

        private float velocidad;
        /// <summary>Velocidad de movimiento del personaje en el eje X</summary>
        public float Velocidad
        {
            get { return velocidad; }
            set { velocidad = value; }
        }

        private double velocidadY;
        /// <summary>Velocidad de movimiento del personaje en el eje Y (útil para los saltos)</summary>
        public double VelocidadY
        {
            get { return velocidadY; }
            set { velocidadY = value; }
        }

        private bool salto;
        /// <summary>Variable booleana para activar la gravedad en el salto</summary>
        public bool Salto
        {
            get { return salto; }
            set { salto = value; }
        }

        private double gravedad;
        /// <summary>Aceleración de la gravedad del campo de juego.</summary>
        public double Gravedad
        {
            get { return gravedad; }
            set { gravedad = value; }
        }

        private int pixelesxmetro;
        /// <summary>Relación entre pixeles y metro.</summary>
        public int PixelesXMetro
        {
            get { return pixelesxmetro; }
            set { pixelesxmetro = value; }
        }

        /// <summary>Posición del personaje en la pantalla.</summary>
        public Vector2 Posicion
        {
            get { return caminando.Posicion; }
            set
            {
                caminando.Posicion = saltando.Posicion = parado.Posicion = value;
            }
        }

        /// <summary>Obtiene el ancho del personaje.</summary>
        public int Ancho
        {
            get { return caminando.Ancho; }
        }

        /// <summary>Obtiene el alto del personaje.</summary>
        public int Alto
        {
            get { return caminando.Alto; }
        }

        /// <summary>
        /// Crea una instancia de un personaje.
        /// </summary>
        /// <param name="posicion"><seealso cref="Posicion"/></param>
        /// <param name="cuadrosPorSegundo"><seealso cref="DuendeAnimado.CuadrosPorSegundo"/></param>
        /// <param name="color"><seealso cref="DuendeBasico.Color"/></param>
        /// <param name="escala"><seealso cref="DuendeBasico.Escala"/></param>
        /// <param name="IA">Indica si el personaje se trata de un enemigo.</param>
        public PersonajeOriginal(Vector2 posicion, int cuadrosPorSegundo = 4, Color color = new Color(),
            Vector2 escala = new Vector2(), bool IA = false)
        {
            if (!IA)
            {
                caminando = new DuendeAnimado("Recursos/monito", posicion, new Point(65, 100), new Point(12, 1), cuadrosPorSegundo, Color.White, new Vector2(1f));
                saltando = new DuendeEstatico("Recursos/brincando", Vector2.Zero, Color.White, new Vector2(1f));
                parado = new DuendeEstatico("Recursos/parado", Vector2.Zero, Color.White, new Vector2(1f));
            }
            else
            {
                caminando = new DuendeAnimado("Recursos/monitoMalo", posicion, new Point(65, 100), new Point(12, 1), cuadrosPorSegundo, new Color(250,159,159,255), new Vector2(1f));
                saltando = new DuendeEstatico("Recursos/brincandoMalo", Vector2.Zero, new Color(250, 159, 159, 255), new Vector2(1f));
                parado = new DuendeEstatico("Recursos/paradoMalo", Vector2.Zero, new Color(250, 159, 159, 255), new Vector2(1f));
            }

            this.IA = IA;
            velocidad = 4.5f;
            velocidadY = 0;
            salto = false;
            gravedad = 9.8;
            pixelesxmetro = 100;
        }

        /// <summary>Reinicia el personaje.</summary>
        public void Inicializar()
        {
            velocidadY = 0;
            vi = 0;
            salto = false;
        }

        /// <summary>
        /// Carga los elementos multimedia para el personaje.
        /// </summary>
        /// <param name="contenido">Administrador de contenido.</param>
        public void CargarContenido(ContentManager contenido)
        {
            salta = contenido.Load<SoundEffect>("Sonidos/Salto");
            caminando.CargarContenido(contenido);
            saltando.CargarContenido(contenido);
            parado.CargarContenido(contenido);
        }

        /// <summary>
        /// Libera los recursos especiales ocupados por el juego.
        /// </summary>
        public void LiberarContenido()
        {
            salta.Dispose();
            caminando.LiberarContenido();
            saltando.LiberarContenido();
            parado.LiberarContenido();
        }

        internal long tiempoTranscurrido;
        internal double vi;
        int dy;

        int cumbre = 0xFFFF;
        int? enPlataforma = null;

        /// <summary>
        /// Actualiza el estado del personaje.
        /// </summary>
        /// <param name="tiempoJuego">Tiempo del juego.</param>
        /// <param name="graficos">dimensiones de pantalla.</param>
        /// <param name="teclado">Estado del teclado.</param>
        /// <param name="opciones"><seealso cref="Nivel.opciones"/></param>
        /// <param name="nivelOrigen">Nivel al que pertenece este personaje.</param>
        /// <param name="posicionSeguir">Posición del personaje a seguir en caso de que sea una IA.</param>
        public void Actualizar(GameTime tiempoJuego, Viewport graficos, KeyboardState teclado, SistemaPerfiles.OpcionesJuego opciones, Niveles.Nivel1.Nivel1 nivelOrigen = null, Vector2 posicionSeguir = new Vector2())
        {
            if ((!IA ? (opciones.wasd == 0 ? teclado.IsKeyDown(Keys.Up) : teclado.IsKeyDown(Keys.W)) :
                (Posicion.X > (posicionSeguir.X - Velocidad * 2) && Posicion.X < (posicionSeguir.X + Velocidad * 2) && Posicion.Y > posicionSeguir.Y)) && !salto)
            {
                vi = Math.Sqrt(((double)(graficos.Height - 75) / 3) / pixelesxmetro * 2 * gravedad);
                tiempoTranscurrido = 0;
                salto = true;
                if (opciones.sonidos != 0) salta.Play(0.4f, -0.5f * (IA ? 2 : 1), -0.28f);
            }
            else if ((!IA ? (opciones.wasd == 0 ? teclado.IsKeyDown(Keys.Down) : teclado.IsKeyDown(Keys.S)) :
                (Posicion.X > (posicionSeguir.X - Velocidad * 2) && Posicion.X < (posicionSeguir.X + Velocidad * 2) && Posicion.Y < posicionSeguir.Y)) && !salto)
            {
                vi = 0;
                salto = true;
                tiempoTranscurrido = 0;
            }
            if (!IA ? (opciones.wasd == 0 ? teclado.IsKeyDown(Keys.Right) : teclado.IsKeyDown(Keys.D)) : 
                (posicionSeguir.X > Posicion.X + Velocidad))
            {
                Posicion = new Vector2(Posicion.X + velocidad, Posicion.Y);
                Posicion = new Vector2(Posicion.X + Ancho > graficos.Width ? graficos.Width - Ancho : Posicion.X, Posicion.Y);
                caminando.Efecto = saltando.Efecto = parado.Efecto = SpriteEffects.None;
                estaCaminando = true;
            }
            else if (!IA ? (opciones.wasd == 0 ? teclado.IsKeyDown(Keys.Left) : teclado.IsKeyDown(Keys.A)) :
                (posicionSeguir.X < Posicion.X - Velocidad))
            {
                Posicion = new Vector2(Posicion.X - velocidad, Posicion.Y);
                Posicion = new Vector2(Posicion.X < 0 ? 0 : Posicion.X, Posicion.Y);
                caminando.Efecto = saltando.Efecto = parado.Efecto = SpriteEffects.FlipHorizontally;
                estaCaminando = true;
            }
            else estaCaminando = false;

            if (Salto)
            {
                tiempoTranscurrido += tiempoJuego.ElapsedGameTime.Milliseconds;
                velocidadY = vi - gravedad * (tiempoTranscurrido / 1000f);
                dy = (int)(velocidadY * (tiempoJuego.ElapsedGameTime.Milliseconds / 1000f) * pixelesxmetro);
                //ny = (int)((velocidadY * (tiempoTranscurrido / 1000f) - (Gravedad / 2) * (tiempoTranscurrido / 1000f) * (tiempoTranscurrido / 1000f)) * pixelesxmetro);
                Posicion = new Vector2(Posicion.X, Posicion.Y - dy);
                if (Posicion.Y >= graficos.Height - Alto)
                {
                    Posicion = new Vector2(Posicion.X, graficos.Height - caminando.Alto);
                    salto = false;
                }
            }


            if (Salto) saltando.Actualizar(tiempoJuego, graficos, teclado);
            else
            {
                if (estaCaminando) caminando.Actualizar(tiempoJuego, graficos, teclado);
                else caminando.CuadroActual = Point.Zero;
            }

            if (nivelOrigen != null)
            {
                if (Salto && VelocidadY < 0)
                {
                    if (Posicion.Y + Alto < cumbre) cumbre = (int)Posicion.Y + Alto;
                    Rectangle bordesMonito = new Rectangle((int)Posicion.X, (int)Posicion.Y, Ancho, Alto);
                    for (int c = 0; c < nivelOrigen.plataformas.Length; c++)
                    {
                        if (cumbre < nivelOrigen.plataformas[c].Posicion.Y && Posicion.Y - nivelOrigen.plataformas[c].Posicion.Y + Alto < -dy + 1)
                        //if (cumbre < nivelOrigen.plataformas[c].Posicion.Y)
                        {
                            if (new Rectangle((int)nivelOrigen.plataformas[c].Posicion.X, (int)nivelOrigen.plataformas[c].Posicion.Y, nivelOrigen.plataformas[c].Ancho, nivelOrigen.plataformas[c].Alto).Intersects(bordesMonito))
                            {
                                Salto = false;
                                vi = 0;
                                tiempoTranscurrido = 0;
                                VelocidadY = 0;
                                Posicion = new Vector2(Posicion.X, nivelOrigen.plataformas[c].Posicion.Y - Alto + 1);
                                enPlataforma = c;
                            }
                        }
                    }
                }
                else cumbre = 0xFFFF;
                if (!Salto && enPlataforma != null)
                {
                    Rectangle bordesMonito = new Rectangle((int)Posicion.X, (int)Posicion.Y, Ancho, Alto);
                    if (!new Rectangle((int)nivelOrigen.plataformas[(int)enPlataforma].Posicion.X, (int)nivelOrigen.plataformas[(int)enPlataforma].Posicion.Y, nivelOrigen.plataformas[(int)enPlataforma].Ancho, nivelOrigen.plataformas[(int)enPlataforma].Alto).Intersects(bordesMonito))
                    {
                        enPlataforma = null;
                    }
                    if (enPlataforma == null)
                    {
                        Salto = true;
                    }
                }
            }
        }

        /// <summary>
        /// Dibuja al personaje.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch.</param>
        /// <param name="gameTime">Tiempo del juego.</param>
        public void Dibujar(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (salto) saltando.Dibujar(spriteBatch, gameTime);
            else
            {
                if (estaCaminando) caminando.Dibujar(spriteBatch, gameTime);
                else parado.Dibujar(spriteBatch, gameTime);
            }
        }
    }
}
