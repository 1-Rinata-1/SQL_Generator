-- Хранимая процедура для получения всех записей из таблицы workshops
DELIMITER $$
CREATE PROCEDURE SelectAllworkshops()
BEGIN
    SELECT * FROM workshops;
END$$
DELIMITER ;

