
namespace Zillow {
    using System;

    public class Node<T> where T : IComparable {
        public T Data { get; set; }
        public Node<T> Left { get; set; }
        public Node<T> Center { get; set; }
        public Node<T> Right { get; set; }
        public Node() {
            this.Data = default(T);
            this.Left = this.Center = this.Right = null;

        }

        public Node(T value) {
            this.Data = value;
            this.Left = this.Center = this.Right = null;
        }
    }
}
