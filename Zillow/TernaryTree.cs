
namespace Zillow {
    using System;
    using System.Collections.Generic;
    public class TernaryTree<T> where T: IComparable {
        public Node<T> Head { get; set; }
        public void Add(T value) {
            var childNode = new Node<T>(value);
            if (childNode != null)
                this.Add(childNode);
        }

        public void Delete(T value) {
            // nullable bool to store iterators relation with parent.
            // if parent.left = iterator -> nodeDirection = -1
            // if parent.center = iterator -> nodeDirection = 0
            // if parent.right = iterator -> nodeDirection = 1
            int nodeDirection = 0;
            // ASSUMPTION: we will delete the first node that matches this - 
            // for instance if a node with value 65 has a center child with value 65 
            // we will delete the parent not the child.
            var iterator = this.Head;
            Node<T> parent = null, child = null;
            iterator = this.findNode(value, out parent);

            if (iterator == null) {
                // too bad - we didnt find a node
                return;
            } 
            // we found the node. Lets store the direction once instead of computing that each time.
            nodeDirection = this.getDirection(iterator, parent);
            // the nones
            // case : leaf node
            if (iterator.Left == null && iterator.Right == null && iterator.Center == null) {
                this.deleteLeafNode(nodeDirection, parent);
                return;
            }
            // everything else:
            // start with the leftest subtree
            // replace current node with root of leftest subtree
            // add each node from all the other subtrees to the tree starting
            // at current node.
            if (iterator.Left!= null) {

                // leftest subtree is iterator.left
                child = iterator.Left;
                // replace current node with the leftest subtree
                this.reorientNode(nodeDirection, parent, child);
                // add nodes from the remaining subtrees to the new current node.

                if (iterator.Center != null) {
                    // add all the nodes under the center to the subtree starting from child
                    this.AddSubTree(iterator.Center);
                }
                if (iterator.Right != null) {
                    // add all the nodes under right to the subtree starting from child
                    this.AddSubTree(iterator.Right);
                }
            }
            else if (iterator.Center != null) {
                // iterator.left was null so leftest subtree is now center.
                child = iterator.Center;
                // replace current node with the leftest subtree
                this.reorientNode(nodeDirection, parent, child);
                // add nodes from the remaining subtrees to the new current node.

                if (iterator.Right != null) {
                    // add all the nodes under right to the subtree starting from child
                    this.AddSubTree(iterator.Right);
                }
            }
            else if (iterator.Right != null) {
                // iterator.left and iterator.center are both null. So leftest subtree is iterator.right.
                child = iterator.Right;
                // replace current node with the leftest subtree
                this.reorientNode(nodeDirection, parent, child);
            }
        }

        /// <summary>
        /// Private method to align the child to the correct position of the parent.
        /// </summary>
        /// <param name="nodeDirection">-1 for left, 0 for center, +1 for right.</param>
        /// <param name="parent">the node under which to place the child.</param>
        /// <param name="child">the node to be reoriented.</param>
        private void reorientNode(int nodeDirection, Node<T> parent, Node<T> child) {
            // assign the correct subtree
            if (parent == null) {
                this.Head = child;
                return;
            }
            if (nodeDirection == -1) {
                parent.Left = child;
            }
            else if (nodeDirection == 1) {
                parent.Right = child;
            }
            else {
                parent.Center = child;
            }
        }

        /// <summary>
        /// If this is a leaf node, figure out the parent direction and delete the node.
        /// </summary>
        /// <param name="nodeDirection">-1=parent.left, 0=parent.center, +1=parent.right </param>
        /// <param name="parent">parent of the leaf node</param>
        private void deleteLeafNode(int nodeDirection, Node<T> parent) {
            if (parent == null) {
                // this is the only node
                this.Head = null;
            }
            else if (nodeDirection == -1) {
                // we are on the left subtree
                parent.Left = null;
            }
            else if (nodeDirection == 1) {
                // we are on the right subtree
                parent.Right = null;
            }
            else {
                // superfluous case
                // we will never be on the child center node.
                parent.Center = null;
            }
        }

        private int getDirection(Node<T> iterator, Node<T> parent) {
            if (iterator != null && parent != null) {
                if (iterator.Data.CompareTo(parent.Data) < 0) return -1;
                else if (iterator.Data.CompareTo(parent.Data) > 0) return 1;
                return 0;
            }
            // return a error value 
            return 3;
        }

        private Node<T> findNode(T value, out Node<T> parent) {
            var iterator = this.Head;
            parent = null;
            while (iterator != null && iterator.Data.CompareTo(value) != 0) {
                // until we have not reached the end of the tree and we have not found a node that matches - LOOP
                parent = iterator;
                if (iterator.Data.CompareTo(value) > 0) {
                    iterator = iterator.Left;
                }
                else if (iterator.Data.CompareTo(value) < 0) {
                    iterator = iterator.Right;
                }
                // no need to check center since we are comparing the node itself to the value.
                // this also implies that we will never have the iterator on the child center node. 
            }

            return iterator;
        }

        private void AddSubTree(Node<T> subRoot) {
            if (subRoot == null) {
                // if subRoot is null there is nothing to add.
                // nothing to do here.
                return;
            }

            // find the correct spot for the subtree.
            this.Add(subRoot);
        }

        /// <summary>
        /// Add a new node to the current tree.
        /// </summary>
        /// <param name="childNode">Node to be added.</param>
        private void Add(Node<T> childNode) {
            if (this.Head == null) {
                // the tree is empty, we are head. 
                this.Head = childNode;
                return;
            }
            // find the correct spot to add this node.
            var current = this.Head;
            while (true) {
                var parent = current;
                if (childNode.Data.CompareTo(current.Data) < 0) {
                    current = current.Left;
                    if (current == null) {
                        parent.Left = childNode;
                        return;
                    }
                }
                else if (childNode.Data.CompareTo(current.Data) == 0) {
                    current = current.Center;
                    if (current == null) {
                        parent.Center = childNode;
                        return;
                    }
                }
                else {
                    current = current.Right;
                    if (current == null) {
                        parent.Right = childNode;
                        return;
                    }
                }
            }
        }
    }
}
