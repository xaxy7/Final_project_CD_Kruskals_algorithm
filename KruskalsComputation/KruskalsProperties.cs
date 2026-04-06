namespace kruskalscomputation.properties;

using DataStructureLibrary.Graph;

class VertexProperty : BasicVertexProperty;

class EdgeProperty : BasicEdgeProperty<Vertex<VertexProperty>>{
    public int Weight;
}

