﻿USE ISAFinance_JKT
GO

DELETE FROM DBO.JournalDetail 
WHERE RecordID NOT IN (SELECT RecordID FROM ISAFinance.DBO.JournalDetail)

GO

