Imports Microsoft.Win32
Imports System.Runtime.InteropServices
Imports System.Reflection

Public Class KBHook

    Public Sub DisableCtrlAltDel(ByVal EnableDisable As Boolean)
        Dim regkey As RegistryKey
        Try
            If EnableDisable Then
                regkey = Registry.CurrentUser.CreateSubKey("Software\Microsoft\Windows\CurrentVersion\Policies\System")
                regkey.SetValue("DisableTaskMgr", 1)
                regkey = Registry.CurrentUser.CreateSubKey("Software\Microsoft\Windows\CurrentVersion\Policies")
                regkey.SetValue("DisableTaskMgr", 1)
            Else
                regkey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Policies\System", True)
                If Not regkey Is Nothing Then
                    regkey.SetValue("DisableTaskMgr", 0)
                End If
                regkey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Policies", True)
                If Not regkey Is Nothing Then
                    regkey.SetValue("DisableTaskMgr", 0)
                End If
            End If

            regkey.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Declare Function UnhookWindowsHookEx Lib "user32" _
        (ByVal hHook As Integer) As Integer

    Private Declare Function SetWindowsHookEx Lib "user32" _
      Alias "SetWindowsHookExA" (ByVal idHook As Integer, _
      ByVal lpfn As KeyboardHookDelegate, ByVal hmod As Integer, _
      ByVal dwThreadId As Integer) As Integer

    Private Declare Function GetAsyncKeyState Lib "user32" _
      (ByVal vKey As Integer) As Integer

    Private Declare Function CallNextHookEx Lib "user32" _
      (ByVal hHook As Integer, _
      ByVal nCode As Integer, _
      ByVal wParam As Integer, _
      ByVal lParam As KBDLLHOOKSTRUCT) As Integer

    Public Structure KBDLLHOOKSTRUCT
        Dim vkCode As Integer
        Dim scanCode As Integer
        Dim flags As Integer
        Dim time As Integer
        Dim dwExtraInfo As Integer
    End Structure

    ' Low-Level Keyboard Constants
    Private Const HC_ACTION As Integer = 0
    Private Const LLKHF_EXTENDED As Integer = &H1
    Private Const LLKHF_INJECTED As Integer = &H10
    Private Const LLKHF_ALTDOWN As Integer = &H20
    Private Const LLKHF_UP As Integer = &H80

    ' Virtual Keys
    Private Const VK_TAB = &H9
    Private Const VK_RALT = 165
    Private Const VK_LALT = 164
    Private Const VK_CONTROL = &H11
    Private Const VK_ESCAPE = &H1B
    Private Const VK_DELETE = &H2E
    Private Const VK_LWIN = &H5B
    Private Const VK_RWIN = &H5C
    Private Const WH_KEYBOARD_LL As Integer = 13&

    Dim IsDisabledAltTab As Boolean = False
    Dim IsDisabledAltEsc As Boolean = False
    Dim IsDisabledCtrlEsc As Boolean = False
    Dim IsDisabledWinKey As Boolean = False
    Dim IsDisabledCtrlAltDel As Boolean = False


    Public KeyboardHandle As Integer


    ' Implement this function to block as many
    ' key combinations as you'd like
    Private Function IsHooked( _
      ByRef Hookstruct As KBDLLHOOKSTRUCT) As Boolean

        'Debug.WriteLine("Hookstruct.vkCode: " & Hookstruct.vkCode)
        'Debug.WriteLine(Hookstruct.vkCode = VK_ESCAPE)
        'Debug.WriteLine(Hookstruct.vkCode = VK_TAB)

        If IsDisabledCtrlEsc Then
            If (Hookstruct.vkCode = VK_ESCAPE) And _
              CBool(GetAsyncKeyState(VK_CONTROL) _
              And &H8000) Then
                'Call HookedState("Ctrl + Esc blocked")
                Return True
            End If
        End If

        If IsDisabledAltTab Then
            If (Hookstruct.vkCode = VK_TAB) And _
      CBool(Hookstruct.flags And _
      LLKHF_ALTDOWN) Then
                'Call HookedState("Alt + Tab blockd")
                Return True
            End If
        End If

        If IsDisabledAltEsc Then
            If (Hookstruct.vkCode = VK_ESCAPE) And _
              CBool(Hookstruct.flags And _
                LLKHF_ALTDOWN) Then
                'Call HookedState("Alt + Escape blocked")
                Return True
            End If
        End If

        If IsDisabledWinKey Then
            If (Hookstruct.vkCode = VK_LWIN) And _
              CBool(Hookstruct.flags) Or (Hookstruct.vkCode = VK_RWIN) And _
                CBool(Hookstruct.flags) Then
                'Call HookedState("Windows Key Blocked")
                Return True
            End If
        End If

        Return False
    End Function

    Private Sub HookedState(ByVal Text As String)
        'Debug.WriteLine(Text)
    End Sub

    Private Function KeyboardCallback(ByVal Code As Integer, _
      ByVal wParam As Integer, _
      ByRef lParam As KBDLLHOOKSTRUCT) As Integer

        If (Code = HC_ACTION) Then
            'Debug.WriteLine("Calling IsHooked")

            If (IsHooked(lParam)) Then
                Return 1
            End If

        End If

        Return CallNextHookEx(KeyboardHandle, _
          Code, wParam, lParam)

    End Function


    Public Delegate Function KeyboardHookDelegate( _
      ByVal Code As Integer, _
      ByVal wParam As Integer, ByRef lParam As KBDLLHOOKSTRUCT) _
                   As Integer

    <MarshalAs(UnmanagedType.FunctionPtr)> _
    Private callback As KeyboardHookDelegate

    Public Sub HookKeyboard()
        callback = New KeyboardHookDelegate(AddressOf KeyboardCallback)

        KeyboardHandle = SetWindowsHookEx( _
          WH_KEYBOARD_LL, callback, _
          Marshal.GetHINSTANCE( _
          [Assembly].GetExecutingAssembly.GetModules()(0)).ToInt32, 0)

        Call CheckHooked()
    End Sub

    Private Sub CheckHooked()
        If (Hooked()) Then
            Debug.WriteLine("Keyboard hooked")
        Else
            Debug.WriteLine("Keyboard hook failed: " & Err.LastDllError)
        End If
    End Sub

    Private Function Hooked()
        Hooked = KeyboardHandle <> 0
    End Function

    Public Sub UnhookKeyboard()
        If (Hooked()) Then
            Call UnhookWindowsHookEx(KeyboardHandle)
        End If
    End Sub

    Public Sub DisableWinKey(ByVal EnableDisable As Boolean)
        IsDisabledWinKey = EnableDisable
    End Sub

    Public Sub DisableCtrlEsc(ByVal EnableDisable As Boolean)
        IsDisabledCtrlEsc = EnableDisable
    End Sub

    Public Sub DisableAltTab(ByVal EnableDisable As Boolean)
        IsDisabledAltTab = EnableDisable
    End Sub

    Public Sub DisableAltEsc(ByVal EnableDisable As Boolean)
        IsDisabledAltEsc = EnableDisable
    End Sub

    Public Sub DisableAltCtrlDel(ByVal EnableDisable As Boolean)
        IsDisabledCtrlAltDel = EnableDisable
    End Sub

End Class
