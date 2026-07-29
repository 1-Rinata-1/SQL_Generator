-- Хранимая процедура для обновления записи в таблице products
DELIMITER $$
CREATE PROCEDURE Updateproducts(
    IN p_ID_product INT,
    IN p_Name VARCHAR(255),
    IN p_Category VARCHAR(100),
    IN p_Supplier VARCHAR(255),
    IN p_Quantity INT,
    IN p_Cost DECIMAL(10, 2),
    IN p_Unit VARCHAR(20),
    IN p_ID_warehouse INT
)
BEGIN
    UPDATE products
    SET
        Name = p_Name,
        Category = p_Category,
        Supplier = p_Supplier,
        Quantity = p_Quantity,
        Cost = p_Cost,
        Unit = p_Unit,
        ID_warehouse = p_ID_warehouse
    WHERE ID_product = p_ID_product;
END$$
DELIMITER ;

