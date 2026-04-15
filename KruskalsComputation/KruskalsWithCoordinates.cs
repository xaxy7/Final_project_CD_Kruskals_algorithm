
using DataStructureLibrary.Graph;
using kruskalscomputation.properties;


namespace kruskalscomputation;


static class KruskalsWithCoordinates
{
    // solution for the Minimum Spanning Tree in Euclidean space using Kruskal's algorithm
    public static CoordinateKruskalsResult RunKruskalsWithCoordinates(List<Vertex<CoordinatesVertexProperty>> vertices)
    {
        Graph<CoordinatesVertexProperty, CoordinatesEdgeProperty> graph = new Graph<CoordinatesVertexProperty, CoordinatesEdgeProperty>();

        List<Edge<Vertex<CoordinatesVertexProperty>,CoordinatesEdgeProperty>> edgesList = CreateEdgesFromInput( vertices,  graph);
        
        //a dictionary of parent is declared, that serves as a disjoint set
        Dictionary<Vertex<CoordinatesVertexProperty>, Vertex<CoordinatesVertexProperty>> parent = new Dictionary<Vertex<CoordinatesVertexProperty>, Vertex<CoordinatesVertexProperty>>();

        List<Edge<Vertex<CoordinatesVertexProperty>, CoordinatesEdgeProperty>> mstEdges =
        new List<Edge<Vertex<CoordinatesVertexProperty>, CoordinatesEdgeProperty>>();

        // vertices are extracted from the graph and added to a parent Dictionary that serves as a disjoint set
        foreach(Vertex<CoordinatesVertexProperty> v in vertices)
        {
            parent[v] = v;
        }
        //edges are sorted based on weight of them (smallest to largest weight)        
        edgesList = edgesList
            .OrderBy(e => e.Property.Length)
            .ToList();

        //loop that calculates the list of edges that the Minimum Spanning Tree consists of        
        foreach(Edge<Vertex<CoordinatesVertexProperty>, CoordinatesEdgeProperty> edge in edgesList)
        {
            Vertex<CoordinatesVertexProperty> source = edge.Property.Source!;
            Vertex<CoordinatesVertexProperty> target = edge.Property.Target!;

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
        //utilizing the class CoordinateKruskalsResult to return both allEdges of the graph and MSTedges, which are later used to generate the graph 
        CoordinateKruskalsResult result = new CoordinateKruskalsResult();
        result.AllEdges = edgesList;
        result.MstEdges = mstEdges;

        return result;


    }

// function that receives the vertices and creates edges between each of them, using the length of the edge as weight
    static List<Edge<Vertex<CoordinatesVertexProperty>,CoordinatesEdgeProperty>> CreateEdgesFromInput(List<Vertex<CoordinatesVertexProperty>> vertices, Graph<CoordinatesVertexProperty, CoordinatesEdgeProperty> graph)
    {
        List<Edge<Vertex<CoordinatesVertexProperty>,CoordinatesEdgeProperty>> edges = new List<Edge<Vertex<CoordinatesVertexProperty>,CoordinatesEdgeProperty>>();

        //for loop that creates and edge between every 2 vertices
        for(int i = 0; i < vertices.Count; i++)
        {
            for(int j  = i +1; j < vertices.Count; j++)
            {
                Vertex<CoordinatesVertexProperty> source = vertices[i];
                Vertex<CoordinatesVertexProperty> target = vertices[j];
                Edge<Vertex<CoordinatesVertexProperty>, CoordinatesEdgeProperty> edge =  graph.AddEdge(source,target)!;
                //length between two vertices using euclidean distance formula
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