Imports System.Runtime.InteropServices

Namespace SistemaPerfiles

    ''' <summary>Lista de perfiles de jugador.</summary>
    Partial Public Class ListaPerfilesJugador : Implements IEnumerable(Of PerfilJugador), IDisposable

        ''' <summary>Tamaño máximo en bytes del nombre de jugador.</summary>
        Public Const TamNombresJugador As Integer = 30
        ''' <summary>Cantidad de niveles del juego.</summary>
        Public Const NumeroNiveles As Integer = 5
        ''' <summary>Ruta donde se almacenarán los perfiles de jugador. Por default, en la carpeta "Mis documentos" del usuario logueado.</summary>
        Public Shared RutaPerfiles As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\PhysicXNA-Perfiles.pjpx"

        Private Lista As IntPtr = Nothing   ' NodoPerfil

#Region "Constructores"

        ''' <summary>Crea una lista vacía.</summary>
        Public Sub New()

        End Sub

        ''' <summary>
        ''' Carga una lista de perfiles a partir de un archivo
        ''' </summary>
        ''' <param name="rutaArchivo">Ruta del archivo de los perfiles.</param>
        Public Sub New(rutaArchivo As String)

            Lista = CargarPerfilesArchivo(rutaArchivo)
            If Lista = Nothing Then Throw New ExcepcionPerfilJugador("No se pudo cargar el perfil de jugador.")

        End Sub

#End Region

#Region "Métodos y porpiedades"

        ''' <summary>
        ''' Guarda la lista en un archivo.
        ''' </summary>
        ''' <param name="rutaArchivo">Ruta del archivo de destino.</param>
        Public Sub Guadar(rutaArchivo As String)
            If GuardarPerfilesArchivo(Lista, rutaArchivo) = 0 Then Throw New ExcepcionPerfilJugador("No se pudo guardar el perfil de jugador.")
        End Sub

        ''' <summary>
        ''' Agrega un perfil de jugador nuevo al final de la lista.
        ''' </summary>
        ''' <remarks>No pueden existir perfiles con nombres repetidos.</remarks>
        ''' <param name="nuevo">Perfil a agregar.</param>
        Public Sub AgregarPerfil(nuevo As PerfilJugador)

            If BuscarNodoPerfil(Lista, nuevo.opciones.nombre) <> 0 Then Throw New ExcepcionPerfilJugador("Ya existe otro perfil con el mismo nombre.")
            If AgregarNodoPerfil(Lista, nuevo) = 0 Then Throw New ExcepcionPerfilJugador("No se pudo agregar el nuevo perfil de jugador.")

        End Sub

        ''' <summary>
        ''' Elimina el perfil de jugador en el lugar dado de la lista.
        ''' </summary>
        ''' <param name="lugar">Lugar que ocupa el perfil a eliminar.</param>
        Public Sub EliminarPerfil(lugar As Integer)
            If EliminarNodoPerfil(Lista, lugar) = 0 Then Throw New IndexOutOfRangeException("No se pudo eliminar el perfil de jugador.")
        End Sub

        ''' <summary>
        ''' Elimina un perfil de jugador de la lista dado su nombre.
        ''' </summary>
        ''' <param name="nombre">Nombre del perfil a eliminar.</param>
        Public Sub EliminarPerfil(nombre As String)

            Dim lugar As Integer = BuscarNodoPerfil(Lista, nombre)
            If lugar = 0 Then Throw New ExcepcionPerfilJugador("No existe el perfil especificado.")
            EliminarPerfil(lugar)

        End Sub

        ''' <summary>
        ''' Busca un perfil en la lista dado su nombre y verifique si existe.
        ''' </summary>
        ''' <param name="nombre">Nombre del perfil a buscar.</param>
        ''' <returns>Booleano que indica si existe el perfil solicitado.</returns>
        Public Function ExistePerfil(nombre As String) As Boolean
            Return BuscarNodoPerfil(Lista, nombre) <> 0
        End Function

        ''' <summary>
        ''' Busca un perfil de jugador por nombre en la lista especificada.
        ''' <reparks>En caso de que no sea encontrado, la función devolverá 0.</reparks>
        ''' </summary>
        ''' <param name="nombre"></param>
        ''' <returns>El lugar que ocupa en la lista comenzando en 1.</returns>
        Public Function BuscarPerfil(nombre As String) As Integer
            Return BuscarNodoPerfil(Lista, nombre)
        End Function

        ''' <summary>
        ''' Elimina todos los perfiles de la lista.
        ''' </summary>
        Public Sub Limpiar()

            EliminarListaPerfiles(Lista)
            Lista = Nothing

        End Sub

        ''' <summary>
        ''' Intercambia las posiciones en la lista de 2 perfiles de jugador.
        ''' </summary>
        ''' <param name="primero">Posición en la lista del primer perfil a intercambiar.</param>
        ''' <param name="segundo">Posición en la lista del segundo perfil a intercambiar.</param>
        Public Sub IntercambiarLugares(primero As Integer, segundo As Integer)
            If IntercambiarPerfiles(Lista, primero, segundo) = 0 Then Throw New ExcepcionPerfilJugador("No se encontró alguno de los perfiles especificados en la lista.")
        End Sub

        ''' <summary>
        ''' Cuenta el número de perfiles en la lista.
        ''' </summary>
        Public ReadOnly Property NumeroPerfiles As Integer

            Get
                Return ContarNodosPerfilLista(Lista)
            End Get

        End Property

        ''' <summary>
        ''' Muestra toda la información de todos los perfiles de jugador en la lista de una forma organizada.
        ''' </summary>
        ''' <returns>Cadena con toda la información de los perfiles.</returns>
        Public Overrides Function ToString() As String

            Dim salida As String = String.Empty
            For Each Item As PerfilJugador In Me
                salida += Item.ToString() + Chr(13) + Chr(10) + Chr(13) + Chr(10) + Chr(13) + Chr(10)
            Next

            Return salida

        End Function

#End Region

#Region "Indizadores"

        ''' <summary>
        ''' Consulta o actualiza un perfil de jugador en el lugar especificado.
        ''' </summary>
        ''' <param name="index">Lugar que ocupa en la lista el perfil solicitado.</param>
        ''' <returns>Perfil solicitado.</returns>
        Default Public Property Items(index As Integer) As PerfilJugador

            Get

                If index < 1 Or index > NumeroPerfiles Then Throw New IndexOutOfRangeException("No existe el perfil especificado.")
                Return CType(Marshal.PtrToStructure(VerPerfilLista(Lista, index), GetType(PerfilJugador)), PerfilJugador)

            End Get

            Set(value As PerfilJugador)

                If index < 1 Or index > NumeroPerfiles Then Throw New IndexOutOfRangeException("No existe el perfil especificado.")
                Dim lugar As Integer = BuscarNodoPerfil(Lista, value.opciones.nombre)
                If lugar <> 0 And lugar <> index Then Throw New ExcepcionPerfilJugador("Ya existe otro perfil con el mismo nombre")
                ActualizarPerfilLista(Lista, value, index)

            End Set

        End Property

        ''' <summary>
        ''' Consulta o actualiza un perfil de jugador dado su nombre.
        ''' </summary>
        ''' <param name="index">Nombre del perfil a consultar.</param>
        ''' <returns>Perfil solicitado.</returns>
        Default Public Property Items(index As String) As PerfilJugador

            Get

                Dim lugar As Integer = BuscarNodoPerfil(Lista, index)
                If lugar = 0 Then Throw New ExcepcionPerfilJugador("No existe el perfil especificado")
                Return Me(lugar)

            End Get

            Set(value As PerfilJugador)

                Dim lugar As Integer = BuscarNodoPerfil(Lista, index)
                If lugar = 0 Then Throw New ExcepcionPerfilJugador("No existe el perfil especificado")
                Me(lugar) = value

            End Set

        End Property

#End Region

#Region "Implementación de IEnumerable"

        ''' <summary>Clase auxiliar enumeradora de perfiles.</summary>
        Public Class EnumeradorListaPerfilesJugador : Implements IEnumerator(Of PerfilJugador)

            Private Lista As IntPtr ' NodoLista
            Private Lugar As Integer = 0

            ''' <summary>
            ''' Construye un enumerador de perfiles a partir del primer nodo de la lista.
            ''' </summary>
            ''' <param name="lista">Primer nodo de la lista de perfiles.</param>
            Public Sub New(lista As IntPtr) ' NodoLista
                Me.Lista = lista
            End Sub

            ''' <summary>Obtiene el elemento apuntado.</summary>
            Public ReadOnly Property Current As PerfilJugador Implements IEnumerator(Of PerfilJugador).Current
                Get
                    Return CType(Marshal.PtrToStructure(VerPerfilLista(Lista, Lugar), GetType(PerfilJugador)), PerfilJugador)
                End Get
            End Property

            ReadOnly Property Current1 As Object Implements IEnumerator.Current
                Get
                    Return Current
                End Get
            End Property

            ''' <summary>
            ''' Mueve el apuntador al siguiente elemento.
            ''' </summary>
            ''' <returns>false en caso de que no se pueda seguir avanzando.</returns>
            Public Function MoveNext() As Boolean Implements IEnumerator.MoveNext

                Lugar += 1
                Return Lugar <= ContarNodosPerfilLista(Lista)

            End Function

            ''' <summary>Reinicia el apuntador del enumerador.</summary>
            Public Sub Reset() Implements IEnumerator.Reset
                Lugar = 0
            End Sub

            ''' <summary>Libera los recursos ocupados por el enumerador de la lista.</summary>
            Public Sub Dispose() Implements IDisposable.Dispose

            End Sub

        End Class


        ''' <summary>
        ''' Devuelve un enumerador de la lista de perfiles.
        ''' </summary>
        ''' <returns>Enumerador de la lista.</returns>
        Public Function GetEnumerator() As EnumeradorListaPerfilesJugador
            Return New EnumeradorListaPerfilesJugador(Lista)
        End Function

        Function GetEnumerator1() As IEnumerator(Of PerfilJugador) Implements IEnumerable(Of PerfilJugador).GetEnumerator
            Return GetEnumerator()
        End Function

        Function GetEnumerator2() As IEnumerator Implements IEnumerable.GetEnumerator
            Return GetEnumerator()
        End Function

#End Region

#Region "Destructor e Implementación de IDisposable"

        ''' <summary>Indica si ya se ha liberado los recursos ocupados por la lista.</summary>
        Protected Liberado As Boolean = False

        ''' <summary>
        ''' Libera los recursos ocupados por la lista de perfiles.
        ''' </summary>
        ''' <param name="disposing">Indica si se ha llamado a este método explícitamente.</param>
        Protected Overridable Sub Dispose(disposing As Boolean)

            If Liberado Then Exit Sub
            If disposing Then
                ' Liberar objetos administrados
            End If

            ' Liberar objetos no administrados
            Limpiar()
            Liberado = True

        End Sub

        ''' <summary>Libera los recursos ocupados por la lista de perfiles.</summary>
        Public Sub Dispose() Implements IDisposable.Dispose

            Dispose(True)
            GC.SuppressFinalize(Me)

        End Sub

        ''' <summary>Libera los recursos ocupados por la lista de perfiles.</summary>
        Protected Overrides Sub Finalize()

            Dispose(False)
            MyBase.Finalize()

        End Sub

#End Region

    End Class

End Namespace
