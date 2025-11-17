#include <iostream>
#include <stack>
#include <vector>
using namespace std;

int main()
{
    int n;
    cin >> n;

    vector<int> seq(n);
    for (int i = 0; i < n; i++)
    {
        cin >> seq[i];
    }

    stack<int> s;

    for (int i = 0; i < n; i++)
    {
        s.push(seq[i]);
    }

    for (int i = 0; i < n; i++)
    {
        if (seq[i] != s.top())
        {
            cout << "It is not";
            return 0;
        }
        s.pop();
    }

    cout << "It is";
    return 0;
}


/*
    A stack is a data structure which follows the LIFO (Last in, Last out) priciple. Which means that The first 
    item which is appended to the top of the stack will be the last one to be be removed (poped). The stack helps
    solvin this problem by reversing the items and the stack is the perfect fit beacause we have to top pop the last
    item from the stack and that last iten will be the first item in another stack or list. 
    A stack is pretty much like a an array, but the priiciple is that we can not insert an item in the middle of a stack, unlike ans array 
*/


