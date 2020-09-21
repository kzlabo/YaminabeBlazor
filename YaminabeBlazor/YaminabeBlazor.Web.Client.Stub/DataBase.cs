/*
 * Copyright 2020 kzlabo
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using YaminabeBlazor.Core.Enums;
using YaminabeBlazor.Core.Factories;
using YaminabeBlazor.Core.Models;

namespace YaminabeBlazor.Web.Client.Stub
{
    /// <summary>
    /// クライアントのみ実行用のオンメモリデータベースを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    public static class DataBase
    {
        #region -------------------- property --------------------

        /// <summary>
        /// 商品マスタを取得または設定します。
        /// </summary>
        internal static List<ProductEntityModel> Products { get; set; }

        /// <summary>
        /// 商品カテゴリマスタを取得または設定します。
        /// </summary>
        internal static List<ProductCategoryEntityModel> ProductCategories { get; set; }

        /// <summary>
        /// ブランドマスタを取得または設定します。
        /// </summary>
        internal static List<BrandEntityModel> Brands { get; set; }

        /// <summary>
        /// 得意先マスタを取得または設定します。
        /// </summary>
        internal static List<CustomerEntityModel> Customers { get; set; }

        /// <summary>
        /// 回収日区分を取得または設定します。
        /// </summary>
        internal static IReadOnlyList<CollectionDateTypeModel> CollectionDateTypes { get; set; }

        /// <summary>
        /// 締日区分を取得または設定します。
        /// </summary>
        internal static IReadOnlyList<CutoffDateTypeModel> CutoffDateTypes { get; set; }

        /// <summary>
        /// 消費税区分を取得または設定します。
        /// </summary>
        internal static IReadOnlyList<TaxTypeModel> TaxTypes { get; set; }

        /// <summary>
        /// 消費税端数処理区分を取得または設定します。
        /// </summary>
        internal static IReadOnlyList<TaxRoundTypeModel> TaxRoundTypes { get; set; }

        /// <summary>
        /// 消費税計算区分を取得または設定します。
        /// </summary>
        internal static IReadOnlyList<TaxCalcTypeModel> TaxCalcTypes { get; set; }

        /// <summary>
        /// 商品タグ区分を取得または設定します。
        /// </summary>
        internal static IReadOnlyList<ProductTagTypeModel> ProductTagTypes { get; set; }

        #endregion

        #region -------------------- constructor --------------------

        /// <summary>
        /// <see cref="DataBase"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        static DataBase()
        {
            // 商品マスタ
            DataBase.Products = new List<ProductEntityModel>()
            {
                ProductFactory.Load(
                    productId: "000010010001",
                    productName: "ポテトチップス（うすしお）",
                    brandId: "00001",
                    catetoryId: "001",
                    listPrice: 100,
                    productTagType: ProductTagTypeOptions.None,
                    updateDateTime: DateTime.Now,
                    updateUserId: "Stub"
                    ),
                ProductFactory.Load(
                    productId: "000010010002",
                    productName: "ポテトチップス（のり塩）",
                    brandId: "00001",
                    catetoryId: "001",
                    listPrice: 120,
                    productTagType: ProductTagTypeOptions.None,
                    updateDateTime: DateTime.Now,
                    updateUserId: "Stub"
                    ),
                ProductFactory.Load(
                    productId: "000010010003",
                    productName: "ポテトチップス（コンソメ）",
                    brandId: "00001",
                    catetoryId: "001",
                    listPrice: 150,
                    productTagType: ProductTagTypeOptions.None,
                    updateDateTime: DateTime.Now,
                    updateUserId: "Stub"
                    ),
                ProductFactory.Load(
                    productId: "000020020001",
                    productName: "チョコレート（ビター）",
                    brandId: "00002",
                    catetoryId: "002",
                    listPrice: 200,
                    productTagType: ProductTagTypeOptions.None,
                    updateDateTime: DateTime.Now,
                    updateUserId: "Stub"
                    ),
                ProductFactory.Load(
                    productId: "000020020002",
                    productName: "チョコレート（ミルク）",
                    brandId: "00002",
                    catetoryId: "002",
                    listPrice: 230,
                    productTagType: ProductTagTypeOptions.None,
                    updateDateTime: DateTime.Now,
                    updateUserId: "Stub"
                    ),
                ProductFactory.Load(
                    productId: "000020020003",
                    productName: "チョコレート（ホワイト）",
                    brandId: "00002",
                    catetoryId: "002",
                    listPrice: 250,
                    productTagType: ProductTagTypeOptions.None,
                    updateDateTime: DateTime.Now,
                    updateUserId: "Stub"
                    ),
                ProductFactory.Load(
                    productId: "000030010001",
                    productName: "ポテトチップス（ピザ）",
                    brandId: "00003",
                    catetoryId: "001",
                    listPrice: 200,
                    productTagType: ProductTagTypeOptions.MadeToOrder,
                    updateDateTime: DateTime.Now,
                    updateUserId: "Stub"
                    ),
                ProductFactory.Load(
                    productId: "000030010002",
                    productName: "ポテトチップス（ピリ辛）",
                    brandId: "00003",
                    catetoryId: "001",
                    listPrice: 200,
                    productTagType: ProductTagTypeOptions.LimitedEdition,
                    updateDateTime: DateTime.Now,
                    updateUserId: "Stub"
                    ),
                ProductFactory.Load(
                    productId: "000030010003",
                    productName: "ポテトチップス（ガーリック）",
                    brandId: "00003",
                    catetoryId: "001",
                    listPrice: 200,
                    productTagType: ProductTagTypeOptions.LimitedEdition | ProductTagTypeOptions.EndOfSale,
                    updateDateTime: DateTime.Now,
                    updateUserId: "Stub"
                    ),
                ProductFactory.Load(
                    productId: "000030030001",
                    productName: "アイスキャンデー",
                    brandId: "00003",
                    catetoryId: "003",
                    listPrice: 50,
                    productTagType: ProductTagTypeOptions.None,
                    updateDateTime: DateTime.Now,
                    updateUserId: "Stub"
                    ),
                ProductFactory.Load(
                    productId: "000030030002",
                    productName: "アイスクリーム",
                    brandId: "00003",
                    catetoryId: "003",
                    listPrice: 50,
                    productTagType: ProductTagTypeOptions.None,
                    updateDateTime: DateTime.Now,
                    updateUserId: "Stub"
                    ),
                ProductFactory.Load(
                    productId: "000030030003",
                    productName: "アイスシャーベット",
                    brandId: "00003",
                    catetoryId: "003",
                    listPrice: 50,
                    productTagType: ProductTagTypeOptions.None,
                    updateDateTime: DateTime.Now,
                    updateUserId: "Stub"
                    )
            };

            // 商品カテゴリマスタ
            DataBase.ProductCategories = new List<ProductCategoryEntityModel>()
            {
                ProductCategoryFactory.Load(
                    categoryId: "001",
                    categoryName: "ポテトチップス",
                    updateDateTime: DateTime.Now,
                    updateUserId: "Stub"
                    ),
                ProductCategoryFactory.Load(
                    categoryId: "002",
                    categoryName: "チョコレート",
                    updateDateTime: DateTime.Now,
                    updateUserId: "Stub"
                    ),
                ProductCategoryFactory.Load(
                    categoryId: "003",
                    categoryName: "アイス",
                    updateDateTime: DateTime.Now,
                    updateUserId: "Stub"
                    )
            };

            // ブランドマスタ
            DataBase.Brands = new List<BrandEntityModel>()
            {
                BrandFactory.Load(
                    brandId: "00001",
                    brandName: "メーカーＡ",
                    note: "ポテトチップス専門メーカー。業界最大手とされている。",
                    updateDateTime: DateTime.Now,
                    updateUserId: "Stub"
                    ),
                BrandFactory.Load(
                    brandId: "00002",
                    brandName: "メーカーＢ",
                    note: "チョコレート専門メーカー。",
                    updateDateTime: DateTime.Now,
                    updateUserId: "Stub"
                    ),
                BrandFactory.Load(
                    brandId: "00003",
                    brandName: "メーカーＣ",
                    note: "総合お菓子メーカー。ポテトチップス・チョコレート・アイスと幅広く商品展開を行っている。",
                    updateDateTime: DateTime.Now,
                    updateUserId: "Stub"
                    )
            };

            // 得意先マスタ
            DataBase.Customers = new List<CustomerEntityModel>()
            {
                CustomerFactory.Load(
                    customerId: "0000000001",
                    customerName: "アルファ商店",
                    customerKanaName: "ｱﾙﾌｧｼｮｳﾃﾝ",
                    customerShortName: "α商店",
                    establishmentDate: DateTime.Parse("2020/04/01"),
                    ceo: "アルファ 一郎",
                    postalCode: "0000000",
                    address: "〇〇都〇〇区〇〇丁目〇〇番 〇〇ビル〇〇階",
                    tel: "0000000000",
                    fax: "0000000000",
                    mail: "alfa@test.test.test",
                    hp: "http://alfa.test.com/",
                    cutoffDateType: CutoffDateTypeOptions.None,
                    cutoffDate: 0,
                    collectionDateType: CollectionDateTypeOptions.None,
                    collectionDate: 0,
                    taxType: TaxTypeOptions.None,
                    taxCalcType: TaxCalcTypeOptions.None,
                    taxRoundType: TaxRoundTypeOptions.None,
                    note: null,
                    updateDateTime: DateTime.Now,
                    updateUserId: "Stub"
                    ),
                CustomerFactory.Load(
                    customerId: "0000000002",
                    customerName: "ベータ商店",
                    customerKanaName: "ﾍﾞｰﾀｼｮｳﾃﾝ",
                    customerShortName: "β商店",
                    establishmentDate: DateTime.Parse("1920/04/01"),
                    ceo: "ベータ 太郎",
                    postalCode: "0000000",
                    address: "〇〇都〇〇区〇〇丁目〇〇番 〇〇ビル〇〇階",
                    tel: "0000000000",
                    fax: "0000000000",
                    mail: "beta@test.test.test",
                    hp: "http://beta.test.com/",
                    cutoffDateType: CutoffDateTypeOptions.None,
                    cutoffDate: 0,
                    collectionDateType: CollectionDateTypeOptions.None,
                    collectionDate: 0,
                    taxType: TaxTypeOptions.None,
                    taxCalcType: TaxCalcTypeOptions.None,
                    taxRoundType: TaxRoundTypeOptions.None,
                    note: null,
                    updateDateTime: DateTime.Now,
                    updateUserId: "Stub"
                    ),
                CustomerFactory.Load(
                    customerId: "0000000003",
                    customerName: "ガンマ商店",
                    customerKanaName: "ｶﾞﾝﾏｼｮｳﾃﾝ",
                    customerShortName: "γ商店",
                    establishmentDate: DateTime.Parse("1999/12/31"),
                    ceo: "ガンマ 大治郎",
                    postalCode: "0000000",
                    address: "〇〇都〇〇区〇〇丁目〇〇番 〇〇ビル〇〇階",
                    tel: "0000000000",
                    fax: "0000000000",
                    mail: "gamma@test.test.test",
                    hp: "http://gamma.test.com/",
                    cutoffDateType: CutoffDateTypeOptions.None,
                    cutoffDate: 0,
                    collectionDateType: CollectionDateTypeOptions.None,
                    collectionDate: 0,
                    taxType: TaxTypeOptions.None,
                    taxCalcType: TaxCalcTypeOptions.None,
                    taxRoundType: TaxRoundTypeOptions.None,
                    note: null,
                    updateDateTime: DateTime.Now,
                    updateUserId: "Stub"
                    )
            };

            // 回収日区分
            DataBase.CollectionDateTypes = CollectionDateTypeFactory.CreateInitialData();

            // 締日区分
            DataBase.CutoffDateTypes = CutoffDateTypeFactory.CreateInitialData();

            // 消費税区分
            DataBase.TaxTypes = TaxTypeFactory.CreateInitialData();

            // 消費税端数処理区分
            DataBase.TaxRoundTypes = TaxRoundTypeFactory.CreateInitialData();

            // 消費税計算区分
            DataBase.TaxCalcTypes = TaxCalcTypeFactory.CreateInitialData();

            // 商品タグ区分
            DataBase.ProductTagTypes = ProductTagTypeFactory.CreateInitialData();

            // 1000件越えテスト用
            for (var i = 0; i < 1000; i++)
            {
                DataBase.Products.Add(
                    ProductFactory.Load(
                        productId: $"99999999{i:4}",
                        productName: $"テスト商品 {i}",
                        brandId: "99999",
                        catetoryId: "999",
                        listPrice: 9999,
                        productTagType: ProductTagTypeOptions.None,
                        updateDateTime: DateTime.Now,
                        updateUserId: "Stub"
                        )); ;
            }
            DataBase.ProductCategories.Add(
                ProductCategoryFactory.Load(
                    categoryId: "999",
                    categoryName: "テストお菓子",
                    updateDateTime: DateTime.Now,
                    updateUserId: "Stub"
                    ));
            DataBase.Brands.Add(
                BrandFactory.Load(
                    brandId: "99999",
                    brandName: "テストメーカー",
                    note: "1000件テスト用",
                    updateDateTime: DateTime.Now,
                    updateUserId: "Stub"
                    ));
        }

        #endregion
    }
}
