using DataStructureLibrary.Graph;
using kruskalscomputation.properties;
using ScottPlot;

namespace kruskalscomputation;


static class CoordinateGraphRendered
{
    public static void DrawGraph(
        List<Vertex<CoordinatesVertexProperty>> vertices, List<Edge<Vertex<CoordinatesVertexProperty>, CoordinatesEdgeProperty>> allEdges, List<Edge<Vertex<CoordinatesVertexProperty>, CoordinatesEdgeProperty>> mstEdges)
    {
        Plot plot = new Plot();
        foreach(Edge<Vertex<CoordinatesVertexProperty>, CoordinatesEdgeProperty> edge in allEdges)
        {
            double [] xs = new double[]
            {
                edge.Property.Source!.Property.X,
                edge.Property.Target!.Property.X
            };

            double[] ys = new double[]
            {
                edge.Property.Source!.Property.Y,
                edge.Property.Target!.Property.Y,
            };
            plot.Add.ScatterLine(xs,ys);
        }
        foreach(Edge<Vertex<CoordinatesVertexProperty>, CoordinatesEdgeProperty> edge in mstEdges)
        {
            double [] xs = new double[]
            {
                edge.Property.Source!.Property.X,
                edge.Property.Target!.Property.X
            };

            double[] ys = new double[]
            {
                edge.Property.Source!.Property.Y,
                edge.Property.Target!.Property.Y,
            };
            plot.Add.ScatterLine(xs,ys);
        }

        double[] dataX = new double[vertices.Count];
        double[] dataY = new double[vertices.Count];

        for(int i = 0; i < vertices.Count; i++)
        {
            dataX[i] = vertices[i].Property.X;
            dataY[i] = vertices[i].Property.Y;
        }

        plot.Add.ScatterPoints(dataX, dataY);

        foreach (Vertex<CoordinatesVertexProperty> vertex in vertices)
        {
            plot.Add.Text(vertex.Property.Name, vertex.Property.X, vertex.Property.Y);
        }
        plot.Axes.AutoScale();

        string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string fileName = $"graph_{timestamp}.png";

        plot.SavePng(fileName,1000, 800);

        Console.WriteLine($"Graph saved to: {fileName}");

    }


}
