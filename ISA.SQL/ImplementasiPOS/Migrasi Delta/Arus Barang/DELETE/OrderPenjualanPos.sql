﻿USE ISAdb_JKT
GO

DELETE FROM DBO.OrderPenjualanPos 
WHERE HtrID NOT IN (SELECT HtrID FROM ISAdb.DBO.OrderPenjualanPos)


GO

