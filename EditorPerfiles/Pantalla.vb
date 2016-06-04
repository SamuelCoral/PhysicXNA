Imports System.Runtime.InteropServices

<StructLayout(LayoutKind.Sequential)>
Public Structure DEVMODE

    Private Const CCHDEVICENAME As Integer = &H20
    Private Const CCHFORMNAME As Integer = &H20
    ''' <summary></summary>
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=&H20)>
    Public dmDeviceName As String
    ''' <summary></summary>
    Public dmSpecVersion As Short
    ''' <summary></summary>
    Public dmDriverVersion As Short
    ''' <summary></summary>
    Public dmSize As Short
    ''' <summary></summary>
    Public dmDriverExtra As Short
    ''' <summary></summary>
    Public dmFields As Integer
    ''' <summary></summary>
    Public dmPositionX As Integer
    ''' <summary></summary>
    Public dmPositionY As Integer
    ''' <summary></summary>
    Public dmDisplayOrientation As Integer
    ''' <summary></summary>
    Public dmDisplayFixedOutput As Integer
    ''' <summary></summary>
    Public dmColor As Short
    ''' <summary></summary>
    Public dmDuplex As Short
    ''' <summary></summary>
    Public dmYResolution As Short
    ''' <summary></summary>
    Public dmTTOption As Short
    ''' <summary></summary>
    Public dmCollate As Short
    ''' <summary></summary>
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=&H20)>
    Public dmFormName As String
    ''' <summary></summary>
    Public dmLogPixels As Short
    ''' <summary></summary>
    Public dmBitsPerPel As Integer
    ''' <summary></summary>
    Public dmPelsWidth As Integer
    ''' <summary></summary>
    Public dmPelsHeight As Integer
    ''' <summary></summary>
    Public dmDisplayFlags As Integer
    ''' <summary></summary>
    Public dmDisplayFrequency As Integer
    ''' <summary></summary>
    Public dmICMMethod As Integer
    ''' <summary></summary>
    Public dmICMIntent As Integer
    ''' <summary></summary>
    Public dmMediaType As Integer
    ''' <summary></summary>
    Public dmDitherType As Integer
    ''' <summary></summary>
    Public dmReserved1 As Integer
    ''' <summary></summary>
    Public dmReserved2 As Integer
    ''' <summary></summary>
    Public dmPanningWidth As Integer
    ''' <summary></summary>
    Public dmPanningHeight As Integer

End Structure

Module Pantalla

    ''' <summary>
    ''' Obtiene una configuración de pantalla.
    ''' </summary>
    ''' <param name="deviceName">Nombre del dispositivo o null para la pantalla primaria.</param>
    ''' <param name="modeNum">Configuración a tomar.</param>
    ''' <param name="devMode">Estructura de información sobre la que se guardarán los datos obtenidos.</param>
    ''' <returns>false en caso de que la configuración no exista.</returns>
    <DllImport("user32.dll")>
    Private Function EnumDisplaySettings(deviceName As String, modeNum As Integer, ByRef devMode As DEVMODE) As Boolean

    End Function

    ''' <summary>Configuración de pantalla actual.</summary>
    Private Const ENUM_CURRENT_SETTINGS As Integer = -1
    ''' <summary>Configuración de pantalla almacenada en el registro de Windows.</summary>
    Private Const ENUM_REGISTRY_SETTINGS As Integer = -2

    ''' <summary>
    ''' Obtiene el listado de las resoluciones de pantalla permitidas.
    ''' </summary>
    ''' <returns>Lista de cadenas de las resoluciones permitidas en el formato "Ancho"x"Alto".</returns>
    Public Function ObtenerResoluciones() As List(Of String)

        Dim r As List(Of String) = New List(Of String)()

        Dim vDevMode As DEVMODE = New DEVMODE(), aDevMode As DEVMODE = New DEVMODE(), devMode As DEVMODE = New DEVMODE()
        EnumDisplaySettings(Nothing, ENUM_CURRENT_SETTINGS, devMode)

        Dim i As Integer = 0, c As Integer, repetido As Boolean
        While (EnumDisplaySettings(Nothing, i, vDevMode))

            i += 1
            'if (vDevMode.dmBitsPerPel != devMode.dmBitsPerPel || vDevMode.dmDisplayFrequency != devMode.dmDisplayFrequency) continue;
            repetido = False
            For c = i - 2 To 0 Step -1

                EnumDisplaySettings(Nothing, c, aDevMode)
                If aDevMode.dmPelsHeight = vDevMode.dmPelsHeight And aDevMode.dmPelsWidth = vDevMode.dmPelsWidth Then

                    repetido = True
                    Exit For

                End If
            Next

            If Not repetido And vDevMode.dmPelsWidth >= 800 And vDevMode.dmPelsHeight >= 600 Then
                r.Add(vDevMode.dmPelsWidth.ToString() + " x " + vDevMode.dmPelsHeight.ToString())
            End If

        End While

        Return r

    End Function

End Module
