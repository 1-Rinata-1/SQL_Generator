-- Хранимая процедура для добавления записи в таблицу warehouses
DELIMITER $$
CREATE PROCEDURE Insertwarehouses(
    IN p_Name VARCHAR(100),
    IN p_ID_operator INT
)
BEGIN
    INSERT INTO warehouses (
        Name, ID_operator
    ) VALUES (
        p_Name, p_ID_operator
    );
END$$
DELIMITER ;

