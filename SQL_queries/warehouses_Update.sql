-- Хранимая процедура для обновления записи в таблице warehouses
DELIMITER $$
CREATE PROCEDURE Updatewarehouses(
    IN p_ID_warehouse INT,
    IN p_Name VARCHAR(100),
    IN p_ID_operator INT
)
BEGIN
    UPDATE warehouses
    SET
        Name = p_Name,
        ID_operator = p_ID_operator
    WHERE ID_warehouse = p_ID_warehouse;
END$$
DELIMITER ;

