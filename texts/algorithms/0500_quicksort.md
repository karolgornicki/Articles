# Quicksort 

## Introduction

This segments presents one of the most renowned algorithms which on average is slightly faster than mergesort. We’ll start with the basic concept, discuss implementation and further improvements.  

## Goal

As always, our goal is to design an API. In this case we want to expose a function which can sort array pretty much of any type. 

## Implementation 

The underlying idea of quicksort is rather simple. What is usually tricky about this algorithm is to get the implementation right as we have to be careful with indices pointing to the right elements. 

It is a recursive algorithm. At each call we want to organize our array in the following way. We take our first element of the array, let’s call it a pivot value. Our goal is to rearrange element of the array in such way that first we have all element which are smaller than our pivot value, than we have our pivot, and after than we have all larger or equal elements. As a recursive step, we then sort all elements which are less than pivot, using the same method. We also apply the same approach for values greater or equal to the pivot. The pivot value itself is in the right place, so we don’t have to include it into any further sorting. 

In other words, take pivot value, shift all smaller values to the left, all greater or equal to the right, and apply the same technique to each subarray. 

Let’s see how we can implement this. It is obvious that 1-element array is already sorted. Thus we can put an if statement to prevent any further recursive calls. Next all we have to do is to shift values to the left and right of the pivot and sort each subarray.

```csharp 
private static void Sort<T>(T[] xs, int lo, int hi) where T : IComparable
{
    if (hi <= lo) return;

    int j = Partition(xs, lo, hi);

    Sort(xs, lo, j - 1);
    Sort(xs, j + 1, hi);
}
```

In order to reorganize the array we used Partition function. Moreover, this function returns an index of a new location of a pivot value. Why new location? Pivot was taken as a first value from the array, then we moved values less than pivot to the beginning of the array, so our pivot had to move towards the middle/end of the array to make room for those values.

Implementation of the Partition function is somewhat tricky as we use ++x notation. What this operation does it first increments the value and then evaluates it (in contrast x++ was first evaluating and then incrementing). 

```csharp
private static int Partition<T>(T[] xs, int lo, int hi) where T : IComparable
{
    int left = lo, 
        right = hi + 1;

    while (true)
    {
        while (Utils.IsLess(xs[++left], xs[lo]))
        {
            if (left == hi) break;
        }

        while (Utils.IsLess(xs[lo], xs[--right]))
        {
            if (right == lo) break;
        }

        if (left >= right) break;
        Utils.Exch(xs, left, right);
    }

    Utils.Exch(xs, lo, right);
    return right;
}
```

Partition function keeps track of two indices, left and right. The idea is that everything on the left to the “left” index is smaller than pivot, and everything on the right to the “right” index is greater or equal to pivot. As we iterate they are getting closer to the middle, and once they meet we are done with rearranging the array.

How do we do it? We have a top-level while loop which will terminate then indices left and right meet. Inside we have 2 more while loops. The goal of the first inner while loop is to find the element, starting from the left, which is greater than pivot. The other inner while loop is searching for the element, starting from the right, which is smaller than pivot. When we find 2 of these elements we simply swap them with each other, and continue searching for another two to swap.

When we exit the top-level while loop, which means we reorganized the array there is one last step. We have to move the pivot value to the middle. All the time we were rearranging elements in the array pivot table was constantly occupying the first index. Now it’s the time to move it into the right place. All we have to do is to swap pivot value from the first index with the value at index right. 

We can’t bother the end user of our API to provide indices for start and end of our array so, we write one more Sort function, sitting on top of our private functions. At this point we can do one more improvement. From empirical studies we know, that this algorithm performs best when the elements are in random order. In order to ensure good performance we can shuffle the array before sorting it. Since shuffling in O(N) it doesn’t have much cost and brings significant benefits. 

```csharp
public static T[] Sort<T>(T[] xs) where T : IComparable
{
    xs = Shuffling.Shuffle(xs);
    Sort(xs, 0, xs.Length - 1);
    return xs;
}
```

## Improvement - 3-way quicksort 

In real-world cases very often we have to sort collections with duplicated keys. Our initial algorithms doesn’t make any adjustment for that. We can do better than that. We can modify our original algorithm. When partitioning, rather than splitting array into 2 subarrays, let's split it into 3 - values less than pivot, values equal to pivot, and values greater than pivot. Values which are equal to pivot are already sorted, so we can leave them where they are and only sort recursively the other two subarrays. 

In this variant we use 2 indices which indicate the boundaries of 3 arrays - lt (less than) and gt (greater than). Every time we inspect an element of an array we have to compare it to pivot. If it’s less than pivot we swap this element with a pivot, and update indices i and lt. If it is greater than pivot, we swp this element with a value that gt is pointing to, and update indices i and gt. Otherwise, if element and pivot are equal, we simply move to the next element on the array. Once we are done, we sort recursively 2 subarrays.

```csharp
private static void Sort<T>(T[] xs, int lo, int hi) where T : IComparable
{
    if (hi <= lo) return;

    int lt = lo, 
        gt = hi, 
        i = lo;

    T pivot = xs[lo];

    while (i <= gt)
    {
        int cmp = xs[i].CompareTo(pivot);

        if (cmp < 0) Utils.Exch(xs, i++, lt++);
        else if (cmp > 0) Utils.Exch(xs, i, gt--);
        else i++;
    }

    Sort(xs, lo, lt - 1);
    Sort(xs, gt + 1, hi);
}
```

## Properties 

Quicksort algorithm is faster than mergesort because it doesn’t copy elements to auxiliary array. In optimistic scenarios is actually close to linear, but on average O(NlgN). It’s also in place, meaning it doesn’t require extra memory. However, it is not stable - the algorithm does not guarantee that the order of elements will be preserved if they were already put in the correct order - because we shuffle the array.

## Other applications

Another application of the exact same idea is quick search. The goal is to find Nth largest element in unordered array. Naive approach is to sort the array and then count elements from the end. However, we can do better than that. We can use quicksort approach to partition an array, and only investigate 1 subarray - since we have boundaries of each subarray we know how many elements they count and if the value we are looking for is either in one, the other, or is it a pivot.

## Summary 

Quicksort is a very efficient and very frequently used sorting algorithm, performing on average O(NlgN). It is a recursive algorithm which builds on the idea of rearranging elements on the array based on the pivot value, and then recursively sorting each subarray.

Quicksort code can be found [here](https://github.com/karolgornicki/Articles/blob/master/src/Algorithms/Algorithms/Sorts/QuickSort.cs).
3-way quick sort code can be found [here](https://github.com/karolgornicki/Articles/blob/master/src/Algorithms/Algorithms/Sorts/QuickSort3Way.cs).