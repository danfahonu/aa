-- 1. Báo cáo Tồn kho Tổng hợp
CREATE PROCEDURE sp_Report_TonKhoTongHop
AS
BEGIN
    SELECT 
        hh.MAHH AS ItemCode,
        hh.TENHH AS ItemName,
        hh.DVT AS Unit,
        ISNULL(SUM(k.SO_LUONG_TON), 0) AS Quantity,
        ISNULL(SUM(k.SO_LUONG_TON * k.DON_GIA_NHAP), 0) AS Value -- Simple valuation based on FIFO/Specific ID logic from KHO_CHITIET_TONKHO
    FROM DM_HANGHOA hh
    LEFT JOIN KHO_CHITIET_TONKHO k ON hh.MAHH = k.MAHH
    GROUP BY hh.MAHH, hh.TENHH, hh.DVT
    HAVING ISNULL(SUM(k.SO_LUONG_TON), 0) > 0 -- Only show items with stock
    ORDER BY hh.TENHH;
END;
GO

-- 2. Báo cáo Doanh thu Lợi nhuận
CREATE PROCEDURE sp_Report_DoanhThuLoiNhuan
    @FromDate DATETIME,
    @ToDate DATETIME
AS
BEGIN
    SELECT 
        p.NGAYLAP AS Date,
        p.SOPHIEU AS InvoiceNo,
        SUM(ct.THANHTIEN) AS Revenue,
        SUM(ct.SL * hh.GIAVON) AS COGS,
        SUM(ct.THANHTIEN) - SUM(ct.SL * hh.GIAVON) AS Profit
    FROM PHIEU p
    JOIN PHIEU_CT ct ON p.SOPHIEU = ct.SOPHIEU
    JOIN DM_HANGHOA hh ON ct.MAHH = hh.MAHH
    WHERE p.LOAI = 'X' -- Export/Sale
      AND p.NGAYLAP BETWEEN @FromDate AND @ToDate
    GROUP BY p.NGAYLAP, p.SOPHIEU
    ORDER BY p.NGAYLAP;
END;
GO

-- 3. Báo cáo Thẻ kho
CREATE PROCEDURE sp_Report_TheKho
    @MaHH NVARCHAR(50),
    @FromDate DATETIME,
    @ToDate DATETIME
AS
BEGIN
    -- Calculate Opening Balance
    DECLARE @OpeningBalance INT = 0;
    
    SELECT @OpeningBalance = ISNULL(SUM(CASE WHEN p.LOAI = 'N' THEN ct.SL ELSE -ct.SL END), 0)
    FROM PHIEU p
    JOIN PHIEU_CT ct ON p.SOPHIEU = ct.SOPHIEU
    WHERE ct.MAHH = @MaHH
      AND p.NGAYLAP < @FromDate;

    -- Select Transactions
    SELECT 
        p.NGAYLAP AS Date,
        p.SOPHIEU AS RefNo,
        CASE WHEN p.LOAI = 'N' THEN N'Nhập' ELSE N'Xuất' END AS Type,
        CASE WHEN p.LOAI = 'N' THEN ct.SL ELSE 0 END AS ImportQty,
        CASE WHEN p.LOAI = 'X' THEN ct.SL ELSE 0 END AS ExportQty,
        0 AS Balance -- Placeholder, calculated in app or via recursive CTE if needed
    FROM PHIEU p
    JOIN PHIEU_CT ct ON p.SOPHIEU = ct.SOPHIEU
    WHERE ct.MAHH = @MaHH
      AND p.NGAYLAP BETWEEN @FromDate AND @ToDate
    ORDER BY p.NGAYLAP, p.SOPHIEU;
END;
GO
