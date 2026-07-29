-- Хранимая процедура для получения записи по ID из таблицы workshops
DELIMITER $$
CREATE PROCEDURE SelectworkshopsById(
    IN p_ID_workshop INT
)
BEGIN
    SELECT * FROM workshops
    WHERE ID_workshop = p_ID_workshop;
END$$
DELIMITER ;

