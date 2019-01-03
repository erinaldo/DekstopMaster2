﻿USE [ISAdb]
GO
/****** Object:  StoredProcedure [dbo].[usp_OrderPenjualan_LIST_BO]    Script Date: 03/22/2011 09:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ===============================================
-- Author:		Stephanie
-- Create date: 22 Mar 11
-- Description:	List data on table OrderPenjualan
--				Khusus untuk Back Order 
-- ===============================================
CREATE PROCEDURE [dbo].[usp_OrderPenjualan_LIST_BO] 
	-- Add the parameters for the stored procedure here
	@fromDate datetime = NULL,
	@toDate datetime = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @initCab varchar(2)

	SELECT TOP 1 
		@initCab = InitCabang
	FROM dbo.Perusahaan
    -- Insert statements for procedure here

	SELECT 
		a.RowID, 
		a.HtrID, 
		a.Cabang1, 
		a.Cabang2, 
		a.Cabang3, 
		a.NoRequest, 
		a.TglRequest, 
		a.NoDO, 
		dbo.fnGetIDDO(a.RowID) AS DOID,
		(CASE WHEN a.StatusBatal LIKE '%BATAL%' THEN '*' ELSE
			(CASE WHEN a.NoRequest NOT LIKE '%!%' AND a.NPrint IS NULL 
			AND a.Cabang2 != @initCab THEN '?' ELSE '' END) END) AS FlagDO,
		a.TglDO, 
		a.NoACCPusat, 
		a.ACCPiutangID, 
		a.NoACCPiutang, 
		a.TglACCPiutang, 
		a.RpACCPiutang,
		a.RpPlafonToko,
		a.RpPiutangTerakhir,
		a.RpGiroTolakTerakhir,
		a.RpOverdue,
		a.StatusBatal,
		(SELECT TOP 1 n.TglSuratJalan FROM dbo.NotaPenjualan n 
			WHERE a.RowID = n.DOID ORDER BY n.TglSuratJalan DESC) AS TglSuratJalan,
		a.HariKredit, 
		a.KodeToko,
		ISNULL(b.TokoID, '') AS TokoID,
		ISNULL(b.WilID, '') AS WilID,
		ISNULL(b.Daerah, '') AS Daerah,
		dbo.fnGetStatusToko(a.TglDO, a.KodeToko, a.Cabang1) AS StsToko,
		a.KodeSales, 
		c.NamaSales,
		b.NamaToko,
		a.AlamatKirim, 
		a.Kota, 
		a.DiscFormula, 
		a.Disc1, 
		a.Disc2, 
		a.Disc3, 
		a.QtyTolak, 
		a.OverDue, 
		a.isClosed, 
		a.Catatan1, 
		a.Catatan2, 
		a.Catatan3, 
		a.Catatan4, 
		a.Catatan5, 
		a.NoDOBO, 
		a.TglReorder, 
		a.StatusBO, 
		a.SyncFlag, 
		a.LinkID, 
		LEFT(a.TransactionType, 1) AS TunaiKredit,
		a.TransactionType, 
		a.Expedisi,
		a.Shift,
		a.HariKirim, 
		a.HariSales, 
		a.NPrint,
		ISNULL( (SELECT SUM(d.QtyDO * d.HrgJual) FROM dbo.OrderPenjualanDetail d WHERE a.RowID = d.HeaderID) , 0 )
			AS RpJual,
		ISNULL((SELECT SUM(
			ISNULL(dbo.fnHitungNet3Disc((d.QtyDO * d.HrgJual), d.Disc1, d.Disc2, d.Disc3, d.DiscFormula), 0) 
			- (d.QtyDO * d.Pot)) FROM dbo.OrderPenjualanDetail d WHERE a.RowID = d.HeaderID), 0)
			AS  RpNet, -- Perhitungan nilai jual untuk DO menggunakan QtyDO	
		ISNULL((SELECT SUM(d.QtyDO * d.Pot) FROM dbo.OrderPenjualanDetail d WHERE a.RowID = d.HeaderID), 0) AS RpPot,
		a.LastUpdatedBy, 
		a.LastUpdatedTime
	FROM dbo.OrderPenjualan a 
		LEFT OUTER JOIN dbo.Toko b ON a.KodeToko = b.KodeToko
		LEFT OUTER JOIN dbo.Sales c ON a.KodeSales = c.SalesID
	WHERE
		(a.TglDO >= @fromDate OR @fromDate IS NULL)
		AND (a.TglDO <= @toDate OR @toDate IS NULL)
		/* Filter: jika QtyDO-QtySuratJalan > 0 dan Belum TutupBO */
		AND ((SELECT SUM(d.QtyDO) FROM dbo.OrderPenjualanDetail d 
				WHERE a.RowID = d.HeaderID) - 
				(SELECT SUM(e.QtySuratJalan) FROM dbo.OrderPenjualanDetail d 
				LEFT OUTER JOIN dbo.NotaPenjualanDetail e ON d.RowID = e.DODetailID
				WHERE a.RowID = d.HeaderID) > 0
			AND a.StatusBO = 1)
    
END











