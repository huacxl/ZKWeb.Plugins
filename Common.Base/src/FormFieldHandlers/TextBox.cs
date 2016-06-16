﻿using System.Collections.Generic;
using ZKWeb.Localize;
using ZKWeb.Plugins.Common.Base.src.Extensions;
using ZKWeb.Plugins.Common.Base.src.Model;
using ZKWebStandard.Ioc;
using ZKWeb.Templating;

namespace ZKWeb.Plugins.Common.Base.src.FormFieldHandlers {
	/// <summary>
	/// 文本框
	/// </summary>
	[ExportMany(ContractKey = typeof(TextBoxFieldAttribute)), SingletonReuse]
	public class TextBox : IFormFieldHandler {
		/// <summary>
		/// 获取表单字段的html
		/// </summary>
		public string Build(FormField field, IDictionary<string, string> htmlAttributes) {
			var attribute = (TextBoxFieldAttribute)field.Attribute;
			var templateManager = Application.Ioc.Resolve<TemplateManager>();
			var password = templateManager.RenderTemplate("tmpl.form.text.html", new {
				name = field.Attribute.Name,
				value = (field.Value ?? "").ToString(),
				placeholder = new T(attribute.PlaceHolder),
				attributes = htmlAttributes
			});
			return field.WrapFieldHtml(htmlAttributes, password);
		}

		/// <summary>
		/// 解析提交的字段的值
		/// </summary>
		public object Parse(FormField field, IList<string> values) {
			return values[0];
		}
	}
}
