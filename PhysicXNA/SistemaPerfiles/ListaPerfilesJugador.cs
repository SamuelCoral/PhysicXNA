using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace PhysicXNA.SistemaPerfiles
{
    /// <summary>Lista de perfiles de jugador.</summary>
    public partial class ListaPerfilesJugador : IEnumerable<PerfilJugador>, IDisposable
    {
        unsafe private NodoPerfil* lista = null;
        /// <summary>Cantidad de niveles del juego.</summary>
        public const int numeroNiveles = 5;
        /// <summary>Tamaño máximo en bytes del nombre de jugador.</summary>
        public const int tamNombresJugador = 30;
        /// <summary>Ruta donde se almacenarán los perfiles de jugador. Por default, en la carpeta "Mis documentos" del usuario logueado.</summary>
        public static String rutaPerfiles = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PhysicXNA-Perfiles.pjpx";

        #region Constructores

        /// <summary>Crea una lista vacía.</summary>
        public ListaPerfilesJugador()
        {
            
        }

        /// <summary>
        /// Carga una lista de perfiles a partir de un archivo
        /// </summary>
        /// <param name="rutaArchivo">Ruta del archivo de los perfiles.</param>
        public ListaPerfilesJugador(String rutaArchivo)
        {
            unsafe
            {
                lista = CargarPerfilesArchivo(rutaArchivo);
                // No arrojar excepciones, si hay un error simplemente crear una lista vacía
                //if (lista == null) throw new ExcepcionPerfilJugador("No se pudo cargar el perfil de jugador.");
            }
        }

        #endregion

        #region Métodos y porpiedades

        /// <summary>
        /// Guarda la lista en un archivo.
        /// </summary>
        /// <param name="rutaArchivo">Ruta del archivo de destino.</param>
        public void Guadar(String rutaArchivo)
        {
            unsafe
            {
                if(GuardarPerfilesArchivo(lista, rutaArchivo) == 0) throw new ExcepcionPerfilJugador("No se pudo guardar el perfil de jugador.");
            }
        }

        /// <summary>
        /// Agrega un perfil de jugador nuevo al final de la lista.
        /// </summary>
        /// <remarks>No pueden existir perfiles con nombres repetidos.</remarks>
        /// <param name="nuevo">Perfil a agregar.</param>
        public void AgregarPerfil(PerfilJugador nuevo)
        {
            unsafe
            {
                if (BuscarNodoPerfil(lista, nuevo.opciones.nombre) != 0) throw new ExcepcionPerfilJugador("Ya existe otro perfil con el mismo nombre.");
                if (AgregarNodoPerfil(ref lista, ref nuevo) == 0) throw new ExcepcionPerfilJugador("No se pudo agregar el nuevo perfil de jugador.");
            }
        }

        /// <summary>
        /// Elimina el perfil de jugador en el lugar dado de la lista.
        /// </summary>
        /// <param name="lugar">Lugar que ocupa el perfil a eliminar.</param>
        public void EliminarPerfil(int lugar)
        {
            unsafe
            {
                if (EliminarNodoPerfil(ref lista, lugar) == 0) throw new IndexOutOfRangeException("No se pudo eliminar el perfil de jugador.");
            }
        }

        /// <summary>
        /// Elimina un perfil de jugador de la lista dado su nombre.
        /// </summary>
        /// <param name="nombre">Nombre del perfil a eliminar.</param>
        public void EliminarPerfil(String nombre)
        {
            unsafe
            {
                int lugar = BuscarNodoPerfil(lista, nombre);
                if (lugar == 0) throw new ExcepcionPerfilJugador("No existe el perfil especificado.");
                EliminarPerfil(lugar);
            }
        }

        /// <summary>
        /// Busca un perfil en la lista dado su nombre y verifique si existe.
        /// </summary>
        /// <param name="nombre">Nombre del perfil a buscar.</param>
        /// <returns>Booleano que indica si existe el perfil solicitado.</returns>
        public bool ExistePerfil(String nombre)
        {
            unsafe
            {
                return BuscarNodoPerfil(lista, nombre) != 0;
            }
        }

        /// <summary>
        /// Busca un perfil de jugador por nombre en la lista especificada.
        /// <reparks>En caso de que no sea encontrado, la función devolverá 0.</reparks>
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns>El lugar que ocupa en la lista comenzando en 1.</returns>
        public int BuscarPerfil(String nombre)
        {
            unsafe
            {
                return BuscarNodoPerfil(lista, nombre);
            }
        }

        /// <summary>
        /// Elimina todos los perfiles de la lista.
        /// </summary>
        public void Limpiar()
        {
            unsafe
            {
                EliminarListaPerfiles(lista);
                lista = null;
            }
        }

        /// <summary>
        /// Intercambia las posiciones en la lista de 2 perfiles de jugador.
        /// </summary>
        /// <param name="primero">Posición en la lista del primer perfil a intercambiar.</param>
        /// <param name="segundo">Posición en la lista del segundo perfil a intercambiar.</param>
        public void IntercambiarLugares(int primero, int segundo)
        {
            unsafe
            {
                if (IntercambiarPerfiles(ref lista, primero, segundo) == 0) throw new ExcepcionPerfilJugador("No se encontró alguno de los perfiles especificados en la lista.");
            }
        }

        /// <summary>
        /// Cuenta el número de perfiles en la lista.
        /// </summary>
        public int NumeroPerfiles
        {
            get
            {
                unsafe
                {
                    return ContarNodosPerfilLista(lista);
                }
            }
        }

        /// <summary>
        /// Muestra toda la información de todos los perfiles de jugador en la lista de una forma organizada.
        /// </summary>
        /// <returns>Cadena con toda la información de los perfiles.</returns>
        public override string ToString()
        {
            String salida = "";
            foreach (PerfilJugador item in this)
            {
                salida += item.ToString() + "\n\n\n";
            }

            return salida;
        }

        #endregion

        #region Indizadores

        /// <summary>
        /// Consulta o actualiza un perfil de jugador en el lugar especificado.
        /// </summary>
        /// <param name="index">Lugar que ocupa en la lista el perfil solicitado.</param>
        /// <returns>Perfil solicitado.</returns>
        public PerfilJugador this[int index]
        {
            get
            {
                unsafe
                {
                    if (index < 1 || index > NumeroPerfiles) throw new IndexOutOfRangeException("No existe el perfil especificado.");
                    return (PerfilJugador)Marshal.PtrToStructure(VerPerfilLista(lista, index), typeof(PerfilJugador));
                }
            }

            set
            {
                unsafe
                {
                    if (index < 1 || index > NumeroPerfiles) throw new IndexOutOfRangeException("No existe el perfil especificado.");
                    int lugar = BuscarNodoPerfil(lista, value.opciones.nombre);
                    if(lugar != 0 && lugar != index) throw new ExcepcionPerfilJugador("Ya existe otro perfil con el mismo nombre");
                    ActualizarPerfilLista(lista, ref value, index);
                }
            }
        }

        /// <summary>
        /// Consulta o actualiza un perfil de jugador dado su nombre.
        /// </summary>
        /// <param name="index">Nombre del perfil a consultar.</param>
        /// <returns>Perfil solicitado.</returns>
        public PerfilJugador this[String index]
        {
            get
            {
                unsafe
                {
                    int lugar = BuscarNodoPerfil(lista, index);
                    if (lugar == 0) throw new ExcepcionPerfilJugador("No existe el perfil especificado");
                    return this[lugar];
                }
            }

            set
            {
                unsafe
                {
                    int lugar = BuscarNodoPerfil(lista, index);
                    if (lugar == 0) throw new ExcepcionPerfilJugador("No existe el perfil especificado");
                    this[lugar] = value;
                }
            }
        }

        #endregion

        #region Implementación de IEnumerable

        /// <summary>Clase auxiliar enumeradora de perfiles.</summary>
        public class EnumeradorListaPerfilesJugador : IEnumerator<PerfilJugador>
        {
            unsafe private NodoPerfil* lista;
            int lugar = 0;

            /// <summary>
            /// Construye un enumerador de perfiles a partir del primer nodo de la lista.
            /// </summary>
            /// <param name="lista">Primer nodo de la lista de perfiles.</param>
            unsafe public EnumeradorListaPerfilesJugador(NodoPerfil* lista)
            {
                this.lista = lista;
            }

            /// <summary>Obtiene el elemento apuntado.</summary>
            unsafe public PerfilJugador Current
            {
                get { return (PerfilJugador)Marshal.PtrToStructure(VerPerfilLista(lista, lugar), typeof(PerfilJugador)); }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            /// <summary>
            /// Mueve el apuntador al siguiente elemento.
            /// </summary>
            /// <returns>false en caso de que no se pueda seguir avanzando.</returns>
            public bool MoveNext()
            {
                unsafe
                {
                    lugar++;
                    return lugar <= ContarNodosPerfilLista(lista);
                }
            }

            /// <summary>Reinicia el apuntador del enumerador.</summary>
            public void Reset()
            {
                lugar = 0;
            }

            /// <summary>Libera los recursos ocupados por el enumerador de la lista.</summary>
            public void Dispose()
            {
                
            }
        }


        /// <summary>
        /// Devuelve un enumerador de la lista de perfiles.
        /// </summary>
        /// <returns>Enumerador de la lista.</returns>
        public EnumeradorListaPerfilesJugador GetEnumerator()
        {
            unsafe
            {
                return new EnumeradorListaPerfilesJugador(lista);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        IEnumerator<PerfilJugador> IEnumerable<PerfilJugador>.GetEnumerator()
        {
            return (IEnumerator<PerfilJugador>)GetEnumerator();
        }

        #endregion

        #region Destructor e Implementación de IDisposable

        /// <summary>Indica si ya se ha liberado los recursos ocupados por la lista.</summary>
        protected bool liberado = false;

        /// <summary>
        /// Libera los recursos ocupados por la lista de perfiles.
        /// </summary>
        /// <param name="disposing">Indica si se ha llamado a este método explícitamente.</param>
        protected virtual void Dispose(bool disposing)
        {
            unsafe
            {
                if (liberado) return;
                if (disposing)
                {
                    // Liberar objetos administrados
                }

                // Liberar objetos no administrados
                Limpiar();
                liberado = true;
            }
        }

        /// <summary>Libera los recursos ocupados por la lista de perfiles.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Libera los recursos ocupados por la lista de perfiles.</summary>
        ~ListaPerfilesJugador()
        {
            Dispose(false);
        }

        #endregion
    }
}
