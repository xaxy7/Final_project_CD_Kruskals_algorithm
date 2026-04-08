namespace kruskalscomputation;

using System.Globalization;
using DataStructureLibrary.Graph;
using kruskalscomputation.properties;
using static kruskalscomputation.KruskalsDefault;
using static kruskalscomputation.KruskalsWithCoordinates;

class Program
{

    private static List<Edge<Vertex<VertexProperty>, EdgeProperty>> mstEdges = new();
    private static List<Edge<Vertex<CoordinatesVertexProperty>,CoordinatesEdgeProperty>> mstEdgesCoordinates = new();
    // static void Main(string[] args)
    // {
    //     Graph<VertexProperty, EdgeProperty> graph = new Graph<VertexProperty, EdgeProperty>();
    //     List<Edge<Vertex<VertexProperty>, EdgeProperty>> edgesList = new List<Edge<Vertex<VertexProperty>, EdgeProperty>>();
    //     Vertex<VertexProperty> v1 = graph.AddVertex("A");
    //     Vertex<VertexProperty> v2 = graph.AddVertex("B");
    //     Vertex<VertexProperty> v3 = graph.AddVertex("C");
    //     Vertex<VertexProperty> v4 = graph.AddVertex("D");
    

    //     Edge<Vertex<VertexProperty>, EdgeProperty> e1 = graph.AddEdge(v1, v2)!;
    //     e1.Property.Weight = 10;
    //     edgesList.Add(e1);

    //     Edge<Vertex<VertexProperty>, EdgeProperty> e2 = graph.AddEdge(v2,v3)!;
    //     e2.Property.Weight = 15;
    //     edgesList.Add(e2);

    //     Edge<Vertex<VertexProperty>, EdgeProperty> e3 = graph.AddEdge(v3, v4)!;
    //     e3.Property.Weight = 5;
    //     edgesList.Add(e3);

    //     Edge<Vertex<VertexProperty>, EdgeProperty> e4 = graph.AddEdge(v1, v4)!;
    //     e4.Property.Weight = 20;
    //     edgesList.Add(e4);

    //     Edge<Vertex<VertexProperty>, EdgeProperty> e5 = graph.AddEdge(v1, v3)!;
    //     e5.Property.Weight = 6;
    //     edgesList.Add(e5);

    //     mstEdges = RunKruskalsDefualt(graph, edgesList);
    //     // PrintEdges(mstEdges);
    //     // PrintMSTWeight(mstEdges);
    //     // mstEdgesCoordinates = KruskalsWithCoordinatesTest();


    //     // PrintEdgesCoordinates(mstEdgesCoordinates);
    //     // PrintMSTLengthCoordinates(mstEdgesCoordinates);
    //     List<Vertex<CoordinatesVertexProperty>> vertices = ReadVerticesFromInput();
    //     mstEdgesCoordinates = RunKruskalsWithCoordinates(vertices);
    //     PrintEdgesCoordinates(mstEdgesCoordinates);
    //     PrintMSTLengthCoordinates(mstEdgesCoordinates);       

    // }

    static void Main(string[] args)
    {
        if (args.Length > 0)
        {
            string mode = args[0];

            if (mode == "KruskalsDefault")
            {
                RunDefaultMode();
                return;
            }
            else if (mode == "KruskalsWithCoordinates")
            {
                RunCoordinateMode();
                return;
            }
            else
            {
                Console.WriteLine("Unknown mode.");
                return;
            }
        }

        Console.WriteLine("Choose algorithm:");
        Console.WriteLine("1 - KruskalsDefault");
        Console.WriteLine("2 - KruskalsWithCoordinates");

        string choice = Console.ReadLine()!;

        if (choice == "1")
        {
            RunDefaultMode();
        }
        else if (choice == "2")
        {
            RunCoordinateMode();
        }
        else
        {
            Console.WriteLine("Invalid option.");
        }
    }

    static void RunDefaultMode()
    {
        Console.WriteLine("Choose default input type:");
        Console.WriteLine("0 - Random input");
        Console.WriteLine("1 - Manual input");

        string choice = Console.ReadLine()!;

        Graph<VertexProperty, EdgeProperty> graph;
        List<Edge<Vertex<VertexProperty>, EdgeProperty>> edgesList;

        if (choice == "0")
        {
            GenerateRandomDefaultInput(out graph, out edgesList);
        }
        else if (choice == "1")
        {
            ReadDefaultInput(out graph, out edgesList);
        }
        else
        {
            Console.WriteLine("Invalid option.");
            return;
        }

        List<Edge<Vertex<VertexProperty>, EdgeProperty>> mstEdges =
            RunKruskalsDefualt(graph, edgesList);

        PrintEdgesDefault(mstEdges);
        PrintMSTWeightDefault(mstEdges);
    }

    static void RunCoordinateMode()
    {
        Console.WriteLine("Choose coordinate input type:");
        Console.WriteLine("0 - Random input");

        Console.WriteLine("1 - Manual input (Name X Y)");
        Console.WriteLine("2 - Short input (N X1 Y1 X2 Y2 ... XN YN)");

        string inputChoice = Console.ReadLine()!;

        List<Vertex<CoordinatesVertexProperty>> vertices;

        if(inputChoice == "0")
        {
            vertices = GenerateRandomCoordinateInput();
        }
        else if (inputChoice == "1")
        {
            vertices = ReadVerticesFromInput();
        }
        else if (inputChoice == "2")
        {
            vertices = ReadVerticesFromShortInput();
        }
        else
        {
            Console.WriteLine("Invalid input type.");
            return;
        }

        List<Edge<Vertex<CoordinatesVertexProperty>, CoordinatesEdgeProperty>> mstEdges =
            RunKruskalsWithCoordinates(vertices);

        PrintEdgesCoordinates(mstEdges);
        PrintMSTLengthCoordinates(mstEdges);
        CoordinateGraphRendered.DrawGraph(vertices, ,mstEdges);
    }    
    static List<Vertex<CoordinatesVertexProperty>> GenerateRandomCoordinateInput()
    {
        Random rand = new Random();
        int count = rand.Next(4,8);

        List<Vertex<CoordinatesVertexProperty>> vertices = new List<Vertex<CoordinatesVertexProperty>>();

        for(int i = 0 ; i < count; i++)
        {
            Vertex<CoordinatesVertexProperty> v = new Vertex<CoordinatesVertexProperty>($"V{i+1}");

            v.Property.X = rand.NextDouble() * 10;
            v.Property.Y = rand.NextDouble() * 10;

            vertices.Add(v);
        }
        return vertices;
    }
    
    static void GenerateRandomDefaultInput(out Graph<VertexProperty, EdgeProperty> graph, out List<Edge<Vertex<VertexProperty>,EdgeProperty>> edgesList)
    {
        graph = new Graph<VertexProperty, EdgeProperty>();
        edgesList = new List<Edge<Vertex<VertexProperty>, EdgeProperty>>();

        Random rand = new Random();

        int vCount = rand.Next(4,8);

        List<Vertex<VertexProperty>> vertices = new List<Vertex<VertexProperty>>();

        for(int i = 0 ; i < vCount; i++)
        {
            Vertex<VertexProperty> v = graph.AddVertex($"V{i+1}");
            vertices.Add(v);
        }
        // ensure connectivity (chain)
        for (int i = 0; i < vCount - 1; i++)
        {
            Edge<Vertex<VertexProperty>, EdgeProperty> edge =
                graph.AddEdge(vertices[i], vertices[i + 1])!;

            edge.Property.Weight = rand.Next(1, 20);
            edgesList.Add(edge);
        }
        // add extra random edges
        int extraEdges = rand.Next(vCount, vCount * 2);

        for (int i = 0; i < extraEdges; i++)
        {
            int a = rand.Next(vCount);
            int b = rand.Next(vCount);

            if (a == b) continue;

            Edge<Vertex<VertexProperty>, EdgeProperty>? edge =
                graph.AddEdge(vertices[a], vertices[b]);

            if (edge != null)
            {
                edge.Property.Weight = rand.Next(1, 20);
                edgesList.Add(edge);
            }
        }       

    }
    static List<Edge<Vertex<CoordinatesVertexProperty>,CoordinatesEdgeProperty>> KruskalsWithCoordinatesTest()
    {
        List<Vertex<CoordinatesVertexProperty>> vertices = new List<Vertex<CoordinatesVertexProperty>>();

        Vertex<CoordinatesVertexProperty> v1 = new Vertex<CoordinatesVertexProperty>("A");
        v1.Property.X = 0;
        v1.Property.Y = 0;
        vertices.Add(v1);

        Vertex<CoordinatesVertexProperty> v2 = new Vertex<CoordinatesVertexProperty>("B");
        v2.Property.X = 2;
        v2.Property.Y = 1;
        vertices.Add(v2);

        Vertex<CoordinatesVertexProperty> v3 = new Vertex<CoordinatesVertexProperty>("C");
        v3.Property.X = 4;
        v3.Property.Y = 0;
        vertices.Add(v3);

        Vertex<CoordinatesVertexProperty> v4 = new Vertex<CoordinatesVertexProperty>("D");
        v4.Property.X = 1;
        v4.Property.Y = 4;
        vertices.Add(v4);

        Vertex<CoordinatesVertexProperty> v5 = new Vertex<CoordinatesVertexProperty>("E");
        v5.Property.X = 3;
        v5.Property.Y = 5;
        vertices.Add(v5);

        Vertex<CoordinatesVertexProperty> v6 = new Vertex<CoordinatesVertexProperty>("F");
        v6.Property.X = 6;
        v6.Property.Y = 2;
        vertices.Add(v6);

        Vertex<CoordinatesVertexProperty> v7 = new Vertex<CoordinatesVertexProperty>("G");
        v7.Property.X = 5;
        v7.Property.Y = 6;

        return RunKruskalsWithCoordinates(vertices);
    }
    
    static void ReadDefaultInput(out Graph<VertexProperty, EdgeProperty> graph, out List<Edge<Vertex<VertexProperty>,EdgeProperty>> edgesList)
    {
        graph = new Graph<VertexProperty, EdgeProperty>();
        edgesList = new List<Edge<Vertex<VertexProperty>, EdgeProperty>>();

        Console.WriteLine("Enter number of vertices:");
        int vCount = int.Parse(Console.ReadLine()!);

        Dictionary<string, Vertex<VertexProperty>> vertices =
            new Dictionary<string, Vertex<VertexProperty>>();

        Console.WriteLine("Enter vertex names:");

        for (int i = 0; i < vCount; i++)
        {
            string name = Console.ReadLine()!;
            Vertex<VertexProperty> v = graph.AddVertex(name);
            vertices[name] = v;
        }

        Console.WriteLine("Enter number of edges:");
        int eCount = int.Parse(Console.ReadLine()!);

        Console.WriteLine("Enter edges as: Source Target Weight");

        for (int i = 0; i < eCount; i++)
        {
            string[] parts = Console.ReadLine()!
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string sourceName = parts[0];
            string targetName = parts[1];
            int weight = int.Parse(parts[2]);

            Vertex<VertexProperty> source = vertices[sourceName];
            Vertex<VertexProperty> target = vertices[targetName];

            Edge<Vertex<VertexProperty>, EdgeProperty> edge = graph.AddEdge(source, target)!;
            edge.Property.Weight = weight;

            edgesList.Add(edge);
        }   
    }

    static List<Vertex<CoordinatesVertexProperty>> ReadVerticesFromInput()
    {
        List<Vertex<CoordinatesVertexProperty>> vertices = new List<Vertex<CoordinatesVertexProperty>>();
        HashSet<string> usedNames = new HashSet<string>();

        Console.WriteLine("Enter number of vertices:");

        int count;
        while (!int.TryParse(Console.ReadLine(), out count) || count <= 0)
        {
            Console.WriteLine("Invalid number. Please enter a positive integer:");
        }

        Console.WriteLine("Enter each vertex as: Name X Y");

        for (int i = 0; i < count; i++)
        {
            string line = Console.ReadLine()!;
            string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 3)
            {
                Console.WriteLine("Invalid format. Use: Name X Y");
                i--;
                continue;
            }

            string name = parts[0];

            if (usedNames.Contains(name))
            {
                Console.WriteLine("Duplicate vertex name. Try again.");
                i--;
                continue;
            }

            if (!double.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double x) ||
                !double.TryParse(parts[2], NumberStyles.Float, CultureInfo.InvariantCulture, out double y))
            {
                Console.WriteLine("Invalid coordinates. Use numbers like 1.5 2.3");
                i--;
                continue;
            }

            Vertex<CoordinatesVertexProperty> vertex = new Vertex<CoordinatesVertexProperty>(name);
            vertex.Property.X = x;
            vertex.Property.Y = y;

            vertices.Add(vertex);
            usedNames.Add(name);
        }

        return vertices;
    }


    static List<Vertex<CoordinatesVertexProperty>> ReadVerticesFromShortInput()
    {
        Console.WriteLine("Enter: N X1 Y1 X2 Y2 ... XN YN");

        string input = Console.ReadLine()!;
        string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length < 3)
        {
            throw new Exception("Input too short.");
        }

        if (!int.TryParse(parts[0], out int count) || count <= 0)
        {
            throw new Exception("Invalid vertex count.");
        }

        if (parts.Length != 1 + count * 2)
        {
            throw new Exception("Wrong number of coordinate values.");
        }

        List<Vertex<CoordinatesVertexProperty>> vertices = new List<Vertex<CoordinatesVertexProperty>>();

        for (int i = 0; i < count; i++)
        {
            double x = double.Parse(parts[1 + i * 2], CultureInfo.InvariantCulture);
            double y = double.Parse(parts[2 + i * 2], CultureInfo.InvariantCulture);

            Vertex<CoordinatesVertexProperty> vertex =
                new Vertex<CoordinatesVertexProperty>($"V{i + 1}");

            vertex.Property.X = x;
            vertex.Property.Y = y;

            vertices.Add(vertex);
        }

        return vertices;
    }

    static void PrintEdgesDefault(List<Edge<Vertex<VertexProperty>, EdgeProperty>> mstEdges)
    {
        foreach(Edge<Vertex<VertexProperty>, EdgeProperty> edge in mstEdges)
        {
            Console.WriteLine($"Source: {edge.Property.Source}, Target: {edge.Property.Target}, Weight: {edge.Property.Weight}" );
        }
    }
    static void PrintEdgesCoordinates(List<Edge<Vertex<CoordinatesVertexProperty>, CoordinatesEdgeProperty>> mstEdges)
    {
        foreach(Edge<Vertex<CoordinatesVertexProperty>, CoordinatesEdgeProperty> edge in mstEdges)
        {
            Console.WriteLine($"Source: {edge.Property.Source}, Target: {edge.Property.Target}, Length: {edge.Property.Length}" );
        }
    }
    static void PrintMSTWeightDefault(List<Edge<Vertex<VertexProperty>, EdgeProperty>> mstEdges)
    {
        int totalWeight = 0;
        foreach(Edge<Vertex<VertexProperty>, EdgeProperty> edge in mstEdges)
        {
            totalWeight += edge.Property.Weight;
        }
        Console.WriteLine($"Total Minimal Spanning Tree Weight: {totalWeight}");
    }
    static void PrintMSTLengthCoordinates(List<Edge<Vertex<CoordinatesVertexProperty>, CoordinatesEdgeProperty>> mstEdges)
    {
        double totalLength = 0;
        foreach(Edge<Vertex<CoordinatesVertexProperty>, CoordinatesEdgeProperty> edge in mstEdges)
        {
            totalLength += edge.Property.Length;
        }
        Console.WriteLine($"Total Minimal Spanning Tree Length: {totalLength}");
    }



}
