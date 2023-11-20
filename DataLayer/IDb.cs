﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class IDb
    {
        public interface IDB<T, K> where K : IConvertible
        {
            Task CreateAsync(T item);

            Task<T> ReadAsync(K key, bool useNavigationalProperties = false, bool isReadOnly = true);

            Task<ICollection<T>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true);

            Task UpdateAsync(T item, bool useNavigationalProperties = false);

            Task DeleteAsync(K key);
        }
    }
}
