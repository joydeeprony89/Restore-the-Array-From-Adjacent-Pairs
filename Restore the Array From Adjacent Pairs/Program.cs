using System.Collections.Generic;

namespace Restore_the_Array_From_Adjacent_Pairs
{
  class Program
  {
    static void Main(string[] args)
    {
      // [[2,1],[3,4],[3,2]]
      // [[4,-2],[1,4],[-3,1]]
    }
  }
  public class Solution
  {
    public int[] RestoreArray(int[][] adjacentPairs)
    {
      // step 1 = create the adj list for each pairs, (1,2) say we can reach 2 from 1 or reach 1 from 2, as its mentioned in the question

      // Step 2 - identify the head and tail => nodes are having frequency 1 i.e there are no outbound edge are the nodes as head and tail of the 1D array.

      // Step 3 - DFS

      // Step 1
      Dictionary<int, List<int>> adj = new Dictionary<int, List<int>>();
      foreach (var pair in adjacentPairs)
      {
        int i = pair[0];
        int j = pair[1];
        if (!adj.ContainsKey(i))
        {
          adj.Add(i, new List<int>());
        }
        if (!adj.ContainsKey(j))
        {
          adj.Add(j, new List<int>());
        }
        adj[i].Add(j);
        adj[j].Add(i);
      }


      // Step 2
      int? head = null;
      int? tail = null;
      foreach (var key in adj.Keys)
      {
        if (adj[key].Count == 1 && head == null)
        {
          head = key;
        }
        else if (adj[key].Count == 1)
        {
          tail = key;
        }
      }

      HashSet<int> visited = new HashSet<int>();
      List<int> result = new List<int>();

      // Step 3
      void Dfs(Dictionary<int, List<int>> adj, int head, int tail, HashSet<int> visited, int i, List<int> result)
      {
        result.Add(head);
        if (visited.Contains(head) || head == tail) return;

        visited.Add(head);
        var neighbours = adj[head];
        foreach (var n in neighbours)
        {
          if (!visited.Contains(n))
            Dfs(adj, n, tail, visited, i + 1, result);
        }
      }

      Dfs(adj, head.Value, tail.Value, visited, 0, result);

      return result.ToArray();
    }
  }
}
