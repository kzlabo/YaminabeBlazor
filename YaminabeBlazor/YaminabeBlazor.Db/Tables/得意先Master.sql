﻿CREATE TABLE [dbo].[得意先Master]
(
    [得意先コード] NVARCHAR(20) NOT NULL PRIMARY KEY, 
    [得意先名] NVARCHAR(50) NOT NULL, 
    [得意先名カナ] NVARCHAR(50) NULL, 
    [得意先略名] NVARCHAR(20) NULL, 
    [設立年月日] DATE NULL, 
    [代表者] NVARCHAR(50) NULL, 
    [郵便番号] NVARCHAR(7) NULL, 
    [住所] NVARCHAR(500) NULL, 
    [電話番号] NVARCHAR(50) NULL, 
    [FAX番号] NVARCHAR(50) NULL, 
    [メールアドレス] NVARCHAR(256) NULL, 
    [ホームページアドレス] NVARCHAR(100) NULL, 
    [締日区分] INT NOT NULL, 
    [締日] INT NOT NULL, 
    [回収日区分] INT NOT NULL, 
    [回収日] INT NOT NULL, 
    [消費税区分] INT NOT NULL, 
    [消費税計算区分] INT NOT NULL, 
    [消費税端数処理区分] INT NOT NULL, 
    [メモ] NVARCHAR(500) NULL,
    [更新日時] DATETIME NOT NULL, 
    [更新者コード] NVARCHAR(20) NOT NULL
)
