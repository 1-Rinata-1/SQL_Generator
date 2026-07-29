-- Хранимая процедура для получения всех записей из таблицы warehouses
DELIMITER $$
CREATE PROCEDURE SelectAllwarehouses()
BEGIN
    SELECT * FROM warehouses;
END$$
DELIMITER ;

