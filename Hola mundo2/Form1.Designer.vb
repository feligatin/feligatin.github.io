<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnescribir = New System.Windows.Forms.Button()
        Me.txttexto = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'btnescribir
        '
        Me.btnescribir.Location = New System.Drawing.Point(315, 105)
        Me.btnescribir.Name = "btnescribir"
        Me.btnescribir.Size = New System.Drawing.Size(94, 29)
        Me.btnescribir.TabIndex = 0
        Me.btnescribir.Text = "Decir: ""Hola Mundo"""
        Me.btnescribir.UseVisualStyleBackColor = True
        '
        'txttexto
        '
        Me.txttexto.Location = New System.Drawing.Point(153, 56)
        Me.txttexto.Name = "txttexto"
        Me.txttexto.Size = New System.Drawing.Size(530, 27)
        Me.txttexto.TabIndex = 1
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(887, 457)
        Me.Controls.Add(Me.txttexto)
        Me.Controls.Add(Me.btnescribir)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Hola Mundo"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnescribir As Button
    Friend WithEvents txttexto As TextBox
End Class
