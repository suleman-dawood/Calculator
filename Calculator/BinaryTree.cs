using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class BinaryTree
    {
        public string Value;
        public BinaryTree Left;
        public BinaryTree Right;

        public BinaryTree(string value)
        {
            Value = value;
            Left = Right = null;
        }
    }
}
