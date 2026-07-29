-- Хранимая процедура для получения всех записей из таблицы products
DELIMITER $$
CREATE PROCEDURE SelectAllproducts()
BEGIN
    SELECT * FROM products;
END$$
DELIMITER ;

