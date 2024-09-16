#include <iostream>
#include <random>
using namespace std;

// Function prototypes
int DidPlayerWin(int CPUval, int PlayerValue); // Determines the result of the game
int GetCPUplay(int min, int max); // Generates a random choice for the CPU

int main() {
    // Display the options to the player
    std::cout << "(1) Rock" << "\n";
    std::cout << "(2) Paper" << "\n";
    std::cout << "(3) Scissors" << "\n";
    
    int PlayerVal;
    // Prompt the player to choose a value between 1 and 3
    std::cout << "What do you play: " << "\n";
    std::cin >> PlayerVal;
    
    // Validate the player's input
    if(PlayerVal >= 1 && PlayerVal <= 3){
        // Player has provided a valid input
    }else {
        // Invalid input, request the player to input a valid value
        do{
            std::cout << "You have played an invalid input, select a valid input" << "\n";
            std::cin >> PlayerVal;
        } while(!(PlayerVal >= 1 && PlayerVal <= 3)); // Continue until a valid input is given
    } 
    
    // Get the CPU's random move (1, 2, or 3)
    int CPUnum = GetCPUplay(1, 3);

    // Determine the outcome of the game
    int result = DidPlayerWin(CPUnum, PlayerVal);
    
    // Display the result based on the value returned by DidPlayerWin()
    if (result == 0) { 
        cout << "Game Draw!\n";  // It's a draw
    } 
    else if (result == 1) { 
        cout << "Congratulations! The player won the game!\n";  // Player wins
    } 
    else { 
        cout << "The computer won the game!\n";  // CPU wins
    } 

    return 0;
}

// Function to generate a random number between min and max (inclusive) for the CPU's move
int GetCPUplay(int min, int max) {
    static bool first = true; // Ensure srand is called only once
    if (first) {  
        srand( time(NULL) ); // Seed the random number generator
        first = false;
    }
    return min + rand() % (( max + 1 ) - min); // Return a random value between min and max
}

// Function to determine if the player won, lost, or if the game is a draw
int DidPlayerWin(int CPUval, int PlayerValue) {
    /*
    (1) Rock
    (2) Paper
    (3) Scissors
    */
    
    // Check for a draw
    if (PlayerValue == CPUval) { 
        return 0; // Return 0 for a draw
    } 
    
    // Conditions where the player wins
    if (PlayerValue == 3 && CPUval == 2) { // Scissors beats Paper
        return 1; 
    } 
    if (PlayerValue == 2 && CPUval == 1) { // Paper beats Rock
        return 1; 
    } 
    if (PlayerValue == 1 && CPUval == 3) { // Rock beats Scissors
        return 1; 
    } 
    
    // If none of the above, the player has lost
    return -1;
}
