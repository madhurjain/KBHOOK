Imports KBHOOK

Public Class mainForm
    Dim kbhook As KBHOOK.KBHook
    Private Sub mainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        kbhook = New KBHOOK.KBHook()
        kbhook.HookKeyboard()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCtrlAltDel.Click
        If btnCtrlAltDel.Text = "Hook Keyboard" Then
            kbhook.DisableAltCtrlDel(True)
            kbhook.DisableCtrlAltDel(True)
            kbhook.DisableAltEsc(True)
            kbhook.DisableCtrlEsc(True)
            kbhook.DisableAltTab(True)
            kbhook.DisableWinKey(True)
            btnCtrlAltDel.Text = "Unhook Keyboard"
        Else
            kbhook.DisableAltCtrlDel(False)
            kbhook.DisableCtrlAltDel(False)
            kbhook.DisableAltEsc(False)
            kbhook.DisableCtrlEsc(False)
            kbhook.DisableAltTab(False)
            kbhook.DisableWinKey(False)
            btnCtrlAltDel.Text = "Hook Keyboard"
        End If
    End Sub

    Private Sub mainForm_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        kbhook.UnhookKeyboard()
    End Sub
End Class
