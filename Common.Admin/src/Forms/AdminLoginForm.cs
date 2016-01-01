﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZKWeb.Core;
using ZKWeb.Plugins.Common.Base.src;
using ZKWeb.Plugins.Common.Base.src.Model;

namespace ZKWeb.Plugins.Common.Admin.src.Forms {
	/// <summary>
	/// 管理员登陆
	/// </summary>
	[Form("AdminLoginForm", SubmitButtonText = "Login")]
	public class AdminLoginForm : ModelFormBuilder {
		/// <summary>
		/// 用户名
		/// </summary>
		[TextBoxField("Username", "Please enter username")]
		[Required]
		public string Username { get; set; }
		/// <summary>
		/// 密码
		/// </summary>
		[PasswordField("Password", "Please enter password")]
		[Required]
		public string Password { get; set; }
		/// <summary>
		/// 验证码
		/// </summary>
		[CaptchaField("Captcha", "Common.Admin.AdminLoginCaptcha", "Please enter captcha")]
		[Required]
		public string Captcha { get; set; }
		/// <summary>
		/// 记住登陆
		/// </summary>
		[CheckBoxField("RememberLogin")]
		public bool RememberLogin { get; set; }
		
		/// <summary>
		/// 绑定
		/// </summary>
		protected override void OnBind() { }

		/// <summary>
		/// 提交
		/// </summary>
		/// <returns></returns>
		protected override object OnSubmit() {
			if (Username == "admin" && Password == "123456") {
				return new { success = true, message = "login success" };
			}
			return new { message = new T("Incorrect username or password") };
		}
	}
}