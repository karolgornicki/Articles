# Merge Sort 

## Introduction 

Previously we discussed very simple but inefficient sorting algorithms. This segments describes merge sort algorithms - very efficient and frequently used. We will start with the underlying idea of merging, walk through implementation details and talk about its efficiency. 

## Goal 

As in the previous segment (see [Elementary sorting algorithms](https://github.com/karolgornicki/Articles/blob/master/texts/algorithms/0300_elementary_sorts.md)) we want to create a class which exposes a generic Sort function capable of handling pretty much any array.

## Implementation 

The idea of merge sort is childishly simple. For a moment let’s assume we have 2 sorted arrays. How could we design an algorithm that merges these 2 arrays into one? We could have 2 iterators, each traversing different array. We would start from the left - from the smallest elements - and incrementally move to larger elements. At each step we can compare the 2 values - the one that first iterator is pointing to, to the other. Finally we can copy smaller value to our result array and adjust one iterator.

Yes, we have to create an additional array to store our final result. This is an important aspect of this algorithm and we’ll come back to this later. 

OK, so we know how to combine 2 sorted arrays. But how can we sort them in the first place? Let’s ask ourselves - how would we sort a 1 element array? It’s a silly question, we wouldn’t - it’s already sorted. So, if we have 2 1-element arrays, which are both sorted, we can merge them together and get 2-element sorted array. We can see a pattern emerging from this reasoning. Take a large array - break it into 2 arrays, sort them and merge. How would you sort small arrays? In exactly the same way - break each into 2 smaller arrays and sort and merge. We repeat this process so many times until we get a 1 element array - which is sorted by default. 

This kind of algorithms is called recursive which means that it calls itself. Let’s look at the code. 

First stop it top level Sort function. At this point we create an auxiliary array. It’s just easier to create it once and use it whenever we need it rather than each time create one. Also, notice that we provide indices for boundaries of the array. We do that because rather than having 2 arrays and merging it into 3rd we can store all objects on one array and keep track of start and end of each sub-array.

```csharp
public static T[] Sort<T>(T[] xs) where T : IComparable
{
    T[] aux = new T[xs.Length];
    Sort(xs, aux, 0, xs.Length - 1);
    return xs;
}
```

Next, let’s look at the implementation of private Sort method which takes 2 arrays and indices. At the top of the function we have a guard which prevent the algorithm from attempting to sort a 1-element array. We calculate middle index - which is used to split our original array into 2 subarrays which then are sorted in turn. We sort them calling the exact same function.

```csharp
private static void Sort<T>(T[] xs, T[] aux, int lo, int hi) where T : IComparable
{
    if (lo < hi)
    {
        int mi = lo + (hi - lo) / 2;
        Sort(xs, aux, lo, mi);
        Sort(xs, aux, mi + 1, hi);
        Merge(xs, aux, lo, mi, hi);
    }
}
```

Lastly let’s look how Merge function can be implemented. Our goal is to have our result array in xs, so we first copy objects from xs to aux (auxiliary array). Next, we traverse both sub-arrays, at each iteration pick smaller item and put into xs array. We used i++ notation - what it does is it first takes the value of i and applies it to extract the value from the array, and after that it increments i by 1. It’s just less boilerplate code. 

```csharp
private static void Merge<T>(T[] xs, T[] aux, int lo, int mi, int hi) where T : IComparable
{
    // Copy xs to aux.
    for (int i = lo; i <= hi; i++)
    {
        aux[i] = xs[i];
    }

    // Set iterators for 2 halves.
    int iLeft = lo;
    int iRight = mi + 1;

    // Merge 2 halves.
    for (int i = lo; i <= hi; i++)
    {
        if (iLeft > mi) xs[i] = aux[iRight++];
        else if (iRight > hi) xs[i] = aux[iLeft++];
        else if (Utils.IsLess(aux[iLeft], aux[iRight])) xs[i] = aux[iLeft++];
        else xs[i] = aux[iRight++];
    }
}
```

## Complexity 

We claimed in the introduction that this algorithm is more efficient than previously introduced (see [Elementary sorting algorithms](https://github.com/karolgornicki/Articles/blob/master/texts/algorithms/0300_elementary_sorts.md)). But how can we tell? 

We can calculate this by analyzing different properties of our algorithm. First we talked about merging - how complex merging is? During the merge we essentially traverse 2 subarrays during which we made N comparisons, where N is the sum of elements on both arrays. Therefore, merging is a linear time operation, O(N).

How about sorting itself. This is a bit more tricky to estimate. Let’s say that sorting N element array takes T(N) time. We know, that sorting a 1-element array takes no time - it’s already sorted. Therefore, sorting 1-element array is T(1) = O(1).

So in our example, in order to sort array we have to sort 2 halves, each T(N/2), and then merge them - N.

So, 

```
T(N) = T(N/2) + T(N/2) + N
```

Which is 

```
T(N) = 2T(N/2) + N
```

We can divide each side by N

```
T(N)/N = 2T(N/2)/N + N/N
```

Which is 

```
T(N)/N = T(N/2)/(N/2) + 1
```

Now we can observe, that we can apply the same to T(N/2)/(N/2) which would lead to 

```
T(N)/N = T(N/4)/(N/4) + 1 + 1
```

We would continue this process, until we would arrive, on the right-hand side, with N/N. This assumes that N is a power of 2 - which gives us upper bound algorithm’s complexity. 

Eventually 

```
T(N)/N = T(N/N)/(N/N) + 1 + … + 1
```

T(N/N)/(N/N) = 1, so the question is how many 1s do we have to sum. Each time when we divided by 2 we “appended” +1 to our formula. Given that N was power of 2 in the first place we can see that +1 was appended lgN times (that is logarithm with base 2). 

In conclusion,

```
T(N)/N = 1 + lgN

T(N) = N + NlgN
```

Which in big-O notation is O(NlgN).

## Other properties 

One thing to notice is that although this is a very fast algorithm it does require extra memory. In order to merge we had to create additional array. There might be situations in which we can’t afford that. 

Another important aspect is whether algorithm is stable. The characteristic of stable algorithm is that is preserves the original order of elements if they are originally put the in the right order. This implementation of merge sort is stable.

## Improvements 

This algorithm, although fast, can be improved. 

It turns out that sorting small arrays using this algorithm is pretty inefficient, due to overhead of copying vast number of small arrays. It’s actually more efficient to start with merge sort, and when we get to the point when we want to sort 6-element array we simply switch to selection sort. 

We could also avoid copying elements to auxiliary array each time we do a merge, but instead sort algorithm could put them in the first place. The downside is that both of these approaches would complicate the code.

## Summary 

This section describes the idea and implementation of merge sort - one of few very efficient sorting algorithms. 

Full code can be found [here](https://github.com/karolgornicki/Articles/tree/master/src/Algorithms/Algorithms/Sorts).