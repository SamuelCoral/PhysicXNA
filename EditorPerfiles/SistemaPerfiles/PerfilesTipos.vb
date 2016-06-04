Imports System.Runtime.InteropServices

Namespace SistemaPerfiles

    ''' <summary>Dificultad en la que se ejecuta el juego, usada en algunos niveles.</summary>
    Public Enum DificultadJuego As Byte

        ''' <summary>Fácil.</summary>
        Fácil = 0
        ''' <summary>Medio.</summary>
        Medio = 1
        ''' <summary>Difícil.</summary>
        Difícil = 2

    End Enum



    ''' <summary>Estructura que contiene el nombre del perfil, último nivel alcanzado, dificultad del juego y opciones de vídeo, sonido y controles.</summary>
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi, Pack:=8)>
    Public Structure OpcionesJuego

        ''' <summary>Nombre del perfil.</summary><remarks>NOTA: No pueden haber 2 perfiles con el mismo nombre y no puede exceder los 40 caracteres.</remarks>
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=ListaPerfilesJugador.TamNombresJugador)>
        Public nombre As String
        ''' <summary>Último nivel del juego superado.</summary><remarks>NOTA: No puede exceder 5.</remarks>
        Public nivel As Byte
        ''' <summary>Booleano que indica si ya se han completado todos los niveles del juego por lo menos una vez.</summary>
        Public juego_completado As Byte
        ''' <summary>Dificultad del juego elegida. Véase <see cref="DificultadJuego"/></summary>
        Public dificultad As DificultadJuego

        ' Opciones de video
        ''' <summary>Resolución horizontal de pantalla.</summary>
        Public res_horizontal As UInt16
        ''' <summary>Resolución vertical de pantalla.</summary>
        Public res_vertical As UInt16
        ''' <summary>Booleano que indica si usar o no pantalla completa.</summary>
        Public pantalla_completa As Byte

        ' Opciones de sonido
        ''' <summary>Booleano que indica si usar o no los sonidos de los efectos especiales.</summary>
        Public sonidos As Byte
        ''' <summary>Booleano que indica si usar o no la música en el juego.</summary>
        Public musica As Byte
        ''' <summary>Volumen de la música del juego.</summary>
        Public volumen As Byte

        ' Opciones de controles
        ''' <summary>Booleano que indica si usar WASD o las teclas direccionales.</summary>
        Public wasd As Byte
        ''' <summary>Booleano que indica si se desea invertir los botones del mouse.</summary>
        Public invertir_mouse As Byte

        ''' <summary>
        ''' Crea una instancia de esta estructura dadas todas sus propiedades.
        ''' </summary>
        ''' <param name="nombre"><seealso cref="nombre"/></param>
        ''' <param name="nivel"><seealso cref="nivel"/></param>
        ''' <param name="juegoCompletado"><seealso cref="juego_completado"/></param>
        ''' <param name="dificultad"><seealso cref="dificultad"/></param>
        ''' <param name="resolucionPantalla">Reolución de pantalla. Véase <see cref="Point"/></param>
        ''' <param name="pantallaCompleta"><seealso cref="pantalla_completa"/></param>
        ''' <param name="sonidos"><seealso cref="sonidos"/></param>
        ''' <param name="musica"><seealso cref="musica"/></param>
        ''' <param name="volumen"><seealso cref="volumen"/></param>
        ''' <param name="usarWASD"><seealso cref="wasd"/></param>
        ''' <param name="invertirBotonesMouse"><seealso cref="invertir_mouse"/></param>
        Public Sub New(nombre As String, nivel As Integer, juegoCompletado As Boolean, dificultad As DificultadJuego, resolucionPantalla As Point, pantallaCompleta As Boolean, sonidos As Boolean, musica As Boolean, volumen As Integer, usarWASD As Boolean, invertirBotonesMouse As Boolean)

            Me.nombre = nombre
            Me.nivel = nivel
            Me.juego_completado = juegoCompletado
            Me.dificultad = dificultad
            Me.res_horizontal = resolucionPantalla.X
            Me.res_vertical = resolucionPantalla.Y
            Me.pantalla_completa = pantallaCompleta
            Me.sonidos = sonidos
            Me.musica = musica
            Me.volumen = volumen
            Me.wasd = usarWASD
            Me.invertir_mouse = invertirBotonesMouse

        End Sub

        ''' <summary>
        ''' Crea una instancia de esta estructura con valores vacíos.
        ''' </summary>
        ''' <param name="nombre"><seealso cref="nombre"/></param>
        Public Sub New(nombre As String)

            Me.nombre = nombre
            Me.nivel = 0
            Me.juego_completado = 0
            Me.dificultad = 0
            Me.res_horizontal = 800
            Me.res_vertical = 600
            Me.pantalla_completa = 0
            Me.sonidos = 1
            Me.musica = 1
            Me.volumen = 255
            Me.wasd = 0
            Me.invertir_mouse = 0

        End Sub

        ''' <summary>
        ''' Obtiene toda la información de esta estructura de una manera organizada.
        ''' </summary>
        ''' <returns>Cadena con todos los valores de esta estructura listados.</returns>
        Public Overloads Function ToString() As String

            Return "Nombre de jugador:" + Chr(9) + Chr(9) + nombre + Chr(13) + Chr(10) +
                "Último nivel superado:" + Chr(9) + nivel.ToString() + Chr(13) + Chr(10) +
                "Juego completado:" + Chr(9) + Chr(9) + CStr(IIf(juego_completado = 0, "Falso", "Cierto")) + Chr(13) + Chr(10) +
                "Dificultad de juego:" + Chr(9) + Chr(9) + dificultad.ToString() + Chr(13) + Chr(10) +
                "Resolución de pantalla:" + Chr(9) + res_horizontal.ToString() + " x " + res_vertical.ToString() + Chr(13) + Chr(10) +
                "Pantalla completa:" + Chr(9) + Chr(9) + CStr(IIf(pantalla_completa = 0, "Desactivada", "Activada")) + Chr(13) + Chr(10) +
                "Sonidos:" + Chr(9) + Chr(9) + Chr(9) + CStr(IIf(sonidos = 0, "Desactivados", "Activados")) + Chr(13) + Chr(10) +
                "Música:" + Chr(9) + Chr(9) + Chr(9) + CStr(IIf(musica = 0, "Desactivada", "Activada")) + Chr(13) + Chr(10) +
                "Volumen de la música:" + Chr(9) + volumen.ToString() + Chr(13) + Chr(10) +
                "Usar WASD:" + Chr(9) + Chr(9) + CStr(IIf(wasd = 0, "Desactivado", "Activado")) + Chr(13) + Chr(10) +
                "Invertir ratón:" + Chr(9) + Chr(9) + CStr(IIf(invertir_mouse = 0, "Desactivado", "Activado"))

        End Function

    End Structure



    ''' <summary>Estructura que contiene la puntuación de un nivel.</summary>
    <StructLayout(LayoutKind.Sequential, Pack:=8)>
    Public Structure PuntuacionesJuego

        ''' <summary>Minutos del tiempo en que se superó el nivel.</summary><remarks>NOTA: No puede exceder 100.</remarks>
        Public minutos As Byte
        ''' <summary>Segundos del tiempo en que se superó el nivel.</summary><remarks>NOTA: No puede exceder 60.</remarks>
        Public segundos As Byte
        ''' <summary>Centisegundos del tiempo en que se superó el nivel.</summary><remarks>NOTA: No puede exceder 100.</remarks>
        Public centisegundos As Byte

        ''' <summary>
        ''' Crea una instancia de esta estructura dadas todas sus propiedades.
        ''' </summary>
        ''' <param name="minutos"><seealso cref="minutos"/></param>
        ''' <param name="segundos"><seealso cref="segundos"/></param>
        ''' <param name="centisegundos"><seealso cref="centisegundos"/></param>
        Public Sub New(minutos As Integer, segundos As Integer, centisegundos As Integer)

            If minutos >= 100 Or segundos >= 60 Or centisegundos >= 100 Then Throw New ExcepcionPerfilJugador("Valores de tiempo no válidos.")
            Me.minutos = minutos
            Me.segundos = segundos
            Me.centisegundos = centisegundos

        End Sub

        ''' <summary>
        ''' Crea una instancia de esta estructura a partir de un TimeSpan. Véase <see cref="TimeSpan"/>
        ''' </summary>
        ''' <param name="tiempo">Tiempo en que se superó el nivel. Véase <see cref="TimeSpan"/></param>
        Public Sub New(tiempo As TimeSpan)

            Me.minutos = tiempo.Minutes
            Me.segundos = tiempo.Seconds
            Me.centisegundos = tiempo.Milliseconds / 10

        End Sub


        ''' <summary>
        ''' Convierte la estructura de puntuaciones en una estructura TimeSpan. Véase <see cref="TimeSpan"/>
        ''' </summary>
        ''' <returns>El TimeSpan equivalente a esta estructura. Véase <see cref="TimeSpan"/></returns>
        Public Function ConvertirTimeSpan() As TimeSpan
            Return New TimeSpan(0, 0, minutos, segundos, centisegundos * 10)
        End Function


        ''' <summary>
        ''' Obtiene el tiempo en que se completó el nivel en forma de cadena con el formato Minutos:Segundos:Centisegundos.
        ''' </summary>
        ''' <returns>Cadena del tiempo en que se completó el nivel.</returns>
        Public Overrides Function ToString() As String
            Return minutos.ToString("00") + ":" + segundos.ToString("00") + ":" + centisegundos.ToString("00")
        End Function

        ''' <summary>
        ''' Devuelve un código hash construido a base de las propiedades de esta estructura.
        ''' </summary>
        ''' <returns>Código hash.</returns>
        Public Overrides Function GetHashCode() As Integer
            Return centisegundos + 100 * (segundos + 60 * (minutos))
        End Function

        ''' <summary>
        ''' Determina si un obejo es igual a esta estrucutra.
        ''' </summary>
        ''' <param name="obj">Objeto a comprobar.</param>
        ''' <returns>Booleano que indica si ambos objetos son iguales.</returns>
        Public Overrides Function Equals(obj As Object) As Boolean

            Try
                Return Me = CType(obj, PuntuacionesJuego)
            Catch
                Return False
            End Try

        End Function


        ''' <summary>
        ''' Verifica si 2 puntuaciones son iguales.
        ''' </summary>
        ''' <param name="primero">Primer elemento a comparar.</param>
        ''' <param name="segundo">Segundo elemento a comparar.</param>
        ''' <returns>Booleano que indica la igualdad entre las estructuras.</returns>
        Public Shared Operator =(primero As PuntuacionesJuego, segundo As PuntuacionesJuego) As Boolean

            Return primero.centisegundos = segundo.centisegundos And
                primero.segundos = segundo.segundos And
                primero.minutos = segundo.minutos

        End Operator

        ''' <summary>
        ''' Verifica si 2 puntuaciones son desiguales.
        ''' </summary>
        ''' <param name="primero">Primer elemento a comparar.</param>
        ''' <param name="segundo">Segundo elemento a comparar.</param>
        ''' <returns>Booleano que indica la desigualdad entre las estructuras.</returns>
        Public Shared Operator <>(primero As PuntuacionesJuego, segundo As PuntuacionesJuego) As Boolean

            Return primero.centisegundos <> segundo.centisegundos Or
                primero.segundos <> segundo.segundos Or
                primero.minutos <> segundo.minutos

        End Operator

        ''' <summary>
        ''' Suma 2 estructuras de puntuaciónes.
        ''' </summary>
        ''' <param name="primero">Primer elemento a sumar.</param>
        ''' <param name="segundo">Segundo elemento a sumar.</param>
        ''' <returns>Estructura de puntuaciones sumadas.</returns>
        Public Shared Operator +(primero As PuntuacionesJuego, segundo As PuntuacionesJuego) As PuntuacionesJuego

            primero.centisegundos += segundo.centisegundos
            primero.segundos += segundo.centisegundos / 100
            primero.centisegundos = primero.centisegundos Mod 100
            primero.segundos += segundo.segundos
            primero.minutos += segundo.segundos / 60
            primero.segundos = primero.segundos Mod 60
            primero.minutos += segundo.minutos
            Return primero

        End Operator


        ''' <summary>
        ''' Convierte una estructura de puntuaciones a una estructura TimeSpan mediante casteo.
        ''' </summary>
        ''' <param name="origen">Estructura a convetir.</param>
        ''' <returns>TimeSpan equivalente a la estructura de puntuacion.</returns>
        Public Shared Widening Operator CType(origen As PuntuacionesJuego) As TimeSpan
            Return origen.ConvertirTimeSpan()
        End Operator

        ''' <summary>
        ''' Convierte una estructura TimeSpan a una estructura de puntuaciones mediante casteo.
        ''' </summary>
        ''' <param name="origen">Estructura a convetir.</param>
        ''' <returns>Estructura de puntuaciones equivalente al TimeSpan.</returns>
        Public Shared Narrowing Operator CType(origen As TimeSpan) As PuntuacionesJuego
            Return New PuntuacionesJuego(origen)
        End Operator

    End Structure



    ''' <summary>Estructura que contiene toda la información del perfil de jugador.
    ''' <para>Esta información incluye: nombre del perfil, último nivel alcanzado, dificultad del juego, opciones de vídeo, sonido y controles y las puntuaciones de todos los niveles.</para></summary>
    <StructLayout(LayoutKind.Sequential, Pack:=8)>
    Public Structure PerfilJugador

        ''' <summary>Información del perfil, y configuraciones.</summary>
        Public opciones As OpcionesJuego
        ''' <summary>Arreglo de puntuaciones de los 5 niveles.</summary>
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=ListaPerfilesJugador.NumeroNiveles)>
        Public puntuaciones() As PuntuacionesJuego

        ''' <summary>
        ''' Crea una instancia de esta estructura con puntuaciones vacías a partir de una estructura de información de perfil.
        ''' </summary>
        ''' <param name="opciones"><seealso cref="opciones"/></param>
        Public Sub New(opciones As OpcionesJuego)

            Me.opciones = opciones
            puntuaciones = New PuntuacionesJuego(ListaPerfilesJugador.NumeroNiveles) {}
            For c = 0 To puntuaciones.Length - 1 Step 1
                puntuaciones(c) = New PuntuacionesJuego()
            Next

        End Sub

        ''' <summary>
        ''' Crea una instancia vacía de esta estructura dado el nombre del perfil.
        ''' </summary>
        ''' <param name="nombre">Nombre del perfil de jugador.</param>
        Public Sub New(nombre As String)

            opciones = New OpcionesJuego(nombre)
            puntuaciones = New PuntuacionesJuego(ListaPerfilesJugador.NumeroNiveles) {}
            For c = 0 To puntuaciones.Length - 1 Step 1
                puntuaciones(c) = New PuntuacionesJuego()
            Next

        End Sub

        ''' <summary>
        ''' Obtiene la información completa del perfil de jugador, esto incluye sus configuraciones y sus puntuaciones en cada nivel.
        ''' </summary>
        ''' <returns>Cadena con toda la información del perfil organizada.</returns>
        Public Overrides Function ToString() As String

            Dim niveles As String = String.Empty
            For c = 0 To ListaPerfilesJugador.NumeroNiveles - 1 Step 1
                niveles += "Nivel " + CStr(c + 1) + ":" + Chr(9) + puntuaciones(c).ToString() + Chr(13) + Chr(10)
            Next
            Return opciones.ToString() + Chr(13) + Chr(10) + Chr(13) + Chr(10) + niveles

        End Function

    End Structure



    ''' <summary>Estructura de un nodo que forma parte de una lista de perfiles de jugador.</summary>
    <StructLayout(LayoutKind.Sequential, Pack:=8)>
    Public Structure NodoPerfil

        ''' <summary>Perfil del nodo. Véase <see cref="PerfilJugador"/></summary>
        Public perfil As IntPtr     ' PerfilJugador
        ''' <summary>Siguiente elemento de la lista.</summary>
        Public siguiente As IntPtr  ' NodoPerfil

    End Structure

End Namespace
