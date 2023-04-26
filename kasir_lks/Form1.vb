Imports System.Data.SqlClient

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Button Menu
        PanelMenu.Visible = False
        PanelBeranda.Visible = False
        PanelAkun.Visible = False
        PanelKasir.Visible = False
        PanelStok.Visible = False
        PanelLogin.Visible = True

        'Button Akun
        btnAkunHapus.Enabled = False
        btnAkunEdit.Enabled = False

        'Button Produk
        btnProdukEdit.Enabled = False
        btnProdukHapus.Enabled = False
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If txtUsername.Text = "" Or txtPassword.Text = "" Then
            MsgBox("Isi seluruh kolom!", MsgBoxStyle.Exclamation)
        Else
            Call Koneksi()
            cmd = New SqlCommand("SELECT * FROM users WHERE username='" & txtUsername.Text & "' AND password='" & txtPassword.Text & "'", Conn)
            Dr = cmd.ExecuteReader
            Dr.Read()

            If Dr.HasRows Then
                PanelLogin.Visible = False
                PanelMenu.Visible = True
                PanelBeranda.Visible = True

                If Dr("lvl") = "admin" Then
                    btnBeranda.Visible = True
                    btnKasir.Visible = True
                    btnStok.Visible = True
                    btnAkun.Visible = True
                End If

                If Dr("lvl") = "kasir" Then
                    btnBeranda.Visible = True
                    btnKasir.Visible = True
                    btnStok.Visible = False
                    btnAkun.Visible = False
                End If

                If Dr("lvl") = "manager" Then
                    btnBeranda.Visible = True
                    btnKasir.Visible = False
                    btnStok.Visible = True
                    btnAkun.Visible = False
                End If

                PanelBeranda.Visible = True
                txtBerandaIdAkun.Text = Dr("id_user")
                btnBeranda.PerformClick()

                txtUsername.Text = ""
                txtPassword.Text = ""
                MsgBox("Berhasil Login")
                Dr.Close()

            Else
                MsgBox("Terjadi Kesalahan saat Login", MsgBoxStyle.Exclamation)
                txtPassword.Text = ""
                txtPassword.Focus()
                Dr.Close()
            End If
        End If
    End Sub

    Private Sub btnBeranda_Click(sender As Object, e As EventArgs) Handles btnBeranda.Click
        PanelMenu.Visible = True
        PanelBeranda.Visible = True
        PanelAkun.Visible = False
        PanelKasir.Visible = False
        PanelStok.Visible = False

        PanelLogin.Visible = False

        Dr.Close()
        cmd = New SqlCommand("SELECT * FROM users WHERE id_user=" & txtBerandaIdAkun.Text, Conn)
        Dr = cmd.ExecuteReader
        Dr.Read()
        labelWelcome.Text = "Selamat Datang " & Dr("username") & " Anda login sebagai " & Dr("nama_lengkap")
        Dr.Close()

        cmd = New SqlCommand("SELECT count(id_transaksi) as 'jumlahTransaksi' FROM transaksi WHERE status=1", Conn)
        Dr = cmd.ExecuteReader
        Dr.Read()
        LabelBerandaJumlahTransaksi.Text = Dr("jumlahTransaksi")
        Dr.Close()

        TableLoad("SELECT id_transaksi as 'Id', tgl_bayar as 'Tanggal', total_harga as 'Total', bayar as 'Nominal Uang', kembalian as 'Kembalian' FROM transaksi WHERE status=1", ViewBerandaLaporan)

    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        Dim yakin As Integer = MsgBox("Anda yakin ingin Logout?", MsgBoxStyle.YesNo, "Logout")
        If yakin = MsgBoxResult.Yes Then
            PanelMenu.Visible = False
            PanelBeranda.Visible = False
            PanelAkun.Visible = False
            PanelKasir.Visible = False
            PanelStok.Visible = False

            PanelLogin.Visible = True
            MsgBox("Logout Berhasil")
        Else
            MsgBox("Logout Batal")
        End If
    End Sub

    Private Sub btnKasir_Click(sender As Object, e As EventArgs) Handles btnKasir.Click
        PanelMenu.Visible = True
        PanelBeranda.Visible = False
        PanelAkun.Visible = False
        PanelKasir.Visible = True
        PanelStok.Visible = False

        PanelLogin.Visible = False

        btnKasirRefresh.PerformClick()
    End Sub

    Private Sub btnStok_Click(sender As Object, e As EventArgs) Handles btnStok.Click
        TableLoad("SELECT kode_produk as 'Kode Produk', produk as 'Produk', jumlah as 'Jumlah', harga as 'Harga' FROM produk", viewProduk)

        PanelMenu.Visible = True
        PanelBeranda.Visible = False
        PanelAkun.Visible = False
        PanelKasir.Visible = False
        PanelStok.Visible = True

        PanelLogin.Visible = False
        btnProdukRefresh.PerformClick()
    End Sub

    Private Sub btnAkun_Click(sender As Object, e As EventArgs) Handles btnAkun.Click
        TableLoad("SELECT id_user as 'ID', nama_lengkap as 'Nama Lengkap', username as 'Username', lvl as 'Jabatan' FROM users", viewAkun)

        PanelMenu.Visible = True
        PanelBeranda.Visible = False
        PanelAkun.Visible = True
        PanelKasir.Visible = False
        PanelStok.Visible = False

        PanelLogin.Visible = False
        btnAkunRefresh.PerformClick()
    End Sub

    Private Sub viewAkun_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles viewAkun.CellContentDoubleClick
        Dim i As Integer = viewAkun.CurrentRow.Index
        txtAkunId.Text = viewAkun.Item(0, i).Value
        txtAkunNama.Text = viewAkun.Item(1, i).Value
        txtAkunUsername.Text = viewAkun.Item(2, i).Value
        txtAkunJabatan.Text = viewAkun.Item(3, i).Value

        Call Koneksi()
        cmd = New SqlCommand("SELECT * FROM users WHERE id_user=" & txtAkunId.Text, Conn)
        Dr = cmd.ExecuteReader
        Dr.Read()
        Dim passwordAkunRahasia As String = Dr("password")
        Dr.Close()

        txtAkunPassword.Text = passwordAkunRahasia

        btnAkunSimpan.Enabled = False
        btnAkunEdit.Enabled = True
        btnAkunHapus.Enabled = True
    End Sub

    Private Sub btnAkunSimpan_Click(sender As Object, e As EventArgs) Handles btnAkunSimpan.Click
        If txtAkunNama.Text = "" Or txtAkunUsername.Text = "" Or txtAkunPassword.Text = "" Or txtAkunJabatan.Text = "" Then

            txtAkunId.Text = ""
            txtAkunNama.Text = ""
            txtAkunUsername.Text = ""
            txtAkunPassword.Text = ""
            txtAkunJabatan.Text = ""

            txtAkunPassword.Enabled = True

            btnAkunEdit.Enabled = False
            btnAkunHapus.Enabled = False
            MsgBox("Isi Seluruh Kolom!")

        Else
            Call Koneksi()
            cmd = New SqlCommand("INSERT INTO users (nama_lengkap, username, password, lvl) VALUES ('" & txtAkunNama.Text & "', '" & txtAkunUsername.Text & "', '" & txtAkunPassword.Text & "', '" & txtAkunJabatan.Text & "')", Conn)
            cmd.ExecuteNonQuery()
            TableLoad("SELECT id_user as 'ID', nama_lengkap as 'Nama Lengkap', username as 'Username', lvl as 'Jabatan' FROM users", viewAkun)
            txtAkunId.Text = ""
            txtAkunNama.Text = ""
            txtAkunUsername.Text = ""
            txtAkunPassword.Text = ""
            txtAkunJabatan.Text = ""
            MsgBox("Berhasil Simpan Data")
        End If

    End Sub

    Private Sub btnAkunHapus_Click(sender As Object, e As EventArgs) Handles btnAkunHapus.Click
        Dim yakin As Integer = MsgBox("Anda yakin ingin menghapus data milik " & txtAkunNama.Text & "?", MsgBoxStyle.YesNo, "Alert")
        If yakin = MsgBoxResult.Yes Then
            If txtAkunId.Text = "" Then
                MsgBox("Pilih Akun terlebih dahulu!")
            Else
                Call Koneksi()
                cmd = New SqlCommand("DELETE FROM users WHERE id_user=" & txtAkunId.Text, Conn)
                cmd.ExecuteNonQuery()
                TableLoad("SELECT id_user as 'ID', nama_lengkap as 'Nama Lengkap', username as 'Username', lvl as 'Jabatan' FROM users", viewAkun)
                txtAkunId.Text = ""
                txtAkunNama.Text = ""
                txtAkunUsername.Text = ""
                txtAkunPassword.Text = ""
                txtAkunJabatan.Text = ""

                btnAkunSimpan.Enabled = True
                btnAkunHapus.Enabled = False
                btnAkunEdit.Enabled = False
                MsgBox("Berhasil Hapus Data")
            End If
        Else
            MsgBox("Batal Hapus")
        End If
    End Sub

    Private Sub btnAkunEdit_Click(sender As Object, e As EventArgs) Handles btnAkunEdit.Click
        Dim yakin As Integer = MsgBox("Anda yakin ingin Edit data milik " & txtAkunNama.Text & "?", MsgBoxStyle.YesNo, "Alert")
        If yakin = MsgBoxResult.Yes Then
            If txtAkunId.Text = "" Then
                MsgBox("Pilih Akun terlebih dahulu!")
            Else
                Call Koneksi()
                cmd = New SqlCommand("UPDATE users SET nama_lengkap='" & txtAkunNama.Text & "', username='" & txtAkunUsername.Text & "', password='" & txtAkunPassword.Text & "', lvl='" & txtAkunJabatan.Text & "' WHERE id_user=" & txtAkunId.Text, Conn)
                cmd.ExecuteNonQuery()
                TableLoad("SELECT id_user as 'ID', nama_lengkap as 'Nama Lengkap', username as 'Username', lvl as 'Jabatan' FROM users", viewAkun)
                txtAkunId.Text = ""
                txtAkunNama.Text = ""
                txtAkunUsername.Text = ""
                txtAkunPassword.Text = ""
                txtAkunJabatan.Text = ""

                btnAkunSimpan.Enabled = True
                btnAkunHapus.Enabled = False
                btnAkunEdit.Enabled = False
                MsgBox("Berhasil Hapus Data")
            End If
        Else
            MsgBox("Batal Hapus")
        End If
    End Sub

    Private Sub btnAkunRefresh_Click(sender As Object, e As EventArgs) Handles btnAkunRefresh.Click
        TableLoad("SELECT id_user as 'ID', nama_lengkap as 'Nama Lengkap', username as 'Username', lvl as 'Jabatan' FROM users", viewAkun)
        txtAkunId.Text = ""
        txtAkunNama.Text = ""
        txtAkunUsername.Text = ""
        txtAkunPassword.Text = ""
        txtAkunJabatan.Text = ""

        btnAkunSimpan.Enabled = True
        btnAkunHapus.Enabled = False
        btnAkunEdit.Enabled = False
    End Sub

    Private Sub btnProdukSimpan_Click(sender As Object, e As EventArgs) Handles btnProdukSimpan.Click
        If txtProdukKode.Text = "" Or txtProdukNama.Text = "" Or txtProdukJumlah.Text = "" Or txtProdukHarga.Text = "" Then

            btnProdukEdit.Enabled = False
            btnProdukHapus.Enabled = False
            MsgBox("Isi Seluruh Kolom!")

        Else
            Call Koneksi()
            cmd = New SqlCommand("SELECT * FROM produk WHERE kode_produk='" & txtProdukKode.Text & "'", Conn)
            Dr = cmd.ExecuteReader
            Dr.Read()

            If Dr.HasRows Then
                MsgBox("Kode Barang sudah dipakai", MsgBoxStyle.Exclamation)

            Else
                Call Koneksi()
                cmd = New SqlCommand("INSERT INTO produk (kode_produk, produk, jumlah, harga) VALUES ('" & txtProdukKode.Text & "', '" & txtProdukNama.Text & "', '" & txtProdukJumlah.Text & "', '" & txtProdukHarga.Text & "')", Conn)
                cmd.ExecuteNonQuery()
                TableLoad("SELECT kode_produk as 'Kode Produk', produk as 'Produk', jumlah as 'Jumlah', harga as 'Harga' FROM produk", viewProduk)

                btnProdukRefresh.PerformClick()
                MsgBox("Berhasil Simpan Data")
            End If
        End If
    End Sub

    Private Sub btnProdukEdit_Click(sender As Object, e As EventArgs) Handles btnProdukEdit.Click
        Dim yakin As Integer = MsgBox("Anda yakin ingin Edit data Produk " & txtProdukNama.Text & "?", MsgBoxStyle.YesNo, "Alert")
        If yakin = MsgBoxResult.Yes Then
            If txtProdukKode.Text = "" Then
                MsgBox("Pilih Produk terlebih dahulu!")
            Else
                Call Koneksi()
                cmd = New SqlCommand("UPDATE produk SET produk='" & txtProdukNama.Text & "', jumlah='" & txtProdukJumlah.Text & "', harga=" & txtProdukHarga.Text & " WHERE kode_produk='" & txtProdukKode.Text & "'", Conn)
                cmd.ExecuteNonQuery()
                TableLoad("SELECT kode_produk as 'Kode Produk', produk as 'Produk', jumlah as 'Jumlah', harga as 'Harga' FROM produk", viewProduk)

                btnProdukRefresh.PerformClick()
                MsgBox("Berhasil Edit Data")
            End If
        Else
            MsgBox("Batal Edit")
        End If
    End Sub

    Private Sub btnProdukHapus_Click(sender As Object, e As EventArgs) Handles btnProdukHapus.Click
        If txtProdukKode.Text = "" Then
            MsgBox("Isi Kode Produk Terlebih Dahulu")
        Else
            cmd = New SqlCommand("DELETE FROM produk WHERE kode_produk='" & txtProdukKode.Text & "'", Conn)
            cmd.ExecuteNonQuery()
            TableLoad("SELECT kode_produk as 'Kode Produk', produk as 'Produk', jumlah as 'Jumlah', harga as 'Harga' FROM produk", viewProduk)
            btnProdukRefresh.PerformClick()
            MsgBox("Berhasil Hapus Data")
        End If
    End Sub

    Private Sub btnProdukRefresh_Click(sender As Object, e As EventArgs) Handles btnProdukRefresh.Click
        TableLoad("SELECT kode_produk as 'Kode Produk', produk as 'Produk', jumlah as 'Jumlah', harga as 'Harga' FROM produk", viewProduk)

        txtProdukKode.Text = ""
        txtProdukNama.Text = ""
        txtProdukJumlah.Text = ""
        txtProdukHarga.Text = ""

        txtProdukKode.Enabled = True

        btnProdukSimpan.Enabled = True
        btnProdukEdit.Enabled = False
        btnProdukHapus.Enabled = False
    End Sub

    Private Sub viewProduk_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles viewProduk.CellContentDoubleClick
        Dim i As Integer = viewProduk.CurrentRow.Index
        txtProdukKode.Text = viewProduk.Item(0, i).Value
        txtProdukNama.Text = viewProduk.Item(1, i).Value
        txtProdukJumlah.Text = viewProduk.Item(2, i).Value
        txtProdukHarga.Text = viewProduk.Item(3, i).Value

        btnProdukSimpan.Enabled = False
        btnProdukEdit.Enabled = True
        btnProdukHapus.Enabled = True
        txtProdukKode.Enabled = False
    End Sub

    Private Sub btnKasirTambah_Click(sender As Object, e As EventArgs) Handles btnKasirTambah.Click
        If txtKasirKode.Text = "" Then
            MsgBox("Isi Kolom kode Produk!")
        Else
            Dr.Close()
            cmd = New SqlCommand("SELECT * FROM produk WHERE kode_produk='" & txtKasirKode.Text & "'", Conn)
            Dr = cmd.ExecuteReader
            Dr.Read()

            If Dr.HasRows Then
                If Dr("jumlah") < 1 Then
                    Dr.Close()
                    MsgBox("Stok tidak mencukupi")
                Else
                    Dim stokMinus As Integer = Val(Dr("jumlah")) - 1
                    cmd = New SqlCommand("UPDATE produk SET jumlah=" & stokMinus & " WHERE kode_produk='" & txtKasirKode.Text & "'", Conn)
                    Dr.Close()
                    cmd.ExecuteNonQuery()

                    cmd = New SqlCommand("INSERT INTO keranjang (kode_produk, transaksi_id) VALUES ('" & txtKasirKode.Text & "', '" & txtKasirIdTransaksi.Text & "')", Conn)
                    cmd.ExecuteNonQuery()


                    TableLoad("SELECT id_keranjang as 'Id', produk as 'Produk', harga as 'Harga'  FROM transaksi INNER JOIN keranjang ON keranjang.transaksi_id = transaksi.id_transaksi INNER JOIN produk ON keranjang.kode_produk = produk.kode_produk WHERE status=0", viewKasir)
                    btnKasirRefresh.PerformClick()

                End If

            Else
                MsgBox("Tidak ada produk dengan Kode " & txtKasirKode.Text, MsgBoxStyle.Exclamation)
                btnKasirRefresh.PerformClick()
            End If
        End If
    End Sub


    Private Sub viewKasir_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles viewKasir.CellContentDoubleClick
        Dim i As Integer = viewKasir.CurrentRow.Index

        cmd = New SqlCommand("SELECT * FROM keranjang WHERE id_keranjang=" & viewKasir.Item(0, i).Value, Conn)
        Dr = cmd.ExecuteReader
        Dr.Read()

        Dim idKeranjang As String = Dr("id_keranjang")

        txtKasirIdItem.Text = Dr("id_keranjang")
        txtKasirKode.Text = Dr("kode_produk")

        txtKasirKode.Enabled = False

        btnKasirHapus.Enabled = True
        btnKasirTambah.Enabled = False

        Dr.Close()
    End Sub

    Private Sub btnKasirHapus_Click(sender As Object, e As EventArgs) Handles btnKasirHapus.Click
        If txtKasirIdItem.Text = "" Then
            MsgBox("Pilih item terlebih dahulu")
        Else
            Dr.Close()
            cmd = New SqlCommand("DELETE FROM keranjang WHERE id_keranjang=" & txtKasirIdItem.Text, Conn)
            cmd.ExecuteNonQuery()
            btnKasirRefresh.PerformClick()
            MsgBox("Berhasil Hapus data")
        End If
    End Sub

    Private Sub btnKasirRefresh_Click(sender As Object, e As EventArgs) Handles btnKasirRefresh.Click
        TableLoad("SELECT id_keranjang as 'Id', produk as 'Produk', harga as 'Harga'  FROM transaksi INNER JOIN keranjang ON keranjang.transaksi_id = transaksi.id_transaksi INNER JOIN produk ON keranjang.kode_produk = produk.kode_produk WHERE status=0", viewKasir)

        btnKasirHapus.Enabled = False
        btnKasirTambah.Enabled = True

        txtKasirKode.Enabled = True

        txtKasirKode.Text = ""
        txtKasirIdItem.Text = ""

        cmd = New SqlCommand("SELECT * FROM transaksi WHERE status = 0", Conn)
        Dr = cmd.ExecuteReader
        Dr.Read()

        If Dr.HasRows Then
            txtKasirIdTransaksi.Text = Dr("id_transaksi")
            Dr.Close()

            cmd = New SqlCommand("SELECT sum(harga) as 'totalHarga' FROM keranjang INNER JOIN produk ON keranjang.kode_produk = produk.kode_produk WHERE transaksi_id=" & txtKasirIdTransaksi.Text, Conn)
            Dr = cmd.ExecuteReader
            Dr.Read()
            labelHargaTotal.Text = Dr("totalHarga").ToString
            Dr.Close()
        Else
            Dr.Close()

            Dim HariIni As Date = Date.Now
            cmd = New SqlCommand("INSERT INTO transaksi (tgl_transaksi, status) VALUES ('" & HariIni & "', '0')", Conn)
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("SELECT * FROM transaksi WHERE status=0", Conn)
            Dr = cmd.ExecuteReader
            Dr.Read()

            txtKasirIdTransaksi.Text = Dr("id_transaksi")
            Dr.Close()

            cmd = New SqlCommand("SELECT sum(harga) as 'totalHarga' FROM keranjang INNER JOIN produk ON keranjang.kode_produk = produk.kode_produk WHERE transaksi_id=" & txtKasirIdTransaksi.Text, Conn)
            Dr = cmd.ExecuteReader
            Dr.Read()
            labelHargaTotal.Text = Dr("totalHarga").ToString
            Dr.Close()
        End If

    End Sub

    Private Sub btnKasirBayar_Click(sender As Object, e As EventArgs) Handles btnKasirBayar.Click

        If labelHargaTotal.Text = "" Then
            MsgBox("Tidak ada produk dalam keranjang", MsgBoxStyle.Exclamation, "Alert")
            btnKasirRefresh.PerformClick()
        Else
            If Val(txtKasirUang.Text) < Val(labelHargaTotal.Text) Then
                Dr.Close()
                MsgBox("Uang tidak mencukupi")
            Else
                Dr.Close()
                Dim HariIni As String = Format(Date.Now, "yyyy-MM-dd").ToString

                cmd = New SqlCommand("UPDATE transaksi SET total_harga='" & labelHargaTotal.Text & "', bayar='" & txtKasirUang.Text & "', tgl_bayar='" & HariIni & "', status=1 WHERE id_transaksi='" & txtKasirIdTransaksi.Text & "'", Conn)
                cmd.ExecuteNonQuery()

                TableLoad("SELECT id_keranjang as 'Id', produk as 'Produk', harga as 'Harga'  FROM transaksi INNER JOIN keranjang ON keranjang.transaksi_id = transaksi.id_transaksi INNER JOIN produk ON keranjang.kode_produk = produk.kode_produk WHERE status=0", viewKasir)

                MsgBox("Pembayaran Berhasil")
                btnKasirRefresh.PerformClick()
            End If
        End If

    End Sub

    Private Sub btnNewKasir_Click(sender As Object, e As EventArgs) Handles btnNewKasir.Click
        If txtKasirIdItem.Text = "" Then
            Dim HariIni As Date = Date.Now
            cmd = New SqlCommand("INSERT INTO transaksi (tgl_transaksi, status) VALUES ('" & HariIni & "', '0')", Conn)
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("SELECT * FROM transaksi WHERE status=0", Conn)
            Dr = cmd.ExecuteReader
            Dr.Read()

            txtKasirIdTransaksi.Text = Dr("id_transaksi")
            Dr.Close()
        Else
            Dr.Close()

            Dim yakin As Integer = MsgBox("Anda ingin membuat transaksi baru?", MsgBoxStyle.YesNo.Exclamation, "Alert")
            If yakin = MsgBoxResult.Yes Then
                Dim HariIni As Date = Date.Now
                cmd = New SqlCommand("INSERT INTO transaksi (tgl_transaksi, status) VALUES ('" & HariIni & "', '0')", Conn)
                cmd.ExecuteNonQuery()
                btnKasirRefresh.PerformClick()
            Else

            End If
        End If
    End Sub

    Private Sub BtnBerandaRefresh_Click(sender As Object, e As EventArgs) Handles BtnBerandaRefresh.Click
        TableLoad("SELECT id_transaksi as 'Id', tgl_bayar as 'Tanggal', total_harga as 'Total', bayar as 'Nominal Uang', kembalian as 'Kembalian' FROM transaksi WHERE status=1", ViewBerandaLaporan)
    End Sub

    Private Sub BtnBerandaFilter_Click(sender As Object, e As EventArgs) Handles BtnBerandaFilter.Click
        TableLoad("SELECT id_transaksi as 'Id', tgl_bayar as 'Tanggal', total_harga as 'Total', bayar as 'Nominal Uang', kembalian as 'Kembalian' FROM transaksi WHERE status=1 AND tgl_bayar BETWEEN '" & DateBerandaAwal.Text & "' AND '" & DateBerandaAkhir.Text & "'", ViewBerandaLaporan)
    End Sub
End Class
