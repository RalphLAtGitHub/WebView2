Public Class Form1

    Private m_WebViewForm As WebViewForm

    Public Sub New()

        InitializeComponent()

        Me.IsMdiContainer = True
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Button2.Enabled = False

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        m_WebViewForm = New WebViewForm With {.MdiParent = Me}

        m_WebViewForm.Show()
        Me.Button2.Enabled = True

        m_WebViewForm.SendMessage("Button1 was clicked.")  'message NOT received by JS

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        m_WebViewForm.SendMessage("Button2 was clicked.") 'message received by JS

    End Sub

End Class