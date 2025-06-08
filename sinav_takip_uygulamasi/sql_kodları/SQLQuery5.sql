use NotHesaplamaSistemi

SELECT name
FROM sys.foreign_keys
WHERE parent_object_id = OBJECT_ID('ArsivNotlar');

ALTER TABLE ArsivNotlar
DROP CONSTRAINT FK__ArsivNotl__Ogren__5FB337D6;

SELECT name
FROM sys.foreign_keys
WHERE parent_object_id = OBJECT_ID('Notlar');

ALTER TABLE Notlar
DROP CONSTRAINT FK__Notlar__OgrenciI__5CD6CB2B;

ALTER TABLE Notlar
ADD CONSTRAINT FK_Notlar_Ogrenciler
FOREIGN KEY (OgrenciID)
REFERENCES Ogrenciler(OgrenciID)
ON DELETE CASCADE;




