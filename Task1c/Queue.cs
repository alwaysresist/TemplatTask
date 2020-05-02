using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Queue<T> : ICloneable
    {
        T[] array;
        int start, end;
        bool isLooped;

        bool isFull() => this.start == this.end && this.isLooped;
        bool isEmpty() => this.start == this.end && !this.isLooped;

        public Queue(int size)
        {
            this.array = new T[size];
            this.start = this.end = 0;
            this.isLooped = false;
        }

        public void Push(T element)
        {
            if (this.isFull())
            {
                throw new InvalidOperationException();
            }
            this.array[this.end] = element;
            this.end = (this.end + 1) % this.array.Length;
            if (this.end == 0)
            {
                this.isLooped = true;
            }
        }

        public T Pop()
        {
            if (this.isEmpty())
            {
                throw new InvalidOperationException();
            }
            T element = this.array[this.start];
            this.start = (this.start + 1) % this.array.Length;
            if (this.start == 0)
            {
                this.isLooped = false;
            }
            return element;
        }

        public object Clone()
        {
            Queue<T> que = new Queue<T>(this.array.Length);
            que.array = (T[])this.array.Clone();
            que.start = this.start;
            que.end = this.end;
            que.isLooped = this.isLooped;
            return que;
        }

        public static Queue<T> Combine(Queue<T> first, Queue<T> second)
        {
            int size = first.array.Length + second.array.Length;
            Queue<T> que = new Queue<T>(size);
            int index = first.start;
            bool isLooped = false;
            while (index != first.end || isLooped != first.isLooped)
            {
                que.Push(first.array[index]);
                index = (index + 1) % first.array.Length;
                if (index == 0)
                {
                    isLooped = true;
                }
            }
            index = second.start;
            isLooped = false;
            while (index != second.end || isLooped != second.isLooped)
            {
                que.Push(second.array[index]);
                index = (index + 1) % second.array.Length;
                if (index == 0)
                {
                    isLooped = true;
                }
            }
            return que;
        }

    }
}
