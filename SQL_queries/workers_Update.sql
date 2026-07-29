-- Хранимая процедура для обновления записи в таблице workers
DELIMITER $$
CREATE PROCEDURE Updateworkers(
    IN p_ID_worker INT,
    IN p_Position VARCHAR(50),
    IN p_LastName VARCHAR(50),
    IN p_FirstName VARCHAR(50),
    IN p_Patronymic VARCHAR(50),
    IN p_Phone VARCHAR(20)
)
BEGIN
    UPDATE workers
    SET
        Position = p_Position,
        LastName = p_LastName,
        FirstName = p_FirstName,
        Patronymic = p_Patronymic,
        Phone = p_Phone
    WHERE ID_worker = p_ID_worker;
END$$
DELIMITER ;

