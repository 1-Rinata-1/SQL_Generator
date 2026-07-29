-- Создание таблицы product_instances
CREATE TABLE IF NOT EXISTS product_instances (
    Inventory_number INT NOT NULL,
    Expiration_date DATETIME,
    ID_product INT NOT NULL,
    FOREIGN KEY (ID_product) REFERENCES products(ID_product)
) ENGINE=InnoDB;

