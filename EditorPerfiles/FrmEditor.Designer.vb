<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmEditor
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmEditor))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArchivoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NuevoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.AbrirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GuardarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GuardarComoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CerrarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.SalirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AyudaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AcercaDeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LstPerfilesJugador = New System.Windows.Forms.ListBox()
        Me.GrpPerfilesJugador = New System.Windows.Forms.GroupBox()
        Me.BtnBajarTodos = New System.Windows.Forms.Button()
        Me.BtnSubirTodos = New System.Windows.Forms.Button()
        Me.BtnBajarUno = New System.Windows.Forms.Button()
        Me.BtnSubirUno = New System.Windows.Forms.Button()
        Me.BtnMover = New System.Windows.Forms.Button()
        Me.GrpOperacionesLista = New System.Windows.Forms.GroupBox()
        Me.BtnImportar = New System.Windows.Forms.Button()
        Me.GrpOrdenLista = New System.Windows.Forms.GroupBox()
        Me.RdbDescendente = New System.Windows.Forms.RadioButton()
        Me.RdbAscendente = New System.Windows.Forms.RadioButton()
        Me.BtnOrdenarLista = New System.Windows.Forms.Button()
        Me.BtnInvertirLista = New System.Windows.Forms.Button()
        Me.BtnAgregarPerfil = New System.Windows.Forms.Button()
        Me.GrpOperacionesPerfil = New System.Windows.Forms.GroupBox()
        Me.BtnEliminar = New System.Windows.Forms.Button()
        Me.BtnCopiar = New System.Windows.Forms.Button()
        Me.BtnRenombrar = New System.Windows.Forms.Button()
        Me.GrpOpcionesJuego = New System.Windows.Forms.GroupBox()
        Me.LblJuegoCompletado = New System.Windows.Forms.Label()
        Me.LblNivelSuperado = New System.Windows.Forms.Label()
        Me.CmbDificultad = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GrpOpciones = New System.Windows.Forms.GroupBox()
        Me.GrpOpcionesControles = New System.Windows.Forms.GroupBox()
        Me.ChkInvertirMouse = New System.Windows.Forms.CheckBox()
        Me.ChkWASD = New System.Windows.Forms.CheckBox()
        Me.GrpOpcionesSonido = New System.Windows.Forms.GroupBox()
        Me.ChkSonidos = New System.Windows.Forms.CheckBox()
        Me.ChkMusica = New System.Windows.Forms.CheckBox()
        Me.LblVolumen = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TrkVolumen = New System.Windows.Forms.TrackBar()
        Me.GrpOpcionesVideo = New System.Windows.Forms.GroupBox()
        Me.ChkPantallaCompleta = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CmbResoluciones = New System.Windows.Forms.ComboBox()
        Me.GrpPuntuaciones = New System.Windows.Forms.GroupBox()
        Me.LblPuntuacion = New System.Windows.Forms.Label()
        Me.BtnReiniciarPuntuaciones = New System.Windows.Forms.Button()
        Me.NumNivelPuntuaciones = New System.Windows.Forms.NumericUpDown()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TltFrmEditor = New System.Windows.Forms.ToolTip(Me.components)
        Me.MenuStrip1.SuspendLayout()
        Me.GrpPerfilesJugador.SuspendLayout()
        Me.GrpOperacionesLista.SuspendLayout()
        Me.GrpOrdenLista.SuspendLayout()
        Me.GrpOperacionesPerfil.SuspendLayout()
        Me.GrpOpcionesJuego.SuspendLayout()
        Me.GrpOpciones.SuspendLayout()
        Me.GrpOpcionesControles.SuspendLayout()
        Me.GrpOpcionesSonido.SuspendLayout()
        CType(Me.TrkVolumen, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpOpcionesVideo.SuspendLayout()
        Me.GrpPuntuaciones.SuspendLayout()
        CType(Me.NumNivelPuntuaciones, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArchivoToolStripMenuItem, Me.AyudaToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(733, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArchivoToolStripMenuItem
        '
        Me.ArchivoToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NuevoToolStripMenuItem, Me.ToolStripSeparator1, Me.AbrirToolStripMenuItem, Me.GuardarToolStripMenuItem, Me.GuardarComoToolStripMenuItem, Me.CerrarToolStripMenuItem, Me.ToolStripSeparator2, Me.SalirToolStripMenuItem})
        Me.ArchivoToolStripMenuItem.Name = "ArchivoToolStripMenuItem"
        Me.ArchivoToolStripMenuItem.Size = New System.Drawing.Size(60, 20)
        Me.ArchivoToolStripMenuItem.Text = "&Archivo"
        '
        'NuevoToolStripMenuItem
        '
        Me.NuevoToolStripMenuItem.Image = Global.EditorPerfiles.My.Resources.Resources.nuevo
        Me.NuevoToolStripMenuItem.Name = "NuevoToolStripMenuItem"
        Me.NuevoToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.NuevoToolStripMenuItem.Size = New System.Drawing.Size(202, 22)
        Me.NuevoToolStripMenuItem.Text = "&Nuevo"
        Me.NuevoToolStripMenuItem.ToolTipText = "Crea una lista de perfiles nueva."
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(199, 6)
        '
        'AbrirToolStripMenuItem
        '
        Me.AbrirToolStripMenuItem.Image = Global.EditorPerfiles.My.Resources.Resources.abrir
        Me.AbrirToolStripMenuItem.Name = "AbrirToolStripMenuItem"
        Me.AbrirToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.AbrirToolStripMenuItem.Size = New System.Drawing.Size(202, 22)
        Me.AbrirToolStripMenuItem.Text = "&Abrir..."
        Me.AbrirToolStripMenuItem.ToolTipText = "Abre una lista de perfiles de un archivo."
        '
        'GuardarToolStripMenuItem
        '
        Me.GuardarToolStripMenuItem.Enabled = False
        Me.GuardarToolStripMenuItem.Image = Global.EditorPerfiles.My.Resources.Resources.guardar
        Me.GuardarToolStripMenuItem.Name = "GuardarToolStripMenuItem"
        Me.GuardarToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G), System.Windows.Forms.Keys)
        Me.GuardarToolStripMenuItem.Size = New System.Drawing.Size(202, 22)
        Me.GuardarToolStripMenuItem.Text = "&Guardar"
        Me.GuardarToolStripMenuItem.ToolTipText = "Guarda los cambios a la lista de perfiles actual."
        '
        'GuardarComoToolStripMenuItem
        '
        Me.GuardarComoToolStripMenuItem.Enabled = False
        Me.GuardarComoToolStripMenuItem.Image = Global.EditorPerfiles.My.Resources.Resources.guardarComo
        Me.GuardarComoToolStripMenuItem.Name = "GuardarComoToolStripMenuItem"
        Me.GuardarComoToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.GuardarComoToolStripMenuItem.Size = New System.Drawing.Size(202, 22)
        Me.GuardarComoToolStripMenuItem.Text = "Guardar c&omo..."
        Me.GuardarComoToolStripMenuItem.ToolTipText = "Guarda la lista de perfiles actual en un archivo nuevo."
        '
        'CerrarToolStripMenuItem
        '
        Me.CerrarToolStripMenuItem.Enabled = False
        Me.CerrarToolStripMenuItem.Image = Global.EditorPerfiles.My.Resources.Resources.cerrar
        Me.CerrarToolStripMenuItem.Name = "CerrarToolStripMenuItem"
        Me.CerrarToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.CerrarToolStripMenuItem.Size = New System.Drawing.Size(202, 22)
        Me.CerrarToolStripMenuItem.Text = "&Cerrar"
        Me.CerrarToolStripMenuItem.ToolTipText = "Cierra la lista actual."
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(199, 6)
        '
        'SalirToolStripMenuItem
        '
        Me.SalirToolStripMenuItem.Image = CType(resources.GetObject("SalirToolStripMenuItem.Image"), System.Drawing.Image)
        Me.SalirToolStripMenuItem.Name = "SalirToolStripMenuItem"
        Me.SalirToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SalirToolStripMenuItem.Size = New System.Drawing.Size(202, 22)
        Me.SalirToolStripMenuItem.Text = "Sa&lir"
        Me.SalirToolStripMenuItem.ToolTipText = "Sale de la aplicación."
        '
        'AyudaToolStripMenuItem
        '
        Me.AyudaToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AcercaDeToolStripMenuItem})
        Me.AyudaToolStripMenuItem.Name = "AyudaToolStripMenuItem"
        Me.AyudaToolStripMenuItem.Size = New System.Drawing.Size(53, 20)
        Me.AyudaToolStripMenuItem.Text = "Ay&uda"
        '
        'AcercaDeToolStripMenuItem
        '
        Me.AcercaDeToolStripMenuItem.Name = "AcercaDeToolStripMenuItem"
        Me.AcercaDeToolStripMenuItem.Size = New System.Drawing.Size(135, 22)
        Me.AcercaDeToolStripMenuItem.Text = "Acerca &de..."
        '
        'LstPerfilesJugador
        '
        Me.LstPerfilesJugador.FormattingEnabled = True
        Me.LstPerfilesJugador.HorizontalScrollbar = True
        Me.LstPerfilesJugador.Location = New System.Drawing.Point(6, 19)
        Me.LstPerfilesJugador.Name = "LstPerfilesJugador"
        Me.LstPerfilesJugador.Size = New System.Drawing.Size(150, 134)
        Me.LstPerfilesJugador.TabIndex = 0
        '
        'GrpPerfilesJugador
        '
        Me.GrpPerfilesJugador.Controls.Add(Me.LstPerfilesJugador)
        Me.GrpPerfilesJugador.Controls.Add(Me.BtnBajarTodos)
        Me.GrpPerfilesJugador.Controls.Add(Me.BtnSubirTodos)
        Me.GrpPerfilesJugador.Controls.Add(Me.BtnBajarUno)
        Me.GrpPerfilesJugador.Controls.Add(Me.BtnSubirUno)
        Me.GrpPerfilesJugador.Controls.Add(Me.BtnMover)
        Me.GrpPerfilesJugador.Enabled = False
        Me.GrpPerfilesJugador.Location = New System.Drawing.Point(12, 27)
        Me.GrpPerfilesJugador.Name = "GrpPerfilesJugador"
        Me.GrpPerfilesJugador.Size = New System.Drawing.Size(261, 160)
        Me.GrpPerfilesJugador.TabIndex = 1
        Me.GrpPerfilesJugador.TabStop = False
        Me.GrpPerfilesJugador.Text = "Perfiles en la lista"
        '
        'BtnBajarTodos
        '
        Me.BtnBajarTodos.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnBajarTodos.Enabled = False
        Me.BtnBajarTodos.Image = Global.EditorPerfiles.My.Resources.Resources.invertir
        Me.BtnBajarTodos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnBajarTodos.Location = New System.Drawing.Point(165, 131)
        Me.BtnBajarTodos.Name = "BtnBajarTodos"
        Me.BtnBajarTodos.Size = New System.Drawing.Size(90, 22)
        Me.BtnBajarTodos.TabIndex = 5
        Me.BtnBajarTodos.Text = "Bajar todos"
        Me.BtnBajarTodos.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TltFrmEditor.SetToolTip(Me.BtnBajarTodos, "Baja el perfil seleccionado al final de la lista.")
        Me.BtnBajarTodos.UseVisualStyleBackColor = True
        '
        'BtnSubirTodos
        '
        Me.BtnSubirTodos.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSubirTodos.Enabled = False
        Me.BtnSubirTodos.Image = CType(resources.GetObject("BtnSubirTodos.Image"), System.Drawing.Image)
        Me.BtnSubirTodos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnSubirTodos.Location = New System.Drawing.Point(165, 19)
        Me.BtnSubirTodos.Name = "BtnSubirTodos"
        Me.BtnSubirTodos.Size = New System.Drawing.Size(90, 22)
        Me.BtnSubirTodos.TabIndex = 1
        Me.BtnSubirTodos.Text = "Subir todos"
        Me.BtnSubirTodos.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TltFrmEditor.SetToolTip(Me.BtnSubirTodos, "Sube el perfil seleccionado al conienzo de la lista.")
        Me.BtnSubirTodos.UseVisualStyleBackColor = True
        '
        'BtnBajarUno
        '
        Me.BtnBajarUno.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnBajarUno.Enabled = False
        Me.BtnBajarUno.Image = Global.EditorPerfiles.My.Resources.Resources.subir
        Me.BtnBajarUno.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnBajarUno.Location = New System.Drawing.Point(165, 103)
        Me.BtnBajarUno.Name = "BtnBajarUno"
        Me.BtnBajarUno.Size = New System.Drawing.Size(90, 22)
        Me.BtnBajarUno.TabIndex = 4
        Me.BtnBajarUno.Text = "Bajar uno"
        Me.BtnBajarUno.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TltFrmEditor.SetToolTip(Me.BtnBajarUno, "Baja el perfil seleccionado un lugar en la lista.")
        Me.BtnBajarUno.UseVisualStyleBackColor = True
        '
        'BtnSubirUno
        '
        Me.BtnSubirUno.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSubirUno.Enabled = False
        Me.BtnSubirUno.Image = Global.EditorPerfiles.My.Resources.Resources.subir
        Me.BtnSubirUno.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnSubirUno.Location = New System.Drawing.Point(165, 47)
        Me.BtnSubirUno.Name = "BtnSubirUno"
        Me.BtnSubirUno.Size = New System.Drawing.Size(90, 22)
        Me.BtnSubirUno.TabIndex = 2
        Me.BtnSubirUno.Text = "Subir uno"
        Me.BtnSubirUno.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TltFrmEditor.SetToolTip(Me.BtnSubirUno, "Sube el perfil seleccionado un lugar en la lista.")
        Me.BtnSubirUno.UseVisualStyleBackColor = True
        '
        'BtnMover
        '
        Me.BtnMover.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnMover.Enabled = False
        Me.BtnMover.Image = Global.EditorPerfiles.My.Resources.Resources.mover
        Me.BtnMover.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnMover.Location = New System.Drawing.Point(165, 75)
        Me.BtnMover.Name = "BtnMover"
        Me.BtnMover.Size = New System.Drawing.Size(90, 22)
        Me.BtnMover.TabIndex = 3
        Me.BtnMover.Text = "Mover"
        Me.BtnMover.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TltFrmEditor.SetToolTip(Me.BtnMover, "Mueve el perfil seleccionado al lugar especificado en la lista.")
        Me.BtnMover.UseVisualStyleBackColor = True
        '
        'GrpOperacionesLista
        '
        Me.GrpOperacionesLista.Controls.Add(Me.BtnImportar)
        Me.GrpOperacionesLista.Controls.Add(Me.GrpOrdenLista)
        Me.GrpOperacionesLista.Controls.Add(Me.BtnOrdenarLista)
        Me.GrpOperacionesLista.Controls.Add(Me.BtnInvertirLista)
        Me.GrpOperacionesLista.Controls.Add(Me.BtnAgregarPerfil)
        Me.GrpOperacionesLista.Enabled = False
        Me.GrpOperacionesLista.Location = New System.Drawing.Point(12, 193)
        Me.GrpOperacionesLista.Name = "GrpOperacionesLista"
        Me.GrpOperacionesLista.Size = New System.Drawing.Size(156, 246)
        Me.GrpOperacionesLista.TabIndex = 2
        Me.GrpOperacionesLista.TabStop = False
        Me.GrpOperacionesLista.Text = "Operaciones con la lista"
        '
        'BtnImportar
        '
        Me.BtnImportar.Image = Global.EditorPerfiles.My.Resources.Resources.importar
        Me.BtnImportar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnImportar.Location = New System.Drawing.Point(6, 50)
        Me.BtnImportar.Name = "BtnImportar"
        Me.BtnImportar.Size = New System.Drawing.Size(144, 32)
        Me.BtnImportar.TabIndex = 0
        Me.BtnImportar.Text = "Importar perfiles"
        Me.BtnImportar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TltFrmEditor.SetToolTip(Me.BtnImportar, "Añade perfiles de otro archivo a esta lista.")
        Me.BtnImportar.UseVisualStyleBackColor = True
        '
        'GrpOrdenLista
        '
        Me.GrpOrdenLista.Controls.Add(Me.RdbDescendente)
        Me.GrpOrdenLista.Controls.Add(Me.RdbAscendente)
        Me.GrpOrdenLista.Location = New System.Drawing.Point(6, 161)
        Me.GrpOrdenLista.Name = "GrpOrdenLista"
        Me.GrpOrdenLista.Size = New System.Drawing.Size(144, 79)
        Me.GrpOrdenLista.TabIndex = 4
        Me.GrpOrdenLista.TabStop = False
        Me.GrpOrdenLista.Text = "Orden"
        '
        'RdbDescendente
        '
        Me.RdbDescendente.AutoSize = True
        Me.RdbDescendente.Location = New System.Drawing.Point(6, 42)
        Me.RdbDescendente.Name = "RdbDescendente"
        Me.RdbDescendente.Size = New System.Drawing.Size(89, 17)
        Me.RdbDescendente.TabIndex = 1
        Me.RdbDescendente.Text = "Descendente"
        Me.RdbDescendente.UseVisualStyleBackColor = True
        '
        'RdbAscendente
        '
        Me.RdbAscendente.AutoSize = True
        Me.RdbAscendente.Checked = True
        Me.RdbAscendente.Location = New System.Drawing.Point(6, 19)
        Me.RdbAscendente.Name = "RdbAscendente"
        Me.RdbAscendente.Size = New System.Drawing.Size(82, 17)
        Me.RdbAscendente.TabIndex = 0
        Me.RdbAscendente.TabStop = True
        Me.RdbAscendente.Text = "Ascendente"
        Me.RdbAscendente.UseVisualStyleBackColor = True
        '
        'BtnOrdenarLista
        '
        Me.BtnOrdenarLista.Image = Global.EditorPerfiles.My.Resources.Resources.ordenar
        Me.BtnOrdenarLista.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnOrdenarLista.Location = New System.Drawing.Point(6, 123)
        Me.BtnOrdenarLista.Name = "BtnOrdenarLista"
        Me.BtnOrdenarLista.Size = New System.Drawing.Size(144, 32)
        Me.BtnOrdenarLista.TabIndex = 3
        Me.BtnOrdenarLista.Text = "Ordenar nombres"
        Me.BtnOrdenarLista.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TltFrmEditor.SetToolTip(Me.BtnOrdenarLista, "Ordena todos los perfiles de la lista en el modo especificado debajo.")
        Me.BtnOrdenarLista.UseVisualStyleBackColor = True
        '
        'BtnInvertirLista
        '
        Me.BtnInvertirLista.Image = Global.EditorPerfiles.My.Resources.Resources.invertir
        Me.BtnInvertirLista.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnInvertirLista.Location = New System.Drawing.Point(6, 81)
        Me.BtnInvertirLista.Name = "BtnInvertirLista"
        Me.BtnInvertirLista.Size = New System.Drawing.Size(144, 32)
        Me.BtnInvertirLista.TabIndex = 2
        Me.BtnInvertirLista.Text = "Invertir lista"
        Me.BtnInvertirLista.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TltFrmEditor.SetToolTip(Me.BtnInvertirLista, "Invierte el orden de todos los perfiles de la lista.")
        Me.BtnInvertirLista.UseVisualStyleBackColor = True
        '
        'BtnAgregarPerfil
        '
        Me.BtnAgregarPerfil.Image = Global.EditorPerfiles.My.Resources.Resources.agregar
        Me.BtnAgregarPerfil.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnAgregarPerfil.Location = New System.Drawing.Point(6, 19)
        Me.BtnAgregarPerfil.Name = "BtnAgregarPerfil"
        Me.BtnAgregarPerfil.Size = New System.Drawing.Size(144, 32)
        Me.BtnAgregarPerfil.TabIndex = 1
        Me.BtnAgregarPerfil.Text = "Añadir Perfil"
        Me.BtnAgregarPerfil.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TltFrmEditor.SetToolTip(Me.BtnAgregarPerfil, "Añade un perfil vacío a la lista.")
        Me.BtnAgregarPerfil.UseVisualStyleBackColor = True
        '
        'GrpOperacionesPerfil
        '
        Me.GrpOperacionesPerfil.Controls.Add(Me.BtnEliminar)
        Me.GrpOperacionesPerfil.Controls.Add(Me.BtnCopiar)
        Me.GrpOperacionesPerfil.Controls.Add(Me.BtnRenombrar)
        Me.GrpOperacionesPerfil.Enabled = False
        Me.GrpOperacionesPerfil.Location = New System.Drawing.Point(279, 27)
        Me.GrpOperacionesPerfil.Name = "GrpOperacionesPerfil"
        Me.GrpOperacionesPerfil.Size = New System.Drawing.Size(313, 57)
        Me.GrpOperacionesPerfil.TabIndex = 3
        Me.GrpOperacionesPerfil.TabStop = False
        Me.GrpOperacionesPerfil.Text = "Operaciones con el perfil seleccionado"
        '
        'BtnEliminar
        '
        Me.BtnEliminar.Image = Global.EditorPerfiles.My.Resources.Resources.borrar
        Me.BtnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnEliminar.Location = New System.Drawing.Point(210, 19)
        Me.BtnEliminar.Name = "BtnEliminar"
        Me.BtnEliminar.Size = New System.Drawing.Size(96, 32)
        Me.BtnEliminar.TabIndex = 2
        Me.BtnEliminar.Text = "Eliminar"
        Me.BtnEliminar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TltFrmEditor.SetToolTip(Me.BtnEliminar, "Elimina el perfil seleccionado.")
        Me.BtnEliminar.UseVisualStyleBackColor = True
        '
        'BtnCopiar
        '
        Me.BtnCopiar.Image = Global.EditorPerfiles.My.Resources.Resources.copiar
        Me.BtnCopiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnCopiar.Location = New System.Drawing.Point(108, 19)
        Me.BtnCopiar.Name = "BtnCopiar"
        Me.BtnCopiar.Size = New System.Drawing.Size(96, 32)
        Me.BtnCopiar.TabIndex = 1
        Me.BtnCopiar.Text = "Copiar"
        Me.BtnCopiar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TltFrmEditor.SetToolTip(Me.BtnCopiar, "Copia el perfil seleccionado asignándole un nuevo nombre.")
        Me.BtnCopiar.UseVisualStyleBackColor = True
        '
        'BtnRenombrar
        '
        Me.BtnRenombrar.Image = Global.EditorPerfiles.My.Resources.Resources.renombrar
        Me.BtnRenombrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnRenombrar.Location = New System.Drawing.Point(6, 19)
        Me.BtnRenombrar.Name = "BtnRenombrar"
        Me.BtnRenombrar.Size = New System.Drawing.Size(96, 32)
        Me.BtnRenombrar.TabIndex = 0
        Me.BtnRenombrar.Text = "Renombrar"
        Me.BtnRenombrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TltFrmEditor.SetToolTip(Me.BtnRenombrar, "Especifica un nuevo nombre para el perfil seleccionado.")
        Me.BtnRenombrar.UseVisualStyleBackColor = True
        '
        'GrpOpcionesJuego
        '
        Me.GrpOpcionesJuego.Controls.Add(Me.LblJuegoCompletado)
        Me.GrpOpcionesJuego.Controls.Add(Me.LblNivelSuperado)
        Me.GrpOpcionesJuego.Controls.Add(Me.CmbDificultad)
        Me.GrpOpcionesJuego.Controls.Add(Me.Label3)
        Me.GrpOpcionesJuego.Controls.Add(Me.Label2)
        Me.GrpOpcionesJuego.Controls.Add(Me.Label1)
        Me.GrpOpcionesJuego.Enabled = False
        Me.GrpOpcionesJuego.Location = New System.Drawing.Point(279, 90)
        Me.GrpOpcionesJuego.Name = "GrpOpcionesJuego"
        Me.GrpOpcionesJuego.Size = New System.Drawing.Size(313, 97)
        Me.GrpOpcionesJuego.TabIndex = 4
        Me.GrpOpcionesJuego.TabStop = False
        Me.GrpOpcionesJuego.Text = "Opciones del juego"
        '
        'LblJuegoCompletado
        '
        Me.LblJuegoCompletado.AutoSize = True
        Me.LblJuegoCompletado.Location = New System.Drawing.Point(141, 43)
        Me.LblJuegoCompletado.Name = "LblJuegoCompletado"
        Me.LblJuegoCompletado.Size = New System.Drawing.Size(21, 13)
        Me.LblJuegoCompletado.TabIndex = 6
        Me.LblJuegoCompletado.Text = "No"
        '
        'LblNivelSuperado
        '
        Me.LblNivelSuperado.AutoSize = True
        Me.LblNivelSuperado.Location = New System.Drawing.Point(141, 21)
        Me.LblNivelSuperado.Name = "LblNivelSuperado"
        Me.LblNivelSuperado.Size = New System.Drawing.Size(13, 13)
        Me.LblNivelSuperado.TabIndex = 5
        Me.LblNivelSuperado.Text = "0"
        '
        'CmbDificultad
        '
        Me.CmbDificultad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbDificultad.FormattingEnabled = True
        Me.CmbDificultad.Items.AddRange(New Object() {"Fácil", "Medio", "Difícil"})
        Me.CmbDificultad.Location = New System.Drawing.Point(144, 65)
        Me.CmbDificultad.Name = "CmbDificultad"
        Me.CmbDificultad.Size = New System.Drawing.Size(117, 21)
        Me.CmbDificultad.TabIndex = 2
        Me.TltFrmEditor.SetToolTip(Me.CmbDificultad, "Especifica la dificultad de juego seleccionada actualmente.")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 68)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(103, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Dificultado del juego"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(94, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Juego completado"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(108, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Último nivel superado"
        '
        'GrpOpciones
        '
        Me.GrpOpciones.Controls.Add(Me.GrpOpcionesControles)
        Me.GrpOpciones.Controls.Add(Me.GrpOpcionesSonido)
        Me.GrpOpciones.Controls.Add(Me.GrpOpcionesVideo)
        Me.GrpOpciones.Enabled = False
        Me.GrpOpciones.Location = New System.Drawing.Point(174, 193)
        Me.GrpOpciones.Name = "GrpOpciones"
        Me.GrpOpciones.Size = New System.Drawing.Size(547, 246)
        Me.GrpOpciones.TabIndex = 5
        Me.GrpOpciones.TabStop = False
        Me.GrpOpciones.Text = "Configuraciones del juego"
        '
        'GrpOpcionesControles
        '
        Me.GrpOpcionesControles.Controls.Add(Me.ChkInvertirMouse)
        Me.GrpOpcionesControles.Controls.Add(Me.ChkWASD)
        Me.GrpOpcionesControles.Location = New System.Drawing.Point(6, 199)
        Me.GrpOpcionesControles.Name = "GrpOpcionesControles"
        Me.GrpOpcionesControles.Size = New System.Drawing.Size(535, 41)
        Me.GrpOpcionesControles.TabIndex = 2
        Me.GrpOpcionesControles.TabStop = False
        Me.GrpOpcionesControles.Text = "Opciones de control"
        '
        'ChkInvertirMouse
        '
        Me.ChkInvertirMouse.AutoSize = True
        Me.ChkInvertirMouse.Location = New System.Drawing.Point(127, 19)
        Me.ChkInvertirMouse.Name = "ChkInvertirMouse"
        Me.ChkInvertirMouse.Size = New System.Drawing.Size(99, 17)
        Me.ChkInvertirMouse.TabIndex = 1
        Me.ChkInvertirMouse.Text = "Invertir botones"
        Me.TltFrmEditor.SetToolTip(Me.ChkInvertirMouse, "Especifica si se desea utilizar el botón derecho del ratón como botón principal p" & _
        "ara el juego.")
        Me.ChkInvertirMouse.UseVisualStyleBackColor = True
        '
        'ChkWASD
        '
        Me.ChkWASD.AutoSize = True
        Me.ChkWASD.Location = New System.Drawing.Point(6, 19)
        Me.ChkWASD.Name = "ChkWASD"
        Me.ChkWASD.Size = New System.Drawing.Size(84, 17)
        Me.ChkWASD.TabIndex = 0
        Me.ChkWASD.Text = "Usar WASD"
        Me.TltFrmEditor.SetToolTip(Me.ChkWASD, "Especifica si se desean utilizar las teclas WASD en lugar de las flechas direccio" & _
        "nales para ciertos niveles del juego.")
        Me.ChkWASD.UseVisualStyleBackColor = True
        '
        'GrpOpcionesSonido
        '
        Me.GrpOpcionesSonido.Controls.Add(Me.ChkSonidos)
        Me.GrpOpcionesSonido.Controls.Add(Me.ChkMusica)
        Me.GrpOpcionesSonido.Controls.Add(Me.LblVolumen)
        Me.GrpOpcionesSonido.Controls.Add(Me.Label6)
        Me.GrpOpcionesSonido.Controls.Add(Me.TrkVolumen)
        Me.GrpOpcionesSonido.Location = New System.Drawing.Point(6, 94)
        Me.GrpOpcionesSonido.Name = "GrpOpcionesSonido"
        Me.GrpOpcionesSonido.Size = New System.Drawing.Size(535, 96)
        Me.GrpOpcionesSonido.TabIndex = 1
        Me.GrpOpcionesSonido.TabStop = False
        Me.GrpOpcionesSonido.Text = "Opciones de sonido"
        '
        'ChkSonidos
        '
        Me.ChkSonidos.AutoSize = True
        Me.ChkSonidos.Location = New System.Drawing.Point(127, 70)
        Me.ChkSonidos.Name = "ChkSonidos"
        Me.ChkSonidos.Size = New System.Drawing.Size(64, 17)
        Me.ChkSonidos.TabIndex = 2
        Me.ChkSonidos.Text = "Sonidos"
        Me.TltFrmEditor.SetToolTip(Me.ChkSonidos, "Especifica si se desean escuchar los sonidos de los efectos especiales del juego." & _
        "")
        Me.ChkSonidos.UseVisualStyleBackColor = True
        '
        'ChkMusica
        '
        Me.ChkMusica.AutoSize = True
        Me.ChkMusica.Location = New System.Drawing.Point(6, 70)
        Me.ChkMusica.Name = "ChkMusica"
        Me.ChkMusica.Size = New System.Drawing.Size(60, 17)
        Me.ChkMusica.TabIndex = 1
        Me.ChkMusica.Text = "Música"
        Me.TltFrmEditor.SetToolTip(Me.ChkMusica, "Especifica si se desea escuchar la música del juego.")
        Me.ChkMusica.UseVisualStyleBackColor = True
        '
        'LblVolumen
        '
        Me.LblVolumen.AutoSize = True
        Me.LblVolumen.Location = New System.Drawing.Point(504, 22)
        Me.LblVolumen.Name = "LblVolumen"
        Me.LblVolumen.Size = New System.Drawing.Size(25, 13)
        Me.LblVolumen.TabIndex = 2
        Me.LblVolumen.Text = "255"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 22)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(48, 13)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Volumen"
        '
        'TrkVolumen
        '
        Me.TrkVolumen.Location = New System.Drawing.Point(60, 19)
        Me.TrkVolumen.Maximum = 255
        Me.TrkVolumen.Name = "TrkVolumen"
        Me.TrkVolumen.Size = New System.Drawing.Size(438, 45)
        Me.TrkVolumen.TabIndex = 0
        Me.TltFrmEditor.SetToolTip(Me.TrkVolumen, "Especifica el volumen de la música del juego.")
        Me.TrkVolumen.Value = 255
        '
        'GrpOpcionesVideo
        '
        Me.GrpOpcionesVideo.Controls.Add(Me.ChkPantallaCompleta)
        Me.GrpOpcionesVideo.Controls.Add(Me.Label5)
        Me.GrpOpcionesVideo.Controls.Add(Me.Label4)
        Me.GrpOpcionesVideo.Controls.Add(Me.CmbResoluciones)
        Me.GrpOpcionesVideo.Location = New System.Drawing.Point(6, 19)
        Me.GrpOpcionesVideo.Name = "GrpOpcionesVideo"
        Me.GrpOpcionesVideo.Size = New System.Drawing.Size(535, 69)
        Me.GrpOpcionesVideo.TabIndex = 0
        Me.GrpOpcionesVideo.TabStop = False
        Me.GrpOpcionesVideo.Text = "Opciones de video"
        '
        'ChkPantallaCompleta
        '
        Me.ChkPantallaCompleta.AutoSize = True
        Me.ChkPantallaCompleta.Location = New System.Drawing.Point(127, 46)
        Me.ChkPantallaCompleta.Name = "ChkPantallaCompleta"
        Me.ChkPantallaCompleta.Size = New System.Drawing.Size(15, 14)
        Me.ChkPantallaCompleta.TabIndex = 1
        Me.TltFrmEditor.SetToolTip(Me.ChkPantallaCompleta, "Especifica si se usa el modo de pantalla completa en el juego.")
        Me.ChkPantallaCompleta.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 46)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(92, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Pantalla Completa"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 21)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(115, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Resolución de pantalla"
        '
        'CmbResoluciones
        '
        Me.CmbResoluciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbResoluciones.FormattingEnabled = True
        Me.CmbResoluciones.Location = New System.Drawing.Point(127, 18)
        Me.CmbResoluciones.Name = "CmbResoluciones"
        Me.CmbResoluciones.Size = New System.Drawing.Size(209, 21)
        Me.CmbResoluciones.TabIndex = 0
        Me.TltFrmEditor.SetToolTip(Me.CmbResoluciones, "Especifica la resolución de pantalla en la que se ejecuta el juego.")
        '
        'GrpPuntuaciones
        '
        Me.GrpPuntuaciones.Controls.Add(Me.LblPuntuacion)
        Me.GrpPuntuaciones.Controls.Add(Me.BtnReiniciarPuntuaciones)
        Me.GrpPuntuaciones.Controls.Add(Me.NumNivelPuntuaciones)
        Me.GrpPuntuaciones.Controls.Add(Me.Label7)
        Me.GrpPuntuaciones.Enabled = False
        Me.GrpPuntuaciones.Location = New System.Drawing.Point(598, 27)
        Me.GrpPuntuaciones.Name = "GrpPuntuaciones"
        Me.GrpPuntuaciones.Size = New System.Drawing.Size(123, 160)
        Me.GrpPuntuaciones.TabIndex = 6
        Me.GrpPuntuaciones.TabStop = False
        Me.GrpPuntuaciones.Text = "Mejores tiempos"
        '
        'LblPuntuacion
        '
        Me.LblPuntuacion.AutoSize = True
        Me.LblPuntuacion.Location = New System.Drawing.Point(9, 75)
        Me.LblPuntuacion.Name = "LblPuntuacion"
        Me.LblPuntuacion.Size = New System.Drawing.Size(49, 13)
        Me.LblPuntuacion.TabIndex = 5
        Me.LblPuntuacion.Text = "00:00:00"
        '
        'BtnReiniciarPuntuaciones
        '
        Me.BtnReiniciarPuntuaciones.Image = Global.EditorPerfiles.My.Resources.Resources.reiniciar
        Me.BtnReiniciarPuntuaciones.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnReiniciarPuntuaciones.Location = New System.Drawing.Point(9, 117)
        Me.BtnReiniciarPuntuaciones.Name = "BtnReiniciarPuntuaciones"
        Me.BtnReiniciarPuntuaciones.Size = New System.Drawing.Size(105, 32)
        Me.BtnReiniciarPuntuaciones.TabIndex = 4
        Me.BtnReiniciarPuntuaciones.Text = "Reiniciar"
        Me.BtnReiniciarPuntuaciones.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TltFrmEditor.SetToolTip(Me.BtnReiniciarPuntuaciones, "Reinicia todos sus mejores tiempos.")
        Me.BtnReiniciarPuntuaciones.UseVisualStyleBackColor = True
        '
        'NumNivelPuntuaciones
        '
        Me.NumNivelPuntuaciones.Location = New System.Drawing.Point(74, 22)
        Me.NumNivelPuntuaciones.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumNivelPuntuaciones.Name = "NumNivelPuntuaciones"
        Me.NumNivelPuntuaciones.Size = New System.Drawing.Size(40, 20)
        Me.NumNivelPuntuaciones.TabIndex = 0
        Me.NumNivelPuntuaciones.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 24)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(31, 13)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Nivel"
        '
        'FrmEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(733, 451)
        Me.Controls.Add(Me.GrpPuntuaciones)
        Me.Controls.Add(Me.GrpOpciones)
        Me.Controls.Add(Me.GrpOpcionesJuego)
        Me.Controls.Add(Me.GrpOperacionesPerfil)
        Me.Controls.Add(Me.GrpOperacionesLista)
        Me.Controls.Add(Me.GrpPerfilesJugador)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.Name = "FrmEditor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.GrpPerfilesJugador.ResumeLayout(False)
        Me.GrpOperacionesLista.ResumeLayout(False)
        Me.GrpOrdenLista.ResumeLayout(False)
        Me.GrpOrdenLista.PerformLayout()
        Me.GrpOperacionesPerfil.ResumeLayout(False)
        Me.GrpOpcionesJuego.ResumeLayout(False)
        Me.GrpOpcionesJuego.PerformLayout()
        Me.GrpOpciones.ResumeLayout(False)
        Me.GrpOpcionesControles.ResumeLayout(False)
        Me.GrpOpcionesControles.PerformLayout()
        Me.GrpOpcionesSonido.ResumeLayout(False)
        Me.GrpOpcionesSonido.PerformLayout()
        CType(Me.TrkVolumen, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpOpcionesVideo.ResumeLayout(False)
        Me.GrpOpcionesVideo.PerformLayout()
        Me.GrpPuntuaciones.ResumeLayout(False)
        Me.GrpPuntuaciones.PerformLayout()
        CType(Me.NumNivelPuntuaciones, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ArchivoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NuevoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SalirToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LstPerfilesJugador As System.Windows.Forms.ListBox
    Friend WithEvents AbrirToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GuardarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GuardarComoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CerrarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BtnSubirTodos As System.Windows.Forms.Button
    Friend WithEvents BtnSubirUno As System.Windows.Forms.Button
    Friend WithEvents BtnMover As System.Windows.Forms.Button
    Friend WithEvents BtnBajarUno As System.Windows.Forms.Button
    Friend WithEvents BtnBajarTodos As System.Windows.Forms.Button
    Friend WithEvents GrpPerfilesJugador As System.Windows.Forms.GroupBox
    Friend WithEvents GrpOperacionesLista As System.Windows.Forms.GroupBox
    Friend WithEvents BtnInvertirLista As System.Windows.Forms.Button
    Friend WithEvents BtnAgregarPerfil As System.Windows.Forms.Button
    Friend WithEvents BtnOrdenarLista As System.Windows.Forms.Button
    Friend WithEvents GrpOrdenLista As System.Windows.Forms.GroupBox
    Friend WithEvents RdbDescendente As System.Windows.Forms.RadioButton
    Friend WithEvents RdbAscendente As System.Windows.Forms.RadioButton
    Friend WithEvents GrpOperacionesPerfil As System.Windows.Forms.GroupBox
    Friend WithEvents BtnEliminar As System.Windows.Forms.Button
    Friend WithEvents BtnCopiar As System.Windows.Forms.Button
    Friend WithEvents BtnRenombrar As System.Windows.Forms.Button
    Friend WithEvents BtnImportar As System.Windows.Forms.Button
    Friend WithEvents GrpOpcionesJuego As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CmbDificultad As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GrpOpciones As System.Windows.Forms.GroupBox
    Friend WithEvents CmbResoluciones As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GrpOpcionesVideo As System.Windows.Forms.GroupBox
    Friend WithEvents ChkPantallaCompleta As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GrpOpcionesSonido As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TrkVolumen As System.Windows.Forms.TrackBar
    Friend WithEvents LblVolumen As System.Windows.Forms.Label
    Friend WithEvents GrpOpcionesControles As System.Windows.Forms.GroupBox
    Friend WithEvents ChkInvertirMouse As System.Windows.Forms.CheckBox
    Friend WithEvents ChkWASD As System.Windows.Forms.CheckBox
    Friend WithEvents ChkSonidos As System.Windows.Forms.CheckBox
    Friend WithEvents ChkMusica As System.Windows.Forms.CheckBox
    Friend WithEvents GrpPuntuaciones As System.Windows.Forms.GroupBox
    Friend WithEvents NumNivelPuntuaciones As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents BtnReiniciarPuntuaciones As System.Windows.Forms.Button
    Friend WithEvents AyudaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AcercaDeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TltFrmEditor As System.Windows.Forms.ToolTip
    Friend WithEvents LblJuegoCompletado As System.Windows.Forms.Label
    Friend WithEvents LblNivelSuperado As System.Windows.Forms.Label
    Friend WithEvents LblPuntuacion As System.Windows.Forms.Label

End Class
