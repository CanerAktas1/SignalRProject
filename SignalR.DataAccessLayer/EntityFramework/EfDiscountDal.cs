﻿using SignalR.DataAccessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DataAccessLayer.Repositories;
using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccessLayer.EntityFramework
{
    public class EfDiscountDal : GenericRepository<Discount>, IDiscountDal
    {
        public EfDiscountDal(SignalRContext context) : base(context)
        {
        }

        public void ChangeStatus(int id)
        {
           using (var context = new SignalRContext())
            {
                var value = context.Discounts.Find(id);
                if(value.Status == true)
                    value.Status = false;
                else
                    value.Status = true;
                context.SaveChanges();
            }
        }

        public List<Discount> GetListByStatusTrue()
        {
            using (var context = new SignalRContext())
            {
                return context.Discounts.Where(x=>x.Status == true).ToList();
            }
        }
    }
}
