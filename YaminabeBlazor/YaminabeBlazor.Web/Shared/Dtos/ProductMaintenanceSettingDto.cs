﻿/*
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
using YaminabeBlazor.Web.Shared.ListItems;

namespace YaminabeBlazor.Web.Shared.Dtos
{
    /// <summary>
    /// 商品マスタメンテメンテナンス設定データの転送モデルを提供します。
    /// </summary>
    /// <revisionHistory>
    ///     <revision date="2020/09/21" version="0.0.1-alfa" author="kzlabo">新規作成。</revision>
    /// </revisionHistory>
    [Serializable]
    public class ProductMaintenanceSettingDto
    {
        #region -------------------- property --------------------

        /// <summary>
        /// ブランドマスタの選択リストモデルを取得または設定します。
        /// </summary>
        public List<BrandListItem> BrandList { get; set; }

        /// <summary>
        /// 商品カテゴリマスタの選択リストモデルを取得または設定します。
        /// </summary>
        public List<ProductCategoryListItem> ProductCategoryList { get; set; }

        /// <summary>
        /// 商品タグ区分マスタの選択リストモデルを取得または設定します。
        /// </summary>
        public List<ProductTagTypeListItem> ProductTagTypeList { get; set; }

        #endregion
    }
}
