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
    static void Main(string[] args)
    {
        Graph<VertexProperty, EdgeProperty> graph = new Graph<VertexProperty, EdgeProperty>();
        List<Edge<Vertex<VertexProperty>, EdgeProperty>> edgesList = new List<Edge<Vertex<VertexProperty>, EdgeProperty>>();
        Vertex<VertexProperty> v1 = graph.AddVertex("A");
        Vertex<VertexProperty> v2 = graph.AddVertex("B");
        Vertex<VertexProperty> v3 = graph.AddVertex("C");
        Vertex<VertexProperty> v4 = graph.AddVertex("D");
    

        Edge<Vertex<VertexProperty>, EdgeProperty> e1 = graph.AddEdge(v1, v2)!;
        e1.Property.Weight = 10;
        edgesList.Add(e1);

        Edge<Vertex<VertexProperty>, EdgeProperty> e2 = graph.AddEdge(v2,v3)!;
        e2.Property.Weight = 15;
        edgesList.Add(e2);

        Edge<Vertex<VertexProperty>, EdgeProperty> e3 = graph.AddEdge(v3, v4)!;
        e3.Property.Weight = 5;
        edgesList.Add(e3);

        Edge<Vertex<VertexProperty>, EdgeProperty> e4 = graph.AddEdge(v1, v4)!;
        e4.Property.Weight = 20;
        edgesList.Add(e4);

        Edge<Vertex<VertexProperty>, EdgeProperty> e5 = graph.AddEdge(v1, v3)!;
        e5.Property.Weight = 6;
        edgesList.Add(e5);

        mstEdges = RunKruskalsDefualt(graph, edgesList);
        // PrintEdges(mstEdges);
        // PrintMSTWeight(mstEdges);
        // mstEdgesCoordinates = KruskalsWithCoordinatesTest();


        // PrintEdgesCoordinates(mstEdgesCoordinates);
        // PrintMSTLengthCoordinates(mstEdgesCoordinates);
        List<Vertex<CoordinatesVertexProperty>> vertices = ReadVerticesFromInput();
        mstEdgesCoordinates = RunKruskalsWithCoordinates(vertices);
        PrintEdgesCoordinates(mstEdgesCoordinates);
        PrintMSTLengthCoordinates(mstEdgesCoordinates);       



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
    
    static List<Vertex<CoordinatesVertexProperty>> ReadVerticesFromInput()
    {
        int count = int.Parse(Console.ReadLine()!);

        List<Vertex<CoordinatesVertexProperty>> vertices = new List<Vertex<CoordinatesVertexProperty>>();

        for (int i = 0; i < count; i++)
        {
            string line = Console.ReadLine()!;
            string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string name = parts[0];


            double x = double.Parse(parts[1], CultureInfo.InvariantCulture);
            double y = double.Parse(parts[2], CultureInfo.InvariantCulture);

            Vertex<CoordinatesVertexProperty> vertex = new Vertex<CoordinatesVertexProperty>(name);
            vertex.Property.X = x;
            vertex.Property.Y = y;

            vertices.Add(vertex);
        }       
        return vertices;
    }

    static void PrintEdges(List<Edge<Vertex<VertexProperty>, EdgeProperty>> mstEdges)
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
    static void PrintMSTWeight(List<Edge<Vertex<VertexProperty>, EdgeProperty>> mstEdges)
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
