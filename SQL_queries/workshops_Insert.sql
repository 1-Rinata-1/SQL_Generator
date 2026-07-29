-- Хранимая процедура для добавления записи в таблицу workshops
DELIMITER $$
CREATE PROCEDURE Insertworkshops(
    IN p_ID_warehouse_keeper INT
)
BEGIN
    INSERT INTO workshops (
        ID_warehouse_keeper
    ) VALUES (
        p_ID_warehouse_keeper
    );
END$$
DELIMITER ;

