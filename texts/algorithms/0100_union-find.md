# Dynamic connectivity problem (union find algorithm)

## Introduction 

In this section we’ll describe dynamic connectivity problem. It is one of the most basic algorithms, however frequently used in real-world applications. Briefly, its goal is to efficiently model operations of connecting together objects and then finding whether 2 given objects are connected with each other. 

## Example

Let’s say we have 10 objects. We’ll use numbers from 0 to 9 to represent them. Initially there are no connections between them. Next we can connect few, for example, 2 with 5, 7 with 3, and 1 with 5. After that we can query, is 1 connected with 9 (no), is 1 connected to 2 (yes), etc. 

The challenge here is to have an efficient representation of this model.

To avoid any misunderstandings:
We can say that object x is connected to object x.
If x is connected to y then we can say that y is connected to x too.
If x is connected to y, and y is connected to z then x is connected to z.

## Development strategy 

Most of software development problems can be solved using a very simple, yet effective approach.
At first we develop a naive algorithm. It’s usually the most obvious one that springs to our mind. 
Next we assess if it’s good enough - Is it fast enough? Does it fit in memory limits?
If it’s not, we find out why, write an improved version, and go back to point #2.

We’ll apply the same strategy here. We’ll start with the simplest solution, demonstrate its problems, and next we’ll improve it. 

## Goal 

Essentially what we have to do here is to develop an API that models this problem. In C# that could be a class with:
A Constructor which creates the initial state of our model.
A method void Union(int x, int y) which connects 2 elements.
A method bool IsConnected(int x, int y) which determines if 2 elements are connected.

## Naive solution (design)

On previously mentioned example we can observe that as we start adding connections we create connected components - group of elements which are connected with each other. We can actually very easily model that.

We can represent our objects as an array of integers of size N. By default array is populated with zeros, so we can initialize it with the value of their indices. And this is pretty much our constructor.

```csharp
private int[] elements;

public NaiveUnionFind(int n)
{
    elements = new int[n];
    for (int i = 0; i < n; i++)
    {
        elements[i] = i;
    }
}
```

Thanks to that, each index has a different value. If 2 indices would have the same value that would mean they are connected. One way to think about them is as they were IDs of component to which given index belong to. That leads us to our implementation of union method that connects two elements.

```csharp
public void Union(int x, int y)
{
    // We are going to assign all elements from component x
    // to component y.
    int xComponent = elements[x];
    int yComponent = elements[y];
    for (int i = 0; i < elements.Length; i++)
    {
        if (elements[i] == xComponent)
        {
            elements[i] = yComponent;
        }
    }
}
```

In order to initialize our model we have to traverse the entire array and update all values.

Lastly, we have to implement IsConnected method. All we have to do is to check if values under both indices are the same. If they are, elements are connected.

```csharp
public bool AreConnected(int x, int y)
{
    return elements[x] == elements[y];
}
```

## Naive solution (analysis) 

Although our initial algorithm works it is very inefficient. Let’s break it into parts and assess each individually. Initialization - it takes linear time, O(N), to create an array and populate with initial values. Union - takes O(N) as we have to go through the entire array and update values corresponding to the other component. AreConnected - it’s a constant time operation, O(1).

Although in many cases O(N) is a pretty good result, in this simple example we can do much better.

## Solution using trees (design)

There is not much we can do when it comes to the part when we initialize the model. However, there is a room for improvement when we add a new connection. Previously, we had to traverse the entire array and flick values corresponding to other components. That’s too expensive. We can introduce the notion of trees to our model. 

How would that work? The idea is that connected objects form a tree structure. What is characteristic to a tree is that is has a root (top element) and no cycles - meaning that if we would follow connections like a path (without going backwards) we would never visit the same element twice.

It’s very easy to grasp this concept by examining a simple example. We start with 10 elements. Next, we connect 2 and 5. Each of these two components is a one element tree, so it doesn’t really matter how we connect them. Let’s agree that we always connect first to the second. To do that we update value of index 2 to be 5.

Next we add another connection 7 and 3. Same situation as earlier - value under index 7 becomes 3 now.

Next we add connection between 1 and 5. What we have to do now is connect 1-element tree with 2-element tree. To do that we connect their roots - top level elements. How do we know which one is a root? It’s simple - if the value is different than the index, it means that this element is connected to that value. Then we can navigate to that index and so on. When we finally reach the index which value is exactly the same (so there is no other node we can go) we know that this is the top of the tree - the root. Applying the same strategy, we identify the root of the second tree as 2 and then connect 1 with 2 by updating value under 1.

How are we going to check if 2 elements are connected. Simple, we take 2 elements, for each we are finding their roots. If it turns out that both elements share the same root we know they are connected.

Constructor implementation is unchanged. Since both, Union and AreConnected methods require finding a root for each element we can abstract this as a helper function - GetRoot. Now the implementation of Union and AreConnected look the following

```csharp
private int GetRoot(int x)
{
    while (elements[x] != x)
    {
        x = elements[x];
    }
    return x;
}

public void Union(int x, int y)
{
    int xRoot = GetRoot(x);
    int yRoot = GetRoot(y);

    elements[xRoot] = yRoot;
}

public bool AreConnected(int x, int y)
{
    return GetRoot(x) == GetRoot(y);
}
```

## Solution using trees (analysis)

This solution is better than the naive algorithm but it still leaves room for improvement. Initialization of the model take O(N), adding a connection is also O(N) even though we are using trees to represent a connected component. The problem is that this tree is not balanced - meaning that we can construct very tall trees and in order to reach to the root, each time we’ll have to traverse through all its elements. For the same reason, AreConnected is also O(N).

## Solution using weighted trees (design)

We identified the problem with the previous design - tall trees. We can improve that by introducing a simple rule. Rather than always connecting first tree to the second, we can keep track of the size of each tree and connect smaller to the larger. This way the height of a tree will be at most lg N. 

In order to implement this concept we have to introduce another array which will keep track of the size of each component. So, our updated constructor will look like this 

```csharp
private int[] elements;
private int[] sizes;

public ImprovedUnionFind(int n)
{
    elements = new int[n];
    sizes = new int[n];
    for (int i = 0; i < n; i++)
    {
        elements[i] = i;
        sizes[i] = 1;
    }
}
```

And updated Union method (AreConnected method is unchanged).

```csharp
public void Union(int x, int y)
{
    int xRoot = GetRoot(x);
    int yRoot = GetRoot(y);

    if (sizes[xRoot] > sizes[yRoot])
    {
        elements[yRoot] = xRoot;
        sizes[xRoot] += sizes[yRoot];
    }
    else
    {
        elements[xRoot] = yRoot;
        sizes[yRoot] += sizes[xRoot];
    }
}
```

## Solution using weighted trees (analysis)

Initialization - O(N)
Union - lg N
AreConnected - lg N

It could be further improved by flattening the tree structure each time we’re traversing it, which would make, over time, AreConnected operation pretty much O(1).

The full implementation of each algorithm can be found under:

  * [Naive Union Find](https://github.com/karolgornicki/Articles/blob/master/src/Algorithms/Algorithms/UnionFind/NaiveUnionFind.cs)
  * [Union Find](https://github.com/karolgornicki/Articles/blob/master/src/Algorithms/Algorithms/UnionFind/UnionFind.cs)
  * [Improved Union Find](https://github.com/karolgornicki/Articles/blob/master/src/Algorithms/Algorithms/UnionFind/ImprovedUnionFind.cs)
