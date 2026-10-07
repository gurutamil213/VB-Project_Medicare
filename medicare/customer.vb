Imports System.IO
Imports System.Data.OleDb
Public Class customer
    Dim cn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\VB-Project_Medicare\medicare.accdb")

    Private Sub customer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        MaximizeBox = False


        cn.Open()
        Dim cmd2 As New OleDbCommand("select * from customer", cn)
        cmd2.ExecuteNonQuery()
        Dim da As New OleDbDataAdapter(cmd2)
        Dim dt As New DataTable
        da.Fill(dt)
        DataGridView1.DataSource = dt
        cn.Close()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        DateTimePicker1.Text = Nothing
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        cn.Open()
        Dim cmd As New OleDbCommand("insert into customer(cid,cname,caddress,fdate,no_visi) values ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "'," & DateTimePicker1.Text & ",'" & TextBox4.Text & "')", cn)
        cmd.ExecuteNonQuery()
        MsgBox("added")
        cn.Close()

        cn.Open()
        Dim cmd2 As New OleDbCommand("select * from customer", cn)
        cmd2.ExecuteNonQuery()
        Dim da As New OleDbDataAdapter(cmd2)
        Dim dt As New DataTable
        da.Fill(dt)
        DataGridView1.DataSource = dt
        cn.Close()
    End Sub


    Private Sub customer_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Form2.Visible = True
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        cn.Open()
        Dim cmd4 As New OleDbCommand("update customer set cname='" & TextBox2.Text & "',caddress='" & TextBox3.Text & "',no_visi='" & TextBox4.Text & "',fdate='" & DateTimePicker1.Text & "' where cid='" & TextBox1.Text & "' ", cn)
        cmd4.ExecuteNonQuery()
        MsgBox("Updated")
        cn.Close()


        cn.Open()
        Dim cmd2 As New OleDbCommand("select * from customer", cn)
        cmd2.ExecuteNonQuery()
        Dim da As New OleDbDataAdapter(cmd2)
        Dim dt As New DataTable
        da.Fill(dt)
        DataGridView1.DataSource = dt
        cn.Close()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        cn.Open()
        Dim cmd1 As New OleDbCommand("delete from customer where cid ='" & TextBox1.Text & "'", cn)
        cmd1.ExecuteNonQuery()
        MsgBox("Deleted")
        cn.Close()

        cn.Open()
        Dim cmd2 As New OleDbCommand("select * from customer", cn)
        cmd2.ExecuteNonQuery()
        Dim da As New OleDbDataAdapter(cmd2)
        Dim dt As New DataTable
        da.Fill(dt)
        DataGridView1.DataSource = dt
        cn.Close()
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim index As Integer
        index = e.RowIndex
        Dim selectedrow As DataGridViewRow
        selectedrow = DataGridView1.Rows(index)
        TextBox1.Text = selectedrow.Cells(0).Value.ToString()
        TextBox2.Text = selectedrow.Cells(1).Value.ToString()
        TextBox3.Text = selectedrow.Cells(2).Value.ToString()
        TextBox4.Text = selectedrow.Cells(4).Value.ToString()
        DateTimePicker1.Text = selectedrow.Cells(3).Value.ToString()
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        Dim ch As Char = e.KeyChar
        If Not Char.IsDigit(ch) And Not Char.IsControl(ch) Then
            e.Handled = True
        End If
    End Sub

    
End Class