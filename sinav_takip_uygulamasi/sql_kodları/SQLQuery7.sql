-- Trigger hangi tabloda ve hangi olayda �al���yor zaten biliyoruz:
SELECT
    OBJECT_NAME(parent_id) AS TableName,
    name AS TriggerName,
    is_disabled
FROM sys.triggers
WHERE name = 'trg_NotlarGuncelleme';
