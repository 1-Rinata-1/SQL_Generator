-- Хранимая процедура для получения записи по ID из таблицы warehouses
DELIMITER $$
CREATE PROCEDURE SelectwarehousesById(
    IN p_ID_warehouse INT
)
BEGIN
    SELECT * FROM warehouses
    WHERE ID_warehouse = p_ID_warehouse;
END$$
DELIMITER ;

