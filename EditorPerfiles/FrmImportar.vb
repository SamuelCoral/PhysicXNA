Imports EditorPerfiles.SistemaPerfiles

Public Class FrmImportar

    Private ErrorAlImportar As Boolean
    Private NombresOmitidos As String
    Private ListaPerfiles As ListaPerfilesJugador

    Public Sub New(listaPerfilesImportar As ListaPerfilesJugador)

        ' Llamada necesaria para el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        ListaPerfiles = listaPerfilesImportar
        For Each perfil As PerfilJugador In listaPerfilesImportar
            LstPerfilesJugador.Items.Add(perfil.opciones.nombre)
        Next
    End Sub

    Private Sub BtnCancelar_Click(sender As Object, e As EventArgs) Handles BtnCancelar.Click
        Close()
    End Sub

    Private Sub FrmImportar_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed

        ListaPerfiles.Dispose()
        FrmEditor.Show()

    End Sub

    Private Sub BtnAceptar_Click(sender As Object, e As EventArgs) Handles BtnAceptar.Click

        If LstPerfilesJugador.SelectedItems.Count = 0 Then Close()
        ErrorAlImportar = False
        NombresOmitidos = String.Empty
        For C = 1 To LstPerfilesJugador.SelectedItems.Count
            Try

                FrmEditor.ListaPerfilesJugadorAbierta.AgregarPerfil(ListaPerfiles(LstPerfilesJugador.SelectedItems(C - 1)))
                FrmEditor.LstPerfilesJugador.Items.Add(LstPerfilesJugador.SelectedItems(C - 1))

            Catch ex As Exception

                ErrorAlImportar = True
                NombresOmitidos += LstPerfilesJugador.SelectedItems(C - 1).ToString() + Chr(13) + Chr(10)

            End Try
        Next
        If ErrorAlImportar Then MsgBox("Los siguientes perfiles no pudieron importarse porque los nombres están repetidos en la lista original:" +
            Chr(13) + Chr(10) + Chr(13) + Chr(10) + NombresOmitidos, MsgBoxStyle.Exclamation, "Error al importar perfiles")

        FrmEditor.LstPerfilesJugador.SelectedIndex = FrmEditor.LstPerfilesJugador.Items.Count - 1
        FrmEditor.AplicarCambios()
        Close()

    End Sub
End Class