namespace  kruskalscomputation;


using DataStructureLibrary.Graph;
using kruskalscomputation.properties;



static class KruskalsDefault
{

    public static List<Edge<Vertex<VertexProperty>, EdgeProperty>> RunKruskalsDefualt(Graph<VertexProperty, EdgeProperty> graph, List<Edge<Vertex<VertexProperty>, EdgeProperty>> edgesList)
    {
        List<Edge<Vertex<VertexProperty>, EdgeProperty>> mstEdges = new List<Edge<Vertex<VertexProperty>, EdgeProperty>>();

        Dictionary<Vertex<VertexProperty>, Vertex<VertexProperty>> parent = new  Dictionary<Vertex<VertexProperty>, Vertex<VertexProperty>>();

        
        foreach(Vertex<VertexProperty> v in graph.GetVertices())
        {
            parent[v] = v;
        }
        edgesList = edgesList
            .OrderBy(e => e.Property.Weight)
            .ToList();

        foreach(Edge<Vertex<VertexProperty>, EdgeProperty> edge in edgesList)
        {
           Vertex<VertexProperty> source = edge.Property.Source!;
           Vertex<VertexProperty> target = edge.Property.Target!;

            if(Find(source, parent) != Find(target, parent))
            {
                mstEdges.Add(edge);
                Union(source,target, parent);
            }
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