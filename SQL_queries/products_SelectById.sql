-- Хранимая процедура для получения записи по ID из таблицы products
DELIMITER $$
CREATE PROCEDURE SelectproductsById(
    IN p_ID_product INT
)
BEGIN
    SELECT * FROM products
    WHERE ID_product = p_ID_product;
END$$
DELIMITER ;

