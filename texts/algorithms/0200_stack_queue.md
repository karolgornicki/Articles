# Stack and queue 

## Introduction 

One of the two basic structures that are being frequently used in number of algorithms are stack and queue. They serve as a collection of objects, allowing to add and remove elements.

Since these concepts are used in various other algorithms it’s a good practice to encapsulate them as separate data structures and reuse when appropriate. 

## Goal

The goal is to develop an API which will provide these 2 data structures. We can define them as classes. Each class should have a generic definition allowing to store any type of data. Moreover, each should provide the following functions 

  * Queue
    * bool IsEmpty()
    * void Enqueue(e)
    * T Dequeue()
  * Stack
    * bool IsEmpty()
    * void Push(e)
    * T Pop()

## Stack 

Stack is a structure which implements LIFO dyscypline - Last In First Out. The last added (Push) element added to the collection will be first to be returned (Pop).

It can be easily implemented in 2 ways:
Using linked list.
Using resizing array.

At first let’s discuss linked list implementation. Normally we would represent a linked list using nodes. Each node holds a value and has a pointer to the next node. Node is a helper class, so in C# we can declare it as internal and it would look like this.

```csharp
internal class Node<T>
{
    public T Value { get; set; }
    public Node<T> Next { get; set; }
}
```

Now in our Stack implementation all we have to do is to keep track which node in the list is first. When we pop a value from the stack we have to update this variable. Implementation of that concept can look like this.

```csharp
public class Stack<T>
{
    private Node<T> first;

    public Stack()
    {
        first = null;
    }

    public void Push(T item)
    {
        Node<T> node = new Node<T>() { Value = item, Next = first };
        first = node;
    }

    public T Pop()
    {
        if (first == null)
        {
            throw new InvalidOperationException("You can not pop value from empty stack.");
        }
        T val = first.Value;
        first = first.Next;
        return val;
    }

    public bool IsEmpty()
    {
        return first == null;
    }
}
```

## Queue

Queue is a slightly different data structure - it implements FIFO dyscypline - First In First Out. The order in which elements were added to the queue is the same in which they will be removed. 

In order to implement this concept we can reuse the idea of linked list. Queue class however will have to keep track of the beginning and the end of our internal linked list - we will be removing elements from one end, and append new elements the other other end.

Sample implementation can look like this.

```csharp
public class Queue<T>
{
    private Node<T> first;
    private Node<T> last;

    public Queue()
    {
        first = null;
        last = null;
    }

    public void Enqueue(T item)
    {
        Node<T> node = new Node<T>() { Value = item };
        if (IsEmpty())
        {
            first = node;
            last = node;
        }
        else
        {
            last.Next = node;
            last = node;
        }
    }

    public T Dequeue()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("You can not dequeue from empty queue.");
        }
        else
        {
            T val = first.Value;
            first = first.Next;
            return val;
        }
    }

    public bool IsEmpty()
    {
        return first == null;
    }
}
```

## Array-based implementation

So far we used linked-list implementation. Alternatively we could use arrays. 

Why would we even consider it? Memory usage. In linked list implementation in order to represent a single object we create the whole class - with the value that we care about, plus all additional stuff. If we would represent our data as array we will save quite a lot of memory.

On the other hand using arrays poses another challenge. Arrays have fixed length. How we could solve this problem? We are starting with an array of fixed size. If it’s full we create a new array, twice of the size of the original and copy all elements across. In order to properly manage memory we would also like to shrink our array if it’s only used in 25%. 

In order to manage removed elements we will have to keep pointers indicating under which index our list currently starts and ends. Moreover, whenever we remove an element from the collection we would have to assign a null value under that index in order to avoid excessive memory usage, a.k.a. Memory leaks.

So, we are gaining smaller memory footprint at the cost of worst performance. We will have to copy our collection whenever we resize the array. There is no other way than simply traversing the old array and re-assigning references to objects.

## Summary 

In conclusion, if we care about performance and we are happy to compromise on memory footprint, we should opt for linked list implementation. Otherwise array based implementation is preferable. 

Full code can be found [here](https://github.com/karolgornicki/Articles/blob/master/src/Algorithms/Algorithms/StackQueue).