/******************************************************************************
Project 3: Compound Interest
Write a Program to Find Compound Interest.
In this problem, you have to write a program that calculates and prints the compound interest for the given Principle, Rate of Interest, and Time.
*******************************************************************************/

#include <iostream>
#include <math.h>
#include <stdio.h>
using namespace std;

double GetAmount(double P, double R, int T);
double GetCompoundInterest(double A, double P);

int main()
{
    double PrincipleAmount;
    std::cout<<"Principle amount: $";
    std::cin >> PrincipleAmount;
    
    double Rate;
    std::cout<<"Rate: ";
    std::cin >> Rate;
    
    
    int Time;
    std::cout<<"Time (years): ";
    std::cin >> Time;
    
    std::cout << "\n";
    
    double Amount = GetAmount(PrincipleAmount, Rate, Time);
    std::cout<<"Amount: " << Amount << "\n";
    
    double CompoundInterest = GetCompoundInterest(Amount, PrincipleAmount);
    std::cout<<"Compound interest: " << CompoundInterest << "\n";
    
    
    return 0;
}

double GetAmount(double P, double R, int T){
    //Amount= P(1 + R/100)t
    /*
    P is principal amount 
    R is the rate and 
    T is the time span
    */
    return P * ((pow((1 + R / 100), T)));
}

double GetCompoundInterest(double A, double P){
    //Compound Interest = Amount - P
    /*
    P is principal amount 
    R is the rate and 
    T is the time span
    */
    return A - P;
}
