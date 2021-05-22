using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;
using System.Threading;


namespace JuegoSnake.clases.BicolaEnlazada
{

    class SnakeConBicola
    {
        private int vidas = 3;
        private int punteo = 0;

        //Enumeracion interna para las direcciones que vayamos a tomar con las teclas
        internal enum Direction
        {
            Abajo, Izquierda, Derecha, Arriba
        }


        private static void DibujaPantalla(Size size)//Size para poder establecer el alto y el ancho del control desde el Point que especifiquemos
        {
            Console.Title = "Culebrita comelona";
            Console.WindowHeight = size.Height + 2;//establece la altura del área de la ventana de la consola.
            Console.WindowWidth = size.Width + 2;//establece el ancho de la ventana de la consola.
            Console.BufferHeight = Console.WindowHeight;//establece la altura del área de búfer.
            Console.BufferWidth = Console.WindowWidth;//establece el ancho del área de búfer.
            Console.CursorVisible = false;//establece el cursor visible en false.
            Console.BackgroundColor = ConsoleColor.Magenta;//Color del borde de la ventana
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.Black; //Color de fondo

            //Recorremos la altura y el ancho establecidos 
            for (int row = 0; row < size.Height; row++)//Altura
            {
                for (int col = 0; col < size.Width; col++)//Ancho
                {
                    Console.SetCursorPosition(col + 1, row + 1); //Movemos el cursor a estas coordenadas de la pantalla
                    Console.Write(" ");
                }
            }
        }


        //Para el cuadrito de puntuacion
        private static void MuestraPunteo(int punteo)
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(1, 0);
            Console.Write($"Puntuacion: {punteo.ToString("00000000")}");
        }


        //Para mostrar el cuadrito de puntuacion
        private static void MuestraPunteoK(int punteo, int vidas)
        {

            Archivo archivo = new Archivo();//Instanceamos la clase Archivo
            Console.BackgroundColor = ConsoleColor.Cyan;//Color del cuadrito donde me va a mostrar el punteo
            Console.ForegroundColor = ConsoleColor.Black;//Color de letra
            Console.SetCursorPosition(1, 0);//Colocamos la puntuacion actual a estas coordenadas de la pantalla
            Console.Write($"Puntuacion: {punteo.ToString("00000000")}");//Mostramos el punteo Actual
            Console.SetCursorPosition(25, 0);//Colocamos el cuadrito a estas coordenadas de la pantalla
            Console.Write($"Maximo: {archivo.punteoMaximo(punteo)}");//Mostramos el punteo Maximo
            Console.SetCursorPosition(40, 0);//Colocamos el cuadrito de puntuacion Maxima a estas coordenadas de la pantalla

            switch (vidas)//Evaluamos las vidas Para mostrar los corazones
            {
                case 1:
                    Console.Write($"{(char)3} ");//Si solo tenemos una vida me apraecera solo un corazon
                    break;
                case 2:
                    Console.Write($"{(char)3} {(char)3} ");//Si tenemos dos vidas dos corazones
                    break;
                case 3:
                    Console.Write($"{(char)3} {(char)3} {(char)3}");//Si tenemos las 3 vidas tres corazones
                    break;
            }
        }


        //Para obtener la direccion que tomara la culebra dependiendo de la tecla que presionemos
        private static Direction ObtieneDireccion(Direction direccionAcutal)
        {
            if (!Console.KeyAvailable) return direccionAcutal;//KeyAvailable indica si hay alguna tecla disponible para ser leída

            var tecla = Console.ReadKey(true).Key; //lee una tecla desde teclado.

            switch (tecla)//Evaluamos la tecla que presionemos
            {
                case ConsoleKey.DownArrow://Flecha hacia abajo
                    if (direccionAcutal != Direction.Arriba)
                        direccionAcutal = Direction.Abajo;//Tomara la direccion hacia abajo
                    break;
                case ConsoleKey.LeftArrow://Flecha izquierda
                    if (direccionAcutal != Direction.Derecha)
                        direccionAcutal = Direction.Izquierda;
                    break;
                case ConsoleKey.RightArrow://Flecha Derecha
                    if (direccionAcutal != Direction.Izquierda)
                        direccionAcutal = Direction.Derecha;
                    break;
                case ConsoleKey.UpArrow://Flecha hacia arriba
                    if (direccionAcutal != Direction.Abajo)
                        direccionAcutal = Direction.Arriba;
                    break;
            }
            return direccionAcutal;
        }


        //Para obtener la siguiente direccion que tomara la culebra dependiendo de la tecla que presionemos
        private static Point ObtieneSiguienteDireccion(Direction direction, Point currentPosition)//Establecer las coordenadas
        {
            Point siguienteDireccion = new Point(currentPosition.X, currentPosition.Y);//Establecer un par ordenado de coordenadas x e y 
            
            switch (direction)//Evaluamos la direccion que tomara
            {
                case Direction.Arriba:
                    siguienteDireccion.Y--;//Decrementamos la coordenada Y
                    break;
                case Direction.Izquierda:
                    siguienteDireccion.X--;
                    break;
                case Direction.Abajo:
                    siguienteDireccion.Y++;
                    break;
                case Direction.Derecha:
                    siguienteDireccion.X++;
                    break;
            }
            return siguienteDireccion;
        }



        private static bool MoverLaCulebrita(BiCola culebra, Point posiciónObjetivo,
            int longitudCulebra, Size screenSize)
           
        {

            //Casteamos el metodo finalBicola con Point para Establecer las coordenadas
            //En lugar de .Last usamos el metodo finalBicola
            var lastPoint = (Point) culebra.finalBicola();

            if (lastPoint.Equals(posiciónObjetivo)) return true;//Comparamos los valores

            if (culebra.Any(posiciónObjetivo)) return false;
           

            if (posiciónObjetivo.X < 0 || posiciónObjetivo.X >= screenSize.Width
                    || posiciónObjetivo.Y < 0 || posiciónObjetivo.Y >= screenSize.Height)
            {
                return false;
            }

            Console.BackgroundColor = ConsoleColor.Green;//Color del cuerpo de la culebra
            Console.SetCursorPosition(lastPoint.X + 1, lastPoint.Y + 1);//Movemos el cursor a estas coordenadas de la pantalla
            Console.WriteLine(" ");

            //Usamos el metodo de insertar en lugar de Enqueue
            culebra.insertar(posiciónObjetivo);

            Console.BackgroundColor = ConsoleColor.Red;//Color de la cabecita
            Console.SetCursorPosition(posiciónObjetivo.X + 1, posiciónObjetivo.Y + 1);//Movemos el cursor a estas coordenadas de la pantalla
            Console.Write(" ");

            // Quitar cola
            //Usamos el metodo de numElementosBicola de la clase Bicola en lugar de Count
            if (culebra.numElementosBicola() > longitudCulebra)
            {
                //Usamos el metodo de quitar de la clase Bicola en Lugar de Dequeue
                var removePoint = (Point) culebra.quitar();
                Console.BackgroundColor = ConsoleColor.Black;//Color del recorrido de la culebra
                Console.SetCursorPosition(removePoint.X + 1, removePoint.Y + 1);//Movemos el cursor a estas coordenadas de la pantalla
                Console.Write(" ");
            }
            return true;
        }

        private static Point MostrarComida(Size screenSize, BiCola culebra)
        {
            
            var lugarComida = Point.Empty;//Indica un punto que tiene los valores X e Y establecidos en cero

            var cabezaCulebra = (Point) culebra.finalBicola();//Usamos el metodo de finalBicola de la clase Bicola en lugar de Last
            var coordenada = cabezaCulebra.X;

            var rnd = new Random(); //Para que la comida se muestre cada ves en una posicion aleatoria

            do
            {
                //permite que se indiquen dos valores, de esa forma sacará un número aleatorio que esté dentro del rango de esos números indicados.
                var x = rnd.Next(0, screenSize.Width - 1);//Alto
                var y = rnd.Next(0, screenSize.Height - 1);//Ancho
                
                if (culebra.ToString().All(x => coordenada != x || coordenada != y)
                    && Math.Abs(x - cabezaCulebra.X) + Math.Abs(y - cabezaCulebra.Y) > 8)
                {
                    lugarComida = new Point(x, y);
                    Console.Beep(659, 125);//Emitir sonido cuando la culebra come

                }
                //Se implementara mientras
            } while (lugarComida == Point.Empty);

            Console.BackgroundColor = ConsoleColor.Blue;//Color del cuadrito de comida
            Console.SetCursorPosition(lugarComida.X + 1, lugarComida.Y + 1);
            Console.Write(" ");

            return lugarComida;
        }

        
        public void jugarConIntentos()
        {
            var velocidad = 100; //MIENTRAS MAS ALTO SEA EL NUMERO MAS LENTO SE PONE Y MIENTRAS MAS BAJO ES EL NUMERO SE PONE MAS RAPIDO
            var posiciónComida = Point.Empty;//Indicar un punto que tiene los valores X e Y establecidos en cero
            var tamañoPantalla = new Size(60, 20);//Alto y ancho de la consola
            
            var culebrita = new BiCola();

            var longitudCulebra = 3; //ESTABBLECEMOS EL TAMAÑO DEL CUERPO DE LA CULEBRA CON EL QUE VA A COMENZAR
            var posiciónActual = new Point(0, 9); //ESTABLECEMOS EN QUE POSICION COMENZARA A SALIR LA CULEBRA
            
            culebrita.insertar(posiciónActual);//ENCOLANDO

            var dirección = Direction.Derecha; //LA CULEBRA COMIENZA A SALIR DEL LADO DERECHO PORQUE LE TENEMOS.DERECHA

            DibujaPantalla(tamañoPantalla);//Dibujamos pantalla con lo que establecimos de la variable tamañoPantalla
            MuestraPunteoK(punteo, vidas);//Mostramos el punteo delo que establecimos de la variable punteo

            //Para que el juego se repita mientras tenga vidas
            while (vidas != 0)
            {
                bool juegarsi = MoverLaCulebrita(culebrita, posiciónActual, longitudCulebra, tamañoPantalla);

                if (juegarsi)
                {
                    Thread.Sleep(velocidad);//Suspende el subproceso actual durante el período de tiempo especificado en velocidad.
                    dirección = ObtieneDireccion(dirección);
                    posiciónActual = ObtieneSiguienteDireccion(dirección, posiciónActual);

                    if (posiciónActual.Equals(posiciónComida))//Comparamos los objetos
                    {
                        posiciónComida = Point.Empty; //Indica un punto que tiene los valores X e Y establecidos en cero
                        longitudCulebra++; //Incrementamos el cuerpo de la culebra cada ves que come
                        punteo += 10; //VA SUBIENDO EL PUNTEO DE 10 EN 10 CADA VES QUE COME
                        
                        MuestraPunteoK(punteo, vidas);//Mostramos el punteo y las vidas

                        velocidad -= 10;//Sube de velocidad cada ves que come
                    }

                    //MUESTRA LA COMIDA 
                    if (posiciónComida == Point.Empty) //Indica un punto que tiene los valores X e Y establecidos en cero
                    {
                        posiciónComida = MostrarComida(tamañoPantalla, culebrita);
                    }
                }
                else//Cada ves que pierda
                {
                    vidas--;//Me restara una vida
                    Console.ResetColor();//Establece los colores de la consola en sus valores predeterminados.
                    Console.SetCursorPosition(tamañoPantalla.Width / 2 - 15, tamañoPantalla.Height / 2);
                    
                    if (vidas == 0)
                    {
                        Console.SetCursorPosition(tamañoPantalla.Width / 2 - 4, tamañoPantalla.Height / 2);
                        Console.Write($"¡GAME OVER!");
                    }
                    else
                    {
                        Console.Write($"Haz perdido una vida, te quedan {vidas}");
                    }

                    Thread.Sleep(2000);//Suspende el subproceso actual durante el período de tiempo especificado en velocidad.
                    
                    Console.ReadKey();

                    jugarConIntentos(); //Mientras tenga vidas volvemos a jugar
                }

            }

        }


    }
}


