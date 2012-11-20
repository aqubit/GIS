Imports System.Reflection
Public Enum TipoOrdenamiento
    Ascendente = 0
    Descendente = 1
End Enum

Public NotInheritable Class GenericSorter(Of T)
    Implements IComparer(Of T)
    Private _criterio As String
    Private _tipoOrdenamiento As TipoOrdenamiento
    Public Property tipoOrdenamiento() As TipoOrdenamiento
        Get
            Return _tipoOrdenamiento
        End Get
        Set(ByVal value As TipoOrdenamiento)
            _tipoOrdenamiento = value
        End Set
    End Property
    Public Property criterio() As String
        Get
            Return _criterio
        End Get
        Set(ByVal value As String)
            _criterio = value
        End Set
    End Property
    Sub New(ByRef atributo As String, ByRef tipo As TipoOrdenamiento)
        _criterio = atributo
        _tipoOrdenamiento = tipoOrdenamiento
    End Sub
    Sub New(ByRef atributo As String)
        _criterio = atributo
        _tipoOrdenamiento = 0
    End Sub

    Public Function Compare( _
            ByVal obj1 As T, _
            ByVal obj2 As T _
    ) As Integer Implements IComparer(Of T).Compare
        Dim result As Integer
        Dim value1 As Integer
        Dim value2 As Integer
        Dim propertyInfo As PropertyInfo
        Dim type As Type = GetType(Equipamiento)
        propertyInfo = type.GetProperty(_criterio)
        value1 = propertyInfo.GetValue(obj1, Nothing)
        value2 = propertyInfo.GetValue(obj2, Nothing)
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
        If _tipoOrdenamiento = 1 Then
            result *= -1
        End If
        Return result
    End Function
End Class

