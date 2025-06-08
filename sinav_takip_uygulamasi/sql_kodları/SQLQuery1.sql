-- Veritabaný oluþturma
CREATE DATABASE NotHesaplamaSistemi;

 -- Veritabanýný kullanma
USE NotHesaplamaSistemi;

CREATE TABLE Ogretmenler (
    OgretmenID INT PRIMARY KEY IDENTITY, -- Otomatik artan ID
    KullaniciAdi NVARCHAR(50) NOT NULL UNIQUE, -- Kullanýcý adý
    Sifre NVARCHAR(100) NOT NULL -- Þifre
);

CREATE TABLE Ogrenciler (
    OgrenciID INT PRIMARY KEY IDENTITY, -- Öðrenci ID
    Numara NVARCHAR(20) NOT NULL UNIQUE, -- Öðrenci numarasý (Kullanýcý adý olarak da kullanýlabilir)
    Ad NVARCHAR(50), -- Öðrencinin adý
    Soyad NVARCHAR(50), -- Öðrencinin soyadý
    Sifre NVARCHAR(100) NOT NULL -- Þifre
);


CREATE TABLE Notlar (
    NotID INT PRIMARY KEY IDENTITY, -- Not ID
    OgrenciID INT FOREIGN KEY REFERENCES Ogrenciler(OgrenciID), -- Öðrenci ID'si ile iliþkilendirilir
    Vize INT, -- Vize notu
    Final INT, -- Final notu
    Ortalama FLOAT, -- Ortalama
    Durum NVARCHAR(10) -- Durum: Geçti veya Kaldý
);


INSERT INTO Ogretmenler (KullaniciAdi, Sifre) 
VALUES ('hoca1', '12345'); -- Öðretmen kullanýcý adý: hoca1, þifre: 12345


INSERT INTO Ogrenciler (Numara, Ad, Soyad, Sifre) 
VALUES 
('1234567890', 'Ahmet', 'Yýlmaz', 'ahmet123'), 
('2345678901', 'Mehmet', 'Kaya', 'mehmet123'), 
('3456789012', 'Ayþe', 'Demir', 'ayse123');


INSERT INTO Notlar (OgrenciID, Vize, Final, Ortalama, Durum) 
VALUES 
(1, 60, 70, (60 + 70) / 2, 'Geçti'),
(2, 40, 50, (40 + 50) / 2, 'Kaldý'),
(3, 80, 90, (80 + 90) / 2, 'Geçti');



