Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.NetworkAnalyst
Imports ESRI.ArcGIS.esriSystem


Module NA_Solver

    Public Function doSolve(ByVal pNAContext As INAContext, ByVal pGPMessages As IGPMessages) As String
        On Error GoTo FAIL

        'Resolver el problema

        doSolve = "Error en la solución de la ruta"
        Dim IsPartialSolution As Boolean
        IsPartialSolution = pNAContext.Solver.Solve(pNAContext, pGPMessages, Nothing)

        If IsPartialSolution = False Then
            doSolve = "OK"
        Else
            doSolve = "Solución Parcial"
        End If

        Exit Function
FAIL:

        If Err.Number Then
            doSolve = doSolve & " Error # " & Str(Err.Number) & " Descripción " & Err.Description
        End If
    End Function

    '*********************************************************************************
    ' Configuración del solucionador de rutas (Solver)
    '*********************************************************************************
    Public Sub setSolverSettings(ByRef pContext As INAContext, ByVal sImpedanceName As String, ByVal bOneWay As Boolean, ByVal bUseHierarchy As Boolean, ByVal bBestSQ As Boolean)

        'Configurar
        Try
            Dim pSolver As INASolver
            pSolver = pContext.Solver

            Dim pRteSolver As INARouteSolver
            pRteSolver = pSolver

            pRteSolver.OutputLines = esriNAOutputLineType.esriNAOutputLineTrueShapeWithMeasure
            pRteSolver.CreateTraversalResult = True
            pRteSolver.UseTimeWindows = False
            pRteSolver.FindBestSequence = False
            pRteSolver.PreserveFirstStop = False
            pRteSolver.PreserveLastStop = False

            'Asignar un solucionador de rutas genérico
            'Asignar impedancia 
            Dim pSolverSettings As INASolverSettings
            pSolverSettings = pSolver
            pSolverSettings.ImpedanceAttributeName = sImpedanceName

            ' Asignar restricciones
            Dim restrictions As IStringArray
            restrictions = pSolverSettings.RestrictionAttributeNames
            restrictions.RemoveAll()
            ' Grado de importancia
            pSolverSettings.UseHierarchy = bUseHierarchy
            If bUseHierarchy Then
                pSolverSettings.HierarchyAttributeName = "hierarchy"
                pSolverSettings.HierarchyLevelCount = 3
                pSolverSettings.MaxValueForHierarchy(1) = 1
                pSolverSettings.NumTransitionToHierarchy(1) = 9

                pSolverSettings.MaxValueForHierarchy(2) = 2
                pSolverSettings.NumTransitionToHierarchy(2) = 9
            End If

            pSolver.UpdateContext(pContext, ObtenerDENetworkDataset((pContext.NetworkDataset)), New GPMessages)

            ' Actualizar StreetDirectionAgent
            Dim pNAAgent As INAAgent
            pNAAgent = pContext.Agents.ItemByName("StreetDirectionsAgent")
            pNAAgent.OnContextUpdated()
        Catch ex As Exception
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Warning)
        End Try

    End Sub

    '*********************************************************************************
    ' Crear NASolver y NAContext
    '*********************************************************************************
    Public Function crearSolverContext(ByRef pNetDataset As INetworkDataset) As INAContext
        'Obtener el elemento
        Dim pDENDS As IDENetworkDataset
        crearSolverContext = Nothing
        Try
            pDENDS = ObtenerDENetworkDataset(pNetDataset)

            Dim pNASolver As INASolver
            Dim pContextEdit As INAContextEdit
            pNASolver = New NARouteSolver
            pContextEdit = pNASolver.CreateContext(pDENDS, "Route")
            pContextEdit.Bind(pNetDataset, New GPMessages)
            crearSolverContext = pContextEdit
        Catch ex As Exception
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Warning)
        End Try
    End Function
End Module
