-- Создание таблицы workshops
CREATE TABLE IF NOT EXISTS workshops (
    ID_workshop INT AUTO_INCREMENT NOT NULL,
    ID_warehouse_keeper INT NOT NULL,
    FOREIGN KEY (ID_warehouse_keeper) REFERENCES workers(ID_worker)
) ENGINE=InnoDB;

