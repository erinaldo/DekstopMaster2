USE ISA_dbf
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ISA_dbf].[dbo].[SAS_staf]') AND type in (N'U'))
DROP TABLE ISA_dbf.dbo.SAS_staf
GO

SELECT *
INTO ISA_dbf.dbo.SAS_staf
FROM OPENROWSET('VFPOLEDB', 'C:\SAS_Kasir\'; ' '; ' ', 'SELECT  * FROM SAS_staf')c
GO

