-- Процедура для удаления записи из таблицы warehouses
DELIMITER $$
CREATE PROCEDURE Deletewarehouses(
    IN p_ID_warehouse INT
)
BEGIN
    DELETE FROM warehouses
    WHERE ID_warehouse = p_ID_warehouse;
END$$
DELIMITER ;

