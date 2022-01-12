using System;
using System.Collections.Generic;
using System.IO;

namespace externalSort
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new int[] { 3, 5, 7, 1, 2, 8 };
            var list2 = new int[] { 210, 43, 6, 9, 141, 3, 5, 7, 31, 2, 8 };

            //не внешняя сортировка слиянием
            //Console.WriteLine("не внешняя сортировка слиянием");
            //Console.WriteLine("начальный массив: {0}", string.Join(", ", list));
            //Console.WriteLine("oтсортированный массив: {0}", string.Join(", ", MergeSort(list)));
            //Console.WriteLine("начальный массив2: {0}", string.Join(", ", list2));
            //Console.WriteLine("oтсортированный массив2: {0}", string.Join(", ", MergeSort(list2)));


            var file = MakeFile("file.txt", 10);
            //NaturalMergeSort.DoPolypathNaturalSort("file.txt", 2);


            //8. Внешняя сортировка слиянием
            Console.WriteLine("---\n внешняя сортировка слиянием");
            //Прямое слияние - DirectMergeSort - some error
            //Console.WriteLine("Прямое слияние:");
            //Console.WriteLine("array: {0}", file);
            //DirectMergeSort.SortFile(file);
            //Console.WriteLine("sorted array: {0}", file);

            //Естественное слияние - NaturalMergeSort - работает
            //Console.WriteLine("Естественное слияние:");
            //Console.WriteLine("Прямое слияние:");
            //Console.WriteLine("array: {0}", file);
            NaturalMergeSort.DoPolypathNaturalSort("file.txt", 2);
            //Console.WriteLine("sorted array: {0}", file);

            //Многопутевое слияние
            //Console.WriteLine("Многопутевое слияние:");


        }
        public static string MakeFile(string filePath, int length)
        {
            if (File.Exists(filePath)) File.Delete(filePath);
            var rnd = new Random();
            var file = new StreamWriter(filePath);
            //var a = new int[length];

            for (var i = 0; i < length; i++) file.WriteLine(rnd.Next(100));

            file.Close();

            return file.ToString();
        }



        //Прямое слияние - DirectMergeSort
        public static class DirectMergeSort
        {
            public static void SortFile(string file)
            {
                var i = 1;
                while (SplitFile(file, "A.txt", "B.txt", i))
                {
                    MergeFiles("A.txt", "B.txt", file, i);
                    i *= 2;
                }
            }

            public static bool SplitFile(string originPath, string firstOutput, string secondOutput, int step)
            {
                if (File.Exists(firstOutput)) File.Delete(firstOutput);
                if (File.Exists(secondOutput)) File.Delete(secondOutput);

                StreamReader origin = new StreamReader(originPath);
                StreamWriter[] file = new StreamWriter[] { new StreamWriter(firstOutput), new StreamWriter(secondOutput) };

                string line;
                int i, j = 0;
                for (i = 0; true; i++)
                {
                    line = origin.ReadLine();
                    if (line == null) break;

                    file[(i / step) % 2].WriteLine(line);
                    if ((i / step) % 2 == 0) j++;
                }

                origin.Close();
                file[0].Close();
                file[1].Close();

                return !(j >= i - 1);
            }

            public static void MergeFiles(string firstFile, string secondFile, string resultFile, int step)
            {
                if (File.Exists(resultFile)) File.Delete(resultFile);

                StreamWriter result = new StreamWriter(resultFile);
                StreamReader[] file = new StreamReader[] { new StreamReader(firstFile), new StreamReader(secondFile) };

                string[] line = new string[2] { file[0].ReadLine(), file[1].ReadLine() };
                int[] pos = new int[2];
                while (true)
                {
                    if (pos[0] >= step || line[0] == null)
                    {
                        while (pos[1] < step && line[1] != null)
                        {
                            result.WriteLine(line[1]);
                            line[1] = file[1].ReadLine();
                            pos[1]++;
                        }

                        pos = new int[2];
                        if (line[0] == null && line[1] == null) break;
                    }
                    else if (pos[1] >= step || line[1] == null)
                    {
                        while (pos[0] < step && line[0] != null)
                        {
                            result.WriteLine(line[0]);
                            line[0] = file[0].ReadLine();
                            pos[0]++;
                        }

                        pos = new int[2];
                    }
                    else
                    {
                        if (int.Parse(line[0]) < int.Parse(line[1]))
                        {
                            result.WriteLine(line[0]);
                            line[0] = file[0].ReadLine();
                            pos[0]++;
                        }
                        else
                        {
                            result.WriteLine(line[1]);
                            line[1] = file[1].ReadLine();
                            pos[1]++;
                        }
                    }
                }

                result.Close();
                file[0].Close();
                file[1].Close();
            }
        }


        //Естественное слияние - NaturalMergeSort
        public class NaturalMergeSort
        {
            public static void DoPolypathNaturalSort(string filePath, int fileCount)
            {
                while (DivideFile(filePath, fileCount))
                {
                    MergeFiles(filePath, fileCount);
                }
            }

            private static bool DivideFile(string originFilePath, int fileCount)
            {
                StreamReader file = new StreamReader(originFilePath);

                for (int i = 0; i < fileCount; i++)
                    if (File.Exists(i + ".txt"))
                        File.Delete(i + ".txt");

                StreamWriter[] resultFiles = new StreamWriter[fileCount];
                for (int i = 0; i < fileCount; i++)
                    resultFiles[i] = new StreamWriter(i + ".txt");

                string line = file.ReadLine();
                int curNum = int.MinValue;
                int curFileNum = 0;
                bool isSorted = true;
                while (line != null)
                {
                    var lastNum = curNum;
                    curNum = Int32.Parse(line);

                    if (lastNum > curNum)
                    {
                        curFileNum = (curFileNum + 1) % fileCount;
                        isSorted = false;
                    }

                    resultFiles[curFileNum].WriteLine(line);

                    line = file.ReadLine();
                }

                file.Close();
                for (int i = 0; i < fileCount; i++)
                    resultFiles[i].Close();

                return !isSorted;
            }

            private static void MergeFiles(string resultFilePath, int fileCount)
            {
                if (File.Exists(resultFilePath))
                    File.Delete(resultFilePath);
                StreamWriter resultFile = new StreamWriter(resultFilePath);

                StreamReader[] file = new StreamReader[fileCount];
                for (int i = 0; i < fileCount; i++)
                    file[i] = new StreamReader(i + ".txt");

                LinkedList<int> curFileNums = new LinkedList<int>();
                string[] lines = new string[fileCount];
                int[] nums = new int[fileCount];
                int[] lastNums = new int[fileCount];
                for (int i = 0; i < fileCount; i++)
                {
                    lines[i] = file[i].ReadLine();
                    if (lines[i] != null)
                    {
                        nums[i] = Int32.Parse(lines[i]);
                        curFileNums.AddLast(i);
                    }
                }

                while (curFileNums.Count > 0)
                {
                    while (curFileNums.Count > 0)
                    {
                        int fileNum = GetFileNumWithMinNum(curFileNums, nums);

                        resultFile.WriteLine(lines[fileNum]);
                        lines[fileNum] = file[fileNum].ReadLine();
                        if (lines[fileNum] == null)
                        {
                            curFileNums.Remove(fileNum);
                        }
                        else
                        {
                            lastNums[fileNum] = nums[fileNum];
                            nums[fileNum] = Int32.Parse(lines[fileNum]);
                            if (lastNums[fileNum] > nums[fileNum])
                                curFileNums.Remove(fileNum);
                        }
                    }

                    for (int i = 0; i < fileCount; i++)
                        if (lines[i] != null)
                            curFileNums.AddLast(i);
                }

                resultFile.Close();
                for (int i = 0; i < fileCount; i++)
                    file[i].Close();
            }

            private static int GetFileNumWithMinNum(LinkedList<int> curFileNums, int[] nums)
            {
                int fileNum = -1;
                int minNum = int.MaxValue;
                foreach (int i in curFileNums)
                {
                    if (nums[i] < minNum)
                    {
                        minNum = nums[i];
                        fileNum = i;
                    }
                }

                return fileNum;
            }
        }




        //не внешняя сортировка слиянием
        static int[] temporaryArray;
        static int[] Merge(int[] list, int start, int middle, int end)
        {
            var leftPtr = start;
            var rightPtr = middle + 1;
            var length = end - start + 1;
            for (int i = 0; i < length; i++)
            {
                if (rightPtr > end || (leftPtr <= middle && list[leftPtr] < list[rightPtr]))
                {
                    temporaryArray[i] = list[leftPtr];
                    leftPtr++;
                }
                else
                {
                    temporaryArray[i] = list[rightPtr];
                    rightPtr++;
                }
            }
            for (int i = 0; i < length; i++)
                list[i + start] = temporaryArray[i];
            return list;
        }
        static int[] MergeSort(int[] list, int start, int end)
        {
            if (start == end) return list;
            var middle = (start + end) / 2;
            MergeSort(list, start, middle);
            MergeSort(list, middle + 1, end);
            return Merge(list, start, middle, end);

        }
        static int[] MergeSort(int[] array)
        {
            temporaryArray = new int[array.Length];
            return MergeSort(array, 0, array.Length - 1);
        }

    }
}
