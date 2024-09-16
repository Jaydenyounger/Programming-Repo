#include <iostream>
#include <string>
using namespace std;

// Function prototypes
bool isSafe(int** board, int row, int col, int N); 
void printSolution(int** board, int N, string queen, string empty);
bool solveNQUtil(int** board, int col, int N);

int main() {
    // Structure to hold the queen's symbol, empty cell symbol, board size, and whether a solution exists
    struct {
        string Queen;     // Symbol for the queen
        string Empty;     // Symbol for an empty space
        int size;         // Size of the board (N x N)
        bool CanBeSolved; // Flag to check if a solution exists
    } Structure;
    
    // Get user input for queen and empty space symbols and board size
    cout << "Queen = ";
    cin >> Structure.Queen;
    
    cout << "Empty = ";
    cin >> Structure.Empty;
    
    cout << "Board Size = ";
    cin >> Structure.size;
    
    // Dynamically allocate memory for the board
    int N = Structure.size;
    int** board = new int*[N];
    for (int i = 0; i < N; i++) {
        board[i] = new int[N];
        for (int j = 0; j < N; j++) {
            board[i][j] = 0; // Initialize all cells to 0 (empty)
        }
    }
    
    // Attempt to solve the N-Queens problem
    if (!solveNQUtil(board, 0, N)) {
        cout << "Solution does not exist\n"; // If no solution is found
        Structure.CanBeSolved = false;
    } else {
        printSolution(board, N, Structure.Queen, Structure.Empty); // Print the solution
        Structure.CanBeSolved = true;
    }

    // Deallocate memory for the board
    for (int i = 0; i < N; i++) {
        delete[] board[i];
    }
    delete[] board;
    
    return 0;
}

// print the solution board
void printSolution(int** board, int N, string queen, string empty) {
    for (int i = 0; i < N; i++) {
        for (int j = 0; j < N; j++)
            if (board[i][j])
                cout << queen << " "; // Print the queen symbol
            else
                cout << empty << " "; // Print the empty space symbol
        cout << "\n";
    }
}

// check if it's safe to place a queen at board[row][col]
bool isSafe(int** board, int row, int col, int N) {
    int i, j;
    
    // Check this row on the left side
    for (i = 0; i < col; i++)
        if (board[row][i])
            return false;
    
    // Check upper diagonal on the left side
    for (i = row, j = col; i >= 0 && j >= 0; i--, j--)
        if (board[i][j])
            return false;
    
    // Check lower diagonal on the left side
    for (i = row, j = col; j >= 0 && i < N; i++, j--)
        if (board[i][j])
            return false;
    
    // If no conflicts, it's safe to place the queen here
    return true;
}


// solves the N-Queens problem using recursion
bool solveNQUtil(int** board, int col, int N) {
    // Base case: If all queens are placed, return true
    if (col >= N)
        return true;
    
    // Try placing a queen in each row one by one
    for (int i = 0; i < N; i++) {
        // Check if it's safe to place the queen at board[i][col]
        if (isSafe(board, i, col, N)) {
            board[i][col] = 1; // Place the queen
            
            // Recur to place the rest of the queens
            if (solveNQUtil(board, col + 1, N))
                return true;
            
            // If placing queen at board[i][col] doesn't lead to a solution,
            // remove the queen (BACKTRACK)
            board[i][col] = 0;
        }
    }
    
    // If the queen cannot be placed in any row in this column, return false
    return false;
}
