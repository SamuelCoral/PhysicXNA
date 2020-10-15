using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using PhysicXNA.Nucleo;
using PhysicXNA.SistemaPerfiles;

namespace PhysicXNA.MenusJuego
{
    /// <summary>Menús del juego.</summary>
    public class Menu : DrawableGameComponent
    {
        internal Game juego;
        internal ContentManager Content;
        private SpriteBatch spriteBatch;
        /// <summary>Dimensiones de pantalla.</summary>
        public Viewport dimensiones;
        internal KeyboardState estadoTeclado, estadoTecladoAnt;
        internal MouseState estadoMouse, estadoMouseAnt;
        internal Teclado tecladoNombres;

        /// <summary>Escenario mostrado en el menú. Véase <see cref="EscenarioMenu"/></summary>
        public int escenario;
        /// <summary>Perfil de jugador seleccionado sobre el cual se toman y guardan datos.</summary>
        public int perfilSeleccionado = 0;
        /// <summary>Indica si el nivel a jugar se va a jugar en modo historia.</summary>
        public bool jugarHistoria;
        /// <summary>Indica si el próximo nivel a mostrar será el escenario de créditos.</summary>
        public bool mostrarCreditos;
        /// <summary>Indica que nivel se está ejecutando actualmente o null cuando se esté en el menú principal.</summary>
        public int? nivelEjecutandose;
        private bool mostrarScroll;
        /// <summary>Reglas del nivel actual a mostrar.</summary>
        public String[] reglasNivel;
        /// <summary>Texto con información que se muestra al acabar el nivel.</summary>
        public String textoSabiasQue;
        internal int pElemento;
        internal ListaPerfilesJugador perfiles;
        internal BotonMenu[][] botones;

        private DuendeEstatico cursor;
        private DuendeEstatico fondoTexto;
        internal DuendeEstatico barraAyudaSuperior;
        internal DuendeEstatico barraAyudaInferior;
        internal Texto3D textoAyudaSuperior;
        internal Texto3D textoAyudaInferior;
        internal Texto3D textoAdvertencia;
        private Texto3D textoPuntuaciones;
        internal Texto3D textoReglas;
        internal Texto3D[] tituloReglas;
        private BotonMenu botonVolver;
        private BotonMenu botonIniciar;
        private BotonMenu botonContinuar;
        private Logotipo indicadorScroll;
        private Video splash;
        internal Video fondo;
        internal Video fondoPausa;
        internal VideoPlayer reproductorVideo;
        internal SoundEffect click;
        /// <summary>Canción del juego.</summary>
        public Song cancion;

        private BotonMenu[] botonesOKCancelar;
        internal List<BotonMenu> listaBotonesPerfil;
        private BotonMenu[][] botonesMenuPerfiles;

        private Logotipo logo;


        /// <summary>
        /// Construye el menú del juego.
        /// </summary>
        /// <param name="juego">Juego en el que se mostrará.</param>
        /// <param name="listaPerfiles">Lista de perfiles de jugador.</param>
        public Menu(Game juego, ListaPerfilesJugador listaPerfiles)
            : base(juego)
        {
            Visible = Enabled = true;

            mostrarCreditos = false;
            jugarHistoria = false;
            nivelEjecutandose = null;
            this.juego = juego;
            this.Content = juego.Content;
            this.dimensiones = juego.GraphicsDevice.Viewport;
            this.estadoTecladoAnt = new KeyboardState();
            this.estadoMouseAnt = new MouseState();
            this.reproductorVideo = new VideoPlayer();
            listaBotonesPerfil = new List<BotonMenu>();
            if (listaPerfiles.NumeroPerfiles > 0) foreach (PerfilJugador perfil in listaPerfiles) listaBotonesPerfil.Add(new BotonMenu(perfil.opciones.nombre, TipoBotonMenu.Accion, AccionBotonMenu.SeleccionarPerfil));
            tecladoNombres = new Teclado(3);
            this.perfiles = listaPerfiles;
            escenario = (int)EscenarioMenu.Splash;
            pElemento = 0;

            this.indicadorScroll = new Logotipo("Menu/Scroll", 50, new Vector2(0.3f));
            this.fondoTexto = new DuendeEstatico("Menu/FondoTexto", Vector2.Zero);
            this.cursor = new DuendeEstatico("Cursores/Cursor", Vector2.Zero, Color.White, new Vector2(0.1f, 0.1f));
            this.barraAyudaInferior = new DuendeEstatico("Menu/BarraAyuda", Vector2.Zero, new Color(96, 32, 255, 230));
            this.barraAyudaSuperior = new DuendeEstatico("Menu/BarraAyuda", Vector2.Zero, new Color(0, 32, 255, 196), Vector2.Zero, SpriteEffects.FlipVertically);
            this.textoAyudaSuperior = new Texto3D("Menu/FuenteMenu", "", Vector2.Zero, 30, new Color(255, 250, 105), escala: new Vector2(0.75f));
            this.textoAyudaInferior = new Texto3D("Menu/FuenteMenu", "", Vector2.Zero, 30, Color.GreenYellow, 3, new Vector2(0.5f));
            this.textoAdvertencia = new Texto3D("Menu/FuenteMenu", "", Vector2.Zero, 30, Color.Crimson);
            this.textoPuntuaciones = new Texto3D("Menu/FuentePuntuaciones", "", Vector2.Zero, 30, Color.Crimson);
            this.textoReglas = new Texto3D("Menu/FuenteReglas", "", Vector2.Zero, 30, new Color(0, 67, 222, 250), 1);
            this.botonVolver = new BotonMenu("Volver", TipoBotonMenu.Accion, AccionBotonMenu.CambiarEscenario, "Regresa al menú principal.", 1);
            this.botonIniciar = new BotonMenu("Iniciar", TipoBotonMenu.Accion, AccionBotonMenu.CambiarEscenario, "", perfiles.NumeroPerfiles == 0 ? (int)EscenarioMenu.PrimerArranque : (int)EscenarioMenu.VerPerfiles);
            this.botonContinuar = new BotonMenu("Continuar", TipoBotonMenu.Accion, AccionBotonMenu.Continuar);

            tituloReglas = new Texto3D[3];
            tituloReglas[0] = new Texto3D("Menu/FuenteEntrada", "Objetivo", Vector2.Zero, 30, Color.Crimson);
            tituloReglas[1] = new Texto3D("Menu/FuenteEntrada", "Controles", Vector2.Zero, 30, Color.Crimson);
            tituloReglas[2] = new Texto3D("Menu/FuenteEntrada", "Aprendizaje", Vector2.Zero, 30, Color.Crimson);

            // Construcción de botones del menú
            botones = new BotonMenu[7][];

            // Sub-menú de pruebas.
            botones[0] = new BotonMenu[5];
            botones[0][0] = new BotonMenu("Jugar", TipoBotonMenu.Accion, AccionBotonMenu.Jugar, "Comienza o reanuda la partida.");
            botones[0][1] = new BotonMenu("Selección", TipoBotonMenu.Seleccion, "Botón de selección de opciones", 0, new String[] { "Una Opción", "Otra opción", "Una más", "Otra y ya" });
            botones[0][2] = new BotonMenu("Barra", TipoBotonMenu.Barra, "Botón de barra deslizable", 50, 100);
            botones[0][3] = new BotonMenu("Si/No", TipoBotonMenu.SiNo, "Botón de selección booleana", 0);
            botones[0][4] = new BotonMenu("Regresar", TipoBotonMenu.Accion, AccionBotonMenu.CambiarEscenario, "Volver", 0);

            // Sub-menú de selección de nivel al finalizar el juego
            botones[6] = new BotonMenu[4];
            botones[6][0] = new BotonMenu("Volver", TipoBotonMenu.Accion, AccionBotonMenu.CambiarEscenario, "Vuelve al menú principal.", 1);
            botones[6][1] = new BotonMenu("Nivel", TipoBotonMenu.Seleccion, "Nivel que desea jugar", 0, new String[] { "Herramientas", "Vectores", "Tiro parabólico", "Acción reacción", "Gravedad" });
            botones[6][2] = new BotonMenu("Video", TipoBotonMenu.SiNo, "Volver a ver el video de introducción al nivel.", 1);
            botones[6][3] = new BotonMenu("Jugar", TipoBotonMenu.Accion, AccionBotonMenu.Jugar, "Jugar al nivel seleccionado.");

            // Menú principal del juego
            botones[1] = new BotonMenu[9];
            botones[1][0] = new BotonMenu("Jugar", TipoBotonMenu.Accion, AccionBotonMenu.Jugar, "Comenzar a jugar.");
            botones[1][1] = new BotonMenu("Elegir nivel", TipoBotonMenu.Accion, AccionBotonMenu.CambiarEscenario, "Elige un nivel para volver a jugar.", 6);
            botones[1][2] = new BotonMenu("Reglas", TipoBotonMenu.Accion, AccionBotonMenu.CambiarEscenario, "Ver las reglas y controles de este nivel.", (int)EscenarioMenu.ReglasNivel);
            botones[1][3] = new BotonMenu("Opciones", TipoBotonMenu.Accion, AccionBotonMenu.CambiarEscenario, "Muestra el submenú de opciones.", 2);
            botones[1][4] = new BotonMenu("Puntuaciones", TipoBotonMenu.Accion, AccionBotonMenu.CambiarEscenario, "Muestra tus mejores tiempos de cada nivel.", (int)EscenarioMenu.VerPuntuaciones);
            botones[1][5] = new BotonMenu("Volver", TipoBotonMenu.Accion, AccionBotonMenu.CambiarEscenario, "Vuelve al menú se selección de perfiles.", (int)EscenarioMenu.VerPerfiles);
            botones[1][6] = new BotonMenu("Volver", TipoBotonMenu.Accion, AccionBotonMenu.CambiarEscenario, "Sale del nivel y vuelve al menú principal.", (int)EscenarioMenu.ConfirmarSalirNivel);
            botones[1][7] = new BotonMenu("Reiniciar", TipoBotonMenu.Accion, AccionBotonMenu.CambiarEscenario, "Reinicia el nivel actual", (int)EscenarioMenu.ConfirmarReiniciarNivel);
            botones[1][8] = new BotonMenu("Salir", TipoBotonMenu.Accion, AccionBotonMenu.CambiarEscenario, "Sale del juego.", (int)EscenarioMenu.SalirJuego);
            
            botones[1][2].visible = botones[1][6].visible = botones[1][7].visible = false;

            // Submenú de opciones
            botones[2] = new BotonMenu[5];
            botones[2][0] = new BotonMenu("Video", TipoBotonMenu.Accion, AccionBotonMenu.CambiarEscenario, "Muestra las opciones de video.", 3);
            botones[2][1] = new BotonMenu("Sonido", TipoBotonMenu.Accion, AccionBotonMenu.CambiarEscenario, "Muestra las opciones de sonido.", 4);
            botones[2][2] = new BotonMenu("Control", TipoBotonMenu.Accion, AccionBotonMenu.CambiarEscenario, "Muestra las opciones de controles.", 5);
            botones[2][3] = new BotonMenu("Dificultad", TipoBotonMenu.Seleccion, "Dificultad del juego.", 0, new String[] { "Fácil", "Medio", "Difícil" });
            botones[2][4] = new BotonMenu("Volver", TipoBotonMenu.Accion, AccionBotonMenu.GuardarPerfil, "Vuelve al menú principal.");

            // Menú de opciones de video
            botones[3] = new BotonMenu[3];
            botones[3][0] = new BotonMenu("Volver", TipoBotonMenu.Accion, AccionBotonMenu.GuardarPerfil, "Vuelve al menú principal.");
            botones[3][1] = new BotonMenu("Resolución", TipoBotonMenu.Seleccion, "Resolución de pantalla.", 0, Pantalla.ObtenerResoluciones().ToArray());
            botones[3][2] = new BotonMenu("Pant. Comp.", TipoBotonMenu.SiNo, "Usar el modo de pantalla completa.", 0);
            

            // Menú de opciones de sonido
            botones[4] = new BotonMenu[4];
            botones[4][0] = new BotonMenu("Volver", TipoBotonMenu.Accion, AccionBotonMenu.GuardarPerfil, "Vuelve al menú principal.");
            botones[4][1] = new BotonMenu("Música", TipoBotonMenu.SiNo, "Activa o desactiva la música del juego.", 0);
            botones[4][2] = new BotonMenu("Sonidos", TipoBotonMenu.SiNo, "Activa o desactiva los sonidos del juego.", 1);
            botones[4][3] = new BotonMenu("Volumen", TipoBotonMenu.Barra, "Volumen de la música del juego.", 0, 255);

            // Menú de opciones de controles
            botones[5] = new BotonMenu[3];
            botones[5][0] = new BotonMenu("Volver", TipoBotonMenu.Accion, AccionBotonMenu.GuardarPerfil, "Vuelve al menú principal.");
            botones[5][1] = new BotonMenu("WASD", TipoBotonMenu.SiNo, "Utilizar WASD en lugar de las flechas direccionales.", 0);
            botones[5][2] = new BotonMenu("Invertir", TipoBotonMenu.SiNo, "Invierte los botones del mouse.", 0);

            // Menú de perfiles
            botonesMenuPerfiles = new BotonMenu[2][];
            botonesMenuPerfiles[0] = new BotonMenu[3];
            botonesMenuPerfiles[1] = new BotonMenu[4];
            botonesMenuPerfiles[0][0] = new BotonMenu("Nuevo", TipoBotonMenu.Accion, AccionBotonMenu.NuevoPerfil, "Crea un nuevo perfil.");
            botonesMenuPerfiles[0][1] = new BotonMenu("Cargar", TipoBotonMenu.Accion, AccionBotonMenu.CargarPerfil, "Carga el perfil elegido.");
            botonesMenuPerfiles[0][2] = new BotonMenu("Opciones", TipoBotonMenu.Accion, AccionBotonMenu.OperacionesPerfil, "Muestra las opciones del perfil.");
            botonesMenuPerfiles[1][0] = new BotonMenu("Copiar", TipoBotonMenu.Accion, AccionBotonMenu.CambiarEscenario, "Copia el perfil seleccionado.", (int)EscenarioMenu.CopiarPerfil);
            botonesMenuPerfiles[1][1] = new BotonMenu("Renombrar", TipoBotonMenu.Accion, AccionBotonMenu.CambiarEscenario, "Renombra el perfil seleccionado.", (int)EscenarioMenu.RenombrarPerfil);
            botonesMenuPerfiles[1][2] = new BotonMenu("Eliminar", TipoBotonMenu.Accion, AccionBotonMenu.CambiarEscenario, "Elimina el perfil seleccionado.", (int)EscenarioMenu.EliminarPerfil);
            botonesMenuPerfiles[1][3] = new BotonMenu("Volver", TipoBotonMenu.Accion, AccionBotonMenu.OperacionesPerfil);

            // Botones de OK y Cancelar
            botonesOKCancelar = new BotonMenu[2];
            botonesOKCancelar[0] = new BotonMenu("OK", TipoBotonMenu.Accion, AccionBotonMenu.OK);
            botonesOKCancelar[1] = new BotonMenu("Cancelar", TipoBotonMenu.Accion, AccionBotonMenu.CambiarEscenario, "");

            logo = new Logotipo("Menu/Logos", 40, new Vector2(0.3f));
        }

        /// <summary>
        /// Muestra u oculta los botones apropiados para el tipo de menú
        /// </summary>
        /// <param name="pausa">Indica si desea mostrar los botones del menú de pausa.</param>
        public void BotonesMenu(bool pausa)
        {
            botones[1][5].visible = !pausa;   // Volver al menú de perfiles
            botones[2][3].visible = !pausa;   // Cambiar de dificultad
            botones[3][1].visible = !pausa;   // Resolución de pantalla
            botones[1][2].visible = pausa;    // Reglas
            botones[1][6].visible = pausa;    // Salir del nivel
            botones[1][7].visible = pausa;    // Reiniciar el nivel

            if (reproductorVideo.State != MediaState.Stopped) reproductorVideo.Stop();
            if (pausa)
            {
                reproductorVideo.Play(fondoPausa);
                botones[1][1].visible = false;
                botones[1][0].texto.Texto = "Reanudar";
                botones[1][0].ayuda = "Reanuda la partida actual";
            }
            else
            {
                reproductorVideo.Play(fondo);
                int nivel = perfiles[perfilSeleccionado].opciones.nivel;
                bool juegoCompletado = perfiles[perfilSeleccionado].opciones.juego_completado != 0;

                botones[1][1].visible = juegoCompletado;
                if (juegoCompletado && nivel == 0)
                {
                    botones[1][0].texto.Texto = "Volver a jugar";
                    botones[1][0].ayuda = "Vuelve a comenzar el juego desde el primer nivel.";
                }
                else
                {
                    if (nivel == 0)
                    {
                        botones[1][0].texto.Texto = "Comenzar juego";
                        botones[1][0].ayuda = "Comenzar a jugar.";
                    }
                    else
                    {
                        botones[1][0].texto.Texto = "Continuar juego";
                        botones[1][0].ayuda = "Continua el juego desde el último nivel guardado.";
                    }
                }
            }
        }

        /// <summary>
        /// Carga el contenido de los elementos del menú.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            splash = Content.Load<Video>("Videos/Intro-Demo");
            fondo = Content.Load<Video>("Videos/FondoMenu");
            fondoPausa = Content.Load<Video>("Videos/FondoMenuPausa");
            click = Content.Load<SoundEffect>("Menu/Click");
            cancion = Content.Load<Song>("Canciones/Enter The Future");
            reproductorVideo.Play(splash);
            indicadorScroll.CargarContenido(Content);
            cursor.CargarContenido(Content);
            fondoTexto.CargarContenido(Content);
            barraAyudaSuperior.CargarContenido(Content);
            barraAyudaInferior.CargarContenido(Content);
            textoAyudaSuperior.CargarContenido(Content);
            textoAyudaInferior.CargarContenido(Content);
            textoAdvertencia.CargarContenido(Content);
            tecladoNombres.CargarContenido(Content);
            textoReglas.CargarContenido(Content);
            textoPuntuaciones.CargarContenido(Content);
            botonVolver.CargarContenido(Content);
            botonIniciar.CargarContenido(Content);
            botonContinuar.CargarContenido(Content);

            foreach (Texto3D titulo in tituloReglas) titulo.CargarContenido(Content);
            foreach (BotonMenu boton in listaBotonesPerfil) boton.CargarContenido(Content);
            foreach (BotonMenu[] submenu in botones) foreach (BotonMenu boton in submenu) boton.CargarContenido(Content);
            foreach (BotonMenu[] submenu in botonesMenuPerfiles) foreach (BotonMenu boton in submenu) boton.CargarContenido(Content);
            foreach (BotonMenu boton in botonesOKCancelar) boton.CargarContenido(Content);

            logo.CargarContenido(Content);

            base.LoadContent();
        }

        /// <summary>
        /// Libera los recursos ocupados por los elementos del menú.
        /// </summary>
        protected override void UnloadContent()
        {
            reproductorVideo.Dispose();
            cursor.LiberarContenido();
            fondoTexto.LiberarContenido();
            cancion.Dispose();
            indicadorScroll.LiberarContenido();
            barraAyudaSuperior.LiberarContenido();
            barraAyudaInferior.LiberarContenido();
            textoAyudaSuperior.LiberarContenido();
            textoAyudaInferior.LiberarContenido();
            textoAdvertencia.LiberarContenido();
            tecladoNombres.LiberarContenido();
            textoReglas.LiberarContenido();
            textoPuntuaciones.LiberarContenido();
            botonVolver.LiberarContenido();
            botonIniciar.LiberarContenido();
            botonContinuar.LiberarContenido();

            foreach (Texto3D titulo in tituloReglas) titulo.LiberarContenido();
            foreach (BotonMenu boton in listaBotonesPerfil) boton.LiberarContenido();
            foreach (BotonMenu[] submenu in botones) foreach (BotonMenu boton in submenu) boton.LiberarContenido();
            foreach (BotonMenu[] submenu in botonesMenuPerfiles) foreach (BotonMenu boton in submenu) boton.LiberarContenido();
            foreach (BotonMenu boton in botonesOKCancelar) boton.LiberarContenido();

            logo.LiberarContenido();

            base.UnloadContent();
        }


        private int c, invisibles;
        private int? barraSeleccionada = null;
        internal bool opcionesPerfilSeleccionadas = false;

        /// <summary>
        /// Actualiza los elementos del menú.
        /// </summary>
        /// <param name="gameTime">Tiempo del juego.</param>
        public override void Update(GameTime gameTime)
        {
            estadoTeclado = Keyboard.GetState();
            estadoMouse = Mouse.GetState();

            cursor.Posicion = new Vector2(estadoMouse.X < 0 ? 0 : estadoMouse.X > dimensiones.Width - cursor.Ancho ? dimensiones.Width - cursor.Alto : estadoMouse.X,
                estadoMouse.Y < 0 ? 0 : estadoMouse.Y > dimensiones.Height - cursor.Alto ? dimensiones.Height - cursor.Alto : estadoMouse.Y);
            barraAyudaSuperior.Escala = new Vector2((float)dimensiones.Width / barraAyudaSuperior.AnchoReal, 1f);
            barraAyudaInferior.Escala = new Vector2((float)dimensiones.Width / barraAyudaInferior.AnchoReal, 1f);
            barraAyudaInferior.Posicion = new Vector2(0, dimensiones.Height - barraAyudaInferior.Alto);
            textoAyudaInferior.Texto = "";
            indicadorScroll.Posicion = new Vector2((float)dimensiones.Width - indicadorScroll.Ancho, (float)dimensiones.Height - indicadorScroll.Alto);
            indicadorScroll.Actualizar(gameTime, dimensiones, estadoTeclado);
            mostrarScroll = false;

            switch (escenario)
            {
                case (int)EscenarioMenu.Splash:
                    if (reproductorVideo.State == MediaState.Stopped || (estadoMouse.LeftButton == ButtonState.Released && estadoMouseAnt.LeftButton == ButtonState.Pressed))
                    {
                        if (reproductorVideo.State != MediaState.Stopped) reproductorVideo.Stop();
                        reproductorVideo.Play(fondo);
#if !MONOGAME
                        reproductorVideo.IsLooped = true;
#endif
                        MediaPlayer.Play(cancion);
                        escenario = (int)EscenarioMenu.PantallaInicio;
                    }
                    break;

                case (int)EscenarioMenu.PantallaInicio:
                    botonIniciar.textura.Posicion = new Vector2((dimensiones.Width - botonIniciar.textura.Ancho) / 2f, dimensiones.Height - botonIniciar.textura.Alto - barraAyudaInferior.Alto - 20f);
                    botonIniciar.ActualizarGenerico(this, gameTime, 0);
                    logo.Actualizar(gameTime, dimensiones, estadoTeclado);
                    logo.Posicion = new Vector2((dimensiones.Width - logo.Ancho) / 2f, 30f);
                    break;

                case (int)EscenarioMenu.VerPerfiles:
                    textoAyudaSuperior.Texto = "Escoja un perfil y una acción.";
                    mostrarScroll = (pElemento < listaBotonesPerfil.Count - 1 || pElemento < botonesMenuPerfiles[opcionesPerfilSeleccionadas ? 1 : 0].Length - 1);
                    if ((estadoMouse.ScrollWheelValue < estadoMouseAnt.ScrollWheelValue || (estadoTeclado.IsKeyDown(Keys.Down) && !estadoTecladoAnt.IsKeyDown(Keys.Down))) && mostrarScroll) pElemento++;
                    if ((estadoMouse.ScrollWheelValue > estadoMouseAnt.ScrollWheelValue || (estadoTeclado.IsKeyDown(Keys.Up) && !estadoTecladoAnt.IsKeyDown(Keys.Up))) && pElemento > 0) pElemento--;

                    for (c = pElemento; c < listaBotonesPerfil.Count; c++)
                    {
                        listaBotonesPerfil[c].textura.Posicion = new Vector2(botonesMenuPerfiles[0][0].textura.Posicion.X + botonesMenuPerfiles[0][0].textura.Ancho + 10, (c - pElemento) * (listaBotonesPerfil[c].textura.Alto + 20) + 20 + barraAyudaSuperior.Alto);
                        listaBotonesPerfil[c].textura.Escala = new Vector2((dimensiones.Width - listaBotonesPerfil[c].textura.Posicion.X - 20) / listaBotonesPerfil[c].textura.AnchoReal, listaBotonesPerfil[c].textura.Escala.Y);
                        listaBotonesPerfil[c].ActualizarGenerico(this, gameTime, c);
                    }

                    for (c = pElemento; c < botonesMenuPerfiles[opcionesPerfilSeleccionadas ? 1 : 0].Length; c++)
                    {
                        botonesMenuPerfiles[opcionesPerfilSeleccionadas ? 1 : 0][c].habilitado = c > 0 && perfilSeleccionado == 0 ? false : true;
                        botonesMenuPerfiles[opcionesPerfilSeleccionadas ? 1 : 0][c].textura.Posicion = new Vector2(20, (c - pElemento) * (botonesMenuPerfiles[0][0].textura.Alto + 20) + 20 + barraAyudaSuperior.Alto);
                        botonesMenuPerfiles[opcionesPerfilSeleccionadas ? 1 : 0][c].ActualizarGenerico(this, gameTime, c);
                    }
                    break;

                case (int)EscenarioMenu.CrearPerfil:
                    textoAyudaSuperior.Texto = "Escriba el nombre del nuevo perfil.";
                    textoAdvertencia.Posicion = new Vector2((dimensiones.Width - textoAdvertencia.TamTexto.X) / 2, textoAyudaInferior.Posicion.Y);
                    tecladoNombres.Actualizar(this, gameTime);
                    botonesOKCancelar[1].valor = (int)EscenarioMenu.VerPerfiles;
                    for (c = 0; c < 2; c++)
                    {
                        botonesOKCancelar[c].textura.Posicion = new Vector2(dimensiones.Width / 2 - (c == 0 ? botonesOKCancelar[c].textura.Ancho : 0), tecladoNombres.barra.Posicion.Y + tecladoNombres.barra.Alto);
                        botonesOKCancelar[c].ActualizarGenerico(this, gameTime, c);
                    }
                    break;

                case (int)EscenarioMenu.PrimerArranque:
                    textoAyudaSuperior.Texto = "¡Bienvenido! Porfavor, escriba su nombre.";
                    textoAdvertencia.Posicion = new Vector2((dimensiones.Width - textoAdvertencia.TamTexto.X) / 2, textoAyudaInferior.Posicion.Y);
                    tecladoNombres.Actualizar(this, gameTime);
                    botonesOKCancelar[0].textura.Posicion = new Vector2(dimensiones.Width / 2 - (c == 0 ? botonesOKCancelar[c].textura.Ancho : 0), tecladoNombres.barra.Posicion.Y + tecladoNombres.barra.Alto);
                    botonesOKCancelar[0].ActualizarGenerico(this, gameTime, c);
                    break;

                case (int)EscenarioMenu.CopiarPerfil:
                    textoAyudaSuperior.Texto = "Escriba el nombre de la copia del perfil.";
                    textoAdvertencia.Posicion = new Vector2((dimensiones.Width - textoAdvertencia.TamTexto.X) / 2, textoAyudaInferior.Posicion.Y);
                    tecladoNombres.Actualizar(this, gameTime);
                    botonesOKCancelar[1].valor = (int)EscenarioMenu.VerPerfiles;
                    for (c = 0; c < 2; c++)
                    {
                        botonesOKCancelar[c].textura.Posicion = new Vector2(dimensiones.Width / 2 - (c == 0 ? botonesOKCancelar[c].textura.Ancho : 0), tecladoNombres.barra.Posicion.Y + tecladoNombres.barra.Alto);
                        botonesOKCancelar[c].ActualizarGenerico(this, gameTime, c);
                    }
                    break;

                case (int)EscenarioMenu.EliminarPerfil:
                    textoAyudaSuperior.Texto = "¿Seguro que desea eliminar el perfil seleccionado?";
                    tecladoNombres.Actualizar(this, gameTime);
                    botonesOKCancelar[1].valor = (int)EscenarioMenu.VerPerfiles;
                    for (c = 0; c < 2; c++)
                    {
                        botonesOKCancelar[c].textura.Posicion = new Vector2(dimensiones.Width / 2 - (c == 0 ? botonesOKCancelar[c].textura.Ancho : 0), tecladoNombres.barra.Posicion.Y + tecladoNombres.barra.Alto);
                        botonesOKCancelar[c].ActualizarGenerico(this, gameTime, c);
                    }
                    break;

                case (int)EscenarioMenu.RenombrarPerfil:
                    textoAyudaSuperior.Texto = "Escriba el nuevo nombre del perfil.";
                    textoAdvertencia.Posicion = new Vector2((dimensiones.Width - textoAdvertencia.TamTexto.X) / 2, textoAyudaInferior.Posicion.Y);
                    tecladoNombres.Actualizar(this, gameTime);
                    botonesOKCancelar[1].valor = (int)EscenarioMenu.VerPerfiles;
                    for (c = 0; c < 2; c++)
                    {
                        botonesOKCancelar[c].textura.Posicion = new Vector2(dimensiones.Width / 2 - (c == 0 ? botonesOKCancelar[c].textura.Ancho : 0), tecladoNombres.barra.Posicion.Y + tecladoNombres.barra.Alto);
                        botonesOKCancelar[c].ActualizarGenerico(this, gameTime, c);
                    }
                    break;

                case (int)EscenarioMenu.SalirJuego:
                    textoAyudaSuperior.Texto = "¿Seguro que desea salir del juego?";
                    tecladoNombres.Actualizar(this, gameTime);
                    botonesOKCancelar[1].valor = 1;
                    for (c = 0; c < 2; c++)
                    {
                        botonesOKCancelar[c].textura.Posicion = new Vector2(dimensiones.Width / 2 - (c == 0 ? botonesOKCancelar[c].textura.Ancho : 0), tecladoNombres.barra.Posicion.Y + tecladoNombres.barra.Alto);
                        botonesOKCancelar[c].ActualizarGenerico(this, gameTime, c);
                    }
                    break;

                case (int)EscenarioMenu.ConfirmarReiniciarNivel:
                    textoAyudaSuperior.Texto = "¿Seguro que desea reinicial el nivel?";
                    tecladoNombres.Actualizar(this, gameTime);
                    botonesOKCancelar[1].valor = 1;
                    for (c = 0; c < 2; c++)
                    {
                        botonesOKCancelar[c].textura.Posicion = new Vector2(dimensiones.Width / 2 - (c == 0 ? botonesOKCancelar[c].textura.Ancho : 0), tecladoNombres.barra.Posicion.Y + tecladoNombres.barra.Alto);
                        botonesOKCancelar[c].ActualizarGenerico(this, gameTime, c);
                    }
                    break;

                case (int)EscenarioMenu.ConfirmarSalirNivel:
                    textoAyudaSuperior.Texto = "¿Seguro que desea regresar al menú principal?";
                    tecladoNombres.Actualizar(this, gameTime);
                    botonesOKCancelar[1].valor = 1;
                    for (c = 0; c < 2; c++)
                    {
                        botonesOKCancelar[c].textura.Posicion = new Vector2(dimensiones.Width / 2 - (c == 0 ? botonesOKCancelar[c].textura.Ancho : 0), tecladoNombres.barra.Posicion.Y + tecladoNombres.barra.Alto);
                        botonesOKCancelar[c].ActualizarGenerico(this, gameTime, c);
                    }
                    break;

                case (int)EscenarioMenu.VerPuntuaciones:
                    textoAyudaSuperior.Texto = "Sus mejores tiempos.";
                    mostrarScroll = pElemento < ListaPerfilesJugador.numeroNiveles - 1;
                    if (estadoMouse.ScrollWheelValue < estadoMouseAnt.ScrollWheelValue && mostrarScroll) pElemento++;
                    if (estadoMouse.ScrollWheelValue > estadoMouseAnt.ScrollWheelValue && pElemento > 0) pElemento--;
                    botonVolver.textura.Posicion = new Vector2((dimensiones.Width - botonVolver.textura.Ancho) / 2, 20 + barraAyudaSuperior.Alto);
                    botonVolver.ActualizarGenerico(this, gameTime, 0);
                    textoPuntuaciones.Posicion = new Vector2(20, botonVolver.textura.Posicion.Y + botonVolver.textura.Alto + 20);
                    textoPuntuaciones.Texto = "";
                    for (c = pElemento; c < ListaPerfilesJugador.numeroNiveles; c++) textoPuntuaciones.Texto += "Nivel " + (c + 1).ToString("00") + ": " + perfiles[perfilSeleccionado].puntuaciones[c].ToString() + "\n";
                    //textoPuntuaciones.Actualizar(gameTime);
                    break;

                case (int)EscenarioMenu.ReglasNivel:
                    textoAyudaSuperior.Texto = "Como jugar:";
                    mostrarScroll = pElemento < 2;
                    if (estadoMouse.ScrollWheelValue < estadoMouseAnt.ScrollWheelValue && mostrarScroll) pElemento++;
                    if (estadoMouse.ScrollWheelValue > estadoMouseAnt.ScrollWheelValue && pElemento > 0) pElemento--;
                    botonVolver.textura.Posicion = new Vector2((dimensiones.Width - botonVolver.textura.Ancho) / 2, 20 + barraAyudaSuperior.Alto);
                    botonVolver.ActualizarGenerico(this, gameTime, 0);
                    tituloReglas[0].Texto = "Objetivo";
                    tituloReglas[pElemento].Posicion = new Vector2((dimensiones.Width - tituloReglas[pElemento].TamTexto.X) / 2, botonVolver.textura.Posicion.Y + botonVolver.textura.Alto + 20);
                    tituloReglas[pElemento].Actualizar(gameTime);
                    textoReglas.Posicion = new Vector2(20, tituloReglas[pElemento].Posicion.Y + tituloReglas[pElemento].TamTexto.Y + 20);
                    textoReglas.Texto = reglasNivel[pElemento];
                    textoReglas.Escala = new Vector2(((float)dimensiones.Width - 50) / textoReglas.TamRealTexto.X);
                    fondoTexto.Posicion = textoReglas.Posicion - new Vector2(10f);
                    fondoTexto.Escala = new Vector2((dimensiones.Width - 20f) / fondoTexto.AnchoReal, (barraAyudaInferior.Posicion.Y - fondoTexto.Posicion.Y - 10f) / fondoTexto.AltoReal);
                    break;

                case (int)EscenarioMenu.SabiasQue:
                    textoAyudaSuperior.Texto = nivelEjecutandose == 5 ? "¡Juego completado!" : "¡Nivel completado!";
                    botonContinuar.textura.Posicion = new Vector2((dimensiones.Width - botonContinuar.textura.Ancho) / 2, 20 + barraAyudaSuperior.Alto);
                    botonContinuar.ActualizarGenerico(this, gameTime, 0);
                    tituloReglas[0].Texto = "¿SABIAS QUE...?";
                    tituloReglas[pElemento].Posicion = new Vector2((dimensiones.Width - tituloReglas[pElemento].TamTexto.X) / 2, botonContinuar.textura.Posicion.Y + botonContinuar.textura.Alto + 20);
                    tituloReglas[pElemento].Actualizar(gameTime);
                    textoReglas.Posicion = new Vector2(20, tituloReglas[pElemento].Posicion.Y + tituloReglas[pElemento].TamTexto.Y + 20);
                    textoReglas.Texto = textoSabiasQue;
                    textoReglas.Escala = new Vector2(((float)dimensiones.Width - 50) / textoReglas.TamRealTexto.X);
                    fondoTexto.Posicion = textoReglas.Posicion - new Vector2(10f);
                    fondoTexto.Escala = new Vector2((dimensiones.Width - 20f) / fondoTexto.AnchoReal, (barraAyudaInferior.Posicion.Y - fondoTexto.Posicion.Y - 10f) / fondoTexto.AltoReal);
                    break;

                case (int)EscenarioMenu.SalirNivel:
                case (int)EscenarioMenu.ReiniciarNivel:
                case (int)EscenarioMenu.CargarPerfil:

                    break;

                // Sub-menú
                default:

                    mostrarScroll = pElemento < botones[escenario].Length - 1;
                    if ((estadoMouse.ScrollWheelValue < estadoMouseAnt.ScrollWheelValue || (estadoTeclado.IsKeyDown(Keys.Down) && !estadoTecladoAnt.IsKeyDown(Keys.Down))) && mostrarScroll) pElemento++;
                    if ((estadoMouse.ScrollWheelValue > estadoMouseAnt.ScrollWheelValue || (estadoTeclado.IsKeyDown(Keys.Up) && !estadoTecladoAnt.IsKeyDown(Keys.Up))) && pElemento > 0) pElemento--;

                    if (barraSeleccionada != null)
                    {
                        botones[escenario][(int)barraSeleccionada].valor = (int)Math.Round((estadoMouse.X - botones[escenario][(int)barraSeleccionada].barraNegra.Posicion.X - botones[escenario][(int)barraSeleccionada].indicadorBarra.Ancho / 2) * botones[escenario][(int)barraSeleccionada].maximo / (botones[escenario][(int)barraSeleccionada].barraNegra.Ancho - botones[escenario][(int)barraSeleccionada].indicadorBarra.Ancho));
                        botones[escenario][(int)barraSeleccionada].valor = botones[escenario][(int)barraSeleccionada].valor < 0 ? 0 : botones[escenario][(int)barraSeleccionada].valor >= botones[escenario][(int)barraSeleccionada].maximo ? botones[escenario][(int)barraSeleccionada].maximo : botones[escenario][(int)barraSeleccionada].valor;
                        if (botones[escenario][(int)barraSeleccionada].texto.Texto == "Volumen") MediaPlayer.Volume = botones[escenario][(int)barraSeleccionada].valor / 255f;
                        if (botones[5][2].valor == 0 ? estadoMouse.LeftButton == ButtonState.Released : estadoMouse.RightButton == ButtonState.Released) barraSeleccionada = null;
                    }

                    for (c = pElemento, invisibles = 0; c < botones[escenario].Length; c++)
                    {
                        if (!botones[escenario][c].visible)
                        {
                            invisibles++;
                            continue;
                        }

                        switch (botones[escenario][c].tipo)
                        {
                            case TipoBotonMenu.Accion:
                                botones[escenario][c].textura.Posicion = new Vector2((dimensiones.Width - botones[escenario][c].textura.Ancho) / 2, (c - pElemento - invisibles) * (botones[escenario][c].textura.Alto + 20) + 20 + barraAyudaSuperior.Alto);
                                break;

                            case TipoBotonMenu.Barra:
                                botones[escenario][c].textura.Posicion = new Vector2(20, (c - pElemento - invisibles) * (botones[escenario][c].textura.Alto + 20) + 20 + barraAyudaSuperior.Alto);
                                botones[escenario][c].barraNegra.Posicion = new Vector2(botones[escenario][c].textura.Posicion.X + botones[escenario][c].textura.Ancho + 20, botones[escenario][c].textura.Posicion.Y + (botones[escenario][c].textura.Alto - botones[escenario][c].barraNegra.Alto) / 2);
                                botones[escenario][c].barraNegra.Escala = new Vector2((dimensiones.Width - botones[escenario][c].barraNegra.Posicion.X - 100) / botones[escenario][c].barraNegra.AnchoReal, 1);
                                botones[escenario][c].barra.Posicion = botones[escenario][c].barraNegra.Posicion;
                                botones[escenario][c].barra.Escala = new Vector2((dimensiones.Width - botones[escenario][c].barra.Posicion.X - 100 - botones[escenario][c].indicadorBarra.Ancho) / botones[escenario][c].barra.AnchoReal * botones[escenario][c].valor / botones[escenario][c].maximo + (botones[escenario][c].indicadorBarra.Ancho / 2 / botones[escenario][c].barra.AnchoReal), 1f);
                                botones[escenario][c].textoOpcion.Posicion = new Vector2(botones[escenario][c].barraNegra.Posicion.X + botones[escenario][c].barraNegra.Ancho + 20, botones[escenario][c].textura.Posicion.Y + (botones[escenario][c].textura.Alto - botones[escenario][c].texto.TamTexto.Y) / 2);
                                botones[escenario][c].textoOpcion.Texto = botones[escenario][c].valor.ToString();
                                botones[escenario][c].bordeBarraHor[0].Escala = botones[escenario][c].bordeBarraHor[1].Escala = new Vector2(1f, (float)botones[escenario][c].barra.Alto / botones[escenario][c].bordeBarraHor[0].AltoReal);
                                botones[escenario][c].bordeBarraHor[0].Posicion = new Vector2(botones[escenario][c].barra.Posicion.X - botones[escenario][c].bordeBarraHor[0].Ancho, botones[escenario][c].barra.Posicion.Y);
                                botones[escenario][c].bordeBarraHor[1].Posicion = new Vector2(botones[escenario][c].barra.Posicion.X + botones[escenario][c].barraNegra.Ancho, botones[escenario][c].barra.Posicion.Y);
                                botones[escenario][c].bordeBarraVer[0].Escala = botones[escenario][c].bordeBarraVer[1].Escala = new Vector2(1f, ((float)botones[escenario][c].barraNegra.Ancho + 2 * botones[escenario][c].bordeBarraHor[0].Ancho) / botones[escenario][c].bordeBarraVer[0].AltoReal);
                                botones[escenario][c].bordeBarraVer[0].Posicion = new Vector2(botones[escenario][c].bordeBarraHor[0].Posicion.X, botones[escenario][c].bordeBarraHor[0].Posicion.Y);
                                botones[escenario][c].bordeBarraVer[1].Posicion = new Vector2(botones[escenario][c].bordeBarraHor[0].Posicion.X, botones[escenario][c].bordeBarraHor[0].Posicion.Y + botones[escenario][c].bordeBarraHor[0].Alto + botones[escenario][c].bordeBarraVer[1].Ancho);
                                botones[escenario][c].indicadorBarra.Escala = new Vector2(1f, (float)botones[escenario][c].barra.Alto / botones[escenario][c].indicadorBarra.AltoReal);
                                botones[escenario][c].indicadorBarra.Posicion = new Vector2(botones[escenario][c].barra.Posicion.X + botones[escenario][c].barra.Ancho, botones[escenario][c].barra.Posicion.Y);
                                break;

                            case TipoBotonMenu.Seleccion:
                                botones[escenario][c].textura.Posicion = new Vector2(20, (c - pElemento - invisibles) * (botones[escenario][c].textura.Alto + 20) + 20 + barraAyudaSuperior.Alto);
                                botones[escenario][c].flechaOpciones[0].Posicion = new Vector2(botones[escenario][c].textura.Posicion.X + botones[escenario][c].textura.Ancho + 20, botones[escenario][c].textura.Posicion.Y + (botones[escenario][c].textura.Alto - botones[escenario][c].flechaOpciones[0].Alto) / 2);
                                botones[escenario][c].botonOpcion.Posicion = new Vector2(botones[escenario][c].flechaOpciones[0].Posicion.X + botones[escenario][c].flechaOpciones[0].Ancho + 10, botones[escenario][c].textura.Posicion.Y);
                                botones[escenario][c].botonOpcion.Escala = new Vector2((dimensiones.Width - botones[escenario][c].botonOpcion.Posicion.X - 50 - botones[escenario][c].flechaOpciones[1].Ancho) / botones[escenario][c].botonOpcion.AnchoReal, botones[escenario][c].botonOpcion.Escala.Y);
                                botones[escenario][c].flechaOpciones[1].Posicion = new Vector2(botones[escenario][c].botonOpcion.Posicion.X + botones[escenario][c].botonOpcion.Ancho + 10, botones[escenario][c].flechaOpciones[0].Posicion.Y);
                                botones[escenario][c].textoOpcion.Texto = botones[escenario][c].opciones[botones[escenario][c].valor];
                                botones[escenario][c].textoOpcion.Posicion = new Vector2(botones[escenario][c].botonOpcion.Posicion.X + (botones[escenario][c].botonOpcion.Ancho - botones[escenario][c].textoOpcion.TamTexto.X) / 2,
                                    botones[escenario][c].textura.Posicion.Y + (botones[escenario][c].textura.Alto - botones[escenario][c].texto.TamTexto.Y) / 2);
                                break;

                            case TipoBotonMenu.SiNo:
                                botones[escenario][c].textura.Posicion = new Vector2((dimensiones.Width - botones[escenario][c].textura.Ancho) / 2, (c - pElemento - invisibles) * (botones[escenario][c].textura.Alto + 20) + 20 + barraAyudaSuperior.Alto);
                                botones[escenario][c].barra.Escala = new Vector2((float)botones[escenario][c].barra.AltoReal / 2 / botones[escenario][c].barra.AnchoReal, 0.5f);
                                botones[escenario][c].barra.Posicion = new Vector2(botones[escenario][c].textura.Posicion.X + botones[escenario][c].textura.Ancho + 20, botones[escenario][c].textura.Posicion.Y + (botones[escenario][c].textura.Alto - botones[escenario][c].barra.Alto) / 2);
                                botones[escenario][c].barra.Color = botones[escenario][c].valor != 0 ? Color.White : Color.Black;
                                botones[escenario][c].bordeBarraHor[0].Escala = botones[escenario][c].bordeBarraHor[1].Escala = new Vector2(1f, (float)botones[escenario][c].barra.Alto / botones[escenario][c].bordeBarraHor[0].AltoReal);
                                botones[escenario][c].bordeBarraHor[0].Posicion = new Vector2(botones[escenario][c].barra.Posicion.X - botones[escenario][c].bordeBarraHor[0].Ancho, botones[escenario][c].barra.Posicion.Y);
                                botones[escenario][c].bordeBarraHor[1].Posicion = new Vector2(botones[escenario][c].barra.Posicion.X + botones[escenario][c].barra.Ancho, botones[escenario][c].barra.Posicion.Y);
                                botones[escenario][c].bordeBarraVer[0].Escala = botones[escenario][c].bordeBarraVer[1].Escala = new Vector2(1f, ((float)botones[escenario][c].barra.Ancho + 2 * botones[escenario][c].bordeBarraHor[0].Ancho) / botones[escenario][c].bordeBarraVer[0].AltoReal);
                                botones[escenario][c].bordeBarraVer[0].Posicion = new Vector2(botones[escenario][c].bordeBarraHor[0].Posicion.X, botones[escenario][c].bordeBarraHor[0].Posicion.Y);
                                botones[escenario][c].bordeBarraVer[1].Posicion = new Vector2(botones[escenario][c].bordeBarraHor[0].Posicion.X, botones[escenario][c].bordeBarraHor[0].Posicion.Y + botones[escenario][c].bordeBarraHor[0].Alto + botones[escenario][c].bordeBarraVer[1].Ancho);
                                if (botones[escenario][c].texto.Texto == "Música")
                                {
                                    if (botones[escenario][c].valor == 0) MediaPlayer.Pause();
                                    else MediaPlayer.Resume();
                                }
                                break;
                        }


                        if (botones[escenario][c].ActualizarGenerico(this, gameTime, c)) break;


                        if (botones[escenario][c].tipo == TipoBotonMenu.Barra)
                        {
                            if (estadoMouse.X > botones[escenario][c].barraNegra.Posicion.X && estadoMouse.X < botones[escenario][c].barraNegra.Posicion.X + botones[escenario][c].barraNegra.Ancho &&
                                estadoMouse.Y > botones[escenario][c].barraNegra.Posicion.Y && estadoMouse.Y < botones[escenario][c].barraNegra.Posicion.Y + botones[escenario][c].barraNegra.Alto &&
                                estadoMouse.Y < barraAyudaInferior.Posicion.Y && (botones[5][2].valor == 0 ?
                                estadoMouse.LeftButton == ButtonState.Pressed && estadoMouseAnt.LeftButton == ButtonState.Released :
                                estadoMouse.RightButton == ButtonState.Pressed && estadoMouseAnt.RightButton == ButtonState.Released))
                            {
                                barraSeleccionada = c;
                            }
                        }



                        else if (botones[escenario][c].tipo == TipoBotonMenu.Seleccion)
                        {
                            if (estadoMouse.X > botones[escenario][c].flechaOpciones[0].Posicion.X && estadoMouse.X < botones[escenario][c].flechaOpciones[0].Posicion.X + botones[escenario][c].flechaOpciones[0].Ancho &&
                                estadoMouse.Y > botones[escenario][c].flechaOpciones[0].Posicion.Y && estadoMouse.Y < botones[escenario][c].flechaOpciones[0].Posicion.Y + botones[escenario][c].flechaOpciones[0].Alto &&
                                estadoMouse.Y < barraAyudaInferior.Posicion.Y)
                            {
                                if (botones[5][2].valor == 0 ? estadoMouse.LeftButton == ButtonState.Released && estadoMouseAnt.LeftButton == ButtonState.Pressed : estadoMouse.RightButton == ButtonState.Released && estadoMouseAnt.RightButton == ButtonState.Pressed)
                                {
                                    botones[escenario][c].valor--;
                                    if (botones[escenario][c].valor < 0) botones[escenario][c].valor = botones[escenario][c].opciones.Length - 1;
                                }
                                botones[escenario][c].flechaOpciones[0].Color = Color.DarkGreen;
                            }
                            else botones[escenario][c].flechaOpciones[0].Color = Color.White;

                            if (estadoMouse.X > botones[escenario][c].flechaOpciones[1].Posicion.X && estadoMouse.X < botones[escenario][c].flechaOpciones[1].Posicion.X + botones[escenario][c].flechaOpciones[0].Ancho &&
                                estadoMouse.Y > botones[escenario][c].flechaOpciones[1].Posicion.Y && estadoMouse.Y < botones[escenario][c].flechaOpciones[1].Posicion.Y + botones[escenario][c].flechaOpciones[0].Alto)
                            {
                                if (botones[5][2].valor == 0 ? estadoMouse.LeftButton == ButtonState.Released && estadoMouseAnt.LeftButton == ButtonState.Pressed : estadoMouse.RightButton == ButtonState.Released && estadoMouseAnt.RightButton == ButtonState.Pressed)
                                {
                                    botones[escenario][c].valor++;
                                    if (botones[escenario][c].valor >= botones[escenario][c].opciones.Length) botones[escenario][c].valor = 0;
                                }
                                botones[escenario][c].flechaOpciones[1].Color = Color.DarkGreen;
                            }
                            else botones[escenario][c].flechaOpciones[1].Color = Color.White;
                        }



                        else if (botones[escenario][c].tipo == TipoBotonMenu.SiNo) if (estadoMouse.X > botones[escenario][c].barra.Posicion.X && estadoMouse.X < botones[escenario][c].barra.Posicion.X + botones[escenario][c].barra.Ancho &&
                            estadoMouse.Y > botones[escenario][c].barra.Posicion.Y && estadoMouse.Y < botones[escenario][c].barra.Posicion.Y + botones[escenario][c].barra.Alto && (botones[5][2].valor == 0 ?
                            estadoMouse.LeftButton == ButtonState.Released && estadoMouseAnt.LeftButton == ButtonState.Pressed && estadoMouse.Y < barraAyudaInferior.Posicion.Y :
                            estadoMouse.RightButton == ButtonState.Released && estadoMouseAnt.RightButton == ButtonState.Pressed))
                        {
                            botones[escenario][c].valor = botones[escenario][c].valor == 0 ? 1 : 0;
                        }
                    }

                    switch (escenario)
                    {
                        case 0: textoAyudaSuperior.Texto = "Menú de pruebas"; break;
                        case 1: textoAyudaSuperior.Texto = 
                            botones[1][0].texto.Texto == "Reanudar" ? "Juego pausado" :
                            "Bienvenid@ a \"PhysicXNA\" " + perfiles[perfilSeleccionado].opciones.nombre; break;
                    }

                    break;
            }

            textoAyudaSuperior.Posicion = new Vector2((dimensiones.Width - textoAyudaSuperior.TamTexto.X) / 2, 20);

            estadoTecladoAnt = estadoTeclado;
            estadoMouseAnt = estadoMouse;

            base.Update(gameTime);
        }

        /// <summary>
        /// Dibuja los elementos del menú.
        /// </summary>
        /// <param name="gameTime">Tiempo del juego.</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(reproductorVideo.GetTexture(), dimensiones.Bounds, botones[1][0].texto.Texto == "Reanudar" ? new Color(32, 32, 255, 96) : Color.White);
            switch (escenario)
            {
                case (int)EscenarioMenu.Splash:
                    spriteBatch.End();
                    return;

                case (int)EscenarioMenu.PantallaInicio:
                    botonIniciar.Dibujar(spriteBatch, gameTime);
                    logo.Dibujar(spriteBatch, gameTime);
                    cursor.Dibujar(spriteBatch, gameTime);
                    spriteBatch.End();
                    return;

                case (int)EscenarioMenu.VerPerfiles:
                    for (c = pElemento; c < listaBotonesPerfil.Count; c++) listaBotonesPerfil[c].Dibujar(spriteBatch, gameTime);
                    for (c = pElemento; c < botonesMenuPerfiles[opcionesPerfilSeleccionadas ? 1 : 0].Length; c++) botonesMenuPerfiles[opcionesPerfilSeleccionadas ? 1 : 0][c].Dibujar(spriteBatch, gameTime);
                    break;

                case (int)EscenarioMenu.CopiarPerfil:
                case (int)EscenarioMenu.RenombrarPerfil:
                case (int)EscenarioMenu.CrearPerfil:
                    tecladoNombres.Dibujar(spriteBatch, gameTime);
                    foreach (BotonMenu boton in botonesOKCancelar) boton.Dibujar(spriteBatch, gameTime);
                    break;

                case (int)EscenarioMenu.PrimerArranque:
                    tecladoNombres.Dibujar(spriteBatch, gameTime);
                    botonesOKCancelar[0].Dibujar(spriteBatch, gameTime);
                    break;

                case (int)EscenarioMenu.ConfirmarSalirNivel:
                case (int)EscenarioMenu.ConfirmarReiniciarNivel:
                case (int)EscenarioMenu.SalirJuego:
                case (int)EscenarioMenu.EliminarPerfil:
                    foreach (BotonMenu boton in botonesOKCancelar) boton.Dibujar(spriteBatch, gameTime);
                    break;

                case (int)EscenarioMenu.ReglasNivel:
                    fondoTexto.Dibujar(spriteBatch, gameTime);
                    tituloReglas[pElemento].Dibujar(spriteBatch, gameTime);
                    textoReglas.Dibujar(spriteBatch, gameTime);
                    botonVolver.Dibujar(spriteBatch, gameTime);
                    break;

                case (int)EscenarioMenu.SabiasQue:
                    fondoTexto.Dibujar(spriteBatch, gameTime);
                    tituloReglas[pElemento].Dibujar(spriteBatch, gameTime);
                    textoReglas.Dibujar(spriteBatch, gameTime);
                    botonContinuar.Dibujar(spriteBatch, gameTime);
                    break;

                case (int)EscenarioMenu.VerPuntuaciones:
                    botonVolver.Dibujar(spriteBatch, gameTime);
                    textoPuntuaciones.Dibujar(spriteBatch, gameTime);
                    break;

                case (int)EscenarioMenu.CargarPerfil:

                    break;

                default:

                    for (c = pElemento, invisibles = 0; c < botones[escenario].Length; c++)
                    {
                        if (!botones[escenario][c].visible)
                        {
                            invisibles++;
                            continue;
                        }

                        botones[escenario][c].Dibujar(spriteBatch, gameTime);
                    }

                    break;
            }

            barraAyudaSuperior.Dibujar(spriteBatch, gameTime);
            textoAyudaSuperior.Dibujar(spriteBatch, gameTime);
            barraAyudaInferior.Dibujar(spriteBatch, gameTime);
            if (mostrarScroll) indicadorScroll.Dibujar(spriteBatch, gameTime);
            textoAyudaInferior.Dibujar(spriteBatch, gameTime);
            textoAdvertencia.Dibujar(spriteBatch, gameTime);

            cursor.Dibujar(spriteBatch, gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
