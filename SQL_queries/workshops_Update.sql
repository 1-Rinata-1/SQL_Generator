-- Хранимая процедура для обновления записи в таблице workshops
DELIMITER $$
CREATE PROCEDURE Updateworkshops(
    IN p_ID_workshop INT,
    IN p_ID_warehouse_keeper INT
)
BEGIN
    UPDATE workshops
    SET
        ID_warehouse_keeper = p_ID_warehouse_keeper
    WHERE ID_workshop = p_ID_workshop;
END$$
DELIMITER ;

