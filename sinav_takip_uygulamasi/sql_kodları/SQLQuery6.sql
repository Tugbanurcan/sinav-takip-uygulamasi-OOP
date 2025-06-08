SELECT DISTINCT OgrenciID FROM ArsivNotlar WHERE OgrenciID NOT IN (SELECT OgrenciID FROM Ogrenciler)


DELETE FROM ArsivNotlar 
WHERE OgrenciID NOT IN (SELECT OgrenciID FROM Ogrenciler);


INSERT INTO Ogrenciler (Numara, Ad, Soyad, Sifre) 
VALUES 
('123', 'Ahmet', 'Yýlmaz', 'ahmet123'), 
('124', 'Mehmet', 'Kaya', 'mehmet123'), 
('125', 'Ayþe', 'Demir', 'ayse123'),
('126', 'Aysun', 'Demirci', 'a'),
('127', 'Ayberk', 'Derin', 'ayb1'),
('128', 'Mahmut', 'Peltek', 'mah123'),
('129', 'Bahar', 'Dem', 'baah123');


SELECT TOP 10 
    an.OgrenciID, 
    o.Numara, 
    o.Ad, 
    o.Soyad
FROM ArsivNotlar an
LEFT JOIN Ogrenciler o ON an.OgrenciID = o.OgrenciID


SELECT DISTINCT an.OgrenciID
FROM ArsivNotlar an
WHERE an.OgrenciID NOT IN (SELECT OgrenciID FROM Ogrenciler)

DELETE FROM Ogrenciler WHERE OgrenciID = 18;

SELECT * FROM ArsivNotlar WHERE OgrenciID = 18;


SELECT * FROM ArsivNotlar 
SELECT * FROM ArsivNotlar;



DELETE FROM ArsivNotlar WHERE OgrenciID = 18;

ALTER TABLE ArsivNotlar
ADD CONSTRAINT FK_Arsiv_Ogrenciler FOREIGN KEY (OgrenciID)
REFERENCES Ogrenciler(OgrenciID);

ALTER TABLE ArsivNotlar
DROP CONSTRAINT FK_Arsiv_Ogrenciler;







