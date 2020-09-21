CREATE TABLE [dbo].[ブランドMaster]
(
    [ブランドコード] NVARCHAR(20) NOT NULL PRIMARY KEY, 
    [ブランド名] NVARCHAR(50) NOT NULL, 
    [メモ] NVARCHAR(500) NULL, 
    [更新日時] DATETIME NOT NULL, 
    [更新者コード] NVARCHAR(20) NOT NULL,
)
