-- Хранимая процедура для получения всех записей из таблицы product_instances
DELIMITER $$
CREATE PROCEDURE SelectAllproduct_instances()
BEGIN
    SELECT * FROM product_instances;
END$$
DELIMITER ;

