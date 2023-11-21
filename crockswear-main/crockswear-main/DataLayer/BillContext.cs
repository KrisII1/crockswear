using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataLayer.IDb;

namespace DataLayer
{
    public class BillContext : IDB<Bill, int>
    {
        private readonly CrockDBContext dbContext;

        public BillContext(CrockDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(Bill item)
        {
            try
            {
                User userDb = dbContext.Users.FirstOrDefault(x=>x.Id==item.User.Id);
                if(userDb != null)
                {
                    item.User = userDb;
                }
                List<Basket> orders = item.Orders.ToList();
                for (var i = 0; i < orders.Count; i++)
                {
                    Basket orderDb = await dbContext.Orders.FindAsync(orders[i].Id);
                    if (orderDb != null)
                    {
                        orders[i] = orderDb;
                    }


                }
                item.Orders = orders;

                dbContext.Bills.Add(item);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(int key)
        {
            try
            {
                Bill billFromDb = await ReadAsync(key, false, false);

                if (billFromDb != null)
                {
                    dbContext.Bills.Remove(billFromDb);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("Bill with that id does not exist!");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ICollection<Bill>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Bill> query = dbContext.Bills;

                // Include navigational properties if needed
                if (useNavigationalProperties)
                {
                    query = query.Include(b => b.User)
                                 .Include(b => b.Orders);
                }

                // Set read-only option if needed
                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Bill> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Bill> query = dbContext.Bills;

                // Include navigational properties if needed
                if (useNavigationalProperties)
                {
                    query = query.Include(b => b.User)
                                 .Include(b => b.Orders);
                }

                // Set read-only option if needed
                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.FirstOrDefaultAsync(b => b.Id == key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Bill item, bool useNavigationalProperties = false)
        {
            try
            {
                Bill billFromDb = await ReadAsync(item.Id, true, false);

                // Update scalar properties
                billFromDb.Address = item.Address;
                billFromDb.BankCard = item.BankCard;
                billFromDb.Price = item.Price;

                // Update navigational property if requested
                if (useNavigationalProperties)
                {
                    User userDB = await dbContext.Users.FindAsync(item.User.Id);
                    if (userDB != null)
                    {
                        billFromDb.User = userDB;
                    }
                    else
                    {
                        billFromDb.User = item.User;
                    }

                    List<Basket> orders = item.Orders.ToList();
                    for(var i = 0; i < orders.Count;i++)
                    {
                        Basket orderDb = await dbContext.Orders.FindAsync(orders[i].Id);
                        if(orderDb != null)
                        {
                            orders[i] = orderDb;
                        }
                        

                    }
                    billFromDb.Orders = orders;

                
                }

                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
