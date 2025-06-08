
use NotHesaplamaSistemi;

DROP TABLE Notlar;


CREATE TABLE Notlar (
    NotID INT PRIMARY KEY IDENTITY, -- Not ID
    OgrenciID INT, -- ��renci ID'si
    Vize INT, -- Vize notu
    Final INT, -- Final notu
    Ortalama FLOAT, -- Ortalama
    Durum NVARCHAR(10), -- Durum: Ge�ti veya Kald�
    FOREIGN KEY (OgrenciID) REFERENCES Ogrenciler(OgrenciID) -- ��renci ile ili�kilendirilmi�
);



CREATE TABLE ArsivNotlar (
    NotID INT PRIMARY KEY IDENTITY, -- Not ID
    OgrenciID INT, -- ��renci ID'si
    Vize INT, -- Vize notu
    Final INT, -- Final notu
    Ortalama FLOAT, -- Ortalama
    Durum NVARCHAR(10), -- Durum: Ge�ti veya Kald�
    SilinmeTarihi DATETIME, -- Silinme tarihi
    FOREIGN KEY (OgrenciID) REFERENCES Ogrenciler(OgrenciID) -- ��renci ile ili�kilendirilmi�
);




-- ��renci silindi�inde, notlar�n�n ar�ivlenmesi
CREATE TRIGGER trg_OgrenciSil
ON Ogrenciler
AFTER DELETE
AS
BEGIN
    DECLARE @OgrenciID INT, @Vize INT, @Final INT, @Ortalama FLOAT, @Durum NVARCHAR(10);

    -- Silinen ��renci bilgilerini almak
    SELECT @OgrenciID = OgrenciID
    FROM DELETED; -- DELETED, silinen sat�rlar� temsil eder

    -- Silinen ��rencinin notlar�n� almak
    SELECT @Vize = Vize, @Final = Final
    FROM Notlar
    WHERE OgrenciID = @OgrenciID;

    -- Ortalama hesapla
    SET @Ortalama = (@Vize + @Final) / 2.0;

    -- Ge�me/Kald� durumu belirle
    IF @Ortalama >= 50
        SET @Durum = 'Ge�ti';
    ELSE
        SET @Durum = 'Kald�';

    -- Silinen ��rencinin notlar�n� Ar�ivNotlar tablosuna ekle
    INSERT INTO ArsivNotlar (OgrenciID, Vize, Final, Ortalama, Durum, SilinmeTarihi)
    VALUES (@OgrenciID, @Vize, @Final, @Ortalama, @Durum, GETDATE());
END;
GO


-- ��rencilerin ge�me durumlar�n� g�r�nt�leme sorgusu
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
                    WHEN (i.Vize + i.Final) / 2.0 >= 50 THEN 'Ge�ti'
                    ELSE 'Kald�'
               END
    FROM Notlar n
    INNER JOIN INSERTED i ON n.NotID = i.NotID;
END;
GO


