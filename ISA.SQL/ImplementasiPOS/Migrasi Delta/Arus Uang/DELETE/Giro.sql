﻿USE ISAFinance_JKT
GO

DELETE FROM DBO.Giro 
WHERE GiroRecID NOT IN (SELECT GiroRecID FROM ISAFinance.DBO.Giro)

GO

