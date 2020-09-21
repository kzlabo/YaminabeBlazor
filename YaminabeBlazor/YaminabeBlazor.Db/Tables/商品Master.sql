CREATE TABLE [dbo].[商品Master]
(
    [商品コード] NVARCHAR(20) NOT NULL PRIMARY KEY, 
    [商品名] NVARCHAR(50) NOT NULL, 
    [ブランドコード] NVARCHAR(20) NOT NULL, 
    [カテゴリコード] NVARCHAR(20) NOT NULL, 
    [定価] MONEY NOT NULL, 
    [商品タグ] INT NOT NULL,
    [更新日時] DATETIME NOT NULL, 
    [更新者コード] NVARCHAR(20) NOT NULL
)
