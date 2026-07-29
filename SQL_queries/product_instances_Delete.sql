-- Процедура для удаления записи из таблицы product_instances
DELIMITER $$
CREATE PROCEDURE Deleteproduct_instances(
    IN p_Inventory_number INT
)
BEGIN
    DELETE FROM product_instances
    WHERE Inventory_number = p_Inventory_number;
END$$
DELIMITER ;

