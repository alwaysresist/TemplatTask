#pragma once
#include <algorithm>
#include <stdexcept>

template <class T>
class Queue;

template<class T>
Queue<T> CombineQueues(const Queue<T>& first, const Queue<T>& second);

template <class T>
class Queue
{
private:
	int size;
	T* array;
	int start, end;
	bool isLooped;
	bool isFull() {
		return this->start == this->end && this->isLooped;
	}
	bool isEmpty() {
		return this->start == this->end && !this->isLooped;
	}
public:
	Queue(int size) {
		this->size = size;
		this->array = new T[this->size];
		this->start = 0;
		this->end = 0;
		this->isLooped = false;
	}
	Queue(const Queue& queue) {
		this->size = queue.size;
		this->array = new T[this->size];
		std::copy(queue.array, queue.array + queue.size, this->array);
		this->start = queue.start;
		this->end = queue.end;
		this->isLooped = queue.isLooped;
	}
	~Queue() {
		delete[] array;
	}
	void push(T element) {
		if (this->isFull())
			throw std::out_of_range("Queue is full.");
		this->array[this->end] = element;
		this->end = (this->end + 1) % this->size;
		if (this->end == 0)
			this->isLooped = true;
	}
	T pop() {
		if (this->isEmpty())
			throw std::out_of_range("Queue is empty.");
		T element = this->array[this->start];
		this->start = (this->start + 1) % this->size;
		if (this->start == 0)
			this->isLooped = false;
		return element;
	}
	friend Queue CombineQueues<T>(const Queue<T>& first, const Queue<T>& second);
};

template<class T>
Queue<T> CombineQueues(const Queue<T>& first, const Queue<T>& second)
{
	int size = first.size + second.size;
	Queue<T> queue = Queue<T>(size);
	int index = first.start;
	bool isLooped = false;
	while (index != first.end || index == first.end && isLooped != first.isLooped) 
	{
		queue.push(first.array[index]);
		index = (index + 1) % first.size;
		if (index == 0)
			isLooped = true;
	}
	index = second.start;
	isLooped = false;
	while (index != second.end || index == second.end && isLooped != second.isLooped) 
	{
		queue.push(second.array[index]);
		index = (index + 1) % second.size;
		if (index == 0)
			isLooped = true;
	}
	return queue;
}