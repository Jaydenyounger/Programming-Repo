import java.util.*;
import java.io.*;

public class TopoSort {

    public static void main(String[] args) {

        HashMap<Integer, ArrayList<Integer>> G1 = getBuiltInGraph(); //Acyclic Graph (DAG) G1.
        String[] names = { "cs143", "cs321", "cs322", "cs142", "cs370", "cs341", "cs326", "cs378", "cs401", "cs421" };

        int[] topo_order = topo_sort(G1); //Perform DFS topological sort on G1
            System.out.println("Topological Order for G1:");
                // print elements on the G1 hash map 
                for (int i = 0; i < names.length; i++) {
                    System.out.printf("%s: %d, ", names[i], topo_order[i]); 
                }
        System.out.println();
        System.out.println("-----------------------------------------------------------------------------------------------------");
        
        


        // Validate the topological order
        boolean isValid = validateTopoOrder(topo_order, G1);
        System.out.println("Is valid? " + isValid);
        System.out.println("-----------------------------------------------------------------------------------------------------");

        //Stored File paths for the DAGs
        /*
         * Note: the DAG files need to be on downloads or copy the location where the DAG files are at
         */
        String[] filePaths = { 
            "Downloads\\Java_TopoSort_CS 2123 Project 2/DAG1.txt", 
            "Downloads\\Java_TopoSort_CS 2123 Project 2/DAG2.txt", 
            "Downloads\\Java_TopoSort_CS 2123 Project 2/DAG3.txt",
            "Downloads\\Java_TopoSort_CS 2123 Project 2/DAG4.txt",
            "Downloads\\Java_TopoSort_CS 2123 Project 2/DAG5.txt"
        };


        for (String filePath : filePaths) { // loops through the filepaths array that contains the file paths to gather
            try {
                HashMap<Integer, ArrayList<Integer>> graphFromFile = loadGraphFromFile(filePath);//Load the graph from the file
                System.out.println("Loaded graph from " + filePath); // notify that we loaded a graph
                System.out.println(); // space out
                
                long startTime = System.nanoTime(); // get start time
                int[] dfsTopoOrder = topo_sort(graphFromFile);// Perform DFS-based Topological Sort
                long endTime = System.nanoTime();// get end time
                double dfsTimeMs = (endTime - startTime) / 1_000_000.0; // get the final run time
                System.out.printf("DFS Topo Sort for %s: %.3f ms\n", filePath, dfsTimeMs); // print time

                
                HashMap<Integer, ArrayList<Integer>> copyGraphForInduction = deepCopyGraph(graphFromFile); //copy of the graph to use with Induction Topological Sort
                startTime = System.nanoTime();// get start time
                int[] inductionTopoOrder = ind_topo(copyGraphForInduction);// Perform Induction-based Topological Sort
                endTime = System.nanoTime();// get end time
                double inductionTimeMs = (endTime - startTime) / 1_000_000.0;// get the final run time
                System.out.printf("Induction Topo Sort for %s: %.3f ms\n", filePath, inductionTimeMs);// print time
                System.out.println(); // space out


                // Calculate Speedup (Ti/Td)
                double speedup = inductionTimeMs / dfsTimeMs; // for the report, dividing the induction and dfs to get the speed up times
                System.out.printf("Speedup (Ti/Td) for %s: %.3f ms\n", filePath, speedup); // print the speed up times
                System.out.println("-----------------------------------------------------------------------------------------------------");// add dividers in between each set of data

            } catch (FileNotFoundException e) {
                System.out.println("File not found: " + filePath); //If no file is found, display file not found
                //the DAG files need to be on downloads or copy the location where the DAG files are at
            }
        }
    }




 
    public static HashMap<Integer, ArrayList<Integer>> loadGraphFromFile(String filePath) throws FileNotFoundException {
    // read the graph from the file and returns the graph to use
        HashMap<Integer, ArrayList<Integer>> graph = new HashMap<>();// empty graph
        Scanner scanner = new Scanner(new File(filePath));//a Scanner object to read the file

            while (scanner.hasNextLine()) { // Read the file till the end of the file is reached
                String[] edge = scanner.nextLine().split("\\s+");// 
                int tail = Integer.parseInt(edge[0]);// The starting vertex
                int head = Integer.parseInt(edge[1]);// The ending vertex

                    graph.computeIfAbsent(tail, k -> new ArrayList<>()).add(head);// Add the edge to the graph
                    graph.computeIfAbsent(head, k -> new ArrayList<>());//make sure the head vertex exists in the graph
            }
        return graph; // Return the new graph.
    }





    public static HashMap<Integer, ArrayList<Integer>> deepCopyGraph(HashMap<Integer, ArrayList<Integer>> original) {
    // creates a copy of the graph for induction topo sort
        HashMap<Integer, ArrayList<Integer>> copy = new HashMap<>(); // empty map to add elements to it
            for (Map.Entry<Integer, ArrayList<Integer>> entry : original.entrySet()) { // Loop through the orginal map
                copy.put(entry.getKey(), new ArrayList<>(entry.getValue())); // puts each element to the copy map
            }
        return copy;// returns the new copy
    }





    
    public static void dfs_topo(HashMap<Integer, ArrayList<Integer>> G, Integer s, Set<Integer> visited, Stack<Integer> stack) {
    // DFS topological sort
        visited.add(s);// Marks the current vertex
            for (Integer neighbor : G.get(s)) {// loops through each vertex
                if (!visited.contains(neighbor)) {// If the neighbor has not been visited, recursively perform DFS on it
                    dfs_topo(G, neighbor, visited, stack);
                }
            }
        stack.push(s); // push the current vertex onto the stack
    }





    public static int[] topo_sort(HashMap<Integer, ArrayList<Integer>> G) {
    // DFS-based topological sorting of the graph 
        int[] L = new int[G.size()];// store the topological order
        Set<Integer> visited = new HashSet<>();// visited vertices during DFS
        Stack<Integer> stack = new Stack<>();// hold the vertices in post-order
            for (Integer vertex : G.keySet()) {// Loop through each vertex in the graph
                if (!visited.contains(vertex)) {// If the vertex has not been visited, perform DFS on it
                    dfs_topo(G, vertex, visited, stack);
                }
            }

        int order = 1;
            while (!stack.isEmpty()) {// While the stack is not empty, pop elements to form the topological order
                Integer vertex = stack.pop();
                L[vertex] = order++;
            }
        return L; // Return the order of the graph
    }





    
    public static int[] ind_topo(HashMap<Integer, ArrayList<Integer>> G) {
    // Induction-based topological sort
        int[] count = new int[G.size()]; // in-degree of each node
            for (int i = 0; i < G.size(); i++) {
                for (Integer v : G.get(i)) {
                count[v]++;
                }
            }

        int[] L = new int[G.size()]; // an array to record the ordering of all nodes
        int k = 1; // topo order's value starts from 1 in induction-based method
            while (!G.isEmpty()) {
                Integer u = null;
                    for (Integer u1 : G.keySet()) {
                        if (count[u1] == 0) {
                            u = u1;
                            break; // break after finding the source node
                        }
                    }
            L[u] = k; // assign a order value to this node
            k++;
                for (Integer v : G.get(u)) {
                    count[v]--;
                }
                G.remove(u); // remove this node and its outgoing edges from G
            }
        return L;// returns the topological order of the graph
    }





    
    public static HashMap<Integer, ArrayList<Integer>> getBuiltInGraph() {
    // creates and returns a built-in graph for G1
        int cs143 = 0, cs321 = 1, cs322 = 2, cs142 = 3, cs370 = 4, cs341 = 5, cs326 = 6, cs378 = 7, cs401 = 8, cs421 = 9;
        int[][] g1 = { { cs321, cs341, cs370, cs378 }, 
        { cs322, cs326 }, 
        { cs401, cs421 }, 
        { cs143 }, {}, 
        { cs401 },
        { cs401, cs421 }, 
        { cs401 }, 
        {}, 
        {} 
        };

        HashMap<Integer, ArrayList<Integer>> G1 = new HashMap<>();// Create a HashMap to represent the graph
            for (int i = 0; i < g1.length; i++) {// Populate the HashMap with vertices
                ArrayList<Integer> neighbors = new ArrayList<>();
                    for (int j = 0; j < g1[i].length; j++) {//  Iterate through the outgoing edges
                        neighbors.add(g1[i][j]);// Add each neighbor to the list
                    }
                G1.put(i, neighbors);// Put the current vertex and its neighbors into the neighbors HashMap
            }
        return G1;// return the built in graph
    }





    public static boolean validateTopoOrder(int[] topo_order, HashMap<Integer, ArrayList<Integer>> graph) {
    // validates the topological order of a graph
        HashMap<Integer, Integer> position = new HashMap<>();
            for (int i = 0; i < topo_order.length; i++) {// // Map each node to its position
                position.put(i, topo_order[i]);  
            }

            for (Integer u : graph.keySet()) {// Iterate through each node in the graph
                for (Integer v : graph.get(u)) {// Iterate through each neighbor 
                    if (position.get(u) >= position.get(v)) {// Check if node u's position
                        return false;  
                    }
                }
            }
        return true;  // Returns if the order is valid 
    }
}
