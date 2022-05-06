Imports System.IO
Imports System.Management
Imports System.Text
Public Enum LogLevel
    ' Fields
    Debug = 1
    [Error] = 3
    Information = 2
    Trace = 0
    '''''
    'Date: 2013-01-31
    'Author: Sim
    '''''
    TDuration = 4
    '''''
    'Date: 2013-02-01
    'Author: Tammie
    '''''
    OEELog = 5
    '''''
    'Date: 2013-01-31
    'Author: BK
    '''''
    UPH = 6
    Vision = 7
End Enum

Public Class Logger
    Private Shared ReadOnly obj As Object
    Private Shared currentLevel As LogLevel

    Shared Sub New()
        Logger.obj = New Object
        Logger.currentLevel = LogLevel.Trace
    End Sub

    Public Shared Property Level() As LogLevel
        Get
            Return Logger.currentLevel
        End Get
        Set(ByVal value As LogLevel)
            Logger.currentLevel = value
        End Set
    End Property

    Private Shared Sub Append(ByVal message As String, Optional ByVal FileName As String = "")
        SyncLock Logger.obj
            Dim strPath As String = System.Windows.Forms.Application.StartupPath & Path.DirectorySeparatorChar & "Logs"
            If Not Directory.Exists(strPath) Then
                Directory.CreateDirectory(strPath)
            End If
            Console.WriteLine(message)
            Dim strFileName As String
            If Not FileName = "" Then
                strFileName = FileName + (DateTime.Now.ToString("yyyyMMdd") & ".log")
            Else
                strFileName = (DateTime.Now.ToString("yyyyMMdd") & ".log")
            End If
            Dim strPathFileName As String = strPath & Path.DirectorySeparatorChar & strFileName
            If Not File.Exists(strPathFileName) Then

                Using stream As FileStream = New FileStream((strPathFileName), FileMode.Append, FileAccess.Write)
                    Using writer As StreamWriter = New StreamWriter(stream)
                        writer.WriteLine()
                        writer.WriteLine("Application Start: " + vbTab + vbTab + DateTime.Now)
                        writer.WriteLine("OS Type: " + vbTab + vbTab + vbTab + Environment.OSVersion.Platform.ToString())
                        writer.WriteLine("OS Service Pack: " + vbTab + vbTab + Environment.OSVersion.ServicePack)
                        writer.WriteLine("OS Version: " + vbTab + vbTab + vbTab + Environment.OSVersion.Version.ToString())
                        writer.WriteLine("OS: " + vbTab + vbTab + vbTab + vbTab + getOSInfo())
                        writer.WriteLine("Machine Name: " + vbTab + vbTab + vbTab + Environment.MachineName)
                        writer.WriteLine("Machine UserName: " + vbTab + vbTab + Environment.UserName)
                        writer.WriteLine("Machine ProcessorCount: " + vbTab + Environment.ProcessorCount.ToString())
                        writer.WriteLine("ID: " + vbTab + vbTab + vbTab + vbTab + getCPUID())
                        writer.WriteLine()
                        writer.Flush()
                    End Using
                End Using
            End If

            Using stream As FileStream = New FileStream((strPathFileName), FileMode.Append, FileAccess.Write)
                Using writer As StreamWriter = New StreamWriter(stream)
                    writer.WriteLine(message)
                    writer.Flush()
                End Using
            End Using
        End SyncLock
    End Sub

    Private Shared Sub Log(ByVal message As String, ByVal level As LogLevel, Optional ByVal ex As Exception = Nothing, Optional ByVal boolSeparateFile As Boolean = False)
        Dim strFilename As String = String.Empty
        If boolSeparateFile AndAlso level = LogLevel.Error Then
            strFilename = "Err_"
        ElseIf boolSeparateFile AndAlso level = LogLevel.Information Then
            strFilename = "Info_"
        ElseIf boolSeparateFile AndAlso level = LogLevel.Vision Then
            strFilename = "Vision_"
        End If

        message = String.Format("{0:yyyy-MM-dd HH:mm:ss.fff} : {1} : {2}", DateTime.Now, level, message)

        Logger.Append(message, strFilename)
        If ex IsNot Nothing Then
            If ex.InnerException IsNot Nothing Then
                message = String.Format("{0:yyyy-MM-dd HH:mm:ss.fff} : {1} : {2}", DateTime.Now, "Inner exception", ex.InnerException.Message)
                Logger.Append(message, strFilename)
            End If

            message = String.Format("{0:yyyy-MM-dd HH:mm:ss.fff} : {1} : {2}", DateTime.Now, "Message", ex.Message)
            Logger.Append(message, strFilename)
            message = String.Format("{0:yyyy-MM-dd HH:mm:ss.fff} : {1} : {2}", DateTime.Now, "Source", ex.Source)
            Logger.Append(message, strFilename)
            message = String.Format("{0:yyyy-MM-dd HH:mm:ss.fff} : {1} : {2}", DateTime.Now, "Stacktrace", ex.StackTrace)
            Logger.Append(message, strFilename)
        End If

    End Sub


    Public Shared Sub Info(ByVal message As String)
        Logger.Log(message, LogLevel.Information)
    End Sub

    Public Shared Sub InfoSF(ByVal message As String)
        Logger.Log(message, LogLevel.Information, Nothing, False)
    End Sub

    Public Shared Sub [Error](ByVal message As String, ByVal ex As Exception)
        Logger.Log(message, LogLevel.Error, ex)
    End Sub

    Public Shared Sub ErrorSF(ByVal message As String, ByVal ex As Exception)
        Logger.Log(message, LogLevel.Error, ex, True)
    End Sub

    Public Shared Sub Trace(ByVal message As String)
        Logger.Log(message, LogLevel.Trace)
    End Sub

    Public Shared Sub Debug(ByVal message As String)
        Logger.Log(message, LogLevel.Debug)
    End Sub

    ''''''
    'Date: 2013-01-31
    'Author: Sim
    'Write OEE Log
    '''''
    Public Shared Sub writeOEELog(ByVal message As String)
        Logger.Log2(message, LogLevel.OEELog, Nothing, True)
    End Sub

    '''''
    'Date: 2013-01-31
    'Author: Sim
    '''''
    Public Shared Sub Duration(ByVal message As String)
        Logger.Log2(message, LogLevel.TDuration, Nothing, True)
    End Sub

    Public Shared Sub writeVisionLog(ByVal message As String)

        Logger.Log(message, LogLevel.Vision)

    End Sub

    Private Function getUniqueID(ByVal drive As String) As String
        If drive = String.Empty Then
            'Find first drive
            For Each compDrive As DriveInfo In DriveInfo.GetDrives()
                If compDrive.IsReady Then
                    drive = compDrive.RootDirectory.ToString()
                    Exit For
                End If
            Next
        End If

        If drive.EndsWith(":\") Then
            'C:\ -> C
            drive = drive.Substring(0, drive.Length - 2)
        End If

        Dim volumeSerial As String = getVolumeSerial(drive)
        Dim cpuID As String = getCPUID()

        'Mix them up and remove some useless 0's
        Return cpuID.Substring(13) + cpuID.Substring(1, 4) + volumeSerial + cpuID.Substring(4, 4)
    End Function

    Private Function getVolumeSerial(ByVal drive As String) As String
        Dim disk As New ManagementObject("win32_logicaldisk.deviceid=""" + drive + ":""")
        disk.[Get]()

        Dim volumeSerial As String = disk("VolumeSerialNumber").ToString()
        disk.Dispose()

        Return volumeSerial
    End Function

    Private Shared Function getCPUID() As String
        Dim cpuInfo As String = ""
        Dim managClass As New ManagementClass("win32_processor")
        Dim managCollec As ManagementObjectCollection = managClass.GetInstances()

        For Each managObj As ManagementObject In managCollec
            If cpuInfo = "" Then
                'Get only the first CPU's ID
                cpuInfo = managObj.Properties("processorID").Value.ToString()
                Exit For
            End If
        Next

        Return cpuInfo
    End Function

    Private Shared Function getOSInfo() As String
        'Get Operating system information.
        Dim os As OperatingSystem = Environment.OSVersion
        'Get version information about the os.
        Dim vs As Version = os.Version

        'Variable to hold our return value
        Dim operatingSystem As String = ""

        If os.Platform = PlatformID.Win32Windows Then
            'This is a pre-NT version of Windows
            Select Case vs.Minor
                Case 0
                    operatingSystem = "95"
                    Exit Select
                Case 10
                    If vs.Revision.ToString() = "2222A" Then
                        operatingSystem = "98SE"
                    Else
                        operatingSystem = "98"
                    End If
                    Exit Select
                Case 90
                    operatingSystem = "Me"
                    Exit Select
                Case Else
                    Exit Select
            End Select
        ElseIf os.Platform = PlatformID.Win32NT Then
            Select Case vs.Major
                Case 3
                    operatingSystem = "NT 3.51"
                    Exit Select
                Case 4
                    operatingSystem = "NT 4.0"
                    Exit Select
                Case 5
                    If vs.Minor = 0 Then
                        operatingSystem = "2000"
                    Else
                        operatingSystem = "XP"
                    End If
                    Exit Select
                Case 6
                    If vs.Minor = 0 Then
                        operatingSystem = "Vista"
                    Else
                        operatingSystem = "7"
                    End If
                    Exit Select
                Case Else
                    Exit Select
            End Select
        End If
        'Make sure we actually got something in our OS check
        'We don't want to just return " Service Pack 2" or " 32-bit"
        'That information is useless without the OS version.
        If operatingSystem <> "" Then
            'Got something.  Let's prepend "Windows" and get more info.
            operatingSystem = "Windows " + operatingSystem
            'See if there's a service pack installed.
            If os.ServicePack <> "" Then
                'Append it to the OS name.  i.e. "Windows XP Service Pack 3"
                operatingSystem += " " + os.ServicePack
            End If
            'Append the OS architecture.  i.e. "Windows XP Service Pack 3 32-bit"
            operatingSystem += " " + getOSArchitecture().ToString() + "-bit"
        End If
        'Return the information we've gathered.
        Return operatingSystem
    End Function


    Private Shared Function getOSArchitecture() As Integer
        Dim pa As String = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE")
        Return (If(([String].IsNullOrEmpty(pa) OrElse [String].Compare(pa, 0, "x86", 0, 3, True) = 0), 32, 64))
    End Function

#Region "UPH"
    'BK LO 31/1/2013
    'FOR UPH LOGGER
    Private Shared Sub Append2(ByVal message As String, Optional ByVal FileName As String = "")
        SyncLock Logger.obj
            Dim strPath As String = System.Windows.Forms.Application.StartupPath & Path.DirectorySeparatorChar & "Logs\UPH"
            Dim key() As String = message.Split(" ")

            If Not Directory.Exists(strPath) Then
                Directory.CreateDirectory(strPath)
            End If
            Console.WriteLine(message)
            Dim strFileName As String
            If Not FileName = "" Then
                strFileName = FileName + ".log"
            Else
                strFileName = (DateTime.Now.ToString("yyyyMMdd") & ".log")
            End If
            Dim strPathFileName As String = strPath & Path.DirectorySeparatorChar & strFileName
            'BK Added 20130205
            If FileName = "pass" Then
                strPathFileName = strFileName + ""
            End If
            If Not File.Exists(strPathFileName) Then
                Using stream As FileStream = New FileStream((strPathFileName), FileMode.CreateNew, FileAccess.Write)
                    Using writer As StreamWriter = New StreamWriter(stream)
                        writer.WriteLine(message)
                        writer.Flush()
                    End Using
                End Using
                'ElseIf key(0) = "00" Then
                '    Using stream As FileStream = New FileStream((strPathFileName), FileMode.Create, FileAccess.Write)
                '        Using writer As StreamWriter = New StreamWriter(stream)
                '            writer.WriteLine(message)
                '            writer.Flush()
                '        End Using
                '    End Using
            Else
                Using stream As FileStream = New FileStream((strPathFileName), FileMode.Append, FileAccess.Write)
                    Using writer As StreamWriter = New StreamWriter(stream)
                        writer.WriteLine(message)
                        writer.Flush()
                    End Using
                End Using
            End If
        End SyncLock
    End Sub

    Private Shared Sub Append3(ByVal message As String, Optional ByVal FileName As String = "")
        SyncLock Logger.obj
            Dim strPath As String = System.Windows.Forms.Application.StartupPath & Path.DirectorySeparatorChar & "Logs\UPH\Process"
            If Not Directory.Exists(strPath) Then
                Directory.CreateDirectory(strPath)
            End If
            Console.WriteLine(message)
            Dim strFileName As String
            If Not FileName = "" Then
                strFileName = FileName + ".log"
            Else
                strFileName = (DateTime.Now.ToString("yyyyMMdd") & ".log")
            End If
            Dim strPathFileName As String = strPath & Path.DirectorySeparatorChar & strFileName
            'BK Added 20130205
            If FileName = "pass" Then
                strPathFileName = strFileName + ""
            End If
            If FileName = "product" Then
                strPathFileName = strFileName + ""
            End If
            If FileName = "shift" Then
                strPathFileName = strFileName + ""
            End If
            If Not File.Exists(strPathFileName) Then
                Using stream As FileStream = New FileStream((strPathFileName), FileMode.CreateNew, FileAccess.Write)
                    Using writer As StreamWriter = New StreamWriter(stream)
                        writer.WriteLine(message)
                        writer.Flush()
                    End Using
                End Using
            Else
                Using stream As FileStream = New FileStream((strPathFileName), FileMode.Create, FileAccess.Write)
                    Using writer As StreamWriter = New StreamWriter(stream)
                        writer.WriteLine(message)
                        writer.Flush()
                    End Using
                End Using
            End If
        End SyncLock
    End Sub
#End Region

#Region "OEE"
    ' Date: 2013-02-01
    ' Author: BK Lo
    ' Description" OEE Logger
    Private Shared Sub AppendOEE(ByVal message As String, Optional ByVal FileName As String = "")
        SyncLock Logger.obj
            Dim strPath As String
            If FileName = "OEE_" Then
                strPath = System.Windows.Forms.Application.StartupPath & Path.DirectorySeparatorChar & "Logs\OEE"
            Else
                strPath = System.Windows.Forms.Application.StartupPath & Path.DirectorySeparatorChar & "Logs\Duration"
            End If

            If Not Directory.Exists(strPath) Then
                Directory.CreateDirectory(strPath)
            End If
            Console.WriteLine(message)
            Dim strFileName As String
            If Not FileName = "" Then
                strFileName = FileName + (DateTime.Now.ToString("yyyyMMdd") & ".log")
            Else
                strFileName = (DateTime.Now.ToString("yyyyMMdd") & ".log")
            End If
            Dim strPathFileName As String = strPath & Path.DirectorySeparatorChar & strFileName
            If Not File.Exists(strPathFileName) Then
                Using stream As FileStream = New FileStream((strPathFileName), FileMode.CreateNew, FileAccess.Write)
                    Using writer As StreamWriter = New StreamWriter(stream)

                        writer.WriteLine()
                        writer.WriteLine("Application Start: " + vbTab + vbTab + DateTime.Now)
                        writer.WriteLine("OS Type: " + vbTab + vbTab + vbTab + Environment.OSVersion.Platform.ToString())
                        writer.WriteLine("OS Service Pack: " + vbTab + vbTab + Environment.OSVersion.ServicePack)
                        writer.WriteLine("OS Version: " + vbTab + vbTab + vbTab + Environment.OSVersion.Version.ToString())
                        writer.WriteLine("OS: " + vbTab + vbTab + vbTab + vbTab + getOSInfo())
                        writer.WriteLine("Machine Name: " + vbTab + vbTab + vbTab + Environment.MachineName)
                        writer.WriteLine("Machine UserName: " + vbTab + vbTab + Environment.UserName)
                        writer.WriteLine("Machine ProcessorCount: " + vbTab + Environment.ProcessorCount.ToString())
                        writer.WriteLine("ID: " + vbTab + vbTab + vbTab + vbTab + getCPUID())
                        writer.WriteLine()
                        writer.Flush()

                        writer.WriteLine(message)
                        'writer.Flush()
                    End Using
                End Using
            Else
                Using stream As FileStream = New FileStream((strPathFileName), FileMode.Append, FileAccess.Write)
                    Using writer As StreamWriter = New StreamWriter(stream)
                        writer.WriteLine(message)
                        'writer.Flush()
                    End Using
                End Using
            End If
        End SyncLock
    End Sub

#End Region

    Private Shared Sub Log2(ByVal message As String, ByVal level As LogLevel, Optional ByVal ex As Exception = Nothing, Optional ByVal boolSeparateFile As Boolean = False)
        Dim strFilename As String = String.Empty
        If boolSeparateFile AndAlso level = LogLevel.Error Then
            strFilename = "Err_"
        ElseIf boolSeparateFile AndAlso level = LogLevel.Information Then
            strFilename = "Info_"
        ElseIf boolSeparateFile AndAlso level = LogLevel.UPH Then
            strFilename = "UPH_"
        ElseIf boolSeparateFile AndAlso level = LogLevel.OEELog Then
            strFilename = "OEE_"
        ElseIf boolSeparateFile AndAlso level = LogLevel.TDuration Then
            strFilename = "Duration_"
            ' message = String.Format("{0:yyyy-MM-dd HH:mm:ss.fff} : {1}", DateTime.Now, message)
            message = String.Format("{0}", message)

        End If
        'message = String.Format("{1}", message)
        'Logger.Append2(message, strFilename)

        If strFilename = "OEE_" Or strFilename = "Duration_" Then
            Logger.AppendOEE(message, strFilename)
        Else
            Logger.Append2(message, strFilename)
        End If
    End Sub

    Public Shared Sub TrackUPH(ByVal message As String, ByVal fileName As String)
        Logger.Append3(message, fileName)
    End Sub

    Public Shared Sub LogUPH(ByVal message As String, ByVal fileName As String)
        Logger.Append2(message, fileName)
    End Sub

#Region "Vision"
    Public Shared Function getLatestVision(ByVal SourceDir As String) As String()
        Dim fName As String = ""
        Dim i As Integer = 0
        getLatestVision = Nothing
        Try
            If Directory.Exists(SourceDir) Then
                Dim X As DirectoryInfo = New DirectoryInfo(SourceDir)
                Dim listofFile As FileInfo() = X.GetFiles()
                Dim fileList(listofFile.Length - 1) As String

                For Each fName In Directory.GetFiles(SourceDir)
                    If File.Exists(fName) Then
                        Dim dFile As String = String.Empty
                        dFile = Path.GetFileName(fName)

                        fileList(i) = dFile
                        i = i + 1
                        'File.Move(fName, dFilePath)
                    End If
                Next
                Array.Sort(fileList)
                Return (fileList)
            End If

        Catch ex As Exception
            'SetErrorMsg = "Move File Fail,File Location = " & fName & " ,Msg = " & ex.Message.ToString
        End Try
    End Function

    Public Shared Function ReadFromLog(ByVal pathSource As String) As String()
        Try
            Dim value As String
            Dim lines As String()
            Dim arg() As String = {vbCrLf, vbLf}
            Using sr As New StreamReader(pathSource, Encoding.Default)
                value = sr.ReadToEnd()
                lines = value.Split(arg, StringSplitOptions.None)
                For i As Integer = 0 To lines.Length - 1
                    lines(i) = Replace(lines(i), "\n", "")
                Next
                Return lines
            End Using
        Catch ex As Exception
            [Error]("OEE Read From Log", ex)
            Return Nothing
        End Try

    End Function

    Private Shared Sub MoveFileToLog(ByVal SourceDir As String, ByVal DestinationDir As String)
        Try
            If (Not Directory.Exists(Path.GetDirectoryName(DestinationDir))) Then
                Directory.CreateDirectory(Path.GetDirectoryName(DestinationDir))
            End If

            File.Copy(SourceDir, DestinationDir, True)
            File.Delete(SourceDir)
        Catch ex As Exception
            [Error]("OEE MoveFileToLog", ex)
        End Try

    End Sub

    Public Shared Function RetriveVision(ByVal pathSource As String, ByVal Destinationdir As String) As String()
        Try
            Dim vision() As String = Nothing
            Dim sourceDir() As String = getLatestVision(pathSource)
            If sourceDir IsNot Nothing Then
                If sourceDir.Length > 0 Then
                    vision = ReadFromLog(pathSource & "\\" & sourceDir(sourceDir.Length - 1))
                    For i As Integer = 0 To sourceDir.Length - 1
                        MoveFileToLog(pathSource & "\\" & sourceDir(i), Destinationdir & "\\" & sourceDir(i))
                    Next
                End If
            End If
            Return vision
        Catch ex As Exception
            [Error]("OEE RetriveVision", ex)
            Return Nothing
        End Try

    End Function

#End Region


#Region "Cage Alignment"
    ' Date: 2013-02-01
    ' Author: BK Lo
    ' Description" OEE Logger
    Public Shared Sub LogAlignmentData(ByVal message As String)
        SyncLock Logger.obj
            Dim strPath As String

            strPath = System.Windows.Forms.Application.StartupPath & Path.DirectorySeparatorChar & "Logs\Vision_Result\Cage_Alignment_Data"

            If Not Directory.Exists(strPath) Then
                Directory.CreateDirectory(strPath)
            End If
            Console.WriteLine(message)
            Dim strFileName As String

            strFileName = "Cage_Alignment_Data_" + (DateTime.Now.ToString("yyyyMMdd") & ".csv")


            Dim strPathFileName As String = strPath & Path.DirectorySeparatorChar & strFileName
            If Not File.Exists(strPathFileName) Then
                Using stream As FileStream = New FileStream((strPathFileName), FileMode.CreateNew, FileAccess.Write)
                    Using writer As StreamWriter = New StreamWriter(stream)

                        writer.WriteLine("Date,Time,Cage Alignment Data,Cage Alignment Result")

                        writer.WriteLine(message)
                        'writer.Flush()
                    End Using
                End Using
            Else
                Using stream As FileStream = New FileStream((strPathFileName), FileMode.Append, FileAccess.Write)
                    Using writer As StreamWriter = New StreamWriter(stream)
                        writer.WriteLine(message)
                    End Using
                End Using
            End If
        End SyncLock
    End Sub

#End Region

#Region "Cap Alignment"
    ' Date: 2013-02-01
    ' Author: BK Lo
    ' Description" OEE Logger
    Public Shared Sub LogAlignmentDataCap(ByVal message As String)
        SyncLock Logger.obj
            Dim strPath As String

            strPath = System.Windows.Forms.Application.StartupPath & Path.DirectorySeparatorChar & "Logs\Vision_Result\Cap_Alignment_Data"

            If Not Directory.Exists(strPath) Then
                Directory.CreateDirectory(strPath)
            End If
            Console.WriteLine(message)
            Dim strFileName As String

            strFileName = "Cap_Alignment_Data_" + (DateTime.Now.ToString("yyyyMMdd") & ".csv")


            Dim strPathFileName As String = strPath & Path.DirectorySeparatorChar & strFileName
            If Not File.Exists(strPathFileName) Then
                Using stream As FileStream = New FileStream((strPathFileName), FileMode.CreateNew, FileAccess.Write)
                    Using writer As StreamWriter = New StreamWriter(stream)

                        writer.WriteLine("Date,Time,Cap Alignment Data,Cap Alignment Result")

                        writer.WriteLine(message)
                        'writer.Flush()
                    End Using
                End Using
            Else
                Using stream As FileStream = New FileStream((strPathFileName), FileMode.Append, FileAccess.Write)
                    Using writer As StreamWriter = New StreamWriter(stream)
                        writer.WriteLine(message)
                    End Using
                End Using
            End If
        End SyncLock
    End Sub

#End Region

#Region "Vision Result"
    ' Date: 2013-02-01
    ' Author: BK Lo
    ' Description" OEE Logger
    Public Shared Sub VisionResult(ByVal message As String)
        SyncLock Logger.obj
            Dim strPath As String

            strPath = System.Windows.Forms.Application.StartupPath & Path.DirectorySeparatorChar & "Logs\Vision_Result\Condition_Checking"

            If Not Directory.Exists(strPath) Then
                Directory.CreateDirectory(strPath)
            End If
            Console.WriteLine(message)
            Dim strFileName As String

            strFileName = "Condition_Checking_" + (DateTime.Now.ToString("yyyyMMdd") & ".csv")


            Dim strPathFileName As String = strPath & Path.DirectorySeparatorChar & strFileName
            If Not File.Exists(strPathFileName) Then
                Using stream As FileStream = New FileStream((strPathFileName), FileMode.CreateNew, FileAccess.Write)
                    Using writer As StreamWriter = New StreamWriter(stream)

                        writer.WriteLine("Date,Time,Step,Rubber Crack,White Dot,Top Cap Close Tight,Cage Close Tight,Overall")

                        writer.WriteLine(message)
                        'writer.Flush()
                    End Using
                End Using
            Else
                Using stream As FileStream = New FileStream((strPathFileName), FileMode.Append, FileAccess.Write)
                    Using writer As StreamWriter = New StreamWriter(stream)
                        writer.WriteLine(message)
                    End Using
                End Using
            End If
        End SyncLock
    End Sub

#End Region

#Region " Cap Vision Result"
    ' Date: 2013-02-01
    ' Author: BK Lo
    ' Description" OEE Logger
    Public Shared Sub CapVisionResult(ByVal message As String)
        SyncLock Logger.obj
            Dim strPath As String

            strPath = System.Windows.Forms.Application.StartupPath & Path.DirectorySeparatorChar & "Logs\Vision_Result\Top_Condition_Checking"

            If Not Directory.Exists(strPath) Then
                Directory.CreateDirectory(strPath)
            End If
            Console.WriteLine(message)
            Dim strFileName As String

            strFileName = "Top_Condition_Checking_" + (DateTime.Now.ToString("yyyyMMdd") & ".csv")


            Dim strPathFileName As String = strPath & Path.DirectorySeparatorChar & strFileName
            If Not File.Exists(strPathFileName) Then
                Using stream As FileStream = New FileStream((strPathFileName), FileMode.CreateNew, FileAccess.Write)
                    Using writer As StreamWriter = New StreamWriter(stream)

                        writer.WriteLine("Date,Time,Step,Inner,Outer,Overall")

                        writer.WriteLine(message)
                        'writer.Flush()
                    End Using
                End Using
            Else
                Using stream As FileStream = New FileStream((strPathFileName), FileMode.Append, FileAccess.Write)
                    Using writer As StreamWriter = New StreamWriter(stream)
                        writer.WriteLine(message)
                    End Using
                End Using
            End If
        End SyncLock
    End Sub

#End Region
End Class
