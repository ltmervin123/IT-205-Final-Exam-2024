Imports MySql.Data.MySqlClient
Imports System.Data.SqlClient
Module Module1
    Public con, MysqlConn1 As New MySqlConnection
    Public cmd, MysqlComm1 As New MySqlCommand
    Public dataReader, MysqlReader1 As MySqlDataReader
    Public dataTable, MysqlTable1 As New DataTable
    Public sql, sql1 As String
    Public dataAdapter, da1 As New MySqlDataAdapter
    Public dataSet, ds1 As New DataSet

    Public gintUserID, gintUnitPrice, gintISBN As Integer
    Public gstrUserType, gstrTitle, gintDiscountedAmount As String
    Public gboolOnOff As Boolean

    Public pintbookAdd, pintbookEdit, pintbookDelete As Integer
    Public pintborrowerAdd, pintborrowerEdit, pintborrowerDelete As Integer
    Public pintuserAdd, pintuserEdit, pintuserDelete As Integer
    Public pintTransactions, pintReports, pintUtilities As Integer

    Public Sub connection()

        '*** Connection to answer ***
        Dim connectionURL As String = "server=localhost;port=3306;username=root;password=;database=final_exam"
        con.ConnectionString = connectionURL
        Try
            con.Open()
        Catch ex As Exception
            MsgBox("Error in connection. The error is" & ex.Message)
            Exit Sub
        End Try
    End Sub
End Module
