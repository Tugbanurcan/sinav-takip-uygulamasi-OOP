
use NotHesaplamaSistemi;

DROP TABLE Notlar;


CREATE TABLE Notlar (
    NotID INT PRIMARY KEY IDENTITY, -- Not ID
    OgrenciID INT, -- Öðrenci ID'si
    Vize INT, -- Vize notu
    Final INT, -- Final notu
    Ortalama FLOAT, -- Ortalama
    Durum NVARCHAR(10), -- Durum: Geçti veya Kaldý
    FOREIGN KEY (OgrenciID) REFERENCES Ogrenciler(OgrenciID) -- Öðrenci ile iliþkilendirilmiþ
);



CREATE TABLE ArsivNotlar (
    NotID INT PRIMARY KEY IDENTITY, -- Not ID
    OgrenciID INT, -- Öðrenci ID'si
    Vize INT, -- Vize notu
    Final INT, -- Final notu
    Ortalama FLOAT, -- Ortalama
    Durum NVARCHAR(10), -- Durum: Geçti veya Kaldý
    SilinmeTarihi DATETIME, -- Silinme tarihi
    FOREIGN KEY (OgrenciID) REFERENCES Ogrenciler(OgrenciID) -- Öðrenci ile iliþkilendirilmiþ
);




-- Öðrenci silindiðinde, notlarýnýn arþivlenmesi
CREATE TRIGGER trg_OgrenciSil
ON Ogrenciler
AFTER DELETE
AS
BEGIN
    DECLARE @OgrenciID INT, @Vize INT, @Final INT, @Ortalama FLOAT, @Durum NVARCHAR(10);

    -- Silinen öðrenci bilgilerini almak
    SELECT @OgrenciID = OgrenciID
    FROM DELETED; -- DELETED, silinen satýrlarý temsil eder

    -- Silinen öðrencinin notlarýný almak
    SELECT @Vize = Vize, @Final = Final
    FROM Notlar
    WHERE OgrenciID = @OgrenciID;

    -- Ortalama hesapla
    SET @Ortalama = (@Vize + @Final) / 2.0;

    -- Geçme/Kaldý durumu belirle
    IF @Ortalama >= 50
        SET @Durum = 'Geçti';
    ELSE
        SET @Durum = 'Kaldý';

    -- Silinen öðrencinin notlarýný ArþivNotlar tablosuna ekle
    INSERT INTO ArsivNotlar (OgrenciID, Vize, Final, Ortalama, Durum, SilinmeTarihi)
    VALUES (@OgrenciID, @Vize, @Final, @Ortalama, @Durum, GETDATE());
END;
GO


-- Öðrencilerin geçme durumlarýný görüntüleme sorgusu
SELECT Ogrenciler.Ad, Ogrenciler.Soyad, Ogrenciler.Numara, Notlar.Vize, Notlar.Final, Notlar.Ortalama, Notlar.Durum
FROM Ogrenciler
JOIN Notlar ON Ogrenciler.OgrenciID = Notlar.OgrenciID
ORDER BY Notlar.Ortalama DESC;


DROP TRIGGER trg_NotlarGuncelleme;

CREATE TRIGGER trg_NotlarGuncelleme
ON Notlar
AFTER INSERT, UPDATE
AS
BEGIN
    UPDATE n
    SET 
        Ortalama = (i.Vize + i.Final) / 2.0,
        Durum = CASE 
                    WHEN (i.Vize + i.Final) / 2.0 >= 50 THEN 'Geçti'
                    ELSE 'Kaldý'
               END
    FROM Notlar n
    INNER JOIN INSERTED i ON n.NotID = i.NotID;
END;
GO


