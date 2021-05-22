using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JuegoSnake.clases
{
    class Archivo
    {
        String direccion;//Para la direccion

        public Archivo()
        {
            direccion = @"C:\Users\Sindy Barrera\Desktop\Juego Snake K\JUEGOSNAKE\JuegoSnake\resources\punteo.txt";
        }

        //Para leer el archivo de texto
        public int obtenerPunteo()
        {
            TextReader leer = new StreamReader(direccion);//Indica que lea el archivo de texto
            int dato = Convert.ToInt32(leer.ReadLine());//Convierte la variable leer de tipo TextReader a tipo int
            leer.Close();//Cerramos el archivo
            return dato;
            
        }

        //Para que escriba en el archivo de texto el punteo maximo obtenido
        public int punteoMaximo(int punteoActual)
        {
            //El punteo no cambiara hasta que alcance un nuevo record en el juego

            if(punteoActual > obtenerPunteo())//Si es mayor al ultimo punteo leido
            {
                TextWriter escribir = new StreamWriter(direccion);//Indica que escriba en el archivo de texto
                escribir.WriteLine(punteoActual);//Le indico que me escriba el punteo actual obtenido
                escribir.Close();//Cerramos el archivo
                return obtenerPunteo();
            }
            else
            {
                return obtenerPunteo();
            }
        }
    }
}
