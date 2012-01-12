Imports KBHOOK

Public Class mainForm
    Dim kbhook As KBHOOK.KBHook
    Private Sub mainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        kbhook = New KBHOOK.KBHook()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCtrlAltDel.Click
        If btnCtrlAltDel.Text = "Disable Windows Shortcuts" Then
            kbhook.HookKeyboard()
            kbhook.DisableAltCtrlDel(True)
            kbhook.DisableCtrlAltDel(True)
            kbhook.DisableAltEsc(True)
            kbhook.DisableCtrlEsc(True)
            kbhook.DisableAltTab(True)
            kbhook.DisableWinKey(True)
            btnCtrlAltDel.Text = "Enable Windows Shortcuts"
        Else
            kbhook.UnhookKeyboard()
            kbhook.DisableAltCtrlDel(False)
            kbhook.DisableCtrlAltDel(False)
            kbhook.DisableAltEsc(False)
            kbhook.DisableCtrlEsc(False)
            kbhook.DisableAltTab(False)
            kbhook.DisableWinKey(False)
            btnCtrlAltDel.Text = "Disable Windows Shortcuts"
        End If
    End Sub
End Class
