Imports System.Data.Common
Imports MySql.Data.MySqlClient
Public Class Form1
    Dim mintISBN, intRemainingNoOfCopies As Integer

    Private Sub frmBooks_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.AcceptButton = btnAdd
        Me.CancelButton = btnExit
        Call clear()
        ' Call connection()
        Call showlistview()
        Call textboxes(False, False, False, False, False, False, False, False)
        Call buttons(True, False, False, False, False, False, True)
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Call clear()
        Me.AcceptButton = btnSave
        Me.CancelButton = btnCancel
        Call textboxes(True, True, True, True, True, True, True, True)
        Call buttons(False, True, False, False, False, True, False)
        txtISBN.Focus()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim intRemainingNoOfCopies As Integer
        mintISBN = Val(txtISBN.Text)
        dtpDatePublished.CustomFormat = "yyyy/MM/dd"
        If txtISBN.Text = "" Or txtBookTitle.Text = "" Or txtAuthor.Text = "" Or txtPublisher.Text = "" Or dtpDatePublished.Text = "" Or cboCategory.Text = "" Or txtNoOfCopies.Text = "" Then
            MsgBox("Please fill up all the fields.")
            Exit Sub
        End If

        Try
            connection()
            Dim querys As String = "select * from books where ISBN = @ISBN"
            cmd.Connection = con
            cmd.CommandText = querys
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@ISBN", txtISBN.Text)
            dataAdapter.SelectCommand = cmd
            dataTable.Clear()
            dataAdapter.Fill(dataTable)
            If dataTable.Rows.Count > 0 Then
                mintISBN = dataTable.Rows(0).Item(0)
                If mintISBN = txtISBN.Text Then
                    MsgBox("ISBN already exists!")
                    mintISBN = 0
                    txtISBN.Focus()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Checking ISBN error : " + ex.Message)
        Finally
            con.Close()
        End Try
        intRemainingNoOfCopies = txtNoOfCopies.Text

        '*** SQL to answer ***
        Try
            connection()
            Dim query As String = "INSERT INTO books VALUES (@ISBN, @bookTitle, @author, @publisher, @datePublished, @category, @numberOfCopies, @remainingNumberOfCopies)"
            cmd.Connection = con
            cmd.CommandText = query
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@ISBN", mintISBN)
            cmd.Parameters.AddWithValue("@bookTitle", txtBookTitle.Text)
            cmd.Parameters.AddWithValue("@author", txtAuthor.Text)
            cmd.Parameters.AddWithValue("@publisher", txtPublisher.Text)
            cmd.Parameters.AddWithValue("@datePublished", dtpDatePublished.Value.ToString("yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("@category", cboCategory.Text)
            cmd.Parameters.AddWithValue("@numberOfCopies", txtNoOfCopies.Text)
            cmd.Parameters.AddWithValue("@remainingNumberOfCopies", intRemainingNoOfCopies)
            Dim i As Integer = cmd.ExecuteNonQuery()
            MsgBox("Successfully saved.")


        Catch ex As Exception
            MessageBox.Show("Something went wrong durring saving ERROR : " + ex.Message)
        Finally
            con.Close()
            If MsgBox("Do you want to add another?", vbYesNo, "Confirm") = vbYes Then
                txtISBN.Focus()
                Call clear()
                Call buttons(False, True, False, False, False, True, False)
                Call textboxes(True, True, True, True, True, True, True, True)
                txtISBN.Focus()
                Me.AcceptButton = btnSave
                Me.CancelButton = btnCancel
            Else
                Call clear()
                Call buttons(True, False, False, False, False, False, True)
                Call textboxes(False, False, False, False, False, False, False, False)
                Me.AcceptButton = btnAdd
                Me.CancelButton = btnExit
            End If
            Call showlistview()
        End Try
        ' Call TryCatch()


    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Me.AcceptButton = btnUpdate
        Me.CancelButton = btnCancel
        Call textboxes(True, True, True, True, True, True, True, True)
        Call buttons(False, False, False, True, False, True, False)
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click

        ' *** SQL to answer ***
        Try
            connection()
            sql = "UPDATE books SET title = @title, author = @author, publisher = @publisher, datePublished = @datePublished, category = @category, numberOfCopies = @numberOfCopies, remainingNumberOfCopies = @remainingNumberOfCopies WHERE ISBN = @ISBN"
            cmd.Connection = con
            cmd.CommandText = sql
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@title", txtBookTitle.Text)
            cmd.Parameters.AddWithValue("@author", txtAuthor.Text)
            cmd.Parameters.AddWithValue("@publisher", txtPublisher.Text)
            cmd.Parameters.AddWithValue("@datePublished", dtpDatePublished.Value.ToString("yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("@category", cboCategory.Text)
            cmd.Parameters.AddWithValue("@numberOfCopies", txtNoOfCopies.Text)
            cmd.Parameters.AddWithValue("@remainingNumberOfCopies", lblRemainingNoOfCopies.Text)
            cmd.Parameters.AddWithValue("@ISBN", txtISBN.Text)
            cmd.ExecuteNonQuery()
            'Call TryCatch()
            MsgBox("Successfully Updated.")
        Catch ex As Exception
            MessageBox.Show("An Error Occured During Updating : " + ex.Message)
        Finally
            Me.AcceptButton = btnAdd
            Me.CancelButton = btnExit
            Call clear()
            Call textboxes(False, False, False, False, False, False, False, False)
            Call buttons(True, False, False, False, False, False, True)
            con.Close()
            Call showlistview()
        End Try

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        mintISBN = Val(txtISBN.Text)
        If MsgBox("Do you want to Delete this Data?", vbYesNo, "Confirm") = vbYes Then
            MsgBox("Successfully Deleted.")

            ' *** SQL to answer ***
            Try
                connection()
                sql = "DELETE FROM books WHERE ISBN = @ISBN"
                cmd.Connection = con
                cmd.CommandText = sql
                cmd.Parameters.Clear()
                cmd.Parameters.AddWithValue("@ISBN", mintISBN)
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MessageBox.Show("An Error Occured During Deletion Error : " + ex.Message)
            Finally
                con.Close()
            End Try
            ' Call TryCatch()
        Else
            Me.Visible = True
        End If
        If ListView1.Enabled = False Then
            ListView1.Enabled = True
        End If
        Me.AcceptButton = btnAdd
        Me.CancelButton = btnExit
        Call showlistview()
        Call clear()
        Call buttons(True, False, False, False, False, False, True)
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Call clear()
        Me.AcceptButton = btnAdd
        Me.CancelButton = btnExit
        Call textboxes(False, False, False, False, False, False, False, False)
        Call buttons(True, False, False, False, False, False, True)
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub showlistview()

        ' dataSet = New DataSet
        'dataAdapter = New MySql.Data.MySqlClient.MySqlDataAdapter("select * from books", con)
        'dataAdapter.Fill(dataSet, "books")
        Try
            connection()
            Dim query As String = "SELECT * FROM books"
            cmd.Connection = con
            cmd.CommandText = query
            dataSet.Clear()
            dataAdapter.SelectCommand = cmd
            dataAdapter.Fill(dataSet, "books")
            Me.ListView1.Items.Clear()
            If dataSet.Tables("books").Rows.Count > 0 Then
                For i As Integer = 0 To dataSet.Tables("books").Rows.Count - 1
                    With ListView1.Items.Add(dataSet.Tables("books").Rows(i).Item(0).ToString)
                        .SubItems.Add(dataSet.Tables("books").Rows(i).Item(1).ToString)
                        .SubItems.Add(dataSet.Tables("books").Rows(i).Item(2).ToString)
                        .SubItems.Add(dataSet.Tables("books").Rows(i).Item(3).ToString)
                        .SubItems.Add(dataSet.Tables("books").Rows(i).Item(4).ToString)
                        .SubItems.Add(dataSet.Tables("books").Rows(i).Item(5).ToString)
                        .SubItems.Add(dataSet.Tables("books").Rows(i).Item(6).ToString)
                        .SubItems.Add(dataSet.Tables("books").Rows(i).Item(7).ToString)
                    End With
                    lblRecCount.Text = dataSet.Tables("books").Rows.Count
                Next
            Else
                lblRecCount.Text = 0
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con.Close()
        End Try

    End Sub

    Private Sub ListView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.Click
        txtISBN.Text = ListView1.SelectedItems(0).SubItems(0).Text
        txtBookTitle.Text = ListView1.SelectedItems(0).SubItems(1).Text
        txtAuthor.Text = ListView1.SelectedItems(0).SubItems(2).Text
        txtPublisher.Text = ListView1.SelectedItems(0).SubItems(3).Text
        dtpDatePublished.Text = ListView1.SelectedItems(0).SubItems(4).Text
        cboCategory.Text = ListView1.SelectedItems(0).SubItems(5).Text
        txtNoOfCopies.Text = ListView1.SelectedItems(0).SubItems(6).Text
        lblRemainingNoOfCopies.Text = ListView1.SelectedItems(0).SubItems(7).Text
        Me.AcceptButton = btnAdd
        Me.CancelButton = btnExit
        Call textboxes(False, False, False, False, False, False, False, False)
        Call buttons(True, False, True, False, True, False, True)
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        ListView1.Items.Clear()
        If cboSearch.Text = "" Then
            MsgBox("Choose from Seach Category first.")
            cboSearch.Focus()
            Exit Sub
        End If
        If cboSearch.Text = "ISBN" Then

            ' *** SQL to answer ***
            sql = "Select * from books where ISBN like '" & txtISBN.Text & "%'"
        ElseIf cboSearch.Text = "Book Title" Then
            sql = "Select * from books where title like '" & txtSearch.Text & "%'"
        ElseIf cboSearch.Text = "Author" Then
            sql = "Select * from books where author like '" & txtSearch.Text & "%'"
        ElseIf cboSearch.Text = "Publisher" Then
            sql = "Select * from books where publisher like '" & txtSearch.Text & "%'"
        ElseIf cboSearch.Text = "Date Published" Then
            sql = "Select * from books where datePublished like '" & txtSearch.Text & "%'"
        ElseIf cboSearch.Text = "No. Of Copies" Then
            sql = "Select * from books where numberOfCopies like '" & txtNoOfCopies.Text & "%'"
        ElseIf cboSearch.Text = "Category" Then
            sql = "Select * from books where category like '" & txtSearch.Text & "%'"
        End If
        cmd.Connection = con
        cmd.CommandText = sql
        dataSet.Clear()
        dataAdapter.SelectCommand = cmd
        dataAdapter.Fill(dataSet, "books")
        Me.ListView1.Items.Clear()

        Me.ListView1.Items.Clear()
        If dataSet.Tables("books").Rows.Count > 0 Then
            For i As Integer = 0 To dataSet.Tables("books").Rows.Count - 1
                With ListView1.Items.Add(dataSet.Tables("books").Rows(i).Item(0).ToString)
                    .SubItems.Add(dataSet.Tables("books").Rows(i).Item(1).ToString)
                    .SubItems.Add(dataSet.Tables("books").Rows(i).Item(2).ToString)
                    .SubItems.Add(dataSet.Tables("books").Rows(i).Item(3).ToString)
                    .SubItems.Add(dataSet.Tables("books").Rows(i).Item(4).ToString)
                    .SubItems.Add(dataSet.Tables("books").Rows(i).Item(5).ToString)
                    .SubItems.Add(dataSet.Tables("books").Rows(i).Item(6).ToString)
                    .SubItems.Add(dataSet.Tables("books").Rows(i).Item(7).ToString)
                End With
                lblRecCount.Text = dataSet.Tables("books").Rows.Count
            Next
        Else
            lblRecCount.Text = 0
        End If
    End Sub

    Private Sub clear()
        txtISBN.Text = ""
        txtBookTitle.Text = ""
        txtAuthor.Text = ""
        txtPublisher.Text = ""
        txtNoOfCopies.Text = ""
        cboCategory.Text = ""
        lblRemainingNoOfCopies.Text = ""
    End Sub

    Private Sub textboxes(ByVal rISBN As Boolean, ByVal rBookTitle As Boolean, ByVal rAuthor As Boolean, ByVal rPublisher As Boolean, ByVal rDatePublished As Boolean, ByVal rNoOfCopies As Boolean, ByVal rCategory As Boolean, ByVal rRemainingCopies As Boolean)
        txtISBN.Enabled = rISBN
        txtBookTitle.Enabled = rBookTitle
        txtAuthor.Enabled = rAuthor
        txtPublisher.Enabled = rPublisher
        dtpDatePublished.Enabled = rDatePublished
        txtNoOfCopies.Enabled = rNoOfCopies
        cboCategory.Enabled = rCategory
        lblRemainingNoOfCopies.Enabled = rRemainingCopies
    End Sub

    Private Sub buttons(ByVal rAdd As Boolean, ByVal rSave As Boolean, ByVal rEdit As Boolean, ByVal rUpdate As Boolean, ByVal rDelete As Boolean, ByVal rCancel As Boolean, ByVal rExit As Boolean)
        btnAdd.Enabled = rAdd
        btnSave.Enabled = rSave
        btnEdit.Enabled = rEdit
        btnUpdate.Enabled = rUpdate
        btnDelete.Enabled = rDelete
        btnCancel.Enabled = rCancel
        btnExit.Enabled = rExit
    End Sub

    Private Sub TryCatch()
        Try
            cmd = New MySql.Data.MySqlClient.MySqlCommand(sql, con)
            cmd.ExecuteNonQuery()
            ' dataReader = cmd.ExecuteReader()
            dataReader.Close()
            Call connection()
        Catch ex As Exception
            MsgBox("Error in saving database. Error is :" & ex.Message)
            dataReader.Close()
            Exit Sub
        End Try
    End Sub
End Class
