-- Хранимая процедура для добавления записи в таблицу products
DELIMITER $$
CREATE PROCEDURE Insertproducts(
    IN p_Name VARCHAR(255),
    IN p_Category VARCHAR(100),
    IN p_Supplier VARCHAR(255),
    IN p_Quantity INT,
    IN p_Cost DECIMAL(10, 2),
    IN p_Unit VARCHAR(20),
    IN p_ID_warehouse INT
)
BEGIN
    INSERT INTO products (
        Name, Category, Supplier, Quantity, Cost, Unit, ID_warehouse
    ) VALUES (
        p_Name, p_Category, p_Supplier, p_Quantity, p_Cost, p_Unit, p_ID_warehouse
    );
END$$
DELIMITER ;

