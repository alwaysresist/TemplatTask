using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    
    class AVLtree<T> where T : IComparable
    {
        class AVLnode
        {
            public T data;
            public int height;
            public AVLnode left, right;

            public AVLnode(T data)
            {
                this.data = data;
                this.height = 1;
                this.left = null;
                this.right = null;
            }

            public int Balance()
            {
                int leftHeight, rightHeight;

                if (this.left != null)
                {
                    leftHeight = this.left.height;
                }
                else
                {
                    leftHeight = 0;
                }
                if (this.right != null)
                {
                    rightHeight = this.right.height;
                }
                else
                {
                    rightHeight = 0;
                }

                return rightHeight - leftHeight;
            }

            public void MakeHeight()
            {
                int leftHeight, rightHeight;

                if (this.left != null)
                {
                    leftHeight = this.left.height;
                }
                else
                {
                    leftHeight = 0;
                }
                if (this.right != null)
                {
                    rightHeight = this.right.height;
                }
                else
                {
                    rightHeight = 0;
                }

                this.height = Math.Max(leftHeight, rightHeight) + 1;
            }
        }
        

        AVLnode root;
        int count;
        bool Changes;

        public AVLtree()
        {
            this.root = null;
            this.Changes = false;
            this.count = 0;
        }

        public AVLtree(T data)
        {
            this.root = new AVLnode(data);
            this.Changes = false;
            this.count = 1;
        }

        void _Balance(ref AVLnode ptr)
        {
            int oldHeight = ptr.height;
            ptr.MakeHeight();
            int balance = ptr.Balance();
            if (balance > 1)
            {
                if (ptr.right.Balance() < 0)
                {
                    this.TurnLeft(ref ptr.right);
                }
                this.TurnRight(ref ptr);
                if (ptr.height == oldHeight)
                {
                    this.Changes = false;
                }
            }
            else if (balance < -1)
            {
                if (ptr.left.Balance() > 0)
                {
                    this.TurnRight(ref ptr.left);
                }
                this.TurnLeft(ref ptr);
                if (ptr.height == oldHeight)
                {
                    this.Changes = false;
                }
            }
        }
        void _Insert(ref AVLnode pointer, T data)
        {
            if (pointer == null)
            {
                this.Changes = true;
                pointer = new AVLnode(data);
            }
            else
            {
                if (data.CompareTo(pointer.data) == -1)
                {
                    this._Insert(ref pointer.left, data);
                    if (this.Changes)
                        this._Balance(ref pointer);
                }
                else
                {
                    this._Insert(ref pointer.right, data);
                    if (this.Changes)
                        this._Balance(ref pointer);
                }
            }
        }

        public void Insert(T data) 
        {
            this._Insert(ref this.root, data);
            this.count += 1;
        }

        void TurnLeft(ref AVLnode ptr)
        {
            AVLnode temp = ptr.left;
            ptr.left = temp.right;
            temp.right = ptr;
            ptr.MakeHeight();
            temp.MakeHeight();
            ptr = temp;
        }

        void TurnRight(ref AVLnode ptr)
        {
            AVLnode temp;
            temp = ptr.right;
            ptr.right = temp.left;
            temp.left = ptr;
            ptr.MakeHeight();
            temp.MakeHeight();
            ptr = temp;
        }

        void ListOfLeaves(AVLnode ptr, T[] array, ref int arrayIndex)
        {
            if (ptr.left != null)
                ListOfLeaves(ptr.left, array, ref arrayIndex);
            array[arrayIndex] = ptr.data;
            arrayIndex++;
            if (ptr.right != null)
                ListOfLeaves(ptr.right, array, ref arrayIndex);
        }

        bool Search(AVLnode ptr, T data)
        {
            if (ptr != null)
            {
                if (data.CompareTo(ptr.data) == 0)
                {
                    return true;
                }
                else if (data.CompareTo(ptr.data) == -1)
                {
                    return this.Search(ptr.left, data);
                }
                else if (data.CompareTo(ptr.data) == 1)
                {
                    return this.Search(ptr.right, data);
                }
            }
            return false;
        }

        void Dispose(AVLnode ptr)
        {
            if (ptr != null)
            {
                if (ptr.left != null)
                {
                    this.Dispose(ptr.left);
                }
                if (ptr.right != null)
                {
                    this.Dispose(ptr.right);
                }
            }
        }

        public void _insert(T data) 
        {
            this._Insert(ref this.root, data);
            this.count += 1;
        }
        void FindToDel(ref AVLnode replaceable, AVLnode ptr, ref AVLnode temp)
        {
            if (replaceable.right != null)
            {
                this.FindToDel(ref replaceable.right, ptr, ref temp);
                this._Balance(ref replaceable);
            }
            else
            {
                temp = replaceable;
                ptr.data = replaceable.data;
                replaceable = replaceable.left;
            }
        }

        void Remove(ref AVLnode ptr, T data)
        {
            if (ptr != null)
            {
                if (data.CompareTo(ptr.data) == -1)
                {
                    this.Remove(ref ptr.left, data);
                    this._Balance(ref ptr);
                }
                else if (data.CompareTo(ptr.data) == 1)
                {
                    this.Remove(ref ptr.right, data);
                    this._Balance(ref ptr);
                }
                else
                {
                    AVLnode temp = ptr;
                    if (ptr.right == null)
                        ptr = ptr.left;
                    else if (ptr.left == null)
                        ptr = ptr.right;
                    else
                        this.FindToDel(ref ptr.left, ptr, ref temp);
                }
            }
        }

        public void Remove(T data)
        {
            this.Remove(ref this.root, data);
            this.count--;
        }

        bool _Contains(AVLnode ptr, T data)
        {
            if (ptr != null)
            {
                if (data.CompareTo(ptr.data) == -1)
                {
                    return this._Contains(ptr.left, data);
                }
                else if (data.CompareTo(ptr.data) == 1)
                {
                    return this._Contains(ptr.right, data);
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public bool Find(T data)
        {
            return this._Contains(this.root, data);
        }
        public int getCount()
        {
            return this.count;
        }

        public T[] Leaves()
        {
            T[] arr = new T[this.count];
            int arrIndex = 0;
            ListOfLeaves(this.root, arr, ref arrIndex);
            return arr;
        }
    }
}
