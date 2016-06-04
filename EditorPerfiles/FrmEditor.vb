Imports EditorPerfiles.SistemaPerfiles
Imports System.IO

Public Class FrmEditor

    Friend ListaPerfilesJugadorAbierta As ListaPerfilesJugador = Nothing
    Private TituloVentana As String = "Editor de perfiles de PhysicXNA"
    Private DialogoAbrir As OpenFileDialog
    Private DialogoGuardar As SaveFileDialog
    Private FiltroArchivos As String = "Archivo de lista de perfiles de jugador de PhysicXNA (*.pjpx)|*.pjpx|Todos los archivos (*.*)|*.*"
    Private CambiosArchivo As Boolean = False
    Private ArchivoNuevo As Boolean
    Private CargandoDatos As Boolean = False
    Private BarraClickeada As Boolean = False

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Text = TituloVentana

        Dim controlesConImagen As List(Of Control) = New List(Of Control)() From {
            BtnSubirTodos,
            BtnSubirUno,
            BtnMover,
            BtnBajarUno,
            BtnBajarTodos
        }

        For Each control As Button In controlesConImagen
            Using i As Bitmap = New Bitmap(control.Height - 8, control.Height - 10), g As Graphics = Graphics.FromImage(i)
                g.DrawImage(control.Image, 0, 0, i.Width, i.Height)
                control.Image = New Bitmap(i)
            End Using
        Next

        Dim controlesConImagenARotar As List(Of Control) = New List(Of Control)() From {BtnBajarTodos, BtnBajarUno}
        For Each control As Button In controlesConImagenARotar
            Using i As Bitmap = New Bitmap(control.Image)
                i.RotateFlip(RotateFlipType.RotateNoneFlipY)
                control.Image = New Bitmap(i)
            End Using
        Next

        NumNivelPuntuaciones.Maximum = ListaPerfilesJugador.NumeroNiveles
        For Each resolucion As String In ObtenerResoluciones()
            CmbResoluciones.Items.Add(resolucion)
        Next

        DialogoAbrir = New OpenFileDialog()
        DialogoGuardar = New SaveFileDialog()
        DialogoAbrir.Filter = FiltroArchivos
        DialogoGuardar.Filter = FiltroArchivos
        DialogoAbrir.FileName = ListaPerfilesJugador.RutaPerfiles
        DialogoGuardar.FileName = ListaPerfilesJugador.RutaPerfiles

    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        If Not IntentarCerrarArchivo() Then

            e.Cancel = True
            Exit Sub

        End If
        CambiosArchivo = False
        CerrarToolStripMenuItem.PerformClick()

    End Sub

    Private Sub SalirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalirToolStripMenuItem.Click
        Close()
    End Sub

    Private Sub CerrarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CerrarToolStripMenuItem.Click

        If Not IntentarCerrarArchivo() Then Exit Sub
        If Not ListaPerfilesJugadorAbierta Is Nothing Then ListaPerfilesJugadorAbierta.Dispose()
        ListaPerfilesJugadorAbierta = Nothing
        CambiosArchivo = False
        HabilitarDeshabilitarControles(False)
        Text = TituloVentana

        LstPerfilesJugador.SelectedIndex = -1
        LstPerfilesJugador.Items.Clear()

    End Sub

    Private Sub HabilitarDeshabilitarControles(habilitar As Boolean)

        GuardarToolStripMenuItem.Enabled = habilitar
        GuardarComoToolStripMenuItem.Enabled = habilitar
        CerrarToolStripMenuItem.Enabled = habilitar
        GrpPerfilesJugador.Enabled = habilitar
        GrpOperacionesLista.Enabled = habilitar

    End Sub

    Private Function IntentarCerrarArchivo() As Boolean

        If CambiosArchivo Then

            Select Case MsgBox("¿Desea guardar los cambios efectuados al documento antes de cerrarlo?", MsgBoxStyle.YesNoCancel, "Confirmar salir")
                Case MsgBoxResult.Yes
                    Return GuardarArchivo()
                Case MsgBoxResult.Cancel
                    Return False
            End Select

        End If
        Return True

    End Function

    Friend Sub AplicarCambios()

        If CambiosArchivo Then Exit Sub
        CambiosArchivo = True
        Text += "*"

    End Sub

    Private Function GuardarArchivo() As Boolean

        If Not CambiosArchivo Then Return True
        If ArchivoNuevo Then
            Return GuardarArchivoComo()
        Else
            ListaPerfilesJugadorAbierta.Guadar(DialogoAbrir.FileName)
        End If

        Text = Text.Remove(Text.Length - 1)
        CambiosArchivo = False
        Return True

    End Function

    Private Function GuardarArchivoComo() As Boolean

        If DialogoGuardar.ShowDialog() = Windows.Forms.DialogResult.OK Then

            ListaPerfilesJugadorAbierta.Guadar(DialogoGuardar.FileName)
            DialogoAbrir.FileName = DialogoGuardar.FileName
            Text = TituloVentana + " - " + Path.GetFileName(DialogoAbrir.FileName)
            CambiosArchivo = False
            Return True

        End If
        Return False

    End Function

    Private Sub AbrirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbrirToolStripMenuItem.Click

        If Not IntentarCerrarArchivo() Then Exit Sub
        If DialogoAbrir.ShowDialog() = Windows.Forms.DialogResult.OK Then

            Try

                CambiosArchivo = False
                CerrarToolStripMenuItem.PerformClick()
                ListaPerfilesJugadorAbierta = New ListaPerfilesJugador(DialogoAbrir.FileName)
                HabilitarDeshabilitarControles(True)
                ArchivoNuevo = False
                Text = TituloVentana + " - " + Path.GetFileName(DialogoAbrir.FileName)
                DialogoGuardar.FileName = DialogoAbrir.FileName
                For Each Perfil In ListaPerfilesJugadorAbierta
                    LstPerfilesJugador.Items.Add(Perfil.opciones.nombre)
                Next
                LstPerfilesJugador.SelectedIndex = 0

            Catch ex As ExcepcionPerfilJugador
                MsgBox(ex.Message + Chr(13) + Chr(10) + "Esto es debido a que el archivo seleccionado tenga un formato incorrecto, esté dañado o vacío.",
                       MsgBoxStyle.Critical, "Error al abrir")
            End Try

        End If

    End Sub

    Private Sub NuevoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NuevoToolStripMenuItem.Click

        If Not IntentarCerrarArchivo() Then Exit Sub
        CerrarToolStripMenuItem.PerformClick()
        ListaPerfilesJugadorAbierta = New ListaPerfilesJugador()
        HabilitarDeshabilitarControles(True)
        ArchivoNuevo = True
        Text = TituloVentana + " - Archivo nuevo"
        DialogoAbrir.FileName = ListaPerfilesJugador.RutaPerfiles
        DialogoGuardar.FileName = ListaPerfilesJugador.RutaPerfiles

    End Sub

    Private Sub GuardarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GuardarToolStripMenuItem.Click
        GuardarArchivo()
    End Sub

    Private Sub GuardarComoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GuardarComoToolStripMenuItem.Click
        GuardarArchivoComo()
    End Sub

    Public Sub SubirBajarElementoSeleccionado(Subir As Boolean)

        Dim incremento As Integer = IIf(Subir, -1, 1)
        ListaPerfilesJugadorAbierta.IntercambiarLugares(LstPerfilesJugador.SelectedIndex + 1 + incremento, LstPerfilesJugador.SelectedIndex + 1)
        Dim itemCambiar As String = LstPerfilesJugador.Items(LstPerfilesJugador.SelectedIndex)
        LstPerfilesJugador.Items(LstPerfilesJugador.SelectedIndex) = LstPerfilesJugador.Items(LstPerfilesJugador.SelectedIndex + incremento)
        LstPerfilesJugador.Items(LstPerfilesJugador.SelectedIndex + incremento) = itemCambiar
        LstPerfilesJugador.SelectedIndex += incremento
        AplicarCambios()

    End Sub

    Private Sub BtnSubirTodos_Click(sender As Object, e As EventArgs) Handles BtnSubirTodos.Click
        While LstPerfilesJugador.SelectedIndex <> 0
            SubirBajarElementoSeleccionado(True)
        End While
    End Sub

    Private Sub BtnSubirUno_Click(sender As Object, e As EventArgs) Handles BtnSubirUno.Click
        SubirBajarElementoSeleccionado(True)
    End Sub

    Private Sub BtnMover_Click(sender As Object, e As EventArgs) Handles BtnMover.Click

        Dim posicionCadena As String = InputBox("Escriba la nueva posición en la que desea colocar el perfil en la lista." +
                                                Chr(13) + Chr(10) + "(Valores del 1 al " + LstPerfilesJugador.Items.Count.ToString() + ").",
                                                "Mover perfil", CStr(LstPerfilesJugador.SelectedIndex + 1))

        If posicionCadena = String.Empty Then Exit Sub
        Dim posicion As Integer
        Try
            posicion = Integer.Parse(posicionCadena)
        Catch ex As ArgumentNullException
            MsgBox("El valor no puede ser vacío", MsgBoxStyle.Exclamation, "Error al mover")
            Exit Sub
        Catch ex As FormatException
            MsgBox("El valor debe conformarse de solo números.", MsgBoxStyle.Exclamation, "Error al mover")
            Exit Sub
        Catch ex As OverflowException
            MsgBox("El valor es un número muy largo." + Chr(13) + Chr(10) +
                    "Asegúrese de haber escogido un valor válido.", MsgBoxStyle.Exclamation, "Error al mover")
            Exit Sub
        End Try
        If posicion > LstPerfilesJugador.Items.Count Or posicion < 1 Then
            MsgBox("El valor está fuera de los límites establecidor." + Chr(13) + Chr(10) +
                    "Asegúrese de haber escogido un valor válido.", MsgBoxStyle.Exclamation, "Error al mover")
            Exit Sub
        End If

        Dim subir As Boolean = posicion < LstPerfilesJugador.SelectedIndex + 1
        While LstPerfilesJugador.SelectedIndex <> posicion - 1
            SubirBajarElementoSeleccionado(subir)
        End While

    End Sub

    Private Sub BtnBajarUno_Click(sender As Object, e As EventArgs) Handles BtnBajarUno.Click
        SubirBajarElementoSeleccionado(False)
    End Sub

    Private Sub BtnBajarTodos_Click(sender As Object, e As EventArgs) Handles BtnBajarTodos.Click
        While LstPerfilesJugador.SelectedIndex <> LstPerfilesJugador.Items.Count - 1
            SubirBajarElementoSeleccionado(False)
        End While
    End Sub

    Private Sub LstPerfilesJugador_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LstPerfilesJugador.SelectedIndexChanged

        BtnSubirTodos.Enabled = LstPerfilesJugador.SelectedIndex <> -1 And LstPerfilesJugador.SelectedIndex > 0
        BtnSubirUno.Enabled = BtnSubirTodos.Enabled
        BtnBajarTodos.Enabled = LstPerfilesJugador.SelectedIndex <> -1 And LstPerfilesJugador.SelectedIndex < LstPerfilesJugador.Items.Count - 1
        BtnBajarUno.Enabled = BtnBajarTodos.Enabled
        BtnMover.Enabled = BtnBajarTodos.Enabled Or BtnSubirTodos.Enabled

        Dim habilitar As Boolean = LstPerfilesJugador.SelectedIndex <> -1
        GrpOperacionesPerfil.Enabled = habilitar
        GrpOpciones.Enabled = habilitar
        GrpOpcionesJuego.Enabled = habilitar
        GrpPuntuaciones.Enabled = habilitar

        If LstPerfilesJugador.SelectedIndex <> -1 Then

            CargandoDatos = True
            Dim perfilSeleccionado As PerfilJugador = ListaPerfilesJugadorAbierta(LstPerfilesJugador.SelectedIndex + 1)
            'NumNivel.Value = perfilSeleccionado.opciones.nivel
            LblNivelSuperado.Text = perfilSeleccionado.opciones.nivel.ToString()
            'ChkJuegoCompletado.Checked = perfilSeleccionado.opciones.juego_completado
            LblJuegoCompletado.Text = CStr(IIf(perfilSeleccionado.opciones.juego_completado, "Si", "No"))
            CmbDificultad.SelectedIndex = perfilSeleccionado.opciones.dificultad
            CmbResoluciones.SelectedIndex = -1
            CmbResoluciones.SelectedItem = perfilSeleccionado.opciones.res_horizontal.ToString() + " x " + perfilSeleccionado.opciones.res_vertical.ToString()
            If CmbResoluciones.SelectedIndex = -1 Then

                MsgBox("La resolución de pantalla configurada para este perfil no es compatible con esta pantalla." + Chr(13) + Chr(10) +
                       "Se ha ajustado la resolución mas baja.", MsgBoxStyle.Information, "Resolución de pantalla incompatible")
                CmbResoluciones.SelectedIndex = 0

            End If
            ChkPantallaCompleta.Checked = perfilSeleccionado.opciones.pantalla_completa
            TrkVolumen.Value = perfilSeleccionado.opciones.volumen
            LblVolumen.Text = TrkVolumen.Value.ToString()
            ChkMusica.Checked = perfilSeleccionado.opciones.musica
            ChkSonidos.Checked = perfilSeleccionado.opciones.sonidos
            ChkWASD.Checked = perfilSeleccionado.opciones.wasd
            ChkInvertirMouse.Checked = perfilSeleccionado.opciones.invertir_mouse
            'NumMinutos.Value = perfilSeleccionado.puntuaciones(NumNivelPuntuaciones.Value - 1).minutos
            'NumSegundos.Value = perfilSeleccionado.puntuaciones(NumNivelPuntuaciones.Value - 1).segundos
            'NumCentesimas.Value = perfilSeleccionado.puntuaciones(NumNivelPuntuaciones.Value - 1).centisegundos
            LblPuntuacion.Text = perfilSeleccionado.puntuaciones(NumNivelPuntuaciones.Value - 1).ToString()
            CargandoDatos = False

        End If

    End Sub

    Private Sub BtnAgregarPerfil_Click(sender As Object, e As EventArgs) Handles BtnAgregarPerfil.Click

        Dim nombreNuevo As String = InputBox("Escriba el nombre para el nuevo perfil de jugador.", "Añadir perfil")
        If nombreNuevo.Length >= ListaPerfilesJugador.TamNombresJugador Then
            MsgBox("El nombre es demasiado largo." + Chr(13) + Chr(10) +
                   "Verifique que no exceda los " + ListaPerfilesJugador.TamNombresJugador.ToString() + " caracteres.", MsgBoxStyle.Exclamation, "Error al añadir")
            Exit Sub
        End If
        If nombreNuevo = String.Empty Then Exit Sub
        Try
            ListaPerfilesJugadorAbierta.AgregarPerfil(New PerfilJugador(nombreNuevo))
        Catch ex As ExcepcionPerfilJugador
            MsgBox(ex.Message + Chr(13) + Chr(10) + "Por favor escriba un nombre que no esté repetido en la lista.", MsgBoxStyle.Exclamation, "Error al añadir")
            Exit Sub
        End Try
        LstPerfilesJugador.Items.Add(nombreNuevo)
        LstPerfilesJugador.SelectedIndex = LstPerfilesJugador.Items.Count - 1
        AplicarCambios()

    End Sub

    Private Sub BtnInvertirLista_Click(sender As Object, e As EventArgs) Handles BtnInvertirLista.Click

        For c = 1 To LstPerfilesJugador.Items.Count / 2

            ListaPerfilesJugadorAbierta.IntercambiarLugares(c, LstPerfilesJugador.Items.Count - c + 1)
            Dim itemCambiar As String = LstPerfilesJugador.Items(c - 1)
            LstPerfilesJugador.Items(c - 1) = LstPerfilesJugador.Items(LstPerfilesJugador.Items.Count - c)
            LstPerfilesJugador.Items(LstPerfilesJugador.Items.Count - c) = itemCambiar

        Next
        LstPerfilesJugador.SelectedIndex = -1
        AplicarCambios()

    End Sub

    Private Sub BtnOrdenarLista_Click(sender As Object, e As EventArgs) Handles BtnOrdenarLista.Click

        Dim nombresOrdenados As List(Of String) = New List(Of String)()
        For Each nombreLista As String In LstPerfilesJugador.Items
            nombresOrdenados.Add(nombreLista)
        Next
        nombresOrdenados.Sort()
        If RdbDescendente.Checked Then nombresOrdenados.Reverse()
        For c = 1 To LstPerfilesJugador.Items.Count
            ListaPerfilesJugadorAbierta.IntercambiarLugares(c, ListaPerfilesJugadorAbierta.BuscarPerfil(nombresOrdenados(c - 1)))
        Next
        For c = 0 To LstPerfilesJugador.Items.Count - 1
            LstPerfilesJugador.Items(c) = nombresOrdenados(c)
        Next
        LstPerfilesJugador.SelectedIndex = -1
        AplicarCambios()

    End Sub

    Private Sub BtnRenombrar_Click(sender As Object, e As EventArgs) Handles BtnRenombrar.Click

        Dim nombreNuevo As String = InputBox("Escriba el nuevo nombre para el perfil de jugador.",
                                             "Renombrar perfil", LstPerfilesJugador.Items(LstPerfilesJugador.SelectedIndex))
        If nombreNuevo.Length >= ListaPerfilesJugador.TamNombresJugador Then
            MsgBox("El nombre es demasiado largo." + Chr(13) + Chr(10) +
                   "Verifique que no exceda los " + ListaPerfilesJugador.TamNombresJugador.ToString() + " caracteres.", MsgBoxStyle.Exclamation, "Error al renombrar")
            Exit Sub
        End If
        If nombreNuevo = String.Empty Then Exit Sub
        Try

            Dim perfilAuxiliar As PerfilJugador = ListaPerfilesJugadorAbierta(LstPerfilesJugador.SelectedIndex + 1)
            If nombreNuevo = perfilAuxiliar.opciones.nombre Then Exit Sub
            perfilAuxiliar.opciones.nombre = nombreNuevo
            ListaPerfilesJugadorAbierta(LstPerfilesJugador.SelectedIndex + 1) = perfilAuxiliar

        Catch ex As ExcepcionPerfilJugador
            MsgBox(ex.Message + Chr(13) + Chr(10) + "Por favor escriba un nombre que no esté repetido en la lista.", MsgBoxStyle.Exclamation, "Error al renombrar")
            Exit Sub
        End Try
        LstPerfilesJugador.Items(LstPerfilesJugador.SelectedIndex) = nombreNuevo
        AplicarCambios()

    End Sub

    Private Sub BtnCopiar_Click(sender As Object, e As EventArgs) Handles BtnCopiar.Click

        Dim nombreNuevo As String = InputBox("Escriba el nuevo nombre para el perfil de jugador.",
                                             "Copiar perfil", "Copia de " + LstPerfilesJugador.Items(LstPerfilesJugador.SelectedIndex))
        If nombreNuevo.Length >= ListaPerfilesJugador.TamNombresJugador Then
            MsgBox("El nombre es demasiado largo." + Chr(13) + Chr(10) +
                   "Verifique que no exceda los " + ListaPerfilesJugador.TamNombresJugador.ToString() + " caracteres.", MsgBoxStyle.Exclamation, "Error al copiar")
            Exit Sub
        End If
        If nombreNuevo = String.Empty Then Exit Sub
        Try

            Dim perfilAuxiliar As PerfilJugador = ListaPerfilesJugadorAbierta(LstPerfilesJugador.SelectedIndex + 1)
            perfilAuxiliar.opciones.nombre = nombreNuevo
            ListaPerfilesJugadorAbierta.AgregarPerfil(perfilAuxiliar)

        Catch ex As ExcepcionPerfilJugador
            MsgBox(ex.Message + Chr(13) + Chr(10) + "Por favor escriba un nombre que no esté repetido en la lista.", MsgBoxStyle.Exclamation, "Error al copiar")
            Exit Sub
        End Try
        LstPerfilesJugador.Items.Add(nombreNuevo)
        AplicarCambios()

    End Sub

    Private Sub BntEliminar_Click(sender As Object, e As EventArgs) Handles BtnEliminar.Click
        If MsgBox("¿Está seguro de que desea eliminar el perfil seleccionado?", MsgBoxStyle.YesNo, "Eliminar perfil") = MsgBoxResult.Yes Then

            ListaPerfilesJugadorAbierta.EliminarPerfil(LstPerfilesJugador.SelectedIndex + 1)
            LstPerfilesJugador.Items.RemoveAt(LstPerfilesJugador.SelectedIndex)
            LstPerfilesJugador.SelectedIndex = -1
            AplicarCambios()

        End If
    End Sub

    Private Sub BtnImportar_Click(sender As Object, e As EventArgs) Handles BtnImportar.Click
        Using dialogoAbrirImportar As OpenFileDialog = New OpenFileDialog()

            dialogoAbrirImportar.Filter = DialogoAbrir.Filter
            dialogoAbrirImportar.FileName = DialogoAbrir.FileName
            If dialogoAbrirImportar.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Try

                    Dim listaPerfilesImportar As ListaPerfilesJugador = New ListaPerfilesJugador(dialogoAbrirImportar.FileName)
                    Dim formularioImportar As FrmImportar = New FrmImportar(listaPerfilesImportar)
                    formularioImportar.ShowDialog()

                Catch ex As ExcepcionPerfilJugador
                    MsgBox(ex.Message + Chr(13) + Chr(10) + "Esto es debido a que el archivo seleccionado tenga un formato incorrecto, esté dañado o vacío.",
                       MsgBoxStyle.Critical, "Error al abrir")
                End Try
            End If

        End Using
    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrkVolumen.Scroll

        LblVolumen.Text = TrkVolumen.Value.ToString()
        If BarraClickeada Then Exit Sub
        GuardarCambios(TrkVolumen, e)

    End Sub

    Private Sub TrkVolumen_MouseDown(sender As Object, e As MouseEventArgs) Handles TrkVolumen.MouseDown
        BarraClickeada = True
    End Sub

    Private Sub TrkVolumen_MouseUp(sender As Object, e As MouseEventArgs) Handles TrkVolumen.MouseUp

        If Not BarraClickeada Then Exit Sub
        BarraClickeada = False
        GuardarCambios(TrkVolumen, e)

    End Sub

    Private Sub NumNivelPuntuaciones_ValueChanged(sender As Object, e As EventArgs) Handles NumNivelPuntuaciones.ValueChanged

        If ListaPerfilesJugadorAbierta Is Nothing Then Exit Sub
        CargandoDatos = True
        Dim perfilSeleccionado As PerfilJugador = ListaPerfilesJugadorAbierta(LstPerfilesJugador.SelectedIndex + 1)
        'NumMinutos.Value = perfilSeleccionado.puntuaciones(NumNivelPuntuaciones.Value - 1).minutos
        'NumSegundos.Value = perfilSeleccionado.puntuaciones(NumNivelPuntuaciones.Value - 1).segundos
        'NumCentesimas.Value = perfilSeleccionado.puntuaciones(NumNivelPuntuaciones.Value - 1).centisegundos
        LblPuntuacion.Text = perfilSeleccionado.puntuaciones(NumNivelPuntuaciones.Value - 1).ToString()
        CargandoDatos = False

    End Sub

    Private Sub GuardarCambios(sender As Object, e As EventArgs) Handles _
        CmbDificultad.SelectedIndexChanged,
        CmbResoluciones.SelectedIndexChanged,
        ChkPantallaCompleta.CheckedChanged,
        ChkSonidos.CheckedChanged,
        ChkMusica.CheckedChanged,
        ChkWASD.CheckedChanged,
        ChkInvertirMouse.CheckedChanged

        If CargandoDatos Then Exit Sub
        Dim perfilActualizar As PerfilJugador = ListaPerfilesJugadorAbierta(LstPerfilesJugador.SelectedIndex + 1)
        'perfilActualizar.opciones.nivel = NumNivel.Value
        'perfilActualizar.opciones.juego_completado = ChkJuegoCompletado.Checked
        perfilActualizar.opciones.dificultad = CmbDificultad.SelectedIndex
        perfilActualizar.opciones.res_horizontal = CmbResoluciones.SelectedItem.ToString.Split("x".ToCharArray(), 2)(0)
        perfilActualizar.opciones.res_vertical = CmbResoluciones.SelectedItem.ToString.Split("x".ToCharArray(), 2)(1)
        perfilActualizar.opciones.pantalla_completa = ChkPantallaCompleta.Checked
        perfilActualizar.opciones.volumen = TrkVolumen.Value
        perfilActualizar.opciones.musica = ChkMusica.Checked
        perfilActualizar.opciones.sonidos = ChkSonidos.Checked
        perfilActualizar.opciones.wasd = ChkWASD.Checked
        perfilActualizar.opciones.invertir_mouse = ChkInvertirMouse.Checked
        'perfilActualizar.puntuaciones(NumNivelPuntuaciones.Value - 1).minutos = NumMinutos.Value
        'perfilActualizar.puntuaciones(NumNivelPuntuaciones.Value - 1).segundos = NumSegundos.Value
        'perfilActualizar.puntuaciones(NumNivelPuntuaciones.Value - 1).centisegundos = NumCentesimas.Value
        ListaPerfilesJugadorAbierta(LstPerfilesJugador.SelectedIndex + 1) = perfilActualizar
        AplicarCambios()

    End Sub

    Private Sub BtnReiniciarPuntuaciones_Click(sender As Object, e As EventArgs) Handles BtnReiniciarPuntuaciones.Click
        If MsgBox("¿Está seguro que desea reiniciar todos sus mejores tiempos?", MsgBoxStyle.YesNo, "Reiniciar puntuaciones") = MsgBoxResult.Yes Then

            'For c = NumNivelPuntuaciones.Minimum To NumNivelPuntuaciones.Maximum

            '   NumNivelPuntuaciones.Value = c
            '   NumMinutos.Value = 0
            '   NumSegundos.Value = 0
            '   NumCentesimas.Value = 0


            'Next
            Dim perfilSeleccionado = ListaPerfilesJugadorAbierta(LstPerfilesJugador.SelectedIndex + 1)
            For c = 0 To perfilSeleccionado.puntuaciones.Length - 1
                perfilSeleccionado.puntuaciones(c) = New PuntuacionesJuego()
            Next
            ListaPerfilesJugadorAbierta(LstPerfilesJugador.SelectedIndex + 1) = perfilSeleccionado
            NumNivelPuntuaciones.Value = NumNivelPuntuaciones.Minimum
            AplicarCambios()

        End If
    End Sub

    Private Sub AcercaDeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AcercaDeToolStripMenuItem.Click
        FrmAcercaDe.ShowDialog()
    End Sub

End Class
