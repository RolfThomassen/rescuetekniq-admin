// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Linq;
using System.Configuration;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Xml.Linq;
using System.Collections;
using System.Data;
// End of VB project level imports

using RescueTekniq.CODE;

//Imports Microsoft.VisualBasic
//Imports System.net.Mail
//Imports System.Text
//Imports System.Configuration

//Public Class SendEmail

//#Region " Privates "

//    Private SmtpURL As String = ""
//    Private SmtpAcount As String = ""
//    Private SmtpCode As String = ""

//    Private _SendToBCC As Boolean = False
//    Private _BCC As MailAddress = New MailAddress("admin@RescueTekniq.dk", "admin")
//    Private _Fra As MailAddress = New MailAddress("admin@RescueTekniq.dk", "admin")
//    Private _Til As MailAddress '= New MailAddress()
//    Private _Subject As String = ""
//    Private _Body As String = ""
//    Private _FileList As List(Of String)

//    Private _Priority As MailPriority = MailPriority.Normal
//    Private _BodyEncoding As Encoding = Encoding.Default
//    Private _IsBodyHtml As Boolean = True

//#End Region

//#Region " New "

//    Public Sub New()
//        SmtpURL = ConfigurationManager.AppSettings("smtp.server")
//        SmtpAcount = ConfigurationManager.AppSettings("smtp.acount")
//        SmtpCode = ConfigurationManager.AppSettings("smtp.code")

//        '"massmail.scannet.dk"
//        'ConfigurationManager.AppSettings("adminrolename")
//    End Sub

//#End Region

//#Region " Propterties "

//    Public ReadOnly Property FileList() As List(Of String)
//        Get
//            Return _FileList
//        End Get
//    End Property

//    Public Property Subject() As String
//        Get
//            Return _Subject
//        End Get
//        Set(ByVal value As String)
//            _Subject = value
//        End Set
//    End Property

//    Public Property Body() As String
//        Get
//            Return _Body
//        End Get
//        Set(ByVal value As String)
//            _Body = value
//        End Set
//    End Property

//    Public Property IsBodyHtml() As Boolean
//        Get
//            Return _IsBodyHtml
//        End Get
//        Set(ByVal value As Boolean)
//            _IsBodyHtml = value
//        End Set
//    End Property

//    Public Property Priority() As MailPriority
//        Get
//            Return _Priority
//        End Get
//        Set(ByVal value As MailPriority)
//            _Priority = value
//        End Set
//    End Property

//    Public Property BodyEncoding() As Encoding
//        Get
//            Return _BodyEncoding
//        End Get
//        Set(ByVal value As Encoding)
//            _BodyEncoding = value
//        End Set
//    End Property

//    Public Property Fra() As MailAddress
//        Get
//            Return _Fra
//        End Get
//        Set(ByVal value As MailAddress)
//            _Fra = value
//        End Set
//    End Property

//    Public Property Til() As MailAddress
//        Get
//            Return _Til
//        End Get
//        Set(ByVal value As MailAddress)
//            _Til = value
//        End Set
//    End Property

//    Public Property SendToBCC() As Boolean
//        Get
//            Return _SendToBCC
//        End Get
//        Set(ByVal value As Boolean)
//            _SendToBCC = value
//        End Set
//    End Property

//    Public Property BCC() As MailAddress
//        Get
//            Return _BCC
//        End Get
//        Set(ByVal value As MailAddress)
//            _BCC = value
//        End Set
//    End Property

//#End Region

//#Region " Metodes "

//    Public Sub SendEmailMessage(ByVal strFrom As String, ByVal strTo As String, _
//                                ByVal strSubject As String _
//                                )
//        Dim _to(0) As String : _to(0) = strTo
//        Dim _files(0) As String : _files(0) = ""

//        SendEmailMessage(strFrom, _to, strSubject, "", _files)
//    End Sub

//    Public Sub SendEmailMessage(ByVal strFrom As String, ByVal strTo As String, _
//                                ByVal strSubject As String, ByVal strMessage As String _
//                                )
//        Dim _to(0) As String : _to(0) = strTo
//        Dim _files(0) As String : _files(0) = ""

//        SendEmailMessage(strFrom, _to, strSubject, strMessage, _files)
//    End Sub

//    Public Sub SendEmailMessage(ByVal strFrom As String, ByVal strTo As String, _
//                                ByVal strSubject As String, ByVal strMessage As String, _
//                                ByVal file As String _
//                                )
//        Dim _to(0) As String : _to(0) = strTo
//        Dim _files(0) As String : _files(0) = file

//        SendEmailMessage(strFrom, _to, strSubject, strMessage, _files)
//    End Sub

//    Public Sub SendEmailMessage(ByVal strFrom As String, ByVal strTo() As String, _
//                                ByVal strSubject As String, ByVal strMessage As String, _
//                                ByVal file As String _
//                                )
//        'Dim _to(0) As String : _to(0) = strTo
//        Dim _files(0) As String : _files(0) = file

//        SendEmailMessage(strFrom, strTo, strSubject, strMessage, _files)
//    End Sub

//    Public Sub SendEmailMessage(ByVal strFrom As String, ByVal strTo As String, _
//                                ByVal strSubject As String, ByVal strMessage As String, _
//                                ByVal fileList() As String _
//                                )
//        Dim _to(0) As String : _to(0) = strTo
//        'Dim _files(0) As String : _files(0) = file

//        SendEmailMessage(strFrom, _to, strSubject, strMessage, fileList)
//    End Sub

//    Public Sub SendEmailMessage(ByVal strFrom As String, _
//                                ByVal strTo() As String, _
//                                ByVal strSubject As String, _
//                                ByVal strMessage As String, _
//                                ByVal fileList() As String _
//                                )
//        Try
//            For Each item As String In strTo
//                'For each to address create a mail message
//                Dim MailMsg As New MailMessage() 'New MailAddress(strFrom.Trim()), New MailAddress(item)

//                MailMsg.From = New MailAddress(strFrom.Trim)
//                MailMsg.To.Add(New MailAddress(item))
//                If SendToBCC Then MailMsg.Bcc.Add(BCC)

//                MailMsg.ReplyTo = New MailAddress("admin@RescueTekniq.dk")

//                MailMsg.BodyEncoding = BodyEncoding 'Encoding.Default
//                MailMsg.Subject = strSubject.Trim()
//                MailMsg.Body = strMessage.Trim()
//                MailMsg.Priority = Priority
//                MailMsg.IsBodyHtml = True
//                MailMsg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure + DeliveryNotificationOptions.OnSuccess

//                'attach each file attachment
//                For Each strfile As String In fileList
//                    If Not strfile = "" Then
//                        If IO.File.Exists(strfile) Then
//                            Dim MsgAttach As New Attachment(strfile)
//                            MailMsg.Attachments.Add(MsgAttach)
//                        End If
//                    End If
//                Next

//                'Smtpclient to send the mail message
//                Dim SmtpMail As New SmtpClient
//                SmtpMail.Host = SmtpURL
//                SmtpMail.Send(MailMsg)

//                LogLine.LogLine()

//            Next
//            'Message Successful
//        Catch ex As Exception
//            'Message Error
//        End Try
//    End Sub

//    Public Sub SendEmailMessageAsOne(ByVal strFrom As String, _
//                                ByVal strTo() As String, _
//                                ByVal strSubject As String, _
//                                ByVal strMessage As String, _
//                                ByVal fileList() As String _
//                                )
//        Try
//            Dim MailMsg As New MailMessage()
//            MailMsg.From = New MailAddress(strFrom.Trim)
//            If SendToBCC Then MailMsg.Bcc.Add(BCC)

//            'create ONE email message with all TO-address
//            For Each item As String In strTo
//                MailMsg.To.Add(New MailAddress(item))
//            Next

//            MailMsg.BodyEncoding = BodyEncoding
//            MailMsg.Subject = strSubject.Trim()
//            MailMsg.Body = strMessage.Trim()
//            MailMsg.Priority = Priority
//            MailMsg.IsBodyHtml = True

//            'attach each file attachment
//            For Each strfile As String In fileList
//                If Not strfile.Trim = "" Then
//                    If IO.File.Exists(strfile) Then
//                        Dim MsgAttach As New Attachment(strfile.Trim)
//                        MailMsg.Attachments.Add(MsgAttach)
//                    End If
//                End If
//            Next

//            'Smtpclient to send the mail message
//            Dim SmtpMail As New SmtpClient
//            SmtpMail.Host = SmtpURL
//            SmtpMail.Send(MailMsg)

//            'Message Successful
//        Catch ex As Exception
//            'Message Error
//        End Try
//    End Sub

//    Public Sub SendEmail()
//        Try
//            Dim MailMsg As New MailMessage()
//            MailMsg.From = Fra
//            MailMsg.To.Add(Til)
//            If SendToBCC Then MailMsg.Bcc.Add(BCC)

//            MailMsg.BodyEncoding = BodyEncoding
//            MailMsg.Subject = Subject
//            MailMsg.Body = Body
//            MailMsg.Priority = Priority
//            MailMsg.IsBodyHtml = True

//            'attach each file attachment
//            For Each strfile As String In FileList
//                If Not strfile.Trim = "" Then
//                    If IO.File.Exists(strfile) Then
//                        Dim MsgAttach As New Attachment(strfile.Trim)
//                        MailMsg.Attachments.Add(MsgAttach)
//                    End If
//                End If
//            Next

//            'Smtpclient to send the mail message
//            Dim SmtpMail As New SmtpClient
//            SmtpMail.Host = SmtpURL
//            SmtpMail.Send(MailMsg)


//            'Message Successful
//        Catch ex As Exception
//            'Message Error
//        End Try
//    End Sub

//#End Region

//End Class

