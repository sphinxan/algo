using System;
using System.Collections.Generic;
using System.IO;

namespace sort
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new int[] { 3, 5, 7, 1, 2, 8 };
            var list2 = new int[] { 210, 43, 6, 9, 141, 3, 5, 7, 31, 2, 8 };
            var list3 = new string[] { "Carmen", "Adela", "Beatrix", "Abbey", "Abigale", "Barbara", "Camalia", "Belinda", "Beckie" };

            ////ввод массива с консоли
            //Console.Write("Введите элементы массива: ");
            //var parts = Console.ReadLine().Split(new[] { " ", ",", ";" }, StringSplitOptions.RemoveEmptyEntries);
            //var array = new int[parts.Length];
            //for (int i = 0; i < parts.Length; i++)
            //{
            //    array[i] = Convert.ToInt32(parts[i]);
            //}



            //1. cортировка пузырьком - bubbleSort
            //Console.WriteLine("cортировка пузырьком");
            //Console.WriteLine("начальный массив: {0}", string.Join(", ", list));
            //Console.WriteLine("oтсортированный массив: {0}", string.Join(", ", SortingBubble_1(list)));
            //Console.WriteLine("начальный массив2: {0}", string.Join(", ", list2));
            //Console.WriteLine("oтсортированный массив2: {0}", string.Join(", ", SortingBubble_1(list2)));



            //2. сортировка вставками - insertionSort
            //Console.WriteLine("сортировка вставками");
            //Console.WriteLine("начальный массив: {0}", string.Join(", ", list));
            //Console.WriteLine("oтсортированный массив: {0}", string.Join(", ", InsertionSort_2(list)));
            //Console.WriteLine("начальный массив2: {0}", string.Join(", ", list2));
            //Console.WriteLine("oтсортированный массив2: {0}", string.Join(", ", InsertionSort_2(list2)));



            //3. сортировка выбором - selectionSort
            //Console.WriteLine("сортировка выбором");
            //Console.WriteLine("начальный массив: {0}", string.Join(", ", list));
            //Console.WriteLine("oтсортированный массив: {0}", string.Join(", ", SelectionSort_3(list)));
            //Console.WriteLine("начальный массив2: {0}", string.Join(", ", list2));
            //Console.WriteLine("oтсортированный массив2: {0}", string.Join(", ", SelectionSort_3(list2)));



            //4. шейкерная сортировка - shakerSort
            //Console.WriteLine("шейкерная сортировка");
            //Console.WriteLine("начальный массив: {0}", string.Join(", ", list));
            //Console.WriteLine("oтсортированный массив: {0}", string.Join(", ", ShakerSort_4(list)));
            //Console.WriteLine("начальный массив2: {0}", string.Join(", ", list2));
            //Console.WriteLine("oтсортированный массив2: {0}", string.Join(", ", ShakerSort_4(list2)));



            //5. сортировка Шелла - shellSort
            //Console.WriteLine("сортировка Шелла");
            //Console.WriteLine("начальный массив: {0}", string.Join(", ", list));
            //Console.WriteLine("oтсортированный массив: {0}", string.Join(", ", ShellSort_5(list)));
            //Console.WriteLine("начальный массив2: {0}", string.Join(", ", list2));
            //Console.WriteLine("oтсортированный массив2: {0}", string.Join(", ", ShellSort_5(list2)));

            //5.2 сортировка Шелла - Седжвика
            //Console.WriteLine("сортировка Шелла - Седжвика");
            //Console.WriteLine("начальный массив: {0}", string.Join(", ", list));
            //Console.WriteLine("oтсортированный массив: {0}", string.Join(", ", ShellSort_Sedjwik(list, list.Length)));
            //Console.WriteLine("начальный массив2: {0}", string.Join(", ", list2));
            //Console.WriteLine("oтсортированный массив2: {0}", string.Join(", ", ShellSort_Sedjwik(list2, list2.Length)));



            //7. быстрая сортировка - quickSort
            //Console.WriteLine("быстрая сортировка");
            //Console.WriteLine("начальный массив: {0}", string.Join(", ", list));
            //Console.WriteLine("oтсортированный массив: {0}", string.Join(", ", QuickSort_7(list, 0, list.Length - 1)));
            //Console.WriteLine("начальный массив2: {0}", string.Join(", ", list2));
            //Console.WriteLine("oтсортированный массив2: {0}", string.Join(", ", QuickSort_7(list2, 0, list2.Length - 1)));



            // 9. сортировка с помощью двоичного дерева - treeSort
            //Console.WriteLine("сортировка с помощью двоичного дерева ");
            //Console.WriteLine("начальный массив: {0}", string.Join(", ", list));
            //Console.WriteLine("oтсортированный массив: {0}", string.Join(" ", TreeSort(list)));
            //Console.WriteLine("начальный массив2: {0}", string.Join(", ", list2));
            //Console.WriteLine("oтсортированный массив2: {0}", string.Join(" ", TreeSort(list2)));

            ////9. или же создание рандом массива
            //Console.Write("n = ");
            //var n = int.Parse(Console.ReadLine());
            //var a = new int[n];
            //var random = new Random();
            //for (int i = 0; i < a.Length; i++)
            //{
            //    a[i] = random.Next(0, 100);
            //}
            //Console.WriteLine("начальный массив: {0}", string.Join(", ", list));
            //Console.WriteLine("oтсортированный массив: {0}", string.Join(" ", TreeSort(list)));



            //10. поразрядная сортировка - radixSort
            //Console.WriteLine("поразрядная сортировка");
            //Console.WriteLine("начальный массив: {0}", string.Join(", ", list));
            //Console.WriteLine("oтсортированный массив: {0}", string.Join(", ", RadixSort_10(list, list.Length)));
            //Console.WriteLine("начальный массив2: {0}", string.Join(", ", list2));
            //Console.WriteLine("oтсортированный массив2: {0}", string.Join(", ", RadixSort_10(list2, list2.Length)));


            //13. ABC - сортировка (для строк) - absSort
            //Console.WriteLine("ABC - сортировка");
            //Console.WriteLine("начальный массив: {0}", string.Join(", ", list3));
            //Console.WriteLine("oтсортированный массив: {0}", string.Join(", ", AbsSort_13(list3)));


        }



        //1.Сортировка пузырьком - bubbleSort
        public static int[] SortingBubble_1(int[] list)
        {
            for(int i = 0; i < list.Length; i++)
            {
                for (int j = i + 1; j < list.Length; j++)
                {
                    if(list[i] > list[j])
                    {
                        int temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;
                    }
                }
            }
            return list;
        }




        //2.Сортировка вставками - insertionSort
        public static int[] InsertionSort_2(int[] list)
        {
            for (var i = 0; i < list.Length; i++)
            {
                var key = list[i];
                var j = i;
                while ((j > 0) && (list[j - 1] > key))
                {
                    var temp = list[j - 1];
                    list[j - 1] = list[j];
                    list[j] = temp;

                    j--;
                }

                list[j] = key;
            }
            return list;
        }




        //3. Сортировка выбором - selectionSort
        public static int[] SelectionSort_3(int[] list, int currentIndex = 0)
        {
            if (currentIndex == list.Length)
                return list;

            var index = IndexOfMin(list, currentIndex);
            if (index != currentIndex)
            {
                var t = list[index];
                list[index] = list[currentIndex];
                list[currentIndex] = t;
            }

            return SelectionSort_3(list, currentIndex + 1);
        }

        static int IndexOfMin(int[] array, int n) //метод поиска позиции минимального элемента подмассива, начиная с позиции n
        {
            int result = n;
            for (var i = n; i < array.Length; ++i)
            {
                if (array[i] < array[result])
                {
                    result = i;
                }
            }

            return result;
        }




        //4. Шейкерная сортировка - shakerSort
        public static int[] ShakerSort_4(int[] list)
        {
            for (var i = 0; i < list.Length / 2; i++)
            {
                var swapFlag = false;
                for (var j = i; j < list.Length - i - 1; j++) //проход слева направо
                {
                    if (list[j] > list[j + 1])
                    {
                        Swap_4(ref list[j], ref list[j + 1]);
                        swapFlag = true;
                    }
                }

                for (var j = list.Length - 2 - i; j > i; j--) //проход справа налево
                {
                    if (list[j - 1] > list[j])
                    {
                        Swap_4(ref list[j - 1], ref list[j]);
                        swapFlag = true;
                    }
                }

                if (!swapFlag) //если обменов не было выходим
                {
                    break;
                }
            }
            return list;
        }

        public static void Swap_4(ref int x, ref int y) //4. метод для обмена элементов массива
        {
            var t = x;
            x = y;
            y = t;
        }




        //5. Сортировка Шелла - shellSort
        public static int[] ShellSort_5(int[] list)
        {
            var d = list.Length / 2; //расстояние между элементами, которые сравниваются
            while (d >= 1)
            {
                for (var i = d; i < list.Length; i++)
                {
                    var j = i;
                    while ((j >= d) && (list[j - d] > list[j]))
                    {
                        var t = list[j];
                        list[j] = list[j - d];
                        list[j - d] = t;

                        j -= d;
                    }
                }

                d /= 2;
            }
            return list;
        }

        //5.2 Сортировка Шелла - Седжвика
        public static int[] ShellSort_Sedjwik(int[] list, int size)
        {
            int inc, i, j, s;
            var seq = new int[40];

            s = Increment(seq, size);  //вычисление последовательности приращений
            while (s >= 0)
            {
                inc = seq[s--]; //сортировка вставками с инкрементами

                for (i = inc; i < size; i++)
                {
                    var temp = list[i];
                    for (j = i - inc; (j >= 0) && (list[j] > temp); j -= inc)
                        list[j + inc] = list[j];
                    list[j + inc] = temp;
                }
            }
            return list;
        }

        public static int Increment(int[] inc, int size)
        {
            int p1 = 1, p2 = 1, p3 = 1, s = -1;

            do
            {
                if (++s % 2 != 0) //нечет
                {
                    inc[s] = 8 * p1 - 6 * p2 + 1;
                }
                else //четно
                {
                    inc[s] = 9 * p1 - 9 * p3 + 1;
                    p2 *= 2;
                    p3 *= 2;
                }
                p1 *= 2;
            } while (3 * inc[s] < size);

            return s > 0 ? --s : 0;
        }




        //7.Быстрая сортировка - quickSort
        public static int[] QuickSort_7(int[] list, int minIndex, int maxIndex)
        {
            if(minIndex >= maxIndex)
            {
                return list;
            }
            int pivotIndex = Partition(list, minIndex, maxIndex);
            QuickSort_7(list, minIndex, pivotIndex - 1);
            QuickSort_7(list, pivotIndex + 1, maxIndex);

            return list;
        }

        public static int Partition(int[] list, int minIndex, int maxIndex) //метод возвращающий индекс опорного элемента
        {
            var pivot = minIndex - 1;
            for(int i = minIndex; i < maxIndex; i++)
            {
                if(list[i] < list[maxIndex])
                {
                    pivot++;
                    Swap(ref list[pivot], ref list[i]);
                }
            }
            pivot++;
            Swap(ref list[pivot], ref list[maxIndex]);
            return pivot;
        }

        public static void Swap(ref int x, ref int y) //метод для обмена элементов массива
        {
            var t = x;
            x = y;
            y = t;
        }




        //9. Cортировкa с помощью двоичного дерева - treeSort
        public static int[] TreeSort(int[] list)
        {
            var treeNode = new TreeNode(list[0]);
            for (int i = 1; i < list.Length; i++)
            {
                treeNode.Insert(new TreeNode(list[i]));
            }

            return treeNode.Transform();
        }




        //10. Поразрядная сортировка - radixSort
        public static int[] RadixSort_10(int[] list, int n)
        {
            int m = GetMax(list, n); //Находим максимальное число, чтобы узнать количество цифр

            for (int exp = 1; m / exp > 0; exp *= 10) //Делаем подсчет для каждой цифры.
                CountSort(list, n, exp);

            return list;
        }

        public static int GetMax(int[] list, int n)
        {
            int max = list[0];

            for (int i = 1; i < n; i++)
                if (list[i] > max)
                    max = list[i];

            return max;
        }

        public static void CountSort(int[] list, int n, int exp)
        {
            int[] output = new int[n]; //выходной массив
            int[] count = new int[10];

            //инициализация всех элементов count до 0
            for (int i = 0; i < 10; i++)
                count[i] = 0;

            //Сохраняем количество вхождений в count[]
            for (int i = 0; i < n; i++)
                count[(list[i] / exp) % 10]++;

            //Изменить count[i] так, чтобы count[i] теперь содержал фактический положение этой цифры в выходных данных []
            for (int i = 1; i < 10; i++)
                count[i] += count[i - 1];

            for (int i = n - 1; i >= 0; i--) //Создаем выходной массив
            {
                output[count[(list[i] / exp) % 10] - 1] = list[i];
                count[(list[i] / exp) % 10]--;
            }

            //Копируем выходной массив в list[]
            for (int i  = 0; i < n; i++)
                list[i] = output[i];
        }




        //13. ABC - сортировка (для строк) - absSort
        public static string[] AbsSort_13(string[] list) => new ABC_Sorter(list).AbcSort();
    }

    public class ABC_Sorter //13. ABC - сортировка (для строк) - absSort
    {
        public static int?[] Indexes;
        public static List<int?[]> Level;
        public static List<string> Result;
        public static string[] List; 

        public ABC_Sorter(string[] list) 
        {
            List = list; 
            Indexes = new int?[List.Length]; 
            Level = new List<int?[]> { new int?[26] };
            Result = new List<string>(); 
        } 

        public string[] AbcSort() 
        { 
            for (var i = 0; i < List.Length; i++) 
            {
                var letter = char.ToUpper(List[i][0]) - 65;
                Indexes[i] = Level[0][letter];
                Level[0][letter] = i;
            } 
            ClearLevel(0); 
            return Result.ToArray();
        } 

        public static void ClearLevel(int depth) 
        { 
            if (Level.Count == depth + 1) 
                Level.Add(new int?[26]); 
            for (var i = 0; i < 26; i++) 
            { 
                if (Level[depth][i] != null) 
                { 
                    var pos = Level[depth][i].GetValueOrDefault(); 
                    if (Indexes[pos] == null) 
                    { 
                        Result.Add(List[pos]);
                        Level[depth][i] = null;
                    }
                    else 
                    { 
                        MarkChain(pos, depth); 
                        Level[depth][i] = null;
                        ClearLevel(depth + 1);
                    }
                }
            }
        }

        public static void MarkChain(int pos, int depth) 
        {
            while (true) 
            {
                var nextPos = Indexes[pos];
                if (depth + 1 >= List[pos].Length)
                {
                    Result.Add(List[pos]);
                    Indexes[pos] = null;
                }
                else
                {
                    int letter = char.ToUpper(List[pos][depth + 1]) - 65;
                    Indexes[pos] = Level[depth + 1][letter];
                    Level[depth + 1][letter] = pos; } 
                if (nextPos == null) 
                    break; 
                else 
                    pos = nextPos.GetValueOrDefault(); 
            }
        }
    } 


    public class TreeNode //9. простая реализация бинарного дерева
    {
        public TreeNode(int data) => Data = data;

        public int Data { get; set; } //данные
        public TreeNode Left { get; set; }  //левая ветка дерева
        public TreeNode Right { get; set; } //правая ветка дерева

        public void Insert(TreeNode node)  //рекурсивное добавление узла в дерево
        {
            if (node.Data < Data)
            {
                if (Left == null)
                {
                    Left = node;
                }
                else
                {
                    Left.Insert(node);
                }
            }
            else
            {
                if (Right == null)
                {
                    Right = node;
                }
                else
                {
                    Right.Insert(node);
                }
            }
        }

        public int[] Transform(List<int> elements = null) //преобразование дерева в отсортированный массив
        {
            if (elements == null)
            {
                elements = new List<int>();
            }

            if (Left != null)
            {
                Left.Transform(elements);
            }

            elements.Add(Data);

            if (Right != null)
            {
                Right.Transform(elements);
            }

            return elements.ToArray();
        }
    }

}
