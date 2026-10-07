Public Class mdi

    Private Sub SalesEntryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesEntryToolStripMenuItem.Click
        sales.Show()
        billdeatil.Visible = False

    End Sub

    Private Sub SalesDetailsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesDetailsToolStripMenuItem.Click
        billdeatil.Show()
        sales.Visible = False
    End Sub

    Private Sub mdi_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
       
    End Sub

    Private Sub mdi_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Form2.Visible = True
    End Sub
End Class