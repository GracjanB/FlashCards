using System;
using System.Collections.Generic;
using System.Threading;

namespace FlashCards.Helpers.Extensions
{
    public static class ListRandomShuffleExtension
    {
        private static Random RandomGen = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;

            while(n > 1)
            {
                n--;
                int k = RandomGen.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
