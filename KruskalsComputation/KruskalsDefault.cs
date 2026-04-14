namespace  kruskalscomputation;


using DataStructureLibrary.Graph;
using kruskalscomputation.properties;



static class KruskalsDefault
{
    // default solution for the MST using Kruskal's Algorithm
    public static List<Edge<Vertex<VertexProperty>, EdgeProperty>> RunKruskalsDefault(Graph<VertexProperty, EdgeProperty> graph, List<Edge<Vertex<VertexProperty>, EdgeProperty>> edgesList)
    {
        List<Edge<Vertex<VertexProperty>, EdgeProperty>> mstEdges = new List<Edge<Vertex<VertexProperty>, EdgeProperty>>();

        Dictionary<Vertex<VertexProperty>, Vertex<VertexProperty>> parent = new  Dictionary<Vertex<VertexProperty>, Vertex<VertexProperty>>();

        // vertices are extracted from the graph and added to a parent Dictionary that serves as a disjoint set
        foreach(Vertex<VertexProperty> v in graph.GetVertices())
        {
            parent[v] = v;
        }
        //edges are sorted based on weight of them (smallest to largest weight)
        edgesList = edgesList
            .OrderBy(e => e.Property.Weight)
            .ToList();

        //loop that calculates the list of edges that the Minimum Spanning Tree consists of
        foreach(Edge<Vertex<VertexProperty>, EdgeProperty> edge in edgesList)
        {
           Vertex<VertexProperty> source = edge.Property.Source!;
           Vertex<VertexProperty> target = edge.Property.Target!;

            // if the source vertex and the target vertex are yet not connected by the Spanning Tree, add them to the Spanning tree
            if(Find(source, parent) != Find(target, parent))
            {
                mstEdges.Add(edge);

                //adds the vertices to the disjoint set
                Union(source,target, parent);
            }
            //condition that ends the loop faster
            if(mstEdges.Count == graph.GetVertices().Count() - 1)
            {
                break;
            }
        }
        return mstEdges;
        
    }
    

    private static Vertex<VertexProperty> Find(Vertex<VertexProperty> v, Dictionary<Vertex<VertexProperty>, Vertex<VertexProperty>> parent)
    {
        if (!parent.ContainsKey(v))
            throw new Exception("Vertex not found in disjoint set");

        while (parent[v] != v)
        {
            v = parent[v];
        }

        return v;
    }

    private static void Union(Vertex<VertexProperty> a, Vertex<VertexProperty> b, Dictionary<Vertex<VertexProperty>, Vertex<VertexProperty>> parent)
    {
        Vertex<VertexProperty> rootA = Find(a, parent);
        Vertex<VertexProperty> rootB = Find(b, parent);

        if (rootA != rootB)
        {
            parent[rootA] = rootB;
        }
    }

}