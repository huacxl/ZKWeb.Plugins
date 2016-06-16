﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ZKWeb.Plugins.Common.Base.src.Scaffolding;
using ZKWeb.Plugins.Common.Base.src.Model;
using ZKWeb.Templating;
using ZKWebStandard.Extensions;
using ZKWebStandard.Ioc;
using ZKWebStandard.Collection;
using ZKWeb.Plugins.Common.Base.src.Extensions;

namespace ZKWeb.Plugins.Common.Base.src.FormFieldHandlers {
	/// <summary>
	/// 勾选框分组列表
	/// 显示结构
	/// ▢ 全选
	/// 　▢ 分组A
	/// 　　▢ 多选框AA ▢ 多选框AB ▢ 多选框AC
	/// 　▢ 分组B
	/// 　　▢ 多选框BA ▢ 多选框BB ▢ 多选框BC
	/// </summary>
	[ExportMany(ContractKey = typeof(CheckBoxGroupsFieldAttribute)), SingletonReuse]
	public class CheckBoxGroups : IFormFieldHandler {
		/// <summary>
		/// 获取表单字段的html
		/// </summary>
		public string Build(FormField field, IDictionary<string, string> htmlAttributes) {
			var attribute = (CheckBoxGroupsFieldAttribute)field.Attribute;
			var listItemGroupsProvider = (IListItemGroupsProvider)Activator.CreateInstance(attribute.Source);
			var listItemGroups = listItemGroupsProvider.GetGroups().ToList();
			var templateManager = Application.Ioc.Resolve<TemplateManager>();
			var fieldHtml = templateManager.RenderTemplate("tmpl.form.hidden.html", new {
				name = field.Attribute.Name,
				value = JsonConvert.SerializeObject(field.Value ?? new string[0]),
				attributes = htmlAttributes
			});
			var checkboxGroups = templateManager.RenderTemplate("common.base/tmpl.checkbox_groups.html", new {
				groups = listItemGroups.Select(g => new { key = g.Key, items = g }),
				fieldName = field.Attribute.Name,
				fieldHtml = new HtmlString(fieldHtml)
			});
			return field.WrapFieldHtml(htmlAttributes, checkboxGroups);
		}

		/// <summary>
		/// 解析提交的字段的值
		/// </summary>
		public object Parse(FormField field, IList<string> values) {
			return JsonConvert.DeserializeObject<IList<string>>(values[0]);
		}
	}
}
