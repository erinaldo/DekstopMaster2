﻿USE ISAFinance_JKT
GO



INSERT INTO DBO.BBK
SELECT * FROM ISAFinance.DBO.BBK WHERE RecordID NOT IN (SELECT RecordID FROM DBO.BBK)
GO 