﻿USE ISAdb_JKT
GO

DELETE FROM DBO.HistoryHPPA 
WHERE HistoryID NOT IN (SELECT HistoryID FROM ISAdb.DBO.HistoryHPPA)


GO