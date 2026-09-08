Module Dates


    Public Function GetFirstDayOfMonth(dtDate As DateTime) As DateTime
        Return New DateTime(dtDate.Year, dtDate.Month, 1)
    End Function
    Public Function GetLastDayOfMonth(ByVal dtDate As DateTime) As DateTime
        Dim dtTo As New DateTime(dtDate.Year, dtDate.Month, 1)
        dtTo = dtTo.AddMonths(1)
        dtTo = dtTo.AddDays(-(dtTo.Day))
        Return dtTo
    End Function
    Public Function GetFirsttDayOfYear(ByVal dtDate As DateTime) As DateTime
        Dim dtTo As New DateTime(dtDate.Year, 1, 1)
        Return dtTo
    End Function
    Public Function GetLastDayOfYear(ByVal dtDate As DateTime) As DateTime
        Dim dtTo As New DateTime(dtDate.Year, 12, 31)
        Return dtTo
    End Function


End Module
