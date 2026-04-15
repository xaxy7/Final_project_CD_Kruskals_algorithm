namespace kruskalscomputation;

using System.Diagnostics;
using System.Globalization;
using DataStructureLibrary.Graph;
using kruskalscomputation.properties;
using static kruskalscomputation.KruskalsDefault;
using static kruskalscomputation.KruskalsWithCoordinates;

class Program
{

    static void Main(string[] args)
    {
        // mode selection for "dotnet run -- KruskalsDefault / KruskalsWithCoordinates"
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
        // Mode selection for just "dotnet run"
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

// Function that displays and process the menu for the default Kruskals Solution
    static void RunDefaultMode()
    {
        Console.WriteLine("Choose default input type:");
        Console.WriteLine("0 - Random input");
        Console.WriteLine("1 - Manual input");

        string choice = Console.ReadLine()!;

        Graph<VertexProperty, EdgeProperty> graph;
        List<Edge<Vertex<VertexProperty>, EdgeProperty>> edgesList;

        //Menu with choices of either random input or manual one
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
            RunKruskalsDefault(graph, edgesList);

        Console.WriteLine("List of Edges in the minimum spanning tree: ");
        PrintEdgesDefault(mstEdges);
        PrintMSTWeightDefault(mstEdges);
    }
// Function that displays and process the menu for the Kruskals Algorithm solution but Euclidean / with coordinates
    static void RunCoordinateMode()
    {
        Console.WriteLine("Choose coordinate input type:");
        Console.WriteLine("0 - Random input");

        Console.WriteLine("1 - Manual input (Name X Y)");
        Console.WriteLine("2 - Short input (N X1 Y1 X2 Y2 ... XN YN)");

        string inputChoice = Console.ReadLine()!;

        List<Vertex<CoordinatesVertexProperty>> vertices;
        // mode selection: random input, manual input long (with name choices for vertices), manual input short (auto generated names)
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

        CoordinateKruskalsResult result = KruskalsWithCoordinates.RunKruskalsWithCoordinates(vertices);

        Console.WriteLine("List of edges in the minimum spanning tree: ");
        PrintEdgesCoordinates(result.MstEdges);
        PrintMSTLengthCoordinates(result.MstEdges);
        CoordinateGraphRendered.DrawGraph(vertices,result.AllEdges ,result.MstEdges);
    }    

    //function that generates random vertices for the coordinates solution
    static List<Vertex<CoordinatesVertexProperty>> GenerateRandomCoordinateInput()
    {
        Random rand = new Random();
        int count = rand.Next(4,8); // decides random amount of vertices 

        List<Vertex<CoordinatesVertexProperty>> vertices = new List<Vertex<CoordinatesVertexProperty>>();

        for(int i = 0 ; i < count; i++)
        {
            Vertex<CoordinatesVertexProperty> v = new Vertex<CoordinatesVertexProperty>($"V{i+1}");

            v.Property.X = rand.NextDouble() * 10;
            v.Property.Y = rand.NextDouble() * 10;

            vertices.Add(v);
        }
        Console.WriteLine("List of randomly generated vertices:");
        for(int i = 0; i < vertices.Count; i++)
        {
            var vertex = vertices[i];

            Console.WriteLine($"Name: {vertex.Property.Name} Coordinates: X: {vertex.Property.X} Y: {vertex.Property.Y}");
        }
        return vertices;
    }
   
    //function that generates random vertices for the default solution
    static void GenerateRandomDefaultInput(out Graph<VertexProperty, EdgeProperty> graph, out List<Edge<Vertex<VertexProperty>,EdgeProperty>> edgesList)
    {
        graph = new Graph<VertexProperty, EdgeProperty>();
        edgesList = new List<Edge<Vertex<VertexProperty>, EdgeProperty>>();

        Random rand = new Random();

        int vCount = rand.Next(4,8); // decides random amount of vertices 

        List<Vertex<VertexProperty>> vertices = new List<Vertex<VertexProperty>>();

        for(int i = 0 ; i < vCount; i++)
        {
            Vertex<VertexProperty> v = graph.AddVertex($"V{i+1}");
            vertices.Add(v);
        }
        //this for loop ensures connectivity so that every edge has a weight
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
        //print the edges for debugging:
        Console.WriteLine("List of randomly generated edges:");
        for(int i = 0; i < edgesList.Count; i++)
        {
            var edge = edgesList[i];
  
            Console.WriteLine($"Source: {edge.Property.Source!.Property.Name} Target: {edge.Property.Target!.Property.Name} Weight: {edge.Property.Weight}");
        
        }              
    }
    
    //function that reads the default manual input
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

// function that reads the vertices from long manual input for the coordinate solution
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

//function that reads the vertices from short manual input for coordinate solution
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
            Console.WriteLine($"Source: {edge.Property.Source!.Property.Name}, Target: {edge.Property.Target!.Property.Name}, Weight: {edge.Property.Weight}" );
        }
    }
    static void PrintEdgesCoordinates(List<Edge<Vertex<CoordinatesVertexProperty>, CoordinatesEdgeProperty>> mstEdges)
    {
        foreach(Edge<Vertex<CoordinatesVertexProperty>, CoordinatesEdgeProperty> edge in mstEdges)
        {
            Console.WriteLine($"Source: {edge.Property.Source!.Property.Name}, Target: {edge.Property.Target!.Property.Name}, Length: {edge.Property.Length}" );
        }
    }
    static void PrintMSTWeightDefault(List<Edge<Vertex<VertexProperty>, EdgeProperty>> mstEdges)
    {
        int totalWeight = 0;
        foreach(Edge<Vertex<VertexProperty>, EdgeProperty> edge in mstEdges)
        {
            totalWeight += edge.Property.Weight;
        }
        Console.WriteLine($"Total Minimum Spanning Tree Weight: {totalWeight}");
    }
    static void PrintMSTLengthCoordinates(List<Edge<Vertex<CoordinatesVertexProperty>, CoordinatesEdgeProperty>> mstEdges)
    {
        double totalLength = 0;
        foreach(Edge<Vertex<CoordinatesVertexProperty>, CoordinatesEdgeProperty> edge in mstEdges)
        {
            totalLength += edge.Property.Length;
        }
        Console.WriteLine($"Total Minimum Spanning Tree Length: {totalLength}");
    }



}
