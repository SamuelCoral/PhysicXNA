Namespace SistemaPerfiles

    ''' <summary>Excepción que ocurre al realizar operaciones con los perfiles de jugador.</summary>
    <Serializable>
    Public Class ExcepcionPerfilJugador : Inherits Exception

        ''' <summary>Crea una instancia de esta clase.</summary>
        Public Sub New()

        End Sub

        ''' <summary>
        ''' Crea una instancia de esta clase y especifica en un mensaje la causa del error.
        ''' </summary>
        ''' <param name="message">Mensaje que causó el error.</param>
        Public Sub New(message As String)
            MyBase.New(message)
        End Sub

        ''' <summary>
        ''' Crea una instancia de esta clase y especifica en un mensaje la causa del error y una excepción base.
        ''' </summary>
        ''' <param name="message">Mensaje que causó el error.</param>
        ''' <param name="inner">Excepción que causó esta nueva excepción.</param>
        Public Sub New(message As String, inner As Exception)
            MyBase.New(message, inner)
        End Sub

    End Class

End Namespace