﻿USE ISAFinance_JKT
GO

DELETE FROM DBO.Bank 
WHERE BankID NOT IN (SELECT BankID FROM ISAFinance.DBO.Bank)

GO
