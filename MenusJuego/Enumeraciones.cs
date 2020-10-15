namespace PhysicXNA.MenusJuego
{
    /// <summary>Indica que escenario se está mostrando del menú.</summary>
    public enum EscenarioMenu : int
    {
        /// <summary>Presentación inicial.</summary>
        Splash = 0x0F00,
        /// <summary>Pantalla que se muestra cuando se inicial el juego sin haber ningún perfil guardado.</summary>
        PrimerArranque = 0x0F01,
        /// <summary>Pantalla de selección de perfiles.</summary>
        VerPerfiles = 0x0F02,
        /// <summary>Pantalla de creación de perfiles.</summary>
        CrearPerfil = 0x0F03,
        /// <summary>Pantalla de renombramiento de perfiles.</summary>
        RenombrarPerfil = 0x0F04,
        /// <summary>Pantalla donde escribirá el nuevo nombre del perfil a copiar.</summary>
        CopiarPerfil = 0x0F05,
        /// <summary>Pantalla de confirmación de eliminación de perfil.</summary>
        EliminarPerfil = 0x0F06,
        /// <summary>Pantalla donde se visualizan sus puntuaciones.</summary>
        VerPuntuaciones = 0x0F07,
        /// <summary>Manda a cambiar la resolución de pantalla del juego y guarda los datos del perfil para actualizarlos.</summary>
        CargarPerfil = 0x0F08,
        /// <summary>Manda a reiniciar el nivel que se está ejecutando actualmente.</summary>
        ReiniciarNivel = 0x0F09,
        /// <summary>Pantalla de confirmación de salir del juego.</summary>
        SalirJuego = 0x0F0A,
        /// <summary>Pantalla de confirmación de salir del nivel.</summary>
        ConfirmarSalirNivel = 0x0F0B,
        /// <summary>Pantalla de confirmación de reiniciar el nivel.</summary>
        ConfirmarReiniciarNivel = 0x0F0C,
        /// <summary>Manda a salir del nivel.</summary>
        SalirNivel = 0x0F0D,
        /// <summary>Ver las reglas del nivel.</summary>
        ReglasNivel = 0x0F0E,
        /// <summary>Pantalla que se muestra al acabar el video de Splash.</summary>
        PantallaInicio = 0x0F0F,
        /// <summary>Pantalla con un mensaje estilo sabías qué?... que aparece al terminar cada nivel.</summary>
        SabiasQue = 0x0F10,
    }

    /// <summary>Indica el uso del botón de menú</summary>
    public enum TipoBotonMenu
    {
        /// <summary>Botón que ejecutará alguna acción al ser clickeado, Véase <see cref="AccionBotonMenu"/>.</summary>
        Accion,
        /// <summary>Botón de selección booleana.</summary>
        SiNo,
        /// <summary>Botón de selección de valores en una barra deslizable.</summary>
        Barra,
        /// <summary>Botón de selección de múltiples opciones.</summary>
        Seleccion,
    }

    /// <summary>Indica la acción que puede ejecutar un botón de menú en caso de que sea botón de acción.</summary>
    public enum AccionBotonMenu
    {
        /// <summary>Botón que inicia el juego.</summary>
        Jugar,
        /// <summary>Botón que cambia el escenario del menú. Véase <see cref="EscenarioMenu"/></summary>
        CambiarEscenario,
        /// <summary>Botón que manda a cargar toda la información del perfil de jugador.</summary>
        CargarPerfil,
        /// <summary>Botón que guarda toda la información del perfil de jugador.</summary>
        GuardarPerfil,
        /// <summary>Botón de perfil de jugador.</summary>
        SeleccionarPerfil,
        /// <summary>Botón de creación de nuevo perfil de jugador.</summary>
        NuevoPerfil,
        /// <summary>Botón de confirmación de acción a perfil.<para>Funciona diferente para los escenarios <see cref="EscenarioMenu.CrearPerfil"/>, <see cref="EscenarioMenu.RenombrarPerfil"/>, <see cref="EscenarioMenu.EliminarPerfil"/> y <see cref="EscenarioMenu.CopiarPerfil"/>.</para></summary>
        OK,
        /// <summary>Botón que muestra u oculta las operaciones que se le pueden realizar a un perfil de jugador.</summary>
        OperacionesPerfil,
        /// <summary>Botón que realiza la operación de continuar la partida cuando finalizó un nivel.</summary>
        Continuar,
    }
}

