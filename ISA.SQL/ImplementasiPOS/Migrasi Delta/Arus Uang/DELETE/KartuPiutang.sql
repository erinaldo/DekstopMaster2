﻿USE ISAFinance_JKT
GO

DELETE FROM DBO.KartuPiutang 
WHERE KPID NOT IN (SELECT KPID FROM ISAFinance.DBO.KartuPiutang)

GO

