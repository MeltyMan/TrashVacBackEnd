//
//  main.cpp
//  PrimeTest
//
//  Created by Karl Johansson on 2021-08-25.
//

#include <iostream>
#include <ctime>
#include <ratio>
#include <chrono>

using namespace std;
using namespace std::chrono;
bool isPrime(long n);
void writeProgress(long current, long total);


int main(int argc, const char * argv[]) {
    // insert code here...
   
    if (argc < 3){
        cout << "Missing paramters\n";
        return 0;
    }
    
    
    
    
    high_resolution_clock::time_point t1 = high_resolution_clock::now();
    
    
    char *p;
   
    long n1 = strtol(argv[1], &p, 10);
    long n2 = strtol(argv[2], &p, 10);
    long current = 0;
    long total = (n2 - n1) + 1;
    long primeCount = 0;
    int progressSteps = total / 200;
    int stepsSinceLastUpdate = 0;
    
    bool verboseMode = false;
    
    if (argc == 4)
    {
        std::cout << "argv[3]: " << argv[3] << "\n";
        string vs(argv[3]);
        
        if (vs == "-v")
        {
            verboseMode = true;
        }
        
    }
    
    long *primeList = new long[total];
    
    cout << "Prime tester\n------------\n";
    cout << "Testing span from " << n1 << " to " << n2 << "\n";
    writeProgress(current, total);
    for (long n = n1; n <= n2; n++)
    {
        current++;
        
        
        if (verboseMode)
        {
            writeProgress(current, total);
            
        }
        else
        {
            stepsSinceLastUpdate++;
            if (stepsSinceLastUpdate >= progressSteps)
            {
                stepsSinceLastUpdate = 0;
                writeProgress(current, total);
            }
        }
        bool r = isPrime(n);
        
        if (r)
        {
            primeCount++;
            primeList[primeCount-1] = n;
            //cout << n << " is a prime number...\n";
        }
        
    }
    
    high_resolution_clock::time_point t2 = high_resolution_clock::now();
    if (!verboseMode)
    {
        writeProgress(current, total);
    }
    
    duration<double, std::milli> time_span = t2 - t1;
    std::cout << "\nDone! Found " << primeCount << " prime(s).\n";
    std::cout << "It took " << time_span.count() << " milliseconds.\n";
    std::cout << "Do you want to see the primes? >>";
    char ch;
    ch = getchar();
    
    if (ch == 121 || ch == 89)
    {
        for (int i = 0; i < primeCount; i++)
        {
            std::cout << primeList[i] << "\n";
        }
        //std::cout << "you selected yes";
    }
    
    std::cout << std::endl;
    return 0;
}

bool isPrime(long n)
{
    
    if (n <= 1)
        return false;
    if (n <= 3)
        return true;
  
    
    if (n % 2 == 0 || n % 3 == 0)
        return false;
  
    for (int i = 5; i * i <= n; i = i + 6)
        if (n % i == 0 || n % (i + 2) == 0)
            return false;
  
    return true;
}
void writeProgress(long current, long total)
{
    double perc = ((double)current / (double)total) * 100;
    std::cout << "\r" << perc << "%     ";
    std::flush(std::cout);
}

