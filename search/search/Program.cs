using System;
using System.Collections.Generic;
using System.Linq;

namespace search
{
    class Program
    {
        static void Main(string[] args)
        {
            ////6. Алгоритм бинарного поиска
            //Console.WriteLine("бинарный поиск(рекурсивная реализация)");
            ////Console.WriteLine("бинарный поиск(итеративная реализация)");
            //Console.Write("Введите элементы массива: ");
            //var s = Console.ReadLine().Split(new[] { " ", ",", ";" }, StringSplitOptions.RemoveEmptyEntries);
            //var list = new int[s.Length];
            //for (int i = 0; i < s.Length; i++)
            //{
            //    list[i] = Convert.ToInt32(s[i]);
            //}

            //Array.Sort(list); //сортируем массив
            //Console.WriteLine("Упорядоченный массив: {0}", string.Join(", ", list));

            //while (true)
            //{
            //    Console.Write("Введите искомое значение или -90 для выхода: ");
            //    var value = Convert.ToInt32(Console.ReadLine());
            //    if (value == -90)
            //    {
            //        break;
            //    }

            //    var searchResult = BinarySearch_6_1(list, value, 0, list.Length - 1);
            //    //var searchResult = BinarySearch_6_2(list, value, 0, list.Length - 1);
            //    if (searchResult < 0)
            //    {
            //        Console.WriteLine("Элемент со значением {0} не найден", value);
            //    }
            //    else
            //    {
            //        Console.WriteLine("Элемент найден. Индекс элемента со значением {0} равен {1}", value, searchResult);
            //    }
            //}



            //16, 17 Алгоритм Крускала , Прима
            var graph = new Graph(4);
            //var weights = new Dictionary<Edge, double>
            //{
            //    [graph.Connect(0, 1)] = 1,
            //    [graph.Connect(0, 2)] = 2,
            //    [graph.Connect(0, 3)] = 6,
            //    [graph.Connect(1, 3)] = 4,
            //    [graph.Connect(2, 3)] = 2
            //};


            graph.Connect(0, 1, 1);
            graph.Connect(0, 2, 2);
            graph.Connect(0, 3, 6);
            graph.Connect(1, 3, 4);
            graph.Connect(2, 3, 2);

            foreach (var e in Kruskal(graph))
            {
                Console.WriteLine(e.Weight);
            }

            //var t = Prim(graph);

            //foreach (var m in t)
            //{
            //    Console.WriteLine(m.To.NodeNumber + " " + m.From.NodeNumber + " " + m.Weight);
            //}







            //20. Алгоритм Дейкстры(поиск кратчайшего пути между двумя заданными вершинами графа)
            //Console.WriteLine("Алгоритм Дейкстры(поиск кратчайшего пути между двумя заданными вершинами графа)");
            //var graph = new GraphDijkstra();

            //graph.AddVertex("A"); //добавление вершин
            //graph.AddVertex("B");
            //graph.AddVertex("C");
            //graph.AddVertex("D");
            //graph.AddVertex("E");
            //graph.AddVertex("F");
            //graph.AddVertex("G");

            //graph.AddEdge("A", "B", 22); //добавление ребер
            //graph.AddEdge("A", "C", 33);
            //graph.AddEdge("A", "D", 61);
            //graph.AddEdge("B", "C", 47);
            //graph.AddEdge("B", "E", 93);
            //graph.AddEdge("C", "D", 11);
            //graph.AddEdge("C", "E", 79);
            //graph.AddEdge("C", "F", 63);
            //graph.AddEdge("D", "F", 41);
            //graph.AddEdge("E", "F", 17);
            //graph.AddEdge("E", "G", 58);
            //graph.AddEdge("F", "G", 84);

            //var dijkstra = new Dijkstra(graph);
            //var path = dijkstra.FindShortestPath("A", "G");
            //Console.WriteLine(path);
        }

        //6.1 Алгоритм бинарного поиска (рекурсивный)
        public static int BinarySearch_6_1(int[] list, int searchedValue, int first, int last)
        {
            if (first > last) //границы сошлись
            {
                return -1; //элемент не найден
            }

            var middle = (first + last) / 2; //средний индекс подмассива
            var middleValue = list[middle]; //значение в средине подмассива

            if (middleValue == searchedValue)
            {
                return middle;
            }
            else
            {
                if (middleValue > searchedValue) //рекурсивный вызов поиска для левого подмассива
                {
                    return BinarySearch_6_1(list, searchedValue, first, middle - 1);
                }
                else  //для правого подмассива
                {
                    return BinarySearch_6_1(list, searchedValue, middle + 1, last);
                }
            }
        }

        //6.2 Алгоритм бинарного поиска (итеративный - с использованием цикла)
        public static int BinarySearch_6_2(int[] list, int searchedValue, int left, int right)
        {
            while (left <= right) //пока не сошлись границы массива
            {
                var middle = (left + right) / 2; //индекс среднего элемента

                if (searchedValue == list[middle])
                {
                    return middle;
                }
                else if (searchedValue < list[middle])
                {
                    right = middle - 1;  //сужаем рабочую зону массива с правой стороны
                }
                else
                {
                    left = middle + 1; //сужаем рабочую зону массива с левой стороны
                }
            }
            return -1; //ничего не нашли
        }




        //16. Алгоритм Крускала (поиска минимального остовного дерева)
        public static IEnumerable<Edge> Kruskal(Graph graph)
        {
            var tree = new List<Edge>(); //пустое дерево
            foreach (var edge in graph.Edges.Where(t => t.Weight > 0).OrderBy(x => x.Weight))
            {
                tree.Add(edge);
                var temp = MakeGraph(tree);
                if (HasCycle(temp))
                    tree.Remove(edge);
            }

            return tree;
        }


        //17. Алгоритм Прима (поиска минимального остовного дерева) 
        public static IEnumerable<Edge> Prim(Graph graph)
        {
            var tree = new List<Edge>();
            var possibleEdges = new List<Edge>();
            var nodes = new List<Node>();
            Random rnd = new Random();
            int num = rnd.Next(0, graph.Length);
            Node node = graph.Nodes.Where(t => t.NodeNumber == num).First(); //берем вершину
            nodes.Add(node);

            while (tree.Count < graph.Length - 1) //пока не включены все вершины графа
            {
                foreach (var t in node.IncidentEdges) //находим самые легкие ребра
                    if (!possibleEdges.Contains(t) && t.Weight > 0 && 
                        tree.Find(b => b.To == t.From && b.From == t.To) == null
                        && !tree.Contains(t))
                        possibleEdges.Add(t);

                possibleEdges = possibleEdges.OrderBy(b => b.Weight).ToList();

                foreach (var t in possibleEdges)
                {
                    tree.Add(t); //присоединяем к дереву
                    if ((nodes.Contains(t.To) && nodes.Contains(t.From)) || HasCycle(MakeGraph(tree)))
                    {
                        tree.Remove(t);
                        continue;
                    }

                    node = t.To == node ? t.From : t.To;
                    possibleEdges.Remove(t);
                    break;
                }
            }

            return tree; //мин остовное дерево
        }

        public static Graph MakeGraph(List<Edge> tree) //16, 17. создание множества из набора вершин
        {
            List<Node> nodes = new List<Node>();
            foreach (var edge in tree)
            {
                if (!nodes.Contains(edge.From))
                    nodes.Add(edge.From);
                if (!nodes.Contains(edge.To))
                    nodes.Add(edge.To);
            }

            Graph graph = new Graph(nodes.Count);
            nodes.OrderBy(t => t.NodeNumber);
            foreach (var n in tree)
            {
                if (!graph.Edges.Contains(n))
                    graph.Connect(graph.Nodes.Where(t => t.NodeNumber == nodes.IndexOf(n.To))
                            .First().NodeNumber,
                        graph.Nodes.Where(t => t.NodeNumber == nodes.IndexOf(n.From))
                            .First().NodeNumber, n.Weight);
            }

            return graph;
        }

        public static bool HasCycle(Graph graph) //16, 17
        {
            int length = graph.Nodes.Count();
            bool[] visited = new bool[length];

            for (int i = 0; i < length; i++) visited[i] = false;

            for (int u = 0; u < length; u++)
                if (!visited[u])
                    if (graph.IsCyclicUtil((Node)graph.Nodes.Where(t => t.NodeNumber == u)
                             .First(), visited, null))
                        return true;
            return false;
        }
    }


    public class Edge //16, 17 ребро
    {
        public Node From;
        public Node To;
        public int Weight;

        public Edge(Node first, Node second, int weight = 0)
        {
            From = first;
            To = second;
            Weight = weight;
        }

        public bool IsIncident(Node node) => From == node || To == node;

        public Node OtherNode(Node node)
        {
            if (!IsIncident(node)) throw new ArgumentException();
            if (From == node) return To;
            return From;
        }
    }

    public class Node //16, 17 вершина
    {
        private List<Edge> _incidentEdges = new List<Edge>();
        public int NodeNumber;

        public Node(int number) => NodeNumber = number;

        public override string ToString() => NodeNumber.ToString();// чтобы нормально в дебаге отображалось

        public IEnumerable<Node> IncidentNodes //перечислить ноды кт инцидентны данной, но не позволит изменить
        {
            get { return _incidentEdges.Select(z => z.OtherNode(this)); }
            // get
            // {
            //     foreach (var edge in _incidentEdges) yield return edge.OtherNode(this);
            // }
        }

        public IEnumerable<Edge> IncidentEdges // тоже самое, что и с узлами
        {
            get
            {
                foreach (var edge in _incidentEdges) yield return edge;
            }
        }

        public static Edge Connect(Node node1, Node node2, int weight = 0) // создание связи в неориентированном графе
        {
            var edge = new Edge(node1, node2, weight);
            node1._incidentEdges.Add(edge);
            node2._incidentEdges.Add(edge);
            return edge;
        }
    }

    public class Graph //16, 17
    {
        public Node[] _nodes;

        public Graph(int nodesCount) // чтобы избежать повторения номеров вершин, узнаем их количество
        {
            _nodes = Enumerable.Range(0, nodesCount).Select(x => new Node(x)).ToArray();
        }

        public int Length => _nodes.Length;

        public Node this[int index] => _nodes[index]; // для извлечения вершин, чтобы их соединить

        public IEnumerable<Node> Nodes
        {
            get
            {
                foreach (var node in _nodes) yield return node;
            }
        }

        public Edge Connect(int index1, int index2, int weight = 0) => Node.Connect(_nodes[index1], _nodes[index2], weight);

        public IEnumerable<Edge> Edges
        {
            get { return _nodes.SelectMany(z => z.IncidentEdges).Distinct(); }
        }

        public static Graph MakeGraph(params int[] incidentNodes)
        {
            var graph = new Graph(incidentNodes.Max() + 1);
            for (var i = 0; i < incidentNodes.Length - 1; i += 2)
                graph.Connect(incidentNodes[i], incidentNodes[i + 1]);
            return graph;
        }

        public bool IsCyclicUtil(Node current, bool[] visited, Node parent)
        {
            visited[current.NodeNumber] = true;

            foreach (var i in current.IncidentNodes)
            {
                if (!visited[i.NodeNumber])
                {
                    if (IsCyclicUtil(i, visited, current))
                        return true;
                }
                else if (i != parent)
                    return true;
            }
            return false;
        }
    }






    public class GraphVertexDijkstra //20. Вершина графа
    {
        public string Name { get; } //Название вершины
        public List<GraphEdgeDijkstra> Edges { get; } //Список ребер

        public GraphVertexDijkstra(string vertexName)
        {
            Name = vertexName;
            Edges = new List<GraphEdgeDijkstra>();
        }

        public void AddEdge(GraphEdgeDijkstra newEdge) => Edges.Add(newEdge); //Добавить ребро

        public void AddEdge(GraphVertexDijkstra vertex, int edgeWeight) => AddEdge(new GraphEdgeDijkstra(vertex, edgeWeight)); //Добавить ребро

        public override string ToString() => Name; //Преобразование в строку
    }

    public class GraphEdgeDijkstra //20. Ребро графа
    {
        public GraphVertexDijkstra ConnectedVertex { get; } //Связанная вершина
        public int EdgeWeight { get; } //Вес ребра
   
        public GraphEdgeDijkstra(GraphVertexDijkstra connectedVertex, int weight)
        {
            ConnectedVertex = connectedVertex;
            EdgeWeight = weight;
        }
    }

    public class GraphDijkstra //20. Граф
    {
        public List<GraphVertexDijkstra> Vertices { get; } //Список вершин графа
        public GraphDijkstra() => Vertices = new List<GraphVertexDijkstra>();

        public void AddVertex(string vertexName) => Vertices.Add(new GraphVertexDijkstra(vertexName)); //Добавление вершины

        public GraphVertexDijkstra FindVertex(string vertexName) //Поиск вершины
        {
            foreach (var v in Vertices)
            {
                if (v.Name.Equals(vertexName))
                {
                    return v;
                }
            }

            return null;
        }

        public void AddEdge(string firstName, string secondName, int weight) //Добавление ребра
        {
            var v1 = FindVertex(firstName);
            var v2 = FindVertex(secondName);
            if (v2 != null && v1 != null)
            {
                v1.AddEdge(v2, weight);
                v2.AddEdge(v1, weight);
            }
        }
    }

    public class GraphVertexInfoDijkstra //20. Информация о вершинах графа
    {
        public GraphVertexDijkstra Vertex { get; set; } //Вершина
        public bool IsUnvisited { get; set; } //Не посещенная вершина
        public int EdgesWeightSum { get; set; } //Сумма весов ребер
        public GraphVertexDijkstra PreviousVertex { get; set; } //Предыдущая вершина

        public GraphVertexInfoDijkstra(GraphVertexDijkstra vertex)
        {
            Vertex = vertex;
            IsUnvisited = true;
            EdgesWeightSum = int.MaxValue;
            PreviousVertex = null;
        }
    }

    public class Dijkstra //20. Алгоритм Дейкстры - реализация поиска кратчайшего пути
    {
        GraphDijkstra graph;
        List<GraphVertexInfoDijkstra> infos;

        public Dijkstra(GraphDijkstra graph) => this.graph = graph;

        public void InitInfo() //Инициализация информации
        {
            infos = new List<GraphVertexInfoDijkstra>();
            foreach (var v in graph.Vertices)
            {
                infos.Add(new GraphVertexInfoDijkstra(v));
            }
        }

        GraphVertexInfoDijkstra GetVertexInfo(GraphVertexDijkstra v) //Получение информации о вершине графа
        {
            foreach (var i in infos)
            {
                if (i.Vertex.Equals(v))
                {
                    return i;
                }
            }

            return null;
        }

        public GraphVertexInfoDijkstra FindUnvisitedVertexWithMinSum() //Поиск непосещенной вершины с минимальным значением суммы
        {
            var minValue = int.MaxValue;
            GraphVertexInfoDijkstra minVertexInfo = null;
            foreach (var i in infos)
            {
                if (i.IsUnvisited && i.EdgesWeightSum < minValue)
                {
                    minVertexInfo = i;
                    minValue = i.EdgesWeightSum;
                }
            }

            return minVertexInfo;
        }

        public string FindShortestPath(string startName, string finishName) //Поиск кратчайшего пути по названиям вершин
        {
            return FindShortestPath(graph.FindVertex(startName), graph.FindVertex(finishName));
        }

        public string FindShortestPath(GraphVertexDijkstra startVertex, GraphVertexDijkstra finishVertex) //Поиск кратчайшего пути по вершинам
        {
            InitInfo();
            var first = GetVertexInfo(startVertex);
            first.EdgesWeightSum = 0;
            while (true)
            {
                var current = FindUnvisitedVertexWithMinSum();
                if (current == null)
                {
                    break;
                }

                SetSumToNextVertex(current);
            }

            return GetPath(startVertex, finishVertex);
        }

        public void SetSumToNextVertex(GraphVertexInfoDijkstra info) //Вычисление суммы весов ребер для следующей вершины
        {
            info.IsUnvisited = false;
            foreach (var e in info.Vertex.Edges)
            {
                var nextInfo = GetVertexInfo(e.ConnectedVertex);
                var sum = info.EdgesWeightSum + e.EdgeWeight;
                if (sum < nextInfo.EdgesWeightSum)
                {
                    nextInfo.EdgesWeightSum = sum;
                    nextInfo.PreviousVertex = info.Vertex;
                }
            }
        }

        public string GetPath(GraphVertexDijkstra startVertex, GraphVertexDijkstra endVertex) //Формирование пути
        {
            var path = endVertex.ToString();
            while (startVertex != endVertex)
            {
                endVertex = GetVertexInfo(endVertex).PreviousVertex;
                path = endVertex.ToString() + path;
            }

            return path;
        }
    }

}
