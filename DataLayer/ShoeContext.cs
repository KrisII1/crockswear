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
    public class ShoeContext : IDB<Shoe, int>
    {
        private readonly CrockDBContext dbContext;

        public ShoeContext(CrockDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(Shoe item)
        {
            try
            {
                dbContext.Shoes.Add(item);
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
                Shoe shoeFromDb = await ReadAsync(key, false, false);

                if (shoeFromDb != null)
                {
                    dbContext.Shoes.Remove(shoeFromDb);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("Shoe with that id does not exist!");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ICollection<Shoe>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Shoe> query = dbContext.Shoes;

                // Include navigational properties if needed
                if (useNavigationalProperties)
                {
                    query = query.Include(s => s.Manager)
                                 .Include(s => s.Orders);
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

        public async Task<Shoe> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Shoe> query = dbContext.Shoes;

                // Include navigational properties if needed
                if (useNavigationalProperties)
                {
                    query = query.Include(s => s.Manager)
                                 .Include(s => s.Orders);
                }

                // Set read-only option if needed
                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.FirstOrDefaultAsync(s => s.Id == key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Shoe item, bool useNavigationalProperties = false)
        {
            try
            {
                Shoe shoeFromDb = await ReadAsync(item.Id, true, false);

                
                shoeFromDb.Brand = item.Brand;
                shoeFromDb.Model = item.Model;
                shoeFromDb.Size = item.Size;
                shoeFromDb.Price = item.Price;
                shoeFromDb.Color = item.Color;
                shoeFromDb.Description = item.Description;

                
                if (useNavigationalProperties)
                {
                    Manager managerfromdb = await dbContext.Managers.FindAsync(item.Manager.Id);
                    if (managerfromdb != null)
                    {
                        shoeFromDb.Manager = managerfromdb;
                    }
                    else
                    {
                        shoeFromDb.Manager = item.Manager;
                    }

                    var originalOrders = shoeFromDb.Orders.ToList();
                    var newOrders = item.Orders.ToList();

                    
                    for (var i = 0; i <newOrders.Count;i++)
                    {
                        Order orderFromDb = await dbContext.Orders.FindAsync(newOrders[i].Id);
                        if (orderFromDb != null)
                        {
                            newOrders[i] = orderFromDb;
                        }
                    }
                    shoeFromDb.Orders= newOrders;   
                    
                    
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
