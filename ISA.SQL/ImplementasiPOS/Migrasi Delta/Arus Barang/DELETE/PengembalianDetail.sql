﻿USE ISAdb_JKT
GO

DELETE FROM DBO.PengembalianDetail 
WHERE RecordID NOT IN (SELECT RecordID FROM ISAdb.DBO.PengembalianDetail)


GO

