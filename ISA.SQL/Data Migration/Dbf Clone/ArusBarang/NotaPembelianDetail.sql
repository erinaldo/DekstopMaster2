USE ISA_dbf
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ISA_dbf].[dbo].[dtransb]') AND type in (N'U'))
DROP TABLE ISA_dbf.dbo.dtransb
GO

SELECT *
INTO ISA_dbf.dbo.dtransb
FROM OPENROWSET('VFPOLEDB', 'C:\SAS_Database\'; ' '; ' ', 'SELECT  * FROM dtransb')c
GO
