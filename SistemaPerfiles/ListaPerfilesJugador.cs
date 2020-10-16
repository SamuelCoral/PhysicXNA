using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace PhysicXNA.SistemaPerfiles
{
    /// <summary>Lista de perfiles de jugador.</summary>
    public partial class ListaPerfilesJugador : IEnumerable<PerfilJugador>
    {
        private List<PerfilJugador> lista = new List<PerfilJugador>();
        /// <summary>Cantidad de niveles del juego.</summary>
        public const int numeroNiveles = 5;
        /// <summary>Tamaño máximo en bytes del nombre de jugador.</summary>
        public const int tamNombresJugador = 30;
        /// <summary>Ruta donde se almacenarán los perfiles de jugador. Por default, en la carpeta "Mis documentos" del usuario logueado.</summary>
        public static String rutaPerfiles = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PhysicXNA-Perfiles.json";

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
            try
            {
                lista = JsonConvert.DeserializeObject<List<PerfilJugador>>(File.ReadAllText(rutaArchivo));
            }
            catch(FileNotFoundException)
            {
                
            }
            // No arrojar excepciones, si hay un error simplemente crear una lista vacía
            //if (lista == null) throw new ExcepcionPerfilJugador("No se pudo cargar el perfil de jugador.");
        }

        #endregion

        #region Métodos y porpiedades

        /// <summary>
        /// Guarda la lista en un archivo.
        /// </summary>
        /// <param name="rutaArchivo">Ruta del archivo de destino.</param>
        public void Guadar(String rutaArchivo)
        {
            try
            {
                File.WriteAllText(rutaArchivo, JsonConvert.SerializeObject(lista, Formatting.Indented));
            }
            catch(IOException)
            {
                throw new ExcepcionPerfilJugador("No se pudo guardar el perfil de jugador.");
            }
        }

        /// <summary>
        /// Agrega un perfil de jugador nuevo al final de la lista.
        /// </summary>
        /// <remarks>No pueden existir perfiles con nombres repetidos.</remarks>
        /// <param name="nuevo">Perfil a agregar.</param>
        public void AgregarPerfil(PerfilJugador nuevo)
        {
            if(lista.Any(perfil => perfil.opciones.nombre == nuevo.opciones.nombre))
                throw new ExcepcionPerfilJugador("Ya existe otro perfil con el mismo nombre.");
            lista.Add(nuevo);
        }

        /// <summary>
        /// Elimina el perfil de jugador en el lugar dado de la lista.
        /// </summary>
        /// <param name="lugar">Lugar que ocupa el perfil a eliminar.</param>
        public void EliminarPerfil(int lugar)
        {
            lista.RemoveAt(lugar - 1);
        }

        /// <summary>
        /// Elimina un perfil de jugador de la lista dado su nombre.
        /// </summary>
        /// <param name="nombre">Nombre del perfil a eliminar.</param>
        public void EliminarPerfil(String nombre)
        {
            lista.RemoveAt(BuscarPerfil(nombre) - 1);
        }

        /// <summary>
        /// Busca un perfil en la lista dado su nombre y verifique si existe.
        /// </summary>
        /// <param name="nombre">Nombre del perfil a buscar.</param>
        /// <returns>Booleano que indica si existe el perfil solicitado.</returns>
        public bool ExistePerfil(String nombre)
        {
            return lista.Any(perfil => perfil.opciones.nombre == nombre);
        }

        /// <summary>
        /// Busca un perfil de jugador por nombre en la lista especificada.
        /// <reparks>En caso de que no sea encontrado, la función devolverá 0.</reparks>
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns>El lugar que ocupa en la lista comenzando en 1.</returns>
        public int BuscarPerfil(String nombre)
        {
            int i = 1;
            foreach(PerfilJugador perfil in lista)
            {
                if(perfil.opciones.nombre == nombre)
                    return i;
                i++;
            }
            return 0;
        }

        /// <summary>
        /// Elimina todos los perfiles de la lista.
        /// </summary>
        public void Limpiar()
        {
            lista = new List<PerfilJugador>();
        }

        /// <summary>
        /// Cuenta el número de perfiles en la lista.
        /// </summary>
        public int NumeroPerfiles
        {
            get
            {
                return lista.Count;
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
                if (index < 1 || index > NumeroPerfiles) throw new IndexOutOfRangeException("No existe el perfil especificado.");
                return lista[index - 1];
            }

            set
            {
                if (index < 1 || index > NumeroPerfiles) throw new IndexOutOfRangeException("No existe el perfil especificado.");
                int lugar = BuscarPerfil(value.opciones.nombre);
                if(lugar != 0 && lugar != index) throw new ExcepcionPerfilJugador("Ya existe otro perfil con el mismo nombre");
                lista[index - 1] = value;
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
                int lugar = BuscarPerfil(index);
                if (lugar == 0) throw new ExcepcionPerfilJugador("No existe el perfil especificado");
                return this[lugar];
            }

            set
            {
                int lugar = BuscarPerfil(index);
                if (lugar == 0) throw new ExcepcionPerfilJugador("No existe el perfil especificado");
                this[lugar] = value;
            }
        }

        #endregion

        IEnumerator IEnumerable.GetEnumerator()
        {
            return lista.GetEnumerator();
        }

        IEnumerator<PerfilJugador> IEnumerable<PerfilJugador>.GetEnumerator()
        {
            return lista.GetEnumerator();
        }
    }
}
