using System;
using System.Collections.Generic;

namespace hash
{
    public class HashTableItem
    {
        public int Value { get; private set; }
        public HashTableItem(int value) => Value = value;

        public int GetValue() => Value;
        public void SetValue(int value) => Value = value;
    }

    class OpenAddressHashTable
    {
        public HashTableItem[] Items = new HashTableItem[10000]; //все элементы будут храниться в этом массиве
        public HashTableItem DeleteSimbol = new HashTableItem(-1);
        public int hashParam = 13; //рандом число, для того чтобы точки например (1,3) и (3,1) не давали одинаковый хэш
        public int capacity = 10000;
        public List<int> ListLength = new List<int>();

        public HashTableItem[] GetItems() => Items;

        public OpenAddressHashTable()
        {
            for (int i = 0; i < capacity; i++)
                Items[i] = null;
        }

        public int HashFunction(int value, int i) => (int)Math.Abs((value * hashParam + i * Math.PI * value) % capacity);

        //поиск элемента
        public int? SearchItem(int value)
        {
            int i = 0;
            int hash = HashFunction(value, i);

            while (i < 10000) //проверяем все ячейки 
            {
                if (Items[hash].GetValue() == value)
                {
                    return Items[hash].GetValue();
                }
                i++;
                hash = HashFunction(value, i);
            }
            return null; //поиск неуспешен
        }

        //вставка элемента
        public void AddItem(int value)
        {
            var i = 0;
            int hash = HashFunction(value, i);

            while (Items[hash] != null && !Items[hash].Equals(DeleteSimbol)) //проверяем ячейки, до тех пор, пока не находим пустую ячейку
            {
                if (Items[hash].GetValue() == value)
                {
                    Items[hash].SetValue(value); //Помещаем вставляемый элемент в найденную ячейку
                    break;
                }
                i++;
                hash = HashFunction(value, i);
            }
            ListLength.Add(i);
            Items[hash] = new HashTableItem(value);
        }

        //удаление элемента
        public void DeleteItem(int value)
        {
            int i = 0;
            int hash = HashFunction(value, i);

            while (Items[hash] != null)
            {
                if (Items[hash].GetValue() == value)
                {
                    Items[hash] = DeleteSimbol;
                    break;
                }
                i++;
                hash = HashFunction(value, i);
            }
        }

        //Подсчет длины самого длинного кластера в таблице
        public int CalculatingClasterMaxLength()
        {
            int max = 0;

            foreach (var e in ListLength)
            {
                if (e > max)
                    max = e;
            }

            return max;
        }

        //Подсчет длины самого min кластера в таблице
        public int CalculatingClasterMinLength()
        {
            int min = capacity;

            foreach (var e in ListLength)
            {
                if (e < min && e != 0)
                    min = e;
            }

            return min;
        }

        //Подсчет длины среднего кластера в таблице
        public int CalculatingClasterAverageLength()
        {
            var sum = 0;
            int i = 0;

            foreach (var e in ListLength)
            {
                if (e != 0)
                {
                    sum += e;
                    i++;
                }
            }
            Console.WriteLine(i);
            return (sum / i);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var table = new OpenAddressHashTable();
            DoingSmth(table);
        }

        public static void DoingSmth(OpenAddressHashTable table)
        {
             var list = new List<int>();

            GenerationItems(list);
            InsertAllItems(list, table);
            var max = table.CalculatingClasterMaxLength();
            var min = table.CalculatingClasterMinLength();
            var average = table.CalculatingClasterAverageLength();
            Console.WriteLine($"длина самого длинного кластера в таблице: {max}");
            Console.WriteLine($"длина среднего кластера в таблице: {average}");
            Console.WriteLine($"длина самого короткого кластера в таблице: {min}");

        }

        //Генерация 9000 элементов с различными ключами
        public static void GenerationItems(List<int> list)
        {
            var random = new Random();

            for (int i = 0; i < 9000; i++)
                list.Add(random.Next());
        }

        //Вставить все элементы в хеш-таблицу
        public static void InsertAllItems(List<int> list, OpenAddressHashTable table)
        {
            foreach (var e in list)
                table.AddItem(e);
        }
    }
}
