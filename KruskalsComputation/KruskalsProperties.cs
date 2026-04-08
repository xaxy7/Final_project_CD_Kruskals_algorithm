namespace kruskalscomputation.properties;

using DataStructureLibrary.Graph;

public class VertexProperty : BasicVertexProperty;

public class CoordinatesVertexProperty : BasicVertexProperty
{
    public double X;
    public double Y;
}

public class EdgeProperty : BasicEdgeProperty<Vertex<VertexProperty>>{
    public int Weight;
}

public class CoordinatesEdgeProperty : BasicEdgeProperty<Vertex<CoordinatesVertexProperty>>
{
    public double Length;
};
