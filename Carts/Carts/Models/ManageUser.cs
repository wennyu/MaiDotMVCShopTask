using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;//可利用「錯誤>顯示可能的修正」
using System.Linq;
using System.Web;

//在Models中新增ManageUser類別，其中包含Id(會員編號)、UserName(會員暱稱)、Email(電子郵件)，
//此類別是給會員管理中顯示會員列表(Index)與編輯會員(Edit)使用的。

namespace Carts.Models
{
    public class ManageUser
    {
        [Required] //必要欄位
        public string Id { get; set; }

        [Required] //必要欄位
        [StringLength(maximumLength: 256, ErrorMessage = "{0} 的長度至少必須為 {2} 個字元。", MinimumLength = 1)] // 字元長度1~256
        [Display(Name = "暱稱")] //欄位顯示文字
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "電子郵件")]
        public string Email { get; set; }
    }
}