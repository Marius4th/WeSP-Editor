﻿Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions

Public Class HTMLParser
    Public Shared Function IsStringHexColor(ByVal hex As String) As Boolean
        If hex.StartsWith("#") AndAlso (hex.Length = 9 OrElse hex.Length = 7) Then Return True
        Return False
    End Function

    Public Shared Function ColorToHexString(ByRef col As Color) As String
        Return "#" & Conversion.Hex(col.R).PadLeft(2, "0") & Conversion.Hex(col.G).PadLeft(2, "0") & Conversion.Hex(col.B).PadLeft(2, "0") & Conversion.Hex(col.A).PadLeft(2, "0")
    End Function

    Public Shared Function HexStringToColor(ByVal hex As String) As Color
        Dim col As New Color
        hex = hex.Replace("#", "")
        If hex.Length >= 8 Then 'RGBA
            Return Color.FromArgb(Convert.ToInt32(hex.Substring(6, 2), 16), Convert.ToInt32(hex.Substring(0, 2), 16), Convert.ToInt32(hex.Substring(2, 2), 16), Convert.ToInt32(hex.Substring(4, 2), 16))
        ElseIf hex.Length >= 6 Then 'RGB
            Return Color.FromArgb(Convert.ToInt32(hex.Substring(0, 2), 16), Convert.ToInt32(hex.Substring(2, 2), 16), Convert.ToInt32(hex.Substring(4, 2), 16))
        End If
        Return col
    End Function

    Public Shared Function IsStringRGBColor(ByVal rgb As String) As Boolean
        If Regex.IsMatch(rgb, "\s*rgb\(\d+,\d+,\d+\)\s*") Then Return True
        Return False
    End Function

    Public Shared Function ColorToRGBString(ByRef col As Color) As String
        Return "rgb(" & col.R & "," & col.G & "," & col.B & ")"
    End Function

    Public Shared Function RGBStringToColor(ByVal rgb As String) As Color
        Dim col As New Color
        rgb = rgb.Replace("rgb(", "").Replace(")", "")
        Dim nums = Split(rgb, ",")
        If nums.Length <> 3 Then Return Color.White

        col = Color.FromArgb(255, nums(0), nums(1), nums(2))
        Return col
    End Function

    Public Shared Function IsStringRGBAColor(ByVal rgba As String) As Boolean
        If Regex.IsMatch(rgba, "\s*rgba\(\d+,\d+,\d+,\d+\)\s*") Then Return True
        Return False
    End Function

    Public Shared Function ColorToRGBAString(ByRef col As Color) As String
        Return "rgba(" & col.R & "," & col.G & "," & col.B & "," & col.A & ")"
    End Function

    Public Shared Function RGBAStringToColor(ByVal rgba As String) As Color
        Dim col As New Color
        rgba = rgba.Replace("rgba(", "").Replace(")", "")
        Dim nums = Split(rgba, ",")
        If nums.Length <> 4 Then Return Color.White

        col = Color.FromArgb(nums(3), nums(0), nums(1), nums(2))
        Return col
    End Function

    Public Shared Function HTMLStringToColor(ByRef html As String)
        If IsStringHexColor(html) Then
            Return HexStringToColor(html)
        ElseIf IsStringRGBColor(html) Then
            Return RGBStringToColor(html)
        ElseIf IsStringRGBAColor(html) Then
            Return RGBAStringToColor(html)
        End If

        Return Color.FromName(html)
    End Function


    Public Shared Function GetAttributeValue(html As String, propName As String, Optional isNameAlone As Boolean = True) As String
        'html = html.Replace("'", """")
        'propName = propName.Replace("'", """")
        'If it doesn't contain '='
        If isNameAlone Then propName &= "="""

        Dim subStart As Integer = html.IndexOf(propName)
        If subStart < 0 Then Return "0"

        subStart += propName.Length
        Dim subEnd As Integer = html.IndexOf("""", subStart)
        Dim substr As String = html.Substring(subStart, subEnd - subStart)

        Return substr
    End Function

    Public Shared Function GetAttributes(html As String) As Dictionary(Of String, String)
        Dim attrs As New Dictionary(Of String, String)
        'html = html.Replace("'", """")
        'propName = propName.Replace("'", """")
        Dim rxEnd As New Regex("/>[\s\n]+|>[\s\n]+|><")

        html = Regex.Replace(html, "^[\s\t]*<\w+\s+", "")
        html = rxEnd.Split(html, 2)(0)

        Dim lastAttr As String = ""
        For Each item As String In Split(html, """")
            If item.EndsWith("=") Then
                lastAttr = item.Substring(0, item.Length - 1).Replace(" ", "").ToLower
                If Not attrs.ContainsKey(lastAttr) Then
                    attrs.Add(lastAttr, "")
                End If
            ElseIf attrs.ContainsKey(lastAttr) AndAlso attrs(lastAttr).Length <= 0 Then 'In case of duplicates, use the first one as Firefox and Chrome does
                attrs(lastAttr) = item
                lastAttr = ""
            End If
        Next

        Return attrs
    End Function




    'TODO
    Public Class ClFakeDOM
        Public Elements As New List(Of ClElement) 'root elements

        'Iterates through all the elements and sub elements
        Public Iterator Function GetAllElements() As IEnumerable(Of ClElement)
            For Each elem As ClElement In Elements
                Yield elem
                Yield elem.GetAllElements()
            Next
        End Function
    End Class
    'TODO
    Public Class ClElement
        Public Name As String
        Public Attributes As New Dictionary(Of String, String)
        Public Elements As New List(Of ClElement) 'sub elements

        Public Iterator Function GetAllElements() As IEnumerable(Of ClElement)
            For Each elem As ClElement In Elements
                Yield elem
                Yield elem.GetAllElements()
            Next
        End Function
    End Class
    'TODO
    Public Shared Function GetElements(html As String) As ClFakeDOM
        Dim fdom As New ClFakeDOM
        Dim lst As List(Of String)
        Dim rx As New Regex("(<[a-z|A-Z])")

        lst = SplitRxKeep(html, "<[a-z|A-Z]", -1, True)

        For Each item In lst

        Next
    End Function

End Class
