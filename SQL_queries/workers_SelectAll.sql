-- Хранимая процедура для получения всех записей из таблицы workers
DELIMITER $$
CREATE PROCEDURE SelectAllworkers()
BEGIN
    SELECT * FROM workers;
END$$
DELIMITER ;

