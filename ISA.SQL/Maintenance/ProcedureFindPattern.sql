﻿USE ISAdb
GO

DECLARE @searchArg varchar(250)
SET @searchArg = 'WITH INDEX'

SELECT ROUTINE_NAME, ROUTINE_TYPE, ROUTINE_DEFINITION FROM INFORMATION_SCHEMA.ROUTINES
WHERE
ROUTINE_DEFINITION LIKE '%' + @searchArg + '%'
 