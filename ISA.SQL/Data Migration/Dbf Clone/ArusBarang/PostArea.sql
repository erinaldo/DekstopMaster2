USE ISA_dbf
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ISA_dbf].[dbo].[postarea]') AND type in (N'U'))
DROP TABLE ISA_dbf.dbo.postarea
GO

SELECT *
INTO ISA_dbf.dbo.postarea
FROM OPENROWSET('VFPOLEDB', 'C:\SAS_Database\'; ' '; ' ', 'SELECT  * FROM postarea')c
GO


