Imports System
Imports System.Collections
Imports System.IO
Imports System.IO.Compression
Imports System.Linq.Expressions
Imports System.Reflection.PortableExecutable
Imports System.Runtime
Imports System.Text
Imports System.Text.RegularExpressions

Module Program

    Public br As BinaryReader
    Public input As String

    Sub Main(args As String())

        If args.Count = 0 Then
            Console.WriteLine("Tool UnPack - 2CongLC.vn :: 2024")
        Else
            input = args(0)
        End If

        Dim p As String = Nothing
        If IO.File.Exists(input) Then
            br = New BinaryReader(File.OpenRead(input))
            Dim CR1 As Char = br.ReadChar ' Offset = 0, Length = 2
            Dim LF1 As Char = br.ReadChar ' Offset = 2, Length = 2
            Dim FileType As String = New String(br.ReadChars(60)) ' Offset = 4, Length = 60
            Dim CR2 As Char = br.ReadChar ' Offset = 64, Length = 2
            Dim LF2 As Char = br.ReadChar ' Offset = 66, Length = 2
            Dim UserTitle As String = New String(br.ReadChars(60)) ' Offset = 68, Length = 60
            Dim CR3 As Char = br.ReadChar ' Offset = 128, Length = 2
            Dim LF3 As Char = br.ReadChar ' Offset = 130, Length = 2
            Dim EOF1 As Char = br.ReadChar ' Offset = 132, Length = 2     
            Dim FileVersion As Int32 = Nothing
            If AscW(EOF1) = &H1A Then
                Dim offs As Int32 = br.BaseStream.Position
                FileVersion = br.ReadInt32
                If FileVersion <> &H1 Then
                    br.BaseStream.Seek(offs + &H7, SeekOrigin.Current)
                    FileVersion = br.ReadInt32
                    If FileVersion <> &H2 Then
                        Console.WriteLine("unknow version")
                    End If
                End If
                Console.WriteLine("version :  {0}", FileVersion)
            ElseIf AscW(EOF1) = &H2A Then
                Dim m_head As Char = br.ReadChar
                Dim encode As New List(Of Char)(32 + 1)
                br.Read(encode.ToArray(), 0, encode.Count - 1)
                Dim m_encode = New String(encode.ToArray())
                Dim m_tail As Char = br.ReadChar
                Dim m_detect_head As Char = br.ReadChar
                If AscW(m_detect_head) <> (AscW(m_head) Xor &H11) Then
                    Console.WriteLine("Invalid head (Head: {0} | Detect: {1} | Xor: {2})", m_head, m_detect_head, AscW(m_detect_head) * &H11)
                End If
                Dim detect_encode As New List(Of Char)(32 + 1)
                br.Read(detect_encode.ToArray(), 0, detect_encode.Count - 1)
                Dim m_detect_encode As String = detect_encode.ToString()
                Dim xorValue As Long = System.Convert.ToInt64(m_encode.ToString(), 10) Xor &H16B4423
                If m_detect_encode <> detect_encode.ToString() Then
                    Console.WriteLine(" - Invalid encode (Encode: {0} | Detect: {1} | Xor: {2})", m_encode, m_detect_encode, detect_encode.ToString())
                End If
                Dim m_detect_tail As Char = br.ReadChar
                If AscW(m_detect_tail) <> (AscW(m_tail) Xor &H11) Then
                    Console.WriteLine(" - Invalid tail (Tail: {0} | Detect: {1} | Xor: {2})", m_tail, m_detect_tail, (AscW(m_tail) Xor &H11))
                End If
                FileVersion = br.ReadInt32
                If FileVersion <> &H1 Then
                    Console.WriteLine("unknow version")
                End If
            End If
            Dim RootDirPos As Int32 = br.ReadInt32
            Dim RootDirSize As Int32 = br.ReadInt32
            Dim RootDirTime As Int32 = br.ReadInt32
            Dim NextWritePos As Int32 = br.ReadInt32 '
            Dim Time As Int32 = br.ReadInt32
            Dim LargestKeyAry As Int32 = br.ReadInt32 '
            Dim LargestDirNameSize As Int32 = br.ReadInt32
            Dim LargestRezNameSize As Int32 = br.ReadInt32
            Dim LargestCommentSize As Int32 = br.ReadInt32
            Dim IsSorted As Byte = br.ReadByte




            'p = Path.GetDirectoryName(input) & "\" & Path.GetFileNameWithoutExtension(input)
            'Directory.CreateDirectory(p)

            'Using bw As New BinaryWriter(File.Create(p & "//" & n))
            ' bw.Write(buffer)
            'End Using

            br.Close()
            Console.WriteLine("unpack done!!!")
        End If
        Console.ReadLine()
    End Sub

    Class TableData
        Public offset As Int32 ' Length = 4
        Public size As Int32 ' Length = 4
        Public time As Int32 ' Length = 4
        Public namelen As String ' Length = 4
        Public Sub New()
            offset = br.ReadInt32
            size = br.ReadInt32
            time = br.ReadInt32
            namelen = New String(br.ReadChars(4))
        End Sub
    End Class

    Class FileData
        Public pos As Int32 ' Length = 4
        Public size As Int32 ' Length = 4
        Public time As Int32 ' Length = 4
        Public id As Int32 ' Length = 4
        Public ext As Int32 ' Length = 4
        Public numkeys As Int32 ' Length = 4
        Public namelen As String ' Length = 4
        Public Sub New()
            pos = br.ReadInt32
            size = br.ReadInt32
            time = br.ReadInt32
            id = br.ReadInt32
            ext = br.ReadInt32
            numkeys = br.ReadInt32
            namelen = New String(br.ReadChars(4))
        End Sub
    End Class



End Module
