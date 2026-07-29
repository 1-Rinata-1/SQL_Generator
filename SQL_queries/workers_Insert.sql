-- Хранимая процедура для добавления записи в таблицу workers
DELIMITER $$
CREATE PROCEDURE Insertworkers(
    IN p_Position VARCHAR(50),
    IN p_LastName VARCHAR(50),
    IN p_FirstName VARCHAR(50),
    IN p_Patronymic VARCHAR(50),
    IN p_Phone VARCHAR(20)
)
BEGIN
    INSERT INTO workers (
        Position, LastName, FirstName, Patronymic, Phone
    ) VALUES (
        p_Position, p_LastName, p_FirstName, p_Patronymic, p_Phone
    );
END$$
DELIMITER ;

