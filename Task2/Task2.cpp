#include <iostream>
#include <list>
#include "AVLtree.h"
using namespace std;

int main()
{
	AVLtree<int> tree = AVLtree<int>(70);
	tree._insert(1);
	tree._insert(2);
	tree._insert(3);
	tree._insert(50);
	tree._insert(100);
	tree._delete(50);
	int* arr = tree._ToArray();
	for (int i = 0; i < tree._getCount(); i++)
		cout << arr[i] << " ";
	cout << endl;
	std::list<int> lst = tree._GetLeaves();
	for (int i : lst)
		std::cout << i << " ";
	delete[] arr;
	return 0;
}
