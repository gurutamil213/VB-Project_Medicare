<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class mdi
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(mdi))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.SalesEntryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SalesDetailsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SalesEntryToolStripMenuItem, Me.SalesDetailsToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(705, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'SalesEntryToolStripMenuItem
        '
        Me.SalesEntryToolStripMenuItem.Name = "SalesEntryToolStripMenuItem"
        Me.SalesEntryToolStripMenuItem.Size = New System.Drawing.Size(75, 20)
        Me.SalesEntryToolStripMenuItem.Text = "Sales Entry"
        '
        'SalesDetailsToolStripMenuItem
        '
        Me.SalesDetailsToolStripMenuItem.Name = "SalesDetailsToolStripMenuItem"
        Me.SalesDetailsToolStripMenuItem.Size = New System.Drawing.Size(83, 20)
        Me.SalesDetailsToolStripMenuItem.Text = "Sales Details"
        '
        'mdi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(705, 389)
        Me.Controls.Add(Me.MenuStrip1)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "mdi"
        Me.Text = "SALES"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents SalesEntryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SalesDetailsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
