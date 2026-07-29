-- Хранимая процедура для получения записи по ID из таблицы workers
DELIMITER $$
CREATE PROCEDURE SelectworkersById(
    IN p_ID_worker INT
)
BEGIN
    SELECT * FROM workers
    WHERE ID_worker = p_ID_worker;
END$$
DELIMITER ;

