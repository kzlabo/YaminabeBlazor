/*
配置後スクリプト テンプレート							
--------------------------------------------------------------------------------------
 このファイルには、ビルド スクリプトに追加される SQL ステートメントが含まれています。		
 SQLCMD 構文を使用して、ファイルを配置後スクリプトにインクルードできます。			
 例:      :r .\myfile.sql								
 SQLCMD 構文を使用して、配置後スクリプト内の変数を参照できます。		
 例:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
SET NOCOUNT ON;

:r .\DevData\開発用変数.sql
:r .\DevData\商品Master.sql
:r .\DevData\商品カテゴリMaster.sql
:r .\DevData\ブランドMaster.sql
:r .\DevData\得意先Master.sql
