#include <iostream>
#include <string>
using namespace std;

// Function prototypes for the different types of pyramids
void PrintPyramid(int rows, string Print, int Space);             // Regular pyramid
void PrintInvertedPyramid(int rows, string Print, int Space);     // Inverted pyramid
void PrintFloydsPyramid(int rows, string Print, int Space);       // Floyd's pyramid

int main() {

    // Structure to hold the input for the pyramid configurations
    struct {
        string asterisk;  // Character to use for the pyramid (e.g., "*")
        int Space;        // Space variable (not used in this version but included)
        int rows;         // Number of rows for the pyramids
    } Structure;

    Structure.asterisk = "*";  // Default symbol for the pyramid
    
    // Getting input for the regular pyramid
    std::cout << "Row amount for Pyramid: ";
    std::cin >> Structure.rows;  // Input number of rows
    cout << "\n";  // New line for better formatting

    std::cout << "Pyramid";
    cout << "\n\n";  // New lines for pyramid separation
    PrintPyramid(Structure.rows, Structure.asterisk, Structure.Space);  // Call the function to print the pyramid
    cout << "\n";  // New line after the pyramid
    
    // Getting input for the inverted pyramid
    std::cout << "Row amount for Inverted Pyramid: ";
    std::cin >> Structure.rows;  // Input number of rows
    cout << "\n";
    std::cout << "Inverted Pyramid";
    cout << "\n\n";  // New lines for inverted pyramid separation
    PrintInvertedPyramid(Structure.rows, Structure.asterisk, Structure.Space);  // Call the function to print the inverted pyramid
    
    // Getting input for Floyd's pyramid
    std::cout << "Row amount for Floyd's Pyramid: ";
    std::cin >> Structure.rows;  // Input number of rows
    cout << "\n";
    std::cout << "Floyd's Pyramid";
    cout << "\n\n";  // New lines for Floyd's pyramid separation
    PrintFloydsPyramid(Structure.rows, Structure.asterisk, Structure.Space);  // Call the function to print Floyd's pyramid
    
    return 0;  // End of the program
}

// Function to print a regular pyramid
void PrintPyramid(int rows, string Print, int Space) {
    // Loop over each row
    for (int i = 1, k = 0; i <= rows; ++i, k = 0) {
        // Print leading spaces to center the pyramid
        for (Space = 1; Space <= rows - i; ++Space) {
            cout << " ";  // Output a space character
        }

        // Print pyramid pattern for the current row
        while (k != 2 * i - 1) {
            cout << Print;  // Output the pyramid character (e.g., "*")
            ++k;  // Increment k to control the width of the pyramid
        }
        cout << endl;  // Move to the next line after each row
    }
}

// Function to print an inverted pyramid
void PrintInvertedPyramid(int rows, string Print, int Space) {
    // Loop over each row starting from the largest
    for (int i = rows; i >= 1; --i) {
        // Print leading spaces to center the inverted pyramid
        for (int space = 0; space < rows - i; ++space)
            cout << "  ";  // Output two spaces to indent the inverted pyramid

        // Print stars for the current row in the first half
        for (int j = i; j <= 2 * i - 1; ++j)
            cout << "* ";  // Output the star character

        // Print stars for the current row in the second half
        for (int j = 0; j < i - 1; ++j)
            cout << "* ";  // Output the star character again

        cout << endl;  // Move to the next line after each row
    }
}

// Function to print Floyd's pyramid
void PrintFloydsPyramid(int rows, string Print, int Space) {
    int number = 1;  // Start number for Floyd's pyramid
    
    // Loop over each row
    for (int i = 1, k = 0; i <= rows; ++i, k = 0) {
        // Print leading spaces to center the pyramid
        for (Space = 1; Space <= rows - i; ++Space) {
            cout << " ";  // Output a space character
        }

        // Print numbers in a pyramid pattern
        while (k != 2 * i - 1) {
            cout << number;  // Output the current number
            ++k;  // Increment k to control the width of the pyramid
            ++number;  // Increment the number to print the next value
        }
        cout << endl;  // Move to the next line after each row
    }
}