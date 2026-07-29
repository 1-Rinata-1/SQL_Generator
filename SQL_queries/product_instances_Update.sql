-- Хранимая процедура для обновления записи в таблице product_instances
DELIMITER $$
CREATE PROCEDURE Updateproduct_instances(
    IN p_Inventory_number INT,
    IN p_Expiration_date DATETIME,
    IN p_ID_product INT
)
BEGIN
    UPDATE product_instances
    SET
        Expiration_date = p_Expiration_date,
        ID_product = p_ID_product
    WHERE Inventory_number = p_Inventory_number;
END$$
DELIMITER ;

