Imports System.Collections.Generic
Imports System.Reflection

Public Class Equipamiento
    Inherits IAsociable
    Public Sub New()

    End Sub

    Public Sub New(ByRef eqo As Equipamiento)
        _nombre = eqo._nombre
        _id = eqo._id
        _idPmee = eqo._idPmee
        _idUPZ = eqo._idUPZ
        _distancia = eqo._distancia
        _areaBiblioteca = eqo._areaBiblioteca
        _areaLaboratorio = eqo._areaLaboratorio
        _areaTallerArtes = eqo._areaTallerArtes
        _areaAulaMultimedios = eqo._areaAulaMultimedios
        _areaAulaMultiple = eqo._areaAulaMultiple
        _areaAreaLibre = eqo._areaAreaLibre
        _ofertaBiblioteca = eqo._ofertaBiblioteca
        _ofertaLaboratorios = eqo._ofertaLaboratorios
        _ofertaTallerArtes = eqo._ofertaTallerArtes
        _ofertaAulaMultimedios = eqo._ofertaAulaMultimedios
        _ofertaAulaMultiple = eqo._ofertaAulaMultiple
        _ofertaAreaLibre = eqo._ofertaAreaLibre
        _asociacion = eqo._asociacion
        _procesado = eqo._procesado
    End Sub
    Public Overloads Shared Function CompareByDistance( _
        ByVal obj1 As Equipamiento, _
        ByVal obj2 As Equipamiento _
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
    Public Overrides Function Copiar() As IAsociable
        Return New Equipamiento(Me)
    End Function
End Class

