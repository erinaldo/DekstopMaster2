﻿USE ISAdb_JKT
GO

DELETE FROM DBO.Koret 
WHERE ReturID NOT IN (SELECT ReturID FROM ISAdb.DBO.Koret)


GO

