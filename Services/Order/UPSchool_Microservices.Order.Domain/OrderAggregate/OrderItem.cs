using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPSchool_Microservices.Order.Domain.Core;

namespace UPSchool_Microservices.Order.Domain.OrderAggregate
{
	public class OrderItem:Entity
	{
		public string ProductId { get; set; }
		public string ProductName { get; set; }
		public string PictureUrl { get; set; }
		public decimal Price { get; set; }

		public OrderItem()
		{

		}

		public OrderItem(string productId, string productName, string pictureUrl, decimal price)
		{
			ProductId = productId;
			ProductName = productName;
			PictureUrl = pictureUrl;
			Price = price;
		}

		public void UpdateOrderItem(string productName, string pictureUrl, decimal price) 
		{
			ProductName = productName;
			PictureUrl = pictureUrl;
			Price = price;
		}
	}
}
