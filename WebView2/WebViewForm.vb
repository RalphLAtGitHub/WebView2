Imports Microsoft.Web.WebView2
Imports Microsoft.Web.WebView2.Core

Public Class WebViewForm

    Private m_WebView As WinForms.WebView2

    Public Sub New()

        InitializeComponent()

        m_WebView = New WinForms.WebView2 With {.Dock = DockStyle.Fill}
        Me.Controls.Add(m_WebView)
        InitializeWebView()

    End Sub

    Private Async Sub InitializeWebView()

        AddHandler m_WebView.CoreWebView2InitializationCompleted, AddressOf InitializationCompletedHandler
        AddHandler m_WebView.NavigationCompleted, AddressOf NavigationCompletedHandler

        Dim m_Options As New CoreWebView2EnvironmentOptions With {.AdditionalBrowserArguments = "--allow-file-access-from-files"}
        Dim m_EnvironmentFolder As CoreWebView2Environment = Await CoreWebView2Environment.CreateAsync(userDataFolder:=IO.Path.Combine(IO.Path.GetTempPath, "Test"), options:=m_Options)

        'Await m_WebView.EnsureCoreWebView2Async(m_EnvironmentFolder)

        Dim m_Frame As New Windows.Threading.DispatcherFrame()

        Using m_Task As Task = m_WebView.EnsureCoreWebView2Async(m_EnvironmentFolder)

            m_Task.ContinueWith(Sub() m_Frame.Continue = False)
            m_Frame.Continue = True
            Windows.Threading.Dispatcher.PushFrame(m_Frame)

        End Using

        m_WebView.Source = New Uri(IO.Path.Combine(Environment.CurrentDirectory, "WebResources\test.html"))
        m_WebView.CoreWebView2.OpenDevToolsWindow()

    End Sub

    Private Async Sub InitializeWebViewAsync()

        AddHandler m_WebView.CoreWebView2InitializationCompleted, AddressOf InitializationCompletedHandler
        AddHandler m_WebView.NavigationCompleted, AddressOf NavigationCompletedHandler

        Dim m_Options As New CoreWebView2EnvironmentOptions With {.AdditionalBrowserArguments = "--allow-file-access-from-files"}
        Dim m_EnvironmentFolder As CoreWebView2Environment = Await CoreWebView2Environment.CreateAsync(userDataFolder:=IO.Path.Combine(IO.Path.GetTempPath, "Test"), options:=m_Options)
        Await m_WebView.EnsureCoreWebView2Async(m_EnvironmentFolder)

        m_WebView.Source = New Uri(IO.Path.Combine(Environment.CurrentDirectory, "WebResources\test.html"))
        m_WebView.CoreWebView2.OpenDevToolsWindow()

    End Sub

    Private Sub InitializationCompletedHandler(sender As Object, e As CoreWebView2InitializationCompletedEventArgs)

        If e.IsSuccess Then

            Debug.WriteLine("Init completed - Edge Runtime Version is " + CoreWebView2Environment.GetAvailableBrowserVersionString)

        Else

            MsgBox("WebView Initialization Error - Error Code is " + e.InitializationException.ToString, MsgBoxStyle.Exclamation, "Test")

        End If

    End Sub

    Private Async Sub NavigationCompletedHandler(sender As Object, e As CoreWebView2NavigationCompletedEventArgs)

        If e.IsSuccess Then

            Debug.WriteLine("Navigation completed - Navigation ID is " + e.NavigationId.ToString)
            SendMessage("Navigation completed.")
            Await m_WebView.ExecuteScriptAsync("setCenterAt(51.0, -5.0);")

        Else

            MsgBox("WebView Navigation Error - Error Code is " + e.WebErrorStatus.ToString, MsgBoxStyle.Exclamation, "Test")

        End If

    End Sub

    Public Async Sub SendMessage(ByVal Text As String)

        Dim m_Message As String = "sendMessage('" + Text + "');"
        Debug.WriteLine("Start executing script: '" + m_Message + "'")
        Await m_WebView.ExecuteScriptAsync(m_Message)
        Debug.WriteLine("Executing script done.")

    End Sub

End Class