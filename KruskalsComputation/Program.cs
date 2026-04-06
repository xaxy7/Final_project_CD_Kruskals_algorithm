namespace kruskalscomputation;

using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using DataStructureLibrary.Graph;
using kruskalscomputation.properties;
using Microsoft.VisualBasic;

class Program
{
    static Dictionary<Vertex<VertexProperty>, Vertex<VertexProperty>> parent = new  Dictionary<Vertex<VertexProperty>, Vertex<VertexProperty>>();
    static List<Edge<Vertex<VertexProperty>,EdgeProperty>> mstEdges = new();
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

        edgesList = edgesList
            .OrderBy(e => e.Property.Weight)
            .ToList();
        


        foreach(Vertex<VertexProperty> v in graph.GetVertices())
        {
            parent[v] = v;
        }


        foreach(Edge<Vertex<VertexProperty>, EdgeProperty> edge in edgesList)
        {
           Vertex<VertexProperty> source = edge.Property.Source!;
           Vertex<VertexProperty> target = edge.Property.Target!;

            if(Find(source) != Find(target))
            {
                mstEdges.Add(edge);
                Union(source,target);
            }
            if(mstEdges.Count == graph.GetVertices().Count() - 1)
            {
                break;
            }
        }
        PrintEdges();
        PrintMSTWeight();


    }
    static void PrintEdges()
    {
        foreach(Edge<Vertex<VertexProperty>, EdgeProperty> edge in mstEdges)
        {
            Console.WriteLine($"Source: {edge.Property.Source}, Target: {edge.Property.Target}, Weight: {edge.Property.Weight}" );
        }
    }
    static void PrintMSTWeight()
    {
        int totalWeight = 0;
        foreach(Edge<Vertex<VertexProperty>, EdgeProperty> edge in mstEdges)
        {
            totalWeight += edge.Property.Weight;
        }
        Console.WriteLine($"Total Minimal Spanning Tree Weight: {totalWeight}");
    }
    static Vertex<VertexProperty> Find(Vertex<VertexProperty> v)
    {
        if (!parent.ContainsKey(v))
            throw new Exception("Vertex not found in disjoint set");
        while(parent[v] != v)
        {
            v = parent[v];
        }

        return v;

    }
    static void Union(Vertex<VertexProperty> a, Vertex<VertexProperty> b)
    {
        Vertex<VertexProperty> rootA = Find(a);
        Vertex<VertexProperty> rootB = Find(b);

        if(rootA != rootB)
        {
            parent[rootA] = rootB;
        }
    }


}
