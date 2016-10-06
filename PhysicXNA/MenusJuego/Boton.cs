using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using PhysicXNA.Nucleo;
using PhysicXNA.SistemaPerfiles;

namespace PhysicXNA.MenusJuego
{
    /// <summary>Clase que contiene los elementos de un botón que forma parte de un escenario del menú.</summary>
    /// <remarks>Contiene información de la intención del botón.</remarks>
    public class BotonMenu
    {
        // Generales

        /// <summary>Tipo de botón. Véase <see cref="TipoBotonMenu"/></summary>
        public TipoBotonMenu tipo;
        /// <summary>Textura del botón.</summary>
        public DuendeEstatico textura;
        /// <summary>Texto que aparecerá en el botón.</summary>
        public Texto3D texto;
        /// <summary>Indica si el elemento se va a dibujar o no.</summary>
        public bool visible;
        /// <summary>Indica si el elemento se puede accionar en caso de que sea un botón de acción.</summary>
        public bool habilitado;


        // Opcionales

        /// <summary>
        /// <para>El valor de un botón puede tener distintos usos dependiendo de su tipo.</para>
        /// <para>Si es un botón booleano (SiNo) 0 indicará desmarcado y otro valor marcado.</para>
        /// <para>Si es de acción y es de cambiar escenario, indicará el nuevo escenario o sub-menú a ser mostrado</para>
        /// <para>Si es una barra deslizable, indicará el valor seleccionado</para>
        /// <para>Si es un selector de opciones, indicará el índice de la opción seleccionada</para>
        /// </summary>
        public int valor;
        /// <summary>Texto de ayuda que aparecerá en la parte inferior de la pantalla al posar el puntero del mouse sobre el botón.</summary>
        public String ayuda;
        /// <summary>Acción que ejecutará el botón en caso de ser botón de acción. Véase <see cref="AccionBotonMenu"/></summary>
        public AccionBotonMenu accion;
        /// <summary>Barra deslizable en caso de ser botón de barra.</summary>
        public DuendeEstatico barra;
        /// <summary>Barra completa negra mostrada debajo de la barra deslizable para indicar el valor completo.</summary>
        public DuendeEstatico barraNegra;
        /// <summary>Borde horizontal de la barra deslizable.</summary>
        public DuendeEstatico[] bordeBarraHor;
        /// <summary>Borde horizontal de la barra deslizable.</summary>
        public DuendeEstatico[] bordeBarraVer;
        /// <summary>Indicador pequeño de la barra deslizable.</summary>
        public DuendeEstatico indicadorBarra;
        /// <summary>Botón de opción en caso de que sea botón de selección de opciones.</summary>
        public DuendeEstatico botonOpcion;
        /// <summary>Flechas de cambio de opción en caso de que sea un botón de selección de opciones.</summary>
        public DuendeEstatico[] flechaOpciones;
        /// <summary>Opciones posibles a seleccionar en caso de que sea botón de selección.</summary>
        public String[] opciones;
        /// <summary>Texto que adopta el botón para cada opción seleccionada.</summary>
        public Texto3D textoOpcion;
        /// <summary>Valor máximo que puede adoptar la barra deslizable en caso de que el botón sea de ese tipo.</summary>
        public int maximo;



        /// <summary>Auxiliar para el constructor</summary>
        private void ConstruirElementos(String nombre)
        {
            textura = new DuendeEstatico("Menu/Boton", Vector2.Zero, Color.White, new Vector2(0.3f, 0.2f));
            texto = new Texto3D("Menu/FuenteMenu", nombre, Vector2.Zero, 30, Color.GreenYellow);
            visible = habilitado = true;

            if (tipo == TipoBotonMenu.Barra || tipo == TipoBotonMenu.SiNo)
            {
                barra = new DuendeEstatico("Menu/Barra", Vector2.Zero);
                bordeBarraHor = new DuendeEstatico[2];
                bordeBarraVer = new DuendeEstatico[2];
                bordeBarraHor[0] = new DuendeEstatico("Menu/BordeBarra", Vector2.Zero);
                bordeBarraHor[1] = new DuendeEstatico("Menu/BordeBarra", Vector2.Zero, Color.White, Vector2.Zero, SpriteEffects.FlipHorizontally);
                bordeBarraVer[0] = new DuendeEstatico("Menu/BordeBarra", Vector2.Zero, Color.White, Vector2.Zero, SpriteEffects.FlipHorizontally, -(float)Math.PI / 2);
                bordeBarraVer[1] = new DuendeEstatico("Menu/BordeBarra", Vector2.Zero, Color.White, Vector2.Zero, SpriteEffects.None, -(float)Math.PI / 2);
                if (tipo == TipoBotonMenu.Barra)
                {
                    barraNegra = new DuendeEstatico("Menu/Barra", Vector2.Zero, Color.Black);
                    textoOpcion = new Texto3D("Menu/FuenteMenu", String.Empty, Vector2.Zero, 30, Color.Red);
                    indicadorBarra = new DuendeEstatico("Menu/Indicador", Vector2.Zero);
                }
            }
            else if (tipo == TipoBotonMenu.Seleccion)
            {
                flechaOpciones = new DuendeEstatico[2];
                flechaOpciones[0] = new DuendeEstatico("Menu/Flecha", Vector2.Zero, Color.White, new Vector2(0.1f, 0.1f), SpriteEffects.FlipHorizontally);
                flechaOpciones[1] = new DuendeEstatico("Menu/Flecha", Vector2.Zero, Color.White, new Vector2(0.1f, 0.1f));
                botonOpcion = new DuendeEstatico("Menu/Boton", Vector2.Zero, Color.White, new Vector2(0.3f, 0.2f));
                textoOpcion = new Texto3D("Menu/FuenteMenu", "", Vector2.Zero, 30, Color.GreenYellow);
            }
        }

        /// <summary>
        /// Constructor para botón de acción
        /// </summary>
        /// <param name="nombre">Texto que aparecerá en el botón.</param>
        /// <param name="tipo">Tipo de botón. Para este constructor debe ser TipoBotonMenu.Accion</param>
        /// <param name="accion">Acción que ejecutará el botón al ser pulsado.</param>
        /// <param name="ayuda">Texto de ayuda opcional que aparecerá en la parte inferior de la pantalla.</param>
        /// <param name="nuevoEscenario">Sub-menu que será mostrado si es un botón de cambio de escenario.</param>
        public BotonMenu(String nombre, TipoBotonMenu tipo, AccionBotonMenu accion, String ayuda = "", int nuevoEscenario = 0)
        {
            this.tipo = tipo;
            this.ayuda = ayuda;
            this.accion = accion;
            this.valor = nuevoEscenario;
            ConstruirElementos(nombre);
        }

        /// <summary>
        /// Constructor para botón de barra o booleano
        /// </summary>
        /// <param name="nombre">Texto que aparecerá en el botón.</param>
        /// <param name="tipo">Tipo de botón. Para este constructor debe ser TipoBotonMenu.SiNo o TipoBotonMenu.Barra</param>
        /// <param name="ayuda">Texto de ayuda que aparecerá en la parte inferior de la pantalla.</param>
        /// <param name="valor">Valor por default del botón. Véase <see cref="valor"/></param>
        /// <param name="maximo">Valor máximo que puede tomar el botón si es un botón de barra.</param>
        public BotonMenu(String nombre, TipoBotonMenu tipo, String ayuda, int valor, int maximo = 0)
        {
            this.tipo = tipo;
            this.ayuda = ayuda;
            this.valor = valor;
            this.maximo = maximo;
            ConstruirElementos(nombre);
        }

        /// <summary>
        /// Constructor para botón de selección de opciones
        /// </summary>
        /// <param name="nombre">Texto que aparecerá en el botón.</param>
        /// <param name="tipo">Tipo de botón. Para este constructor debe ser TipoBotonMenu.Seleccion</param>
        /// <param name="ayuda">Texto de ayuda que aparecerá en la parte inferior de la pantalla.</param>
        /// <param name="valor">Valor por default del botón. Véase <see cref="valor"/></param>
        /// <param name="opciones">Arreglo de opciones.</param>
        public BotonMenu(String nombre, TipoBotonMenu tipo, String ayuda, int valor, String[] opciones)
        {
            this.tipo = tipo;
            this.ayuda = ayuda;
            this.valor = valor;
            this.opciones = opciones;
            ConstruirElementos(nombre);
        }


        /// <summary>
        /// Carga el contenido para cada elemento del botón del menú.
        /// </summary>
        /// <param name="contenido">Administrador de contenidos del juego para cargar el contenido.</param>
        public void CargarContenido(ContentManager contenido)
        {
            textura.CargarContenido(contenido);
            texto.CargarContenido(contenido);

            if (tipo == TipoBotonMenu.Barra || tipo == TipoBotonMenu.SiNo)
            {
                barra.CargarContenido(contenido);
                bordeBarraHor[0].CargarContenido(contenido);
                bordeBarraHor[1].CargarContenido(contenido);
                bordeBarraVer[0].CargarContenido(contenido);
                bordeBarraVer[1].CargarContenido(contenido);
                if (tipo == TipoBotonMenu.Barra)
                {
                    barraNegra.CargarContenido(contenido);
                    textoOpcion.CargarContenido(contenido);
                    indicadorBarra.CargarContenido(contenido);
                }
            }

            else if (tipo == TipoBotonMenu.Seleccion)
            {
                flechaOpciones[0].CargarContenido(contenido);
                flechaOpciones[1].CargarContenido(contenido);
                botonOpcion.CargarContenido(contenido);
                textoOpcion.CargarContenido(contenido);
            }
        }

        /// <summary>Libera los recursos ocupados por los elementos del botón del menú.</summary>
        public void LiberarContenido()
        {
            textura.LiberarContenido();
            texto.LiberarContenido();

            if (tipo == TipoBotonMenu.SiNo || tipo == TipoBotonMenu.Barra)
            {
                barra.LiberarContenido();
                bordeBarraHor[0].LiberarContenido();
                bordeBarraHor[1].LiberarContenido();
                bordeBarraVer[0].LiberarContenido();
                bordeBarraVer[1].LiberarContenido();
                if (tipo == TipoBotonMenu.Barra)
                {
                    barraNegra.LiberarContenido();
                    indicadorBarra.LiberarContenido();
                    textoOpcion.LiberarContenido();
                }
            }
            else if (tipo == TipoBotonMenu.Seleccion)
            {
                botonOpcion.LiberarContenido();
                flechaOpciones[0].LiberarContenido();
                flechaOpciones[1].LiberarContenido();
                textoOpcion.LiberarContenido();
            }
        }

        private void CargarDatosPerfil(Menu menuOrigen)
        {
            PerfilJugador perfilAuxiliarTemporal = menuOrigen.perfiles[menuOrigen.perfilSeleccionado];
            menuOrigen.botones[2][3].valor = (int)perfilAuxiliarTemporal.opciones.dificultad;
            int resolucionEscogida = Pantalla.ObtenerResoluciones().IndexOf(perfilAuxiliarTemporal.opciones.res_horizontal.ToString() + " x " + perfilAuxiliarTemporal.opciones.res_vertical.ToString());
            menuOrigen.botones[3][1].valor = resolucionEscogida < 0 ? 0 : resolucionEscogida;
            menuOrigen.botones[3][2].valor = perfilAuxiliarTemporal.opciones.pantalla_completa;
            menuOrigen.botones[4][1].valor = perfilAuxiliarTemporal.opciones.musica;
            menuOrigen.botones[4][2].valor = perfilAuxiliarTemporal.opciones.sonidos;
            menuOrigen.botones[4][3].valor = perfilAuxiliarTemporal.opciones.volumen;
            menuOrigen.botones[5][1].valor = perfilAuxiliarTemporal.opciones.wasd;
            menuOrigen.botones[5][2].valor = perfilAuxiliarTemporal.opciones.invertir_mouse;

            if (perfilAuxiliarTemporal.opciones.musica == 0)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(menuOrigen.cancion);
                MediaPlayer.Pause();
            }
            else if (MediaPlayer.State == MediaState.Paused) MediaPlayer.Resume();

            int nivel = perfilAuxiliarTemporal.opciones.nivel;
            bool juegoCompletado = perfilAuxiliarTemporal.opciones.juego_completado != 0;

            if (juegoCompletado && nivel == 0)
            {
                menuOrigen.botones[1][0].texto.Texto = "Volver a jugar";
                menuOrigen.botones[1][0].ayuda = "Vuelve a comenzar el juego desde el primer nivel.";
            }
            else
            {
                if (nivel == 0)
                {
                    menuOrigen.botones[1][0].texto.Texto = "Comenzar juego";
                    menuOrigen.botones[1][0].ayuda = "Comenzar a jugar.";
                }
                else
                {
                    menuOrigen.botones[1][0].texto.Texto = "Continuar juego";
                    menuOrigen.botones[1][0].ayuda = "Continua el juego desde el último nivel guardado.";
                }
            }
            menuOrigen.botones[1][1].visible = juegoCompletado;
            menuOrigen.pElemento = 0;
            menuOrigen.escenario = (int)EscenarioMenu.CargarPerfil;
        }

        /// <summary>
        /// Realiza algunas operaciones genéricas de actualización de estado de los botones del menú. Entre ellas:
        /// <para> - Comprobar si el mouse está posado sobre el botón.</para>
        /// <para> - Ejecutar la acción del botón.</para>
        /// <para> - Alinear el texto del botón.</para>
        /// </summary>
        /// <param name="menuOrigen">Menú al que pertenece el botón.</param>.
        /// <param name="tiempoJuego">Tiempo del juego.</param>
        /// <param name="idBoton">Id del botón en el submenú.</param>
        /// <returns>Indica si es necesario romper el ciclo de actualización.</returns>
        public bool ActualizarGenerico(Menu menuOrigen, GameTime tiempoJuego, int idBoton)
        {
            if (habilitado)
            {
                if (menuOrigen.estadoMouse.X > textura.Posicion.X && menuOrigen.estadoMouse.X < textura.Posicion.X + textura.Ancho &&
                    menuOrigen.estadoMouse.Y > textura.Posicion.Y && menuOrigen.estadoMouse.Y < textura.Posicion.Y + textura.Alto &&
                    menuOrigen.estadoMouse.Y < menuOrigen.barraAyudaInferior.Posicion.Y)
                {
                    if (tipo == TipoBotonMenu.Accion && (menuOrigen.botones[5][2].valor == 0 ? menuOrigen.estadoMouse.LeftButton == ButtonState.Released && menuOrigen.estadoMouseAnt.LeftButton == ButtonState.Pressed :
                        menuOrigen.estadoMouse.RightButton == ButtonState.Released && menuOrigen.estadoMouseAnt.RightButton == ButtonState.Pressed))
                    {
                        if (menuOrigen.botones[4][2].valor != 0) menuOrigen.click.Play();
                        switch (accion)
                        {
                            case AccionBotonMenu.GuardarPerfil:
                                PerfilJugador perfilTemporal = menuOrigen.perfiles[menuOrigen.perfilSeleccionado];
                                String[] dimensiones = new String[2];
                                dimensiones = menuOrigen.botones[3][1].opciones[menuOrigen.botones[3][1].valor].Split(new char[] { 'x' }, 2);
                                perfilTemporal.opciones = new OpcionesJuego(perfilTemporal.opciones.nombre, perfilTemporal.opciones.nivel,
                                    perfilTemporal.opciones.juego_completado != 0, (DificultadJuego)menuOrigen.botones[2][3].valor,
                                    new Point(int.Parse(dimensiones[0]), int.Parse(dimensiones[1])), menuOrigen.botones[3][2].valor != 0,
                                    menuOrigen.botones[4][2].valor != 0, menuOrigen.botones[4][1].valor != 0, menuOrigen.botones[4][3].valor,
                                    menuOrigen.botones[5][1].valor != 0, menuOrigen.botones[5][2].valor != 0);
                                menuOrigen.perfiles[menuOrigen.perfilSeleccionado] = perfilTemporal;
                                menuOrigen.perfiles.Guadar(ListaPerfilesJugador.rutaPerfiles);
                                menuOrigen.pElemento = 0;
                                menuOrigen.escenario = (int)EscenarioMenu.CargarPerfil;
                                if (menuOrigen.botones[1][0].texto.Texto == "Reanudar") MediaPlayer.Pause();
                                return true;

                            case AccionBotonMenu.CargarPerfil:
                                CargarDatosPerfil(menuOrigen);
                                return true;

                            case AccionBotonMenu.CambiarEscenario:
                                menuOrigen.tecladoNombres.Texto = menuOrigen.textoAdvertencia.Texto = "";
                                menuOrigen.escenario = valor;
                                if (menuOrigen.escenario == (int)EscenarioMenu.VerPerfiles)
                                {
                                    menuOrigen.perfilSeleccionado = 0;
                                    menuOrigen.opcionesPerfilSeleccionadas = false;
                                }
                                menuOrigen.pElemento = 0;
                                return true;

                            case AccionBotonMenu.OperacionesPerfil:
                                menuOrigen.opcionesPerfilSeleccionadas = !menuOrigen.opcionesPerfilSeleccionadas;
                                menuOrigen.pElemento = 0;
                                if (!menuOrigen.opcionesPerfilSeleccionadas) menuOrigen.perfilSeleccionado = 0;
                                break;

                            case AccionBotonMenu.OK:
                                bool err = false;
                                switch (menuOrigen.escenario)
                                {
                                    case (int)EscenarioMenu.CrearPerfil:
                                        if (menuOrigen.tecladoNombres.Texto == "") { menuOrigen.textoAdvertencia.Texto = "Porfavor, escriba un nombre."; err = true; }
                                        else if (menuOrigen.perfiles.ExistePerfil(menuOrigen.tecladoNombres.Texto)) { menuOrigen.textoAdvertencia.Texto = "Ya existe otro perfil con el mismo nombre."; err = true; }
                                        else
                                        {
                                            menuOrigen.perfiles.AgregarPerfil(new PerfilJugador(menuOrigen.tecladoNombres.Texto));
                                            BotonMenu nuevoBoton = new BotonMenu(menuOrigen.tecladoNombres.Texto, TipoBotonMenu.Accion, AccionBotonMenu.SeleccionarPerfil);
                                            nuevoBoton.CargarContenido(menuOrigen.Content);
                                            menuOrigen.listaBotonesPerfil.Add(nuevoBoton);
                                            menuOrigen.textoAdvertencia.Texto = "";
                                            menuOrigen.tecladoNombres.Texto = "";
                                            menuOrigen.escenario = (int)EscenarioMenu.VerPerfiles;
                                        }
                                        break;

                                    case (int)EscenarioMenu.PrimerArranque:
                                        if (menuOrigen.tecladoNombres.Texto == "") { menuOrigen.textoAdvertencia.Texto = "Porfavor, escriba un nombre."; err = true; }
                                        else
                                        {
                                            menuOrigen.perfiles.AgregarPerfil(new PerfilJugador(menuOrigen.tecladoNombres.Texto));
                                            BotonMenu nuevoBoton = new BotonMenu(menuOrigen.tecladoNombres.Texto, TipoBotonMenu.Accion, AccionBotonMenu.SeleccionarPerfil);
                                            nuevoBoton.CargarContenido(menuOrigen.Content);
                                            menuOrigen.listaBotonesPerfil.Add(nuevoBoton);
                                            menuOrigen.textoAdvertencia.Texto = "";
                                            menuOrigen.tecladoNombres.Texto = "";
                                            menuOrigen.escenario = 1;
                                            menuOrigen.perfilSeleccionado = 1;
                                            menuOrigen.pElemento = 0;
                                            menuOrigen.perfiles.Guadar(ListaPerfilesJugador.rutaPerfiles);
                                            CargarDatosPerfil(menuOrigen);
                                            return true;
                                        }
                                        break;

                                    case (int)EscenarioMenu.CopiarPerfil:
                                        if (menuOrigen.tecladoNombres.Texto == "") { menuOrigen.textoAdvertencia.Texto = "Porfavor, escriba un nombre."; err = true; }
                                        else if (menuOrigen.perfiles.ExistePerfil(menuOrigen.tecladoNombres.Texto)) { menuOrigen.textoAdvertencia.Texto = "Ya existe otro perfil con el mismo nombre."; err = true; }
                                        else
                                        {
                                            PerfilJugador perfilAuxiliar = menuOrigen.perfiles[menuOrigen.perfilSeleccionado];
                                            perfilAuxiliar.opciones.nombre = menuOrigen.tecladoNombres.Texto;
                                            menuOrigen.perfiles.AgregarPerfil(perfilAuxiliar);
                                            BotonMenu nuevoBoton = new BotonMenu(menuOrigen.tecladoNombres.Texto, TipoBotonMenu.Accion, AccionBotonMenu.SeleccionarPerfil);
                                            nuevoBoton.CargarContenido(menuOrigen.Content);
                                            menuOrigen.listaBotonesPerfil.Add(nuevoBoton);
                                            menuOrigen.textoAdvertencia.Texto = "";
                                            menuOrigen.tecladoNombres.Texto = "";
                                            menuOrigen.escenario = (int)EscenarioMenu.VerPerfiles;
                                        }
                                        break;

                                    case (int)EscenarioMenu.RenombrarPerfil:
                                        if (menuOrigen.tecladoNombres.Texto == "") { menuOrigen.textoAdvertencia.Texto = "Porfavor, escriba un nombre."; err = true; }
                                        else if (menuOrigen.perfiles.ExistePerfil(menuOrigen.tecladoNombres.Texto)) { menuOrigen.textoAdvertencia.Texto = "Ya existe otro perfil con el mismo nombre."; err = true; }
                                        else
                                        {
                                            PerfilJugador perfilAuxiliar = menuOrigen.perfiles[menuOrigen.perfilSeleccionado];
                                            perfilAuxiliar.opciones.nombre = menuOrigen.tecladoNombres.Texto;
                                            menuOrigen.perfiles[menuOrigen.perfilSeleccionado] = perfilAuxiliar;
                                            menuOrigen.listaBotonesPerfil[menuOrigen.perfilSeleccionado - 1].texto.Texto = menuOrigen.tecladoNombres.Texto;
                                            menuOrigen.textoAdvertencia.Texto = "";
                                            menuOrigen.tecladoNombres.Texto = "";
                                            menuOrigen.escenario = (int)EscenarioMenu.VerPerfiles;
                                        }
                                        break;

                                    case (int)EscenarioMenu.EliminarPerfil:
                                        menuOrigen.perfiles.EliminarPerfil(menuOrigen.perfilSeleccionado);
                                        menuOrigen.listaBotonesPerfil.RemoveAt(menuOrigen.perfilSeleccionado - 1);
                                        menuOrigen.textoAdvertencia.Texto = "";
                                        menuOrigen.tecladoNombres.Texto = "";
                                        menuOrigen.escenario = (int)EscenarioMenu.VerPerfiles;
                                        break;

                                    case (int)EscenarioMenu.SalirJuego:
                                        menuOrigen.juego.Exit();
                                        break;

                                    case (int)EscenarioMenu.ConfirmarReiniciarNivel:
                                        menuOrigen.Visible = menuOrigen.Enabled = false;
                                        menuOrigen.escenario = (int)EscenarioMenu.ReiniciarNivel;
                                        break;

                                    case (int)EscenarioMenu.ConfirmarSalirNivel:
                                        menuOrigen.Visible = menuOrigen.Enabled = false;
                                        menuOrigen.BotonesMenu(false);
                                        menuOrigen.escenario = (int)EscenarioMenu.SalirNivel;
                                        break;
                                }

                                if (!err)
                                {
                                    if (menuOrigen.escenario == (int)EscenarioMenu.VerPerfiles)
                                    {
                                        menuOrigen.perfilSeleccionado = 0;
                                        menuOrigen.opcionesPerfilSeleccionadas = false;
                                    }
                                    menuOrigen.pElemento = 0;
                                    menuOrigen.perfiles.Guadar(ListaPerfilesJugador.rutaPerfiles);
                                }
                                break;

                            case AccionBotonMenu.NuevoPerfil:
                                menuOrigen.escenario = (int)EscenarioMenu.CrearPerfil;
                                menuOrigen.opcionesPerfilSeleccionadas = false;
                                break;

                            case AccionBotonMenu.SeleccionarPerfil:
                                menuOrigen.perfilSeleccionado = idBoton + 1;
                                break;

                            case AccionBotonMenu.Continuar:
                                if (!menuOrigen.jugarHistoria)
                                {
                                    if(menuOrigen.perfiles[menuOrigen.perfilSeleccionado].opciones.musica != 0) MediaPlayer.Play(menuOrigen.cancion);
                                    if (menuOrigen.reproductorVideo.State != MediaState.Stopped) menuOrigen.reproductorVideo.Stop();
                                    menuOrigen.reproductorVideo.Play(menuOrigen.fondo);
                                    MediaPlayer.Stop();
                                    MediaPlayer.Play(menuOrigen.cancion);
                                    if (menuOrigen.perfiles[menuOrigen.perfilSeleccionado].opciones.musica == 0) MediaPlayer.Pause();
                                    menuOrigen.escenario = 1;
                                }
                                else
                                {
                                    if (menuOrigen.reproductorVideo.State != MediaState.Stopped) menuOrigen.reproductorVideo.Stop();
                                    menuOrigen.reproductorVideo.Play(menuOrigen.fondoPausa);
                                    menuOrigen.reproductorVideo.Pause();
                                    menuOrigen.Enabled = menuOrigen.Visible = false;
                                    menuOrigen.pElemento = 0;
                                    menuOrigen.escenario = 1;
                                    if (menuOrigen.mostrarCreditos) menuOrigen.jugarHistoria = false;
                                    menuOrigen.BotonesMenu(true);
                                }
                                break;

                            case AccionBotonMenu.Jugar:
                                if (menuOrigen.reproductorVideo.State != MediaState.Stopped) menuOrigen.reproductorVideo.Stop();
                                menuOrigen.reproductorVideo.Play(menuOrigen.fondoPausa);
                                menuOrigen.reproductorVideo.Pause();
                                menuOrigen.Enabled = menuOrigen.Visible = false;
                                menuOrigen.pElemento = 0;
                                if (texto.Texto != "Reanudar") menuOrigen.jugarHistoria = texto.Texto != "Jugar";
                                menuOrigen.BotonesMenu(true);
                                break;
                        }
                    }

                    textura.Color = accion == AccionBotonMenu.SeleccionarPerfil && menuOrigen.perfilSeleccionado == idBoton + 1 ? Color.DarkRed : Color.DarkRed;
                    texto.Actualizar(tiempoJuego);
                    menuOrigen.textoAyudaInferior.Texto = ayuda;
                    menuOrigen.textoAyudaInferior.Posicion = new Vector2(20, menuOrigen.barraAyudaInferior.Posicion.Y + 20);
                }
                else if (accion != AccionBotonMenu.SeleccionarPerfil || menuOrigen.perfilSeleccionado != idBoton + 1)
                {
                    textura.Color = Color.White;
                    texto.Reiniciar();
                }
                else
                {
                    textura.Color = Color.Red;
                    texto.Actualizar(tiempoJuego);
                }
            }
            else
            {
                textura.Color = new Color(255, 255, 255, 24);
                texto.Reiniciar();
            }

            texto.Posicion = new Vector2(textura.Posicion.X + (textura.Ancho - texto.TamTexto.X) / 2,
                                textura.Posicion.Y + (textura.Alto - texto.TamTexto.Y) / 2);

            return false;
        }

        /// <summary>
        /// Dibuja todos los elementos del botón según su tipo.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch.</param>
        /// <param name="tiempoJuego">Tiempo del juego.</param>
        public void Dibujar(SpriteBatch spriteBatch, GameTime tiempoJuego)
        {
            textura.Dibujar(spriteBatch, tiempoJuego);
            texto.Dibujar(spriteBatch, tiempoJuego);
            if (tipo == TipoBotonMenu.Barra || tipo == TipoBotonMenu.SiNo)
            {
                if (tipo == TipoBotonMenu.Barra)
                {
                    barraNegra.Dibujar(spriteBatch, tiempoJuego);
                    textoOpcion.Dibujar(spriteBatch, tiempoJuego);
                }

                barra.Dibujar(spriteBatch, tiempoJuego);
                bordeBarraHor[0].Dibujar(spriteBatch, tiempoJuego);
                bordeBarraHor[1].Dibujar(spriteBatch, tiempoJuego);
                bordeBarraVer[0].Dibujar(spriteBatch, tiempoJuego);
                bordeBarraVer[1].Dibujar(spriteBatch, tiempoJuego);

                if (tipo == TipoBotonMenu.Barra) indicadorBarra.Dibujar(spriteBatch, tiempoJuego);
            }

            if (tipo == TipoBotonMenu.Seleccion)
            {
                flechaOpciones[0].Dibujar(spriteBatch, tiempoJuego);
                flechaOpciones[1].Dibujar(spriteBatch, tiempoJuego);
                botonOpcion.Dibujar(spriteBatch, tiempoJuego);
                textoOpcion.Dibujar(spriteBatch, tiempoJuego);
            }
        }
    }
}
