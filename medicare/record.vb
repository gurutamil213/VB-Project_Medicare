Imports System.IO
Imports System.Data.OleDb
Public Class record
    Dim cn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\VB-Project_Medicare\medicare.accdb")

    Private Sub record_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Form2.Visible = True
    End Sub



    Private Sub record_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'emp_count auto fetch
        cn.Open()
        Dim cmd As New OleDbCommand("select count(*) from employee ", cn)
        cmd.ExecuteNonQuery()
        Dim dr As OleDbDataReader
        dr = cmd.ExecuteReader
        If dr.Read() Then
            Label4.Text = Val(dr(0))

        End If

        cn.Close()

        'cus_count auto fetch
        cn.Open()
        Dim cmd1 As New OleDbCommand("select count(*) from customer ", cn)
        cmd1.ExecuteNonQuery()
        Dim dr1 As OleDbDataReader
        dr1 = cmd1.ExecuteReader
        If dr1.Read() Then
            Label5.Text = Val(dr1(0))

        End If

        cn.Close()

        'items_count auto fetch
        cn.Open()
        Dim cmd2 As New OleDbCommand("select count(*) from stock ", cn)
        cmd2.ExecuteNonQuery()
        Dim dr2 As OleDbDataReader
        dr2 = cmd2.ExecuteReader
        If dr2.Read() Then
            Label6.Text = Val(dr2(0))

        End If

        cn.Close()
    End Sub

   
End Class