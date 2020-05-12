Imports MySql.Data.MySqlClient
'Leonardo Sellanes
Public Class Form1
    Dim conexion As MySqlConnection = New MySqlConnection
    Dim cmd As New MySqlCommand
    Sub ActualizarSelect()

        Dim ds As DataSet = New DataSet
        Dim adaptador As MySqlDataAdapter = New MySqlDataAdapter

        conexion.ConnectionString = "server=localhost; database=veterinaria; uid=root;pwd=;"

        Try
            conexion.Open()
            cmd.Connection = conexion

            cmd.CommandText = "SELECT * FROM perros ORDER BY Nombre ASC"
            adaptador.SelectCommand = cmd
            adaptador.Fill(ds, "Tabla")
            grdMascotas.DataSource = ds
            grdMascotas.DataMember = "Tabla"

            conexion.Close()


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnIngresar.Click

        If (txtNombre.Text <> "") And (txtRaza.Text <> "") And (txtColor.Text <> "") Then
            conexion.Open()
            cmd.Connection = conexion

            cmd.CommandText = "INSERT INTO perros(Nombre,Raza,Color) VALUES (@Nombre, @Raza, @Color)"
            cmd.Prepare()

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text)
            cmd.Parameters.AddWithValue("@Raza", txtRaza.Text)
            cmd.Parameters.AddWithValue("@Color", txtColor.Text)
            cmd.ExecuteNonQuery()

            conexion.Close()
            txtNombre.Clear()
            txtRaza.Clear()
            txtColor.Clear()

            ActualizarSelect()
        Else
            MsgBox("¡Debe ingresar Todos los datos Solicitados!")
        End If
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ActualizarSelect()

    End Sub

    Private Sub grdMascotas_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdMascotas.CellContentClick

    End Sub
    Private Sub grdMascotas_SelectionChanged(sender As Object, e As EventArgs) Handles grdMascotas.SelectionChanged
        If (grdMascotas.SelectedRows.Count > 0) Then
            txtNombre.Text = grdMascotas.Item("Nombre", grdMascotas.SelectedRows(0).Index).Value
            txtRaza.Text = grdMascotas.Item("Raza", grdMascotas.SelectedRows(0).Index).Value
            txtColor.Text = grdMascotas.Item("Color", grdMascotas.SelectedRows(0).Index).Value
            txtID.Text = grdMascotas.Item("id", grdMascotas.SelectedRows(0).Index).Value
        End If
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        If (txtNombre.Text <> "") And (txtRaza.Text <> "") And (txtColor.Text <> "") Then
            conexion.ConnectionString = "server=localhost; database=veterinaria;Uid=root;Pwd=;"

            Try
                conexion.Open()
                cmd.Connection = conexion

                cmd.CommandText = "UPDATE perros SET Nombre=@Nombre, Raza=@Raza, Color=@Color WHERE id=@id"
                cmd.Prepare()

                cmd.Parameters.Clear()
                cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text)
                cmd.Parameters.AddWithValue("@Raza", txtRaza.Text)
                cmd.Parameters.AddWithValue("@Color", txtColor.Text)
                cmd.Parameters.AddWithValue("@id", txtID.Text)
                cmd.ExecuteNonQuery()
                txtNombre.Clear()
                txtColor.Clear()
                txtRaza.Clear()
                txtID.Clear()

                conexion.Close()

                ActualizarSelect()

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox("¡Debe seleccionar una mascota y modificar sus datos antes de proceder!")
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        conexion.ConnectionString = "server=localhost; database=veterinaria;Uid=root;Pwd=;"
        If MessageBox.Show("¿Desea dar de Alta esta mascota?,Sera eliminado de el registro", "Alta", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then

        End If

        Try
            conexion.Open()
            cmd.Connection = conexion

            cmd.CommandText = "DELETE FROM perros WHERE id=@id"
            cmd.Prepare()

            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@id", txtID.Text)
            cmd.ExecuteNonQuery()
            txtNombre.Clear()
            txtColor.Clear()
            txtRaza.Clear()
            txtID.Clear()

            conexion.Close()

            ActualizarSelect()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If (txtNombre.Text <> "") Or (txtRaza.Text <> "") Or (txtColor.Text <> "") Then
            txtNombre.Clear()
            txtRaza.Clear()
            txtColor.Clear()
            txtID.Clear()
        Else
            MsgBox("Los campos ya se encuntran vacios")
        End If


    End Sub
End Class
