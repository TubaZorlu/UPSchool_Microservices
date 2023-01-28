using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPSchool_Microservices.Order.Domain.Core;

namespace UPSchool_Microservices.Order.Domain.OrderAggregate
{
	public class Order : Entity, IAggregateRoot
	{

		public DateTime CreatedDate { get; set; }
		public Adress Adress { get; set; }
		public string BuyerId { get; set; }

		private readonly List<OrderItem> _orderItems;

		public IReadOnlyCollection<OrderItem> OrderItems =>_orderItems;

		public Order() 
		{

		}

		public Order(string buyerID,Adress adress) 
		{
			_orderItems = new List<OrderItem>();
			CreatedDate = DateTime.Now;
			BuyerId = buyerID;
			Adress = adress;
		}


		public void AddOrderItem(string productId, String prductName, decimal price, string pictureURl) 
		{
			var existProductt = _orderItems.Any(x => x.ProductId == productId);

			if (!existProductt)
			{
				var newOrderItem = new OrderItem(productId, prductName, pictureURl, price);
				_orderItems.Add(newOrderItem);
			}
		}

		public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);


	}
}
