﻿TRUNCATE TABLE [得意先Master];

INSERT INTO [得意先Master](
	[得意先コード],
	[得意先名],
	[得意先名カナ],
	[得意先略名],
	[設立年月日],
	[代表者],
	[郵便番号],
	[住所],
	[電話番号],
	[FAX番号],
	[メールアドレス],
	[ホームページアドレス],
	[締日区分],
	[締日],
	[回収日区分],
	[回収日],
	[消費税区分],
	[消費税計算区分],
	[消費税端数処理区分],
	[メモ],
	[更新日時],
	[更新者コード]
)
VALUES
(
	N'0000000001',
	N'アルファ商店',
	N'ｱﾙﾌｧｼｮｳﾃﾝ',
	N'α商店',
	N'2020/04/01',
	N'アルファ 一郎',
	N'0000000',
	N'〇〇都〇〇区〇〇丁目〇〇番 〇〇ビル〇〇階',
	N'0000000000',
	N'0000000000',
	N'alfa@test.test.test',
	N'http://alfa.test.com/',
	0,
	0,
	0,
	0,
	0,
	0,
	0,
	NULL,
	@開発日時,
	@開発者コード
),
(
	N'0000000002',
	N'ベータ商店',
	N'ﾍﾞｰﾀｼｮｳﾃﾝ',
	N'β商店',
	N'1920/04/01',
	N'ベータ 太郎',
	N'0000000',
	N'〇〇都〇〇区〇〇丁目〇〇番 〇〇ビル〇〇階',
	N'0000000000',
	N'0000000000',
	N'beta@test.test.test',
	N'http://beta.test.com/',
	0,
	0,
	0,
	0,
	0,
	0,
	0,
	NULL,
	@開発日時,
	@開発者コード
),
(
	N'0000000003',
	N'ガンマ商店',
	N'ｶﾞﾝﾏｼｮｳﾃﾝ',
	N'γ商店',
	N'1999/12/31',
	N'ガンマ 大治郎',
	N'0000000',
	N'〇〇都〇〇区〇〇丁目〇〇番 〇〇ビル〇〇階',
	N'0000000000',
	N'0000000000',
	N'gamma@test.test.test',
	N'http://gamma.test.com/',
	0,
	0,
	0,
	0,
	0,
	0,
	0,
	NULL,
	@開発日時,
	@開発者コード
);