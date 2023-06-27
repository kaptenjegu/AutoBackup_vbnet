Imports System.IO

Public Class Form1
    Public app_bat, jb_1, jb_2 As String
    Public loop_int, n_exe As Integer
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.WindowState = FormWindowState.Minimized
        lbl_log.Text = "Memulai Aplikasi"
        Console.WriteLine("start aplikasi")
        konfig()
        mulai()
        Timer1.Start()
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        mulai()
    End Sub
    Sub mulai()
        Try
            Dim waktu As String = TimeOfDay.Hour & ":" & TimeOfDay.Minute
            If (jb_1 = waktu) Or (jb_2 = waktu) Then
                Shell(Application.StartupPath & app_bat, AppWinStyle.Hide)
                'lbl_log.Text = "Mulai Proses backup - freeze 2 menit"
                lbl_log.Text = TimeOfDay.Hour & ":" & TimeOfDay.Minute & " || auto backup running - " & Date.Now
                Threading.Thread.Sleep(120000)    'ms - 2 menit
            End If
        Catch ex As Exception
            lbl_log.Text = TimeOfDay.Hour & ":" & TimeOfDay.Minute & " || Error : " & ex.Message
        End Try
    End Sub
    Private Sub konfig()
        Try
            Dim reader As StreamReader = My.Computer.FileSystem.OpenTextFileReader(Application.StartupPath & "\konfigurasi.txt")
            Dim hasil As String
            Dim n As Integer = 1
            Do
                hasil = reader.ReadLine
                If n = 1 Then
                    app_bat = hasil
                ElseIf n = 2 Then
                    jb_1 = hasil
                ElseIf n = 3 Then
                    jb_2 = hasil
                End If
                'lbl_log.Text &= a
                n += 1
            Loop Until hasil Is Nothing

            reader.Close()
        Catch ex As Exception
            MsgBox("!!! File tidak lengkap, aplikasi tidak bisa berjalan !!!")
            End
        End Try
    End Sub
    Private Sub cmdRestart_Click(sender As Object, e As EventArgs) Handles cmdRestart.Click
        lbl_log.Text = "Memulai Aplikasi"
        Console.WriteLine("start aplikasi")
        konfig()
        mulai()
        Timer1.Start()
    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            NotifyIcon1.Visible = True
            Me.Hide()
            NotifyIcon1.BalloonTipText = "Program dihide di tray"
            NotifyIcon1.ShowBalloonTip(30)
        End If
    End Sub

    Private Sub NotifyIcon1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles NotifyIcon1.DoubleClick
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        NotifyIcon1.Visible = False
    End Sub

End Class
