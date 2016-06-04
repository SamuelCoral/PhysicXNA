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
using PhysicXNA;
using PhysicXNA.SistemaPerfiles;
using PhysicXNA.MenusJuego;

namespace PhysicXNA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        ListaPerfilesJugador perfiles;

        Menu menu;
        Nucleo.Nivel[] niveles;
        Random aleatorio;
        bool menuMostrado, menuMostradoAnt, juegoMostrado, juegoMostradoAnt;

        /// <summary>Constructor del juego.</summary><remarks>Aquí es donde deberíamos comenzar a analizar el programa.</remarks>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();
            menuMostradoAnt = true;
            aleatorio = new Random();
            
            perfiles = new ListaPerfilesJugador(ListaPerfilesJugador.rutaPerfiles);

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            menu = new Menu(this, perfiles);
            niveles = new Nucleo.Nivel[ListaPerfilesJugador.numeroNiveles + 1];
            niveles[0] = new Niveles.Nivel1.Nivel1(this, "Canciones/On Cloud Nine", "Videos/Nivel 1", 20);
            niveles[1] = new Niveles.Nivel2.Nivel2(this, "Canciones/Remains", "Videos/Nivel 2", 20);
            niveles[2] = new Niveles.Nivel3.Nivel3(this, "Canciones/That Break", "Videos/Nivel 3", 20);
            niveles[3] = new Niveles.Nivel4.Nivel4(this, "Canciones/Micro Dreams", "Videos/Nivel 4", 20);
            niveles[4] = new Niveles.Nivel5.Nivel5(this, "Canciones/Lost In Hyperspace", "Videos/Nivel 5", 20);
            niveles[5] = new Niveles.Creditos(this, "Canciones/Lost In Hyperspace", "Videos/Creditos", 20);
            
            foreach(Nucleo.Nivel nivel in niveles) this.Components.Add(nivel);
            this.Components.Add(menu);
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            MediaPlayer.IsRepeating = true;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            perfiles.Dispose();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            menuMostrado = menu.Enabled;
            if (menu.nivelEjecutandose != null)
            {
                juegoMostrado = niveles[(int)menu.nivelEjecutandose].Enabled;
                if (!juegoMostrado && juegoMostradoAnt)
                {
                    menu.Enabled = menu.Visible = true;
                    menu.escenario = niveles[(int)menu.nivelEjecutandose].nuevoEscenario;
                    niveles[(int)menu.nivelEjecutandose].Visible = true;
                    menu.reproductorVideo.Resume();
                    if (perfiles[menu.perfilSeleccionado].opciones.musica != 0) MediaPlayer.Pause();
                }

                if (!menuMostrado && menuMostradoAnt)
                {
                    if (menu.escenario == (int)EscenarioMenu.ReiniciarNivel)
                    {
                        MediaPlayer.Stop();
                        niveles[(int)menu.nivelEjecutandose].Inicializar();
                        MediaPlayer.Play(niveles[(int)menu.nivelEjecutandose].cancionNivel);
                        if (perfiles[menu.perfilSeleccionado].opciones.musica == 0) MediaPlayer.Pause();
                        menu.escenario = 1;
                    }

                    niveles[(int)menu.nivelEjecutandose].Enabled = niveles[(int)menu.nivelEjecutandose].Visible = true;
                    if (perfiles[menu.perfilSeleccionado].opciones.musica != 0) MediaPlayer.Resume();

                    if (menu.escenario == (int)EscenarioMenu.SalirNivel)
                    {
                        menu.Enabled = menu.Visible = true;
                        menu.reproductorVideo.Resume();
                        menuMostrado = true;
                        niveles[(int)menu.nivelEjecutandose].Enabled = niveles[(int)menu.nivelEjecutandose].Visible = false;
                        menu.nivelEjecutandose = null;
                        menu.escenario = 1;
                        MediaPlayer.Stop();
                        MediaPlayer.Play(menu.cancion);
                        if (perfiles[menu.perfilSeleccionado].opciones.musica == 0) MediaPlayer.Pause();
                    }
                }
            }

            if (!menuMostrado && menuMostradoAnt && menu.nivelEjecutandose == null)
            {
                OpcionesJuego opciones = perfiles[menu.perfilSeleccionado].opciones;
                int nivelJugar = menu.mostrarCreditos ? 5 : !menu.jugarHistoria ? menu.botones[6][1].valor : menu.perfiles[menu.perfilSeleccionado].opciones.nivel;
                niveles[nivelJugar].Enabled = niveles[nivelJugar].Visible = true;
                niveles[nivelJugar].resolucionPantalla = GraphicsDevice.Viewport;
                niveles[nivelJugar].opciones = opciones;
                niveles[nivelJugar].reproduciendoVideo = menu.jugarHistoria ? true : menu.botones[6][2].valor != 0;
                niveles[nivelJugar].Inicializar();
                MediaPlayer.Stop();
                if (!niveles[nivelJugar].reproduciendoVideo)
                {
                    MediaPlayer.Play(niveles[nivelJugar].cancionNivel);
                    if (opciones.musica == 0) MediaPlayer.Pause();
                }
                menu.reglasNivel = niveles[nivelJugar].reglas;
                menu.textoSabiasQue = SabiasQue.mensajes[aleatorio.Next(SabiasQue.mensajes.Length)];
                menu.nivelEjecutandose = nivelJugar;
                juegoMostradoAnt = false;
            }

            if (menu.escenario == (int)EscenarioMenu.CargarPerfil)
            {
                graphics.PreferredBackBufferWidth = perfiles[menu.perfilSeleccionado].opciones.res_horizontal;
                graphics.PreferredBackBufferHeight = perfiles[menu.perfilSeleccionado].opciones.res_vertical;
                graphics.IsFullScreen = perfiles[menu.perfilSeleccionado].opciones.pantalla_completa != 0;
                graphics.ApplyChanges();
                if (menu.nivelEjecutandose != null) niveles[(int)menu.nivelEjecutandose].opciones = perfiles[menu.perfilSeleccionado].opciones;
                MediaPlayer.Volume = perfiles[menu.perfilSeleccionado].opciones.volumen / 255f;
                menu.dimensiones = GraphicsDevice.Viewport;
                menu.escenario = 1;
            }

            if (menu.nivelEjecutandose != null)
            {
                if (niveles[(int)menu.nivelEjecutandose].nivelCompletado)
                {
                    menu.Enabled = menu.Visible = true;
                    menu.BotonesMenu(false);

                    if (!menu.mostrarCreditos)
                    {
                        PerfilJugador perfillTemporal = perfiles[menu.perfilSeleccionado];
                        if (niveles[(int)menu.nivelEjecutandose].tiempoCompletado < perfillTemporal.puntuaciones[(int)menu.nivelEjecutandose] ||
                            perfillTemporal.puntuaciones[(int)menu.nivelEjecutandose] == TimeSpan.Zero)
                        {
                            perfillTemporal.puntuaciones[(int)menu.nivelEjecutandose] = (PuntuacionesJuego)niveles[(int)menu.nivelEjecutandose].tiempoCompletado;
                        }
                        if (menu.jugarHistoria)
                        {
                            perfillTemporal.opciones.nivel++;
                            if (perfillTemporal.opciones.nivel >= ListaPerfilesJugador.numeroNiveles)
                            {
                                perfillTemporal.opciones.nivel = 0;
                                perfillTemporal.opciones.juego_completado = 1;
                                menu.mostrarCreditos = true;
                            }
                        }

                        perfiles[menu.perfilSeleccionado] = perfillTemporal;
                        perfiles.Guadar(ListaPerfilesJugador.rutaPerfiles);

                        menu.escenario = (int)EscenarioMenu.SabiasQue;
                        menu.reproductorVideo.Stop();
                        menu.reproductorVideo.Play(menu.fondoPausa);
                        MediaPlayer.Stop();
                    }
                    else
                    {
                        menu.mostrarCreditos = false;
                        menu.escenario = 1;
                        menu.reproductorVideo.Stop();
                        menu.reproductorVideo.Play(menu.fondo);
                        MediaPlayer.Stop();
                        MediaPlayer.Play(menu.cancion);
                        if (perfiles[menu.perfilSeleccionado].opciones.musica == 0) MediaPlayer.Pause();
                    }

                    niveles[(int)menu.nivelEjecutandose].Enabled = niveles[(int)menu.nivelEjecutandose].Visible = false;
                    menu.nivelEjecutandose = null;
                }
            }

            menuMostradoAnt = menuMostrado;
            juegoMostradoAnt = juegoMostrado;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.LightBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
