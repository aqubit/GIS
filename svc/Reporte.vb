Imports System.Reflection

Public Class Reporte
    Private _worksheet As Excel.Worksheet
    Private _geoAdapter As GeoAdapter
    Private _estandar As Estandar
    Private _color As Integer = RGB(249, 247, 166)
    Private _color2 As Integer = RGB(250, 185, 169)

    Sub New(ByRef geo As GeoAdapter, ByRef est As Estandar, ByRef worksheet As Excel.Worksheet)
        _worksheet = worksheet
        _geoAdapter = geo
        _estandar = est
    End Sub
    Public Sub doReportexUPZ( _
            ByRef enuUPZs As IEnumerator, _
            ByRef iFila As Integer, _
            ByRef label As String _
    )
        Dim iNumUPZ As Integer

        Dim asociaciones As Asociacion = global_redNodal.asociaciones
        Dim aies As Asociacion = global_redNodal.aies

        While enuUPZs.MoveNext()
            iNumUPZ = CInt(_geoAdapter.leerTablaUPZ("NUMERO", "NOMBRE = '" & enuUPZs.Current & "'", TipoConsulta.Atributo).Item(0))
            'Indicar qué elementos pertenecen a la UPZ
            aies.procesado = False
            For Each el As IAsociable In aies.elementos
                If el.upz = iNumUPZ Then
                    el.procesado = False
                Else
                    el.procesado = True
                End If
            Next
            For Each el As IAsociable In asociaciones.elementos
                If el.upz = iNumUPZ Then
                    el.procesado = False
                Else
                    el.procesado = True
                End If
            Next
            iFila += 2
            _worksheet.Cells(iFila, 1) = String.Format("UPZ : {0} ", enuUPZs.Current)
            iFila += 1
            doReporte(asociaciones, aies, iFila, label)
        End While

    End Sub

    Public Sub doReportexAFE( _
            ByRef iFila As Integer, _
            ByRef label As String _
    )
        Dim asociaciones As Asociacion = global_redNodal.asociaciones
        Dim aies As Asociacion = global_redNodal.aies

        aies.procesado = False
        asociaciones.procesado = False
        doReporte(asociaciones, aies, iFila, label)
    End Sub

    Public Sub doReporte( _
            ByRef asociaciones As Asociacion, _
            ByRef aies As Asociacion, _
            ByRef iFila As Integer, _
            ByRef label As String _
    )
        Dim iBorderStart, iBorder2Start As Integer
        Dim excelRange As Excel.Range
        Dim noasociados As New List(Of IAsociable)(2)
        'Duplicar el arreglo
        Dim copiaAies As New List(Of AIE)(aies.elementos.Count)
        For Each el As IAsociable In aies.elementos
            copiaAies.Add(CType(el, AIE))
        Next

        iFila += 1
        _worksheet.Cells(iFila, 1) = String.Format("Asociaciones peatonales de {0} ", label)

        For Each aso As IAsociable In asociaciones.elementos
            If Not aso.procesado Then
                iFila += 2
                iBorderStart = iFila
                doReporteElemento(aso, iFila, 1)
                For Each eqo As IAsociable In aso.elementos
                    iBorder2Start = iFila + 1
                    doReporteElemento(eqo, iFila, 2)
                    excelRange = _worksheet.Range(_worksheet.Cells(iBorder2Start, 2), _worksheet.Cells(iFila, 8))
                    excelRange.BorderAround()
                    excelRange.Interior.Color = _color
                    iBorder2Start = iFila + 1
                    doReporteRutas(eqo, copiaAies, iFila, 2)
                    noasociados.Remove(eqo)
                    If iBorder2Start <> (iFila + 1) Then
                        excelRange = _worksheet.Range(_worksheet.Cells(iBorder2Start, 2), _worksheet.Cells(iFila, 8))
                        excelRange.BorderAround()
                        excelRange.Interior.Color = _color2
                    End If
                Next
                excelRange = _worksheet.Range(_worksheet.Cells(iBorderStart, 1), _worksheet.Cells(iFila, 9))
                excelRange.BorderAround()
            Else
                For Each eqo As IAsociable In aso.elementos
                    doRemoveRutas(eqo, copiaAies)
                    noasociados.Remove(eqo)
                Next
            End If
        Next
        iFila += 1
        _worksheet.Cells(iFila, 1) = String.Format("{0} asociados vehicularmente", label)
        For Each el As AIE In copiaAies
            If Not el.procesado Then
                iBorderStart = iFila + 1
                doReporteElemento(el.eqo1, iFila, 2)
                excelRange = _worksheet.Range(_worksheet.Cells(iBorderStart, 2), _worksheet.Cells(iFila, 8))
                excelRange.BorderAround()
                excelRange.Interior.Color = _color
                iBorderStart = iFila + 1
                doReporteRutas(el.eqo1, copiaAies, iFila, 2)
                If iBorderStart <> (iFila + 1) Then
                    excelRange = _worksheet.Range(_worksheet.Cells(iBorderStart, 2), _worksheet.Cells(iFila, 8))
                    excelRange.BorderAround()
                    excelRange.Interior.Color = _color2
                End If
                noasociados.Remove(el.eqo1)
                noasociados.Remove(el.eqo2)
            Else
                noasociados.Remove(el.eqo1)
                noasociados.Remove(el.eqo2)
            End If
        Next
        iFila += 1
        _worksheet.Cells(iFila, 1) = String.Format("{0} no asociados", label)
        For Each el As IAsociable In noasociados
            If Not el.procesado Then
                iBorderStart = iFila + 1
                doReporteElemento(el, iFila, 2)
                excelRange = _worksheet.Range(_worksheet.Cells(iBorderStart, 2), _worksheet.Cells(iFila, 8))
                excelRange.BorderAround()
                excelRange.Interior.Color = _color
            End If
        Next
    End Sub

    Private Sub doReporteRutas( _
        ByRef ele As IAsociable, _
        ByRef aies As List(Of AIE), _
        ByRef iFila As Integer, _
        ByRef iColumna As Integer _
    )
        Dim aiesEle As New List(Of AIE)
        For Each ruta As AIE In aies
            If ruta.eqo1.Equals(ele) Then aiesEle.Add(ruta)
        Next
        For Each ruta As AIE In aiesEle
            iFila += 1
            _worksheet.Cells(iFila, iColumna) = "Utiliza ->"
            _worksheet.Cells(iFila, iColumna + 1) = ruta.eqo2.nombre & " id_pmee :" & "(" & ruta.eqo2.idPmee & ")"
            iFila += 1
            _worksheet.Cells(iFila, iColumna) = "Distancia (m):"
            _worksheet.Cells(iFila, iColumna + 1) = ruta.distancia
            For Each rel As Relacion In ruta.relaciones
                iFila += 1
                _worksheet.Cells(iFila, iColumna + 1) = "Ambiente : " & rel.ambiente
                iFila += 1
                _worksheet.Cells(iFila, iColumna + 2) = "M2 : " & rel.cantidad
                iFila += 1
                If rel.tipo = TipoAsociacion.Peatonal Then
                    _worksheet.Cells(iFila, iColumna + 2) = "Tipo : Peatonal"
                Else
                    _worksheet.Cells(iFila, iColumna + 2) = "Tipo : Vehicular"
                End If
            Next
            aies.Remove(ruta)
        Next
    End Sub

    Private Sub doReporteElemento( _
        ByRef eqo As IAsociable, _
        ByRef iFila As Integer, _
        ByRef iColumna As Integer _
    )
        Dim col As Colegio = Nothing
        Dim iBorderStart As Integer

        If TypeOf (eqo) Is Colegio Then
            col = eqo
        End If

        iFila += 2
        iBorderStart = iFila
        _worksheet.Cells(iFila, iColumna) = "Nombre : " & eqo.nombre
        iFila += 1
        _worksheet.Cells(iFila, iColumna) = "id_pmee : " & eqo.idPmee
        iFila += 1
        If col IsNot Nothing Then
            Dim strCaracter As String = "Privado"
            If col.idCaracter = CaracterEqo.Oficial Then
                strCaracter = "Oficial"
            End If
            _worksheet.Cells(iFila, iColumna) = "Carácter : " & strCaracter
            iFila += 1
            _worksheet.Cells(iFila, iColumna) = "Índice de construcción"
            _worksheet.Cells(iFila, iColumna + 1) = col.indiceConstruccion
            iFila += 1
            _worksheet.Cells(iFila, iColumna) = "Índice de ocupación"
            _worksheet.Cells(iFila, iColumna + 1) = col.indiceOcupacion
        End If
        If eqo.elementos IsNot Nothing Then
            iFila += 1
            _worksheet.Cells(iFila, iColumna) = "Número de elementos : " & eqo.elementos.Count
        End If
        iFila += 1
        _worksheet.Cells(iFila, iColumna + 2) = "Superávit"
        _worksheet.Cells(iFila, iColumna + 3) = "Déficit"
        iFila += 1
        _worksheet.Cells(iFila, iColumna + 4) = "Sede"
        If eqo.getSavedState() IsNot Nothing Then
            _worksheet.Cells(iFila, iColumna + 5) = "Asociación"
            _worksheet.Cells(iFila, iColumna + 6) = "Cubierto en asociación"
        End If
        iFila += 1
        _worksheet.Cells(iFila, iColumna) = "Ambiente"
        _worksheet.Cells(iFila, iColumna + 2) = "m2"
        _worksheet.Cells(iFila, iColumna + 3) = "m2"
        If eqo.getSavedState() IsNot Nothing Then
            _worksheet.Cells(iFila, iColumna + 4) = "m2"
            _worksheet.Cells(iFila, iColumna + 5) = "m2"
        End If
        'Biblioteca
        iFila += 1
        _worksheet.Cells(iFila, iColumna) = "Biblioteca"
        doReporteAmbiente(eqo, iFila, iColumna, "ofertaBiblioteca")
        'Laboratorio
        iFila += 1
        _worksheet.Cells(iFila, iColumna) = "Laboratorio"
        doReporteAmbiente(eqo, iFila, iColumna, "ofertaLaboratorio")
        'Taller de artes
        iFila += 1
        _worksheet.Cells(iFila, iColumna) = "Taller de artes"
        doReporteAmbiente(eqo, iFila, iColumna, "ofertaTallerArtes")
        'Aula múltiple
        iFila += 1
        _worksheet.Cells(iFila, iColumna) = "Aula múltiple"
        doReporteAmbiente(eqo, iFila, iColumna, "ofertaAulaMultiple")
        'Aula multimedios
        iFila += 1
        _worksheet.Cells(iFila, iColumna) = "Aula multimedios"
        doReporteAmbiente(eqo, iFila, iColumna, "ofertaAulaMultimedios")
        'Área libre
        iFila += 1
        _worksheet.Cells(iFila, iColumna) = "Área libre"
        doReporteAmbiente(eqo, iFila, iColumna, "ofertaAreaLibre")
    End Sub
    Private Sub doReporteAmbiente( _
        ByRef eqo As IAsociable, _
        ByRef iFila As Integer, _
        ByRef iColumna As Integer, _
        ByRef ambiente As String _
    )
        Dim type As Type = GetType(IAsociable)
        Dim propertyInfo As PropertyInfo
        Dim defSede, defAsociacion, defFinal, defTotal As Integer
        Dim eqoAntes As IAsociable = eqo.getSavedState()
        Dim eqoDefAso As IAsociable
        Dim eqoDespues As IAsociable = eqo


        propertyInfo = type.GetProperty(ambiente)
        If eqoAntes IsNot Nothing Then
            eqoDefAso = eqoAntes.Copiar()
            defTotal = propertyInfo.GetValue(eqoDefAso, Nothing)
            _estandar.doObtenerDeficitAsociacion(eqoDefAso)
            defAsociacion = propertyInfo.GetValue(eqoDefAso, Nothing)
            defFinal = propertyInfo.GetValue(eqoDespues, Nothing)
            If defTotal >= 0 Then
                defTotal = propertyInfo.GetValue(eqoDefAso, Nothing)
                _worksheet.Cells(iFila, iColumna + 2) = defTotal & " | " & defFinal
            Else
                defSede = defTotal - defAsociacion
                _worksheet.Cells(iFila, iColumna + 3) = defTotal
                _worksheet.Cells(iFila, iColumna + 4) = defSede
                _worksheet.Cells(iFila, iColumna + 5) = defAsociacion
                _worksheet.Cells(iFila, iColumna + 6) = Math.Abs(defAsociacion - defFinal)
            End If
        End If
    End Sub

    Private Sub doRemoveRutas( _
            ByRef ele As IAsociable, _
            ByRef aies As List(Of AIE) _
        )
        Dim aiesEle As New List(Of AIE)
        For Each ruta As AIE In aies
            If ruta.eqo1.Equals(ele) Then aiesEle.Add(ruta)
        Next
        For Each ruta As AIE In aiesEle
            aies.Remove(ruta)
        Next
    End Sub

End Class
