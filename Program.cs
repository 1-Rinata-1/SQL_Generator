using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FactoryWarehouseGenerator
{
    // таблица
    public class Table
    {
        public string TableName { get; set; }
        public List<Column> Columns { get; set; }
        public List<ForeignKey> ForeignKeys { get; set; }
        public string FilesPath { get; set; }
        public Table()
        {
            Columns = new List<Column>();
            ForeignKeys = new List<ForeignKey>();
        }
    }
    
    // столбец таблицы
    public class Column
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public int Size { get; set; }
        public int Precision { get; set; }
        public int Scale { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsForeignKey { get; set; }
        public bool IsNullable { get; set; }
        public string DefaultValue { get; set; }
    }

    // внешний ключ
    public class ForeignKey
    {
        public string ColumnName { get; set; }
        public string ReferenceTable { get; set; }
        public string ReferenceColumn { get; set; }
    }

    // основной генератор кода
    public class FactoryWarehouseCodeGenerator
    {
        private const string Tab1 = "    ";
        private const string Tab2 = "        ";
        private const string Tab3 = "            ";

        // генерация кода для заполнения базы данных
        public void GenerateDatabaseCode(Table table)
        {
            GenerateCreateTableScript(table);
            GenerateCRUDProcedures(table);
        }

        // генерация кода для создания таблиц
        private void GenerateCreateTableScript(Table table)
        {
            var result = new List<string>();
            result.Add($"-- Создание таблицы {table.TableName}");
            result.Add($"CREATE TABLE IF NOT EXISTS {table.TableName} (");

            var primary_keys = table.Columns.FindAll(c => c.IsPrimaryKey);

            for (int i = 0; i < table.Columns.Count; i++)
            {
                var column = table.Columns[i];
                string columnDefinition = $"{Tab1}{column.Name} {GetDataTypeDefinition(column)}";

                if (!column.IsNullable)
                    columnDefinition += " NOT NULL";

                if (!string.IsNullOrEmpty(column.DefaultValue))
                    columnDefinition += $" DEFAULT {column.DefaultValue}";

                bool isLast = (i == table.Columns.Count - 1);
                bool needsComma = !isLast || primary_keys.Count > 1 || table.ForeignKeys.Count > 0;
                if (needsComma)
                    columnDefinition += ",";

                result.Add(columnDefinition);
            }

            if (primary_keys.Count > 1)
            {
                string pkColumns = string.Join(", ", primary_keys.ConvertAll(c => c.Name));
                result.Add($"{Tab1}PRIMARY KEY ({pkColumns})");
                if (table.ForeignKeys.Count > 0)
                    result.Add($"{Tab1},");
            }

            if (table.ForeignKeys.Count > 0)
            {
                for (int i = 0; i < table.ForeignKeys.Count; i++)
                {
                    var fk = table.ForeignKeys[i];
                    string fkLine = $"{Tab1}FOREIGN KEY ({fk.ColumnName}) REFERENCES {fk.ReferenceTable}({fk.ReferenceColumn})";
                    if (i < table.ForeignKeys.Count - 1)
                        fkLine += ",";
                    result.Add(fkLine);
                }
            }
            result.Add(") ENGINE=InnoDB;");
            result.Add("");
            SaveToFile(result, table.FilesPath + $"{table.TableName}_CreateTable.sql");
        }

        // генерация кода для создания процедур
        private void GenerateCRUDProcedures(Table table)
        {
            GenerateInsertProcedure(table);
            GenerateUpdateProcedure(table);
            GenerateDeleteProcedure(table);
            GenerateSelectProcedures(table);
        }

        // генерация кода для процедуры добавления записи
        private void GenerateInsertProcedure(Table table)
        {
            var result = new List<string>();
            result.Add($"-- Хранимая процедура для добавления записи в таблицу {table.TableName}");
            // смена символа окончания запроса
            result.Add("DELIMITER $$");
            result.Add($"CREATE PROCEDURE Insert{table.TableName}(");

            var parameters = new List<string>();
            foreach (var column in table.Columns)
            {
                if (!column.IsPrimaryKey || !column.DataType.ToLower().Contains("auto_increment"))
                {
                    parameters.Add($"{Tab1}IN p_{column.Name} {GetDataTypeDefinition(column)}");
                }
            }
            result.Add(string.Join($",{Environment.NewLine}", parameters));
            result.Add(")");
            result.Add("BEGIN");
            result.Add($"{Tab1}INSERT INTO {table.TableName} (");

            var columns = new List<string>();
            var values = new List<string>();
            foreach (var column in table.Columns)
            {
                if (!column.IsPrimaryKey || !column.DataType.ToLower().Contains("auto_increment"))
                {
                    columns.Add(column.Name);
                    values.Add($"p_{column.Name}");
                }
            }
            result.Add($"{Tab2}{string.Join(", ", columns)}");
            result.Add($"{Tab1}) VALUES (");
            result.Add($"{Tab2}{string.Join(", ", values)}");
            result.Add($"{Tab1});");
            result.Add("END$$");
            // возвращение символа окончания запроса
            result.Add("DELIMITER ;");
            result.Add("");
            SaveToFile(result, table.FilesPath + $"{table.TableName}_Insert.sql");
        }

        // генерация кода для процедуры обновления записи
        private void GenerateUpdateProcedure(Table table)
        {
            var result = new List<string>();
            result.Add($"-- Хранимая процедура для обновления записи в таблице {table.TableName}");
            result.Add("DELIMITER $$");
            result.Add($"CREATE PROCEDURE Update{table.TableName}(");

            var parameters = new List<string>();
            foreach (var column in table.Columns)
            {
                string type = GetDataTypeDefinition(column);
                if (type == "INT AUTO_INCREMENT") type = "INT";
                parameters.Add($"{Tab1}IN p_{column.Name} {type}");
            }
            result.Add(string.Join($",{Environment.NewLine}", parameters));
            result.Add(")");
            result.Add("BEGIN");
            result.Add($"{Tab1}UPDATE {table.TableName}");
            result.Add($"{Tab1}SET");

            var setClauses = new List<string>();
            foreach (var column in table.Columns)
            {
                if (!column.IsPrimaryKey)
                {
                    setClauses.Add($"{Tab2}{column.Name} = p_{column.Name}");
                }
            }
            result.Add(string.Join($",{Environment.NewLine}", setClauses));

            var primaryKey = table.Columns.Find(c => c.IsPrimaryKey);
            if (primaryKey != null)
            {
                result.Add($"{Tab1}WHERE {primaryKey.Name} = p_{primaryKey.Name};");
            }
            result.Add("END$$");
            result.Add("DELIMITER ;");
            result.Add("");
            SaveToFile(result, table.FilesPath + $"{table.TableName}_Update.sql");
        }

        // генерация кода для процедуры удаления записи
        private void GenerateDeleteProcedure(Table table)
        {
            var result = new List<string>();
            result.Add($"-- Процедура для удаления записи из таблицы {table.TableName}");
            result.Add("DELIMITER $$");
            result.Add($"CREATE PROCEDURE Delete{table.TableName}(");
            var primary_key = table.Columns.Find(c => c.IsPrimaryKey);
            if (primary_key != null)
            {
                string type = GetDataTypeDefinition(primary_key);
                if (type == "INT AUTO_INCREMENT") type = "INT";
                result.Add($"{Tab1}IN p_{primary_key.Name} {type}");
            }
            result.Add(")");
            result.Add("BEGIN");
            result.Add($"{Tab1}DELETE FROM {table.TableName}");
            if (primary_key != null)
            {
                result.Add($"{Tab1}WHERE {primary_key.Name} = p_{primary_key.Name};");
            }
            result.Add("END$$");
            result.Add("DELIMITER ;");
            result.Add("");
            SaveToFile(result, table.FilesPath + $"{table.TableName}_Delete.sql");
        }

        // генерация кода для процедуры получения записи
        private void GenerateSelectProcedures(Table table)
        {
            GenerateSelectAllProcedure(table);
            GenerateSelectByIdProcedure(table);
        }

        // генерация кода для процедуры получения всех записей
        private void GenerateSelectAllProcedure(Table table)
        {
            var result = new List<string>();
            result.Add($"-- Хранимая процедура для получения всех записей из таблицы {table.TableName}");
            result.Add("DELIMITER $$");
            result.Add($"CREATE PROCEDURE SelectAll{table.TableName}()");
            result.Add("BEGIN");
            result.Add($"{Tab1}SELECT * FROM {table.TableName};");
            result.Add("END$$");
            result.Add("DELIMITER ;");
            result.Add("");
            SaveToFile(result, table.FilesPath + $"{table.TableName}_SelectAll.sql");
        }

        // генерация кода для процедуры получения записи по ID
        private void GenerateSelectByIdProcedure(Table table)
        {
            var result = new List<string>();
            result.Add($"-- Хранимая процедура для получения записи по ID из таблицы {table.TableName}");
            result.Add("DELIMITER $$");
            result.Add($"CREATE PROCEDURE Select{table.TableName}ById(");
            var primary_key = table.Columns.Find(c => c.IsPrimaryKey);
            if (primary_key != null)
            {
                string type = GetDataTypeDefinition(primary_key);
                if (type == "INT AUTO_INCREMENT") type = "INT";
                result.Add($"{Tab1}IN p_{primary_key.Name} {type}");
            }
            result.Add(")");
            result.Add("BEGIN");
            result.Add($"{Tab1}SELECT * FROM {table.TableName}");
            if (primary_key != null)
            {
                result.Add($"{Tab1}WHERE {primary_key.Name} = p_{primary_key.Name};");
            }
            result.Add("END$$");
            result.Add("DELIMITER ;");
            result.Add("");
            SaveToFile(result, table.FilesPath + $"{table.TableName}_SelectById.sql");
        }

        // определение типа данных
        private string GetDataTypeDefinition(Column column)
        {
            string type = column.DataType.ToLower();
            if (type == "varchar")
                return $"VARCHAR({column.Size})";
            if (type == "int")
                return column.Size > 0 ? $"INT({column.Size})" : "INT";
            if (type == "decimal")
            {
                int precision = column.Precision > 0 ? column.Precision : 10;
                int scale = column.Scale >= 0 ? column.Scale : 2;
                return $"DECIMAL({precision}, {scale})";
            }
            if (type == "datetime")
                return "DATETIME";
            if (type == "auto_increment")
                return "INT AUTO_INCREMENT";
            if (type == "boolean")
                return "BOOLEAN";
            return column.DataType.ToUpper();
        }

        // сохранение сгенерированных строк в файл
        private void SaveToFile(List<string> content, string filePath)
        {
            try
            {
                string directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
                File.WriteAllLines(filePath, content, Encoding.UTF8);
                Console.WriteLine($"Файл успешно создан: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении файла {filePath}: {ex.Message}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            FactoryWarehouseCodeGenerator generator = new FactoryWarehouseCodeGenerator();
            string path = @"C:\Users\Rinata\Desktop\КАИ\ТВПП ИТС\SQL_Generator\SQL_queries\";

            // таблица работник
            var workers = new Table { TableName = "workers", FilesPath = path };
            workers.Columns.AddRange(new[]
            {
                new Column { Name = "ID_worker", DataType = "auto_increment", IsPrimaryKey = true, IsNullable = false },
                new Column { Name = "Position", DataType = "VARCHAR", Size = 50, IsNullable = false },
                new Column { Name = "LastName", DataType = "VARCHAR", Size = 50, IsNullable = false },
                new Column { Name = "FirstName", DataType = "VARCHAR", Size = 50, IsNullable = false },
                new Column { Name = "Patronymic", DataType = "VARCHAR", Size = 50, IsNullable = true },
                new Column { Name = "Phone", DataType = "VARCHAR", Size = 20, IsNullable = false }
            });
            generator.GenerateDatabaseCode(workers);

            // таблица цех
            var workshops = new Table { TableName = "workshops", FilesPath = path };
            workshops.Columns.AddRange(new[]
            {
                new Column { Name = "ID_workshop", DataType = "auto_increment", IsPrimaryKey = true, IsNullable = false },
                new Column { Name = "ID_warehouse_keeper", DataType = "INT", IsNullable = false, IsForeignKey = true }
            });
            workshops.ForeignKeys.Add(new ForeignKey
            {
                ColumnName = "ID_warehouse_keeper",
                ReferenceTable = "workers",
                ReferenceColumn = "ID_worker"
            });
            generator.GenerateDatabaseCode(workshops);

            // таблица склад
            var warehouses = new Table { TableName = "warehouses", FilesPath = path };
            warehouses.Columns.AddRange(new[]
            {
                new Column { Name = "ID_warehouse", DataType = "auto_increment", IsPrimaryKey = true, IsNullable = false },
                new Column { Name = "Name", DataType = "VARCHAR", Size = 100, IsNullable = false },
                new Column { Name = "ID_operator", DataType = "INT", IsNullable = false, IsForeignKey = true }
            });
            warehouses.ForeignKeys.Add(new ForeignKey
            {
                ColumnName = "ID_operator",
                ReferenceTable = "workers",
                ReferenceColumn = "ID_worker"
            });
            generator.GenerateDatabaseCode(warehouses);

            // таблица товар
            var products = new Table { TableName = "products", FilesPath = path };
            products.Columns.AddRange(new[]
            {
                new Column { Name = "ID_product", DataType = "auto_increment", IsPrimaryKey = true, IsNullable = false },
                new Column { Name = "Name", DataType = "VARCHAR", Size = 255, IsNullable = false },
                new Column { Name = "Category", DataType = "VARCHAR", Size = 100, IsNullable = false },
                new Column { Name = "Supplier", DataType = "VARCHAR", Size = 255, IsNullable = false },
                new Column { Name = "Quantity", DataType = "INT", IsNullable = false },
                new Column { Name = "Cost", DataType = "DECIMAL", Precision = 10, Scale = 2, IsNullable = false },
                new Column { Name = "Unit", DataType = "VARCHAR", Size = 20, IsNullable = false },
                new Column { Name = "ID_warehouse", DataType = "INT", IsNullable = false, IsForeignKey = true }
            });
            products.ForeignKeys.Add(new ForeignKey
            {
                ColumnName = "ID_warehouse",
                ReferenceTable = "warehouses",
                ReferenceColumn = "ID_warehouse"
            });
            generator.GenerateDatabaseCode(products);

            // таблица экземпляр товара
            var productInstances = new Table { TableName = "product_instances", FilesPath = path };
            productInstances.Columns.AddRange(new[]
            {
                new Column { Name = "Inventory_number", DataType = "INT", IsPrimaryKey = true, IsNullable = false },
                new Column { Name = "Expiration_date", DataType = "DATETIME", IsNullable = true },
                new Column { Name = "ID_product", DataType = "INT", IsNullable = false, IsForeignKey = true }
            });
            productInstances.ForeignKeys.Add(new ForeignKey
            {
                ColumnName = "ID_product",
                ReferenceTable = "products",
                ReferenceColumn = "ID_product"
            });
            generator.GenerateDatabaseCode(productInstances);

            // таблица заявка
            var requests = new Table { TableName = "requests", FilesPath = path };
            requests.Columns.AddRange(new[]
            {
                new Column { Name = "ID_request", DataType = "auto_increment", IsPrimaryKey = true, IsNullable = false },
                new Column { Name = "Workshop_id", DataType = "INT", IsNullable = false, IsForeignKey = true },
                new Column { Name = "Product_id", DataType = "INT", IsNullable = false, IsForeignKey = true },
                new Column { Name = "Quantity", DataType = "INT", IsNullable = false },
                new Column { Name = "Request_date", DataType = "DATETIME", IsNullable = false, DefaultValue = "CURRENT_TIMESTAMP" },
                new Column { Name = "Status", DataType = "VARCHAR", Size = 20, IsNullable = false }
            });
            requests.ForeignKeys.AddRange(new[]
            {
                new ForeignKey { 
                    ColumnName = "Workshop_id", 
                    ReferenceTable = "workshops", 
                    ReferenceColumn = "ID_workshop" 
                },
                new ForeignKey { 
                    ColumnName = "Product_id", 
                    ReferenceTable = "products", 
                    ReferenceColumn = "ID_product" 
                }
            });
            generator.GenerateDatabaseCode(requests);

            string filePath = "";
            Path.GetDirectoryName(filePath);
            Console.WriteLine($"Генерация кода завершена! Файлы с кодом расположены в папке {path}");
            Console.ReadKey();
        }
    }
}