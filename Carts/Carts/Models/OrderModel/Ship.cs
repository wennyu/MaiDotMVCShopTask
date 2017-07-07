using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Carts.Models.OrderModel
{
    /// <summary>
    /// 
    /// </summary>
    public class Ship
    {
        /// <summary>
        /// 收貨人姓名
        /// </summary>
        [Required]
        [Display(Name = "收貨人姓名")]
        [StringLength(maximumLength: 30, ErrorMessage = "{0} 的長度至少必須為 {2} 個字元。"
            , MinimumLength = 2)]// 字元長度：2~30
        public string ReceiverName { get; set; }

        /// <summary>
        /// 收貨人電話
        /// </summary>
        [Required]
        [Display(Name = "收貨人電話")]
        [StringLength(maximumLength: 15, ErrorMessage = "{0}的長度至少必須為 {2} 個字元。"
            , MinimumLength = 8)]// 字元長度：8~15
        public string ReceiverPhone { get; set; }

        /// <summary>
        /// Gets or sets the receiver address.收貨人住址
        /// </summary>
        [Required]
        [Display(Name = "收貨人地址")]
        [StringLength(maximumLength: 256, ErrorMessage = "{0} 的長度至少必須為 {2} 個字元。"
            ,MinimumLength =8)]// 字元長度：8~256
        public string ReceiverAddress { get; set; }

    }
}