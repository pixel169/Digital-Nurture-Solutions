CREATE DATABASE ProductsDB;
GO
USE ProductsDB;
GO

CREATE TABLE products (
    product_id INT IDENTITY(1,1) PRIMARY KEY,
    product_name NVARCHAR(100) NOT NULL,
    category NVARCHAR(50) NOT NULL,
    price DECIMAL(10,2) NOT NULL,
    brand NVARCHAR(50),
    launch_date DATE
);
GO

INSERT INTO products (product_name, category, price, brand, launch_date) VALUES
-- Electronics
('Samsung Galaxy S24', 'Electronics', 79999.00, 'Samsung', '2024-01-15'),
('iPhone 15', 'Electronics', 79900.00, 'Apple', '2023-09-20'),
('OnePlus 12', 'Electronics', 64999.00, 'OnePlus', '2024-01-10'),
('Xiaomi 14', 'Electronics', 54999.00, 'Xiaomi', '2024-02-25'),
('Realme GT 6', 'Electronics', 42999.00, 'Realme', '2024-03-15'),
('Vivo V30 Pro', 'Electronics', 41999.00, 'Vivo', '2024-02-28'),

-- Clothing
('Fabindia Kurta Set', 'Clothing', 2499.00, 'Fabindia', '2024-01-01'),
('Allen Solly Shirt', 'Clothing', 1899.00, 'Allen Solly', '2024-01-15'),
('Levis Jeans', 'Clothing', 3499.00, 'Levis', '2024-02-01'),
('Raymond Suit', 'Clothing', 8999.00, 'Raymond', '2024-01-20'),
('Peter England Blazer', 'Clothing', 4999.00, 'Peter England', '2024-02-10'),
('Van Heusen Formal Shirt', 'Clothing', 1899.00, 'Van Heusen', '2024-01-25'),

-- Food & Beverages
('Amul Dark Chocolate', 'Food & Beverages', 299.00, 'Amul', '2024-01-01'),
('Tata Tea Premium', 'Food & Beverages', 450.00, 'Tata', '2024-01-05'),
('Britannia Good Day Cookies', 'Food & Beverages', 65.00, 'Britannia', '2024-01-10'),
('Haldirams Bhujia', 'Food & Beverages', 180.00, 'Haldirams', '2024-01-15'),
('MTR Ready Mix', 'Food & Beverages', 120.00, 'MTR', '2024-01-20'),
('Patanjali Honey', 'Food & Beverages', 450.00, 'Patanjali', '2024-01-25'),

-- Home & Kitchen
('Prestige Pressure Cooker', 'Home & Kitchen', 2299.00, 'Prestige', '2024-01-01'),
('Bajaj Mixer Grinder', 'Home & Kitchen', 4599.00, 'Bajaj', '2024-01-10'),
('Godrej Refrigerator', 'Home & Kitchen', 28999.00, 'Godrej', '2024-01-15'),
('LG Washing Machine', 'Home & Kitchen', 35999.00, 'LG', '2024-01-20'),
('Philips Air Fryer', 'Home & Kitchen', 8999.00, 'Philips', '2024-01-25'),
('Havells Ceiling Fan', 'Home & Kitchen', 2299.00, 'Havells', '2024-02-01');
GO

-- 1. ROW_NUMBER() - Top 3 products in each category (unique ranking)
WITH RankedProducts AS (
    SELECT 
        product_name,
        category,
        price,
        brand,
        ROW_NUMBER() OVER (PARTITION BY category ORDER BY price DESC) as row_num
    FROM products
)
SELECT 
    product_name,
    category,
    FORMAT(price, 'N2') as price_formatted,
    brand,
    row_num
FROM RankedProducts
WHERE row_num <= 3
ORDER BY category, row_num;
GO

-- 2. RANK() - Top 3 ranks in each category
WITH RankedProducts AS (
    SELECT 
        product_name,
        category,
        price,
        brand,
        RANK() OVER (PARTITION BY category ORDER BY price DESC) as rank_num
    FROM products
)
SELECT 
    product_name,
    category,
    FORMAT(price, 'N2') as price_formatted,
    brand,
    rank_num
FROM RankedProducts
WHERE rank_num <= 3
ORDER BY category, rank_num;
GO

-- 3. DENSE_RANK() - Top 3 dense ranks in each category
WITH DenseRankedProducts AS (
    SELECT 
        product_name,
        category,
        price,
        brand,
        DENSE_RANK() OVER (PARTITION BY category ORDER BY price DESC) as dense_rank_num
    FROM products
)
SELECT 
    product_name,
    category,
    FORMAT(price, 'N2') as price_formatted,
    brand,
    dense_rank_num
FROM DenseRankedProducts
WHERE dense_rank_num <= 3
ORDER BY category, dense_rank_num;
GO
SELECT 
    product_name,
    category,
    FORMAT(price, 'N2') as price_formatted,
    brand,
    ROW_NUMBER() OVER (PARTITION BY category ORDER BY price DESC) as row_num,
    RANK() OVER (PARTITION BY category ORDER BY price DESC) as rank_num,
    DENSE_RANK() OVER (PARTITION BY category ORDER BY price DESC) as dense_rank_num
FROM products
ORDER BY category, price DESC;
GO