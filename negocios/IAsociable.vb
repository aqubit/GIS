Imports System.Collections.Generic
Imports System.Reflection
Public MustInherit Class IAsociable
    Implements IComparable

    Protected _nombre As String
    Protected _id As Integer
    Protected _idPmee As Integer
    Protected _idUPZ As Integer
    Protected _distancia As Double
    Protected _areaBiblioteca As Integer
    Protected _areaLaboratorio As Integer
    Protected _areaTallerArtes As Integer
    Protected _areaAulaMultimedios As Integer
    Protected _areaAulaMultiple As Integer
    Protected _areaAreaLibre As Integer
    'Los ambientes compartidos
    Protected _ofertaBiblioteca As Integer
    Protected _ofertaLaboratorios As Integer
    Protected _ofertaTallerArtes As Integer
    Protected _ofertaAulaMultimedios As Integer
    Protected _ofertaAulaMultiple As Integer
    Protected _ofertaAreaLibre As Integer
    'La asociación a la que pertenece
    Protected _asociacion As Asociacion
    Protected _procesado As Boolean
    Protected _savedState As IAsociable

    Public Property upz() As Integer
        Get
            Return _idUPZ
        End Get
        Set(ByVal value As Integer)
            _idUPZ = value
        End Set
    End Property

    Public Overridable ReadOnly Property deficitTotalAreaConstruida() As Integer
        Get
            Return 0
        End Get
    End Property
    Public Overridable ReadOnly Property deficitTotalAreaLibre() As Integer
        Get
            Return 0
        End Get
    End Property
    Public Overridable Property asociacion() As Asociacion
        Get
            Return _asociacion
        End Get
        Set(ByVal value As Asociacion)
            _asociacion = value
        End Set
    End Property

    Public Overridable Property idPmee() As Integer
        Get
            Return _idPmee
        End Get
        Set(ByVal value As Integer)
            _idPmee = value
        End Set
    End Property

    Public Overridable Property nombre() As String
        Get
            Return _nombre
        End Get
        Set(ByVal value As String)
            _nombre = value
        End Set
    End Property
    Public Overridable Property areaBiblioteca() As Integer
        Get
            Return _areaBiblioteca
        End Get
        Set(ByVal value As Integer)
            _areaBiblioteca = value
        End Set
    End Property

    Public Overridable Property areaLaboratorio() As Integer
        Get
            Return _areaLaboratorio
        End Get
        Set(ByVal value As Integer)
            _areaLaboratorio = value
        End Set
    End Property

    Public Overridable Property areaTallerArtes() As Integer
        Get
            Return _areaTallerArtes
        End Get
        Set(ByVal value As Integer)
            _areaTallerArtes = value
        End Set
    End Property
    Public Overridable Property areaAulaMultimedios() As Integer
        Get
            Return _areaAulaMultimedios
        End Get
        Set(ByVal value As Integer)
            _areaAulaMultimedios = value
        End Set
    End Property
    Public Overridable Property areaAulaMultiple() As Integer
        Get
            Return _areaAulaMultiple
        End Get
        Set(ByVal value As Integer)
            _areaAulaMultiple = value
        End Set
    End Property
    Public Overridable Property areaAreaLibre() As Integer
        Get
            Return _areaAreaLibre
        End Get
        Set(ByVal value As Integer)
            _areaAreaLibre = value
        End Set
    End Property

    Public Overridable Property ofertaLaboratorio() As Integer
        Get
            Return _ofertaLaboratorios
        End Get
        Set(ByVal value As Integer)
            _ofertaLaboratorios = value
        End Set
    End Property

    Public Overridable Property ofertaTallerArtes() As Integer
        Get
            Return _ofertaTallerArtes
        End Get
        Set(ByVal value As Integer)
            _ofertaTallerArtes = value
        End Set
    End Property
    Public Overridable Property ofertaAulaMultimedios() As Integer
        Get
            Return _ofertaAulaMultimedios
        End Get
        Set(ByVal value As Integer)
            _ofertaAulaMultimedios = value
        End Set
    End Property
    Public Overridable Property ofertaAulaMultiple() As Integer
        Get
            Return _ofertaAulaMultiple
        End Get
        Set(ByVal value As Integer)
            _ofertaAulaMultiple = value
        End Set
    End Property
    Public Overridable Property ofertaAreaLibre() As Integer
        Get
            Return _ofertaAreaLibre
        End Get
        Set(ByVal value As Integer)
            _ofertaAreaLibre = value
        End Set
    End Property
    Public Overridable Property ofertaBiblioteca() As Integer
        Get
            Return _ofertaBiblioteca
        End Get
        Set(ByVal value As Integer)
            _ofertaBiblioteca = value
        End Set
    End Property
    Public Overridable Property distancia() As Double
        Get
            Return _distancia
        End Get
        Set(ByVal value As Double)
            _distancia = value
        End Set
    End Property
    Public Overridable ReadOnly Property elementos() As List(Of IAsociable)
        Get
            Return Nothing
        End Get
    End Property

    'Por defecto comparar por ID
    Public Function CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
        Dim result As Integer
        Dim eqo2 As IAsociable
        eqo2 = obj
        result = 0
        If eqo2 Is Nothing Then
            result = 1
        Else
            If _idPmee < eqo2.idPmee Then
                result = -1
            ElseIf _idPmee > eqo2.idPmee Then
                result = 1
            Else
                result = 0
            End If
        End If
        Return result
    End Function

    ''' <param name="obj">The <see cref="T:System.Object"></see> to compare with the current <see cref="T:System.Object"></see>.</param>
    ''' <returns>true if the specified <see cref="T:System.Object"></see> is equal to the current <see cref="T:System.Object"></see>; otherwise, false.</returns>
    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        Dim result As Boolean = False
        If (obj IsNot Nothing) And (TypeOf (obj) Is IAsociable) Then
            Dim x As IAsociable
            x = obj
            If idPmee = x.idPmee Then
                result = True
            End If
        End If
        Return result
    End Function
    'Precondiciones:
    '1. La lista de colegios viene ordenada por ID
    Public Shared Function buscarElemento( _
        ByRef target As IAsociable, _
        ByRef eqos As List(Of IAsociable) _
    ) As IAsociable
        Dim result As Integer
        buscarElemento = Nothing
        If eqos IsNot Nothing Then
            result = eqos.BinarySearch(target)
            If result >= 0 And result < eqos.Count Then
                buscarElemento = eqos.Item(result)
            End If
        End If
    End Function
    Public Shared Function CompareByDistance( _
        ByVal obj1 As IAsociable, _
        ByVal obj2 As IAsociable _
    ) As Integer
        Dim result As Integer
        Dim value1 As Double
        Dim value2 As Double
        value1 = obj1.distancia
        value2 = obj2.distancia
        result = 0
        If obj1 Is Nothing Then
            If obj2 Is Nothing Then
                result = 0
            Else
                result = -1
            End If
        Else
            If obj2 Is Nothing Then
                result = 1
            Else
                If value1 < value2 Then
                    result = -1
                ElseIf value1 > value2 Then
                    result = 1
                Else
                    result = 0
                End If
            End If
        End If
        Return result
    End Function
    Public Overridable Property procesado() As Boolean
        Get
            Return _procesado
        End Get
        Set(ByVal value As Boolean)
            _procesado = value
        End Set
    End Property
    Public MustOverride Function Copiar() As IAsociable 'Deep copy
    Public Overridable Function getSavedState() As IAsociable
        Return _savedState
    End Function
    Public Overridable Sub saveState()
        _savedState = Me.Copiar()
    End Sub
    Public Overridable Sub TraceConsole()
        Debug.Print("*****************************************************************")
        Debug.Print("nombre : " & Me.nombre & " " & Me.idPmee)
        Debug.Print("Biblioteca : " & Me.ofertaBiblioteca)
        Debug.Print("Aula multimedios : " & Me.ofertaAulaMultimedios)
        Debug.Print("Aula multiple : " & Me.ofertaAulaMultiple)
        Debug.Print("Laboratorio : " & Me.ofertaLaboratorio)
        Debug.Print("Taller de artes : " & Me.ofertaTallerArtes)
        Debug.Print("Area libre : " & Me.ofertaAreaLibre)
        Debug.Print("*****************************************************************")
    End Sub
End Class

