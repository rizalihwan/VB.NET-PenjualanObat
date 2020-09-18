Imports System.Data
Imports System.Data.OleDb
Module Module1
    Public con As OleDbConnection
    Public CMD As OleDbCommand
    Public DS As New DataSet
    Public DA As OleDbDataAdapter
    Public RD As OleDbDataReader
    Public lokasidata As String
    Public Sub konek()
        lokasidata = "provider=microsoft.jet.oledb.4.0;data source=obat.mdb"
        con = New OleDbConnection(lokasidata)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
    End Sub
End Module