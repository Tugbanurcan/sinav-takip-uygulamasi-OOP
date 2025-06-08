use NotHesaplamaSistemi


DROP TABLE Ayarlar


CREATE TABLE Ayarlar (
    AyarAdi VARCHAR(50) PRIMARY KEY,
    AyarDegeri FLOAT
);

-- Varsayýlan deðerleri ekleyelim:
INSERT INTO Ayarlar (AyarAdi, AyarDegeri) VALUES 
('VizeOrani', 40),
('FinalOrani', 60),
('GecmeNotu', 55);


DROP TRIGGER [dbo].[trg_NotlarGuncelleme];

CREATE TRIGGER [dbo].[trg_NotlarGuncelleme]
ON [dbo].[Notlar]
AFTER INSERT, UPDATE
AS
BEGIN
    DECLARE @gecmeNotu INT;
    DECLARE @vizeOrani FLOAT;
    DECLARE @finalOrani FLOAT;

    SELECT 
        @gecmeNotu = CAST(AyarDegeri AS INT) FROM Ayarlar WHERE AyarAdi = 'GecmeNotu';
    SELECT 
        @vizeOrani = CAST(AyarDegeri AS FLOAT) FROM Ayarlar WHERE AyarAdi = 'VizeOrani';
    SELECT 
        @finalOrani = CAST(AyarDegeri AS FLOAT) FROM Ayarlar WHERE AyarAdi = 'FinalOrani';

    UPDATE n
    SET 
        Ortalama = ((i.Vize * @vizeOrani) + (i.Final * @finalOrani)) / 100.0,
        Durum = CASE 
                    WHEN ((i.Vize * @vizeOrani) + (i.Final * @finalOrani)) / 100.0 >= @gecmeNotu THEN 'Geçti'
                    ELSE 'Kaldý'
               END
    FROM Notlar n
    INNER JOIN INSERTED i ON n.NotID = i.NotID;
END;
GO
