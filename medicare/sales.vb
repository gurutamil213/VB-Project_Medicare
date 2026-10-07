Imports System.DateTime
Imports System.IO
Imports System.Data.OleDb
Imports System.Drawing.Printing

Public Class sales
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

    Private Sub sales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Labeldate.Text = Date.Now.Date
        Me.MdiParent = mdi

        cn.Open()
        Dim cmd As New OleDbCommand("select * from customer", cn)
        cmd.ExecuteNonQuery()
        Dim da As New OleDbDataAdapter(cmd)
        Dim ds As New DataSet
        da.Fill(ds)
        Dim column As New AutoCompleteStringCollection
        Dim i As Integer
        For i = 0 To ds.Tables(0).Rows.Count - 1
            column.Add(ds.Tables(0).Rows(i)("cid").ToString)
        Next
        TextBox1.AutoCompleteSource = AutoCompleteSource.CustomSource
        TextBox1.AutoCompleteCustomSource = column
        TextBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cn.Close()


        cn.Open()
        Dim cmd3 As New OleDbCommand("select itemname from stock", cn)
        cmd3.ExecuteNonQuery()
        Dim da1 As New OleDbDataAdapter(cmd3)
        Dim ds1 As New DataTable
        da1.Fill(ds1)
        ComboBox1.DataSource = ds1
        ComboBox1.DisplayMember = "itemname"
        cn.Close()

        'bill number auto count
        cn.Open()
        Dim cmd6 As New OleDbCommand("select max(bno) from purchase", cn)
        cmd6.ExecuteNonQuery()

        Dim dr4 As OleDbDataReader
        dr4 = cmd6.ExecuteReader
        If dr4.Read Then
            If Val(dr4(0)) > 0 Then
                Dim b_no As Double
                b_no = Val(dr4(0))
                bnTextBox.Text = (b_no + 1)

            ElseIf (Convert.IsDBNull(Val(dr4(0)))) Then
                bnTextBox.Text = "1"

            Else
                bnTextBox.Text = "1"
            End If
        End If


        cn.Close()


    End Sub

    Private Sub ComboBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            cn.Open()
            Dim cmd4 As New OleDbCommand("select * from stock where itemname='" & ComboBox1.Text & "'", cn)
            cmd4.ExecuteNonQuery()
            Dim dr2 As OleDbDataReader
            dr2 = cmd4.ExecuteReader
            If dr2.Read() Then
                TextBox8.Text = dr2("sellrate")
                TextBox5.Text = dr2("itemquan")
                TextBox12.Text = dr2("itemid")
            Else
                MsgBox("select correct item name")
            End If

            cn.Close()
        End If

    End Sub



    Private Sub ComboBox1_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectionChangeCommitted
        cn.Open()
        Dim cmd4 As New OleDbCommand("select * from stock where itemname='" & ComboBox1.Text & "'", cn)
        cmd4.ExecuteNonQuery()
        Dim dr2 As OleDbDataReader
        dr2 = cmd4.ExecuteReader
        If dr2.Read() Then
            TextBox8.Text = dr2("sellrate")
            TextBox5.Text = dr2("itemquan")
            TextBox12.Text = dr2("itemid")
        Else
            MsgBox("select correct item name")
        End If

        cn.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim ad55 As Integer = DataGridView1.Rows.Add()
        DataGridView1.Rows.Item(ad55).Cells("column1").Value = TextBox12.Text
        DataGridView1.Rows.Item(ad55).Cells("column2").Value = ComboBox1.Text
        DataGridView1.Rows.Item(ad55).Cells("column3").Value = TextBox8.Text
        DataGridView1.Rows.Item(ad55).Cells("column4").Value = TextBox6.Text
        DataGridView1.Rows.Item(ad55).Cells("column5").Value = TextBox11.Text
        DataGridView1.CurrentCell = DataGridView1(0, DataGridView1.Rows.Count - 1)
        Dim total As Decimal = 0
        For Each row As DataGridViewRow In DataGridView1.Rows
            total += row.Cells("column5").Value
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

  
    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then

            cn.Open()
            Dim cmd1 As New OleDbCommand("select * from customer where cid='" & TextBox1.Text & "'", cn)
            cmd1.ExecuteNonQuery()
            Dim dr As OleDbDataReader
            dr = cmd1.ExecuteReader
            If dr.Read() Then
                TextBox2.Text = dr("cname")
                TextBox3.Text = dr("no_visi")
            Else
                MsgBox("enter correct data")
            End If

            cn.Close()

           
        End If

    End Sub

    Private Sub TextBox6_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox6.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Val(TextBox6.Text) <= Val(TextBox5.Text) Then
                TextBox11.Text = Val(TextBox6.Text) * Val(TextBox8.Text)
            Else
                MsgBox("out of stock")
            End If
        End If
    End Sub

   
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        For Each row As DataGridViewRow In DataGridView1.SelectedRows
            DataGridView1.Rows.Remove(row)
        Next

        Dim total As Decimal = 0
        For Each row As DataGridViewRow In DataGridView1.Rows
            total += row.Cells("column5").Value
        Next
        TextBox7.Text = total.ToString("0.00")
        TextBox4.Text = Val(TextBox7.Text) * 0.28


        If Val(TextBox3.Text) > 4 Then
            TextBox9.Text = ((Val(TextBox7.Text) + Val(TextBox4.Text)) * 0.04).ToString("0.00")
        Else
            TextBox9.Text = ("0")
        End If

        TextBox10.Text = (Val(TextBox7.Text) + Val(TextBox4.Text) - Val(TextBox9.Text)).ToString("0.00")
    End Sub

    
   
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click


        cn.Open()
        Dim cmd5 As New OleDbCommand("insert into purchase(bno,bdate,cid,total,gst,discount,gtotal) values (" & bnTextBox.Text & ",'" & Labeldate.Text & "','" & TextBox1.Text & "'," & TextBox7.Text & "," & TextBox4.Text & "," & TextBox9.Text & "," & TextBox10.Text & ")", cn)
        cmd5.ExecuteNonQuery()

        cn.Close()

        For x As Integer = 0 To DataGridView1.Rows.Count - 1


            Dim cmd7 As New OleDbCommand("insert into bill(bdate,bno,cid,cname,itemno,itemname,itemquan,itemrate) values ('" & Labeldate.Text & "','" & bnTextBox.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DataGridView1.Rows(x).Cells(0).Value & "','" & DataGridView1.Rows(x).Cells(1).Value & "','" & DataGridView1.Rows(x).Cells(3).Value & "','" & DataGridView1.Rows(x).Cells(4).Value & "')", cn)


            cn.Open()
            cmd7.ExecuteNonQuery()
            cn.Close()
        Next
        MsgBox("added")


       


        cn.Open()
        Dim cmd10 As New OleDbCommand("update customer set no_visi='" & TextBox3.Text + 1 & "' where cname='" & TextBox2.Text & "'", cn)
        cmd10.ExecuteNonQuery()

        Dim dr10 As OleDbDataReader
        dr10 = cmd10.ExecuteReader

        If dr10.Read() Then
            TextBox3.Text = Val(dr10(0))

        End If

        cn.Close()

        For y As Integer = 0 To DataGridView1.Rows.Count - 1
            cn.Open()
            Dim cmd8 As New OleDbCommand("select itemquan from stock where itemname='" & DataGridView1.Rows(y).Cells(1).Value & "'", cn)
            cmd8.ExecuteNonQuery()
            Dim dr6 As OleDbDataReader
            dr6 = cmd8.ExecuteReader
            dr6.Read()

            Dim cquan As Double
            cquan = Val(dr6(0)) - DataGridView1.Rows(y).Cells(3).Value()



            Dim cmd9 As New OleDbCommand("update stock set itemquan='" & Val(cquan) & "' where itemname='" & DataGridView1.Rows(y).Cells(1).Value & "'", cn)
            cmd9.ExecuteNonQuery()
            cn.Close()
        Next



    End Sub



    Private Sub PD_BeginPrint(ByVal sender As Object, ByVal e As PrintEventArgs) Handles PD.BeginPrint
        Dim pagesetup As New PageSettings
        pagesetup.PaperSize = New PaperSize("Custom", 250, 500) 'fixed size
        'pagesetup.PaperSize = New PaperSize("Custom", 250, longpaper)
        PD.DefaultPageSettings = pagesetup
    End Sub

    Private Sub PD_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs) Handles PD.PrintPage
        Dim f6 As New Font("Calibri", 6, FontStyle.Regular)
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
        e.Graphics.DrawString("" & bnTextBox.Text & "", f8, Brushes.Black, 70, 75)

        e.Graphics.DrawString("Customer ID", f8, Brushes.Black, 100, 75)
        e.Graphics.DrawString(":", f8, Brushes.Black, 170, 75)
        e.Graphics.DrawString("" & TextBox1.Text & "", f8, Brushes.Black, 175, 75)

        e.Graphics.DrawString("Cashier", f8, Brushes.Black, 0, 85)
        e.Graphics.DrawString(":", f8, Brushes.Black, 50, 85)
        e.Graphics.DrawString("ABI", f8, Brushes.Black, 70, 85)

        e.Graphics.DrawString("Customer Name", f8, Brushes.Black, 100, 85)
        e.Graphics.DrawString(":", f8, Brushes.Black, 180, 85)
        e.Graphics.DrawString("" & TextBox2.Text & "", f8, Brushes.Black, 185, 85)

        e.Graphics.DrawString("" & Labeldate.Text & "", f8, Brushes.Black, 0, 95)
        'DetailHeader
        e.Graphics.DrawString("Item", f8, Brushes.Black, 0, 110)
        e.Graphics.DrawString("Price", f8, Brushes.Black, 100, 110)
        e.Graphics.DrawString("Qty", f8, Brushes.Black, 180, 110, right)
        e.Graphics.DrawString("Total", f8, Brushes.Black, rightmargin, 110, right)
        '
        e.Graphics.DrawString(line, f8, Brushes.Black, 0, 120)

        Dim height As Integer 'DGV Position
        Dim i As Long
        Dim j As Long
        DataGridView1.AllowUserToAddRows = False
        'If DataGridView1.CurrentCell.Value Is Nothing Then
        '    Exit Sub
        'Else
        For row As Integer = 0 To DataGridView1.RowCount - 1
            height += 15
            e.Graphics.DrawString(DataGridView1.Rows(row).Cells(1).Value.ToString, f8, Brushes.Black, 0, 115 + height)
            e.Graphics.DrawString(DataGridView1.Rows(row).Cells(2).Value.ToString, f8, Brushes.Black, 100, 115 + height)
            i = DataGridView1.Rows(row).Cells(2).Value
            DataGridView1.Rows(row).Cells(2).Value = Format(i, "##,##0")
            e.Graphics.DrawString(DataGridView1.Rows(row).Cells(3).Value.ToString, f8, Brushes.Black, 180, 115 + height, right)
            j = DataGridView1.Rows(row).Cells(3).Value
            DataGridView1.Rows(row).Cells(3).Value = Format(j, "##,##0")
            e.Graphics.DrawString(DataGridView1.Rows(row).Cells(4).Value.ToString, f8, Brushes.Black, rightmargin, 115 + height, right)
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



    
End Class

