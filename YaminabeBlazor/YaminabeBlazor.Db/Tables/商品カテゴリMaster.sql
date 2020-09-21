CREATE TABLE [dbo].[商品カテゴリMaster]
(
    [カテゴリコード] NVARCHAR(20) NOT NULL PRIMARY KEY, 
    [カテゴリ名] NVARCHAR(50) NOT NULL, 
    [更新日時] DATETIME NOT NULL, 
    [更新者コード] NVARCHAR(20) NOT NULL
)
