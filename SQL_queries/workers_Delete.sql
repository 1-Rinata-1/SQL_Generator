-- Процедура для удаления записи из таблицы workers
DELIMITER $$
CREATE PROCEDURE Deleteworkers(
    IN p_ID_worker INT
)
BEGIN
    DELETE FROM workers
    WHERE ID_worker = p_ID_worker;
END$$
DELIMITER ;

