﻿USE ISAdb_JKT
GO

DELETE FROM DBO.BackOrder 
WHERE RecordID NOT IN (SELECT RecordID FROM ISAdb.DBO.BackOrder)


GO