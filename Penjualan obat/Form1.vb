Public Class Form1
    Dim sql As String
    Dim gam As String
    Dim PathFile As String
    Sub panggildata()
        konek()
        DA = New OleDb.OleDbDataAdapter("SELECT * from tb_kesehatan", con)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "tb_kesehatan")
        DataGridView1.DataSource = DS.Tables("tb_kesehatan")
        DataGridView1.ReadOnly = True
    End Sub
    Sub jalan()
        Dim ob As New System.Data.OleDb.OleDbCommand
        Call konek()
        ob.Connection = con
        ob.CommandType = CommandType.Text
        ob.CommandText = sql
        ob.ExecuteNonQuery()
        ob.Dispose()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        PictureBox1.ImageLocation = ""
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call panggildata()
    End Sub
    Private Sub DataGridView1_RowHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        Dim a As Integer
        a = DataGridView1.CurrentRow.Index
        If a = DataGridView1.NewRowIndex Then
            MsgBox("Data Tidak Ada!")
        Else
            TextBox1.Text = DataGridView1.Item(1, a).Value
            TextBox2.Text = DataGridView1.Item(2, a).Value
            TextBox3.Text = DataGridView1.Item(3, a).Value
            TextBox4.Text = DataGridView1.Item(4, a).Value
            TextBox6.Text = DataGridView1.Item(5, a).Value
            Dim objcmd As New System.Data.OleDb.OleDbCommand
            Call konek()
            objcmd.Connection = con
            objcmd.CommandType = CommandType.Text
            objcmd.CommandText = "select * from tb_kesehatan where kode_obat='" & TextBox1.Text & "'"
            RD = objcmd.ExecuteReader()
            RD.Read()
            If RD.HasRows Then
                PictureBox1.ImageLocation = RD.Item("foto")
                PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
            End If
            Button1.Enabled = False
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
            MsgBox("Data Belum Terisi Dengan Lengkap!")
        Else
            sql = "insert into tb_kesehatan(kode_obat,nama_obat,fungsi,harga,foto)values('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & TextBox6.Text & "' )"
            Call jalan()
            MsgBox("Data Berhasil Tersimpan")
            Call panggildata()
            DataGridView1.Refresh()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        sql = "DELETE from tb_kesehatan where kode_obat='" & TextBox1.Text & "'"
        Call jalan()
        MsgBox("Data Dihapus!")
        Call panggildata()
        Button1.Enabled = True
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        sql = "UPDATE tb_kesehatan set kode_obat='" & TextBox1.Text & "',nama_obat='" & TextBox2.Text & "',fungsi='" & TextBox3.Text & "',harga ='" & TextBox4.Text & "',foto='" & TextBox6.Text & "' where kode_obat='" & TextBox1.Text & "'"
        Call jalan()
        MsgBox("Data Telah Diperbarui!")
        Call panggildata()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim kaluar As String
        kaluar = MsgBox("Apakah Yakin Mau keluar?", vbYesNo, "toko obat serbaguna")
        If kaluar = vbYes Then
            End
        End If
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        konek()
        DA = New OleDb.OleDbDataAdapter("SELECT * from tb_kesehatan where nama_obat Like '%" & TextBox5.Text & "%'", con)
        DS = New DataSet
        DA.Fill(DS, "tb_kesehatan")
        DataGridView1.DataSource = DS.Tables("tb_kesehatan")
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label6.Text = TimeOfDay
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        OpenFileDialog1.Filter = "JPG Files(*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|GIF Files(*.gif)|*.gif|PNG Files(*.png)|*.png|BMP Files(*.bmp)|*.bmp|TIFF Files(*.tiff)|*.tiff"
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
            PictureBox1.Image = New Bitmap(OpenFileDialog1.FileName)
            Button5.Enabled = True
            PathFile = OpenFileDialog1.FileName
            TextBox6.Text = PathFile.Substring(PathFile.LastIndexOf("\") + 1)
            TextBox6.Text = OpenFileDialog1.FileName
            gam = OpenFileDialog1.FileName
            PictureBox1.Image = Image.FromFile(TextBox6.Text)
        End If
    End Sub
End Class
