#include <iostream>
#include <bits/stdc++.h> // For using certain standard functions
#include <chrono> // For measuring execution time
using namespace std;
using namespace std::chrono; // To simplify time-related function calls

// Function declarations
int FibonacciRecursion(int n);
int IterativeFibonacci(int n);
int TailFibonacciRecursion(int n);

// Class to implement Fibonacci using Dynamic Programming
class FibonacciDynamic {
public:
    // Dynamic programming solution for Fibonacci
    int fib(int n) {
        int f[n + 2]; // Create an array to store Fibonacci numbers
        int i;
        f[0] = 0; // Base case
        f[1] = 1; // Base case

        // Calculate Fibonacci numbers and store in array
        for (i = 2; i <= n; i++) {
            f[i] = f[i - 1] + f[i - 2];
        }
        return f[n]; // Return nth Fibonacci number
    }
};

int main() {
    
    // Input number from user
    int n;
    std::cout << "Input: ";
    std::cin >> n;
    std::cout << "\n";
    
    // Measure and output time for recursive Fibonacci
    auto FibRecStart = high_resolution_clock::now(); // Start timer
    int FibRec = FibonacciRecursion(n); // Get Fibonacci result using recursion
    auto FibRecStop = high_resolution_clock::now(); // Stop timer
    auto FibRecDurationMIC = duration_cast<microseconds>(FibRecStop - FibRecStart); // Measure time in microseconds
    auto FibRecDurationMIL = duration_cast<milliseconds>(FibRecStop - FibRecStart); // Measure time in milliseconds
    auto FibRecDurationNAN = duration_cast<nanoseconds>(FibRecStop - FibRecStart);  // Measure time in nanoseconds
    std::cout << "Fibonacci recursion output: " << FibRec << "\n"; // Output result
    std::cout << "Total time: " << FibRecDurationMIC.count() << " microseconds\n";  // Output execution time
    std::cout << "Total time: " << FibRecDurationMIL.count() << " milliseconds\n";  // Output execution time
    std::cout << "Total time: " << FibRecDurationNAN.count() << " nanoseconds\n";   // Output execution time
    std::cout << "\n\n\n";
    
    // Measure and output time for iterative Fibonacci
    auto FibIterStart = high_resolution_clock::now(); // Start timer
    int FibIter = IterativeFibonacci(n); // Get Fibonacci result using iteration
    auto FibIterStop = high_resolution_clock::now(); // Stop timer
    auto FibIterDurationMIC = duration_cast<microseconds>(FibIterStop - FibIterStart); // Measure time in microseconds
    auto FibIterDurationMIL = duration_cast<milliseconds>(FibIterStop - FibIterStart); // Measure time in milliseconds
    auto FibIterDurationNAN = duration_cast<nanoseconds>(FibIterStop - FibIterStart);  // Measure time in nanoseconds
    std::cout << "Iterative Fibonacci output: " << FibIter << "\n"; // Output result
    std::cout << "Total time: " << FibIterDurationMIC.count() << " microseconds\n";   // Output execution time
    std::cout << "Total time: " << FibIterDurationMIL.count() << " milliseconds\n";   // Output execution time
    std::cout << "Total time: " << FibIterDurationNAN.count() << " nanoseconds\n";    // Output execution time
    std::cout << "\n\n\n";
    
    // Measure and output time for tail-recursive Fibonacci
    auto FibTailStart = high_resolution_clock::now(); // Start timer
    int FibTail = TailFibonacciRecursion(n); // Get Fibonacci result using tail recursion
    auto FibTailStop = high_resolution_clock::now(); // Stop timer
    auto FibTailDurationMIC = duration_cast<microseconds>(FibTailStop - FibTailStart); // Measure time in microseconds
    auto FibTailDurationMIL = duration_cast<milliseconds>(FibTailStop - FibTailStart); // Measure time in milliseconds
    auto FibTailDurationNAN = duration_cast<nanoseconds>(FibTailStop - FibTailStart);  // Measure time in nanoseconds
    std::cout << "Tail Recursion Fibonacci output: " << FibTail << "\n"; // Output result
    std::cout << "Total time: " << FibTailDurationMIC.count() << " microseconds\n";   // Output execution time
    std::cout << "Total time: " << FibTailDurationMIL.count() << " milliseconds\n";   // Output execution time
    std::cout << "Total time: " << FibTailDurationNAN.count() << " nanoseconds\n";    // Output execution time
    std::cout << "\n\n\n";
    
    // Measure and output time for dynamic programming Fibonacci
    FibonacciDynamic FibDyn; // Create object of FibonacciDynamic class
    auto FibDynStart = high_resolution_clock::now(); // Start timer
    std::cout << "Dynamic Fibonacci output: " << FibDyn.fib(n) << "\n"; // Get Fibonacci result using dynamic programming
    auto FibDynStop = high_resolution_clock::now(); // Stop timer
    auto FibDynDurationMIC = duration_cast<microseconds>(FibDynStop - FibDynStart); // Measure time in microseconds
    auto FibDynDurationMIL = duration_cast<milliseconds>(FibDynStop - FibDynStart); // Measure time in milliseconds
    auto FibDynDurationNAN = duration_cast<nanoseconds>(FibDynStop - FibDynStart);  // Measure time in nanoseconds
    std::cout << "Total time: " << FibDynDurationMIC.count() << " microseconds\n";  // Output execution time
    std::cout << "Total time: " << FibDynDurationMIL.count() << " milliseconds\n";  // Output execution time
    std::cout << "Total time: " << FibDynDurationNAN.count() << " nanoseconds\n";   // Output execution time
    std::cout << "\n\n\n";
    
    return 0;
}

// Recursive Fibonacci function
int FibonacciRecursion(int n) {
    if (n <= 1) // Base case
        return n;
    return FibonacciRecursion(n - 1) + FibonacciRecursion(n - 2); // Recursive call
}

// Iterative Fibonacci function
int IterativeFibonacci(int n) {
    int previous = 1; // First Fibonacci number
    int current = 1;  // Second Fibonacci number
    int next = 1;
    for (int i = 3; i <= n; ++i) { // Start from 3rd number and go to nth
        next = current + previous; // Calculate next Fibonacci number
        previous = current; // Update previous number
        current = next; // Update current number
    }
    return next; // Return nth Fibonacci number
}

// Tail-recursive Fibonacci function
int TailFibonacciRecursion(int n) {
    int a = 0, b = 1, c, i; // a and b are the first two numbers
    if (n == 0) // Base case
        return a;
    for (i = 2; i <= n; i++) { // Loop through until nth number
        c = a + b; // Calculate next Fibonacci number
        a = b; // Move forward in the sequence
        b = c; // Move forward in the sequence
    }
    return b; // Return nth Fibonacci number
}