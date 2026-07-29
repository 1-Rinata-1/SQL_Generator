-- Создание таблицы products
CREATE TABLE IF NOT EXISTS products (
    ID_product INT AUTO_INCREMENT NOT NULL,
    Name VARCHAR(255) NOT NULL,
    Category VARCHAR(100) NOT NULL,
    Supplier VARCHAR(255) NOT NULL,
    Quantity INT NOT NULL,
    Cost DECIMAL(10, 2) NOT NULL,
    Unit VARCHAR(20) NOT NULL,
    ID_warehouse INT NOT NULL,
    FOREIGN KEY (ID_warehouse) REFERENCES warehouses(ID_warehouse)
) ENGINE=InnoDB;

