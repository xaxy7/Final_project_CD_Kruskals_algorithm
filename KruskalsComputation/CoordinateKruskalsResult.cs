using DataStructureLibrary.Graph;
using kruskalscomputation.properties;

namespace kruskalscomputation;


public class CoordinateKruskalsResult
{
    public List<Edge<Vertex<CoordinatesVertexProperty>, CoordinatesEdgeProperty>> AllEdges { get; set; }
    public List<Edge<Vertex<CoordinatesVertexProperty>, CoordinatesEdgeProperty>> MstEdges { get; set; }

    public CoordinateKruskalsResult()
    {
        AllEdges = new List<Edge<Vertex<CoordinatesVertexProperty>, CoordinatesEdgeProperty>>();
        MstEdges = new List<Edge<Vertex<CoordinatesVertexProperty>, CoordinatesEdgeProperty>>();
    }
}