Imports System.IO
Imports System.Data.OleDb

Public Class stock
    Dim cn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\VB-Project_Medicare\medicare.accdb")
    

    Private Sub stock_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Form2.Show()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        cn.Open()
        Dim cmd As New OleDbCommand("insert into stock(itemid,itemname,itemquan,itemrate,sellrate,idate,mdate,xdate) values ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & TextBox5.Text & "'," & DateTimePicker1.Text & "," & DateTimePicker2.Text & "," & DateTimePicker3.Text & ")", cn)
        cmd.ExecuteNonQuery()
        MsgBox("added")
        cn.Close()

        cn.Open()
        Dim cmd2 As New OleDbCommand("select * from stock", cn)
        cmd2.ExecuteNonQuery()
        Dim da As New OleDbDataAdapter(cmd2)
        Dim dt As New DataTable
        da.Fill(dt)
        DataGridView1.DataSource = dt
        cn.Close()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        cn.Open()
        Dim cmd1 As New OleDbCommand("delete from stock where itemid ='" & TextBox1.Text & "'", cn)
        cmd1.ExecuteNonQuery()
        MsgBox("deleted")
        cn.Close()

        cn.Open()
        Dim cmd2 As New OleDbCommand("select * from stock", cn)
        cmd2.ExecuteNonQuery()
        Dim da As New OleDbDataAdapter(cmd2)
        Dim dt As New DataTable
        da.Fill(dt)
        DataGridView1.DataSource = dt
        cn.Close()
    End Sub

    Private Sub stock_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        
        cn.Open()
        Dim cmd2 As New OleDbCommand("select * from stock", cn)
        cmd2.ExecuteNonQuery()
        Dim da As New OleDbDataAdapter(cmd2)
        Dim dt As New DataTable
        da.Fill(dt)
        DataGridView1.DataSource = dt
        cn.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        cn.Open()
        Dim cmd4 As New OleDbCommand("update stock set itemname='" & TextBox2.Text & "',itemquan='" & TextBox3.Text & "',itemrate='" & TextBox4.Text & "',sellrate='" & TextBox5.Text & "',idate='" & DateTimePicker1.Text & "', mdate='" & DateTimePicker2.Text & "',xdate='" & DateTimePicker3.Text & "' where itemid='" & TextBox1.Text & "' ", cn)
        cmd4.ExecuteNonQuery()
        MsgBox("updated")
        cn.Close()
      

        cn.Open()
        Dim cmd2 As New OleDbCommand("select * from stock", cn)
        cmd2.ExecuteNonQuery()
        Dim da As New OleDbDataAdapter(cmd2)
        Dim dt As New DataTable
        da.Fill(dt)
        DataGridView1.DataSource = dt
        cn.Close()
    End Sub



    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim index As Integer
        index = e.RowIndex
        Dim selectedrow As DataGridViewRow
        selectedrow = DataGridView1.Rows(index)
        TextBox1.Text = selectedrow.Cells(0).Value.ToString()
        TextBox2.Text = selectedrow.Cells(1).Value.ToString()
        TextBox3.Text = selectedrow.Cells(2).Value.ToString()
        TextBox4.Text = selectedrow.Cells(3).Value.ToString()
        TextBox5.Text = selectedrow.Cells(4).Value.ToString()
        DateTimePicker1.Text = selectedrow.Cells(5).Value.ToString()
        DateTimePicker2.Text = selectedrow.Cells(6).Value.ToString()
        DateTimePicker3.Text = selectedrow.Cells(7).Value.ToString()
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress, TextBox1.KeyPress
        Dim ch As Char = e.KeyChar
        If Not Char.IsDigit(ch) And Not Char.IsControl(ch) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox4_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress, TextBox4.KeyPress
        Dim ch As Char = e.KeyChar
        If Not Char.IsDigit(ch) And Not Char.IsControl(ch) AndAlso ch <> "." Then
            e.Handled = True
        End If
    End Sub
End Class
