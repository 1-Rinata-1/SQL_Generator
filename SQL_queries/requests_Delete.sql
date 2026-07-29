-- Процедура для удаления записи из таблицы requests
DELIMITER $$
CREATE PROCEDURE Deleterequests(
    IN p_ID_request INT
)
BEGIN
    DELETE FROM requests
    WHERE ID_request = p_ID_request;
END$$
DELIMITER ;

