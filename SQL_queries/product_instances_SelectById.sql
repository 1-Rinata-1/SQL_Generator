-- Хранимая процедура для получения записи по ID из таблицы product_instances
DELIMITER $$
CREATE PROCEDURE Selectproduct_instancesById(
    IN p_Inventory_number INT
)
BEGIN
    SELECT * FROM product_instances
    WHERE Inventory_number = p_Inventory_number;
END$$
DELIMITER ;

