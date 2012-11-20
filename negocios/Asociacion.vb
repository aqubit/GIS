Public Class Asociacion
    Inherits IAsociable

    Protected _elementos As List(Of IAsociable)
    Protected Shared _seed As Integer
    Sub New()
        _elementos = New List(Of IAsociable)
        _seed += 1
        _idPmee = _seed
    End Sub
    Private Sub New(ByRef aso As Asociacion)
        _elementos = New List(Of IAsociable)(aso.elementos.Count)
        For Each eqo As IAsociable In aso.elementos
            _elementos.Add(eqo.Copiar)
        Next
        _idPmee = aso._idPmee
        _id = aso._id
        _procesado = aso._procesado
        _asociacion = aso._asociacion
    End Sub

    Public Overrides Property nombre() As String
        Get
            Return "Asociación"
        End Get
        Set(ByVal value As String)
            'Do nothing
        End Set
    End Property
    Public Overrides ReadOnly Property deficitTotalAreaConstruida() As Integer
        Get
            Dim total As Integer
            For Each el As IAsociable In _elementos
                total += el.deficitTotalAreaConstruida
            Next el
            Return total
        End Get
    End Property
    Public Overrides ReadOnly Property deficitTotalAreaLibre() As Integer
        Get
            Dim total As Integer
            For Each el As IAsociable In _elementos
                total += el.deficitTotalAreaLibre
            Next el
            Return total
        End Get
    End Property

    Public Overrides Property ofertaLaboratorio() As Integer
        Get
            Dim total As Integer
            For Each el As IAsociable In _elementos
                total += el.ofertaLaboratorio
            Next el
            Return total
        End Get
        Set(ByVal value As Integer)

        End Set
    End Property

    Public Overrides Property ofertaTallerArtes() As Integer
        Get
            Dim total As Integer
            For Each el As IAsociable In _elementos
                total += el.ofertaTallerArtes
            Next el
            Return total
        End Get
        Set(ByVal value As Integer)

        End Set
    End Property
    Public Overrides Property ofertaAulaMultimedios() As Integer
        Get
            Dim total As Integer
            For Each el As IAsociable In _elementos
                total += el.ofertaAulaMultimedios
            Next el
            Return total
        End Get
        Set(ByVal value As Integer)

        End Set
    End Property
    Public Overrides Property ofertaAulaMultiple() As Integer
        Get
            Dim total As Integer
            For Each el As IAsociable In _elementos
                total += el.ofertaAulaMultiple
            Next el
            Return total
        End Get
        Set(ByVal value As Integer)

        End Set
    End Property
    Public Overrides Property ofertaAreaLibre() As Integer
        Get
            Dim total As Integer
            For Each el As IAsociable In _elementos
                total += el.ofertaAreaLibre
            Next el
            Return total
        End Get
        Set(ByVal value As Integer)

        End Set
    End Property
    Public Overrides Property ofertaBiblioteca() As Integer
        Get
            Dim total As Integer
            For Each el As IAsociable In _elementos
                total += el.ofertaBiblioteca
            Next el
            Return total
        End Get
        Set(ByVal value As Integer)

        End Set
    End Property
    Public Overrides ReadOnly Property elementos() As List(Of IAsociable)
        Get
            Return _elementos
        End Get
    End Property
    Public Sub Add(ByRef el As IAsociable)
        If el IsNot Nothing Then
            _elementos.Add(el)
        End If
    End Sub
    Public Sub AddRange(ByRef lista As List(Of IAsociable))
        If lista IsNot Nothing Then
            For Each el As IAsociable In lista
                If el IsNot Nothing Then
                    _elementos.Add(el)
                End If
            Next
        End If
    End Sub
    'Añade los elementos de la asociación aso a los elementos de la asociación actual
    Public Sub addAsociacion(ByRef aso As Asociacion)
        If aso IsNot Nothing Then
            _elementos.AddRange(aso.elementos)
        End If
    End Sub
    Public Sub Sort(ByRef comparador As IComparer(Of IAsociable))
        _elementos.Sort(comparador)
    End Sub
    Public Function FindAll(ByVal predicate As System.Predicate(Of IAsociable)) As List(Of IAsociable)
        Return _elementos.FindAll(predicate)
    End Function
    Public Function Find(ByRef obj As IAsociable) As IAsociable
        Dim aso, target As IAsociable
        target = Nothing
        If obj IsNot Nothing Then
            For Each aso In _elementos
                If aso.Equals(obj) Then
                    target = aso
                End If
            Next
        End If
        Return target
    End Function
    Public Overrides Function Copiar() As IAsociable
        Return New Asociacion(Me)
    End Function
    Public Overrides Property procesado() As Boolean
        Get
            Return _procesado
        End Get
        Set(ByVal value As Boolean)
            For Each el As IAsociable In _elementos
                el.procesado = value
            Next
            _procesado = value
        End Set
    End Property

    Public Overrides Sub TraceConsole()
        Debug.Print("------------------------Asociación-----------------------------------------")
        Debug.Print("nombre : " & Me.nombre & " " & Me.idPmee)
        Debug.Print("Biblioteca : " & Me.ofertaBiblioteca)
        Debug.Print("Aula multimedios : " & Me.ofertaAulaMultimedios)
        Debug.Print("Aula multiple : " & Me.ofertaAulaMultiple)
        Debug.Print("Laboratorio : " & Me.ofertaLaboratorio)
        Debug.Print("Taller de artes : " & Me.ofertaTallerArtes)
        Debug.Print("Area libre : " & Me.ofertaAreaLibre)
        Debug.Print("")
        For Each el As IAsociable In _elementos
            el.TraceConsole()
            Debug.Print("")
        Next
        Debug.Print("--------------------------------------------------------------------------")
    End Sub


End Class
