Imports System.Runtime.InteropServices

Namespace SistemaPerfiles

    Partial Public Class ListaPerfilesJugador

        Private Const RutaDll As String = "SistemaPerfiles.dll"
        Private Const ModoLlamado As CallingConvention = CallingConvention.Cdecl

        <DllImport(RutaDll, CallingConvention:=ModoLlamado)>
        Private Shared Function CargarPerfilesArchivo(<MarshalAs(UnmanagedType.AnsiBStr)> ruta As String) As IntPtr
            ' Devuelve NodoPerfil
        End Function

        <DllImport(RutaDll, CallingConvention:=ModoLlamado)>
        Private Shared Function GuardarPerfilesArchivo(perfiles As IntPtr, <MarshalAs(UnmanagedType.AnsiBStr)> ruta As String) As Byte
            ' Pide NodoPerfil
        End Function

        <DllImport(RutaDll, CallingConvention:=ModoLlamado)>
        Private Shared Function AgregarNodoPerfil(ByRef perfiles As IntPtr, ByRef nuevo As PerfilJugador) As Byte
            ' Pide NodoPerfil
        End Function

        <DllImport(RutaDll, CallingConvention:=ModoLlamado)>
        Private Shared Function EliminarNodoPerfil(ByRef perfiles As IntPtr, lugar As Integer) As Byte
            ' Pide NodoPerfil
        End Function

        <DllImport(RutaDll, CallingConvention:=ModoLlamado)>
        Private Shared Function VerPerfilLista(perfiles As IntPtr, lugar As Integer) As IntPtr
            ' Pide NodoPerfil
            ' Devuelve PerfilJugador
        End Function

        <DllImport(RutaDll, CallingConvention:=ModoLlamado)>
        Private Shared Function VerNodoLista(perfiles As IntPtr, lugar As Integer) As IntPtr
            ' Pide NodoPerfil
            ' Devuelve NodoPerfil
        End Function

        <DllImport(RutaDll, CallingConvention:=ModoLlamado)>
        Private Shared Function IntercambiarPerfiles(ByRef perfiles As IntPtr, primero As Integer, segundo As Integer) As Byte
            ' Pide NodoPerfil
        End Function

        <DllImport(RutaDll, CallingConvention:=ModoLlamado)>
        Private Shared Function ActualizarPerfilLista(perfiles As IntPtr, ByRef perfil As PerfilJugador, lugar As Integer) As Byte
            ' Pide NodoPerfil
        End Function

        <DllImport(RutaDll, CallingConvention:=ModoLlamado)>
        Private Shared Function BuscarNodoPerfil(perfiles As IntPtr, <MarshalAs(UnmanagedType.AnsiBStr)> nombre As String) As Integer
            ' Pide NodoPerfil
        End Function

        <DllImport(RutaDll, CallingConvention:=ModoLlamado)>
        Private Shared Function ContarNodosPerfilLista(perfiles As IntPtr) As Integer
            ' Pide NodoPerfil
        End Function

        <DllImport(RutaDll, CallingConvention:=ModoLlamado)>
        Private Shared Sub EliminarListaPerfiles(perfiles As IntPtr)
            ' Pide NodoPerfil
        End Sub

        <DllImport(RutaDll, CallingConvention:=ModoLlamado)>
        Private Shared Function EsPerfilCorrecto(ByRef perfil As PerfilJugador) As Byte

        End Function

    End Class

End Namespace