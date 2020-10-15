using System;
using System.Runtime.InteropServices;

namespace PhysicXNA.SistemaPerfiles
{
    public partial class ListaPerfilesJugador
    {
        private const String rutaDll = "SistemaPerfiles.dll";
        private const CallingConvention ModoLlamado = CallingConvention.Cdecl;


        [DllImport(rutaDll, CallingConvention = ModoLlamado)]
        unsafe private extern static NodoPerfil* CargarPerfilesArchivo([MarshalAs(UnmanagedType.AnsiBStr)] String ruta);

        [DllImport(rutaDll, CallingConvention = ModoLlamado)]
        unsafe private extern static Byte GuardarPerfilesArchivo(NodoPerfil* perfiles, [MarshalAs(UnmanagedType.AnsiBStr)] String ruta);

        [DllImport(rutaDll, CallingConvention = ModoLlamado)]
        unsafe private extern static Byte AgregarNodoPerfil(ref NodoPerfil* perfiles, ref PerfilJugador nuevo);

        [DllImport(rutaDll, CallingConvention = ModoLlamado)]
        unsafe private extern static Byte EliminarNodoPerfil(ref NodoPerfil* perfiles, int lugar);

        [DllImport(rutaDll, CallingConvention = ModoLlamado)]
        unsafe private extern static IntPtr VerPerfilLista(NodoPerfil* perfiles, int lugar); // PerfilJugador

        [DllImport(rutaDll, CallingConvention = ModoLlamado)]
        unsafe private extern static NodoPerfil* VerNodoLista(NodoPerfil* perfiles, int lugar);

        [DllImport(rutaDll, CallingConvention = ModoLlamado)]
        unsafe private extern static Byte IntercambiarPerfiles(ref NodoPerfil* perfiles, int primero, int segundo);

        [DllImport(rutaDll, CallingConvention = ModoLlamado)]
        unsafe private extern static Byte ActualizarPerfilLista(NodoPerfil* perfiles, ref PerfilJugador perfil, int lugar);

        [DllImport(rutaDll, CallingConvention = ModoLlamado)]
        unsafe private extern static int BuscarNodoPerfil(NodoPerfil* perfiles, [MarshalAs(UnmanagedType.AnsiBStr)] String nombre);

        [DllImport(rutaDll, CallingConvention = ModoLlamado)]
        unsafe private extern static int ContarNodosPerfilLista(NodoPerfil* perfiles);

        [DllImport(rutaDll, CallingConvention = ModoLlamado)]
        unsafe private extern static void EliminarListaPerfiles(NodoPerfil* perfiles);

        [DllImport(rutaDll, CallingConvention = ModoLlamado)]
        private extern static Byte EsPerfilCorrecto(ref PerfilJugador perfil);
    }
}
