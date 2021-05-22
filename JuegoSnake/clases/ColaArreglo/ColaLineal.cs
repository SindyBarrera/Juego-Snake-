using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace JuegoSnake.clases.ColaArreglo
{

    class ColaLineal
    {
        protected int fin;
        private static int MAXTAMQ = 9999;
        protected int frente;

        protected Object[] listaCola;

        public int tamaño;///////////////
       

        //Constructor que es el que va a crear la cola
        public ColaLineal()
        {
            frente = 0;
            fin = -1;
            listaCola = new object[MAXTAMQ];
        }


        public bool colaVacia()
        {
            return frente > fin;
        }
        public bool colaLlena()
        {
            return fin == MAXTAMQ - 1;
        }

        //Operaciones para trabajar con datos en la cola
        public void insertar(Object elemento)
        {
            if (!colaLlena())
            {
               
                listaCola[++fin] = elemento;
                tamaño++;
               
            }
            else
            {
                throw new Exception("Overflow de la cola");
            }
        }

        //Quitar elemento de la cola
        public Object quitar()
        {
            if (!colaVacia())
            {
                tamaño--;
                return listaCola[frente++];
                

            }
            else
            {
                throw new Exception("Cola Vacia");
            }
        }

        //Limpiar toda la cola
        public void borrarCola()
        {
            frente = 0;
            fin = -1;
        }

        //Acceso a la cola
        public Object frenteCola()
        {
            if (!colaVacia())
            {
                tamaño--;
                return listaCola[frente];

            }
            else
            {
                throw new Exception("Cola Vacia");
            }
        }

        public Object finalColaLineal()
        {
            if (!colaVacia())
            {
                return listaCola[fin];
            }
            else
            {
                throw new Exception("Cola vacia");
            }

        }

        public int Tamaño()
        {
            int tam;
            tam = tamaño;
            return tamaño;

        }

        public bool Any(Point x)
        {
            bool encontrado = false;
            for (int i = 0; i < Tamaño(); i++)
            {
                Point dato = (Point)listaCola[i];
                if (dato.X == x.X && dato.Y == x.Y)
                {
                    encontrado = true;
                }
                dato = new Point();
            }
            return encontrado;
        }


    }//end class
}
