Public Class AIE
    Inherits IAsociable
    Private _eqo1 As IAsociable
    Private _eqo2 As IAsociable
    Private _relaciones As List(Of Relacion)
    Private Shared _seed As Integer
    Sub New()
        _relaciones = New List(Of Relacion)
        _seed += 1
        _idPmee = _seed
    End Sub
    Sub New(ByRef el As AIE)
        _eqo1 = el.eqo1
        _eqo2 = el.eqo2
        _relaciones = New List(Of Relacion)(el.relaciones)
        _idPmee = el._idPmee
        _id = el._id
    End Sub
    Public Property eqo1() As IAsociable
        Get
            Return _eqo1
        End Get
        Set(ByVal value As IAsociable)
            _eqo1 = value
        End Set
    End Property

    Public Property eqo2() As IAsociable
        Get
            Return _eqo2
        End Get
        Set(ByVal value As IAsociable)
            _eqo2 = value
        End Set
    End Property

    Public ReadOnly Property relaciones() As List(Of Relacion)
        Get
            Return _relaciones
        End Get
    End Property

    Public Function existeRelacion(ByRef ambiente As String) As Boolean
        Dim result As Boolean = False
        For Each rel As Relacion In _relaciones
            If rel.ambiente.Equals(ambiente) Then
                result = True
                Exit For
            End If
        Next
        Return result
    End Function

    Public Overrides Property ofertaLaboratorio() As Integer
        Get
            Return (eqo1.ofertaLaboratorio + eqo2.ofertaLaboratorio)
        End Get
        Set(ByVal value As Integer)
            eqo1.ofertaLaboratorio += value
            eqo2.ofertaLaboratorio -= value
        End Set
    End Property

    Public Overrides Property ofertaTallerArtes() As Integer
        Get
            Return (eqo1.ofertaTallerArtes + eqo2.ofertaTallerArtes)
        End Get
        Set(ByVal value As Integer)
            eqo1.ofertaTallerArtes += value
            eqo2.ofertaTallerArtes -= value
        End Set
    End Property
    Public Overrides Property ofertaAulaMultimedios() As Integer
        Get
            Return (eqo1.ofertaAulaMultimedios + eqo2.ofertaAulaMultimedios)
        End Get
        Set(ByVal value As Integer)
            eqo1.ofertaAulaMultimedios += value
            eqo2.ofertaAulaMultimedios -= value
        End Set
    End Property
    Public Overrides Property ofertaAulaMultiple() As Integer
        Get
            Return (eqo1.ofertaAulaMultiple + eqo2.ofertaAulaMultiple)
        End Get
        Set(ByVal value As Integer)
            eqo1.ofertaAulaMultiple += value
            eqo2.ofertaAulaMultiple -= value
        End Set
    End Property
    Public Overrides Property ofertaAreaLibre() As Integer
        Get
            Return (eqo1.ofertaAreaLibre + eqo2.ofertaAreaLibre)
        End Get
        Set(ByVal value As Integer)
            eqo1.ofertaAreaLibre += value
            eqo2.ofertaAreaLibre -= value
        End Set
    End Property
    Public Overrides Property ofertaBiblioteca() As Integer
        Get
            Return (eqo1.ofertaBiblioteca + eqo2.ofertaBiblioteca)
        End Get
        Set(ByVal value As Integer)
            eqo1.ofertaBiblioteca += value
            eqo2.ofertaBiblioteca -= value
        End Set
    End Property
    ''' <param name="obj">The <see cref="T:System.Object"></see> to compare with the current <see cref="T:System.Object"></see>.</param>
    ''' <returns>true if the specified <see cref="T:System.Object"></see> is equal to the current <see cref="T:System.Object"></see>; otherwise, false.</returns>
    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        Dim result As Boolean = False
        If (obj IsNot Nothing) And (TypeOf (obj) Is AIE) Then
            Dim x As AIE
            x = obj
            If (eqo1.idPmee = x.eqo1.idPmee) And (eqo2.idPmee = x.eqo2.idPmee) Then
                result = True
            End If
        End If
        Return result
    End Function
    Public Overrides Function Copiar() As IAsociable
        Dim nuevo As New AIE(Me)
        Return nuevo
    End Function
    Public Overrides Property procesado() As Boolean
        Get
            Return _procesado
        End Get
        Set(ByVal value As Boolean)
            _procesado = value
        End Set
    End Property
    Public Shared Function existeRelacion( _
        ByRef aies As List(Of IAsociable), _
        ByRef target As AIE, _
        ByRef strAmbiente As String _
    ) As Boolean
        Dim ruta As AIE
        For Each el As IAsociable In aies
            ruta = el
            For Each rel As Relacion In ruta.relaciones
                If ((rel.ambiente = strAmbiente) And _
                     (ruta.eqo1 Is target.eqo1) And _
                     (ruta.eqo2 Is target.eqo2) _
                ) Then
                    Return True
                End If
            Next
        Next
        Return False
    End Function
End Class

