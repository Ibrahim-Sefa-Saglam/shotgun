using UnityEngine;

// Node class representing each node in the stack
public class Node
{
    public Node next;
    public Node previous;
    public float distance;
    public int order;
    public GameObject gameObject;

    // Constructor to create a node
    public Node(float distance, int order, GameObject gameObject = null)
    {
        this.distance = distance;
        this.order = order;
        this.gameObject = gameObject;
        this.next = null;
        this.previous = null;
    }
}

// Stack class to manage the stack of nodes
public class Stack_for_exp
{
    public Node head;
    public Node tail;
    public int count;

    // Constructor to initialize the stack
    public Stack_for_exp()
    {
        head = null;
        tail = null;
        count = 0;
    }

    // Push method to add a node to the stack
    public void Push(float distance, GameObject gameObject = null)
    {
        count++;
        Node newNode = new Node(distance, count, gameObject);

        if (tail == null) // First node to be added
        {
            head = newNode;
            tail = newNode;
        }
        else
        {
            tail.next = newNode;
            newNode.previous = tail;
            tail = newNode;
        }
    }
}
