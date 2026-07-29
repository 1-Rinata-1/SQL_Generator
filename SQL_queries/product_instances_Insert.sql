-- Хранимая процедура для добавления записи в таблицу product_instances
DELIMITER $$
CREATE PROCEDURE Insertproduct_instances(
    IN p_Inventory_number INT,
    IN p_Expiration_date DATETIME,
    IN p_ID_product INT
)
BEGIN
    INSERT INTO product_instances (
        Inventory_number, Expiration_date, ID_product
    ) VALUES (
        p_Inventory_number, p_Expiration_date, p_ID_product
    );
END$$
DELIMITER ;

