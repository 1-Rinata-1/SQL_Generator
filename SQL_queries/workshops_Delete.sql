-- Процедура для удаления записи из таблицы workshops
DELIMITER $$
CREATE PROCEDURE Deleteworkshops(
    IN p_ID_workshop INT
)
BEGIN
    DELETE FROM workshops
    WHERE ID_workshop = p_ID_workshop;
END$$
DELIMITER ;

