/******************************************************************************
                              C++ Project 1: sorting
    For this project I will test out the following sorting algorithms.
    
    Bubble Sort: works by repeatedly swapping the adjacent elements if they are in the wrong order. 
    This algorithm is not suitable for large data sets as its average and worst-case time complexity is quite high.
    
    Heap Sort: Heap sort is a comparison-based sorting technique based on Binary Heap Data Structure. 
    It can be seen as an optimization over selection sort where we first find the max (or min) element and swap it with the last (or first). 
    We repeat the same process for the remaining elements. 
    https://www.geeksforgeeks.org/heap-sort/
    
    Stooge Sort:a It divides the array into two overlapping parts (2/3 each). 
    After that it performs sorting in first 2/3 part and then it performs sorting in last 2/3 part. 
    And then, sorting is done on first 2/3 part to ensure that the array is sorted.
    https://www.geeksforgeeks.org/stooge-sort/
    
*******************************************************************************/

#include <iostream>
using namespace std;

void printArray(int arr[], int size); // prints our array of element
void bubbleSort(int arr[], int n); // Does our bubble Sort algorithm, takes in the arr and the size of the array.
void heapify(int arr[], int N, int i); // heapifies the array of elements
void heapSort(int arr[], int N); // Performs heap sort
void stoogesort(int arr[], int l, int h); // erforms stooge sort

int main(){
    // line 31 to 64 initializes the 3 arrays that we will sort 
    int size;
    std::cout<<"Size of the array: ";
    std::cin >> size;
    
    int MaxElementValue;
    std::cout<<"Size of each element: ";
    std::cin >> MaxElementValue;
    
    std::cout << "\n";
    std::cout << "Array 1: ";
    
    int array1[size];
    for(int i=0; i<size; i++){ 
            array1[i] = (rand()%MaxElementValue)+1; 
            std::cout << array1[i] << " ";
    } 
    
    std::cout << "\n";
    std::cout << "Array 2: ";
    
    int array2[size];
    for(int i=0; i<size; i++){ 
            array2[i] = (rand()%MaxElementValue)+1; 
            std::cout << array2[i] << " ";
    } 
    
    std::cout << "\n";
    std::cout << "Array 3: ";
    
    int array3[size];
    for(int i=0; i<size; i++){ 
            array3[i] = (rand()%MaxElementValue)+1; 
            std::cout << array3[i] << " ";
    } 
    
    // uses bubble sort to sort out array 1
    std::cout << "\n";
    std::cout << "\n" << "Bubble sorted array:";
        bubbleSort(array1, size);
        printArray(array1, size);
        
    // uses heap sort to sort out array 2 
    std::cout << "\n";
    std::cout << "\n" << "Heap sorted array:";
        heapSort(array2, size);
        printArray(array2, size);
    
    // uses stooge sort to sort out array 3
    std::cout << "\n";
    std::cout << "\n" << "Stooge sorted array:";
        stoogesort(array3, 0, size-1);
        printArray(array3, size);
    
    return 0;
}

void printArray(int arr[], int size){
    int i;
    for (i = 0; i < size; i++)
        std::cout << " " << arr[i];
}

void bubbleSort(int arr[], int n){
    int i, j;
    bool swapped;
    for (i = 0; i < n - 1; i++) {
        swapped = false;
        for (j = 0; j < n - i - 1; j++) {
            if (arr[j] > arr[j + 1]) {
                swap(arr[j], arr[j + 1]);
                swapped = true;
            }
        }

        if (swapped == false)
            break;
    }
}

void heapify(int arr[], int N, int i){
    int largest = i;
    int l = 2 * i + 1;
    int r = 2 * i + 2;

    if (l < N && arr[l] > arr[largest])
        largest = l;
    if (r < N && arr[r] > arr[largest])
        largest = r;
    if (largest != i) {
        swap(arr[i], arr[largest]);
        heapify(arr, N, largest);
    }
}

void heapSort(int arr[], int N){
    for (int i = N / 2 - 1; i >= 0; i--)
        heapify(arr, N, i);
    for (int i = N - 1; i > 0; i--) {
        swap(arr[0], arr[i]);
        heapify(arr, i, 0);
    }
}

void stoogesort(int arr[], int l, int h) { 
    if (l >= h) 
        return; 
    if (arr[l] > arr[h]) 
        swap(arr[l], arr[h]); 
    if (h - l + 1 > 2) { 
        int t = (h - l + 1) / 3; 
  
        stoogesort(arr, l, h - t); 
        stoogesort(arr, l + t, h); 
        stoogesort(arr, l, h - t); 
    } 
} 
