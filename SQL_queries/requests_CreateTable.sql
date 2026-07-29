-- Создание таблицы requests
CREATE TABLE IF NOT EXISTS requests (
    ID_request INT AUTO_INCREMENT NOT NULL,
    Workshop_id INT NOT NULL,
    Product_id INT NOT NULL,
    Quantity INT NOT NULL,
    Request_date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Status VARCHAR(20) NOT NULL,
    FOREIGN KEY (Workshop_id) REFERENCES workshops(ID_workshop),
    FOREIGN KEY (Product_id) REFERENCES products(ID_product)
) ENGINE=InnoDB;

