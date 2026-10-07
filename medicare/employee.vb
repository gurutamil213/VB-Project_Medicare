Imports System.IO
Imports System.Data.OleDb

Public Class employee

    Dim cn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\VB-Project_Medicare\medicare.accdb")

    Private Sub employee_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Form2.Visible = True
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        cn.Open()
        Dim cmd As New OleDbCommand("insert into employee(eid,ename,ephone,email,eaddress,esalary,jdate) values ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "'," & DateTimePicker1.Text & ")", cn)
        cmd.ExecuteNonQuery()
        MsgBox("added")
        cn.Close()

        cn.Open()
        Dim cmd2 As New OleDbCommand("select * from employee", cn)
        cmd2.ExecuteNonQuery()
        Dim da As New OleDbDataAdapter(cmd2)
        Dim dt As New DataTable
        da.Fill(dt)
        DataGridView1.DataSource = dt
        cn.Close()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        cn.Open()
        Dim cmd1 As New OleDbCommand("delete from employee where eid ='" & TextBox1.Text & "'", cn)
        cmd1.ExecuteNonQuery()
        MsgBox("deleted")
        cn.Close()

        cn.Open()
        Dim cmd2 As New OleDbCommand("select * from employee", cn)
        cmd2.ExecuteNonQuery()
        Dim da As New OleDbDataAdapter(cmd2)
        Dim dt As New DataTable
        da.Fill(dt)
        DataGridView1.DataSource = dt
        cn.Close()
    End Sub

    Private Sub employee_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cn.Open()
        Dim cmd2 As New OleDbCommand("select * from employee", cn)
        cmd2.ExecuteNonQuery()
        Dim da As New OleDbDataAdapter(cmd2)
        Dim dt As New DataTable
        da.Fill(dt)
        DataGridView1.DataSource = dt
        cn.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        cn.Open()
        Dim cmd4 As New OleDbCommand("update employee set ename='" & TextBox2.Text & "',ephone='" & TextBox3.Text & "',email='" & TextBox4.Text & "',eaddress='" & TextBox5.Text & "',esalary='" & TextBox6.Text & "',jdate='" & DateTimePicker1.Text & "' where eid='" & TextBox1.Text & "'", cn)
        cmd4.ExecuteNonQuery()
        MsgBox("Updated")
        cn.Close()


        cn.Open()
        Dim cmd2 As New OleDbCommand("select * from employee", cn)
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
        TextBox5.Text = selectedrow.Cells(3).Value.ToString()
        TextBox6.Text = selectedrow.Cells(5).Value.ToString()
        DateTimePicker1.Text = selectedrow.Cells(6).Value.ToString()
        
    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox6.KeyPress, TextBox3.KeyPress, TextBox1.KeyPress
        Dim ch As Char = e.KeyChar
        If Not Char.IsDigit(ch) And Not Char.IsControl(ch) Then
            e.Handled = True
        End If
    End Sub
End Class