-- Хранимая процедура для получения всех записей из таблицы requests
DELIMITER $$
CREATE PROCEDURE SelectAllrequests()
BEGIN
    SELECT * FROM requests;
END$$
DELIMITER ;

