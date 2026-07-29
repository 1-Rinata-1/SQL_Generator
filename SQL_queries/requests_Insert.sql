-- Хранимая процедура для добавления записи в таблицу requests
DELIMITER $$
CREATE PROCEDURE Insertrequests(
    IN p_Workshop_id INT,
    IN p_Product_id INT,
    IN p_Quantity INT,
    IN p_Request_date DATETIME,
    IN p_Status VARCHAR(20)
)
BEGIN
    INSERT INTO requests (
        Workshop_id, Product_id, Quantity, Request_date, Status
    ) VALUES (
        p_Workshop_id, p_Product_id, p_Quantity, p_Request_date, p_Status
    );
END$$
DELIMITER ;

