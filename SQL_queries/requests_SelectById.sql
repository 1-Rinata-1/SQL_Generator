-- Хранимая процедура для получения записи по ID из таблицы requests
DELIMITER $$
CREATE PROCEDURE SelectrequestsById(
    IN p_ID_request INT
)
BEGIN
    SELECT * FROM requests
    WHERE ID_request = p_ID_request;
END$$
DELIMITER ;

