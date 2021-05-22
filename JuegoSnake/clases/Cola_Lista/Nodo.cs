using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace JuegoSnake.clases.ColaLista
{
    class Nodo
    {
        public Point elemento;
        public Nodo siguiente;

        //Constructor
        public Nodo(Point x)
        {
            elemento = x;
            siguiente = null;
        }

        /*public int X { get; internal set; }
        public int Y { get; internal set; }

        //public int Y { get; set; }
        //public int X { get; set; }

        public struct Point
        {
            public int X { get; set; }
            public int Y { get; set; }
        }*/


    }
}
