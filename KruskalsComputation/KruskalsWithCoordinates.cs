
using DataStructureLibrary.Graph;
using kruskalscomputation.properties;

namespace kruskalscomputation;


static class KruskalsWithCoordinates
{
    
    public static List<Edge<Vertex<CoordinatesVertexProperty>,CoordinatesEdgeProperty>> RunKruskalsWithCoordinates(List<Vertex<CoordinatesVertexProperty>> vertices)
    {
        Graph<CoordinatesVertexProperty, CoordinatesEdgeProperty> graph = new Graph<CoordinatesVertexProperty, CoordinatesEdgeProperty>();

        List<Edge<Vertex<CoordinatesVertexProperty>,CoordinatesEdgeProperty>> edgesList = CreateEdgesFromInput( vertices,  graph);
        
        Dictionary<Vertex<CoordinatesVertexProperty>, Vertex<CoordinatesVertexProperty>> parent = new Dictionary<Vertex<CoordinatesVertexProperty>, Vertex<CoordinatesVertexProperty>>();

        List<Edge<Vertex<CoordinatesVertexProperty>, CoordinatesEdgeProperty>> mstEdges =
        new List<Edge<Vertex<CoordinatesVertexProperty>, CoordinatesEdgeProperty>>();

        foreach(Vertex<CoordinatesVertexProperty> v in vertices)
        {
            parent[v] = v;
        }
        edgesList = edgesList
            .OrderBy(e => e.Property.Length)
            .ToList();
        
        foreach(Edge<Vertex<CoordinatesVertexProperty>, CoordinatesEdgeProperty> edge in edgesList)
        {
            Vertex<CoordinatesVertexProperty> source = edge.Property.Source!;
            Vertex<CoordinatesVertexProperty> target = edge.Property.Target!;
            
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


    static List<Edge<Vertex<CoordinatesVertexProperty>,CoordinatesEdgeProperty>> CreateEdgesFromInput(List<Vertex<CoordinatesVertexProperty>> vertices, Graph<CoordinatesVertexProperty, CoordinatesEdgeProperty> graph)
    {
        List<Edge<Vertex<CoordinatesVertexProperty>,CoordinatesEdgeProperty>> edges = new List<Edge<Vertex<CoordinatesVertexProperty>,CoordinatesEdgeProperty>>();

        for(int i = 0; i < vertices.Count; i++)
        {
            for(int j  = i +1; j < vertices.Count; j++)
            {
                Vertex<CoordinatesVertexProperty> source = vertices[i];
                Vertex<CoordinatesVertexProperty> target = vertices[j];
                Edge<Vertex<CoordinatesVertexProperty>, CoordinatesEdgeProperty> edge =  graph.AddEdge(source,target)!;
                double length = Math.Sqrt(Math.Pow(target.Property.X - source.Property.X, 2) + Math.Pow(target.Property.Y - source.Property.Y,2));
                edge.Property.Length = length;
  
                edges.Add(edge);
                


            }
        }
        


        return edges;
    }
    private static Vertex<CoordinatesVertexProperty> Find(Vertex<CoordinatesVertexProperty> v, Dictionary<Vertex<CoordinatesVertexProperty>, Vertex<CoordinatesVertexProperty>> parent)
    {
        if (!parent.ContainsKey(v))
            throw new Exception("Vertex not found in disjoint set");

        while (parent[v] != v)
        {
            v = parent[v];
        }

        return v;
    }

    private static void Union(Vertex<CoordinatesVertexProperty> a, Vertex<CoordinatesVertexProperty> b, Dictionary<Vertex<CoordinatesVertexProperty>, Vertex<CoordinatesVertexProperty>> parent)
    {
        Vertex<CoordinatesVertexProperty> rootA = Find(a, parent);
        Vertex<CoordinatesVertexProperty> rootB = Find(b, parent);

        if (rootA != rootB)
        {
            parent[rootA] = rootB;
        }
    }

}