using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;

namespace PhysicXNA.SistemaPerfiles
{
    /// <summary>Dificultad en la que se ejecuta el juego, usada en algunos niveles.</summary>
    public enum DificultadJuego : byte
    {
        /// <summary>Fácil.</summary>
        Fácil       = 0,
        /// <summary>Medio.</summary>
        Medio       = 1,
        /// <summary>Difícil.</summary>
        Difícil     = 2
    }

    /// <summary>Estructura que contiene el nombre del perfil, último nivel alcanzado, dificultad del juego y opciones de vídeo, sonido y controles.</summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 8)]
    public struct OpcionesJuego
    {
        /// <summary>Nombre del perfil.</summary><remarks>NOTA: No pueden haber 2 perfiles con el mismo nombre y no puede exceder los 40 caracteres.</remarks>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ListaPerfilesJugador.tamNombresJugador)]
        public String nombre;
        /// <summary>Último nivel del juego superado.</summary><remarks>NOTA: No puede exceder 5.</remarks>
	    public Byte nivel;
        /// <summary>Booleano que indica si ya se han completado todos los niveles del juego por lo menos una vez.</summary>
        public Byte juego_completado;
        /// <summary>Dificultad del juego elegida. Véase <see cref="DificultadJuego"/></summary>
        public DificultadJuego dificultad;

	    // Opciones de video
        /// <summary>Resolución horizontal de pantalla.</summary>
        public UInt16 res_horizontal;
        /// <summary>Resolución vertical de pantalla.</summary>
        public UInt16 res_vertical;
        /// <summary>Booleano que indica si usar o no pantalla completa.</summary>
        public Byte pantalla_completa;

	    // Opciones de sonido
        /// <summary>Booleano que indica si usar o no los sonidos de los efectos especiales.</summary>
        public Byte sonidos;
        /// <summary>Booleano que indica si usar o no la música en el juego.</summary>
        public Byte musica;
        /// <summary>Volumen de la música dle juego.</summary>
        public Byte volumen;

        // Opciones de controles
        /// <summary>Booleano que indica si usar WASD o las teclas direccionales.</summary>
        public Byte wasd;
        /// <summary>Booleano que indica si se desea invertir los botones del mouse.</summary>
        public Byte invertir_mouse;

        /// <summary>
        /// Crea una instancia de esta estructura dadas todas sus propiedades.
        /// </summary>
        /// <param name="nombre"><seealso cref="nombre"/></param>
        /// <param name="nivel"><seealso cref="nivel"/></param>
        /// <param name="juegoCompletado"><seealso cref="juego_completado"/></param>
        /// <param name="dificultad"><seealso cref="dificultad"/></param>
        /// <param name="resolucionPantalla">Reolución de pantalla. Véase <see cref="Point"/></param>
        /// <param name="pantallaCompleta"><seealso cref="pantalla_completa"/></param>
        /// <param name="sonidos"><seealso cref="sonidos"/></param>
        /// <param name="musica"><seealso cref="musica"/></param>
        /// <param name="volumen"><seealso cref="volumen"/></param>
        /// <param name="usarWASD"><seealso cref="wasd"/></param>
        /// <param name="invertirBotonesMouse"><seealso cref="invertir_mouse"/></param>
        public OpcionesJuego(String nombre, int nivel, bool juegoCompletado, DificultadJuego dificultad, Point resolucionPantalla, bool pantallaCompleta, bool sonidos, bool musica, int volumen, bool usarWASD, bool invertirBotonesMouse)
        {
            this.nombre = nombre;
            this.nivel = (Byte)nivel;
            this.juego_completado = juegoCompletado ? (Byte)1 : (Byte)0;
            this.dificultad = dificultad;
            this.res_horizontal = (UInt16)resolucionPantalla.X;
            this.res_vertical = (UInt16)resolucionPantalla.Y;
            this.pantalla_completa = pantallaCompleta ? (Byte)1 : (Byte)0;
            this.sonidos = sonidos ? (Byte)1 : (Byte)0;
            this.musica = musica ? (Byte)1 : (Byte)0;
            this.volumen = (Byte)volumen;
            this.wasd = usarWASD ? (Byte)1 : (Byte)0;
            this.invertir_mouse = invertirBotonesMouse ? (Byte)1 : (Byte)0;
        }

        /// <summary>
        /// Crea una instancia de esta estructura con valores vacíos.
        /// </summary>
        /// <param name="nombre"><seealso cref="nombre"/></param>
        public OpcionesJuego(String nombre)
        {
            this.nombre = nombre;
            this.nivel = 0;
            this.juego_completado = 0;
            this.dificultad = 0;
            this.res_horizontal = 800;
            this.res_vertical = 600;
            this.pantalla_completa = 0;
            this.sonidos = 1;
            this.musica = 1;
            this.volumen = 255;
            this.wasd = 0;
            this.invertir_mouse = 0;
        }

        /// <summary>
        /// Obtiene toda la información de esta estructura de una manera organizada.
        /// </summary>
        /// <returns>Cadena con todos los valores de esta estructura listados.</returns>
        public override string ToString()
        {
            return
                "Nombre de jugador:         " + nombre + "\n" +
                "Último nivel superado:     " + nivel.ToString() + "\n" +
                "Juego completado:          " + (juego_completado == 0 ? "Falso" : "Cierto") +
                "Dificultad de juego:       " + dificultad.ToString() + "\n" +
                "Resolución de pantalla:    " + res_horizontal.ToString() + " x " + res_vertical.ToString() + "\n" +
                "Pantalla completa:         " + (pantalla_completa == 0 ? "Desactivada" : "Activada") + "\n" +
                "Sonidos:                   " + (sonidos == 0 ? "Desactivados" : "Activados") + "\n" +
                "Música:                    " + (musica == 0 ? "Desactivada" : "Activada") + "\n" +
                "Volumen de la música:      " + volumen.ToString() + "\n" +
                "Usar WASD:                 " + (wasd == 0 ? "Desactivado" : "Activado") + "\n" +
                "Invertir ratón:            " + (invertir_mouse == 0 ? "Desactivado" : "Activado");
        }
    }

    /// <summary>Estructura que contiene la puntuación de un nivel.</summary>
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct PuntuacionesJuego
    {
        /// <summary>Minutos del tiempo en que se superó el nivel.</summary><remarks>NOTA: No puede exceder 100.</remarks>
        public Byte minutos;
        /// <summary>Segundos del tiempo en que se superó el nivel.</summary><remarks>NOTA: No puede exceder 60.</remarks>
        public Byte segundos;
        /// <summary>Centisegundos del tiempo en que se superó el nivel.</summary><remarks>NOTA: No puede exceder 100.</remarks>
        public Byte centisegundos;

        /// <summary>
        /// Crea una instancia de esta estructura dadas todas sus propiedades.
        /// </summary>
        /// <param name="minutos"><seealso cref="minutos"/></param>
        /// <param name="segundos"><seealso cref="segundos"/></param>
        /// <param name="centisegundos"><seealso cref="centisegundos"/></param>
        public PuntuacionesJuego(int minutos, int segundos, int centisegundos)
        {
            if (minutos >= 100 || segundos >= 60 || centisegundos >= 100) throw new ExcepcionPerfilJugador("Valores de tiempo no válidos.");
            this.minutos = (Byte)minutos;
            this.segundos = (Byte)segundos;
            this.centisegundos = (Byte)centisegundos;
        }

        /// <summary>
        /// Crea una instancia de esta estructura a partir de un TimeSpan. Véase <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="tiempo">Tiempo en que se superó el nivel. Véase <see cref="TimeSpan"/></param>
        public PuntuacionesJuego(TimeSpan tiempo)
        {
            this.minutos = (Byte)tiempo.Minutes;
            this.segundos = (Byte)tiempo.Seconds;
            this.centisegundos = (Byte)(tiempo.Milliseconds / 10);
        }

        /// <summary>
        /// Convierte la estructura de puntuaciones en una estructura TimeSpan. Véase <see cref="TimeSpan"/>
        /// </summary>
        /// <returns>El TimeSpan equivalente a esta estructura. Véase <see cref="TimeSpan"/></returns>
        public TimeSpan ConvertirTimeSpan()
        {
            return new TimeSpan(0, 0, minutos, segundos, centisegundos * 10);
        }

        /// <summary>
        /// Obtiene el tiempo en que se completó el nivel en forma de cadena con el formato Minutos:Segundos:Centisegundos.
        /// </summary>
        /// <returns>Cadena del tiempo en que se completó el nivel.</returns>
        public override string ToString()
        {
            return minutos.ToString("00") + ":" + segundos.ToString("00") + ":" + centisegundos.ToString("00");
        }

        /// <summary>
        /// Devuelve un código hash construido a base de las propiedades de esta estructura.
        /// </summary>
        /// <returns>Código hash.</returns>
        public override int GetHashCode()
        {
            return centisegundos + 100 * (segundos + 60 * (minutos));
        }

        /// <summary>
        /// Determina si un obejo es igual a esta estrucutra.
        /// </summary>
        /// <param name="obj">Objeto a comprobar.</param>
        /// <returns>Booleano que indica si ambos objetos son iguales.</returns>
        public override bool Equals(object obj)
        {
            try
            {
                return this == (PuntuacionesJuego)obj;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Verifica si 2 puntuaciones son iguales.
        /// </summary>
        /// <param name="primero">Primer elemento a comparar.</param>
        /// <param name="segundo">Segundo elemento a comparar.</param>
        /// <returns>Booleano que indica la igualdad entre las estructuras.</returns>
        public static bool operator ==(PuntuacionesJuego primero, PuntuacionesJuego segundo)
        {
            return primero.centisegundos == segundo.centisegundos &&
                primero.segundos == segundo.segundos &&
                primero.minutos == segundo.minutos;
        }

        /// <summary>
        /// Verifica si 2 puntuaciones son desiguales.
        /// </summary>
        /// <param name="primero">Primer elemento a comparar.</param>
        /// <param name="segundo">Segundo elemento a comparar.</param>
        /// <returns>Booleano que indica la desigualdad entre las estructuras.</returns>
        public static bool operator !=(PuntuacionesJuego primero, PuntuacionesJuego segundo)
        {
            return primero.centisegundos != segundo.centisegundos ||
                primero.segundos != segundo.segundos ||
                primero.minutos != segundo.minutos;
        }

        /// <summary>
        /// Convierte una estructura de puntuaciones a una estructura TimeSpan mediante casteo.
        /// </summary>
        /// <param name="origen">Estructura a convetir.</param>
        /// <returns>TimeSpan equivalente a la estructura de puntuacion.</returns>
        public static implicit operator TimeSpan(PuntuacionesJuego origen)
        {
            return origen.ConvertirTimeSpan();
        }

        /// <summary>
        /// Convierte una estructura TimeSpan a una estructura de puntuaciones mediante casteo.
        /// </summary>
        /// <param name="origen">Estructura a convetir.</param>
        /// <returns>Estructura de puntuaciones equivalente al TimeSpan.</returns>
        public static explicit operator PuntuacionesJuego(TimeSpan origen)
        {
            return new PuntuacionesJuego(origen);
        }

        /// <summary>
        /// Suma 2 estructuras de puntuaciónes.
        /// </summary>
        /// <param name="primero">Primer elemento a sumar.</param>
        /// <param name="segundo">Segundo elemento a sumar.</param>
        /// <returns>Estructura de puntuaciones sumadas.</returns>
        public static PuntuacionesJuego operator +(PuntuacionesJuego primero, PuntuacionesJuego segundo)
        {
            primero.centisegundos += segundo.centisegundos;
            primero.segundos += (Byte)(segundo.centisegundos / 100);
            primero.centisegundos %= 100;
            primero.segundos += segundo.segundos;
            primero.minutos += (Byte)(segundo.segundos / 60);
            primero.segundos %= 60;
            primero.minutos += segundo.minutos;
            return primero;
        }
    }

    /// <summary>Estructura que contiene toda la información del perfil de jugador.
    /// <para>Esta información incluye: nombre del perfil, último nivel alcanzado, dificultad del juego, opciones de vídeo, sonido y controles y las puntuaciones de todos los niveles.</para></summary>
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct PerfilJugador
    {
        /// <summary>Información del perfil, y configuraciones.</summary>
	    public OpcionesJuego opciones;
        /// <summary>Arreglo de puntuaciones de los 5 niveles.</summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = ListaPerfilesJugador.numeroNiveles)]
	    public PuntuacionesJuego[] puntuaciones;

        /// <summary>
        /// Crea una instancia de esta estructura con puntuaciones vacías a partir de una estructura de información de perfil.
        /// </summary>
        /// <param name="opciones"><seealso cref="opciones"/></param>
        public PerfilJugador(OpcionesJuego opciones)
        {
            this.opciones = opciones;
            int c;
            puntuaciones = new PuntuacionesJuego[ListaPerfilesJugador.numeroNiveles];
            for (c = 0; c < puntuaciones.Length; c++) puntuaciones[c] = new PuntuacionesJuego();
        }

        /// <summary>
        /// Crea una instancia vacía de esta estructura dado el nombre del perfil.
        /// </summary>
        /// <param name="nombre">Nombre del perfil de jugador.</param>
        public PerfilJugador(String nombre)
        {
            opciones = new OpcionesJuego(nombre);
            int c;
            puntuaciones = new PuntuacionesJuego[ListaPerfilesJugador.numeroNiveles];
            for (c = 0; c < puntuaciones.Length; c++) puntuaciones[c] = new PuntuacionesJuego();
        }

        /// <summary>
        /// Obtiene la información completa del perfil de jugador, esto incluye sus configuraciones y sus puntuaciones en cada nivel.
        /// </summary>
        /// <returns>Cadena con toda la información del perfil organizada.</returns>
        public override string ToString()
        {
            String niveles = "";
            int c;
            for (c = 0; c < ListaPerfilesJugador.numeroNiveles; c++) niveles += "Nivel " + (c + 1).ToString() + "   : " + puntuaciones[c].ToString() + "\n";
            return opciones.ToString() + "\n\n" + niveles;
        }
    }

    /// <summary>Estructura de un nodo que forma parte de una lista de perfiles de jugador.</summary>
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct NodoPerfil
    {
        /// <summary>Perfil del nodo. Véase <see cref="PerfilJugador"/></summary>
	    public IntPtr perfil;   // PerfilJugador
        /// <summary>Siguiente elemento de la lista.</summary>
	    unsafe public NodoPerfil* siguiente;
    }
}
