﻿USE ISAFinance_JKT
GO

DELETE FROM DBO.Inden 
WHERE RecordID NOT IN (SELECT RecordID FROM ISAFinance.DBO.Inden)

GO

