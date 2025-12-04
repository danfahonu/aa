-- Trigger to update inventory (TONKHO) in DM_HANGHOA when a Sales Slip (PHIEU_XUAT) is created or deleted.
-- Assumes PHIEU.LOAI = 'X' for Export/Sales slips.

CREATE TRIGGER trg_UpdateTonKho_BanHang
ON PHIEU_CT
AFTER INSERT, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- Handle INSERT (New Sales Item) -> Deduct Stock
    IF EXISTS (SELECT * FROM inserted)
    BEGIN
        UPDATE h
        SET h.TONKHO = h.TONKHO - i.SL
        FROM DM_HANGHOA h
        INNER JOIN inserted i ON h.MAHANG = i.MAHANG
        INNER JOIN PHIEU p ON i.MAPHIEU = p.MAPHIEU
        WHERE p.LOAI = 'X';
    END

    -- Handle DELETE (Removed Sales Item) -> Restore Stock
    IF EXISTS (SELECT * FROM deleted)
    BEGIN
        UPDATE h
        SET h.TONKHO = h.TONKHO + d.SL
        FROM DM_HANGHOA h
        INNER JOIN deleted d ON h.MAHANG = d.MAHANG
        INNER JOIN PHIEU p ON d.MAPHIEU = p.MAPHIEU
        WHERE p.LOAI = 'X';
    END
END;
GO
