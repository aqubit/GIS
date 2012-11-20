Public Class Nodo
    Inherits Asociacion
    Sub New()
        _elementos = New List(Of IAsociable)
        _seed += 1
        _idPmee = _seed
    End Sub

    Sub New(ByRef aNodo As Nodo)
        _elementos = New List(Of IAsociable)(aNodo.elementos.Count)
        For Each eqo As IAsociable In aNodo.elementos
            _elementos.Add(eqo.Copiar)
        Next
        _idPmee = aNodo._idPmee
        _id = aNodo._id
        _procesado = aNodo._procesado
        _asociacion = aNodo._asociacion
    End Sub
    Public Overrides Property nombre() As String
        Get
            Return "Nodo"
        End Get
        Set(ByVal value As String)
            'Do nothing
        End Set
    End Property
    Public Overrides Function Copiar() As IAsociable
        Return New Nodo(Me)
    End Function

    Public Overrides Property ofertaLaboratorio() As Integer
        Get
            Dim total As Integer
            For Each el As IAsociable In _elementos
                total += el.ofertaLaboratorio
            Next el
            Return total
        End Get
        Set(ByVal value As Integer)
            For Each el As IAsociable In _elementos
                If el.ofertaLaboratorio < 0 Then
                    el.ofertaLaboratorio = 0
                End If
            Next el
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
            For Each el As IAsociable In _elementos
                If el.ofertaTallerArtes < 0 Then
                    el.ofertaTallerArtes = 0
                End If
            Next el
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
            For Each el As IAsociable In _elementos
                If el.ofertaAulaMultimedios < 0 Then
                    el.ofertaAulaMultimedios = 0
                End If
            Next el
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
            For Each el As IAsociable In _elementos
                If el.ofertaAulaMultiple < 0 Then
                    el.ofertaAulaMultiple = 0
                End If
            Next el
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
            For Each el As IAsociable In _elementos
                If el.ofertaAreaLibre < 0 Then
                    el.ofertaAreaLibre = 0
                End If
            Next el
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
            For Each el As IAsociable In _elementos
                If el.ofertaBiblioteca < 0 Then
                    el.ofertaBiblioteca = 0
                End If
            Next el
        End Set
    End Property
    Public Overrides Sub TraceConsole()
        Debug.Print("------------------------Nodo-----------------------------------------")
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
        Debug.Print("---------------------------------------------------------------------")
    End Sub
End Class
