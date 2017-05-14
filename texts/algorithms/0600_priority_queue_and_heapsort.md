# Priority Queue & Heapsort

## Introduction 

In the earliest segments we talked about basic data structures, stack and queue. There is one more elementary data structure which is frequently used and has a very interesting design. It's priority queue. 

Priority queue, as its name suggests, is a queue which collects elements but internally it organizes them by a priority (for numbers that can be whichever is greater) and allows you to retrieve the element with the highest priority. 

Finally, we will see how the idea of priority queue can be used for sorting, and do it quite efficiently. 

## Goal 

In this section our goal is 2-fold. We want to design API for priority queue (PQ) and sorting function. With regards to priority queue, the user must be able to create PQ, add element to it, remove (with the highest priority), and check if PQ is empty.

## Priority queue: underlying concept 

So, we have to design a data structure which collects elements, and keeps them in order. For clarity of explanations, let's assume we're dealing with integer numbers, and we want to extract from our queue always the highest value. How would we do it? We can organize our numbers into a heap.

What's a heap? Heap is a essentially a tree. Tree is made up of nodes. Each node has a value, and 2 references to its child nodes: left and right. Node can be represented as a class:

```csharp
class Node
{
    public int Value { get; set; }
    public Node Left { get; set; }
    public Node Right { get; set; }
}
```

Using Node class we could create a tree. The element at the top, called root, would hold the highest value, it's children (Left and Right) would hold smaller values, their children even smaller values and so on.

We can immediately see few challenges:

1. When we create a large queue, for each element we'll be creating a Node object, and that can take quite a lot of space.
2. Any time we add a new element to our collection we have to add this element into the right spot, which may require rearranging other elements.

We will address these concerns in the next section.

## Priority queue: implementation

Let's discuss how we can go about implementing PQ. One of our concerns was creating lots of objects, where at the end of the day we only store ints. It turns out that we can represent a tree-like structure using a flat array. Kth element has 2 children: 2K and 2K + 1. Obviously, this way we have to start our array at index 1.

Since arrays always have fixed length we can apply the same strategy as for stacks - using resizing array. For simplicity for the rest of this section we'll set a fixed size of PQ in a constructor.

Let's start implementing. We'll need an internal array to store added elements, and keep track of how many we've already got. Constructor initializes them and for free we already get IsEmpty function. 

```csharp
private T[] _pq;
private int _n;

public MaxPQ(int capacity)
{
    _pq = new T[capacity + 1];
    _n = 0;
}

public bool IsEmpty()
{
    return _n == 0;
}
```

Next, we implement Insert. The underlying idea is very simple. We add new element as the last element to our array, at index _n. However, that disorganizes our tree - it might happen that it's parent _n/2 is greater. To fix this we implement a helper function - Swim. It allows the element to "swim" upwards in the tree structure to the point where its parent is greater than our element. 

So, the Insert function is fairly trivial.

```csharp
public void Insert(T key)
{
    _pq[++_n] = key;
    Swim(_n);
}
```

And all that the Swim function does, it checks if the parent is greater that our element, and if that's the case swap them around and repeats the process using a new position.

```csharp
private void Swim(int n)
{
    while (n > 1 && IsLess(n / 2, 2))
    {
        Exch(n / 2, n);
        n = n / 2;
    }
}
```

In our implementation we used 2 new helper functions.

```csharp
private bool IsLess(int i, int j)
{
    return _pq[i].CompareTo(_pq[j]) < 0;
}

private void Exch(int i, int j)
{
    T tmp = _pq[i];
    _pq[i] = _pq[j];
    _pq[j] = tmp;
}
```

The last element of our API is function which retrieves the largest element from the collection. How would we do it? We just have to take the element from the beginning of the array, at index 1. 

We can't just leave it like that, so we move the last element in our array. This again make the tree unorganized. All we have to do is to "sink"  this element to the position on the tree where its 2 children are smaller than our element.

How would we do it? It's simple. We take our element at the root and it's 2 children. We find the largest of 2 children nodes. If our element is smaller than this child, we swap them around and repeat the process, otherwise we're done.

The implementation of Sink function:

```csharp
private void Sink(int i)
{
    while (2 * i <= _n)
    {
        int j = 2 * i;
        if (i < _n && IsLess(j, j + 1)) { j++; }
        if (IsLess(i, j))
        {
            Exch(i, j);
            i = j;
        }
        else
        {
            break;
        }
    }
}
```

And DelMax, function retrieving, or deleting max value from PQ, can be implemented as below. 

```csharp
public T DelMax()
{
    if (_n == 0)
    {
        throw new InvalidOperationException("You can't remove element from empty priority queue.");
    }

    T max = _pq[1];
    Exch(1, _n--);
    _pq[_n + 1] = default(T);
    Sink(1);
    return max;
}
```

That's all. We've got MaxPQ.

## Heapsort: underlying concept

Let's telescope back and see what PQ really does. It collects elements, and allow you to remove the largest one, and then it self organizing itself, so you can remove the next largest element. This is essentially sorting, only in reverse order! 

So, we can use the idea of PQ as underlying vehicle. Once we have a tree structure well organized, we swap first element (the largest) with the last one in the array. This is almost like DelMax from the previous section. Next we update size of our tree (so the last element is not included - because as the largest element it's already in the right place - at the end of the array).

We repeat this process until we apply it to every element.

Before we start there is one more step - how do we organize elements into organized tree? It's actually very simple. Our array is N-element long. So, we take Nth element parent, that would be N/2 and check if it's both children are smaller, and swap if needed. In other words, we apply Sink function. That organizes a subtree with N/2 at root. Next we do the same for the next subtree at the bottom: with root at K = N/2 - 1 and its 2 children 2K and 2K + 1. We repeat that process all the way until we reach the root (= index 1).

## Heapsort: implementation

The implementation gets a bit tricky here. We receive an array which starts with index 0, however, our tree structure requires that our array effectively "starts" at index 1. We could copy our original array into a helper array, or just adjust our index variables whenever it's required (which is what we do here).

We start with out top-level Sort function. First it has to organize array into a heap (correctly organized tree). Next it rearranges the largest elements at the back of the array.

```csharp
public static T[] Sort(T[] xs)
{
    int n = xs.Length;
    for (int i = n / 2; i >= 1; i--)
    {
        Sink(xs, i, n);
    }

    while (n > 1)
    {
        Exch(xs, 1, n);
        Sink(xs, 1, --n);
    }
    
    return xs;
}
```

Sink function is exactly the same as in PQ. Minor adjustments are applied to the other helper functions, IsLess and Exch, to accommodate the fact, that we have in fact 0-indexed array.

```csharp
private static bool IsLess(T[] xs, int i, int j) 
{
    // (i/j - 1) because array is indexed from 0
    return xs[i - 1].CompareTo(xs[j - 1]) < 0;
}

private static void Exch(T[] xs, int i, int j)
{
    T tmp = xs[--i];    // -- because array is indexed from 0
    xs[i] = xs[--j];    // -- because array is indexed from 0
    xs[j] = tmp;
}
```

That completes the implementation of heapsort algorithm. 

## Analysis 

Priority queue. 

Inserting a new element to PQ takes lgN time, because N-element balanced tree has lgN levels. In the worst case scenario add element at the bottom of the tree and it has to "swim" all the way to the root.

Deleting max element takes also lgN time. We remove max element, move element from the bottom of the tree to the top and let it "sink", potentially to the bottom.

Heapsort. 

The complexity of heapsort is O(NlgN). Intuitively, for half of the elements in array we apply Sink operation, where each at worst is lgN. Then for N elements we apply Sink operation, each at worst lgN.

## Source code 

Implementation of PQ can be found [here](https://github.com/karolgornicki/Articles/blob/master/src/Algorithms/Algorithms/PriorityQueue/MaxPQ.cs)

Implementation of heapsort can be found [here](https://github.com/karolgornicki/Articles/blob/master/src/Algorithms/Algorithms/PriorityQueue/HeapSort.cs)