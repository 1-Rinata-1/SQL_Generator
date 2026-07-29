-- Процедура для удаления записи из таблицы products
DELIMITER $$
CREATE PROCEDURE Deleteproducts(
    IN p_ID_product INT
)
BEGIN
    DELETE FROM products
    WHERE ID_product = p_ID_product;
END$$
DELIMITER ;

