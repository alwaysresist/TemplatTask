#pragma once
#include <iostream>
#include <list>

template <class T>
class AVLtree;

template <class T>
class AVLNode {
private:
	T data;
	int height;
	AVLNode* left, * right;
	AVLNode(T data) {
		this->data = data;
		this->height = 1;
		this->left = nullptr;
		this->right = nullptr;
	}
	int Balance() {
		int leftHeight, rightHeight;
		if (this->left != nullptr)
			leftHeight = this->left->height;
		else
			leftHeight = 0;
		if (this->right != nullptr)
			rightHeight = this->right->height;
		else
			rightHeight = 0;
		return rightHeight - leftHeight;
	}
	void NewHeight() {
		int leftHeight, rightHeight;
		if (this->left != nullptr)
			leftHeight = this->left->height;
		else
			leftHeight = 0;
		if (this->right != nullptr)
			rightHeight = this->right->height;
		else
			rightHeight = 0;
		this->height = leftHeight > rightHeight ? leftHeight + 1 : rightHeight + 1;
	}
public:
	friend class AVLtree<T>;
};


template <class T>
class AVLtree {
private:
	AVLNode<T> *root;
	bool changes;
	int count;
	void Insert(AVLNode<T>*& ptr, T data) {
		if (ptr == nullptr) 
		{
			this->changes = true;
			ptr = new AVLNode<T>(data);
		}
		else
		{
			if (data < ptr->data) 
			{
				this->Insert(ptr->left, data);
				if (this->changes)
					this->Balance(ptr);
			}
			else 
			{
				this->Insert(ptr->right, data);
				if (this->changes)
					this->Balance(ptr);
			}
		}
	}
	void Delete(AVLNode<T>*& ptr, T data) {
		AVLNode<T>* temp;
		if (ptr != nullptr) 
		{
			if (data < ptr->data) 
			{
				this->Delete(ptr->left, data);
				this->Balance(ptr);
			}
			else if (data > ptr->data) 
			{
				this->Delete(ptr->right, data);
				this->Balance(ptr);
			}
			else
			{
				temp = ptr;
				if (ptr->right == nullptr)
					ptr = ptr->left;
				else if (ptr->left == nullptr)
					ptr = ptr->right;
				else
					this->FindToDel(ptr->left, ptr, temp);
				delete temp;
			}
		}
	}
	bool Search(AVLNode<T>* ptr, T data) {
		if (ptr != nullptr)
		{
			if (data == ptr->data)
				return true;
			else if (data < ptr->data)
				return this->Search(ptr->left, data);
			else if (data > ptr->data)
				return this->Search(ptr->right, data);
		}
		return false;
	}
	void FindToDel(AVLNode<T>*& replaceable, AVLNode<T>* ptr, AVLNode<T>*& temp) {
		if (replaceable->right != nullptr) 
		{
			this->FindToDel(replaceable->right, ptr, temp);
			this->Balance(replaceable);
		}
		else 
		{
			temp = replaceable;
			ptr->data = replaceable->data;
			replaceable = replaceable->left;
		}
	}
	void Balance(AVLNode<T>*& ptr) {
		int oldHeight = ptr->height;
		ptr->NewHeight();
		int balance = ptr->Balance();

		if (balance > 1) 
		{
			if (ptr->right->Balance() < 0)
				this->TurnLeft(ptr->right);
			this->TurnRight(ptr);
			if (ptr->height == oldHeight)
				this->changes = false;
		}
		else if (balance < -1) 
		{
			if (ptr->left->Balance() > 0)
				this->TurnRight(ptr->left);
			this->TurnLeft(ptr);
			if (ptr->height == oldHeight)
				this->changes = false;
		}
	}
	void TurnLeft(AVLNode<T>*& ptr) {
		AVLNode<T>* temp;
		temp = ptr->left;
		ptr->left = temp->right;
		temp->right = ptr;
		ptr->NewHeight();
		temp->NewHeight();
		ptr = temp;
	}
	void TurnRight(AVLNode<T>*& ptr) {
		AVLNode<T>* temp;
		temp = ptr->right;
		ptr->right = temp->left;
		temp->left = ptr;
		ptr->NewHeight();
		temp->NewHeight();
		ptr = temp;
	}
	
	void ToArray(AVLNode<T>* treeptr, T* array, int& arrayptr) {
		if (treeptr->left != nullptr)
			ToArray(treeptr->left, array, arrayptr);
		array[arrayptr] = treeptr->data;
		arrayptr++;
		if (treeptr->right != nullptr)
			ToArray(treeptr->right, array, arrayptr);
	}
	void GetLeaves(AVLNode<T>* ptr, std::list<T>& list) {
		if (ptr->left != nullptr)
			GetLeaves(ptr->left, list);
		if (ptr->right != nullptr)
			GetLeaves(ptr->right, list);
		if (ptr->left == nullptr && ptr->right == nullptr)
			list.push_back(ptr->data);
	}
	void Dispose(AVLNode<T>* ptr) {
		if (ptr != nullptr) 
		{
			if (ptr->left != nullptr)
				this->Dispose(ptr->left);
			if (ptr->right != nullptr)
				this->Dispose(ptr->right);
			delete ptr;
		}
	}
public:
	AVLtree() {
		this->root = nullptr;
		this->changes = false;
		this->count = 0;
	}
	AVLtree(T data) {
		this->root = new AVLNode<T>(data);
		this->changes = false;
		this->count = 1;
	}
	~AVLtree() {
		this->Dispose(this->root);
	}
	void _insert(T data) {
		this->Insert(this->root, data);
		this->count++;
	}
	void _delete(T data) {
		this->Delete(this->root, data);
		this->count--;
	}
	bool _search(T data) {
		return this->Search(this->root, data);
	}
	int _getCount() {
		return this->count;
	}
	T* _ToArray() {
		T* array = new T[this->count];
		int arrayPointer = 0;
		ToArray(this->root, array, arrayPointer);
		return array;
	}
	std::list<T> _GetLeaves() { //List of leaves
		std::list<T> list;
		GetLeaves(this->root, list);
		return list;
	}
};