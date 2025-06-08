CREATE TABLE ArsivOgrenciler (
    ArsivID INT IDENTITY PRIMARY KEY,
    OgrenciID INT,
    Ad NVARCHAR(50),
    Soyad NVARCHAR(50),
    Vize INT,
    Final INT,
    Ortalama FLOAT,
    Durum NVARCHAR(10),
    SilinmeTarihi DATETIME
);



DROP TRIGGER trg_OgrenciSil;


CREATE TRIGGER trg_OgrenciSil
ON Ogrenciler
AFTER DELETE
AS
BEGIN
    INSERT INTO ArsivOgrenciler (OgrenciID, Ad, Soyad, Vize, Final, Ortalama, Durum, SilinmeTarihi)
    SELECT 
        d.OgrenciID,
        d.Ad,
        d.Soyad,
        n.Vize,
        n.Final,
        (CAST(n.Vize AS FLOAT) + CAST(n.Final AS FLOAT)) / 2.0 AS Ortalama,
        CASE 
            WHEN ((CAST(n.Vize AS FLOAT) + CAST(n.Final AS FLOAT)) / 2.0) >= 50 THEN 'Geçti' 
            ELSE 'Kaldý' 
        END AS Durum,
        GETDATE()
    FROM 
        DELETED d
    LEFT JOIN 
        Notlar n ON d.OgrenciID = n.OgrenciID;

    -- Notlarý sil (isteðe baðlý)
    DELETE n
    FROM Notlar n
    INNER JOIN DELETED d ON n.OgrenciID = d.OgrenciID;
END;


INSERT INTO Ogrenciler (Numara, Ad, Soyad, Sifre) 
VALUES 
('159', 'Ahmet', 'Yýlmaz', 'ahmet123'), 
('216', 'Mehmet', 'Kaya', 'mehmet123'), 
('170', 'Ayþe', 'Demir', 'ayse123');