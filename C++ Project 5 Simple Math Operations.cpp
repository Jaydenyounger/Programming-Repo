#include <iostream>

// Function prototypes
double add(double x, double y);
double Multiply(double x, double y);
double Subtract(double x, double y);
double Divide(double x, double y);

int main()
{
    // Get the first number
    double x;
    std::cout << "Enter your 1st number: ";
    std::cin >> x;
    
    std::cout << "\n";
    
    // Get the second number
    double y;
    std::cout << "Enter your 2nd number: ";
    std::cin >> y;
    
    std::cout << "\n";
    
    // Prompt user for operation selection
    std::cout << "Enter (A) for Addition\n";
    std::cout << "Enter (M) for Multiplication\n";
    std::cout << "Enter (S) for Subtraction\n";
    std::cout << "Enter (D) for Division\n";
    
    char KeyName;
    std::cout << "Operation Key: ";
    std::cin >> KeyName;
    std::cout << "\n";

    std::cout << "Operation Solution: ";

    if (KeyName == 'A' || KeyName == 'a') { // Addition
        double a = add(x, y);
        std::cout << a;
    } 
    else if (KeyName == 'M' || KeyName == 'm') { // Multiplication
        double m = Multiply(x, y);
        std::cout << m;
    } 
    else if (KeyName == 'S' || KeyName == 's') { // Subtraction
        double s = Subtract(x, y);
        std::cout << s;
    } 
    else if (KeyName == 'D' || KeyName == 'd') { // Division
        if (y == 0) { // Prevent division by zero
            std::cout << "Error: Division by zero!";
        } else {
            double d = Divide(x, y);
            std::cout << d;
        }
    } 
    else {
        std::cout << "Operation Key is invalid."; // Invalid key entered
    }
    
    return 0;
}

// Function to add two numbers
double add(double x, double y) {
    return x + y;
}

// Function to multiply two numbers
double Multiply(double x, double y) {
    return x * y;
}

// Function to subtract two numbers
double Subtract(double x, double y) {
    return x - y;
}

// Function to divide two numbers
double Divide(double x, double y) {
    return x / y;
}