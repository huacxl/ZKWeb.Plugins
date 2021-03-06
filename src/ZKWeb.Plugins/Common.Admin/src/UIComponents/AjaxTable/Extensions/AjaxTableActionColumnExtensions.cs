﻿using System;
using ZKWeb.Database;
using ZKWeb.Localize;
using ZKWeb.Plugins.Common.Admin.src.Controllers.Interfaces;
using ZKWeb.Plugins.Common.Base.src.UIComponents.AjaxTable;
using ZKWeb.Plugins.Common.Base.src.UIComponents.AjaxTable.Extensions;
using ZKWeb.Plugins.Common.Base.src.UIComponents.ScriptStrings;
using ZKWebStandard.Extensions;

namespace ZKWeb.Plugins.Common.Admin.src.UIComponents.AjaxTable.Extensions {
	/// <summary>
	/// Ajax表格操作列的扩展函数
	/// </summary>
	public static class AjaxTableActionColumnExtensions {
		/// <summary>
		/// 添加查看按钮
		/// 点击后弹出编辑数据的模态框
		/// </summary>
		/// <param name="column">操作列</param>
		/// <param name="typeName">类型名称</param>
		/// <param name="editUrl">编辑使用的Url</param>
		/// <param name="name">名称，不指定时使用默认值</param>
		/// <param name="buttonClass">按钮的Css类，不指定时使用默认值</param>
		/// <param name="iconClass">图标的Css类，不指定时使用默认值</param>
		/// <param name="titleTemplate">标题的模板，格式是underscore.js的格式，参数传入row</param>
		/// <param name="urlTemplate">编辑Url的模板，格式是underscore.js的格式，参数传入row</param>
		/// <param name="dialogParameters">弹出框的参数，不指定时使用默认值</param>
		public static void AddEditAction(
			this AjaxTableActionColumn column, string typeName, string editUrl,
			string name = null, string buttonClass = null, string iconClass = null,
			string titleTemplate = null, string urlTemplate = null, object dialogParameters = null) {
			column.AddRemoteModalForBelongedRow(
				name ?? new T("View"),
				buttonClass ?? "btn btn-xs btn-info",
				iconClass ?? "fa fa-edit",
				titleTemplate ?? new T("Edit {0}", new T(typeName)),
				urlTemplate ?? (editUrl + "?id=<%-row.Id%>"),
				dialogParameters);
		}

		/// <summary>
		/// 添加查看按钮
		/// 点击后弹出编辑数据的模态框
		/// 根据增删查改的控制器自动生成，各个参数如不指定则使用默认值
		/// </summary>
		/// <typeparam name="TCrudController">控制器类型</typeparam>
		public static void AddEditActionFor<TCrudController>(
			this AjaxTableActionColumn column,
			string name = null, string buttonClass = null, string iconClass = null,
			string titleTemplate = null, string urlTemplate = null, object dialogParameters = null)
			where TCrudController : class, ICrudController, new() {
			var app = new TCrudController();
			column.AddEditAction(app.EntityTypeName, app.EditUrl,
				name, buttonClass, iconClass, titleTemplate, urlTemplate, dialogParameters);
		}

		/// <summary>
		/// 添加删除按钮
		/// 点击后弹出确认框，确认后把json=[数据Id]提交到删除url
		/// </summary>
		/// <param name="column">操作列</param>
		/// <param name="typeName">类型名称</param>
		/// <param name="deleteUrl">删除使用的Url</param>
		/// <param name="name">名称，不指定时使用默认值</param>
		/// <param name="buttonClass">按钮的Css类，不指定时使用默认值</param>
		/// <param name="iconClass">图标的Css类，不指定时使用默认值</param>
		/// <param name="titleTemplate">标题的模板，格式是underscore.js的格式，参数传入rows</param>
		/// <param name="urlTemplate">编辑Url的模板，格式是underscore.js的格式，参数传入rows</param>
		/// <param name="primaryKey">数据Id保存的名称，不指定时使用默认值</param>
		/// <param name="dialogParameters">弹出框的参数，不指定时使用默认值</param>
		public static void AddDeleteAction(
			this AjaxTableActionColumn column, string typeName, string deleteUrl,
			string name = null, string buttonClass = null, string iconClass = null,
			string titleTemplate = null, string urlTemplate = null,
			string primaryKey = null, object dialogParameters = null) {
			primaryKey = primaryKey ?? nameof(IEntity<Guid>.Id);
			column.AddConfirmActionForBelongedRow(
				name ?? new T("Delete"),
				buttonClass ?? "btn btn-xs btn-danger",
				iconClass ?? "fa fa-remove",
				titleTemplate ?? new T("Delete {0}", new T(typeName)),
				BaseScriptStrings.ConfirmMessageTemplateForMultiSelected(
					new T("Sure to delete following {0}?", new T(typeName)), "ToString"),
				BaseScriptStrings.PostConfirmedActionForMultiSelected(primaryKey, deleteUrl),
				dialogParameters ?? new { type = "type-danger" });
		}

		/// <summary>
		/// 添加删除按钮
		/// 根据增删查改的控制器自动生成，各个参数如不指定则使用默认值
		/// </summary>
		/// <typeparam name="TCrudController">控制器类型</typeparam>
		public static void AddDeleteActionFor<TCrudController>(
			this AjaxTableActionColumn column,
			string name = null, string buttonClass = null, string iconClass = null,
			string titleTemplate = null, string urlTemplate = null,
			string primaryKey = null, object dialogParameters = null)
			where TCrudController : class, ICrudController, new() {
			var app = new TCrudController();
			column.AddDeleteAction(app.EntityTypeName,
				app.BatchUrl + "?action=delete", name, buttonClass, iconClass,
				titleTemplate, urlTemplate, primaryKey, dialogParameters);
		}

		/// <summary>
		/// 添加恢复按钮
		/// 点击后弹出确认框，确认后把json=[数据Id]提交到恢复url
		/// </summary>
		/// <param name="column">操作列</param>
		/// <param name="typeName">类型名称</param>
		/// <param name="recoverUrl">恢复使用的Url</param>
		/// <param name="name">名称，不指定时使用默认值</param>
		/// <param name="buttonClass">按钮的Css类，不指定时使用默认值</param>
		/// <param name="iconClass">图标的Css类，不指定时使用默认值</param>
		/// <param name="titleTemplate">标题的模板，格式是underscore.js的格式，参数传入rows</param>
		/// <param name="urlTemplate">编辑Url的模板，格式是underscore.js的格式，参数传入rows</param>
		/// <param name="primaryKey">数据Id保存的名称，不指定时使用默认值</param>
		/// <param name="dialogParameters">弹出框的参数，不指定时使用默认值</param>
		public static void AddRecoverAction(
			this AjaxTableActionColumn column, string typeName, string recoverUrl,
			string name = null, string buttonClass = null, string iconClass = null,
			string titleTemplate = null, string urlTemplate = null,
			string primaryKey = null, object dialogParameters = null) {
			primaryKey = primaryKey ?? nameof(IEntity<Guid>.Id);
			column.AddConfirmActionForBelongedRow(
				name ?? new T("Recover"),
				buttonClass ?? "btn btn-xs btn-info",
				iconClass ?? "fa fa-history",
				titleTemplate ?? new T("Recover {0}", new T(typeName)),
				BaseScriptStrings.ConfirmMessageTemplateForMultiSelected(
					new T("Sure to recover following {0}?", new T(typeName)), "ToString"),
				BaseScriptStrings.PostConfirmedActionForMultiSelected(primaryKey, recoverUrl),
				dialogParameters);
		}

		/// <summary>
		/// 添加恢复按钮
		/// 根据增删查改的控制器自动生成，各个参数如不指定则使用默认值
		/// </summary>
		/// <typeparam name="TCrudController">控制器类型</typeparam>
		public static void AddRecoverActionFor<TCrudController>(
			this AjaxTableActionColumn column,
			string name = null, string buttonClass = null, string iconClass = null,
			string titleTemplate = null, string urlTemplate = null,
			string primaryKey = null, object dialogParameters = null)
			where TCrudController : class, ICrudController, new() {
			var app = new TCrudController();
			column.AddRecoverAction(app.EntityTypeName,
				app.BatchUrl + "?action=recover", name, buttonClass, iconClass,
				titleTemplate, urlTemplate, primaryKey, dialogParameters);
		}

		/// <summary>
		/// 添加永久删除按钮
		/// 点击后弹出确认框，确认后把json=[数据Id]提交到永久删除url
		/// </summary>
		/// <param name="column">操作列</param>
		/// <param name="typeName">类型名称</param>
		/// <param name="deleteForeverUrl">永久删除使用的Url</param>
		/// <param name="name">名称，不指定时使用默认值</param>
		/// <param name="buttonClass">按钮的Css类，不指定时使用默认值</param>
		/// <param name="iconClass">图标的Css类，不指定时使用默认值</param>
		/// <param name="titleTemplate">标题的模板，格式是underscore.js的格式，参数传入rows</param>
		/// <param name="urlTemplate">编辑Url的模板，格式是underscore.js的格式，参数传入rows</param>
		/// <param name="primaryKey">数据Id保存的名称，不指定时使用默认值</param>
		/// <param name="dialogParameters">弹出框的参数，不指定时使用默认值</param>
		public static void AddDeleteForeverAction(
			this AjaxTableActionColumn column, string typeName, string deleteForeverUrl,
			string name = null, string buttonClass = null, string iconClass = null,
			string titleTemplate = null, string urlTemplate = null,
			string primaryKey = null, object dialogParameters = null) {
			primaryKey = primaryKey ?? nameof(IEntity<Guid>.Id);
			column.AddConfirmActionForBelongedRow(
				name ?? new T("Delete Forever"),
				buttonClass ?? "btn btn-xs btn-danger",
				iconClass ?? "fa fa-remove",
				titleTemplate ?? new T("Delete {0} Forever", new T(typeName)),
				BaseScriptStrings.ConfirmMessageTemplateForMultiSelected(
					new T("Sure to delete following {0} forever?", new T(typeName)), "ToString"),
				BaseScriptStrings.PostConfirmedActionForMultiSelected(primaryKey, deleteForeverUrl),
				dialogParameters ?? new { type = "type-danger" });
		}

		/// <summary>
		/// 添加永久删除按钮
		/// 根据增删查改的控制器自动生成，各个参数如不指定则使用默认值
		/// </summary>
		/// <typeparam name="TCrudController">控制器类型</typeparam>
		public static void AddDeleteForeverActionFor<TCrudController>(
			this AjaxTableActionColumn column,
			string name = null, string buttonClass = null, string iconClass = null,
			string titleTemplate = null, string urlTemplate = null,
			string primaryKey = null, object dialogParameters = null)
			where TCrudController : class, ICrudController, new() {
			var app = new TCrudController();
			column.AddDeleteForeverAction(app.EntityTypeName,
				app.BatchUrl + "?action=delete_forever", name, buttonClass, iconClass,
				titleTemplate, urlTemplate, primaryKey, dialogParameters);
		}

		/// <summary>
		/// 对增删查改页面使用的Ajax表格操作列进行标准的设置
		/// 添加以下按钮
		/// - 查看（如果编辑Url不是空，且数据未删除）
		/// - 删除（如果批量Url不是空，且数据未删除）
		/// - 恢复（如果批量Url不是空，且数据已删除）
		/// - 永久删除（如果批量Url不是空，且数据已删除）
		/// </summary>
		/// <typeparam name="TCrudController">控制器的类型</typeparam>
		/// <param name="column">操作列</param>
		/// <param name="request">搜索请求</param>
		public static void StandardSetupFor<TCrudController>(
			this AjaxTableActionColumn column, AjaxTableSearchRequest request)
			where TCrudController : class, ICrudController, new() {
			var app = new TCrudController();
			var deleted = request.Conditions.GetOrDefault<bool>("Deleted");
			if (!string.IsNullOrEmpty(app.EditUrl) && !deleted) {
				column.AddEditActionFor<TCrudController>();
			}
			if (!string.IsNullOrEmpty(app.BatchUrl)) {
				if (!deleted && app.AllowDeleteRecover) {
					column.AddDeleteActionFor<TCrudController>();
				}
				if (deleted && app.AllowDeleteRecover) {
					column.AddRecoverActionFor<TCrudController>();
				}
				if ((deleted && app.AllowDeleteForever) || (!app.AllowDeleteRecover)) {
					column.AddDeleteForeverActionFor<TCrudController>();
				}
			}
		}
	}
}
