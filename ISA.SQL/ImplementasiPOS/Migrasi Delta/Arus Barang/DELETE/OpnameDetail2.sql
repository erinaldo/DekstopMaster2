﻿USE ISAdb_JKT
GO

DELETE FROM DBO.OpnameDetail2 
WHERE RecordID NOT IN (SELECT RecordID FROM ISAdb.DBO.OpnameDetail2)


GO  