using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PhysicXNA.Nucleo
{
    /// <summary>Nivel genérico del juego.</summary>
    public abstract class Nivel : DrawableGameComponent
    {
        /// <summary>Admnistrador de contenidos.</summary>
        protected ContentManager Content;
        /// <summary>Motor de dibujo.</summary>
        protected SpriteBatch spriteBatch;
        /// <summary>Estado del teclado, debería ser refrescado al inicio de cada Update.</summary>
        protected KeyboardState estadoTeclado;
        /// <summary>Estado del teclado en el Update anterior refrescado automáticamente.</summary>
        protected KeyboardState estadoTecladoAnt;
        /// <summary>Estado del ratón, debería ser refrescado al inicio de cada Update.</summary>
        protected MouseState estadoRaton;
        /// <summary>Estado del ratón en el Update anterior refrescado automáticamente.</summary>
        protected MouseState estadoRatonAnt;
        /// <summary>Tiempo utilizado con la intención de ejecutar los elementos del juego a una cierta velocidad.</summary>
        protected int tiempoJuego;
        /// <summary>Velocidad de actualización de elementos genéricos del nivel.</summary>
        protected int cuadrosPorSegundo;
        /// <summary>Resolución de pantalla.</summary>
        public Viewport resolucionPantalla;

        /// <summary>Arreglo que contiene en cada elemento una página de las reglas de este nivel.</summary>
        public String[] reglas;
        /// <summary>Configuración del juego.</summary>
        public SistemaPerfiles.OpcionesJuego opciones;
        /// <summary>Indica si el nivel ya se ha completado.</summary>
        public bool nivelCompletado;
        /// <summary>Indica si se está reproduciendo el nivel actualmente en lugar de jugar el nivel.</summary>
        public bool reproduciendoVideo;
        /// <summary>Contador que inicia desde que inicial el nivel.</summary>
        public TimeSpan tiempoCompletado;
        private String rutaMusica;
        private String rutaVideo;
        /// <summary>Canción del nivel.</summary>
        public Song cancionNivel;
        /// <summary>Video introductorio a cada nivel qeu se reproduce al principio.</summary>
        public Video videoIntro;
        private VideoPlayer reproductorVideo;
        internal DuendeEstatico botonPausa;
        internal DuendeEstatico botonAyuda;
        /// <summary>Escenario al que será redirido el jugador cuando clickeé los botones de acción.</summary>
        public int nuevoEscenario;
        /// <summary>Fondo del excenario.</summary>
        protected List<DuendeEstatico> dibujarAntes;

        /// <summary>
        /// Constructor genérico de nivel.
        /// </summary>
        /// <param name="juego">Juego en que será mostrado el nivel.</param>
        /// <param name="rutaMusica">Ruta del archivo de música que será reproducido en el nivel.</param>
        /// <param name="rutaVideo">Ruta del archivo de video de introducción al nivel.</param>
        /// <param name="cuadrosPorSegundo"><seealso cref="cuadrosPorSegundo"/></param>
        public Nivel(Game juego, String rutaMusica, String rutaVideo, int cuadrosPorSegundo)
            : base(juego)
        {
            Content = juego.Content;
            this.rutaMusica = rutaMusica;
            this.rutaVideo = rutaVideo;
            this.cuadrosPorSegundo = cuadrosPorSegundo;
            this.resolucionPantalla = juego.GraphicsDevice.Viewport;
            this.reglas = new String[3];
            botonPausa = new DuendeEstatico("Recursos/pausa", Vector2.Zero, escala: new Vector2(0.2f));
            botonAyuda = new DuendeEstatico("Recursos/ayuda", Vector2.Zero, escala: new Vector2(0.2f));
            Enabled = Visible = false;
            nuevoEscenario = 1;
            dibujarAntes = new List<DuendeEstatico>();
            reproductorVideo = new VideoPlayer();
        }

        /// <summary>
        /// Reinicia todos los elementos del nivel.
        /// </summary>
        virtual public void Inicializar()
        {
            tiempoCompletado = new TimeSpan();
            tiempoCompletado = new TimeSpan();
            estadoTecladoAnt = new KeyboardState();
            estadoRatonAnt = new MouseState();
            nivelCompletado = false;
            if (reproduciendoVideo) reproductorVideo.Play(videoIntro);

            dibujarAntes.Clear();
            botonPausa.Posicion = new Vector2((float)resolucionPantalla.Width - botonPausa.Ancho - 10, 10);
            botonAyuda.Posicion = new Vector2(botonPausa.Posicion.X - botonAyuda.Ancho - 10, 10);
        }

        /// <summary>
        /// Carga el contenido de los elementos del nivel.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            cancionNivel = Content.Load<Song>(rutaMusica);
            videoIntro = Content.Load<Video>(rutaVideo);
            botonPausa.CargarContenido(Content);
            botonAyuda.CargarContenido(Content);
            base.LoadContent();
        }

        /// <summary>
        /// Libera los recursos ocupados por los elementos del nivel.
        /// </summary>
        protected override void UnloadContent()
        {
            cancionNivel.Dispose();
            botonPausa.LiberarContenido();
            botonAyuda.LiberarContenido();
            reproductorVideo.Dispose();
            base.UnloadContent();
        }

        /// <summary>
        /// Actualiza los elementos del nivel.
        /// </summary>
        /// <param name="gameTime">Tiempo del juego.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            estadoTeclado = Keyboard.GetState();
            estadoRaton = Mouse.GetState();

            if (reproduciendoVideo)
            {
                if(reproductorVideo.State == MediaState.Stopped || (opciones.invertir_mouse == 0 ?
                    estadoRaton.LeftButton == ButtonState.Released && estadoRatonAnt.LeftButton == ButtonState.Pressed :
                    estadoRaton.RightButton == ButtonState.Released && estadoRatonAnt.RightButton == ButtonState.Pressed))
                {
                    reproductorVideo.Stop();
                    reproduciendoVideo = false;
                    MediaPlayer.Play(cancionNivel);
                    if (opciones.musica == 0) MediaPlayer.Pause();
                }

                estadoTecladoAnt = estadoTeclado;
                estadoRatonAnt = estadoRaton;
                return;
            }

            tiempoCompletado += gameTime.ElapsedGameTime;
            tiempoJuego += gameTime.ElapsedGameTime.Milliseconds;

            if ((opciones.invertir_mouse == 0 ? estadoRaton.LeftButton == ButtonState.Pressed && estadoRatonAnt.LeftButton == ButtonState.Released : estadoRaton.RightButton == ButtonState.Pressed && estadoRatonAnt.RightButton == ButtonState.Released)
                && estadoRaton.X > botonPausa.Posicion.X && estadoRaton.Y < botonPausa.Alto)
            {
                estadoRaton = new MouseState(estadoRaton.X, estadoRaton.Y, estadoRaton.ScrollWheelValue, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released);
                nuevoEscenario = 1;
                Enabled = Visible = false;
            }

            if ((opciones.invertir_mouse == 0 ? estadoRaton.LeftButton == ButtonState.Pressed && estadoRatonAnt.LeftButton == ButtonState.Released : estadoRaton.RightButton == ButtonState.Pressed && estadoRatonAnt.RightButton == ButtonState.Released) 
                && estadoRaton.X > botonAyuda.Posicion.X && estadoRaton.Y < botonAyuda.Alto)
            {
                estadoRaton = new MouseState(estadoRaton.X, estadoRaton.Y, estadoRaton.ScrollWheelValue, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released);
                nuevoEscenario = (int)MenusJuego.EscenarioMenu.ReglasNivel;
                Enabled = Visible = false;
            }

            if (!estadoTeclado.IsKeyDown(Keys.Escape) && estadoTecladoAnt.IsKeyDown(Keys.Escape))
            {
                nuevoEscenario = 1;
                Enabled = Visible = false;
            }
        }

        /// <summary>
        /// Dibuja los elementos del nivel.
        /// </summary>
        /// <param name="gameTime">Tiempo del juego.</param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Begin();
            if (reproduciendoVideo)
            {
                spriteBatch.Draw(reproductorVideo.GetTexture(), resolucionPantalla.Bounds, Color.White);
                spriteBatch.End();
                return;
            }
            foreach (DuendeEstatico objetoDibujar in dibujarAntes) objetoDibujar.Dibujar(spriteBatch, gameTime);
            botonPausa.Dibujar(spriteBatch, gameTime);
            botonAyuda.Dibujar(spriteBatch, gameTime);
        }
    }
}
