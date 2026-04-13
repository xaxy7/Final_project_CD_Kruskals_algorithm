using DataStructureLibrary.Graph;
using kruskalscomputation.properties;
using ScottPlot;
using ScottPlot.Plottables;

namespace kruskalscomputation;


static class CoordinateGraphRendered
{
    //function that is responsible for drawing a graph for the coordinate solution, using ScottPlot external library
    public static void DrawGraph(
        List<Vertex<CoordinatesVertexProperty>> vertices, List<Edge<Vertex<CoordinatesVertexProperty>, CoordinatesEdgeProperty>> allEdges, List<Edge<Vertex<CoordinatesVertexProperty>, CoordinatesEdgeProperty>> mstEdges)
    {
        Plot plot = new Plot();
        // loop that adds every single edge to the plot, showing possible connections between vertices
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
            var line = plot.Add.ScatterLine(xs, ys);
            line.Color = ScottPlot.Colors.Gray;
            line.LineWidth = 1;
        }
        //loop that adds the MST edges, marking them with red color, and also displaying labels with the length of the edges
        foreach(Edge<Vertex<CoordinatesVertexProperty>, CoordinatesEdgeProperty> edge in mstEdges)
        {
            double x1 = edge.Property.Source!.Property.X;
            double y1 = edge.Property.Source!.Property.Y;


            double x2 = edge.Property.Target!.Property.X;
            double y2 = edge.Property.Target!.Property.Y;

            //0.1 to offset the label so it doesn't sit exactly on the line
            double midX = (x1 + x2) /2 + 0.1;
            double midY = (y1 + y2) /2 + 0.1;

            // adds the length labels
            var text = plot.Add.Text(edge.Property.Length.ToString("F2"), midX, midY);
            text.LabelFontColor = Colors.Red;
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
            var line = plot.Add.ScatterLine(xs, ys);
            line.Color = ScottPlot.Colors.Red;
            line.LineWidth = 3;
        }

        double[] dataX = new double[vertices.Count];
        double[] dataY = new double[vertices.Count];


        // adds the vertices to the graph
        for(int i = 0; i < vertices.Count; i++)
        {
            dataX[i] = vertices[i].Property.X;
            dataY[i] = vertices[i].Property.Y;
        }
    
        plot.Add.ScatterPoints(dataX, dataY);

        //adds the name of the vertices with an offset
        foreach (Vertex<CoordinatesVertexProperty> vertex in vertices)
        {
                plot.Add.Text(vertex.Property.Name, vertex.Property.X + 0.1, vertex.Property.Y + 0.1);
        }
        plot.Axes.AutoScale();

        //saves the graph to png 
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string fileName = $"generated_graphs/graph_{timestamp}.png";

        plot.SavePng(fileName,1000, 800);

        Console.WriteLine($"Graph saved to: {fileName}");

    }


}
