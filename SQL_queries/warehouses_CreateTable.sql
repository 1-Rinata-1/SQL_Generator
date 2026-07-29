-- Создание таблицы warehouses
CREATE TABLE IF NOT EXISTS warehouses (
    ID_warehouse INT AUTO_INCREMENT NOT NULL,
    Name VARCHAR(100) NOT NULL,
    ID_operator INT NOT NULL,
    FOREIGN KEY (ID_operator) REFERENCES workers(ID_worker)
) ENGINE=InnoDB;

