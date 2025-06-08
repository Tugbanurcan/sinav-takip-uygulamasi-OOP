DELETE FROM Ogrenciler WHERE OgrenciID = 8;


use NotHesaplamaSistemi;


DROP TRIGGER IF EXISTS trg_OgrenciSil;

CREATE TRIGGER trg_OgrenciSil
ON Ogrenciler
AFTER DELETE
AS
BEGIN
    -- Silinen öðrenciler arasýnda döngü
    DECLARE @OgrenciID INT;

    DECLARE cur CURSOR FOR
    SELECT OgrenciID FROM DELETED;

    OPEN cur;
    FETCH NEXT FROM cur INTO @OgrenciID;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Eðer öðrencinin notu varsa iþle
        IF EXISTS (SELECT 1 FROM Notlar WHERE OgrenciID = @OgrenciID)
        BEGIN
            DECLARE @Vize INT, @Final INT, @Ortalama FLOAT, @Durum NVARCHAR(10);

            SELECT @Vize = Vize, @Final = Final
            FROM Notlar
            WHERE OgrenciID = @OgrenciID;

            SET @Ortalama = (@Vize + @Final) / 2.0;

            SET @Durum = CASE 
                            WHEN @Ortalama >= 50 THEN 'Geçti'
                            ELSE 'Kaldý'
                        END;

            INSERT INTO ArsivNotlar (OgrenciID, Vize, Final, Ortalama, Durum, SilinmeTarihi)
            VALUES (@OgrenciID, @Vize, @Final, @Ortalama, @Durum, GETDATE());

            -- Notlar tablosundan da sil (istersen)
            DELETE FROM Notlar WHERE OgrenciID = @OgrenciID;
        END;

        FETCH NEXT FROM cur INTO @OgrenciID;
    END;

    CLOSE cur;
    DEALLOCATE cur;
END;