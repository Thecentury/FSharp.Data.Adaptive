﻿using System;
using FSharp.Data.Adaptive;
using CSharp.Data.Adaptive;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CSharpInterop
{
    public class Program
    {


        public static void Main()
        {
            DefaultEqualityComparer.SetProvider(DefaultEqualityComparer.System);

            var mappy =
                new HashMapBuilder<int, int> { { 1, 3 }, { 2, 4 } }
                .ToHashMap()
                .Add(4, 5)
                .Remove(2)
                .Alter(1, (int? o) => o)
                .Alter(1, (Microsoft.FSharp.Core.FSharpOption<int> o) => o)
                .Alter(1, o => o)
                .Map((k, v) => v * 3);

            var delta123123 = mappy.ComputeDeltaTo(HashMap.Empty<int, int>());

            var (sm, dm) = mappy.ApplyDelta(delta123123);

            var smasdasd = HashMap.Empty<int, int>().ApplyDelta(ref delta123123);

            var test =
                new[] { 1, 2, 3, 4, 5 }
                .ToHashSet()
                .Map(i => i * 2)
                .Filter(i => i % 2 == 0)
                .Collect(i => HashSet.Single(i))
                .Subtract(HashSet.Single(2))
                .Alter(2, i => !i)
                .MapNullable(i => i < 10 ? (int?)i : null);

            var delta = test.ComputeDeltaTo(HashSet.Empty<int>()).Add(SetOperation<int>.Add(2131));
            var sepp1 = test.ApplyDelta(ref delta);
            var (newState, realDelta) = test.ApplyDelta(delta);

            var changeableMap = new ChangeableHashMap<int, int>(new[] { (1, 2), (2, 3) });
            var changeableSet = new ChangeableHashSet<int>(new[] { 1, 2, 3, 4 });
            var changeableList = new ChangeableIndexList<int>(new[] { 123, 321 });


            var dependent =
                changeableSet
                .Filter(a => a < 10)
                .MapAdaptive(a => AdaptiveValue.Constant(a))
                .MapNullable(a => a < 3 ? (int?)a : null)
                .UnionWith(changeableMap.Keys())
                .Collect(a => new[] { a % 2 }.ToAdaptiveHashSet())
                .FilterAdaptive(a => AdaptiveValue.Constant(true))
                .UnionWith(new[] { 4, 5, 6 }.ToAdaptiveHashSet())
                .SortBy(a => -a)
                .Append(changeableList);

            dependent.AddCallback((s, d) => { Console.WriteLine("{0}, {1}", s, d); });

            Console.WriteLine("{0}", dependent.GetValue());
            using (Adaptive.Transact)
            {
                changeableSet.Value = new HashSetBuilder<int>() { 5, 6, 7, 8, 9, 0 }.ToHashSet();
                changeableMap.Value = HashMapModule.empty<int, int>();
                changeableList.Value = IndexListModule.empty<int>();
                changeableList.Add(1);
            }
            Console.WriteLine("{0}", dependent.GetValue());

            using (Adaptive.Transact)
            {
                changeableSet.Value = HashSetModule.empty<int>();
                changeableMap.Value = HashMapModule.empty<int, int>();
                changeableList.Value = IndexListModule.empty<int>();
                changeableList.Add(1);
            }
            Console.WriteLine("{0}", dependent.GetValue());


            var set = new ChangeableHashSet<int[]>();
            var len = set.Map((arr) => arr.Length);

            var arr1 = new[] { 1, 2, 3 };
            var arr2 = new[] { 1, 2, 3 };

            using (Adaptive.Transact)
            {
                set.Add(arr1);
            }
            Console.WriteLine("{0}", len.Content.GetValue());

            using (Adaptive.Transact)
            {
                set.Remove(arr2);
            }
            Console.WriteLine("{0}", len.Content.GetValue());


            using (Adaptive.Transact)
            {
                set.Remove(arr1);
            }
            Console.WriteLine("{0}", len.Content.GetValue());




        }
    }
}
