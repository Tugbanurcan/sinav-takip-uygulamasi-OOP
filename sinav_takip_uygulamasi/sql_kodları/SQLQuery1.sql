-- Veritaban� olu�turma
CREATE DATABASE NotHesaplamaSistemi;

 -- Veritaban�n� kullanma
USE NotHesaplamaSistemi;

CREATE TABLE Ogretmenler (
    OgretmenID INT PRIMARY KEY IDENTITY, -- Otomatik artan ID
    KullaniciAdi NVARCHAR(50) NOT NULL UNIQUE, -- Kullan�c� ad�
    Sifre NVARCHAR(100) NOT NULL -- �ifre
);

CREATE TABLE Ogrenciler (
    OgrenciID INT PRIMARY KEY IDENTITY, -- ��renci ID
    Numara NVARCHAR(20) NOT NULL UNIQUE, -- ��renci numaras� (Kullan�c� ad� olarak da kullan�labilir)
    Ad NVARCHAR(50), -- ��rencinin ad�
    Soyad NVARCHAR(50), -- ��rencinin soyad�
    Sifre NVARCHAR(100) NOT NULL -- �ifre
);


CREATE TABLE Notlar (
    NotID INT PRIMARY KEY IDENTITY, -- Not ID
    OgrenciID INT FOREIGN KEY REFERENCES Ogrenciler(OgrenciID), -- ��renci ID'si ile ili�kilendirilir
    Vize INT, -- Vize notu
    Final INT, -- Final notu
    Ortalama FLOAT, -- Ortalama
    Durum NVARCHAR(10) -- Durum: Ge�ti veya Kald�
);


INSERT INTO Ogretmenler (KullaniciAdi, Sifre) 
VALUES ('hoca1', '12345'); -- ��retmen kullan�c� ad�: hoca1, �ifre: 12345


INSERT INTO Ogrenciler (Numara, Ad, Soyad, Sifre) 
VALUES 
('1234567890', 'Ahmet', 'Y�lmaz', 'ahmet123'), 
('2345678901', 'Mehmet', 'Kaya', 'mehmet123'), 
('3456789012', 'Ay�e', 'Demir', 'ayse123');


INSERT INTO Notlar (OgrenciID, Vize, Final, Ortalama, Durum) 
VALUES 
(1, 60, 70, (60 + 70) / 2, 'Ge�ti'),
(2, 40, 50, (40 + 50) / 2, 'Kald�'),
(3, 80, 90, (80 + 90) / 2, 'Ge�ti');



