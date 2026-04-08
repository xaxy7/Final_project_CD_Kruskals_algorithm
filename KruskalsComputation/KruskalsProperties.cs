namespace kruskalscomputation.properties;

using DataStructureLibrary.Graph;

class VertexProperty : BasicVertexProperty;

class CoordinatesVertexProperty : BasicVertexProperty
{
    public double X;
    public double Y;
}

class EdgeProperty : BasicEdgeProperty<Vertex<VertexProperty>>{
    public int Weight;
}

class CoordinatesEdgeProperty : BasicEdgeProperty<Vertex<CoordinatesVertexProperty>>
{
    public double Length;
};
