-- Хранимая процедура для обновления записи в таблице requests
DELIMITER $$
CREATE PROCEDURE Updaterequests(
    IN p_ID_request INT,
    IN p_Workshop_id INT,
    IN p_Product_id INT,
    IN p_Quantity INT,
    IN p_Request_date DATETIME,
    IN p_Status VARCHAR(20)
)
BEGIN
    UPDATE requests
    SET
        Workshop_id = p_Workshop_id,
        Product_id = p_Product_id,
        Quantity = p_Quantity,
        Request_date = p_Request_date,
        Status = p_Status
    WHERE ID_request = p_ID_request;
END$$
DELIMITER ;

