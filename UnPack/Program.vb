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
    Public source As String

    Sub Main(args As String())

        If args.Count = 0 Then
            Console.WriteLine("Tool UnPack - 2CongLC.vn :: 2024")
        Else
            source = args(0)
        End If

        Dim p As String = Nothing
        If IO.File.Exists(source) Then
            br = New BinaryReader(File.OpenRead(source))
            Dim CR1 As Char = br.ReadChar ' Offset = 0, Length = 2
            Dim LF1 As Char = br.ReadChar ' Offset = 2, Length = 2
            Dim FileType As String = New String(br.ReadChars(60)) ' Offset = 4, Length = 60
            Dim CR2 As Char = br.ReadChar ' Offset = 64, Length = 2
            Dim LF2 As Char = br.ReadChar ' Offset = 66, Length = 2
            Dim UserTitle As String = New String(br.ReadChars(60)) ' Offset = 68, Length = 60
            Dim CR3 As Char = br.ReadChar ' Offset = 128, Length = 2
            Dim LF3 As Char = br.ReadChar ' Offset = 130, Length = 2
            Dim EOF1 As Char = br.ReadChar ' Offset = 132, Length = 2
            Dim Version As Int32 = br.ReadInt32 ' Offet = 134, Length = 4
            Dim RootDirPos As Int32 = br.ReadInt32 ' Offset = 138, Length = 4
            Dim RootDirSize As Int32 = br.ReadInt32 ' Offset = 142, Length = 4
            Dim RootDirTime As Int32 = br.ReadInt32 ' Offset = 146, Length = 4
            Dim NextWritePos As Int32 = br.ReadInt32 ' Offset = 150, Length = 4
            Dim Time As Int32 = br.ReadInt32 ' Offset = 154, Length = 4
            Dim LargestKeyAry As Int32 = br.ReadInt32 ' Offset = 158, Length = 4
            Dim LargestDirNameSize As Int32 = br.ReadInt32 ' Offset = 162, Length = 4
            Dim LargestRezNameSize As Int32 = br.ReadInt32 ' Offset = 166, Length = 4
            Dim LargestCommentSize As Int32 = br.ReadInt32 ' Offset = 170, Length = 4
            Dim IsSorted As Byte = br.ReadByte ' Offset = 174, Length = 1

            Console.WriteLine("
                             - File Type : {0}
                             - UserTitle : {1}
                             - FileFormatVersion : {2}
                             - RootDirPos : {3}
                             - RootDirSize : {4}", FileType, UserTitle, FileFormatVersion, RootDirPos, RootDirSize)

            br.BaseStream.Position = RootDirPos
            Dim buffer As Byte() = New Byte(RootDirSize) {}
            br.Read(buffer, 0, RootDirSize)
            For i As Integer = 0 To RootDirSize - 1
                buffer(i) = CByte(&H49 + (keys((RootDirPos Mod keys.Length) Xor Not buffer(i)))
                RootDirPos += 1
            Next
            Dim ms as New MemoryStream(buffer)
            ms.Position = 0
            br = New BinaryReader(ms)
            While br.BaseStream.Position < br.BaseStream.Length


            End While







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

    Public keys As Byte() = {
    &HF0, &HF0, &H9D, &H9, &HA, &H66, &HAD, &H6A, &H85, &H1D,
    &HFD, &H3F, &H51, &H23, &HE7, &HF3, &HB1, &HE, &H78, &HEC,
    &HD1, &H50, &H7B, &H6B, &H17, &H3F, &H61, &HC5, &H79, &HC,
    &H57, &H32, &H1A, &HF3, &HB8, &H6B, &H68, &HDE, &H2A, &H5F,
    &H1, &HBA, &H98, &H3A, &H99, &HC0, &H54, &H2, &H24, &HF7,
    &H9B, &H9, &H87, &H23, &HC4, &H6F, &HE, &H6C, &H44, &HFA,
    &HDB, &HFB, &HE8, &H85, &HAB, &HC2, &H65, &H3C, &HE, &HC4,
    &H93, &HF6, &H6D, &HB, &H8A, &HD6, &H11, &H8D, &HE3, &H8F,
    &H71, &H52, &H5D, &H6E, &HFC, &HFD, &H29, &H82, &HB0, &H1D,
    &H13, &H11, &HAE, &H5C, &HD5, &HA9, &H1B, &HF8, &HCE, &HFC,
    &H79, &H9C, &H5A, &HD6, &HCE, &HFD, &HC, &H64, &HCA, &H60,
    &H16, &H12, &H31, &H5B, &H8, &H3A, &HCF, &H4, &H3E, &HEA,
    &H23, &HDC, &H28, &HFA, &H20, &HA5, &HC0, &HB8, &H21, &H73,
    &H5E, &H6C, &H6A, &H2B, &H31, &HE9, &H6D, &HBD, &H9A, &H73,
    &H11, &H4C, &HB1, &H43, &H3A, &H8E, &H28, &HCE, &HDC, &H9B,
    &HD4, &H31, &HCF, &H77, &H1D, &HE4, &H9F, &H8A, &H8B, &HA,
    &HB2, &H4E, &HC0, &H8D, &HDD, &H74, &HB, &H56, &HCF, &HB7,
    &HEE, &HD5, &H74, &HA7, &HB5, &H1B, &HA1, &HA9, &H85, &HCB,
    &H45, &H68, &HFF, &H1F, &H59, &HFB, &HCD, &H42, &HDA, &HFF,
    &H59, &H37, &H5, &HE7, &HDC, &H9E, &H12, &HBD, &H1B, &H87,
    &HBB, &H97, &H2, &H9A, &HC2, &H4, &H66, &HD3, &HBE, &HA7,
    &H2C, &H11, &H66, &H4E, &H10, &HBD, &HA8, &HB3, &H54, &HC2,
    &HC0, &H39, &H8D, &H17, &H91, &HDA, &HE0, &H21, &H86, &H8A,
    &HD3, &H24, &H37, &H4A, &H10, &H13, &HA, &H38, &H45, &HE2,
    &H26, &HC6, &H66, &HC0, &HDE, &H73, &H9B, &H53, &HE2, &H2D,
    &HA, &H57, &H7E, &HAC, &HC9, &HC4, &HC, &H4, &H33, &HD5,
    &HFA, &H9F, &HE5, &H15, &H8A, &HFD, &H95, &HCF, &H9A, &H57,
    &H16, &H2, &HB2, &H81, &HBE, &H39, &H8C, &H3A, &H72, &H6A,
    &H6F, &H34, &H8A, &H2F, &H84, &HE, &HEE, &H96, &H6D, &H80,
    &H83, &HBC, &H6A, &H2, &H45, &H84, &H3A, &H1C, &H49, &HA0,
    &H1, &HB7, &HDA, &H2C, &H76, &H96, &HFF, &H1D, &H8E, &H49,
    &HA7, &HCA, &HF5, &HD6, &HB0, &HBD, &H7F, &H51, &H21, &H25,
    &HEA, &HAC, &HB7, &H15, &H16, &HF6, &H24, &HD7, &HE, &H54,
    &H27, &H96, &HD, &HEC, &HD4, &H96, &HC9, &H0, &H33, &H4D,
    &H43, &H83, &H8C, &H7B, &H59, &H5E, &H96, &HAF, &H5F, &HAC,
    &HC3, &H4A, &HF9, &H23, &HFC, &H62, &H7B, &HFF, &HF5, &HB9,
    &HC, &H91, &H6A, &H1, &HCD, &HC9, &H87, &HBB, &H43, &HFC,
    &HA4, &HE7, &H49, &HD, &HB5, &HC7, &HC3, &H5A, &H95, &HF7,
    &H52, &H91, &H78, &H1D, &H52, &HC4, &HBC, &H63, &H5A, &HE4,
    &H6A, &H11, &H7B, &HFF, &H8D, &H72, &H8E, &H64, &HB5, &H53,
    &HB8, &H7, &HDD, &H4E, &H7F, &H4D, &HF4, &H35, &H99, &H96,
    &H4A, &HC6, &HC6, &HB7, &H20, &HF6, &HEB, &HA9, &HA1, &H18,
    &HAF, &HA7, &H77, &H7, &HE2, &HB, &H49, &HBA, &HE1, &H12,
    &H60, &H55, &H41, &HDD, &HA8, &H21, &H3, &HE5, &H5B, &H8F,
    &H81, &H1E, &H8D, &H8B, &H6A, &H11, &HE0, &H6F, &HF9, &H2F,
    &H96, &HC1, &HBA, &H8E, &H4D, &H6, &H6, &H62, &H9A, &HE8,
    &H92, &H66, &HCC, &HFB, &H34, &H7B, &H11, &H42, &H34, &HBC,
    &H3D, &HDC, &H63, &H3E, &H7A, &HF7, &H2C, &HD4, &H19, &H60,
    &HF5, &HF3, &HC5, &HE1, &HF9, &H1D, &H5F, &HB4, &HEF, &HEF,
    &HBA, &H4E, &HB1, &H35, &H7B, &HBD, &H26, &H1D, &H61, &HD0,
    &HB0, &HF4, &H2C, &H65, &H64, &H84, &H6B, &HFB, &H3C, &H74,
    &H6D, &HE1, &H93, &HD2, &H98, &H36, &H2A, &H18, &H5F, &HFA,
    &HE2, &HE1, &H23, &H7C, &H8C, &H93, &H2E, &H53, &HEE, &H40,
    &H23, &H2C, &H56, &HF3, &HFB, &HB3, &HEC, &HBC, &HFA, &HC7,
    &H6, &HA6, &HC0, &H4B, &HCC, &HE8, &HBB, &HC1, &H4C, &H84,
    &H41, &H1, &H67, &HA2, &H8F, &H43, &HB2, &HD6, &HEA, &HB6,
    &HA4, &HA0, &H21, &HF7, &H45, &H5E, &HBC, &H8E, &H9F, &HF2,
    &H3, &HCC, &H3B, &H5F, &H35, &H36, &HD4, &H91, &H18, &HC3,
    &H9E, &HA6, &H36, &H32, &H44, &HE0, &HFA, &HB2, &HF1, &H91,
    &HEF, &H1F, &H9D, &H39, &H66, &H10, &HDA, &H18, &HC2, &HFE,
    &H66, &H73, &H9F, &HBA, &HC8, &HD2, &H2C, &H7B, &H23, &H6A,
    &HD9, &HBD, &H9E, &H2, &HB2, &H35, &H7E, &H87, &H9E, &H1B,
    &H58, &H9A, &HC1, &H6, &H70, &H49, &H3D, &H9A, &HB4, &H46,
    &H9F, &H4D, &H67, &HCB, &H2A, &H82, &HDC, &H75, &H4A, &H32,
    &H70, &H50, &H68, &H6E, &HA, &H5C, &H65, &HF2, &H5E, &HC4,
    &HF6, &HE, &H34, &H4, &H23, &H24, &HF3, &H4B, &H30, &HF3,
    &HB2, &H4E, &H26, &H2, &H7, &HC8, &H3D, &H54, &HE5, &HFB,
    &H6F, &HB4, &HB0, &H5E, &H71, &HD8, &HE1, &HB9, &H44, &H92,
    &H69, &H2, &HBB, &H5C, &H16, &H24, &H16, &H70, &H3E, &HFD,
    &H9, &HBD, &HF2, &HD2, &H69, &HE7, &HEE, &H74, &HB3, &HA1,
    &H92, &H5A, &HC0, &H99, &H1A, &HF2, &HDD, &H3A, &H62, &H5E,
    &H81, &H7D, &H66, &HF0, &HE9, &H14, &HCA, &H8F, &HDD, &H24,
    &HA6, &H5A, &HD4, &HD8, &HD3, &HB8, &HBB, &H3, &H3, &H1D,
    &HA6, &H19, &HD1, &HC6, &H9E, &HBA, &H25, &HA8, &HD8, &H16,
    &HB, &HCF, &H8D, &H5C, &H5B, &H78, &HB9, &H88, &H60, &H19,
    &HFB, &HB8, &HC1, &HA0, &HD9, &H65, &HF3, &H24, &HAF, &H9F,
    &H6A, &H4F, &H72, &HAC, &HD2, &HB3, &HAC, &H2F, &H87, &H5C,
    &HCB, &H2B, &H9A, &HD0, &H1C, &H18, &H8F, &HC7, &HA7, &H47,
    &H26, &HD6, &H32, &HE5, &H68, &H4A, &HA5, &HC4, &H31, &H7C,
    &H16, &H44, &H8C, &HD8, &HB0, &H8C, &H1, &HD6, &HCD, &H51,
    &H37, &H2B, &H62, &H7B, &HF, &H66, &H20, &HD8, &H88, &H4B,
    &H6C, &H23, &HAB, &H1C, &H84, &HA2, &HAF, &H15, &H1, &H95,
    &HAC, &H62, &H3, &HBB, &HF, &HC2, &H3C, &H29, &HF, &H24,
    &H22, &HB9, &H6B, &H72, &H86, &H46, &HA6, &HD6, &HCB, &H6,
    &HE, &HB0, &H4, &H2C, &HBD, &H7E, &H35, &H29, &HED, &HFE,
    &HF9, &HB9, &HC1, &HBC, &HC9, &HA, &HD8, &H5B, &H2F, &H33,
    &HE9, &HD0, &HF, &H3E, &H9A, &HCC, &H63, &HC, &HE0, &HA3,
    &H91, &H4A, &H25, &HE1, &HA9, &HB3, &H6B, &HD2, &HC6, &HF2,
    &HBA, &H41, &HD5, &H51, &HF, &HAE, &HFB, &H7C, &HF, &H30,
    &HE4, &H9A, &HBE, &H50, &H36, &HF9, &H7A, &H17, &H62, &H8E,
    &H7B, &H94, &H23, &H8C, &H15, &HC, &HD5, &H48, &H2, &H2B,
    &HFB, &HB6, &HEB, &H5B, &H22, &HBE, &H75, &H9E, &H6A, &H99,
    &H1A, &HD, &HF6, &H90, &HFC, &H57, &H79, &H43, &H1, &H6F,
    &H2F, &HCD, &H74, &HAB, &H74, &HF5, &H65, &H9D, &H43, &HBB,
    &H13, &HDE, &HD5, &H6D, &H97, &H8, &HA9, &H9E, &H11, &H2E,
    &H2A, &H29, &HA0, &HFD, &H3F, &H84, &H52, &HDB, &HFB, &HB4,
    &H67, &H30, &HB3, &H8, &HB, &H2D, &HB7, &HEE, &HDA, &H41,
    &HED, &H1C, &H6A, &H7F, &H98, &H4F, &H14, &H45, &H75, &HD4,
    &H42, &H44, &H8C, &H34, &H86, &H4F, &HD9, &H28, &HAF, &H10,
    &H1E, &H25, &H22, &HF7, &H1A, &HC0, &HBE, &HA0, &H5D, &H1E,
    &H7C, &HE3, &HF, &HBE, &H17, &HE4, &HC5, &HD5, &HF9, &H4D,
    &HD0, &H7F, &HA7
}

End Module
