Imports System.Data.SqlClient

Module ModuleKoneksi
    Public Conn As SqlConnection
    Public cmd As SqlCommand
    Public Da As SqlDataAdapter
    Public Dt As DataTable
    Public Dr As SqlDataReader
    Public Akun As SqlDataReader

    Public Sub Koneksi()
        Conn = New SqlConnection("Data Source=YOUR_PC\SQLEXPRESS;Initial Catalog=kasir_lks;Integrated Security=True")
        If Conn.State = ConnectionState.Closed Then Conn.Open()
    End Sub

    Public Sub SqlQuery(ByVal sql As String)
        Dr.Close()
        Call Koneksi()
        cmd = New SqlCommand(sql, Conn)
        cmd.ExecuteNonQuery()
    End Sub

    Public Sub TableLoad(ByVal sql As String, ByVal table As Object)

        Call Koneksi()
        Da = New SqlDataAdapter(sql, Conn)
        Dt = New DataTable
        Da.Fill(Dt)
        table.DataSource = Dt
    End Sub

End Module

