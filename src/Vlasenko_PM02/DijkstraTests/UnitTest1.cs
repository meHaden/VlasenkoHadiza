using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

public class DijkstraTests
{
    [Fact]
    public void FindsShortestPath_InSimpleGraph()
    {
        int n = 3;
        double[,] graph = new double[n, n];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                graph[i, j] = double.PositiveInfinity;
            }
            graph[i, i] = 0;
        }

        graph[0, 1] = 2;
        graph[1, 0] = 2;
        graph[1, 2] = 3;
        graph[2, 1] = 3;

        var distances = Program.Dijkstra(graph, 0); 

        Assert.Equal(0, distances[0]);
        Assert.Equal(2, distances[1]);
        Assert.Equal(5, distances[2]);
    }
}