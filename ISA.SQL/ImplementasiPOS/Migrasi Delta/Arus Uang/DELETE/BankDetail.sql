﻿USE ISAFinance_JKT
GO

DELETE FROM DBO.BankDetail 
WHERE RecordID NOT IN (SELECT RecordID FROM ISAFinance.DBO.BankDetail)

GO
