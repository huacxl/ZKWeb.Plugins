﻿using DotLiquid;
using DryIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ZKWeb.Localize;
using ZKWeb.Plugins.Common.Admin.src.Managers;
using ZKWeb.Plugins.Common.Admin.src.Model;
using ZKWeb.Templating;
using ZKWeb.Web;
using ZKWeb.Web.Interfaces;

namespace ZKWeb.Plugins.Common.Admin.src.Scaffolding {
	/// <summary>
	/// 后台应用的基础类
	/// </summary>
	public abstract class AdminApp :
		IPrivilegesProvider, IWebsiteStartHandler, ILiquidizable {
		/// <summary>
		/// 应用名称
		/// </summary>
		public abstract string Name { get; }
		/// <summary>
		/// url链接
		/// </summary>
		public abstract string Url { get; }
		/// <summary>
		/// 格式的css类名
		/// </summary>
		public virtual string TileClass { get { return "tile bg-navy"; } }
		/// <summary>
		/// 图标的css类名
		/// </summary>
		public virtual string IconClass { get { return "fa fa-archive"; } }
		/// <summary>
		/// 允许显示此应用的用户类型列表
		/// </summary>
		public virtual UserTypes[] AllowedUserTypes { get { return UserTypesGroup.Admin; } }
		/// <summary>
		/// 显示此应用要求的权限列表
		/// </summary>
		public virtual string[] RequiredPrivileges { get { return new string[0]; } }
		/// <summary>
		/// 对应的处理函数，会自动进行权限检查
		/// </summary>
		/// <returns></returns>
		protected abstract IActionResult Action();

		/// <summary>
		/// 允许直接描画到模板
		/// </summary>
		/// <returns></returns>
		object ILiquidizable.ToLiquid() {
			return new HtmlString(ToString());
		}

		/// <summary>
		/// 获取Html代码
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			var templateManager = Application.Ioc.Resolve<TemplateManager>();
			var html = templateManager.RenderTemplate(
				"common.admin/app_tile.html",
				new { name = new T(Name), tileClass = TileClass, url = Url, iconClass = IconClass });
			return html;
		}

		/// <summary>
		/// 获取权限列表
		/// </summary>
		/// <returns></returns>
		public virtual IEnumerable<string> GetPrivileges() {
			foreach (var privilege in RequiredPrivileges) {
				yield return privilege;
			}
		}

		/// <summary>
		/// 网站启动时注册处理函数
		/// </summary>
		public virtual void OnWebsiteStart() {
			var controllerManager = Application.Ioc.Resolve<ControllerManager>();
			var privilegesCheckedAction = new Func<IActionResult>(() => {
				PrivilegesChecker.Check(AllowedUserTypes, RequiredPrivileges);
				return Action();
			});
			controllerManager.RegisterAction(Url, HttpMethods.GET, privilegesCheckedAction);
			controllerManager.RegisterAction(Url, HttpMethods.POST, privilegesCheckedAction);
		}
	}
}