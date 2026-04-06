namespace kruskalscomputation;

using DataStructureLibrary.Graph;
using kruskalscomputation.properties;
using static kruskalscomputation.KruskalsDefault;

class Program
{

    private static List<Edge<Vertex<VertexProperty>, EdgeProperty>> mstEdges = new();
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

        mstEdges = RunKruskal(graph, edgesList);

        PrintEdges(mstEdges);
        PrintMSTWeight(mstEdges);


    }
    static void PrintEdges(List<Edge<Vertex<VertexProperty>, EdgeProperty>> mstEdges)
    {
        foreach(Edge<Vertex<VertexProperty>, EdgeProperty> edge in mstEdges)
        {
            Console.WriteLine($"Source: {edge.Property.Source}, Target: {edge.Property.Target}, Weight: {edge.Property.Weight}" );
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



}
