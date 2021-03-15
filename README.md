# Demo related to MicrosoftEdge/WebView2Feedback issue #1065

Click on Button1 creates a new MDI child form with a WebView2 control inside (try to create WebView2 synchronously) and shows the child form. Message send to JS in the click handeler doesn't reach JS. Apparently the WebView2 control was not generated synchronously.

Click on Button2 sends a message to JS without any problems.

Creating the child form 'in reserve' in the ctor of the MDI parent (WebView2 is created asynchronously) is a 'workaround'. The only question is whether this is a useful result. This applies in particular to migrating from WebBrowser control to WebView2 control, since WebBrowser control is generated synchronously with the result that the relevant applications are designed to be synchrion.
