﻿using System;
using ZKWeb.Plugins.Shopping.Order.src.Domain.Structs;

namespace ZKWeb.Plugins.Shopping.Order.src.Components.OrderProductUnitPriceCalaculators.Interfaces {
	/// <summary>
	/// 订单商品单价的计算器
	/// </summary>
	public interface IOrderProductUnitPriceCalculator {
		/// <summary>
		/// 计算订单商品的单价
		/// 计算结果请保存到result中
		/// </summary>
		/// <param name="userId">用户Id，未登录时等于null</param>
		/// <param name="parameters">订单商品的创建参数</param>
		/// <param name="result">计算结果</param>
		void Calculate(Guid? userId, CreateOrderProductParameters parameters, OrderPriceCalcResult result);
	}
}
