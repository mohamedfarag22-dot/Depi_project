using FinalProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _context.orders
                .Include(o => o.User)
                .Include(o => o.Service)
                .ToList();
        }

        public Order GetOrderById(int id)
        {
            return _context.orders
                .Include(o => o.User)
                .Include(o => o.Service)
                .FirstOrDefault(o => o.Id == id);
        }

        public void AddOrder(Order order)
        {
            _context.orders.Add(order);
            _context.SaveChanges();
        }

        public void DeleteOrder(int id)
        {
            var order = _context.orders.Find(id);
            if (order != null)
            {
                _context.orders.Remove(order);
                _context.SaveChanges();
            }
        }

        public void EditOrder(Order order)
        {
            var editedOrder = _context.orders.Find(order.Id);
            if (editedOrder != null)
            {
                _context.orders.Update(editedOrder);
                _context.SaveChanges();
            }
        }
    }
}
