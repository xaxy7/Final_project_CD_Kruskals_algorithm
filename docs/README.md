## About The Project
(built With i.e., what kinds of environment?)

## Getting Started 
(how to install and run your program)

### Prerequisites
- .NET SDK installed (version 10)
- a code editor (for example VS Code)
### Installation
1. First download the project, extract the files from zip
2. Open in an IDE 
3. Go to terminal and run:
```bash
cd kruskalscomputation
```
```bash
dotnet add package ScottPlot
```
```bash
dotnet build
```
```bash
dotnet run 
```
### Usage

```dotnet run``` opens a menu where you can choose the mode for the calculations of the Minimal Spanning Tree
- Kruskal's Default
- Kruskal's With Coordinates

#### Kruskal's Default:
This solution offers two input modes:

0. Random input (Generates random edges)
1. Manual input (Allows the user to input the edges with their name and weight)
   - (Number of vertices, names of vertices, number of edges, edges: [Source, Target, Weight]) format

The output is a list of edges (with their source, target and weight stated) that are part of the MST.
The total weight/cost fot the MST is also displayed in the terminal.

#### Kruskal's with coordinates:

This solution offers 3 input modes:

0. Random Input (generates random vertices)
1. Long Manual Input (Requires the input to include the name of each vertex alongside the coordinates)
   - ( Name X Y ) format
2. Short Manual Input (auto generates names for vertices, needing only the coordinates)
    - Short Input (N X1 Y1 X2 Y2 ... XN YN) format

The output is also a list of edges (with their source, target and length). It also displays the minimal spanning tree length. On top of that, a graph is generated of the MST, and saved in the ```generated_graphs``` directory.

## Roadmap 
1. MST solution using Kruskal's algorithm with default settings
2. MST solution using Kruskal's algorithm with coordinates
3. Graph generation for the coordinate solution

## Contributing 
General improvement to the code are welcome

## License 
(your project license)

## Contact
## Acknowledgments
