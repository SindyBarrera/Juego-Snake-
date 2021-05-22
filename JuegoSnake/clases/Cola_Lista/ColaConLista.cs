using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace JuegoSnake.clases.ColaLista
{
    class ColaConLista
    {
        public Nodo frente;
        public Nodo fin;

        public int tamaño;

        //Constructor: crear la cola vacia
        public ColaConLista()
        {
            frente = fin = null;
            tamaño = 0;
        }

        //Verificar si la cola esta vacia
        public bool colaVacia()
        {
            return (frente == null);
        }

        //INSERTAR: elemento por el final de la cola
        public void insertar(Point elemento)
        {
            Nodo a;
            a = new Nodo(elemento);
            if (colaVacia())
            {

                frente = a;  
            }
            else
            {
                
                fin.siguiente = a;
            }
            fin = a;
            tamaño++;
        }

        //Extraer o quitar un elemento para que nos devuelva el valor antes de quitarlo
        public Point quitar()
        {
            Point aux;
            if (!colaVacia())
            {
                
                aux = frente.elemento;
                //tamaño--;
                frente = frente.siguiente;
                
            }
            else
            {
                throw new Exception("Error pq la cola esta vacia");

            }
            return aux;
        }

        //Vaciar la cola, o liberar todos los nodos
        //Hacemos una iteracion por toda la cola 
        public void borrarCola()
        {
            for (; frente != null;)
            {
                frente = frente.siguiente;//Pasando el siguiente asi mismo y se va eliminando
            }
        }
        //Devolver el valor que esta al frente de la cola
        public Point frenteCola()
        {
            if (colaVacia())
            {
                throw new Exception("Error porque la cola esta vacía");
            }
            return (frente.elemento);
        }

        public Point finalColaConLista()
        {
            if (!colaVacia())
            {
                return (fin.elemento);
            }
            else
            {
                throw new Exception("Cola vacia");
            }

        }


        public int Tamaño()
        {
            return tamaño;
        }
        /*public Queue<Point> convertirToQueue()
        {
            Nodo rec = frente;
            Queue<Point> aux = new Queue<Point>();
            
            while(rec.siguiente != null)
            {
                frente = frente.siguiente;
                aux.Enqueue(frente.elemento);
                
            }
            return aux;
        }*/

        public bool busquda(Point elemento)
        {
            Nodo aux = frente;
            bool encontrado = false;
            while(aux.siguiente != null)
            {
                aux = aux.siguiente;
                if(aux.elemento.X == elemento.X && aux.elemento.Y == elemento.Y)
                {
                    encontrado = true;
                }
            }
            return encontrado;

        }
    }//end class
}
