using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace JuegoSnake.clases.ColaArreglo
{
    class ColaCircular
    {
        private static int fin;
        private static int MAXTAMQ = 999;
        protected int frente;
        protected Object[] listaCola;

        public int tamaño;/////////////////////////////////////////

        // avavnza un aposicion

        public int siguiente(int r)
        {
            return (r + 1) % MAXTAMQ;
        }

        public ColaCircular()
        {
            frente = 0;
            fin = MAXTAMQ - 1;
            listaCola = new object[MAXTAMQ];
        }

        // Validaciones

        public bool colaVacia()
        {
            return frente == siguiente(fin);
        }

        public bool colaLlena()
        {
            return frente == siguiente(siguiente(fin));
        }

        //operaciones de modificacion de cola
        public void insertar(Object elemento)
        {
            if (!colaLlena())
            {
                fin = siguiente(fin);
                listaCola[fin] = elemento;
                tamaño++;//////////////////////////////////////////////////////
            }
            else
            {
                throw new Exception("Overflow de la cola");
            }


        }
        // quitar elemento
        public Object quitar()
        {
            if (!colaVacia())
            {
                Object tm = listaCola[frente];
                frente = siguiente(frente);
                tamaño--;//////////////////////////////////////////
                return tm;
            }
            else
            {
                throw new Exception("Cola vacia");
            }
        }

        public void borrarCola()
        {
            frente = 0;
            fin = MAXTAMQ - 1;
        }

        //obtener el valor de frente
        public Object frenteCola()
        {
            if (!colaVacia())
            {
                return listaCola[frente];
            }
            else
            {
                throw new Exception("Cola vacia");
            }
        }

        public Object finalColaCircular()
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

        //Para que pierda cuando chochamos con el cuerpo de la culebra
        public bool Any(Point x)
        {
            bool encontrado = false;

            for(int i = 0; i < Tamaño(); i++)
            {
                Point dato =(Point)listaCola[i];//Posicion por posicion
                if(dato.X == x.X && dato.Y == x.Y)
                {
                    encontrado = true;
                }
                dato = new Point();
            }
            return encontrado;
        }

    }//Fin de la class
}

