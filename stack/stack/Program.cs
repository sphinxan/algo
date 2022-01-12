using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace stack
{
    class Program
    {
        class Item<T>
        {
            public T Value { get; set; }
            public Item<T> Next { get; set; }
            public Item<T> Previous { get; set; }

            public Item(T value, Item<T> next, Item<T> previous)
            {
                Value = value;
                Next = next;
                Previous = previous;
            }
        }

        class DoublyLinkedList<T> : IEnumerable<T>
        {
            Item<T> Head; //первый
            Item<T> Tail; //последний
            int CountOfItems; //количество элементов
            public int Count() => CountOfItems;
            public bool IsEmptyList() => CountOfItems == 0;

            public void PlaceLast(T value) //добавить элемент
            {
                Item<T> item = new Item<T>(value, null, null);

                if (Head == null)
                    Head = Tail = item;
                else
                {
                    Tail.Next = item;
                    item.Previous = Tail;
                    Tail = item;
                }
                CountOfItems++;
            }

            //public bool Delete(int value) //удалить элемент по значению
            //{
            //    Item current = Head;

            //    while (current != null) //поиск удаляемого узла
            //    {
            //        if (current.Value.Equals(value))
            //        {
            //            break;
            //        }
            //        current = current.Next;
            //    }
            //    if (current != null)
            //    {
            //        if (current.Next != null) //если узел не последний
            //        {
            //            current.Next.Previous = current.Previous;
            //        }
            //        else  //если последний, переустанавливаем tail
            //        {
            //            Tail = current.Previous;
            //        }

            //        if (current.Previous != null) //если узел не первый
            //        {
            //            current.Previous.Next = current.Next;
            //        }
            //        else //если первый, переустанавливаем head
            //        {
            //            Head = current.Next;
            //        }
            //        CountOfItems--;
            //        return true;
            //    }
            //    return false;
            //}

            public T DeleteLastItem()
            {
                if (Head == null)
                    throw new InvalidOperationException();

                var value = Tail.Value;

                Tail = Tail.Previous;
                Tail.Next = null;

                CountOfItems--;
                return value;
            }

            public T DeleteFirstItem()
            {
                if (Head == null)
                    throw new InvalidOperationException();

                var value = Head.Value;

                //Head.Next.Previous = null; //2 элемент привиос в нал
                Head = Head.Next; //переустанавливаем head
                if (Head == null)
                    Tail = null;

                CountOfItems--;
                return value;
            }

            public T ViewLastItem() => Tail.Value;

            public T ViewFirstItem() => Head.Value;

            public void ClearList() //очистить список
            {
                Head = null;
                Tail = null;
                CountOfItems = 0;
            }

            public bool ContainsItem(T value) //проверить наличие элемента и вернуть true при его наличии
            {
                Item<T> current = Head;
                while (current != null)
                {
                    if (current.Value.Equals(value))
                        return true;
                    current = current.Next;
                }
                return false;
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                Item<T> item = Head;
                while (item != Tail)
                {
                    sb.Append(item.Value.ToString() + ", ");
                    item = item.Next;
                }
                sb.Append(item.Value.ToString());
                return sb.ToString();
            }


            IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)this).GetEnumerator();

            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                Item<T> current = Head;
                while (current != null)
                {
                    yield return current.Value;
                    current = current.Next;
                }
            }

            public IEnumerable<T> BackEnumerator()
            {
                Item<T> current = Tail;
                while (current != null)
                {
                    yield return current.Value;
                    current = current.Previous;
                }
            }

        }

        class Stack<T> : DoublyLinkedList<T> //добавить в конец, удалить с конца
        {
            public void Push(T value) => PlaceLast(value); //добавить элемент в конец стека

            public T Pop() => DeleteLastItem(); //удалить и посмотреть последний элемент стека

            public T Peek() => ViewLastItem(); //посмотреть последний элемент стека без его удаления

            public bool IsEmpty() => IsEmptyList();

            public int Size() => Count();

            public void Print() => Console.WriteLine("элементы стека: " + ToString());

            public void Clear() => ClearList(); //очистить стек

            public bool Contains(T value) => ContainsItem(value);
        }


        class Queue<T> : DoublyLinkedList<T> //добавить в конец, удалить из начала
        {
            public void Enqueue(T value) => PlaceLast(value); //добавить элемент в конец очереди

            public T Dequeue() => DeleteFirstItem(); //удалить и посмотреть первый элемент очереди

            public T Peek() => ViewFirstItem(); //посмотреть первый элемент очереди без его удаления

            public bool IsEmpty() => IsEmptyList();

            public int Size() => Count();

            public void Print() => Console.WriteLine("элементы очереди: " + ToString());

            public void Clear() => ClearList(); //очистить очередь

            public bool Contains(T value) => ContainsItem(value);
        }


        static void Main(string[] args)
        {
            //stack
            Console.WriteLine("stack");
            var list = new Stack<int>();

            list.Push(1);
            list.Push(3);
            list.Push(5);

            list.Print();
            Console.WriteLine("размер стека = " + list.Size());

            Console.WriteLine("последний элемент: " + list.Peek());

            list.Pop();
            Console.WriteLine("удалили последний элемент:");
            list.Print();
            Console.WriteLine("теперь размер стека = " + list.Size());

            Console.WriteLine("stack is empty: " + list.IsEmpty());

            Console.WriteLine("---------------");


            //queue
            Console.WriteLine("queue");
            var list2 = new Queue<int>();

            list2.Enqueue(2);
            list2.Enqueue(4);
            list2.Enqueue(6);

            list2.Print();
            Console.WriteLine("размер очереди = " + list2.Size());

            Console.WriteLine("первый элемент: " + list2.Peek());

            list2.Dequeue();
            Console.WriteLine("удалили первый элемент:");
            list2.Print();
            Console.WriteLine("теперь размер очереди = " + list2.Size());

            Console.WriteLine("queue is empty: " + list2.IsEmpty());

        }
    }
}
