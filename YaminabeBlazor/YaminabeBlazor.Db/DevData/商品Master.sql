﻿TRUNCATE TABLE [商品Master];

INSERT INTO [商品Master](
	[商品コード], 
	[商品名], 
	[ブランドコード], 
	[カテゴリコード], 
	[定価], 
	[商品タグ], 
	[更新日時], 
	[更新者コード]
)
VALUES
(
	N'000010010001',
	N'ポテトチップス（うすしお）',
	N'00001',
	N'001',
	100,
	0,
	@開発日時,
	@開発者コード
),
(
	N'000010010002',
	N'ポテトチップス（のり塩）',
	N'00001',
	N'001',
	120,
	0,
	@開発日時,
	@開発者コード
),
(
	N'000010010003',
	N'ポテトチップス（コンソメ）',
	N'00001',
	N'001',
	150,
	0,
	@開発日時,
	@開発者コード
),
(
	N'000020020001',
	N'チョコレート（ビター）',
	N'00002',
	N'002',
	200,
	0,
	@開発日時,
	@開発者コード
),
(
	N'000020020002',
	N'チョコレート（ミルク）',
	N'00002',
	N'002',
	230,
	0,
	@開発日時,
	@開発者コード
),
(
	N'000020020003',
	N'チョコレート（ホワイト）',
	N'00002',
	N'002',
	250,
	0,
	@開発日時,
	@開発者コード
),
(
	N'000030010001',
	N'ポテトチップス（ピザ）',
	N'00003',
	N'001',
	200,
	4,
	@開発日時,
	@開発者コード
),
(
	N'000030010002',
	N'ポテトチップス（ピリ辛）',
	N'00003',
	N'001',
	200,
	1,
	@開発日時,
	@開発者コード
),
(
	N'000030010003',
	N'ポテトチップス（ガーリック）',
	N'00003',
	N'001',
	200,
	3,
	@開発日時,
	@開発者コード
),
(
	N'000030030001',
	N'アイスキャンデー',
	N'00003',
	N'003',
	50,
	0,
	@開発日時,
	@開発者コード
),
(
	N'000030030002',
	N'アイスクリーム',
	N'00003',
	N'003',
	50,
	0,
	@開発日時,
	@開発者コード
),
(
	N'000030030003',
	N'アイスシャーベット',
	N'00003',
	N'003',
	50,
	0,
	@開発日時,
	@開発者コード
)