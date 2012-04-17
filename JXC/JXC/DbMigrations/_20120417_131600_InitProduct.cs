﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbMigration;
using OEA.Library;
using System.Transactions;

namespace JXC.DbMigrations
{
    public class _20120417_131600_InitProduct : DataMigration
    {
        protected override string GetDescription()
        {
            return "添加 商品 的初始数据。";
        }

        protected override void Up()
        {
            this.RunCode(db =>
            {
                //由于本类没有支持 Down 操作，所以这里面的 Up 需要防止重入。
                var repo = RF.Concreate<ProductCategoryRepository>();
                var list = repo.GetAll();
                if (list.Count == 0)
                {
                    list.Add(new ProductCategory
                    {
                        Name = "服饰类",
                        TreeChildren ={
                            new ProductCategory{ Name = "裤子" },
                            new ProductCategory{ Name = "裙子" },
                            new ProductCategory{ Name = "上衣" },
                            new ProductCategory{ Name = "鞋子" },
                        }
                    });
                    list.Add(new ProductCategory
                    {
                        Name = "食品类",
                        TreeChildren ={
                            new ProductCategory{ Name = "生鲜食品" },
                            new ProductCategory{ Name = "饮料" },
                        }
                    });

                    repo.Save(list);
                }
            });
        }
    }
}
