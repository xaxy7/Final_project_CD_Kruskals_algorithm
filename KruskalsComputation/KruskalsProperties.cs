namespace kruskalscomputation.properties;

using DataStructureLibrary.Graph;



// VertexProperty is used for the default solution
public class VertexProperty : BasicVertexProperty;

// this one is ofc used for the coordinate solution, adds X and Y variables that serve as coordinates of vertices
public class CoordinatesVertexProperty : BasicVertexProperty
{
    public double X;
    public double Y;
}


//used for the default solution, adds weight to edges, which is essential for MST calculation
public class EdgeProperty : BasicEdgeProperty<Vertex<VertexProperty>>{
    public int Weight;
}

//used for the coordinate solution, adds length that has the same function as weight, but with different variable type
public class CoordinatesEdgeProperty : BasicEdgeProperty<Vertex<CoordinatesVertexProperty>>
{
    public double Length;
};
