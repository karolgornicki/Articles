# Elementary sorting algorithms

## Introduction 

This segment introduces 2 basic sorting algorithm - selection sort and insert sort. Their performance is not satisfactory, however, their simplistic design makes them perfect candidates for introduction to the topic. The next segment describes more efficient implementations.

## Goal 

We want to design an API which will expose various sorting algorithm. Our goal is to create a class for each algorithm. Every class must have a static function which sorts given array. Function must be flexible and accept array of any types, as long as they implement IComprable. 

In particular, our algorithms must cope with edge cases, ie extreme use cases. Few examples would be empty array, which by default is sorted, one-element array, or already sorted array.

## Utility functions 

Before we start implement any functions we will need few utility functions. In our Goals section we mentioned that we must be able to handle any array of types that implement IComparable. .NET introduces an interface [IComparable](https://msdn.microsoft.com/en-us/library/system.icomparable(v=vs.110).aspx) which declares a function which is used for comparison - CompareTo(o). Briefly, it returns value less than zero, zero or greater than zero which represents relations between the 2 objects.

It will be very useful for us to have IsLess function that takes 2 elements, evaluates them and returns a bool value. The challenge is that we want this function to be able to handle any type. In order to meet this requirement all we have to do is to declare its arguments as IComparable. The implementation of this function can look like this. 

```csharp
public static bool IsLess(IComparable x, IComparable y)
{
    return x.CompareTo(y) < 0;
}
```

Now we can use IsLess function to create another very useful function, IsSorted. What’s a definition of a sorted array? For now let’s only focus on ascending case. It is an array in which for any 2 adjacent elements, the one on the right is not smaller than the one on the left. With that in mind we can implement this function.

```csharp
public static bool IsSorted<T>(T[] xs) where T : IComparable
{
    for (int i = 0; i < xs.Length - 1; i++)
    {
        if (IsLess(xs[i+1], xs[i]))
        {
            return false;
        }
    }
    return true;
}
```

The above code is using generic type T. This is for convenience. If we would use IComparable instead, every time we would call this function we would have to cast arrays to IComparable. With generic definition we don’t have to do it. 

Last utility function is Exch - it has 3 arguments, array, and 2 index values. All it does it swaps elements under both indices.

```csharp
public static void Exch<T>(T[] xs, int i, int j) //where T : IComparable
{
    if (i < 0 || j < 0 || i >= xs.Length || j >= xs.Length)
    {
        throw new IndexOutOfRangeException();
    }
    T tmp = xs[i];
    xs[i] = xs[j];
    xs[j] = tmp;
}
```

## Selection Sort 

The idea of selection sort is fairly simple. We traverse an array from left to right looking for the smallest element. Once we found it, we swap it with the first element.Next we traverse the array again but this time we start on the second element, seek the smallest and swap.Once we applied this routine to all elements the array is sorted. 

With our utility functions the implementation is very straight-forward. 

```csharp
public static T[] Sort<T>(T[] xs) where T : IComparable
{
    for (int i = 0; i < xs.Length; i++)
    {
        int iMin = i;
        for (int j = i; j < xs.Length; j++)
        {
            if (Utils.IsLess(xs[j], xs[iMin]))
            {
                iMin = j;
            }
        }
        Utils.Exch(xs, i, iMin);
    }
    return xs;
}
```

What’s the complexity of this algorithm? It’s fairly easy to deduce.Let’s say that the most costly operation is lookup into the table - extracting the value hidden under particular index. How many times this operation is called? When we traverse the array for the first time we look at value under every index - N. The next iteration will check N - 1 indices, the next N - 2, and so on. At the last check we’ll investigate only 1 index.

So, to sum up, N + (N - 1) + (N - 2) + … + 1 = ?? From math class we know that it’s N^2/2. In “big O” notation we’ll call this a quadratic complexity, O(N^2).

## Insert Sort 

Insert sort is a bit more efficient than selection sort. The main idea is that we traverse the array from left to right and at each iteration we compare the current element to the one on its left. If the one on the left is greater we swap them and repeat the process with the next element to the left. 

The other way to look at this is like we partition our array into 2 parts - the array on the left which is sorted, and the one on the right, which is not. We start with 1-element array on the left. By default it is sorted. Next we want to add another element to this array on the left (at index 1). In order to do that we have to move this element as far left in order to make the table sorted again. And we simply repeat the process until we arrive to the end of the array.

Sample code can look like this.

```csharp
public static T[] Sort<T>(T[] xs) where T : IComparable
{
    for (int i = 1; i < xs.Length; i++)
    {
        for (int j = i; j >= 1; j--)
        {
            if (Utils.IsLess(xs[j], xs[j - 1]))
            {
                Utils.Exch(xs, j, j - 1);
            }
        }
    }
    return xs;
}
```

The complexity of this algorithm is again O(N^2).

## Shuffling 

Very similar to sorting is shuffling problem. How to reorganize elements on the array so that they will be distributed uniformly at random. One way to do it is to associate each index with a random value and then sort by those values. However, that’s very inefficient. Decent sorting algorithms are O(NlgN). We can do much better than that. 

What we can do instead is iterate through our array, from left to right. At each iteration we can swap our current element with randomly selected element from the remaining (to the right of our current element) part of the array. This takes O(N) and leaves elements of the array in random order. 

Sample implementation.

```csharp
public static T[] Shuffle<T>(T[] xs)
{
    for (int i = 0; i < xs.Length; i++)
    {
        Random r = new Random();
        int idxNew = r.Next(i, xs.Length - 1);
        Utils.Exch(xs, i, idxNew);
    }

    return xs;
}
```

## Summary 

This section described few very basic sorting algorithms. They illustrate that the problem can be solved in just few lines. Performance of described sorting algorithms is unacceptable, though. Algorithms of quadratic complexity simply don’t scale up. We need to develop algorithms which are at worst linearithmic, meaning O(NlgN). 

Full code can be found [here](https://github.com/karolgornicki/Articles/blob/master/src/Algorithms/Algorithms/Sorts).