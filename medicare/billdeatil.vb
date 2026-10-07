Imports System.IO
Imports System.Data.OleDb
Imports System.Drawing.Printing
Public Class billdeatil
    Dim cn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\VB-Project_Medicare\medicare.accdb")
    Dim WithEvents PD As New PrintDocument
    Dim PPD As New PrintPreviewDialog
    Dim longpaper As Integer

    Sub changelongpaper()
        Dim rowcount As Integer
        longpaper = 0
        rowcount = DataGridView1.Rows.Count
        longpaper = rowcount * 15
        longpaper = longpaper + 240
    End Sub

    Private Sub billdeatil_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = mdi

       

    End Sub

    


    Private Sub cbbn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbbn.Click
        cn.Open()

        Dim cmd As New OleDbCommand("select * from purchase where bdate='" & d1.Value.Date & "'", cn)
        cmd.ExecuteNonQuery()
        Dim da As New OleDbDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)
        cbbn.DataSource = dt
        If cbbn.DataSource Is Nothing Then
            MsgBox("no data")
        Else
            cbbn.DisplayMember = "bno"
        End If

        cn.Close()


    End Sub

    Private Sub cbbn_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cbbn.KeyDown
        If e.KeyCode = Keys.Enter Then

            display()
            display1()
            display2()
            calculation()

        End If

    End Sub
    Private Sub display()
        cn.Open()
        Dim cmd1 As New OleDbCommand("select * from bill where bdate='" & d1.Value.Date & "' and bno='" & cbbn.Text & "'", cn)
        cmd1.ExecuteNonQuery()
        Dim dr As OleDbDataReader
        dr = cmd1.ExecuteReader
        If dr.Read() Then
            TextBox1.Text = dr("cid")
            TextBox2.Text = dr("cname")

        Else
            MsgBox("enter correct data")
        End If

        cn.Close()



    End Sub
    Private Sub display1()
        cn.Open()
        Dim cmd3 As New OleDbCommand("select * from customer where cid='" & TextBox1.Text & "'", cn)
        cmd3.ExecuteNonQuery()
        Dim dr1 As OleDbDataReader
        dr1 = cmd3.ExecuteReader
        If dr1.Read() Then

            TextBox3.Text = dr1("no_visi")
        Else
            MsgBox("enter correct data")
        End If

        cn.Close()
    End Sub
    Private Sub display2()

        cn.Open()
        Dim cmd2 As New OleDbCommand("select itemno,itemname,itemquan,itemrate from bill where bno='" & cbbn.Text & "'", cn)
        cmd2.ExecuteNonQuery()
        Dim da1 As New OleDbDataAdapter(cmd2)
        Dim dt1 As New DataTable
        da1.Fill(dt1)
        DataGridView1.DataSource = dt1
        cn.Close()
    End Sub

    Private Sub calculation()
        Dim total As Decimal = 0
        For Each row As DataGridViewRow In DataGridView1.Rows
            total += row.Cells("itemrate").Value
        Next
        TextBox7.Text = total.ToString("0.00")
        TextBox4.Text = Val(TextBox7.Text) * 0.28

        If Val(TextBox3.Text) > 4 Then
            TextBox9.Text = ((Val(TextBox7.Text) + Val(TextBox4.Text)) * 0.04).ToString("0.00")
        Else
            TextBox9.Text = "0"

        End If
        TextBox10.Text = (Val(TextBox7.Text) + Val(TextBox4.Text) - Val(TextBox9.Text)).ToString("0.00")
    End Sub

    Private Sub DataGridView1_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles DataGridView1.RowsRemoved
        calculation()

    End Sub

    Private Sub PD_BeginPrint(ByVal sender As Object, ByVal e As PrintEventArgs) Handles PD.BeginPrint
        Dim pagesetup As New PageSettings
        pagesetup.PaperSize = New PaperSize("Custom", 250, 500) 'fixed size
        'pagesetup.PaperSize = New PaperSize("Custom", 250, longpaper)
        PD.DefaultPageSettings = pagesetup
    End Sub

    Private Sub PD_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs) Handles PD.PrintPage
        Dim f6 As New Font("calibri", 6, FontStyle.Regular)
        Dim f8 As New Font("Calibri", 8, FontStyle.Regular)
        Dim f10 As New Font("Calibri", 10, FontStyle.Regular)
        Dim f10b As New Font("Calibri", 10, FontStyle.Bold)
        Dim f12b As New Font("Calibri", 12, FontStyle.Bold)
        Dim f14 As New Font("Calibri", 14, FontStyle.Bold)

        Dim leftmargin As Integer = PD.DefaultPageSettings.Margins.Left
        Dim centermargin As Integer = PD.DefaultPageSettings.PaperSize.Width / 2
        Dim rightmargin As Integer = PD.DefaultPageSettings.PaperSize.Width

        'font alignment
        Dim right As New StringFormat
        Dim center As New StringFormat

        right.Alignment = StringAlignment.Far
        center.Alignment = StringAlignment.Center

        Dim line As String
        line = "****************************************************************"


        'e.Graphics.DrawImage(logoImage, 0, 250, 150, 50)
        'e.Graphics.DrawImage(logoImage, CInt((e.PageBounds.Width - logoImage.Width) / 2), CInt((e.PageBounds.Height - logoImage.Height) / 2), logoImage.Width, logoImage.Height)

        'e.Graphics.DrawString("Store :", f14, Brushes.Black, centermargin, 5, center)
        e.Graphics.DrawString("SHIBA MEDICALS", f12b, Brushes.Black, centermargin, 40, center)
        e.Graphics.DrawString("9A 19 THULASIMAGILAN COMPLEX, CHECKPOST, ODDANCHATRAM.", f6, Brushes.Black, centermargin, 60, center)

        e.Graphics.DrawString("Invoice ID", f8, Brushes.Black, 0, 75)
        e.Graphics.DrawString(":", f8, Brushes.Black, 50, 75)
        e.Graphics.DrawString("" & cbbn.Text & "", f8, Brushes.Black, 70, 75)

        e.Graphics.DrawString("Customer ID", f8, Brushes.Black, 100, 75)
        e.Graphics.DrawString(":", f8, Brushes.Black, 170, 75)
        e.Graphics.DrawString("" & TextBox1.Text & "", f8, Brushes.Black, 175, 75)

        e.Graphics.DrawString("Cashier", f8, Brushes.Black, 0, 85)
        e.Graphics.DrawString(":", f8, Brushes.Black, 50, 85)
        e.Graphics.DrawString("ABI", f8, Brushes.Black, 70, 85)

        e.Graphics.DrawString("Customer Name", f8, Brushes.Black, 100, 85)
        e.Graphics.DrawString(":", f8, Brushes.Black, 180, 85)
        e.Graphics.DrawString("" & TextBox2.Text & "", f8, Brushes.Black, 185, 85)

        e.Graphics.DrawString("" & d1.Value.Date & "", f8, Brushes.Black, 0, 95)
        'DetailHeader
        e.Graphics.DrawString("Item", f8, Brushes.Black, 0, 110)

        e.Graphics.DrawString("Qty", f8, Brushes.Black, 180, 110, right)
        e.Graphics.DrawString("Total", f8, Brushes.Black, rightmargin, 110, right)
        '
        e.Graphics.DrawString(line, f8, Brushes.Black, 0, 120)

        Dim height As Integer 'DGV Position

        Dim j As Long
        DataGridView1.AllowUserToAddRows = False
        'If DataGridView1.CurrentCell.Value Is Nothing Then
        '    Exit Sub
        'Else
        For row As Integer = 0 To DataGridView1.RowCount - 1
            height += 15
            e.Graphics.DrawString(DataGridView1.Rows(row).Cells(1).Value.ToString, f8, Brushes.Black, 0, 115 + height)
            
            e.Graphics.DrawString(DataGridView1.Rows(row).Cells(2).Value.ToString, f8, Brushes.Black, 180, 115 + height, right)
            j = DataGridView1.Rows(row).Cells(2).Value
            DataGridView1.Rows(row).Cells(2).Value = Format(j, "##,##0")
            e.Graphics.DrawString(DataGridView1.Rows(row).Cells(3).Value.ToString, f8, Brushes.Black, rightmargin, 115 + height, right)
        Next
        Dim height2 As Integer
        height2 = 145 + height
        Dim t_price As Double = Val(TextBox7.Text)
        Dim gst As Double = Val(TextBox4.Text)
        Dim dis As Double = Val(TextBox9.Text)
        Dim g_price As Double = Val(TextBox10.Text)
        e.Graphics.DrawString(line, f8, Brushes.Black, 0, height2)
        e.Graphics.DrawString("Total: " & Format(t_price, "##,##0"), f10, Brushes.Black, rightmargin, 10 + height2, right)
        e.Graphics.DrawString("GST: " & Format(gst, "##,##0"), f10, Brushes.Black, rightmargin, 25 + height2, right)
        e.Graphics.DrawString("Discount: " & Format(dis, "##,##0"), f10, Brushes.Black, rightmargin, 40 + height2, right)
        e.Graphics.DrawString("Bill Amount: " & Format(g_price, "##,##0"), f12b, Brushes.Black, rightmargin, 55 + height2, right)


        e.Graphics.DrawString("Thank you...", f10, Brushes.Black, centermargin, 70 + height2, right)

    End Sub






    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        changelongpaper()
        PPD.Document = PD
        PPD.ShowDialog()
        'PD.Print()  'Direct Print
    End Sub

  
    Private Sub d1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles d1.TextChanged
        cbbn.Text = ""
        TextBox1.Text = ""
        TextBox10.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox7.Text = ""
        TextBox9.Text = ""
        DataGridView1.DataSource = Nothing
    End Sub
End Class