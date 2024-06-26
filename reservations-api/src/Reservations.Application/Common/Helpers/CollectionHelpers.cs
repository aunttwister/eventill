﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Common.Helpers
{
    public static class CollectionHelpers
    {
        public static void AddRange<T>(
            this ICollection<T> destination,
            IEnumerable<T> source)
        {
            foreach (T item in source)
            {
                destination.Add(item);
            }
        }

        public static void ForEach<T>(
            this IEnumerable<T> enumeration, 
            Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }
    }
}
