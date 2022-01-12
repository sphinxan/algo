using System;
using System.Collections.Generic;
using System.Linq;
using static graph.Program;

namespace graph
{
	public class Graph
	{
		public Graph()
		{
			Nodes = new();
			Links = new();
		}

		public Dictionary<string, Node> Nodes { get; set; } //узлы
		public List<Link> Links { get; set; } //ребра

		public void AddNode(string nodeName) //добавление узла
		{
			if (!Nodes.ContainsKey(nodeName)) Nodes.Add(nodeName, new Node(nodeName));
		}

		public void AddLink<T>(T source, T target, int weight = 1) //добавление ребра
		{
			string s = source.ToString();
			string t = target.ToString();

			AddNode(s);
			AddNode(t);
			Links.Add(new Link(s, t, weight));

			Nodes[s].AddNeighbour(t);
		}

		public void ChangeNode(string node, string newName) //изменение узла
		{
			Nodes[node].UpdateName(newName);
		}

		public void ChangeLink<T>(T source, T target, int weight) //изменение ребра
		{
			string s = source.ToString();
			string t = target.ToString();

			for (int i = 0; i < Links.Count; i++)
			{
				if (s == Links[i].Source &&
					t == Links[i].Target) Links[i].Weight = weight;
			}
		}

		public void AddWeight<T>(T source, T target, int delta)
		{
			for (int i = 0; i < Links.Count; i++)
			{
				if (Links[i].Source == source.ToString() &&
					Links[i].Target == target.ToString()) Links[i].Weight += delta;
			}
		}

		public void RemoveNode(string node) //удаление узла
		{
			Nodes.Remove(node);
			foreach (var n in Nodes.Values)
			{
				if (n.Neighbours.Contains(node)) n.Neighbours.Remove(node);
			}

			for (int i = 0; i < Links.Count; i++)
			{
				if (Links[i].Source == node ||
					Links[i].Target == node) Links.RemoveAt(i);
			}
		}

		public void RemoveLink(string link) //удаление ребра
		{
			for (int i = 0; i < Links.Count; i++)
			{
				if (link == Links[i].ToString()) Links.RemoveAt(i);
			}
		}
	}

	public class Node //узел
	{
		public string Name { get; set; }
		public List<string> Neighbours { get; set; }

		public Node(string name)
		{
			Name = name;
			Neighbours = new();
		}

		public void UpdateName(string newName) => Name = newName;

		public void AddNeighbour(string neighbour)
		{
			if (!Neighbours.Contains(neighbour)) Neighbours.Add(neighbour);
		}
	}

	public class Link //ребрo
	{
		public string Source { get; set; }
		public string Target { get; set; }
		public int Weight { get; set; }

		public Link(string source, string target, int weight)
		{
			Source = source;
			Target = target;
			Weight = weight;
		}

		public override string ToString() => Source + "-" + Target;
	}

	public class GraphAlgorithms
    {
		public Graph graph;
		public WriteLog WriteLog;

		public GraphAlgorithms(Graph graph, WriteLog writeLog)
		{
			this.graph = graph;
			WriteLog = writeLog;
		}

		public void DFT(string node) //Обход в глубину
		{
			Dictionary<string, bool> visitedNodes = new(); //Список посещенных узлов
			foreach (var e in graph.Nodes)
			{
				visitedNodes.Add(e.Key, false);
			}

			WriteLog($"Обход в глубину\n" + $"Начинаем с узла {node}\n");
			DFTRecursive(node, visitedNodes); //Вызов рекурсивного метода
		}

		private void DFTRecursive(string node, Dictionary<string, bool> visited) //Рекурсивный метод
		{
			WriteLog($"Отмечаем узел {node}\n");
			visited[node] = true; //Отмечаем полученный узел

			List<string> neighbours = graph.Nodes[node].Neighbours; //Выполняем подобное для каждого соседнего узла
			WriteLog($"Соседи узла {node}:");
			foreach (var e in neighbours)
			{
				WriteLog($" {e}");
			}
			WriteLog("\n------\n");
			foreach (var n in neighbours)
			{
				if (!visited[n]) DFTRecursive(n, visited);
			}
		}

		public void BFT(string node) //Обход в ширину
		{
			if (!graph.Nodes.ContainsKey(node)) throw new Exception("Error");
			
			Dictionary<string, bool> visitedNodes = new(); //Список посещенных узлов
			foreach (var e in graph.Nodes)
			{
				visitedNodes.Add(e.Key, false);
			}

			LinkedList<string> queue = new(); //Очередь узлов для посещения

			WriteLog($"Обход в ширину\n" + $"Начинаем с узла {node}\n");
			WriteLog($"Отмечаем узел {node}\n");
			visitedNodes[node] = true; //Отмечаем первый узел и добавляем в очередь
			WriteLog($"Добавляем узел {node} в очередь\n");
			queue.AddLast(node);
			WriteLog("Очередь:");
			foreach (var e in queue)
			{
				WriteLog($" {e}");
			}
			WriteLog("\n-----\n");

			while (queue.Any())
			{
				node = queue.First(); //Убираем первый узел в очереди
				WriteLog($"Берём первый узел из очереди: {node}\n");
				queue.RemoveFirst();

				//Получаем список соседних узлов, Отмечаем их и добавляем в очередь
				List<string> neighbours = graph.Nodes[node].Neighbours;
				WriteLog($"Соседи узла {node}:");
				foreach (var e in neighbours)
				{
					WriteLog($" {e}");
				}
				WriteLog("\n");

				foreach (var val in neighbours)
				{
					if (!visitedNodes[val])
					{
						visitedNodes[val] = true;
						queue.AddLast(val);
					}
				}

				WriteLog("Очередь:");
				foreach (var e in queue)
				{
					WriteLog($" {e}");
				}
				WriteLog("\n------\n");
			}
		}
	}

	public class Program
    {
		public delegate void WriteLog(string text);

		public static void Main()
        {
			WriteLog writeLog = WriteInConsole;
			TestDFT(writeLog);
			TestBFT(writeLog);
		}

		public static void WriteInConsole(string text) => Console.Write(text);

		public static void TestDFT(WriteLog writeLog) //Обход в глубину - создание графа
		{
			Graph graph = new();
			GraphAlgorithms algorithms = new(graph, writeLog);

			graph.AddLink(1, 2);
			graph.AddLink(1, 7);
			graph.AddLink(1, 8);
			graph.AddLink(2, 3);
			graph.AddLink(2, 6);
			graph.AddLink(3, 4);
			graph.AddLink(3, 5);
			graph.AddLink(8, 9);
			graph.AddLink(8, 12);
			graph.AddLink(9, 10);
			graph.AddLink(9, 11);

			algorithms.DFT("1");
		}

		public static void TestBFT(WriteLog writeLog) //Обход в ширину - создание графа
		{
			Graph graph = new();
			GraphAlgorithms algorithms = new(graph, writeLog);

			graph.AddLink(1, 2);
			graph.AddLink(1, 3);
			graph.AddLink(1, 4);
			graph.AddLink(2, 5);
			graph.AddLink(3, 6);
			graph.AddLink(3, 7);
			graph.AddLink(4, 8);
			graph.AddLink(5, 9);
			graph.AddLink(6, 10);

			algorithms.BFT("1");
		}
	}
}
