-- Создание таблицы workers
CREATE TABLE IF NOT EXISTS workers (
    ID_worker INT AUTO_INCREMENT NOT NULL,
    Position VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    FirstName VARCHAR(50) NOT NULL,
    Patronymic VARCHAR(50),
    Phone VARCHAR(20) NOT NULL
) ENGINE=InnoDB;

